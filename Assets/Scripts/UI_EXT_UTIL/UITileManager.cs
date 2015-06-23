using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
 
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  UITileManager  _____  class  _____
public class UITileManager : UIManagerBase
{
    AgTile mTileObj;
    float muiFlyingSpeed;

    public event Dlgt_GObj_Void EventSetAlienToTouchManager;

    GameObject mAlienPrev, mAlienNext, mAlien, mAlienTarget;

    public UITileManager (int pX, int pY, AmBoxArea pRange, float pDiagDist, bool? pVert, string pName, UITouchManager pTouchMan, 
                          bool reverse = false, bool pSwitchLock = false, bool pLimit2Inside = false, bool pEnableScrl = true) : 
        base(pDiagDist, pVert, pTouchMan, pEnableScrl)
    {
        muiSwitchLock = pSwitchLock;
        mName = pName;
        muiWagu = pRange;
        muiWagu.dicCell = dicCell;
        muiWagu.myOption = muiOption;

        mTileObj = new AgTile (null, pX, pY, new Vector3 (muiWagu.Container.Xmin, muiWagu.Container.Ymin, 0), 
                               new Vector3 (muiWagu.Container.Xmax, muiWagu.Container.Ymax, 0), pVert.Value, reverse);
        muiLimit2Inside = pLimit2Inside;
    }

    public override void AddAMember (GameObject pObject)
    { // Add GameObject(button) to > dicCell <
        base.AddAMember (pObject);
        int x, y;
        mTileObj.GetSequenceNumOf (dicCell.Count - 1, out x, out y);
        pObject.transform.position = mTileObj.GetCenter (x, y);
        pObject.CellCs ().SetInit (pOption: muiOption, pMan: this);
        //pObject.CellCs ().SizeAnimationSet (true, muiBttnScale, muiBttnScale);
    }

    public override void GetPosition (int pX, int pY, out Vector3 pVect)
    {
        pVect = mTileObj.GetCenter (pX, pY);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Mouse  _____  D O W N  _____
    public override void MouseDown (Vector3 pWorldPo, Vector3 pScreenPo)
    {
        if (muiState == UiState.SPEED_LIMIT)
            return;

        Ag.LogIntenseWord (mName.LogWith ("Wagu") + " MouseDown in Tile Manager  :: Started @ " + pWorldPo);
        if (!muiWagu.IsInside (pWorldPo)) 
            return;
        //Ag.LogIntenseWord (" MouseDown in Tile Manager   Outside .. return .. ");
        base.MouseDown (pWorldPo, pScreenPo);
        SetState (UiState.SCROLL);
        return;
    }

    bool SpeedLimit ()
    {
        float delta = Vector3.Distance (muiPrvTouchCo, muiTouchCo);
        if (Mathf.Abs (delta) > 3f) {
            Ag.LogString (mName.LogWith ("Wagu") + " __________  Too Fast  !!!!    Mouse Up Action > _____ ");
            mCnt = 0;
            MouseUp ();
            SetState (UiState.SPEED_LIMIT);
            return true;
        }
        return false;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Mouse  _____  H O L D  _____
    public override void MouseHold (Vector3 pWorldPo, Vector3 pScreenPo, GameObject pAlien)
    { // Set muiPrvTouchCo, muiTouchCo, muiAccumDist

        if (muiState == UiState.SPEED_LIMIT)
            return;
        base.MouseHold (pWorldPo, pScreenPo, pAlien);

        bool isInside = muiWagu.IsInside (pWorldPo);
        if (isInside) {

            if (muiState == UiState.ALIEN_WENT) {
                MyObjectAlienCameBack ();
                SetState (UiState.SELECTED);
            }
        } else {  // Outside Case ...

            if (muiState == UiState.ALIEN_CAME) { // Alien went out .. 
                SetState (UiState.NONE);
                if (mAlienTarget != null) {
                    mAlienTarget.CellCs ().Released ();
                    mAlienTarget = null;
                } 
                return;
            }

            if (muiLimit2Inside) { // Touch Sequence Manage ...
                muiPrvTouchCo = pWorldPo; // Scroll From Outside to Inside ... No MouseDown Event .. 
                if (touchStt.HasValue && touchStt.Value)
                    MouseUp ();
                return;
            } else {
                if (MouseHoldOutsideAction (pWorldPo, pScreenPo, pAlien))
                    return;
            }
        } 

        // Inside Case ....   Alien has come !!!!!!! 
        if (pAlien != null && isInside) {

            if (SpeedLimit ())
                return;

            //(mName.LogWith ("Wagu") + "   Alien is not Null ..  ").HtLog ();
            if (muiSelectedObj == null) {
                (mName.LogWith ("Wagu") + "   SelectedObj is NULL    Accept       >>> Alien is Here <<< .. ..  ").HtLog ();
                int xIdx, yIdx;
                Vector3 v4tile = pWorldPo.AppliedDist (muiOption.scrlVert.Value, -muiWagu.mContDist);
                bool inside = mTileObj.IsInside (v4tile, out xIdx, out yIdx);
                if (inside) {
                    SetState (UiState.ALIEN_CAME);
                    AlienIsInside (xIdx, yIdx, pAlien);
                    return;
                } else if (pAlien == muiSelectedObj)  // My Case .. 
                    return; //ReleaseAliens ();
            } else
                return;
        }
        //base.MouseHold (pWorldPo, pScreenPo, pAlien);
        //if (isInside) (mName.LogWith ("Wagu") + muiWagu.mContDist.LogWith ("Distance")).HtLog ();

        if (!muiOption.scrlVert.HasValue)
            return; // No Scroll Allowed .. !!!!

        switch (muiState) {
            case UiState.SCROLL_OFFLIMIT:
            case UiState.SCROLL:

            if (optnScrlEnable)
                DoScroll (muiTouchCo - muiPrvTouchCo);

            bool? minCase;
            if (muiWagu.OffLimitOf (out minCase))
                SetState (UiState.SCROLL_OFFLIMIT);
            if (muiOption.numSelection < muiTouchCnt && muiAccumDist < muiOption.accumDistLimit4Selection) { // Selection ..
                muiSelectedObj = muiTouchCo.GetNearestFrom (null, dicCell);
                if (muiSelectedObj != null) {
                    if (muiSelectedObj.CellCs ().muiSortOrStuck.HasValue && muiSelectedObj.CellCs ().muiSortOrStuck.Value)
                        return;
                    muiSelectedObj.CellCs ().Selected ("Drag");
                    SetState (UiState.SELECTED);
                    Ag.LogDouble (" Selected :: " + muiSelectedObj.name);
                }
            }
            return; // Translate and Finish !!!
            case UiState.SELECTED:
            (mName.LogWith ("Wagu") + "   Mouse Hold :::   Selected   ").HtLog ();
            muiSelectedObj.CellCs ().SetCurrPosi (pWorldPo);

            if (SpeedLimit ())
                return;

            muiSelectedObj.CellCs ().dlgtAlien = null;
            SwitchingProcess ("Sort");
            return;
            //        case UiState.SCROLL_OFFLIMIT:
            //            DoScroll (muiTouchCo - muiPrvTouchCo);
            //            return;


            case UiState.ALIEN_WENT:
            (mName.LogWith ("Wagu") + muiState + "  Inside : " + isInside).HtLog ();
            return;

        }
        return;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Mouse  _____  U P _____
    public override void MouseUp ()
    {
        if (muiState == UiState.SPEED_LIMIT)
            return;

        base.MouseUp ();

        switch (muiState) {
            case UiState.SCROLL:
            case UiState.SCROLL_OFFLIMIT:
            if (arrTouch.Count < 3)
                return;
            float del = arrTouch [arrTouch.Count - 1].CurrentPosition (muiOption.scrlVert.Value) - 
                arrTouch [0].CurrentPosition (muiOption.scrlVert.Value);
            muiFlyingSpeed = del / arrTouch.Count;
            //Ag.LogIntenseWord (muiFlyingSpeed.LogWith ("FlyingSpeed"));
            if (Math.Abs (muiFlyingSpeed) > 0.005f || muiSelectedObj == null)
                SetState (UiState.FLY);
            else
                SetState (UiState.NONE);
            Ag.LogString ("   __________  Mouse Up   __________________________________________________________ " + muiState);
            return;
            case UiState.SELECTED:
            Ag.LogDouble (mName.LogWith ("Wagu") + "  SELECTED ::  Reset Switch, Selected Objects ... ");
            ReleaseHoveringObj ();
            ReleaseSwitchTarget ();
            Ag.LogString ("   __________  Mouse Up   __________________________________________________________ " + muiState);
            Ag.LogIntense (4, false);
            break; // return;
            case UiState.ALIEN_WENT:
            EventSetAlienToTouchManager (null);
            Ag.LogString ("   __________  Mouse Up   __________________________________________________________ " + muiState);
            return;
            case UiState.ALIEN_CAME:
            AlienSwitchProcess ();
            break;
        }
        ReleaseProcess ();
        SetState (UiState.NONE);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Update  _____  Action  _____
    public override void UpdateAction ()
    {
        bool? minCase;
        /*
        if (mCnt++ % 3 != 2)
            return;
            */

        bool offLmt = muiWagu.OffLimitOf (out minCase);
        //if (offLmt)
        //muiState.Show (" ");

        switch (muiState) {
            case  UiState.SPEED_LIMIT:
            if (mCnt++ > 110)
                SetState (UiState.NONE);
            break;

            case UiState.NONE:
            (mName + " mCnt " + mCnt.LogWith ("Cnt")).HtLog ();
            if (offLmt) {
                if (muiWagu.Return2Limit_Done ())
                    SetState (UiState.FlyBackToLimit);
            } else if (mCnt % 100 == 1)
                ArrangeCells (logCase: false);
            break;
            case UiState.FLY:
            if (offLmt) {
                muiFlyingSpeed *= 0.9f;
                SetState (UiState.FLY_OFFLIMIT);
            } // continue to next line ..... 
            Fly ();
            break;
            case UiState.FLY_OFFLIMIT:
            muiFlyingSpeed *= 0.7f;//0.85f;
            Fly ();
            break;
            case UiState.FlyBackToLimit:
            ArrangeCells ();
            SetState (UiState.NONE);
            break;
        }
    }

    bool MouseHoldOutsideAction (Vector3 pWorldPo, Vector3 pScreenPo, GameObject pAlien)
    { // If it's Selected, Alien case ... return true ... so the MouseHold will stop !!! 
        switch (muiState) {
            case UiState.SCROLL:
            muiEdgeCo = null;
            break;
        case UiState.SELECTED:
            (mName.LogWith ("Wagu") + "  MouseHoldOutsideAction ::  case : SELECTED ").HtLog ();
            //("                           muiSelectedObj : " + muiSelectedObj.name).HtLog ();
            if (muiEdgeCo == null) {
                muiEdgeCo = pWorldPo;
                muiTouchCnt = 0;
            } else {
                muiTouchCnt ++;
                if (muiTouchCnt > muiOption.numBeAlien) {
                    MyObjectBecameAlien_DelegateSet (pWorldPo);
                    SetState (UiState.ALIEN_WENT);
                    //return true;
                } else
                    muiSelectedObj.CellCs ().SetCurrPosi (muiEdgeCo.Value);
            }
            return true;
        case UiState.ALIEN_WENT: 
            (mName.LogWith ("Wagu") + "  MouseHoldOutsideAction ::  case : ALIEN_WENT    ___  SetCurrPosition  ___ ").HtLog ();
            muiSelectedObj.CellCs ().SetCurrPosi (pWorldPo);
            break;
        }
        return false;
    }

    void AlienIsInside (int xIdx, int yIdx, GameObject pAlien)
    {        //Ag.LogIntense (5, true);
        //Ag.LogString (mName.LogWith ("Wagu") + "   Alien Came ..    It's Inside  .......__________==================================");
        Vector3 tarVect = mTileObj.GetCenter (xIdx, yIdx);
        tarVect = tarVect.AppliedDist (muiOption.scrlVert.Value, muiWagu.mContDist);
        GameObject curObj = tarVect.GetNearestFrom (null, dicCell);

        CuCell curCll = curObj.CellCs ();

        if (!pAlien.CellCs ().SameKindOf (curObj))
            return;
        if (curCll.muiSortOrStuck.HasValue && curCll.muiSortOrStuck.Value)
            return;
        //Ag.LogString (mName.LogWith ("Wagu") + " Set Alien & Alien Target ");
        mAlien = pAlien;
        if (mAlienTarget == curObj)
            return;
        if (mAlienTarget != null)
            mAlienTarget.CellCs ().Released ();
        mAlienTarget = curObj;
        mAlienTarget.CellCs ().Selected ("Alien");
    }

    void AlienSwitchProcess () // Called from Mouse Up. 
    {
        if (mAlien == null || mAlienTarget == null)
            return;

        Ag.LogIntenseWord (" UITileManager :: AlienSwitchProcess ");

        CuCell aliCll = mAlien.CellCs ();
        CuCell curCll = mAlienTarget.CellCs ();

        switch (muiOption.mMethodBtwWagu) {
            case "SWAP":
            //(" UITileManager :: AlienSwitchProcess   " + mAlienTarget.name.LogWith("AlienTarget")).HtLog();
            aliCll.mSwapGobj = mAlienTarget;
            curCll.SwapSortInfoWith (mAlien);
            curCll.Pstn.SwapCurposiAndSetTargetWith (aliCll.Pstn);
            dicCell.Remove (mAlienTarget.name);
            break;
            case "INSERT":
            mAlienTarget.CellCs ().InsertAt (mAlien);
            break;
        }

        muiSelectedObj = mAlien;
        dicCell.Add (mAlien.name, mAlien);
        aliCll.mAlienArrived = true;

        ArrangeCells ();

        if (mAlien.CellCs ().dlgtAlien != null)
            mAlien.CellCs ().dlgtAlien ();
        mAlien = null;

        EventSetAlienToTouchManager (null);


        ReleaseProcess ();
        //SetState (UiState.SELECTED);
    }

    void MyObjectAlienCameBack ()
    {

        Ag.LogIntenseWord ("   void MyObjectAlienCameBack()    Release  Alien .... ");
        EventSetAlienToTouchManager (null);
        ReleaseAlien ();

    }

    void MyObjectBecameAlien_DelegateSet (Vector3 pWorldPo)
    { //if (muiSelectedObj.CellCs ().mAlienArrived.Value) { // Arrived .. 
        muiSelectedObj.CellCs ().SetCurrPosi (pWorldPo);
        muiSelectedObj.CellCs ().mAlienArrived = false;

        EventSetAlienToTouchManager (muiSelectedObj);
        mAlienPrev = muiSelectedObj.CellCs ().mPrevGobj;
        mAlienNext = muiSelectedObj.CellCs ().mNextGobj;

        muiSelectedObj.CellCs ().dlgtAlien += () => {
            switch (muiOption.mMethodBtwWagu) {
            case "SWAP":
                Ag.LogIntenseWord (mName.LogWith ("Wagu") + " UITileManager :: AddAlienEvent2Cell   _________   { Swap : Delegate Event } ");
                dicCell.Add (muiSelectedObj.CellCs ().mSwapGobj.name, muiSelectedObj.CellCs ().mSwapGobj);
                break;
            case "INSERT":
                if (mAlienPrev == null) {
                    mAlienNext.CellCs ().mPrevGobj = null;
                    mAlienNext.CellCs ().mSortNum = 0;
                } else
                    mAlienPrev.CellCs ().mNextGobj = mAlienNext;
                if (mAlienNext == null)
                    mAlienPrev.CellCs ().mNextGobj = null;
                else
                    mAlienNext.CellCs ().mPrevGobj = mAlienPrev;
                if (mAlienPrev == null)
                    mAlienNext.CellCs ().RearrangeSortNum (mAlienNext);
                else
                    mAlienPrev.CellCs ().RearrangeSortNum (mAlienPrev);
                break;
            }

            muiSelectedObj.CellCs ().mAlienArrived = null;
            dicCell.Remove (muiSelectedObj.name);
            ArrangeCells ();
            muiEdgeCo = null;
            muiSelectedObj.CellCs ().dlgtAlien = null;
            ReleaseProcess ();
            //Ag.LogIntenseWord (mName.LogWith ("Wagu") + "  UITileManager :: AddAlienEvent2Cell >>>>    Alien Process   ____ Completed _____  ");
            SetState (UiState.NONE);
        };
    }

    void Fly ()
    {
        Vector3 dirVect = (muiOption.scrlVert.Value) ? 
            new Vector3 (0, 1f, 0) : new Vector3 (1f, 0, 0);
        dirVect *= muiFlyingSpeed;
        //(" UITileManager :: Fly   ..... DoScroll >  " + dirVect + muiFlyingSpeed.LogWith("FlyingSpeed") ).HtLog ();

        if (optnScrlEnable)
            muiWagu.DoScroll (dirVect);

        muiFlyingSpeed *= 0.95f;
        if (Mathf.Abs (muiFlyingSpeed) < 0.05f) {
            SetState (UiState.NONE);
        }
    }

    protected override void SwitchingProcess (string mode)
    { // Called by MouseHold . 
        base.SwitchingProcess (mode);
        if (muiSelectedObj == null)
            return;
        if (muiSwitchLock)
            return;

        //(mName.LogWith("Wagu") + " UITileManager :: SetSwitchingTargetObj >>  SwitchingTarget :: " + muiSwitchingTar + " Hovering :: " + muiSelectedObj).HtLog ();
        int xIdx, yIdx;
        Vector3 v4tile = muiTouchCo.AppliedDist (muiOption.scrlVert.Value, -muiWagu.mContDist);
        bool inside = mTileObj.IsInside (v4tile, out xIdx, out yIdx);
        //(" _____________ :: SetSwitchingTargetObj >> Touch Co : " + muiTouchCo + "  Inside ? " + inside + " X/Y : " + xIdx + " / " + yIdx ).HtLog ();
        if (inside) {
            //muiWagu.AutoScroll (muiTouchCo);

            Vector3 tarVect = mTileObj.GetCenter (xIdx, yIdx, pRevApplied: false);
            tarVect = tarVect.AppliedDist (muiOption.scrlVert.Value, muiWagu.mContDist);
            GameObject curObj = tarVect.GetNearestFrom (null, dicCell);

            //(" UITileManager :: SwitchingProcess  >>>>  CurObj : " + curObj.name + " At : " + tarVect).HtLog ();
            if (curObj.name == muiSelectedObj.name) {
                ReleaseSwitchTarget (); // return to my muiSelectedObj position.. so release previous muiSwitchingTar .. 
                return;
            }
            if (muiSwitchingTar != null && muiSwitchingTar.name == curObj.name)
                return; // continue case .. 
            if (curObj.CellCs ().muiSortOrStuck.HasValue && curObj.CellCs ().muiSortOrStuck.Value)
                return;

            if (!muiSelectedObj.CellCs ().SameKindOf (curObj))
                return;

            // {SwapCase} // ReleaseSwitchTarget (); // Previous Object 원상회복
            muiSwitchingTar = curObj; // ##### Select new Switching Target  #####
            muiSelectedObj.CellCs ().SortSwitch (muiSwitchingTar);
            ArrangeCells ();
            //Ag.LogDouble (" UITileManager :: SwitchingProcess  >>  Selected: " + muiSelectedObj.name + " ||  New Target: " + muiSwitchingTar.name);
            // {SwapCase} // muiSwitchingTar.CellCs ().Selected ("Swap");
        }
    }

    protected override void ReleaseProcess ()
    {
        Ag.LogIntenseWord (mName.LogWith ("Wagu") + " Release Process" + muiState.LogWith ("State"));
        base.ReleaseProcess ();
        ReleaseAlien ();
    }

    void ReleaseAlien ()
    {
        if (mAlien != null)
            mAlien.CellCs ().Released ();
        if (mAlienTarget != null)
            mAlienTarget.CellCs ().Released ();
    }

    public Vector3 GetCenterAt (int x, int y)
    {
        return mTileObj.GetCenter (x, y);
    }

    void ArrangeCells (bool logCase = true)
    {
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            int x, y;
            mTileObj.GetSequenceNumOf (kv.Value.CellCs ().mSortNum, out x, out y);
            //(kv.Value.name.LogWith ("Name") + kv.Value.CellCs ().mSortNum.LogWith ("SortNum") + x.LogWith ("x") + y.LogWith ("y")).HtLog ();
            //(mTileObj.GetCenterWithOffset (x, y, muiWagu.mContDist, muiOption.scrlVert.Value).LogWith (" The Cord ") + muiWagu.mContDist.LogWith ("Dist")).HtLog ();
            kv.Value.CellCs ().SetTargetPosition (mTileObj.GetCenterWithOffset (x, y, muiWagu.mContDist, muiOption.scrlVert.Value));
            //kv.Value.CellCs ().SetCurrPosi (mTileObj.GetCenterWithOffset (x, y, muiWagu.mContDist, muiOption.scrlVert.Value));
        }
        if (logCase)
            LogSortNum ();
    }
}
