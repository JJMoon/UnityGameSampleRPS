using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

// Ag.MyGame Ag.EmGame
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Game Logic  _____  Class  _____
public class AmGameLogic : AmObject
{
    public List<NodeGameTurnRslt > arrGameTurn = new List<NodeGameTurnRslt> ();
    public List<AmGameScore> arrScore = new List<AmGameScore> ();
    public string UiTurnBonus;
    public float SemiTotal, CurAccumTotal, CeremonyBonus;
    public int LeagueDiff;
    // 리그차 보너스 제외 점수.
    public float CurScore {
        get {
            AmGameScore lastScore = arrScore.GetLastMember ();
            if (lastScore == null)
                return 0;
            return lastScore.GetScore ();
        }
    }

    public float PlayScoreTtl {
        get { 
            float rVal = 0;
            foreach (AmGameScore scre in arrScore) {
                rVal += scre.PlayScore;
            }
            return rVal;
        }
    }

    public float UniformScoreTtl {
        get { 
            float rVal = 0;
            foreach (AmGameScore scre in arrScore) {
                rVal += scre.bonusUnif;
            }
            return rVal;
        }
    }

    public float CostumeScoreTtl {
        get { 
            float rVal = 0;
            foreach (AmGameScore scre in arrScore) {
                rVal += scre.bonusCstm;
            }
            return rVal;
        }
    }

    public int TotalRound {
        get { return arrScore.Count % 2 == 0 ? arrScore.Count / 2 : arrScore.Count / 2 + 1; }
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  AmGameLogic  _____  Class  _____
    public AmGameLogic (string MyLeague, string EnLeague)
    {
        if (MyLeague == null || EnLeague == null || MyLeague.Length < 3 || EnLeague.Length < 3) {
            LeagueDiff = 0;
            return;
        }
        int myLg = int.Parse (MyLeague.Substring (MyLeague.Length - 1, 1));
        int enLg = int.Parse (EnLeague.Substring (EnLeague.Length - 1, 1));
        LeagueDiff = (enLg < myLg) ? myLg - enLg : 0;
    }

    public void CurBaseAddScores (out float baseScr, out float addedScr)
    {
        baseScr = addedScr = 0;
        AmGameScore lastScore = arrScore.GetLastMember ();
        if (lastScore == null)
            return;
        lastScore.GetScore ();
        baseScr = lastScore.BaseScore;
        addedScr = 0; //lastScore.AddedScore;
    }

    public float GetLoserTotalScore (string league, int semiTotalOfWinner, int loserPoint) // ㄴㅏㅇㅡㅣ ㅈㅓㅁㅅㅜ . my score.
    {
        if (string.IsNullOrEmpty (league))
            return 0;
        float ratio = 0;
        //int leagNum = league.GetContinuousInteger ();  // wrong .. 

        int leagNum = loserPoint / 20000;
        if (leagNum > 4)
            leagNum = 4;
        //leagNum = 5 - leagNum; // 4, ... 1, 0
        ratio = leagNum * 0.25f;
        return ratio * semiTotalOfWinner;
//
//        if (20000 <= CurrentScore && CurrentScore < 40000)
//            ratio = 0.25f;
//        if (40000 <= CurrentScore && CurrentScore < 60000)
//            ratio = 0.5f;
//        if (60000 <= CurrentScore && CurrentScore < 80000)
//            ratio = 0.75f;
//        if (80000 <= CurrentScore)
        //            ratio = 1.0f;           return ratio * semiTotalOfWinner;
    }

    public float ApplyDeckIncrease (float pScore, int[] pDeckItem, int effectiveNum)
    {
        int itemNum = pDeckItem.GetEAofMatch (1);
        return (1 + itemNum * 0.07f) * pScore;  // (14%) * pScore
    }

    public float ApplyDeckLosingScore (float pScore, int[] pDeckItem, int effectiveNum)
    {
        int itemNum = pDeckItem.GetEAofMatch (7);
        return (1 - itemNum * 0.1f) * pScore;
    }

    public float GetTotalScore (int pCeremony = 0)
    {
        float rVal = 0;
        Ag.LogStartWithStr (1, " GetTotalScore  ::  ");
        foreach (AmGameScore scre in arrScore) {
            //AmGameScore.GetScore 를 합산
            rVal += scre.GetScore ();
            if (scre.KindOfBall > 0) {
                string log = " GetTotalScore  :: Cur Score ::  " + rVal + " \t Ball Event  >>>   " + scre.KindOfBall + "  \t\t Win ? " + scre.didWin;
                if (scre.didWin)
                    rVal *= (1 + scre.KindOfBall * 0.1f);
                else
                    rVal *= (1 - scre.KindOfBall * 0.1f);
                Ag.LogString (log + "   \t\t Ball Event applied Score ::  " + rVal);
            }
        }
        CurAccumTotal = rVal; // 경기가 끝나지 않았을 경우엔 이 값 이용.



        //CurTurnTotal += CeremonyBonus;
        UiTurnBonus = "-";
        if (TotalRound == 3) { // 3턴 승 
            UiTurnBonus = "x300%";
            rVal *= 3;
        }
        if (TotalRound == 4) {
            UiTurnBonus = "x200%";
            rVal *= 2;
        }
        if (TotalRound == 5) {
            UiTurnBonus = "x140%";
            rVal *= 1.4f;
        }

        CeremonyBonus = pCeremony == 1 ? 100 : CeremonyBonus;
        CeremonyBonus = pCeremony == 2 ? 50 : CeremonyBonus;
        CeremonyBonus = pCeremony == 3 ? 250 : CeremonyBonus;
        CeremonyBonus = pCeremony == 4 ? 200 : CeremonyBonus;
        CeremonyBonus = pCeremony == 5 ? 150 : CeremonyBonus;

        Ag.LogString ("Get Total Score ::  Cur Accum Total >>>  " + CurAccumTotal + "   Ceremony  >>  " + CeremonyBonus + "   LeagueDiff  >>> " + LeagueDiff);

        rVal += CeremonyBonus;
        SemiTotal = rVal; // 패자 차감 점수 계산용
        return SemiTotal * (1 + LeagueDiff * 0.1f); // 리그 차이 극복 보너스. 
    }

    public int GetGoalNumber ()
    {
        int rV = 0;
        foreach (AmGameScore obj in arrScore) {
            if (obj.didWin && obj.isKick)
                rV++;
        }
        return rV;
    }

    public bool? DidIFinalWin (AmGameLogic enLogic)
    {  // null : not yet, true : My Win, false : My lose
        Ag.LogIntense (3, true);
        (" DidIFinalWin :   turn N ::    " + arrScore.Count).HtLog ();

        int turn = arrScore.Count;
        if (10 <= turn && turn % 2 == 1)
            return null; // 5 Round 이상.. 선공만 찼다. 계속..
        int myGoal = GetGoalNumber (), enGoal = enLogic.GetGoalNumber ();
        ("  Goal : <<  " + myGoal + " / " + enGoal + "  >>   . . . . . .     ").HtLog ();

        if (myGoal == enGoal)
            return null;

        if (10 <= turn) { // 5 Round 이상
            if (myGoal != enGoal)
                return (myGoal > enGoal); // 승/패 판정
            else
                return null; // 아직 안 끝났음. 
        }
        (" It's under 5 round >>> Rest Kick  <Me / En> : < " + RemainKickNum () + " / " + enLogic.RemainKickNum () + " >").HtLog ();

        int bigGoal, smlGoal, smlMax;

        if (myGoal > enGoal) {
            bigGoal = myGoal;
            smlGoal = enGoal;
            smlMax = smlGoal + enLogic.RemainKickNum ();
        } else {
            bigGoal = enGoal;
            smlGoal = myGoal;
            smlMax = smlGoal + RemainKickNum ();
        }
        // ("  My : " + myGoal + "   En : " + enGoal + "   Sml : " + smlGoal + "   SmlMax : " + smlMax).HtLog ();
        if (bigGoal > smlMax)
            return (myGoal > enGoal); // 승/패 판정
        return null;
    }
    //  _////////////////////////////////////////////////_    _____  Methods  _____   ^^  _____
    public void AddNewTurn (NodeGameTurnRslt pMyTurn, NodeGameTurnRslt pEnTurn, int kindOfBall, int[] pUnifCstmInfo)
    {
        Ag.LogStartWithStr (5, " AmGameLogic :: AddNewTurn ____ ____ ____ ____ ____ ____ ____ ");

        // Win or Lose 
        arrGameTurn.Add (pMyTurn);

        // Score Calculation
        // HOST / VSTR
        int kD = pEnTurn.direction, kS = pEnTurn.skill, dD = pMyTurn.direction, dS = pMyTurn.skill;
        bool myKick = pMyTurn.roll == "KICK" ? true : false;
        if (myKick) {
            kD = pMyTurn.direction;
            kS = pMyTurn.skill;
            dD = pEnTurn.direction;
            dS = pEnTurn.skill;
        }
        Ag.LogString (string.Format ("AddNewTurn  :: Kick {0} / {1}     Keep {2} / {3} ", kD, kS, dD, dS));
        bool kickWon = AgUtilGame.DidKickerWinThisTurn (kD, kS, dD, dS);
        Dlgt_Gen_Obj_Bool<AmGameScore> dlgtScr = ( AmGameScore pObj) => { 
            return pObj.isKick == myKick;
        };
        AmGameScore preScr = arrScore.GetLastMemberWithCond (dlgtScr);
        //if (preScr == null)            (" It's First Kicker ?  " + myKick).HtLog ();
        //else            Ag.LogString ("     Pre Player " + preScr.ToString ());
        int enemDir = -1;

        if (!myKick && pEnTurn.skill > 0 && pEnTurn.direction == 5) // panenka ... 
            enemDir = 5;

        AmGameScore gObj = new AmGameScore (myKick, (!myKick ^ kickWon), preScr, pMyTurn, pUnifCstmInfo, kindOfBall, enemDir);
        //Ag.LogString ("AddNewTurn  :: myKick " + myKick + " ,  kickWon " + kickWon + "   I win ? " + (!myKick ^ kickWon));
        arrScore.Add (gObj);
    }

    int RemainKickNum ()
    {
        if (arrScore.Count >= 10)
            return 0;
        int kickNum = 0;
        foreach (AmGameScore gObj in arrScore) {
            if (gObj.isKick)
                kickNum++;
        }
        return 5 - kickNum;
    }

    public void ShowMySelf ()
    {
        Ag.LogStartWithStr (2, "  AmGameLogic ::  " + arrScore.Count.LogWith ("arrScore") + arrGameTurn.Count.LogWith ("arrGameTurn"));
        foreach (AmGameScore gO in arrScore) {
            Ag.LogString (gO.ToString ());
        }
        Ag.LogNewLine (1);
        Ag.LogString ("        NodeGameTurn >>>>>>  " + arrGameTurn.Count + "  ea");
        string turnInfo = "";
        foreach (NodeGameTurnRslt gO in arrGameTurn) {
            turnInfo += gO.ToString ();
        }
        Ag.LogString (turnInfo);
        Ag.LogString (" My Goal Number is ::  >>>>>>>>>>>>>>>>  Goal EA : " + GetGoalNumber () + "   ! ! ! ! <<<<<<<<<<<<<<<<<<  <<<   ");
        Ag.LogIntense (2, false);
    }

    AmGameScore GetPreObj ()
    {
        int ea = arrScore.Count;
        if (ea < 2)
            return null;
        return arrScore [ea - 2];
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Game Score  _____  Sub Class  _____
    public class AmGameScore
    {
        public bool isKick, didWin;
        public int KindOfBall, curSkill, curDirect, unifShirt16, unifPants16, unifSock16, costume04, enemyDir;
        // 키커일때 키커의 방향. 스킬이 0이면 0. for panenka ..
        public float BaseScore, PlayScore;
        public float bonusRond, bonusUnif, bonusCstm;
        NodeGameTurnRslt mCurTurn;

        public AmGameScore (bool pKick, bool pWin, AmGameScore preObj, NodeGameTurnRslt curTurn, int[] unifCstm, int kindOfBall = 0, int enDir = -1)
        {
            KindOfBall = kindOfBall;  // bronze : 1,  silver : 2, gold : 3  No : 0..
            isKick = pKick;
            didWin = pWin;
            mCurTurn = curTurn;
            curSkill = curTurn.skill;
            curDirect = curTurn.direction;
            enemyDir = enDir; // for panenka only ...
            unifShirt16 = unifCstm [0];
            unifPants16 = unifCstm [1];
            unifSock16 = unifCstm [2];
            costume04 = unifCstm [3];
            Ag.LogString (" AmGameScore :: Creation     >>>   Uniform : " + unifCstm [0] + "/" + unifCstm [1] + "/" + unifCstm [2] + "    Costume : " + unifCstm [3]);
            GetScore ();
        }

        public override string ToString ()
        {
            string sign = isKick ? "\t\t Kicker >>>>>>>  " : "\t  _____  Keeper __ \t";
            string rStr = ("   GameScore : gradeBns : " + GetRatioOfGrade () + " \tlevelBns : " + GetRatioOfLevel () +
                          " \tenchantBns : " + mCurTurn.enchant * 0.1f);
            rStr += string.Format (" \t\tWin?={0}, \tKick={1} {2}\tDir / Skill= {3} / {4} \t bonusU/C : {5} / {6} / {7} \t Base/Play Score : {8} / {9} \t\t Score: {10}   ]", 
                didWin, isKick, sign, curDirect, curSkill, bonusUnif, bonusCstm, " __ ", BaseScore, PlayScore, GetScore ());
            return rStr;
        }

        public float GetScore ()
        {
            BaseScore = GetBaseScore (); // ex) 200
            float gradeBonusRatio = GetRatioOfGrade (); // ex) 0.15
            float levelBonusRatio = GetRatioOfLevel (); // ex) 0.16
            float enchantBonusRatio = mCurTurn.enchant * 0.1f;
            // Uniform  / Costume
            float unifBonusRatio = 0;  // Start with 0..
            unifBonusRatio += unifShirt16 == 1 ? 0f : 0.04f;  // 셔츠의 값 1 ~ 6
            unifBonusRatio += unifPants16 == 1 ? 0f : 0.03f; // 팬츠
            unifBonusRatio += unifSock16 == 1 ? 0f : 0.02f; // 양말.
            float cstmBonusRatio = costume04 * 0.02f; // == 0 ? 0f : 0.03f; // Costume  0 ~ 4 .. 0/2/4/6/8 % No costume => 0

            Ag.LogString ("GetScore :: Ratio : Grd / Lvl / Enchant " + gradeBonusRatio + " / " + levelBonusRatio + " / " + enchantBonusRatio);

            PlayScore = BaseScore * (1 + gradeBonusRatio + levelBonusRatio + enchantBonusRatio);
            bonusUnif = BaseScore * unifBonusRatio;
            bonusCstm = BaseScore * cstmBonusRatio;
            //bonusBall = 1 + KindOfBall * 0.1f;  // ex) 1.2

            return PlayScore + bonusUnif + bonusCstm; // + AddedScore;
        }
        //  _////////////////////////////////////////////////_    _____   Base   _____   Score   _____
        /// <summary>
        /// returns base score ...   50, 100, 150, 200, 300
        /// </summary>
        /// <returns>The base score.</returns>
        float GetBaseScore ()
        {
            //Ag.LogIntenseWord (" Turn Number " + mCurTurn.turnNum);
            if (!didWin) // 지면 무조건 0점
                return 0;
            if (mCurTurn.turnNum > 10)
                return 100; // 4 6라운드부터는 앞의 기준을 무시하고 킥커는 골성공 시 100점, 골키퍼는 골방어 시 100점으로 계산
            if (isKick) {
                if (curDirect == 5) // Panenka 성공 150점  .. 200
                    return 150;
            } else {
                if (enemyDir == 5) // 파넨카를 막은 경우. 나의 방향/스킬 무관.
                    return 150;
                if (curDirect * curSkill == 0) // 똥볼을 똥점프로 막은 경우 기본점수 50점 .. 100
                    return 50;
            }
            if (curSkill == 0 || curSkill == 1)
                return 100;
            if (curSkill == 2)
                return 150;
            if (curSkill == 3)
                return 300;
            Ag.LogIntenseWord (" Base Score Error ");
            return 100; // Error case ..  100, 200, 300 ... 
        }

        float GetRatioOfGrade ()
        {
            int percent = 0; //, added = Tbl.dicGamePointOfCardGrade [mCurTurn.level];
            switch (mCurTurn.grade) {
            case "S":
                percent = 40;
                //added *= 2;
                break;
            case "A":
                percent = 20;
                //added *= 2;
                break;
            case "B":
                percent = 10;
                break;
            case "C":
                percent = 5;
                break;
            }
            return ((float)percent) * 0.01f;
            //return ((float)(percent + added)) * 0.01f;
        }

        float GetRatioOfLevel ()
        {
            int added = Tbl.dicGamePointOfCardGrade [mCurTurn.level];
            if (mCurTurn.grade == "S" || mCurTurn.grade == "A")
                added *= 2;
            return added * 0.01f;
        }
    }
}

