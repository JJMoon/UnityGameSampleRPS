using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{

    /// <summary>
    /// 할인 가격 입력
    /// </summary>

    void ShowSalePrice (UILabel BeforeLabel, string BeforePrice, UILabel AfterLabel,string AfterPrice) {
        BeforeLabel.text = BeforePrice;
        AfterLabel.text = AfterPrice;
    }
    /// <summary>
    /// 할인 가격 입력
    /// </summary>
    int EventPopupPrice;
    int AfterPrice (string Popupcode) {

        for (int i = 0 ; i < Ag.mySelf.arrPopupItem.Count; i++) {
            if (Ag.mySelf.arrPopupItem[i].WAS.popupCode == Popupcode) {
                EventPopupPrice = Ag.mySelf.arrPopupItem[i].WAS.price;
            }
        }
        return EventPopupPrice;
    }
    int EventPopupEa;
    int AfterEa (string Popupcode) {
        
        for (int i = 0 ; i < Ag.mySelf.arrPopupItem.Count; i++) {
            if (Ag.mySelf.arrPopupItem[i].WAS.popupCode == Popupcode) {
                EventPopupEa = Ag.mySelf.arrPopupItem[i].WAS.EA;
            }
        }
        return EventPopupEa;
    }


    /// <summary>
    /// Show Popups after combi Action.
    /// </summary>

    public string popupCode, mProductCode;
    int mPaymentPrice;

    AmServer mServer;

    void PopupAfterCombi ()
    {
        // show image 
        InitPopupList();
        int ranN = AgUtil.RandomInclude (1, 999);
        if (ranN % 5 == 1) {
            if (ranN % 2 == 0) {
                Productid = "com.appsgraphy.kvsskakao.popupstore04";
                mProductCode = "1000007367";
                mPaymentPrice = 20000;
                GetTranjectionkey();
                popupCode = "DiscCombi";
                #if UNITY_ANDROID
                GoogleIAB.consumeProduct (Productid);
                GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
                #endif
                MenuCommonOpen ("Ui_popupstore", "PopupCardMixLuckTicket", true);
                /*
                ShowSalePrice (dicMenuList["PopupCardMixLuckTicket"].transform.FindChild ("btn_item/Label_cost_before1").GetComponent<UILabel>(),
                               (ItemPrice("CardCombiAdvtHigh")*AfterEa ("DiscCombi")).ToString(),
                               dicMenuList["PopupCardMixLuckTicket"].transform.FindChild ("btn_item/Label_cost1").GetComponent<UILabel>(),
                               AfterPrice ("DiscCombi").ToString());

                dicMenuList["PopupCardMixLuckTicket"].transform.FindChild ("btn_item/Label_gold").GetComponent<UILabel>().text = "X "+ AfterEa ("DiscCombi").ToString();
                */
                                
                // General Show ...
            } else {
                Productid = "com.appsgraphy.kvsskakao.popupstore05";
                GetTranjectionkey();
                mProductCode = "1000007368";
                mPaymentPrice = 20000;
                popupCode = "DiscSave";


                #if UNITY_ANDROID

                GoogleIAB.consumeProduct (Productid);
                GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
                #endif
                MenuCommonOpen ("Ui_popupstore", "PopupCardMixGradeReserveTicket", true);

                /*
                ShowSalePrice (dicMenuList["PopupCardMixGradeReserveTicket"].transform.FindChild ("btn_item/Label_cost_before1").GetComponent<UILabel>(),
                               (ItemPrice("CardCombiGrade")* AfterEa ("DiscSave")).ToString(),
                               dicMenuList["PopupCardMixGradeReserveTicket"].transform.FindChild ("btn_item/Label_cost1").GetComponent<UILabel>(),
                               AfterPrice ("DiscSave").ToString());
                dicMenuList["PopupCardMixGradeReserveTicket"].transform.FindChild ("btn_item/Label_safe").GetComponent<UILabel>().text = "X "+ AfterEa ("DiscSave").ToString();
                */
            }
            // Show --- Grade retain ... 
        }
        // button Action ...
    }
    /// <summary>
    /// 이벤트 팝업으로 최종적으로 구매할때 부르는 함수
    /// </summary>
    void PopupPurchaseAction ()
    {
        #if UNITY_IPHONE
        AgStt.mIAP.TheUser = Ag.mySelf;
        AgStt.mIAP.PurchaseProduct (Productid);
        #endif
        
        #if UNITY_ANDROID

        AgStt.IntendedPause = true;
        if (Ag.CurStorePlfm == StorePlfm.GooglePlay) {
            GoogleIAB.purchaseProduct (Productid,GetUniqueKey(20));
        }
        if (Ag.CurStorePlfm == StorePlfm.Nstore) {
            IABEventListener.CallPaymentNRequest(mProductCode, mPaymentPrice, GetUniqueKey(20));
        }
        #endif
        /*
        WasPopupPurchase aObj = new WasPopupPurchase () { PopupCode = popupCode ,User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0 : 성공, -1 : 중복, -2 : 허용 불가 이름, 1: 존재하지 않는 사용자
            case 0:
                PopupAfterPurchaseAction ();
                break;
            case 1:
                MenuCommonOpen ("Ui_popup", "havenotcash", false);
                break;
            }
            aObj = null;
        };
        */
    }
    /// <summary>
    /// 구매하고 나서 성공했을때 아이템 정보 불러오기
    /// </summary>

    void PopupAfterPurchaseAction ()
    {
        Userinfo ();
        ItemInfo ();
        CostumeInfo();
    }

    void PopupStoreCashOrPoint (bool Cash, GameObject Gobj) {
        Gobj.transform.FindChild ("btngrid/btn_cash").gameObject.SetActive(false);
        Gobj.transform.FindChild ("btngrid/btn_point").gameObject.SetActive(false);
        if (Cash) Gobj.transform.FindChild ("btngrid/btn_cash").gameObject.SetActive(true);
        else Gobj.transform.FindChild ("btngrid/btn_point").gameObject.SetActive(true);
    }
    /// <summary>
    /// 경기 종료후에 부르는 이벤트 팝업
    /// </summary>
    void PopupAfterGameEnd ()
    {
        InitPopupList();
        mServer = new AmServer ();
        switch (mServer.KindOfPopup ("GameEnd", Ag.mySelf.myRank.WAS.lossNumWeek)) {
        case AmServer.PopupStore.DiscCardPurchase:
            Productid = "com.appsgraphy.kvsskakao.popupstore01";
            GetTranjectionkey();
            mProductCode = "1000007364";
            mPaymentPrice = 20000;
            popupCode = "DiscCardPurchase";
            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            MenuCommonOpen ("Ui_popupstore", "PopupScoutPlayers", true);
            /*
            ShowSalePrice (dicMenuList["PopupScoutPlayers"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           ItemPrice("Abnomal").ToString(),
                           dicMenuList["PopupScoutPlayers"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscCardPurchase").ToString());
            PopupStoreCashOrPoint (true, dicMenuList[ "PopupScoutPlayers"]);
            */

            break;
        case AmServer.PopupStore.DiscGloves:
            Productid = "com.appsgraphy.kvsskakao.popupstore02";
            GetTranjectionkey();
            popupCode = "DiscGlove";
            mProductCode = "1000007365";
            mPaymentPrice = 20000;

            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;

            #endif
            MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerGloves", true);/*
            ShowSalePrice (dicMenuList["PopupBuySoccerGloves"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           (ItemPrice ("KeeperGloves03")*AfterEa ("DiscGlove")).ToString(),
                           dicMenuList["PopupBuySoccerGloves"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscGlove").ToString());
            dicMenuList["PopupBuySoccerGloves"].transform.FindChild ("btn_item/Label_glove").GetComponent<UILabel>().text = "X " + AfterEa ("DiscGlove").ToString();
            PopupStoreCashOrPoint (false, dicMenuList[ "PopupBuySoccerGloves"]);
            */
            break;
        case AmServer.PopupStore.DiscShoes:
            Productid = "com.appsgraphy.kvsskakao.popupstore03";
            GetTranjectionkey();
            popupCode = "DiscShoes";
            mProductCode = "1000007366";
            mPaymentPrice = 20000;

            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerShoe", true);
            /*
            ShowSalePrice (dicMenuList["PopupBuySoccerShoe"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           (ItemPrice ("KickerShoes02")*AfterEa ("DiscShoes")).ToString(),
                           dicMenuList["PopupBuySoccerShoe"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscShoes").ToString());
            dicMenuList["PopupBuySoccerShoe"].transform.FindChild ("btn_item/Label_shoes").GetComponent<UILabel>().text = "X " + AfterEa ("DiscShoes").ToString();
            PopupStoreCashOrPoint (false, dicMenuList[ "PopupBuySoccerShoe"]);
            */
            break;
        case AmServer.PopupStore.DiscHeartDay:
            Productid = "";
            popupCode = "DiscHeartDay";
            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaDay", true);
            /*
            ShowSalePrice (dicMenuList["PopupBuyPlayTimeaDay"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           ItemPrice ("GloveFreeDay").ToString(),
                           dicMenuList["PopupBuyPlayTimeaDay"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscHeartDay").ToString());
            PopupStoreCashOrPoint (true, dicMenuList[ "PopupBuyPlayTimeaDay"]);
            */
            break;
        case AmServer.PopupStore.DiscHeartWeek:
            Productid = "com.appsgraphy.kvsskakao.popupstore07";
            GetTranjectionkey();
            popupCode = "DiscHeartWeek";
            mProductCode = "1000007370";
            mPaymentPrice = 30000;

            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaWeek", true);
            /*
            ShowSalePrice (dicMenuList["PopupBuyPlayTimeaWeek"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           ItemPrice ("GloveFreeWeek").ToString(),
                           dicMenuList["PopupBuyPlayTimeaWeek"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscHeartWeek").ToString());
            PopupStoreCashOrPoint (true, dicMenuList[ "PopupBuyPlayTimeaWeek"]);
            */
            break;
        case AmServer.PopupStore.DiscHeartMonth:
            Productid = "com.appsgraphy.kvsskakao.popupstore08";
            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            mProductCode = "1000007371";
            mPaymentPrice = 30000;
            GetTranjectionkey();
            popupCode = "DiscHeartMonth";
            MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaMonth", true);
            /*
            ShowSalePrice (dicMenuList["PopupBuyPlayTimeaMonth"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           ItemPrice ("GloveFreeMonth").ToString(),
                           dicMenuList["PopupBuyPlayTimeaMonth"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           AfterPrice ("DiscHeartMonth").ToString());
            PopupStoreCashOrPoint (true, dicMenuList[ "PopupBuyPlayTimeaMonth"]);
            */
            break;

        }
    }
    /// <summary>
    /// 캐쉬 소모후 나타내는 팝업
    /// </summary>

    void PopupAfterUserCash ()
    {
        InitPopupList();
        mServer = new AmServer ();
        switch (mServer.KindOfPopup ("AfterCashUse", Ag.mySelf.myRank.WAS.weekScore)) {
        case AmServer.PopupStore.DiscCash:
            Productid = "com.appsgraphy.kvsskakao.popupstore06";
            #if UNITY_ANDROID
            GoogleIAB.consumeProduct (Productid);
            GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
            #endif
            GetTranjectionkey();
            mProductCode = "1000007369";
            mPaymentPrice = 50000;
            MenuCommonOpen ("Ui_popupstore", "PopupBuyEventCash", true);
            /*
            ShowSalePrice (dicMenuList["PopupBuyEventCash"].transform.FindChild ("btn_item/Label_cost_before").GetComponent<UILabel>(),
                           "0",
                           dicMenuList["PopupBuyEventCash"].transform.FindChild ("btn_item/Label_cost").GetComponent<UILabel>(),
                           "0");
            dicMenuList["PopupBuyEventCash"].transform.FindChild ("btngrid/btn_cash").gameObject.SetActive(false);
            dicMenuList["PopupBuyEventCash"].transform.FindChild ("btngrid/btn_point").gameObject.SetActive(false);
            */
            break;
        }
    }
    /// <summary>
    /// 팝업창 초기화
    /// </summary>
    void InitPopupList () {


        #if UNITY_ANDROID
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore01_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore02_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore03_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore04_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore05_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore06_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore07_test");
        GoogleIAB.consumeProduct ("com.appsgraphy.kvsskakao.popupstore08_test");


        var skus = new string[] {
            "com.prime31.testproduct",
            "android.test.purchased",
            "com.prime31.managedproduct",
            "com.prime31.testsubscription"
        };
        
        GoogleIAB.queryInventory( skus );
        #endif






        dicMenuList["PopupScoutPlayers"].SetActive(false);
        dicMenuList["PopupBuySoccerGloves"].SetActive(false);
        dicMenuList["PopupBuySoccerShoe"].SetActive(false);
        dicMenuList["PopupCardMixLuckTicket"].SetActive(false);
        dicMenuList ["PopupCardMixGradeReserveTicket"].SetActive(false);
        dicMenuList["PopupBuyEventCash"].SetActive(false);
        dicMenuList["PopupBuyPlayTimeaWeek"].SetActive(false);
        dicMenuList["PopupBuyPlayTimeaMonth"].SetActive(false);
        dicMenuList["PopupBuy_pop"].SetActive(false);
        dicMenuList["PopupExit_pop"].SetActive(false);

    }

    /// <summary>
    /// 가격할인 이벤트 상품 팝업-선수영입
    /// </summary>
    void PopupScoutPlayers ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupScoutPlayers", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupScoutPlayersClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupScoutPlayers", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    /// <summary>
    /// 가격할인 이벤트 상품 팝업-축구화
    /// </summary>
    void PopupBuySoccerShoes ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerShoe", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuySoccerShoesClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerShoe", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }



    /// <summary>
    /// 가격할인 이벤트 상품 팝업-글러브
    /// </summary>
    void PopupBuySoccerGloves ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerGloves", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuySoccerGlovesClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuySoccerGloves", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    /// <summary>
    /// 가격할인 이벤트 상품 팝업-조합권(최고등급보존권)
    /// </summary>
    void PopupCardMixGradeReserveTicket ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupCardMixGradeReserveTicket", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupCardMixGradeReserveTicketClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupCardMixGradeReserveTicket", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

   

    /// <summary>
    /// 가격할인 이벤트 상품 팝업-조합권(고급행운권)
    /// </summary>
    void PopupCardMixLuckTicket ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupCardMixLuckTicket", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupCardMixLuckTicketClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupCardMixLuckTicket", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    

    /// <summary>
    /// 가격할인 이벤트 재화 팝업-캐쉬
    /// </summary>
    void PopupBuyEventCash ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyEventCash", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuyEventCashClose ()
    {
       // MenuCommonOpen ("Ui_popupstore", "PopupBuyEventCash", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    /// <summary>
    /// 가격할인 이벤트 재화 팝업-플레이볼 1 day
    /// </summary>
    void PopupBuyEventBuyPlayTimeaDay ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaDay", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuyEventBuyPlayTimeaDayClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaDay", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    /// <summary>
    /// 가격할인 이벤트 재화 팝업-플레이볼 7 days
    /// </summary>
    void PopupBuyEventBuyPlayTimeaWeek ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaWeek", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuyEventBuyPlayTimeaWeekClose ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaWeek", false);
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
    }

    /// <summary>
    /// 가격할인 이벤트 재화 팝업-플레이볼 30 days
    /// </summary>
    void PopupBuyEventBuyPlayTimeaMonth ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaMonth", false);
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", true);
    }

    void PopupBuyEventBuyPlayTimeaMonthClose ()
    {
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", true);
        //MenuCommonOpen ("Ui_popupstore", "PopupBuyPlayTimeaMonth", false);
    }
    /// <summary>
    /// 구매 결정
    /// </summary>
    void buy_pop_Decied_Ok ()
    {
        Popup_Allpopup_Close ();
        MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", false);
        PopupPurchaseAction ();
    }
   
    void buy_pop_Close ()
    {
        //MenuCommonOpen ("Ui_popupstore", "PopupBuy_pop", false);
        dicMenuList["PopupBuy_pop"].SetActive(false);
    }



    /// <summary>
    /// 구매포기 팝업 확인
    /// </summary>
    void Exit_pop_Close () {
        Popup_Allpopup_Close ();
        MenuCommonOpen ("Ui_popupstore", "PopupExit_pop", false);
    }

    void Exit_pop_Ok () {
        dicMenuList ["PopupExit_pop"].SetActive(false);
    }

    /// <summary>
    /// 모든 팝업 끄기
    /// </summary>

    void Popup_Allpopup_Close () {
        dicMenuList ["PopupBuy_pop"].SetActive(false);
        dicMenuList ["PopupBuyPlayTimeaMonth"].SetActive(false);
        dicMenuList ["PopupBuyPlayTimeaWeek"].SetActive(false);
        //dicMenuList ["PopupBuyPlayTimeaDay"].SetActive(false);
        dicMenuList ["PopupBuyEventCash"].SetActive(false);
        dicMenuList ["PopupCardMixLuckTicket"].SetActive(false);
        dicMenuList ["PopupCardMixGradeReserveTicket"].SetActive(false);

        dicMenuList ["PopupBuySoccerGloves"].SetActive(false);
        dicMenuList ["PopupBuySoccerShoe"].SetActive(false);
        dicMenuList ["PopupScoutPlayers"].SetActive(false);
    }





}
