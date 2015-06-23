//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    #region Condition Check Logic   ___ return bool  >>

    //bool IsReadyToStartGame { get { return Percent > 0 || Ag.mySelf.FreeCouponLimitDT > System.DateTime.Now; } }
    bool IsReadyToStartGame { get { return Percent > 0 || Ag.mySelf.IsFreeCouponRemain; } }

    bool MatchingConditionNotMade ()
    {
        int SumPlayerCost;
        if (!Ag.mySelf.CardTotalCostCheck (out SumPlayerCost)) {  // 코스트 체크
            MenuCommonOpen ("popup_levelpointalert", "KickOffpopup", true);
            dicMenuList ["popup_levelpointalert"].transform.FindChild ("Label_maxlevelpoint").GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();
            dicMenuList ["popup_levelpointalert"].transform.FindChild ("Label_mylevelpoint").GetComponent<UILabel> ().text = SumPlayerCost.ToString ();
            return true;
        }
        //if (PlayerExtendEa > 0) {
        if (!Ag.mySelf.CardContractLimitCheck ()) { // 계약 한도 체크
            MenuCommonOpen ("popup_playeralert", "KickOffpopup", true);
            return true;
        }
        if (mSumGold < 0) {
            MenuCommonOpen ("KickOffpopup", "pointover", true);
            return true;
        }

        //if (KakaoGameUserInfo.Instance.heart < 1 && Ag.mySelf.FreeCouponLimitDT < System.DateTime.Now) {
        if (!IsReadyToStartGame) {
            MenuCommonOpen ("Ui_popup", "havenotplayball", true);
            return true;
        }
        return false;
    }

    #endregion

    #region Coroutine ... ____

    //  _////////////////////////////////////////////////_    _____  Matching  _____   Coroutine   _____
    IEnumerator RandomAppliedAndWaitForEnemy ()  //BotMatching ()
    {
        Ag.LogIntenseWord (Ag.CoroutineSign + "    .... RandomAppliedAndWaitForEnemy  :: ");
        // 15 초 기다렸다가 매칭이 되었으면 그냥 패스.. 아니면 봇과 매칭.
        yield return new WaitForSeconds (15f);   //MatchingFlag = true;
        Ag.LogIntenseWord (Ag.CoroutineSign + "   After 15 Sec ....  Menu... KickOff Ready    >>   Flag :: ");
        if (Ag.GameStt.ExchangeParsedForGominjung)
            Ag.LogDouble ("   Game already Started... Bot NO.. ::   ");
        else {
            Ag.mVirServer.maiGradeOfBot = 0;
            Ag.NodeObj.CancelRandomStartGameWithBot ();
        }
        // 2 초 후에 봇과 대전 / 패스
        yield return new WaitForSeconds (2f);
        if (Ag.GameStt.ExchangeParsedForGominjung)
            Ag.LogDouble (Ag.CoroutineSign + "  Just Matching ... Not with Bot  ...  ");
        else { // Un usual case ...  !!!!!   Exchange is too late !!! 
            Ag.LogDouble ("   RandomAppliedAndWaitForEnemy    ::  ExchangeParsedForGominjung is false  >>>  Exception  step ..  ");
            //if (!Ag.mSingleMode)
            //  Btn_Fun_MatchCancleAndGoOut ();
        }
    }

    IEnumerator DelayActivateKickOffButton ()
    {
        // disable . setactive (true);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready_inert", true);
        yield return new WaitForSeconds (1f);
        mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready_inert", false);
        // disable . setactive (false);
        // enable . setActive (true);
    }

    IEnumerator StartWithBot ()  //BotMatching ()
    {
        Ag.mSingleMode = true;
        yield return new WaitForSeconds (5f);
        if (Ag.mVirServer.maiGradeOfBot == -1)
            Ag.mVirServer.maiGradeOfBot = 0;
        Ag.NodeObj.StartGameWithBotProcess ();
    }

    IEnumerator CoruMatchRequire ()
    {

        yield return new WaitForSeconds (0.5f);
        FriendInit ();
        yield return new WaitForSeconds (1f);
        if (!FriendListUpdate)
            StartCoroutine (FriendUpdate ());
    }

    IEnumerator WaitAndGoMatchAndExit () // 나가기 버튼 3초간 비활성.
    {
        dicMenuList ["btn_exit"].SetActive (false);
        yield return new WaitForSeconds (3f);
        dicMenuList ["btn_exit"].SetActive (true);
    }

    IEnumerator xxUniformInput ()
    {
        yield return new WaitForSeconds (0.1f);
        EnemyUniformSetting ();
        Ag.LogIntenseWord ("    IEnumerator UniformInput   ::   Game Tutorial ....  " + AgStt.mgGameTutorial);
        if (!AgStt.mgGameTutorial)
            SetEnemyLabelText ();
        //StopAllCoroutines ();
        StartCoroutine (StartCountDownKickOff ());
    }

    #endregion

    public void EnemyOrBotUniformLabelSetting ()
    {
        Ag.LogIntenseWord ("    EnemyJoin  :::    call UniformInput   ");
        //StartCoroutine (UniformInput ());   .....  
        EnemyUniformSetting ();
        //Ag.LogIntenseWord ("    IEnumerator UniformInput   ::   Game Tutorial ....  " + AgStt.mgGameTutorial);
        if (!AgStt.mgGameTutorial)
            SetEnemyLabelText ();
        StartCoroutine (StartCountDownKickOff ());

        if (AgStt.mgGameTutorial)
            StartCoroutine (EnemPic (""));
        else {
            if (Ag.mSingleMode) {
                Ag.LogString ("  Enem Pic is NULL !!!   EnemyFace ");
                dicMenuList ["data_someone_face"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_random");

            } else {
                Ag.LogString ("  Enem Pic is NULL !!!   EnemyFace Not Bot Mode    >>>  Random  : " + Ag.NodeObj.IsRandom);
                if (Ag.NodeObj.IsRandom) {
                    dicMenuList ["data_someone_face"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_random");
                } else {
                    StartCoroutine (EnemPic (Ag.NodeObj.MySocket.CurEnemy.profileURL));
                }
            }
        }
        dicMenuList ["data_someone"].SetActive (true);
    }

    void SetEnemyLabelText ()
    {
        if (!Ag.mSingleMode) {

            Ag.LogIntenseWord ("  LoadEnemyData ::  Enemy Data >>>>>    Matching  Mode >>>  Set   ___   Not Bot   ___ Rank info    ....   ");

            dicMenuList ["ENEMY_flag"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("flag/" + Ag.mCountryData.SetNationSprite (Ag.NodeObj.MySocket.CurEnemy.country));
            try {
                dicMenuList ["ENEMY_Label_userteamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.MySocket.CurEnemy.teamName);
                dicMenuList ["ENEMY_Label_rank"].GetComponent<UILabel> ().text = Ag.NodeObj.MySocket.CurEnemy.rankObj.thisWeekRank.ToString ();
                dicMenuList ["ENEMY_Label_successivewin"].GetComponent<UILabel> ().text =  Ag.NodeObj.MySocket.CurEnemy.rankObj.winNum + Ag.NodeObj.MySocket.CurEnemy.rankObj.lossNum + WWW.UnEscapeURL ("%EC%A0%84") + Ag.NodeObj.MySocket.CurEnemy.rankObj.winNum + WWW.UnEscapeURL (" %EC%8A%B9") + Ag.NodeObj.MySocket.CurEnemy.rankObj.lossNum + WWW.UnEscapeURL ("%ED%8C%A8");
                dicMenuList ["ENEMY_Label_tatalscore"].GetComponent<UILabel> ().text = Ag.NodeObj.MySocket.CurEnemy.rankObj.weekScore.ToString ();
                //        dicMenuList ["ENEMY_Label_userleague"].GetComponent<UILabel> ().text = "-";

                if (Ag.NodeObj.IsRandom) {
                    dicMenuList ["ENEMY_Label_usernickname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%8C%80%EC%A0%84%20%EC%8A%B9%EB%B6%80%EC%B0%A8%EA%B8%B0");
                } else {
                    dicMenuList ["ENEMY_Label_usernickname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.EnemyUser.KkoNickEncode.RemoveQuotationMark ());
                }
                Division (dicMenuList ["ENEMY_division"], Ag.NodeObj.MySocket.CurEnemy.league);

                //Ag.mViewCard.CardLeagueSpritename (Ag.NodeObj.EnemyUser.WAS.League);
                //item.Target.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                //dicMenuList ["ENEMY_division"]

            } catch {
                Debug.Log ("Error EnemyData");
            }
        } else {

            Ag.LogIntenseWord ("  Load Enemy Data >>>>>    Single Mode >>>  Set   ___   B O T  ___ Rank info    ....   ");

            dicMenuList ["ENEMY_flag"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("flag/" + "flag_KOR");
            dicMenuList ["ENEMY_Label_userteamname"].GetComponent<UILabel> ().text = Ag.mVirServer.teamName;
            dicMenuList ["ENEMY_Label_usernickname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%8C%80%EC%A0%84%20%EC%8A%B9%EB%B6%80%EC%B0%A8%EA%B8%B0");
            Division (dicMenuList ["ENEMY_division"], Ag.mySelf.WAS.League);
            //Debug.Log (Ag.NodeObj.MyUser.KkoNickEncode + " MyUserData :: KKONick");
            //Debug.Log (Ag.NodeObj.EnemyUser.KkoNickEncode + " EnemyData :: KKONick");
            dicMenuList ["ENEMY_Label_rank"].GetComponent<UILabel> ().text = Ag.NodeObj.EnemyUser.myRank.WAS.thisWeekRank.ToString ();
            dicMenuList ["ENEMY_Label_successivewin"].GetComponent<UILabel> ().text = (Ag.NodeObj.EnemyUser.myRank.WAS.winNum + Ag.NodeObj.EnemyUser.myRank.WAS.lossNum) + WWW.UnEscapeURL ("%EC%A0%84") + Ag.NodeObj.EnemyUser.myRank.WAS.winNum + WWW.UnEscapeURL (" %EC%8A%B9") + Ag.NodeObj.EnemyUser.myRank.WAS.lossNum + WWW.UnEscapeURL (" %ED%8C%A8");
            dicMenuList ["ENEMY_Label_tatalscore"].GetComponent<UILabel> ().text = Ag.NodeObj.EnemyUser.myRank.WAS.weekScore.ToString ();
        }

        Ag.NodeObj.EnemyUser.ApplyCurrentDeck ();

        Debug.Log ("Ag.NodeObj.EnemyUser.DeckItemEA" + Ag.NodeObj.EnemyUser.DeckItemEA);
        if (Ag.NodeObj.EnemyUser.DeckItemEA > 0) {
            dicMenuList ["someonedec"].transform.FindChild ("flag").GetComponent<UISprite> ().spriteName = "flag_" + Ag.NodeObj.EnemyUser.mMaxCountry;
            dicMenuList ["someonedec"].transform.FindChild ("deckslot").gameObject.SetActive (true);
            dicMenuList ["someonedec"].SetActive (true);
            dicMenuList ["someonedec"].transform.FindChild ("deckslot/4set").gameObject.SetActive (false);
            dicMenuList ["someonedec"].transform.FindChild ("deckslot/5set").gameObject.SetActive (false);
            dicMenuList ["someonedec"].transform.FindChild ("deckslot/6set").gameObject.SetActive (false);
            dicMenuList ["someonedec"].transform.FindChild ("deckslot/" + (Ag.NodeObj.EnemyUser.DeckItemEA+3) + "set").gameObject.SetActive (true);
            dicMenuList ["someonedec"].transform.FindChild ("deckslot/" + (Ag.NodeObj.EnemyUser.DeckItemEA+3) + "set/Label_"+(Ag.NodeObj.EnemyUser.DeckItemEA+3)+"set").GetComponent<UILabel> ().text = Ag.NodeObj.EnemyUser.mMaxCountry;
        } else {
            dicMenuList ["someonedec"].SetActive (false);
        }
    }

    void MatchCancelUI ()
    {
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (false);
        dicMenuList ["Ui_setup"].SetActive (false);
        dicMenuList ["Ui_kickoff"].SetActive (true);
        dicMenuList ["Panel_teamback"].SetActive (true);
        dicMenuList ["LPanel_lineup"].SetActive (true);
        dicMenuList ["MainCamera"].SetActive (false);
        dicMenuList ["PrePareMatch"].SetActive (false);
        dicMenuList ["Panel_matching"].SetActive (false);
        //dicMenuList ["loading"].SetActive (false);
        dicMenuList ["btn_exit"].SetActive (false);
        dicMenuList ["data_someone"].SetActive (false);
        //dicMenuList ["provokebox_you"].SetActive (false);
        dicMenuList ["Panel_provokebox"].SetActive (false);

        dicMenuList ["Panel_firstaction"].SetActive (false);
        dicMenuList ["Panel_firstaction1"].SetActive (false);

        dicMenuList ["Ball"].SetActive (true);

        mKicker.SetActive (true);
        mKicker.animation.Play ();
        SoundManager.Instance.Play_Effect_SoundStop ();
        dicMenuList ["ready_me"].SetActive (false);
        dicMenuList ["ready_someone"].SetActive (false);
        dicMenuList ["Panel_count"].SetActive (false);

        dicMenuList ["Panel_top_cash"].gameObject.SetActive (true);
        dicMenuList ["Panel_top_coin"].gameObject.SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/btn_shop", true);
        mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/btn_shop_non", false);


        Btn_Fun_DrinkItem ();
    }

    void RefuseOKUI ()
    {
        Ag.LogDouble (" Menu_MatchUI :: RefuseOKUI  ");
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["versus_refuse"].SetActive (false);
        dicMenuList ["PrePareMatch"].SetActive (false);
        dicMenuList ["Ui_setup"].SetActive (false);
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (false);
        dicMenuList ["Ui_kickoff"].SetActive (true);
        dicMenuList ["Panel_teamback"].SetActive (true);
        dicMenuList ["LPanel_lineup"].SetActive (true);
        dicMenuList ["MainCamera"].SetActive (false);
        dicMenuList ["Ball"].SetActive (true);
    }

    void MatchRequireClose ()
    {
        Ag.LogDouble (" Menu_MatchUI :: MatchRequireClose  ");
        dicMenuList ["MainCamera"].SetActive (true);
        dicMenuList ["Ui_kickoff"].SetActive (false);
        dicMenuList ["Ui_lobby"].SetActive (true);
        dicMenuList ["Ui_team"].SetActive (false);
        dicMenuList ["PrePareMatch"].SetActive (false);
        dicPlayerOrObj ["Coin"].animation.Stop ();
        dicPlayerOrObj ["Refree"].animation.Stop ("referee02");
    }
}