//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class MenuManager : AmSceneBase
{
    //  ////////////////////////////////////////////////     ////////////////////////     Go to Another Screen ..
    //  ////////////////////////////////////////////////     ////////////////////////     MainMenu PopUPList
    //------------------------------------
    int mExtendPlayeEa;

    void Btn_fun_KeeperRecharterOpen ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_recharter", true);
        dicMenuList ["Keeper_popup_recharter"].transform.FindChild ("Label_playname").GetComponent<UILabel> ().text = WWW.UnEscapeURL (mwas.playerName);
        cardExtendPrice (mwas.grade, false);
        Btn_fun_KeeperRecharterea1 ();
    }

    void Btn_fun_KeeperRecharterClose ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_recharter", false);
    }

    void Btn_fun_KeeperRecharterOK ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_recharter", false);
        CardExtend (mwas.ID, mExtendPlayeEa);
    }

    void Btn_fun_KeeperRecharterea1 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,0));
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1", true).GetComponent<UICheckbox> ().isChecked = true;
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1", true).GetComponent<UICheckbox> ().Set (true);
    }

    void Btn_fun_KeeperRecharterea10 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,1));
    }

    void Btn_fun_KeeperRecharterea30 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,2));
    }

    void Btn_fun_KeeperRecharterea50 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,3));
    }

    void Btn_fun_kickerRecharterOpen ()
    {
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_recharter", true);
        dicMenuList ["Kicker_popup_recharter"].transform.FindChild ("Label_playname").GetComponent<UILabel> ().text = WWW.UnEscapeURL (mwas.playerName);
        cardExtendPrice (mwas.grade, true);
        Btn_fun_Recharterea1 ();

    }

    void Btn_fun_RecharterClose ()
    {
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_recharter", false);
    }

    void Btn_fun_RecharterOK ()
    {
        CardExtend (mwas.ID, mExtendPlayeEa);
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_recharter", false);

    }

    void CardExtend (int cardid, int mExtendPlayeEa)
    {

        dicMenuList ["CenterCircle"].SetActive (true);
        WasCardExtend aObj = new WasCardExtend () {
            User = Ag.mySelf,
            cardId = cardid,
            count = mExtendPlayeEa
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) {
            case 0: // Success 
                Userinfo ();
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;

            }
        };
    }

    void Btn_fun_Recharterea1 ()
    {

        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,0));
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1", true).GetComponent<UICheckbox> ().isChecked = true;
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1", true).GetComponent<UICheckbox> ().Set (true);
    }

    void Btn_fun_Recharterea10 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,1));
    }

    void Btn_fun_Recharterea30 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,2));
    }

    void Btn_fun_Recharterea50 ()
    {
        mExtendPlayeEa = int.Parse(GetRecharterEa(mwas.grade,3));
    }

    void Lobbysubmenuclose ()
    {
        dicMenuList.SetActiveAll (false, new string [] { "shoes", "mixitem", "coupon", "buycard", "glove" });
    }

    void CostumeSetting ()
    {
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes01").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item02/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes02").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item03/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes03").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item04/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes04").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiGrade").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves01").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves02").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves03").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves04").ToString ();

    }

    void Btn_Fun_shoesList ()
    {
        Lobbysubmenuclose ();
        dicMenuList ["shoes"].SetActive (true);


        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes01").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 2%";

        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item02/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes02").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item02/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 4%";

        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item03/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes03").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item03/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 6%";

        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item04/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes04").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item04/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 8%";
        Btn_Fun_SoccerShose1 ();

        for (int i = 1; i < 5; i++) {
            ItemEventOnCheck ("KickerShoes0" + i, FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item0" + i + "/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item0" + i + "/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item0" + i + "/Label_eventprice", true));
        }
    }

    int CostumeListEa (string id)
    {
        int ShoesEA = 0;
//        Debug.Log ("Start");
        for (int i = 0; i < Ag.mySelf.arrCostume.Count; i++) {
            if (Ag.mySelf.arrCostume [i].WAS.itemTypeId == id && Ag.mySelf.arrCostume [i].WAS.cardId == -1) {
                ShoesEA++;
//                Debug.Log ("ShoesEA");
            }
        }
        return ShoesEA;
    }

    int CombiItemListEa (string id)
    {
        int CombiItemEA = 0;
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                CombiItemEA = Ag.mySelf.arrItem [i].WAS.ea;
//                Debug.Log ("ShoesEA");
            }
        }
        return CombiItemEA;

    }

    void Btn_Fun_CardMixItem ()
    {
        Btn_Fun_MixItem1 ();
        Lobbysubmenuclose ();
        dicMenuList ["mixitem"].SetActive (true);
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiGrade").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03/Label_ possession", true).GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
        ItemEventOnCheck ("CardCombiGrade", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01/Label_eventprice", true));
        ItemEventOnCheck ("CardCombiAdvtHigh", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item02/Label_eventprice", true));
        ItemEventOnCheck ("CardCombiAdvt", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item03/Label_eventprice", true));



    }

    void Btn_Fun_Coupon ()
    {
        Lobbysubmenuclose ();
        dicMenuList ["coupon"].SetActive (true);
        dicMenuList ["coupon"].transform.FindChild ("Label_error").gameObject.SetActive (false);
    }

    void Btn_Fun_Card ()
    {
        Lobbysubmenuclose ();
        dicMenuList ["buycard"].SetActive (true);
        if (ItemEventOnCheck ("Normal"))
            dicMenuList ["buycard"].transform.FindChild ("grid_eventpricenormal").gameObject.SetActive (true);
        if (ItemEventOnCheck ("Abnomal"))
            dicMenuList ["buycard"].transform.FindChild ("grid_eventpricesuper").gameObject.SetActive (true);
        if (ItemEventOnCheck ("KLeague"))
            dicMenuList ["buycard"].transform.FindChild ("grid_eventpricekleague").gameObject.SetActive (true);

        ItemEventOnCheck ("Normal", dicMenuList ["buycard"].transform.FindChild ("grid_txteventnormal").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_cutpricenormal").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_eventpricenormal/Label_1").gameObject);
        ItemEventOnCheck ("Abnomal", dicMenuList ["buycard"].transform.FindChild ("grid_txteventsuper").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_cutpricesuper").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_eventpricesuper/Label_1").gameObject);
        ItemEventOnCheck ("KLeague", dicMenuList ["buycard"].transform.FindChild ("grid_txteventkleague").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_cutpricekleague").gameObject, dicMenuList ["buycard"].transform.FindChild ("grid_eventpricekleague/Label_1").gameObject);

    }

    void Btn_Fun_Glove ()
    {
        Lobbysubmenuclose ();
        dicMenuList ["glove"].SetActive (true);
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves01").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves02").ToString ();
        ;
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves03").ToString ();
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/Label_amount", true).GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves04").ToString ();

        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 2%";
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 4%";
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 6%";
        FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/Label_discript", true).GetComponent<UILabel> ().text = "가산점 + 8%";

        Btn_Fun_SoccerGlove1 ();
        ItemEventOnCheck ("KeeperGloves01", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01/Label_eventprice", true));
        ItemEventOnCheck ("KeeperGloves02", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item02/Label_eventprice", true));
        ItemEventOnCheck ("KeeperGloves03", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item03/Label_eventprice", true));
        ItemEventOnCheck ("KeeperGloves04", FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/txtevent", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/Sprite (cutline)", true), FindMyChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item04/Label_eventprice", true));
            


    }
    //-------------------------------------------------------------   ItemShopOpen
    void Btn_Fun_GiftBoxOpen ()
    {
        mBackDepthFlag = true;
        dicMenuList ["LPanel_itemshop"].SetActive (true);
        dicMenuList ["Ui_lobby_checkbox0_card"].GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["Ui_lobby_checkbox0_card"].GetComponent<UICheckbox> ().Set (true);
        Btn_Fun_Card ();
    }

    void Btn_Fun_GiftBoxClose ()
    {
        dicMenuList ["LPanel_itemshop"].SetActive (false);
    }
    //-------------------------------------------------------------   MainShop
    void Btn_Fun_EquipBoxShopCash ()
    {
        mBackDepthFlag = true;
        var skus = new string[] {
            "com.prime31.testproduct",
            "android.test.purchased",
            "com.prime31.managedproduct",
            "com.prime31.testsubscription"
        };


        #if UNITY_ANDROID
        GoogleIAB.queryInventory( skus );
        #endif
        dicMenuList ["LPanel_shop"].SetActive (true);
        ShopTabBar (false, false, true);

        /*
        if (PriceEventOnCheck ("")) 
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table1/btn_1/event_bundle").gameObject.SetActive(true);
        if (PriceEventOnCheck ("")) 
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table1/btn_2/event_bundle").gameObject.SetActive(true);
        if (PriceEventOnCheck ("")) 
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table1/btn_3/event_bundle").gameObject.SetActive(true);
        if (PriceEventOnCheck (""))     
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table2/btn_1/event_bundle").gameObject.SetActive(true);
        if (PriceEventOnCheck ("")) 
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table2/btn_2/event_bundle").gameObject.SetActive(true);
        if (PriceEventOnCheck ("")) 
            dicMenuList ["LPanel_shop_table_cash"].transform.FindChild("table2/btn_3/event_bundle").gameObject.SetActive(true);
        */

    }

    void Btn_Fun_EquipBoxShopGlove ()
    {
        mBackDepthFlag = true;
        ItemEventOnCheck ("Heart5", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_1/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_1/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_1/Label_eventprice").gameObject);
        ItemEventOnCheck ("Heart11", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_2/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_2/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_2/Label_eventprice").gameObject);
        ItemEventOnCheck ("Heart24", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_3/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_3/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table1/btn_3/Label_eventprice").gameObject);
        ItemEventOnCheck ("GloveFreeDay", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_1/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_1/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_1/Label_eventprice").gameObject);
        ItemEventOnCheck ("GloveFreeMonth", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_2/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_2/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_2/Label_eventprice").gameObject);
        ItemEventOnCheck ("GloveFreeWeek", dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_3/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_3/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table2/btn_3/Label_eventprice").gameObject);

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




        dicMenuList ["LPanel_shop"].SetActive (true);
        ShopTabBar (false, true, false);
    }

    void Btn_Fun_EquipBoxShopGold ()
    {
        mBackDepthFlag = true;
        ItemEventOnCheck ("Gold1000", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_1/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_1/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_1/Label_eventprice").gameObject);
        ItemEventOnCheck ("Gold5500", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_2/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_2/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_2/Label_eventprice").gameObject);
        ItemEventOnCheck ("Gold12000", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_3/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_3/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table1/btn_3/Label_eventprice").gameObject);
        ItemEventOnCheck ("Gold39000", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_1/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_1/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_1/Label_eventprice").gameObject);
        ItemEventOnCheck ("Gold70000", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_2/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_2/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_2/Label_eventprice").gameObject);
        ItemEventOnCheck ("Gold150000", dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_3/event_bundle").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_3/Sprite (cutline)").gameObject, dicMenuList ["LPanel_shop_table_point"].transform.FindChild ("table2/btn_3/Label_eventprice").gameObject);


        /*
        if (ItemEventOnCheck ("Gold1000")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table1/btn_1/event_bundle").gameObject.SetActive(true);
        if (ItemEventOnCheck ("Gold5500")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table1/btn_2/event_bundle").gameObject.SetActive(true);
        if (ItemEventOnCheck ("Gold12000")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table1/btn_3/event_bundle").gameObject.SetActive(true);
        if (ItemEventOnCheck ("Gold39000")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table2/btn_1/event_bundle").gameObject.SetActive(true);
        if (ItemEventOnCheck ("Gold70000")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table2/btn_2/event_bundle").gameObject.SetActive(true);
        if (ItemEventOnCheck ("Gold150000")) 
            dicMenuList ["LPanel_shop_table_point"].transform.FindChild("table2/btn_3/event_bundle").gameObject.SetActive(true);

        */
        dicMenuList ["LPanel_shop"].SetActive (true);
        ShopTabBar (true, false, false);
    }

    void Btn_Fun_EquipBoxShopClose ()
    {
        dicMenuList ["LPanel_shop"].SetActive (false);
    }

    void ShopTabBar (bool pGold, bool pGlove, bool pCoin)
    {
        dicMenuList ["LPanel_shop_table_point"].SetActive (pGold);
        dicMenuList ["LPanel_shop_table_glove"].SetActive (pGlove);
        dicMenuList ["LPanel_shop_table_cash"].SetActive (pCoin);


        dicMenuList ["checkbox0_cash"].GetComponent<UICheckbox> ().isChecked = pCoin;
        dicMenuList ["checkbox0_cash"].GetComponent<UICheckbox> ().Set (pCoin);

        dicMenuList ["checkbox3_glove"].GetComponent<UICheckbox> ().isChecked = pGlove;
        dicMenuList ["checkbox3_glove"].GetComponent<UICheckbox> ().Set (pGlove);

        dicMenuList ["checkbox1_point"].GetComponent<UICheckbox> ().isChecked = pGold;
        dicMenuList ["checkbox1_point"].GetComponent<UICheckbox> ().Set (pGold);


    }
    //  ////////////////////////////////////////////////     ////////////////////////
    //  ////////////////////////////////////////////////     ////////////////////////     Lineup
    public IEnumerator MAXLEVEL ()
    {
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/pop_maxlevel", true).transform.gameObject.SetActive (true);
        yield return new WaitForSeconds (1f);
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/pop_maxlevel", true).transform.gameObject.SetActive (false);

    }

    /// <summary>
    /// 컨디션 최고일때 띄우는 팝업
    /// </summary>
     
    public IEnumerator MaxCondition (GameObject Gobj)
    {
        Gobj.SetActive (true);
        yield return new WaitForSeconds (1f);
        Gobj.SetActive (false);
    }

    void Btn_Fun_CostUp ()
    {
        //SendWasCardupdate();
        if (Level == 7) {
            StartCoroutine (MAXLEVEL ());
            return;
        }
        dicMenuList ["UI_teamPopup"].SetActive (true);
        dicMenuList ["popup_levelup"].SetActive (true);
        CostUpLabel ();
    }

    void btn_Costup_Cancel ()
    {
        dicMenuList ["UI_teamPopup"].SetActive (false);
        dicMenuList ["popup_levelup"].SetActive (false);
    }

    void btn_Costup_Ok ()
    {
        Label_nowlevel ();
        dicMenuList ["UI_teamPopup"].SetActive (false);
        dicMenuList ["popup_levelup"].SetActive (false);
        dicMenuList ["CenterCircle"].SetActive (true);
        WasFuncCostUp aObj = new WasFuncCostUp () { User = Ag.mySelf }; //, buyType = bt  };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { //
            case 0:
                WasUserInfo bObj = new WasUserInfo () { User = Ag.mySelf, flag = 0 };
                bObj.messageAction = (int paInt) => {
                    if (paInt == 0)
                        CostUpLabel ();
                };
                Ag.LogString (" result : Success ");
                break;
            case -1:
                if (Ag.mySelf.GetBuyType("FuncCostUp" + (Level + 1)) == 0) {
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                    return;
                }
                if (Ag.mySelf.GetBuyType("FuncCostUp" + (Level + 1)) == 1) {
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                    return;
                } 
                break;
            }
        };

    }

    public void MainUserInfo ()
    {
        Ag.LogIntenseWord ("  MainUserInfo    KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);
        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 0 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { 
            case 0:
                StartCoroutine(IntendedPausefalse());
                break;
            case -1:
            case 4:
                return;
            }
        };
    }

    IEnumerator IntendedPausefalse () {
        yield return new WaitForSeconds (10f);
        AgStt.IntendedPause = false;
    }


    void FetchCard ()
    {



    }

    void Userinfo ()
    {
        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 1 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0
            case 0:
                Ag.LogString (" result : Success ");

                Ag.LogDouble ("  MenuManager TeamManager :: UserInfo    Result : 0    CostUpLabel ");

                Ag.mySelf.SetCostumeToCard ();
                for (int i = 0; i < Ag.mySelf.arrCard.Count; i++) {
                    if (mCard.WAS.ID == Ag.mySelf.arrCard [i].WAS.ID) {
                        mwas = Ag.mySelf.arrCard [i].WAS;
                        mCard = Ag.mySelf.arrCard [i];
                        //Debug.Log ("CardINFO" + mCard.WAS.ID + mwas.ID + mwas.level);
                    }
                }
                
                Ag.LogDouble ("  MenuManager TeamManager :: Game Object ");
                
                dicMenuList ["Lobby_Flag"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("flag/" + Ag.mCountryData.SetNationSprite (Ag.mySelf.WAS.Country));
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_teamname").GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
                dicMenuList ["Lobby_Coach_TeamName"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
                dicMenuList ["Label_teamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
                Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);
                
                dicMenuList ["Lobby_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                dicMenuList ["Ui_team_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                dicMenuList ["MY_Label_division"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                
                Ag.LogDouble ("  MenuManager TeamManager ::  isKicker ? >>  " + mCard.WAS.isKicker);
                
                RanKuser ();
                
                if (mCard.WAS.isKicker) {
                    KickerInfo ();
                } else {
                    KeeperInfo ();
                    
                }
                
                try { 
                    StartCoroutine (CardCostAll2 (0.1f));
                } catch {
                    Ag.LogDouble ("  MenuManager TeamManager :: Card Nothing ");
                }


                CostUpLabel ();
                break;
            case -1:
            case 4:
                return;
            }
        };

    }

    IEnumerator KeeperSet ()
    {
        yield return new WaitForSeconds (0.5f);

    }

    void Btn_Fun_levelinfopopup ()
    {
        dicMenuList ["bundle_startinglevel"].SetActive (true);
    }

    void Btn_Fun_levelinfopopupClose ()
    {
        dicMenuList ["bundle_startinglevel"].SetActive (false);
    }

    bool CardLineup = false;

    void Btn_Fun_Panel_keeper_Close ()
    {
        dicMenuList ["Panel_keeper"].SetActive (false);
        if (CardLineup) {
            AllCardSelect ();
            //Btn_Fun_SortByGrade_setting();
            StartCoroutine ("SortbySetting");
        } else {
            MIXCARDSELECT ();
            //Btn_SortbyGradeAndFlag_CardMix ();
            StartCoroutine (WaitAndBtn_SortbyGradeAndFlag_CardMix ());
            //Btn_SortbyGrade_CardMixPopupClose ();
        }

        LineUpNationFlagEffOnOff ();
    }

    void Btn_Fun_Panel_kicker_Close ()
    {
        dicMenuList ["Panel_kicker"].SetActive (false);
        for (int i = 0; i < BarObj.Count; i++) {
            DestroyObject (BarObj [i]);
        }
        if (CardLineup) {
            AllCardSelect ();
            //Btn_Fun_SortByGrade_setting();
            StartCoroutine ("SortbySetting");
        } else {

            for (int i = 0; i < arrCardMixItemSelect.Count; i++) {
                DestroyObject(arrCardMixItemSelect[i].gameObject);
            }
            MIXCARDSELECT ();

            //Btn_SortbyGradeAndFlag_CardMix ();
            StartCoroutine (WaitAndBtn_SortbyGradeAndFlag_CardMix ());
            //Btn_SortbyGrade_CardMixPopupClose ();
        }

        LineUpNationFlagEffOnOff ();

    }
    //*--------------------------all submenu close
    void LineUpsubmenuclose ()
    {
        dicMenuList ["LPanel_buycard"].SetActive (false);
        dicMenuList ["LPanel_lineup"].SetActive (false);
        dicMenuList ["LPanel_cardmix"].SetActive (false);
        dicMenuList ["LPanel_coach"].SetActive (false);
        dicMenuList ["LPanel_uniform"].SetActive (false);
    }
    //*--------------------------
    string mMenuName;

    void Btn_Fun_GotoLineup ()
    {
        Ag.mySelf.ApplyCurrentDeck ();

        SortBtnInitSet();

        mBackDepthFlag = true;

        if (AgStt.mgGameTutorial) {
            Application.LoadLevel ("GameScene");
            return;
        }

        dicMenuList ["Menu_btn_tutorial"].SetActive (true);
        dicMenuList ["Menu_btn_book"].SetActive (false);
        //InitMenuTutor();
        InitMenuTutor ();

        if (!PreviewLabs.PlayerPrefs.GetBool ("MenuTutorLineup")) {
            dicMenuList ["Ui_menututorial"].SetActive (true);
            dicMenuList ["menututor_lineup"].SetActive (true);
        }
        FriendListUpdate = LobbyPlayAniFlag = false;
        SendWasCardupdate ();

        mMenuName = "Btn_Fun_GotoLineup";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;
        }
        CereMonyPreviewClose ();
        LineUpsubmenuclose ();
        dicMenuList ["Ui_team"].SetActive (true);
        dicMenuList ["Ui_lobby"].SetActive (false);
        dicMenuList ["Ui_kickoff"].SetActive (false);
        dicMenuList ["Panel_teamback"].SetActive (true);
        dicMenuList ["LPanel_lineup"].SetActive (true);
        dicMenuList ["MainCamera"].SetActive (false);
        dicMenuList ["checkbox0_lineup"].GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["checkbox0_lineup"].GetComponent<UICheckbox> ().Set (true);

        //DestoryAllCard ();
        CardLineup = true;
        AllCardSelect ();
        StartCoroutine (WaitanLineupRePositionSetting ());
        //LineUpNationFlagEffOnOff ();
        ShowCardEff ();
        SortBygrade = Sortbyflag = false;
    }

    IEnumerator WaitanLineupRePositionSetting ()
    {
        yield return new WaitForSeconds (0.2f);
        float Card_characterPosY, Card_characterPosZ;
        Card_characterPosY = FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character", true).transform.localPosition.y;
        Card_characterPosZ = FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character", true).transform.localPosition.z;
        
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character", true).transform.localPosition = new Vector3 (0, Card_characterPosY, Card_characterPosZ);
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character", true).GetComponent<UIDraggablePanel> ().repositionClipping = true;
        
        //Debug.Log ("CardMix + character X "+FindMyChild(dicMenuList ["Ui_team"],"LPanel_lineup/card_character", true).transform.position.x);
    }

    void Btn_Fun_LineupClose ()
    {

        SortBtnInitSet ();
        SendWasCardupdate ();

        LobbyPlayAniFlag = true;
        mMenuName = "Btn_Fun_LineupClose";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;
        }
        //DestoryAllCard ();
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (true);
        dicMenuList ["MainCamera"].SetActive (true);
        //StNet.UpdateAllCards (Ag.mySelf);
        Ag.mySelf.SetCostumeToCard ();


    }

    void Btn_Fun_UniformEditOpen ()
    {
        SendWasCardupdate ();
        Ag.Uniform = true;

        Update ();
        //DestoryAllCard ();
        LineUpsubmenuclose ();
        dicMenuList ["LPanel_uniform"].SetActive (true);
        Btn_Fun_KickerMode ();

        dicMenuList ["Menu_btn_tutorial"].SetActive (false);
        dicMenuList ["Menu_btn_book"].SetActive (false);
        /*
        for (int i = 0; i < Ag.mySelf.arrUniform.Count; i++) {
            Debug.Log (Ag.mySelf.arrUniform.Count);
            Debug.Log (Ag.mySelf.arrUniform[i].WAS.itemTypeId);
            Debug.Log (Ag.mySelf.arrUniform[i].WAS.applyFlag);
            Debug.Log (Ag.mySelf.arrUniform[i].WAS.colorInfo);
            Debug.Log (Ag.mySelf.arrUniform[i].WAS.textureInfo);
            Debug.Log (Ag.mySelf.arrUniform[i].Kick.Pants.Texture);
            Debug.Log (Ag.mySelf.arrUniform[i].Kick.Pants.Color);
        }
        */

    }

    void Btn_Fun_PlayerBookOpen () {

        MenuCommonOpen ("LPanel_playerbook", "Ui_popup");


    }
    void Btn_Fun_PlayerBookClose () {
        //dicMenuList ["CenterCircle"].SetActive (false);
        MenuCommonOpen ("LPanel_playerbook", "Ui_popup", false);
    }


    void Btn_Fun_TutorialOpen ()
    {
        switch (mMenuName) {
        case "Btn_Fun_GotoLineup":
            dicMenuList ["Ui_menututorial"].SetActive (true);
            dicMenuList ["menututor_lineup"].SetActive (true);
            break;
        case "Btn_Fun_CardMixOpen":
            dicMenuList ["Ui_menututorial"].SetActive (true);
            dicMenuList ["menututor_cardmix"].SetActive (true);
            break;
        }
    }

    void Btn_Fun_DirectorModeOpen ()
    {
        SortBtnInitSet();
        SendWasCardupdate ();
        mMenuName = "Btn_Fun_DirectorModeOpen";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;
        }
        dicMenuList ["Menu_btn_tutorial"].SetActive (false);
        dicMenuList ["Menu_btn_book"].SetActive (false);
        //Update ();
        //DestoryAllCard ();
        LineUpsubmenuclose ();
        dicMenuList ["LPanel_coach"].SetActive (true);
        if (Ag.mGuest) {
            dicMenuList ["Label_coachname"].GetComponent<UILabel> ().text = "No name";
        } else {
            dicMenuList ["Label_coachname"].GetComponent<UILabel> ().text = StcPlatform.PltmNick;
        }
        dicMenuList ["Label_nations"].GetComponent<UILabel> ().text = Ag.mCountryData.SetNationFlag (Ag.mySelf.WAS.Country);
        dicMenuList ["Label_teamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
        dicMenuList ["Label1_Allrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNumWeek + Ag.mySelf.myRank.WAS.lossNumWeek + "전" + Ag.mySelf.myRank.WAS.winNumWeek + "승" + Ag.mySelf.myRank.WAS.lossNumWeek + "패";
        dicMenuList ["Label2_WinRate"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.weekScore.ToString () + "점";
        dicMenuList ["Label3_TopPoint"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.thisWeekRank.ToString () + "위";
        dicMenuList ["Label4_TopWinNum"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.lastWeekRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.lastWeekRank.ToString () + "위";
        //dicMenuList ["Coach_KakaoFace"].GetComponent<UITexture> ().material = mMaterial;
        dicMenuList ["Label1_totalrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNum + Ag.mySelf.myRank.WAS.lossNum + "전" + Ag.mySelf.myRank.WAS.winNum + "승" + Ag.mySelf.myRank.WAS.lossNum + "패";
        dicMenuList ["Label3_totalTopPoint"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScoreRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.bestScoreRank.ToString () + "위";
        dicMenuList ["Label2_totalWinRate"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScore.ToString () + "점";
    }

    void Btn_Fun_CardMixOpen ()
    {
        SortBygrade = Sortbyflag = false;

        InitMenuTutor ();
        SortBtnInitSet();

        if (!PreviewLabs.PlayerPrefs.GetBool ("MenuTutorCardMix")) {
            dicMenuList ["Ui_menututorial"].SetActive (true);
            dicMenuList ["menututor_cardmix"].SetActive (true);
        }
        dicMenuList ["Menu_btn_tutorial"].SetActive (true);

        SendWasCardupdate ();
        mMenuName = "Btn_Fun_CardMixOpen";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;
        }
        //Update ();
        MixLuck = false;
        MixSuper = false;

        CardLineup = false;
        LineUpsubmenuclose ();
        dicMenuList ["LPanel_cardmix"].SetActive (true);
        dicMenuList ["Menu_btn_book"].SetActive (false);

        DestoryAllCard ();
        MIXCARDSELECT ();
        MixItemSetting ();
        MixItemInit ();
    }

    void Btn_Fun_BuyCard ()
    {
        SortBtnInitSet();
        Debug.Log ("ClickedBuyCard");
        SendWasCardupdate ();
        mMenuName = "Btn_Fun_BuyCard";
        if (Ag.Uniform) {
            Ag.Uniform = false;
            if (!PutonNotbuyUniform ())
                return;

        }
        dicMenuList ["Menu_btn_tutorial"].SetActive (false);
        dicMenuList ["Menu_btn_book"].SetActive (true);

        //Update ();
        LineUpsubmenuclose ();
        dicMenuList ["LPanel_buycard"].SetActive (true);

        if (ItemEventOnCheck ("Normal"))
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_eventpricenormal").gameObject.SetActive (true);
        if (ItemEventOnCheck ("Abnomal"))
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_eventpricesuper").gameObject.SetActive (true);
        if (ItemEventOnCheck ("KLeague"))
            dicMenuList ["Ui_team"].transform.FindChild ("LPanel_buycard/grid_eventpricekleague").gameObject.SetActive (true);

        ItemEventOnCheck ("Normal", FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_txteventnormal", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_cutpricenormal", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_eventpricenormal/Label_1", true));
        ItemEventOnCheck ("Abnomal", FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_txteventsuper", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_cutpricesuper", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_eventpricesuper/Label_1", true));
        ItemEventOnCheck ("KLeague", FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_txteventkleague", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_cutpricekleague", true), FindMyChild (dicMenuList ["Ui_team"], "LPanel_buycard/grid_eventpricekleague/Label_1", true));

        //DestoryAllCard ();


    }
    //  ////////////////////////////////////////////////     ////////////////////////
    void GoHome ()
    {
        OpenBoxAndCloseBox (dicMenuList ["RankBox"]);
    }
    //  ////////////////////////////////////////////////     ////////////////////////     ShopTabBar
    //  ////////////////////////////////////////////////     ////////////////////////     LineUp
    void LineUpOpen ()
    {

    }

    public bool LineUpCardFlag = false, CardMixFlag = false;
    //  ////////////////////////////////////////////////     ////////////////////////     BuyCard
    //  ////////////////////////////////////////////////     ////////////////////////     Card Mix
    //  ////////////////////////////////////////////////     ////////////////////////     BuyCard
    //  ////////////////////////////////////////////////     ////////////////////////    TeamNameEdit
    void ComeBackMenu ()
    {
    }
    //  ////////////////////////////////////////////////     ////////////////////////     OpenBox CloseBox Procedure
    void OpenBoxAndCloseBox (GameObject pNewBox)
    {
        if (pNewBox == mPresentOpenBox) {
            pNewBox.SetActive (true);
            return;
        } else {
            if (mPresentOpenBox != null) {
                mPresentOpenBox.animation.Play ("sidepanel2");
                //StartCoroutine (TopMenuCloseCoru(mPresentOpenBox));
                mPresentOpenBox.SetActive (false);
            }
            mPresentOpenBox = pNewBox;
        }
        pNewBox.SetActive (true);
        pNewBox.animation.Play ();
    }
    //  ////////////////////////////////////////////////     ////////////////////////     TeamManager TabBar Function
    void TeamManagerTabBar (GameObject pNewBox)
    {

    }
    //  ////////////////////////////////////////////////     ////////////////////////     MatchQuit Function
    //  ////////////////////////////////////////////////     ////////////////////////     CloseNoticePanel Function
    void CloseNoticePanel ()
    {
       
    }
    //  ////////////////////////////////////////////////     ////////////////////////     KickOff DrinkItem
    void KickerNumNameEdit ()
    {

    }

    void KeeperNumNameEdit ()
    {

    }

    void NumNameEditOk ()
    {

    }

    void NumNameEditClose ()
    {

    }

    Texture2D mTex;
    AmCard mCardInfo;

    IEnumerator CaptureImage ()
    {

        Camera owner = mRscrcMan.FindGameObject ("RenderCamera", true).gameObject.GetComponent< Camera > ();
        mRscrcMan.FindGameObject ("RenderCamera/Name", true).gameObject.GetComponent<TextMesh> ().text = WWW.UnEscapeURL (mCardInfo.WAS.playerName);
        mRscrcMan.FindGameObject ("RenderCamera/Num", true).gameObject.GetComponent<TextMesh> ().text = mCardInfo.WAS.backNum.ToString ();
        //mRscrcMan.FindGameObject ("RenderCamera/Name", true).gameObject.GetComponent<TextMesh> ().color = Tbl.dicDeckBacRgbCode [mCardInfo.WAS.country];
        //mRscrcMan.FindGameObject ("RenderCamera/Num", true).gameObject.GetComponent<TextMesh> ().color = Tbl.dicDeckBacRgbCode [mCardInfo.WAS.country];
        RenderTexture target = owner.targetTexture;

        // wait for end of current frame
        yield return new WaitForEndOfFrame ();
        //      if (target == null)
        //          Debug.Log ("Camera Is Null");
//        Debug.Log ("Target.Widht" + target.width + "Target.Height" + target.height);
        // create texture to hold image
        mTex = new Texture2D (target.width, target.height, TextureFormat.ARGB32, false);
        RenderTexture.active = owner.targetTexture;
        owner.Render ();
        // capture the image
        mTex.ReadPixels (new Rect (0, 0, target.width, target.height), 0, 0, false);
        RenderTexture.active = null;
        mTex.Apply ();
        FindMyChild (mKicker, "Clothes", true).gameObject.transform.renderer.sharedMaterials [0].SetTexture ("_DecalTex", mTex);

    }

    IEnumerator CaptureImage (GameObject Gobj)
    {

        Camera owner = mRscrcMan.FindGameObject ("RenderCamera", true).gameObject.GetComponent< Camera > ();
        mRscrcMan.FindGameObject ("RenderCamera/Name", true).gameObject.GetComponent<TextMesh> ().text = WWW.UnEscapeURL (Ag.mySelf.GetCardOrderOf(1).WAS.playerName);
        mRscrcMan.FindGameObject ("RenderCamera/Num", true).gameObject.GetComponent<TextMesh> ().text = Ag.mySelf.GetCardOrderOf(1).WAS.backNum.ToString ();
        RenderTexture target = owner.targetTexture;
        yield return new WaitForEndOfFrame ();

        mTex = new Texture2D (target.width, target.height, TextureFormat.ARGB32, false);
        RenderTexture.active = owner.targetTexture;
        owner.Render ();
        // capture the image
        mTex.ReadPixels (new Rect (0, 0, target.width, target.height), 0, 0, false);
        RenderTexture.active = null;
        mTex.Apply ();
        FindMyChild (Gobj, "Clothes", true).gameObject.transform.renderer.sharedMaterials [0].SetTexture ("_DecalTex", mTex);

    }


    // end CaptureImage
    //  ////////////////////////////////////////////////     ////////////////////////  PlayerDetailInfo
    void Btn_Fun_Kicker_conditionUP ()
    {
        /*
        if (mwas.condition == 2) {
            StartCoroutine(MaxCondition(dicMenuList["KickerPop_condition"]));
            return;
        }
        */
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_condition");
    }

    void Btn_Fun_Kicker_conditionok ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);
        WasCardEnchantRecover aObj = new WasCardEnchantRecover () {
            User = Ag.mySelf, code = 254, cardID = mwas.ID
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:
            case 0:
                Userinfo ();
                //dicMenuList ["alert"].SetActive (false);
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
        };

        MenuCommonOpen ("Kicker_popup", "Kicker_popup_condition", false);

    }

    void Btn_Fun_Kicker_conditionClose ()
    {

        MenuCommonOpen ("Kicker_popup", "Kicker_popup_condition", false);

    }

    void Btn_Fun_Kicker_dresseditem ()
    {
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_item");
        //dicMenuList ["Kicker_popup_item"].transform.FindChild ("glove").transform.gameObject.SetActive (false);
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes").transform.gameObject.SetActive (true);
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes01").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes02").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes03").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes04").ToString ();

        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes01").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes02").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes03").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes04").ToString ();

        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/Label_discript").GetComponent<UILabel> ().text = "가산점 + 2%";
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/Label_discript").GetComponent<UILabel> ().text = "가산점 + 4%";
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/Label_discript").GetComponent<UILabel> ().text = "가산점 + 6%";
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/Label_discript").GetComponent<UILabel> ().text = "가산점 + 8%";

        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choicenot", false);

        Btn_fun_EquipShose1 ();





        ItemListKicker ("02");
        ItemListKicker ("03");
        ItemListKicker ("04");
        ItemListKicker ("01");

    }

    string Shoes;

    void ItemListKicker (string id)
    {
        ItemEventOnCheck ("KickerShoes01", dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/txtevent").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/cutline").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/Label_eventprice").gameObject);
        ItemEventOnCheck ("KickerShoes02", dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/txtevent").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/cutline").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/Label_eventprice").gameObject);
        ItemEventOnCheck ("KickerShoes03", dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/txtevent").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/cutline").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/Label_eventprice").gameObject);
        ItemEventOnCheck ("KickerShoes04", dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/txtevent").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/cutline").gameObject, dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/Label_eventprice").gameObject);

        if (CostumeListEa ("KickerShoes" + id) > 0) {
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Label_price").gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Sprite (icon_gold)").gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Label_txt").gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/txtevent").gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/cutline").gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Label_eventprice").gameObject.SetActive (false);

            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_buy", false);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_choice", true);


        } else {
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Label_price").gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Sprite (icon_gold)").gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item" + id + "/Label_txt").gameObject.SetActive (false);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_buy", true);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/choicebtngrid/btn_choice", false);
        }
    }

    void Btn_fun_EquipShose1 ()
    {

        ItemListKicker ("01");
        Shoes = "KickerShoes01";
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item01", true).GetComponent<UICheckbox> ().isChecked = true;
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item01", true).GetComponent<UICheckbox> ().Set (true);

    }

    void Btn_fun_EquipShose2 ()
    {
        ItemListKicker ("02");
        Shoes = "KickerShoes02";
    }

    void Btn_fun_EquipShose3 ()
    {
        ItemListKicker ("03");
        Shoes = "KickerShoes03";
    }

    void Btn_fun_EquipShose4 ()
    {
        ItemListKicker ("04");
        Shoes = "KickerShoes04";
    }

    void Btn_Fun_Kicker_dresseditemOk ()
    {


        dicMenuList ["Kicker_popup_itemalert"].SetActive (true);
        ShoesItem (Shoes);
        //MenuCommonOpen ("Kicker_popup", "Kicker_popup_item", false);
    }

    void ShoesItem (string Shoes)
    {
        dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item01").transform.gameObject.SetActive (false);
        dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item02").transform.gameObject.SetActive (false);
        dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item03").transform.gameObject.SetActive (false);
        dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item04").transform.gameObject.SetActive (false);

        for (int i = 0; i < mCard.arrCostumeInCard.Count; i++) {
            if (mCard.arrCostumeInCard[i].WAS.itemTypeId == Shoes) {
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choicenot", true);
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choice", false);
            } else {
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choicenot", false);
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_itemalert/choicebtngrid/btn_choice", true);
            }
        }


        if (Shoes == "KickerShoes01") {
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item01").transform.gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item01/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 2%";
        }
        if (Shoes == "KickerShoes02") {
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item02").transform.gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item02/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 4%";
        }
        if (Shoes == "KickerShoes03") {
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item03").transform.gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item03/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 6%";
        }
        if (Shoes == "KickerShoes04") {
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item04").transform.gameObject.SetActive (true);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("item/item04/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 8%";
        }
    }

    void Btn_Fun_Kicker_dresseditemClose ()
    {
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_item", false);
    }

    void KeeperCostumeEquip ()
    {
        //if (mCard.arrCostumeInCard [0].WAS.itemTypeId == Glove) return;

        dicMenuList ["CenterCircle"].SetActive (true);
        dicMenuList ["Keeper_popup_itemalert"].SetActive (false);
        CostumeChoice (Glove, mwas.ID);

        WasCostumeUpdate aObj = new WasCostumeUpdate () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 
            case 0:
                Userinfo ();
                Ag.LogString (" result : Success ");
                break;
            }
        };
    }

    void KeeperCostumeEquipCancel ()
    {
        dicMenuList ["Keeper_popup_itemalert"].SetActive (false);
    }

    void KickerCostumeEquip ()
    {

        //if (mCard.arrCostumeInCard [0].WAS.itemTypeId == Shoes) return;

        CostumeChoice (Shoes, mwas.ID);
        dicMenuList ["CenterCircle"].SetActive (true);
        WasCostumeUpdate aObj = new WasCostumeUpdate () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                Userinfo ();
                Ag.LogString (" result : Success ");
                break;
            }
        };
        dicMenuList ["Kicker_popup_itemalert"].SetActive (false);


    }

    void KickerCostumeEquipCancel ()
    {
        dicMenuList ["Kicker_popup_itemalert"].SetActive (false);
    }

    void Btn_Fun_Kicker_enchantPlayer ()
    {

        if (mwas.level == 10) {
            StartCoroutine (MAXLEVEL ());
            return;
        }
        LevelUpPrice (mwas.level, true);
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_training");
        dicMenuList ["Kicker_training_result"].SetActive (false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("effect").gameObject.SetActive(false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("fail").gameObject.SetActive(false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("success").gameObject.SetActive(false);


    }

    IEnumerator KickerTraining_Result (bool Success) {
        dicMenuList ["Kicker_popup"].SetActive (true);
        dicMenuList ["Kicker_training_result"].SetActive (true);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("effect").gameObject.SetActive (true);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("sprite").gameObject.SetActive (true);
        yield return new WaitForSeconds (3.5f);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("effect").gameObject.SetActive (false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("sprite").gameObject.SetActive (false);
        if (Success) {
            dicMenuList ["Kicker_training_result"].transform.FindChild ("success").gameObject.SetActive(true);
        } else {
            dicMenuList ["Kicker_training_result"].transform.FindChild ("fail").gameObject.SetActive(true);
        }

        yield return new WaitForSeconds (2f);
        //dicMenuList ["Kicker_popup"].SetActive (false);
        dicMenuList ["Kicker_training_result"].SetActive (false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("success").gameObject.SetActive(false);
        dicMenuList ["Kicker_training_result"].transform.FindChild ("fail").gameObject.SetActive(false);
        mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_training", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_cancle", true);
    }

    void Btn_Fun_Kicker_enchantPlayerok ()
    {


        mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_training", false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_cancle", false);

        dicMenuList ["CenterCircle"].SetActive (true);
        WasCardLevelup aObj = new WasCardLevelup () {
            User = Ag.mySelf, cardID = mwas.ID //, buyType = Buytype
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:

                StartCoroutine(KickerTraining_Result(true));
                mCard.WAS.ResetWidthAndSkill ();
                LevelUpPrice (mwas.level, true);
                if (mwas.level == 10) MenuCommonOpen ("Kicker_popup", "Kicker_popup_training", false);
                Userinfo ();
                SendWasCardupdate ();
                Ag.LogString (" result : Success " + "MWAS.LEVEL" + mwas.level);



                return;
            case -1:
                //StartCoroutine(Training_Result(false));
                if (Ag.mySelf.GetBuyType ("FuncLevelUp" + (mwas.level + 1)) == 0) {
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                } else {
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                }
                mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_training", true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_training/btngrid1/btn_cancle", true);
                break;
            case 4:
                StartCoroutine(KickerTraining_Result(false));
                break;
            }
        };
        //MenuCommonOpen ("Kicker_popup", "Kicker_popup_training", );
    }

    void Btn_Fun_Kicker_enchantPlayerclose ()
    {

        MenuCommonOpen ("Kicker_popup", "Kicker_popup_training", false);
    }

    void Btn_Fun_Kicker_PlayerNameEdit ()
    {
        MenuCommonOpen ("Kicker_popup", "Kicker_popup_editplayername");
        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = "";
    }

    void PlayerNameInit () {
        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = "";
        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = "";
    }

    void Btn_Fun_Kicker_PlayerNameEditOk ()
    {
        string Playername, BackNum;
        dicMenuList ["CenterCircle"].SetActive (true);

        Playername = WWW.EscapeURL (dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Input_editname").GetComponent<UIInput> ().text);
        BackNum = dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Input_editnumber").GetComponent<UIInput> ().text;
        //Debug.Log ("BackNum" + BackNum);
        WasFuncBackNumEdit aObj = new WasFuncBackNumEdit () {

            User = Ag.mySelf, cardID = mwas.ID,
            backNum = mwas.backNum,//int.Parse (BackNum),
            playerName = Playername, //buyType = 1

        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                Userinfo ();
                //dicMenuList ["alert"].SetActive (true);
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;

            case 4:
                dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                break;
            case 5:
                dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                break;
            }
        };
    }

    void Btn_Fun_Kicker_PlayerNameEditClose ()
    {

        MenuCommonOpen ("Kicker_popup", "Kicker_popup_editplayername", false);
    }

    /// <summary>
    /// 컨디션올리는 액션을 수행하는 함수
    /// </summary>
    void Btn_Fun_Keeper_conditionUP ()
    {
        if (mwas.condition == 2) {
            StartCoroutine (MaxCondition (dicMenuList ["KeeperPop_condition"]));
            return;
        }
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_condition");

    }

    void Btn_Fun_Keeper_conditionUPok ()
    {

        dicMenuList ["CenterCircle"].SetActive (true);
        WasCardEnchantRecover aObj = new WasCardEnchantRecover () {
            User = Ag.mySelf, code = 254, cardID = mwas.ID
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                Userinfo ();
                //dicMenuList ["alert"].SetActive (true);
                break;
            case -1:
                //Debug.Log ("Conditionup Failed");
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                MenuCommonOpen ("Kicker_popup", "Kicker_popup_editplayername", false);
                break;

            }
        };
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_condition", false);
    }

    void Btn_Fun_Keeper_conditionUPClose ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_condition", false);
    }

    void Btn_Fun_Keeper_dresseditem ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_item");
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choicenot", false);

//        dicMenuList ["Keeper_popup_item"].transform.FindChild ("shoes").transform.gameObject.SetActive (false);
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove").transform.gameObject.SetActive (true);
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves01").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves02").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves03").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves04").ToString ();

        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves01").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves02").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves03").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves04").ToString ();

        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/Label_discript").GetComponent<UILabel> ().text = "가산점 + 2%";
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/Label_discript").GetComponent<UILabel> ().text = "가산점 + 4%";
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/Label_discript").GetComponent<UILabel> ().text = "가산점 + 6%";
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/Label_discript").GetComponent<UILabel> ().text = "가산점 + 8%";
        Btn_fun_EquipGlove1 ();




        ItemListKeeper ("04");
        ItemListKeeper ("03");
        ItemListKeeper ("02");
        ItemListKeeper ("01");

    }

    string Glove;

    void ItemListKeeper (string id)
    {
        ItemEventOnCheck ("KeeperGloves01", dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/txtevent").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/cutline").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/Label_eventprice").gameObject);
        ItemEventOnCheck ("KeeperGloves02", dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/txtevent").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/cutline").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/Label_eventprice").gameObject); 
        ItemEventOnCheck ("KeeperGloves03", dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/txtevent").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/cutline").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/Label_eventprice").gameObject); 
        ItemEventOnCheck ("KeeperGloves04", dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/txtevent").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/cutline").gameObject, dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/Label_eventprice").gameObject); 

        if (CostumeListEa ("KeeperGloves" + id) > 0) {
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Label_price").gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Sprite (icon_gold)").gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Label_txt").gameObject.SetActive (true);

            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/txtevent").gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/cutline").gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Label_eventprice").gameObject.SetActive (false);

            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_buy", false);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_choice", true);


        } else {
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Label_price").gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Sprite (icon_gold)").gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item" + id + "/Label_txt").gameObject.SetActive (false);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_buy", true);
            FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/choicebtngrid/btn_choice", false);
        }
    }

    void Btn_fun_EquipGlove1 ()
    {
        ItemListKeeper ("01");
        Glove = "KeeperGloves01";
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item01", true).GetComponent<UICheckbox> ().isChecked = true;
        FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item01", true).GetComponent<UICheckbox> ().Set (true);

    }

    void Btn_fun_EquipGlove2 ()
    {
        ItemListKeeper ("02");
        Glove = "KeeperGloves02";
    }

    void Btn_fun_EquipGlove3 ()
    {
        ItemListKeeper ("03");
        Glove = "KeeperGloves03";
    }

    void Btn_fun_EquipGlove4 ()
    {
        ItemListKeeper ("04");
        Glove = "KeeperGloves04";
    }

    void CostumeChoice (string id, int Cardid)
    {
        for (int i = 0; i < Ag.mySelf.arrCostume.Count; i++) {
            if (Ag.mySelf.arrCostume [i].WAS.itemTypeId == id && Ag.mySelf.arrCostume [i].WAS.cardId == -1) {
                Ag.mySelf.arrCostume [i].WAS.cardId = Cardid;
                break;
            }
        }
    }

    void Btn_Fun_Keeper_dresseditemchoice ()
    {

        dicMenuList ["Keeper_popup_itemalert"].SetActive (true);

        GlovesItem (Glove);
    }

    void GlovesItem (string Gloves)
    {
        dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item01").transform.gameObject.SetActive (false);
        dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item02").transform.gameObject.SetActive (false);
        dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item03").transform.gameObject.SetActive (false);
        dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item04").transform.gameObject.SetActive (false);

        for (int i = 0; i < mCard.arrCostumeInCard.Count; i++) {
            if (mCard.arrCostumeInCard[i].WAS.itemTypeId == Gloves) {
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choicenot", true);
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choice", false);
            } else {
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choice", true);
                FindMyChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_itemalert/choicebtngrid/btn_choicenot", false);
            }
        }




        if (Gloves == "KeeperGloves01") {
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item01").transform.gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item01/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 2%";
        }
        if (Gloves == "KeeperGloves02") {
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item02").transform.gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item02/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 4%";
        }
        if (Gloves == "KeeperGloves03") {
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item03").transform.gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item03/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 6%";
        } 
        if (Gloves == "KeeperGloves04") {
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item04").transform.gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("item/item04/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 8%";
        }
    }

    void Btn_Fun_Keeper_dresseditemBuy ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);
        MenuCommonOpen ("Ui_Popup", "buy_item", false);
        WasPurchaseCostume aObj = new WasPurchaseCostume () { User = Ag.mySelf, costumeName = Glove }; // , buyType = 1  };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
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

        //MenuCommonOpen ("Keeper_popup", "Keeper_popup_item", false);
    }

    void Btn_Fun_Keeper_dresseditemClose ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_item", false);
    }

    void Btn_Fun_Keeper_enchantPlayer ()
    {

        if (mwas.level == 10) {
            StartCoroutine (MAXLEVEL ());
            return;
        }

        MenuCommonOpen ("Keeper_popup", "Keeper_popup_training");
        dicMenuList ["Keeper_training_result"].SetActive (false);
        LevelUpPrice (mwas.level, false);
        dicMenuList ["Keeper_training_result"].SetActive (false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("effect").gameObject.SetActive(false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("fail").gameObject.SetActive(false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("success").gameObject.SetActive(false);


    }
    IEnumerator KeeperTraining_Result (bool Success) {
        dicMenuList ["Keeper_popup"].SetActive (true);
        dicMenuList ["Keeper_training_result"].SetActive (true);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("effect").gameObject.SetActive (true);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("sprite").gameObject.SetActive (true);
        yield return new WaitForSeconds (3.5f);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("effect").gameObject.SetActive (false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("sprite").gameObject.SetActive (false);
        if (Success) {
            dicMenuList ["Keeper_training_result"].transform.FindChild ("success").gameObject.SetActive(true);
        } else {
            dicMenuList ["Keeper_training_result"].transform.FindChild ("fail").gameObject.SetActive(true);
        }

        yield return new WaitForSeconds (2f);
        //dicMenuList ["Keeper_popup"].SetActive (false);
        dicMenuList ["Keeper_training_result"].SetActive (false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("success").gameObject.SetActive(false);
        dicMenuList ["Keeper_training_result"].transform.FindChild ("fail").gameObject.SetActive(false);
        mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_training", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_cancle", true);
    }



    void Btn_Fun_Keeper_enchantPlayerok ()
    {

        mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_training", false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_cancle", false);
        dicMenuList ["CenterCircle"].SetActive (true);
        int Buytype = 1;
        if (mwas.level == 5 || mwas.level == 9)
            Buytype = 0;
        //MenuCommonOpen ("Keeper_popup", "Keeper_popup_training", false);
        WasCardLevelup aObj = new WasCardLevelup () {
            User = Ag.mySelf, cardID = mwas.ID //, buyType = Buytype
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                Userinfo ();
                LevelUpPrice (mwas.level, false);
                if (mwas.level == 10) MenuCommonOpen ("Keeper_popup", "Keeper_popup_training", false);

                StartCoroutine(KeeperTraining_Result(true));
                //dicMenuList ["alert"].SetActive (true);
                Ag.LogString (" result : Success "); 
                break;

            case -1:
                //Debug.Log ("Enchant Failed");
                if (Ag.mySelf.GetBuyType ("FuncLevelUp" + (mwas.level + 1)) == 0) {
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                } else {
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                }
                mRscrcMan.FindChild(dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_training", true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_training/btngrid1/btn_cancle", true);
                break;
            case 4:
                StartCoroutine(KeeperTraining_Result(false));
                break;
            }
        };

    }

    void Btn_Fun_Keeper_enchantPlayerClose ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_training", false);

    }

    void Btn_Fun_Keeper_PlayerNameEdit ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_editplayername", true);
        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = "";
    }

    void Btn_Fun_Keeper_PlayerNameEditOk ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);
        string Playername, backNum;
        Playername = WWW.EscapeURL (dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Input_editname").GetComponent<UIInput> ().text);
        //backNum = mwas.backNum;

        WasFuncBackNumEdit aObj = new WasFuncBackNumEdit () {

            User = Ag.mySelf, cardID = mwas.ID,
            backNum = mwas.backNum,
            playerName = Playername

        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                //dicMenuList ["alert"].SetActive (true);
                Userinfo ();
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Keeper_popup", "Keeper_popup_editplayername", false);
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            case 4:
                dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                break;
            case 5:
                dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_alerterror").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                break;

            }
        };


    }

    void Btn_Fun_Keeper_PlayerNameEditClose ()
    {
        MenuCommonOpen ("Keeper_popup", "Keeper_popup_editplayername", false);
    }

    /// <summary>
    /// Coach
    /// </summary>
    int mCountry = 0;
    string newTeamname = "";

    void Btn_Fun_teamnameedit ()
    {
        mCountry = Ag.mySelf.WAS.Country;
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = "";
        dicMenuList ["popup_teamnameedit"].SetActive (true);
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_teamname").GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("nations/scroll_flag").transform.localPosition = new Vector3 (0, 0, -10);
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("nations/scroll_flag").GetComponent<UIPanel> ().clipRange = new Vector4 (0, 0, 220, 210);
        FlagMove ();
    }

    void Btn_Fun_ScrollMoveLeft ()
    {
        if (mCountry >= 0 && mCountry <= 30) {
            mCountry++;     
        }
        FlagMove ();
    }

    void Btn_Fun_ScrollMoveRight ()
    {
        if (mCountry > 0 && mCountry <= 31) {
            mCountry--;     
        }
        FlagMove ();
    }

    void FlagMove ()
    {
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("nations/scroll_flag").GetComponent<SpringPanel> ().target.x = mCountry * -240;
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("nations/scroll_flag").GetComponent<SpringPanel> ().enabled = true;
        dicMenuList ["popup_teamnameedit"].transform.FindChild ("nations/Label_nationcount").gameObject.GetComponent<UILabel> ().text = "(" + (mCountry + 1).ToString () + "/32)";
    }

    void wasTeamNamCheck ()
    { 

        if (newTeamname.Length > 9) {
            //Debug.Log ("new TeamName" + newTeamname.Length);
            dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%B4%20%EC%9D%B4%EB%A6%84%EC%9D%84%20%EC%93%B0%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
            return;
        }

        WasTeamCheck aObj = new WasTeamCheck () { ID = Ag.mySelf.WAS.KkoID, TgtName = WWW.EscapeURL (newTeamname) };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
            case 1:

                WaSTeamEdit (mCountry, WWW.EscapeURL (newTeamname));
                //dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = "";
                break;
            case -1:
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%A4%91%EB%B3%B5%EB%90%9C%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                ;
                break;
            case -2:
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%B4%20%EC%9D%B4%EB%A6%84%EC%9D%84%20%EC%93%B0%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
                break;
            }
        };
    }

    void WaSTeamEdit (int Country, string newTeamname)
    {
        if (newTeamname == "") {
            dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%B4%20%EC%9D%B4%EB%A6%84%EC%9D%84%20%EC%93%B0%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
            return;
        }

        dicMenuList ["popup_teamnameedit"].SetActive (false);
        WasFuncTeamEdit aObj = new WasFuncTeamEdit () {
            User = Ag.mySelf, nuTeamName = newTeamname, mCountry = Country
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                //PopupAfterUserCash();
                Ag.mySelf.WAS.Country = mCountry;
                Ag.LogString (" result : Success ");
                Userinfo ();
                break;
            case -1:
                dicMenuList ["popup_teamnameedit"].SetActive (true);
                Ag.LogString (" result : SamName ");
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%A4%91%EB%B3%B5%EB%90%9C%20%EC%9D%B4%EB%A6%84%EC%9E%85%EB%8B%88%EB%8B%A4.");
                break;
            case -2:
                dicMenuList ["popup_teamnameedit"].SetActive (true);
                Ag.LogString (" result : Dont Use This Name ");
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%B4%20%EC%9D%B4%EB%A6%84%EC%9D%84%20%EC%93%B0%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
                break;
            case 2:
                dicMenuList ["popup_teamnameedit"].SetActive (true);
                Ag.LogString (" result : BuyTypeError ");
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EA%B5%AC%EB%A7%A4%20%ED%83%80%EC%9E%85%20%EC%97%90%EB%9F%AC");
                break;
            case 1:
                Ag.LogString (" result : No money ");
                dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Label_alert").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%BA%90%EC%89%AC%EA%B0%80%20%EB%B6%80%EC%A1%B1%ED%95%A9%EB%8B%88%EB%8B%A4.");
                MenuCommonOpen ("Ui_popup", "havenotcash", true);

                break;

            }
        };
    }

    void Btn_Fun_teamnameeditOk ()
    {
        newTeamname = dicMenuList ["popup_teamnameedit"].transform.FindChild ("club/Input_name").GetComponent<UIInput> ().text;
        WaSTeamEdit (mCountry , WWW.EscapeURL(newTeamname));

        //WaSTeamEdit (mCountry, newTeamname);
        //wasTeamNamCheck ();


    }

    void Btn_Fun_teamnameeditCancel ()
    {
        dicMenuList ["popup_teamnameedit"].SetActive (false);
    }

    void Btn_Fun_btn_recordrefresh ()
    {
        Userinfo ();
        StartCoroutine (recordrefresh (60));
    }

    public IEnumerator recordrefresh (float pTime)
    {
        int Time = 0;
        dicMenuList ["btn_recordrefresh"].collider.enabled = false;
        dicMenuList ["btn_recordrefresh"].transform.FindChild ("Background").gameObject.SetActive (false);
        dicMenuList ["btn_recordrefresh"].transform.FindChild ("Background2").gameObject.SetActive (true);

        while (Time < 60) {
            //Debug.Log ("INcoming");
            yield return new WaitForSeconds (1);
            Time++;
            if ((60 - Time) >= 10)
                dicMenuList ["Coach_Label_refreshtime"].GetComponent<UILabel> ().text = "00:" + (60 - Time).ToString ();
            else
                dicMenuList ["Coach_Label_refreshtime"].GetComponent<UILabel> ().text = "00:0" + (60 - Time).ToString ();
        }

        dicMenuList ["btn_recordrefresh"].collider.enabled = true;
        dicMenuList ["btn_recordrefresh"].transform.FindChild ("Background").gameObject.SetActive (true);
        dicMenuList ["btn_recordrefresh"].transform.FindChild ("Background2").gameObject.SetActive (false);


    }

    void Btn_Fun_recordinitialization ()
    {
        dicMenuList ["popup_recordinitialization"].SetActive (true);
    }

    void Btn_Fun_recordinitializationCancel ()
    {
        dicMenuList ["popup_recordinitialization"].SetActive (false);
    }

    void Btn_Fun_recordinitializationOk ()
    {

        WasFuncInitRank aObj = new WasFuncInitRank () { User = Ag.mySelf  };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                dicMenuList ["popup_recordinitialization"].SetActive (false);
                Ag.LogIntenseWord (" result : Success ");
                //PopupAfterUserCash();
                //RanKuser();
                Userinfo ();
                break;
            case -1:
                dicMenuList ["popup_recordinitialization"].SetActive (false);
                MenuCommonOpen ("Ui_popup", "havenotcash", true);
                break;
            }
        };
    }

    void TeamManagerShopClose ()
    {

    }
    //  ////////////////////////////////////////////////     ////////////////////////     Exit PopUP
    void PopupExitOk ()
    {
        AgStt.NodeClose ();
        System.Diagnostics.Process.GetCurrentProcess ().Kill ();
        //Application.Quit ();
        #if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
        jo.Call ("ExitGame");
        //Debug.Log("");
        #endif

    }

    void PopupExitClosePopup ()
    {
        MenuCommonOpen ("Ui_popup", "ask_Exit", false);
    }
}
