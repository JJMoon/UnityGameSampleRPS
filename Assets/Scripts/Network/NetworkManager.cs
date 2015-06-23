// [2013:11:19:MOON<Start>]
using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Net.Sockets;
using System.Linq;
using System.Runtime.Serialization;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  NetworkManager  _____  Class  _____
public class NetworkManager
{
    public bool mNetDebug = true;
    //public AmNetBase mAliveNetThread = null;
    //  _////////////////////////////////////////////////_    _____   Protected   _____   Variables   _____
    protected string myName;
    protected StateArray mStt;
    protected byte[] mNetBuffer = new byte[1024];
    // Timing
    protected int INT_REST = 1500, INT_IDLE = 5000;
    protected int mInterbal, LIMIT_IDLE = 30, mLimitPacketWaiting = 100;
    protected string mEndLog = " _____[ N ][ E ][ T ]_____";
    //  _////////////////////////////////////////////////_    _____   Private   _____   Variables   _____
    //int[] waitMSec = new int[] { 100, 100, 200, 500, 1000, 2000, 5000, 10000, 10000 };
    int[] waitMSec = new int[] { 100, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 5000, 10000, 10000 };
    Thread mThread;
    bool mThreadEnd = false;
    AmNetUnitJob CurJob;
    int mCounter;
    List<AmNetUnitJob> arrJobs = new List<AmNetUnitJob> ();
    List<AmNetUnitJob> arrDeleted = new List<AmNetUnitJob> ();
    //  _////////////////////////////////////////////////_    _____   Creation   _____   Methods   _____
    public NetworkManager ()
    {
        Ag.LogString ("NetworkManager :: Creation ..... ");
        mInterbal = INT_REST;

        GenState ();
        StartThread ();
    }

    ~NetworkManager ()
    {
        Ag.LogIntense (5, true);
        Ag.LogString ("NetworkManager :: ~~~ Destruct ..... ");
        mThread.Abort ();
        mThread = null;
        //mTcpClient = null;
    }

    public void AddAUnitJob (AmNetUnitJob aJob)
    {
        arrJobs.Add (aJob);
        SetState ("PacketSend");
        mStt.DoAction ();
    }
    //  _////////////////////////////////////////////////_    _____   Send / Receive   _____   Methods   _____
    public void SendPacket ()
    {
        if (arrJobs.Count > 0) {
            CurJob = arrJobs [0];
            CurJob.SendJob ();
        }
        SetState ("Receive");
    }
    //  _////////////////////////////////////////////////_    _____   Virtual   _____   Methods   _____
    public virtual void LimitParseWaiting ()
    {
        //AgStt.muiHQ.FatalError(this, Error.NET_FAIL);
        SetState ("Offline");
    }

    public virtual void LimitOnlineWaiting ()
    {
        // Heart beat check in "Online" State
    }

    public bool IsOnline ()
    {
        switch (GetStateName ()) {
        case "Offline":
            return false;
        default:
            return true;
        }
    }

    public bool IsFree () // 패킷이 없어 노는 상태
    {
        if (arrJobs.Count == 0 && mStt.GetCurStateName () == "Online")
            return true;

        return false;
    }

    public string GetStateName ()
    {
        return mStt.GetCurStateName ();
    }

    public void SetState (string pStateName)
    {
        mStt.SetStateWithNameOf (pStateName);
    }

    public virtual void GenState ()
    {
        mStt = new StateArray ();

        mStt.AddAMember ("Online", 0);
        mStt.AddEntryAction (() => {
            mInterbal = INT_REST;
        });
        mStt.AddDuringAction (() => {
            if (arrJobs.Count > 0) {
                SetState ("PacketSend");  // Change State
            }
        });
        mStt.AddAMember ("PacketSend", 0);
        mStt.AddEntryAction (() => {
            SendPacket ();
            mInterbal = waitMSec [0];
        });
        mStt.AddAMember ("Receive", 0);
        mStt.AddDuringAction (() => {
            if (CurJob.dlgtJobCompleted ())
                PrepareNextJob ();
            else
                RetryJob ();
        });
        mStt.AddAMember ("ERROR", 0);
        mStt.AddEntryAction (() => {
            mInterbal = INT_REST;
        });
        SetState ("Online");  // Set Initial State..
        StartThread ();     // Start Thread
    }

    void PrepareNextJob ()
    {
        CurJob.AfterJob ();

        if (CurJob.NextJob == null) {
            arrJobs.Remove (CurJob);
            SetState ("Online");  // Change State
            return;
        } 
        arrJobs [0] = CurJob.NextJob;
        SetState ("PacketSend");  // Change State
        return;
    }

    void RetryJob ()
    {
        mInterbal = waitMSec [CurJob.RetryCnt];
        Ag.LogString (" Retry Job :: Count " + CurJob.RetryCnt);
        if (CurJob.RetryCnt > 6) {
            CurJob.BeforeRetryAction ();
            Ag.LogString (" Retry Job :: BeforeRetryAction ");
            SetState ("ERROR");
            //SendPacket ();
            //SetState ("PacketSend");  // Change State
        }
        CurJob.SendJob ();
    }

    protected void LogCurrentState (string pStt)
    {
        Ag.LogString ("NetworkManager :: LogCurrentState :: " + pStt + "  NetName :: >>" + myName + "<<  [" + mStt.GetCounter () + "]  " + mEndLog);
    }
    //  _////////////////////////////////////////////////_    _____   Network   _____   Methods   _____
    void StartThread ()
    {
        Ag.LogIntense (10, true);
        Ag.LogIntenseWord ("Start Thread");
        if (mThread != null)
            return;
        mThread = new Thread (ThreadWorker);
        mThread.Start ();
    }

    public void StopNetwork ()
    {
        Ag.LogIntenseWord ("NetworkManager :: StopNetwork   " + myName);
        if (mThread != null) {
            Ag.LogString ("  Abort Thread ");
            mThreadEnd = true;
            //mThread.Abort ();
        }
        mThreadEnd = true;
        mInterbal = INT_REST;   // Time Interbal Set <><>
        mStt.SetStateWithNameOf ("Online");
    }

    void RemoveReceivedPack ()
    {
        foreach (AmNetUnitJob unit in arrJobs) {
            //("  AmNetBase :: RemoveReceivedPack()    ____   Unit :: " + unit.mPID + " , Stage is " + unit.mStage + " .       _____     Is it SendOnly ? ==> " + unit.mIsSendOnly).HtLog();
            if (unit.dlgtJobCompleted ()) {
                //("  AmNetBase :: RemoveReceivedPack()    Delete Unit :: " + unit.mPID + " whose Stage is " + unit.mStage + "   "  ).HtLog();
                arrDeleted.Add (unit);
                arrJobs.Remove (unit);
                return;
            }
            if (unit.sendOnly) {  //unit.mStage == AmPackUnit.Stage.SENT && unit.mIsSendOnly) {
                ("  NetworkManager :: RemoveReceivedPack()    ____   Unit :: " + unit.jobName + " .       _____     Is it SendOnly ? ==> " + unit.sendOnly).HtLog ();
                arrDeleted.Add (unit);
                arrJobs.Remove (unit);
                return;
            }
        }
        if (arrDeleted.Count > 20) {
            //DebugCurPack();
            arrDeleted.RemoveRange (0, 10);
        }
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Thread  _____  Loop  _____
    void ThreadWorker ()
    {
        //Catch and report any exceptions here, 
        //so that Unity doesn't crash!
        try {
            _ThreadWorker ();
        } catch (Exception e) {
            if (!(e is ThreadAbortException))
                Debug.LogError ("Unexpected Death: " + e.ToString ()); 
        }
    }

    void _ThreadWorker ()
    {
        while (true) {
            if (mThreadEnd)
                return;

            mCounter++;

            Thread.Sleep (mInterbal);

            if (mNetDebug)
                Debug.Log ("___   >>>   { " + mCounter + " }   <<<       arrJobs :: " + "<<< [  " + arrJobs.Count + "  ]  ||||| Work Stage >> " +
                mStt.GetCurStateName () + "  |||    << ");

            mStt.DoAction ();
        }
    }
}