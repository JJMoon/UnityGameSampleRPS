using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Scout  _____  Class  _____
public class Scouter
{
    public List<SctCounter> arrCount = new List<SctCounter> ();

    public void AddValue (int pDir, bool pSuccess)
    {
        //int idx = pDir / 2; // == 0 ? pDir / 2 : pDir / 2 + 1;
        arrCount [pDir].Increse (pSuccess);
    }

    /// <summary>
    /// Total Game number of the direction.
    /// </summary>
    /// <returns>The sum of success & failure.</returns>
    public int GameNumberOfDirect (int pDir)
    {
        return arrCount [pDir].GetNumber;
    }
    
    /// <summary>
    /// 스카우터의 값을 입력. 넣으려는 방향, 성공한 개수, 실패한 개수
    /// </summary>
    public void SetValue (int pDir, int Snum, int Fnum)
    {
        arrCount [pDir].Scss = Snum;
        arrCount [pDir].Fail = Fnum;
    }
    // Sub Class ... 자료를 담는 섭 클래스
    //  _////////////////////////////////////////////////_    _____  Class  _____   Scouter Counter Object   _____ Start
    public class SctCounter
    {
        public int Scss, Fail;

        public int GetNumber { get { return Scss + Fail; } }

        public string AsString { get { return (Scss + "_" + Fail); } }
        // 생성할 때는 꼭 초기값을 주도록 함.
        public SctCounter (int S, int F)
        {
            Scss = S;
            Fail = F;
        }
        // 값을 하나 올릴 때 호출. true = 성공..
        public void Increse (bool success)
        {
            if (success)
                Scss++;
            else
                Fail++;
        }
    }
    //  _////////////////////////////////////////////////_    _____  Class  _____   Scouter Counter Object   _____ End
    public Scouter (bool isKicker = true)
    {
        SetVoidCount ();
    }

    public Scouter (JSONNode pJson)
    {
        if (AgUtil.IsNullJson (pJson)) {
            SetVoidCount ();
            return;
        }
        StackOfInt stObj = new StackOfInt (pJson);
        for (int k = 0; k < 6; k++) {
            arrCount.Add (new SctCounter (stObj.PopHeadInt (), stObj.PopHeadInt ()));
        }
    }
    // 처음에 6방향 (똥, 1~4, 파넨카)을 0으로 세팅
    void SetVoidCount ()
    {
        for (int k = 0; k < 6; k++) {
            arrCount.Add (new SctCounter (0, 0));
        }
    }

    public string GetString ()
    {
        string rVal = "";
        for (int k = 0; k < arrCount.Count; k++) {
            rVal += arrCount [k].AsString;
            rVal += "_";
        }
        return rVal.RemoveTail ();
    }
    //
    public override string ToString ()
    {
        return string.Format ("[Scouter] Dir 0 : <{0}>,     ~~   < {1}  _  {2}  _  {3}  _  {4}  ____ {5} > ", 
            GameNumberOfDirect (0), GameNumberOfDirect (1), GameNumberOfDirect (2), GameNumberOfDirect (3), GameNumberOfDirect (4), GameNumberOfDirect (5));
    }
}
//  _////////////////////////////////////////////////_    _____   Card   _____   Class   _____
public partial class AmCard : AmObject
{
    public Scouter ScoutObj;

    public void SetGradeLevelWithNewDirection (string pGrade, int pLevel)
    {
        WAS.grade = pGrade;
        WAS.level = pLevel;

        WAS.InitDirectionAndSkill ();
    }

    public void SetBotPlayerInfo ()
    {
        int botCNum = WAS.isKicker ? Tbl.arrKickBotCard.Count : Tbl.arrKeepBotCard.Count;
        Ag.LogString (" Bot Player Info  ::   arr Num ? >>   " + botCNum);
        WasCard curCrd = WAS.isKicker ? Tbl.arrKickBotCard [AgUtil.RandomInclude (0, botCNum - 1)] : Tbl.arrKeepBotCard [AgUtil.RandomInclude (0, botCNum - 1)];
        WAS.country = curCrd.country;
        WAS.look = curCrd.look;
        //  JKLeeMustFinishThis
    }

    public void SetBotScouter ()
    {
        if (!WAS.isKicker) {
            ScoutObj = new Scouter ("3_4_11_13_14_22_15_16_11_12_18_21");
            return;
        }

        ScoutObj = new Scouter ();
        for (int k = 1; k <= 4; k++) {
            int wid = WAS.GetWidthOfDirection (k);
            int s = wid * 5 / AgUtil.RandomInclude (50, 98);
            int f = wid * 4 / AgUtil.RandomInclude (40, 78);
            s = s.GetSmaller (AgUtil.RandomInclude (3, 15)); 
            f = f.GetSmaller (AgUtil.RandomInclude (3, 15)); 
            //(k + "    wid " + wid + "   s / f " + s + ", " + f).HtLog ();
            ScoutObj.SetValue (k, s, f);
        }
        ScoutObj.GetString ().HtLog ();
    }

    public byte SetKeeperDirect (int pBotGrade)
    {
        // Bot Grade  ::  -1, 0, 
        if (!Ag.mgIsKick)
            return 0;
        byte rslt = 0; // = mDirectObj.PickWideAndNarrowRight ();
        switch (pBotGrade) {
        case 0:
            rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // 넓은 방향만 동일한 확률로 킥
            break;
        case 1:
        case 2:
            rslt = mDirectObj.MaxDirect (is2ndCase: true); // 상대의 두번째 넓은 방향으로 점프
            break;
        case 3:
        case 4:
            rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // 상대의 넓은 방향만 동일한 확률로 점프
            break;
        }
        return rslt;
    }

    public byte SetKickerDirect (int pBotGrade)
    {
        if (Ag.mgIsKick)
            return 0;
        byte rslt = 0;

        //Ag.LogString(" AmCardAI :: SetKickerDirect (byte pBotGrade)    isKicket ? " + WAS.isKicker);
        //mDirectObj.ShowMyself();

        switch (pBotGrade) {
        case 0:
            rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // 넓은 방향만 동일한 확률 Jump
            break;
        case 1:
        case 2:
            //rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // Just pick from wide area..'
            rslt = mDirectObj.MaxDirect (); // 가장 넓은 방향으로 킥
            break;
        case 3:
        case 4:
            rslt = mDirectObj.PickRandomKick (); // 랜덤 방향으로 킥
            break;
        }
        return rslt;
    }
}
