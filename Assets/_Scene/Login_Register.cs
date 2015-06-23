//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Timers;

public partial class Login_Register : AmSceneBase
{
    int mXNum;
    // Use this for initialization
    public override void Start ()
    {


        //CheckWasServerVision ();
        GameResourceInit ();


        if (Ag.PlatformLogout)
            onAuthorized (KakaoNativeExtension.Instance.hasValidTokenCache ());

        if (!StcPlatform.InitCompleted)
            KakaoNativeExtension.Instance.Init (onInitComplete, onTokens);  // PLATFORM_DEPENDENT_SOURCE  Do not erase this comment !!!

        mXNum = 0;
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            BgmSound.Instance.Play ();
        }

//        if (KakaoResponseHandler.Instance.tokens == null)
//            KakaoResponseHandler.Instance.tokens += ((accessToken, refreshToken) => {
//                if (string.IsNullOrEmpty (accessToken)) {
//                    mGuestButton.SetActive (true);
//                    mKakaoButton.SetActive (true);
//                }
//
//                StcPlatform.TheToken = accessToken;
//            });// delegateTokensKakao;

        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TitleIntro_02");

        if (KakaoNativeExtension.Instance.hasValidTokenCache ()) {
            StcPlatform.TheToken = PlayerPrefs.GetString (KakaoStringKeys.Commons.accessTokenKeyForPlayerPrefs, null);
            Ag.LogIntenseWord ( "Access Token :: " + StcPlatform.TheToken);
        } else {
            Ag.LogIntenseWord ("LoginReguster  ::  Token Is null ");
        }
    }



    public Timer sysTimer;


    void StopTimer ()
    {
        Ag.LogIntenseWord (" >>>>>   Stop Timer  @  Login   Wifi <--> LTE   >>>>   End    ");
        if (sysTimer != null) {
            sysTimer.Stop ();
            sysTimer = null;
        }
    }

    void SetTimerLogin ()
    {
        Ag.LogIntenseWord (" Set Timer  @  Login  ");

        if (sysTimer != null) {
            sysTimer.Stop ();
            sysTimer = null;
            //"  Timer  Reset !!! ".HtLog ();
        }

        sysTimer = new Timer ();
        sysTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
            Ag.LogIntenseWord ("   Login   Time out  .... Wifi <--> LTE     Failure       >>>>>    ");

            Ag.NetExcpt.ConnectLossSignalGone = true;  // popup open..

            //   Error Popup  Show ...  " Login Fail ...  go to start ".. 
//            mPopup2.SetActive (true);
//            mRscrcMan.FindChild (mPopup2, "alert_networkerror", true);
//
            sysTimer.Enabled = false;
            sysTimer = null;


        };
        sysTimer.Interval = 40000; // milli second ..
        sysTimer.Enabled = true;
    }

    public bool UrgenNotice ()
    {


//        if (Joycity.UrgentNotice == null) {
//            Joycity.UrgentNotice = new JceTextNotice ();
//            JceTextNotice mJoy = new JceTextNotice ();
//            mJoy.url = "http://www.naver.com";
//
//            Joycity.UrgentNotice = mJoy;
//        }

        if (Joycity.UrgentNotice != null) {
            mPopup2.SetActive (true);
            dicMenuList ["Urgentalert"].SetActive (true);
            dicMenuList ["Urgentalert"].transform.FindChild ("Label_content").GetComponent<UILabel> ().text = Joycity.UrgentNotice.content;
            dicMenuList ["Urgentalert"].transform.FindChild ("Label_title").GetComponent<UILabel> ().text = Joycity.UrgentNotice.title;
            return true;
        } else
            return false;
    }
    // Update is called once per frame
    public override void Update ()
    {


        if (Ag.NetExcpt.ConnectLossSignalGone) {
            mPopup2.SetActive (true);
            mRscrcMan.FindChild (mPopup2, "alert_networkerror", true);
        }
        if (Ag.NetExcpt.WasLoginDuplicate) {
            DoubleLoginError ();
        }
        FindMyChild (mMakeGudan, "club/Input_name/Label", true).transform.localPosition = new Vector3 (0, 0, 0);
    }

    /// <summary>
    /// 한글 입력인지 영문입력인지 체크 하는부분 
    /// </summary>

    bool HangleCheck (string g_strLoginUserID)
    {
        bool bHangul = false;  //한글인가 체크
        char[] cLoginUserID = g_strLoginUserID.ToCharArray (0, g_strLoginUserID.Length);
        foreach (char c1 in cLoginUserID) {
            if (char.GetUnicodeCategory (c1) ==
                System.Globalization.UnicodeCategory.OtherLetter) {  // 한글인지 check
                bHangul = true;
                break;
            } else
                bHangul = false;
        }
        
        if (bHangul) {
            return cLoginUserID.Length <= 36;
        } else {
            return cLoginUserID.Length <= 9;
        }
    }

    public override void OnGUI ()
    {
        int appFriendsCount = KakaoFriends.Instance.appFriends.Count;
        int friendsCount = KakaoFriends.Instance.friends.Count;
        int KakaoGameappFriendsCount = KakaoGameFriends.Instance.leaderboardFriends.Count;
        int KakaoGameFriendCount = KakaoGameFriends.Instance.kakaotalkFriends.Count;
    }

    void LeftMoveFlag ()
    {
        if (mXNum > 0 && mXNum <= 31) {
            mXNum--;     
        }
        FlagMove ();
    }

    void RightMoveFlag ()
    {
        if (mXNum >= 0 && mXNum <= 30) {
            mXNum++;     
        }
        FlagMove ();
    }

    void FlagMove ()
    {
        mScrollflag.GetComponent<SpringPanel> ().target.x = mXNum * -240;
        mScrollflag.GetComponent<SpringPanel> ().enabled = true;
        mScrollFlagLabel.gameObject.GetComponent<UILabel> ().text = "(" + (mXNum + 1).ToString () + "/32)";
        Ag.mySelf.WAS.Country = mXNum;
    }

    /// <summary>
    /// 카카오 모드 로그인
    /// </summary>
    void KakaoLogin ()
    {
        Ag.mGuest = false;
        AgStt.IntendedPause = true;
        KakaoNativeExtension.Instance.Login (onLoginComplete, onLoginError);
        //mGuestButton.SetActive (false);
    }

    /// <summary>
    /// 게스트 모드 로그인
    /// </summary>
    void GuestLogin ()
    {
        mPopup.SetActive (true);
        dicMenuList ["terms"].SetActive (false);
        dicMenuList ["alert_normal"].SetActive (true);

    }

    public void UserInfo ()
    {
        //JCE.JceEventBanner (Ag.mySelf);
        Ag.LogIntenseWord ("Login_Register.cs :: UserInfo >>   KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);

        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 1 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { 
            case 0:
                LocalSettingSave ();
                PriceItem ();
                //Ag.mySelf.CheckFirstDailyEventToday ();
                break;
            case -1:
            case 4:
                return;
            }
            Ag.mySelf.WAS.profileURL = StcPlatform.ProfileURL;
            AgStt.NodeClose ();
            AgStt.NodeOpen ();
        };
    }

    /// <summary>
    /// 카카오 싱크 하기
    /// </summary>
    public void KakaoSyncOk ()
    {
        mPopup.SetActive (false);
        dicMenuList ["notification_kakaologin"].SetActive (false);
        RegistProcess ();
    }

    /// <summary>
    /// 카카오 싱크 취소하기
    /// </summary>
    public void KakaoSyncCancel ()
    {
        mPopup.SetActive (false);
        dicMenuList ["notification_kakaologin"].SetActive (false);
    }

    /// <summary>
    /// 일반 모드 접속하기
    /// </summary>
    public void NormalModeLoginOk ()
    {
        Ag.mGuest = true;
        Ag.mySelf.WAS.KkoID = "";
        OnLoginWas ();
        //mGuestButton.SetActive (false);
        mKakaoButton.SetActive (false);
        mPopup.SetActive (false);
        dicMenuList ["alert_normal"].SetActive (false);
    }

    /// <summary>
    /// 일반모드 취소하기
    /// </summary>
    public void NormalModeLoginCancel ()
    {
        mPopup.SetActive (false);
        dicMenuList ["alert_normal"].SetActive (false);
    }



    public void alert_ServerVersionCheckClose ()
    {
        mPopup2.SetActive (false);
        dicMenuList ["alert_ServerVersionCheck"].SetActive (false);
    }


    public void alert_ServerVersionCheck_DownloadUrl ()
    {

        Application.OpenURL ("https://play.google.com/store/apps/details?id=com.appsgraphy.KVSdev");

    }



    public void RegCardUpdate ()
    {
        Ag.LogIntenseWord (" RegCardUpdate  KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);

        WasCardUpdate aObj = new WasCardUpdate () { User = Ag.mySelf, arrSendCard = Ag.mySelf.GetMainCards ()
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { //
            case 0:
                UserInfo ();
                break;
            case -1:
            case 4:
                return;
            }
        };
    }
    // Kakao Init
    private void onAuthorized (bool _authorized)
    {

        Ag.LogDouble ("   Login_Register >>>   on Authorized    authorized ?? " + _authorized);
        Ag.LogDouble ("  AgStt.SvrVersion >>>   :: " + AgStt.SvrVersion+ "   AgStt.CliVersion " + AgStt.CliVersion);
        // 긴급공지

        #if UNITY_EDITOR
        _authorized = true;
        #endif

        if (AgStt.SvrVersion > AgStt.CliVersion && Ag.CurStorePlfm == StorePlfm.GooglePlay) { // Update case ... Server is faster 
            mPopup2.SetActive (true);
            dicMenuList ["alert_ServerVersionCheck"].SetActive (true);
            return;
        }

        if (UrgenNotice ())
            return;
        if (_authorized) {
            Ag.mGuest = false;
            //           mGuestButton.SetActive (false);
//            KakaoNativeExtension.Instance.ShowAlertMessage ("Move to Main, Because Already finished Login Process!");
            //KakaoNativeExtension.Instance.Friends (onFriendsComplete, onFriendsError);
            Ag.onAuthorizedflag = false;
            KakaoNativeExtension.Instance.LocalUser (onLocalUserComplete, onLocalUserError);
            #if UNITY_EDITOR
            OnLoginWas ();
            #endif
            //OnLoginWas ();
        } else {
            //mPopup.SetActive (true);
            //
            Ag.mGuest = true;
            //mGuestButton.SetActive (true);
            mKakaoButton.SetActive (true);
            mMakeGudan.SetActive (false);
            /*
            if (Ag.PlatformLogout)
                return; 
                */
            GuestLoginWas ();
        }
    }

    public void LocalSettingSave ()
    {
        if (!PreviewLabs.PlayerPrefs.GetBool ("FirstAppInstall")) {
            //PreviewLabs.PlayerPrefs.SetBool ("FirstCharge_check", false);
            PreviewLabs.PlayerPrefs.SetBool ("BgmSoundOff", true);
            PreviewLabs.PlayerPrefs.SetBool ("FXSoundOff", true);
            PreviewLabs.PlayerPrefs.SetBool ("viewUserpic", true);
            PreviewLabs.PlayerPrefs.SetBool ("MessageAlert", true);

            PreviewLabs.PlayerPrefs.SetBool ("MenuTutorLineup", false);
            PreviewLabs.PlayerPrefs.SetBool ("MenuTutorCardMix", false);
            PreviewLabs.PlayerPrefs.SetBool ("MenuTutorPlayerInfo", false);
            PreviewLabs.PlayerPrefs.SetBool ("FirstAppInstall", true);
            for (int i = 0; i < 50; i++) {
                PreviewLabs.PlayerPrefs.SetString ("JoyCityImageBanner" + i, "1390441587486");
            }
            PreviewLabs.PlayerPrefs.SetString ("ReviewStampTime", "1390441587486");
            PreviewLabs.PlayerPrefs.Flush ();
        }
    }




    /// <summary>
    /// 일반로그인시
    /// </summary>
    public void GuestLoginWas ()
    {
        //Ag.mGuest = true;
        WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 
            case 0:
                mPopup.SetActive (false);
                dicMenuList ["terms"].SetActive (false);

                Ag.LogIntenseWord (" result : Success ");
                mMainFlag = true;
                mPushLabelLoadName.SetActive (true);
                mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EA%B2%BD%EA%B8%B0%EC%9E%A5%EC%97%90%20%EC%9E%85%EC%9E%A5%EC%A4%91%EC%9E%85%EB%8B%88%EB%8B%A4.");
                //mGuestButton.SetActive (false);
                mKakaoButton.SetActive (false);
                if (Ag.mGuest) {
                    Ag.mySelf.WAS.KkoNick = "No name";
                    Ag.mySelf.KkoNickEncode = "No name";
                } else {
                    Ag.mySelf.WAS.KkoNick = StcPlatform.PltmNick;
                    Ag.mySelf.KkoNickEncode = WWW.EscapeURL (StcPlatform.PltmNick);
                    Ag.mySelf.TeamNameEncoded = WWW.EscapeURL (Ag.mySelf.WAS.TeamName);
                }

                UserInfo ();
                break;
            case 4:
                mPopup2.SetActive(true);
                KakaoNativeExtension.Instance.Unregister (onUnregisterComplete, onUnregisterError);
                dicMenuList["alert_withdrawman"].SetActive(true);
                break;
            case -1:
            
            case 5:
                Ag.LogString("GuestLoginWas ::" + Ag.PlatformLogout );
                mKakaoButton.SetActive (true);
                //mGuestButton.SetActive (true);
                mMakeGudan.SetActive(false);
                mPopup.SetActive (true);
                dicMenuList ["terms"].SetActive (true);
                return;
            }
        };
    }

    /// <summary>
    /// 카카오 로그인시
    /// </summary>
    public void OnLoginWas ()
    {
        #if UNITY_EDITOR
        //if (!Ag.mGuest)
        GitIgnoreThis.SetKKOID4Test ();
        Ag.LogIntenseWord ("  UNITY_EDITOR_OSX    :: GitIgnoreThis.SetKKOID4Test ()   ");
        #endif

        JCE.JceImageNotice (Ag.mySelf);

        SetTimerLogin ();

        Ag.LogIntenseWord ("  OnLoginWas   KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);
        WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };

        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 
            case 0:
                Ag.LogIntenseWord (" result : Success ");
                mMainFlag = true;
                mPushLabelLoadName.SetActive (true);
                mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EA%B2%BD%EA%B8%B0%EC%9E%A5%EC%97%90%20%EC%9E%85%EC%9E%A5%EC%A4%91%EC%9E%85%EB%8B%88%EB%8B%A4.");

                if (Ag.mGuest) {
                    Ag.mySelf.WAS.KkoNick = "No name";
                    Ag.mySelf.KkoNickEncode = "No name";
                } else {
                    Ag.mySelf.WAS.KkoNick = StcPlatform.PltmNick;
                    Ag.mySelf.KkoNickEncode = WWW.EscapeURL (StcPlatform.PltmNick);
                    Ag.mySelf.TeamNameEncoded = WWW.EscapeURL (Ag.mySelf.WAS.TeamName);
                }
                UserInfo ();
                //mGuestButton.SetActive (false);

                break;
            case -2:
                mPopup.SetActive (true);
                dicMenuList ["notification_kakaologin"].SetActive (true);
                AgStt.FromGuest2Kakao = true;

                //RegistProcess();
                //kakaoLogin
                break;
            case 4:
                mPopup2.SetActive(true);
                KakaoNativeExtension.Instance.Unregister (onUnregisterComplete, onUnregisterError);
                dicMenuList["alert_withdrawman"].SetActive(true);
                break;
            case -1:
            case -5:
            case 5:
                Ag.LogIntenseWord (" Login : go to Regist ...  " + mMakeGudan.ToString ());
                BgmSound.Instance.Play ();
                LocalSettingSave ();
                PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", false);
                PreviewLabs.PlayerPrefs.Flush ();
                mMakeGudan.SetActive (true);  // 
                mMakeGudan.transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = "";
                mPushLabel.SetActive (false);
                StopTimer ();

                return;
            }
        };
    }

    void PriceItem ()
    {

        pushSetting ();
        #if UNITY_IPHONE
				//JCE.JceNotiTokenSetting (Ag.mySelf);
        #endif
        
        WasItemPrice aObj = new WasItemPrice () { User = Ag.mySelf, DiscountOnly = false };
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0: 
                //JCE.JceLogin(Ag.mySelf);
                JCE.JceImageNotice(Ag.mySelf);
                //JCE.JceEventBanner(Ag.mySelf);
                FetchEventList ();
                break;
            }
        };
    }
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    //---------------------------------------------------------------------------------------- kakao
    private void onLoginComplete ()
    {
        //mPushLabel.SetActive (true);
        //KakaoNativeExtension.Instance.ShowAlertMessage ("Login Success!");

        KakaoNativeExtension.Instance.LocalUser (onLocalUserComplete, onLocalUserError);
        mKakaoButton.SetActive (false);
    }

    private void onLoginError (string status, string message)
    {
        //mGuestButton.SetActive (true);
        //showAlertErrorMessage (status, message);
    }

    private void onInitComplete ()
    {
        StcPlatform.InitCompleted = true;
		Ag.PlatformLogout = true;
        StcPlatform.Authorized (onAuthorized); // delegate setting ... 
    }

    private void onTokens (string accessToken, string refreshToken)
    {
        StcPlatform.TheToken = accessToken;
        Ag.LogIntenseWord ("accessToken :: " + accessToken);
        KakaoNativeExtension.Instance.updateTokenCache (accessToken, refreshToken);
//        if (KakaoNativeExtension.Instance.hasValidTokenCache () == true) {
//            KakaoNativeExtension.Instance.ShowAlertMessage ("Has Token Already");
//        } else {
//            KakaoNativeExtension.Instance.ShowAlertMessage ("I have not Tockens!");
//        }
    }

    private void onFriendsComplete ()
    {
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Friend Complete");
    }

    private void onFriendsError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onUnregisterComplete ()
    {

    }

    private void onUnregisterError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    public void pushSetting ()
    {
        #if UNITY_ANDROID
        try {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            jo.Call ("GetRegisterId");
            jo.Call ("PushRegid");
        } catch {
            " OSX Editor    ....    OK  ".HtLog ();
        }
        //Debug.Log("");
        #endif

    }

    private void onLocalUserComplete ()
    {



        /*
         * please below propery after called localUserComplete
         */
        string nickName = StcPlatform.PltmNick;
        string hashedTalkUserId = StcPlatform.HashedUserID;
        string userId = StcPlatform.UserID;
        string profileImageUrl = StcPlatform.ProfileURL;
        string countryIso = StcPlatform.CountryISO;
        bool messageBlocked = StcPlatform.IsMsgBlocked;

        string alertMessage = "";

        if (nickName != null && nickName.Length > 0) {
            alertMessage += "nickName : ";
            alertMessage += nickName;
            alertMessage += "\n";
        }

        if (hashedTalkUserId != null && hashedTalkUserId.Length > 0) {
            alertMessage += "hashedTalkUserId :";
            alertMessage += hashedTalkUserId;
            alertMessage += "\n";
        }

        if (userId != null && userId.Length > 0) {
            alertMessage += "userId :";
            alertMessage += userId;
            alertMessage += "\n";
        }

        if (profileImageUrl != null && profileImageUrl.Length > 0) {
            alertMessage += "profileImageUrl :";
            alertMessage += profileImageUrl;
            alertMessage += "\n";
        }

        if (countryIso != null && countryIso.Length > 0) {
            alertMessage += "countryIso :";
            alertMessage += countryIso;
            alertMessage += "\n";
        }

        alertMessage += (messageBlocked == true ? "true" : "false");

//        KakaoNativeExtension.Instance.ShowAlertMessage (alertMessage);

        mPushLabel.SetActive (true);
        mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%84%A0%EC%88%98%20%EC%A0%95%EB%B3%B4%EB%A5%BC%20%EB%B6%88%EB%9F%AC%20%EC%98%A4%EA%B3%A0%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4.");
        LoadGameInfo ();
        KakaoNativeExtension.Instance.Friends (onFriendsComplete, onFriendsError);

    }

    private void onLocalUserError (string status, string message)
    {
        //Ag.NetExcpt.ConnectLossAct ();
        showAlertErrorMessage (status, message);
    }
}
