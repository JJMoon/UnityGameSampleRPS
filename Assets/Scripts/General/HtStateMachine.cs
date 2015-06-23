// [2012:10:10:MOON] Heart Beat
// [2012:10:11:MOON] Interrupt Twisting.. 
// [2012:10:15:MOON] XXX
// [2012:11:13:MOON] Change State
// [2013:1:10:MOON] Integration with Networks
// [2013:1:21:MOON] mCounter
// [2013:12:3:MOON<Packet 제거>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Base State  <<<<<
public class BaseState
{
    //  ////////////////////////////////////////////////     Public
    public BaseState mExitState;
    // Never Change this...[2012:10:11:MOON] Interrupt Twisting..
    public BaseState mTempExitState;
    public bool mDidExecute_Entry = false, mIsExitStateChanged = false, mIsFsmDebug = true, mIsDebug = false, mNoDebug = true;
    public string mName;
    //public AmPack mPackOfState;
    public bool mIsPacketType = false;
    public Action mfnTimeOutProcess;
    public float mfLimitTime;
    public int mHowManyEntryDone;
    public ulong mCounter;
    //  ////////////////////////////////////////////////     Private  **
    string mMarkSign = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
    //  ////////////////////////////////////////////////     Protected  **
    protected AgTime mTimer;
    protected float mfTime;
    protected DateTime mEntryTime;
    //  ////////////////////////////////////////////////     Creation
    public BaseState ()
    {
        mHowManyEntryDone = 0;
        mfLimitTime = -1f;
    }

    public BaseState (string pName)
    {
        mHowManyEntryDone = 0;
        mfLimitTime = -1f;
        mName = pName;
    }

    public BaseState (string pName, float pTimerSet)
    {
        mHowManyEntryDone = 0;
        mfLimitTime = -1f;
        mName = pName;
        mfTime = pTimerSet;
    }

    void MarkSign ()
    {
        if (mNoDebug)
            return;

        if (mIsDebug)
            Debug.Log (mMarkSign + mMarkSign + mMarkSign + mMarkSign + "  Debug ..... \n");
        else
            Debug.Log (mMarkSign + mMarkSign + mMarkSign + mMarkSign + " \n");
    }
    //  ////////////////////////////////////////////////     Main Actions...
    public virtual BaseState Action ()
    {
        mIsExitStateChanged = false;
        if (!mDidExecute_Entry) {
            //if (mIsPacketType) EntryActionPacket();
            EntryAction ();
        }
        if (mIsExitStateChanged) {
            mIsExitStateChanged = false;

            //Ag.LogString ("### BaseState::Exit 01 >> " + mName);
            return mTempExitState;
        }
        
        if (mfLimitTime > 0) { // Limit Time is set !!!
            TimeSpan spanT = DateTime.Now - mEntryTime;
            if (spanT.TotalMilliseconds > mfLimitTime * 1000f) {
                if (!mNoDebug) {
                    Ag.LogString (" ");
                    Ag.LogString (" ");
                    Ag.LogString (" ");
                    Ag.LogString (" ");
                    Ag.LogString (" ");
                    Debug.Log ("BaseState :: >>>  >>>>>>>>>>>>>>>>>>>>>  TimeOutProcess :: BaseState :: >>>  >>>>>>>>>>>>>>>>>>>>>   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  ");
                    Debug.Log ("BaseState :: >>>  >>>>>>>>>>>>>>>>>>>>>  TimeOutProcess :: " + mName + " <<<   Limit Time   >>> " + mfLimitTime + "  is Over  $$$$$$$ \n");
                    Debug.Log ("BaseState :: >>>  >>>>>>>>>>>>>>>>>>>>>  TimeOutProcess :: BaseState :: >>>  >>>>>>>>>>>>>>>>>>>>>   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  ");
                    Ag.LogString (" ");
                    Ag.LogString (" ");
                }
                
                if (mfnTimeOutProcess == null) {
                    mDidExecute_Entry = false;
                    return mExitState;
                } else
                    mfnTimeOutProcess ();
            }
        }
     
        DuringAction ();
        
        if (mIsExitStateChanged) {
            mIsExitStateChanged = false;
            //Debug.Log ("### BaseState::Exit  >>    " + mName);
            return mTempExitState;
        }
        
        if (ExitCondition ()) {
            ExitAction ();  // Stage can be changed here !!!   [2012:10:10:MOON] Heart Beat
            if (mIsExitStateChanged) {
                mIsExitStateChanged = false;
                //Debug.Log ("### BaseState::Exit  >>    " + mName);
                return mTempExitState;
            } else {
                return mExitState; // ExitAction();  // [2012:10:10:MOON] Heart Beat
            }
        }
        if (mIsExitStateChanged) {
            mIsExitStateChanged = false;
            //Debug.Log ("### BaseState::Exit  >>    " + mName);
            return mTempExitState;
        }
        return this;
    }
    //  ////////////////////////////////////////////////     Main Actions...  ^ % $ # ! $ ^    Interrupted
    public virtual void Interrupted (BaseState pTempExitState)
    {
        mIsExitStateChanged = true; 
        mDidExecute_Entry = false;
        pTempExitState.mDidExecute_Entry = false;
        mTempExitState = pTempExitState;  // [2012:10:11:MOON] Interrupt Twisting..
        string theMark = "=> => => "; //" ~ ~ ~ ";
        string theLine = theMark + theMark + theMark + theMark + theMark + theMark + theMark + theMark + theMark + theMark + theMark;
        theLine = theLine + theLine;

        if (mIsDebug)
            Ag.LogString ("BaseState :: >>> Interrupted :: _ _ _ _ _ _ _ _    From [  __  " + mName + "  __  ]  ==>>  To [  __  " +
            pTempExitState.mName + "  __  ]     >>>>\n");
    }

    public virtual void DuringAction ()
    {
        mCounter++;
        //Debug.Log("BaseState :: DuringAction  \n");
    }

    public virtual bool ExitCondition ()
    {  // Condition.. true or false...
        //Debug.Log("BaseState :: >>> Exit Condition :: " + mName + " <<<<<   BaseState  ...parent.. \n");
        if (mTimer != null)
            return mTimer.DidTimerFinished ();
        
        //Ag.LogString(" Exit at Base 000000 ");
        
        return false;
    }
    //  ////////////////////////////////////////////////     Entry Actions....
    void CommonEntryAction ()
    {
        mCounter = 0;  
        mEntryTime = DateTime.Now;
        mHowManyEntryDone++;
        mDidExecute_Entry = true;
        //  "SIGN_MARK    >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
        
        //string entryS = "V V V V V V V V V "; //"====== entry =====";


        //Ag.LogString ("BaseState :: [[[ Entry Action ]]]   >>>>> ");
        //Ag.LogString (entryS + entryS + entryS + entryS + entryS + entryS + entryS + entryS + entryS + "\n");
        if (mNoDebug)
            return;
        MarkSign ();
        if (mIsDebug)
            Ag.LogString ("BaseState :: [[[ Entry Action ]]]   >>>>> ___ " + mName + " _____  <<<<<  ____ E n t r y ____   \n");
    }

    public virtual void EntryAction ()
    {
        if (mIsDebug)
            Ag.LogString (". . . . .     EntryAction   . . . . .   \n");
        CommonEntryAction ();
        if (mfTime > 0.01) {
            mTimer = new AgTime ();
            mTimer.WaitTimeFor (mfTime);
        }
    }
    //    public virtual AmPack EntryActionPacket ()
    //    {
    //        CommonEntryAction ();
    //        //Ag.LogString("<<<<<<<<<<<<<<<<<<<  <<<<<<<<<<<<<<<<<<<  <<<<<<<<<<<<<<<<<<<  <<<<<<<<<<<<<<<<<<<      EntryActionPacket   >>> Packet <<<  \n");
    //
    //        if (!mNoDebug)
    //            Ag.LogString ("WWWWWWWWWWWWWWWWWWWWWWW     EntryActionPacket WWWWW>>> Packet <<<WWWWWWWWWWW \n");
    //        return mPackOfState;
    //    }
    //  ////////////////////////////////////////////////     Exit Actions....
    public virtual void ExitAction ()
    {
        //string entryS = "WWWWWWW exit WWWWWWWW";
        //string exitStr = ".......................................................";

        if (mIsDebug) {
            Debug.Log ("\n");
            MarkSign ();
            Debug.Log ("BaseState :: _____[[[ Exit Action ]]] ___________ " + mName + " ___________ ExitAction  >>>>>  \n");
        }

        //Debug.Log("BaseState ::  ........... ........... ........... " + mName + "  ........... ...........  E x i t  >>>> >>> >>> \n");
        //Debug.Log("................................................................................................................... \n");
        //Debug.Log("................................................................................................................... \n");
        mDidExecute_Entry = false; // In case of Entering Again....
    }
}
//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  State Game  <<<<<
public class StateGame : BaseState
{
    public Action mEntryAction, mDuringAction, mExitAction;
    public Dlgt_V_Bool mExitCondition;
    List<Action> arrEtry, arrDuri, arrExit;
    List<Dlgt_V_Bool> arrExitCondition;
    //  ////////////////////////////////////////////////     Creation
    public StateGame () : base ()
    {
    }

    public StateGame (string pName) : base (pName)
    {
        
    }

    public StateGame (string pName, float pTimerSet) : base (pName, pTimerSet)
    {
    }

    public override void DuringAction ()
    {
        base.DuringAction ();
        if (mDuringAction != null)
            mDuringAction ();
        if (arrDuri == null)
            return;

        foreach (Action anAct in arrDuri) {
            anAct ();
        }
    }

    public override void EntryAction ()
    {
        base.EntryAction ();
        if (mEntryAction != null)
            mEntryAction ();
        if (arrEtry == null)
            return;
        foreach (Action anAct in arrEtry) {
            anAct ();
        }
    }

    public override bool ExitCondition ()
    {
        bool rVal = false;
        if (mTimer != null) {
            if (!mTimer.DidTimerFinished ())
                return false; // 시간이 안 됐으므로 탈출 불가. 
            else
                rVal = true;
        }   // Ag.LogString(" Exit at StateGame 0 ");

        if (arrExitCondition != null)
            foreach (Dlgt_V_Bool aCond in arrExitCondition) {
                if (!aCond ())
                    return false; // 조건이 하나라도 안 맞으면 리턴
            }

        if (mExitCondition != null) {
            return mExitCondition ();
        }   // Ag.LogString(" Exit at StateGame 00 ");
        return rVal;
    }

    public override void ExitAction ()
    {
        base.ExitAction ();
        if (mExitAction != null)
            mExitAction ();
        if (arrExit == null)
            return;
        foreach (Action anAct in arrExit) {
            anAct ();
        }
    }

    public float GetTime ()
    {
        if (mTimer != null)
            return mTimer.SecondsLeft ();
        
        return 0f;
    }

    public void AddAnActionTo (string pType, Action pAction) // arrEtry, arrDuri, arrExit;
    {  // [2013:4:19:MOON] Added
        List<Action> curArr = GetArrAction (pType);
        curArr.Add (pAction);
    }

    public void AddExitConditionTo (Dlgt_V_Bool pCond)
    {  // [2013:4:19:MOON] Added
        if (arrExitCondition == null)
            arrExitCondition = new List<Dlgt_V_Bool> ();
        arrExitCondition.Add (pCond);
    }
    //  ////////////////////////////////////////////////     Private
    List<Action> GetArrAction (string pType)
    {
        switch (pType) {
        case "Entry":
            if (arrEtry == null)
                return arrEtry = new List<Action> ();
            else
                return arrEtry;
        case "During":
            if (arrDuri == null)
                return arrDuri = new List<Action> ();
            else
                return arrDuri;
        case "Exit":
            if (arrExit == null)
                return arrExit = new List<Action> ();
            else
                return arrExit;
        }
        return null;
    }
}
//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  State Packet  <<<<<
//public class StatePacket : StateGame
//{
//
//    bool mWillRemoveCurrentPacketAndSet;
//
//
//    //public FunctionPointer mDuringAction;
//    //public FunctionPointerBool mExitCondition;
//    public FunctionPointerAmPack mEntryActionPack;
//    //  ////////////////////////////////////////////////     Creation
//    public StatePacket () : base ()
//    {
//        mIsPacketType = true;
//    }
//
//    public StatePacket (string pName) : base ( pName )
//    {
//        mIsPacketType = true;
//        mWillRemoveCurrentPacketAndSet = true;
//    }
//
//    public StatePacket (string pName, float pTimerSet) : base ( pName, pTimerSet )
//    {
//        mIsPacketType = true;
//    }
//
//    public override void DuringAction ()
//    {
//        base.DuringAction ();
//        if (mDuringAction != null)
//            mDuringAction ();
//    }
//
//    public override void ExitAction ()
//    {
//        base.ExitAction ();
//    }
//
//    public override AmPack EntryActionPacket ()
//    {
//        if (mEntryActionPack != null)
//            mPackOfState = mEntryActionPack (); // Packet Dynamic Assign..
//
//        return base.EntryActionPacket ();
//    }
//
//    public override bool ExitCondition ()
//    {
//        if (mTimer != null || mExitCondition != null)
//        if (!base.ExitCondition ())
//            return false;
//
//        if (DidPacketParsingFinished ())
//            return true;
//        else
//            return false; // No Exit Condition... Packet Sent ==> Ok to go...
//        //return base.ExitCondition ();
//    }
//
//    bool DidPacketParsingFinished ()
//    {
//        return true; // Ag.net.IsNetworkIdle();
//    }
//
//}





