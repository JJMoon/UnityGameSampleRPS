// [2014:5:20:MOON<Start>]
using System;
using UnityEngine;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  MtPackManager  _____  Class  _____
public partial class MtPackManager : AmSceneBase
{
    public bool mNetDebug = true;
    public Action NetworkFailedJob;
    //public AmNetBase mAliveNetThread = null;
    //  _////////////////////////////////////////////////_    _____   Protected   _____   Variables   _____
    protected string myName;
    protected StateArray mStt;
    protected byte[] mNetBuffer = new byte[1024];
    // Timing
    int SendWaitingCount;
    protected int INT_REST = 1500, INT_IDLE = 5000;
    protected int mInterbal, LIMIT_IDLE = 30, mLimitPacketWaiting = 100, mInEverySomeFrame = 50;
    protected string mEndLog = " _____[ N ][ E ][ T ]_____";
    //  _////////////////////////////////////////////////_    _____   Private   _____   Variables   _____
    //int[] waitMSec = new int[] { 100, 100, 200, 500, 1000, 2000, 5000, 10000, 10000 };
    bool mThreadEnd = false;
    AmNetUnitJob CurJob;
    List<AmNetUnitJob> arrJobs = new List<AmNetUnitJob> ();
    List<AmNetUnitJob> arrDeleted = new List<AmNetUnitJob> ();
    float[] FrameRate = new float[30];
    AmUI BotGUI = new AmUI ();
    //  _////////////////////////////////////////////////_    _____   Connection   _____   URI   _____
    string Dns, Lun;

    //  _////////////////////////////////////////////////_    _____   Creation   _____   Methods   _____
    public override void Start ()
    {
        Ag.LogIntenseWord ("NetworkManager :: Creation ..... ");
        mInterbal = INT_REST;

        if (AgStt.NetManager == null) {
            //AgStt.NetManager = this;
            DontDestroyOnLoad (this.gameObject);
        } else
            Destroy (gameObject);

        if (Ag.mIsDebug)
            GitIgnoreThis.GitIgnoreSetup ();

        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            AgStt.FpLoginIOS.CheckRootingJailbreak ();
        #endif

        AgStt.IntendedPause = false;

        GenState ();
        myGUI.SetColumns (2, 5);
        BotGUI.SetColumns (1, 12);
    }

    public override void BaseStartSetting ()
    {
        Ag.Dic ["Example"] = new Dictionary <string, GameObject> ();

        Ag.Lst ["SomeList"] = new List <GameObject> ();

        // Load GameObject

        Ag.Dic ["Example"] ["SomeGameObject"] = GetPrefabAt ("folder", "name"); // null; // FindMyChild ();

        StartCoroutine ("LoadGameObjects");
    }

    public void AddAUnitJob (AmNetUnitJob aJob)
    {
        arrJobs.Add (aJob);
        mInEverySomeFrame = 5;
        mCounter -= mCounter % 5;
        mCounter += 3;
        SetState ("PacketSend");
        //mStt.DoAction ();
    }
    //  _////////////////////////////////////////////////_    _____   Send / Receive   _____   Methods   _____
    public void SendPacket ()
    {
        if (arrJobs.Count > 0) {
            CurJob = arrJobs [0];
            WasObject curWas = (WasObject)CurJob.Content;
            //Ag.LogString ("  delg will send >>> " + curWas.dlgt_WillSend);
            if (curWas.dlgt_WillSend != null && !curWas.dlgt_WillSend ()) {
                arrJobs.Remove (CurJob);
                Ag.LogDouble ("    NetworkManagerMono  :: Send Pack   " + curWas.GetType () + "    Will    NOT    be sent   >>>>   Cancel Case ... ");
                SetState ("Online");
                return;
            }
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
        Ag.LogIntenseWord (" NetworkManagerMono :: GenState ");
        mStt = new StateArray ();

        mStt.AddAMember ("Online", 0);
        mStt.AddEntryAction (() => {
            mInEverySomeFrame = 5;
        });
        mStt.AddDuringAction (() => {
            if (arrJobs.Count > 0) {
                SetState ("PacketSend");  // Change State
            } else {
                mInEverySomeFrame = 100;
            }
        });
        mStt.AddExitCondition (() => {
            return false;
        });
        mStt.AddAMember ("PacketSend", 0);
        mStt.AddEntryAction (() => {
            //SendWaitingCount = 0;
            SendPacket ();
        });
        mStt.AddDuringAction (() => { 
            SendWaitingCount++;
            Ag.LogDouble ("  ::  PacketSend  >>>   SendWaitingCount : " + SendWaitingCount);
            if (SendWaitingCount > 5) {
                arrJobs.Remove (CurJob);
                //Ag.NetExcpt.DisconnectedWith (was: true);
                SetState ("Online");
            }
        });
        mStt.AddAMember ("Receive", 0);
        mStt.AddDuringAction (() => {
            SendWaitingCount = 0;
            if (CurJob.dlgtJobCompleted ())
                PrepareNextJob ();
            else {
                arrJobs.Remove (CurJob);
                SetState ("Online");  // Change State
                return;
                //                if (NetworkFailedJob != null)
                //                    NetworkFailedJob ();
                // RetryJob ();

                //Ag.HttpNetworkFailure = true;
            }
        });
        mStt.AddAMember ("ERROR", 0);
        mStt.AddEntryAction (() => {
            mInEverySomeFrame = 150;
        });
        SetState ("Online");  // Set Initial State..
        mStt.DoAction ();

    }

    void PrepareNextJob ()
    {
        CurJob.AfterJob ();

        if (CurJob.NextJob == null) {
            //Ag.LogString (" NetworkManagerMono :: PrepareNextJob >> CurJob.NextJob == null ");
            //Ag.LogString (" NetworkManagerMono :: PrepareNextJob >>  " + CurJob.GetType () + "  is being Removed  ____   ");
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
        //mInEverySomeFrame += mInEverySomeFrame > 50 ? 5 : 10;
        mInEverySomeFrame += 40;
        Ag.LogString (" Retry Job :: Count " + CurJob.RetryCnt);
        if (CurJob.RetryCnt > 6) {
            CurJob.BeforeRetryAction ();
            Ag.LogString (" Retry Job :: BeforeRetryAction ");
            SetState ("ERROR");
            arrJobs.Clear ();
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
    public void StopNetwork ()
    {
        Ag.LogIntenseWord ("NetworkManager :: StopNetwork   " + myName);
        mThreadEnd = true;
        //mInterbal = INT_REST;   // Time Interbal Set <><>
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

    DateTime PauseStartTime = DateTime.Now;
    // 리부팅 할 지 여부를 포커스 잃기 전에 판단하자.
    bool willReboot = false;

    public void OnApplicationPause (bool pauseStatus)
    {
        if (AgStt.NetManager == null)
            return;

        Ag.LogIntenseWord (" OnApplicationPause        Status  Going Out ?  ::  " + pauseStatus);

        //#if UNITY_IPHONE
        //if (Application.platform == RuntimePlatform.IPhonePlayer && AgStt.IntendedPause) {
        if (AgStt.IntendedPause) {
            Ag.LogIntenseWord (" OnApplicationPause   Skip     AgStt.IntendedPause  is   True    || Intended..     ");
            if (pauseStatus)
                AgStt.NodeClose ();
            else {
                AgStt.NodeOpen ();
                AgStt.IntendedPause = false;
            }
            return;
        }
        //#endif

        if (pauseStatus) {  // going out  // iPhone Home Button is pressed ...
            AgStt.NodeClose ();

            willReboot = Ag.GameStt.WillLoadTitleScene;

            PauseStartTime = DateTime.Now;

            //if (AgStt.IsGaming.HasValue && AgStt.IsGaming.Value) { // Escape during Gaming..
            //if (Ag.GameStt.WillSendWasGameReport) { // Escape during Gaming..
            //if (Application.loadedLevelName == "GameScene") {
            //GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["popup"].SetActive (true);
            //GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["alert_out"].SetActive (true);

            //                WasGameReport aObj = new WasGameReport () {
            //                    User = Ag.mySelf, winnerID = Ag.NetExcpt.GamingEnemyID, loserID = Ag.mySelf.WAS.KkoID,
            //                    winPo = Ag.NetExcpt.GamingEnemyPoint, losPo = 0
            //                };
            //                aObj.messageAction = (int pInt) => {
            //                };
            //                arrJobs [arrJobs.Count - 1].SendJob ();




            //} else {   502
            //GameObject.Find ("Axis/Camera/Match").GetComponent<MenuManager> ().MenuCommonOpen ("alert_someoneout", "Ui_popup", true);  502
            //}  
            //}
        } else {  // came back
            AgStt.NodeOpen ();
            Ag.GameStt.FocusBack ();

            if (arrJobs != null)
                arrJobs.Clear ();

            if (willReboot) { // || (Ag.mSingleMode && Ag.mVirServer.maiGradeOfBot >= 0)) {
                Ag.LogIntenseWord ("  NetworkManager Mono  ::: ... WillLoadTitleScene >>>   true ");
                Application.LoadLevel ("Title");
                return;
            }
            if ((DateTime.Now - PauseStartTime).TotalSeconds > 1200) {  // 1200  20 minutes 
                Ag.PlatformLogout = true;
                Application.LoadLevel ("Title");
                return;
            }
            AutoLoginProcess ();
        }
    }

    void AutoLoginProcess ()
    {
        WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };
        aObj.messageAction = (int pInt) => {
            if (pInt == 0) {
                WasUserInfo uObj = new WasUserInfo () { User = Ag.mySelf, flag = 0 };
                uObj.messageAction = (int uInt) => {
                    Ag.LogString (" User Info OK ");
                };
                WasItemInfo bObj = new WasItemInfo () { User = Ag.mySelf };
                bObj.messageAction = (int xpInt) => {
                };
            }
            aObj = null;

        };
    }

    public override void OnApplicationQuit ()
    {
        Ag.LogIntenseWord (" OnApplicationQuit");
        AgStt.NodeClose ();
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Thread  _____  Loop  _____
    int hour = -1;
    string everyHour = "";
    bool HourlyEvent = false;

    public bool HaveSeenHourlyEvent { set { HourlyEvent = value; } }

    public override void Update ()
    {
        mCounter++;

        if (AgStt.DebugOnDevice) {
            int cur = (int)(mCounter % 30);
            FrameRate [cur] = Time.time;

            float last = cur == 0 ? FrameRate [29] : FrameRate [cur - 1];
            float before = cur == 29 ? FrameRate [0] : FrameRate [cur + 1];
            AgStt.FpsLast = 1f / (FrameRate [cur] - last);
            AgStt.FramePS = 30f / (FrameRate [cur] - before);
        }

        if (mThreadEnd) {
            mInEverySomeFrame++;
            return;
        }

        if (mCounter % mInEverySomeFrame != 1)
            return;

        //        string sceneName = "";
        //        if (Application.platform == RuntimePlatform.OSXEditor)
        //            sceneName = EditorApplication.currentScene;
        //        else
        //            sceneName = Application.loadedLevelName;
        //        //Ag.LogString (sceneName + "   index " + sceneName.IndexOf ("Game") );
        //        if (sceneName.IndexOf ("Game") > 0)
        //            Ag.CurrentScene = "GAME";
        //        else
        //            Ag.CurrentScene = "MENU";

        // 30 1 sec.   정각보상  // 오전 8시~오후 11시   // Sleep Check ...
        if (mCounter % 200 == 1) {
            DateTime iNow = DateTime.Now;
            //Ag.LogString ("  Now  ::   " + iNow + "   Min : ?  " + (iNow.Minute == 0) + "  Hour  " + (iNow.Hour != hour) + "    Node ?   " + (Ag.NodeObj != null));
            //everyHour = "  Now  :: >> " + iNow + " <<      Min : < " + (iNow.Minute == 0) + " >       Hour : < " + (iNow.Hour != hour) + " >        Node :  < " + (Ag.NodeObj != null) + " >";

            //            TimeSpan deActiveTS = DateTime.Now - Ag.NetExcpt.WASActionTime;
            //            if (deActiveTS.TotalSeconds > 1200)
            //                Application.LoadLevel ("Title");
            //
            if (iNow.Minute == 0 && iNow.Hour != hour && Ag.NodeObj != null && (8 <= iNow.Hour && iNow.Hour <= 23)) {
                WasCodeOnlyProtocol aObj = new WasCodeOnlyProtocol () { User = Ag.mySelf, protoCode = 610 };
                aObj.messageAction = (int pInt) => {
                    if (pInt == 0) {
                        hour = iNow.Hour;
                        if (Ag.CurrentScene == "MENU")
                            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["LPanel_olclock"].SetActive (true);
                        else
                            HourlyEvent = true;
                        Ag.LogIntenseWord ("   >>>>>     " + Tbl.dicHanLog ["EveryHourEvent"] + "       Result ::  " + pInt);
                    }
                    aObj = null;
                };
            }

            if (HourlyEvent && Application.loadedLevelName == "StartMenu")
                FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["LPanel_olclock"].SetActive (true);
        }

        if (mNetDebug && mStt != null)
            ("< Net > >>  { " + mCounter + " }   <<<  [arrJobs.Count ::  " + arrJobs.Count + " ]    @ Scene : " + Ag.CurrentScene
            //+ "    \t State >> " + mStt.GetCurStateName () + "    <<  >>>  " + everyHour).HtLog ();
                + "    \t " + "  <<  >>>  " + everyHour).HtLog ();

        everyHour = " .. ";

        if (mStt != null)
            mStt.DoAction ();
        if (mCounter > 1000000)
            mCounter = 0;
    }

    public override void OnGUI ()
    {
        Rect curRUp = BotGUI.GetRect (0, 10); // Bot Test Tool...  Skip Tutorial 
        int colN = 2, colEA = 5;

        /*
        if (GUI.Button (BotGUI.DivideRect (curRUp, colEA, colN++), Ag.BotTestSetting + " : B : " + Ag.mVirServer.maiGradeOfBot)) {  // Update
            Ag.BotTestSetting++;
            Ag.BotTestSetting = (Ag.BotTestSetting > 4) ? -1 : Ag.BotTestSetting;
        }
        if (GUI.Button (BotGUI.DivideRect (curRUp, colEA, colN++), "Tutor")) { 
            PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", true);
            PreviewLabs.PlayerPrefs.Flush ();
        }
        */


        if (!(Ag.mIsDebug && AgStt.DebugOnDevice))
            return;

        muiCol = 0;
        muiRow = 0;
        GUI.contentColor = Color.yellow;

        int idx = 0;
        GUI.Button (myGUI.GetRect (muiCol++, muiRow), AgStt.GetLogString (idx++, true));
        GUI.Button (myGUI.GetRect (muiCol, muiRow), AgStt.GetLogString (idx, true));
        idx = muiCol = 0;
        muiRow = 1;
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        muiCol = muiRow = 1;
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));
        GUI.Button (myGUI.GetRect (muiCol, muiRow++), AgStt.GetLogString (idx++));

    }
}