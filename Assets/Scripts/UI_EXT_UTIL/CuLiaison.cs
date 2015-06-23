using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CuLiaison : AmObject
{
    GameObject liason;
    public GameObject HomeSel, DestSwt;

    public GameObject Liaison { get { return liason; } set { liason = value; } }

    public CuUiManager FrameHome { get; set; }

    public CuUiManager FrameDest { get; set; }

    public int EmptyCellIdx { get; set; }

    bool HaveLiason { get { return Liaison != null; } }

    public CuLiaison ()
    {
    }

    public void SetLiaison (GameObject pObj)
    {
        Ag.LogDouble (" 28 : [ CuLiaison :: SetLiaison ]    with  >> " + pObj.name + " <<");
        Liaison = pObj;
        Liaison.CellCs ().Selected ("Drag");
    }

    public void OverTaken ()
    {
        Liaison = null;
    }

    public void UpdateAction ()
    {
    }

    public void TouchDown (Vector3 pCo)
    {

    }

    public void TouchHold (Vector3 pCo)
    {
        if (HaveLiason) {
            Liaison.CellCs ().SetCurrPosi (pCo, -20f);  // Outside .. Follow Touch Position.
        }
    }

    public void TouchUp ()
    {
        Ag.LogDouble (" CuLiaison :: Touch UP !!!  " ); // + FrameDest.mSwitchObj.name);
        if (!HaveLiason || FrameHome == null || FrameDest == null || FrameDest.mSwitchObj == null)
            SetOrRelease (ref liason, null);
        else
            LiaisonSwitch ();
    }

    public void LogLiaisonInfo(string pCmt = " LogLiasonInfo ")
    {
        //Ag.LogString ("[ CuLiaison :: LogLiaisonInfo() ] _ _ _ _ _ _ _ _  Comment :: " + pCmt);
        string fhStr = FrameHome == null ? " FrameHome : Null " : "\t>>  " + FrameHome.mName + " <<  is Home ";
        string fdStr = FrameDest == null ? " FrameDest : Null " : "\t>>  " + FrameDest.mName + " <<  is Dest ";
        Ag.LogString("____________________  68 : [ CuLiaison :: LogLiaisonInfo() ]  " + fhStr + "  ,  " + fdStr + " Comment :: " + pCmt);

        string goHome = HomeSel == null ? " HomeSel : Null " : "\t>>  " + HomeSel.name + " <<  is HomeSel ";
        string goDest = DestSwt == null ? " DestSwt : Null " : "\t>>  " + DestSwt.name + " <<  is DestSwt ";
        Ag.LogString("____________________  72 : [ CuLiaison :: LogLiaisonInfo() ]  " + goHome + "  ,  " + goDest + " Comment :: " + pCmt);
        //"\n".HtLog ();
    }

    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Switch  _____  Combination  _____
    public void InsertRemove (Vector3 pV)
    { // called from TouchUp ..
        if (FrameHome == null || FrameDest == null || Liaison == null) {
            SetNull ();
            return;
        }

        Ag.LogIntenseWord ("[ CuLiaison :: InsertRemove ] { 84 }  >>>  Frame HOme / Dest :: " + FrameHome.mName + " / " + FrameDest.mName);
        //Ag.LogDouble ("    Liaison >> " + Liaison.name + EmptyCellIdx.LogWith ("EmptyCellIdx")); // + " , " + FrameDest.mSwitchObj);

        string homeSelect = HomeSel != null ? HomeSel.name : " __ NULL __ ";
        string destSwitch = DestSwt != null ? DestSwt.name : " __ NULL __ ";

        Ag.LogDouble ("     [ CuLiaison :: InsertRemove ] { 90 }  " + homeSelect.LogWith ("Home Select") + destSwitch.LogWith ("Dest Switch"));

        if (DestSwt == null) {
            FrameHome.RemoveCellWith (Liaison.name);
            FrameDest.AddCellAt (EmptyCellIdx, Liaison);
            SetNull ();
            return;
        }
        "  >>>     ".HtLog ();

        if (HomeSel == null) {
            SetNull ();
            return;
        }

        SwapBetweenFrames (HomeSel, DestSwt);
        /*  if (FrameDest != null)
            FrameDest.LogPositions ();  // debug
        if (FrameHome != null)
            FrameHome.LogPositions ();  // */
        SetNull ();
    }

    void SetNull ()
    {
        //FrameHome.LogSortNum ();        FrameDest.LogSortNum ();
        FrameHome = FrameDest = null;
        liason = DestSwt = HomeSel = null;
        TouchUp ();
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Switch  _____  Btw Frames  _____
    void LiaisonSwitch ()
    { // Called from Mouse Up.   UITileManager :: AlienSwitchProcess
        //if (mAlien == null || mAlienTarget == null)            return;
        Ag.LogIntenseWord (" CuLiaison :: LiaisonSwitch ");

        CuCell alien = Liaison.CellCs ();
        CuCell targt = FrameDest.mSwitchObj.CellCs ();
        GameObject Target = FrameDest.mSwitchObj;

        if (!alien.SameKindOf (Target)) {
            FrameHome = FrameDest = null;
            Liaison = null;
            return;
        }

        switch ("SWAP") {
            case "SWAP":

            FrameHome.RemoveCellWith (Liaison.name, Target);
            FrameDest.RemoveCellWith (Target.name, Liaison);

            Ag.Swap<int> (ref alien.mSortNum, ref targt.mSortNum);
            Ag.Swap<GameObject> (ref alien.mPrevGobj, ref targt.mPrevGobj);
            Ag.Swap<GameObject> (ref alien.mNextGobj, ref targt.mNextGobj);

            alien.Pstn.SwapCurposiAndSetTargetWith (targt.Pstn);
            break;
            //case "INSERT":            //Target.CellCs ().InsertAt (mAlien);
        }

        FrameDest.ArrangeCells ();
        FrameHome.ArrangeCells ();

        FrameHome = FrameDest = null;
        Liaison = null;
    }

    void SwapBetweenFrames (GameObject pHome, GameObject pDest)
    {

        Ag.LogIntenseWord ("SwapBetweenFrames  " + pHome.name + "   " + pDest.name);

        CuCell alien = pHome.CellCs ();
        CuCell targt = pDest.CellCs ();

        alien.Pstn.SwapCurposiAndSetTargetWith (targt.Pstn);

        FrameHome.RemoveCellWith (pHome.name, pDest);
        FrameDest.RemoveCellWith (pDest.name, pHome);

        Ag.Swap<int> (ref alien.mSortNum, ref targt.mSortNum);
        Ag.Swap<GameObject> (ref alien.mPrevGobj, ref targt.mPrevGobj);
        Ag.Swap<GameObject> (ref alien.mNextGobj, ref targt.mNextGobj);

    }

    void SetOrRelease (ref GameObject pTar, GameObject pObj = null)
    {
        if (pObj == pTar)
            return;
        if (pTar != null) {
            pTar.CellCs ().Released ();
        }
        pTar = pObj;
    }
}

