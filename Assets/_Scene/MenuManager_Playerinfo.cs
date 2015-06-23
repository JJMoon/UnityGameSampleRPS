using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    int CostumeNum;
    int mSkill0, mSkill1;

    public void KickerCard () {
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Label_playername").gameObject.GetComponent<UILabel> ().text =Tbl.dicDeckBackNumCol[mwas.country] + WWW.UnEscapeURL(mwas.playerName);
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Label_playernumber").gameObject.GetComponent<UILabel> ().text =Tbl.dicDeckBackNumCol[mwas.country] + mwas.backNum.ToString();
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Sprite (uniform_korea)").gameObject.GetComponent<UISprite> ().spriteName = Tbl.arrDeckUniformName[mwas.country];
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/nations").gameObject.GetComponent<UISprite> ().spriteName = "flag_"+mwas.country;
        //dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Sprite (card_legends)").GetComponent<UISprite> ().spriteName = mGradeSprite;
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Sprite (img_cardgk)").gameObject.SetActive (false);
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/enchant1/Label_overall").GetComponent<UILabel> ().text = "+"+mwas.level;
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard/Label_playcount").GetComponent<UILabel> ().text = mwas.limitGameEA.ToString ();

    }

    public void KeeperCard () {
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Label_playername").gameObject.GetComponent<UILabel> ().text =Tbl.dicDeckBackNumCol[mwas.country] + WWW.UnEscapeURL(mwas.playerName);
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Label_playernumber").gameObject.GetComponent<UILabel> ().text =Tbl.dicDeckBackNumCol[mwas.country] + mwas.backNum.ToString();
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Sprite (uniform_korea)").gameObject.GetComponent<UISprite> ().spriteName = Tbl.arrDeckUniformName[mwas.country];
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/nations").gameObject.GetComponent<UISprite> ().spriteName = "flag_"+mwas.country;
        //dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Sprite (card_legends)").GetComponent<UISprite> ().spriteName = mGradeSprite;
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Sprite (img_cardgk)").gameObject.SetActive (true);
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/enchant1/Label_overall").GetComponent<UILabel> ().text = "+"+mwas.level;
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card/Label_playcount").GetComponent<UILabel> ().text = mwas.limitGameEA.ToString ();
    }


    public void KeeperInfo ()
    {

        //KeeperCard ();

        mSkill0 = mwas.skill[0];
        mSkill1 = mwas.skill[1];
        Debug.Log (mwas.level+"Level");
        //dicMenuList.Add ("Panel_directionbar_Keeper", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/Panel_directionbar", true));
        //dicMenuList.Add ("Panel_directionbar_Kicker", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/Panel_directionbar", true));  

        Debug.Log ("CostumeName" + Costume);
        if (Glove == "KeeperGloves01")
            Btn_fun_EquipGlove1 ();
        if (Glove == "KeeperGloves02")
            Btn_fun_EquipGlove2 ();
        if (Glove == "KeeperGloves03")
            Btn_fun_EquipGlove3 ();
        if (Glove == "KeeperGloves04")
            Btn_fun_EquipGlove4 ();

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item01/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item02/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item03/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item04/set", true).SetActive(false);


        if (mCard.arrCostumeInCard.Count > 0) {
            dicMenuList ["KeeperItemIcon"].SetActive (true);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove1").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove2").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove3").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove4").gameObject.SetActive (false);


            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/Label_noitem").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove1").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove2").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove3").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove4").transform.gameObject.SetActive (false);



            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves01") {
                dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove1").gameObject.SetActive (true);
                dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove1").transform.gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item01/set", true).SetActive(true);
                CostumeNum = 1;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves02") {
                dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove2").gameObject.SetActive (true);
                dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove2").transform.gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item02/set", true).SetActive(true);
                CostumeNum = 2;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves03") {
                dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove3").gameObject.SetActive (true);
                dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove3").transform.gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item03/set", true).SetActive(true);
                CostumeNum = 3;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves04") {
                dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove4").gameObject.SetActive (true);
                dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove4").transform.gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_item/glove/glovelist/grid/item04/set", true).SetActive(true);
                CostumeNum = 4;
            }

            dicMenuList ["KeeperItemIcon_Label_noitem"].SetActive (false);
        } else {
            dicMenuList ["KeeperItemIcon"].SetActive (true);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove1").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove2").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove3").gameObject.SetActive (false);
            dicMenuList ["KeeperItemIcon"].transform.FindChild ("glove4").gameObject.SetActive (false);

            dicMenuList ["KeeperItemIcon_Label_noitem"].SetActive (true);

            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/Label_noitem").transform.gameObject.SetActive (true);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove1").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove2").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove3").transform.gameObject.SetActive (false);
            dicMenuList ["Keeper_popup_itemalert"].transform.FindChild ("itemicon/glove4").transform.gameObject.SetActive (false);

        }



        mCard.WAS.ResetWidthAndSkill ();
        mwas.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Keep.Shirt.Texture, Ag.mySelf.arrUniform [0].Keep.Pants.Texture, Ag.mySelf.arrUniform [0].Keep.Socks.Texture,CostumeNum,out mSkill0,out mSkill1);



        //dicMenuList ["Panel_directionbar_Keeper"].transform.FindChild ("bar1").transform.localScale = new Vector3 (254 * mwas.direction [0] / 100, 24, 1);
        //dicMenuList ["Panel_directionbar_Keeper"].transform.FindChild ("bar2").transform.localScale = new Vector3 (254 * mwas.direction [1] / 100, 24, 1);
        float mGoodbar = mwas.direction[0] == 100 ? 1 : 500;
        float mGoodbar2 = mwas.direction[1] == 100 ? 1 : 500;
        dicMenuList ["Panel_skillbar_Keeper_L"].transform.FindChild("bar4").transform.localScale = new Vector3 (510 * (mSkill0 * (1-(100 - (float)mwas.direction [0])/mGoodbar) / 1000), 24, 1);
        dicMenuList ["Panel_skillbar_Keeper_L"].transform.FindChild("bar3").transform.localScale = new Vector3 (510 * (mSkill1 * ((float)mwas.direction [0] / 100)) / 1000, 24, 1);


        dicMenuList ["Panel_skillbar_Keeper"].transform.FindChild("bar4").transform.localScale = new Vector3 (510 * (mSkill0 * (1-(100 - (float)mwas.direction [0])/mGoodbar) / 1000), 24, 1);
        dicMenuList ["Panel_skillbar_Keeper"].transform.FindChild("bar3").transform.localScale = new Vector3 (510 * (mSkill1 * ((float)mwas.direction [1] / 100)) / 1000, 24, 1);

        //Debug.Log ("mwas.direction [1]" + mwas.direction [1]);

        //dicMenuList ["Panel_skillbar_Keeper"].transform.FindChild("bar4").transform.localScale = new Vector3 (510 * 350 / 1000, 24, 1);


        //dicMenuList ["Panel_keeper_face"].GetComponent<UISprite> ().spriteName = mwas.info;

        dicMenuList ["Label_aft_balance"].GetComponent<UILabel> ().text = mwas.GetValueOfBalance (mwas.level + 1).ToString ();
        dicMenuList ["Label_aft_flashjump"].GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh (1).ToString ();
        dicMenuList ["Label_aft_lightningjump"].GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString ();
        dicMenuList ["Label_aft_bonusscore"].GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();


        dicMenuList ["Label_bf_balance"].GetComponent<UILabel> ().text = mwas.GetValueOfBalance (mwas.level).ToString ();
        dicMenuList ["Label_bf_flashjump"].GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh ().ToString ();
        dicMenuList ["Label_bf_lightningjump"].GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Label_bf_bonusscore"].GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();




        dicMenuList ["Label_Kplefttop"].GetComponent<UILabel> ().text = "-";
        dicMenuList ["Label_Kpleftbottom"].GetComponent<UILabel> ().text = "-";
        dicMenuList ["Label_Kprighttop"].GetComponent<UILabel> ().text = "-";
        dicMenuList ["Label_Kprightbottom"].GetComponent<UILabel> ().text = "-";


        if (mwas.grade == "S" || (mwas.grade == "A" && mwas.level >= 6)) {
            dicMenuList ["Label_KpEnchant2"].SetActive(true);
            dicMenuList ["Label_KpEnchant1"].SetActive(false);
            dicMenuList ["Label_Kpoverall2"].GetComponent<UILabel> ().text = "+" + mwas.level.ToString();
        } else {
            dicMenuList ["Label_KpEnchant1"].SetActive(true);
            dicMenuList ["Label_KpEnchant2"].SetActive(false);
            dicMenuList ["Label_Kpoverall1"].GetComponent<UILabel> ().text = "+" + mwas.level.ToString();
        }

        dicMenuList ["Label_Kpplayername"].GetComponent<UILabel> ().text = mwas.backNum.ToString () + " " +UnityEngine.WWW.UnEscapeURL (mwas.playerName);
        dicMenuList ["Label_Kpplayernumber"].SetActive(false);
        dicMenuList ["Kpgradetitle"].SetActive (true);
        Grade (dicMenuList ["Kpgradetitle"], mwas.grade);


        dicMenuList ["Icon_KeeperCondition"].GetComponent<UISprite> ().spriteName = ConditionName (mwas.condition);

		dicMenuList ["Keeper_popup_condition"].transform.FindChild ("before_condition/2_normal").GetComponent<UISprite>().spriteName = ConditionName (mwas.condition);

        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();

        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;
        dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();



        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_dataafter/1_blance/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBalance (1).ToString ();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_dataafter/2_flashjump/Label_1").GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh (1).ToString();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_dataafter/3_lightningjump/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_dataafter/4_bonusscore/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();

        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_databefore/1_blance/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBalance (0).ToString ();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_databefore/2_flashjump/Label_1").GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh ().ToString ();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_databefore/3_lightningjump/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Keeper_popup_condition"].transform.FindChild ("stat_databefore/4_bonusscore/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();


        dicMenuList ["Keeper_popup_training"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();

        dicMenuList ["Keeper_popup_training"].transform.FindChild("Label_before").GetComponent<UILabel>().text = mwas.level.ToString();
        dicMenuList ["Keeper_popup_training"].transform.FindChild("Label_after").GetComponent<UILabel>().text = (mwas.level+1).ToString();

        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_databefore/1_blance/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBalance (0).ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_databefore/2_flashjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh (0).ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_databefore/3_lightningjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_databefore/4_bonusscore/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();

        
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_dataafter/1_blance/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBalance (1).ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_dataafter/2_flashjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" :mwas.GetValueOfFireOrFresh (1).ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_dataafter/3_lightningjump/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString ();
        dicMenuList ["Keeper_popup_training"].transform.FindChild ("stat_dataafter/4_bonusscore/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();
    



        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;;
        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();

        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_namebefore").GetComponent<UILabel> ().text = WWW.UnEscapeURL (mwas.playerName);
        dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_numberbefore").GetComponent<UILabel> ().text = mwas.backNum.ToString();
        //dicMenuList ["Keeper_popup_editplayername"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = "500";
        Debug.Log ("WAsLEVEL"+ mwas.level);
        dicMenuList ["Keeper_traininglevel"].GetComponent<UILabel> ().text = mwas.level.ToString ();
        dicMenuList ["Keeper_popup_recharter_label"].GetComponent<UILabel> ().text = mwas.limitGameEA.ToString ();

        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item01/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves01").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item02/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves02").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item03/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves03").ToString ();
        dicMenuList ["Keeper_popup_item"].transform.FindChild ("glove/glovelist/grid/item04/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KeeperGloves04").ToString ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/profile_detail/grid_btn/btn_dresseditem/Label_amount", true).GetComponent<UILabel> ().text = (CostumeListEa ("KeeperGloves01") + CostumeListEa ("KeeperGloves02") + CostumeListEa ("KeeperGloves03") + CostumeListEa ("KeeperGloves04")).ToString ();
        //Btn_fun_EquipGlove1 ();
//		mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "30"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"1" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "60"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"10" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "90"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"30" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "120"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"50" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_1/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,0) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_10/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,1) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_30/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,2) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup/popup_recharter/checkboxgrid/btn_50/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,3) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");


    }




    public void KickerInfo ()
    {

        //KickerCard ();

        //Debug.Log (mwas.level+"Level");
        for (int i= 0; i < BarObj.Count; i++) {
            DestroyObject (BarObj [i]);
        }

        //Debug.Log (Costume + "Costumename");
        if (Shoes == "KickerShoes01")
            Btn_fun_EquipShose1 ();
        if (Shoes == "KickerShoes02")
            Btn_fun_EquipShose2 ();
        if (Shoes == "KickerShoes03")
            Btn_fun_EquipShose3 ();
        if (Shoes == "KickerShoes04")
            Btn_fun_EquipShose4 ();

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item01/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item02/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item03/set", true).SetActive(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item04/set", true).SetActive(false);


        if (mCard.arrCostumeInCard.Count > 0) {
            dicMenuList ["KickerItemIcon"].SetActive (true);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes1").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes2").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes3").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes4").gameObject.SetActive (false);

            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/Label_noitem").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes1").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes2").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes3").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes4").transform.gameObject.SetActive (false);


            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes01") {
                dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes1").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item01/set", true).SetActive(true);
                dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes1").transform.gameObject.SetActive (true);
                CostumeNum = 1;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes02") {
                dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes2").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item02/set", true).SetActive(true);
                dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes2").transform.gameObject.SetActive (true);
                CostumeNum = 2;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes03") {
                dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes3").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item03/set", true).SetActive(true);
                dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes3").transform.gameObject.SetActive (true);
                CostumeNum = 3;
            }
            if (mCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes04") {
                dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes4").gameObject.SetActive (true);
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_item/shoes/shoeslist/grid/item04/set", true).SetActive(true);
                dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes4").transform.gameObject.SetActive (true);
                CostumeNum = 4;
            }

            dicMenuList ["KickerItemIcon_Label_noitem"].SetActive (false);
        } else {
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes1").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes2").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes3").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].transform.FindChild ("shoes4").gameObject.SetActive (false);
            dicMenuList ["KickerItemIcon"].SetActive (true);
            dicMenuList ["KickerItemIcon_Label_noitem"].SetActive (true);

            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes1").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes2").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes3").transform.gameObject.SetActive (false);
            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/shoes4").transform.gameObject.SetActive (false);

            dicMenuList ["Kicker_popup_itemalert"].transform.FindChild ("itemicon/Label_noitem").transform.gameObject.SetActive (true);
        }
        //Debug.Log ("CostumenUm" + CostumeNum);
        mSkill0 = mwas.skill[0];
        mSkill1 = mwas.skill[1];
        
        mCard.WAS.ResetWidthAndSkill ();
        mwas.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Kick.Shirt.Texture, Ag.mySelf.arrUniform [0].Kick.Pants.Texture, Ag.mySelf.arrUniform [0].Kick.Socks.Texture,CostumeNum,
                                 out mSkill0, out mSkill1);


        dicMenuList ["Panel_skillbar_Kicker"].transform.FindChild("bar4").transform.localScale = new Vector3 (510f * mSkill0 / 1000, 24, 1);
        dicMenuList ["Panel_skillbar_Kicker"].transform.FindChild("bar3").transform.localScale = new Vector3 (510f * mSkill1 / 1000, 24, 1);
        //dicMenuList ["Panel_skillbar_Kicker"].transform.FindChild("bar4").transform.localScale = new Vector3 (510 * 350 / 1000, 24, 1);

		//dicMenuList ["Panel_kicker_face"].GetComponent<UISprite> ().spriteName = mwas.info;

        dicMenuList ["Label_aft_direct"].GetComponent<UILabel> ().text = mwas.GetValueOfDirection (1).ToString ();
        //dicMenuList ["Label_aft_accuracy"].GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Label_aft_firekick"].GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" : mwas.GetValueOfFireOrFresh (1).ToString ();
        dicMenuList ["Label_aft_blazekick"].GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString ();
        dicMenuList ["Label_aft_addscore"].GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();

        dicMenuList ["Label_bf_direct"].GetComponent<UILabel> ().text = mwas.GetValueOfDirection (0).ToString ();
        //dicMenuList ["Label_bf_accuracy"].GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Label_bf_firekick"].GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" : mwas.GetValueOfFireOrFresh ().ToString ();
        dicMenuList ["Label_bf_blazekick"].GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Label_bf_addscore"].GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();

        if (mwas.grade == "A" && mwas.level >= 6 || mwas.grade == "S") {
            dicMenuList ["Label_aft_volcanokick"].GetComponent<UILabel> ().text = mwas.GetValueOfVolcano(1).ToString ();
            dicMenuList ["Label_bf_volcanokick"].GetComponent<UILabel> ().text = mwas.GetValueOfVolcano().ToString();
        } else {
            dicMenuList ["Label_aft_volcanokick"].GetComponent<UILabel> ().text = "0" ;
            dicMenuList ["Label_bf_volcanokick"].GetComponent<UILabel> ().text = "0";
        }

       


        dicMenuList ["Label_lefttop"].GetComponent<UILabel> ().text = mCard.ScouterGameNum (1).ToString();
		dicMenuList ["Label_leftbottom"].GetComponent<UILabel> ().text = mCard.ScouterGameNum (3).ToString();
		dicMenuList ["Label_righttop"].GetComponent<UILabel> ().text = mCard.ScouterGameNum (2).ToString();
		dicMenuList ["Label_rightbottom"].GetComponent<UILabel> ().text = mCard.ScouterGameNum (4).ToString();
		dicMenuList ["Label_Center"].GetComponent<UILabel> ().text = mCard.ScouterGameNum (5).ToString();


        if (mwas.grade == "S" || (mwas.grade == "A" && mwas.level >= 6)) {
            dicMenuList ["Label_enchant2"].SetActive(true);
            dicMenuList ["Label_enchant1"].SetActive(false);
            dicMenuList ["Label_overall2"].GetComponent<UILabel> ().text = "+"+mwas.level.ToString();
        } else {
            dicMenuList ["Label_enchant1"].SetActive(true);
            dicMenuList ["Label_enchant2"].SetActive(false);
            dicMenuList ["Label_overall1"].GetComponent<UILabel> ().text = "+"+mwas.level.ToString();
        }





        dicMenuList ["Label_playername"].GetComponent<UILabel> ().text = UnityEngine.WWW.UnEscapeURL (mwas.playerName);
        dicMenuList ["Label_playernumber"].SetActive(false);
        dicMenuList ["gradetitle"].SetActive (true);
        Grade (dicMenuList ["gradetitle"], mwas.grade);

        dicMenuList ["Icon_KickerrCondition"].GetComponent<UISprite> ().spriteName = ConditionName (mwas.condition);

        dicMenuList ["Kicker_popup_training"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;

        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;
        dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();



        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/1_direct/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfDirection (0).ToString ();
        //dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/2_accuracy/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/3_firekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfFireOrFresh (0).ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/4_blazekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/4.1_volcanokick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfVolcano (0).ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_databefore/5_addscore/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();

        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/1_direct/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfDirection (1).ToString ();
        //dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/2_accuracy/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/3_firekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfFireOrFresh (1).ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/4_blazekick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/4.1_volcanokick/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfVolcano (1).ToString ();
        dicMenuList ["Kicker_popup_training"].transform.FindChild ("stat_dataafter/5_addscore/Label_1").gameObject.GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();

        dicMenuList ["Kicker_popup_training"].transform.FindChild("Label_before").GetComponent<UILabel>().text = mwas.level.ToString();
        dicMenuList ["Kicker_popup_training"].transform.FindChild("Label_after").GetComponent<UILabel>().text = (mwas.level+1).ToString();


        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;

        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/1_direct/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfDirection (1).ToString ();
        //dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/2_accuracy/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/3_firekick/Label_1").GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" : mwas.GetValueOfFireOrFresh (1).ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/4_blazekick/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening (1).ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/5_addscore/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level+1).ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/4.1_volcanokick/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfVolcano(1).ToString ();


        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/1_direct/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfDirection (0).ToString ();
        //dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/2_accuracy/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfExactness ().ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/3_firekick/Label_1").GetComponent<UILabel> ().text = mwas.grade == "S" ? "full" : mwas.GetValueOfFireOrFresh ().ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/4_blazekick/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBlazeOrLightening ().ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/5_addscore/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfBonus (mwas.level).ToString ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_databefore/4.1_volcanokick/Label_1").GetComponent<UILabel> ().text = mwas.GetValueOfVolcano(0).ToString();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("stat_dataafter/4.1_volcanokick/ar1").gameObject.SetActive(mwas.GetValueOfVolcano(0) == mwas.GetValueOfVolcano(1) ? false : true);



        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().mwas = mCard.WAS;
        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_namebefore").GetComponent<UILabel> ().text = WWW.UnEscapeURL (mwas.playerName);
        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_numberbefore").GetComponent<UILabel> ().text = mwas.backNum.ToString ();
        //dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = "500";

        dicMenuList ["Kicker_traininglevel"].GetComponent<UILabel> ().text = mwas.level.ToString ();
        dicMenuList ["Kicker_popup_recharter_label"].GetComponent<UILabel> ().text = mwas.limitGameEA.ToString ();

        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item01/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes01").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item02/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes02").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item03/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes03").ToString ();
        dicMenuList ["Kicker_popup_item"].transform.FindChild ("shoes/shoeslist/grid/item04/Label_amount").GetComponent<UILabel> ().text = CostumeListEa ("KickerShoes04").ToString ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/profile_detail/grid_btn/btn_dresseditem/Label_amount", true).GetComponent<UILabel> ().text = (CostumeListEa ("KickerShoes01") + CostumeListEa ("KickerShoes02") + CostumeListEa ("KickerShoes03") + CostumeListEa ("KickerShoes04")).ToString ();

        dicMenuList ["Kicker_popup_training"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();
        dicMenuList ["Kicker_popup_condition"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();
		dicMenuList ["Kicker_popup_condition"].transform.FindChild ("before_condition/2_normal").GetComponent<UISprite>().spriteName = ConditionName (mwas.condition);


        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();


        DrawKickerbar ();


//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "30"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"1" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "60"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"10" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "90"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"30" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
//        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/Label", true).GetComponent<UILabel>().text = (mwas.grade == "S" || mwas.grade == "A")?  "120"+ WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0"):"50" + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");


        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_1/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,0) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_10/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,1) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_30/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,2) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_kicker/popup/popup_recharter/checkboxgrid/btn_50/Label", true).GetComponent<UILabel>().text = GetRecharterEa(mwas.grade,3) + WWW.UnEscapeURL("%EA%B2%BD%EA%B8%B0");


    }

    // RecharterEa 

    string[] RecharterEa;
    string GetRecharterEa (string pGrade, int pArrayNum) {
        RecharterEa = new string[4];

        switch (pGrade) {
        case "S":
            RecharterEa[0] = "60";
            RecharterEa[1] = "120";
            RecharterEa[2] = "180";
            RecharterEa[3] = "240";
            break;
        case "A":
            RecharterEa[0] = "50";
            RecharterEa[1] = "100";
            RecharterEa[2] = "150";
            RecharterEa[3] = "200";
            break;
        case "B":
        case "C":
        case "D":
            RecharterEa[0] = "1";
            RecharterEa[1] = "10";
            RecharterEa[2] = "30";
            RecharterEa[3] = "50";
            break;

        }
        return RecharterEa[pArrayNum];
    }
//
//    public int GetValueOfFireOrFresh ()
//    {
//        //return (int)(skill [0] / 250f * 100f); // in % unit
//        return (int)(GetConditionAppliedSkill (0) / 300f * 100f); // in % unit
//    }
//
//    public int GetValueOfBlazeOrLightening ()
//    {
//        //return (int)(skill [1] / 80f * 100f); // in % unit
//        return (int)(GetConditionAppliedSkill (1) / 70f * 100f); // in % unit
//    }
//
//    public int GetConditionAppliedSkill (int pIdx)
//    {
//        return (int)(mwas.skill [pIdx] * 1.2f);
//    }
//    public int GetValueOfBonus ()
//    {
//        int rVal = (5 - GradeValue) * 5; // 기 본 값.
//        return rVal;
//    }
//
//    public int GradeValue {
//        get {
//            switch (mwas.grade) {
//            case "S":
//                return 1;
//            case "A":
//                return 2;
//            case "B":
//                return 3;
//            case "C":
//                return 4;
//            }
//            return 5;
//        }
//    }


    string mCondition;
    float Condition;

    string ConditionName (int pCondition)
    {

        switch (pCondition) {
        case 2:
            mCondition = "arrow_bup";
            break;
        case 1:
            mCondition = "arrow_blittleup";
            break;
        case 0:
            mCondition = "arrow_bnormal";
            break;
        case -1:
            mCondition = "arrow_blittledown";
            break;
        case -2:
            mCondition = "arrow_bdown";
            break;
        }
        return mCondition;
    }

    string Grade (GameObject Gobj, string grade)
    {
        Gobj.transform.FindChild ("1txt_legend").gameObject.SetActive (false);
        Gobj.transform.FindChild ("2txt_pro").gameObject.SetActive (false);
        Gobj.transform.FindChild ("3txt_semipro").gameObject.SetActive (false);
        Gobj.transform.FindChild ("4txt_amatuer").gameObject.SetActive (false);
        Gobj.transform.FindChild ("5txt_student").gameObject.SetActive (false);
        switch (grade) {
        case "S":
            Gobj.transform.FindChild ("1txt_legend").gameObject.SetActive (true);
            break;
        case "A":
            Gobj.transform.FindChild ("2txt_pro").gameObject.SetActive (true);
            break;
        case "B":
            Gobj.transform.FindChild ("3txt_semipro").gameObject.SetActive (true);
            break;
        case "C":
            Gobj.transform.FindChild ("4txt_amatuer").gameObject.SetActive (true);
            break;
        case "D":
            Gobj.transform.FindChild ("5txt_student").gameObject.SetActive (true);
            break;
        }
        return mCondition;
    }
    AmCard DirCard;
    List<GameObject> BarObj;
    void DrawKickerbar ()
    {
        mCard.WAS.ResetWidthAndSkill ();
        DirCard = new AmCard ();
        DirCard.mDirectObj.mWidth = new int[] {
            mCard.WAS.direction [0],
            mCard.WAS.direction [2],
            mCard.WAS.direction [4],
            mCard.WAS.direction [6]

        };
        DirCard.mDirectObj.mPosition = new int[] {
            mCard.WAS.direction [1],
            mCard.WAS.direction [3],
            mCard.WAS.direction [5],
            mCard.WAS.direction [7]

        };

        DirCard.SetDirectionArea ();
        int num = 0;
        num = DirCard.arrArea.Count;
        GameObject mGame;
        for (int i=0; i<num; i++) {
            int[] curVal = (int[])DirCard.arrArea [i]; // 3, 15, 50 < posi, sta, end >
            int color = curVal [0];
            int sta = curVal [1], end = curVal [2];
            float staX =  (float)(sta / 1000.0f);
            float width = (float)((end - sta) / 1000.0f);
            float staEnd = staX + width;
            mGame = (GameObject) Instantiate ( Resources.Load("prefab_General/KickerBar"));
            mGame.transform.parent = dicMenuList ["Panel_directionbar_Kicker"].transform;
            mGame.transform.localPosition = new Vector3((staX * 508)-254,0,0);
            mGame.GetComponent<UISprite> ().spriteName = "kickbar" + color;
            if (color == 0 && width * 510 < 0)
                width = 0;
            mGame.transform.localScale = new Vector3(width * 510 ,24,1);
            BarObj.Add (mGame);
        }
    }
}
