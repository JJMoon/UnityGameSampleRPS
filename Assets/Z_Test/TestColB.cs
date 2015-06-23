using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System.Timers;
using LitJson;
using SimpleJSON;

// http://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa  dictionary <-> object   mapping example ..
public partial class Test : AmSceneBase
{
    string scoutMsg = "scout", scouterStr = " yet ";


    Timer sysTimer;

    public  void SetColumnB ()
    {
        int colN = 0, colEA;

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  ***  _____  column 2  _____
        muiCol++;
        muiRow = 0;






        //  _////////////////////////////////////////////////_    _____  Login  _____    User 2    _____
        Rect Ruser2 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 2;

        if (GUI.Button (myGUI.DivideRect (Ruser2, colEA, colN++), "User2")) {
            WasLogin aObj = new WasLogin () { User = user2, 
            };
            aObj.messageAction = (int pInt) => {
                if (pInt == 0) {
                    WasUserInfo bObj = new WasUserInfo () { User = user2, flag = 1 };
                }
            };
        }
        if (GUI.Button (myGUI.DivideRect (Ruser2, colEA, colN++), "Info")) {
            WasUserInfo aObj = new WasUserInfo () { User = user2, flag = 0 };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }



        WasRank rnk = myUser.myRank.WAS, rk2 = user2.myRank.WAS;
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), "Win:" + rnk.winNum + "  Los:" + rnk.lossNum + "  Rnk:" + rnk.thisWeekRank + "  Scr:" + rnk.weekScore +
        "  CntWin:" + rnk.contWinNum);
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), "Win:" + rk2.winNum + "  Los:" + rk2.lossNum + "  Rnk:" + rk2.thisWeekRank + "  Scr:" + rk2.weekScore +
        "  CntWin:" + rk2.contWinNum);

      

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    User2 & Game   _____
        Rect curRuser2 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
         
        if (GUI.Button (myGUI.DivideRect (curRuser2, colEA, colN++), "G Start")) {  // Game Start & Report
            WasGameStart aObj = new WasGameStart () { User = myUser, enemyID = user2.WAS.KkoID, friendGame = 0,
                contWinMyFlag = 1, contWinEnemFlag = 1,
                arrCardId = myUser.GetMainCardIDs (), arrayEnemyId = user2.GetMainCardIDs ()
            };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        }
        if (GUI.Button (myGUI.DivideRect (curRuser2, colEA, colN++), "Report")) {  // Game Start & Report
            WasGameReport aObj = new WasGameReport () {
                User = myUser, winnerID = myUser.WAS.KkoID, loserID = user2.WAS.KkoID,
                winPo = 3500, losPo = 0
            };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        }

        //        if (GUI.Button (myGUI.DivideRect (curRuser2, colEA, colN++), "Bot : S")) {  // Game Start & Report
        //            WasGameStart aObj = new WasGameStart () { User = myUser, enemyID = "BOT", friendGame = 0,
        //                arrCardId = myUser.GetMainCardIDs (), arrayEnemyId = new int[] { 0, 0, 0, 0, 0 }
        //            };
        //            aObj.messageAction = (int pInt) => {
        //                switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
        //                case 0:
        //                    myUser.WAS.GameSessionKey = aObj.NdObj["gameSessionKey"];
        //                    return;
        //                }
        //            };
        //        }
        //        if (GUI.Button (myGUI.DivideRect (curRuser2, colEA, colN++), "B:Rprt")) {  // Game Start & Report
        //            WasGameReport aObj = new WasGameReport () {
        //                //User = myUser, winnerID = myUser.WAS.KkoID, loserID = "BOT",
        //                User = myUser, winnerID = myUser.WAS.KkoID, loserID = "BOT",
        //                winPo = 5000, losPo = 0
        //            };
        //            aObj.messageAction = (int pInt) => {
        //                switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
        //                case 0:
        //                    Ag.LogString (" result : Success ");
        //                    return;
        //                }
        //            };
        //        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Deck   _____
        Rect rctDeck = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (rctDeck, colEA, colN++), "Deck")) {  // Game Start & Report

            myUser.WAS.DeckItem = new int[] { 3, 3, 4 };

            // country setting
            foreach (AmCard aCd in myUser.arrCard) {
                aCd.WAS.country = "Cntry_" + AgUtil.RandomInclude (1, 3);
            }

            myUser.ApplyCurrentDeck ();
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Scouter   _____
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), scoutMsg + "  " + scouterStr);
        Rect rctScut = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (rctScut, colEA, colN++), "0_1_ +")) {  // Game Start & Report
            AmCard ccard = myUser.GetCardOrderOf (1);
            ccard.AddScouterValue (AgUtil.RandomInclude (1, 4), true);
            scoutMsg = ccard.WAS.scouter; //ccard.ScoutObj.GetString ();
        }
        if (GUI.Button (myGUI.DivideRect (rctScut, colEA, colN++), "Set")) {  // Game Start & Report
            AmCard ccard = myUser.GetCardOrderOf (1);
            int num = 1;
            scouterStr = ccard.ScoutObj.GameNumberOfDirect (num++) + " / " + ccard.ScoutObj.GameNumberOfDirect (num++) + " / " +
            ccard.ScoutObj.GameNumberOfDirect (num++) + " / " + ccard.ScoutObj.GameNumberOfDirect (num++) +
            " / " + ccard.ScoutObj.GameNumberOfDirect (num++);
            scoutMsg = ccard.WAS.scouter; //ccard.ScoutObj.GetString ();
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Card   _____
        Rect curCard = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 5;
        if (GUI.Button (myGUI.DivideRect (curCard, colEA, colN++), "C:Extend")) { // Card
            WasCardExtend aObj = new WasCardExtend () {
                User = myUser,
                cardId = myUser.arrCard [2].WAS.ID,
                count = 50,
            };
            aObj.messageAction = (int pInt) => {
            };
        }
        if (GUI.Button (myGUI.DivideRect (curCard, colEA, colN++), "Enchant")) { // Card 
            WasCardEnchantRecover aObj = new WasCardEnchantRecover () {
                User = myUser, code = 251, cardID = myUser.arrCard [0].WAS.ID
            };
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curCard, colEA, colN++), "Recover")) { // Card
            WasCardEnchantRecover aObj = new WasCardEnchantRecover () {
                User = myUser, code = 254, cardID = myUser.arrCard [1].WAS.ID
            };
            aObj.messageAction = (int pInt) => {
            };
        }
        if (GUI.Button (myGUI.DivideRect (curCard, colEA, colN++), "Level Up")) { // Card
            WasCardLevelup aObj = new WasCardLevelup () {
                User = myUser, cardID = myUser.arrCard [1].WAS.ID
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        if (GUI.Button (myGUI.DivideRect (curCard, colEA, colN++), "Combi")) { // Card
            int idx = myUser.arrCard.Count - 1;
            WasCardCombi aObj = new WasCardCombi () {
                User = myUser, cardID1 = myUser.arrCard [idx--].WAS.ID, cardID2 = myUser.arrCard [idx--].WAS.ID, cardID3 = myUser.arrCard [idx--].WAS.ID//, buyType = 0 // Combi
            };
//            aObj.arrItemStr.Add ("CardCombiAdvt");
//            aObj.arrItemStr.Add ("CardCombiAdvtHigh"); // 
            //aObj.arrItemStr.Add ("CardCombiGrade");
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    WasCardUniformCostume aaObj = new WasCardUniformCostume () { User = myUser, code = 240 };
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        }
       
        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Func   _____
        Rect curR4 = myGUI.GetRect (muiCol, muiRow++);
        if (GUI.Button (myGUI.DivideRect (curR4, 4, 0), "InitRank")) {
            WasFuncInitRank aObj = new WasFuncInitRank () { User = myUser  };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curR4, 4, 1), "TeamEdit")) {
            WasFuncTeamEdit aObj = new WasFuncTeamEdit () {
                User = myUser, nuTeamName = "IwasYOURMOTHER"

            };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curR4, 4, 2), "BackNum")) {
            WasFuncBackNumEdit aObj = new WasFuncBackNumEdit () {
                User = myUser, cardID = myUser.arrCard [2].WAS.ID,
                backNum = 77,
                playerName = "Mama"
            };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curR4, 4, 3), "CostUp")) {
            WasFuncCostUp aObj = new WasFuncCostUp () { User = myUser };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 

        muiRow++;

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Util   _____
        Rect noRet = myGUI.GetRect (muiCol, muiRow++);
        colEA = 2;
        colN = 0;
        if (GUI.Button (myGUI.DivideRect (noRet, colEA, colN++), "No Return")) {
            WasNoReturn aObj = new WasNoReturn () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        }

        if (GUI.Button (myGUI.DivideRect (noRet, colEA, colN++), "Invite")) {
            WasInvite aObj = new WasInvite () { User = myUser, friendID = "88894476708738001" };
            aObj.messageAction = (int pInt) => {
            };
        } 

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Util   _____
        Rect curA2 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (curA2, colEA, colN++), " Review Event ")) {  // App Store Event
            WasReview aObj = new WasReview () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curA2, colEA, colN++), "  1st.Bot ")) {  // Perform 1st Game with Bot
            Ag.LogString ("  myUser.DidProcessFirstGame   " + myUser.DidProcessFirstGame);
            myUser.FirstGameDoneWithBot ();
            Ag.LogString ("  myUser.DidProcessFirstGame   " + myUser.DidProcessFirstGame);
        } 
        if (GUI.Button (myGUI.DivideRect (curA2, colEA, colN++), "Daily:" + DailyChkReturn.ToString ().Substring (0, 2))) {  // 출 첵.
            //DailyChkReturn = myUser.ShowDailyEvent;
            myUser.CheckFirstDailyEventToday ();
            //if (!DailyChkReturn)                 myUser.TEST_ResetDailyEvent ();
        }
        if (GUI.Button (myGUI.DivideRect (curA2, colEA, colN++), " Win/Lose ")) {
            myUser.DidWinOrLoseGame (AgUtil.RandomInclude (1, 111) % 2 == 1);
            //if (!DailyChkReturn)                 myUser.TEST_ResetDailyEvent ();
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Util   _____
        Rect rec00 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (rec00, colEA, colN++), "IniDaily")) {
            myUser.ForDebugDailyCheck ();
        }
        if (GUI.Button (myGUI.DivideRect (rec00, colEA, colN++), "DayEvnt")) {
            myUser.CheckFirstDailyEventToday ();
        }
        if (GUI.Button (myGUI.DivideRect (rec00, colEA, colN++), "Debug:IniTry")) {
            myUser.ForDebugSingleTryInit ();
        }

        //  _////////////////////////////////////////////////_    _____  Buff  _____    Deck   _____
        Rect recDk = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (recDk, colEA, colN++), "Deck")) {
            Tbl.GetBuffGoldPoint (99).ToString().HtLog() ;
            Tbl.GetBuffGoldPoint (1).ToString().HtLog() ;
        }

    }
}