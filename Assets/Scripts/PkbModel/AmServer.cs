// [2012:11:13:MOON] Started
// [2013:3:13:MOON] Re opened..
// [2014:1:3:MOON] Uniform .. logic change. 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public partial class AmServer
{
    public AmUniform Uniform = new AmUniform ();
    int mTurn, mRound, mBotID;
    // Turn = 2 * kicknum
    bool mMyAdvantage;
    // Kick first..
    public int maiGradeOfBot = -1;
    public string kkoNick, teamName;
    // 1 ~ 4  // or 0 : General...
    public string PrifileName { // "Bot_Profile_034" 리턴. 
        get {
            string head = (mBotID < 10) ? "Bot_Profile_00" : "Bot_Profile_0";
            return head + mBotID;
        }
    }

    public AmServer ()
    {
        SetUniform (2);
        BotIDSetting ();
    }

    public void BotIDSetting ()
    {
        mBotID = AgUtil.RandomInclude (1, 200);
        SetNickTeamName ();
    }

    public enum PopupStore
    {
        None,
        DiscCardPurchase,
        DiscShoes,
        DiscGloves,
        DiscCombi,
        DiscCash,
        DiscHeartDay,
        DiscHeartWeek,
        DiscHeartMonth,
        DiscDaily
    }

    /// <summary>
    /// 어떤 팝업을 띄울지 리턴.
    /// </summary>
    /// <returns>팝업의 종류 </returns>
    public PopupStore KindOfPopup (string pCase, int pScore = 0)
    {
        Debug.Log ("KindOfpopup Test    " + "AgMgdidwin   " + Ag.mgDidWin + " Pscore " + pScore);

        int ranNum = AgUtil.RandomInclude (1, 100);
        switch (pCase) {
        case "GameEnd":
            if (Ag.mySelf.ShowHeartPopup) {
                Ag.mySelf.CheckPopupHeartToday ();

                if (Ag.mySelf.GameNumberOfToday == 10 && !Ag.mySelf.IsFreeCouponRemain)
                    return PopupStore.DiscHeartWeek;
                if (Ag.mySelf.GameNumberOfToday == 15 && !Ag.mySelf.IsFreeCouponRemain)
                    return PopupStore.DiscHeartMonth;
            } 
            //if (Ag.mgDidWin == false && ranNum % 4 == 1) { // 25%
            if (!Ag.mgDidWin && ranNum % 10 == 1) { // 10%
                return PopupStore.DiscCardPurchase;
            }
            if (Ag.mgDidWin && pScore < 1000) { // 테스트 용도로 50000점 이하일때 100% 적용
                int ranN = AgUtil.RandomInclude (1, 999);
                if (ranN % 50 == 4)  // 2 %
                    return PopupStore.DiscGloves;
                if (ranN % 50 == 3)  // 2 %
                    return PopupStore.DiscShoes;
            }
            return PopupStore.None;
        case "AfterCashUse":
            if (ranNum % 5 == 1)
                return PopupStore.DiscCash;
            return PopupStore.None;
        }
        return PopupStore.None;
    }
    // 유니폼 랜덤하게 입을 것 (편집된 상태)
    // 상대 유저와 같은 리그로 표시
    // 상대 선수와 동일한 등급 카드로 라인업을 구성하되 킥커 순서와 레벨은 랜덤
    public AmUniform SetUniform (int step, AmUniform pUnif = null)
    {
        AmUniform theUnif;
        if (pUnif == null)
            theUnif = new AmUniform ();
        else
            theUnif = pUnif;

        int sMai = AgUtil.RandomInclude (5, 12), sSub = AgUtil.RandomInclude (5, 12);
        if (sSub == sMai)
            sSub = (sSub > 8) ? sSub - 1 : sSub + 1;
        // Kicker ..
        int txtr = (step > 2) ? AgUtil.RandomInclude (2, 5) : 1;
        int txtrL = (step > 2) ? AgUtil.RandomInclude (2, 5) : 1;
        theUnif.Kick.SetValue (txtr, sMai, sSub,
            txtrL, AgUtil.RandomInclude (1, 3), AgUtil.RandomInclude (1, 3),
            1, sMai, sSub);
        sMai = AgUtil.RandomInclude (5, 12);
        sSub = AgUtil.RandomInclude (5, 12);
        if (sSub == sMai)
            sSub = (sSub > 8) ? sSub - 1 : sSub + 1;
        // Keeper
        txtr = (step > 2) ? AgUtil.RandomInclude (2, 5) : 1;
        txtrL = (step > 2) ? AgUtil.RandomInclude (2, 5) : 1;
        theUnif.Keep.SetValue (txtr, sMai, sSub,
            txtrL, AgUtil.RandomInclude (1, 3), AgUtil.RandomInclude (1, 3),
            1, sMai, sSub);
        return theUnif;
    }

    public AmCard xxGetFakeCard (string grade, bool isKicker)
    {
        AmCard rObj = new AmCard ();
        rObj.WAS.grade = grade;
        rObj.WAS.level = AgUtil.RandomInclude (0, 9);
        rObj.WAS.isKicker = isKicker;
        rObj.WAS.InitDirectionAndSkill ();
        rObj.WAS.ShowMySelf ();
        return rObj;
    }

    public void Initialize ()
    {
        mTurn = mRound = 0;
        
        System.Random rObj = new System.Random ();
        int ranNum = rObj.Next (111);
        int remain = ranNum % 2;
        Ag.mgSelfWinNo = Ag.mgEnemWinNo = 0;
        Ag.mgIsKick = mMyAdvantage = (remain == 0);

        Ag.myEnem.CopyTextureFrom (Ag.mySelf);
		
        ("  AmServer :: Initialize      Is MY Advantage ??? " + mMyAdvantage).HtLog ();

//        if (mMyAdvantage) {
//            Ag.myEnem.mCurPlayer = Ag.myEnem.GetCurrentKeeper ();  //Ag.mySelf.GetCurrentKeeper();
//            Ag.mySelf.mCurPlayer = Ag.mySelf.GetPlayerOrderOf (1);
//        } else {
//            Ag.myEnem.mCurPlayer = Ag.myEnem.GetPlayerOrderOf (1);
//            Ag.mySelf.mCurPlayer = Ag.myEnem.GetCurrentKeeper ();
//        }
        //Ag.myEnem.mNick = "PSYKICK";
        Ag.myEnem.myRank.mCountry = 0;
        
        Debug.Log ("Enemy Player Uno : " + Ag.myEnem.mCurPlayer.mPlayerUNO);
        Ag.LogIntenseWord ("Server Init : My Advantage :: " + mMyAdvantage);
    }
    //  _////////////////////////////////////////////////_    _____   ...   _____   xxx   _____
    public bool Result (AmCard pCurCard = null)
    {
        Ag.LogStartWithStr (2, "  AmServer ::  Result  ");
        ("   " + Ag.mgIsKick.ShowBool (" My Roll : ", "Kick", "Keep") + "            Bot  Grade  >>> " + maiGradeOfBot + " <<< ").HtLog ();
        ("   " + pCurCard.WAS.isKicker.ShowBool (" CurCard :", "Kicker", "Keeper") + " <<< ").HtLog ();

        ("   Kick Order : " + pCurCard.WAS.kickOrder + "    polygon : " + pCurCard.WAS.info).HtLog ();

        pCurCard.mDirectObj.ShowMyself ();

        // Generate Enemy Direction
        if (Ag.mgIsKick) {
            // Apply my Info to enem direction.. 
            Ag.mgEnemDirec = pCurCard.SetKeeperDirect (maiGradeOfBot);

            //Ag.mgEnemDirec = Ag.mySelf.mCurPlayer.SetKeeperDirect (maiGradeOfBot);
            //Ag.mySelf.mCurPlayer.mDirectObj.GetWideRandomDirect( pApplyWidth:false );
        } else {
            Ag.mgEnemDirec = pCurCard.SetKickerDirect (maiGradeOfBot);

            //Ag.mgEnemDirec = Ag.myEnem.mCurPlayer.SetKickerDirect (maiGradeOfBot);
            // Ag.myEnem.mCurPlayer.mDirectObj.GetWideRandomDirect( true  );
        }
        Ag.LogIntenseWord (" AmServer ::  Result   >>>>> >>>>>    " + Ag.mgIsKick.ShowBool (" I am ", " Kicker ", " Keeper ") + "   Ag.mgEnemDirec  " + Ag.mgEnemDirec);
		
        // Generate Enemy Skill
        System.Random rObj = new System.Random ();
        int great = 0, ddong = 0;

        switch (maiGradeOfBot) {
        case 0:
            great = 20;
            break;
        case 1:
            ddong = 20;
            break;
        case 2:
            ddong = 10;
            great = 10;
            break;
        case 3:
            great = 20;
            break;
        case 4:
            great = 40;
            break;
        }

        if (rObj.GetRandomTrue (great))
            Ag.mgEnemSkill = 2;
        else
            Ag.mgEnemSkill = 1;

        //        if (!Ag.mgIsKick && rObj.GetRandomTrue (perfect))
        //  Ag.mgEnemSkill = 3;

        if (rObj.GetRandomTrue (ddong))
            Ag.mgEnemSkill = 0;

        Ag.LogIntenseWord ("  AmServer ::  Result     Dir / Skl ::  " + Ag.mgEnemDirec + "   /   " + Ag.mgEnemSkill);

        return true;

//        if (Ag.mgIsKick) {
//            Ag.mgEnemDirec = 1;
//            Ag.mgEnemSkill = 1;
//        }

//        // Debugging Log ...
//        Ag.LogString (" AmServer :: Result      Enemy Direct >>  " + Ag.mgEnemDirec + "     Enemy Skill >>  " + Ag.mgEnemSkill);
//
//        //  Kick Result Matrix ... 1: Goul, 2: No goul, 3: Special case..
//        byte[,,] resultMat = new byte[2, 3, 3] { { { 2, 1, 1 }, { 2, 2, 1 }, { 2, 2, 2 } }, // Same Direction  // { Miss }  { Normal }  { Miracle } 
//            { { 2, 1, 1 }, { 2, 1, 1 }, { 2, 3, 1 } }
//        }; // Different Direction
//        // Set Variables...
//        byte kickDir, kickSkl, keepDir, keepSkl;
//        if (Ag.mgIsKick) { 
//            kickDir = Ag.mgDirection; 
//            kickSkl = Ag.mgSkill; 
//            keepDir = Ag.mgEnemDirec; 
//            keepSkl = Ag.mgEnemSkill; 
//            Ag.LogString (" AmServer :: Result   myDir   kick Dir / Skl : " + kickDir + " / " + kickSkl + "       enDir    keep Dir / Skl : " + keepDir + "  /  " + keepSkl);
//
//        } else { 
//            keepDir = Ag.mgDirection; 
//            keepSkl = Ag.mgSkill; 
//            kickDir = Ag.mgEnemDirec; 
//            kickSkl = Ag.mgEnemSkill; 
//            Ag.LogString (" AmServer :: Result   myDir    keep Dir / Skl : " + keepDir + "  /  " + keepSkl + "       enDir    kick Dir / Skl : " + kickDir + " / " + kickSkl);
//        }
//
//        // Kicker's DDong ball Case ....  No goul...
//        if (kickDir == 0 || kickSkl == 0)
//            return ResultSub (2);
//
//        // Kicker's Perfect Kick ... Always Goul
//        if (kickSkl == 3)
//            return ResultSub (1);
//
//        // Check Panenka Kick
//        if (kickDir == 5) {  // skill is 1 ...
//            if (keepDir != 0 && keepSkl == 2)
//                return ResultSub (1);  // Goul..
//            else
//                return ResultSub (2);  // No Goul..
//        }
//
//        // Use Kick Result Matrix ...
//        int nRes = resultMat [kickDir == keepDir ? 0 : 1, keepSkl, kickSkl];
//
//        if (nRes == 3) {
//            if (kickDir % 2 == keepDir % 2)
//                nRes = 2; // No goul
//			else
//                nRes = 1; // Goul in.
//        }
//        if (kickDir == 0)
//            nRes = 2; // kick fail..
//
//        Ag.LogNewLine (4);
//        return ResultSub (nRes);
    }
    //    bool xxResultSub (int pResult)  // 1: Goul, 2: No goul, 3: Special case..
    //    {
    //        // Score Calculation
    //        if (pResult == 1) {
    //            if (Ag.mgIsKick)
    //                Ag.mgSelfWinNo++;
    //            else
    //                Ag.mgEnemWinNo++;
    //        }
    //
    //        if (Ag.mgIsKick ^ pResult == 2)
    //            Ag.mgDidWin = true;
    //        else
    //            Ag.mgDidWin = false;
    //
    //        // Check Game Finish
    //        CheckFinalWin ();
    //
    //        // Who is Final Winner.
    //        if (Ag.mgDidGameFinish) {
    //            if (Ag.mgSelfWinNo > Ag.mgEnemWinNo)
    //                Ag.mgDidWin = true;
    //            else
    //                Ag.mgDidWin = false;
    //        }
    //
    //        // 4 Debugging...
    //        if (Ag.mgDidWin) {
    //            string sign = ">>>>> Win <<<<< ";
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //        }
    //        Ag.LogString ("  AmServer :: ResultSub    Did I Win? :>>" + Ag.mgDidWin.ToString () + "  MyWinNO:>>" + Ag.mgSelfWinNo + "  EnemyWin:>>" + Ag.mgEnemWinNo +
    //        "  EnemDirec:>>" + Ag.mgEnemDirec + "  EnemSkill:>>" + Ag.mgEnemSkill +
    //        "  Game End?:>>" + Ag.mgDidGameFinish.ToString ());
    //        if (Ag.mgDidGameFinish) {
    //            string sign = ">>>>> Finish <<<<< ";
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //            Ag.LogString (sign);
    //        }
    //        Ag.LogNewLine (5);
    //
    //        Ag.mgGamePackReceived = true;
    //        return true;
    //    }
    void CheckFinalWin ()
    {
        int subtrac = (int)Math.Abs (Ag.mgSelfWinNo - Ag.mgEnemWinNo);
        
        if (mRound <= 5) {
            CheckFinalWinBelow5 ();
        } else {
            if (mTurn % 2 == 0 && subtrac != 0)
                Ag.mgDidGameFinish = true;
        }

        Ag.SwitchStep ();  // 2013.9.5 .. Temporary Added .. Check later ... [Moon/JkLee]
    }

    void CheckFinalWinBelow5 ()
    {
        Ag.mgDidGameFinish = false;
        // Check My Win..
        int maxScore = (int)Ag.mgEnemWinNo + RemainKickNum (false); // Enem
        if (maxScore < Ag.mgSelfWinNo)
            Ag.mgDidGameFinish = true;
        
        maxScore = (int)Ag.mgSelfWinNo + RemainKickNum (true);
        if (maxScore < Ag.mgEnemWinNo)
            Ag.mgDidGameFinish = true;
    }

    int RemainKickNum (bool pMyself)
    {
        int remain = 5 - mRound;
        if (mTurn % 2 == 0)
            return remain;  // even Number.
        
        if (mMyAdvantage ^ !pMyself)
            return remain;
        else
            return remain + 1;
    }
}

