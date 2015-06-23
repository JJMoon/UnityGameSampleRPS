using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{
    void Goto_BuyCash ()
    {
        Btn_Fun_EquipBoxShopCash ();
        MenuCommonOpen ("Ui_popup", "havenotcash", false);
    }

    void Cancel_Buy_Cash ()
    {
        MenuCommonOpen ("Ui_popup", "havenotcash", false);
    }

    void Goto_BuyPoint ()
    {
        MenuCommonOpen ("Ui_popup", "havenotpoint", false);
        Btn_Fun_EquipBoxShopGold ();
    }

    void Cancel_Buy_Point ()
    {
        MenuCommonOpen ("Ui_popup", "havenotpoint", false);
    }

    void Goto_BuyPlayball ()
    {
        Btn_Fun_EquipBoxShopGlove ();
        MenuCommonOpen ("Ui_popup", "havenotplayball", false);
    }

    void Cancel_Buy_Playball ()
    {

        MenuCommonOpen ("Ui_popup", "havenotplayball", false);
    }

    void alert_ok ()
    {
        dicMenuList ["alert"].SetActive (false);
        CommonPopupClose ();
    }

    void Buy_alert_ok ()
    {
        MenuCommonOpen ("Ui_popup", "buy_alert", false);
        AgStt.InAppPurchaseSuccess = false;
        //CommonPopupClose ();
    }

    void CommonPopupClose ()
    {
        dicMenuList ["UI_teamPopup"].SetActive (false);
        dicMenuList ["popup_levelup"].SetActive (false);
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_editplayername", false);
        dicMenuList ["popup_buyuniform"].SetActive (false);
    }

    void LobbyPopupPlayerFullClose ()
    {
        MenuCommonOpen ("Ui_Popup", "Lobby_popup_playerfull", false);
    }

    void TeamPopupPlayerFullClose ()
    {
        MenuCommonOpen ("Lineup_popup", "Team_popup_playerfull", false);
    }

    void PopupPlayerFullClose ()
    {
        MenuCommonOpen ("Ui_popup", "popup_playerfull", false);
    }

    void popup_MessageErrorClose () {
        MenuCommonOpen ("Ui_popup", "popup_MessageError", false);
    }

    void popup_allCardGetClose () {
        MenuCommonOpen ("Ui_popup", "popup_allCardGet", false);
    }

    //-----------------BuyCard ---------------
    //-----------------BuyCard ---------------
    //-----------------BuyCard ---------------
    //-----------------BuyCard ---------------
    int mCardBuyType;

    void Btn_Fun_highl1card ()
    {
        mLobbyBuyCardFlag = true;
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_Popup", "Lobby_popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketPremium") > 0) {
            MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl1card", true);
            dicMenuList ["LobbyBuyFreepopuphighl1card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
            mCardBuyType = 2;
            return;
        }
        mCardBuyType = 0;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl1card");
        dicMenuList ["LobbyBuypopuphighl1card"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = GetRealBuyPrice ("Abnomal").ToString ();

    }

    void Btn_Fun_highl3card ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketPremium3") > 0) {
            MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl3card", true);
            dicMenuList ["LobbyBuyFreepopuphighl1card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium3").ToString ();
            mCardBuyType = 2;
            return;
        }
        mCardBuyType = 0;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl3card");
    }

    void Btn_Fun_normal1card ()
    {
        mLobbyBuyCardFlag = true;
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketNormal") > 0) {
            MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal1card", true);
            dicMenuList ["LobbyBuyFreepopupnormal1card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
            mCardBuyType = 2;
            return;
        }
        mCardBuyType = 1;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal1card");
        dicMenuList ["LobbyBuypopupnormal1card"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = GetRealBuyPrice ("Normal").ToString ();
    }

    void Btn_Fun_normal3card ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketNormal3") > 0) {
            MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal3card", true);


            dicMenuList ["LobbyBuyFreepopupnormal3card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal3").ToString ();
            mCardBuyType = 2;
            return;
        }

        mCardBuyType = 1;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal3card");
    }

    void Btn_Fun_Kleague_card ()
    {
        mLobbyBuyCardFlag = true;
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        mCardBuyType = 0;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague1card");

        dicMenuList ["LobbyBuypopupkleague1card"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = GetRealBuyPrice ("Best").ToString ();
    }

    void Btn_Fun_Kleague_3card ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        mCardBuyType = 0;
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague3card");

    }

    void Btn_Fun_Lobby_BuyHighCard1OK ()
    {
        BuyCardInit ();
        LeagueType = "N";
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl1card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl1card", false);
        dicMenuList ["Lobbycardopen_popup"].SetActive (true);


        BuyCardOk (dicMenuList ["LobbyHigh1Card"], dicMenuList ["Lobbyhighl1_1card"], 1, 1, mCardBuyType, 0);

    }
    //    void Btn_Fun_Lobby_BuyHighCard3OK ()
    //    {
    //        BuyCardInit ();
    //        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl3card", false);
    //        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl3card", false);
    //        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
    //        LeagueType = "N";
    //        BuyCardOk (dicMenuList ["LobbyHigh3Card"], dicMenuList ["Lobbyhighl3_1card"], dicMenuList ["Lobbyhighl3_2card"], dicMenuList ["Lobbyhighl3_3card"], 1, 3, mCardBuyType);
    //    }
    void Btn_Fun_Lobby_BuyNorMarCard1OK ()
    {
        BuyCardInit ();
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal1card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal1card", false);

        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
        LeagueType = "N";
        BuyCardOk (dicMenuList ["Lobbynormal1Card"], dicMenuList ["Lobbynormal1_1card"], 0, 1, mCardBuyType, 0);

    }
    //    void Btn_Fun_Lobby_BuyNorMarCard3OK ()
    //    {
    //        BuyCardInit ();
    //        LeagueType = "N";
    //        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal3card", false);
    //        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal3card", false);
    //        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
    //        BuyCardOk (dicMenuList ["Lobbynormal3Card"], dicMenuList ["Lobbynormal3_1card"], dicMenuList ["Lobbynormal3_2card"], dicMenuList ["Lobbynormal3_3card"], 0, 3, mCardBuyType);
    //
    //    }
    void Lobby_popup_playerfullClose ()
    {
        MenuCommonOpen ("Ui_Popup", "Lobby_popup_playerfull", false);
    }

    void Btn_Fun_Lobby_BuyKleagueCard1OK ()
    {
        BuyCardInit ();
        LeagueType = "N";
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague1card", false);

        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
        BuyCardOk (dicMenuList ["Lobbykleague1card"], dicMenuList ["Lobbykleague1_1card"], 2, 1, mCardBuyType, 0);

    }

//    void Btn_Fun_Lobby_BuyKleagueCard3OK ()
//    {
//        BuyCardInit ();
//        LeagueType = "N";
//        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague3card", false);
//        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
//        BuyCardOk (dicMenuList ["Lobbykleague3card"], dicMenuList ["Lobbykleague3_1card"], dicMenuList ["Lobbykleague3_2card"], dicMenuList ["Lobbykleague3_3card"], 0, 3, mCardBuyType);
//
//    }

    void Btn_Fun_Lobby_BuyHighCard1cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl1card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl1card", false);
        ItemInfo ();
    }

    void Btn_Fun_Lobby_BuyHighCard3cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopuphighl3card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl3card", false);
        ItemInfo ();
    }

    void Btn_Fun_Lobby_BuyNorMarCard1cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal1card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal1card", false);
        ItemInfo ();
    }

    void Btn_Fun_Lobby_BuyNorMarCard3cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupnormal3card", false);
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal3card", false);
        ItemInfo ();

    }

    void Btn_Fun_Lobby_BuyKleagueCard1cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague1card", false);
    }

    void Btn_Fun_Lobby_BuyKleagueCard3cancel ()
    {
        MenuCommonOpen ("LobbyBuycardopen_popup", "LobbyBuypopupkleague3card", false);
    }
    //--------------------------------------------------------------------------------------ReBuyCard
    void Btn_Fun_HighCard_Rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        Btn_Fun_RebuyCardPopupClose ();
        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
        BuyCardOk (dicMenuList ["LobbyHigh1Card"], dicMenuList ["Lobbyhighl1_1card"], 1, 1, mCardBuyType, 1);
    }

    void Btn_Fun_NorMalCard_Rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        Btn_Fun_RebuyCardPopupClose ();
        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
        BuyCardOk (dicMenuList ["Lobbynormal1Card"], dicMenuList ["Lobbynormal1_1card"], 0, 1, mCardBuyType, 0);

    }

    void Btn_Fun_KleagueCard_Rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        Btn_Fun_RebuyCardPopupClose ();
        dicMenuList ["Lobbycardopen_popup"].SetActive (true);
        BuyCardOk (dicMenuList ["Lobbykleague1card"], dicMenuList ["Lobbykleague1_1card"], 2, 1, mCardBuyType, 1);
    }

    void Btn_Fun_team_BuyCardHigh1_rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        //Btn_Fun_BuyCardHigh1Close ();
        RebuyCardPopupClose ();
        dicMenuList ["cardopen_popup"].SetActive (true);

        BuyCardOk (dicMenuList ["highl1card"], dicMenuList ["highl1_1card"], 1, 1, mCardBuyType, 1);
    }

    void Btn_Fun_team_BuyNormal1_rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        //Btn_Fun_BuyCardNormal1Close ();
        RebuyCardPopupClose ();
        dicMenuList ["cardopen_popup"].SetActive (true);

        BuyCardOk (dicMenuList ["normal1card"], dicMenuList ["normal1_1card"], 0, 1, mCardBuyType, 0);
    }

    void Btn_Fun_team_BuyCarKleague1_rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        //Btn_Fun_BuyCarKleague1Close ();
        RebuyCardPopupClose ();
        dicMenuList ["cardopen_popup"].SetActive (true);
        BuyCardOk (dicMenuList ["kleague1card"], dicMenuList ["kleague1_1card"], 2, 1, mCardBuyType, 1);

    }

    void Btn_Fun_CardPopupClose ()
    {
        Debug.Log ("MenuManager_buyItem  Ag.mySelf.SingleTryDone" + Ag.mySelf.SingleTryDone);
        if (Ag.mySelf.SingleTryDone % 2 == 0 && IsReadyToStartGame && mBuyCardOption == 0 && Ag.mySelf.ShowSingleTry (false)) {
            // Show Popup ... Try Mode A ...
            MenuCommonOpen ("popup_experienceAcard", "Ui_popup", true);
        }
        if (mBuyCardOption == 1) {
            // Show Popup ... Try Mode A ...
            //PopupAfterUserCash();
        }

        ItemCardFetch (mOption, LeagueType, mOpenCard);
        //popup All Close
        Btn_Fun_RebuyCardPopupClose ();
    }

    void Btn_Fun_RebuyCardPopupClose ()
    {
        dicMenuList.SetActiveAll (false, new string [] {
            "LobbyHigh1Card", "LobbyHigh3Card", "Lobbynormal1Card", "Lobbynormal3Card", "Lobbycardopen_popup",
            "kleague1card", "kleague3card"
        });
    }

    /// <summary>
    /// A 카드 체험을 시작. 게임 씬으로 이동.
    /// </summary>
    void StartSingleTryA ()
    {
        Ag.SingleTry = 1; // 1 : A, 2 : S
        Ag.mySelf.SetCardsForSingleTry ();
        Btn_Fun_MatchSetUp ();
        //mGameMatchOk = true;
        dicMenuList ["btn_exit"].SetActive (false);
        MenuCommonOpen ("popup_experienceAcard", "Ui_popup", false);
        // load scene ..
    }

    void Cacel_Experience_Acard ()
    {
        Ag.mySelf.ConfirmSingleTry (false);
        MenuCommonOpen ("popup_experienceAcard", "Ui_popup", false);
    }

    ///3:1 트레이드에서 조합권 구매
    /// 
    /// 
    void CombiItemPopup_close ()
    {
        MenuCommonOpen ("UI_teamPopup", "Team_buy_Combiitem", false);
    }

    void CombiItemPopup_buyok ()
    {
        MenuCommonOpen ("UI_teamPopup", "Team_buy_Combiitem", false);
        WasPurchaseItem aObj = new WasPurchaseItem () {
            User = Ag.mySelf,
            itemType = "CombiAdvt",
            itemTypeId = CombiItemName, //"FuncBackNumEdit : ",
            ea = 1,
            //buyType = BuyType
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                ItemInfo ();
                //dicMenuList ["alert"].SetActive (true);
                MenuCommonOpen ("Ui_Popup", "buy_item", false);
                Ag.LogString (" result : Success ");
                if (CombiItemName == "CardCombiGrade" || CombiItemName == "CardCombiAdvtHigh") {
                    //PopupAfterUserCash ();
                }
                break;
            case -1:
                if (CombiItemName == "CardCombiGrade")
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                if (CombiItemName == "CardCombiAdvtHigh")
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                if (CombiItemName == "CardCombiAdvt")
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
            // Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
        };

    }
}
