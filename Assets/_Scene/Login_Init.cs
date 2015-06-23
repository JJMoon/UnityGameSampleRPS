//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Login_Register: AmSceneBase
{
    /**
     * 
     * 구단 이름, 개인정보 동의, 타이틀
     */

    public Dictionary<string,GameObject> dicMenuList = new Dictionary<string, GameObject> ();
    bool mMainFlag;
    public GameObject mMakeGudan, mProvisioning, mTitle, mTitleBtn, mCountryList, mGameManager, mPushLabel, mNationFlag, mUICreateTeam, 
        mGudanname, mScrollflag, mScrollFlagLabel, mGudanCheckLabel, mPushLabelLoadName, mLoadingCircle, mKakaoButton, mPopup, mProCheckBox1, mProCheckBox2, mPopup2, NetworkErrorPopup, mGuestButton;
    // Use this for initialization
    void GameResourceInit ()
    {
        mMainFlag = false;
        mRscrcMan = new HtRsrcMan ("");
//        mRscrcMan.FindGameObject ("Ui_cameraLogo",true).GetComponent<Title> ().mAppLogoPanel.SetActive (false);
        //mNationFlag = mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_nations/ContryMenu/CountryFlag", true);
        mGameManager = mRscrcMan.FindGameObject ("GameManager", true);
        //------------------------------------------------------------ 구단패널 버튼에 UISendMessage 컴포추가
        mMakeGudan = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_createteam", false);
        mGudanname = mRscrcMan.FindChild (mMakeGudan, "club/Input_name", true);
        mGudanCheckLabel = mRscrcMan.FindChild (mMakeGudan, "club/Label_alert", true);

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mMakeGudan, "btn_ok", true).gameObject, mGameManager, "GudanNameClose");

        mTitle = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_title", true);


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mMakeGudan, "nations/btn_left", true), mGameManager, "LeftMoveFlag");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mMakeGudan, "nations/btn_right", true), mGameManager, "RightMoveFlag");

        mScrollflag = mRscrcMan.FindChild (mMakeGudan, "nations/scroll_flag", true).gameObject;
        mScrollFlagLabel = mRscrcMan.FindChild (mMakeGudan, "nations/Label_nationcount", true).gameObject;

        mLoadingCircle = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_loading", false);
        mKakaoButton = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_title/btn_kakao", false);
        //mGuestButton = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_title/btn_guest", false);

        mPushLabel = mRscrcMan.FindChild (mTitle, "loding_alert1", false).gameObject;
        mPushLabelLoadName = mRscrcMan.FindChild (mPushLabel, "Label", false);

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mTitle, "btn_kakao", false), mGameManager, "KakaoLogin");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mTitle, "btn_guest", false), mGameManager, "GuestLogin");
        /*
        
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mMakeGudan,"Panel_back/btn_close", true), mGameManager, "GudanNameClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mMakeGudan,"Panel_back/btn_ok", true).gameObject , mGameManager , "GudanNameOK");
        //------------------------------------------------------------- 제 3자 정보동의 패널 버튼 UISendmessage 컴포 추가
        mProvisioning = mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_term", true);
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mProvisioning,"Panel_terms_bg/btn_close", true).gameObject , mGameManager , "ProvisiningClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mProvisioning,"Panel_terms_bg/btn_ok", true).gameObject,  mGameManager , "ProvisiningOk");
        //------------------------------------------------------------- 타이틀로고 패널 버튼에 UISendmessage 컴포 추가

        mTitleBtn = mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_btn", true);
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mTitleBtn,"btn_guset", true).gameObject ,mGameManager, "Title_Guest");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(mTitleBtn,"btn_kakaologin", true).gameObject ,mGameManager, "Title_KakaoLogin" );
        //------------------------------------------------------------- 국가리스트 패널
        mPushLabel = mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_push", false);
        mCountryList = mRscrcMan.FindGameObject ("CountryListItem", false);
        //------------------------------------------------------------- 점검 패널에 UISendmessage 컴포추가
        dicMenuList.Add ("Repair", mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_refair", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicMenuList["Repair"],"Panel_back/btn_out", true).gameObject ,mGameManager, "RepairPanelClose" );
        //------------------------------------------------------------- 웹뷰 패널에 UISendmessage 컴포추가
        dicMenuList.Add ("Webview", mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_webview", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicMenuList["Webview"],"Panel_terms_bg/btn_ok", true).gameObject ,mGameManager, "Webviewpanelclose" );
        dicMenuList.Add ("countryflag", mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_nations/Panel_back/Plane_flag", true));
        dicMenuList.Add ("countryname", mRscrcMan.FindGameObject ("title/Camera/Anchor/Panel_nations/Panel_back/Label_choose_nation", true));
        Debug.Log ("PanelAdd");
        */
        //mRscrcMan.AddComponentUISendMessage (mMakeGudan ,mGameManager, "GudanNameClose" );

        mPopup = mRscrcMan.FindChild (mTitle, "popup", false).gameObject;

        dicMenuList.Add ("terms", mRscrcMan.FindChild (mPopup, "terms", false));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "terms/btngrid/btn_ok", true), mGameManager, "Provisioningoff");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "terms/termgrid/btn_term1", true), mGameManager, "approval1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "terms/termgrid/btn_term2", true), mGameManager, "approval2");
        mProCheckBox1 = mRscrcMan.FindChild (mPopup, "terms/termgridbtn/Checkbox1", true);
        mProCheckBox2 = mRscrcMan.FindChild (mPopup, "terms/termgridbtn/Checkbox2", true);

        mPopup2 = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_popup", false);

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_networkerror/btn_ok", true), mGameManager, "NetworkError");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_networkerror/btn_close", true), mGameManager, "NetworkError");

        dicMenuList.Add ("Urgentalert", mRscrcMan.FindChild (mPopup2, "alert", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert/btn_ok", true), mGameManager, "UrgentNoticeClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert/btn_close", true), mGameManager, "UrgentNoticeClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert/btn_detail", true), mGameManager, "UrgentNoticeOpenUrl");


        dicMenuList.Add ("alert_ServerVersionCheck", mRscrcMan.FindChild (mPopup2, "alert_ServerVersionCheck", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_ServerVersionCheck/btn_ok", true), mGameManager, "alert_ServerVersionCheckClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_ServerVersionCheck/btn_close", true), mGameManager, "alert_ServerVersionCheckClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_ServerVersionCheck/btn_detail", true), mGameManager, "alert_ServerVersionCheck_DownloadUrl");


        dicMenuList.Add ("alert_normal", FindMyChild (mPopup, "alert_normal", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "alert_normal/btn_close", true), mGameManager, "NormalModeLoginCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "alert_normal/btn_no", true), mGameManager, "NormalModeLoginCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "alert_normal/btn_ok", true), mGameManager, "NormalModeLoginOk");

        dicMenuList.Add ("notification_kakaologin", FindMyChild (mPopup, "notification_kakaologin", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "notification_kakaologin/btn_close", true), mGameManager, "KakaoSyncCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "notification_kakaologin/btn_no", true), mGameManager, "KakaoSyncCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup, "notification_kakaologin/btn_ok", true), mGameManager, "KakaoSyncOk");

        dicMenuList.Add ("alert_doublelogin", FindMyChild (mPopup2, "alert_doublelogin", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_doublelogin/btn_close", true), mGameManager, "AlertDoubleLoginRestart");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_doublelogin/btn_ok", true), mGameManager, "AlertDoubleLoginRestart");

        dicMenuList.Add ("alert_withdrawman", FindMyChild (mPopup2, "alert_withdrawman", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_withdrawman/btn_call", true), mGameManager, "CallCenter");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_withdrawman/btn_close", true), mGameManager, "kickandsavedQuit");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_withdrawman/btn_ok", true), mGameManager, "kickandsavedQuit");

        //dicMenuList.Add ("alert_rooting", FindMyChild (mPopup2, "alert_rooting", false));
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_rooting/btn_close", true), mGameManager, "kickandsavedQuit");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (mPopup2, "alert_rooting/btn_ok", true), mGameManager, "kickandsavedQuit");

    }

    /// <summary>
    /// 중복로그인해서 에러났을때 뜨는창 
    /// </summary>

    void kickandsavedQuit () {

        Application.Quit ();

    }

    void CallCenter () {
        Application.OpenURL("http://cafe.naver.com/rpsonline.cafe?iframe_url=/ArticleList.nhn%3Fsearch.clubid=27342122%26search.menuid=14%26search.boardtype=Q");
    }

    void DoubleLoginError ()
    {
        dicMenuList ["alert_doublelogin"].SetActive (true);
        mPopup2.SetActive (true);
    }

    /// <summary>
    /// 중복로그인해서 게임끄기
    /// </summary>
    void AlertDoubleLoginRestart ()
    {
        Application.Quit ();
    }

    void Provisioningoff ()
    {
        if (mProCheckBox1.GetComponent<UICheckbox> ().isChecked && mProCheckBox2.GetComponent<UICheckbox> ().isChecked)
            mPopup.SetActive (false);
    }

    void NetworkError ()
    {
        mPopup2.SetActive (false);
        Ag.PlatformLogout = true;

        StopTimer ();

        Ag.NetExcpt.Recover ();
        Ag.mGameStartAlready = false;
        Application.LoadLevel ("Title");

    }

    void UrgentNoticeClose ()
    {
        mPopup2.SetActive (false);
        mRscrcMan.FindChild (mPopup2, "alert", true).SetActive (false);
        Application.Quit ();
    }

    void UrgentNoticeOpenUrl () {
        Application.OpenURL (Joycity.UrgentNotice.url);
    }

    void approval1 ()
    {
        Application.OpenURL ("http://cafe.naver.com/rpsonline/4");
    }

    void approval2 ()
    {
        Application.OpenURL ("http://cafe.naver.com/rpsonline/5");
    }
}
