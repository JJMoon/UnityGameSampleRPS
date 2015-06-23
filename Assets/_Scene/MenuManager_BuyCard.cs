using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{
    public int mCardType;

    void Btn_Fun_BuyCardHighRank1 ()
    {
        mCardType = 3;
        mLobbyBuyCardFlag = false;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketPremium") > 0) {
            MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl1card", true);
            dicMenuList ["BuypopupFreehighl1card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
            mCardBuyType = 2;
            return;

        }

        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopuphighl1card"].SetActive (true);
        mCardBuyType = 0;
        dicMenuList ["Buypopuphighl1card"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = GetRealBuyPrice ("Abnomal").ToString ();
        //AddUIPopUP ();
        //CardBuyOk (CardType.HighCard1)
    }

    void Btn_Fun_BuyCardHighRank3 ()
    {
        mCardType = 4;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketPremium3") > 0) {
            MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl3card", true);
            dicMenuList ["BuypopupFreehighl3card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium3").ToString ();
            mCardBuyType = 2;
            return;
        }

        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopuphighl3card"].SetActive (true);
        mCardBuyType = 0;
        //AddUIPopUP ();

    }
   
    void Btn_Fun_BuyCardNormalRank1 ()
    {
        mLobbyBuyCardFlag = false;
        mCardType = 1;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }

        if (CombiItemListEa ("TicketNormal") > 0) {
            MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreenormal1card", true);
            dicMenuList ["BuypopupFreenormal1card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
            mCardBuyType = 2;
            return;
        }

        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopupnormal1card"].SetActive (true);
        dicMenuList ["Buypopupnormal1card"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = GetRealBuyPrice ("Normal").ToString ();

        mCardBuyType = 1;
        //AddUIPopUP ();


    }

    void Btn_Fun_BuyCardNormalRank3 ()
    {
        mCardType = 2;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }
        if (CombiItemListEa ("TicketNormal3") > 0) {
            MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreenormal3card", true);
            dicMenuList ["BuypopupFreenormal3card"].transform.FindChild ("Label_coupon").GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal3").ToString ();
            mCardBuyType = 2;
            return;
        }

        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopupnormal3card"].SetActive (true);
        mCardBuyType = 0;
        //AddUIPopUP ();

    }

    void Btn_Fun_BuyCarKleague1 ()
    {
        mLobbyBuyCardFlag = false;
        mCardType = 5;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }


        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopupkleague1card"].SetActive (true);
        mCardBuyType = 0;
        //AddUIPopUP ();

        dicMenuList ["Buypopupkleague1card"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = GetRealBuyPrice ("Best").ToString ();
    }

    void Btn_Fun_BuyCarKleague3 ()
    {
        mCardType = 6;
        BuyCardInit ();
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }

        dicMenuList ["Buycardopen_popup"].SetActive (true);
        dicMenuList ["Buypopupkleague3card"].SetActive (true);
        mCardBuyType = 0;
        //AddUIPopUP ();

    }

    public IEnumerator InitCard (GameObject GObj)
    {
        yield return new WaitForSeconds (0.1f);
        GObj.GetComponent<PlayerCardInfo> ().CardInit ();
        ShowCardInformation ();
    }

    public IEnumerator InitCard (GameObject GObj, GameObject GObj1, GameObject GObj2)
    {
        yield return new WaitForSeconds (0.1f);
        GObj.GetComponent<PlayerCardInfo> ().CardInit ();
        GObj1.GetComponent<PlayerCardInfo> ().CardInit ();
        GObj2.GetComponent<PlayerCardInfo> ().CardInit ();
    }

    string LeagueType;

    void ExperienceAcard ()
    {
        if (Ag.mySelf.SingleTryDone % 2 == 0 && IsReadyToStartGame && Ag.mySelf.ShowSingleTry (false)) {
            // Show Popup ... Try Mode A ...
            MenuCommonOpen ("popup_experienceAcard", "Ui_popup", true);
        }

    }

    void RebuyCardPopupClose ()
    {
        dicMenuList ["cardopen_popup"].SetActive (false);
        dicMenuList ["highl1card"].SetActive (false);
        dicMenuList ["normal1card"].SetActive (false);
        dicMenuList ["kleague1card"].SetActive (false);
    }

    void Btn_Fun_BuyCardHigh1Close ()
    {
        //PopupAfterUserCash ();
        //ExperienceAcard ();

        //ItemInfo ();
        AfterPurchaseCardClosePopup ();
        //CardInfo ();
        ItemCardFetch (mOption, LeagueType, mOpenCard);
    }

    void Btn_Fun_BuyCardHigh3Close ()
    {
        dicMenuList ["highl3card"].SetActive (false);
        dicMenuList ["cardopen_popup"].SetActive (false);
        //ItemInfo ();
        ItemCardFetch (mOption, LeagueType, mOpenCard);
    }

    /// <summary>
    /// 구매후 나오는 팝업을 끔
    /// </summary>
    void AfterPurchaseCardClosePopup ()
    {
        dicMenuList ["cardopen_popup"].SetActive (false);
        dicMenuList ["highl1card"].SetActive (false);
        dicMenuList ["normal1card"].SetActive (false);
        dicMenuList ["kleague1card"].SetActive (false);
    }

    void Btn_Fun_BuyCardNormal1Close ()
    {
        ExperienceAcard ();
        //ItemInfo ();
        AfterPurchaseCardClosePopup ();
        //CardInfo ();
        ItemCardFetch (mOption, LeagueType, mOpenCard);
    }

    void Btn_Fun_BuyCardNormal3Close ()
    {
        dicMenuList ["normal3card"].SetActive (false);
        dicMenuList ["cardopen_popup"].SetActive (false);
        //ItemInfo ();
        ItemCardFetch (mOption, LeagueType, mOpenCard);
    }

    void Btn_Fun_BuyCarKleague1Close ()
    {
        //PopupAfterUserCash ();
        //ExperienceAcard ();
        AfterPurchaseCardClosePopup ();
        //CardInfo ();
        ItemCardFetch (mOption, LeagueType, mOpenCard);
    }

    void Btn_Fun_BuyCarKleague3Close ()
    {
        dicMenuList ["kleague3card"].SetActive (false);
        dicMenuList ["cardopen_popup"].SetActive (false);
        ItemCardFetch (mOption, LeagueType, mOpenCard);

    }

    void BuyCardInit ()
    {
        dicMenuList ["highl1card"].SetActive (false);
        dicMenuList ["highl3card"].SetActive (false);
        dicMenuList ["normal1card"].SetActive (false);
        dicMenuList ["normal3card"].SetActive (false);
        dicMenuList ["kleague1card"].SetActive (false);
        dicMenuList ["kleague3card"].SetActive (false);

        dicMenuList ["Buypopuphighl1card"].SetActive (false);
        dicMenuList ["Buypopuphighl3card"].SetActive (false);
        dicMenuList ["Buypopupnormal1card"].SetActive (false);
        dicMenuList ["Buypopupnormal3card"].SetActive (false);
        dicMenuList ["Buypopupkleague1card"].SetActive (false);
        dicMenuList ["Buypopupkleague3card"].SetActive (false);

    }

    GameObject UIpopup;

    void AddUIPopUP ()
    {

        UIpopup = (GameObject)Instantiate (Resources.Load ("prefab_General/Ui_popup"), new Vector3 (0, 0, -6.386f), Quaternion.identity);
        UIpopup.transform.parent = mRscrcMan.FindGameObject ("Ui_camera/Camera", true).gameObject.transform;
        UIpopup.transform.localPosition = new Vector3 (0, 0, -588.9795f);
        UIpopup.GetComponent<UIAnchor> ().panelContainer = mRscrcMan.FindGameObject ("Ui_camera", true).GetComponent<UIPanel> ();
        mRscrcMan.FindChild (UIpopup, "base", true);
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (UIpopup, "base/btngrid/btn_ok", true), mTargetObj, "CardBuyOk");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (UIpopup, "base/btngrid/btn_no", true), mTargetObj, "CardBuyCanCel");
        UIpopup.transform.localScale = new Vector3 (1, 1, 1);

    }

    void CardBuyOk ()
    {
        dicMenuList ["cardopen_popup"].SetActive (true);

    }

    void CardBuyCanCel ()
    {
        DestroyObject (UIpopup);
    }

    void Btn_Fun_BuyCardOk ()
    {
        dicMenuList ["cardopen_popup"].SetActive (false);

    }

    void Btn_Fun_BuyHighCard1OK ()
    {

        dicMenuList ["cardopen_popup"].SetActive (true);
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopuphighl1card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl1card", false);

        LeagueType = "N";
        BuyCardOk (dicMenuList ["highl1card"], dicMenuList ["highl1_1card"], 1, 1, mCardBuyType, 0);


    }

//    void Btn_Fun_BuyHighCard3OK ()
//    {
//        dicMenuList ["cardopen_popup"].SetActive (true);
//        dicMenuList ["Buycardopen_popup"].SetActive (false);
//        dicMenuList ["Buypopuphighl3card"].SetActive (false);
//        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl3card", false);
//
//        LeagueType = "N";
//        BuyCardOk (dicMenuList ["highl3card"], dicMenuList ["highl3_1card"], dicMenuList ["highl3_2card"], dicMenuList ["highl3_3card"], 1, 3, mCardBuyType);
//
//    }

    void Btn_Fun_BuyNorMarCard1OK ()
    {
        dicMenuList ["cardopen_popup"].SetActive (true);
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupnormal1card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl1card", false);
        LeagueType = "N";
        BuyCardOk (dicMenuList ["normal1card"], dicMenuList ["normal1_1card"], 0, 1, mCardBuyType, 0);

    }

//    void Btn_Fun_BuyNorMarCard3OK ()
//    {
//        dicMenuList ["cardopen_popup"].SetActive (true);
//        dicMenuList ["Buycardopen_popup"].SetActive (false);
//        dicMenuList ["Buypopupnormal3card"].SetActive (false);
//        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl1card", false);
//        LeagueType = "N";
//        BuyCardOk (dicMenuList ["normal3card"], dicMenuList ["normal3_1card"], dicMenuList ["normal3_2card"], dicMenuList ["normal3_3card"], 0, 3, mCardBuyType);
//    }

    void Btn_Fun_BuyKleagueCard1OK ()
    {
        dicMenuList ["cardopen_popup"].SetActive (true);
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupkleague1card"].SetActive (false);

        LeagueType = "N";
        BuyCardOk (dicMenuList ["kleague1card"], dicMenuList ["kleague1_1card"], 2, 1, mCardBuyType, 0);

    }

//    void Btn_Fun_BuyKleagueCard3OK ()
//    {
//        dicMenuList ["cardopen_popup"].SetActive (true);
//        dicMenuList ["Buycardopen_popup"].SetActive (false);
//        dicMenuList ["Buypopupkleague1card"].SetActive (false);
//        LeagueType = "N";
//        BuyCardOk (dicMenuList ["kleague3card"], dicMenuList ["kleague3_1card"], dicMenuList ["kleague3_2card"], dicMenuList ["kleague3_3card"], 1, 3, mCardBuyType);
//    }

    void Btn_Fun_BuyHighCard1cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopuphighl1card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl1card", false);

    }

    void Btn_Fun_BuyHighCard3cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopuphighl3card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreehighl3card", false);
    }

    void Btn_Fun_BuyNorMarCard1cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupnormal1card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreenormal1card", false);
    }

    void Btn_Fun_BuyNorMarCard3cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupnormal3card"].SetActive (false);
        MenuCommonOpen ("BuyFreecardopen_popup", "BuypopupFreenormal3card", false);
    }

    void Btn_Fun_BuyKleagueCard1cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupkleague1card"].SetActive (false);
    }

    void Btn_Fun_BuyKleagueCard3cancel ()
    {
        dicMenuList ["Buycardopen_popup"].SetActive (false);
        dicMenuList ["Buypopupkleague1card"].SetActive (false);
    }



    /// <summary>
    /// 카드 정보 보여주기
    /// </summary>
    void ShowCardInformation ()
    {
        dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/Label_before").gameObject.GetComponent<UILabel> ().text = (Ag.mySelf.arrNewCard [0].WAS.level - 1).ToString ();
        dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/Label_after").gameObject.GetComponent<UILabel> ().text = (Ag.mySelf.arrNewCard [0].WAS.level).ToString ();
        
        if (Ag.mySelf.arrNewCard [0].WAS.isKicker) {
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/1_direct/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfDirection (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/2_accuracy/Label_1").gameObject.GetComponent<UILabel> ().text = "";
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/3_firekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full":Ag.mySelf.arrNewCard [0].WAS.GetValueOfFireOrFresh (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/4_blazekick/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfBlazeOrLightening (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/5_volcano/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfVolcano(1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_dataafter/6_addscore/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetPointBonus (mwas.level+1).ToString();

            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/1_direct/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfDirection (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/2_accuracy/Label_1").gameObject.GetComponent<UILabel> ().text = "";
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/3_firekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full": Ag.mySelf.arrNewCard [0].WAS.GetValueOfFireOrFresh ().ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/4_blazekick/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfBlazeOrLightening ().ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/5_volcano/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfVolcano(0).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/kicker_data/stat_databefore/6_addscore/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetPointBonus (mwas.level).ToString();
            
        } else {
            //after
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_dataafter/1_blance/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfBalance (0).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_dataafter/2_flashjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full": Ag.mySelf.arrNewCard [0].WAS.GetValueOfFireOrFresh (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_dataafter/3_lightningjump/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfBlazeOrLightening (1).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_dataafter/4_bonusscore/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetPointBonus (mwas.level+1).ToString();
            //before
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_databefore/1_blance/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetValueOfBalance (0).ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_databefore/2_flashjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full": Ag.mySelf.arrNewCard [0].WAS.GetValueOfFireOrFresh ().ToString();;
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_databefore/3_lightningjump/Label_1").gameObject.GetComponent<UILabel> ().text =  Ag.mySelf.arrNewCard [0].WAS.GetValueOfBlazeOrLightening ().ToString();
            dicMenuList ["overlap_card"].transform.FindChild ("popup_overlap/keeper_data/stat_databefore/4_bonusscore/Label_1").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrNewCard [0].WAS.GetPointBonus (mwas.level).ToString();
            
        }
        
    }
    
    /// <summary>
    /// 카드 중복 구매하기
    /// </summary>
    bool mLobbyBuyCardFlag;
    int mCardGradeType; // 0 : Normal, 1 : High, 2: Kleague
    void Card_Rebuy ()
    {
        if (Ag.mySelf.arrCard.Count > 59) {
            MenuCommonOpen ("Ui_popup", "popup_playerfull", true);
            return;
        }

        if (mLobbyBuyCardFlag) {
            if (mCardGradeType == 0)
                Btn_Fun_NorMalCard_Rebuy ();
            if (mCardGradeType == 1)
                Btn_Fun_HighCard_Rebuy ();
            if (mCardGradeType == 2)
                Btn_Fun_KleagueCard_Rebuy ();
        } else {
            if (mCardGradeType == 0)
                Btn_Fun_team_BuyNormal1_rebuy ();
            if (mCardGradeType == 1)
                Btn_Fun_team_BuyCardHigh1_rebuy ();
            if (mCardGradeType == 2)
                Btn_Fun_team_BuyCarKleague1_rebuy ();
        }
        
        
    }
    
    /// <summary>
    /// 중복카드 팝업 끄기
    /// </summary>
    void Popup_OverLab_cardClose ()
    {
        MenuCommonOpen ("Ui_popup", "overlap_card", false);
    }
    
    /// <summary>
    /// 중복카드 팝업 다시 구매하기
    /// </summary>
    void Popup_OverLab_Rebuy ()
    {
        MenuCommonOpen ("Ui_popup", "overlap_card", false);
        Card_Rebuy ();
    }

}
