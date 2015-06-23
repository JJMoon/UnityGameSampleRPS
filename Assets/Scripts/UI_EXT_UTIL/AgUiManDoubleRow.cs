using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class AgUiManDoubleRow : CuUiManager
{
    public Dlgt_V_Bool dodgeSwitchCombi;

    public enum SortBy
    {
        Type,
        Grade,
        Weather,
    }

    public AgUiManDoubleRow (string Name, CuTouch TchObj, CuFrameOption frmOpt, Camera pCam) :
        base (Name, TchObj, frmOpt, pCam)
    {
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Switch  _____  Combination  _____
    public void SwitchCombination (Vector3 pV)
    {
        if (dodgeSwitchCombi ()) // || dodgeScroll ())
            return;
        //(mName + " : 29 [ AgUiManDoubleRow ::  SwitchCombination ]  ___________________________________   Start  " + mName).HtLog ();
        //mLiaison.LogLiaisonInfo (" Switch Combination " + mName);  // Log Debug ...
        mWasInside = mTchArea.IsInside (pV);
        //(mName + " : 33 [ AgUiManDoubleRow ::  SwitchCombination ]  Inside ? " + mWasInside).HtLog ();

        if (!mWasInside) {  // Alien went out ...                       _< Home viewpoint >_
            //(mName + " : 36 ____ !mWasInside ").HtLog ();
            if (mSelectObj != null) {
                if (HaveLiason) {
                    mLiaison.SetLiaison (mSelectObj);
                    mLiaison.FrameHome = this;
                    mLiaison.HomeSel = mSelectObj;
                    //string swtStr = (mSelectObj == null) ? " Null " : mSelectObj.name;
                    //(mName + " : 45 ____ ____ HaveLiaison >>  Setting     mLiaison.HomeSel  with    " + swtStr).HtLog ();
                } else {
                    mSelectObj.CellCs ().SetCurrPosi (pV, -20f);  // Outside .. Follow Touch Position.
                }
            } else { //  Alien went out ..                              _< Dest viewpoint >_
                if (HaveLiason && mLiaison.FrameDest == this) { 
                    mLiaison.FrameDest = null; // Cancel the Dest Setting in Liaison 
                    mLiaison.DestSwt = null;
                }
                SetOrRelease (ref mSwitchObj, null);  //  relted to .. AlienCame_SetSwitchObj
                return;
            }
            SetOrRelease (ref mSelectObj, null);
            SetOrRelease (ref mSwitchObj, null);
            return;
        }
        // mWasInside case ...
        if (mSelectObj == null) { // Alien came .. or Came back..       _< Dest viewpoint >_
            mLiaison.FrameDest = this;
            int nullIdx;
            bool? inSide = AnyObjectAt (pV, out nullIdx);

            if (inSide.HasValue) //  && !inSide.Value)
                mLiaison.EmptyCellIdx = nullIdx;
            //(mName + " : 70 [ AgUiManDoubleRow ::  SwitchCombination ]  " + inSide + " << mSelec==null >>> " + nullIdx).HtLog ();
            if (LiaisonCameBackCheck ()) // CameBack.
                return;
            if (evntAlienCame != null) // Alien Came inside My Area.
                evntAlienCame (pV);
            return;
        }

        mSelectObj.CellCs ().SetCurrPosi (pV, -20f);  // #####  Holding Current Position  #####

        GameObject cObj = pV.GetNearestFrom (null, dicCell);
        if (!mSelectObj.CellCs ().SameKindOf (cObj))
            return;
        //(" 254 :: " + mSelectObj + " ,   " + mSwitchObj + " ,   " + cObj).HtLog ();

        if (mSelectObj == cObj)
            SetOrRelease (ref mSwitchObj, null);
        if (mSwitchObj == cObj || mSelectObj == cObj) {
            return;
        }
        SetOrRelease (ref mSwitchObj, cObj);  // ##### Select new Switching Target  #####
        if (mFrmOpt.optSwitchInFrame) {
            mSelectObj.CellCs ().SortSwitch (mSwitchObj);
            ArrangeCells ();
            //Ag.LogDouble (" Select " + mSelectObj.name + " cObj " + cObj.name);
            mSwitchObj.CellCs ().Selected ("Switch");
        }
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Liaison Came  _____  Set to mSwitchObj  _____
    public void AlienCame_SetSwitchObj (Vector3 pV)
    { // mSelectObj == null .. already.
        //(mName + " : 102 [ AgUiManDoubleRow ::  AlienCame_SetSwitchObj ]    AlienCame_SetSwitchObj   _____     Start     _____ ").HtLog ();
        GameObject cObj;
        int nullIdx;
        bool? isInside = AnyObjectAt (pV, out nullIdx);  // Check if there is a Gobject in that area ...
        //Ag.LogDouble (mName + " : 105 [ AgUiManDoubleRow ::  AlienCame_SetSwitchObj ]   isInside " + isInside + " mSelect : " + mSelectObj);

        if (isInside.HasValue && isInside.Value) // null : is not inside ...  false : no object there ...
            cObj = pV.GetNearestFrom (null, dicCell);
        else { // No Object ..
            //Ag.LogString ("     117  No Inside,,  Set Null    " + isInside.Value); 
            SetOrRelease (ref mSwitchObj, null); // Release ..
            if (HaveLiason) {
                mLiaison.DestSwt = mSwitchObj;
                //string swtStr = mSwitchObj == null ? " Set Null " : mSwitchObj.name;
                //Ag.LogIntenseWord (mName + " : 123 [ AgUiManDoubleRow ::  AlienCame_SetSwitchObj ]   Setting > mLiaison.DestSwt  with " + swtStr);
            }
            return;
        }
        //Ag.LogIntenseWord (mName + " : 114 [ AgUiManDoubleRow ::  AlienCame_SetSwitchObj ]    mLiaison.FrameDest = this   _____     End     _____ ");

        if (mSwitchObj != null)
            mSwitchObj.CellCs ().SetZCordi ("Switch");//"fuck".HtLog();//
        if (mSwitchObj == cObj)
            return;
        if (mSwitchObj != null) {
            SetOrRelease (ref mSwitchObj, null); // Release ..
        } else {
            SetOrRelease (ref mSwitchObj, cObj, "Alien");  // ##### Select new Switching Target  #####
            if (mLiaison != null) {
                mLiaison.DestSwt = mSwitchObj;
            }
        }
        mLiaison.FrameDest = this;
    }

    public void SwitchOutside (Vector3 pV)
    {
        Ag.LogDouble (" AgUiManDoubleRow :: SwitchOutside " + pV);
    }

    public void SortCells (SortBy pSortBy, bool pAscend)
    {
        SaveCurrentPositions ();

        int sortNum = 0;
        var keyValueArr = pAscend ? 
            from gObj in dicCell
                  where gObj.Value != null
                  orderby gObj.Value.CellCs ().PlCard.CdType ascending
                  select gObj :
                from gObj in dicCell
                      where gObj.Value != null 
                orderby gObj.Value.CellCs ().PlCard.CdType descending
                      select gObj;
        switch (pSortBy) {
        case SortBy.Grade:
            keyValueArr = pAscend ? 
                from gObj in dicCell
                         where gObj.Value != null
                         orderby gObj.Value.CellCs ().PlCard.Grade ascending
                         select gObj :
                    from gObj in dicCell
                             where gObj.Value != null 
                    orderby gObj.Value.CellCs ().PlCard.Grade descending
                             select gObj;
            break;
//            case SortBy.Weather:
//            keyValueArr = pAscend ? 
//                from gObj in dicCell where gObj.Value != null 
//                          orderby gObj.Value.CellCs ().PlCard. ascending select gObj :
//                    from gObj in dicCell where gObj.Value != null 
//                    orderby gObj.Value.CellCs ().PlCard.Weather descending select gObj;
//            break;
        }

        foreach (KeyValuePair<string, GameObject> obj in keyValueArr) {
            obj.Value.CellCs ().mSortNum = sortNum;
            obj.Value.CellCs ().SetTargetPosition (arrPositions [sortNum++]);
            obj.Value.CellCs ().SetCurrPosi (obj.Value.CellCs ().TargetPosition);
            //(" Positions ... " + obj.Value.CellCs ().TargetPosition).HtLog ();
        }

        SetPrevNextObjectsWithSortNum ();
        ArrangeCells ();
    }

    public void EvntAlienSettled ()
    {

    }
}