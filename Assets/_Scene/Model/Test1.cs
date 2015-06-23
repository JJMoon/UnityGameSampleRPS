//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test1 : AmSceneBase
{

    public List<GameObject> arrGameObj, arrCardMixObj;
    public Camera muiCam;
    GameObject curObj;
    public bool mDragFlag = false;
    public bool mCardmixFlag = false;
    CuTouch mTouch, mTouchCardMix;
    // Drag Setting..
    AgTile xmuiTile;
    // Private valiables...
    UiState muiState;
    CuUiOption optObj;
    public List<CuFrameOption> arrFrameOpt = new List<CuFrameOption> ();
    CuLiaison mLiaison, mLiaisonMix;
    CuUiManager muiMan;
    List<CuUiManager> arrMan, arrManCardMix;
    //  ////////////////////////////////////////////////     Starting Init Job
    public override void Start ()
    {
        base.Start ();

        arrGameObj = new List<GameObject> ();
        arrCardMixObj = new List<GameObject> ();

        myGUI.SetColumns (3, 10);

        arrMan = new List<CuUiManager> ();
        arrManCardMix = new List<CuUiManager> ();
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
        optObj = new CuUiOption (HoldLimit: 30, SelDist: 0.5f, FlyingSpdMin: 0.05f, SpdLimit: 3f);
        mTouch = new CuTouch (optObj);
        mTouchCardMix = new CuTouch (optObj);

        mLiaison = new CuLiaison ();
        mLiaisonMix = new CuLiaison ();

        //arrGameObj.Clear ();
        // Frame Setting ..  Upper Frame
        int eaX = 11, eaY = 1; // 셀의 갯수. 삽입될 것 고려해서 충분히 잡을 것. 

        //  _////////////////////////////////////////////////_    _____  Frame Option  _____
        CuFrameOption optFrame = new CuFrameOption (optObj, Vert: false, MoveAniSpd: 100, SizeAniSpd: 100, SelectSize: 1.3f, SwitchSize: 0.9f);
        arrFrameOpt.Add (optFrame);

        // Set Touch Manager 
        float dimX, dimY;  // 셀의 크기. 
        Vector3 vecOri, vecMax; // 스크린 좌하/우상 좌표 가져오기. (월드 좌표계로 변환)
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vecOri, out vecMax);
        dimX = (vecMax.x - vecOri.x) / 400; //7; // 스크린 크기를 기준으로 셀의 크기를 정한다.
        dimY = (vecMax.y - vecOri.y) / 300; //8;

        Ag.LogDouble (" TileUITest ::     Screen World Points .. " + vecOri.LogWith ("world 1") + 
            vecMax.LogWith ("world 2") + Ag.mgScrX.LogWith ("ScrX") + Ag.mgScrY.LogWith ("ScrY"));

        AgUiManDoubleRow theMan = new AgUiManDoubleRow ("UPFR", mTouch, optFrame, muiCam);
        theMan.SetFrame (eaX: eaX, eaY: eaY, reverse: false, xyTchFrame: new float[] {
            0.1f, // Xmin
            0.46f, // Ymin
            0.88f, // Xmax
            0.8f, // Ymax
            0.1f,
            0.46f,
            1.78f,
            0.8f
        });


        Ag.LogDouble (" TileUI Test :: Setting  >>>   Vects " + vecOri + ", " + vecMax + " >> dimX/Y : " + dimX + " / " + dimY);

        // Set Wagu, Container ...  TIle Manager ...
        Vector3 boxScale = new Vector3 (dimX * 0.8f, dimY * 0.9f, 0.5f);  // 크기 조절. 
        int serialN = 0;
        for (int ii=0; ii < eaY; ii++) {
            for (int k=0; k < eaX; k++) {

                /*
                GameObject curObj;

                curObj.name = "UP_BOX_" + serialN++; //k.ToString() + ii;
                curObj.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download", "Item" + (serialN % 10 + 1));
                curObj.transform.localScale = boxScale;
                curObj.CellCs ().InitPstnSize (optFrame);
                */

                AmCard playerCard = new AmCard (pGrade: 3, pType: 5, pWthr: "CLOUD");  // Type, Grade, Weather .. setting ...
                //---------------------------------------------------
                if (k < 1) 
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KPtitleA");
                playerCard = new AmCard (pGrade: 1, pType: 1, pWthr: "CLOUD");
                for (int j=1; j<1; j++) {
                    curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                }
                if (k >= 1 && 6 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleA");
                    playerCard = new AmCard (pGrade: 2, pType: 2, pWthr: "CLOUD");
                    for (int j=1; j<2; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 6 && 9 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleB");
                    playerCard = new AmCard (pGrade: 3, pType: 3, pWthr: "CLOUD");
                    for (int j=1; j<3; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 9 && 12 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleC");
                    playerCard = new AmCard (pGrade: 4, pType: 4, pWthr: "CLOUD");
                    for (int j=1; j<4; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 12 && 16 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleD");
                    playerCard = new AmCard (pGrade: 5, pType: 5, pWthr: "CLOUD");
                    for (int j=1; j<5; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 16) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleS");
                    playerCard = new AmCard (pGrade: 1, pType: 1, pWthr: "CLOUD");
                    for (int j=1; j<1; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }

                curObj.name = "Cardmix_" + serialN++; //k.ToString() + ii;
                curObj.transform.localScale = boxScale;
                curObj.CellCs ().InitPstnSize (optFrame);


                (" " + optFrame.optSelectSize).HtLog ();

                arrGameObj.Add (curObj);

                /*
                for (int j=1; j<(serialN % 5 +1); j++) {
                    curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                }
                */
                curObj.transform.FindChild ("Plane_playerface").gameObject.GetComponent<UIButtonMessage> ().target = curObj;


                //-----------------------------------------------------

                curObj.CellCs ().PlCard = playerCard;

                string kind = serialN < 3 ? "ATTK" : "DPNC";
                //theMan.AddACell (curObj, k, ii, kind);


                if (k < 1) {
                    curObj.transform.FindChild ("Plane_playerface").gameObject.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download/playerface", "Keeper0" + (serialN % 2 + 1));
                    theMan.AddACell (curObj, k, ii, "Keeper", false);
                } else {
                    curObj.transform.FindChild ("Plane_playerface").gameObject.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download/playerface", "Kicker0" + (serialN % 8 + 1));
                    theMan.AddACell (curObj, k, ii, "Kicker", false);
                }


                Ag.LogString (" Add a Cell  " + curObj.name + "   " + k.LogWith ("X") + ii.LogWith ("Y"));
            }
        }

        mTouch.evntTouchDown += theMan.CheckInside; // 필수.

        mTouch.evntDrag += theMan.Scroll; // 스크롤 가능하게
        theMan.dodgeScroll += theMan.DidSelected;
        theMan.dodgeScrollArea += theMan.OutOfMyTouchArea;

        mTouch.evntHold += theMan.SelectNearest;
        mTouch.evntHoldMove += theMan.Switch; // 스위치 가능하게..
        mTouch.evntHoldMove += theMan.AutoScroll;

        theMan.dodgeSelectNear += theMan.DidSelected;
        theMan.dodgeSelectNearestArea += theMan.OutOfMyTouchArea;


        mTouch.evntTouchUp += theMan.RelaseProcess;

        mTouch.evntInitFly = theMan.InitiateFly;
        theMan.evntFlyBack = theMan.FlyBack;

        theMan.dodgeAutoScrlVec += theMan.OutOfMyTouchArea;
        theMan.dodgeAutoScrl += theMan.SelectObjIsNull;
        theMan.dodgeAutoScrl += theMan.IsFrameOffLimit;

        theMan.evntAlienCame = theMan.AlienCame_SetSwitchObj;

        theMan.mLiaison = mLiaison;

        //arrManCardMix.Add (theMan);
        arrMan.Add (theMan);




        // Frame Setting ..  Lower Frame
        eaX = 18;
        eaY = 1; // 셀의 갯수. 삽입될 것 고려해서 충분히 잡을 것. 

        //  _////////////////////////////////////////////////_    _____  Frame Option  _____
        optFrame = new CuFrameOption (optObj, Vert: false, MoveAniSpd: 100, SizeAniSpd: 100, SelectSize: 1.2f, SwitchSize: 0.9f, 
                                      SwitchInFrame: false);
        arrFrameOpt.Add (optFrame);

        // Set Touch Manager 
        muiCam.GetComponent<UICam> ().GetWorldPointsOfCurScreen (out vecOri, out vecMax);
        dimX = (vecMax.x - vecOri.x) / 400; //7; // 스크린 크기를 기준으로 셀의 크기를 정한다.
        dimY = (vecMax.y - vecOri.y) / 300; //8;

        Ag.LogDouble (" TileUITest ::     Screen World Points .. " + vecOri.LogWith ("world 1") + 
            vecMax.LogWith ("world 2") + Ag.mgScrX.LogWith ("ScrX") + Ag.mgScrY.LogWith ("ScrY"));

        theMan = new AgUiManDoubleRow ("LOWR", mTouch, optFrame, muiCam); //, new float[] { vecOri.x * 0.9f, vecMax.x*0.9f, vecOri.y*0.9f, vecMax.y*0.9f } );
        theMan.SetFrame (eaX: eaX, eaY: eaY, reverse: false, xyTchFrame: new float[] {
            0.25f, // Xmin
            0.195f, // Ymin
            0.845f, // Xmax
            0.4f, // Ymax
            0.25f,
            0.195f,
            3f,
            0.4f
        });


        Ag.LogDouble (" TileUI Test :: Setting  >>>   Vects " + vecOri + ", " + vecMax + " >> dimX/Y : " + dimX + " / " + dimY);
        Ag.LogString (" Option " + optFrame.optMoveAniSpd.LogWith ("AniSpd"));

        // Set Wagu, Container ...  TIle Manager ...

        //new float[] { vecOri.x, vecMax.x, vecOri.y, vecMax.y } );
        //boxScale = new Vector3 (dimX * 0.8f, dimY * 0.8f, 0.5f);  // 크기 조절. 
        boxScale = new Vector3 (dimX * 0.8f, dimY * 0.9f, 0.5f);  // 크기 조절. 
        serialN = 0;
        //        for (int k=0; k< eaX - 6; k++) { // 12를 기본으로 넣고.. 6개까지 추가 가능.
        //            for (int ii=0; ii< 1; ii++) {
        for (int ii=0; ii < eaY; ii++) {
            for (int k=0; k < eaX; k++) {
                //GameObject curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "ATile");
                //curObj.name = "LW_BOX_" + serialN++; //k.ToString() + ii;
                //curObj.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download", "Item" + (serialN % 10 + 1));
                //curObj.transform.localScale = boxScale;


                (" " + optFrame.optSelectSize).HtLog ();

                //---------------------------------------
                AmCard playerCard = new AmCard (pGrade: 3, pType: 5, pWthr: "CLOUD");
                if (k < 1) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleA");
                    playerCard = new AmCard (pGrade: 2, pType: 2, pWthr: "CLOUD");

                }
                if (k >= 1 && 6 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleA");
                    playerCard = new AmCard (pGrade: 2, pType: 2, pWthr: "CLOUD");
                    for (int j=1; j<2; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 6 && 9 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleB");
                    playerCard = new AmCard (pGrade: 3, pType: 3, pWthr: "CLOUD");
                    for (int j=1; j<3; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 9 && 11 > k) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleC");
                    playerCard = new AmCard (pGrade: 4, pType: 4, pWthr: "CLOUD");
                    for (int j=1; j<4; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                if (k >= 11) {
                    curObj = mRscrcMan.GetPrefabAt (Folder: "_TestMoon", pName: "KtitleS");
                    playerCard = new AmCard (pGrade: 1, pType: 1, pWthr: "CLOUD");
                    for (int j=1; j<1; j++) {
                        curObj.transform.FindChild ("star/star" + j).gameObject.SetActive (false);
                    }
                }
                curObj.name = "UP_CardMix_" + serialN++; //k.ToString() + ii;
                arrGameObj.Add (curObj);
                curObj.transform.localScale = boxScale;
                curObj.CellCs ().InitPstnSize (optFrame);
                //curObj.transform.FindChild ("Plane_playerface").gameObject.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download/playerface", "Kicker0" + (serialN % 8 + 1));




                theMan.AddACell (curObj, k, ii, "Kicker", false);
                curObj.transform.FindChild ("Plane_playerface").gameObject.transform.renderer.material.mainTexture = mRscrcMan.GetTextureIn ("Download/playerface", "Kicker0" + (serialN % 8 + 1));

                //curObj.transform.FindChild ("Plane_playerface").gameObject.GetComponent<UIButtonMessage> ().target = GameObject.Find ("Axis/Camera/Match").gameObject;
                curObj.transform.FindChild ("Plane_playerface").gameObject.GetComponent<UIButtonMessage> ().target = curObj;

                //---------------------------------------
                //AmCard playerCard = new AmCard (pGrade: 3, pType: 5, pWthr: "CLOUD");  // Type, Grade, Weather .. setting ...
                curObj.CellCs ().PlCard = playerCard;



                string kind = serialN < 4 ? "ATTK" : "DPNC";
                //theMan.AddACell (curObj, k, ii, kind);

                Ag.LogString (" Add a Cell  " + curObj.name + "   " + k.LogWith ("X") + ii.LogWith ("Y"));


            }
        }

        mTouch.evntTouchDown += theMan.CheckInside;

        mTouch.evntDrag += theMan.Scroll;
        theMan.dodgeScroll += theMan.DidSelected;
        theMan.dodgeScrollArea += theMan.OutOfMyTouchArea;

        mTouch.evntHold += theMan.SelectNearest;
        mTouch.evntHoldMove += theMan.Switch;

        theMan.dodgeSelectNear += theMan.DidSelected;
        theMan.dodgeSelectNearestArea += theMan.OutOfMyTouchArea;

        mTouch.evntTouchUp += theMan.RelaseProcess;
        mTouch.evntInitFly += theMan.InitiateFly;
        theMan.evntFlyBack += theMan.FlyBack;

        //touchObj.evntAutoScrl += theMan.AutoScroll;
        theMan.dodgeAutoScrlVec += theMan.OutOfMyTouchArea;
        theMan.dodgeAutoScrl += theMan.IsFrameOffLimit;
        theMan.dodgeAutoScrl += () => {
            return true; };

        // Liaison Related .. 
        theMan.evntAlienCame = theMan.AlienCame_SetSwitchObj;

        theMan.mLiaison = mLiaison;

        arrMan.Add (theMan);
        //arrManCardMix.Add (theMan);

    }

    int col, row;

    void OnGUI ()
    {
        col = 0;
        row = 9;
        /*
        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {

        }
        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {

        }
        if (GUI.Button (myGUI.GetRect (col++, row), "Sort by Grade")) {

        }
        */


    }

    public void SortbyGrade (string pStr)
    {

        AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrMan [1];
        //AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrManCardMix [0];
        curMan.SortCells (AgUiManDoubleRow.SortBy.Grade, pAscend: pStr == "1" ? true : false);
    }

    public void SortbyType (string pStr)
    {
        AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrMan [1];
        //AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrManCardMix [0];
        curMan.SortCells (AgUiManDoubleRow.SortBy.Type, pAscend: pStr == "1" ? true : false);
    }

    public void SortybyWeather (string pStr)
    {
        AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrMan [1];
        //AgUiManDoubleRow curMan = (AgUiManDoubleRow)arrManCardMix [0];
        curMan.SortCells (AgUiManDoubleRow.SortBy.Weather, pAscend: pStr == "1" ? true : false);
    }

    public override void SeldomAction ()
    {
        base.SeldomAction ();
        foreach (CuUiManager man in arrMan) {
            man.ArrangeLittle ();
        }

        foreach (CuUiManager man in arrManCardMix) {
            man.ArrangeLittle ();
        }
    }
    // Update is called once per frame
    public override void Update ()
    {
        base.Update ();
        mTouch.UpdateAction ();
        mTouchCardMix.UpdateAction ();



        if (mLiaison != null)
            mLiaison.UpdateAction ();
        if (mLiaisonMix != null)
            mLiaisonMix.UpdateAction ();

        // Split.. Closing..

        if (mDragFlag) {
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
        }

        /*
        if (mDragFlag) {
            if (Input.GetMouseButtonDown (0)) {
                if (!mCardmixFlag)
                    TouchActions ("Down", Input.mousePosition);
                else
                    TouchActionsCardMix ("Down", Input.mousePosition);
            }

            // Scroll & Switch...
            if (Input.GetMouseButton (0)) {
                if (!mCardmixFlag)
                    TouchActions ("Hold", Input.mousePosition);
                else
                    TouchActionsCardMix ("Hold", Input.mousePosition);
            }

            if (Input.GetMouseButtonUp (0)) {
                if (!mCardmixFlag)
                    TouchActions ("Up", Input.mousePosition);
                else
                    TouchActionsCardMix ("Up", Input.mousePosition);
            }

        }
        */


        foreach (CuUiManager man in arrMan) {
            man.UpdateAction ();
        }
        foreach (CuUiManager man in arrManCardMix) {
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
            if (mLiaison != null)
                mLiaison.TouchUp ();
            mTouch.TouchUp ();
            break;
        }
       
    }

    void TouchActionsCardMix (string pAct, Vector3 mousePo)
    {
        Vector3 theV = muiCam.GetScreenPosition (mousePo, 0);
        switch (pAct) {
        case "Down":
            mTouchCardMix.TouchDown (theV);
            if (mLiaisonMix != null)
                mLiaisonMix.TouchDown (theV);
            break;
        case "Hold":
            mTouchCardMix.TouchHold (muiCam.GetScreenPosition (mousePo, 0));
            if (mLiaisonMix != null)
                mLiaisonMix.TouchHold (theV);
            break;
        case "Up":
            if (mLiaisonMix != null)
                mLiaisonMix.TouchUp ();
            mTouchCardMix.TouchUp ();
            break;
        }

    }
}
