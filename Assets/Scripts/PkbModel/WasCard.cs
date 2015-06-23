using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasCardInfo  _____  Class  _____
public class WasCardInfo
{
    public string polygonName = " ", info = " Info ", log = " [ some Log ] ";
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasCard  _____  Class  _____
public partial class WasCard
{
    public bool isKicker;
    public int[] direction, skill;
    public int limitGameEA, cost, level, enchant, condition, backNum, kickOrder, price4trade, ID;
    public string info, grade, playerName, tradeID, tradeState, dirSuccessRatio, leagueType, scouter;
    // SCount
    public int tradeTs;
    // No Parsing ...
    public bool mustUpdate;
    // Deck Item
    public string country, position;
    public int playerID, look;
    public int DIkickDir = 0, DIkickSkl = 0, DIkeepBal = 0, DIkeepSkl = 0;
    // playerID : number  - position : string  - country : string  - look : number

    public int KickOrder {
        set {
            if (value == kickOrder)
                return;
            Ag.LogDouble ("  Kick Order Set  :::   from >  " + kickOrder + "   to > " + value);
            kickOrder = value;
            mustUpdate = true;
        }
        get {
            return kickOrder;
        }
    }

    public int GradeValue {
        get {
            switch (grade) {
            case "S":
                return 1;
            case "A":
                return 2;
            case "B":
                return 3;
            case "C":
                return 4;
            }
            return 5;
        }
    }

    public WasCard ()
    {
        info = playerName = tradeID = tradeState = dirSuccessRatio = "";
        grade = "D";
        tradeTs = 0; //DateTime.Now;
        direction = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // width, posi, 
        skill = new int[] { 0, 0, 0 };
    }

    public void SetDeckItemValue (int pKD, int pKS, int pPBal, int pPS)
    {
        //Ag.LogString ("    SetDeckItemValue (int pKD, int pKS, int pPBal, int pPS)   " + pKD + " / " + pKS + " / " + pPBal + " / " + pPS);
        DIkickDir = pKD;
        DIkickSkl = pKS;
        DIkeepBal = pPBal;
        DIkeepSkl = pPS;
    }

    public int GetWidthOfDirection (int dir)
    { // 1, 2, 3, 4  .. index  is 0 2 4 6
        return direction [(dir - 1) * 2];
    }

    public void SetVarInBot (bool IsKicker, string Grade, int Level)
    {
        isKicker = IsKicker;
        //grade = Grade;
        grade = (Grade == "S")? "A" : Grade;
        level = Level;
        limitGameEA = 3;
        cost = 1;
        condition = enchant = 0;

        string head = "";
        int num = 0;
        if (isKicker) {
            num = AgUtil.RandomInclude (1, 17);
            head = (num < 10) ? "W_N_Kicker00" : "W_N_Kicker0";
        } else {
            num = AgUtil.RandomInclude (1, 4);
            head = (num < 10) ? "W_N_Goalie00" : "W_N_Goalie0";
        }
        info = head + num;
        backNum = AgUtil.RandomInclude (9, 95);
        playerName = GetName ();
    }

    public string GetName ()
    {
        if (Tbl.dicCardName.ContainsKey (info))
            return Tbl.dicCardName [info];
        return Tbl.dicCardName ["DEFAULT"];
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKeyValue ("isKicker", isKicker);
        SendStr += AgUtil.IntArrToJson ("direction", direction);
        SendStr += AgUtil.IntArrToJson ("skill", skill);
        SendStr = SendStr.AddKV3 ("limitGameEA", limitGameEA, "cost", cost, "level", level);
        SendStr = SendStr.AddKV3 ("enchant", enchant, "condition", condition, "backNum", backNum);
        SendStr = SendStr.AddKV3 ("kickOrder", kickOrder, "price4trade", price4trade, "ID", ID); // int
        SendStr = SendStr.AddKV3 ("info", info, "grade", grade, "playerName", playerName); // string
        SendStr = SendStr.AddKV3 ("leagueType", leagueType, "tradeState", tradeState, "dirSuccessRatio", dirSuccessRatio); // string
        SendStr = SendStr.AddKeyValue ("scouter", scouter);

        //public string country, position;        public int playerID, look;
        SendStr = SendStr.AddKV2 ("country", country, "position", position);
        SendStr = SendStr.AddKV2 ("playerID", playerID, "look", look);

        SendStr = SendStr.AddKeyValue ("tradeTs", tradeTs, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }

    public bool WasCardParse (JSONNode pJson)
    {
        try { 
            ID = pJson ["ID"].AsInt;
            try {
                string trfa = pJson ["isKicker"];
                ("                   isKicker ::  " + trfa + "   " + trfa.Length).HtLog ();
                if (trfa.Length > 3)
                    isKicker = pJson ["isKicker"].AsBool;
                else
                    isKicker = (pJson ["isKicker"].AsInt == 1) ? true : false;
            } catch {
                Ag.LogIntenseWord (" isKicker is not   Parse - able    ");
            }
            string kickStr = isKicker ? "  Kicker  " : "  Keeper  ";
            try {
                leagueType = (string)pJson ["leagueType"];
            } catch {
                leagueType = "N";
            }

            pJson.ParseTo ("limitGameEA", out limitGameEA, "cost", out cost);
            pJson.ParseTo ("level", out level, "enchant", out enchant, "condition", out condition);
            pJson.ParseTo ("kickOrder", out kickOrder, "backNum", out backNum);

            grade = pJson ["grade"];
            (" ParseFrom   :  ID :  " + ID + " is a " + kickStr + "      G / L : " + grade + ", " +
            level + "      Order : " + kickOrder + "      BN : " + backNum).HtLog ();
            try {
                playerName = (string)pJson ["playerName"];
            } catch {
                playerName = "No name";
            }

            try {
                scouter = (string)pJson ["scouter"];
            } catch {
                Ag.LogDouble ("   Set Scouter String with ::  0_0_0 ...   ");
                scouter = "0_1_0_0_0_0_0_0_0_0_0_0";
            }

            //(" Back Num : " + backNum + "    Player Name :::  " + playerName).HtLog ();
            playerName = playerName.RemoveQuotationMark ();
            try {
                info = pJson ["info"]; // 처음에 없슴.  ""
            } catch {
                info = "";// ("  info is    fucked  . ... " ).HtLog();
            }
            byte[] chatbytes = Encoding.UTF8.GetBytes (playerName);
            //(" Back Num : " + backNum + "    Player Name :::  " + playerName + "  converted >>  " + Convert.ToBase64String (chatbytes) + " info : >> " + info).HtLog ();

            try {
                int dirNum = pJson ["direction"].Count;
                for (int k = 0; k < dirNum; k++) {
                    direction [k] = pJson ["direction"] [k].AsInt;
                }
                int sklNum = pJson ["skill"].Count;
                for (int k = 0; k < sklNum; k++) {
                    skill [k] = pJson ["skill"] [k].AsInt;
                }
                if (DirSklNotSetYet ())
                    InitDirectionAndSkill ();
            } catch {
                InitDirectionAndSkill ();
                Ag.LogString ("  No Direction Info .... contained    __________ >>>> Fill Direction & Skill  Info  <<<< __________ ");
            }

            // playerID : number             - position : string             - country : string            - look : number
            try {
                playerID = pJson ["playerID"].AsInt;
                look = pJson ["look"].AsInt;
                country = pJson ["country"];
                position = pJson ["position"];
            } catch {
                InitDirectionAndSkill ();
                Ag.LogString ("  No ___   Deck   ___    Info .... contained    __________ >>>>   Deck  playerID, position, country, look    <<<< __________ ");
            }

            ShowMySelf ();
            return true;
        } catch {
            Ag.LogIntenseWord ("WasCard  ::  ParseFrom ()   __________ >>>> Catch <<<< __________  Parse      E R R O R   ");
        }



        return false;
    }

    bool DirSklNotSetYet ()
    {
        if (isKicker)
            return TotalWideDirect () < 1000;
        return direction [0] + direction [1] < 100;
    }

    public void ShowMySelf (string pMsg = "   ")
    {
        //Ag.LogIntense (0, true);
        string kickStr = isKicker ? " Kicker " : " >>>  Keeper  <<< ";
        string theLog = " WasCard :: ShowMySelf >> " + kickStr + " Order : " + kickOrder + "  Grade : " + grade + " , level : " + level + " , enchant : " + enchant + pMsg;
        if (isKicker)
            theLog += " Direction Width " + direction [0] + " / " + direction [2] + " / " + direction [4] + " / " + direction [6] +
            " >>>   Direction  Posi " + direction [1] + " / " + direction [3] + " / " + direction [5] + " / " + direction [7];
        else
            theLog += " Keeper Balance " + direction [0] + " / " + direction [1];
        theLog += String.Format ("  Skill ::  {0} / {1} / {2} > ", skill [0], skill [1], skill [2]);
        theLog += ("  ID : " + ID + "   Scouter : " + scouter);
        theLog.HtLog ();

        //("  Scouter >>>  " + ScoutObj.ToString ()).HtLog ();
    }
    //  _////////////////////////////////////////////////_    _____  선 수 정 보  _____    Get Methods   _____
    public int GetValueOfDirection (int PlusLevel)  // deck applied
    { // 방 향 ::  Dir (narrow) + (5 - 등급) * 10
        int theLevel = PlusLevel + level > 10 ? 10 : PlusLevel + level;
        //return GetDirectionWidthOfSmallArea (theLevel) + (5 - GradeValue) * 10;

        Ag.LogDouble ("  Value of Direction ::  >>>   " + DirectWidthOfSmallAreaWidhDeck (theLevel));

        return DirectWidthOfSmallAreaWidhDeck (theLevel) + (5 - GradeValue) * 10;
    }
    //    public int xxGetValueOfExactness ()  //   Deprecated ....
    //    { // 정 확 도 ::  100 - Dir (Miss) - (등급 - 1) * 10
    //        return 100 - DirectMiss () - (GradeValue - 1) * 10;
    //    }
    public int GetValueOfFireOrFresh (int PlusLevel = 0)  // deck applied
    {
        bool nextLevel = PlusLevel == 0 ? false : true;
        return (int)(GetConditionAppliedSkill (0, nextLevel) / 300f * 100f); // in % unit
    }

    public int GetValueOfBlazeOrLightening (int PlusLevel = 0)  // deck applied
    {
        bool nextLevel = PlusLevel == 0 ? false : true;
        if (grade == "S")
            return (int)(GetConditionAppliedSkill (0, nextLevel) / 70f * 100f); // in % unit
        return (int)(GetConditionAppliedSkill (1, nextLevel) / 70f * 100f); // in % unit
    }

    public int GetValueOfVolcano (int PlusLevel = 0)  // deck applied
    {
        bool nextLevel = PlusLevel == 0 ? false : true;
        if (grade == "S")
            return (int)(GetConditionAppliedSkill (1, nextLevel) / 80f * 100f); // in % unit
        return (int)(GetConditionAppliedSkill (2, nextLevel) / 80f * 100f); // in % unit
    }

    public float GetConditionAppliedSkill (int pIdx, bool nextLevel = false)  // deck applied
    {
        float ratio = 1.0f, deck = isKicker ? 1 + 0.01f * DIkickSkl : 1 + 0.01f * DIkeepSkl;
        ratio += condition * 0.1f;
        if (nextLevel) {
            int[] tempSkil = new int[] {
                GetSkillGood (grade, level + 1),
                GetSkillGreat (grade, level + 1),
                GetSkillPerfect (grade, level + 1)
            };
            return tempSkil [pIdx] * ratio * deck;
        }
        //Ag.LogString (" GetConditionAppliedSkill  >>  " + condition + "  return  " + (int)(skill [pIdx] * ratio) );
        return skill [pIdx] * ratio * deck;
    }

    public int GetPointBonus (int PlusLevel)
    { // 가산점. % 
        int percent = 0, added = Tbl.dicGamePointOfCardGrade [level + PlusLevel];
        switch (grade) {
        case "S":
            percent = 40;
            added *= 2;
            break;
        case "A":
            percent = 20;
            added *= 2;
            break;
        case "B":
            percent = 10;
            break;
        case "C":
            percent = 5;
            break;
        }
        return percent + added;
    }

    public int GetValueOfBalance (int plevel)  // deck applied
    {
        plevel = plevel > 10 ? 10 : plevel;
        if (isKicker)
            return -1; // 에러. 키커면 -1 리턴.
        return BalanceValueWithDeckItem (level + plevel);
//        if (direction [0] > direction [1])
//            return direction [1];
//        else
//            return direction [0];
    }
    //  _////////////////////////////////////////////////_    _____  Bonus  _____    Methods   _____
    public int GetValueOfBonus (int plevel)
    {
        plevel = plevel > 10 ? 10 : plevel;
        int rVal = (5 - GradeValue) * 5; // 기 본 값.
        rVal = grade == "S" ? 40 : rVal;
        rVal = grade == "A" ? 20 : rVal;

        int added = Tbl.dicGamePointOfCardGrade [plevel];
        if (grade == "S" || grade == "A")
            added *= 2;

        return rVal + added;
    }
    //  _////////////////////////////////////////////////_    _____  Set  _____    Get Methods   _____
    public void SetZeroToDirectSkill ()
    { // Node Transfer ...  for Error case ...
        direction = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        skill = new int[] { 0, 0, 0 };
    }

    public void GetSkillFinalValue (int shirt16, int pants16, int sock16, int cstmType04, out int good, out int perf, int DirectNum = 0)
    { 
        float rat0 = 1f, rat1 = 1f;
        good = skill [0];
        perf = skill [1];

        //Ag.LogString ("Good :: GetskillFinalValue1 : " + good + "  : " + perf);
        // Uniform
        rat0 += shirt16 == 1 ? 0f : 0.1f;  // 셔츠의 값 1 ~ 6
        rat1 += shirt16 == 1 ? 0f : 0.05f;
        rat0 += pants16 == 1 ? 0f : 0.05f; // 팬츠
        rat1 += pants16 == 1 ? 0f : 0.03f;
        rat0 += sock16 == 1 ? 0f : 0.04f; // 양말.
        rat1 += sock16 == 1 ? 0f : 0.02f;
        //Ag.LogString (rat0.LogWith ("Ratio Good") + rat1.LogWith ("Ratio Perfect"));
        // Costume
        //Ag.LogString ("Good :: GetskillFinalValue  ratio " + rat0 + " / " + rat1 + " Good / Great :: " + good + "  : " + perf);
        rat0 += cstmType04 == 0 ? 0f : 0.1f; // Costume  0 ~ 4 .. No costume => 0
        rat1 += cstmType04 == 0 ? 0f : 0.05f; // 비율을 모두 더한다.
        //Ag.LogString (rat0.LogWith ("Ratio Good") + rat1.LogWith ("Ratio Perfect"));
        // Condition
        //Debug.Log ("Good :: GetskillFinalValue : " + good + "Getskillfinal Perfect : " + perf + "    >>>   Before Condition ");
        rat0 += condition * 0.1f; // 비율을 모두 더한다.
        rat1 += condition * 0.1f;

        //Ag.LogString ("Good :: GetskillFinalValue  ratio " + rat0 + " / " + rat1 + " Good / Great :: " + good + "  : " + perf);
        //Ag.LogString (rat0.LogWith ("Ratio Good") + rat1.LogWith ("Ratio Perfect"));

        if (!isKicker) {
            if (DirectNum > 0) {
                int idx = (DirectNum % 2 == 0) ? 1 : 0;
                float balV = direction [idx] * 0.01f;
                if (direction [idx] < 100) { 
                    rat0 *= 1f - ((100 - direction [idx]) * 0.2f) * 0.01f; // 1/5 만 적용.
                }
                rat1 *= balV;
            }
        }
        // Deck Item apply
        rat0 += isKicker ? DIkickSkl * 0.01f : 0; //rat0;
        rat1 += isKicker ? DIkickSkl * 0.01f : 0; //rat1;

        //Ag.LogString ("Good :: GetskillFinalValue  ratio " + rat0 + " / " + rat1 + " Good / Great :: " + good + "  : " + perf);
        good = (int)(good * rat0); // 비율 한번에 적용.
        perf = (int)(perf * rat1);

        //Ag.LogString ("Good :: GetskillFinalValue 6:  Good / Great ::: " + good + " : " + perf + "  DIKickSkl : " + DIkickSkl);
    }

    public bool IsLeftStrongKeeper ()
    { 
        if (isKicker)
            return false; // 에러. 키퍼만 해당
        int left = direction [0], rigt = direction [1];
        return left > rigt;
    }

    public int GetAbilityDisplay ()
    {
        return (int)(skill [0] / 3.0f - 13);
    }
    //  _////////////////////////////////////////////////_    _____  방 향 기 본 값  _____    Get Methods   _____
    public int DirectMax ()
    {
        return 70 - level; // 60; // 50 + level; // 50 ~ 60
    }

    int DirectMiss () //
    {
        return DirectMax () - GetDirectionWidthOfSmallArea (level);
    }

    bool CheckSmalArea ()
    {
        List<int> smlPosiArr = new List<int> (); 
        for (int k = 0; k < 4; k++) {
            if (direction [k * 2] < 100 && 0 < direction [k * 2 + 1]) {
                smlPosiArr.Add (direction [k * 2 + 1]);
            }
        } // 24, 355, 555

        int minDist = 30 + 50 + level; // compare minimum distance

        if (smlPosiArr.Count == 1)
            return true;
        bool compare01 = Math.Abs (smlPosiArr [0] - smlPosiArr [1]) > minDist;
        if (smlPosiArr.Count == 2)
            return compare01;
        bool compare02 = Math.Abs (smlPosiArr [1] - smlPosiArr [2]) > minDist;
        bool compare03 = Math.Abs (smlPosiArr [0] - smlPosiArr [2]) > minDist;
        return compare01 && compare02 && compare03;
    }

    void AddWideDirect (int pWid, int pDir, bool pLast)
    {
        if (pLast)
            pWid = 1000 - TotalWideDirect ();
        //("  AddWideDirect ::   " + " Direction : " + pDir + "  Width is   " + pWid + "   Last ? " + pLast).HtLog ();
        direction [(pDir - 1) * 2 + 1] = TotalWideDirect (); // position
        direction [(pDir - 1) * 2] = pWid; // width, position
    }

    int TotalWideDirect ()
    {
        int rVal = 0;
        for (int j = 0; j < 4; j++) {
            int val = direction [j * 2];
            if (val > 140)
                rVal += val;
        }
        //Ag.LogString ("Total Wide " + rVal);
        return rVal;
    }
}
