// [2013:3:13:MOON]  Start AI job... 
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasUserInfo  _____  Class  _____
public partial class AmUser : AmObject
{
    public int SingleTryDone { get { return etcInfoObj.SingleTry; } }

    /// <summary>
    /// 체험 시작. 체험 완료로 서버에 업데이트
    /// </summary>
    public void SetCardsForSingleTry ()
    {
        if (Ag.SingleTry == 0)
            return;
        SetBotGrade (); // 봇의 그레이드는 오리지날로.

        string grade = Ag.SingleTry == 1 ? "A" : "S";
        // apply to only "S" grade ....   not A cards ... 
        string [] polyInfo = new string [] {"W_S_Goalie001", "W_S_Kicker001", "W_S_Kicker002", "W_S_Kicker003", "W_S_Kicker004", "W_S_Kicker005"};
        string [] polyName = new string [] {"%EB%A0%88%ED%8F%AC%20%EC%95%BC%EC%89%B0", "%EB%A7%88%EB%9D%BC%EB%8F%88%EC%95%84", "%ED%97%AC%EB%A0%88", "%EC%98%A4%EB%82%98%ED%9B%84%EB%91%90", "%EC%A7%80%EB%82%9C", "%EB%B0%94%EA%B2%90%EB%B0%94%EC%9A%B0%EC%98%A4"};

        for (int k = 0; k < 6; k++) {
            AmCard kick = GetCardOrderOf (k);
            kick.SetGradeLevelWithNewDirection (grade, 10);  // Kick order initialize ...
            kick.WAS.kickOrder = k;
            if (grade == "S") {
                kick.WAS.info = polyInfo [k];
                kick.WAS.playerName = polyName [k];
            }
        }
        etcInfoObj.SingleTry += Ag.SingleTry; // 0, 1, 2, 3
        UpdateEtcInfoObj ("SetCardsForSingleTry");
    }

    public void GiveUpBuyCard () {
        etcInfoObj.ExperienceScard = 1;
        UpdateEtcInfoObj ("GiveUpBuyCard");
    }
    /// <summary>
    /// PRO_3 => return 3 ..
    /// </summary>
    /// 
    /// 
    /// 

    public int LeagueAsInt { 
        get {
            if (WAS.League == null || WAS.League.Length < 4)
                return 0;
            return int.Parse (WAS.League.Substring (4, 1)); // PRO_3
        }
    }

    public void ForDebugSingleTryInit ()
    {
        etcInfoObj.SingleTry = 0;
        UpdateEtcInfoObj ("ForDebugSingleTryInit");
    }



    public void ForDebugDailyCheck ()
    {
        etcInfoObj.DailyChkMon = 1;
        etcInfoObj.DailyChkDay = 1;
        UpdateEtcInfoObj ("ForDebugDailyCheck");
    }

    private List<string> arrBotGrade = null;

    public string GetBotGrade ()
    {
        if (arrBotGrade == null || arrBotGrade.Count == 0)
            SetBotGrade ();
        string rV = arrBotGrade [0];
//        if (arrBotGrade.Count == 5 && Ag.SingleTry > 0)
//            rV = "A";
//        if (arrBotGrade.Count == 4 && Ag.SingleTry > 0)
//            rV = "C";
        arrBotGrade.RemoveAt (0);
        return rV;
    }

    private void SetBotGrade ()
    {
        arrBotGrade = new List<string> ();
        for (int k = 0; k < 6; k++) {
            string curGrd = GetCardOrderOf (k).WAS.grade;
            if (k < 1)
                arrBotGrade.Add (curGrd);
            else
                arrBotGrade.Insert (AgUtil.RandomInclude (0, k - 1), curGrd);
        }
        Ag.LogStartWithStr (2, " AmUser :: SetBotGrade  ...   " + arrBotGrade [0] + arrBotGrade [1] + arrBotGrade [2] + arrBotGrade [3]);
    }

    public int WhatKindoBot ()
    {
        Ag.LogStartWithStr (5, " AmUser AI.cs     WhatKindoBot ()   >>>    myRank.WAS.thisWeekRank : " + myRank.WAS.thisWeekRank);

        if (Ag.BotTestSetting >= 0)
            return Ag.BotTestSetting;

        if (Ag.SingleTry > 0)
            return 0;

        if (myRank.WAS.thisWeekRank < 50)
            return -1;  //return 3;

        if (!DidProcessFirstGame)
            return 1; // It's the First Game .. 
        // 첫 승 올리고 FirstGameDoneWithBot() 부를 것.

        Ag.LogString ("      - - - - - - League : " + WAS.League + "   Cont Lose Num : " + ContLoseNum + "   contWinNum : " + myRank.WAS.contWinNum);

        if (WAS.League == "PRO_5" && ContLoseNum == 3)
            return 2; // 1;
        if ((WAS.League == "PRO_3" || WAS.League == "PRO_4") && ContLoseNum == 3)
            return 2;
        if (myRank.WAS.contWinNum == 5)
            return 3;
        if (myRank.WAS.contWinNum == 11) // 8)
            return 4;
        if ((WAS.League == "PRO_1" || WAS.League == "PRO_2") && ContLoseNum == 3)
            return 0; // General
        return -1;
    }

    public void xx4Debug_MakeMyCardsGradeS (AmUser pUser)
    {
        int kickOdr = 1;
        bool keep = false;
        for (int k = 0; k < arrCard.Count; k++) {
            AmCard curCd = arrCard [k];
            curCd.WAS.grade = "S";
            curCd.WAS.InitDirectionAndSkill ();

            if (curCd.WAS.isKicker) {
                curCd.WAS.kickOrder = kickOdr < 6 ? kickOdr++ : -1;
            } else {
                curCd.WAS.kickOrder = keep ? -1 : 0;
                if (keep)
                    curCd.WAS.kickOrder = -1;
                else {
                    curCd.WAS.kickOrder = 0;
                    keep = true;
                }
            }
        }
                        
//        WasCardUpdate aObj = new WasCardUpdate () { User = pUser };
//        aObj.messageAction = (int pInt) => {
//            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
//            case 0:
//                Ag.LogString (" result : Success ");
//                return;
//            }
//        };
//    
    }

    /// <summary>
    /// I am Enemy ..  에너미를 봇으로 세팅
    /// Bots the uniform card rank item setting.
    /// </summary>

    public void BotUniformCardRankItemSetting ()
    {
        // Uniform Setting.
        //Ag.mVirServer.BotIDSetting ();

        arrUniform = new List<AmUniform> ();
        arrUniform.Add (Ag.mVirServer.SetUniform (Ag.mVirServer.maiGradeOfBot)); //, arrUniform [0]); // 봇은 유니폼 1개만 필요.

        Ag.LogIntenseWord (" BotUniformCardRankItemSetting  " + arrUniform.Count);

        arrUniform [0].ShowMySelf ();

        // Card Setting
        arrCard = new List<AmCard> ();
        //  _____  Set Keeper
        AmCard aCard = new AmCard ();
        aCard.WAS.SetVarInBot (false, Ag.mySelf.GetBotGrade (), AgUtil.RandomInclude (0, 4));
        aCard.WAS.isKicker = false;
        aCard.WAS.kickOrder = 0;
        aCard.WAS.InitDirectionAndSkill ();
        aCard.SetBotScouter ();
        aCard.SetBotPlayerInfo ();
        aCard.WAS.ShowMySelf ();
        arrCard.Add (aCard);
        //  _____  Set Kicker
        for (int k = 0; k < 5; k++) {
            aCard = new AmCard ();
            //aCard.WAS.SetVarInBot (true, "C", AgUtil.RandomInclude (0, 4));
            aCard.WAS.SetVarInBot (true, Ag.mySelf.GetBotGrade (), AgUtil.RandomInclude (0, 4));
            aCard.WAS.InitDirectionAndSkill ();
            aCard.SetBotScouter ();
            aCard.SetBotPlayerInfo ();
            aCard.WAS.isKicker = true;
            aCard.WAS.kickOrder = k + 1;
            aCard.WAS.ShowMySelf ();
            arrCard.Add (aCard);
        }

        myRank.SetAsBot (Ag.mySelf.LeagueAsInt, Ag.mySelf.myRank);
        Ag.NodeObj.EnemyUser = Ag.myEnem;
        Ag.LogString ("WeekWinNum" + Ag.NodeObj.EnemyUser.myRank.WAS.winNumWeek);

        // Item Setting
        arrItem = new List<AmItem> ();
        // Message
        AmItem aItm = new AmItem ();
        aItm.WAS.itemTypeID = "StartMessage";
        aItm.SetVarInBot ();
        arrItem.Add (aItm);
        aItm = new AmItem ();
        aItm.WAS.itemTypeID = "EndMessage";
        aItm.SetVarInBot ();
        arrItem.Add (aItm);
        aItm = new AmItem ();
        aItm.WAS.itemTypeID = "CeremonyDefault";
        aItm.SetVarInBot ();
        arrItem.Add (aItm);
    }
}
