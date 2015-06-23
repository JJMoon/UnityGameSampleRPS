//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase  // JKLeeMustSeeThis
{
    void Btn_Fun_DrinkAuto ()
    {
        if (ItemTypeId == "BlueDrink") {
            if (mSumGold < 350 && !Ag.mBlueItemFlag) {
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
                return;
            }
            if (Ag.mBlueItemFlag) {
                Ag.mBlueItemFlag = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mBlueItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mBlueItemFlag);
                Btn_Fun_DrinkBlue ();
                return;
            } else {
                Ag.mBlueItemFlag = true;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mBlueItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mBlueItemFlag);
                Btn_Fun_DrinkBlue ();
                return;
            }

        }
        if (ItemTypeId == "RedDrink") {
            if (mSumGold < 350 && !Ag.mRedItemFlag) {
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
                return;
            }

            if (Ag.mRedItemFlag) {
                Ag.mRedItemFlag = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mRedItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mRedItemFlag);
                Btn_Fun_DrinkRed ();
                return;
            } else {

                Ag.mRedItemFlag = true;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mRedItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mRedItemFlag);
                Btn_Fun_DrinkRed ();
                return;
            }
        }
        if (ItemTypeId == "GreenDrink") {
            if (mSumGold < 175 && !Ag.mGreenItemFlag) {
                MenuCommonOpen ("Ui_Popup", "havenotpoint", true);
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
                return;
            }
            if (Ag.mGreenItemFlag) {
                Ag.mGreenItemFlag = false;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mGreenItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mGreenItemFlag);
                Btn_Fun_DrinkGreen ();
                return;
            } else {

                Ag.mGreenItemFlag = true;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = Ag.mGreenItemFlag;
                dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (Ag.mGreenItemFlag);
                Btn_Fun_DrinkGreen ();
                return;
            }


        }


        //LabelSetting (true, mItemName, mPrice);
    }
    //  _////////////////////////////////////////////////_    _____  Button Action  _____    Blue    _____
    void Btn_Fun_DrinkBlue ()
    {
        Ag.LogDouble ("  Btn_Fun_DrinkBlue ()   ");
        if (Ag.mBlueItemFlag) {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (true);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (false);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (false);

            dicMenuList ["DrinkAuto"].transform.FindChild ("icon_coin").gameObject.SetActive (false);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label").gameObject.SetActive (false);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label_price").gameObject.SetActive (false);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label_nonauto").gameObject.SetActive (true);

        } else {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (false);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (true);
            dicMenuList ["item00_blue_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (true);

            dicMenuList ["DrinkAuto"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label").gameObject.SetActive (true);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label_price").gameObject.SetActive (true);
            dicMenuList ["DrinkAuto"].transform.FindChild ("Label_nonauto").gameObject.SetActive (false);
        }
        ItemTypeId = "BlueDrink";
        ItemType = "DRINK";
//        BuyType = 1;
        ItemInit ();
        mItemName = "파워향상";

        Ag.LogDouble ("  Btn_Fun_DrinkBlue ()     >>>>  " );
        //CommonDrinkPurchase ("00_blue");

        //mPrice = "300";
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "드링크 아이템 (블루)";
        Ag.LogDouble ("  Btn_Fun_DrinkBlue ()     >>>>  " );
        dicMenuList ["item00_blue"].SetActive (true);
        Ag.LogDouble ("  Btn_Fun_DrinkBlue ()     >>>>    OK " );
        dicMenuList ["btn_auto_label_Price"].GetComponent<UILabel> ().text = "350";
        dicMenuList ["DrinkAuto"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_piecebuy", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/Label_itemdescritbtn", true);


    }
    //  _////////////////////////////////////////////////_    _____  Button Action  _____    Common Action    _____
    void CommonDrinkPurchase (string colorStr)
    {
        ItemType = "DRINK";
        BuyType = 1;

        ItemInit ();
        mPrice = "300";

        if (Ag.mBlueItemFlag || Ag.mGreenItemFlag || Ag.mBlueItemFlag) {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (true);        
            dicMenuList ["item" + colorStr + "_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (true);
            dicMenuList ["item" + colorStr + "_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (false);
            dicMenuList ["item" + colorStr + "_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (false);
        }
    }

    void Btn_Fun_DrinkItem ()
    {
        ItemInit ();
        SetupSubMenuClose ();
        dicMenuList ["scroll_basicitem"].SetActive (true);
        dicMenuList ["scroll_basicitem"].transform.localPosition = new Vector3 (-150.0005f, -158f, -42.23072f);
        dicMenuList ["scroll_basicitem"].GetComponent<UIDraggablePanel> ().repositionClipping = true;

        dicMenuList ["item00_blue_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("BlueDrink").ToString ();
        dicMenuList ["item01_red_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("RedDrink").ToString ();
        dicMenuList ["item02_green_eaLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("GreenDrink").ToString ();
        dicMenuList ["btngrid_basicitem"].SetActive (true);

        Btn_Fun_DrinkGreen ();
        Btn_Fun_DrinkRed ();
        Btn_Fun_DrinkBlue ();
    
        //Btn_Fun_DrinkAuto ();


        dicMenuList ["DrinkItem"].GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["DrinkItem"].GetComponent<UICheckbox> ().Set (true);
        dicMenuList ["item00_blue_Choice"].GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["item00_blue_Choice"].GetComponent<UICheckbox> ().Set (true);


        StartCoroutine (Bundle_tapBtnoff ());


        ItemEventOnCheck ("BlueDrink", dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item00_blue/event").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item00_blue/cutline").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item00_blue/Label_eventprice").gameObject);
        ItemEventOnCheck ("RedDrink", dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item01_red/event").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item01_red/cutline").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item01_red/Label_eventprice").gameObject);
        ItemEventOnCheck ("GreenDrink", dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item02_green/event").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item02_green/cutline").gameObject, dicMenuList ["scroll_basicitem"].gameObject.transform.FindChild ("grid/item02_green/Label_eventprice").gameObject);
    }
    IEnumerator Bundle_tapBtnoff () {
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox0_item/Background", true).gameObject.SetActive (true);
        yield return new WaitForSeconds (0.1f);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_kickoffback/bundle_tap/checkbox0_item/Background", true).gameObject.SetActive (false);
    }

    //  _////////////////////////////////////////////////_    _____  Button Action  _____    Green    _____
    void Btn_Fun_DrinkGreen ()
    {
        if (Ag.mGreenItemFlag) {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (true);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (false);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (false);
        } else {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (false);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (true);
            dicMenuList ["item02_green_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (true);

        }
        ItemTypeId = "GreenDrink";
        ItemType = "DRINK";
//        BuyType = 1;
        ItemInit ();
        mItemName = "정확도향상";
//        mPrice = "300";
        //CommonDrinkPurchase ("02_green");
        dicMenuList ["Label_itemdescrit"].GetComponent<UILabel> ().text = "<그린 드링크> - 적용 대상 : 킥커 \n효과 : 각 방향 영역을 넓혀주어 다양한 방향 선택이 수월해집니다.";
        dicMenuList ["item02_green"].SetActive (true);
        dicMenuList ["btn_auto_label_Price"].GetComponent<UILabel> ().text = "175";
        dicMenuList ["DrinkAuto"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_piecebuy", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/Label_itemdescritbtn", true);


    }
    //  _////////////////////////////////////////////////_    _____  Button Action  _____    Red    _____
    void Btn_Fun_DrinkRed ()
    {
        if (Ag.mRedItemFlag) {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (true);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (false);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (false);
            dicMenuList ["btn_auto_label_Price"].SetActive (true);
            dicMenuList ["btn_auto_label_Price"].SetActive (false);
        } else {
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["DrinkAuto"].GetComponent<UICheckbox> ().Set (false);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("txt_choose").gameObject.SetActive (false);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("Label_price").gameObject.SetActive (true);
            dicMenuList ["item01_red_Choice"].transform.FindChild ("icon_coin").gameObject.SetActive (true);

        }
        ItemTypeId = "RedDrink";
        ItemType = "DRINK";
//        BuyType = 1;
        ItemInit ();
        mItemName = "집중력향상";
        //CommonDrinkPurchase ("01_red");

        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "드링크 아이템 (레드)";
        dicMenuList ["item01_red"].SetActive (true);
        dicMenuList ["btn_auto_label_Price"].GetComponent<UILabel> ().text = "350";
        dicMenuList ["DrinkAuto"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_piecebuy", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/Label_itemdescritbtn", true);


    }

    void BuyDrink ()
    {
        DrinkEA = 1;
        dicMenuList ["KickOffpopup"].SetActive (true);
        dicMenuList ["buy_Drinkitem"].SetActive (true);
        dicMenuList ["buy_Drinkitem"].transform.FindChild ("Label_item").GetComponent<UILabel> ().text = mItemName;
        dicMenuList ["buy_Drinkitem"].transform.FindChild ("check_blue_red/btn_1").GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["buy_Drinkitem"].transform.FindChild ("check_blue_red/btn_1").GetComponent<UICheckbox> ().Set (true);
    }

    void DrinkEa1 ()
    {
        DrinkEA = 1;
        Debug.Log (DrinkEA);
    }

    void DrinkEa5 ()
    {
        DrinkEA = 5;
        Debug.Log (DrinkEA);
    }

    void DrinkEa10 ()
    {
        DrinkEA = 10;
        Debug.Log (DrinkEA);
    }

    void BuyDrinkOk ()
    {
        dicMenuList ["KickOffpopup"].SetActive (false);
        dicMenuList ["buy_Drinkitem"].SetActive (false);
        BuyItem ();
    }

    void BuyDrinkCancel ()
    {
        dicMenuList ["KickOffpopup"].SetActive (false);
        dicMenuList ["buy_Drinkitem"].SetActive (false);
    }
}