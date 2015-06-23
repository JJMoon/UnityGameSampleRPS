using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class MenuManager : AmSceneBase
{
    Vector3 PreVector, ChangeVector;
    public GameObject PreCard, ChangeCard, CheckGameObj, CheckAfterGameObj;
    bool mCardChoice = false, PreCardFlag, ChangeCardFlag;
    string mPreName, mChangeName, ErrorCont;
    int mCost, mPrecost, mChangeCost, mTouchcount;
    List<GameObject> arrAllCard = new List<GameObject> ();
    List<GameObject> arrEachCard = new List<GameObject> ();
    List<Vector3> arrTopCardVector = new List<Vector3> ();
    bool mAlreadyChoiced = false;
    GameObject TopCardLine, BottomCardLine;
    WasCard TopCard, mBottomCard;
    int Level;

    int CardCostAll ()
    {
        Label_nowlevel ();
        int ChildNum, CostSum;
        CostSum = 0;
        ChildNum = dicMenuList ["TopCardLine"].transform.childCount;
        foreach (Transform child in dicMenuList ["TopCardLine"].transform) {
            CostSum += child.GetComponent<PlayerCardInfo> ().mCard.WAS.cost;
        }
        return CostSum;
    }

    void Label_nowlevel ()
    {
        int LeagueCost;
        switch (Ag.mySelf.WAS.League) {
        case "PRO_1":
            Level = Ag.mySelf.WAS.Cost - 23;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            if (Level == 7) {
                dicMenuList ["Label_nowlevel"].SetActive (false);
                dicMenuList ["Label_maxlevel"].SetActive (true);
            }
            break;
        case "PRO_2":
            Level = Ag.mySelf.WAS.Cost - 22;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            if (Level == 7) {
                dicMenuList ["Label_nowlevel"].SetActive (false);
                dicMenuList ["Label_maxlevel"].SetActive (true);
            }
            break;
        case "PRO_3":
            Level = Ag.mySelf.WAS.Cost - 21;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            if (Level == 7) {
                dicMenuList ["Label_nowlevel"].SetActive (false);
                dicMenuList ["Label_maxlevel"].SetActive (true);
            }
            break;
        case "PRO_4":
            Level = Ag.mySelf.WAS.Cost - 20;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            if (Level == 7) {
                dicMenuList ["Label_nowlevel"].SetActive (false);
                dicMenuList ["Label_maxlevel"].SetActive (true);
            }
            break;
        case "PRO_5":
            Level = Ag.mySelf.WAS.Cost - 19;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            if (Level == 7) {
                dicMenuList ["Label_nowlevel"].SetActive (false);
                dicMenuList ["Label_maxlevel"].SetActive (true);
            }
            break;

        }

    }

    void CostUpLabel ()
    {
        //Debug.Log ("0_1"); 
        int LeagueCost;
        switch (Ag.mySelf.WAS.League) {
        case "PRO_1":
            Level = Ag.mySelf.WAS.Cost - 23;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            break;
        case "PRO_2":
            Level = Ag.mySelf.WAS.Cost - 22;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            break;
        case "PRO_3":
            Level = Ag.mySelf.WAS.Cost - 21;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            break;
        case "PRO_4":
            Level = Ag.mySelf.WAS.Cost - 20;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            break;
        case "PRO_5":
            Level = Ag.mySelf.WAS.Cost - 19;
            dicMenuList ["Label_nowlevel"].GetComponent<UILabel> ().text = Level.ToString ();
            break;
        }
        //Debug.Log ("0");
        Label_nowlevel ();
        //Debug.Log ("1");

        dicMenuList ["Label_countlevel"].GetComponent<UILabel> ().text = (Level + 1).ToString ();
        //dicMenuList ["Label_beforelevel"].GetComponent<UILabel> ().text = Level.ToString () + "LV";
        //dicMenuList ["Label_afterlevel"].GetComponent<UILabel> ().text = (Level + 1).ToString () + "LV";
        dicMenuList ["Label_afterpoint"].GetComponent<UILabel> ().text = (Level + 1).ToString ();
        dicMenuList ["Label_beforepoint"].GetComponent<UILabel> ().text = (Ag.mySelf.WAS.Cost - Level).ToString ();

        //Debug.Log ("2");
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/Label_maxlevelpoint", true).GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();

        Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);

        dicMenuList ["popup_levelup"].transform.FindChild ("division/div5").GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        //Debug.Log ("3");

        //Debug.Log ("4");
        CostUpPrice (Level);
        for (int i = 1; i < Level + 2; i++) {
            dicMenuList ["popup_levelup"].transform.FindChild ("levenimg_bundle/mark" + i).transform.gameObject.SetActive (true);
            dicMenuList ["popup_levelup"].transform.FindChild ("levenimg_bundle").transform.gameObject.SetActive (true);
        }

    }

    void TopCardPosInit ()
    {
        arrTopCardVector.Clear ();
        arrTopCardVector.Add (new Vector3 (-36.31042f, 23, 3.433228e-05f));
        arrTopCardVector.Add (new Vector3 (140, 23, 0));
        arrTopCardVector.Add (new Vector3 (280, 23, 0));
        arrTopCardVector.Add (new Vector3 (420, 23, 0));
        arrTopCardVector.Add (new Vector3 (560, 23, 0));
        arrTopCardVector.Add (new Vector3 (700, 23, 0));
    }

    void AllCardSelect ()
    {
        TopCardPosInit ();
        arrEachCard = new List<GameObject> ();
        GameObject KickerCard, GkCard;
        KickerCard = (GameObject)Resources.Load ("prefab_General/KickerCard");
        GkCard = (GameObject)Resources.Load ("prefab_General/Gk_Card");

        for (int i = 0; i < arrAllCard.Count; i++) {
            DestroyObject (arrAllCard [i]);
        }
        arrAllCard.Clear ();
        for (int i = 0; i < Ag.mySelf.arrCard.Count; i++) {
            //Deleate 
            /*
            if (i < 6)
                Ag.mySelf.arrCard [i].WAS.kickOrder = i;
                */
            //-----------------------------------------------------------------
            GameObject PlayerCard;
            if (Ag.mySelf.arrCard [i].WAS.isKicker) {
                PlayerCard = Instantiate (KickerCard) as GameObject;
            } else {
                PlayerCard = Instantiate (GkCard) as GameObject;
            }
            //Debug.Log (Ag.mySelf.arrCard [i].mID + "id");
            PlayerCard.GetComponent<PlayerCardInfo> ().mwas = Ag.mySelf.arrCard [i].WAS;
            PlayerCard.GetComponent<PlayerCardInfo> ().mCard = Ag.mySelf.arrCard [i];
            if (PlayerCard.GetComponent<PlayerCardInfo> ().mCard.WAS.kickOrder > -1) {
                PlayerCard.transform.parent = dicMenuList ["TopCardLine"].transform;
                PlayerCard.transform.localScale = new Vector3 (1, 1, 1);
                PlayerCard.transform.localPosition = arrTopCardVector [PlayerCard.GetComponent<PlayerCardInfo> ().mCard.WAS.kickOrder];
                PlayerCard.name = "TopCard" + PlayerCard.GetComponent<PlayerCardInfo> ().mCard.WAS.kickOrder;
            } else {
                PlayerCard.transform.parent = dicMenuList ["BottomCardLine"].transform;
                PlayerCard.transform.localScale = new Vector3 (1, 1, 1);
                PlayerCard.transform.localPosition = new Vector3 (0, 0, -25f);
                PlayerCard.name = "BottomCard" + PlayerCard.GetComponent<PlayerCardInfo> ().mCard.WAS.ID;
                //mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_team/LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
                //PlayerCard.transform.localPosition = arrTopCardVector [PlayerCard.GetComponent<PlayerCardInfo> ().mwas.kickOrder];
            }
            PlayerCard.AddComponent<UIButtonMessage> ().functionName = "SendCardInfo";
            PlayerCard.GetComponent<UIButtonMessage> ().target = PlayerCard;

            mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (PlayerCard, "btn_playerinfo", true), PlayerCard, "PlayerDetail");
            PlayerCard.GetComponent<PlayerCardInfo> ().CardInit ();
            arrAllCard.Add (PlayerCard);

            mRscrcMan.FindChild (arrAllCard [i], "btn_playerinfo", true);
            mRscrcMan.FindChild (arrAllCard [i], "cardselect", false);
        }
        StartCoroutine (CardRepositionNow (0.2f));
    }

    void LineUpNationFlagEffOnOff ()
    {
        if (Ag.mySelf.DeckItemEA > 0) {
            foreach (Transform child in dicMenuList ["TopCardLine"].transform) {
                if (child.GetComponent<PlayerCardInfo> ().mwas.country == Ag.mySelf.mMaxCountry)
                    child.gameObject.transform.FindChild ("nations_eff").gameObject.SetActive (true);
                else
                    child.gameObject.transform.FindChild ("nations_eff").gameObject.SetActive (false);
            } 
            foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
                child.gameObject.transform.FindChild ("nations_eff").gameObject.SetActive (false);
            }
        } else {
            foreach (Transform child in dicMenuList ["TopCardLine"].transform) {
                child.gameObject.transform.FindChild ("nations_eff").gameObject.SetActive (false);
            } 
            foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
                child.gameObject.transform.FindChild ("nations_eff").gameObject.SetActive (false);
            }
        }

    }

    void LineUpSetting ()
    {
        Ag.LogDouble ("      MangerTeam.cs  LineUpSetting   ");


        object arrMain = Ag.mySelf.GetMainCards ();

        foreach (Transform child in dicMenuList ["TopCardLine"].transform) {
            int kickOdr = int.Parse (child.name.Substring (child.name.Length - 1, 1));
            //child.GetComponent<PlayerCardInfo> ().mCard.KickOrder = kickOdr;
            Ag.mySelf.SetKickOrder (child.GetComponent<PlayerCardInfo> ().mCard.WAS.ID, kickOdr);

            Ag.LogString ("             ManagerTeam  >>  ID : " + child.GetComponent<PlayerCardInfo> ().mCard.WAS.ID + "  kickOrder : " + kickOdr);
            //Ag.LogString ( "ManagerTeam  >>  Kick Order " + child.GetComponent<PlayerCardInfo> ().mCard.KickOrder);
        } 

        Ag.LogStartWithStr (1, "      MangerTeam.cs  LineUpSetting    After Set Main Cards .... ");
        arrMain = Ag.mySelf.GetMainCards ();

        foreach (Transform child in mRscrcMan.FindChild (dicMenuList["Ui_team"], "LPanel_lineup/card_character/grid", true).transform) {
            //child.GetComponent<PlayerCardInfo> ().mCard.KickOrder = -1;
            Ag.mySelf.SetKickOrder (child.GetComponent<PlayerCardInfo> ().mCard.WAS.ID, -1);
            Ag.LogString ("              ManagerTeam Rest  set -1 >>  ID : " + child.GetComponent<PlayerCardInfo> ().mCard.WAS.ID);
        }

        Ag.LogStartWithStr (1, "      MangerTeam.cs  LineUpSetting    After Set Sub .....  Cards .... ");

        arrMain = Ag.mySelf.GetMainCards ();

        Ag.mySelf.ApplyCurrentDeck ();

        ShowCardEff ();
    }

    string prevDeckCountry;
    int prevDeckNum;

    bool DeckPropertyChanged ()
    {
        bool rVal = false;
        rVal = Ag.mySelf.mMaxCountry != prevDeckCountry || Ag.mySelf.DeckItemEA != prevDeckNum;

        prevDeckCountry = Ag.mySelf.mMaxCountry;
        prevDeckNum = Ag.mySelf.DeckItemEA;

        return rVal;
    }

    void ShowCardEff ()
    {
        if (DeckPropertyChanged ()) {
            ShowDeckEffLabel ();
            LineUpNationFlagEffOnOff ();
            InitDeck ();
        }
    }

    void Dec_effect_Loop ()
    {
        dicMenuList ["dec_effect_loop"].SetActive (true);
        if (Ag.mySelf.DeckItemEA == 1)
            dicMenuList ["dec_effect_loop_4set"].SetActive (true);
        if (Ag.mySelf.DeckItemEA == 2)
            dicMenuList ["dec_effect_loop_5set"].SetActive (true);
        if (Ag.mySelf.DeckItemEA == 3)
            dicMenuList ["dec_effect_loop_6set"].SetActive (true);
    }

    void ShowDeckEffLabel ()
    {
        dicMenuList ["dec_bundle"].SetActive (false);
        dicMenuList ["starting_dis"].SetActive (true);
        dicMenuList ["4set_label"].SetActive (false);
        dicMenuList ["5set_label"].SetActive (false);
        dicMenuList ["6set_label"].SetActive (false);
        dicMenuList ["dec_effect"].SetActive (false);
        dicMenuList ["dec_4effect"].SetActive (false);
        dicMenuList ["dec_5effect"].SetActive (false);
        dicMenuList ["dec_6effect"].SetActive (false);
        dicMenuList ["dec_effect_loop"].SetActive (false);
        dicMenuList ["dec_effect_loop_4set"].SetActive (false);
        dicMenuList ["dec_effect_loop_5set"].SetActive (false);
        dicMenuList ["dec_effect_loop_6set"].SetActive (false);

        Dec_effect_Loop ();

        if (Ag.mySelf.DeckItemEA > 0) {
            dicMenuList ["dec_bundle"].SetActive (true);
            dicMenuList ["starting_dis"].SetActive (false);
            //dicMenuList ["4set_label"].transform.FindChild ("ability_off").gameObject.SetActive (false);
            //dicMenuList ["5set_label"].transform.FindChild ("ability_off").gameObject.SetActive (false);
            //dicMenuList ["6set_label"].transform.FindChild ("ability_off").gameObject.SetActive (false);
            dicMenuList ["dec_Label_nation"].gameObject.GetComponent<UILabel> ().text = Tbl.arrDecknationname [Ag.mySelf.mMaxCountry];
            dicMenuList ["dec_nationsflag"].gameObject.GetComponent<UISprite> ().spriteName = "flag_" + Ag.mySelf.mMaxCountry;

            SoundManager.Instance.Play_Effect_Sound ("Country Name_" + Ag.mySelf.mMaxCountry);

            //dicMenuList ["4set_label"].transform.FindChild ("ability_off/Label").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName[Ag.mySelf.WAS.DeckItem[0]];


            //dicMenuList ["5set_label"].transform.FindChild ("ability_off/Label").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName[Ag.mySelf.WAS.DeckItem[1]];

            //dicMenuList ["6set_label"].transform.FindChild ("ability_off/Label").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName[Ag.mySelf.WAS.DeckItem[2]];
            //dicMenuList ["6set_label"].transform.FindChild ("ability_on/Label_6setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [2]];
            dicMenuList ["dec_effect"].SetActive (true);
            if (Ag.mySelf.DeckItemEA == 1) {
                dicMenuList ["4set_label"].SetActive (true);
                dicMenuList ["4set_label"].transform.FindChild ("4set/ability_on/Label_4setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [0]];
//                dicMenuList ["4set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
//                dicMenuList ["5set_label"].transform.FindChild ("ability_off").gameObject.SetActive (true);
//                dicMenuList ["6set_label"].transform.FindChild ("ability_off").gameObject.SetActive (true);
                dicMenuList ["dec_4effect"].SetActive (true);
            }
                
            if (Ag.mySelf.DeckItemEA == 2) {
                dicMenuList ["5set_label"].SetActive (true);
                dicMenuList ["5set_label"].transform.FindChild ("4set/ability_on/Label_4setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [0]];
                dicMenuList ["5set_label"].transform.FindChild ("5set/ability_on/Label_5setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [1]];

//                dicMenuList ["4set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
//                dicMenuList ["5set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
//                dicMenuList ["6set_label"].transform.FindChild ("ability_off").gameObject.SetActive (true);
                dicMenuList ["dec_5effect"].SetActive (true);
            }
            if (Ag.mySelf.DeckItemEA == 3) {
                dicMenuList ["6set_label"].SetActive (true);
                dicMenuList ["6set_label"].transform.FindChild ("4set/ability_on/Label_4setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [0]];
                dicMenuList ["6set_label"].transform.FindChild ("5set/ability_on/Label_5setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [1]];
                dicMenuList ["6set_label"].transform.FindChild ("6set/ability_on/Label_6setabil").gameObject.GetComponent<UILabel> ().text = Tbl.arrDeckBuffName [Ag.mySelf.WAS.DeckItem [2]];
//                dicMenuList ["4set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
//                dicMenuList ["5set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
//                dicMenuList ["6set_label"].transform.FindChild ("ability_on").gameObject.SetActive (true);
                dicMenuList ["dec_6effect"].SetActive (true);
            }

        } else {
            dicMenuList ["starting_dis"].SetActive (true);
            dicMenuList ["dec_bundle"].SetActive (false);
        }
    }

    void InitDeck ()
    {
        if (Ag.mySelf.DeckItemEA > 1) {
            //Ag.mySelf.mMaxCountry = "";

            foreach (KeyValuePair<string, string> kv in Tbl.dicDeckNation) {
                string key = "Dec_nation_" + kv.Value;
                dicMenuList [key].SetActive (false);
            }

            //Debug.Log ("Ag.mySelf.mMaxCountry :: " + Ag.mySelf.mMaxCountry);
            dicMenuList ["Dec_nation_" + Tbl.dicDeckNation [Ag.mySelf.mMaxCountry]].SetActive (true);
        }

        Ag.mySelf.mMaxCountry = "";
    }

    GameObject Selected;

    void CardSelectUI (GameObject curCard)
    {
        if (curCard == null)
            return;
        if (Selected != null)
            SelectReleaseUI ();
        Selected = curCard;
        FindMyChild (Selected, "cardselect", true);
        FindMyChild (Selected, "choice", true);
    }

    void SelectReleaseUI ()
    {
        Ag.LogString (" Select Release UI ");
        if (Selected == null)
            return;
        Ag.LogString (" Select Release UI Not Null ");
        FindMyChild (Selected, "cardselect", false);
        FindMyChild (Selected, "choice", false);
        Selected = null;
        Ag.LogString (" Select Release UI is Null ");
    }

    bool IsSwapableCase (GameObject GmObj, out bool KeeperKickerSwapCase, out bool CostError, out bool HuboError, out bool SameNameError)
    {
        AmCard SelCrd = Selected.GetComponent<PlayerCardInfo> ().mCard;
        AmCard CurCrd = GmObj.GetComponent<PlayerCardInfo> ().mCard;
        SameNameError = HuboError = CostError = KeeperKickerSwapCase = false;

        Ag.LogStartWithStr (1, "   MenuManager _ Manager Team . cs          Is IsSwapableCase ....  ");

        AmCard mainC, subbC;
        if (SelCrd.WAS.kickOrder >= 0) {
            mainC = SelCrd;
            subbC = CurCrd;
        } else {
            mainC = CurCrd;
            subbC = SelCrd;
        }

        Ag.LogString ("   MenuManager _ Manager Team  ::    " + mainC.WAS.country + " is MainC PlayerCountry         " + subbC.WAS.country + " is SubCC PlayerCountry  ");

        // Keeper / Kicker Check..
        if (SelCrd.WAS.isKicker != CurCrd.WAS.isKicker) {
            KeeperKickerSwapCase = true;
            return false;
        }

        // Main Kicker Swap ...
        if (SelCrd.KickOrder > 0 && CurCrd.KickOrder > 0)
            return true;

        // Swap Between Hubos
        if (SelCrd.KickOrder < 0 && CurCrd.KickOrder < 0) {
            HuboError = true;
            return false;
        }

        // Special Card ..
        Ag.LogString ("   Special Card Check....    main >> " + mainC.IsSpecialCard + "   subb >> " + subbC.IsSpecialCard + "  S CardNum  >> " + Ag.mySelf.SpecialCardNum);
        if (!mainC.IsSpecialCard && subbC.IsSpecialCard && Ag.mySelf.SpecialCardNum >= 2) {
            StartCoroutine (DontchangeCardPosi (1f, "주전 선수에는 스페셜선수가 2명이상 포함될수 없습니다.", false));
            return false;
        }

        Ag.LogString ("   MenuManager _ Manager Team  ::  " + mainC.WAS.country + " MainC Playerid  " + subbC.WAS.country + "SubCC Playerid" + "2");

        Ag.LogString ("   MenuManager _ Manager Team  ::  " + mainC.WAS.country + " MainC Playerid  " + subbC.WAS.country + "SubCC Playerid" + Ag.mySelf.CanThisJoin2MainCards (mainC, subbC));
        /*
        if (!Ag.mySelf.CanThisJoin2MainCards (mainC, subbC)) {
            SameNameError = true;
            return false;
        }
        */

        // Lower Swap is always open..
        int mainCrdCost = SelCrd.WAS.cost, huboCrdCost = CurCrd.WAS.cost;
        if (SelCrd.KickOrder < 0)
            Ag.Swap<int> (ref mainCrdCost, ref huboCrdCost);
        if (mainCrdCost >= huboCrdCost)
            return true;

        // Cost Compare
        Ag.Swap<int> (ref SelCrd.WAS.kickOrder, ref CurCrd.WAS.kickOrder);
        if ((CardCostAll () - CurCrd.WAS.cost + SelCrd.WAS.cost) > Ag.mySelf.WAS.Cost) {
            Ag.Swap<int> (ref SelCrd.WAS.kickOrder, ref CurCrd.WAS.kickOrder);
            CostError = true;
            return false;

        }





        if ((CardCostAll () - SelCrd.WAS.cost + CurCrd.WAS.cost) > Ag.mySelf.WAS.Cost) {
			
            Ag.Swap<int> (ref SelCrd.WAS.kickOrder, ref CurCrd.WAS.kickOrder);
            CostError = true;
            return false;
			
        }

        Ag.Swap<int> (ref SelCrd.WAS.kickOrder, ref CurCrd.WAS.kickOrder);
        return true;
    }

    void SwapCards (GameObject GmObj)
    {
        Ag.Swap<AmCard> (ref Selected.GetComponent<PlayerCardInfo> ().mCard, ref GmObj.GetComponent<PlayerCardInfo> ().mCard);
        Ag.Swap<WasCard> (ref Selected.GetComponent<PlayerCardInfo> ().mwas, ref GmObj.GetComponent<PlayerCardInfo> ().mwas);
        Selected.GetComponent<PlayerCardInfo> ().CardInit ();
        GmObj.GetComponent<PlayerCardInfo> ().CardInit ();

        LineUpSetting ();
        SelectReleaseUI ();
        StartCoroutine (CardRepositionNow (0.2f));
    }

    void CardPosChange (GameObject GmObj)
    {
        if (Selected == null) {
            CardSelectUI (GmObj);
            return;
        }
        //Ag.LogString (" Manager Team.cs :: Card Pos Change   Selected is not NULL ");
        if (Selected == GmObj) {
            SelectReleaseUI ();
            return;
        }
        bool KickKeepErr, CostError, hunoError, SameNameError;
        //Ag.LogString ("Card Pos Change   Is Swapable ?? " + IsSwapableCase (GmObj));
        if (IsSwapableCase (GmObj, out KickKeepErr, out CostError, out hunoError, out SameNameError)) {
            SwapCards (GmObj);
        } else {
            if (KickKeepErr) {
                StartCoroutine (DontchangeCardPosi (1f, "킥커와 골키퍼는 위치를 바꿀수 없습니다.", false));

            }
            if (CostError) {
                StartCoroutine (DontchangeCardPosi (1f, "선발 코스트가 초과되었습니다.", true));
                return;
            }
            if (SameNameError) {
                StartCoroutine (DontchangeCardPosi (1f, "동일한 선수가 이미 포함되어있습니다.", false));
                return;
            }
            //if (hunoError)
            //StartCoroutine (DontchangeCardPosi (1f, "후보간에는 서로 교체할수 없습니다.", true));
            Ag.LogString ("IsSwapableCase Is false");
            CardSelectUI (GmObj);
        }
		
    }

    public  IEnumerator DontchangeCardPosi (float pTime, string Cont, bool CostAlert)
    {
        if (CostAlert) {
            dicMenuList ["Label_mylevelpoint"].SetActive (false);
            dicMenuList ["mylevelpoint_alert"].SetActive (true);
        }
        dicMenuList ["pop_dontchange"].SetActive (true);
        dicMenuList ["pop_dontchange"].transform.FindChild ("Label_content").GetComponent<UILabel> ().text = Cont;
        yield return new WaitForSeconds (pTime);
        if (CostAlert) {
            MyCostAlert ();
        }

        dicMenuList ["pop_dontchange"].SetActive (false);

    }

    public  IEnumerator CardRepositionNow (float pTime)
    {
        yield return new WaitForSeconds (pTime);
        //mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_team/LPanel_lineup/card_character/grid", true).GetComponent<UIGridTuning> ().sorted = true;
        try {
            Ag.LogDouble ("   >>>   IEnumerator CardRepositionNow    ");
            mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_team/LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            Btn_Fun_SortByGrade_setting ();
            //mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/card_character/grid", true).GetComponent<UIGrid> ().Reposition ();
            MyCostAlert ();
            //mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_team/LPanel_lineup/bundle_startinglevel/Label_maxlevelpoint", true).GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();
            mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/Label_maxlevelpoint", true).GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();
            //Btn_Fun_SortByGrade_setting ();
        } catch {
            Debug.Log ("Error");
        }
    }

    void MyCostAlert ()
    {
        if (CardCostAll () > Ag.mySelf.WAS.Cost) {
            dicMenuList ["Label_mylevelpoint"].SetActive (false);
            dicMenuList ["mylevelpoint_alert"].SetActive (true);
        } else {
            dicMenuList ["Label_mylevelpoint"].SetActive (true);
            dicMenuList ["mylevelpoint_alert"].SetActive (false);
        }
        dicMenuList ["Label_mylevelpoint"].GetComponent<UILabel> ().text = CardCostAll ().ToString ();
        dicMenuList ["mylevelpoint_alert"].GetComponent<UILabel> ().text = CardCostAll ().ToString ();

    }

    public  IEnumerator CardCostAll2 (float pTime)
    {

        yield return new WaitForSeconds (pTime);
        MyCostAlert ();
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_startinglevel/Label_maxlevelpoint", true).GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();
    }
}
