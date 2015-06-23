//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


// [2012:10:09:MOON]
// [2012:10:15:MOON] Timeout => go to Match Scene
// [2012:11:9:ljk] DirectionArea deleted
// [2012:11:12:MOON] Heart Beat
// [2012:12:6:MOON] Swipe Distance
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    bool mEnemyLeftFlag;

    void Awake ()
    {
        Application.targetFrameRate = 60;
        Ag.LogNewScene ("Game310_2", "Start");
        mSX = Ag.mgScrX;
        mSY = Ag.mgScrY;
    }

    void TimeProcess (float theTime)
    {
        string curName = mStateArr.GetCurStateName ();
        if (curName == "Curst Start Moving") {
            if (!mTouchTime.HasValue)
                mTouchTime = theTime;
        }
        if (curName == "Touch waiting") {
            mStage.mIsTouched = true;
            mTouchTime = theTime - mTouchTime.Value;
        }
    }

    static float aspectRatio;
    float? mTouchTime;
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>> initialization  Start  <<<<<
    public override void Start ()
    {
        mEnemyLeftFlag = false;

        Ag.CurrentScene = "GAME";

        base.Start ();
        //AgStt.BenderObj.EventTime += TimeProcess;
        mTouchTime = null;
        GameInit ();
        mStateArr = new StateArray ();
        mStage = new AmStage ();
        mStage.arrTheState = mStateArr;


        if (AgStt.mgGameTutorial) {
            Debug.Log ("Tutorial STart");
            SetStateArrayTutorial ();
        } else {
            if (Ag.mSingleMode) {
                Ag.NodeObj.MySocket.SceneLoadComplete.InitSet (true);
                SetStateArraySingleMode ();
            } else {
                Ag.NodeObj.MySocket.ActionSceneLoadComplete ();
                SetStateArray ();  // Set State Machine ......
            }
        }

        CommonStateMethods ();
        //SetArrTexture ();  // Set Texture Array...
        newPosition = mCameraDefn.transform.position;
    }

    public void IleftGame ()
    {
        //mStateArr.SetStateWithNameOf ("ShowEndingResult");
        mEnemyLeftFlag = true;
    }

    public void EnemyLeftGame ()
    {
        mEnemyLeftFlag = true;
        Ag.LogIntenseWord (" EnemyLeft ");
        if (Ag.NodeObj.GameFinish.HasValue)
            return;

        Ag.mgDidWin = true;
        Ag.NodeObj.GameFinish = true; // I won .. 

        if (mStateArr.GetCurStateName () != "ShowEndingResult") {
            mStateArr.SetStateWithNameOf ("EndingCeremony");
        }
    }

    public IEnumerator xBlankPack ()
    {
        while (true) {
            yield return new WaitForSeconds (2);
        }
    }

    void DragDown ()
    {
        mDragExitBtn.animation ["DragBtn"].speed = 1;
        mDragExitBtn.animation.Play ();
        StartCoroutine (StartdrgQuitBtn (2f));
    }

    void PointAniOff ()
    {
        mPoint.SetActive (false);
    }
    //--------------------------------------------------------------------------------------ItemBox
    void MkballActive (bool pActive)
    {

    }

    void BallChangeTexture (string Pstring)
    {
        mKickBall.transform.renderer.material.mainTexture = (Texture2D)Resources.Load ("500Game/UI/" + Pstring);
    }
    //-------------------------------------------------------------------------------ItemBox
    void GotoMenu ()
    {
        Application.LoadLevel ("StartMenu");
    }

    void GoalKeeperCer ()
    {
        if (mStateArr.GetCurStateName () == "EndingCeremony") {
            switch (mKPlastCer) {
            
            case 5:
                KeeperLoseAni1 ();
                break;
            case 6:
                KeeperLoseAni2 ();
                break;
            case 7:
                KeeperLoseAni3 ();
                break;
            case 8:
                KeeperLoseAni4 ();
                break;
            }
        }
    }
    //------------------------------------------------------------------------------- Position Change
    bool EnemUserCheck;

    void EnemyLeftOk ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["alert_someoneout"].SetActive (false);
        //Ag.NodeObj.LeaveMyself ();
        Ag.NodeObj.UserModify ("ONLINE", statusOnly: true);
        Ag.GameStt.GoingOutFromMatching ();
        Application.LoadLevel ("StartMenu"); // 425 MOON
        //mStateArr.SetStateWithNameOf ("ShowEndingResult");
    }

    bool mNetworkError;
    bool mGameOver;

    public void AlertOut ()
    {
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            BgmSound.Instance.Play ();
        }
        Application.LoadLevel ("StartMenu");
    }

    void DebugSetting (int Dir, int Skl, int EnemDir, int EnemSkl)
    {
        Ag.mgDirection = (byte)Dir;
        Ag.mgSkill = (byte)Skl;
        Ag.mgEnemDirec = (byte)EnemDir;
        Ag.mgEnemSkill = (byte)EnemSkl;
    }

    string stt;

    void SelectRightUp ()
    {
        Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 1;
        mStage.TouchProcess ();
        SetPlayerDir2 ();
        mStartDrag = true;
    }

    void SelectRightDown ()
    {
        Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 3;
        mStage.TouchProcess ();
        SetPlayerDir2 ();
        mStartDrag = true;
    }

    void SelectLeftUp ()
    {
        Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 2;
        mStage.TouchProcess ();
        SetPlayerDir2 ();
        mStartDrag = true;
    }

    void SelectLeftDown ()
    {
        Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 4;
        mStage.TouchProcess ();
        SetPlayerDir2 ();
        mStartDrag = true;
    }

    void GameInput ()
    {
        if (Input.GetKeyDown (KeyCode.Q)) {
            //Debug.Log ("KeyisClicked");
            Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 1;
            mStage.TouchProcess ();
            SetPlayerDir2 ();
            mStartDrag = true;
        }
        if (Input.GetKeyDown (KeyCode.W)) {
            //Debug.Log ("KeyisClicked");
            Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 2;
            mStage.TouchProcess ();
            SetPlayerDir2 ();
            mStartDrag = true;
        }
        if (Input.GetKeyDown (KeyCode.E)) {
            //Debug.Log ("KeyisClicked");
            Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 3;
            mStage.TouchProcess ();
            SetPlayerDir2 ();
            mStartDrag = true;
        }
        if (Input.GetKeyDown (KeyCode.R)) {
            //Debug.Log ("KeyisClicked");
            Ag.NodeObj.Direction = mIsKeeperSkl = Ag.mgDirection = 4;
            mStage.TouchProcess ();
            SetPlayerDir2 ();
            mStartDrag = true;
            //mKeeperYellowflag = true;
        }


    }

    public bool mExitflag;

    bool NullCheck ()
    {
        if (AgStt.mgGameTutorial)
            return Ag.NodeObj == null || Ag.NetExcpt.ConnectLossSignalGone;
        //return Ag.NodeObj == null || Ag.NodeObj.MySocket.EnemyLeft || Ag.NetExcpt.ConnectLossSignalGone || !Ag.NodeObj.AmHost.HasValue;
        return Ag.NodeObj == null || Ag.GameStt.NullCheck || !Ag.NodeObj.AmHost.HasValue;
    }

    void ShowNetworkPopup (bool enemyOutCase)
    {
        Ag.LogIntenseWord ("  GameScene ::   ShowNetworkPopup ....    NullCheck  True   ");
        dicGameSceneMenuList ["popup"].SetActive (true);
        if (enemyOutCase)
            dicGameSceneMenuList ["alert_someoneout"].SetActive (true);
        else
            dicGameSceneMenuList ["alert_networkerror"].SetActive (true);
    }

    float Percent;
    int Contmm, Contss;
    bool VictoryTimeFlag;

    public override void Update ()
    {
        if (Ag.NetExcpt.ConnectLossSignalGone) {
            ShowNetworkPopup (false);
            return;
        }

        if (NullCheck () && (mStateArr.GetCurStateName () != "ShowEndingResult" && mStateArr.GetCurStateName () != "EndingCeremony")) {
            ShowNetworkPopup (enemyOutCase: true);
            return;
        }

        if (!Ag.NodeObj.ReMatchRefuseSent.UI && Ag.NodeObj.ReMatchRefuseSent.Enem) {
            EnemyRefusedRematching ();
            Ag.NodeObj.ReMatchRefuseSent.UI = true;
        }

        if (mStateArr.GetCurStateName () == "ShowEndingResult" && !AgStt.mgGameTutorial) {
            Ag.mySelf.ContWinCoolTimeRemain (out Contmm, out Contss);
            dicGameSceneMenuList ["victory_info"].transform.FindChild ("Label_victorytime").gameObject.GetComponent<UILabel> ().text = Contmm.ToFixedWidth (2) + ":" + Contss.ToFixedWidth (2);
            dicGameSceneMenuList ["victory_info"].transform.FindChild ("Progress_victory/Foreground").GetComponent<UISprite> ().fillAmount = Ag.mySelf.ContWinCoolTimeRemainPercent () / 100;
        }


        /*
        if (Input.GetKeyDown (KeyCode.Escape)) { 
            if (mExitflag)
                GotoHome ();
            if (!mExitflag)
                mExitflag = true;
        }
        */
        //---------------------------------------KeeperScout cooltime
        // fill Scout Amount
        int mm, ss;
        Ag.mySelf.ScoutCoolTimeRemain (out mm, out ss);
        if (!Ag.mySelf.CanIScoutNow && (Ag.mySelf.CollTimeScoutPercent () / 100) < 1) {
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Foreground").gameObject.GetComponent<UISprite> ().fillAmount = Ag.mySelf.CollTimeScoutPercent () / 100;
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_time").gameObject.GetComponent<UILabel> ().text = mm.ToFixedWidth (2) + ":" + ss.ToFixedWidth (2);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_time").transform.gameObject.SetActive (true);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_cash").transform.gameObject.SetActive (true);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("cash").transform.gameObject.SetActive (true);

        } 
        if (Ag.mySelf.CanIScoutNow || Ag.mySelf.CollTimeScoutPercent () < 1) {
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_time").transform.gameObject.SetActive (false);
            //dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_time").gameObject.GetComponent<UILabel> ().text = "0" + ":" + "00";
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_cash").transform.gameObject.SetActive (false);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("cash").transform.gameObject.SetActive (false);
        }

        //Debug.Log ("Ag.mySelf.CollTimeScoutPercent()" + Ag.mySelf.CollTimeScoutPercent()/100);

        base.Update ();
        mStateArr.DoAction ();
        stt = mStateArr.GetCurStateName ();
        //Debug.Log ("Ag.mySelf.CollTimeScoutPercent()" + Ag.mySelf.CollTimeScoutPercent()/100);



        if (mGameScoreeff) {
            GameScoreEff ();
        }

        if (mGameOver && mEnemyLeftFlag) {
            mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
            dicGameSceneMenuList ["btn_Label"].SetActive (true);
            // 상대가 이미 퇴장함
            mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text =
                WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EC%9D%B4%EB%AF%B8%20%ED%87%B4%EC%9E%A5%ED%95%A8");
            mEnemyLeftFlag = false;
        }

        if (!mGameOver && mEnemyLeftFlag) {
            dicGameSceneMenuList.SetActiveAll (true, new string[] {
                "popup",
                "alert_someoneout"
            });
            mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
            Ag.mgDidWin = true;
            mNetworkError = true;
            mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = 
                WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EC%9D%B4%EB%AF%B8%20%ED%87%B4%EC%9E%A5%ED%95%A8");
            mEnemyLeftFlag = false;
        }

        if (Ag.NetExcpt.WasLoginDuplicate) {
            DoubleLoginError ();
        }

        if (Ag.NodeObj.RematchBoth) {
            //하트부족시에는 팝업

            Ag.mGreenItemFlag = false;
            Ag.mRedItemFlag = false;
            Ag.mBlueItemFlag = false;
            RematchWasGameStart ();
            Ag.NodeObj.ReMatchSent.InitSet (false);
            Ag.NodeObj.PrepareGame ();

            Application.LoadLevel ("GameScene");
        }

        if (Ag.NodeObj.ReMatchSent.Enem && EnemUserCheck) {
            EnemySentRematchAndWait ();
            StartCoroutine (RematchNoAccept (8));
            EnemUserCheck = false;
        }

        if (mStateArr.GetCurStateName () == "ShowEndingResult" && mResultShow) {
            ResultShow ();
        }
        if (mDirLightFlag) {
            if (mDirLight.GetComponent<Light> ().intensity < 0.8)
                mDirLight.GetComponent<Light> ().intensity += 0.005f;
        }
        string curStt = mStateArr.GetCurStateName ();


        GoalKeeperCer ();
        RedbullNum ();

        if (mStateCere != null)
            mStateCere.DoAction ();
        if (Ag.IsSmartDevice ()) {
            if (!Ag.mgIsKick && mStage.WillInputDrag ()) {
                foreach (Touch touch in Input.touches) {
                    if (!mDidDragStarted) {
                        mVecInit = touch.position;
                        mDidDragStarted = true;
                    } else {
                        mVecFin = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        if (AgStt.mgGameTutorial && !Ag.mgIsKick) {
                            if ((Ag.mRound == 1 && mKeeperDragFlag) || (Ag.mRound == 2 && mKeeperDragFlag))
                                DirectionDrag ();
                        } else {
                            DirectionDrag ();
                        }
                        SetPlayerDir2 ();
                        mStartDrag = true;
                    }
                }
            }
            //------------------------------------------
            if (Input.GetMouseButtonDown (0)) {   // Skill Input
                if (mStage.TouchProcess () && mStage.mIsTouchEnable) {
                    mStage.mCursorPosition = mStage.mCursorPosition > 999 ? 999 : mStage.mCursorPosition;

                    if (curStt == "GameDir") {
                        if (AgStt.mgGameTutorial)
                            mStage.mCursorPosition = mStage.mCursorPosition > 998 ? 998 : mStage.mCursorPosition;
                        Ag.NodeObj.Direction = Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);
                    }
                        
//                    if (curStt == "GameSkl") {
//                        Ag.NodeObj.Skill = Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
//                        if (myCard.WAS.grade == "S") {
//                            Ag.NodeObj.Skill += 1;
//                            Ag.mgSkill += 1;
//                            if (!Ag.mgIsKick && Ag.mgSkill == 3)
//                                Ag.NodeObj.Skill = Ag.mgSkill = 2;
//                        }
//                        Ag.LogString ("  Touched >>>>>    Card : " + myCard.WAS.grade + "     Ag.mgSkill >" + Ag.mgSkill + "       Ag.NodeObj.Skill : " + Ag.NodeObj.Skill);
//                    }
//
                    if (curStt == "GameSkl") {
                        Ag.NodeObj.Skill = Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);

                        if (myCard.WAS.grade == "S")
                            Ag.NodeObj.Skill = Ag.mgSkill += 1;
                        if (!Ag.mgIsKick && Ag.mgSkill == 3) {
                            Ag.LogIntenseWord ("     (!Ag.mgIsKick && Ag.mgSkill == 3)   Case ::  Set Skill to ... 2 ");
                            Ag.NodeObj.Skill = Ag.mgSkill = 2;
                        }

                        Ag.LogString ("  Touched >>>>>    Card : " + myCard.WAS.grade + "     Ag.mgSkill >" + Ag.mgSkill + "       Ag.NodeObj.Skill : " + Ag.NodeObj.Skill);
                    }
                }
            }
            //------------------------------------------
        }

        // PC Input......// if (Ag.mPlatform == Ag.Platform.OSX || !Ag.IsSmartDevice ()) {//[2012:11:9:ljk]
        if (!Ag.IsSmartDevice ()) {//[2012:11:9:ljk]

            if (mStage.WillInputDrag ()) {       // Goul keeper Direction...
                if (AgStt.mgGameTutorial) {
                    if ((Ag.mRound == 1 && mKeeperDragFlag) || (Ag.mRound == 2 && mKeeperDragFlag)) {
                        GameInput ();
                    }
                } else {
                    GameInput ();
                }
            }
            if (Input.GetMouseButtonDown (0)) {   // Skill Input
                if (mStage.TouchProcess ()) {
                    if (curStt == "GameDir") {
                        Ag.NodeObj.Direction = Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);
                        SetPlayerDir2 ();
                    }
                    if (curStt == "GameSkl") {
                        Ag.NodeObj.Skill = Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);

                        if (myCard.WAS.grade == "S")
                            Ag.NodeObj.Skill = Ag.mgSkill += 1;

//                        if (myCard.WAS.grade == "S") {  // 4 Debugging
//                            if (myCard.WAS.isKicker)
//                                Ag.NodeObj.Skill = Ag.mgSkill = 3;
//                            else
//                                Ag.NodeObj.Skill = Ag.mgSkill += 1;
//                        }

                        if (!Ag.mgIsKick && Ag.mgSkill == 3) {
                            Ag.LogIntenseWord ("     (!Ag.mgIsKick && Ag.mgSkill == 3)   Case ::  Set Skill to ... 2 ");
                            Ag.NodeObj.Skill = Ag.mgSkill = 2;
                        }
                        Ag.LogString ("  Touched >>>>>    Card : " + myCard.WAS.grade + "     Ag.mgSkill :: " + Ag.mgSkill + "       Ag.NodeObj.Skill :: " + Ag.NodeObj.Skill);
                    }
                }
            }
        }
    }
    //-------Add 08 29
    void DirectionDrag ()
    {
        mDidDragStarted = false;
        // Distance..
        float distDrag = Mathf.Sqrt (Mathf.Pow (mVecInit.y - mVecFin.y, 2) + Mathf.Pow (mVecInit.x - mVecFin.x, 2));
        if (distDrag < 0.1 * Ag.mgScrX)
            return;
        
        Vector2 dirVec = mVecFin - mVecInit;
        int dirVal;
        // Right... 
        if (dirVec.x > 0) {
            dirVal = (dirVec.y > 0) ? 1 : 3;
        } else {
            dirVal = (dirVec.y > 0) ? 2 : 4;
        }

        Ag.mgDirection = (byte)dirVal;
        Ag.NodeObj.Direction = (byte)dirVal;
        //mDidDragStarted = false;
        //KeeperSwipeCameraMove ();
    }

    int MyDir, MySkl;

    public void DebugMode ()
    {
        if (NullCheck ()) {
            //Ag.LogIntenseWord ("  GameScene :: Debug Mode  ....    NullCheck  True   ");
            return;
        }
        if (Ag.mgIsKick) {
            Ag.NodeObj.Direction = Ag.mgDirection = 4;
            Ag.NodeObj.Skill = Ag.mgSkill = 2;
            Ag.mgEnemDirec = 2;
            Ag.mgEnemSkill = 2;


        } else {
            Ag.NodeObj.Direction = Ag.mgDirection = 2;
            Ag.NodeObj.Skill = Ag.mgSkill = 2;
            Ag.mgEnemDirec = 4;
            Ag.mgEnemSkill = 2;
        }

//        Ag.mgEnemDirec = 2;
//        Ag.mgEnemSkill = 2;
//

        GUI.Label (new Rect (310, 110, 100, 100), "MYDIR + MYSKL      :     " + Ag.NodeObj.Direction + " : " + Ag.mgSkill);
        if (GUI.Button (new Rect (310, 210, 50, 50), "1")) {
            MyDir = Ag.NodeObj.Direction = Ag.mgDirection = 1;
        }
        if (GUI.Button (new Rect (410, 210, 50, 50), "2")) {
            MyDir = Ag.NodeObj.Direction = Ag.mgDirection = 2;
        }
        if (GUI.Button (new Rect (510, 210, 50, 50), "3")) {
            MyDir = Ag.NodeObj.Direction = Ag.mgDirection = 3;
        }
        if (GUI.Button (new Rect (610, 210, 50, 50), "4")) {
            MyDir = Ag.NodeObj.Direction = Ag.mgDirection = 4;
        }
        if (GUI.Button (new Rect (710, 210, 50, 50), "5")) {
            MyDir = Ag.NodeObj.Direction = Ag.mgDirection = 5;
        }

        
        if (GUI.Button (new Rect (310, 260, 50, 50), "1")) {
            MySkl = Ag.NodeObj.Skill = Ag.mgSkill = 1;
        }
        if (GUI.Button (new Rect (410, 260, 50, 50), "2")) {
            MySkl = Ag.NodeObj.Skill = Ag.mgSkill = 2;
        }
        if (GUI.Button (new Rect (510, 260, 50, 50), "3")) {
            MySkl = Ag.NodeObj.Skill = Ag.mgSkill = 3;
        }


        GUI.Label (new Rect (310, 410, 310, 100), "ENEMDIR + ENEMSKL    :   " + Ag.mgEnemDirec + "   :   " + Ag.mgEnemSkill);

        /*
        if (GUI.Button (new Rect (310, 410, 50, 50), "1")) {
             Ag.mgEnemDirec = 1;
        }
        if (GUI.Button (new Rect (410, 410, 50, 50), "2")) {
            Ag.mgEnemDirec = 2;
        }
        if (GUI.Button (new Rect (510, 410, 50, 50), "3")) {
            Ag.mgEnemDirec = 3;
        }
        if (GUI.Button (new Rect (610, 410, 50, 50), "4")) {
            Ag.mgEnemDirec = 4;
        }
        if (GUI.Button (new Rect (710, 410, 50, 50), "5")) {
            Ag.mgEnemDirec = 5;
        }
        */

        



    }

    int mDebugSkl;

    public override void OnGUI ()
    {
        /*
        if (GUI.Button (new Rect (110, 210, 100, 100), "Game Quit")) {
            Ag.mgDidWin = true;
            Ag.NodeObj.GameFinish = true; // I won .. 
            mStateArr.SetStateWithNameOf ("EndingCeremony");
        }
        */





    

        base.OnGUI ();

        if (Ag.mgEnemGiveup && stt != "ShowEndingResult" && stt != "GameFinish") {
            if (stt != "ReadUserInfo")
                mStateArr.SetStateWithNameOf ("ReadUserInfo");
        }
        if (mAwayMyself) { // [2012:10:15:MOON] Timeout => go to Match Scene
            //if ( GUI.Button( mAwayRect, mAwaySelf )  ) {
        }
        if (mStatusSillBar)
            //statusSkill ();
        if (mStatusSkillSound)
            SkillSound ();
        
        // Touch Process......

//        if (Ag.IsSmartDevice () && !mStage.mIsTouched && Input.touchCount > 0) {
//            mStartTime = Time.timeSinceLevelLoad;
//            if (mStage.TouchProcess ()) {
//                if (stt == "GameSkl") {
//                    //AgUtil.LinearPercentVari (mStage.mCursorPosition);
//                    Ag.NodeObj.Skill = Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition); // Save Touch Points [GAM_RLT]
//                    if (myCard.WAS.grade == "S") {
//                        Ag.NodeObj.Skill += 1;
//                        Ag.mgSkill += 1;
//                    }
//                    if (!Ag.mgIsKick && Ag.mgSkill == 3) {
//                        Ag.LogIntenseWord ("     (!Ag.mgIsKick && Ag.mgSkill == 3)   Case ::  Set Skill to ... 2 ");
//                        Ag.NodeObj.Skill = Ag.mgSkill = 2;
//                    }
//                    mStatusSkillSound = true;  
//                }
//                if (Ag.mgIsKick && stt == "GameDir")
//                    Ag.NodeObj.Direction = Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);
//                     
//                Ag.LogIntenseWord ("Touched Direction is >> " + Ag.mgDirection);
//                return;
//            }
//        }

        //--------------------------DebugMOde
        //DebugMode ();
        //Ag.mgEnemSkill = (byte)mDebugSkl;
        //Debug.Log ("Ag.mgEnemSkill :: " + Ag.mgEnemSkill );
        

        // Draw Direction, Skill Bar  &   Cursor....
        if (mStage.WillDrawDirection ())
            DrawGameDirection ();
        if (mStage.WillDrawSkill ())
            DrawGameSkill ();
        
        //GUI.Label (new Rect (scrX*0.5f, scrY*0.95f, scrX*0.5f, scrY*0.05f), "Init_ " + mVecInit.x + " , " + mVecInit.y + 
        //  "     Fin_  " +  mVecFin.x + " , " + mVecFin.y  , mGuiCur);
    }

    void DragPosition (bool flag)
    {
        for (int i = 0; i < 4; i++) {
            arrKeeperBarB [i].SetActive (flag);
        }
    }

    void DragPositionF (bool Flag1)
    {
        for (int i = 0; i < 4; i++) {
            arrKeeperBarF [i].SetActive (Flag1);
        }
    }

    void DragPositionLastSetDir (bool Flag1)
    {
        for (int i = 0; i < 4; i++) {
            arrKeeperBarS [i].SetActive (Flag1);
            arrKeeperBarD [i].SetActive (Flag1);
        }
    }
    //  ////////////////////////////////////////////////     RedbullNum..
    void RedbullNum ()
    {
//        mRedbullCount.text = Ag.mySelf.mPorsion.ToString();
    }

    void KickerScenePlay (bool pflag)
    {
        if (!pflag) {
            //mAdv3.transform.FindChild ("ads1").animation.Stop ();
            //mAdv3.transform.FindChild ("ads2").animation.Stop ();
            //mAdv3.transform.FindChild ("ads3").animation.Stop ();
        } else {
            //mAdv3.transform.FindChild ("ads1").animation.Play ();
            //mAdv3.transform.FindChild ("ads2").animation.Play ();
            //mAdv3.transform.FindChild ("ads3").animation.Play ();
        }
    }

    void SkillSoundAfter ()
    {
        if (Ag.mgIsKick) {
            if (Ag.mgSkill == 2) {
                SoundManager.Instance.Play_Effect_Sound ("Sound effect Kickerfoot");
            }
        } else {
            if (Ag.mgSkill == 2) {
                SoundManager.Instance.Play_Effect_Sound ("Sound effect GoalkeeperH");
            }
        }
    }
    //-----------------------------------------------------------------------------------SoundPlay
    void SkillSound ()
    {
        string fileName = "";
        switch (Ag.mgSkill) {
        case 0:
            fileName = "Bad";
            mSkillSound = true;
            break;
        case 1:
            fileName = "Good";
            break;
        case 2:
            fileName = "Perfect";
            break;    
        }
        SoundManager.Instance.Play_Effect_Sound (fileName);
        mStatusSkillSound = false;
    }
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Animation   <<<<<
    void CameraSet ()
    {
        if (Ag.mgIsKick) {
            mCameraKick.enabled = true;
            mCameraDefn.enabled = false;
        } else {
            mCameraKick.enabled = false;
            mCameraDefn.enabled = true;
        }
    }
    //  ////////////////////////////////////////////////     Pre Animation Play..
    //--------------------------------------------------------------------------------------Texture
    //--------------------------------------------------------------------------------------
    void GotoStartScene ()
    {
        Ag.NodeObj.LeaveMyself ();
        Application.LoadLevel ("StartMenu");
    }

    void KeeperAction ()
    {
        mStateCere = new StateArray ();
        mRotSpeed = 1.2f;
        mStateCere.AddAMember ("Init", 4f);
        mStateCere.AddExitCondition (() => {
            return Mathf.Abs (mCerCamAxis.transform.rotation.y) < 0.7f;
        });
        mStateCere.AddAMember ("Clock", 0.5f);
        mStateCere.AddEntryAction (() => {
            mRotSpeed = 1.2f;
        });
        mStateCere.AddExitCondition (() => {
            return Mathf.Abs (mCerCamAxis.transform.rotation.y) < 0.7f;
        });
        mStateCere.AddAMember ("Counter", 0.5f);
        mStateCere.AddEntryAction (() => {
            mRotSpeed = -0.85f;
        });
        mStateCere.AddExitCondition (() => {
            return Mathf.Abs (mCerCamAxis.transform.rotation.y) < 0.7f;
        });
        mStateCere.SetNextStateOf ("Init", "Counter");
        mStateCere.SetNextStateOf ("Clock", "Counter");
        mStateCere.SetNextStateOf ("Counter", "Clock");
        mStateCere.SetStateWithNameOf ("Init");
    }
}
     