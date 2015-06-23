using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CuUiManager : AmObject
{
    public CuTouch mTouch;
    public CuLiaison mLiaison;
    public GameObject mSelectObj, mSwitchObj;
    public Dictionary<string, GameObject> dicCell = new Dictionary<string, GameObject> ();
    // Methods . evnt, dodge, effct
    public Action evntUpdate, evntFlying, evntFlyBack;
    public Dlgt_V_Bool dodgeScroll, dodgeSelectNear, dodgeAutoScrl;
    public Dlgt_Vec_Bool dodgeScrollArea, dodgeSelectNearestArea, dodgeAutoScrlVec;
    public DlgtVect3 evntAlienCame;
    public Dlgt_GObj_Void effctSelect;
    protected List<Vector3> arrPositions = new List<Vector3> ();
    protected AgTile mTchArea, mFrame;
    protected bool mWasInside;

    protected bool HaveLiason { get { return mLiaison != null; } }

    protected CuFrameOption mFrmOpt;
    Camera muiCam;
    float mScreenLength, mFrameLength;
    Vector3 myDirect;
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  CuUiManager  _____  Class  _____
    public CuUiManager (string Name, CuTouch TchObj, CuFrameOption frmOpt, Camera pCam)
    {
        mName = Name;
        mTouch = TchObj;
        mFrmOpt = frmOpt;
        muiCam = pCam;

        Vector3 vec1, vec2; 
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vec1, out vec2);

        mScreenLength = Vector3.Distance (vec1, vec2);
        myDirect = mFrmOpt.optVert ? new Vector3 (0, 1, 0) : new Vector3 (1, 0, 0);
    }

    public void SetFrame (int eaX, int eaY, bool reverse, float[] xyTchFrame) // 2ea of [ x, y, x, y ]
    {
        bool reverseInfo = reverse;
        Vector3 vecOri, vecMax; // 스크린 좌하/우상 좌표 가져오기. (월드 좌표계로 변환)
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vecOri, out vecMax);
        //Ag.LogDouble (" TileUITest ::     Screen World Points .. " + vecOri.LogWith ("world 1") + 
        //              vecMax.LogWith ("world 2") + Ag.mgScrX.LogWith ("ScrX") + Ag.mgScrY.LogWith ("ScrY"));
        AgTile worldRect = new AgTile (vecOri, vecMax);

        Vector3 ll = new Vector3 (worldRect.GetCodiFromOrigin (IsX: true, Ratio: xyTchFrame [0]), 
                                  worldRect.GetCodiFromOrigin (IsX: false, Ratio: xyTchFrame [1]), 0), 
        ur = new Vector3 (worldRect.GetCodiFromOrigin (IsX: true, Ratio: xyTchFrame [2]), 
                          worldRect.GetCodiFromOrigin (IsX: false, Ratio: xyTchFrame [3]), 0),
        fll = new Vector3 (worldRect.GetCodiFromOrigin (IsX: true, Ratio: xyTchFrame [4]), 
                           worldRect.GetCodiFromOrigin (IsX: false, Ratio: xyTchFrame [5]), 0), 
        fur = new Vector3 (worldRect.GetCodiFromOrigin (IsX: true, Ratio: xyTchFrame [6]), 
                           worldRect.GetCodiFromOrigin (IsX: false, Ratio: xyTchFrame [7]), 0);

        Ag.LogIntenseWord (" CuMiManager :: SetFrame At  " + fll.LogWith ("Frame") + fur.LogWith ("Frame"));

        mFrame = new AgTile (null, eaX, eaY, fll, fur, mFrmOpt.optVert, reverseInfo);
        mTchArea = new AgTile (mFrame, eaX, eaY, ll, ur, mFrmOpt.optVert, reverseInfo);
        mFrameLength = (Vector3.Scale ((ll - ur), myDirect)).magnitude;

        Ag.LogIntenseWord ("CuUiManager :: SetFrame  @ " + ll + " to " + ur + myDirect.LogWith ("Dir") + 
            mFrameLength.LogWith ("FrameLength") + mScreenLength.LogWith ("ScreenLength"));
    }

    public void AddACell (GameObject pObj, int x, int y, string kind, bool? pSortOrStock = null)
    {
        (" Add a Cell  At  " + pObj.transform.position.LogWith (" Cord")).HtLog ();
        pObj.transform.position = mFrame.GetCenter (x, y);
        pObj.GetComponent<CuCell> ().muiSortOrStuck = pSortOrStock;
        pObj.CellCs ().myKind = kind;
        pObj.CellCs ().CellInit ();
        if (dicCell.Count == 0)
            pObj.GetComponent<CuCell> ().mSortNum = 0;
        else {
            var kvArr = from gObj in dicCell where gObj.Value != null 
                orderby gObj.Value.GetComponent<CuCell> ().mSortNum descending select gObj;
            GameObject last = kvArr.First ().Value;
            last.GetComponent<CuCell> ().SetNextObjWith (pObj);
        }
        dicCell.Add (pObj.name, pObj);
    }

    public void AddCellAt (int pIdx, GameObject pObj)
    {
        pObj.CellCs ().mSortNum = pIdx;

        pObj.CellCs ().SetTargetPosition (mFrame.GetCenter (pIdx, 0));
        pObj.CellCs ().SetSavedPosition (pObj.CellCs ().TargetPosition);

        Ag.LogIntenseWord (" Add Cell At " + pIdx + " ,  " + pObj.name + "  at " + pObj.CellCs ().TargetPosition);

        dicCell.Add (pObj.name, pObj);
    }

    public void RemoveCellWith (string pKey, GameObject pSwtTar = null)
    { // Switch or Just Remove ..
        //Ag.LogIntenseWord (mName + " CuUiManager :: RemoveCell " + pKey);
        if (!dicCell.Any (pk => pk.Key == pKey)) 
            return;

        // set pre : current's pre.. 
        GameObject cur = dicCell [pKey], pre = dicCell [pKey].CellCs ().mPrevGobj, nxt = dicCell [pKey].CellCs ().mNextGobj;
        Vector3 preVect = dicCell [pKey].CellCs ().TargetPosition;

        if (pre != null)
            pre.CellCs ().mNextGobj = (pSwtTar == null) ? nxt : pSwtTar;
        if (nxt != null)
            nxt.CellCs ().mPrevGobj = (pSwtTar == null) ? pre : pSwtTar;
        dicCell.Remove (pKey);

        if (pSwtTar == null) {
            cur.CellCs ().mNextGobj = cur.CellCs ().mPrevGobj = null;

            do {
                nxt.CellCs ().mSortNum = (pre == null) ? 0 :
                    pre.CellCs ().mSortNum + 1;
                Vector3 temp = nxt.CellCs ().TargetPosition;
                nxt.CellCs ().SetTargetPosition (preVect);
                nxt.CellCs ().SetSavedPosition (nxt.CellCs ().TargetPosition);
                Ag.LogString (mName + "  131  RemoveCell  " + nxt.name + nxt.CellCs ().TargetPosition);

                pre = nxt;
                nxt = nxt.CellCs ().mNextGobj;
                preVect = temp;
            } while 
                (nxt != null);
        } else {
            dicCell.Add (pSwtTar.name, pSwtTar);
        }
        // mSortNum is Swapped already..
    }
    //
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Touch  _____  Actions  _____
    public void UpdateAction ()
    {
        // Actions not related to Touch ..

        if (mTouch.IsTouchDown && mWasInside) {
            //(mName + "  Touch Down  ... mWasInside  ").HtLog ();
            return;
        }
        // Actions without any Touch .. 
        if (evntFlying != null) { // Flying .. 
            evntFlying ();
            return;
        }
        if (evntFlyBack != null)
            evntFlyBack ();
    }

    public void LogPositions ()
    {
        Ag.LogDouble (mName + " 162 : [ CuUiManager :: LogPositions ] ");
        var keyValueArr = from gObj in dicCell where gObj.Value != null
            orderby gObj.Value.CellCs ().mSortNum ascending select gObj;
        foreach (KeyValuePair<string, GameObject> obj in keyValueArr) {
            Ag.LogString ("    ____    ____   ____   Pstn.SaveV ::  " + obj.Value.CellCs ().Pstn.SaveV);
        }
    }


    public void LogSortNum ()
    {
        Ag.LogDouble (mName + "  CuUiManager  173  LogSortNum ");
        var keyValueArr = from gObj in dicCell where gObj.Value != null
            orderby gObj.Value.CellCs ().mSortNum ascending select gObj;
        foreach (KeyValuePair<string, GameObject> obj in keyValueArr) {
            obj.Value.CellCs ().ShowMySortInfo ();
        }
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Scroll  _____  Evnt  _____
    Vector3 mFlyVect;

    public void Scroll (Vector3 pDir, Vector3 pCur)
    {
        if (dodgeScrollArea (pCur) || dodgeScroll ())
            return;
        //Ag.LogString (mName +  "   CuUiManager :: Scroll" + pCur.LogWith ("Vect"));
        pDir = Vector3.Scale (pDir, myDirect);

        if (mTchArea.IsFrameOutsideMinCase ().HasValue)
            pDir *= 0.2f;

        DoScroll (pDir);
    }

    public void InitiateFly (Vector3 pFly) // Set Update Action 
    {
        if (!mWasInside)
            return;
        mFlyVect = Vector3.Scale (myDirect, pFly);
        //(mName + " : 197 [ CuUiManager ::  Initi Fly ]  ___________________________________   Start  " + mFlyVect).HtLog ();
        evntFlying = () => {
            //(mName + "  is evnt Flying").HtLog();
            DoScroll (mFlyVect);
            mFlyVect *= mFrmOpt.FlyDeaccelRatio;
            if (mTchArea.IsFrameOutsideMinCase ().HasValue)
                mFlyVect *= 0.5f;
            if (Vector3.Magnitude (mFlyVect) < mFrmOpt.optParent.optFlyingSpdMin)
                evntFlying = null;
        };
    }

    public void FlyBack ()
    {
        float length = mTchArea.GetLength2Inside ();
        //(" CuUiManager :: UpdateAction    :: " + length.LogWith ("Length")).HtLog ();
        if (length != 0) {
            if (Mathf.Abs (length) < 0.5f)
                DoScroll (myDirect * (Mathf.Abs (length) < 0.05f ? length * 1.2f : length * 0.13f));
            else
                DoScroll (myDirect * length * 0.15f);
        }
    }

    public void AutoScroll (Vector3 pTch)
    {
        mWasInside = mTchArea.IsInside (pTch);
        if (dodgeAutoScrlVec (pTch) || dodgeAutoScrl ())
            return;

        //("AutoScroll" + mName.LogWith ("Frm") + mTchArea.Center.LogWith ("Cen") + pTch.LogWith ("Vect")).HtLog ();
        Vector3 dirVec = Vector3.Scale (mTchArea.Center - pTch, myDirect);

        //("AutoScroll" + dirVec.LogWith("Dir") + dirVec.magnitude.LogWith("Mag") + mScreenLength.LogWith("Screen") ).HtLog ();

        if (dirVec.magnitude < mFrmOpt.AutoScrollLimitRatio * mFrameLength)
            return;
        dirVec /= (mScreenLength * 1.5f);
        DoScroll (dirVec);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Event & Delegate  _____    _____
    public void CheckInside (Vector3 pVec)
    {
        //foreach (KeyValuePair<string, GameObject> gObj in dicCell) {
        //  Ag.LogString ("  Check Inside :: " + gObj.Value.name + "   " + gObj.Value.CellCs ().Pstn.SaveV);        }
        OutOfMyTouchArea (pVec);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Condition  _____  bool  _____
    public bool IsFrameOffLimit ()
    {
        if (mTchArea.IsFrameOutsideMinCase ().HasValue) // Off Limit
            return true;
        else
            return false;
    }

    public bool OutOfMyTouchArea (Vector3 pVec)
    { //(mTchArea.IsInside (pVec).LogWith ("Inside?") + pVec.LogWith ("Vect")).HtLog ();
        mWasInside = mTchArea.IsInside (pVec);
        return !mWasInside;
    }

    public bool DidSelected ()
    {
        return mSelectObj != null;
    }

    public bool SelectObjIsNull ()
    {
        return mSelectObj == null;
    }
    //
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Event & Delegate  _____  Switch  _____
    public void Switch (Vector3 pVec)
    {
        //Ag.LogIntenseWord (mName.LogWith ("N") + "Switch At " + pVec);
        mWasInside = mTchArea.IsInside (pVec);

        if (!mWasInside) {  // Alien went out ... 
            if (mSelectObj != null) {
                if (HaveLiason) {
                    mLiaison.SetLiaison (mSelectObj);
                    mLiaison.FrameHome = this;
                } else 
                    mSelectObj.CellCs ().SetCurrPosi (pVec, -20f);  // Outside .. Follow Touch Position.
            } else { //  null
                SetOrRelease (ref mSwitchObj, null);  // Alien went out case ... relted to .. AlienCame_SetSwitchObj
                return;
            }
            SetOrRelease (ref mSelectObj, null);
            SetOrRelease (ref mSwitchObj, null);

            // Set Liaison and forget ...
            return;
        }           //(mName + "   >>>   Switch   mSelectObj is  " + mSelectObj).HtLog ();
        // mWasInside case ...
        if (mSelectObj == null) { // Alien came .. or Came back..

            if (LiaisonCameBackCheck ()) // CameBack.
                return;
            if (evntAlienCame != null) // Alien Came inside My Area.
                evntAlienCame (pVec);
            return;
        }
        mSelectObj.CellCs ().SetCurrPosi (pVec, -20f);  // #####  Holding Current Position  #####

        GameObject cObj = pVec.GetNearestFrom (null, dicCell);
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

    protected bool LiaisonCameBackCheck ()
    {
        if (!HaveLiason)
            return false;
        GameObject liason = mLiaison.Liaison;
        if (dicCell.Any (pk => pk.Value == liason)) {
            SetOrRelease (ref mSelectObj, liason, "Drag");
            mLiaison.OverTaken ();
            //(mName +  "  __ LiaisonCameBackCheck  return  True >>> ").HtLog (); 
            return true;
        } else
            return false;
    }

    public void SelectNearest (Vector3 pVec)
    {
        if (dodgeSelectNearestArea (pVec) || dodgeSelectNear ())
            return;

        evntFlying = null;
        SetOrRelease (ref mSelectObj, pVec.GetNearestFrom (null, dicCell), "Drag");
    }

    public void RelaseProcess (Vector3 pVec)
    {
        SetOrRelease (ref mSelectObj, null);
        SetOrRelease (ref mSwitchObj, null);
    }

    public void ReleaseAll (Vector3 pVec)
    {
        int idX, idY;
        if (!mTchArea.IsInside (pVec, out idX, out idY))
            return;
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            kv.Value.CellCs ().Released (delEventActions: true);
        }
        mSelectObj = mSwitchObj = null;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Private Methods  _____    _____
    public void ArrangeCells (bool logCase = true)
    {
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            int x, y;
            mFrame.GetSequenceNumOf (kv.Value.CellCs ().mSortNum, out x, out y);
            //(kv.Value.name.LogWith ("Name") + kv.Value.CellCs ().mSortNum.LogWith ("SortNum") + x.LogWith ("x") + y.LogWith ("y")).HtLog ();
            //(mTileObj.GetCenterWithOffset (x, y, muiWagu.mContDist, muiOption.scrlVert.Value).LogWith (" The Cord ") + muiWagu.mContDist.LogWith ("Dist")).HtLog ();
            kv.Value.CellCs ().SetTargetPosition (mFrame.GetCenterWithOffset (x, y, mFrame.SetBack, mFrmOpt.optVert));
        }
        if (logCase)
            LogSortNum ();
    }

    protected GameObject GetNthObject (int pN)
    {
        if (dicCell.Count == 0)
            return null;
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            if (kv.Value.CellCs ().mSortNum == pN)
                return kv.Value;
        }  //  GO_0, null, GO_2, null, ... 
        return null;
    }

    protected bool? AnyObjectAt (Vector3 pVec, out int NullIdx)
    { // null : is not inside ...  false : no object there ...
        int idX, idY;
        NullIdx = 0;
        bool inside = mFrame.IsInside (pVec, out idX, out idY);
        //Ag.LogDouble (mName + " AnyObjectAt   " + idX + " / " + idY);
        if (!inside)
            return null;

        GameObject theObj = GetNthObject (mFrmOpt.optVert ? idY : idX);

        if (theObj == null) {
            NullIdx = mFrmOpt.optVert ? idY : idX;
            return false;
        }

        //Vector3  mFrame.GetCenterWithOffset (idX, idY, mFrame.SetBack, mFrmOpt.optVert);

        return true;
    }

    protected void SaveCurrentPositions ()
    {
        arrPositions.Clear ();

        var keyValueArr = from gObj in dicCell where gObj.Value != null
            orderby gObj.Value.GetComponent<CuCell> ().mSortNum ascending select gObj;
        foreach (KeyValuePair<string, GameObject> obj in keyValueArr) {
            arrPositions.Add (obj.Value.CellCs ().TargetPosition);
            (" Positions ... " + obj.Value.CellCs ().TargetPosition).HtLog ();
        }
    }

    public void ArrangeLittle ()
    {
        int serNum = 60;
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            kv.Value.CellCs ().Arrange (serNum++);
            serNum = serNum > 70 ? 60 : serNum;
        }
    }

    public void SetPrevNextObjectsWithSortNum ()
    {
        var keyValueArr = from gObj in dicCell where gObj.Value != null
            orderby gObj.Value.GetComponent<CuCell> ().mSortNum ascending select gObj;

        GameObject prev = null;

        foreach (KeyValuePair<string, GameObject> obj in keyValueArr) {

            // Previous ...
            obj.Value.CellCs ().mPrevGobj = prev; // My Prev..
            if (prev != null)
                prev.CellCs ().mNextGobj = obj.Value;  // ThePrev's Next ..

            prev = obj.Value;
        }
        prev.CellCs ().mNextGobj = null; // last object ..
    }

    protected void SetOrRelease (ref GameObject pTar, GameObject pObj = null, string Mode = "None")
    {
        //Ag.LogDouble (" Set / Release :: " + pTar + " ,   " + pObj + "   " + Mode);
        if (pObj == pTar)
            return;
        if (pTar != null) {
            pTar.CellCs ().Released ();
        }
        pTar = pObj;
        if (pTar != null && Mode != "None")
            pTar.CellCs ().Selected (Mode);
    }

    void DoScroll (Vector3 pV)
    {
        mFrame.ApplySetBack (pV);
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            kv.Value.CellCs ().Scroll (pV);
        }
    }
}
