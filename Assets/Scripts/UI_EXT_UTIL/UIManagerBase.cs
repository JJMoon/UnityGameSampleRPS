using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class UIManagerBase : AmObject // 베이스
{
    protected UITouchManager muiTouchMan;
    protected AmUiOption muiOption;
    protected Dictionary<string, GameObject> dicCell = new Dictionary<string, GameObject> ();
    protected List<Vector3> arrTouch = new List<Vector3> ();
    protected int muiTouchCnt = 0;
    protected bool muiLimit2Inside = true, muiSwitchLock, optnScrlEnable;
    protected bool? touchStt;
    protected float muiAccumDist, muiSwitchSpeedLimit;
    protected UiState muiState;
    protected AmBoxArea muiWagu;
    protected Vector3 muiTouchCo, muiPrvTouchCo, muiStartCo;
    protected Vector3? muiEdgeCo;
    protected GameObject muiSelectedObj, muiSwitchingTar;

    public delegate void DelSetState (UiState pState);

    public DelSetState delSetState;

    public UIManagerBase (float pDiagDist, bool? pVert, UITouchManager pTouchMan, bool pEnableScroll)
    {
        muiOption = new AmUiOption (muiSwitchSpeedLimit, pVert);
        muiTouchMan = pTouchMan;
        optnScrlEnable = pEnableScroll;
    }

    public virtual void AddAMember (GameObject pObject)
    {
        if (dicCell.Count == 0)
            pObject.GetComponent<CuCell> ().mSortNum = 0;
        else {
            GameObject last = GetCellFromDic ("Last");
            last.GetComponent<CuCell> ().SetNextObjWith (pObject);
        }
        dicCell.Add (pObject.name, pObject);
    }

    protected GameObject GetCellFromDic (string what)
    {
        GameObject rV = null;
        switch (what) {
        case "Last":
            int sN = -1;
            foreach (KeyValuePair<string, GameObject> kv in dicCell) {
                if (kv.Value.GetComponent<CuCell> ().mSortNum > sN) {
                    sN = kv.Value.GetComponent<CuCell> ().mSortNum;
                    rV = kv.Value;
                }
            }
            break;
        }
        return rV;
    }

    public virtual void GetPosition (int pX, int pY, out Vector3 pVect)
    {
        pVect = new Vector3 (0, 0, 0);
    }

    //  ////////////////////////////////////////////////     ////////////////////////     >>>>> Mouse Down Hold Up.... <<<<<
    public virtual  void MouseDown (Vector3 pWorldPo, Vector3 pScreenPo)
    {
        if (touchStt.HasValue && touchStt.Value == true)
            MouseUp ();
        touchStt = false;
        arrTouch.Clear ();

        muiPrvTouchCo = muiStartCo = muiTouchCo = pWorldPo;
        muiTouchCnt = 0;
        muiAccumDist = 0;
        muiSwitchingTar = muiSelectedObj = null;
        Ag.LogNewLine (5);
        Ag.LogString (mName.LogWith ("Wagu") + " __________  Mouse Down   ________________   At : " + muiStartCo.LogWith ("StartCo") + muiState);
        Ag.LogString (mName.LogWith ("Wagu") + " __________  Mouse Down   ");
        Ag.LogNewLine (2);
    }


    public virtual void MouseHold (Vector3 pWorldPo, Vector3 pScreenPo, GameObject pAlien)
    {
        if (!touchStt.HasValue && IsScrolling() ) {
            MouseUp ();
            MouseDown (pWorldPo, pScreenPo);
        }
        touchStt = true;

        muiPrvTouchCo = muiTouchCo;
        muiTouchCo = pWorldPo;
        muiAccumDist += Mathf.Abs (muiTouchCo.x - muiPrvTouchCo.x);
        muiAccumDist += Mathf.Abs (muiTouchCo.y - muiPrvTouchCo.y);
        muiTouchCnt ++;


        arrTouch.Add (muiTouchCo);
        while (arrTouch.Count > 6)
            arrTouch.RemoveAt (0);

        return;
    }

    public virtual void MouseUp ()
    {
        touchStt = null;
        //Ag.LogString(mName.LogWith("Wagu") + " UITileManager :: Mouse Up   ");
        //Ag.LogString ("    UIManagerBase :: Mouse Up   __________________________________________________________ " + muiState);

    }

    protected void DoScroll (Vector3 pVect)
    {
        //Ag.LogDouble ("  UIManager :: Do Scroll " + pVect.LogWith("pVect") + dicCell.Count.LogWith("Cell Count") );
        if (muiState == UiState.SCROLL_OFFLIMIT)
            pVect *= 0.5f;
        muiWagu.DoScroll (pVect);
    }

    protected virtual void ReleaseProcess ()
    {
        ReleaseHoveringObj ();
        ReleaseSwitchTarget ();
    }

    protected virtual void ReleaseHoveringObj ()
    {
        if (muiSelectedObj != null) {
            //muiSelectedObj.transform.localScale = muiBttnScale;
            //muiSelectedObj.GetComponent<Cell> ().Back2InitPosi ();

            muiSelectedObj.CellCs ().Released ();
        }
        muiEdgeCo = null;
        muiSelectedObj = null;
    }

    protected virtual void ReleaseSwitchTarget ()
    {
        if (muiSwitchingTar != null) {
            //muiSwitchingTar.GetComponent<Cell> ().Released ("Swap");
            //muiSwitchingTar.GetComponent<Cell> ().Back2InitPosi ();
            muiSwitchingTar.CellCs ().Released ();
        }
        muiSwitchingTar = null;
    }

    protected virtual void SwitchingProcess (string mode)
    {
    }
    //  ////////////////////////////////////////////////     Functions...
    protected void SetState (UiState pStt)
    {
        muiState = pStt;

        if (delSetState != null)
            delSetState (pStt);

        //( "  UIManagerBase :: SetState       " + mName.LogWith ("Wagu") + muiWagu.mContDist.LogWith ("Distance")).HtLog ();
        pStt.Show (mName); // Debug
    }

    public virtual void UpdateAction ()
    {
    }


    public void LogSortNum()
    {
        Ag.LogDouble ("UIManagerBase :: LogSortNum ");
        var keyValueArr = from gObj in dicCell where gObj.Value != null orderby gObj.Value.GetComponent<CuCell> ().mSortNum ascending 
            select gObj;
        foreach( KeyValuePair<string, GameObject> obj in keyValueArr ) {
            string prevN, nextN;
            prevN = (obj.Value.GetComponent<CuCell> ().mPrevGobj == null) ? "<<< Null >>>".LogWith("Prev") : 
                obj.Value.GetComponent<CuCell> ().mPrevGobj.GetComponent<CuCell> ().mSortNum.LogWith ("Prev") + obj.Value.GetComponent<CuCell> ().mPrevGobj.name;
            nextN = (obj.Value.GetComponent<CuCell> ().mNextGobj == null) ? "<<< Null >>>".LogWith("Next") : 
                obj.Value.GetComponent<CuCell> ().mNextGobj.GetComponent<CuCell> ().mSortNum.LogWith ("Next") + obj.Value.GetComponent<CuCell> ().mNextGobj.name;
            ("  Sort " + prevN + obj.Value.GetComponent<CuCell> ().mSortNum.LogWith ("Cur: " + obj.Value.name) + nextN ).HtLog ();
        }
    }

    public bool IsScrolling()
    {
        if (UiState.SCROLL.ToString ().Substring (0, 5) == "SCROL")
            return true;
        return false;
    }


}

