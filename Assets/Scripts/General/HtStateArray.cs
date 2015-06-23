// [2013:1:10:MOON] Integration with Networks
// [2013:1:21:MOON] mCounter
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Array....  <<<<<
public class StateArray
{
    //ArrayList arrState;
    List<StateGame> arrState;
    List<AgVariable> arrVar;
    public StateGame mCurState;
    public bool mIsDebug = false;
    //  _////////////////////////////////////////////////_    _____   Do   _____   Action   _____
    public void DoAction ()
    {
        if (arrState == null || arrState.Count == 0) {
            Ag.LogDouble (" HtStateArray :: Error .. no State");
            return;
        }

        // Set Variable Objects..
        if (!mCurState.mDidExecute_Entry) {
        
            int num = arrVar.Count;
            for (int i = 0; i < num; i++) {
                AgVariable curVar = arrVar [i];
                curVar.SetValueBy (mCurState.mName);
            }
            for (int i = 0; i < num; i++) {
                AgVariable curVar = arrVar [num - i - 1];
                if (curVar.IsItClearStage (mCurState.mName))
                    arrVar.RemoveAt (i);
            }
        }
        
        mCurState = (StateGame)mCurState.Action ();
     
        if (mCurState != null) {
            if (!mCurState.mDidExecute_Entry)  // [2012:10:15:MOON] XXX
                mCurState = (StateGame)mCurState.Action ();
        }
    }

    public void AddStateVar (AgVariable pVObj)
    {
        arrVar.Add (pVObj);
    }

    public string GetCurStateName ()
    {
        if (mCurState != null) {
            if (mCurState.mDidExecute_Entry)  // [2012:10:15:MOON] XXX
                return mCurState.mName;
            else
                return "BEFORE_ENTRY : > " + mCurState.mName + " < State ";
        } else
            return "";
    }
    //  ////////////////////////////////////////////////     Creation
    public StateArray ()
    {
        arrState = new List<StateGame> (); // ArrayList ();
        arrVar = new List<AgVariable> ();
    }
    //  ////////////////////////////////////////////////     Initial Setting Members...
    public void AddAMember (string pName, float pTimerSet, string pType = "Normal")
    {
        StateGame newObj;
//        if (pType == "Packet")
//            newObj = new StatePacket (pName, pTimerSet);
//        else
        newObj = new StateGame (pName, pTimerSet);
        arrState.Add (newObj);
    }

    public void AddAdditionalActionOf (string pName, string pType, Action pAction)
    { // [2013:4:19:MOON] Added
        StateGame aObj = (StateGame)GetObjectNameOf (pName);
        //(" HtStateArray :: AddAdditionalActionOf  " + aObj.mName).HtLog ();
        aObj.AddAnActionTo (pType, pAction);
    }

    public void AddAdditionalExitConditionTo (string pName, Dlgt_V_Bool pCondition)
    {
        StateGame aObj = (StateGame)GetObjectNameOf (pName);
        aObj.AddExitConditionTo (pCondition);
    }

    public void AddAMemberAndEntryAction (string pName, float pTimerSet, string pType, Action pEntry)
    {
        StateGame newObj;
//        if (pType == "Packet")
//            newObj = new StatePacket (pName, pTimerSet);
//        else
        newObj = new StateGame (pName, pTimerSet);
        arrState.Add (newObj);
        
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        lastGame.mEntryAction = pEntry;
    }

    public void AddEntryDuringExtCond (string pName, float pTimerSet, string pType, Action pEntry, Action pDuring, Dlgt_V_Bool pCond)
    {
        AddAMemberAndEntryAction (pName, pTimerSet, pType, pEntry);
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        lastGame.mDuringAction = pDuring;
        lastGame.mExitCondition = pCond;
    }

    public void AddEntryAction (Action pEntry)
    {
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        lastGame.mEntryAction = pEntry;
    }

    public void AddExitCondition (Dlgt_V_Bool pCond)
    {
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        //if (pCond == null)
        //  lastGame.mExitCondition = ReturnFalse; 
        lastGame.mExitCondition = pCond;
    }

    public void AddDuringAction (Action pCond)
    {
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        lastGame.mDuringAction = pCond;
    }

    public void AddExitAction (Action pCond)
    {
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        //if (pCond == null)
        //  lastGame.mExitCondition = ReturnFalse; 
        lastGame.mExitAction = pCond;
    }

    public void AddTimeOutProcess (float pLimitTime, Action pTimeout)
    {
        StateGame lastGame = (StateGame)arrState [arrState.Count - 1];
        lastGame.mfLimitTime = pLimitTime;
        lastGame.mfnTimeOutProcess = pTimeout;
    }
    /* public void SetPacket ( AmPack pPacket ) {
        StateGame lastGame = (StateGame)arrState[ arrState.Count - 1 ];
        if ( lastGame.mIsPacketType )
            lastGame.mPackOfState = pPacket;
    } */
    //  ////////////////////////////////////////////////     Packet Type Only ...... X X X X X
    /*public void AddEntryActionPacketReturnType ( FunctionPointerAmPack pEntry ) {
        StatePacket lastGame = (StatePacket)arrState[ arrState.Count - 1 ];
        lastGame.mEntryActionPack = pEntry;
    }*/
    //  ////////////////////////////////////////////////     Set Exit State ..
    public void SetNextStateOf (string pTargetState, string pTheNextState)
    {
        BaseState target = GetObjectNameOf (pTargetState);
        BaseState next = GetObjectNameOf (pTheNextState);
        //if (target == null) Debug.Log (" target ");
        //if (next == null) Debug.Log (" next ");
        target.mExitState = next;
    }

    public void SetExitMemberNameOF (string pState)
    {
        BaseState theObj = GetObjectNameOf (pState);
        int j, num = arrState.Count;
        for (j = 0; j < num; j++) {
            ((BaseState)arrState [j]).mExitState = theObj;
        }
    }

    public void SetExitMemberWithSelf ()
    {
        int j, num = arrState.Count;
        for (j = 0; j < num; j++) {
            ((BaseState)arrState [j]).mExitState = ((BaseState)arrState [j]);
        }
    }

    public void SetSerialExitMember (bool pClose = false)
    {
        int j, num = arrState.Count;
        for (j = 0; j < num - 1; j++) {
            ((BaseState)arrState [j]).mExitState = ((BaseState)arrState [j + 1]);
        }
        if (pClose) {
            ((BaseState)arrState [num - 1]).mExitState = ((BaseState)arrState [0]);
        }
    }

    public void SetStateWithNameOf (string pState)
    {
        StateGame newState = (StateGame)GetObjectNameOf (pState);
        if (mCurState != null)
            mCurState.Interrupted (newState);
        mCurState = newState;
    }
    //  ////////////////////////////////////////////////     Get Object
    public ulong GetCounter ()
    {
        return mCurState.mCounter;
    }

    public void ResetCounter ()
    {
        mCurState.mCounter = 0;
    }

    public BaseState GetObjectNameOf (string pName)
    {
        int j, num = arrState.Count;
        for (j = 0; j < num; j++) {
            if (pName == ((BaseState)arrState [j]).mName) {
                return (BaseState)arrState [j];
            }
        }
        return null;
    }

    public StateGame GetState (string pName)
    {
        int j, num = arrState.Count;
        for (j = 0; j < num; j++) {
            if (pName == arrState [j].mName) {
                return arrState [j];
            }
        }
        return null;
    }

    public bool IsCurState (string pName)
    {
        return (GetCurStateName () == pName);
    }

    public BaseState GetCurStateObj ()
    {
        return mCurState;
    }

    public void SetDebug (bool pIsDebug, bool pNoDebug)
    {
        foreach (StateGame aObj in arrState) {
            aObj.mIsDebug = pIsDebug;
            aObj.mNoDebug = pNoDebug;
        }
    }
}