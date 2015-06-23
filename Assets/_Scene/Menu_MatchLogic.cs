//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    //bool MatchingFlag;
    bool prvMatchStarted = true;
    bool MyDataLoad;
    AgUIButton BtnKickOff = new AgUIButton ();
    //bool mWaitStart = false;
    bool ExeBl_DidNotMatchStarted {
        get { 
            if (prvMatchStarted) {
                prvMatchStarted = false;
                return true;
            }
            return false;
        }
        set { prvMatchStarted = value; }
    }

    public void Btn_Fun_Refuse_Ok () // 거절 팝업의 OK 버튼
    {
        Ag.LogDouble (" Btn_Fun_Refuse_Ok ()");
        Ag.NodeObj.LeaveMyself ();

        RefuseOKUI ();

        mKicker.SetActive (true);
        mKicker.animation.Play ();
        //mInviteRefuse = false;
    }

    public void Btn_Fun_versus_Cancel () // ''대전 신청을 하시겠습니까' 의 취소 Button
    {
        Ag.LogDouble (" Btn_Fun_versus_Cancel ()");
        dicMenuList ["KickOffpopup"].SetActive (false);
        dicMenuList ["invite_versus"].SetActive (false);
    }

    public void Btn_Fun_VersusWaiting_Cancel () // '대전 신청 응답을 기다리는 중' 의 취소버튼
    {
        Ag.LogDouble (" Btn_Fun_VersusWaiting_Cancel ()");
        dicMenuList ["KickOffpopup"].SetActive (false);
        dicMenuList ["invite_versusing"].SetActive (false);
    }

    void Btn_Fun_MatchRequireclose () // '준비하기' 의 우상 X 버튼
    {
        Ag.LogDouble (" Btn_Fun_MatchRequireclose ()");
        FriendListUpdate = false;
        for (int i = 0; i < arrFriendList.Count; i++) {
            DestroyObject (arrFriendList [i]);
        }
        CereMonyPreviewClose ();
        MatchRequireClose ();
        LobbyPlayAniFlag = true;
        arrState.SetStateWithNameOf ("Init");
        StopAllCoroutines ();
        StartCoroutine (BlankPack ());
        //Ag.NodeObj.UserModify ("ONLINE");
    }

    void Btn_Fun_MatchSetUp () // '킥오프' 주황색 버튼
    {
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready", true), mTargetObj, "Btn_Fun_MatchSetUp");
        Ag.LogStartWithStr (10, "  Menu_MatchLogic ::  Btn_Fun_MatchSetUp   >>>>   Disabled ? " + BtnKickOff.IsDiabled); //LogmWaitStart" + mWaitStart);
        Ag.mSingleMode = false;
        Ag.mVirServer.BotIDSetting ();

        if (BtnKickOff.IsDiabled)
            return;
        BtnKickOff.MakeAbility (false); // deactivate the button ..
        if (MatchingConditionNotMade ()) { // ??? exe order check.. with JKLee
            BtnKickOff.MakeAbility (true);
            return;
        }

        StartCoroutine (WaitAndGoMatchAndExit ());  

        // Background Control
        FriendListUpdate = false;
        Ag.mFriendMode = 0;
        CereMonyPreviewClose ();

        Ag.mVirServer.maiGradeOfBot = Ag.mySelf.WhatKindoBot ();  // 봇 게임 여부 / 종류 세팅
        Ag.LogDouble (" Kind Of Bot >>  " + Ag.mVirServer.maiGradeOfBot + " ,     :: ");

        if (Ag.mVirServer.maiGradeOfBot < 0) {
            Ag.NodeObj.RandomMatching (1);
            StartCoroutine (RandomAppliedAndWaitForEnemy ());
        } else // Bot Case ...
            StartCoroutine (StartWithBot ());

        dicMenuList ["Panel_matching"].SetActive (true);
        dicMenuList ["Start_Panel"].SetActive (true);
        arrState.SetStateWithNameOf ("GoMinJung");
    }

    void Btn_Fun_MatchCancleAndGoOut () // '나가기' 버튼
    {
        Ag.LogDouble (" Btn_Fun_MatchCancleAndGoOut ()");
        Ag.NodeObj.LeaveMyself ();
        Ag.GameStt.GoingOutFromMatching ();

        FriendListUpdate = false;
        mBackDepthFlag = true;
        StopAllCoroutines ();

        // Button Disable and Enable
        BtnKickOff.MakeAbility (true);
        StartCoroutine (DelayActivateKickOffButton ());

        //-------------------------------------------------------게스트 모드일때는 친구리스트 안불러옴
        if (Ag.mGuest) {
        } else {
            if (!FriendListUpdate)
                StartCoroutine (FriendUpdate ());
        }

        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"))
            BgmSound.Instance.Play ();

        dicMenuList ["Panel_top"].SetActive (true);
        //EnemyDataLoad = false;
        DestroyObject (mEnemyPlayer);

        //MatchingFlag = false;
        Ag.NodeObj.UserModify ("ONLINE", statusOnly: true);
        Ag.NodeObj.GameStartMsgSent.Enem = Ag.NodeObj.GameStartMsgSent.Enem = false;
        //mPrepare.SetActive (false);
        MatchCancelUI ();
        dicMenuList ["CenterCircle"].SetActive (false);

        MyDataLoad = false;
        DestroyObject (dicPlayerOrObj ["MyPlayer"]);

    }

    void Btn_Fun_GameReady ()
    {
        dicMenuList ["ready_me"].SetActive (true);
        Ag.NodeObj.StartGameMsg ();
    }

    /// <summary>
    /// Kickoff_ready
    /// </summary>

    void Btn_Fun_MatchRequire () // 준비하기
    {
        Ag.LogIntenseWord ("   Btn_Fun_MatchRequire  >>>>  ...     >>   Flag :: ");

        //MatchingFlag = false;


        SortBtnInitSet ();

        Ag.NodeObj.UserModify ("ONLINE", statusOnly: true);

        string TimeNow = PreviewLabs.PlayerPrefs.GetString ("ReviewStampTime");
        mBackDepthFlag = true;

        if (AgStt.mgGameTutorial) {
            Application.LoadLevel ("GameScene");
            return;
        }
        if (TimeNow == null || TimeNow.Length < 5)
            TimeNow = "1390441587486";
        int ranNum = AgUtil.RandomInclude (1, 5);
        if (Ag.mySelf.WAS.reviewEvent == 0 && Ag.mySelf.myRank.WAS.winNum > 3 && ranNum == 1 && Ag.UnixTimeStampToDateTimeAddmili (double.Parse (TimeNow)).AddDays (1) < System.DateTime.Now && Ag.CurStorePlfm == StorePlfm.GooglePlay) {
            MenuCommonOpen ("popup_review", "Ui_popup", true);
        }

        SendWasCardupdate ();
        mMenuName = "Btn_Fun_MatchRequire";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;
        }
        dicMenuList ["MainCamera"].SetActive (false);
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (false);
        dicMenuList ["Ui_kickoff"].SetActive (true);
        Btn_Fun_DrinkItem ();
        Ag.mySelf.arrUniform [0].SetColorInfoString ();
        Ag.mySelf.arrUniform [1].SetColorInfoString ();

        if (Ag.mGuest) { // 게스트 모드일때는 친구리스트 안불러옴
            dicMenuList ["kakao_sync_kickoff"].SetActive (true);
        } else {
            StartCoroutine (CoruMatchRequire ());
        }
    }
}
