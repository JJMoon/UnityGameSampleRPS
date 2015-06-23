//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


// [2012:10:10:MOON] Heart Beat
// [2012:10:18:MOON] Come from Game...
// [2012:10:29:MOON] Tuning...
// [2012:11:13:MOON] Network Debugging
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    Dictionary<string,GameObject> dicPrePareMenuList = new Dictionary<string, GameObject> ();
    Dictionary<string,GameObject> dicPlayerOrObj = new Dictionary<string, GameObject> ();
    public GameObject mEnemyPlayer, mCoin, Sound, mPlayerDir, 
        mMyflag, mEnemyFlag, mBtnBack, mWaiting, mAdv3, mLodingbar3, mKakaoNick, mPrepare;
    Texture2D mkakaoPic;
    // Use this for initialization
    public string NameFolder = "Menu_Camera2/Menu_Lineup/UI Root/Panel/KickerPlayer";
    StateArray arrState;
    GameObject mTargetObj;
    Material Shirts, Pants, Socks;
    List<GameObject> arrPlayerMain = new List<GameObject> ();
    string mCurState;
    ProceduralMaterial mEnemyMaterial;
    Texture KickermShirts, KickerPants, KickermSocks;
    //public GameObject mAppfriendBar;
    //------------------------------------Get User iD
    void AlertOk ()
    {
        dicMenuList ["Ui_"].SetActive (false);
        dicMenuList ["alert"].SetActive (false);
    }

    void PrepareMatchInit ()
    {
        dicPlayerOrObj.Add ("Refree", mRscrcMan.FindGameObject ("PrePareMatch/Referee_Moreno", true)); 
        dicPlayerOrObj.Add ("Coin", mRscrcMan.FindGameObject ("PrePareMatch/coin", true));

        dicMenuList.Add ("PrePareMatch", mRscrcMan.FindGameObject ("PrePareMatch", false));

        try {
            subKickerShirts = Resources.LoadAll ("Textures/Substance/Kickershirts", typeof(ProceduralMaterial));
            subPants = Resources.LoadAll ("Textures/Substance/Pants", typeof(ProceduralMaterial));
            subSocks = Resources.LoadAll ("Textures/Substance/Socks", typeof(ProceduralMaterial));

            EnemysubKickerShirts = Resources.LoadAll ("Textures/EnemySubstance/Kickershirts", typeof(ProceduralMaterial));
            EnemysubPants = Resources.LoadAll ("Textures/EnemySubstance/Pants", typeof(ProceduralMaterial));
            EnemysubSocks = Resources.LoadAll ("Textures/EnemySubstance/Socks", typeof(ProceduralMaterial));
        } catch {
            //Debug.Log ("UNIFORM LOAD ERROR");
        }

        MyShirts = (Material)Resources.Load ("Materials/MyShirts");
        MyPants = (Material)Resources.Load ("Materials/MyPants");
        MySocks = (Material)Resources.Load ("Materials/MySocks");

        Pants = (Material)Resources.Load ("Materials/PantsMaterial");
        Socks = (Material)Resources.Load ("Materials/SocksMaterial");
        Shirts = (Material)Resources.Load ("Materials/ShirtMaterial");


        dicPrePareMenuList = new Dictionary<string, GameObject> ();


        //dicPrePareMenuList.Add ("Ui_tutorial", mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_tutorial", false));
        //dicPrePareMenuList.Add ("Panel_txt", mRscrcMan.FindChild (dicPrePareMenuList ["Ui_tutorial"], "Panel_txt", true));
        //dicPrePareMenuList.Add ("Matching", mRscrcMan.FindGameObject ("matching", false));
        //dicPrePareMenuList.Add ("MainSound", mRscrcMan.FindGameObject ("MainSound", true));

        //---------------------------------------------------------------------------- SubMenu
        //dicPrePareMenuList.Add ("ExitMatch", mRscrcMan.FindChild (dicPrePareMenuList["Matching"],"Camera/Anchor/Panel_base/btn_exit", true));
        //---------------------------------------------------------------------------- MyPlayer 



        //---------------------------------------------------------------------------- Refree

        //---------------------------------------------------------------------------- Menu

        //mTargetObj = mRscrcMan.FindGameObject ("Ui_camera", true);

        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicMenuList["Ui_setup"],"Panel_top/cash",true), mTargetObj, "Btn_Fun_MatchBoxShopCash");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicMenuList["Ui_setup"],"Panel_top/coin",true), mTargetObj, "Btn_Fun_MatchBoxShopCoin");
        //mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicMenuList["Ui_setup"],"Panel_top/heart",true), mTargetObj, "Btn_Fun_MatchBoxShopHeart");

        //EnemysubKeeperShirts = Resources.LoadAll ("Textures/EnemySubstance/Keepershirts", typeof(ProceduralMaterial));



        arrState.SetStateWithNameOf ("Init");
    }

    void StateGominjungUI ()
    {
        dicMenuList ["data_user"].SetActive (true);
        dicMenuList ["ready_someone"].SetActive (false);
        dicMenuList ["btn_start"].SetActive (false);
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"))
            BgmSound.Instance.Pause ();
        dicMenuList ["Ball"].SetActive (false);
        mKicker.SetActive (false);
        dicMenuList ["PrePareMatch"].SetActive (true);
        dicMenuList ["Ui_setup"].SetActive (true);
        dicMenuList ["Ui_kickoff"].SetActive (false);
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (false);
        dicMenuList ["Start_Panel"].SetActive (true);
        if (!MyDataLoad) {
            MYDATALOAD ();
        }

        dicMenuList ["someonedec"].SetActive (false);
        dicMenuList ["Panel_top_cash"].gameObject.SetActive (false);
        dicMenuList ["Panel_top_coin"].gameObject.SetActive (false);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/btn_shop", false);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/btn_shop_non", true);

    }

    void EnemyUniformSetting ()
    {
        AmCard card = new AmCard ();

        Ag.LogString ("Ag.NodeObj.EnemyUser.GetCardOrderOf (1))   ::      " + Ag.NodeObj.EnemyUser.GetCardOrderOf (1).WAS.look);

        if (mEnemyPlayer == null)
            mEnemyPlayer = (GameObject)Instantiate (SetPolyGon (Ag.NodeObj.EnemyUser.GetCardOrderOf (1)));
        card = Ag.NodeObj.EnemyUser.GetCardOrderOf (1);

        mEnemyPlayer.transform.FindChild ("deleveryBall").gameObject.SetActive (false);
        //mEnemyPlayer.transform.position = new Vector3 (-0.54f, 0f, -0.64f);
        mEnemyPlayer.transform.position = new Vector3 (-0.5142088f, 0f, -0.7245067f);
        mEnemyPlayer.transform.localScale = new Vector3 (0.4107453f, 0.4107453f, 0.4107453f);
        mEnemyPlayer.transform.eulerAngles = new Vector3 (0, 358.9525f, 0);
        mEnemyPlayer.animation.Play ("Match_Kicker_walk(131)");

        try {
            EnemyUniform ();
        } catch {
            //Debug.Log ("Error");
        }

        mEnemyPlayer.transform.FindChild ("Clothes").renderer.materials [0].mainTexture = Shirts.mainTexture;
        mEnemyPlayer.transform.FindChild ("Clothes").renderer.materials [1].mainTexture = Pants.mainTexture;
        mEnemyPlayer.transform.FindChild ("Clothes").renderer.materials [2].mainTexture = Socks.mainTexture;
        //EnemyDataLoad = true;

        if (card.arrCostumeInCard.Count > 0) {

            for (int i = 0; i < card.arrCostumeInCard.Count; i++) {
                //Debug.Log ("CostumeNAme" + card.arrCostumeInCard [i].WAS.itemTypeId);
                PlayerCostume.instance.SetCostumeToPlayer (true, mEnemyPlayer, (card.arrCostumeInCard [i].WAS.itemTypeId));
            }
        } else {
            PlayerCostume.instance.SetCostumeToPlayer (true, mEnemyPlayer, "KickerShoes01");
        }

        Ag.LogString ("        EnemyStart .... Done  ");
    }

    void MyUniform ()
    {
        mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.mySelf.arrUniform [0].Kick.Shirt.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub);
        MyShirts.mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)subPants [Ag.mySelf.arrUniform [0].Kick.Pants.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Pants.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Pants.ColSub);
        MyPants.mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)subSocks [Ag.mySelf.arrUniform [0].Kick.Socks.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Socks.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Socks.ColSub);
        MySocks.mainTexture = mProcedureMat.mainTexture;
    }

    void EnemyUniform ()
    {
        //Debug.Log (Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.Texture - 1);
        //Debug.Log (Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColMain - 1);
        //Debug.Log (Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColSub - 1);

        mProcedureMat = (ProceduralMaterial)EnemysubKickerShirts [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColSub);
        Shirts.mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.ColSub);
        Pants.mainTexture = mProcedureMat.mainTexture;
        mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.Texture - 1];
        UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.ColMain);
        UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.ColSub);
        Socks.mainTexture = mProcedureMat.mainTexture;

    }

    void Kick_OffInit ()
    {
        arrState = new StateArray ();
        StateMachineSetup ();
        PrepareMatchInit ();
    }

    void UNiformSetColorColor (string pStr, int pInt)
    {
        switch (pInt) {
        case 0:
            Uniform_SetColor2 (pStr, new Color (0.11f, 0.11f, 0.11f));
            break;
        case 1:
            Uniform_SetColor2 (pStr, new Color (0.25f, 0.25f, 0.25f));
            break;
        case 2:
            Uniform_SetColor2 (pStr, new Color (0.7f, 0.7f, 0.7f));
            break;
        case 3:
            Uniform_SetColor2 (pStr, new Color (0.5f, 0.07f, 0.05f));
            break;
        case 4:
            Uniform_SetColor2 (pStr, new Color (0.63f, 0.18f, 0.05f));
            break;
        case 5:
            Uniform_SetColor2 (pStr, new Color (0.68f, 0.35f, 0));
            break;
        case 6:
            Uniform_SetColor2 (pStr, new Color (0.66f, 0.6f, 0));
            break;
        case 7:
            Uniform_SetColor2 (pStr, new Color (0.15f, 0.39f, 0.07f));
            break;
        case 8:
            Uniform_SetColor2 (pStr, new Color (0.15f, 0.62f, 0.7f));
            break;
        case 9:
            Uniform_SetColor2 (pStr, new Color (0.07f, 0.27f, 0.58f));
            break;
        case 10:
            Uniform_SetColor2 (pStr, new Color (0.078f, 0.125f, 0.53f));
            break;
        case 11:
            Uniform_SetColor2 (pStr, new Color (0.25f, 0.03f, 0.35f));
            break;
        }
    }

    void EnemyNobody ()
    {
        //if (Ag.mgWorkState == Ag.WorkState.NOBODY102) {
        LineUpAniPlay ();
        DestroyObject (mEnemyPlayer);
        //dicPrePareMenuList ["mReady"].SetActive (false);
        dicPrePareMenuList ["mQuit"].SetActive (false);
        //}
    }

    void LineUpPlayerBarView (int Count)
    {
        for (int i = Count; i < 9; i++) {
            arrPlayerMain [i].SetActive (false);
        }
    }

    void LineUpAniPlay ()
    {
        dicPrePareMenuList ["mLineupBack"].animation ["Build_Lineup"].speed = 1f;
        dicPrePareMenuList ["mLineupBack"].animation.Play ("Build_Lineup");
        dicPrePareMenuList ["mMatchRequireBtn"].animation ["Match_Require"].speed = 1f;
        dicPrePareMenuList ["mMatchRequireBtn"].animation.Play ("Match_Require");

    }

    void MyinfoAniPlayR ()
    {
        //dicPrePareMenuList ["Myinfo"].animation ["Meny_Back_info"].speed = -1f;
        //dicPrePareMenuList ["Myinfo"].animation ["Meny_Back_info"].time = dicPrePareMenuList ["Myinfo"].animation ["Meny_Back_info"].length;
        //dicPrePareMenuList ["Myinfo"].animation.Play ("Meny_Back_info");

    }

    void EnemyInfoAniPlayR ()
    {
        //dicPrePareMenuList ["Enemyinfo"].animation ["Menu_Back_Enemyinfo"].speed = -1;
        //dicPrePareMenuList ["Enemyinfo"].animation ["Menu_Back_Enemyinfo"].time = dicPrePareMenuList ["Enemyinfo"].animation ["Menu_Back_Enemyinfo"].length;
        //dicPrePareMenuList ["Enemyinfo"].animation.Play ("Menu_Back_Enemyinfo");
    }

    void EnemyInfoAniPlay ()
    {
        dicPrePareMenuList ["Enemyinfo"].animation ["Menu_Back_Enemyinfo"].speed = 1;
        dicPrePareMenuList ["Enemyinfo"].animation.Play ("Menu_Back_Enemyinfo");
    }
    //---------------------------------------------------------------------------- stateMachine
    public void Visitor ()
    {
        GameSceneVisit ();
        //arrState.SetStateWithNameOf ("GoMinJung");
        //mRscrcMan.FindGameObject ("Ball/Kick/SoccerBall", true).GetComponent<SkinnedMeshRenderer> ().enabled = false;
        //Debug.Log ("goto Gominjung");
    }

    void MYDATALOAD ()
    {
        //DestroyObject (dicPlayerOrObj ["MyPlayer"]);
        AmCard card = new AmCard ();
        card = Ag.mySelf.GetCardOrderOf (1);
        SetPolyGon (Ag.mySelf.GetCardOrderOf (1));
        //dicPlayerOrObj.Add ("MyPlayer", (GameObject)Instantiate (mPoly)); // crash ... 
        dicPlayerOrObj ["MyPlayer"] = (GameObject)Instantiate (mPoly);
        dicPlayerOrObj ["MyPlayer"].transform.localPosition = new Vector3 (0.2011476f, -0.004949205f, -0.6948588f);
        dicPlayerOrObj ["MyPlayer"].transform.localEulerAngles = new Vector3 (0, 276.2166f, 0);
        dicPlayerOrObj ["MyPlayer"].transform.localScale = new Vector3 (0.4107453f, 0.4107453f, 0.4107453f);
        dicPlayerOrObj ["MyPlayer"].animation.Play ("08_Aps_10_(400F)");
        dicPlayerOrObj ["MyPlayer"].transform.FindChild ("deleveryBall").gameObject.SetActive (false);
        StartCoroutine(CaptureImage (dicPlayerOrObj ["MyPlayer"]));

        //Ag.LogIntenseWord ("MYDATALOAD" + Ag.mySelf.GetCardOrderOf (1).WAS.backNum);
        MyUniform ();

        dicPlayerOrObj ["MyPlayer"].transform.FindChild ("Clothes").renderer.materials [0].mainTexture = MyShirts.mainTexture;
        dicPlayerOrObj ["MyPlayer"].transform.FindChild ("Clothes").renderer.materials [1].mainTexture = MyPants.mainTexture;
        dicPlayerOrObj ["MyPlayer"].transform.FindChild ("Clothes").renderer.materials [2].mainTexture = MySocks.mainTexture;

        dicMenuList ["MY_Label_rank"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.thisWeekRank.ToString ();
        dicMenuList ["MY_Label_successivewin"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNum + Ag.mySelf.myRank.WAS.lossNum + WWW.UnEscapeURL ("%EC%A0%84") + Ag.mySelf.myRank.WAS.winNum + WWW.UnEscapeURL ("%EC%8A%B9") + Ag.mySelf.myRank.WAS.lossNum + WWW.UnEscapeURL ("%ED%8C%A8");
        dicMenuList ["MY_Label_tatalscore"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.weekScore.ToString ();
//        dicMenuList ["MY_Label_userleague"].GetComponent<UILabel> ().text = "-";
        Ag.mySelf.ApplyCurrentDeck ();
        if (Ag.mySelf.DeckItemEA > 0) {
            dicMenuList ["mydec"].SetActive (true);
            dicMenuList ["mydec"].transform.FindChild ("flag").GetComponent<UISprite> ().spriteName = "flag_" + Ag.mySelf.mMaxCountry;
            dicMenuList ["mydec"].transform.FindChild ("deckslot").gameObject.SetActive (true);
            dicMenuList ["mydec"].transform.FindChild ("deckslot/4set").gameObject.SetActive (false);
            dicMenuList ["mydec"].transform.FindChild ("deckslot/5set").gameObject.SetActive (false);
            dicMenuList ["mydec"].transform.FindChild ("deckslot/6set").gameObject.SetActive (false);
            Debug.Log ("Ag.mySelf.DeckItemEA" + "deckslot/" + (Ag.mySelf.DeckItemEA+3) + "set");

            dicMenuList ["mydec"].transform.FindChild ("deckslot/" + (Ag.mySelf.DeckItemEA+3) + "set").gameObject.SetActive (true);
            dicMenuList ["mydec"].transform.FindChild ("deckslot/" + (Ag.mySelf.DeckItemEA + 3) + "set/Label_" + (Ag.mySelf.DeckItemEA + 3) + "set").GetComponent<UILabel> ().text = Ag.mySelf.mMaxCountry;
        } else {
            dicMenuList ["mydec"].SetActive (false);
        }


        //------------------------------------------------GuestMode KakaoNick
        if (Ag.mGuest) {
            dicMenuList ["MY_Label_usernickname"].GetComponent<UILabel> ().text = "No name";
        } else {
            dicMenuList ["MY_Label_usernickname"].GetComponent<UILabel> ().text = Ag.mySelf.WAS.KkoNick;
        }

        
        dicMenuList ["MY_Label_userteamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);

        if (card.arrCostumeInCard.Count > 0) {
            for (int i = 0; i < card.arrCostumeInCard.Count; i++) {
                Debug.Log ("CostumeNAme" + card.arrCostumeInCard [i].WAS.itemTypeId);
                PlayerCostume.instance.SetCostumeToPlayer (true, dicPlayerOrObj ["MyPlayer"], (card.arrCostumeInCard [i].WAS.itemTypeId));
            }
        } else {
            PlayerCostume.instance.SetCostumeToPlayer (true, dicPlayerOrObj ["MyPlayer"], "KickerShoes01");
        }
        MyDataLoad = true;
    }

    void Division (GameObject Gobj, string League)
    {
        for (int i = 1; i < 6; i++) {
            Gobj.transform.FindChild ("div" + i).gameObject.SetActive (false);
        }
        switch (League) {
        case "PRO_5":
            Gobj.transform.FindChild ("div5").gameObject.SetActive (true);
            break;
        case "PRO_4":
            Gobj.transform.FindChild ("div4").gameObject.SetActive (true);
            break;
        case "PRO_3":
            Gobj.transform.FindChild ("div3").gameObject.SetActive (true);
            break;
        case "PRO_2":
            Gobj.transform.FindChild ("div2").gameObject.SetActive (true);
            break;
        case "PRO_1":
            Gobj.transform.FindChild ("div1").gameObject.SetActive (true);
            break;
        }

    }

    void Division2 (GameObject Gobj, string League)
    {
        for (int i = 1; i < 6; i++) {
            Gobj.transform.FindChild ("div" + i).gameObject.SetActive (false);
        }
        switch (League) {
        case "PRO_5":
            Gobj.transform.FindChild ("div5").gameObject.SetActive (true);
            break;
        case "PRO_4":
            Gobj.transform.FindChild ("div4").gameObject.SetActive (true);
            break;
        case "PRO_3":
            Gobj.transform.FindChild ("div3").gameObject.SetActive (true);
            break;
        case "PRO_2":
            Gobj.transform.FindChild ("div2").gameObject.SetActive (true);
            break;
        case "PRO_1":
            Gobj.transform.FindChild ("div1").gameObject.SetActive (true);
            break;
        }
        
    }

    IEnumerator EnemPic (string url)
    {
        if (string.IsNullOrEmpty (url)) {
            dicMenuList ["data_someone_face"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
        } else {
            WWW www = new WWW (url);
            yield return www;
            dicMenuList ["data_someone_face"].GetComponent<UITexture> ().material.mainTexture = www.texture;
        } 
    }

    public void GameSceneVisit ()
    {
        mRscrcMan.FindGameObject ("Ball/Kick/SoccerBall", true).GetComponent<SkinnedMeshRenderer> ().enabled = false;
        dicMenuList ["Ui_kickoff"].SetActive (false);
        dicMenuList ["Ui_setup"].SetActive (true);
        //Ag.mSingleMode = true;
        mPrepare.SetActive (true);
    }

    string TeamItemName;

    string GetTeamItemTypeName ()
    {
        mSumGold = 0;
        mSumGold = Ag.mySelf.mGold;
        if (Ag.mBlueItemFlag && Ag.mRedItemFlag && Ag.mGreenItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink25";
            return TeamItemName;
        }
        if (Ag.mBlueItemFlag && Ag.mRedItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink20";
            return TeamItemName;
        }
        if (Ag.mBlueItemFlag && Ag.mGreenItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink15";
            return TeamItemName;
        }
        if (Ag.mRedItemFlag && Ag.mGreenItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink15";
            return TeamItemName;
        }
        if (Ag.mBlueItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink10";
            return TeamItemName;
        }
        if (Ag.mRedItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink10";
            return TeamItemName;
        }
        if (Ag.mGreenItemFlag) {
            TeamItemName = ItemTypeId = "TeamDrink05";
            return TeamItemName;
        }
        return TeamItemName;
    }
    //    void BotSetting ()
    //    {
    //        Ag.myEnem.BotUniformCardRankItemSetting ();
    //        // Uniform Setting.
    //        Ag.myEnem.arrUniform = new List<AmUniform> ();
    //        Ag.myEnem.arrUniform.Add (new AmUniform ());
    //
    //        Ag.mVirServer.BotIDSetting ();
    //
    //        Ag.mVirServer.SetUniform (Ag.mVirServer.maiGradeOfBot, Ag.myEnem.arrUniform [0]); // 봇은 유니폼 1개만 필요.
    //
    //        // Card Setting
    //        Ag.myEnem.arrCard = new List<AmCard> ();
    //        //  _____  Set Keeper
    //        AmCard aCard = new AmCard ();
    //        aCard.WAS.SetVarInBot (false, Ag.mySelf.GetBotGrade(), AgUtil.RandomInclude (0, 4));
    //        aCard.WAS.isKicker = false;
    //        aCard.WAS.kickOrder = 0;
    //        aCard.WAS.InitDirectionAndSkill ();
    //        aCard.WAS.ShowMySelf ();
    //        Ag.myEnem.arrCard.Add (aCard);
    //        //  _____  Set Kicker
    //        for (int k = 0; k < 5; k++) {
    //            aCard = new AmCard ();
    //            //aCard.WAS.SetVarInBot (true, "C", AgUtil.RandomInclude (0, 4));
    //            aCard.WAS.SetVarInBot (true, Ag.mySelf.GetBotGrade(), AgUtil.RandomInclude (0, 4));
    //            aCard.WAS.InitDirectionAndSkill ();
    //            aCard.WAS.isKicker = true;
    //            aCard.WAS.kickOrder = k + 1;
    //            aCard.WAS.ShowMySelf ();
    //            Ag.myEnem.arrCard.Add (aCard);
    //        }
    //
    //        Ag.myEnem.myRank.SetAsBot (GetMyLeagueNum (Ag.mySelf.WAS.League), Ag.mySelf.myRank);
    //        Ag.NodeObj.EnemyUser = Ag.myEnem;
    //        Ag.LogString ("WeekWinNum" + Ag.NodeObj.EnemyUser.myRank.WAS.winNumWeek);
    //
    //        // Item Setting
    //        Ag.myEnem.arrItem = new List<AmItem> ();
    //        // Message
    //        AmItem aItm = new AmItem ();
    //        aItm.WAS.itemTypeID = "StartMessage";
    //        aItm.SetVarInBot ();
    //        Ag.myEnem.arrItem.Add (aItm);
    //        aItm = new AmItem ();
    //        aItm.WAS.itemTypeID = "EndMessage";
    //        aItm.SetVarInBot ();
    //        Ag.myEnem.arrItem.Add (aItm);
    //        aItm = new AmItem ();
    //        aItm.WAS.itemTypeID = "CeremonyDefault";
    //        aItm.SetVarInBot ();
    //        Ag.myEnem.arrItem.Add (aItm);
    //    }
    bool EnemyLeftflag;

    public void PanelCountLoading (int pNum)
    {
        for (int i = 1; i < 6; i++) {
            dicMenuList ["Panel_count"].transform.FindChild ("Label" + i).gameObject.SetActive (false);
        }
        dicMenuList ["Panel_count"].transform.FindChild ("Label" + pNum).gameObject.SetActive (true);
    }

    public IEnumerator StartCountDownKickOff ()
    {
        dicMenuList ["Panel_count"].SetActive (true);
        PanelCountLoading (5);
        yield return new WaitForSeconds (1f);
        PanelCountLoading (4);
        yield return new WaitForSeconds (1f);
        PanelCountLoading (3);
        yield return new WaitForSeconds (1f);
        PanelCountLoading (2);
        yield return new WaitForSeconds (1f);
        PanelCountLoading (1);
        yield return new WaitForSeconds (1f);
        dicMenuList ["Panel_count"].transform.FindChild ("Label1").gameObject.SetActive (false);
        Ag.NodeObj.StartGameMsg ();

    }

    void MessageInfo ()
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox/provokebox_me/Label_message", true).GetComponent<UILabel> ().text = MessageItem (Ag.mySelf, "StartMessage") == null ? WWW.UnEscapeURL ("%EC%95%88%EB%85%95%ED%95%98%EC%84%B8%EC%9A%94") : WWW.UnEscapeURL (MessageItem (Ag.mySelf, "StartMessage"));
        if (!Ag.mSingleMode) {
            mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox/provokebox_you/Label_message", true).GetComponent<UILabel> ().text = MessageItem (Ag.NodeObj.EnemyUser, "StartMessage") == "null" || MessageItem (Ag.NodeObj.EnemyUser, "StartMessage").Length < 1 ? WWW.UnEscapeURL ("%EC%95%88%EB%85%95%ED%95%98%EC%84%B8%EC%9A%94") : WWW.UnEscapeURL (MessageItem (Ag.NodeObj.EnemyUser, "StartMessage").RemoveQuotationMark ());
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_setup"], "Panel_provokebox/provokebox_you/Label_message", true).GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%95%88%EB%85%95%ED%95%98%EC%84%B8%EC%9A%94");
        }
        //Debug.Log ("DEBUGLOG  " + MessageItem (Ag.NodeObj.EnemyUser, "StartMessage"));

    }

    string MessageItem (AmUser muser, string id)
    {
        string MessageItem = "";

        for (int i = 0; i < muser.arrItem.Count; i++) {
            if (muser.arrItem [i].WAS.itemTypeID == id) {
                MessageItem = muser.arrItem [i].WAS.msg;
            }
        }
        return MessageItem;
    }

    void UseHeart ()
    {
        if (Ag.SingleTry > 0)
            return;
        //KakaoNativeExtension.Instance.useHeart (1, this.onUseHeartComplete, this.onUseHeartError);
        Ag.mySelf.HeartCoolTimeNewGameStarted ();
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Label_healthminus", false);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Label_healthminus", true);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Label_healthminus", true).GetComponent<UILabel> ().text = "-" + AgStt.CTHeartGameHealth.ToString ();
    }
}
            