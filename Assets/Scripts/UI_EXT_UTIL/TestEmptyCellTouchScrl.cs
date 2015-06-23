using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class TestEmptyCellTouchScrl : AmSceneBase
{
    public Camera muiCam;
    CuTouch mTouch;
    // Drag Setting..
    AgTile xmuiTile;
    // Private valiables...
    UiState muiState;
    CuUiOption optObj;
    CuFrameOption optFrame;
    CuLiaison mLiaison;
    CuUiManager muiMan;
    List<CuUiManager> arrMan;
    //  ////////////////////////////////////////////////     Starting Init Job
    public override void Start ()
    {
        base.Start ();

        myGUI.SetColumns (3, 10);

        arrMan = new List<CuUiManager> ();

        mTimeLooseAtStartPoint = 0.5f;
        mSeldomActionNum = 300;
        Ag.LogDouble (" >>>>  Start >>>>>>>>>>>>>>>>>>>>    " + GetType ().ToString ());

        SetDoubleHor1Row ();
    }

    public override void BaseStartSetting ()
    {
        base.BaseStartSetting ();
        Ag.LogDouble (" TileUITest :: BaseStartSetting  >>>> Type : " + GetType ().ToString ());
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Set Frames   _____  2 * Single Row  _____
    void SetDoubleHor1Row ()
    {
        //  _////////////////////////////////////////////////_    _____  General Option 양 프레임에 공통 적용  _____
        //optObj = new CuUiOption (HoldLimit: 30, SelDist: 0.5f, FlyingSpdMin: 0.05f, SpdLimit: 3f);
        optObj = new CuUiOption (HoldLimit: 5, SelDist: 1.2f, FlyingSpdMin: 0.05f, SpdLimit: 3f);
        mTouch = new CuTouch (optObj);

        mLiaison = new CuLiaison ();


        // Frame Setting ..  Upper Frame
        int eaX = 5, eaY = 1; // 셀의 갯수. 삽입될 것 고려해서 충분히 잡을 것. 

        //  _////////////////////////////////////////////////_    _____  Frame Option  _____
        optFrame = new CuFrameOption (optObj, Vert: false, MoveAniSpd: 200, SizeAniSpd: 100, SelectSize: 1.2f, SwitchSize: 0.9f,
                                      SwitchInFrame: false);

        // Set Touch Manager 
        float dimX, dimY;  // 셀의 크기. 
        Vector3 vecOri, vecMax; // 스크린 좌하/우상 좌표 가져오기. (월드 좌표계로 변환)
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vecOri, out vecMax);
        dimX = (vecMax.x - vecOri.x) / 5; //7; // 스크린 크기를 기준으로 셀의 크기를 정한다.
        dimY = (vecMax.y - vecOri.y) / 3; //8;

        Ag.LogDouble (" TileUITest ::     Screen World Points .. " + vecOri.LogWith ("world 1") + 
                      vecMax.LogWith ("world 2") + Ag.mgScrX.LogWith ("ScrX") + Ag.mgScrY.LogWith ("ScrY"));

        AgUiManDoubleRow theMan = new AgUiManDoubleRow ("UPFR", mTouch, optFrame, muiCam);
        theMan.SetFrame (eaX: eaX, eaY: eaY, reverse: false, xyTchFrame: new float[] {
            0.1f, // Xmin
            0.55f, // Ymin
            0.9f, // Xmax
            0.9f, // Ymax
            0.1f,
            0.55f,
            0.9f, // Xmax
            0.9f
        });

        Ag.LogDouble (" TileUI Test :: Setting  >>>   Vects " + vecOri + ", " + vecMax + " >> dimX/Y : " + dimX + " / " + dimY);

        // Set Wagu, Container ...  TIle Manager ...
        Vector3 boxScale = new Vector3 (dimX * 0.8f, dimY * 0.8f, 0.5f);  // 크기 조절. 
        int serialN = 0;
        /*for (int ii=0; ii < eaY; ii++) {
            for (int k=0; k < eaX; k++) {
                GameObject curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "ATile");
                curObj.name = "UP_BOX_" + serialN++; //k.ToString() + ii;
                curObj.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download", "Item" + (serialN % 10 + 1));
                curObj.transform.localScale = boxScale;
                curObj.CellCs ().InitPstnSize (optFrame);

                AmCard playerCard = new AmCard (pGrade: AgUtil.RandomInclude (0, 20), pType: 5, pWthr: "CLOUD");  // Type, Grade, Weather .. setting ...
                curObj.CellCs ().PlCard = playerCard;

                string kind = "TEST"; // serialN < 3 ? "ATTK" : "DPNC" ;
                theMan.AddACell (curObj, k, ii, kind);

                Ag.LogString (" Add a Cell  " + curObj.name + "   " + k.LogWith ("X") + ii.LogWith ("Y"));
            }
        } */

        mTouch.evntTouchDown += theMan.CheckInside; // 필수.

        //mTouch.evntDrag += theMan.Scroll; // 스크롤 가능하게
        //theMan.dodgeScroll += theMan.DidSelected;
        //theMan.dodgeScrollArea += theMan.OutOfMyTouchArea;

        mTouch.evntHold += theMan.SelectNearest;

        mTouch.evntHoldMove += theMan.SwitchCombination; // Empty Cell   스위치 가능하게..  ##### 
        theMan.dodgeSwitchCombi += theMan.IsFrameOffLimit;

        //mTouch.evntHoldMove += theMan.AutoScroll;

        theMan.dodgeSelectNear += theMan.DidSelected;
        theMan.dodgeSelectNear += theMan.IsFrameOffLimit;
        theMan.dodgeSelectNearestArea += theMan.OutOfMyTouchArea;

        mTouch.evntTouchUp += mLiaison.InsertRemove;



        //mTouch.evntInitFly = theMan.InitiateFly;
        //theMan.evntFlyBack = theMan.FlyBack;

        //theMan.dodgeAutoScrlArea += theMan.OutOfMyTouchArea;
        //theMan.dodgeAutoScrl += theMan.SelectObjIsNull;

        theMan.evntAlienCame = theMan.AlienCame_SetSwitchObj;
        mTouch.evntTouchUp += theMan.RelaseProcess; // Put @ Last ... 

        theMan.mLiaison = mLiaison;

        arrMan.Add (theMan);







        // Frame Setting ..  Lower Frame
        eaX = 15;
        eaY = 1; // 셀의 갯수. 삽입될 것 고려해서 충분히 잡을 것. 

        //  _////////////////////////////////////////////////_    _____  Frame Option  _____
        optFrame = new CuFrameOption (optObj, Vert: false, MoveAniSpd: 100, SizeAniSpd: 100, SelectSize: 1.2f, SwitchSize: 0.9f, 
                                      SwitchInFrame: false);

        // Set Touch Manager 
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vecOri, out vecMax);
        dimX = (vecMax.x - vecOri.x) / 6; //7; // 스크린 크기를 기준으로 셀의 크기를 정한다.
        dimY = (vecMax.y - vecOri.y) / 3; //8;

        Ag.LogDouble (" TileUITest ::     Screen World Points .. " + vecOri.LogWith ("world 1") + 
                      vecMax.LogWith ("world 2") + Ag.mgScrX.LogWith ("ScrX") + Ag.mgScrY.LogWith ("ScrY"));

        theMan = new AgUiManDoubleRow ("LOWR", mTouch, optFrame, muiCam); //, new float[] { vecOri.x * 0.9f, vecMax.x*0.9f, vecOri.y*0.9f, vecMax.y*0.9f } );
        theMan.SetFrame (eaX: eaX, eaY: eaY, reverse: false, xyTchFrame: new float[] {
            0.1f, // Xmin
            0.05f, // Ymin
            0.9f, // Xmax
            0.45f, // Ymax
            0.1f,
            0.05f,
            2.5f,
            0.45f
        });

        Ag.LogDouble (" TileUI Test :: Setting  >>>   Vects " + vecOri + ", " + vecMax + " >> dimX/Y : " + dimX + " / " + dimY);
        Ag.LogString (" Option " + optFrame.optMoveAniSpd.LogWith ("AniSpd"));

        // Set Wagu, Container ...  TIle Manager ...

        //new float[] { vecOri.x, vecMax.x, vecOri.y, vecMax.y } );
        boxScale = new Vector3 (dimX * 0.8f, dimY * 0.8f, 0.5f);  // 크기 조절. 
        serialN = 0;
        //        for (int k=0; k< eaX - 6; k++) { // 12를 기본으로 넣고.. 6개까지 추가 가능.
        //            for (int ii=0; ii< 1; ii++) {
        for (int ii=0; ii < eaY; ii++) {
            for (int k=0; k < eaX; k++) {
                GameObject curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "ATile");
                curObj.name = "LW_BOX_" + serialN++; //k.ToString() + ii;
                curObj.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download", "Item" + (serialN % 10 + 1));
                curObj.transform.localScale = boxScale;
                curObj.CellCs ().InitPstnSize (optFrame);

                AmCard playerCard = new AmCard (pGrade: AgUtil.RandomInclude (0, 20), pType: 5, pWthr: "CLOUD");  // Type, Grade, Weather .. setting ...
                curObj.CellCs ().PlCard = playerCard;

                string kind = "TEST"; // serialN < 3 ? "ATTK" : "DPNC" ;
                theMan.AddACell (curObj, k, ii, kind);

                Ag.LogString (" Add a Cell  " + curObj.name + "   " + k.LogWith ("X") + ii.LogWith ("Y"));
            }
        }

        mTouch.evntTouchDown += theMan.CheckInside;

        mTouch.evntDrag += theMan.Scroll;
        theMan.dodgeScroll += theMan.DidSelected;
        theMan.dodgeScrollArea += theMan.OutOfMyTouchArea;

        mTouch.evntHold += theMan.SelectNearest;

        // Normal Switch 
        //mTouch.evntHoldMove += theMan.Switch;
        mTouch.evntHoldMove += theMan.SwitchCombination; // Empty Cell   스위치 가능하게..  ##### 
        theMan.dodgeSwitchCombi += theMan.IsFrameOffLimit;

        theMan.dodgeSelectNear += theMan.DidSelected;
        theMan.dodgeSelectNear += theMan.IsFrameOffLimit;
        theMan.dodgeSelectNearestArea += theMan.OutOfMyTouchArea;

        mTouch.evntTouchUp += theMan.RelaseProcess;
        mTouch.evntInitFly += theMan.InitiateFly;
        theMan.evntFlyBack += theMan.FlyBack;

        //touchObj.evntAutoScrl += theMan.AutoScroll;
        theMan.dodgeAutoScrlVec += theMan.OutOfMyTouchArea;
        theMan.dodgeAutoScrl += () => {
            return true; };

        // Liaison Related .. 
        theMan.evntAlienCame = theMan.AlienCame_SetSwitchObj;

        theMan.mLiaison = mLiaison;

        arrMan.Add (theMan);
    }

    int col, row;

    public override void OnGUI ()
    {
        col = 0;
        row = 9;

        //AgUiManDoubleRow curMan = (AgUiManDoubleRow) arrMan [0];

        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {
            //curMan.SortCells (AgUiManDoubleRow.SortBy.Grade, pAscend: true);
        }
        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {

        }
        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {

        }


    }

    public override void SeldomAction ()
    {
        base.SeldomAction ();
        foreach (CuUiManager man in arrMan) {
            man.ArrangeLittle ();
        }
    }
    // Update is called once per frame
    public override void Update ()
    {
        base.Update ();
        mTouch.UpdateAction ();

        if (mLiaison != null)
            mLiaison.UpdateAction ();

        // Split.. Closing..
        if (Input.GetMouseButtonDown (0)) {
            TouchActions ("Down", Input.mousePosition);
        }

        // Scroll & Switch...
        if (Input.GetMouseButton (0)) {
            TouchActions ("Hold", Input.mousePosition);
            //muiMan.TouchAction ("Hold", Input.mousePosition);
        }

        if (Input.GetMouseButtonUp (0)) {
            TouchActions ("Up", Input.mousePosition);
            //muiMan.TouchAction ("Up", Input.mousePosition);
        }

        foreach (CuUiManager man in arrMan) {
            man.UpdateAction ();
        }
        //muiMan.UpdateAction ();
    }

    void TouchActions (string pAct, Vector3 mousePo)
    {
        Vector3 theV = muiCam.GetScreenPosition (mousePo, 0);
        switch (pAct) {
            case "Down":
            mTouch.TouchDown (theV);
            if (mLiaison != null)
                mLiaison.TouchDown (theV);
            break;
            case "Hold":
            mTouch.TouchHold (muiCam.GetScreenPosition (mousePo, 0));
            if (mLiaison != null)
                mLiaison.TouchHold (theV);
            break;
            case "Up":

            mTouch.TouchUp ();
            break;
        }
        //        foreach (CuUiManager man in arrMan) {
        //            man.TouchAction (pAct, mousePo);
        //        }
    }
}