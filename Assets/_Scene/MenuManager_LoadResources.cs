//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    GameObject mMainFade, mPlayerPlanKpCamera, mGotobuyPlayer, mMenuTeamItemBox, mUImanager, mKpUImanager, mMenuShop, m_GObj_TeamUseItem, m_Obj_Slider, m_Obj_SliderBar, 
        mPresentOpenBox, mLineUpOpenBox, mCeremonyItem, mDrinkItem, mMessageItem, mMessageEdit;
    float secnd, SoundValue, mFxSoundValue, mItemMoney = 100, mItemCount = 0, mItemCountNum = 0;
    int mItemList, mGoodPrice;
    public bool LogIN = false, GameStart = false, _PrePare_TeamItemFlag = false;
    public GameObject mProgressbar, mItemSlider, mKicker, mkBall, mFailedBtn, mMenu1, mMenu2, mItemLabellist, mItemShowBox, mUniformBack, mPreviewBack, Menu2, mWeeklyRank,
        mUniform, mPlayerPlan, mMart, mGloveItem, mGoldItem, mCoinItem, mdirectorBox, mKakaobar, mAppfriendBar, Submenu, mDirectionMode;
    public Texture2D mKakaoPic;
    public Camera mPrePareCam, PlayerCam;
    List <GameObject> arrPic;
    List <Material> arrPicMat;
    Transform KickerUniform;
    List <GameObject> arrSound = new List<GameObject> ();
    List <GameObject> arrLkakaobar = new List<GameObject> ();
    public Dictionary<string,GameObject> dicMenuList = new Dictionary<string, GameObject> ();
    // Purchase Related ...
    UILabel ngLblDrinkCnt, ngLblPriceInGold;
    bool showItemPurchasePopup = false;
    // arr, dic,


    void LoadMenuResources ()
    {
        //GameObject.Find ("MainSound").audio.Play();
        Ag.arrPlayerPolygon = new List<GameObject> ();
        arrPic = new List<GameObject> ();
        mRscrcMan = new HtRsrcMan ("");
        mTargetObj = mRscrcMan.FindGameObject ("Axis/Camera/Match", true);

        Ag.LogString (" MenuManager Load Resources :: Started   ");

        dicMenuList.Add ("Panel_top", mRscrcMan.FindGameObject ("Panel_top", true));
        dicMenuList.Add ("CenterCircle", mRscrcMan.FindGameObject ("Ui_camera/Camera/centercircle", false));
        dicMenuList.Add ("GoldLabel", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "coin/Label_coin", true));
        dicMenuList.Add ("CashLabel", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "cash/Label_cash", true));
        //dicMenuList.Add ("GloveLabel", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart/Label_exceed", true));
        dicMenuList.Add ("MainCamera", mRscrcMan.FindGameObject ("Axis/Main Camera", true));
        dicMenuList.Add ("LPanel_shop", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop"], "btn_end", true), mTargetObj, "Btn_LPanel_Shop_CoseBtn_OK");
        dicMenuList.Add ("LPanel_shop_btn_close", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/btn_close", false));
        dicMenuList.Add ("LPanel_shop_table_cash", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/table_cash", true));
        dicMenuList.Add ("LPanel_shop_table_glove", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/table_glove", true));
        dicMenuList.Add ("LPanel_shop_table_point", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/table_point", true));
        dicMenuList.Add ("popup_buycash", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/popup_buycash", false));
        dicMenuList.Add ("popup_buyglove", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/popup_buyglovebundle", false));
        dicMenuList.Add ("popup_buypoint", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/popup_buypoint", false));

        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Label_healthminus", false);
        dicMenuList.Add ("Panel_top_cash", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "cash", true));
        dicMenuList.Add ("Panel_top_coin", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "coin", true));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "btngrid/btn_buy", true), mTargetObj, "Btn_Fun_CashBuyOk");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_CashBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "btn_close", true), mTargetObj, "Btn_Fun_CashBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_GloveBuyOk");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/btn_close", true), mTargetObj, "Btn_Fun_GloveBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_GloveBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "btngrid/btn_buy", true), mTargetObj, "Btn_Fun_PointBuyOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_PointBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "btn_close", true), mTargetObj, "Btn_Fun_PointBuyCancel");


        dicMenuList.Add ("checkbox0_cash", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox0_cash", true));
        dicMenuList.Add ("checkbox3_glove", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox3_glove", true));
        dicMenuList.Add ("checkbox1_point", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox1_point", true));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox0_cash", true), mTargetObj, "Btn_Fun_EquipBoxShopCash");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox3_glove", true), mTargetObj, "Btn_Fun_EquipBoxShopGlove");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/bundle_tap/checkbox1_point", true), mTargetObj, "Btn_Fun_EquipBoxShopGold");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "cash/btn_shop", true), mTargetObj, "Btn_Fun_EquipBoxShopCash");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/btn_shop", true), mTargetObj, "Btn_Fun_EquipBoxShopGlove");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "coin/btn_shop", true), mTargetObj, "Btn_Fun_EquipBoxShopGold");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Panel_top"], "LPanel_shop/btn_close", true), mTargetObj, "Btn_Fun_EquipBoxShopClose");


        dicMenuList.Add ("heart_freebundle_eaHeart", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart/Label_exceed", true));
        //dicMenuList.Add ("heart_freebundle_label", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_free/heartgrid/Label", true));
        //dicMenuList.Add ("heart_freebundle_label", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_free/heartgrid/Label", true));
        dicMenuList.Add ("heart_freebundle_Label_refreshtime", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart/Label_refreshtime", true));
        dicMenuList.Add ("heart_freebundle_heartfree_label", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_free/heartgrid/Label", true));
        dicMenuList.Add ("heart_freebundle_heartfree_Label_refreshtime", mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_free/Label_refreshtime", true));

        dicMenuList.Add ("Ui_lobby", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_lobby", true));
        dicMenuList.Add ("LPanel_friend", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend", false));

        for (int i = 1; i < 6; i++) { 
            dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check"+i).gameObject.SetActive(false);
        }



        dicMenuList.Add ("LPanel_event", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_event", false));

		dicMenuList.Add ("LPanel_olclock", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock", false));
		dicMenuList.Add ("popup_oclock", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock", false));
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/btn_oclock", true), mTargetObj, "Oclock_event_Open");
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock/btn_close", true), mTargetObj, "Oclock_event_Close");
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock/btngrid/btn_ok", true), mTargetObj, "Oclock_event_Close");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/btn_event", false), mTargetObj, "first_purchase_popupOpen");


        dicMenuList.Add ("LPanel_itemshop", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop", false));
        dicMenuList.Add ("LPanel_post", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post", false));
        dicMenuList.Add ("LPanel_setting", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting", false));



        dicMenuList.Add ("Lobby_Coach_Nick", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/Label_name", true));
        dicMenuList.Add ("Lobby_Coach_TeamName", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/Label_teamname", true));
        //dicMenuList.Add ("Lobby_Coach_nations", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/Label_nations", true));
        dicMenuList.Add ("Lobby_gameamount", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/Label_gameamount", true));

        dicMenuList.Add ("Lobby_Allrecord", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_total/record_grid2/Label1", true));
        dicMenuList.Add ("Lobby_rank", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_total/record_grid2/Label3", true));
        dicMenuList.Add ("Lobby_WeekScore", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_total/record_grid2/Label2", true));

        dicMenuList.Add ("Lobby_TopWinNum", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_week/record_grid2/Label1", true));
        dicMenuList.Add ("Lobby_Winrate", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_week/record_grid2/Label1_1", true));
        dicMenuList.Add ("Lobby_WeekRank", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/record/record_week/record_grid2/Label2", true));



        dicMenuList.Add ("Lobby_KakaoFace", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/face", true));
        dicMenuList.Add ("Lobby_Flag", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/flag", true));



        dicMenuList.Add ("scroll_friend", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_friend", true));
        dicMenuList.Add ("scroll_rank", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_rank", false));

        dicMenuList.Add ("Lobbyscroll_message", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_message", true));
        dicMenuList.Add ("scroll_notice", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_notice", false));




        dicMenuList.Add ("buycard", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard", true));
        dicMenuList.Add ("Lobbycardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup", false));

        dicMenuList.Add ("LobbyHigh1Card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl1card", false));
        dicMenuList.Add ("LobbyHigh3Card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl3card", false));
        dicMenuList.Add ("Lobbynormal1Card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal1card", false));
        dicMenuList.Add ("Lobbynormal3Card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal3card", false));
        dicMenuList.Add ("Lobbykleague1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague1card", false));
        dicMenuList.Add ("Lobbykleague3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague3card", false));



        dicMenuList.Add ("Lobbyhighl1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl1card/cardbundle/card1", true));
        dicMenuList.Add ("Lobbyhighl3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("Lobbyhighl3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("Lobbyhighl3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl3card/cardgrid/cardbundle2/card1", true));
        dicMenuList.Add ("Lobbynormal1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal1card/cardbundle/card1", true));
        dicMenuList.Add ("Lobbynormal3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("Lobbynormal3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("Lobbynormal3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal3card/cardgrid/cardbundle2/card1", true));
        dicMenuList.Add ("Lobbykleague1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague1card/cardbundle/card1", true));
        dicMenuList.Add ("Lobbykleague3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("Lobbykleague3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("Lobbykleague3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague3card/cardgrid/cardbundle2/card1", true));


        dicMenuList.Add ("LPanel_check", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_check", false));



        dicMenuList ["Lobbyhighl1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbyhighl3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbyhighl3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbyhighl3_3card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbynormal1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbynormal3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbynormal3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbynormal3_3card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbykleague1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbykleague3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbykleague3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["Lobbykleague3_3card"].AddComponent<PlayerCardInfo> ();


        dicMenuList.Add ("LobbyBuycardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup", false));
        dicMenuList.Add ("LobbyBuypopuphighl1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl1card", false));
        dicMenuList.Add ("LobbyBuypopuphighl3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl3card", false));
        dicMenuList.Add ("LobbyBuypopupnormal1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal1card", false));
        dicMenuList.Add ("LobbyBuypopupnormal3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal3card", false));
        dicMenuList.Add ("LobbyBuypopupkleague1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague1card", false));
        dicMenuList.Add ("LobbyBuypopupkleague3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague3card", false));


        dicMenuList.Add ("LobbyBuyFreecardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup", false));
        dicMenuList.Add ("LobbyBuyFreepopuphighl1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl1card", false));
        dicMenuList.Add ("LobbyBuyFreepopuphighl3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl3card", false));
        dicMenuList.Add ("LobbyBuyFreepopupnormal1card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal1card", false));
        dicMenuList.Add ("LobbyBuyFreepopupnormal3card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal3card", false));


        dicMenuList.Add ("NormalCardTicket1_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_normal/btn_1card/tiket/Label_num", true));
        dicMenuList.Add ("NormalCardTicket3_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_normal/btn_3card/tiket/Label_num", true));
        dicMenuList.Add ("HighCardTicket1_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_super/btn_1card/tiket/Label_num", true));
        dicMenuList.Add ("HighCardTicket3_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_super/btn_3card/tiket/Label_num", true));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3OK");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3cancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/highl3card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardfree_popup/normal3card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3cancel");

    

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_normal/btn_1card", true), mTargetObj, "Btn_Fun_normal1card");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_normal/btn_3card", false), mTargetObj, "Btn_Fun_normal3card");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_super/btn_1card", true), mTargetObj, "Btn_Fun_highl1card");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_super/btn_3card", false), mTargetObj, "Btn_Fun_highl3card");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_kleague/btn_1card", true), mTargetObj, "Btn_Fun_Kleague_card");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/grid_card_kleague/btn_3card", false), mTargetObj, "Btn_Fun_Kleague_3card");
      
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_HighCard_Rebuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_CardPopupClose");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/highl3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_NorMalCard_Rebuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/normal3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_KleagueCard_Rebuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague1card/btn_close", true), mTargetObj, "Btn_Fun_CardPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardopen_popup/kleague3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_CardPopupClose");



        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard3OK");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard3cancel");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/highl3card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/normal3card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyNorMarCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague1card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/buycard/cardbuy_popup/kleague3card/btn_close", true), mTargetObj, "Btn_Fun_Lobby_BuyKleagueCard3cancel");


        dicMenuList.Add ("glove", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove", true));
        dicMenuList.Add ("coupon", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/coupon", true));
        dicMenuList.Add ("mixitem", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem", false));
        dicMenuList.Add ("shoes", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes", false));
        dicMenuList.Add ("Ui_Popup", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup", false));

        dicMenuList.Add ("ask_review", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/ask_review", false));
        dicMenuList.Add ("boast_success", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/boast_success", false));
        dicMenuList.Add ("invite_friend", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_friend", false));
        dicMenuList.Add ("invite_success", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_success", false));
        dicMenuList.Add ("rank_boast", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/rank_boast", false));
        dicMenuList.Add ("Lobby_popup_playerfull", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/popup_playerfull", false));
        dicMenuList.Add ("invite_kakaofail", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_kakaofail", false));




        dicMenuList.Add ("buy_item", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item", false));
        dicMenuList.Add ("buy_item2", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item2", false));
        dicMenuList.Add ("game_withdraw", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/game_withdraw", false));
        dicMenuList.Add ("rankchange", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/rankchange", false));
        dicMenuList.Add ("send_gift", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/send_gift", false));
        dicMenuList.Add ("sendgift_success", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/sendgift_success", false));
        dicMenuList.Add ("AppVersion", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category0_version/Label_version", true));
        dicMenuList.Add ("Appid", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category1_id/Label_id", true));
        dicMenuList.Add ("emptynotice", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_notice/emptynotice", false));
        dicMenuList.Add ("emptymessage", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_message/emptymessage", false));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["Lobby_popup_playerfull"], "btn_close", true), mTargetObj, "Lobby_popup_playerfullClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["Lobby_popup_playerfull"], "btn_ok", true), mTargetObj, "Lobby_popup_playerfullClose");

       
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["sendgift_success"], "btn_close", true), mTargetObj, "sendgift_successPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["sendgift_success"], "btn_ok", true), mTargetObj, "sendgift_successPopupClose");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["invite_success"], "btn_close", true), mTargetObj, "inviteSuccess_successPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["invite_success"], "btn_ok", true), mTargetObj, "inviteSuccess_successPopupClose");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["invite_kakaofail"], "btn_close", true), mTargetObj, "invite_kakaofail_successPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["invite_kakaofail"], "btn_ok", true), mTargetObj, "invite_kakaofail_successPopupClose");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/btn_book", true), mTargetObj, "Btn_Fun_PlayerBookOpen");

       
        /// <summary>
        /// Cash Glove Point
        /// </summary>
        /// 
        /// 
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table1/btn_1", true), mTargetObj, "Btn_Fun_Point3000");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table1/btn_2", true), mTargetObj, "Btn_Fun_Point16500");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table1/btn_3", true), mTargetObj, "Btn_Fun_Point36000");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table2/btn_1", true), mTargetObj, "Btn_Fun_Point117000");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table2/btn_2", true), mTargetObj, "Btn_Fun_Point210000");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_point"], "table2/btn_3", true), mTargetObj, "Btn_Fun_Point450000");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_1", true), mTargetObj, "Btn_Fun_Cash10");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_2", true), mTargetObj, "Btn_Fun_Cash55");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_3", true), mTargetObj, "Btn_Fun_Cash120");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_1", true), mTargetObj, "Btn_Fun_Cash390");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_2", true), mTargetObj, "Btn_Fun_Cash700");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_3", true), mTargetObj, "Btn_Fun_Cash1500");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_1", true), mTargetObj, "Btn_Fun_Cash10");

        #if UNITY_IPHONE
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_1/Label_price", true).GetComponent<UILabel>().text = "$2.99";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_2/Label_price", true).GetComponent<UILabel>().text = "$4.99";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_3/Label_price", true).GetComponent<UILabel>().text = "$9.99";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_1/Label_price", true).GetComponent<UILabel>().text = "$29.99";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_2/Label_price", true).GetComponent<UILabel>().text = "$49.99";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_3/Label_price", true).GetComponent<UILabel>().text = "$99.99";
        #endif
        #if UNITY_ANDROID
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_1/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"3,000";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_2/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"5,000";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table1/btn_3/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"10,000";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_1/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"30,000";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_2/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"50,000";
        mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_cash"], "table2/btn_3/Label_price", true).GetComponent<UILabel>().text = WWW.UnEscapeURL("%5C")+"100,000";
        #endif






        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table1/btn_1", true), mTargetObj, "Btn_Fun_Glove5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table1/btn_2", true), mTargetObj, "Btn_Fun_Glove11");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table1/btn_3", true), mTargetObj, "Btn_Fun_Glove24");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table2/btn_1", true), mTargetObj, "Btn_Fun_OneDayFree");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table2/btn_2", true), mTargetObj, "Btn_Fun_OneWeekFree");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_shop_table_glove"], "table2/btn_3", true), mTargetObj, "Btn_Fun_OneMonthFree");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_leftbtn/btn0_friend", true), mTargetObj, "Btn_Fun_AddFriendListBoxOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/btn_close", true), mTargetObj, "Btn_Fun_AddFriendListBoxClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_leftbtn/btn1_post", true), mTargetObj, "Btn_Fun_PostBoxOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/btn_close", true), mTargetObj, "Btn_Fun_PostBoxClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_leftbtn/btn2_setting", true), mTargetObj, "Btn_Fun_SettingBoxOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/btn_close", true), mTargetObj, "Btn_Fun_SettingBoxClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_leftbtn/btn3_itemshop", true), mTargetObj, "Btn_Fun_GiftBoxOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/btn_close", true), mTargetObj, "Btn_Fun_GiftBoxClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_rightbtn/btn0_team", true), mTargetObj, "Btn_Fun_GotoLineup");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/bundle_rightbtn/btn1_ready", true), mTargetObj, "Btn_Fun_MatchRequire");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/bundle_bottombtn/btn_allreceive", true), mTargetObj, "Btn_Fun_All_Receive");
        dicMenuList.Add ("checkbox0_rank", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/bundle_tap/checkbox0_rank", true));
        dicMenuList.Add ("panel_leftgrade", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "Panel_bottom/panel_leftgrade", true));

        dicMenuList.Add ("checkbox1_myfriend", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/bundle_tap/checkbox1_myfriend", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/bundle_tap/checkbox0_rank", true), mTargetObj, "FriendRank");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/bundle_tap/checkbox1_myfriend", true), mTargetObj, "Btn_Fun_FriendList");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/bundle_tap/checkbox0_message", true), mTargetObj, "Btn_Fun_PostList");
        dicMenuList.Add ("CheckBox_Message", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/bundle_tap/checkbox0_message", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/bundle_tap/checkbox1_notice", true), mTargetObj, "Btn_Fun_NoticeList");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox1_shoes", true), mTargetObj, "Btn_Fun_shoesList");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox3_mixitem", true), mTargetObj, "Btn_Fun_CardMixItem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox4_coupon", false), mTargetObj, "Btn_Fun_Coupon");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox0_card", true), mTargetObj, "Btn_Fun_Card");
        dicMenuList.Add ("Ui_lobby_checkbox0_card", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox0_card", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/bundle_tap/checkbox2_glove", true), mTargetObj, "Btn_Fun_Glove");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_friend/btngrid/btn_ok", true), mTargetObj, "InvitefriendOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_friend/btngrid/btn_cancle", true), mTargetObj, "InvitefriendCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/invite_friend/btn_close", true), mTargetObj, "InvitefriendCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/send_gift/btngrid/btn_ok", true), mTargetObj, "InvitefriendOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/send_gift/btngrid/btn_cancle", true), mTargetObj, "SendGiftCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/send_gift/btn_close", true), mTargetObj, "SendGiftCancel");



        /// <summary>
        /// Buy Soccer Shose, Mixitem
        /// </summary>

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01", true), mTargetObj, "Btn_Fun_SoccerGlove1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02", true), mTargetObj, "Btn_Fun_SoccerGlove2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03", true), mTargetObj, "Btn_Fun_SoccerGlove3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04", true), mTargetObj, "Btn_Fun_SoccerGlove4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/btn_buy", true), mTargetObj, "Btn_Fun_SoccerGlove_Buy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btn_close", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01", true), mTargetObj, "Btn_Fun_SoccerShose1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item02", true), mTargetObj, "Btn_Fun_SoccerShose2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item03", true), mTargetObj, "Btn_Fun_SoccerShose3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item04", true), mTargetObj, "Btn_Fun_SoccerShose4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/btn_buy", true), mTargetObj, "Btn_Fun_BuySoccerShoes_Buy");
        /*
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item/btn_close", true), mTargetObj, "Btn_Fun_BuySoccerGloveShose_BuyCancel");
        */
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01", true), mTargetObj, "Btn_Fun_MixItem1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02", true), mTargetObj, "Btn_Fun_MixItem2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03", true), mTargetObj, "Btn_Fun_MixItem3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/btn_buy", true), mTargetObj, "Btn_Fun_MixItem_Buy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/game_withdraw/btn_close", true), mTargetObj, "Btn_Fun_withDrawCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/game_withdraw/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_withDrawCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/game_withdraw/btngrid/btn_withdraw", true), mTargetObj, "Btn_Fun_withDrawOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item2/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_MixItem_BuyOK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item2/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_MixItem_BuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "popup/buy_item2/btn_close", true), mTargetObj, "Btn_Fun_MixItem_BuyCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/coupon/btn_ok", true), mTargetObj, "Btn_Fun_CouponNumInputOk");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_check/btn_close", true), mTargetObj, "Btn_Fun_DailyCheckOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_check/btn_ok", true), mTargetObj, "Btn_Fun_DailyCheckOk");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_event/btn_close", true), mTargetObj, "Btn_Fun_EventClose");



        /// <summary>
        /// Setting Gameset
        /// </summary>

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true), mTargetObj, "Btn_Fun_BgmSoundOnoff");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true), mTargetObj, "Btn_Fun_FxSoundOnoff");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true), mTargetObj, "Btn_Fun_ViewUserPic");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true), mTargetObj, "Btn_Fun_MessageAlert");

        /// <summary>
        /// Setting Account
        /// </summary>

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category2_btn1/btn0_facebook", true), mTargetObj, "Btn_Fun_OpenFaceBook");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category2_btn1/btn1_hompage", false), mTargetObj, "Btn_Fun_OpenHomepage");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category2_btn1/btn2_costomercenter", true), mTargetObj, "Btn_Fun_OpenCustomerCenter");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category2_btn1/btn0_term", true), mTargetObj, "Btn_Fun_OpenTerm");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category3_btn2/btn1_seccession", true), mTargetObj, "Btn_Fun_Seccession");

        if (Ag.mGuest) {
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category3_btn2/btn2_kakaologout", false), mTargetObj, "Btn_Fun_KakaoLogout");
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category3_btn2/btn3_kakaologin", true), mTargetObj, "kakao_sync_Lobby");
        } else {
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category3_btn2/btn2_kakaologout", true), mTargetObj, "Btn_Fun_KakaoLogout");
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category3_btn2/btn3_kakaologin", false), mTargetObj, "kakao_sync_Lobby");
        }


		dicMenuList.Add ("kakao_sync_Lobby", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/kakao_sync", false));
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["kakao_sync_Lobby"], "", false), mTargetObj, "kakao_sync_Lobby");
		//--------------------------------------------------------------- LineUP

        dicMenuList.Add ("Ui_team", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_team", false));
        dicMenuList.Add ("LPanel_buycard", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard", false));
        dicMenuList.Add ("cardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup", false));
        dicMenuList.Add ("pop_dontchange", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/pop_dontchange", false));
        dicMenuList.Add ("pop_cardfull", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/pop_cardfull", false));
        dicMenuList.Add ("popup_mixitem", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem", false));
        dicMenuList.Add ("gradesave_Label", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item01/Label_ possession", true));
        dicMenuList.Add ("AdvCombi_Label", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item02/Label_ possession", true));
        dicMenuList.Add ("LuckCombi_Label", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03/Label_ possession", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item01", true), mTargetObj, "Cardmix_popup_Select_gradesave");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item02", true), mTargetObj, "Cardmix_popup_Select_AdvCombi");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true), mTargetObj, "Cardmix_popup_Select_LuckCombi");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/btngrid/btn_buy", true), mTargetObj, "Cardmix_popup_btn_buy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/btngrid/btn_cancle", true), mTargetObj, "Cardmix_popup_btn_cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/btn_close", true), mTargetObj, "Cardmix_popup_btn_cancel");
        dicMenuList.Add ("highl1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl1card", false));
        dicMenuList.Add ("highl3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl3card", false));
        dicMenuList.Add ("normal1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal1card", false));
        dicMenuList.Add ("normal3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal3card", false));
        dicMenuList.Add ("kleague1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague1card", false));
        dicMenuList.Add ("kleague3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague3card", false));
        dicMenuList.Add ("highl1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl1card/cardbundle/card1", true));
        dicMenuList.Add ("highl3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("highl3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("highl3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl3card/cardgrid/cardbundle2/card1", true));
        dicMenuList.Add ("normal1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal1card/cardbundle/card1", true));
        dicMenuList.Add ("normal3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("normal3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("normal3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal3card/cardgrid/cardbundle2/card1", true));
        dicMenuList.Add ("kleague1_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague1card/cardbundle/card1", true));
        dicMenuList.Add ("kleague3_1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague3card/cardgrid/cardbundle0/card1", true));
        dicMenuList.Add ("kleague3_2card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague3card/cardgrid/cardbundle1/card1", true));
        dicMenuList.Add ("kleague3_3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague3card/cardgrid/cardbundle2/card1", true));
        dicMenuList ["highl1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["highl3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["highl3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["highl3_3card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["normal1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["normal3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["normal3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["normal3_3card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["kleague1_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["kleague3_1card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["kleague3_2card"].AddComponent<PlayerCardInfo> ();
        dicMenuList ["kleague3_3card"].AddComponent<PlayerCardInfo> ();

        dicMenuList.Add ("Buycardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup", false));
        dicMenuList.Add ("Buypopuphighl1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl1card", false));
        dicMenuList.Add ("Buypopuphighl3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl3card", false));
        dicMenuList.Add ("Buypopupnormal1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal1card", false));
        dicMenuList.Add ("Buypopupnormal3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal3card", false));
        dicMenuList.Add ("Buypopupkleague1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague1card", false));
        dicMenuList.Add ("Buypopupkleague3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague3card", false));

        dicMenuList.Add ("BuyFreecardopen_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup", false));
        dicMenuList.Add ("BuypopupFreehighl1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl1card", false));
        dicMenuList.Add ("BuypopupFreehighl3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl3card", false));
        dicMenuList.Add ("BuypopupFreenormal1card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal1card", false));
        dicMenuList.Add ("BuypopupFreenormal3card", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal3card", false));
       



        dicMenuList.Add ("TopCardLine", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/topcard", true));
        dicMenuList.Add ("BottomCardLine", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character/grid", true));
        dicMenuList.Add ("LPanel_cardmix", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix", false));
        dicMenuList.Add ("TopCardLineCardMix", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/topcard", true));
        dicMenuList.Add ("cardmix_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/cardmix_popup", false));
        dicMenuList.Add ("Lineup_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup", false));
        dicMenuList.Add ("popup_mixerror", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixerror", false));
        dicMenuList.Add ("popup_mixitem1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem1", false));
        dicMenuList.Add ("popup_mixitem2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem2", false));
        dicMenuList.Add ("popup_mixitem3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem3", false));
        dicMenuList.Add ("Team_popup_playerfull", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_playerfull", false));



        dicMenuList.Add ("popup_mixitemuse", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitemuse", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitemuse/btngrid/btn_ignore", true), mTargetObj, "popup_mixitemuseOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitemuse/btngrid/btn_cancle", true), mTargetObj, "popup_mixitemuseCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitemuse/btn_close", true), mTargetObj, "popup_mixitemuseCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_playerfull/btn_ok", true), mTargetObj, "TeamPopupPlayerFullClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem1/btngrid/btn_cancle", true), mTargetObj, "popup_mixitem1_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem1/btn_close", true), mTargetObj, "popup_mixitem1_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem1/btngrid/btn_ok", true), mTargetObj, "popup_mixitem1_btn_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem2/btngrid/btn_cancle", true), mTargetObj, "popup_mixitem2_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem2/btn_close", true), mTargetObj, "popup_mixitem2_Cancel");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem2/btngrid/btn_ok", true), mTargetObj, "popup_mixitem2_btn_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem3/btngrid/btn_cancle", true), mTargetObj, "popup_mixitem3_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem3/btn_close", true), mTargetObj, "popup_mixitem3_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixitem3/btngrid/btn_ok", true), mTargetObj, "popup_mixitem3_btn_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixerror/btn_ok", true), mTargetObj, "popup_mixerror_btn_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_mixerror/btn_close", true), mTargetObj, "popup_mixerror_btn_ok");
        dicMenuList.Add ("LPanel_coach", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach", false));
        dicMenuList.Add ("popup_recordinitialization", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_recordinitialization", false));
        dicMenuList.Add ("popup_teamnameedit", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit", false));
		dicMenuList.Add ("ar1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/nations/btn_left", true));
		dicMenuList.Add ("ar2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/nations/btn_right", true));
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/nations/btn_left", true), mTargetObj, "Btn_Fun_ScrollMoveLeft");
		mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/nations/btn_right", true), mTargetObj, "Btn_Fun_ScrollMoveRight");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/btn_teamnameedit", true), mTargetObj, "Btn_Fun_teamnameedit");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/btngrid/btn_buyedit", true), mTargetObj, "Btn_Fun_teamnameeditOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_teamnameeditCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_teamnameedit/btn_close", true), mTargetObj, "Btn_Fun_teamnameeditCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/btn_recordinitialization", true), mTargetObj, "Btn_Fun_recordinitialization");
        dicMenuList.Add ("btn_recordrefresh", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "btn_recordrefresh", true));
        dicMenuList.Add ("Coach_Label_refreshtime", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "Label_refreshtime", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/btn_recordrefresh", true), mTargetObj, "Btn_Fun_btn_recordrefresh");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_recordinitialization/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_recordinitializationCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_recordinitialization/btn_close", true), mTargetObj, "Btn_Fun_recordinitializationCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/popup_recordinitialization/btngrid/btn_initialization", true), mTargetObj, "Btn_Fun_recordinitializationOk");
        dicMenuList.Add ("Label_coachname", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "Label_name", true));
        dicMenuList.Add ("Label_nations", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "Label_nations", true));
        dicMenuList.Add ("Label_teamname", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "Label_teamname", true));
        dicMenuList.Add ("Label1_Allrecord", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_week/record_data/Label1", true));
        dicMenuList.Add ("Label2_WinRate", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_week/record_data/Label2", true));
        dicMenuList.Add ("Label3_TopPoint", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_week/record_data/Label3", true));
        dicMenuList.Add ("Label4_TopWinNum", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_week/record_data/Label4", true));
        dicMenuList.Add ("Label1_totalrecord", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_total/record_data/Label1", true));
        dicMenuList.Add ("Label2_totalWinRate", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_total/record_data/Label2", true));
        dicMenuList.Add ("Label3_totalTopPoint", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "record_total/record_data/Label3", true));
        dicMenuList.Add ("Coach_KakaoFace", mRscrcMan.FindChild (dicMenuList ["LPanel_coach"], "face", true));
        dicMenuList.Add ("LPanel_levelinfopopup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_levelinfopopup", false));
        dicMenuList.Add ("LPanel_playerstatpopup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup", false));
        dicMenuList.Add ("Panel_keeper", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper", false));
        dicMenuList.Add ("Panel_kicker", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker", false));
        //dicMenuList.Add ("Panel_keeper_face", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/face", false));
        //dicMenuList.Add ("Panel_kicker_face", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/face", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/btn_tutorial", true), mTargetObj, "btn_fun_PlayerinfoTutorial");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/btn_tutorial", true), mTargetObj, "btn_fun_PlayerinfoTutorial");


        dicMenuList.Add ("Panel_directionbar_Keeper", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/Panel_directionbar", false));
        dicMenuList.Add ("Panel_skillbar_Keeper", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/Panel_skillbar", true));
        dicMenuList.Add ("Panel_skillbar_Keeper_L", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/Panel_skillbar_L", true));

        dicMenuList.Add ("Panel_directionbar_Kicker", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/Panel_directionbar", true));
        dicMenuList.Add ("Panel_skillbar_Kicker", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/Panel_skillbar", true));

        dicMenuList.Add ("Label_mylevelpoint", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/Label_mylevelpoint", true));
        dicMenuList.Add ("mylevelpoint_alert", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/mylevelpoint_alert", true));



        /// <summary>
        /// PlayerStat
        /// </summary>
        /// 


        dicMenuList.Add ("Label_aft_direct", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/1_direct/Label_1", true));
        dicMenuList.Add ("Label_aft_accuracy", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/2_accuracy/Label_1", true));
        dicMenuList.Add ("Label_aft_firekick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/3_firekick/Label_1", true));
        dicMenuList.Add ("Label_aft_blazekick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/4_blazekick/Label_1", true));
        dicMenuList.Add ("Label_aft_addscore", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/5_addscore/Label_1", true));

        dicMenuList.Add ("Label_aft_volcanokick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_dataafter/4_1_volcanokick/Label_1", true));
        dicMenuList.Add ("Label_bf_direct", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/1_direct/Label_1", true));
        dicMenuList.Add ("Label_bf_accuracy", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/2_accuracy/Label_1", true));




        dicMenuList.Add ("Label_bf_firekick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/3_firekick/Label_1", true));
        dicMenuList.Add ("Label_bf_blazekick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/4_blazekick/Label_1", true));
        dicMenuList.Add ("Label_bf_addscore", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/5_addscore/Label_1", true));
        dicMenuList.Add ("Label_bf_volcanokick", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/stat_databefore/4_1_volcanokick/Label_1", true));





        dicMenuList.Add ("Label_lefttop", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/shoot_data/1_lefttop/Label_1", true));
        dicMenuList.Add ("Label_leftbottom", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/shoot_data/2_leftbottom/Label_1", true));
        dicMenuList.Add ("Label_righttop", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/shoot_data/3_righttop/Label_1", true));
        dicMenuList.Add ("Label_rightbottom", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/shoot_data/4_rightbottom/Label_1", true));
		dicMenuList.Add ("Label_Center", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/shoot_data/5_center/Label_1", true));
        dicMenuList.Add ("Kicker_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup", false));
        dicMenuList.Add ("Kicker_popup_condition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_condition", false));
        dicMenuList.Add ("Kicker_popup_editplayername", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Kicker_popup_editplayername"], "Input_editname", true), mTargetObj, "PlayerNameInit");

        dicMenuList.Add ("Kicker_popup_item", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item", false));
        dicMenuList.Add ("Kicker_popup_itemalert", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_cancle", true), mTargetObj, "KickerCostumeEquipCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/btn_close", true), mTargetObj, "KickerCostumeEquipCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choice", true), mTargetObj, "KickerCostumeEquip");
        dicMenuList.Add ("Kicker_popup_recharter", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter", false));
        dicMenuList.Add ("Kicker_popup_recharter_label", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_recharter/Label_step", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_recharter", true), mTargetObj, "Btn_fun_kickerRecharterOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/btn_close", true), mTargetObj, "Btn_fun_RecharterClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/btngrid/btn_cancle", true), mTargetObj, "Btn_fun_RecharterClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/btngrid/btn_recharter", true), mTargetObj, "Btn_fun_RecharterOK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1", true), mTargetObj, "Btn_fun_Recharterea1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10", true), mTargetObj, "Btn_fun_Recharterea10");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30", true), mTargetObj, "Btn_fun_Recharterea30");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50", true), mTargetObj, "Btn_fun_Recharterea50");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item01", true), mTargetObj, "Btn_fun_EquipShose1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item02", true), mTargetObj, "Btn_fun_EquipShose2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item03", true), mTargetObj, "Btn_fun_EquipShose3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item04", true), mTargetObj, "Btn_fun_EquipShose4");
        dicMenuList.Add ("Kicker_popup_training", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training", false));
        dicMenuList.Add ("Kicker_training_result", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/training_result", false));


        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();
        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();

        dicMenuList.Add ("Label_enchant2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/enchant2", true));
        dicMenuList.Add ("Label_enchant1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/enchant1", true));

        dicMenuList.Add ("Label_overall2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/enchant2/Label_overall", true));
        dicMenuList.Add ("Label_overall1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/enchant1/Label_overall", true));

        dicMenuList.Add ("Label_playername", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/Label_playername", true));
        dicMenuList.Add ("Label_playernumber", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/Label_playernumber", true));
        dicMenuList.Add ("gradetitle", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/gradetitle", true));

        dicMenuList.Add ("KickerPop_condition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/pop_maxcondition", false));
        dicMenuList.Add ("KeeperPop_condition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/pop_maxcondition", false));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_condition", true), mTargetObj, "Btn_Fun_Kicker_conditionUP");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_condition/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Kicker_conditionClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_condition/btngrid/btn_condition", true), mTargetObj, "Btn_Fun_Kicker_conditionok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_condition/btn_close", true), mTargetObj, "Btn_Fun_Kicker_conditionClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_dresseditem", true), mTargetObj, "Btn_Fun_Kicker_dresseditem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_buy", true), mTargetObj, "Btn_Fun_Kicker_dresseditemBuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Kicker_dresseditemClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_choice", true), mTargetObj, "Btn_Fun_Kicker_dresseditemOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/btn_close", true), mTargetObj, "Btn_Fun_Kicker_dresseditemClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_enchantplayer", true), mTargetObj, "Btn_Fun_Kicker_enchantPlayer");
        dicMenuList.Add ("Kicker_traininglevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_enchantplayer/Label_step", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_cancle", true), mTargetObj, "Btn_Fun_Kicker_enchantPlayerclose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btn_close", true), mTargetObj, "Btn_Fun_Kicker_enchantPlayerclose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_training", true), mTargetObj, "Btn_Fun_Kicker_enchantPlayerok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/btn_playeredit", true), mTargetObj, "Btn_Fun_Kicker_PlayerNameEdit");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Kicker_PlayerNameEditClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername/btngrid/btn_edit", true), mTargetObj, "Btn_Fun_Kicker_PlayerNameEditOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername/btn_close", true), mTargetObj, "Btn_Fun_Kicker_PlayerNameEditClose");

        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card0", true).AddComponent<PlayerCardInfo> ();
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card1", true).AddComponent<PlayerCardInfo> ();
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card2", true).AddComponent<PlayerCardInfo> ();
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).AddComponent<PlayerCardInfo> ();







        /// <summary>
        /// KeeperPlayerStat
        /// </summary>
        /// 

        dicMenuList.Add ("Label_aft_balance", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_dataafter/1_blance/Label_1", true));
        dicMenuList.Add ("Label_aft_flashjump", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_dataafter/2_flashjump/Label_1", true));
        dicMenuList.Add ("Label_aft_lightningjump", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_dataafter/3_lightningjump/Label_1", true));
        dicMenuList.Add ("Label_aft_bonusscore", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_dataafter/4_bonusscore/Label_1", true));
        dicMenuList.Add ("Label_bf_balance", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_databefore/1_blance/Label_1", true));
        dicMenuList.Add ("Label_bf_flashjump", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_databefore/2_flashjump/Label_1", true));
        dicMenuList.Add ("Label_bf_lightningjump", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_databefore/3_lightningjump/Label_1", true));
        dicMenuList.Add ("Label_bf_bonusscore", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/stat_databefore/4_bonusscore/Label_1", true));
        dicMenuList.Add ("Label_Kplefttop", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/shoot_data/1_lefttop/Label_1", true));
        dicMenuList.Add ("Label_Kpleftbottom", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/shoot_data/2_leftbottom/Label_1", true));
        dicMenuList.Add ("Label_Kprighttop", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/shoot_data/3_righttop/Label_1", true));
        dicMenuList.Add ("Label_Kprightbottom", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/shoot_data/4_rightbottom/Label_1", true));
        dicMenuList.Add ("Keeper_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup", false));
        dicMenuList.Add ("Keeper_popup_condition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_condition", false));
        dicMenuList.Add ("Keeper_popup_recharter", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter", false));
        dicMenuList.Add ("Keeper_popup_recharter_label", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_recharter/Label_step", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_recharter", true), mTargetObj, "Btn_fun_KeeperRecharterOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/btn_close", true), mTargetObj, "Btn_fun_KeeperRecharterClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/btngrid/btn_cancle", true), mTargetObj, "Btn_fun_KeeperRecharterClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/btngrid/btn_recharter", true), mTargetObj, "Btn_fun_KeeperRecharterOK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1", true), mTargetObj, "Btn_fun_KeeperRecharterea1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10", true), mTargetObj, "Btn_fun_KeeperRecharterea10");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30", true), mTargetObj, "Btn_fun_KeeperRecharterea30");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50", true), mTargetObj, "Btn_fun_KeeperRecharterea50");
        dicMenuList.Add ("Keeper_popup_editplayername", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Keeper_popup_editplayername"], "Input_editname", true), mTargetObj, "PlayerNameInit");
        dicMenuList.Add ("Keeper_popup_item", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item", false));
        dicMenuList.Add ("Keeper_popup_itemalert", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert", false));
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_close", true), mTargetObj, "KeeperCostumeEquipCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_cancle", true), mTargetObj, "KeeperCostumeEquipCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/btn_close", true), mTargetObj, "KeeperCostumeEquipCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choice", true), mTargetObj, "KeeperCostumeEquip");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item01", true), mTargetObj, "Btn_fun_EquipGlove1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item02", true), mTargetObj, "Btn_fun_EquipGlove2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item03", true), mTargetObj, "Btn_fun_EquipGlove3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item04", true), mTargetObj, "Btn_fun_EquipGlove4");
        dicMenuList.Add ("Keeper_popup_training", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training", false));
        dicMenuList.Add ("Keeper_training_result", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/training_result", false));


        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();
        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("card").gameObject.AddComponent<PlayerCardInfo> ();

        dicMenuList.Add ("Label_KpEnchant2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/enchant2", false));
        dicMenuList.Add ("Label_KpEnchant1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/enchant1", false));



        dicMenuList.Add ("Label_Kpoverall2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/enchant2/Label_overall", true));
        dicMenuList.Add ("Label_Kpoverall1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/enchant1/Label_overall", true));
        dicMenuList.Add ("Icon_KickerrCondition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_condition/condition/0_up", true));
        dicMenuList.Add ("Icon_KeeperCondition", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_condition/condition/0_up", true));
        dicMenuList.Add ("Label_Kpplayername", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/Label_playername", true));
        dicMenuList.Add ("Label_Kpplayernumber", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/Label_playernumber", true));
        dicMenuList.Add ("Kpgradetitle", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/gradetitle", true));
        dicMenuList.Add ("KeeperItemIcon", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/itemicon", false));
        dicMenuList.Add ("KeeperItemIcon_Label_noitem", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/itemicon/Label_noitem", false));
        dicMenuList.Add ("KickerItemIcon", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/itemicon", false));
        dicMenuList.Add ("KickerItemIcon_Label_noitem", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_short/itemicon/Label_noitem", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_condition", true), mTargetObj, "Btn_Fun_Keeper_conditionUP");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_condition/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Keeper_conditionUPClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_condition/btngrid/btn_condition", true), mTargetObj, "Btn_Fun_Keeper_conditionUPok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_condition/btn_close", true), mTargetObj, "Btn_Fun_Keeper_conditionUPClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_dresseditem", true), mTargetObj, "Btn_Fun_Keeper_dresseditem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Keeper_dresseditemClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/btn_close", true), mTargetObj, "Btn_Fun_Keeper_dresseditemClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_choice", true), mTargetObj, "Btn_Fun_Keeper_dresseditemchoice");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_buy", true), mTargetObj, "Btn_Fun_Keeper_dresseditemBuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_enchantplayer", true), mTargetObj, "Btn_Fun_Keeper_enchantPlayer");
        dicMenuList.Add ("Keeper_traininglevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_enchantplayer/Label_step", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_cancle", true), mTargetObj, "Btn_Fun_Keeper_enchantPlayerClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btn_close", true), mTargetObj, "Btn_Fun_Keeper_enchantPlayerClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_training", true), mTargetObj, "Btn_Fun_Keeper_enchantPlayerok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_short/btn_playeredit", true), mTargetObj, "Btn_Fun_Keeper_PlayerNameEdit");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Keeper_PlayerNameEditClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername/btngrid/btn_edit", true), mTargetObj, "Btn_Fun_Keeper_PlayerNameEditOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername/btn_close", true), mTargetObj, "Btn_Fun_Keeper_PlayerNameEditClose");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/btn_close", true), mTargetObj, "Btn_Fun_Panel_keeper_Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/btn_close", true), mTargetObj, "Btn_Fun_Panel_kicker_Close");
        dicMenuList.Add ("LPanel_uniform", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform", false));
        dicMenuList.Add ("Panel_teamback", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback", false));
        dicMenuList.Add ("LPanel_lineup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup", false));


        dicMenuList.Add ("starting_dis", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "starting_dis", true));


        dicMenuList.Add ("dec_bundle", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle", false));
        dicMenuList.Add ("4set_label", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_ability_4", false));
        dicMenuList.Add ("5set_label", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_ability_5", false));
        dicMenuList.Add ("6set_label", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_ability_6", false));

        dicMenuList.Add ("dec_effect", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect", false));
        dicMenuList.Add ("dec_4effect", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect/4set", false));
        dicMenuList.Add ("dec_5effect", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect/5set", false));
        dicMenuList.Add ("dec_6effect", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect/6set", false));

        dicMenuList.Add ("dec_nationsflag", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/nations_info/dec_nationsflag", true));
        dicMenuList.Add ("dec_Label_nation", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/nations_info/Label_nation", true));

        dicMenuList.Add ("dec_effect_loop", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect_loop", false));
        dicMenuList.Add ("dec_effect_loop_4set", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect_loop/4set", false));
        dicMenuList.Add ("dec_effect_loop_5set", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect_loop/5set", false));
        dicMenuList.Add ("dec_effect_loop_6set", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_effect_loop/6set", false));


        dicMenuList.Add ("Dec_nation_algeria", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/algeria", false));
        dicMenuList.Add ("Dec_nation_argentina", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/argentina", false));
        dicMenuList.Add ("Dec_nation_australia", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/australia", false));
        dicMenuList.Add ("Dec_nation_belgium", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/belgium", false));
        dicMenuList.Add ("Dec_nation_bosnia", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/bosnia", false));
        dicMenuList.Add ("Dec_nation_brazil", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/brazil", false));
        dicMenuList.Add ("Dec_nation_cameroon", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/cameroon", false));
        dicMenuList.Add ("Dec_nation_chile", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/chile", false));
        dicMenuList.Add ("Dec_nation_colombia", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/colombia", false));
        dicMenuList.Add ("Dec_nation_costarica", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/costarica", false));
        dicMenuList.Add ("Dec_nation_cotedivoire", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/cotedivoire", false));
        dicMenuList.Add ("Dec_nation_croatia", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/croatia", false));
        dicMenuList.Add ("Dec_nation_ecuador", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/ecuador", false));
        dicMenuList.Add ("Dec_nation_england", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/england", false));
        dicMenuList.Add ("Dec_nation_france", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/france", false));
        dicMenuList.Add ("Dec_nation_germany", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/germany", false));
        dicMenuList.Add ("Dec_nation_ghana", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/ghana", false));
        dicMenuList.Add ("Dec_nation_greece", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/greece", false));
        dicMenuList.Add ("Dec_nation_honduras", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/honduras", false));
        dicMenuList.Add ("Dec_nation_iran", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/iran", false));
        dicMenuList.Add ("Dec_nation_italy", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/italy", false));
        dicMenuList.Add ("Dec_nation_japan", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/japan", false));
        dicMenuList.Add ("Dec_nation_korea", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/korea", false));
        dicMenuList.Add ("Dec_nation_mexico", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/mexico", false));
        dicMenuList.Add ("Dec_nation_netherlands", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/netherlands", false));
        dicMenuList.Add ("Dec_nation_nigeria", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/nigeria", false));
        dicMenuList.Add ("Dec_nation_portugal", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/portugal", false));
        dicMenuList.Add ("Dec_nation_russia", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/russia", false));
        dicMenuList.Add ("Dec_nation_spain", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/spain", false));
        dicMenuList.Add ("Dec_nation_swiss", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/swiss", false));
        dicMenuList.Add ("Dec_nation_union", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/union", false));
        dicMenuList.Add ("Dec_nation_uruguay", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/uruguay", false));
        dicMenuList.Add ("Dec_nation_usa", mRscrcMan.FindChild (dicMenuList ["LPanel_lineup"], "dec_bundle/dec_nations/usa", false));



        dicMenuList.Add ("bundle_startinglevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_levelinfopopup", false));


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/btn_buy", true), mTargetObj, "Btn_Fun_UniformBuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/popup_buyuniform/Panel_btn/btn_buy", true), mTargetObj, "Btn_Fun_UniformBuyOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/popup_buyuniform/Panel_btn/btn_cancle", true), mTargetObj, "Btn_Fun_UniformBuyCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/popup_buyuniform/btn_close", true), mTargetObj, "Btn_Fun_UniformBuyCancel");
        dicMenuList.Add ("popup_buyuniform", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/popup_buyuniform", false));

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/popup_buyuniform/Label_intext", false);

        dicMenuList.Add ("scroll_top", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top", false));
        dicMenuList.Add ("scroll_pants", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants", false));
        dicMenuList.Add ("scroll_socks", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks", false));
        dicMenuList.Add ("scroll_topgk", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk", false));




        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_bottom/bundle_rightbtn/btn1_ready", true), mTargetObj, "Btn_Fun_MatchRequire");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/btn_levelinfo", true), mTargetObj, "Btn_Fun_levelinfopopup");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_levelinfopopup/btn_close", true), mTargetObj, "Btn_Fun_levelinfopopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/btn_close", true), mTargetObj, "Btn_Fun_LineupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox0_lineup", true), mTargetObj, "Btn_Fun_GotoLineup");
        dicMenuList.Add ("checkbox0_lineup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox0_lineup", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox1_buycard", true), mTargetObj, "Btn_Fun_BuyCard");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox2_enchantcard", true), mTargetObj, "Btn_Fun_CardMixOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox3_uniform", true), mTargetObj, "Btn_Fun_UniformEditOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox4_coach", true), mTargetObj, "Btn_Fun_DirectorModeOpen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/btn_tutorial", true), mTargetObj, "Btn_Fun_TutorialOpen");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/btn_book", true), mTargetObj, "Btn_Fun_PlayerBookOpen");




        dicMenuList.Add ("Menu_btn_tutorial", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/btn_tutorial", true));
        dicMenuList.Add ("Menu_btn_book", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/btn_book", true));

        /// <summary>
        /// sort By grade, Stat
        /// </summary>
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortgrade", true), mTargetObj, "Btn_Fun_SortByGrade");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortstat", true), mTargetObj, "Btn_Fun_SortByStat");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/btn_levelup", true), mTargetObj, "Btn_Fun_CostUp");
        dicMenuList.Add ("Label_nowlevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/btn_levelup/Label_nowlevel", true));
        dicMenuList.Add ("Label_maxlevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/btn_levelup/Label_maxlevel", false));

        dicMenuList.Add ("UI_teamPopup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup", false));
        dicMenuList.Add ("popup_levelup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup", false));
        dicMenuList.Add ("icon_cash", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/icon_cash", true));
        //dicMenuList.Add ("Label_beforelevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_beforelevel", true));
        dicMenuList.Add ("Label_countlevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_countlevel", true));

        //dicMenuList.Add ("Label_afterlevel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_afterlevel", true));
        //dicMenuList.Add ("Label_basicpoint", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_basicpoint", false));
        dicMenuList.Add ("Label_afterpoint", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_afterpoint", true));
        dicMenuList.Add ("Label_beforepoint", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_beforepoint", true));
        dicMenuList.Add ("div5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/division/div5", true));
        dicMenuList.Add ("Label_price", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/Label_price", true));


        dicMenuList.Add ("Team_buy_Combiitem", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/buy_item2", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/buy_item2/btn_close", true), mTargetObj, "CombiItemPopup_close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/buy_item2/btngrid/btn_buy", true), mTargetObj, "CombiItemPopup_buyok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/buy_item2/btngrid/btn_cancle", true), mTargetObj, "CombiItemPopup_close");





        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/btn_close", true), mTargetObj, "btn_Costup_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/btngrid/btn_cancle", true), mTargetObj, "btn_Costup_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "popup/popup_levelup/btngrid/btn_buy", true), mTargetObj, "btn_Costup_Ok");



        /// <summary>
        /// Buy Card
        /// </summary>

        dicMenuList.Add ("Team_NormalCardTicket1_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_normal/btn_1card/tiket/Label_num", true));
        dicMenuList.Add ("Team_NormalCardTicket3_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_normal/btn_3card/tiket/Label_num", true));
        dicMenuList.Add ("Team_HighCardTicket1_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_super/btn_1card/tiket/Label_num", true));
        dicMenuList.Add ("Team_HighCardTicket3_NumLabel", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_super/btn_3card/tiket/Label_num", true));
         

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_normal/btn_1card", true), mTargetObj, "Btn_Fun_BuyCardNormalRank1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_normal/btn_3card", false), mTargetObj, "Btn_Fun_BuyCardNormalRank3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_super/btn_1card", true), mTargetObj, "Btn_Fun_BuyCardHighRank1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_super/btn_3card", false), mTargetObj, "Btn_Fun_BuyCardHighRank3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_kleague/btn_1card", true), mTargetObj, "Btn_Fun_BuyCarKleague1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_card_kleague/btn_3card", false), mTargetObj, "Btn_Fun_BuyCarKleague3");
        /// <summary>
        /// Buy MessageBox
        /// </summary>
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyHighCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyHighCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyNorMarCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyNorMarCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyKleagueCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyKleagueCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyNorMarCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyKleagueCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyKleagueCard3cancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/highl3card/btn_close", true), mTargetObj, "Btn_Fun_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/normal3card/btn_close", true), mTargetObj, "Btn_Fun_BuyNorMarCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague1card/btn_close", true), mTargetObj, "Btn_Fun_BuyKleagueCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardbuy_popup/kleague3card/btn_close", true), mTargetObj, "Btn_Fun_BuyKleagueCard3cancel");




        ///
        /// freecard
        /// 
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyHighCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyHighCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal1card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyNorMarCard1OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal3card/btngrid/btn_buy", true), mTargetObj, "Btn_Fun_BuyNorMarCard3OK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_BuyHighCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/highl3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyHighCard3cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal1card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_BuyNorMarCard1cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardfree_popup/normal3card/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_BuyNorMarCard3cancel");
        ///
         
        /// <summary>
        /// Buy Card
        /// </summary>
        /// Buy Cancel
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCardHigh1Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_team_BuyCardHigh1_rebuy");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl1card/btn_close", true), mTargetObj, "Btn_Fun_BuyCardHigh1Close");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/highl3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCardHigh3Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCardNormal1Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_team_BuyNormal1_rebuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal1card/btn_close", true), mTargetObj, "Btn_Fun_BuyCardNormal1Close");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/normal3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCardNormal3Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague1card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCarKleague1Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague1card/btngrid/btn_rebuy", true), mTargetObj, "Btn_Fun_team_BuyCarKleague1_rebuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague1card/btn_close", true), mTargetObj, "Btn_Fun_BuyCarKleague1Close");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_buycard/cardopen_popup/kleague3card/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_BuyCarKleague3Close");
        /// <summary>
        /// Card Mix
        /// </summary>
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/btn_autosort", true), mTargetObj, "Btn_Fun_AutoSort");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/btn_mix", true), mTargetObj, "Btn_Fun_Mix");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/cardmix_popup/mix/btn_receive", true), mTargetObj, "Btun_Fun_MixOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/cardmix_popup/btn_close", true), mTargetObj, "Btun_Fun_MixOk");

        dicMenuList.Add ("gradesave", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn/Checkbox_gradesave", true));
        dicMenuList.Add ("mixluck", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn2/Checkbox_mixluck", true));
        dicMenuList.Add ("mixlucksuper", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn2/Checkbox_mixlucksuper", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn/Checkbox_gradesave", true), mTargetObj, "Btn_Fun_gradesave");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn2/Checkbox_mixluck", true), mTargetObj, "Btn_Fun_mixluck");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_btnoption/bundle_luckbtn2/Checkbox_mixlucksuper", true), mTargetObj, "Btn_Fun_mixlucksuper");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortgrade", true), mTargetObj, "Btn_SortbyGrade_CardMix");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortstat", true), mTargetObj, "Btn_SortbyStat_CardMix");

        Ag.LogString (" MenuManager Load Resources :: Uniform Edit ");
        /// <summary>
        /// Uniform Edit
        /// </summary>
        dicMenuList.Add ("btn_00top", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_00top", true));
        dicMenuList.Add ("btn_01pants", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_01pants", true));
        dicMenuList.Add ("btn_02socks", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_02socks", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_00top", true), mTargetObj, "Btn_Fun_Bundle_Top");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_01pants", true), mTargetObj, "Btn_Fun_Bundle_Pants");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_02socks", true), mTargetObj, "Btn_Fun_Bundle_Socks");
        dicMenuList.Add ("Top", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_00top", true));
        dicMenuList.Add ("Pants", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_01pants", true));
        dicMenuList.Add ("Socks", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_tap/btn_02socks", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color0", true), mTargetObj, "Btn_Fun_MainColor0");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color1", true), mTargetObj, "Btn_Fun_MainColor1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color2", true), mTargetObj, "Btn_Fun_MainColor2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color3", true), mTargetObj, "Btn_Fun_MainColor3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color4", true), mTargetObj, "Btn_Fun_MainColor4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color5", true), mTargetObj, "Btn_Fun_MainColor5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color6", true), mTargetObj, "Btn_Fun_MainColor6");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color7", true), mTargetObj, "Btn_Fun_MainColor7");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color8", true), mTargetObj, "Btn_Fun_MainColor8");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color9", true), mTargetObj, "Btn_Fun_MainColor9");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color10", true), mTargetObj, "Btn_Fun_MainColor10");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color11", true), mTargetObj, "Btn_Fun_MainColor11");
        dicMenuList.Add ("btn_Main_color0", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color0", true));
        dicMenuList.Add ("btn_Main_color1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color1", true));
        dicMenuList.Add ("btn_Main_color2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color2", true));
        dicMenuList.Add ("btn_Main_color3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color3", true));
        dicMenuList.Add ("btn_Main_color4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color4", true));
        dicMenuList.Add ("btn_Main_color5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color5", true));
        dicMenuList.Add ("btn_Main_color6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color6", true));
        dicMenuList.Add ("btn_Main_color7", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color7", true));
        dicMenuList.Add ("btn_Main_color8", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color8", true));
        dicMenuList.Add ("btn_Main_color9", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color9", true));
        dicMenuList.Add ("btn_Main_color10", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color10", true));
        dicMenuList.Add ("btn_Main_color11", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color1/btn_color11", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color0", true), mTargetObj, "Btn_Fun_LineColor0");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color1", true), mTargetObj, "Btn_Fun_LineColor1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color2", true), mTargetObj, "Btn_Fun_LineColor2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color3", true), mTargetObj, "Btn_Fun_LineColor3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color4", true), mTargetObj, "Btn_Fun_LineColor4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color5", true), mTargetObj, "Btn_Fun_LineColor5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color6", true), mTargetObj, "Btn_Fun_LineColor6");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color7", true), mTargetObj, "Btn_Fun_LineColor7");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color8", true), mTargetObj, "Btn_Fun_LineColor8");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color9", true), mTargetObj, "Btn_Fun_LineColor9");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color10", true), mTargetObj, "Btn_Fun_LineColor10");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color11", true), mTargetObj, "Btn_Fun_LineColor11");
        dicMenuList.Add ("Btn_Fun_LineColor0", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color0", true));
        dicMenuList.Add ("Btn_Fun_LineColor1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color1", true));
        dicMenuList.Add ("Btn_Fun_LineColor2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color2", true));
        dicMenuList.Add ("Btn_Fun_LineColor3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color3", true));
        dicMenuList.Add ("Btn_Fun_LineColor4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color4", true));
        dicMenuList.Add ("Btn_Fun_LineColor5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color5", true));
        dicMenuList.Add ("Btn_Fun_LineColor6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color6", true));
        dicMenuList.Add ("Btn_Fun_LineColor7", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color7", true));
        dicMenuList.Add ("Btn_Fun_LineColor8", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color8", true));
        dicMenuList.Add ("Btn_Fun_LineColor9", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color9", true));
        dicMenuList.Add ("Btn_Fun_LineColor10", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color10", true));
        dicMenuList.Add ("Btn_Fun_LineColor11", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_color2/btn_color11", true));


        dicMenuList.Add ("Label_score", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_itemstat/Label_score", true));
        dicMenuList.Add ("Label_good", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_itemstat/Label_good", true));
        dicMenuList.Add ("Label_great", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_itemstat/Label_great", true));


        Ag.LogString (" MenuManager Load Resources :: 1035   ");



        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top00", true), mTargetObj, "Btn_Fun_ShirsType1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top01", true), mTargetObj, "Btn_Fun_ShirsType2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top02", true), mTargetObj, "Btn_Fun_ShirsType3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top03", true), mTargetObj, "Btn_Fun_ShirsType4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top04", true), mTargetObj, "Btn_Fun_ShirsType5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top05", true), mTargetObj, "Btn_Fun_ShirsType6");
        dicMenuList.Add ("Btn_Fun_ShirsType1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top00", true));
        dicMenuList.Add ("Btn_Fun_ShirsType2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top01", true));
        dicMenuList.Add ("Btn_Fun_ShirsType3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top02", true));
        dicMenuList.Add ("Btn_Fun_ShirsType4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top03", true));
        dicMenuList.Add ("Btn_Fun_ShirsType5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top04", true));
        dicMenuList.Add ("Btn_Fun_ShirsType6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_top/grid/top05", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top00", true), mTargetObj, "Btn_Fun_ShirsType1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top01", true), mTargetObj, "Btn_Fun_ShirsType2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top02", true), mTargetObj, "Btn_Fun_ShirsType3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top03", true), mTargetObj, "Btn_Fun_ShirsType4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top04", true), mTargetObj, "Btn_Fun_ShirsType5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top05", true), mTargetObj, "Btn_Fun_ShirsType6");
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top00", true));
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top01", true));
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top02", true));
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top03", true));
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top04", true));
        dicMenuList.Add ("Btn_Fun_Kp_ShirsType6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_topgk/grid/top05", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants00", true), mTargetObj, "Btn_Fun_PantsType1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants01", true), mTargetObj, "Btn_Fun_PantsType2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants02", true), mTargetObj, "Btn_Fun_PantsType3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants03", true), mTargetObj, "Btn_Fun_PantsType4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants04", true), mTargetObj, "Btn_Fun_PantsType5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants05", true), mTargetObj, "Btn_Fun_PantsType6");
        dicMenuList.Add ("Btn_Fun_PantsType1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants00", true));
        dicMenuList.Add ("Btn_Fun_PantsType2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants01", true));
        dicMenuList.Add ("Btn_Fun_PantsType3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants02", true));
        dicMenuList.Add ("Btn_Fun_PantsType4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants03", true));
        dicMenuList.Add ("Btn_Fun_PantsType5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants04", true));
        dicMenuList.Add ("Btn_Fun_PantsType6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_pants/grid/pants05", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks00", true), mTargetObj, "Btn_Fun_SocksType1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks01", true), mTargetObj, "Btn_Fun_SocksType2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks02", true), mTargetObj, "Btn_Fun_SocksType3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks03", true), mTargetObj, "Btn_Fun_SocksType4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks04", true), mTargetObj, "Btn_Fun_SocksType5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks05", true), mTargetObj, "Btn_Fun_SocksType6");
        dicMenuList.Add ("Btn_Fun_SocksType1", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks00", true));
        dicMenuList.Add ("Btn_Fun_SocksType2", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks01", true));
        dicMenuList.Add ("Btn_Fun_SocksType3", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks02", true));
        dicMenuList.Add ("Btn_Fun_SocksType4", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks03", true));
        dicMenuList.Add ("Btn_Fun_SocksType5", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks04", true));
        dicMenuList.Add ("Btn_Fun_SocksType6", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_uniform/scroll_socks/grid/socks05", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/btn_buy", true), mTargetObj, "Btn_Fun_UniformBuy");
        dicMenuList.Add ("Btn_Fun_KickerMode", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_leftbtn_uniform/btn_00kicker", true));
        dicMenuList.Add ("Btn_Fun_KeeperMode", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_leftbtn_uniform/btn_01keeper", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_leftbtn_uniform/btn_00kicker", true), mTargetObj, "Btn_Fun_KickerMode");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_leftbtn_uniform/btn_01keeper", true), mTargetObj, "Btn_Fun_KeeperMode");


        //--------------------------------------------------------------- Set up

        //--------------------------------------------------------------- Kickoff

        //dicMenuList.Add ("Ui_kickoff", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_kickoff", false));
        dicMenuList ["Ui_kickoff"] = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_kickoff", false);

        dicMenuList.Add ("LPanel_friend2", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_friend", true));
        dicMenuList.Add ("LPanel_item", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item", true));
        dicMenuList.Add ("Panel_kickoffback", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback", true));
        dicMenuList.Add ("scroll_basicitem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem", false));
        dicMenuList.Add ("scroll_ceremony", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony", false));
        dicMenuList.Add ("scroll_message", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message", false));
        dicMenuList.Add ("btngrid_basicitem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red", false));
        dicMenuList.Add ("DrinkAuto", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_auto", false));
        dicMenuList.Add ("Label_nonauto", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_auto/Label_nonauto", false));
        dicMenuList.Add ("btngrid_ceremony", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony", false));
        dicMenuList.Add ("btngrid_message", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message", false));
        dicMenuList.Add ("Label_itemdescrit", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/Label_itemdescrit", false));
        dicMenuList.Add ("Label_itemtitle", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/Label_itemtitle", false));
        dicMenuList.Add ("item02_green", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/basicitem_discript/item02_green", false));
		dicMenuList.Add ("item00_blue", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/basicitem_discript/item00_blue", false));
		dicMenuList.Add ("item01_red", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/basicitem_discript/item01_red", false));
		dicMenuList.Add ("kakao_sync_kickoff", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_friend/kakao_sync", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["kakao_sync_kickoff"], "", false), mTargetObj, "kakao_sync_Lobby");
		dicMenuList.Add ("item03_scouter", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/basicitem_discript/Scout", false));
        dicMenuList.Add ("messageitem_discript", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/messageitem_discript", false));
        dicMenuList.Add ("messageitem_discript_Label_itemtitle", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/messageitem_discript/buy_message/Label_itemtitle", false));
        dicMenuList.Add ("messageitem_discript_message_now", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/messageitem_discript/buy_message/message_now", false));


        dicMenuList.Add ("message_custom1", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/messageitem_discript/message_greeting", false));
        dicMenuList.Add ("message_custom2", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/messageitem_discript/message_win", false));
        dicMenuList.Add ("item00_blue_eaLabel", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item00_blue/Label_account", true));
        dicMenuList.Add ("item01_red_eaLabel", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item01_red/Label_account", true));
        dicMenuList.Add ("item01_red_Choice", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item01_red", true));
        dicMenuList.Add ("item00_blue_Choice", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item00_blue", true));
        dicMenuList.Add ("item02_green_Choice", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item02_green", true));
		dicMenuList.Add ("item03_scouter_Choice", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item03_scouter", true));

		dicMenuList.Add ("item02_green_eaLabel", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item02_green/Label_account", true));
        dicMenuList.Add ("KickOffpopup", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup", false));
        dicMenuList.Add ("pointover", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/pointover", false));
        dicMenuList.Add ("popup_BuyItem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item", false));
        dicMenuList.Add ("buy_Drinkitem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem", false));


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["pointover"], "btn_ok", true), mTargetObj, "PointOverPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["pointover"], "btn_close", true), mTargetObj, "PointOverPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/btngrid/btn_buy", true), mTargetObj, "BuyDrinkOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/btngrid/btn_cancle", true), mTargetObj, "BuyDrinkCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/btn_close", true), mTargetObj, "BuyDrinkCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/check_blue_red/btn_1", true), mTargetObj, "DrinkEa1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/check_blue_red/btn_5", true), mTargetObj, "DrinkEa5");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_Drinkitem/check_blue_red/btn_10", true), mTargetObj, "DrinkEa10");

        dicMenuList.Add ("invite_versus", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/invite_versus", false));

        dicMenuList.Add ("versusinvite_success", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/versusinvite_success", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/versusinvite_success/btn_ok", true), mTargetObj, "VersusInviteOkPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/versusinvite_success/btn_close", true), mTargetObj, "VersusInviteOkPopupClose");



        dicMenuList.Add ("popup_levelpointalert", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_levelpointalert", false));
        dicMenuList.Add ("popup_playeralert", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_playeralert", false));
        dicMenuList.Add ("btn_auto_label_Price", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_auto/Label_price", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_playeralert/btn_close", true), mTargetObj, "popup_playeralertOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_playeralert/btngrid/btn_ok", true), mTargetObj, "popup_playeralertOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_levelpointalert/btngrid/btn_ok", true), mTargetObj, "popup_levelpointalertOK");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/popup_levelpointalert/btn_close", true), mTargetObj, "popup_levelpointalertOK");
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_choice", false);
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_choice", false), mTargetObj, "Btn_Fun_ApplyCeremony");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/invite_versus/btngrid/btn_send", true), mTargetObj, "Btn_Fun_versus_Send");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/invite_versus/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_versus_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/invite_versus/btn_close", true), mTargetObj, "Btn_Fun_versus_Cancel");
        dicMenuList.Add ("popup_BuyItem_Label_item", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/Label_item", true));
        dicMenuList.Add ("popup_BuyItem_Label_price", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/Label_price", true));
        dicMenuList.Add ("popup_BuyItem_Label_title", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/Label_title", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/btngrid/btn_buy", true), mTargetObj, "BuyItem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/btngrid/btn_cancle", true), mTargetObj, "BuyItemCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item/btn_close", true), mTargetObj, "BuyItemCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_piecebuy", true), mTargetObj, "BuyDrink");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_auto", true), mTargetObj, "Btn_Fun_DrinkAuto");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_buy", true), mTargetObj, "Btn_Fun_CeremonyBuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_buy", true), mTargetObj, "Btn_Fun_MessageBuy");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_edit", true), mTargetObj, "Btn_Fun_MessageEdit");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn0_team", true), mTargetObj, "Btn_Fun_GotoLineup");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/btn_close", true), mTargetObj, "Btn_Fun_MatchRequireclose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready", true), mTargetObj, "Btn_Fun_MatchSetUp");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready2", true), mTargetObj, "Btn_Fun_MatchSetUp");
        dicMenuList.Add ("Label_victorytime", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready/Label_victorytime", true));
        dicMenuList.Add ("Label_victorynum", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready/Label_victorynum", true));
        dicMenuList.Add ("Progress_victory", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready/Progress_victory", true));


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox0_item", true), mTargetObj, "Btn_Fun_DrinkItem");
        dicMenuList.Add ("DrinkItem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox0_item", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox1_ceremony", true), mTargetObj, "Btn_Fun_Ceremonyitem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox2_message", true), mTargetObj, "Btn_Fun_messageItem");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item00_blue", true), mTargetObj, "Btn_Fun_DrinkBlue");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item01_red", true), mTargetObj, "Btn_Fun_DrinkRed");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item03_scouter", false), mTargetObj, "Btn_Fun_Scouter");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_basicitem/grid/item02_green", true), mTargetObj, "Btn_Fun_DrinkGreen");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/message0", true), mTargetObj, "Btn_Fun_StartMessage");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/message1", true), mTargetObj, "Btn_Fun_EndMessage");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true), mTargetObj, "Btn_Fun_DefCer");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony1", true), mTargetObj, "Btn_Fun_PurCer1");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony2", true), mTargetObj, "Btn_Fun_PurCer2");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony3", true), mTargetObj, "Btn_Fun_PurCer3");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony4", true), mTargetObj, "Btn_Fun_PurCer4");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony5", true), mTargetObj, "Btn_Fun_PurCer5");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony6", true), mTargetObj, "Btn_Fun_PurCer6");




        //--------------------------------------------------------------- 팝업 스토어
        dicMenuList.Add ("Ui_popupstore", FindGameObject ("Ui_camera/Camera/Ui_popupstore", false));
        //--------------------------------------------------------------- 선수 영입 추가 상품
		dicMenuList.Add ("PopupScoutPlayers", FindMyChild (dicMenuList ["Ui_popupstore"],"store_0", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupScoutPlayers", "btn_item/Label_cost2");
		Gobjoff( "PopupScoutPlayers", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupScoutPlayers", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupScoutPlayers", "btn_item/Label_cost1");
		Gobjoff( "PopupScoutPlayers", "btn_item/Label_cost_before1");
		#endif

        //--------------------------------------------------------------- 할인 승리 글러브
		dicMenuList.Add ("PopupBuySoccerGloves", FindMyChild (dicMenuList ["Ui_popupstore"],"store_1", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/Label_cost2");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/Label_cost1");
		Gobjoff( "PopupBuySoccerGloves", "btn_item/Label_cost_before1");
		#endif


        //--------------------------------------------------------------- 할인 승리 축구화
        dicMenuList.Add ("PopupBuySoccerShoe", FindMyChild (dicMenuList ["Ui_popupstore"],"store_2", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/Label_cost2");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/Label_cost1");
		Gobjoff( "PopupBuySoccerShoe", "btn_item/Label_cost_before1");
		#endif


        //--------------------------------------------------------------- 할인 고급 행운권
		dicMenuList.Add ("PopupCardMixLuckTicket", FindMyChild (dicMenuList ["Ui_popupstore"],"store_3", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/Label_cost2");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/Label_cost1");
		Gobjoff( "PopupCardMixLuckTicket", "btn_item/Label_cost_before1");
		#endif

        //--------------------------------------------------------------- 할인 등급 보존권
		dicMenuList.Add ("PopupCardMixGradeReserveTicket", FindMyChild (dicMenuList ["Ui_popupstore"],"store_4", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/Label_cost2");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/Label_cost1");
		Gobjoff( "PopupCardMixGradeReserveTicket", "btn_item/Label_cost_before1");
		#endif

        //--------------------------------------------------------------- 할인, 캐쉬
		dicMenuList.Add ("PopupBuyEventCash", FindMyChild (dicMenuList ["Ui_popupstore"],"store_5", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupBuyEventCash", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupBuyEventCash", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupBuyEventCash", "btn_item/Label_cost2");
		Gobjoff( "PopupBuyEventCash", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupBuyEventCash", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupBuyEventCash", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupBuyEventCash", "btn_item/Label_cost1");
		Gobjoff( "PopupBuyEventCash", "btn_item/Label_cost_before1");
		#endif


        Ag.LogString (" MenuManager Load Resources :: 1322   ");

        //--------------------------------------------------------------- 할인, 하트 일
        //dicMenuList.Add ("PopupBuyPlayTimeaDay", FindMyChild (dicMenuList ["Ui_popupstore"],"store_4_0", false));
        //--------------------------------------------------------------- 할인, 하트 주
		dicMenuList.Add ("PopupBuyPlayTimeaWeek", FindMyChild (dicMenuList ["Ui_popupstore"],"store_6", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/Label_cost2");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/Label_cost1");
		Gobjoff( "PopupBuyPlayTimeaWeek", "btn_item/Label_cost_before1");
		#endif

        //--------------------------------------------------------------- 할인, 하트 월
		dicMenuList.Add ("PopupBuyPlayTimeaMonth", FindMyChild (dicMenuList ["Ui_popupstore"],"store_7", false));
		#if UNITY_ANDROID
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods1/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods2/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods3/Label_price2");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/Label_cost2");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/Label_cost_before2");
		#endif
		#if UNITY_IPHONE
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods1/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods2/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/goods/goods3/Label_price1");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/Label_cost1");
		Gobjoff( "PopupBuyPlayTimeaMonth", "btn_item/Label_cost_before1");
		#endif

        //--------------------------------------------------------------- 구매 확인 팝업
        dicMenuList.Add ("PopupBuy_pop", FindMyChild (dicMenuList ["Ui_popupstore"],"buy_pop", false));
        //--------------------------------------------------------------- 구매 포기시 재확인 팝업
        dicMenuList.Add ("PopupExit_pop", FindMyChild (dicMenuList ["Ui_popupstore"],"exit_pop", false));


        Ag.LogString (" MenuManager Load Resources :: 1366   ");

        
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupScoutPlayers"], "btn_close", true), mTargetObj, "PopupScoutPlayersClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerGloves"], "btn_close", true), mTargetObj, "PopupBuySoccerGlovesClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerShoe"], "btn_close", true), mTargetObj, "PopupBuySoccerShoesClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixLuckTicket"], "btn_close", true), mTargetObj, "PopupCardMixLuckTicketClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixGradeReserveTicket"], "btn_close", true), mTargetObj, "PopupCardMixGradeReserveTicketClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyEventCash"], "btn_close", true), mTargetObj, "PopupBuyEventCashClose");
       // mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaDay"], "btn_close", true), mTargetObj, "PopupBuyEventBuyPlayTimeaDayClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaWeek"], "btn_close", true), mTargetObj, "PopupBuyEventBuyPlayTimeaWeekClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaMonth"], "btn_close", true), mTargetObj, "PopupBuyEventBuyPlayTimeaMonthClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuy_pop"], "btn_close", true), mTargetObj, "buy_pop_Close");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupExit_pop"], "btn_close", true), mTargetObj, "Exit_pop_Close");

        Ag.LogString (" MenuManager Load Resources :: 1381   ");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupScoutPlayers"], "btngrid/btn_buy", true), mTargetObj, "PopupScoutPlayers");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerGloves"], "btngrid/btn_buy", true), mTargetObj, "PopupBuySoccerGloves");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerShoe"], "btngrid/btn_buy", true), mTargetObj, "PopupBuySoccerShoes");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixLuckTicket"], "btngrid/btn_buy", true), mTargetObj, "PopupCardMixLuckTicket");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixGradeReserveTicket"], "btngrid/btn_buy", true), mTargetObj, "PopupCardMixGradeReserveTicket");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyEventCash"], "btngrid/btn_buy", true), mTargetObj, "PopupBuyEventCash");
        //mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaDay"], "btngrid/btn_buy", true), mTargetObj, "PopupBuyEventBuyPlayTimeaDay");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaWeek"], "btngrid/btn_buy", true), mTargetObj, "PopupBuyEventBuyPlayTimeaWeek");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaMonth"], "btngrid/btn_buy", true), mTargetObj, "PopupBuyEventBuyPlayTimeaMonth");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuy_pop"], "btngrid/btn_buy", true), mTargetObj, "buy_pop_Decied_Ok");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupExit_pop"], "btngrid/btn_buy", true), mTargetObj, "Exit_pop_Ok");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupScoutPlayers"], "btngrid/btn_ok", true), mTargetObj, "PopupScoutPlayersClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerGloves"], "btngrid/btn_ok", true), mTargetObj, "PopupBuySoccerGlovesClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerShoe"], "btngrid/btn_ok", true), mTargetObj, "PopupBuySoccerShoesClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixLuckTicket"], "btngrid/btn_ok", true), mTargetObj, "PopupCardMixLuckTicketClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixGradeReserveTicket"], "btngrid/btn_ok", true), mTargetObj, "PopupCardMixGradeReserveTicketClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyEventCash"], "btngrid/btn_ok", true), mTargetObj, "PopupBuyEventCashClose");
        //mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaDay"], "btngrid/btn_ok", true), mTargetObj, "PopupBuyEventBuyPlayTimeaDayClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaWeek"], "btngrid/btn_ok", true), mTargetObj, "PopupBuyEventBuyPlayTimeaWeekClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaMonth"], "btngrid/btn_ok", true), mTargetObj, "PopupBuyEventBuyPlayTimeaMonthClose");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuy_pop"], "btngrid/btn_no", true), mTargetObj, "buy_pop_Close");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupExit_pop"], "btngrid/btn_no", true), mTargetObj, "Exit_pop_Close");

        Ag.LogString (" MenuManager Load Resources :: 1407   ");

        //--------------------------------------------------------------- ui POPUP

        dicMenuList.Add ("Ui_popup", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_popup", false));
        //dicMenuList.Add ("eventweb", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "eventweb_trash_", true));
        dicMenuList.Add ("alert", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert", false));
        dicMenuList.Add ("buy_alert", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "buy_alert", false));

        dicMenuList.Add ("popup_playerfull", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_playerfull", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_playerfull/btn_ok", true), mTargetObj, "PopupPlayerFullClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_playerfull/btn_close", true), mTargetObj, "PopupPlayerFullClose");

        dicMenuList.Add ("popup_MessageError", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_MessageError", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_MessageError/btn_ok", true), mTargetObj, "popup_MessageErrorClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_MessageError/btn_close", true), mTargetObj, "popup_MessageErrorClose");

        dicMenuList.Add ("popup_allCardGet", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_allCardGet", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_allCardGet/btn_ok", true), mTargetObj, "popup_allCardGetClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_allCardGet/btn_close", true), mTargetObj, "popup_allCardGetClose");



        dicMenuList.Add ("overlap_card", mRscrcMan.FindChild (dicMenuList ["Ui_popup"],"overlap_card", false));
        dicMenuList.Add ("overlap_Newcard", mRscrcMan.FindChild (dicMenuList ["Ui_popup"],"overlap_card/popup_overlap/card", true));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["overlap_card"], "popup_overlap/btn_close", true), mTargetObj, "Popup_OverLab_cardClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["overlap_card"], "popup_overlap/btngrid1/btn_cancle", true), mTargetObj, "Popup_OverLab_cardClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["overlap_card"], "popup_overlap/btngrid1/btn_rebuy", true), mTargetObj, "Popup_OverLab_Rebuy");


        dicMenuList.Add ("notification_kakaologin", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "notification_kakaologin", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["notification_kakaologin"], "btn_close", true), mTargetObj, "kakao_sync_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["notification_kakaologin"], "btn_no", true), mTargetObj, "kakao_sync_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["notification_kakaologin"], "btn_ok", true), mTargetObj, "kakao_sync_Ok");

        dicMenuList.Add ("alert_doublelogin", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_doublelogin", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["alert_doublelogin"], "btn_close", true), mTargetObj, "AlertDoubleLoginRestart");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["alert_doublelogin"], "btn_ok", true), mTargetObj, "AlertDoubleLoginRestart");


        dicMenuList.Add ("message_fail", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "message_fail", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_fail"], "btn_close", true), mTargetObj, "message_fail_close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_fail"], "btn_ok", true), mTargetObj, "message_fail_close");


        dicMenuList.Add ("message_cooltimefail", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "message_cooltimefail", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_cooltimefail"], "btn_close", true), mTargetObj, "message_cooltimefail_close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_cooltimefail"], "btn_ok", true), mTargetObj, "message_cooltimefail_close");

        dicMenuList.Add ("message_Deactivateuser", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "message_Deactivateuser", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_Deactivateuser"], "btn_close", true), mTargetObj, "message_Deactivateuser_close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["message_Deactivateuser"], "btn_ok", true), mTargetObj, "message_Deactivateuser_close");



        dicMenuList.Add ("ask_Exit", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "ask_exit", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["ask_Exit"], "btn_close", true), mTargetObj, "PopupExitClosePopup");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["ask_Exit"], "btngrid/btn_no", true), mTargetObj, "PopupExitClosePopup");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList["ask_Exit"], "btngrid/btn_ok", true), mTargetObj, "PopupExitOk");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert/btn_ok", true), mTargetObj, "alert_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "buy_alert/btn_ok", true), mTargetObj, "Buy_alert_ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "buy_alert/btn_close", true), mTargetObj, "Buy_alert_ok");


        dicMenuList.Add ("havenotpoint", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotpoint", false));
        dicMenuList.Add ("havenotcash", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotcash", false));
        dicMenuList.Add ("havenotplayball", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotplayball", false));
        dicMenuList.Add ("alert_someoneout", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_someoneout", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_someoneout/btn_close", true), mTargetObj, "EnemyLeftPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_someoneout/btn_ok", true), mTargetObj, "EnemyLeftPopupClose");


        Ag.LogString (" MenuManager Load Resources :: 1453   ");


        dicMenuList.Add ("alert_networkerror", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_networkerror", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_networkerror/btn_ok", true), mTargetObj, "NetworkReboot");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_networkerror/btn_close", true), mTargetObj, "NetworkReboot");

        dicMenuList.Add ("alert_restart", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_restart", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_restart/btn_ok", true), mTargetObj, "NetworkReboot");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_restart/btn_close", true), mTargetObj, "NetworkReboot");


        dicMenuList.Add ("popup_experienceAcard", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceAcard", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceAcard/btn_close", true), mTargetObj, "Cacel_Experience_Acard");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceAcard/btngrid/btn_later", true), mTargetObj, "Cacel_Experience_Acard");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceAcard/btngrid/btn_ok", true), mTargetObj, "StartSingleTryA");

        dicMenuList.Add ("popup_experienceScard", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceScard", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceScard/btn_close", true), mTargetObj, "Cacel_Experience_Scard");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceScard/btngrid/btn_later", true), mTargetObj, "Cacel_Experience_Scard");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_experienceScard/btngrid/btn_ok", true), mTargetObj, "StartSingleTryS");


        //dicMenuList.Add ("alert_someoneout", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "alert_someoneout", false));
        dicMenuList.Add ("popup_review", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_review",false));

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_review/btn_close", true), mTargetObj, "ReviewPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_review/btngrid/btn_later", true), mTargetObj, "ReviewPopupClose");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_review/btngrid/btn_review", true), mTargetObj, "ReviewPopupOK");

        Ag.LogString (" MenuManager Load Resources :: 1483   ");

        dicMenuList.Add ("popup_uniformalert", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_uniformalert", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_uniformalert/btngrid/btn_back", true), mTargetObj, "UniformIgnore");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_uniformalert/btngrid/btn_cancle", true), mTargetObj, "UniformCancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "popup_uniformalert/btn_close", true), mTargetObj, "UniformCancel");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotcash/btngrid/btn_shop", true), mTargetObj, "Goto_BuyCash");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotcash/btngrid/btn_cancle", true), mTargetObj, "Cancel_Buy_Cash");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotcash/btn_close", true), mTargetObj, "Cancel_Buy_Cash");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotpoint/btngrid/btn_shop", true), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotpoint/btngrid/btn_cancle", true), mTargetObj, "Cancel_Buy_Point");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotpoint/btn_close", true), mTargetObj, "Cancel_Buy_Point");

        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotplayball/btngrid/btn_shop", true), mTargetObj, "Goto_BuyPlayball");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotplayball/btngrid/btn_cancle", true), mTargetObj, "Cancel_Buy_Playball");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "havenotplayball/btn_close", true), mTargetObj, "Cancel_Buy_Playball");
        dicMenuList.Add ("rematch_accept", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "rematch_accept", false));
        dicMenuList.Add ("versus_accept", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_accept", false));
        dicMenuList.Add ("versus_refuse", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_refuse", false));

        dicMenuList.Add ("versus_inplay", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_inplay", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_inplay/btn_close", true), mTargetObj, "versus_inplay_Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_inplay/btngrid/btn_ok", true), mTargetObj, "versus_inplay_Close");

        dicMenuList.Add ("rematch_not", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "rematch_not", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "rematch_not/btn_close", true), mTargetObj, "SendVersusMessageNoreply_Close");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "rematch_not/btngrid/btn_rematch", true), mTargetObj, "RetrySendMatchingMessage");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "rematch_not/btngrid/btn_ok", true), mTargetObj, "SendVersusMessageNoreply_Close");


        dicMenuList.Add ("first_purchase", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "first_purchase", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "first_purchase/btn_close", true), mTargetObj, "first_purchase_popupClose");


        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_refuse/btngrid/btn_ok", true), mTargetObj, "Btn_Fun_Refuse_Ok");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_refuse/btn_close", true), mTargetObj, "Btn_Fun_Refuse_Ok");
        dicMenuList.Add ("invite_versusing", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "invite_versusing", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "invite_versusing/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_VersusWaiting_Cancel");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_accept/btngrid/btn_accept", true), mTargetObj, "Btn_Fun_Invite_Accept");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "versus_accept/btngrid/btn_cancle", true), mTargetObj, "Btn_Fun_Invite_Cancel");


        dicMenuList.Add ("LPanel_playerbook", mRscrcMan.FindChild (dicMenuList ["Ui_popup"], "LPanel_playerbook", false));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["LPanel_playerbook"], "btn_close", true), mTargetObj, "Btn_Fun_PlayerBookClose");

        Ag.LogString (" MenuManager Load Resources :: 1520   ");


        dicMenuList.Add ("Ui_setup", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_setup", false));
        dicMenuList.Add ("Panel_btn", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn", true));
        dicMenuList.Add ("Panel_setupdata", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata", true));
        dicMenuList.Add ("MY_Label_rank", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_userranking", true));
        dicMenuList.Add ("MY_Label_successivewin", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_totalgame", true));
        dicMenuList.Add ("MY_Label_tatalscore", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_userscore", true));
        dicMenuList.Add ("mydec", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_dec/mydec", true));



        //dicMenuList.Add ("MY_Label_userleague", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_userleague", true));
        dicMenuList.Add ("MY_Label_usernickname", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_username", true));
        dicMenuList.Add ("MY_Label_userteamname", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/Label_userteamname", true));
        dicMenuList.Add ("ENEMY_Label_rank", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_userranking", true));
        dicMenuList.Add ("ENEMY_flag", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/flag", true));
        dicMenuList.Add ("ENEMY_Label_successivewin", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_totalgame", true));
        dicMenuList.Add ("someonedec", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_dec/someonedec", false));


        dicMenuList.Add ("ENEMY_Label_tatalscore", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_userscore", true));
        //dicMenuList.Add ("ENEMY_Label_userleague", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_userleague", true));
        dicMenuList.Add ("ENEMY_Label_usernickname", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_username", true));
        dicMenuList.Add ("ENEMY_Label_userteamname", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/Label_userteamname", true));
        dicMenuList.Add ("Panel_matching", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_matching", false));
        //dicMenuList.Add ("loading", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "loading", false));
        dicMenuList.Add ("Start_Panel", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn", false));
        dicMenuList.Add ("btn_exit", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn/btn_exit", false));
        dicMenuList.Add ("btn_start", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn/btn_start", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn/btn_start", true), mTargetObj, "Btn_Fun_GameReady");
        dicMenuList.Add ("ready_me", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "ready_me", false));
        dicMenuList.Add ("ready_someone", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "ready_someone", false));
        dicMenuList.Add ("Panel_count", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_count", false));
        dicMenuList.Add ("Panel_countKickoff", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "img_gamestart", false));
        dicMenuList.Add ("Panel_firstaction", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction", false));
        dicMenuList.Add ("img_attack", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction/img_attack", false));
        dicMenuList.Add ("img_defense", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction/img_defense", false));
        dicMenuList.Add ("Panel_firstaction1", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction1", false));
        dicMenuList.Add ("img_attack1", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction1/img_attack", false));
        dicMenuList.Add ("img_defense1", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_firstaction1/img_defense", false));


        Ag.LogString (" MenuManager Load Resources :: 1563   ");

        dicMenuList.Add ("Panel_provokebox", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox", false));
        dicMenuList.Add ("provokebox_me", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox/provokebox_me", true));
        dicMenuList.Add ("provokebox_you", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox/provokebox_you", true));
        dicMenuList.Add ("data_someone", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone", false));
        dicMenuList.Add ("data_user", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user", true));
        dicMenuList.Add ("data_someone_face", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/face", true));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_btn/btn_exit", true), mTargetObj, "Btn_Fun_MatchCancleAndGoOut");

        Ag.LogString (" MenuManager Load Resources :: 1573   ");

        dicMenuList.Add ("Lobby_division", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_coach/division/div5", true));
        dicMenuList.Add ("Ui_team_division", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_coach/division/div5", true));
        dicMenuList.Add ("MY_Label_division", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_user/division/div5", true));
        dicMenuList.Add ("ENEMY_division", mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_setupdata/data_someone/division", true));

        //----------------------------------------------------------------------------

        dicMenuList.Add ("Ui_menututorial",mRscrcMan.FindGameObject("Ui_camera/Camera/Ui_menututorial",false));

        dicMenuList.Add ("menututor_cardinfo", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "cardinfo", false));
        dicMenuList.Add ("menututor_cardmix", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "cardmix", false));
        dicMenuList.Add ("menututor_cardmix2", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "cardmix2", false));
        dicMenuList.Add ("menututor_lineup", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "lineup", false));
        dicMenuList.Add ("menututor_lineup2", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "lineup2", false));
        dicMenuList.Add ("menututor_lineup3", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "lineup3", false));
        dicMenuList.Add ("menututor_lineup4", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "lineup4", false));
        dicMenuList.Add ("menututor_lobby", mRscrcMan.FindChild (dicMenuList ["Ui_menututorial"], "lobby", false));

        mLobbyPlayerNum = 1;
        // Animation Play..
        dicMenuList.Add ("Ball", mRscrcMan.FindGameObject ("Ball", true));
        mCard = new AmCard ();

        Ag.mySelf.SetCostumeToCard ();
        mCardInfo = Ag.mySelf.GetCardOrderOf (mLobbyPlayerNum++);
        //Debug.Log ("mCardInfo" + mCardInfo.WAS.info);
        //Debug.Log ("KickerCard" + mCardInfo.WAS.isKicker);
        mPoly = SetPolyGon (mCardInfo);
        mKicker = (GameObject)Instantiate (mPoly);
        mKicker.transform.position = new Vector3 (-0.5836174f, 0.01232325f, -0.433503f);
        mKicker.transform.localScale = new Vector3 (0.4107453f, 0.4107453f, 0.4107453f);
        mKicker.transform.eulerAngles = new Vector3 (0, 45.25688f, 0);
        DestroyObject (mKicker.transform.FindChild ("deleveryBall").gameObject.gameObject);
        KickerUniform = mKicker.transform.FindChild ("Clothes").transform.transform;
//        Debug.Log (mCard.arrCostumeInCard.Count+ "CostumeCount");
        if (mCard.arrCostumeInCard.Count > 0) {
            for (int i = 0; i < mCard.arrCostumeInCard.Count; i++) {
                Debug.Log ("CostumeNAme" + mCard.arrCostumeInCard[i].WAS.itemTypeId);
                PlayerCostume.instance.SetCostumeToPlayer (true, mKicker.transform.FindChild ("Clothes").gameObject, (mCard.arrCostumeInCard [i].WAS.itemTypeId));
            }

        } else {
            PlayerCostume.instance.SetCostumeToPlayer (true, mKicker, "KickerShoes01");
        }

        Ag.LogString (" MenuManager Load Resources :: Coroutine   ");

        StartCoroutine (CaptureImage ());
        mKicker.animation.Play ("Main_Kick");

        ReadmyTexture ();
        KickerUniformSetting ();

        BeforeSetUniform.Kick.Shirt.Texture = Ag.mySelf.arrUniform [0].Kick.Shirt.Texture;
        BeforeSetUniform.Keep.Shirt.Texture = Ag.mySelf.arrUniform [0].Keep.Shirt.Texture;
        BeforeSetUniform.Kick.Pants.Texture = Ag.mySelf.arrUniform [0].Kick.Pants.Texture;

        BeforeSetUniform.Keep.Pants.Texture = Ag.mySelf.arrUniform [0].Keep.Pants.Texture;
        BeforeSetUniform.Kick.Socks.Texture = Ag.mySelf.arrUniform [0].Kick.Socks.Texture;
        BeforeSetUniform.Keep.Socks.Texture = Ag.mySelf.arrUniform [0].Keep.Socks.Texture;

        TicketNumSetting ();

        Ag.LogString ("   MenuManager _ Load Resources  done  finish ");
        LobbyPlayAniFlag = true;


        Ag.LogString (" MenuManager Load Resources :: End    .....       ");
        Ag.LogString (" MenuManager Load Resources :: End    .....       ");
        Ag.LogString (" MenuManager Load Resources :: End    .....       ");
    }

    int mLobbyPlayerNum;
    GameObject mPoly;

    public void KickerUniformSetting ()
    {
        /*
        if (Ag.mySelf.arrUniform [0].Kick.Shirt.Texture == 0) {
            Ag.mVirServer.SetUniform (1, Ag.mySelf.arrUniform [0]);
            Ag.mySelf.arrUniform [0].mustUpdate = true;
            WasUniformUpdate unifUp = new WasUniformUpdate() { usr = Ag.mySelf };
        }
        */

        mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.mySelf.arrUniform [0].Kick.Shirt.Texture - 1];
        UNiformSetColorColor2 ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain);
        UNiformSetColorColor2 ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub);
        mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)subPants [Ag.mySelf.arrUniform [0].Kick.Pants.Texture - 1];
        UNiformSetColorColor2 ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Pants.ColMain);
        UNiformSetColorColor2 ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Pants.ColSub);
        mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)subSocks [Ag.mySelf.arrUniform [0].Kick.Socks.Texture - 1];
        UNiformSetColorColor2 ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Socks.ColMain);
        UNiformSetColorColor2 ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Socks.ColSub);
        mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;

    }
    /// <summary>
    /// Popups the store cash or point button off.
    /// </summary>
    public void PopupStoreCashOrPointBtn () {
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupScoutPlayers"], "btngrid/btn_buy", true), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerGloves"], "btngrid/btn_buy", false), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerShoe"], "btngrid/btn_buy", false), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixLuckTicket"], "btngrid/btn_buy", true), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixGradeReserveTicket"], "btngrid/btn_buy", true), mTargetObj, "Goto_BuyCash");
        //mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaDay"], "btngrid/btn_cash", true), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaWeek"], "btngrid/btn_buy", true), mTargetObj, "Goto_BuyCash");
		mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaMonth"], "btngrid/btn_buy", true), mTargetObj, "Goto_BuyCash");

        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupScoutPlayers"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerGloves"], "btngrid/btn_point", true), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuySoccerShoe"], "btngrid/btn_point", true), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixLuckTicket"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupCardMixGradeReserveTicket"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
        //mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaDay"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaWeek"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
        mRscrcMan.AddComponentUISendMessage (FindMyChild (dicMenuList["PopupBuyPlayTimeaMonth"], "btngrid/btn_point", false), mTargetObj, "Goto_BuyPoint");
    }



    public GameObject SetPolyGon (AmCard pPolyGonNum)
    {
        pPolyGonNum.WAS.ShowMySelf ();
        if (pPolyGonNum.WAS.isKicker) {
            return mPoly = (GameObject)Resources.Load ("Kicker/Prefab/" + pPolyGonNum.WAS.look);
        } else {
            return mPoly = (GameObject)Resources.Load ("Keeper/Prefab/" + pPolyGonNum.WAS.look);
        }
    }
	/// <summary>
	/// 팝업 스토어 오브젝트 켜고 끄기
	/// </summary>
	void Gobjoff (string OriginObj, string activefalse) {
		dicMenuList [OriginObj].gameObject.SetActive(false);
		dicMenuList [OriginObj].transform.FindChild(activefalse).gameObject.SetActive(false);
	}

    bool LobbyPlayAniFlag;
    void LobbyPlayAni ()
    {
        if (!mKicker.animation.IsPlaying ("Main_Kick") && dicMenuList ["Ball"].activeSelf) {
            DestroyObject (mKicker);
            mCardInfo = Ag.mySelf.GetCardOrderOf ((mLobbyPlayerNum++ % 5) + 1);
            mPoly = SetPolyGon (mCardInfo);
            mKicker = (GameObject)Instantiate (mPoly);
            dicMenuList ["Ball"].SetActive (false);
            dicMenuList ["Ball"].SetActive (true);
            mKicker.transform.position = new Vector3 (-0.5836174f, 0.01232325f, -0.433503f);
            mKicker.transform.localScale = new Vector3 (0.4107453f, 0.4107453f, 0.4107453f);
            mKicker.transform.eulerAngles = new Vector3 (0, 45.25688f, 0);
            DestroyObject (mKicker.transform.FindChild ("deleveryBall").gameObject.gameObject);
            KickerUniform = mKicker.transform.FindChild ("Clothes").transform.transform;
            mKicker.animation.Play ("Main_Kick");
            if (mCardInfo.arrCostumeInCard.Count > 0) {
                for (int i = 0; i < mCardInfo.arrCostumeInCard.Count; i++) {
                    Debug.Log ("CostumeNAme" + mCardInfo.arrCostumeInCard [i].WAS.itemTypeId);
                    PlayerCostume.instance.SetCostumeToPlayer (true, mKicker, (mCardInfo.arrCostumeInCard [i].WAS.itemTypeId));
                }
            } else {
                PlayerCostume.instance.SetCostumeToPlayer (true, mKicker, "KickerShoes01");
            }

            DestroyObject(mTex);
            if (LobbyPlayAniFlag)
            StartCoroutine (CaptureImage ());
            KickerUniformSetting ();
        }
    }
}
