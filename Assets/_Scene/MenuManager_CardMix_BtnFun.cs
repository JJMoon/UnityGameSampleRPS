using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class MenuManager : AmSceneBase
{




    void Cardmix_popup_Select_gradesave ()
    {
        BuyType = 0;
        CombiItemName = "CardCombiGrade";
    }

    void Cardmix_popup_Select_AdvCombi ()
    {
        BuyType = 0;
        CombiItemName = "CardCombiAdvtHigh";
    }

    void Cardmix_popup_Select_LuckCombi ()
    {
        BuyType = 1;
        CombiItemName = "CardCombiAdvt";

    }

    void Cardmix_popup_btn_cancel ()
    {
        dicMenuList ["popup_mixitem"].SetActive (false);
    }

    void popup_mixitem1_Cancel ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem1", false);
    }

    void MixitemEvent () {


        ItemEventOnCheck ("CardCombiGrade",mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item01/txtevent", true).gameObject, mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item01/Sprite (cutline)", true), mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item01/Label_eventprice", true)); 
        ItemEventOnCheck ("CardCombiAdvtHigh",mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item02/txtevent", true).gameObject, mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item02/Sprite (cutline)", true), mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item02/Label_eventprice", true)); 
        ItemEventOnCheck ("CardCombiAdvt",mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03/txtevent", true).gameObject, mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03/Sprite (cutline)", true), mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03/Label_eventprice", true)); 
    }

    void popup_mixitem1_btn_ok ()
    {

        MenuCommonOpen ("Lineup_popup", "popup_mixitem1", false);
        dicMenuList ["popup_mixitem"].SetActive (true);
        MixitemEvent ();
        Cardmix_popup_Select_LuckCombi ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().Set (true);
    }

    void popup_mixitem2_Cancel ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem2", false);
    }

    void popup_mixitem2_btn_ok ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem2", false);
        dicMenuList ["popup_mixitem"].SetActive (true);
        MixitemEvent ();
        Cardmix_popup_Select_LuckCombi ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().Set (true);

    }

    void popup_mixitem3_Cancel ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem3", false);

    }

    void popup_mixitem3_btn_ok ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem3", false);

        MixitemEvent ();
        Cardmix_popup_Select_LuckCombi ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/popup_mixitem/itemlist/item03", true).GetComponent<UICheckbox> ().Set (true);
        dicMenuList ["popup_mixitem"].SetActive (true);

    }

    /// <summary>
    /// S 카드 체험을 시작. 게임 씬으로 이동.
    /// </summary>
    void StartSingleTryS ()
    {
        Ag.SingleTry = 2; // 1 : A, 2 : S
        Ag.mySelf.SetCardsForSingleTry ();
        Btn_Fun_MatchSetUp();
        dicMenuList ["btn_exit"].SetActive(false);
        //mGameMatchOk = true;
        MenuCommonOpen ("popup_experienceScard","Ui_popup",false);
        // load scene ..
    }

    void Cacel_Experience_Scard () {
        MenuCommonOpen ("popup_experienceScard","Ui_popup",false);
        Ag.mySelf.ConfirmSingleTry(true);
    }

    void Btun_Fun_MixOk ()
    {
        //Ag.MixCount += 1;
        //Btn_Fun_CardMixOpen ();
        DestoryAllCard ();
        MIXCARDSELECT ();
        MixItemSetting ();
        dicMenuList ["cardmix_popup"].SetActive (false);
        ComBiItemCheck ();
        dicMenuList ["Ui_team"].SetActive(true);
        StartCoroutine(WaitandCardMixRePositionSetting());

        if (Ag.mySelf.SingleTryDone < 2 && mBuyCardOption == 0 && Ag.mySelf.ShowSingleTry (true)) {
            // Popup Activate
            MenuCommonOpen ("popup_experienceScard","Ui_popup",true);
            return;
        }
        PopupAfterCombi ();

    }

    IEnumerator WaitandCardMixRePositionSetting () {
        yield return new WaitForSeconds(0.2f);
        float Card_characterPosY, Card_characterPosZ;
        Card_characterPosY = FindMyChild(dicMenuList ["Ui_team"],"LPanel_cardmix/card_character", true).transform.localPosition.y;
        Card_characterPosZ = FindMyChild(dicMenuList ["Ui_team"],"LPanel_cardmix/card_character", true).transform.localPosition.z;

        Btn_SortbyGradeAndFlag_CardMix ();


        FindMyChild(dicMenuList ["Ui_team"],"LPanel_cardmix/card_character", true).transform.localPosition = new Vector3(0,Card_characterPosY, Card_characterPosZ);
        FindMyChild(dicMenuList ["Ui_team"],"LPanel_cardmix/card_character", true).GetComponent<UIDraggablePanel>().repositionClipping = true;

        //Debug.Log ("CardMix + character X "+FindMyChild(dicMenuList ["Ui_team"],"LPanel_cardmix/card_character", true).transform.position.x);
    }

    void ComBiItemCheck ()
    {
        if (CombiItemListEa ("CardCombiGrade") < 1) {
            dicMenuList ["gradesave"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["gradesave"].GetComponent<UICheckbox> ().Set (false);
            CheckGradesave = false;
        }
        if (CombiItemListEa ("CardCombiAdvtHigh") < 1) {
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (false);
            MixSuper = false;
        }
        if (CombiItemListEa ("CardCombiAdvt") < 1) {
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (false);
            MixLuck = false;
        }
    }

    void popup_mixerror_btn_ok ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixerror", false);
    }

    void Btn_SortbyGrade_CardMix ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }

        Sortbyflag = false;

        if (SortBygrade) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            SortBygrade = false;
            return;
        } else {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                select score;
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }

            SortBygrade = true;
            return;
        }
    }

    void Btn_SortbyGrade_CardMixPopupClose ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }
        
        
        if (SortBygrade) {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                    orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                    select score;
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            } 
        } else {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                    orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                    select score;
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            } 
        }
    }



    void Btn_SortbyStat_CardMix ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }

        SortBygrade = false;



        if (Sortbyflag) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            Sortbyflag = false;
            return;
        } else {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.country
                    select score;
            foreach (GameObject i in scoreQuery) {
                //Debug.Log (i.name);
                i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            } 
            Sortbyflag = true;
            return;
        }

    }
    IEnumerator WaitAndBtn_SortbyGradeAndFlag_CardMix () {
        yield return new WaitForSeconds (0.3f);
        Btn_SortbyGradeAndFlag_CardMix ();
    }


    void Btn_SortbyGradeAndFlag_CardMix ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }


        if (Sortbyflag) {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.country
                select score;
            foreach (GameObject i in scoreQuery) {
                //Debug.Log (i.name);
                i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }  
        } if (SortBygrade) {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                select score;
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
        }
        if (!Sortbyflag && !SortBygrade) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
        }
    }




    void Btn_Fun_AutoSort ()
    {
        bool SortFlag = true;
        int SameNameNum = 0;
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        List<GameObject> SortObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }

        IEnumerable<GameObject> scoreQuery =
            from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().Overall 
                select score;
        foreach (GameObject i in scoreQuery) {
            if (arrEachCard.Count > 2)
                return;
            else {
                SameNameNum = 0;
                if (arrEachCard.Count > 0) {
                    for (int jj = 0; jj < arrEachCard.Count; jj++) {
                        if (arrEachCard [jj].name == i.name) {
                            SameNameNum++;
                        }
                    }
                    if (SameNameNum < 1) {
                        SelectCard (i, false, true, true);
                        arrEachCard.Add (i);
                        CardInstantiate (i);
                    }
                } else {
                    SelectCard (i, false, true, true);
                    arrEachCard.Add (i);
                    CardInstantiate (i);
                }
            } 
        }
    }

    void popup_mixitemuseOk ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitemuse", false);
        Btn_Fun_Mix2 ();
    }

    void popup_mixitemuseCancel ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitemuse", false);
    }

    void popup_mixitem_Cancel ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem", false);
    }

    void popup_mixitem_btn_ok ()
    {
        MenuCommonOpen ("Lineup_popup", "popup_mixitem", false);

    }

    bool CheckGradesave = false;

    void Btn_Fun_gradesave ()
    {

        if (CombiItemListEa ("CardCombiGrade") < 1) {
            MenuCommonOpen ("Lineup_popup", "popup_mixitem1", true);
            dicMenuList ["gradesave"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["gradesave"].GetComponent<UICheckbox> ().Set (false);
        } else {
            if (CheckGradesave) {
                //Debug.Log ("CheckError");
                dicMenuList ["gradesave"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["gradesave"].GetComponent<UICheckbox> ().Set (false);
                CheckGradesave = false;
                return;
            }
            if (!CheckGradesave) {
                //Debug.Log ("CheckOK");
                dicMenuList ["gradesave"].GetComponent<UICheckbox> ().isChecked = true;
                dicMenuList ["gradesave"].GetComponent<UICheckbox> ().Set (true);
                CheckGradesave = true;
                return;
            } 

        }
    }

    void Btn_Fun_mixluck ()
    {
        if (CombiItemListEa ("CardCombiAdvt") < 1) {
            MenuCommonOpen ("Lineup_popup", "popup_mixitem2", true);
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (false);
            MixLuck = false;
            if (MixSuper) {
                dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = true;
                dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (true);
            }
            return;
        } else {
            if (MixLuck) {
                dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (false);
                MixLuck = false;
                return;

            }
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (false);
            MixSuper = false;
            MixLuck = true;
        } 
    }

    bool MixSuper, MixLuck, SaveGrade;


    void Btn_Fun_mixlucksuper ()
    {
        if (CombiItemListEa ("CardCombiAdvtHigh") < 1) {
            MenuCommonOpen ("Lineup_popup", "popup_mixitem3", true);
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (false);
            MixSuper = false;
            if (MixLuck) {
                dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = true;
                dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (true);
            }

            return;
        } else {
            if (MixSuper) {
                dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = false;
                dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (false);
                MixSuper = false;
                return;
            }

            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (false);
            MixSuper = true;
            MixLuck = false;

        } 
    }

    void MixItemSetting ()
    {


        dicMenuList ["gradesave_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiGrade").ToString ();
        dicMenuList ["AdvCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        dicMenuList ["LuckCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();


        dicMenuList ["gradesave"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiGrade").ToString ();
        dicMenuList ["mixluck"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
        dicMenuList ["mixlucksuper"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        /*
        dicMenuList ["popup_mixitem"].transform.FindChild ("itemlist/item01/Label_ possession").GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiGrade").ToString ();
        dicMenuList ["popup_mixitem"].transform.FindChild ("itemlist/item02/Label_ possession").GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
        dicMenuList ["popup_mixitem"].transform.FindChild ("itemlist/item03/Label_ possession").GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        */

    }


    void MixItemInit ()
    {
        //Debug.Log (MixLuck +  "     MIXLUCK" + MixSuper + "    MIXSUPER");

        if (CombiItemListEa ("CardCombiAdvt") < 1) {
            //Btn_Fun_mixluck ();
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixluck"].GetComponent<UICheckbox> ().Set (false);
            MixLuck = false;
            dicMenuList ["AdvCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
            dicMenuList ["LuckCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
            dicMenuList ["mixluck"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
            dicMenuList ["mixlucksuper"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();

        }
        if (CombiItemListEa ("CardCombiAdvtHigh") < 1) {
            //Btn_Fun_mixluck ();
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().isChecked = false;
            dicMenuList ["mixlucksuper"].GetComponent<UICheckbox> ().Set (false);
            MixSuper = false;
            dicMenuList ["AdvCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
            dicMenuList ["LuckCombi_Label"].GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
            dicMenuList ["mixluck"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvt").ToString ();
            dicMenuList ["mixlucksuper"].transform.FindChild ("Label_count").gameObject.GetComponent<UILabel> ().text = CombiItemListEa ("CardCombiAdvtHigh").ToString ();
        }



        if (CombiItemListEa ("CardCombiAdvt") > 0 && CombiItemListEa ("CardCombiAdvtHigh") > 0) {
            Btn_Fun_mixlucksuper ();
            return;
        }
        if (CombiItemListEa ("CardCombiAdvtHigh") > 0) {
            Btn_Fun_mixlucksuper ();
            return;
        }
        if (CombiItemListEa ("CardCombiAdvt") > 0) {
            Btn_Fun_mixluck();
            return;
        }




    }
    //  ////////////////////////////////////////////////     ////////////////////////     Sort
    bool SortBygrade = false, Sortbyflag = false;
    int SortNum = 0;

    void Btn_Fun_SortByGrade ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }

        Sortbyflag = false;

        if (SortBygrade) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            SortBygrade = false;
            return;
        } else {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                select score;

            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            SortBygrade = true;
            return;
        }
        /*
        if (SortBygrade)
            SortBygrade = false;
        else
            SortBygrade = true;
            */

    }

    void Btn_Fun_SortByGrade_setting ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }
        
        
        if (SortBygrade) {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                    orderby score.GetComponent<PlayerCardInfo> ().mwas.grade 
                    select score;
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade == "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            foreach (GameObject i in scoreQuery) {
                if (i.GetComponent<PlayerCardInfo>().mwas.grade != "S")
                    i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            } 
        } 
        if (Sortbyflag) {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                            orderby score.GetComponent<PlayerCardInfo> ().mwas.country
                            select score;
            foreach (GameObject i in scoreQuery) {
                //Debug.Log (i.name);
                i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }  
        } 
        if (!Sortbyflag && !SortBygrade) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
        }

    }

    IEnumerator SortbySetting () {
        yield return new WaitForSeconds (0.3f);
        Btn_Fun_SortByGrade_setting ();
    }


    void Btn_Fun_SortByStat ()
    {
        SortNum = 0;
        List<GameObject> GObj = new List<GameObject> ();
        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
            GObj.Add (child.gameObject);
        }
        SortBygrade = false;

        if (Sortbyflag) {
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            Sortbyflag = false;
            return;
        } else {
            IEnumerable<GameObject> scoreQuery =
                from score in GObj
                orderby score.GetComponent<PlayerCardInfo> ().mwas.country
                select score;
            foreach (GameObject i in scoreQuery) {
                //Debug.Log (i.name);
                i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }
            Sortbyflag = true;
            return;
        }
    }
}
