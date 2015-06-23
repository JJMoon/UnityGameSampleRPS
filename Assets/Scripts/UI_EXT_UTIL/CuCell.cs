using UnityEngine;
using System;
using System.Collections;

// ****   Scroll ... Block the Animation ....    *** 
public delegate void DelMove3D (float x,float y,float z);
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  CuCell  _____  Class  _____
public class CuCell : MonoBehaviour
{
    public AmCard PlCard;
    // player card .. 
    //UIManagerBase muiManager;
    public GameObject mPrevGobj, mNextGobj, mSwapGobj;
    public string myKind;
    public CuVect Pstn, Size;
    public int mSortNum;
    public bool? mSelected, mAlienArrived, muiSortOrStuck;
    public Action dlgtAlien;
    public Action evntSelEfct;
    public Action effctSelect;

    public Vector3 TargetPosition { get { return Pstn.Targ; } }
    //protected UiState muiState;
    CuFrameOption mOpt;
    AmUiOption Optn;
    int mCnt;
    //  _////////////////////////////////////////////////_    _____  Unity  _____  MonoBehaviour  _____
    void Start ()
    {
    }

    void Update ()
    {
        if (mOpt == null)
            return;

        if (mCnt++ > 100 && mCnt % 5 != 1)
            return;
        if (mCnt > 300)
            return;

        //Ag.LogString (" CuCell :: Update >> " + gameObject.name + "  " + mCnt + mOpt.optMoveAniSpd.LogWith ("AniSpd"));

        Vector3 curCo = transform.position;

        Pstn.AniToTarget (mOpt);
        Size.AniSize (mOpt);
    }

    public void Arrange (int vUnder300)
    {
        mCnt = vUnder300;
    }
    //  _////////////////////////////////////////////////_    _____  ..  _____  Init  _____
    public void InitPstnSize (CuFrameOption opt)
    {
        mOpt = opt;

        Pstn = new CuVect (gameObject, mOpt, IsPosi: true);
        Size = new CuVect (gameObject, mOpt, IsPosi: false);
    }

    public void CellInit () // Deprecate
    {
        Pstn = new CuVect (gameObject, Optn);
        Size = new CuVect (gameObject, Optn, IsPosi: false);
    }

    public void SetInit (AmUiOption pOption, UIManagerBase pMan)
    {
        Optn = pOption;
        Pstn = new CuVect (gameObject, Optn);
        Size = new CuVect (gameObject, Optn, IsPosi: false);
        //muiManager = pMan;

        //muiManager.delSetState += pStt => { muiState = pStt;  };
    }
    //  _////////////////////////////////////////////////_    _____  Scroll  _____
    public void Scroll (Vector3 pVecVertApplied)
    { // Move Target Posi, Cur Posi
        Pstn.Scroll (pVecVertApplied);
    }

    public void DoScroll (Vector3 pVect)  // Called from AmBoxArea .... Deprecate ....
    {
        SetTargetPosition (gameObject.Move (pVect));
        //Pstn.xxSetCurPosition ();
    }
    //  _////////////////////////////////////////////////_    _____  Set Position / Scale  _____  Anima  _____
    public void Back2InitSize ()
    {
        mCnt = 0;
        Size.Return2SavedVect ();
    }

    public void SetCurrPosi (Vector3 pV, float zOption = 0)
    {
        //(name + "  SetCurrPosi  at   " + pV + Pstn.SaveV.LogWith("SaveV") ).HtLog ();
        mCnt = 0;
        if (zOption != 0)
            pV.z = zOption;
        Pstn.MyVector = pV;
    }

    public void SetTargetPosition (Vector3 pNewCo) 
    {
        //(name + " 107 : SetTargetPosition  at  >>>>>>>     " + pNewCo + Pstn.SaveV.LogWith("SaveV") ).HtLog ();
        mCnt = 60;
        mCnt -= (int)(Vector3.Distance (pNewCo, Pstn.Targ) * 10) * 15;
        Pstn.AmIAnimating = true;

        //mCnt -= (int)(pNewCo. Distance3D (Pstn.Targ) * 10) * 15;
        //if (mCnt < 60)Ag.LogIntenseWord ("SetTargetPosition  " + mCnt);
        Pstn.Targ = pNewCo;
    }

    public void SetSavedPosition (Vector3 pNew)
    {
        //Ag.LogString (" CuCell :: SetSavedPosition(Vector3 pNew)   " + pNew); 
        Pstn.SaveV = pNew;
    }

    public void SetTargetFromMoveVect (Vector3 pDir)
    {
        float x, y;
        x = Optn.scrlVert.Value ? 0 : pDir.x;
        y = Optn.scrlVert.Value ? pDir.y : 0;

        Pstn.AmIAnimating = true;

        Vector3 o = gameObject.transform.position;
        SetTargetPosition (new Vector3 (o.x + x, o.y + y, o.z));
    }

    public void Set_SizeAnimation (float pSize)
    {
        mCnt = 0;
        Size.Return2SavedVect (ForceImmediate: true);
        Size.Targ = gameObject.transform.localScale * pSize;
    }
    //  _////////////////////////////////////////////////_    _____  Select / Release  _____  public  _____
    public void Selected (string mode)
    {
        //myStt = CellStt.SELECTED;
        mCnt = 0;

        if (Pstn.AmIAnimating)
            Pstn.SaveV = Pstn.Targ;

        if (mSelected.HasValue) // 
            return;
        mSelected = true;

        switch (mode) {
        case "Drag":
            Set_SizeAnimation (mOpt.optSelectSize); //(1.2f);
            Pstn.SetTargetZcodi (-20);
            break;
        case "Switch":
            Set_SizeAnimation (0.8f);
            Pstn.SetTargetZcodi (-15);
            break;
        case "Alien":
            Set_SizeAnimation (0.8f);
            break;
        }
    }

    public void Released (bool delEventActions = false)
    {
        mCnt = 0;
        mSelected = null;

        Pstn.AmIAnimating = true;

        Ag.LogIntense (3, true);
        Ag.LogString("Cell :: Released :  at  " + name);
        ShowMyPosition (" Release    will    " + name);

        Back2InitSize ();
        Pstn.SetZeroTargZ ();

        Ag.LogIntense (3, false);
    }

    public void SetZCordi (string mode) // Temporary Setback.
    {
        ShowMyPosition (" Set Z Cordi    ");
        switch (mode) {
        case "Drag":
            Pstn.SetTargetZcodi (-20);
            break;
        case "Switch":
            Pstn.SetTargetZcodi (-15);
            break;
        }
        ShowMyPosition (" Set Z Cordi   End    ");
    }

    public void SwapSortInfoWith (GameObject pT) // switch with Alien .. 
    {
        ShowMySortInfo ();
        pT.CellCs ().ShowMySortInfo ();
        CuCell cll = pT.CellCs ();
        Ag.Swap<GameObject> (ref mPrevGobj, ref cll.mPrevGobj);
        Ag.Swap<GameObject> (ref mNextGobj, ref cll.mNextGobj);
        Ag.Swap<int> (ref mSortNum, ref cll.mSortNum);

        // Set Next / Prev Objects ...
        MyPrevIs (mPrevGobj);
        MyNextIs (mNextGobj);
        cll.MyPrevIs (cll.mPrevGobj);
        cll.MyNextIs (cll.mNextGobj);

        //" !!! ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  magic ".HtLog ();
        ShowMySortInfo ();        
        pT.CellCs ().ShowMySortInfo ();
    }

    public void InsertAt (GameObject iObj)
    { // iObj is Alien 
        Ag.LogIntenseWord (" Insert At " + gameObject.name + "  With : " + iObj.name);
        //mPrevGobj.CellCs ().ShowMySortInfo ();
        //GameObject pre4debug = mPrevGobj;

        Pstn.AmIAnimating = true;

        CuCell iObjCs = iObj.CellCs ();
        if (mPrevGobj == null)
            iObjCs.mSortNum = 0;
        else {
            mPrevGobj.CellCs ().mNextGobj = iObj; // Replace my position..
            iObjCs.mSortNum = mSortNum;
        }
        iObjCs.mPrevGobj = mPrevGobj; // it can be null.
        iObjCs.mNextGobj = gameObject;// myself..
        mPrevGobj = iObj;

        if (mPrevGobj == null)
            RearrangeSortNum (iObj);
        else
            RearrangeSortNum (mPrevGobj);
    }

    public void RearrangeSortNum (GameObject pStartObj)
    {
        Pstn.AmIAnimating = true;
        GameObject next = pStartObj;
        int sortN = pStartObj.CellCs ().mSortNum;
        while (next != null) {
            //("CheckAndSortNumCompareTo :: " + next.name.LogWith ("Name") + sortN.LogWith ("sortN") ).HtLog ();
            next.CellCs ().mSortNum = sortN++;
            next = next.CellCs ().mNextGobj;
        }
    }

    public void SetNextObjWith (GameObject next) // null case included...
    {
        mNextGobj = next;
        if (next == null)
            return;
        next.CellCs ().mPrevGobj = gameObject;
        next.CellCs ().mSortNum = mSortNum + 1;
    }

    public void ShowMySortInfo ()
    {
        string pre = (mPrevGobj == null) ? " \t\t\t\t NULL \t\t " : " \tPrev : \t" + mPrevGobj.name + " , S: " + mPrevGobj.CellCs ().mSortNum;
        string nxt = (mNextGobj == null) ? " \t\t NULL " : " \t\tNext : \t" + mNextGobj.name + " , S: " + mNextGobj.CellCs ().mSortNum;
        ("CuCell ::  My Name  :: " + name + "  >> \t>>\t " + pre + " \t\t\t Curr : " + gameObject.name + " , my S: " + mSortNum + nxt).HtLog ();
    }

    public void SortSwitch (GameObject pTar)
    {
        Ag.LogDouble (" CuCell :: SortSwitch   Me: " + name + " | Target: " + pTar.name);

        //("   ~  ~ ~     Magic  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~").HtLog ();
        //ShowMySortInfo ();        //pTar.CellCs ().ShowMySortInfo ();

        int tarNum = pTar.CellCs ().mSortNum;
        Ag.LogDouble (" CuCell :: SortSwitch    " + mSortNum.LogWith ("SortNum") + tarNum.LogWith ("tarNum"));

        if (Math.Abs (mSortNum - tarNum) == 1) {
            //GameObject first, secon, third, forth;

            if (mSortNum < tarNum) { // Before switch.. me -> tar ==> tar -> me
                GameObject last = pTar.CellCs ().mNextGobj;
                if (mPrevGobj == null) {
                    pTar.CellCs ().mPrevGobj = null;
                    pTar.CellCs ().mSortNum = mSortNum;
                } else
                    mPrevGobj.CellCs ().SetNextObjWith (pTar);
                pTar.CellCs ().SetNextObjWith (gameObject);
                gameObject.CellCs ().SetNextObjWith (last);
            } else {  // tar -> me => me -> tar
                GameObject last = mNextGobj;
                if (pTar.CellCs ().mPrevGobj == null) {
                    mPrevGobj = null;
                    mSortNum = pTar.CellCs ().mSortNum;
                } else
                    pTar.CellCs ().mPrevGobj.CellCs ().SetNextObjWith (gameObject);
                gameObject.CellCs ().SetNextObjWith (pTar);
                pTar.CellCs ().SetNextObjWith (last);
            }
            return;
        }

        pTar.CellCs ().mSortNum = mSortNum;
        mSortNum = tarNum;

        GameObject tarPrev = pTar.CellCs ().mPrevGobj, tarNext = pTar.CellCs ().mNextGobj;
        tarNext.CellCs ().mPrevGobj = gameObject;  // Change Target's Next's Prev ...
        tarPrev.CellCs ().mNextGobj = gameObject;
        mNextGobj.CellCs ().mPrevGobj = pTar; // Change My Next's Prev ...
        mPrevGobj.CellCs ().mNextGobj = pTar;

        pTar.CellCs ().mPrevGobj = mPrevGobj; // Target's Member Change
        pTar.CellCs ().mNextGobj = mNextGobj;
        mPrevGobj = tarPrev;  // My Member Change ..
        mNextGobj = tarNext;

        ("   ~  ~ ~     Magic  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~  ~~~~~~").HtLog ();
        ShowMySortInfo ();
        pTar.CellCs ().ShowMySortInfo ();
    }

    public void ShowMyPosition(string pCmt = "Posi")
    {
        Ag.LogString ("   [ CuCell :: ShowMyPosition ]    " + pCmt);
        Pstn.ShowMyself (pCmt);
    }

    public bool SameKindOf (GameObject pTar)
    { // "ATTK"  "DPNC"  공 수
        return (pTar.CellCs ().myKind == myKind);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  private  _____    _____
    void MyNextIs (GameObject pN)
    { // my next is pN..
        if (pN != null)
            pN.CellCs ().mPrevGobj = gameObject;
    }

    void MyPrevIs (GameObject pP)
    { // 
        if (pP != null)
            pP.CellCs ().mNextGobj = gameObject;
    }
}