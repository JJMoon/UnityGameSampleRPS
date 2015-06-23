using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;
using LitJson;
using SimpleJSON;

public partial class Test : AmSceneBase
{
    public void TestEncript ()
    {
        //  _////////////////////////////////////////////////_    _____  Test  _____    Encript   _____
        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____    Encript   _____");
        UTAES.AESEncrypt128 ("1~`aA+_';\"지금").HtLog ();
        Ag.LogNewLine (3);

        ("1~`aA+_';\"" + WWW.EscapeURL ("지금")).HtLog ();
        UTAES.AESEncrypt128 ("1~`aA+_';\"%EA%B0%80").HtLog (); //+ WWW.EscapeURL("가")).HtLog();

        string encrpt = UTAES.AESEncrypt128 ("This is test string   ");
        encrpt = UTAES.AESDecrypt128 (encrpt);
        Ag.LogIntenseWord ("  UTAES Encript >> " + encrpt);

        encrpt = UTAES.AESEncrypt128 ("1~`aA+_';\"%EA%B0%80", true);
        encrpt = UTAES.AESDecrypt128 (encrpt, true);
        Ag.LogIntenseWord ("  UTAES  private Encript >> " + encrpt);

        encrpt = UTAES.AESEncrypt128 ("1~`aA+_';\"지금", true);
        encrpt = UTAES.AESDecrypt128 (encrpt, true);
        Ag.LogIntenseWord ("  UTAES  private Encript >> " + encrpt);

        Ag.LogDouble ("  AESEncrypt128    Test              >>>>>>>>>>>>>>>>>>>>>>>   .... >>>>       _____  End   >>>  "); 


    }

    public  void GameLogic ()
    {
        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____    Score   _____");
        NodeGameTurnRslt my, en;


        int kickDir = 0, kickSkl, defnDir, defnSkl;
        bool rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, 1, 2);
        rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, kickDir, 2);
        rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, 1, 2);
        rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, 1, 2);
        rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, 1, 2);
        rsss = AgUtilGame.DidKickerWinThisTurn (kickDir++, 3, 1, 2);


        Ag.LogIntenseWord (" Test   :::   " + rsss);


        AmGameLogic myLogic = new AmGameLogic ("League_3", "League_3"); // 5 - 3 => 2... 3 - 5 => 0 ....  !!!! 
        AmGameLogic enLogic = new AmGameLogic ("League_3", "League_3");

        Ag.LogStartWithStr (10, " Round 1");
        my = new NodeGameTurnRslt () { roll = "KICK", direction = 4, skill = 1, grade = "A", level = 2, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KEEP", direction = 0, skill = 0, grade = "C", level = 2, enchant = 0 };
        myLogic.AddNewTurn (my, en, 0, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 0, new int[] { 0, 0, 0, 0, 1, 1, 1, 1 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();

        myLogic.GetTotalScore ().ToString ().HtLog ();
        enLogic.GetTotalScore ().ToString ().HtLog ();

        my = new NodeGameTurnRslt () { roll = "KEEP", direction = 0, skill = 0, grade = "C", level = 2, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KICK", direction = 4, skill = 1, grade = "A", level = 2, enchant = 0 };
        myLogic.AddNewTurn (my, en, 0, new int[] { 0, 0, 0, 0, 1, 1, 1, 1 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 0, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();
        Ag.LogDouble (" TotalScore : " + myLogic.GetTotalScore () + " Cere : " + myLogic.CeremonyBonus + "  Round : " + myLogic.TotalRound + "   TurnBonus : " + myLogic.UiTurnBonus);

        myLogic.GetTotalScore ().ToString ().HtLog ();
        enLogic.GetTotalScore ().ToString ().HtLog ();



        Ag.LogNewLine (10);

        // N:N => 0:0
        Ag.LogStartWithStr (10, " Round 2");
        my = new NodeGameTurnRslt () { roll = "KICK", direction = 4, skill = 1, grade = "B", level = 7, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KEEP", direction = 1, skill = 1, grade = "B", level = 10, enchant = 0 };
        myLogic.AddNewTurn (my, en, 3, new int[] { 1, 3, 1, 3, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 3, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        my = new NodeGameTurnRslt () { roll = "KEEP", direction = 2, skill = 1, grade = "D", level = 0, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KICK", direction = 2, skill = 1, grade = "C", level = 1, enchant = 0 };
        myLogic.AddNewTurn (my, en, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();
        Ag.LogDouble (" TotalScore : " + myLogic.GetTotalScore () + " Cere : " + myLogic.CeremonyBonus + "  Round : " + myLogic.TotalRound + "   TurnBonus : " + myLogic.UiTurnBonus);

        // G:G => 1:1
        Ag.LogStartWithStr (10, " Round 3");
        my = new NodeGameTurnRslt () { roll = "KICK", direction = 4, skill = 1, grade = "B", level = 7, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KEEP", direction = 1, skill = 1, grade = "B", level = 10, enchant = 0 };
        myLogic.AddNewTurn (my, en, 2, new int[] { 1, 3, 1, 3, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 2, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        my = new NodeGameTurnRslt () { roll = "KEEP", direction = 0, skill = 2, grade = "D", level = 0, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KICK", direction = 1, skill = 1, grade = "C", level = 1, enchant = 0 };
        myLogic.AddNewTurn (my, en, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();
        Ag.LogDouble (" TotalScore : " + myLogic.GetTotalScore () + " Cere : " + myLogic.CeremonyBonus + "  Round : " + myLogic.TotalRound + "   TurnBonus : " + myLogic.UiTurnBonus);
        bool? finalWin = myLogic.DidIFinalWin (enLogic);
        if (finalWin.HasValue)
            Ag.LogDouble ("   >>>>>>>>>>         Game is Over     Did I Win ? >>  " + finalWin.Value);

        // G:G => 1:1
        Ag.LogStartWithStr (10, " Round 4");
        my = new NodeGameTurnRslt () { roll = "KICK", direction = 4, skill = 1, grade = "B", level = 7, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KEEP", direction = 1, skill = 1, grade = "B", level = 10, enchant = 0 };
        myLogic.AddNewTurn (my, en, 2, new int[] { 1, 3, 1, 3, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 2, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();
        Ag.LogDouble (" TotalScore : " + myLogic.GetTotalScore () + " Cere : " + myLogic.CeremonyBonus + "  Round : " + myLogic.TotalRound + "   TurnBonus : " + myLogic.UiTurnBonus);

        finalWin = myLogic.DidIFinalWin (enLogic);
        if (finalWin.HasValue)
            Ag.LogDouble ("   >>>>>>>>>>         Game is Over     Did I Win ? >>  " + finalWin.Value);




        my = new NodeGameTurnRslt () { roll = "KEEP", direction = 1, skill = 2, grade = "D", level = 0, enchant = 0 };
        en = new NodeGameTurnRslt () { roll = "KICK", direction = 0, skill = 1, grade = "C", level = 1, enchant = 0 };
        myLogic.AddNewTurn (my, en, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 }); // My Kick, Goal..  Win
        enLogic.AddNewTurn (en, my, 0, new int[] { 1, 1, 1, 0, 1, 1, 1, 0 });
        myLogic.ShowMySelf ();
        enLogic.ShowMySelf ();
        Ag.LogDouble (" TotalScore : Me >> " + myLogic.GetTotalScore () + " Cere : " + myLogic.CeremonyBonus + "  Round : " + myLogic.TotalRound + "   TurnBonus : " + myLogic.UiTurnBonus);
        Ag.LogDouble (" TotalScore : En >> " + enLogic.GetTotalScore () + " Cere : " + enLogic.CeremonyBonus + "  Round : " + enLogic.TotalRound + "   TurnBonus : " + enLogic.UiTurnBonus);


//        bool? finalWin = myLogic.DidIFinalWin (enLogic);
//        if (finalWin.HasValue)
//            Ag.LogIntenseWord ("   >>>>>>>>>>         Game is Over     Did I Win ? >>  " + finalWin.Value);
//
//        (myLogic.GetTotalScore ().LogWith (" My Total Score ") + enLogic.GetTotalScore ().LogWith (" Enemy Score")).HtLog ();
//        (myLogic.SemiTotal.LogWith ("myLogic.SemiTotal") + "  UI Turn Bonus  " + myLogic.UiTurnBonus).HtLog ();
//        Ag.LogNewLine (2);

    }
}