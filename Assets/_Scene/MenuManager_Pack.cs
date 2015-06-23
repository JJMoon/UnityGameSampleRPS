using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{
    public void SendWasCardupdate ()
    {
        if (Ag.mySelf.ShouldUpdateCard ()) {
            //dicMenuList ["CenterCircle"].SetActive (true);
            WasCardUpdate aObj = new WasCardUpdate () { User = Ag.mySelf, // arrSendCard = Ag.mySelf.arrNewCard
            };
            aObj.messageAction = (int pInt) => {
                //dicMenuList ["CenterCircle"].SetActive (false);
                switch (pInt) { // 0:성공
                case 0:
                    SendWasCardInfo ();
                    Ag.LogString ("MenuManager_Pack  ::  CARD UpdateOK   Done  ");
                    break;
                case -1:
                case 4:
                    return;
                }
            };
        }
    }

    public void LobbyBuyCardInit ()
    {
        dicMenuList ["LobbyHigh1Card"].SetActive (false);
        dicMenuList ["Lobbynormal1Card"].SetActive (false);
        dicMenuList ["Lobbykleague1card"].SetActive (false);
    }

    public IEnumerator BuyCardOKBtnLock (float ptime, GameObject OpenCard)
    {
        OpenCard.transform.FindChild ("Label_content").gameObject.SetActive (false);
        OpenCard.transform.FindChild ("btngrid").gameObject.SetActive (false);
        OpenCard.transform.FindChild ("btn_close").gameObject.SetActive (false);
        yield return new WaitForSeconds (ptime);
        OpenCard.transform.FindChild ("Label_content").gameObject.SetActive (true);
        OpenCard.transform.FindChild ("btngrid").gameObject.SetActive (true);
        OpenCard.transform.FindChild ("btn_close").gameObject.SetActive (true);
    }

    int mBuyCardOption;

    /// <summary>
    /// 카드 구매, 조합 후 창 닫을 때 카드/아이템 가져오기
    /// </summary>
    /// 



    void ItemCardFetch (int option, string LeagueType, GameObject Obj)  // ItemInfoPurchaseCard
    {
        WasCardUniformCostume bObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
        bObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                return;
            }
        };

        WasItemInfo aObj = new WasItemInfo () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                if (option == 0 && LeagueType == "N") { 
                    if (CombiItemListEa ("TicketNormal") > 0) {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard2";
                    } else {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "1000";
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_gold";
                    }
                }
                if (option == 1 && LeagueType == "N") { 
                    if (CombiItemListEa ("TicketPremium") > 0) {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard2";
                    } else {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "27";
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                    }
                }
                if (option == 2 && LeagueType == "N") { 
                    Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "45";
                    Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                }

                KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
                dicMenuList ["item00_blue_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("BlueDrink").ToString ();
                dicMenuList ["item01_red_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("RedDrink").ToString ();
                dicMenuList ["item02_green_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("GreenDrink").ToString ();
                CostumeSetting ();
                MixItemSetting ();
                TicketNumSetting ();

                if (ItemTypeId == "EndMessage01")
                    Btn_Fun_EndMessage ();
                if (ItemTypeId == "StartMessage")
                    Btn_Fun_StartMessage ();
                if (ItemTypeId == "CeremonySkill01")
                    Btn_Fun_PurCer1 ();
                if (ItemTypeId == "CeremonySkill02")
                    Btn_Fun_PurCer2 ();
                if (ItemTypeId == "CeremonySkill03")
                    Btn_Fun_PurCer3 ();
                if (ItemTypeId == "CeremonySkill04")
                    Btn_Fun_PurCer4 ();
                if (ItemTypeId == "CeremonySkill05")
                    Btn_Fun_PurCer5 ();
                    /*
                if (ItemTypeId == "CeremonySkill06")
                    Btn_Fun_PurCer6 ();
                    */
                if (PhysicalBarCheck ("HeartSpeedUp")) {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_2").gameObject.SetActive (true);
                } else {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_2").gameObject.SetActive (false);
                }

                if (PhysicalBarCheck ("HeartLimitUp")) {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_3").gameObject.SetActive (true);
                } else {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_3").gameObject.SetActive (false);
                }
                return;

            }
        };
    }

    void ItemInfoPurchaseCard (int option, string LeagueType, GameObject Obj)  // ItemInfoPurchaseCard
    {
        WasItemInfo aObj = new WasItemInfo () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                if (option == 0 && LeagueType == "N") { 
                    if (CombiItemListEa ("TicketNormal") > 0) {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard2";
                    } else {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "1000";
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_gold";
                    }
                }
                if (option == 1 && LeagueType == "N") { 
                    if (CombiItemListEa ("TicketPremium") > 0) {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard2";
                    } else {
                        Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "27";
                        Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                    }
                }
                if (option == 2 && LeagueType == "N") { 
                    Obj.transform.FindChild ("btngrid/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "81";
                    Obj.transform.FindChild ("btngrid/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                }
                break;

            }
        };
    }

    GameObject mOpenCard;
    int mOption;

    void BuyCardOk (GameObject OpenCard, GameObject GObj, int option, int eanum, int buytype, int addFlagBuyType)
    {
        mOpenCard = OpenCard;
        mOption = option;
        if (option == 0 && LeagueType == "N") { 
            if (CombiItemListEa ("TicketNormal") > 0) {
                buytype = 2;
            } else
                buytype = 1;
            mCardGradeType = 0;
        }
        if (option == 1 && LeagueType == "N") { 

            if (CombiItemListEa ("TicketPremium") > 0) {
                buytype = 2;
                addFlagBuyType = 0;
            } else
                buytype = 0;
            mCardGradeType = 1;
                
        }
        if (LeagueType == "N" && option == 2)
            mCardGradeType = 2;

        mBuyCardOption = option;

        WasPurchaseCard aObj = new WasPurchaseCard () { User = Ag.mySelf, option = option, eaNum = eanum, buyType = buytype, leagueType = LeagueType, additionalBuyFlag = addFlagBuyType
        };
        aObj.messageAction = (int pInt) => {
            LobbyBuyCardInit ();

            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:


                OpenCard.SetActive (true);
                GObj.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
                StartCoroutine (InitCard (GObj));
                StartCoroutine (BuyCardOKBtnLock (2f, OpenCard));
                Ag.mySelf.arrCard.Add (Ag.mySelf.arrNewCard [0]);

                //GetCardInfo ();
                ItemInfoPurchaseCard (option, LeagueType, OpenCard);

                // 중복된 카드가 나왔을때
                //Debug.Log ("Ag.mySelf.arrCard[0].WAS.level  :: CardLevel" + Ag.mySelf.arrCard [0].WAS.level);
                if (Ag.mySelf.arrNewCard [0].WAS.level > 0) {
                    AfterPurchaseCardClosePopup ();
                    dicMenuList.SetActiveAll (false, new string [] {
                        "LobbyHigh1Card", "LobbyHigh3Card", "Lobbynormal1Card", "Lobbynormal3Card", "Lobbycardopen_popup",
                        "kleague1card", "kleague3card"
                    });
                    MenuCommonOpen ("Ui_popup", "overlap_card", true);
                    dicMenuList ["overlap_Newcard"].GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
                    StartCoroutine (InitCard (dicMenuList ["overlap_Newcard"]));
                    if (option == 0 && LeagueType == "N") { 
                        if (CombiItemListEa ("TicketNormal") > 0) {
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard1";
                        } else {
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "1000";
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_gold";
                        }
                    }
                    if (option == 1 && LeagueType == "N") { 
                        if (CombiItemListEa ("TicketPremium") > 0) {
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/Label_price").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "img_charactercard2";
                        } else {
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "27";
                            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                        }
                    }
                    if (option == 2 && LeagueType == "N") { 
                        dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/Label_price").GetComponent<UILabel> ().text = "81";
                        dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/btngrid1/btn_rebuy/icon").GetComponent<UISprite> ().spriteName = "icon_cash";
                    }

                    //Debug.Log ("Ag.mySelf.arrCard[0].WAS.isKicker  :: Iskicker?   " + Ag.mySelf.arrNewCard [0].WAS.isKicker);
                    dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data").gameObject.SetActive (Ag.mySelf.arrNewCard [0].WAS.isKicker ? false : true);
                    dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data").gameObject.SetActive (Ag.mySelf.arrNewCard [0].WAS.isKicker ? true : false);
                }

                break;
            case -1:
                if (LeagueType == "N" && option == 0) {
                    dicMenuList ["Lobbycardopen_popup"].SetActive (false);
                    dicMenuList ["cardopen_popup"].SetActive (false);
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                } else {
                    dicMenuList ["Lobbycardopen_popup"].SetActive (false);
                    dicMenuList ["cardopen_popup"].SetActive (false);
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                }
                break;
            case 5:
                MenuCommonOpen ("Ui_popup", "popup_allCardGet", true);
                break;
            }
        };
    }

    void WasUserinfo ()
    {
        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 0 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                RanKuser ();
                break;
            case -1:
            case 4:
                return;
            }
        };
    }

    void ContwinNumRewardRecieve ()
    {
        WasReceiveContWinItems aObj = new WasReceiveContWinItems () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0:
                WasUserinfo ();
                break;
            }

            aObj = null;
        };
    }
    //    void BuyCardOk (GameObject OpenCard, GameObject GObj, GameObject GObj1, GameObject GObj2, int option, int eanum, int buytype)
    //    {
    //        WasPurchaseCard aObj = new WasPurchaseCard () { User = Ag.mySelf, option = option, eaNum = eanum, buyType = buytype, leagueType = LeagueType
    //            //backNum = new List<int> () { 3 }, playerName = new List<string> (){ "Kim" }
    //        };
    //
    //        aObj.messageAction = (int pInt) => {
    //            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
    //            case 0:
    //                OpenCard.SetActive (true);
    //                StartCoroutine (BuyCardOKBtnLock (2f, OpenCard));
    //
    //
    //                Debug.Log (Ag.mySelf.arrNewCard [0].WAS.info);
    //                Debug.Log (Ag.mySelf.arrNewCard [1].WAS.info);
    //                Debug.Log (Ag.mySelf.arrNewCard [2].WAS.info);
    //
    //                GObj.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
    //                GObj1.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [1].WAS;
    //                GObj2.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [2].WAS;
    //
    //                Ag.mySelf.arrCard.Add (Ag.mySelf.arrNewCard [0]);
    //                Ag.mySelf.arrCard.Add (Ag.mySelf.arrNewCard [1]);
    //                Ag.mySelf.arrCard.Add (Ag.mySelf.arrNewCard [2]);
    //
    //                StartCoroutine (InitCard (GObj, GObj1, GObj2));
    //                SendWasCardupdate ();
    //
    //                break;
    //            case -1:
    //                if (LeagueType == "N" && option == 0) {
    //                    dicMenuList ["Lobbycardopen_popup"].SetActive (false);
    //                    dicMenuList ["cardopen_popup"].SetActive (false);
    //                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
    //                } else {
    //                    dicMenuList ["Lobbycardopen_popup"].SetActive (false);
    //                    dicMenuList ["cardopen_popup"].SetActive (false);
    //                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
    //                }
    //                break;
    //            }
    //        };
    //    }
    void InitFriendRank ()
    {
        if (KakaoFriends.Instance.appFriends.Count == 0)
            return;

        WasFriendRank aObj = new WasFriendRank () { User = Ag.mySelf };
        for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
            aObj.arrFriendIDs.Add (KakaoFriends.Instance.appFriends [i].userid);
        }
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0: 
                //RanKuser ();
                //FriendInit ();
                break;
            }
        };
    }

    void Btn_Fun_Mix ()
    {
        if (arrEachCard.Count < 3) {
            MenuCommonOpen ("popup_mixerror", "Lineup_popup");
            //dicMenuList ["popup_mixerror"].SetActive (true);
            return;
        }
        for (int i = 0; i < arrEachCard.Count; i++) {
            if (arrEachCard [i].GetComponent<PlayerCardInfo> ().mwas.grade == "A" || arrEachCard [i].GetComponent<PlayerCardInfo> ().mwas.grade == "S") {
                MenuCommonOpen ("Lineup_popup", "popup_mixitemuse", true);
                return;
            }
        }
        //int idx = myUser.arrCard.Count - 1;
        WasCardCombi aObj = new WasCardCombi () {
            User = Ag.mySelf, cardID1 = arrEachCard [0].GetComponent<PlayerCardInfo> ().mwas.ID, cardID2 = arrEachCard [1].GetComponent<PlayerCardInfo> ().mwas.ID, cardID3 = arrEachCard [2].GetComponent<PlayerCardInfo> ().mwas.ID // Combi

        };
        //Debug.Log ("MixLuck   " + MixLuck + "  Mix Super " + MixSuper);

        if (MixLuck)
            aObj.arrItemStr.Add ("CardCombiAdvt");
        if (MixSuper)
            aObj.arrItemStr.Add ("CardCombiAdvtHigh"); // 
        if (CheckGradesave)
            aObj.arrItemStr.Add ("CardCombiGrade");

        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                dicMenuList ["cardmix_popup"].SetActive (true);

                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card0", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [0].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card1", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [1].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card2", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [2].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card0", true).GetComponent<PlayerCardInfo> ().CardInit ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card1", true).GetComponent<PlayerCardInfo> ().CardInit ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card2", true).GetComponent<PlayerCardInfo> ().CardInit ();

                //Debug.Log ("INFO--------------------------------------------" + Ag.mySelf.arrNewCard [0].WAS.info);
                ItemInfo ();
                SendWasCardInfo ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
                StartCoroutine (Mixbtnclose (3f));
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().CardInit ();
                Ag.LogString (" result : Success ");
                Ag.LogString (" result : Success ");
                //Ag.mySelf.arrCard.Add(Ag.mySelf.arrNewCard[0]);
                break;
            case 1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;

            }
        };
    }

    string Pname;

    string ComBiitemName (string pCombiItemName)
    {

        if (pCombiItemName == "CardCombiGrade")
            Pname = "최고등급보존권";
        if (pCombiItemName == "CardCombiAdvtHigh")
            Pname = "고급행운권";
        if (pCombiItemName == "CardCombiAdvt")
            Pname = "영입행운권";
        return Pname;
    }

    void Cardmix_popup_btn_buy ()
    {
        MenuCommonOpen ("UI_teamPopup", "Team_buy_Combiitem", true);
        dicMenuList ["Team_buy_Combiitem"].transform.FindChild ("Label_item").GetComponent<UILabel> ().text = ComBiitemName (CombiItemName);
        dicMenuList ["Team_buy_Combiitem"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = ItemPrice (CombiItemName).ToString ();
        dicMenuList ["Team_buy_Combiitem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName (CombiItemName).ToString ();
        MenuCommonOpen ("Ui_Popup", "buy_item2", false);

    }

    void Btn_Fun_Mix2 ()
    {
        if (arrEachCard.Count < 3) {
            MenuCommonOpen ("popup_mixerror", "Lineup_popup");
            return;
        }

        WasCardCombi aObj = new WasCardCombi () {
            User = Ag.mySelf, 
            cardID1 = arrEachCard [0].GetComponent<PlayerCardInfo> ().mwas.ID, 
            cardID2 = arrEachCard [1].GetComponent<PlayerCardInfo> ().mwas.ID, 
            cardID3 = arrEachCard [2].GetComponent<PlayerCardInfo> ().mwas.ID // Combi
        };

        //Debug.Log ("MixLuck   " + MixLuck + "  Mix Super " + MixSuper);

        if (MixLuck)
            aObj.arrItemStr.Add ("CardCombiAdvt");
        if (MixSuper)
            aObj.arrItemStr.Add ("CardCombiAdvtHigh"); // 
        if (CheckGradesave)
            aObj.arrItemStr.Add ("CardCombiGrade");

        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                //Debug.Log ("INFO--------------------------------------------" + Ag.mySelf.arrNewCard [0].WAS.info);
                dicMenuList ["cardmix_popup"].SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card0", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [0].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card1", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [1].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card2", true).GetComponent<PlayerCardInfo> ().mwas = arrEachCard [2].GetComponent<PlayerCardInfo> ().mwas;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card0", true).GetComponent<PlayerCardInfo> ().CardInit ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card1", true).GetComponent<PlayerCardInfo> ().CardInit ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card/card2", true).GetComponent<PlayerCardInfo> ().CardInit ();

                ItemInfo ();
                SendWasCardInfo ();
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
                StartCoroutine (Mixbtnclose (3f));
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().CardInit ();
                Ag.LogString (" result : Success ");
                return;
            case 1: 
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }

        };

        WasCardUniformCostume bObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
        bObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Ag.LogString (" result : Success ");
                return;
            }
        };
    }

    public IEnumerator Mixbtnclose (float CloseBtn)
    {
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/btn_receive", false);
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "btn_close", false);
        yield return new WaitForSeconds (CloseBtn);
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/btn_receive", true);
        mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "btn_close", true);
    }

    /// <summary>
    /// CardINfo
    /// </summary>

    public void messageReceiveCardinfo ()
    {
        WasCardUniformCostume bObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
        bObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                SendWasCardupdate ();
                return;
            }
        };

        ItemInfo ();
        CostumeInfo ();
    }

    void SendWasCardInfo ()
    {
        WasCardUniformCostume bObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
        bObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrNewCard [0].WAS;
                mRscrcMan.FindChild (dicMenuList ["cardmix_popup"], "mix/card_new", true).GetComponent<PlayerCardInfo> ().CardInit ();
                /*
                AfterPurchaseCardClosePopup ();
                dicMenuList.SetActiveAll (false, new string [] {
                    "LobbyHigh1Card", "LobbyHigh3Card", "Lobbynormal1Card", "Lobbynormal3Card", "Lobbycardopen_popup",
                    "kleague1card", "kleague3card"
                });
                */
                Ag.LogString ("  Send Was Card Info   ");
                return;
            }
        };


    }

    /// <summary>
    /// ItemInfo
    /// </summary>
    /// 
    /// 

    public void ItemInfo ()
    {
        WasItemInfo aObj = new WasItemInfo () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
                dicMenuList ["item00_blue_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("BlueDrink").ToString ();
                dicMenuList ["item01_red_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("RedDrink").ToString ();
                dicMenuList ["item02_green_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("GreenDrink").ToString ();
                CostumeSetting ();
                MixItemSetting ();
                TicketNumSetting ();

                if (ItemTypeId == "EndMessage01")
                    Btn_Fun_EndMessage ();
                if (ItemTypeId == "StartMessage")
                    Btn_Fun_StartMessage ();
                if (ItemTypeId == "CeremonySkill01")
                    Btn_Fun_PurCer1 ();
                if (ItemTypeId == "CeremonySkill02")
                    Btn_Fun_PurCer2 ();
                if (ItemTypeId == "CeremonySkill03")
                    Btn_Fun_PurCer3 ();
                if (ItemTypeId == "CeremonySkill04")
                    Btn_Fun_PurCer4 ();
                if (ItemTypeId == "CeremonySkill05")
                    Btn_Fun_PurCer5 ();
                /*
                if (ItemTypeId == "CeremonySkill06")
                    Btn_Fun_PurCer6 ();
                    */
                if (PhysicalBarCheck ("HeartSpeedUp")) {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_2").gameObject.SetActive (true);
                } else {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_2").gameObject.SetActive (false);
                }
                
                if (PhysicalBarCheck ("HeartLimitUp")) {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_3").gameObject.SetActive (true);
                } else {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_3").gameObject.SetActive (false);
                }

                if (GloveTypeId == "HeartLimitUp" || GloveTypeId == "HeartSpeedUp")
                    Ag.mySelf.HeartSetMax (); // One time bonus
                return;
            }
        };
    }

    /// <summary>
    /// GameStart
    /// </summary>
    /// 
    /// 
    //int mFriendmode = 1; //friend Mode
    void SendWasGamestart (AmUser MyCard, AmUser Enemcard)
    {  // 1 : retry, 2 : no
        if (Ag.SingleTry > 0)
            return;

        int contwinEnFlag = Ag.mSingleMode ? 1 : 
            (Ag.NodeObj.EnemyUser.ContWinStarted ? 1 : 2);
        if (Enemcard.WAS.KkoID == "BOT")
            Ag.mFriendMode = 0;

        WasGameStart aObj = new WasGameStart () { User = Ag.mySelf, enemyID = Enemcard.WAS.KkoID, friendGame = Ag.mFriendMode,
            contWinMyFlag = (Ag.mySelf.ContWinCoolTimeRemainPercent () > 0) ? 1 : 2,
            contWinEnemFlag = contwinEnFlag,
            arrCardId = Ag.mySelf.GetMainCardIDs (), arrayEnemyId = Enemcard.GetMainCardIDs ()
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                Ag.LogString (" result : Success ");
                break;
            default:
                EnemyLeftflag = true;
                //Ag.NodeObj.LeaveMyself ();
                break;
            }
        };
    }

    /// <summary>
    /// BuyItem
    /// </summary>
    /// 


    void BuyItem ()
    {
        int Ea = 1;
        if (ItemTypeId == "BlueDrink" || ItemTypeId == "RedDrink" || ItemTypeId == "GreenDrink") {
            //Debug.Log ("DRinkEa" + DrinkEA);
            Ea = DrinkEA;
        }
        dicMenuList ["CenterCircle"].SetActive (true);
        MenuCommonOpen ("Ui_Popup", "buy_item", false);
        WasPurchaseItem aObj = new WasPurchaseItem () {
            User = Ag.mySelf,
            itemType = ItemType,
            itemTypeId = ItemTypeId,
            ea = Ea,
            //buyType = BuyType
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) {
            case 0:
                if (ItemTypeId == "EndMessage01") {
                    MessageItemInfo ();
                }
                else {
                    ItemInfo ();
                    LabelSetting (false, "", "", "");
                }

                Ag.LogIntenseWord (" result : Success ");
                break;
            case -1:

                if (ItemTypeId == "EndMessage01" || ItemTypeId == "CeremonySkill03" || ItemTypeId == "CeremonySkill04" || ItemTypeId == "CeremonySkill05")
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                else
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);



                dicMenuList.Add ("KickOffpopup", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup", false));
                dicMenuList.Add ("popup_BuyItem", mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "popup/buy_item", false));
                break;

            }
        };

        if (ItemType == "CEREMONY" || ItemType == "MESSAGE") {
            string[] arrayItemTypeid = new string[] {
                "EndMessage01",
                "CeremonySkill03",
                "CeremonySkill04",
                "CeremonySkill05"
            };
            for (int i = 0; i < arrayItemTypeid.Length; i++) {
                if (ItemTypeId == arrayItemTypeid [i]) {
                    //PopupAfterUserCash ();
                }
            }
        }

    }

    /// <summary>
    /// MessageEdit
    /// </summary>

    void Btn_fun_Ceremony_Apply (string ItemType, int applyid)
    {
        AmItem startMsg = Ag.mySelf.arrItem.GetMemberWithCond ((AmItem iObj) => {
            return iObj.WAS.itemTypeID == ItemType;  //"EndMessage"; //"StartMessage";  // 이렇게 조건을 지정하여 해당 아이템을 가져온다.
        });
        startMsg.WAS.applyID = applyid;

        startMsg.WAS.msg = " again DoitAgain''' ";
        WasItemUpdate aObj = new WasItemUpdate () { User = Ag.mySelf,
            itemObj = startMsg // 이렇게 업데이트 대상 아이템 <하나> 만 넣어준다.
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                CeremonySetNum ();
                Ag.LogString (" result : Success ");
                return;
            case -1:
                Ag.LogString (" result : Cash Not enough ");
                return;
            case 1:
                Ag.LogString (" result : Wrong Unit ");
                return;
            }
        };
    }

    void Btn_Fun_Kicker_dresseditemBuy ()
    {
        MenuCommonOpen ("Ui_Popup", "buy_item", false);

        dicMenuList ["CenterCircle"].SetActive (true);
        WasPurchaseCostume aObj = new WasPurchaseCostume () { User = Ag.mySelf, costumeName = Shoes }; //, buyType = 1 };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 
            case 0:
                //CostumeInfo ();
                Userinfo ();
                //dicMenuList ["alert"].SetActive (true);
                MenuCommonOpen ("Ui_Popup", "buy_item", false);
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
        };
    }

    void UniformBuy (string uniformTypeid)
    {
        WasPurchaseUniform aObj = new WasPurchaseUniform () {
            User = Ag.mySelf,
            uniformTypeID = uniformTypeid
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0:
                Uniform ();

                //dicMenuList ["alert"].SetActive (true);
                dicMenuList ["popup_buyuniform"].SetActive (false);
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));

                break;
            }
            //switch (pInt) { // // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
        };


    }

    void MessageItemInfo () {
        WasItemInfo aObj = new WasItemInfo () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                Btn_fun_EndmessageDef ();
                break;
            case -1:
                Ag.LogString (" result : Cash Not enough ");
                return;
            case 1:
                Ag.LogString (" result : Wrong Unit ");
                return;
            case 5:
            case 6:
                MenuCommonOpen ("Ui_popup", "popup_MessageError", true);
                break;
            }
        };
    }


    void Btn_fun_EndmessageDef () {

        AmItem startMsg = Ag.mySelf.arrItem.GetMemberWithCond ((AmItem iObj) => {
            return iObj.WAS.itemTypeID == ItemTypeId; //"StartMessage";  // 이렇게 조건을 지정하여 해당 아이템을 가져온다.
        });
        if (ItemTypeId == "EndMessage01") {
            startMsg.WAS.msg = dicMenuList ["message_custom2"].transform.FindChild ("Input_start").GetComponent<UIInput> ().text =  WWW.UnEscapeURL("%EC%88%98%EA%B3%A0%ED%95%98%EC%85%A8%EC%8A%B5%EB%8B%88%EB%8B%A4.");
            //dicMenuList ["message_custom2"].transform.FindChild ("Input_start").GetComponent<UIInput> ().activeColor = Color.black;
        }

        dicMenuList ["CenterCircle"].SetActive (true);
        WasItemUpdate aObj = new WasItemUpdate () { User = Ag.mySelf,
            itemObj = startMsg // 이렇게 업데이트 대상 아이템 <하나> 만 넣어준다.
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);

            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                ItemInfo();
                LabelSetting (false, "", "", "");
                Ag.LogString (" result : Success ");
                return;
            case -1:
                Ag.LogString (" result : Cash Not enough ");
                return;
            case 1:
                Ag.LogString (" result : Wrong Unit ");
                return;
            case 5:
            case 6:
                MenuCommonOpen ("Ui_popup", "popup_MessageError", true);
                break;
            }
        };

    }


    void Btn_Fun_MessageEdit ()
    {
        AmItem startMsg = Ag.mySelf.arrItem.GetMemberWithCond ((AmItem iObj) => {
            return iObj.WAS.itemTypeID == ItemTypeId; //"StartMessage";  // 이렇게 조건을 지정하여 해당 아이템을 가져온다.
        });
        if (ItemTypeId == "StartMessage") {
            startMsg.WAS.msg = dicMenuList ["message_custom1"].transform.FindChild ("Input_start").GetComponent<UIInput> ().text;
            //dicMenuList ["message_custom1"].transform.FindChild ("Input_start").GetComponent<UIInput> ().activeColor = Color.black;
        }
        if (ItemTypeId == "EndMessage01") {
            startMsg.WAS.msg = dicMenuList ["message_custom2"].transform.FindChild ("Input_start").GetComponent<UIInput> ().text;
            //dicMenuList ["message_custom2"].transform.FindChild ("Input_start").GetComponent<UIInput> ().activeColor = Color.black;
        }

        dicMenuList ["CenterCircle"].SetActive (true);
        WasItemUpdate aObj = new WasItemUpdate () { User = Ag.mySelf,
            itemObj = startMsg // 이렇게 업데이트 대상 아이템 <하나> 만 넣어준다.
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);

            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                //dicMenuList ["alert"].SetActive (true);
                ItemInfo ();
                Ag.LogString (" result : Success ");
                return;
            case -1:
                Ag.LogString (" result : Cash Not enough ");
                return;
            case 1:
                Ag.LogString (" result : Wrong Unit ");
                return;
            case 5:
            case 6:
                MenuCommonOpen ("Ui_popup", "popup_MessageError", true);
                break;
            }
        };

    }

    /// <summary>
    /// UniformUpdate
    /// </summary>

    void UNiformUpdate ()
    {
        //GetUniformsToUser ();
        WasUniformUpdate aObj = new WasUniformUpdate () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Ag.LogString (" result : Success ");
                break;
            }
        };
    }

    void InviteMenuOpen (string FriendId)
    {
        WasInvite aObj = new WasInvite () { User = Ag.mySelf, friendID = FriendId };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Ag.LogString (" result : Success ");
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/Label_man").GetComponent<UILabel> ().text = Ag.mySelf.InviteCount.ToString ();
                InviteRewardItemCheck ();
                return;
            }
        };
    }

    void InviteRewardItemCheck ()
    {

        if (Ag.mySelf.InviteCount <= 40) {
            if (Ag.mySelf.InviteCount >= 1) {
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check1").gameObject.SetActive (true);

            }
            if (Ag.mySelf.InviteCount >= 10) {
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check2").gameObject.SetActive (true);

            }
            if (Ag.mySelf.InviteCount >= 20) {
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check3").gameObject.SetActive (true);

            }
            if (Ag.mySelf.InviteCount >= 30) {
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check4").gameObject.SetActive (true);

            }
            if (Ag.mySelf.InviteCount >= 40)
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/grid_check/check5").gameObject.SetActive (true);


        }

    }

    void Review ()
    {
        WasReview aObj = new WasReview () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
        };
    }

    public void RankInfo ()
    {
        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 1 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                RanKuser ();
                FriendInit ();
                break;
            case -1:
            case 4:
                return;
            }
        };
    }

    public void Btn_Fun_UniformBuyOk ()
    {

        dicMenuList ["CenterCircle"].SetActive (true);
        WasPurchaseUniform aObj = new WasPurchaseUniform () {
            User = Ag.mySelf,
            uniformTypeID = uniformTypeid
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) {
            case 0:
                Uniform ();
                //dicMenuList ["alert"].SetActive (true);
                dicMenuList ["popup_buyuniform"].SetActive (false);
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
        };
    }

    public void Uniform ()
    {
        WasCardUniformCostume aObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 241 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                TextureSet (mIskicker);
                TextureBuyButtonClose (uniformTypeid);
                Ag.LogString (" result : Success ");
                return;
            }
        };
    }

    void Btn_Fun_GloveBuyOk ()
    {
        dicMenuList ["popup_buyglove"].SetActive (false);
        dicMenuList ["CenterCircle"].SetActive (true);
        if (GloveFree) {
            WasPurchaseItem bObj = new WasPurchaseItem () {
                User = Ag.mySelf,
                itemType = "GloveFreeTime",
                itemTypeId = GloveTypeId,
                ea = 1,
                //buyType = 0
            };
            bObj.messageAction = (int pInt) => {
                dicMenuList ["CenterCircle"].SetActive (false);
                switch (pInt) { // 0:성공
                case 0:
                    //ItemInfo ();
                    Userinfo ();
                    //PopupAfterUserCash();
                    break;
                case -1:
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                    //Debug.Log ("NO MONEY");
                    break;
                }
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
            return;
        } 
        if (GloveTypeId == "FuncHeartMax") {
            WasHeartFillMax aObj = new WasHeartFillMax () { User = Ag.mySelf };
            aObj.messageAction = (int pInt) => {
                dicMenuList ["CenterCircle"].SetActive (false);
                switch (pInt) { // 0:성공
                case 0:
                    Ag.mySelf.HeartSetMax ();
                    break;
                case -1:
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                    break;
                }
                aObj = null;
            };
            return;
        }

        WasPurchaseItem cObj = new WasPurchaseItem () {
            User = Ag.mySelf,
            itemType = "HEARTUPGRADE",
            itemTypeId = GloveTypeId, //"HeartLimitUp",
            ea = 1,
        };
        cObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                ItemInfo ();
//                
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotcash", true);
                break;
            }
        };
            

        

    }
}


