using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasCard  _____  Class  _____
public partial class WasCard
{
    /// <summary>
    /// 최초 값 세팅
    /// </summary>
    public void InitDirectionAndSkill ()
    {
        mustUpdate = true;
        SetSkillValue ();

        if (!isKicker) { // keeper setting.
            direction [0] = direction [1] = 100;
            direction [2] = direction [3] = direction [4] = 0;
            direction [AgUtil.RandomInclude (10, 500) % 2] = CalculateBalance (level); // 0 , 1 ..
            return;
        }

        Ag.LogStartWithStr (1, " WasCard :: ...   SetDirection   ....   isKicker ::  " + isKicker + " Start      .... ID :  " + ID);

        kickOrder = -1;
        int wideN = 1;

        switch (grade) {
        case "S":
        case "A":
            wideN = 3;
            break;
        case "B":
            wideN = 2;
            break;
        case "C":
            wideN = 2;
            break;
        case "D":
            break;
        }
        //Ag.LogString (" Is Grade S ?? " + tempS + "   Current Grade :: " + grade);
        int width = GetDirectionWidthOfSmallArea (level); // 34 .. 38 ..
        // 방향바 0, 2, 4, 6 을 좁은 영역으로 세팅
        for (int j = 0; j < 4; j++) {
            direction [j * 2] = width;
        }
        // SetWideDirection 조합으로 넓은 방향 선택. 
        List<int> wideDirArr = AgUtil.CombiSelect (4, wideN); // (0, 2, 3)
        // in Case of B (different 2 dir), C (same 2 dir)
        if (grade == "C") {
            int dir = wideDirArr [0], dir2;
            if (dir < 2)
                dir2 = dir + 2;
            else
                dir2 = dir - 2;
            wideDirArr.Clear ();
            wideDirArr.Add (dir);
            wideDirArr.Add (dir2);
        }
        if (grade == "B") {
            int dir = wideDirArr [0], dir2, ran = AgUtil.RandomInclude (0, 100) % 2; // ran = 0 or 1
            if (dir % 2 == 0) // 0, 2
                dir2 = ran * 2 + 1; // 1, 3
            else // 1, 3
                dir2 = ran * 2; // 0, 2
            wideDirArr.Clear ();
            wideDirArr.Add (dir);
            wideDirArr.Add (dir2);
        }
        for (int j = 0; j < wideN; j++) {
            int dir = wideDirArr [j] + 1;
            if (wideN == 1) {
                AddWideDirect (pWid: 1000, pDir: dir, pLast: false);
                break;
            }
            //if (j == 0) {
            int max = wideN > 2 ? 400 : 500;
            max = wideN == 4 ? 300 : max;
            //("wideN  :  " + wideN + "    max  " + max).HtLog ();
            AddWideDirect (pWid: AgUtil.RandomInclude (152, max), pDir: dir, pLast: j == wideN - 1);
            //continue;
            //}
            //int wid = 1000 / wideN;
            //AddWideDirect (pWid: AgUtil.RandomInclude (wid - 100, wid + 100), pDir: dir, pLast: j == wideN - 1);
        }

        // Set Small Direction Position
        List<int> smlDirArr = new List<int> ();
        smlDirArr.Add (0);
        smlDirArr.Add (1);
        smlDirArr.Add (2);
        smlDirArr.Add (3);
        foreach (int k in wideDirArr) {
            //Ag.LogString ("  Small Direct is " + k);
            smlDirArr.Remove (k);
        }
        foreach (int sV in smlDirArr) {
            AddSmlDirect (sV + 1);
        }
    }

    /// <summary>
    /// Sub Methods ...  Add direction [idx] with Random [wide] width..
    /// </summary>
    /// <param name="pDir">P dir.</param>
    void AddSmlDirect (int pDir)
    {
        int idx = (pDir - 1) * 2 + 1; // 1, 3, 5, 7  Width 
        do {
            direction [idx] = AgUtil.RandomInclude (50, 650);
        } while (!CheckSmalArea ());
    }

    /// <summary>
    /// skill 값 세팅 { 222, 22, 0 }
    /// </summary>
    void SetSkillValue ()
    {
        int fire = GetSkillGood (grade, level), blaze = GetSkillGreat (grade, level), volcano = GetSkillPerfect (grade, level);
        if (skill [0] < fire || skill [1] < blaze)
            mustUpdate = true;

        Ag.LogDouble ("   Set Skill Value ::  " + fire + " / " + blaze + " / " + volcano);

        if (!isKicker) {  // Keeper case
            if (grade == "S")
                blaze = 0;  // [ 283, 0, 0 ]
            if (grade == "A")
                volcano = 0; // [ 255, 27, 0 ]
        }


        skill = new int[] { fire, blaze, volcano };
    }

    /// <summary>
    /// 좁은 영역 / 밸런스 레벨업 시 변경. 스킬도 변경.
    /// </summary>
    public void ResetWidthAndSkill ()  //_    _____  방향값  레벨업 시 변경  _____    Get Methods   _____
    {
        mustUpdate = true;
        if (isKicker) {
            int width = GetDirectionWidthOfSmallArea (level);
            for (int k = 0; k < 4; k++) {
                if (direction [k * 2] < 150) {  // reset all small Width
                    direction [k * 2] = width;
                }
            }
        } else {
            if (direction [0] == 100)
                direction [1] = CalculateBalance (level);
            else {
                if (direction [1] == 100)
                    direction [0] = CalculateBalance (level);
                else { // 에러 케이스.. 
                    direction [0] = CalculateBalance (level);
                    direction [1] = 100;
                }
            }
        }
        SetSkillValue ();
    }
    //  _////////////////////////////////////////////////_    _____  Base  _____    <Good:Great:Perfect> 영역 단순 계산    _____
    /// <summary>
    /// ''3:Perfect'' 영역 단순 계산
    /// </summary>
    /// <returns>0 ~ ??  </returns>
    /// <param name="pGrd">Grade</param>
    /// <param name="pLevel">Level</param>
    int GetSkillPerfect (string pGrd, int pLevel)
    {
        pLevel = pLevel > 10 ? 10 : pLevel;
        if (!isKicker)
            return 0;
        if (pGrd == "A")
            return (level > 5) ? 7 + (level - 5) * 3 : 0;  // [ 273, 46, 10 ]
        return 0;
    }

    /// <summary>
    /// ''2:Great'' 영역 단순 계산
    /// </summary>
    /// <returns> 10 ~ ~  </returns>
    /// <param name="pGrd">Grade</param>
    /// <param name="pLevel">Level</param>
    int GetSkillGreat (string pGrd, int pLevel)
    {
        pLevel = pLevel > 10 ? 10 : pLevel;
        int blaze = isKicker ? 10 : 15, blazeStep = 3; //  "D" 
        switch (pGrd) {
        case "S":
            blaze = isKicker ? 22 : 0;
            blazeStep = 4;
            break;
        case "A":
            blaze = isKicker ? 22 : 27;
            blazeStep = 4;
            break;
        case "B":
            blaze = isKicker ? 15 : 20;
            break;
        case "C":
            blaze = isKicker ? 15 : 20;
            break;
        }
        blaze += pLevel * blazeStep;
        return blaze;
    }

    /// <summary>
    /// ''1:Good'' 영역 단순 계산
    /// </summary>
    /// <returns> 199 ~ ~  </returns>
    /// <param name="pGrd">Grade</param>
    /// <param name="pLevel">Level</param>
    int GetSkillGood (string pGrd, int pLevel)
    {
        pLevel = pLevel > 10 ? 10 : pLevel;
        int fire = 271; //171;
        switch (pGrd) {
        case "S":
            fire = 383; // 283; //170;
            break;
        case "A":
            fire = 355; //255; //152;
            break;
        case "B":
            fire = 327; //227; //137;
            break;
        case "C":
            fire = 299; //199; //119;
            break;
        }
        fire += pLevel * 3;
        return fire;
    }
    //  _////////////////////////////////////////////////_    _____  Deck Buff  _____    덱 적용된 기본값 계산    _____
    /// <summary>
    /// 덱 버프 적용 후 밸런스 값 계산
    /// </summary>
    /// <returns>증가된 밸런스.</returns>
    /// <param name="pLevel">Level.</param>
    public int BalanceValueWithDeckItem (int pLevel)
    {
        int bal = CalculateBalance (pLevel);
        int diff = 100 - bal;
        diff = (int)(0.01f * DIkeepBal * diff);
        Ag.LogDouble (" BalanceValueWithDeckItem  >>>   " + bal + "   DiKeepBal :  "
        + DIkeepBal + "   After Deck  " + (bal + diff));
        return bal + diff;
    }

    /// <summary>
    /// 덱 버프 적용 후 넓어진 좁은 방향바 폭
    /// </summary>
    public int DirectWidthOfSmallAreaWidhDeck (int pLevel)
    {
        int width = GetDirectionWidthOfSmallArea (pLevel);
        //Ag.LogString (" width  " + width + "    DI : " + DIkickDir);
        width = (int)((1f + 0.01f * DIkickDir) * width);
        //Ag.LogString (" after ....  width  " + width);
        return width;
    }

    public int BuffDirectWidthDeck (int pWidth)
    {
        return (int)((1f + 0.01f * DIkickDir) * pWidth);
        //Ag.LogString (" after ....  width  " + width);
    }

    //  _////////////////////////////////////////////////_    _____  Base  _____    기본값 계산    _____
    /// <summary>
    /// 레벨에 따른 밸런스값
    /// </summary>
    int CalculateBalance (int pLevel) // 키퍼
    {
        int keepBal = 30, levelStep = 2;
        switch (grade) {
        case "S":
            keepBal = 70;
            levelStep = 3;
            break;
        case "A":
            keepBal = 60;
            levelStep = 3;
            break;
        case "B":
            keepBal = 50;
            break;
        case "C":
            keepBal = 40;
            break;
        case "D":
            break;
        }
        keepBal += pLevel * levelStep;
        return keepBal;
    }

    /// <summary>
    /// 레벨에 따른 좁은 방향바 폭
    /// </summary>
    int GetDirectionWidthOfSmallArea (int pLevel)
    {
        int width = 0;
        switch (grade) {
        case "S":
        case "A":
        case "B":
            width = 38;
            break;
        case "C":
        case "D":
            width = 34;
            break;
        }
        width += pLevel * 2;

        return width;
    }
}
