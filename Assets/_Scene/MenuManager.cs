//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


// [2012:11:14:MOON] Menu Fast Loading
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System;

public partial class MenuManager : AmSceneBase
{
    public DateTime DTobj;
    public WasCard mwas;
    bool MatchOk, mBackDepthFlag;
    // , mKickoff;
    List<GameObject> arrFriendList = new List<GameObject> ();
    List<GameObject> arrGmMailList = new List<GameObject> ();
    //AmUser usr;
    public AmCard mCard;
    string mFriendId;



    public override void Start () // Imsi
    {   

        //Purchase = PurchaseType.Nstore;

        Ag.LogIntenseWord (" MenuManager    Started ..... ");
        //mGameMatchOk = false;
        Ag.SingleTry = 0;
        Ag.CurrentScene = "MENU";

        //-----------Popup Cash Item
        int ran = AgUtil.RandomInclude (1, 999);
        //if (Ag.mySelf.MinutesAfterRegist > 1440 && ran % 5 < 5) // 100 % @ 411


        Ag.ContGameNum = 0;
        mBackDepthFlag = false;
        MailItemInit ();
        InitInAppPurchaseIOSnADR ();
        mFriendId = "";
        Ag.mySelf.SetCostumeToCard ();
        //FriendRank ();
        mwas = new WasCard ();
        mCard = new AmCard ();
        mRankFriend = new WasRank ();
        BarObj = new List<GameObject> ();
        mTimeLooseAtStartPoint = 0f;
        Ag.LogIntenseWord (" MenuManager :: Start  .. Game Init ()  ");
        LoadMenuResources ();
        SettingLocal ();

        // '킥오프' 주황색 버튼
        BtnKickOff.MakeAbility (true);
//        BtnKickOff.SetObjectsWithAlt ( FindMyChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready2", false), null,
//            FindMyChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready", true));


        if (Ag.mGuest) {
            Ag.mySelf.WAS.profileURL = "";
            Ag.mySelf.WAS.KkoNick = "No name";
            Ag.mySelf.KkoNickEncode = "No name";
        } else {
            Ag.mySelf.WAS.profileURL = StcPlatform.ProfileURL;
            Ag.mySelf.WAS.KkoNick = StcPlatform.PltmNick;
            Ag.mySelf.KkoNickEncode = WWW.EscapeURL (StcPlatform.PltmNick);
            Ag.mySelf.TeamNameEncoded = WWW.EscapeURL (Ag.mySelf.WAS.TeamName);
        }

        if (PreviewLabs.PlayerPrefs.GetBool ("DidTutorial"))
            AgStt.mgGameTutorial = false;
        else
            AgStt.mgGameTutorial = true;

        #if UNITY_EDITOR
        PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", true);
        AgStt.mgGameTutorial = false;
        #endif

        //GitIgnoreThis.GitIgnoreTutorial ();

        addsendmessageTutorPanel ();

        Ag.LogIntenseWord (" MenuManager :: Start  .. for  ");

        for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
            if (i == KakaoFriends.Instance.appFriends.Count - 1) {
                mFriendId += KakaoFriends.Instance.appFriends [i].userid;
            } else {
                mFriendId += KakaoFriends.Instance.appFriends [i].userid + ",";
            }
            Debug.Log (mFriendId);
        }

        Kick_OffInit ();
        PriceSetting ();
        SetNodeDelegate ();



        Ag.LogIntenseWord (" MenuManager :: Start  .. Sale Item  ");

        SaleItemSetting ();
        //pushSetting ();

        Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);

        Ag.LogIntenseWord (" MenuManager :: Start  .. spriteName  " + Ag.mySelf.WAS.League + Ag.mViewCard.LeagueSpriteNameS);
        dicMenuList ["Lobby_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        dicMenuList ["Ui_team_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        dicMenuList ["MY_Label_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;


        //   Debug.Log (GiftRewardCode()+"   gift");

        #if UNITY_ANDROID
        var skus = new string[] {
            "com.prime31.testproduct",
            "android.test.purchased",
            "com.prime31.managedproduct",
            "com.prime31.testsubscription"
        };
        GoogleIAB.queryInventory( skus );
        #endif

        Ag.mySelf.ApplyCurrentDeck ();
        Ag.LogIntenseWord (" MenuManager :: Start  .. End ....   ");
        //ShowDeckEffLabel ();

        InitFriendRank ();

        if (Ag.mGameStartAlready) {
            Btn_Fun_MatchRequire ();
            PopupAfterGameEnd ();
            if (Ag.mBlueItemFlag)
                StartCoroutine (btn_auto_label_Priceoff ());
        } else {
            if (Ag.mySelf.MinutesAfterRegist > 60 && ran % 20 == 3) // 5% ...  // ran % 5 < 5) // 100 % @ 411
                PopupAfterUserCash ();
            BannerEventAction ();
//            if (!PreviewLabs.PlayerPrefs.GetBool ("FirstCharge_check")) {
//                dicMenuList ["Ui_popup"].SetActive (true);
//                dicMenuList ["first_purchase"].SetActive (true);
//            }

        }


    }

    void BannerEventAction ()
    {
        if (Ag.mySelf.ShowDailyEvent) {
            // 여기를 지나면 한번 본거임.
            dicMenuList ["LPanel_check"].SetActive (true);
            for (int i = 1; i < 8; i++) {
                dicMenuList ["LPanel_check"].transform.FindChild ("bundle_contents/day" + i).gameObject.SetActive (false);
            }
            dicMenuList ["LPanel_check"].transform.FindChild ("bundle_day/day" + (Ag.mySelf.loginCount % 7 == 0 ? 7 : Ag.mySelf.loginCount % 7)).animation.Play ();
            dicMenuList ["LPanel_check"].transform.FindChild ("bundle_contents/day" + (Ag.mySelf.loginCount % 7 == 0 ? 7 : Ag.mySelf.loginCount % 7)).gameObject.SetActive (true);
        }

        ImageBanner (Joycity.arrImageNoti.Count);
        TextBanner ();
    }

    /// <summary>
    /// 텍스트 공지사항 출력
    /// </summary>
    void TextBanner ()
    {


        string pStamp;
        Ag.LogString (" MenuManager :: joycity TextBanner Count" + Joycity.arrTextNotice.Count);
        for (int i = 0; i < Joycity.arrTextNotice.Count; i++) { 

            GameObject Gobj;

            JceTextNotice curNoti = Joycity.arrTextNotice [i];
            //curNoti.IsFreqency = true;

            if (curNoti.IsFreqency) {
                pStamp = PreviewLabs.PlayerPrefs.GetString ("JoyCityTextBannerTextFreq" + curNoti.idx);
                if (string.IsNullOrEmpty (pStamp)) {
                    curNoti.AlreadySeenNum = 0;
                    Ag.LogDouble (" TextBanner >>>   IsFrequency..  first time  " + curNoti.frequency_time + "   AlreadySeen : " + curNoti.AlreadySeenNum);
                } else {
                    try {
                        curNoti.AlreadySeenNum = int.Parse (pStamp);
                        Ag.LogDouble (" TextBanner >>>   IsFrequency..  time ?  " + curNoti.AlreadySeenNum);
                    } catch {
                        Ag.LogIntenseWord (" TextBanner >>>   Catch .... >>>>    Error   ");
                        curNoti.AlreadySeenNum = 1;
                    }

                    if (curNoti.AlreadySeenNum >= int.Parse (curNoti.frequency_time))
                        continue;
                }
            }

            Gobj = (GameObject)Instantiate (Resources.Load ("prefab_General/TextNotice"));
            Gobj.transform.parent = FindGameObject ("Ui_camera/Camera", true).transform;
            Gobj.GetComponent<UIAnchor> ().panelContainer = FindGameObject ("Ui_camera", true).gameObject.GetComponent<UIPanel> ();
            Gobj.transform.localPosition = new Vector3 (0, 0, -1000 - i * 60);
            Gobj.transform.localScale = new Vector3 (1, 1, 1);
            Gobj.GetComponent<CloseThisObject> ().mTimestamp = curNoti.timestamp;

            Gobj.name = "JoyCityTextBanner" + curNoti.idx;
            Gobj.GetComponent<CloseThisObject> ().mUrl = Joycity.arrTextNotice [i].url;
            Ag.LogString ("Joycity.arrTextNotice [i].url" + Joycity.arrTextNotice [i].url ,false);

            Gobj.GetComponent<CloseThisObject> ().JceNotiObj = curNoti;
            Gobj.transform.FindChild ("Label_content").gameObject.GetComponent<UILabel> ().text = curNoti.content;
            Gobj.transform.FindChild ("Label_title").gameObject.GetComponent<UILabel> ().text = curNoti.title;
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_close", true), Gobj, "DestoryTextObj");
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_detail", true), Gobj, "OpenUrl");
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_ok", true), Gobj, "DestoryTextObj");

        }
    }

    /// <summary>
    /// 이미지 공지사항 출력
    /// </summary>
    /// <param name="pCount">P count.</param>
    void ImageBanner (int pCount)
    {
        GameObject Gobj;
        string StampFreq, StampTime;
        double PresentStamp, PresentTime;

        Ag.LogIntenseWord ("    Image Banner >>>>>>>>>>>>>>>>>   ");
        for (int i = 0; i < pCount; i++) {
            JceImgNotice curNoti = Joycity.arrImageNoti [i];
            // Time Stamp Setting ...
            //string timeKey = "JoyCityImageBanner" + curNoti.idx;
            string timeKey = "JoyCityImageBanner" + curNoti.idx;
            StampTime = PreviewLabs.PlayerPrefs.GetString (timeKey);
            if (string.IsNullOrEmpty (StampTime))
                StampTime = "1390441587486";

            PresentStamp = double.Parse (Joycity.arrImageNoti [i].timestamp);
            Material ImageBannerPic;
            ImageBannerPic = Instantiate (Resources.Load ("Materials/Imagebanner")) as Material;

            if (curNoti.IsFreqency) {
                Ag.LogString ("    it's frequency ....    " + ("JoyCityImageBanner1" + curNoti.idx));
                StampFreq = PreviewLabs.PlayerPrefs.GetString ("JoyCityImageBannerFreq" + curNoti.idx);
                if (string.IsNullOrEmpty (StampFreq)) {
                    curNoti.AlreadySeenNum = 0;
                    Ag.LogDouble (" TextBanner >>>   IsFrequency..  first time  " + curNoti.frequency_time + "   AlreadySeen : " + curNoti.AlreadySeenNum);
                } else {
                    try {
                        curNoti.AlreadySeenNum = int.Parse (StampFreq);
                        Ag.LogDouble (" TextBanner >>>   IsFrequency..  time ?  " + curNoti.AlreadySeenNum);
                    } catch {
                        Ag.LogIntenseWord (" TextBanner >>>   Catch .... >>>>    Error   ");
                        curNoti.AlreadySeenNum = 1;
                    }

                    if (curNoti.AlreadySeenNum >= int.Parse (curNoti.frequency_time))
                        continue;
                }
            }

            Gobj = (GameObject)Instantiate (Resources.Load ("prefab_General/Lpanel_Event"));
            Gobj.transform.parent = dicMenuList ["Ui_lobby"].gameObject.transform;
            Gobj.transform.localPosition = new Vector3 (0, 0, -295 - i * 60);
            Gobj.transform.localScale = new Vector3 (1, 1, 1);
            Gobj.name = "JoyCityImageBanner" + curNoti.idx;
            Gobj.GetComponent<CloseThisObject> ().mTimestamp = Joycity.arrImageNoti [i].timestamp;
            Gobj.GetComponent<CloseThisObject> ().JceNotiObj = curNoti;
            Ag.LogIntenseWord (Joycity.arrImageNoti [i].platform.ToString()  + "PlatformNum");

            Gobj.transform.FindChild ("banner").gameObject.GetComponent<UITexture> ().material = ImageBannerPic;

            Gobj.GetComponent<CloseThisObject> ().mUrl = Joycity.arrImageNoti [i].url;

            StartCoroutine (JoycityImageBannerLoad (Joycity.arrImageNoti [i].image_path, Gobj.transform.FindChild ("banner").gameObject));
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_close", true), Gobj, "DestoryObj");
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_datail", true), Gobj, "OpenUrl");

            try {
                if (Ag.UnixTimeStampToDateTimeAddmili (double.Parse(StampTime)).AddDays (1) > Ag.UnixTimeStampToDateTimeAddmili (PresentStamp)) {
                    Gobj.SetActive (false);
                }
            } catch {
                Ag.LogIntenseWord ("ImageBanner Error");
            }
        }
    }

    void SetNodeDelegate ()
    {
        Ag.NodeObj.MySocket.dlgtGameStartAsHost = MatchStart;
        Ag.NodeObj.MySocket.dlgtJoinAsVisitor = MatchStart;
        Ag.NodeObj.MySocket.dlgtInvited = Invited;
        Ag.NodeObj.MySocket.InviteErrorOf = InviteError;
        Ag.NodeObj.MySocket.dlgtIamRefused = InviteRefused;
        Ag.NodeObj.MySocket.dlgtEnemyLeft = EnemyLeft;
    }

    bool MatchStartFlag, InviteStartFlag, InviteRefusedFlag, InViteErrorflag;

    public void InviteError (string pStatus)
    {
        InViteErrorflag = true;
    }

    public override void BaseStartSetting ()
    {
    }

    DateTime KakaoTime;
    int Hours, Minitues, Second;
    double dHours;
    bool mInviteRefuse;

    void xxSubMenuClose ()
    {
        Btn_Fun_PointBuyCancel ();
        Btn_Fun_GloveBuyCancel ();
        Btn_Fun_CashBuyCancel ();
        Btn_Fun_withDrawCancel ();
        Btn_Fun_Kicker_PlayerNameEditClose ();
        Btn_Fun_Kicker_conditionClose ();
        Btn_Fun_Kicker_dresseditemClose ();
        Btn_Fun_Kicker_enchantPlayerclose ();
        Btn_fun_RecharterClose ();
        btn_Costup_Cancel ();
        Btn_Fun_BuyNorMarCard1cancel ();
        Btn_Fun_BuyHighCard1cancel ();
        Btn_Fun_BuyKleagueCard1cancel ();

        Btn_Fun_Panel_kicker_Close ();
        Btn_Fun_Panel_keeper_Close ();

        Btn_Fun_Lobby_BuyNorMarCard1cancel ();
        Btn_Fun_Lobby_BuyHighCard1cancel ();
        Btn_Fun_Lobby_BuyKleagueCard1cancel ();

        Btn_Fun_BuySoccerGloveShose_BuyCancel ();
        Btn_Fun_MixItem_BuyCancel ();
        Btn_Fun_teamnameeditCancel ();


        Btn_Fun_Keeper_conditionUPClose ();
        Btn_Fun_Keeper_PlayerNameEditClose ();
        Btn_Fun_Keeper_dresseditemClose ();
        Btn_Fun_Keeper_enchantPlayerClose ();
        Btn_fun_KeeperRecharterClose ();
        Btn_Fun_UniformBuyCancel ();
        Btn_Fun_recordinitializationCancel ();
        BuyDrinkCancel ();
        BuyItemCancel ();

        SendGiftCancel ();
        InvitefriendCancel ();

        popup_mixitem1_Cancel ();
        popup_mixitem2_Cancel ();
        popup_mixitem3_Cancel ();

        Cardmix_popup_btn_cancel ();

        popup_mixerror_btn_ok ();
        Cancel_Buy_Playball ();

        UniformIgnore ();
    }
    //bool mGameMatchOk = false;
    public override void Update ()
    {
        base.Update ();
        //------------------------------------------------ContWin CoolTime 
        DateTime nw = DateTime.Now;
        DateTime target;
        if (nw.Hour == 23 || nw.Hour < 8) {
            if (nw.Hour == 23)
                nw = nw.AddHours (1);
            target = new DateTime (nw.Year, nw.Month, nw.Day, 8, 0, 0);
        } else
            target = new DateTime (nw.Year, nw.Month, nw.Day, nw.Hour + 1, 0, 0);

        TimeSpan display = target - nw;
        dicMenuList ["popup_oclock"].transform.FindChild ("Label_time").GetComponent<UILabel> ().text =
        //(mDtobj - System.DateTime.Now).Minutes.ToFixedWidth (2) + ":" + (mDtobj - System.DateTime.Now).Seconds.ToFixedWidth (2);
            display.Minutes.ToFixedWidth (2) + ":" + display.Seconds.ToFixedWidth (2);

		if (Input.GetKeyDown (KeyCode.Escape)) { 

		}

//        if (Input.GetKeyDown (KeyCode.Escape)) { 
//            if (mGameMatchOk)
//                return;
//
//            if (!mBackDepthFlag)
//                MenuCommonOpen ("Ui_popup", "ask_Exit", true);
//
//            if (mBackDepthFlag) {
//                Btn_LPanel_Shop_CoseBtn_OK ();
//                Btn_Fun_AddFriendListBoxClose ();
//                Btn_Fun_PostBoxClose ();
//                Btn_Fun_SettingBoxClose ();
//                Btn_Fun_GiftBoxClose ();
//                Btn_Fun_LineupClose ();
//                Btn_Fun_MatchRequireclose ();
//
//                // subMenu
//                SubMenuClose ();
//
//                mBackDepthFlag = false;
//                if (mKickoff)
//                    Btn_Fun_MatchCancleAndGoOut ();
//            }
//        }

        if (Ag.NetExcpt.WasLoginDuplicate) {
            DoubleLoginError ();
        }


        if (Ag.NodeObj != null && Ag.NodeObj.MySocket.dlgtInvited == null)
            SetNodeDelegate ();

        if (DefAniFlag) {
            DefAnimaPlay ();
        }
        //        Debug.Log (Ag.mySelf.FreeCouponLimitDT +  "");

        KakaoPlayCoolTime ();

        if (LobbyPlayAniFlag)
            LobbyPlayAni ();

        if (EnemyLeftflag) {

            if (Ag.GameStt.AmInvitingFriend)
                CancelInvitingByMe ();

            MenuCommonOpen ("alert_someoneout", "Ui_popup", true);
        }

        arrState.DoAction ();
        mCurState = arrState.GetCurStateName ();

        if (MyDataLoad && !dicPlayerOrObj ["MyPlayer"].animation.isPlaying) {
            dicPlayerOrObj ["MyPlayer"].animation.Play ("08_Aps_10_(400F)");
        }

        if (mEnemyPlayer != null && !mEnemyPlayer.animation.isPlaying) {
            mEnemyPlayer.animation.Play ("Kick_Ready");
            if (Ag.mSingleMode)
                Ag.NodeObj.GameStartMsgSent.Enem = true;
        }

        if (MatchStartFlag) {
            MatchStartFlag = false;
            Ag.FriendMatchingMode = true;
        }

        if (InviteStartFlag) {
            int SumPlayerCost = 0, PlayerExtendEa = 0;
            Debug.Log ("Invited Flag OK");
            for (int i = 0; i < 6; i++) {
                SumPlayerCost += Ag.mySelf.GetCardOrderOf (i).WAS.cost;
                if (Ag.mySelf.GetCardOrderOf (i).WAS.limitGameEA < 1)
                    PlayerExtendEa++;
            }

            if (SumPlayerCost > Ag.mySelf.WAS.Cost) {
                Ag.NodeObj.MySocket.ActionRefuse (Ag.mySelf);
                return;
            }
            if (PlayerExtendEa > 0) {
                Ag.NodeObj.MySocket.ActionRefuse (Ag.mySelf);
                return;
            }

            if (mSumGold < 0) {
                Ag.NodeObj.MySocket.ActionRefuse (Ag.mySelf);
                return;
            }

            if (!PreviewLabs.PlayerPrefs.GetBool ("DidTutorial")) {
                Ag.NodeObj.MySocket.ActionRefuse (Ag.mySelf);
                return;
            }

            dicMenuList ["Ui_popup"].SetActive (true);
            dicMenuList ["versus_accept"].SetActive (true);
            dicMenuList ["versus_accept"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = Ag.NodeObj.MySocket.CurEnemy.kkoNick;
            StartCoroutine (InviteRefuse ());
            InviteStartFlag = false;
        }

        if (InviteRefusedFlag) {
            dicMenuList ["Ui_popup"].SetActive (true);
            dicMenuList ["invite_versus"].SetActive (false);
            dicMenuList ["versus_refuse"].SetActive (true);
            dicMenuList ["versus_refuse"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = mKkoNick;
            InviteRefusedFlag = false;
            mInviteRefuse = true;
        }

        dicMenuList ["GoldLabel"].GetComponent<UILabel> ().text = GetGoldValue ().ToString ();
        dicMenuList ["CashLabel"].GetComponent<UILabel> ().text = (Ag.mySelf.mCash1 + Ag.mySelf.mCash2).ToString ();// 코 인 안 씀. 
        //--------------------------------------------------------------- PrePareMatch Update

        if (Ag.NetExcpt.ConnectLossSignalGone && !AgStt.IntendedPause) {
            //if (AgStt.IsGaming.HasValue && AgStt.IsGaming.Value)
            if (Ag.GameStt.IsGameMatched)
                MenuCommonOpen ("alert_restart", "Ui_popup", true);
            else
                MenuCommonOpen ("alert_networkerror", "Ui_popup", true);
        }

        if (AgStt.InAppPurchaseSuccess) {
            MenuCommonOpen ("Ui_popup", "buy_alert", true);
            AgStt.InAppPurchaseSuccess = false;
        }
    }

    public int mSumGold;

    int GetGoldValue ()
    {
        mSumGold = 0;
        mSumGold = Ag.mySelf.mGold;
        if (Ag.mBlueItemFlag && Ag.mRedItemFlag && Ag.mGreenItemFlag) {
            mSumGold = Ag.mySelf.mGold - 875;
            return mSumGold;
        }
        if (Ag.mBlueItemFlag && Ag.mRedItemFlag) {
            mSumGold = Ag.mySelf.mGold - 700;
            return mSumGold;
        }
        if (Ag.mBlueItemFlag && Ag.mGreenItemFlag) {
            mSumGold = Ag.mySelf.mGold - 525;
            return mSumGold;
        }
        if (Ag.mRedItemFlag && Ag.mGreenItemFlag) {
            mSumGold = Ag.mySelf.mGold - 525;
            return mSumGold;
        }
        if (Ag.mBlueItemFlag) {
            mSumGold = Ag.mySelf.mGold - 350;
            return mSumGold;
        }
        if (Ag.mRedItemFlag) {
            mSumGold = Ag.mySelf.mGold - 350;
            return mSumGold;
        }
        if (Ag.mGreenItemFlag) {
            mSumGold = Ag.mySelf.mGold - 175;
            return mSumGold;
        }

        return mSumGold;
    }
    //    void xxHasGLoveNum ()
    //    {
    //        for (int i = 0; i < 5; i++) {
    //            dicMenuList ["heart" + i].SetActive (false);
    //        }
    //        if (Ag.mKakaoGlove > 5) {
    //            for (int i = 0; i < 5; i++) {
    //                dicMenuList ["heart" + i].SetActive (true);
    //            }
    //        }
    //        if (Ag.mKakaoGlove <= 5 && Ag.mKakaoGlove >= 0) {
    //            for (int i = 0; i < Ag.mKakaoGlove; i++) {
    //                dicMenuList ["heart" + i].SetActive (true);
    //            }
    //        }
    //    }
    int InviteNum;
    #if UNITY_ANDROID
    AndroidJavaObject activity;
    #endif
    public override void OnGUI ()
    {
        /*
        if (GUI.Button (new Rect (10, 10, 100, 100), "1")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore01_test";
        }
        if (GUI.Button (new Rect (110, 10, 100, 100), "2")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore02_test";
        }
        if (GUI.Button (new Rect (210, 10, 100, 100), "3")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore03_test";
        }
        if (GUI.Button (new Rect (310, 10, 100, 100), "4")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore04_test";
        }
        if (GUI.Button (new Rect (410, 10, 100, 100), "5")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore05_test";
        }
        if (GUI.Button (new Rect (510, 10, 100, 100), "6")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore06_test";
        }
        if (GUI.Button (new Rect (610, 10, 100, 100), "7")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore07_test";
        }
        if (GUI.Button (new Rect (710, 10, 100, 100), "8")) {
            Productid = "com.appsgraphy.kvsskakao.popupstore08_test";
        }

        if (GUI.Button (new Rect (10, 110, 100, 100), "queryinventory")) {
            var skus = new string[] {
                "com.prime31.testproduct",
                "android.test.purchased",
                "com.prime31.managedproduct",
                "com.prime31.testsubscription"
            };
            #if UNITY_ANDROID
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            GoogleIAB.queryInventory( skus );
            #endif
            GetTranjectionkey();
        }

        if (GUI.Button (new Rect (10, 210, 100, 100), "purchase Item")) {
            AgStt.IntendedPause = true;
            GoogleIAB.purchaseProduct (Productid,GetUniqueKey(20));
        }

        if (GUI.Button (new Rect (110, 210, 100, 100), "consume")) {
            GoogleIAB.consumeProduct (Productid);
        }
        */

        /*




            MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerGloves", true);
            /*
            dicMenuList.Add ("PopupScoutPlayers", FindMyChild (dicMenuList ["Ui_popupstore"],"store_0", false));

            dicMenuList.Add ("PopupBuySoccerGloves", FindMyChild (dicMenuList ["Ui_popupstore"],"store_1", false));

            dicMenuList.Add ("PopupBuySoccerShoe", FindMyChild (dicMenuList ["Ui_popupstore"],"store_2", false));

            dicMenuList.Add ("PopupCardMixLuckTicket", FindMyChild (dicMenuList ["Ui_popupstore"],"store_3", false));

            dicMenuList.Add ("PopupCardMixGradeReserveTicket", FindMyChild (dicMenuList ["Ui_popupstore"],"store_4", false));

            dicMenuList.Add ("PopupBuyEventCash", FindMyChild (dicMenuList ["Ui_popupstore"],"store_5", false));

            dicMenuList.Add ("PopupBuyPlayTimeaWeek", FindMyChild (dicMenuList ["Ui_popupstore"],"store_6", false));

            dicMenuList.Add ("PopupBuyPlayTimeaMonth", FindMyChild (dicMenuList ["Ui_popupstore"],"store_7", false));

        }





        */

    }
    //Delete User Info
    private void onDeleteUserInfoComplete ()
    {
        Debug.Log ("onDeleteUserInfoComplete");
        //please call reset method to user game info
    }

    private void onDeleteUserInfoError (string status, string message)
    {

        Debug.Log ("onDeleteUserInfoError");
        showAlertErrorMessage (status, message);
    }
}






    
   
 
 



