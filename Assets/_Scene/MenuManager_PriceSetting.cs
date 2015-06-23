using UnityEngine;
using System.Collections;

public partial class MenuManager: AmSceneBase
{
    WasItemPriceObj mPriceObj;

    int ItemPrice (string ItemTypeId)
    {
        WasItemPriceObj Price = new WasItemPriceObj ();
        int PriceNum = 0;

        for (int i = 0; i < Ag.mySelf.arrItemPrice.Count; i++) {
            if (ItemTypeId == Ag.mySelf.arrItemPrice [i].itemTypeID) {
                Price = Ag.mySelf.arrItemPrice [i];
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 0)
                    PriceNum = Price.originalCash;
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 1)
                    PriceNum = Price.originalGold;
            }
        }
        return PriceNum;

    }

    int GetRealBuyPrice (string ItemTypeId)
    {
        WasItemPriceObj Price = new WasItemPriceObj ();
        int PriceNum = 0;
        
        for (int i = 0; i < Ag.mySelf.arrItemPrice.Count; i++) {
            if (ItemTypeId == Ag.mySelf.arrItemPrice [i].itemTypeID) {
                Price = Ag.mySelf.arrItemPrice [i];
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 0)
                    PriceNum = Price.cash;
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 1)
                    PriceNum = Price.gold;
            }
        }
        return PriceNum;
    }

    bool ItemEventOnCheck (string ItemTypeId, GameObject EventLabel, GameObject CutoffLine, GameObject SalePrice)
    {
        WasItemPriceObj Price = new WasItemPriceObj ();
        bool CheckEvent = false;
        EventLabel.SetActive (false);
        CutoffLine.SetActive (false);
        SalePrice.SetActive (false);

        for (int i = 0; i < Ag.mySelf.arrItemPrice.Count; i++) {
            if (ItemTypeId == Ag.mySelf.arrItemPrice [i].itemTypeID) {
                Price = Ag.mySelf.arrItemPrice [i];
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 0) {
                    if (Price.cash != Price.originalCash) {
                        //Debug.Log ("     Price.gold" + Price.gold + " priceOriginalGold   " + Price.originalGold + "     ItemTypeid" + ItemTypeId);

                        EventLabel.SetActive (true);
                        CutoffLine.SetActive (true);
                        SalePrice.SetActive (true);
                        SalePrice.GetComponent<UILabel> ().text = Price.cash.ToString ();
                        CheckEvent = true;
                    }
                }
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 1) {
                    if (Price.gold != Price.originalGold) {
                        //Debug.Log ("     Price.gold" + Price.gold + " priceOriginalGold   " + Price.originalGold + "     ItemTypeid" + ItemTypeId + "SalePrice   " + SalePrice.name);
                        EventLabel.SetActive (true);
                        CutoffLine.SetActive (true);
                        SalePrice.SetActive (true);

                        SalePrice.GetComponent<UILabel> ().text = Price.gold.ToString ();
                        CheckEvent = true;
                    }
                } 
            }
        }
        return CheckEvent;
    }

    bool ItemEventOnCheck (string ItemTypeId)
    {
        WasItemPriceObj Price = new WasItemPriceObj ();
        bool CheckEvent = false;

        for (int i = 0; i < Ag.mySelf.arrItemPrice.Count; i++) {
            if (ItemTypeId == Ag.mySelf.arrItemPrice [i].itemTypeID) {
                Price = Ag.mySelf.arrItemPrice [i];
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 0) {
                    if (Price.cash != Price.originalCash) {
                        CheckEvent = true;
                    }
                }
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 1) {
                    if (Price.gold != Price.originalGold) {
                        CheckEvent = true;
                    }
                } 
            }
        }
        return CheckEvent;
    }

    string GoldTypeIconName (string ItemTypeId)
    {
        string BuyTypeIcon = "";
        for (int i = 0; i < Ag.mySelf.arrItemPrice.Count; i++) {
            if (ItemTypeId == Ag.mySelf.arrItemPrice [i].itemTypeID) {
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 0)
                    BuyTypeIcon = "icon_cash";
                if (Ag.mySelf.GetBuyType (ItemTypeId) == 1)
                    BuyTypeIcon = "icon_gold";
            }
        }
        return BuyTypeIcon;
    }

    void PriceSetting ()
    {
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncBackNumEdit").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncBackNumEdit").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_editplayername/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncBackNumEdit").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_editplayername/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncBackNumEdit").ToString ();

        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncHeartMax").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_1/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncHeartMax");

        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_2/Label_price").GetComponent<UILabel> ().text = ItemPrice ("HeartSpeedUp").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_2/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("HeartSpeedUp").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_3/Label_price").GetComponent<UILabel> ().text = ItemPrice ("HeartLimitUp").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_3/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("HeartLimitUp").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("GloveFreeDay").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_1/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GloveFreeDay").ToString ();

        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_2/Label_price").GetComponent<UILabel> ().text = ItemPrice ("GloveFreeWeek").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_2/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GloveFreeWeek").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_3/Label_price").GetComponent<UILabel> ().text = ItemPrice ("GloveFreeMonth").ToString ();
        dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_3/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GloveFreeMonth").ToString ();


        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold1000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold1000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_2/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold5500").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold5500").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_3/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold12000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold12000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table2/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold39000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold39000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table2/btn_2/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold70000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold70000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table2/btn_3/Label_price").GetComponent<UILabel> ().text = ItemPrice ("Gold150000").ToString ();
        dicMenuList ["Panel_top"].transform.FindChild ("LPanel_shop/table_point/table1/btn_1/Sprite (icon_cash)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Gold150000").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricenormal/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Normal").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_gold/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Normal").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricenormal/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Normal") * 3).ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricesuper/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Abnomal").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_cash/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Abnomal").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricesuper/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Abnomal") * 3).ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricekleague/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Best").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_cash2/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Best").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/buycard/grid_pricekleague/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Best") * 3).ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes01").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item01/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerShoes01").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes02").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item02/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerShoes02").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes03").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item03/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerShoes03").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes04").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item04/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerShoes04").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves01").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item01/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperGloves01").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves02").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item01/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperGloves02").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves03").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item03/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperGloves03").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves04").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item04/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperGloves04").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiGrade").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item01/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiGrade").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvtHigh").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item02/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiGrade").ToString ();

        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvt").ToString ();
        dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item03/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiAdvt").ToString ();


        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricenormal/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Normal").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_gold/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Normal").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricenormal/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Normal") * 3).ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricesuper/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Abnomal").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_cash/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Abnomal").ToString ();

        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricesuper/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Abnomal") * 3).ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricekleague/Label_1").GetComponent<UILabel> ().text = ItemPrice ("Best").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_cash2/Sprite (icon_gold1)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("Best").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_pricekleague/Label_2").GetComponent<UILabel> ().text = (ItemPrice ("Best") * 3).ToString ();

        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/bundle_btnoption/btn_mix/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FunCardMix").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiGrade").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item01/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiGrade").ToString ();

        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvtHigh").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item02/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiAdvtHigh").ToString ();

        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvt").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_cardmix/popup_mixitem/itemlist/item03/Sprite (icon_gold)").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CardCombiAdvt").ToString ();




        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_condition/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncRecover").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_condition/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncRecover").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_condition/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncRecover").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_condition/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncRecover").ToString ();



        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_coach/popup_teamnameedit/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncTeamNameEdit").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_coach/popup_teamnameedit/icon_cash").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncTeamNameEdit").ToString ();


        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_coach/popup_recordinitialization/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncResetRank").ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("LPanel_coach/popup_recordinitialization/icon_cash").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncResetRank").ToString ();


        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_blue_red/btn_auto/Label_price").GetComponent<UILabel> ().text = ItemPrice ("TeamBlueDrink").ToString ();
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_blue_red/btn_auto/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("TeamBlueDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_green/btn_auto/Label_price").GetComponent<UILabel> ().text = ItemPrice ("TeamGreenDrink").ToString ();
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_green/btn_auto/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("TeamGreenDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_blue_red/btn_auto/Label_price").GetComponent<UILabel> ().text = ItemPrice ("TeamRedDrink").ToString ();
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/btngrid_basicitem_blue_red/btn_auto/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("TeamRedDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item00_blue/Label_price").GetComponent<UILabel> ().text = ItemPrice ("BlueDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item00_blue/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("BlueDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item02_green/Label_price").GetComponent<UILabel> ().text = ItemPrice ("GreenDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item02_green/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GreenDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item01_red/Label_price").GetComponent<UILabel> ().text = ItemPrice ("RedDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_basicitem/grid/item01_red/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("RedDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("BlueDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("BlueDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_5/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("BlueDrink") * 5).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_5/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("BlueDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_10/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("BlueDrink") * 10).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("BlueDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("GreenDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GreenDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_5/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("GreenDrink") * 5).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_5/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GreenDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_10/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("GreenDrink") * 10).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_green/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("GreenDrink").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("RedDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("RedDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_5/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("RedDrink") * 5).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_5/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("RedDrink").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_10/Label_price").GetComponent<UILabel> ().text = (ItemPrice ("RedDrink") * 10).ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_Drinkitem/check_blue_red/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("RedDrink").ToString ();
        //dicMenuList [""].transform.FindChild ("").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill00").cash.ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill01").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony1/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill01").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony2/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill02").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony2/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill02").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony3/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill03").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony3/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill03").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony4/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill04").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony4/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill04").ToString ();

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony5/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill05").ToString ();
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_ceremony/grid/cerimony6/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill06").cash.ToString ();
        //버튼 가격
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_item/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CeremonySkill01").ToString ();
        //팝업 가격

        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_message/grid/message1/Label_price").GetComponent<UILabel> ().text = ItemPrice ("EndMessage01").ToString ();
        dicMenuList ["Ui_kickoff"].transform.FindChild ("LPanel_item/scroll_message/grid/message1/icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("EndMessage01").ToString ();

        //버튼 가격
        //dicMenuList ["Ui_kickoff"].transform.FindChild ("popup/buy_item/Label_price").GetComponent<UILabel> ().text = ItemPrice ("EndMessage01").ToString ();
        //팝업 가격

    }

    void KickerUniformPrice ()
    {
        dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop1").ToString ();
        dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop1").ToString ();

        dicMenuList ["Btn_Fun_ShirsType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop2").ToString ();
        dicMenuList ["Btn_Fun_ShirsType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop2").ToString ();

        dicMenuList ["Btn_Fun_ShirsType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop3").ToString ();
        dicMenuList ["Btn_Fun_ShirsType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop3").ToString ();

        dicMenuList ["Btn_Fun_ShirsType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop4").ToString ();
        dicMenuList ["Btn_Fun_ShirsType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop4").ToString ();

        dicMenuList ["Btn_Fun_ShirsType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop5").ToString ();
        dicMenuList ["Btn_Fun_ShirsType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop5").ToString ();

        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformTop6").ToString ();
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformTop6").ToString ();



        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants1").ToString ();
        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants1").ToString ();

        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants2").ToString ();
        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants2").ToString ();

        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants3").ToString ();
        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants3").ToString ();

        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants4").ToString ();
        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants4").ToString ();

        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants5").ToString ();
        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants5").ToString ();

        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformPants6").ToString ();
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformPants6").ToString ();

        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks1").ToString ();
        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks1").ToString ();

        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks2").ToString ();
        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks2").ToString ();

        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks3").ToString ();
        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks3").ToString ();

        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks4").ToString ();
        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks4").ToString ();

        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks5").ToString ();
        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks5").ToString ();

        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KickerUniformSocks6").ToString ();
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KickerUniformSocks6").ToString ();
    }

    void KeeperUniformPrice ()
    {
        dicMenuList ["Btn_Fun_Kp_ShirsType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop1").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop1").ToString ();

        dicMenuList ["Btn_Fun_Kp_ShirsType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop2").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop2").ToString ();

        dicMenuList ["Btn_Fun_Kp_ShirsType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop3").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop3").ToString ();

        dicMenuList ["Btn_Fun_Kp_ShirsType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop4").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop4").ToString ();

        dicMenuList ["Btn_Fun_Kp_ShirsType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop5").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop5").ToString ();

        dicMenuList ["Btn_Fun_Kp_ShirsType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformTop6").ToString ();
        dicMenuList ["Btn_Fun_Kp_ShirsType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformTop6").ToString ();


        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants1").ToString ();
        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants1").ToString ();

        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants2").ToString ();
        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants2").ToString ();

        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants3").ToString ();
        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants3").ToString ();

        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants4").ToString ();
        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants4").ToString ();

        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants5").ToString ();
        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants5").ToString ();

        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformPants6").ToString ();
        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformPants6").ToString ();

        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks1").ToString ();
        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks1").ToString ();

        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks2").ToString ();
        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks2").ToString ();

        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks3").ToString ();
        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks3").ToString ();

        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks4").ToString ();
        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks4").ToString ();

        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks5").ToString ();
        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks5").ToString ();


        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("Label_price").gameObject.GetComponent<UILabel> ().text = ItemPrice ("KeeperUniformSocks6").ToString ();
        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("KeeperUniformSocks6").ToString ();

    }

    void LevelUpPrice (int pNum, bool PKickker)
    {
        if (PKickker) {
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_training/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncLevelUp" + (pNum + 1)).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_training/icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncLevelUp" + (pNum + 1)).ToString ();
        } else {
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_training/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncLevelUp" + (pNum + 1)).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_training/icon_coin").gameObject.GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncLevelUp" + (pNum + 1)).ToString ();
        }
    }

    string PlayerGrade;

    void cardExtendPrice (string pGrade, bool PKickker)
    {

        if (mwas.grade == "A")
            PlayerGrade = "FuncCardExtendA";
        if (mwas.grade == "B")
            PlayerGrade = "FuncCardExtendB";
        if (mwas.grade == "C")
            PlayerGrade = "FuncCardExtendC";
        if (mwas.grade == "D")
            PlayerGrade = "FuncCardExtendD";
        if (mwas.grade == "S")
            PlayerGrade = "FuncCardExtendS";

//        if (PKickker) {
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "30")).ToString () : (ItemPrice (PlayerGrade + "1")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "30")).ToString () : (GoldTypeIconName (PlayerGrade + "1")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "60")).ToString () : (ItemPrice (PlayerGrade + "10")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "60")).ToString () : (GoldTypeIconName (PlayerGrade + "10")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "90")).ToString () : (ItemPrice (PlayerGrade + "30")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "90")).ToString () : (GoldTypeIconName (PlayerGrade + "30")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "120")).ToString () : (ItemPrice (PlayerGrade + "50")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "120")).ToString () : (GoldTypeIconName (PlayerGrade + "50")).ToString ();
//
//        } else {
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "30")).ToString () : (ItemPrice (PlayerGrade + "1")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "30")).ToString () : (GoldTypeIconName (PlayerGrade + "1")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "60")).ToString () : (ItemPrice (PlayerGrade + "10")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "60")).ToString () : (GoldTypeIconName (PlayerGrade + "10")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "90")).ToString () : (ItemPrice (PlayerGrade + "30")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "90")).ToString () : (GoldTypeIconName (PlayerGrade + "30")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/Label_price").GetComponent<UILabel> ().text = (mwas.grade == "A" || mwas.grade == "S") ? (ItemPrice (PlayerGrade + "120")).ToString () : (ItemPrice (PlayerGrade + "50")).ToString ();
//            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/icon_coin").GetComponent<UISprite> ().spriteName = (mwas.grade == "A" || mwas.grade == "S") ? (GoldTypeIconName (PlayerGrade + "120")).ToString () : (GoldTypeIconName (PlayerGrade + "50")).ToString ();
//
//        }
        if (PKickker) {
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,0))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,0))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,1))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,1))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,2))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,2))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,3))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,3))).ToString ();

        } else {
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,0))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,0))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,1))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,1))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,2))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,2))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/Label_price").GetComponent<UILabel> ().text = (ItemPrice (PlayerGrade + GetRecharterEa(mwas.grade,3))).ToString ();
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/icon_coin").GetComponent<UISprite> ().spriteName = (GoldTypeIconName (PlayerGrade + GetRecharterEa(mwas.grade,3))).ToString ();

        }

    }

    void CostUpPrice (int level)
    {
        dicMenuList ["icon_cash"].SetActive (true);
        dicMenuList ["icon_cash"].GetComponent<UISprite> ().spriteName = GoldTypeIconName ("FuncCostUp" + (level + 1)).ToString ();
        dicMenuList ["Ui_team"].transform.FindChild ("popup/popup_levelup/Label_price").GetComponent<UILabel> ().text = ItemPrice ("FuncCostUp" + (level + 1)).ToString ();
        

    }
}
