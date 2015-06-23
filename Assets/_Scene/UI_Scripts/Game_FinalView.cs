//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    int MyFinalScore, EnFinalScore, effMyTotalScore, effEnemTotalScore;
    int EnemyScoreAfterGame, MyScoreAfterGame;
    float myWeekScr, enWeekScr, TotalLoserPoint, TotalWinerPoint, FinalWinPoDeck, FinalLosPoDeck;

    public void GameScoreEff ()
    {
        if (Ag.NodeObj.EnemyUser == null)
            return;
        //Ag.LogString ("MyScoreAfterGame   " + MyScoreAfterGame + " EnemyScoreAfterGame   " + EnemyScoreAfterGame); 

        //if (effMyTotalScore < MyScoreAfterGame) {
        if (effMyTotalScore != MyScoreAfterGame) {
            effMyTotalScore += SetScoreAnimationAddValue (MyScoreAfterGame, effMyTotalScore);
        }
        if (effEnemTotalScore != EnemyScoreAfterGame) {
            effEnemTotalScore += SetScoreAnimationAddValue (EnemyScoreAfterGame, effEnemTotalScore);
        }
        dicGameSceneMenuList ["MytotalScore"].GetComponent<UILabel> ().text = effMyTotalScore.ToString ();
        dicGameSceneMenuList ["EnemtotalScore"].GetComponent<UILabel> ().text = effEnemTotalScore.ToString ();
    }

    void SetFinalScores ()
    {
        AmGameLogic myGame = Ag.NodeObj.myGameLogic, enGame = Ag.NodeObj.enGameLogic;
        AmUser myslf = Ag.mySelf, enemy = Ag.NodeObj.EnemyUser;

        // Ag.NodeObj.myGameLogic.CurAccumTotal;
        MyFinalScore = (int)myGame.GetTotalScore (Ag.mySelf.GetApplyIDofItem ("CeremonyDefault")); // 내 점수
        EnFinalScore = (int)enGame.GetTotalScore (Ag.NodeObj.EnemyUser.GetApplyIDofItem ("CeremonyDefault")); // 상대 점수

        // Week Score
        Ag.LogString ("MyCeremonyDefault   " + Ag.mySelf.GetApplyIDofItem ("CeremonyDefault") + " EnCeremonyDefault   " + Ag.myEnem.GetApplyIDofItem ("CeremonyDefault")); 


        Ag.LogString ("MyFinalScore   " + MyFinalScore + " EnFinalScore   " + EnFinalScore); 

        Ag.LogString ("myWeekScr   " + myWeekScr + " enWeekScr   " + enWeekScr); 
        // 차감되는 패자 포인트
        TotalLoserPoint = Ag.mgDidWin ?
            myGame.GetLoserTotalScore (Ag.NodeObj.EnemyLeague, MyFinalScore, (int)enWeekScr) :
            enGame.GetLoserTotalScore (Ag.mySelf.WAS.League, EnFinalScore, (int)myWeekScr);
        TotalWinerPoint = Ag.mgDidWin ? MyFinalScore : EnFinalScore;

        // Deck Apply   5000, 2500 ==> 5500, 2200
        FinalWinPoDeck = Ag.mgDidWin ? myGame.ApplyDeckIncrease (TotalWinerPoint, myslf.WAS.DeckItem, myslf.DeckItemEA) :
            enGame.ApplyDeckIncrease (TotalWinerPoint, enemy.WAS.DeckItem, enemy.DeckItemEA);
        FinalLosPoDeck = Ag.mgDidWin ? enGame.ApplyDeckLosingScore (TotalLoserPoint, enemy.WAS.DeckItem, enemy.DeckItemEA) :
            myGame.ApplyDeckLosingScore (TotalLoserPoint, myslf.WAS.DeckItem, myslf.DeckItemEA);

        Ag.LogString ("TotalLoserPoint   " + TotalLoserPoint + " TotalWinerPoint   " + TotalWinerPoint); 
        // 차감되는 패자 포인트  (Week Score Applied)
        if (Ag.mgDidWin) {
            MyScoreAfterGame = (int)(myWeekScr + FinalWinPoDeck); // TotalWinerPoint); // winner
            EnemyScoreAfterGame = (int)(enWeekScr - (int)FinalLosPoDeck); // (int)TotalLoserPoint);
        } else {
            EnemyScoreAfterGame = (int)(enWeekScr + FinalWinPoDeck); // TotalWinerPoint); // winner
            MyScoreAfterGame = (int)(myWeekScr - (int)FinalLosPoDeck); // (int)TotalLoserPoint);
        }
        Ag.LogString ("FinalWinPoDeck   " + FinalWinPoDeck + " FinalLosPoDeck   " + FinalLosPoDeck); 
        // 4 score animation
        effMyTotalScore = (int)myWeekScr;
        effEnemTotalScore = (int)enWeekScr;
        Ag.LogString ("effMyTotalScore   " + effMyTotalScore + " effEnemTotalScore   " + effEnemTotalScore); 

        Ag.LogString ("MyScoreAfterGame   " + MyScoreAfterGame + " EnemyScoreAfterGame   " + EnemyScoreAfterGame); 
    }

    int SetScoreAnimationAddValue (int total, int aniVal) // total can be - ... 
    {
        bool Negative = aniVal > total;

        int diff = Mathf.Abs (total - aniVal), rVal = 0;
        if (diff > 10000)
            rVal += 2230;
        if (diff > 1000)
            rVal += 123;
        else if (diff > 100)
            rVal += 12;
        else if (diff > 10)
            rVal += 3;
        else
            rVal += 1;

        if (Negative)
            return -rVal;
        return rVal;
    }

    void GameTotalScore ()
    {
        if (Ag.NodeObj == null || Ag.NodeObj.AnyProblemInGame ()) {
            MyFinalScore = EnFinalScore = 0;
            //MyFinalScore = Ag.mgDidWin ? Ag.NodeObj.myGameLogic.CurScore : 0;
            //EnFinalScore = Ag.mgDidWin ? 0 : Ag.NodeObj.enGameLogic.CurScore;
        } else
            SetFinalScores ();

        dicGameSceneMenuList ["MYWin"].SetActive (Ag.mgDidWin);
        dicGameSceneMenuList ["MyLose"].SetActive (!Ag.mgDidWin);
        dicGameSceneMenuList ["EnemtotalScore_minus"].SetActive (true);
        dicGameSceneMenuList ["MytotalScore_plus"].SetActive (true);
        dicGameSceneMenuList ["MytotalScore"].GetComponent<UILabel> ().text = ((int)myWeekScr).ToString ();
        dicGameSceneMenuList ["EnemtotalScore"].GetComponent<UILabel> ().text = ((int)enWeekScr).ToString ();
        EnemyTotalScoreEff ();
        if (Ag.NodeObj.GameFinish.Value) {
            dicGameSceneMenuList ["EnemtotalScore_minus"].GetComponent<UILabel> ().text = "-"+((int)TotalLoserPoint).ToString ();
            dicGameSceneMenuList ["MytotalScore_plus"].GetComponent<UILabel> ().text = ((int)MyFinalScore).ToString ();
        } else {
            dicGameSceneMenuList ["EnemtotalScore_plus"].GetComponent<UILabel> ().text = ((int)EnFinalScore).ToString ();
            dicGameSceneMenuList ["MytotalScore_minus"].GetComponent<UILabel> ().text = "-"+((int)TotalLoserPoint).ToString ();
        }

        StartCoroutine (GameScoreEffFlag ());
    }

    void EnemyTotalScoreEff () {
        if (Ag.NodeObj.GameFinish.Value) {
            dicGameSceneMenuList ["EnemtotalScore_minus"].SetActive (true);
            dicGameSceneMenuList ["MytotalScore_plus"].SetActive (true);
            dicGameSceneMenuList ["EnemtotalScore_plus"].SetActive (false);
            dicGameSceneMenuList ["MytotalScore_minus"].SetActive (false);


        } else {
            dicGameSceneMenuList ["EnemtotalScore_minus"].SetActive (false);
            dicGameSceneMenuList ["MytotalScore_plus"].SetActive (false);
            dicGameSceneMenuList ["EnemtotalScore_plus"].SetActive (true);
            dicGameSceneMenuList ["MytotalScore_minus"].SetActive (true);
        }
    }

    /// <summary>
    /// 상대방 점수 스코어 계산
    /// </summary>
    void Wincheck ()
    {
        GameTotalScore ();
        Ag.NodeObj.MySocket.CurEnemy.rankObj.weekScore = EnemyScoreAfterGame;

        dicGameSceneMenuList ["MyEarnGold"].SetLabelText (Ag.mgDidWin ? "200" : "100"); // GetComponent<UILabel> ().text = "0";

        if (Ag.mGuest) {
            dicGameSceneMenuList ["MyNickname"].GetComponent<UILabel> ().text = "No name"; // "NONAME";
        } else {
            dicGameSceneMenuList ["MyNickname"].GetComponent<UILabel> ().text = Ag.mySelf.WAS.KkoNick;
        }
        dicGameSceneMenuList ["MyUsername"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);

        if (Ag.mSingleMode) {
            dicGameSceneMenuList ["EnemyUserNick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%8C%80%EC%A0%84%20%EC%8A%B9%EB%B6%80%EC%B0%A8%EA%B8%B0");
            dicGameSceneMenuList ["EnemyUsername"].GetComponent<UILabel> ().text = Ag.mVirServer.teamName;
        } else {
            if (Ag.NodeObj.IsRandom) {
                dicGameSceneMenuList ["EnemyUserNick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%8C%80%EC%A0%84%20%EC%8A%B9%EB%B6%80%EC%B0%A8%EA%B8%B0");
            } else {
                dicGameSceneMenuList ["EnemyUserNick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.EnemyUser.KkoNickEncode);
            }
            dicGameSceneMenuList ["EnemyUsername"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.MySocket.CurEnemy.teamName);
        }

        //dicGameSceneMenuList ["Label_roundbonus"].GetComponent<UILabel> ().text = "";
        dicGameSceneMenuList ["Label_round140"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? Ag.NodeObj.myGameLogic.UiTurnBonus : "0";

        //int MyFinalScore = (int)Ag.NodeObj.myGameLogic.GetTotalScore (Ag.mySelf.GetApplyIDofItem ("CeremonyDefault")); 
        //Ag.NodeObj.GameFinish.Value ? "+" + ((int)MyFinalScore).ToString () : Ag.NodeObj.myGameLogic.GetLoserTotalScore ((int)Ag.mySelf.myRank.WAS.weekScore, (int)mEnemyCurScore).ToString ();

        dicGameSceneMenuList ["Label1_gamescore"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? ((int)Ag.NodeObj.myGameLogic.PlayScoreTtl).ToString () : "0";
        dicGameSceneMenuList ["Label2_uniformbonus"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? ((int)Ag.NodeObj.myGameLogic.UniformScoreTtl).ToString () : "0"; 
        dicGameSceneMenuList ["Label1_costumebonus"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? ((int)Ag.NodeObj.myGameLogic.CostumeScoreTtl).ToString () : "0";

        dicGameSceneMenuList ["Label2_ceremonybonus"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? ((int)Ag.NodeObj.myGameLogic.CeremonyBonus).ToString () : "0";
        dicGameSceneMenuList ["Label_leaguebonus"].GetComponent<UILabel> ().text = Ag.NodeObj.GameFinish.Value ? ((int)Ag.NodeObj.myGameLogic.LeagueDiff).ToString () : "0";

        int finalEarnScore;
        if (Ag.mgDidWin)
            finalEarnScore = (int)FinalWinPoDeck;
        else
            finalEarnScore = (int)FinalLosPoDeck;

        if (!Ag.NodeObj.GameFinish.Value && Ag.mySelf.myRank.WAS.weekScore < 20000) {
            dicGameSceneMenuList ["Label_earnscore"].GetComponent<UILabel> ().text = "0";
        } else {
            if (!Ag.NodeObj.GameFinish.Value) {
                dicGameSceneMenuList ["Label_earnscore"].GetComponent<UILabel> ().text = "-" + finalEarnScore.ToString ();
                dicGameSceneMenuList ["GetLabel"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%B0%A8%EA%B0%90%EC%A0%90%EC%88%98");
            } else {
                dicGameSceneMenuList ["Label_earnscore"].GetComponent<UILabel> ().text = finalEarnScore.ToString ();
                dicGameSceneMenuList ["GetLabel"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%ED%9A%8D%EB%93%9D%EC%A0%90%EC%88%98");
            }
        }

        dicGameSceneMenuList ["MyFlag"].GetComponent<UITexture> ().mainTexture = (Texture2D)Resources.Load ("flag/" + Ag.mCountryData.SetNationSprite (Ag.mySelf.WAS.Country));

        if (Ag.mSingleMode) {
            if (!Ag.mGuest)
                StartCoroutine (startPic (StcPlatform.ProfileURL, dicGameSceneMenuList ["MyFace"]));
            dicGameSceneMenuList ["EnemyFace"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_random");
            dicGameSceneMenuList ["EnemyFlag"].GetComponent<UITexture> ().mainTexture = (Texture2D)Resources.Load ("flag/" + "flag_KOR");
        } else {
            dicGameSceneMenuList ["EnemyFlag"].GetComponent<UITexture> ().mainTexture = (Texture2D)Resources.Load ("flag/" + Ag.mCountryData.SetNationSprite (Ag.NodeObj.MySocket.CurEnemy.country));
            if (Ag.mGuest) {
            } else {
                StartCoroutine (startPic (StcPlatform.ProfileURL, dicGameSceneMenuList ["MyFace"]));
                if (Ag.NodeObj.IsRandom) 
                    dicGameSceneMenuList ["EnemyFace"].GetComponent<UITexture>().material.mainTexture = (Texture2D)Resources.Load ("userface_random");
                else
                    StartCoroutine (startPic (Ag.NodeObj.MySocket.CurEnemy.profileURL, dicGameSceneMenuList ["EnemyFace"]));
            }
        }

        mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_gameresult/Label_mygoal", true).gameObject.GetComponent<UILabel> ().text = FunResultNum (arrAllMyScore).ToString ();
        mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_ingame/Panel_gameresult/Label_someonegoal", true).gameObject.GetComponent<UILabel> ().text = FunResultNum (arrAllEnScore).ToString ();

        Ag.LogString ("Ag.mySelf.myRank.WAS.contWinNum +     " + Ag.mySelf.myRank.WAS.contWinNum + "Ag.mgDidWin" + Ag.mFriendMode);
        if (Ag.mySelf.myRank.WAS.contWinNum >= 1 && Ag.mgDidWin && Ag.mFriendMode == 0) {
            dicGameSceneMenuList ["Main_victory_gift"].SetActive (true);
            dicGameSceneMenuList ["result_victories" + Ag.mySelf.myRank.WAS.contWinNum].SetActive (true);
            ContWinNumReward ();
        }

    }
}