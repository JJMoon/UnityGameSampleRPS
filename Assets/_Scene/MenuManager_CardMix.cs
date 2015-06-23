using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    //PlayerCardInfo SetUpCard1, SetUpCard2, SetUpCard3;
    GameObject MplayerCard;
    List<bool> SelectCardListPOS = new List<bool> ();
    List<GameObject> arrCardMixItemSelect = new List<GameObject> ();
    List<GameObject> arrSelected = new List<GameObject> ();

    void DestoryAllCard ()
    {
        if (arrCardMixItemSelect.Count > 0) {
            foreach (GameObject i in arrCardMixItemSelect) {
                DestroyObject (i);
            }
        }
        foreach (GameObject i in arrAllCard) {
            DestroyObject (i);
        }
    }

    void TopCardCardPosInit ()
    {
        arrSelected = new List<GameObject> ();
        SelectCardListPOS = new List<bool> ();
        arrTopCardVector.Clear ();
        arrTopCardVector.Add (new Vector3 (0, 0, 0));
        arrTopCardVector.Add (new Vector3 (150, 0, 0));
        arrTopCardVector.Add (new Vector3 (300, 0, 0));
        //Gobj = new int[3];
        SelectCardListPOS.Add (false);
        SelectCardListPOS.Add (false);
        SelectCardListPOS.Add (false);
    }

    void MIXCARDSELECT ()
    {
        arrEachCard = new List<GameObject> ();
        arrCardMixItemSelect = new List<GameObject> ();
        TopCardCardPosInit ();
        GameObject BottomCardLine, KickerCard, GkCard;
        KickerCard = (GameObject)Resources.Load ("prefab_General/KickerCard");
        GkCard = (GameObject)Resources.Load ("prefab_General/Gk_Card");
        BottomCardLine = mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).gameObject;
        for (int i = 0; i < arrAllCard.Count; i++) {
            DestroyObject (arrAllCard [i]);
        }
        arrAllCard.Clear ();
        for (int i = 0; i < Ag.mySelf.arrCard.Count; i++) {
            if (Ag.mySelf.arrCard [i].WAS.kickOrder > -1)
                continue;
            //-----------------------------------------------------------------
            GameObject PlayerCard;
            if (Ag.mySelf.arrCard [i].WAS.isKicker) {
                PlayerCard = Instantiate (KickerCard) as GameObject;
            } else {
                PlayerCard = Instantiate (GkCard)as GameObject;
            }
            Debug.Log (Ag.mySelf.arrCard [i].mID+ "id");
            PlayerCard.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrCard [i].WAS;
            PlayerCard.GetComponent<PlayerCardInfo> ().mCard = Ag.mySelf.arrCard [i];
            //Debug.Log ("PlayerCardINfo" + PlayerCard.GetComponent<PlayerCardInfo> ().mCard.WAS.kickOrder + "WAS" + PlayerCard.GetComponent<PlayerCardInfo> ().mwas.kickOrder);
            PlayerCard.transform.parent = BottomCardLine.transform;
            PlayerCard.transform.localScale = new Vector3 (1, 1, 1);
            PlayerCard.transform.localPosition = new Vector3 (0, 0, -25f);
            PlayerCard.name = "BottomCard" + PlayerCard.GetComponent<PlayerCardInfo> ().mwas.ID;
            BottomCardLine.GetComponent<UIGrid> ().Reposition ();
            PlayerCard.AddComponent<UIButtonMessage> ().functionName = "SendMixCardInfo";
            PlayerCard.GetComponent<UIButtonMessage> ().target = PlayerCard;
            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (PlayerCard, "btn_playerinfo", true), PlayerCard, "PlayerDetail");

            PlayerCard.GetComponent<PlayerCardInfo> ().CardInit ();
        }
        arrSelected = new List<GameObject> ();
        foreach (Transform child in BottomCardLine.transform) {
            arrAllCard.Add (child.gameObject);

        }


        StartCoroutine (RepositionCardMix ());

    }

    void SelectCard (GameObject GmObj, bool Playerinfo, bool cardSelect, bool choice)
    {
        //mRscrcMan.FindChild (GmObj, "btn_playerinfo", Playerinfo);
        mRscrcMan.FindChild (GmObj, "cardselect", cardSelect);
        mRscrcMan.FindChild (GmObj, "choice", choice);
    }

    void TopCardRemoved (GameObject GmObj)
    {
        for (int i = 0; i < arrCardMixItemSelect.Count; i++) {

            if (arrCardMixItemSelect [i].name == GmObj.name) {

                DestroyObject (GmObj);
                Debug.Log ("arrCardMixItemSelect[i].GetComponent<PlayerCardInfo>().MixOrderNum " + arrCardMixItemSelect [i].GetComponent<PlayerCardInfo> ().MixOrderNum);
                SelectCardListPOS [arrCardMixItemSelect [i].GetComponent<PlayerCardInfo> ().MixOrderNum] = false;

                for (int j = 0; j < SelectCardListPOS.Count; j++) {
                    Debug.Log (SelectCardListPOS [j] + "  Select Bool    ");
                }
                SelectCard (arrEachCard [i], false, false, false);
                arrEachCard.RemoveAt (i);
                arrCardMixItemSelect.RemoveAt (i);

                return;
            }
        }
    }

    void CardInstantiate (GameObject GmObj)
    {
        GameObject TopCard;
        TopCard = (GameObject)Instantiate (GmObj);
        TopCard.name = GmObj.name;
        TopCard.GetComponent<PlayerCardInfo> ().MixCardChoice = true;
        TopCard.GetComponent<PlayerCardInfo> ().mwas = GmObj.GetComponent<PlayerCardInfo>().mwas;
        TopCard.GetComponent<PlayerCardInfo> ().mCard = GmObj.GetComponent<PlayerCardInfo>().mCard;
        TopCard.transform.parent = dicMenuList ["TopCardLineCardMix"].transform;
        TopCard.transform.localScale = new Vector3 (1, 1, 1);

        arrCardMixItemSelect.Add (TopCard);
        SelectCard (TopCard, false, false, false);

        for (int i = 0; i < SelectCardListPOS.Count; i++) {
            if (SelectCardListPOS [i] == false) {
                TopCard.transform.localPosition = arrTopCardVector [i];

                TopCard.GetComponent<PlayerCardInfo> ().MixOrderNum = i;
                SelectCardListPOS [i] = true;
                return;
            }
        }
    }
    /*
    void CardCommonSelect (GameObject GmObj)
    {
        if (arrSelected.Count == 0) {
            //Ag.LogString ("CardChoic");
            arrSelected.Add (GmObj);
            SelectCard (GmObj, true, true, false); //first Setting
        } else {
            if (arrSelected [0].name == GmObj.name) {
                //Ag.LogString ("PlayerInfo1");
                SelectCard (GmObj, false, true, true);
                arrEachCard.Add (GmObj);
                CardInstantiate (GmObj);
                return;

            } else {
                //Ag.LogString ("PlayerInfo2");
                SelectCard (arrSelected [0], false, false, false);
                SelectCard (GmObj, true, true, false);
                arrSelected.Clear ();
                arrSelected.Add (GmObj);
                return;
            }
        }
        return;
    }
    */

    List<GameObject> ArrCell = new List<GameObject>();

    void xxCardSelect (GameObject GmObj)
    {
        if (ArrCell.Count < 3)
            ArrCell.Add (GmObj);
        for (int k=0; k<3; k++) {}
    }

    void CancelSelect (GameObject GmObj)
    {
        ArrCell [ArrCell.IndexOf (GmObj)] = null;
    }

    void CardCommonSelect (GameObject GmObj)
    {
        if (arrSelected.Count == 0) {
            //Ag.LogString ("CardChoic");
            for (int i = 0; i < arrEachCard.Count; i++) {
                if (arrEachCard [i].name == GmObj.name) {
                    //Ag.LogString ("SelectCard Removoed");
                    CardSelect (GmObj);
                    Debug.Log ("SameCard");
                    return;
                }
            }

            //CardSelect (GmObj);
            SelectCard (GmObj, false, true, true);
            arrEachCard.Add (GmObj);
            arrSelected.Add (GmObj);
            CardInstantiate (GmObj);
            Debug.Log ("07");
            return;
        } else {
            if (arrSelected [0].name == GmObj.name) {
                //Ag.LogString ("PlayerInfo1");
                SelectCard (GmObj, true, false, false);
                arrSelected.Clear ();
                CardSelect (GmObj);
                //TopCardRemoved (GmObj);
                Ag.LogString ("Select Card");
                Debug.Log ("08");
                return;
                
            } else {

                for (int i = 0; i < arrEachCard.Count; i++) {
                    if (arrEachCard [i].name == GmObj.name) {
                        //Ag.LogString ("SelectCard Removoed");
                        CardSelect (GmObj);
                        Debug.Log ("SameCard");
                        return;
                    }
                }
                Ag.LogString ("CardSelectAlrday");
                //SelectCard (arrSelected [0], false, false, false);
                arrSelected.Clear ();
                SelectCard (GmObj, true, true, true);
                arrEachCard.Add (GmObj);
                arrSelected.Add (GmObj);
                CardInstantiate (GmObj);
                arrSelected.Add (GmObj);
                return;
            }
        }
        return;
    }


    void CardSelect (GameObject GmObj)
    {
        for (int i = 0; i < arrEachCard.Count; i++) {
            if (arrEachCard [i].name == GmObj.name) {
                //Ag.LogString ("SelectCard Removoed");
                SelectCard (arrEachCard [i], false, false, false);
                if (arrSelected.Count > 0)
                    SelectCard (arrSelected [0], false, false, false);
                arrSelected.Clear ();
                arrEachCard.RemoveAt (i);
                DestroyObject (arrCardMixItemSelect [i]);
                SelectCardListPOS [arrCardMixItemSelect [i].GetComponent<PlayerCardInfo> ().MixOrderNum] = false;
                arrCardMixItemSelect.RemoveAt (i);
                return;
            }
        }
        return;
    }

    void CardSelectAlready ()
    {
        for (int i = 0; i < arrEachCard.Count; i++) {
            SelectCard (arrEachCard [i], false, true, true);
        }
    }

    void CardMixSelect (GameObject GmObj)
    {
        if (GmObj.GetComponent<PlayerCardInfo> ().MixCardChoice == true) {
            TopCardRemoved (GmObj);
        }
        if (GmObj.GetComponent<PlayerCardInfo> ().MixCardChoice == false) {
            //CardSelect (GmObj);
            Ag.LogString ("Select Card2");

            if (arrEachCard.Count == 0) {
                Debug.Log ("03");
                CardCommonSelect (GmObj);
                return;
            }
            if (arrEachCard.Count == 1) {
                Debug.Log ("04");
                CardCommonSelect (GmObj);
                CardSelectAlready ();
                return;

            }
            if (arrEachCard.Count == 2) {
                Debug.Log ("05");
                CardCommonSelect (GmObj);
                CardSelectAlready ();
                return;
            }
            if (arrEachCard.Count == 3) {
                for (int i = 0; i < arrEachCard.Count; i++) {
                    if (arrEachCard [i].name == GmObj.name) {
                        //Ag.LogString ("SelectCard Removoed");
                        SelectCard (arrEachCard [i], false, false, false);
                        arrSelected.Clear ();
                        arrEachCard.RemoveAt (i);
                        DestroyObject (arrCardMixItemSelect [i]);
                        SelectCardListPOS [arrCardMixItemSelect [i].GetComponent<PlayerCardInfo> ().MixOrderNum] = false;
                        arrCardMixItemSelect.RemoveAt (i);
                        return;
                    }
                }
                StartCoroutine (Cardfull ());
                return;
            }
        }
    }

    string Key;

    public IEnumerator Cardfull ()
    {
        dicMenuList ["pop_cardfull"].SetActive (true);
        yield return new WaitForSeconds (1f);
        dicMenuList ["pop_cardfull"].SetActive (false);
    }

    public IEnumerator RepositionCardMix ()
    {

        yield return new WaitForSeconds (0.2f);
        mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_cardmix/card_character/grid", true).gameObject.GetComponent<UIGrid> ().Reposition ();
        //Btn_SortbyGrade_CardMixPopupClose();

    }
}
