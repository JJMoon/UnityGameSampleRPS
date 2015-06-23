//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    public static bool g_TeamSklUpItemFlag, g_TeamDirUpFlag, g_TeamSlowPinFlag;
    Texture2D mKickerDirbar1, mKickerDirbar2, mEffect, mEffect2, mEffectPotion, mPrftTxr, mGoodTxr, mMissTxr, mRndBoxpng, 
        mSwitchPin, KpDirTextInPin01, KpDirTextInPin02, KpDirTextInPin03, KpDirTex, KpDirTextInPin04, KpDirTextInPin05, mPinTex2, mPinTex3;
    public GameObject mPlayerKicker, mPlayerKeeper, mBall, mKickBall, mBippos, mBippos2, kickerDirLD, kickerDirLU, kickerDirRD, kickerDirUP, mkeeperPos, mCerCamAxis, mKickerPos, DefnCam, mDirUpeff;
    ///
    /// Tutorial GameObj
    GameObject mKeeperInfobar3, mTouchEff, mTouchBarEff, mSklNameEff, mIngameObj,
        mObjPopupLabel, mPinObj, mTitleEff, mResultBox, mEnemySkl, mEnemyDir, mMyDir, mMySkl, mMyResult, mResultPanel, mNoticePop,
        mPoint, mTutoGirl, mPoint2, mDirUpitem, mSklUPitem, mSlowPinitem, mEnemyPlayerInforLabel,
        mkeeperDragBar, mTextParticle, mRefLabel, mVsBarObj, mCaption2, mTouchItem, mSkleff, mTutoLabel3, mStatusbar, mLastTutorpanel, mPanelItem;
    /// Stadium GameObj
     
    GameObject mGoalFence1, mGoalFence2, mAdv3, mCrowd, mKpTrailR, mKpTrailL, mTrailBall, mEventBallEffect, mMiniItem, mPlayerPicinfo, 
        mPlayerBioinfo, mGameBareffect, mSkillUpeff, mMissNum2Obj, mPerfecNumObj, mGoldenballeffUp, 
        mParTicle, mDirBareff, mGodenBallCoin, mEventCoin, mGoalKpEff, mGoalKkEff, mKkSpotLight, mKpSpotLight, 
        mDragExitBtn, mKickerDirbar, mKeeperLabel, mDirUpeff2, mSkillUpeff2, mDirLight, mDirUpclone1, mDirUpclone2, 
        mDirUpclone3, mDirUpclone4, mDirUpclone5, mDirUpclone6, mDirUpclone7, mTimer, mSklName, mMainBar, btn_exit, btn_rematch;
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Update / OnGUI  <<<<<
    bool mDidDragStarted, mDidBuyPotion, mKeeperSetDir, mstatusBar, mStatusSillBar, mItemflag1, mItemflag3, mStatusSkillSound, mAwayMyself,
        mEventBronze, mEventSilver, mEventGold, mGoldenBall, mSilverBall, mBronzeBall, mGoldenBallEff, mskillflag = true, mGoldenAfter, mSilverAfter, mBronzeAfter,
        mSkillSound, mEventPotion, mEventminusPotion, mEventItemShowTime, mDidEventPotion, mDirMinuspotion, mSlowEff, 
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>> Last Result   <<<<<
        AllPoint1, WinPoint, BonusCoin, ItemBonus = true, WinBonus, mResultShow, mMissPenalty, mEffballflag = true, mDirLightFlag;
    float DrawXStart = 0.384f, DrawXWidth = 0.46167f, DrawYStart = 0.71188f, DrawYWidth = 0.09375f, mSX, mSY, mRotSpeed, sclPotion = 1.0f, 
        mPos = 1000, mWidth = 0, mStartTime, mEventDirspeed, mEventSkillSpeed;
    /*
    string mFoldNameL = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand", 
        mFoldNameR = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand001", 
        mkickerRfoot = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 R Thigh/Bip001 R Calf/Bip001 R Foot";
    */
    string mFoldNameL = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand", 
        mFoldNameR = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand", 
        mkickerRfoot = "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 R Thigh/Bip001 R Calf/Bip001 R Foot";
    int DragNum = 0, mSinglePlayerNum = 0, mPerfectNum, mMissNum, mPreMyWin, mPreEnWin, mAllUsePoint = 0, mAllPoint;
    //public static GameObject mPlayerKicker;
    public int mAnimaRand = 0, mCerKickAni = 0, mDisKeepAni = 0, mDisKickAni = 0, mCerKeepAni = 0, mKPlastCer, mWinpoint, mBonusCoin, mItemBonus = 0, mWinBonus = 0, mPlayerNum2;
    public float mPlayerInfoX, mPlayerInfoY, mPlayerInfowid, MplayerInfoHeight;
    public Camera mCameraDefn, mCameraKick, mIntroCam, CerCam, PlayerCam, mCamera;
    //  Arrays...
    public StateArray mStateArr, mStateCere;
    public List<GameObject> mGuideBar;
    List<GameObject> arrStatusBar = new List<GameObject> ();
    List<GameObject> arrKeeperBarF = new List<GameObject> ();
    List<GameObject> arrKeeperBarB = new List<GameObject> ();
    List<GameObject> arrKeeperBarS = new List<GameObject> ();
    List<GameObject> arrKeeperBarD = new List<GameObject> ();
    List<GameObject> arrKickerDirBar = new List<GameObject> ();
    List<GameObject> arrKickerDirBar2 = new List<GameObject> ();
    List<Texture2D> arrTexBar = new List<Texture2D> ();
    List<GameObject> ListGameObject = new List<GameObject> ();
    List<GameObject> mKeeperUIBar, mMyPointBall, mEnemyPointBall;
    List<bool> arrMyScore, arrEnScore;
    public Dictionary<int,float> dicGuideObjectPos, dicGuideObjectWidth;
    Dictionary <int, Texture2D> mKpPinTex;
    public Dictionary <string, GameObject> dicGameSceneMenuList;
    ArrayList arrListTxt;
    // State Related..
    // Character Position
    Vector3 mKeeperPosi;
    Vector2 mVecInit, mVecFin;
    // Am Objects
    //public AmPlayer mCurPlayer, mCurEnem;
    private AmStage mStage;
    AmTexture mShirt, mPants, mSocks, mGkShirt, mGkPants, mGkSocks;
    public GameScene_MidCer AmAni;
    AnimationClip Clip, Clip2, Clip3;
    GameObject mMyDirText, mMySklText, mEnemDirText, mEnemSklText;

    void GameInit ()
    {
        //Ag.mgIsKick = false; //Start Setting Kicker
        myCard = new AmCard ();
        EnemCard = new AmCard ();


        ReadmyTexture ();

        //---------------------------------------------------------------------------- ListPoint
        dicGameSceneMenuList = new Dictionary<string, GameObject> ();
        mKpPinTex = new Dictionary<int, Texture2D> ();
        mRscrcMan = new HtRsrcMan ("");
        mMyPointBall = new List<GameObject> ();
        mEnemyPointBall = new List<GameObject> ();
        //mKeeperUIBar = new List<GameObject> ();
        dicGameSceneMenuList.Add ("TargetObj", FindGameObject ("MainControllView", true));
        //FindGameObject ("MainSound",true).audio.Stop ();

        //---------------------------------------------------------------------------- Bot
        //Ag.mSingleMode = true;
        //---------------------------------------------------------------------------- 

        mPlayerInfowid = 0f;
        MplayerInfoHeight = 0.23f;

        //---------------------------------------------------------------------------- Get Coin
        //Ag.mIAP.mFileIO.mReceipt = "this Receipt";


        mGoldenBall = mSilverBall = mBronzeBall = Ag.mBallEventAlready = false;
        //---------------------------------------------------------------------------- 

        Ag.mgEnemGiveup = false;

        mTimer = FindGameObject ("Ui_camera/Camera/Ui_cont/Panel_timer", false).gameObject;
        //---------------------------------------------------------------------------- Init Start

        //---------------------------------------------------------------------------- Menu Tutorial
        dicGameSceneMenuList.Add ("MainBar", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar", false));
        dicGameSceneMenuList.Add ("MainSkillBar", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_skillbar", false));
        dicGameSceneMenuList.Add ("SGrade_MainSkillBar", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_skillbar1", false));

        dicGameSceneMenuList.Add ("MyPointLabel", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/Label_mygoal", true));
        dicGameSceneMenuList.Add ("EnemyPointLabel", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/Label_someonegoal", true));

        dicGameSceneMenuList.Add ("Mynick", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/myinfo/Label_name", true));
        dicGameSceneMenuList.Add ("Enemnick", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/someoneinfo/Label_name", true));
        dicGameSceneMenuList.Add ("MyScore", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/myinfo/Label_comboscore", true));
        dicGameSceneMenuList.Add ("EnemScore", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/someoneinfo/Label_comboscore", true));
        // Keeper Info
        dicGameSceneMenuList.Add ("Keeperinfo", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_versusinfo/keeperinfo", false));

        dicGameSceneMenuList.Add ("direction_left", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "direction_left", true));
        dicGameSceneMenuList.Add ("direction_right", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "direction_right", true));
        dicGameSceneMenuList.Add ("Keeper_Label_backnumber", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "Label_backnumber", true));
        dicGameSceneMenuList.Add ("Keeper_Label_playername", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "Label_playername", true));
        dicGameSceneMenuList.Add ("Keeper_Label_enchant1", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "enchant1", false));
        dicGameSceneMenuList.Add ("Keeper_Label_enchant2", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "enchant2", false));

        dicGameSceneMenuList.Add ("Keeper_Amateur", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "gradebundle/Amateur", true));
        dicGameSceneMenuList.Add ("Keeper_Legend", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "gradebundle/Legend", true));
        dicGameSceneMenuList.Add ("Keeper_professional", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "gradebundle/professional", true));
        dicGameSceneMenuList.Add ("Keeper_Semipro", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "gradebundle/Semipro", true));
        dicGameSceneMenuList.Add ("Keeper_Student", FindMyChild (dicGameSceneMenuList ["Keeperinfo"], "gradebundle/Student", true));

        // Kicker Info
        dicGameSceneMenuList.Add ("Kickerinfo", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_versusinfo/kickerinfo", false));

        dicGameSceneMenuList.Add ("Kicker_Label_backnumber", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "Label_backnumber", true));
        dicGameSceneMenuList.Add ("Kicker_Label_playername", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "Label_playername", true));
        dicGameSceneMenuList.Add ("Kicker_Label_enchant1", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "enchant1", false));
        dicGameSceneMenuList.Add ("Kicker_Label_enchant2", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "enchant2", false));

        dicGameSceneMenuList.Add ("Kicker_Amateur", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "gradebundle/Amateur", true));
        dicGameSceneMenuList.Add ("Kicker_Legend", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "gradebundle/Legend", true));
        dicGameSceneMenuList.Add ("Kicker_professional", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "gradebundle/professional", true));
        dicGameSceneMenuList.Add ("Kicker_Semipro", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "gradebundle/Semipro", true));
        dicGameSceneMenuList.Add ("Kicker_Student", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "gradebundle/Student", true));

        for (int i = 1; i < 6; i++) {
            mMyPointBall.Add (FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/mygoal_check/img0" + i + "/background", false));
            mEnemyPointBall.Add (FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/someonegoal_check/img0" + i + "/background", false));
        }


        dicGameSceneMenuList.Add ("Panel_top", FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top", false));
        //dicGameSceneMenuList.Add ("btn_drink_blue",FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_top/Label_someonegoal", true));
        mIngameObj = FindGameObject ("Ui_camera/Camera/Ui_ingame", true);
        dicGameSceneMenuList.Add ("Ui_cont", FindGameObject ("Ui_camera/Camera/Ui_cont", true));

        dicGameSceneMenuList.Add ("Ui_focus", FindGameObject ("Ui_camera/Camera/Ui_focus", false));

        dicGameSceneMenuList.Add ("loading", FindMyChild (mIngameObj, "loading", false));
        dicGameSceneMenuList.Add ("popup", FindMyChild (mIngameObj, "popup", false));
        dicGameSceneMenuList.Add ("rematch", FindMyChild (mIngameObj, "popup/rematch", false));
        dicGameSceneMenuList.Add ("rematch_accept", FindMyChild (mIngameObj, "popup/rematch_accept", false));
        dicGameSceneMenuList.Add ("rematch_gameout", FindMyChild (mIngameObj, "popup/rematch_gameout", false));
        dicGameSceneMenuList.Add ("rematch_not", FindMyChild (mIngameObj, "popup/rematch_not", false));

        dicGameSceneMenuList.Add ("rematch_refuse", FindMyChild (mIngameObj, "popup/rematch_refuse", false));
        dicGameSceneMenuList.Add ("alert_someoneout", FindMyChild (mIngameObj, "popup/alert_someoneout", false));
        dicGameSceneMenuList.Add ("alert_out", FindMyChild (mIngameObj, "popup/alert_out", false));
        dicGameSceneMenuList.Add ("alert_networkerror", FindMyChild (mIngameObj, "popup/alert_networkerror", false));
        dicGameSceneMenuList.Add ("alert_doublelogin", FindMyChild (mIngameObj, "popup/alert_doublelogin", false));
        dicGameSceneMenuList.Add ("havenothing", FindMyChild (mIngameObj, "popup/havenothing", false));

        dicGameSceneMenuList.Add ("division_notice1", FindMyChild (mIngameObj, "popup/division_notice1", false));
        dicGameSceneMenuList.Add ("division_notice2", FindMyChild (mIngameObj, "popup/division_notice2", false));

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["rematch_not"], "btn_close", true), dicGameSceneMenuList ["TargetObj"], "Rematch_NotResponse");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["rematch_not"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "Rematch_NotResponse");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["alert_doublelogin"], "btn_close", true), dicGameSceneMenuList ["TargetObj"], "AlertDoubleLoginRestart");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["alert_doublelogin"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "AlertDoubleLoginRestart");


        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["alert_networkerror"], "btn_close", true), dicGameSceneMenuList ["TargetObj"], "NetworkErrorPopupClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["alert_networkerror"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "NetworkErrorPopupClose");


        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["division_notice1"], "btn_close", true), dicGameSceneMenuList ["TargetObj"], "Notice1PopupClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["division_notice1"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "Notice1PopupOk");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["division_notice2"], "btn_close", true), dicGameSceneMenuList ["TargetObj"], "Notice2PopupClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["division_notice2"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "Notice2PopupOk");



        dicGameSceneMenuList.Add ("Ui_wineff", FindGameObject ("Ui_camera/Camera/Ui_wineff", false));
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "alert_someoneout/btn_ok", true), dicGameSceneMenuList ["TargetObj"], "EnemyLeftOk");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "alert_someoneout/btn_close", true), dicGameSceneMenuList ["TargetObj"], "EnemyLeftOk");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "alert_out/btn_ok", true), dicGameSceneMenuList ["TargetObj"], "AlertOut");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "alert_out/btn_close", true), dicGameSceneMenuList ["TargetObj"], "AlertOut");
        //mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "rematch/btn_cancle", true), dicGameSceneMenuList ["TargetObj"], "RematchCancel");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "rematch_accept/btngrid/btn_refuse", true), dicGameSceneMenuList ["TargetObj"], "rematch_refuse");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "rematch_accept/btngrid/btn_rematch", true), dicGameSceneMenuList ["TargetObj"], "RematchAceept");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["popup"], "rematch_refuse/btn_ok", true), dicGameSceneMenuList ["TargetObj"], "rematch_refuse_Ok");


        //dicGameSceneMenuList.Add ("btn_left_already", FindMyChild (dicGameSceneMenuList ["popup"], "btn_left_already", false));


        dicGameSceneMenuList.Add ("Panel_keeperarrow_Main", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_blue", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/blue", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_blue_eff", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/blue/blue_eff", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_marine", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/marine", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_marine_eff", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/marine/marine_eff", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_red", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/red", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_red_eff", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/red/red_eff", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_yellow", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/yellow", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_yellow_eff", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow/yellow/yellow_eff", false));

        dicGameSceneMenuList.Add ("Panel_keeperarrow_Main2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow2", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_blue2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow2/blue", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_marine2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow2/marine", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_red2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow2/red", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_yellow2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow2/yellow", false));

        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["Panel_keeperarrow_blue2"], dicGameSceneMenuList ["TargetObj"], "SelectRightUp");
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["Panel_keeperarrow_marine2"], dicGameSceneMenuList ["TargetObj"], "SelectRightDown");
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["Panel_keeperarrow_red2"], dicGameSceneMenuList ["TargetObj"], "SelectLeftUp");
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["Panel_keeperarrow_yellow2"], dicGameSceneMenuList ["TargetObj"], "SelectLeftDown");

        dicGameSceneMenuList.Add ("Panel_keeperarrow_set", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set_B", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set/blue", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set_M", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set/marine", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set_R", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set/red", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set_Y", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set/yellow", false));

        dicGameSceneMenuList.Add ("Panel_keeperarrow_set2", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set2", true));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set2_B", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set2/blue", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set2_M", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set2/marine", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set2_R", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set2/red", false));
        dicGameSceneMenuList.Add ("Panel_keeperarrow_set2_Y", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_keeperarrow_set2/yellow", false));

        dicGameSceneMenuList.Add ("Panel_item", FindMyChild (mIngameObj, "Panel_item", true));
        dicGameSceneMenuList.Add ("btn_drink_blue", FindMyChild (mIngameObj, "Panel_item/btn_drink_blue", true));
        dicGameSceneMenuList.Add ("btn_drink_green", FindMyChild (mIngameObj, "Panel_item/btn_drink_green", true));
        dicGameSceneMenuList.Add ("btn_drink_red", FindMyChild (mIngameObj, "Panel_item/btn_drink_red", true));



        dicGameSceneMenuList.Add ("Eff_Fail", FindMyChild (mIngameObj, "Panel_winningeffect/Fail", false));
        dicGameSceneMenuList.Add ("Eff_Gameresult", FindMyChild (mIngameObj, "Panel_winningeffect/Gameresult", false));
        dicGameSceneMenuList.Add ("Eff_Success", FindMyChild (mIngameObj, "Panel_winningeffect/Success", false));

        dicGameSceneMenuList.Add ("Eff_Fail_Save", FindMyChild (mIngameObj, "Panel_winningeffect/Fail/Save_0", false));
        dicGameSceneMenuList.Add ("Eff_Fail_Attack", FindMyChild (mIngameObj, "Panel_winningeffect/Fail/Attack_0", false));
        dicGameSceneMenuList.Add ("Eff_LoseEff", FindMyChild (mIngameObj, "Panel_winningeffect/Gameresult/loseeff", false));
        dicGameSceneMenuList.Add ("Eff_WinEff", FindMyChild (mIngameObj, "Panel_winningeffect/Gameresult/wineff", false));
        dicGameSceneMenuList.Add ("Eff_Success_Attack", FindMyChild (mIngameObj, "Panel_winningeffect/Success/Attack_1", false));
        dicGameSceneMenuList.Add ("Eff_Success_Save", FindMyChild (mIngameObj, "Panel_winningeffect/Success/Save_1", false));


        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["btn_drink_blue"], dicGameSceneMenuList ["TargetObj"], "BlueDrink");
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["btn_drink_green"], dicGameSceneMenuList ["TargetObj"], "GreenDrink");
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList ["btn_drink_red"], dicGameSceneMenuList ["TargetObj"], "RedDrink");


        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false), dicGameSceneMenuList ["TargetObj"], "Btn_ScouterOn");
        dicGameSceneMenuList.Add ("Kickerinfo_progress_scouter", FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false));
        dicGameSceneMenuList.Add ("Kickerinfo_scouter_discript", FindMyChild (dicGameSceneMenuList ["Panel_item"], "scouter_discript", false));

        dicGameSceneMenuList.Add ("Kickerinfo_scouter_bundle", FindMyChild (dicGameSceneMenuList ["Kickerinfo"], "scouter_bundle", false));


        dicGameSceneMenuList.Add ("BackGridAnim", FindMyChild (mIngameObj, "Panel_item/backgrid_amin", true));
        dicGameSceneMenuList.Add ("BackGridAnim2", FindMyChild (mIngameObj, "Panel_item/backgrid_amin2", true));

        dicGameSceneMenuList.Add ("Anim_back_red", FindMyChild (mIngameObj, "Panel_item/backgrid_amin/back02_red", false));
        dicGameSceneMenuList.Add ("Anim_back_blue", FindMyChild (mIngameObj, "Panel_item/backgrid_amin/back01_blue", false));
        dicGameSceneMenuList.Add ("Anim_back_green", FindMyChild (mIngameObj, "Panel_item/backgrid_amin/back03_green", false));
        dicGameSceneMenuList.Add ("Anim_eff01_red", FindMyChild (mIngameObj, "Panel_item/backgrid_amin2/eff02_red", false));
        dicGameSceneMenuList.Add ("Anim_eff02_blue", FindMyChild (mIngameObj, "Panel_item/backgrid_amin2/eff01_blue", false));
        dicGameSceneMenuList.Add ("Anim_eff03_green", FindMyChild (mIngameObj, "Panel_item/backgrid_amin2/eff03_green", false));

        dicGameSceneMenuList.Add ("eff0_good", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff0_good", true));
        dicGameSceneMenuList.Add ("eff1_great", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff1_great", true));
        //dicGameSceneMenuList.Add ("eff2_volcano", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff2_volcano", true));
        //dicGameSceneMenuList.Add ("eff3_flash", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff3_flash", true));
        //dicGameSceneMenuList.Add ("eff4_lightning", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff4_lightning", true));
        dicGameSceneMenuList.Add ("eff5_miss", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff5_miss", true));
        dicGameSceneMenuList.Add ("eff2_perfect", FindMyChild (dicGameSceneMenuList ["Ui_cont"], "Panel_kickname/eff2_perfect", true));

        dicGameSceneMenuList ["btn_drink_blue"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("BlueDrink").ToString ();
        dicGameSceneMenuList ["btn_drink_green"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("GreenDrink").ToString ();
        dicGameSceneMenuList ["btn_drink_red"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("RedDrink").ToString ();

        dicGameSceneMenuList.Add ("IngameUserDiv", FindMyChild (mIngameObj, "Panel_top/myinfo/division/div5", true));
        dicGameSceneMenuList.Add ("IngameEnemDiv", FindMyChild (mIngameObj, "Panel_top/someoneinfo/division/div5", true));


        //mTeamDirUP = FindGameObject ("StatusBar/DirUpBar", false);
        //mTeamSklUP = FindGameObject ("StatusBar/EnergyDrink", false);
        //mTeamSlowPin = FindGameObject ("StatusBar/SlowPin", false);
        //mStatusbar = FindGameObject ("StatusBar", true);
        //mTutoLabel3 = FindGameObject ("UI Root/Caption/TutorialLabel3", false);
        //mRefLabel = FindGameObject ("UI Root/RefLabel", false);
        //mTextParticle = FindGameObject ("UI Root/textparticle_good", false);
        dicGameSceneMenuList.Add ("Ui_tutorial", FindGameObject ("Ui_camera/Camera/Ui_tutorial", false));
        dicGameSceneMenuList.Add ("Panel_explain", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_explain", false));
        //dicGameSceneMenuList.Add ("mTutoCaption", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_explain/explain0/Label_explain", true));
        //dicGameSceneMenuList.Add("mTitleEff",FindMyChild(dicGameSceneMenuList["Ui_tutorial"],"Panel_txt/Label_kick_power", false));

        dicGameSceneMenuList.Add ("Panel_attackresult", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_attackresult", false));
        dicGameSceneMenuList.Add ("Panel_explain_bottom", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_explain_bottom", false));
        dicGameSceneMenuList.Add ("Panel_order", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_order", false));
        dicGameSceneMenuList.Add ("Panel_reward", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_reward", false));
        dicGameSceneMenuList.Add ("Panel_finger", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger", false));

        dicGameSceneMenuList.Add ("Panel_finger1", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger1", false));


        dicGameSceneMenuList.Add ("Panel_finger2", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger2", true));
        dicGameSceneMenuList.Add ("Drag_finger1", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger2/finger1", false));
        dicGameSceneMenuList.Add ("Drag_finger2", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger2/finger2", false));
        dicGameSceneMenuList.Add ("shadow_bundle1", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger2/shadow_bundle1", false));
        dicGameSceneMenuList.Add ("shadow_bundle2", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_finger2/shadow_bundle2", false));

        dicGameSceneMenuList.Add ("Panel_greetings", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_greetings", false));
        dicGameSceneMenuList.Add ("Panel_greetings_username", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_greetings/Label_username", true));
        dicGameSceneMenuList.Add ("Panel_greetings_teamname", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_greetings/Label_teamname", true));

        dicGameSceneMenuList.Add ("Panel_txt", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt", false));
        dicGameSceneMenuList.Add ("Panel_txt_Label_drink", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt/Label_drink", false));
        dicGameSceneMenuList.Add ("Panel_txt_Label_jump_course", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt/Label_jump_course", false));
        dicGameSceneMenuList.Add ("Panel_txt_Label_jump_speed", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt/Label_jump_speed", false));
        dicGameSceneMenuList.Add ("Panel_txt_Label_kick_course", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt/Label_kick_course", false));
        dicGameSceneMenuList.Add ("Panel_txt_Label_kick_power", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_txt/Label_kick_power", false));

        dicGameSceneMenuList.Add ("tutor_keeperinfo", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/keeperinfo", false));
        dicGameSceneMenuList.Add ("tutor_Kickerinfo", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/kickerinfo1", false));
        dicGameSceneMenuList.Add ("tutor_Kickerinfo_eff", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/kickerinfo1/back_effect_info1", false));
        dicGameSceneMenuList.Add ("tutor_Kickerinfo2", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/kickerinfo2", false));
        dicGameSceneMenuList.Add ("tutor_Kickerinfo2_eff", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/kickerinfo2/back_effect_info2", false));
        dicGameSceneMenuList.Add ("tutor_infoeffect", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_versusinfo/infoeffect", false));

        dicGameSceneMenuList.Add ("PlayMode", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "PlayMode", false));

        dicGameSceneMenuList.Add ("Panel_progressbar_kickbar_user", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_kickbar_user", false));
        dicGameSceneMenuList.Add ("Panel_progressbar_kickbar_user_back_effect_bar1", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_kickbar_user/back_effect/bar1", false));
        dicGameSceneMenuList.Add ("Panel_progressbar_kickbar_user_back_effect_bar2", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_kickbar_user/back_effect/bar2", false));
        dicGameSceneMenuList.Add ("Panel_progressbar_kickbar_user_back_effect_bar3", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_kickbar_user/back_effect/bar3", false));
        dicGameSceneMenuList.Add ("Panel_progressbar_kickbar_user_back_effect_bar4", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_kickbar_user/back_effect/bar4", false));
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "PlayMode/btn_ok", true), dicGameSceneMenuList ["TargetObj"], "PlayDirect");

        dicGameSceneMenuList.Add ("Panel_progressbar_skillbar_user", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_progressbar_skillbar_user", false));
        dicGameSceneMenuList.Add ("Panel_keepereff", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_keepereff", false));

        dicGameSceneMenuList.Add ("blaze_end", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_attackresult/blaze_end", false));
        dicGameSceneMenuList.Add ("keeper_end", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_attackresult/keeper_end", false));
        dicGameSceneMenuList.Add ("kick_end", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_attackresult/kick_end", false));
        dicGameSceneMenuList.Add ("lightning_end", FindMyChild (dicGameSceneMenuList ["Ui_tutorial"], "Panel_attackresult/lightning_end", false));

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicGameSceneMenuList ["Panel_reward"], "btn_ok", true), dicGameSceneMenuList ["TargetObj"], "GotoMainMenu");

        dicGameSceneMenuList.Add ("Panel_provokebox", FindMyChild (mIngameObj, "Panel_provokebox", false));
        dicGameSceneMenuList.Add ("provokebox_me", FindMyChild (mIngameObj, "Panel_provokebox/provokebox_me", false));
        dicGameSceneMenuList.Add ("provokebox_you", FindMyChild (mIngameObj, "Panel_provokebox/provokebox_you", true));


        dicGameSceneMenuList.Add ("Panel_progressbar_fx", FindMyChild (mIngameObj, "Panel_progressbar_fx", true));
        dicGameSceneMenuList.Add ("great", FindMyChild (mIngameObj, "Panel_progressbar_fx/great", false));
        dicGameSceneMenuList.Add ("perfect", FindMyChild (mIngameObj, "Panel_progressbar_fx/perfect", false));

        dicGameSceneMenuList.Add ("Panel_progressbar_effect", FindMyChild (mIngameObj, "Panel_progressbar_effect", true));
        dicGameSceneMenuList.Add ("backeffect_green", FindMyChild (mIngameObj, "Panel_progressbar_effect/backeffect_green", false));
        dicGameSceneMenuList.Add ("backeffect_red", FindMyChild (mIngameObj, "Panel_progressbar_effect/backeffect_red", false));
        dicGameSceneMenuList.Add ("backeffect_blue", FindMyChild (mIngameObj, "Panel_progressbar_effect/backeffect_blue", false));


        //mCamera = FindGameObject ("UI Root/Camera", true).camera;
        mCamera = FindGameObject ("Ui_camera/Camera", true).camera;
        mIntroCam = FindGameObject ("CamKickIntro", true).camera;
        mCameraDefn = FindGameObject ("MainControl/CameraDefn", true).camera;
        mCameraKick = FindGameObject ("MainControl/CameraMain", true).camera;
        mCameraDefn.enabled = false;
        mCameraKick.enabled = false;
        mCrowd = FindGameObject ("Stadium2/crowdf03", true);
        //mGameBareffect = FindGameObject ("BarEffect", true);
        //mKeeperLabel = FindGameObject ("labelAni/fonttex", false);
        mResultPanel = FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_gameresult", false);
        //mNoticePop = FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_gameresult/Panel_btn", false);


        dicGameSceneMenuList.Add ("EnemyFace", FindMyChild (mResultPanel, "result_someone/face", true));

        //dicGameSceneMenuList.Add ("EnemyLose", FindMyChild (mResultPanel, "result_someone/Label_lose", false));
        //dicGameSceneMenuList.Add ("EnemytotalScore", FindMyChild (mResultPanel, "result_someone/Label_totalscore", true));
        //dicGameSceneMenuList.Add ("Enemytotalscore_minus", FindMyChild (mResultPanel, "result_someone/Label_totalscore_minus", true));
        dicGameSceneMenuList.Add ("EnemyUserNick", FindMyChild (mResultPanel, "result_someone/Label_usernickname", true));
        dicGameSceneMenuList.Add ("EnemyUsername", FindMyChild (mResultPanel, "result_someone/Label_userteamname", true));

        dicGameSceneMenuList.Add ("EnemtotalScore_minus", FindMyChild (mResultPanel, "result_someone/totalscore_eff/Label_totalscore_minus", false));
        dicGameSceneMenuList.Add ("EnemtotalScore_plus", FindMyChild (mResultPanel, "result_someone/totalscore_eff/Label_totalscore_plus", false));
        dicGameSceneMenuList.Add ("EnemtotalScore", FindMyChild (mResultPanel, "result_someone/Label_totalscore", true));







        dicGameSceneMenuList.Add ("MyFace", FindMyChild (mResultPanel, "result_user/face", true));
        dicGameSceneMenuList.Add ("MyFlag", FindMyChild (mResultPanel, "result_user/flag", true));
        dicGameSceneMenuList.Add ("EnemyFlag", FindMyChild (mResultPanel, "result_someone/flag", true));
        //dicGameSceneMenuList.Add ("MyGameScore", FindMyChild (mResultPanel, "result_user/Label_gamescore", true));
        //dicGameSceneMenuList.Add ("MyBonusScore", FindMyChild (mResultPanel, "result_user/Label_bonusscore", true));
        dicGameSceneMenuList.Add ("MytotalScore", FindMyChild (mResultPanel, "result_user/Label_totalscore", true));

        //dicGameSceneMenuList.Add ("Label_roundbonus", FindMyChild (mResultPanel, "result_user/Label_roundbonus", true));
        dicGameSceneMenuList.Add ("Label_round140", FindMyChild (mResultPanel, "result_user/Label_round140", true));
        dicGameSceneMenuList.Add ("Label_earnscore", FindMyChild (mResultPanel, "result_user/Label_earnscore", true));
        dicGameSceneMenuList.Add ("GetLabel", FindMyChild (mResultPanel, "result_user/GetLabel", true));

        dicGameSceneMenuList.Add ("Label1_gamescore", FindMyChild (mResultPanel, "result_user/grid_data/Label1_gamescore", true));
        dicGameSceneMenuList.Add ("Label2_uniformbonus", FindMyChild (mResultPanel, "result_user/grid_data/Label2_uniformbonus", true));
        dicGameSceneMenuList.Add ("Label1_costumebonus", FindMyChild (mResultPanel, "result_user/grid_data/Label3_costumebonus", true));
        dicGameSceneMenuList.Add ("Label2_ceremonybonus", FindMyChild (mResultPanel, "result_user/grid2_data/Label2_ceremonybonus", true));
        dicGameSceneMenuList.Add ("Label_leaguebonus", FindMyChild (mResultPanel, "result_user/grid2_data/Label3_leaguebonus", true));

        dicGameSceneMenuList.Add ("MyEarnGold", FindMyChild (mResultPanel, "result_user/Label_earngold", true));
        dicGameSceneMenuList.Add ("MyNickname", FindMyChild (mResultPanel, "result_user/Label_usernickname", true));
        dicGameSceneMenuList.Add ("MyUsername", FindMyChild (mResultPanel, "result_user/Label_userteamname", true));
        dicGameSceneMenuList.Add ("MYWin", FindMyChild (mResultPanel, "result_user/userresult_win", false));
        dicGameSceneMenuList.Add ("MyLose", FindMyChild (mResultPanel, "result_user/userresult_lose", false));

        dicGameSceneMenuList.Add ("MytotalScore_minus", FindMyChild (mResultPanel, "result_user/totalscore_eff/Label_totalscore_minus", false));
        dicGameSceneMenuList.Add ("MytotalScore_plus", FindMyChild (mResultPanel, "result_user/totalscore_eff/Label_totalscore_plus", false));

        dicGameSceneMenuList.Add ("GoalNet_2", FindGameObject ("Stadium2/goalpost/goalnet/goalNet", true));
        dicGameSceneMenuList.Add ("GoalNet", FindGameObject ("Stadium2/goalpost/goalnet", true));
        dicGameSceneMenuList.Add ("btn_Label", FindMyChild (mResultPanel, "Panel_btn/btn_Label", false));

        mRscrcMan.AddComponentUISendMessage (FindMyChild (mResultPanel, "Panel_btn/btn_exit", true), dicGameSceneMenuList ["TargetObj"], "GotoHome");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (mResultPanel, "Panel_btn/btn_rematch", true), dicGameSceneMenuList ["TargetObj"], "Rematch");

        CerCam = FindGameObject ("CeremonyCamera", true).camera;


        //Recieve RewardItem

        /*
        dicGameSceneMenuList.Add ("ContwinNumReward_btn_receive", FindMyChild (mResultPanel, "result_user/btn_receive", true));
        mRscrcMan.AddComponentUISendMessage (dicGameSceneMenuList["ContwinNumReward_btn_receive"] , dicGameSceneMenuList ["TargetObj"], "Btn_Fun_ContWinRewardReceive");
        dicGameSceneMenuList.Add ("ContwinNumReward_Label_donotreceive", FindMyChild (mResultPanel, "result_user/Label_donotreceive", false));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_vic", FindMyChild (mResultPanel, "result_user/grid_vic", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victory", FindMyChild (mResultPanel, "result_user/grid_victory", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_blue", FindMyChild (mResultPanel, "result_user/grid_victoryicon_blue", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_green", FindMyChild (mResultPanel, "result_user/grid_victoryicon_green", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_red", FindMyChild (mResultPanel, "result_user/grid_victoryicon_red", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_card", FindMyChild (mResultPanel, "result_user/grid_victoryicon_card", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_cash", FindMyChild (mResultPanel, "result_user/grid_victoryicon_cash", true));
        dicGameSceneMenuList.Add ("ContwinNumReward_grid_victoryicon_gold", FindMyChild (mResultPanel, "result_user/grid_victoryicon_gold", true));
		*/

        dicGameSceneMenuList.Add ("victory_gift", FindMyChild (mIngameObj, "victory_gift/victory_gift", true));
        dicGameSceneMenuList.Add ("victory_info", FindMyChild (mIngameObj, "victory_gift/victory_info", true));
        dicGameSceneMenuList.Add ("Main_victory_gift", FindMyChild (mIngameObj, "victory_gift", false));




        //dicGameSceneMenuList.Add ("victory_gift", FindMyChild (mIngameObj ,"popup/victory_gift/victory_gift", true));
        mRscrcMan.AddComponentUISendMessage (FindMyChild (mIngameObj, "victory_gift/btn_close", true), dicGameSceneMenuList ["TargetObj"], "CloseContwinNumReward");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (mIngameObj, "victory_gift/btn_ok", true), dicGameSceneMenuList ["TargetObj"], "CloseContwinNumReward");
        RewardItemInit ();

        //dicGameSceneMenuList [""].SetActive(true);



        dicGameSceneMenuList.Add ("Fx_kick_left_top", FindGameObject ("Fx_kick/left_top", true));
        dicGameSceneMenuList.Add ("Fx_kick_right_top", FindGameObject ("Fx_kick/right_top", true));
        dicGameSceneMenuList.Add ("Fx_kick_left_bottom", FindGameObject ("Fx_kick/left_bottom", true));
        dicGameSceneMenuList.Add ("Fx_kick_right_bottom", FindGameObject ("Fx_kick/right_bottom", true));

        kickerDirLU = FindGameObject ("Fx_kick/left_topeff", true);
        kickerDirUP = FindGameObject ("Fx_kick/right_topeff", true);
        kickerDirLD = FindGameObject ("Fx_kick/left_bottomeff", true);
        kickerDirRD = FindGameObject ("Fx_kick/right_bottomeff", true);
        mCerCamAxis = FindGameObject ("CerAxis", true);
        DefnCam = FindGameObject ("CerAxis/DefCamera", true);
        DefnCam.GetComponent<Camera> ().enabled = false;
        mAdv3 = FindGameObject ("Stadium2/PreFAds", true);
        //mGoalFence1 = FindGameObject ("Stadium2/goal_data/GoalFence", true);
        //mGoalFence2 = FindGameObject ("Stadium2/goal_data/GoalFence2", true);
        //mMissPenaltyObj = FindGameObject ("ResultCamera/MissPenalty2", true);
        mDirLight = FindGameObject ("DirLight", true);
        //m_LodingCircle = FindGameObject ("labelAni", true);
        //mMiniItem = FindGameObject ("ItemSlot", true);
        //mParTicle = FindGameObject ("Particle System", true);
        //---------------------------------------------------------------------------- Sub Menu
        mSklName = FindGameObject ("Ui_camera/Camera/Ui_cont/Panel_kickname", false);

        dicGameSceneMenuList.Add ("Panel_kickname2", FindGameObject ("Ui_camera/Camera/Ui_cont/Panel_kickname2", true));

        dicGameSceneMenuList.Add ("eff0_fire", FindMyChild (dicGameSceneMenuList ["Panel_kickname2"], "eff0_fire", false));
        dicGameSceneMenuList.Add ("eff1_blaze", FindMyChild (dicGameSceneMenuList ["Panel_kickname2"], "eff1_blaze", false));
        dicGameSceneMenuList.Add ("eff2_volcano", FindMyChild (dicGameSceneMenuList ["Panel_kickname2"], "eff2_volcano", false));
        dicGameSceneMenuList.Add ("eff3_flash", FindMyChild (dicGameSceneMenuList ["Panel_kickname2"], "eff3_flash", false));
        dicGameSceneMenuList.Add ("eff4_lightning", FindMyChild (dicGameSceneMenuList ["Panel_kickname2"], "eff4_lightning", false));


        for (int i = 2; i < 17; i++) {
            dicGameSceneMenuList.Add ("result_victories" + i, FindMyChild (mResultPanel, "result_victories/result_victories" + i, false));
        }


        //---------------------------------------------------------------------------- Resource Load
        mDirUpeff = mRscrcMan.GetPrefabIn ("prefab_Polygon/Effect", "DirUp");
        mSkillUpeff = mRscrcMan.GetPrefabIn ("prefab_Polygon/Effect", "SuperB");
        mDirUpeff2 = mRscrcMan.GetPrefabIn ("prefab_Polygon/Effect", "DirUp2");
        mSkillUpeff2 = mRscrcMan.GetPrefabIn ("prefab_Polygon/Effect", "SuperA");

        mGoldenballeffUp = mRscrcMan.GetPrefabIn ("Effect", "Mystic");
        //---------------------------------------------------------------------------- Resource Load
        
        mPinTex = mRscrcMan.GetTextureIn ("Textures/GameUI", "keepersidebutton");
        KpDirTextInPin03 = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_blue2");
        KpDirTextInPin01 = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_red2");
        KpDirTextInPin02 = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_yellow2");
        KpDirTextInPin04 = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_sky2");
        KpDirTextInPin05 = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_center2");
        mBarTex = mRscrcMan.GetTextureIn ("Textures/GameUI", "direction_blue2");
        mPinTex2 = mRscrcMan.GetTextureIn ("Textures/GameUI", "keepersidebutton");
        mPinTex3 = mRscrcMan.GetTextureIn ("Textures/GameUI", "keepersidebutton2");
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarLU_C"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarRU_C"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarLD_C"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarRD_C"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarLU"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarRU"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarLD"));
        arrTexBar.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectBarRD"));
        mKickerDirbar1 = mRscrcMan.GetTextureIn ("Textures/Stadium", "direction-copy1");
        mKickerDirbar2 = mRscrcMan.GetTextureIn ("Textures/Stadium", "direction-copy1");
        mEffect = mRscrcMan.GetTextureIn ("UISource/LogIn", "EFFECT_perfect");
        mEffect2 = mRscrcMan.GetTextureIn ("UISource", "EFFECT_buildup");
        mEffectPotion = mRscrcMan.GetTextureIn ("500Game/UI", "RedBull_Effect");
        mMissTxr = mRscrcMan.GetTextureIn ("Effect", "Text_Miss");
        mGoodTxr = mRscrcMan.GetTextureIn ("Effect", "Text_Good");
        mPrftTxr = mRscrcMan.GetTextureIn ("Effect", "Text_Perfect");

        //---------------------------------------------------------------------------- Array Add
        arrListTxt = new ArrayList ();

        mKpPinTex.Add (1, KpDirTextInPin03);
        mKpPinTex.Add (2, KpDirTextInPin01);
        mKpPinTex.Add (3, KpDirTextInPin04);
        mKpPinTex.Add (4, KpDirTextInPin02);
        mKpPinTex.Add (5, KpDirTextInPin05);

        dicGameSceneMenuList.Add ("Fx_kick", FindGameObject ("Fx_kick", false));
        arrKickerDirBar.Add (kickerDirLU);
        arrKickerDirBar.Add (kickerDirUP);
        arrKickerDirBar.Add (kickerDirLD);
        arrKickerDirBar.Add (kickerDirRD);
        dicGameSceneMenuList.Add ("Fx_kick2", FindGameObject ("Fx_kick2", true));
        arrKickerDirBar2.Add (FindMyChild (dicGameSceneMenuList ["Fx_kick2"], "left_topeff", false));
        arrKickerDirBar2.Add (FindMyChild (dicGameSceneMenuList ["Fx_kick2"], "right_topeff", false));
        arrKickerDirBar2.Add (FindMyChild (dicGameSceneMenuList ["Fx_kick2"], "left_bottomeff", false));
        arrKickerDirBar2.Add (FindMyChild (dicGameSceneMenuList ["Fx_kick2"], "right_bottomeff", false));

        arrKeeperBarF.Add (dicGameSceneMenuList ["Panel_keeperarrow_blue"]);
        arrKeeperBarF.Add (dicGameSceneMenuList ["Panel_keeperarrow_red"]);
        arrKeeperBarF.Add (dicGameSceneMenuList ["Panel_keeperarrow_marine"]);
        arrKeeperBarF.Add (dicGameSceneMenuList ["Panel_keeperarrow_yellow"]);

        arrKeeperBarB.Add (dicGameSceneMenuList ["Panel_keeperarrow_blue2"]);
        arrKeeperBarB.Add (dicGameSceneMenuList ["Panel_keeperarrow_red2"]);
        arrKeeperBarB.Add (dicGameSceneMenuList ["Panel_keeperarrow_marine2"]);
        arrKeeperBarB.Add (dicGameSceneMenuList ["Panel_keeperarrow_yellow2"]);

        arrKeeperBarS.Add (dicGameSceneMenuList ["Panel_keeperarrow_set_B"]);
        arrKeeperBarS.Add (dicGameSceneMenuList ["Panel_keeperarrow_set_R"]);
        arrKeeperBarS.Add (dicGameSceneMenuList ["Panel_keeperarrow_set_M"]);
        arrKeeperBarS.Add (dicGameSceneMenuList ["Panel_keeperarrow_set_Y"]);

        arrKeeperBarD.Add (dicGameSceneMenuList ["Panel_keeperarrow_set2_B"]);
        arrKeeperBarD.Add (dicGameSceneMenuList ["Panel_keeperarrow_set2_R"]);
        arrKeeperBarD.Add (dicGameSceneMenuList ["Panel_keeperarrow_set2_M"]);
        arrKeeperBarD.Add (dicGameSceneMenuList ["Panel_keeperarrow_set2_Y"]);

        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Accurucy_Bar_KEEPER_ballPoint")); //0
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarLU")); 
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarRU"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarLD"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarRD"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarCEN"));

        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource/GamePlayUI", "Accurucy_Bar_Back"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource/GamePlayUI", "Accurucy_Bar_Keeper2"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Keeper_GDBAR"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Super_MiracleBar"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource/GamePlayUI", "Accurucy_Bar_KICKER2"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Kicker_GKBAR"));//10
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Super_MiracleBar"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Accurucy_Bar_KICKER"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "DirectionPoint"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Word_Accuracy"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Word_Direction"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Word_Good"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Word_Miss"));
        arrListTxt.Add (mRscrcMan.GetTextureIn ("UIsource", "Word_Perfect"));//18
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "Pin"));//
        arrListTxt.Add (mRscrcMan.GetTextureIn ("Textures/GameUI", "DirectBarCEN"));

        if (AgStt.mgGameTutorial) {
            dicGameSceneMenuList ["perfect"].transform.FindChild ("eff2").gameObject.transform.localPosition = new Vector3 (-150, -278, -7.413093f);
            dicGameSceneMenuList ["perfect"].transform.FindChild ("eff0").gameObject.transform.localPosition = new Vector3 (-150, -278, 0f);
            dicGameSceneMenuList ["great"].transform.FindChild ("eff2").gameObject.transform.localPosition = new Vector3 (-150, -260, -7.413093f);
        } else {
            dicGameSceneMenuList ["perfect"].transform.FindChild ("eff2").gameObject.transform.localPosition = new Vector3 (-116, -278, -7.413093f);
            dicGameSceneMenuList ["perfect"].transform.FindChild ("eff0").gameObject.transform.localPosition = new Vector3 (-116, -278, 0f);
            dicGameSceneMenuList ["great"].transform.FindChild ("eff2").gameObject.transform.localPosition = new Vector3 (-116, -260, -7.413093f);
        }


        //mPlayerKeeper = (GameObject)Instantiate (Ag.mySelf.GetGlkper (Ag.mySelf.mGoalUNO).mPoly);
        mKickBall = (GameObject)Instantiate (Resources.Load ("prefab_Polygon/Ball/Ball"));
        mKickBall.renderer.enabled = false;
//        GameObject.Find ("Fadeinout").SendMessage ("FadeTest", true, SendMessageOptions.DontRequireReceiver);
        SetKickerDir (false);

        mPerfectNum = mMissNum = mPreMyWin = mPreEnWin = 0;
        Ag.mgStepSend = 1;
    }

    int CombiItemListEa (string id)
    {
        int CombiItemEA = 0;
        //Debug.Log ("Start");
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                CombiItemEA = Ag.mySelf.arrItem [i].WAS.ea;
                //Debug.Log ("ShoesEA");
            }
        }
        return CombiItemEA;
    }
}
