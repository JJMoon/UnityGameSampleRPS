//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase  // JKLeeMustSeeThis
{
    void Btn_Fun_PurCer1 ()
    {
        ItemTypeId = "CeremonySkill01";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "슬라이딩";
        mPrice = GetRealBuyPrice ("CeremonySkill01").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "슬라이딩";
        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 100";
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill01");
        CereMonyAction ("CeremonySkill01");
        CereMonyUnlock ("CeremonySkill01", "cerimony1");

    }

    int DrinkEA;

    void LabelSetting (bool pflag, string pItemName, string pPrice, string ItemType)
    {
        dicMenuList ["KickOffpopup"].SetActive (pflag);
        dicMenuList ["popup_BuyItem"].SetActive (pflag);
        if (ItemType == "Drink")
            dicMenuList ["popup_BuyItem"].transform.FindChild ("Label_title").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%95%84%EC%9D%B4%ED%85%9C");
        if (ItemType == "Ceremony")
            dicMenuList ["popup_BuyItem"].transform.FindChild ("Label_title").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%84%B8%EB%A0%88%EB%A8%B8%EB%8B%88%20%EA%B5%AC%EB%A7%A4");
        if (ItemType == "EndMessage")
            dicMenuList ["popup_BuyItem"].transform.FindChild ("Label_title").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%A9%94%EC%84%B8%EC%A7%80%20%EA%B5%AC%EB%A7%A4");
        dicMenuList ["popup_BuyItem_Label_item"].GetComponent<UILabel> ().text = pItemName;
        dicMenuList ["popup_BuyItem_Label_price"].GetComponent<UILabel> ().text = pPrice;

    }

    string ItemTypeId, ItemType;
    int BuyType;

    void CereMonyPreviewClose ()
    {
        DestroyObject (Kicker);
        DestroyObject (Keeper);
        DefAniFlag = false;
    }

    int mItemType = 0;

    void BuyItemCancel ()
    {
        LabelSetting (false, "", "", "");                                                                                                        
    }

    void Btn_Fun_piecebuy ()
    {
        LabelSetting (true, mItemName, mPrice, "Drink");

    }

    void Btn_Fun_CeremonyBuy ()
    {
        LabelSetting (true, mItemName, mPrice, "Ceremony");
    }

    void Btn_Fun_MessageBuy ()
    {
        LabelSetting (true, mItemName, mPrice, "EndMessage");
    }

    /// <summary>
    /// Item Setup
    /// </summary>


    string mItemName, mPrice;

    void SetupSubMenuClose ()
    {
        dicMenuList ["scroll_basicitem"].SetActive (false);
        dicMenuList ["scroll_ceremony"].SetActive (false);
        dicMenuList ["scroll_message"].SetActive (false);
        dicMenuList ["btngrid_basicitem"].SetActive (false);
        dicMenuList ["btngrid_ceremony"].SetActive (false);
        dicMenuList ["btngrid_message"].SetActive (false);
    }

    void Btn_Fun_messageItem ()
    {
        ItemInit ();
        SetupSubMenuClose ();
        dicMenuList ["scroll_message"].SetActive (true);
        dicMenuList ["scroll_message"].transform.localPosition = new Vector3 (-150.0005f, -158f, -42.23072f);
        dicMenuList ["scroll_message"].GetComponent<UIDraggablePanel> ().repositionClipping = true;
        dicMenuList ["btngrid_message"].SetActive (true);
        ItemEventOnCheck ("EndMessage01", dicMenuList ["scroll_message"].transform.FindChild ("grid/message1/event").gameObject, dicMenuList ["scroll_message"].transform.FindChild ("grid/message1/cutline").gameObject, dicMenuList ["scroll_message"].transform.FindChild ("grid/message1/Label_eventprice").gameObject);

        Btn_Fun_EndMessage ();
        Btn_Fun_StartMessage ();
    }

    void Btn_Fun_Ceremonyitem ()
    {
        for (int i = 1; i < 6; i++) { 
            ItemEventOnCheck ("CeremonySkill0" + i, dicMenuList ["scroll_ceremony"].transform.FindChild ("grid/cerimony" + i + "/event").gameObject, dicMenuList ["scroll_ceremony"].transform.FindChild ("grid/cerimony" + i + "/cutline").gameObject, dicMenuList ["scroll_ceremony"].transform.FindChild ("grid/cerimony" + i + "/Label_eventprice").gameObject);
        }

        ItemInit ();
        SetupSubMenuClose ();
        dicMenuList ["scroll_ceremony"].SetActive (true);
        dicMenuList ["scroll_ceremony"].transform.localPosition = new Vector3 (-150.0005f, -158f, -42.23072f);
        dicMenuList ["scroll_ceremony"].GetComponent<UIDraggablePanel> ().repositionClipping = true;
        dicMenuList ["btngrid_ceremony"].SetActive (true);
        //Btn_Fun_PurCer6 ();
        Btn_Fun_PurCer5 ();
        Btn_Fun_PurCer4 ();
        Btn_Fun_PurCer3 ();
        Btn_Fun_PurCer2 ();
        Btn_Fun_PurCer1 ();
        Btn_Fun_DefCer ();
        Btn_Fun_DefCer ();
        CeremonyInit ();
    }

    /// <summary>
    /// Item Equip
    /// </summary>


    IEnumerator btn_auto_label_Priceoff ()
    {
        dicMenuList ["btn_auto_label_Price"].SetActive (true);
        yield return new WaitForSeconds (0.1f);
        dicMenuList ["btn_auto_label_Price"].SetActive (false);
    }

    /// <summary>
    /// Btn Scouter
    /// </summary>
    void Btn_Fun_Scouter ()
    {
        ItemInit ();
        dicMenuList ["item03_scouter"].SetActive (true);
        //dicMenuList ["btn_auto_label_Price"].SetActive(false);
        dicMenuList ["DrinkAuto"].SetActive (false);

        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/btn_piecebuy", false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_basicitem_blue_red/Label_itemdescritbtn", false);
    }



    GameObject Kicker, Keeper;
    bool DefAniFlag = false;
    string[] DefAni;
    int AniNum = 0;

    void CereMonyUnlock (string id, string CereMonyName)
    { 

        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_buy", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_choice", false);
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("txt_locked").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("icon_coin").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("Label_price").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("event").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("cutline").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("Label_eventprice").gameObject.SetActive (false);

                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_buy", false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_choice", true);
                break;
            } else {
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("txt_locked").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("icon_coin").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/" + CereMonyName, true).transform.Find ("Label_price").gameObject.SetActive (true);

                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_buy", true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_ceremony/btn_choice", false);
            }
        }
    }



    void EquipBoxCerAniPlay (Vector3 pvec, Vector3 prot, string paniname)
    {
        dicMenuList ["AngriCer"].transform.localPosition = pvec;
        dicMenuList ["AngriCer"].transform.localEulerAngles = prot;
        dicMenuList ["AngriCer"].animation.Play (paniname);
    }

    /// <summary>
    /// MessageItem
    /// </summary>
    void MessegeItemUnlock (string id, string GameObjectName, string MessageName)
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_buy", true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_edit", false);
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                dicMenuList [GameObjectName].transform.FindChild ("after_Sprite (img_backcondition)").gameObject.SetActive (true);
                dicMenuList [GameObjectName].transform.FindChild ("center").gameObject.SetActive (true);
                dicMenuList [GameObjectName].transform.FindChild ("Input_start").gameObject.SetActive (true);
                dicMenuList [GameObjectName].transform.FindChild ("message_now").gameObject.SetActive (true);

                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("txt_locked").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("icon_coin").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("Label_price").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("txt_choose").gameObject.SetActive (true);

                dicMenuList ["messageitem_discript_message_now"].SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_buy", false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_edit", true);
                dicMenuList [GameObjectName].transform.FindChild ("message_now").GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.arrItem [i].WAS.msg);

                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("event").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("cutline").gameObject.SetActive (false);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("Label_eventprice").gameObject.SetActive (false);

                break;
            } else {
                dicMenuList [GameObjectName].transform.FindChild ("after_Sprite (img_backcondition)").gameObject.SetActive (false);
                dicMenuList [GameObjectName].transform.FindChild ("center").gameObject.SetActive (false);
                dicMenuList [GameObjectName].transform.FindChild ("Input_start").gameObject.SetActive (false);
                dicMenuList [GameObjectName].transform.FindChild ("message_now").gameObject.SetActive (false);

                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("txt_locked").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("icon_coin").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("Label_price").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/" + MessageName, true).transform.Find ("txt_choose").gameObject.SetActive (false);
                dicMenuList ["messageitem_discript_message_now"].SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_buy", true);
                mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/btngrid_message/btn_edit", false);
            }
        }
    }

    /// <summary>
    /// ItemInit
    /// </summary>
    void ItemInit ()
    {
        dicMenuList ["messageitem_discript_message_now"].SetActive (false);
        dicMenuList ["Label_itemtitle"].SetActive (false);
        dicMenuList ["Label_itemdescrit"].SetActive (false);
        dicMenuList ["item02_green"].SetActive (false);
        dicMenuList ["item00_blue"].SetActive (false);
        dicMenuList ["item01_red"].SetActive (false);
        dicMenuList ["item03_scouter"].SetActive (false);
        dicMenuList ["message_custom1"].SetActive (false);
        dicMenuList ["message_custom2"].SetActive (false);
        CereMonyPreviewClose ();
    }

    void Btn_Fun_ApplyCeremony ()
    {
        CeremonyApply ();


        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony1", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony2", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony3", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony4", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony5", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        //mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony6", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
    }

    void CeremonyApply ()  // // JKLeeMustSeeThis
    {
        int typeIDNum = ItemTypeId == "CeremonyDefault" ? 0 : ItemTypeId.GetContinuousInteger ();  
        Btn_fun_Ceremony_Apply ("CeremonyDefault", typeIDNum);

        // JKLeeEraseThis
        //        if (ItemTypeId == "CeremonyDefault") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 0);
        //        } 
        //        if (ItemTypeId == "CeremonySkill01") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 1);
        //        } 
        //        if (ItemTypeId == "CeremonySkill02") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 2);
        //        } 
        //        if (ItemTypeId == "CeremonySkill03") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 3);
        //        } 
        //        if (ItemTypeId == "CeremonySkill04") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 4);
        //        } 
        //        if (ItemTypeId == "CeremonySkill05") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 5);
        //        } 
        //        if (ItemTypeId == "CeremonySkill06") {
        //            Btn_fun_Ceremony_Apply ("CeremonyDefault", 6);
        //        } 
    }

    void Btn_Fun_StartMessage ()
    {
        ItemInit ();
        ItemTypeId = "StartMessage";
        ItemType = "MESSAGE";
        BuyType = 0;
        dicMenuList ["message_custom1"].SetActive (true);
        dicMenuList ["message_custom2"].SetActive (false);
        mItemName = "StartMessage";
        mPrice = ItemPrice ("StartMessage").ToString ();
        dicMenuList ["messageitem_discript"].SetActive (true);
        dicMenuList ["messageitem_discript_message_now"].SetActive (false);
        MessegeItemUnlock (ItemTypeId, "message_custom1", "message0");
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/message0", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_message/grid/message0", true).GetComponent<UICheckbox> ().Set (true);
    }

    public void popup_playeralertOk ()
    {
        MenuCommonOpen ("popup_playeralert", "KickOffpopup", false);
        Btn_Fun_GotoLineup ();
    }

    public void popup_levelpointalertOK ()
    {

        MenuCommonOpen ("popup_levelpointalert", "KickOffpopup", false);
        Btn_Fun_GotoLineup ();
    }

    void VersusInviteOkPopupClose ()
    {
        MenuCommonOpen ("KickOffpopup", "versusinvite_success", false);
    }

    void Btn_Fun_EndMessage ()
    {
        ItemTypeId = "EndMessage01";
        ItemType = "MESSAGE";
        mItemName = "승리메세지";
        BuyType = 0;
        ItemInit ();
        dicMenuList ["message_custom1"].SetActive (false);
        dicMenuList ["message_custom2"].SetActive (true);
        mPrice = ItemPrice ("EndMessage01").ToString ();
        dicMenuList ["messageitem_discript"].SetActive (true);
        dicMenuList ["messageitem_discript_message_now"].SetActive (true);
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("EndMessage01");
        MessegeItemUnlock (ItemTypeId, "message_custom2", "message1");

    }

    void PointOverPopupClose ()
    {
        MenuCommonOpen ("KickOffpopup", "pointover", false);
        Ag.mGreenItemFlag = Ag.mRedItemFlag = Ag.mBlueItemFlag = false;
        Btn_Fun_DrinkBlue ();
        Btn_Fun_DrinkRed ();
        Btn_Fun_DrinkGreen ();

    }

    int Applyid (string id)
    {
        int ApplyID = 0;
        //        Debug.Log ("Start");
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                ApplyID = Ag.mySelf.arrItem [i].WAS.applyID;
                //                Debug.Log ("ShoesEA");
            }
        }
        return ApplyID;

    }

    void Btn_Fun_DefCer ()
    {
        ItemInit ();
        mItemName = "기본 세레모니";
        ItemTypeId = "CeremonyDefault";
        mPrice = "300";
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "기본 세레모니";
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).GetComponent<UICheckbox> ().Set (true);

        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 0";
        CereMonyAction ("CeremonyDefault");
        CereMonyUnlock ("CeremonyDefault", "cerimony0");

    }


    void Btn_Fun_PurCer2 ()
    {
        ItemTypeId = "CeremonySkill02";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "만세";
        mPrice = GetRealBuyPrice ("CeremonySkill02").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "만세";

        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 50";
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill02");
        CereMonyAction ("CeremonySkill02");
        CereMonyUnlock ("CeremonySkill02", "cerimony2");
    }

    void Btn_Fun_PurCer3 ()
    {
        ItemTypeId = "CeremonySkill03";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "빠샤";
        mPrice = GetRealBuyPrice ("CeremonySkill03").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "빠샤";
        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 250";
        CereMonyAction ("CeremonySkill03");
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill03");
        CereMonyUnlock ("CeremonySkill03", "cerimony3");
    }

    void Btn_Fun_PurCer4 ()
    {
        ItemTypeId = "CeremonySkill04";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "OTL";
        mPrice = GetRealBuyPrice ("CeremonySkill04").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "OTL";
        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 200";
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill04");
        CereMonyAction ("CeremonySkill04");
        CereMonyUnlock ("CeremonySkill04", "cerimony4");
    }

    void Btn_Fun_PurCer5 ()
    {
        ItemTypeId = "CeremonySkill05";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "우쮸쮸";
        mPrice = GetRealBuyPrice ("CeremonySkill05").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "우쮸쮸";
        dicMenuList ["btngrid_ceremony"].transform.FindChild ("Label_ceremony").GetComponent<UILabel> ().text = "가산점 + 150";
        dicMenuList ["popup_BuyItem"].transform.FindChild ("icon_coin").GetComponent<UISprite> ().spriteName = GoldTypeIconName ("CeremonySkill05");
        CereMonyAction ("CeremonySkill05");
        CereMonyUnlock ("CeremonySkill05", "cerimony5");
    }

    void Btn_Fun_PurCer6 ()
    {
        ItemTypeId = "CeremonySkill06";
        ItemType = "CEREMONY";
        BuyType = 0;
        ItemInit ();
        mItemName = "세레모니 D";
        mPrice = GetRealBuyPrice ("CeremonySkill06").ToString ();
        dicMenuList ["Label_itemtitle"].SetActive (true);
        dicMenuList ["Label_itemtitle"].GetComponent<UILabel> ().text = "";
        CereMonyAction ("CeremonySkill06");
        CereMonyUnlock ("CeremonySkill06", "cerimony6");
    }

    void CereMonyAction (string CeremonyName)
    {
        DefAni = new string[3];
        DefAni [0] = "CereLastWin01_Preview";
        DefAni [1] = "CereLastWin02_Preview";
        DefAni [2] = "CereLastWin03_Preview";
        DefAniFlag = false;
        DestroyObject (Kicker);
        DestroyObject (Keeper);


        Kicker = (GameObject)Instantiate ((GameObject)Resources.Load ("CeremonyCharacter/CereMonyKicker"));
        Keeper = (GameObject)Instantiate ((GameObject)Resources.Load ("CeremonyCharacter/CereMonyKeeper"));
        Kicker.transform.parent = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_kickoff/LPanel_item", true).transform;
        Keeper.transform.parent = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_kickoff/LPanel_item", true).transform;

        mProcedureMat = (ProceduralMaterial)subKeeperShirts [Ag.mySelf.arrUniform [0].Keep.Shirt.Texture - 1];

        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub);

        Keeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.mySelf.arrUniform [0].Keep.Pants.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Pants.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Pants.ColSub);
        Keeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture = mProcedureMat.mainTexture;

        mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.mySelf.arrUniform [0].Keep.Socks.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Socks.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Socks.ColSub);
        Keeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture = mProcedureMat.mainTexture;


        switch (CeremonyName) {
        case "CeremonyDefault":
            AniNum = 0;
            //DefAniFlag = true;
            Kicker.transform.localPosition = new Vector3 (-170.5433f, -67.64587f, 1100f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 33.15904f, 0);
            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.GetClip (DefAni [0]).wrapMode = WrapMode.Loop;
            Kicker.animation.Play (DefAni [0]);


            break;



        case "CeremonySkill01":
//            Kicker.transform.localPosition = new Vector3 (72.84668f, -67.58621f, 1026.265f);
//            Kicker.transform.localEulerAngles = new Vector3 (0, 93.64019f, 0);
            Kicker.transform.localPosition = new Vector3 (613.1824f, -68.30673f, 747.951f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 123.2176f, 0);

            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.GetClip (DefAni [1]).wrapMode = WrapMode.Loop;
            Kicker.animation.Play (DefAni [1]);

            break;

        case "CeremonySkill02":
            //Kicker.transform.localPosition = new Vector3 (36.97449f, -67.58621f, 863.2329f);
            //Kicker.transform.localEulerAngles = new Vector3 (0, 98.1912f, 0);
            Kicker.transform.localPosition = new Vector3 (-948.2074f, -67.38663f, 173.6537f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 33.88998f, 0);
            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.GetClip (DefAni [2]).wrapMode = WrapMode.Loop;
            Kicker.animation.Play (DefAni [2]);
            break;
        case "CeremonySkill03":
//            Kicker.transform.localPosition = new Vector3 (-168.446f, -69.34174f, -270f);
//            Kicker.transform.localEulerAngles = new Vector3 (0, 60, 0);
            Kicker.transform.localPosition = new Vector3 (-121.5936f, -76.46713f, -290.2468f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 60, 0);
            Kicker.transform.localScale = new Vector3 (331f, 331f, 331f);
//            Keeper.transform.localPosition = new Vector3 (34.68933f, -69.34174f, -270f);
            Keeper.transform.localPosition = new Vector3 (-1.716919f, -81.9038f, -292.8016f);

            Keeper.transform.localEulerAngles = new Vector3 (0, 269.0678f, 0);
            Keeper.transform.localScale = new Vector3 (331f, 331f, 331f);
            Kicker.animation.GetClip ("Cere_Skill_Loser_03_(540F)").wrapMode = WrapMode.Loop;
            Keeper.animation.GetClip ("Cere_Skill_Winner_01_(500F)").wrapMode = WrapMode.Loop;
            Kicker.animation.Play ("Cere_Skill_Loser_03_(540F)");
            Keeper.animation.Play ("Cere_Skill_Winner_01_(500F)");


            /*
            Kicker.transform.localPosition = new Vector3 (40.77454f, -69.34174f, -150f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 272.0993f, 0);
            Kicker.transform.localScale = new Vector3 (331f, 331f, 331f);
            Keeper.transform.localPosition = new Vector3 (-136.1115f, -69.34174f, -150f);
            Keeper.transform.localEulerAngles = new Vector3 (0, 83.10484f, 0);
            Keeper.transform.localScale = new Vector3 (331f, 331f, 331f);
            Kicker.animation.Play ("Cere_Skill_Loser_02_(400F)");
            Keeper.animation.Play ("Cere_Skill_Winner_03_(430F)");
            */
            break;
        case "CeremonySkill04":

            Kicker.transform.localPosition = new Vector3 (-143.0316f, -67.19696f, -150f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 84.77029f, 0);
            Kicker.transform.localScale = new Vector3 (331f, 331f, 331f);
            Keeper.transform.localPosition = new Vector3 (87.58362f, -67.19696f, -150f);
            Keeper.transform.localEulerAngles = new Vector3 (0, 269.0678f, 0);
            Keeper.transform.localScale = new Vector3 (331f, 331f, 331f);
            Kicker.animation.GetClip ("Cere_Skill_Loser_03_(540F)").wrapMode = WrapMode.Loop;
            Keeper.animation.GetClip ("Cere_Skill_Winner_05_(485F)").wrapMode = WrapMode.Loop;
            Kicker.animation.Play ("Cere_Skill_Loser_03_(540F)");
            Keeper.animation.Play ("Cere_Skill_Winner_05_(485F)");
            break;
        case "CeremonySkill05":

//            Kicker.transform.localPosition = new Vector3 (-79.75525f, -60.9021f, -274.1765f);
//            Kicker.transform.localEulerAngles = new Vector3 (0, 60, 0);
            Kicker.transform.localPosition = new Vector3 (-121.9647f, -60.9021f, -274.1765f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 60, 0);
            Kicker.transform.localScale = new Vector3 (331.6858f, 331.6858f, 331.6858f);
//            Keeper.transform.localPosition = new Vector3 (95.30396f, -60.9021f, -274.1765f);
//            Keeper.transform.localEulerAngles = new Vector3 (0, 269.0678f, 0);
            Keeper.transform.localPosition = new Vector3 (30.53894f, -60.54337f, -274.1765f);
            Keeper.transform.localEulerAngles = new Vector3 (0, 269.0678f, 0);
            Keeper.transform.localScale = new Vector3 (331.6858f, 331.6858f, 331.6858f);

            Kicker.animation.GetClip ("Cere_Skill_Loser_02_(400F)").wrapMode = WrapMode.Loop;
            Keeper.animation.GetClip ("Cere_Skill_Winner_02_(430F)").wrapMode = WrapMode.Loop;
            Kicker.animation.Play ("Cere_Skill_Loser_02_(400F)");
            Keeper.animation.Play ("Cere_Skill_Winner_02_(430F)");

            /*
            Kicker.transform.localPosition = new Vector3 (-168.446f, -69.34174f, -150f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 83.10484f, 0);
            Kicker.transform.localScale = new Vector3 (331f, 331f, 331f);
            Keeper.transform.localPosition = new Vector3 (2.860107f, -69.34174f, -150f);
            Keeper.transform.localEulerAngles = new Vector3 (0, 263.1048f, 0);
            Keeper.transform.localScale = new Vector3 (331f, 331f, 331f);
            Kicker.animation.Play ("Cere_Skill_Loser_02_(400F)");
            Keeper.animation.Play ("Cere_Skill_Winner_04-1_(530F)");
            */
            break;
        case "CeremonySkill06":
            /*
            Kicker.transform.localPosition = new Vector3 (-168.446f, -69.34174f, -150f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 83.10484f, 0);
            Kicker.transform.localScale = new Vector3 (331f, 331f, 331f);
            Keeper.transform.localPosition = new Vector3 (2.860107f, -69.34174f, -150f);
            Keeper.transform.localEulerAngles = new Vector3 (0, 263.1048f, 0);
            Keeper.transform.localScale = new Vector3 (331, 331f, 331f);
            Kicker.animation.Play ("Cere_Skill_Loser_01_(560F)");
            Keeper.animation.Play ("Cere_Skill_Winner_04-2_(530F)");
            */

            break;
        }

    }

    void DefAnimaPlay ()
    {
        Keeper.SetActive (false);
        if (!Kicker.animation.isPlaying) {
            Kicker.transform.localPosition = new Vector3 (-100.5433f, -67.64587f, 1100f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 33.15904f, 0);
            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.Play (DefAni [0]);

            /*
            Kicker.transform.localPosition = new Vector3 (72.84668f, -67.58621f, 1026.265f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 93.64019f, 0);
            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.Play (DefAni [1]);
            */

            /*
            Kicker.transform.localPosition = new Vector3 (36.97449f, -67.58621f, 863.2329f);
            Kicker.transform.localEulerAngles = new Vector3 (0, 98.1912f, 0);
            Kicker.transform.localScale = new Vector3 (285.6049f, 285.6049f, 285.6049f);
            Kicker.animation.Play (DefAni [2]);
            */
        }
    }

    void CeremonySetNum ()
    {
        if (ItemTypeId == "CeremonyDefault") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
        if (ItemTypeId == "CeremonySkill01") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony1", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
        if (ItemTypeId == "CeremonySkill02") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony2", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
        if (ItemTypeId == "CeremonySkill03") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony3", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
        if (ItemTypeId == "CeremonySkill04") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony4", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        }
        if (ItemTypeId == "CeremonySkill05") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony5", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
        if (ItemTypeId == "CeremonySkill06") {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony6", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
        } 
    }

    void CeremonyInit ()
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony1", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony2", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony3", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony4", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony5", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);
        //mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony6", true).transform.FindChild ("txt_choose").gameObject.SetActive (false);

        switch (Applyid ("CeremonyDefault")) {
        case 0:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony0", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 1:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony1", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 2:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony2", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 3:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony3", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 4:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony4", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 5:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony5", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        case 6:
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_item/scroll_ceremony/grid/cerimony6", true).transform.FindChild ("txt_choose").gameObject.SetActive (true);
            break;
        }
    }
}