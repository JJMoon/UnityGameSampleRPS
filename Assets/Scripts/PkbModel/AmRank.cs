using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class WasRank
{
    public int weekScore, totalScore, bestScore, bestScoreRank, bestScoreUserNum, contWinNum, winNum, lossNum, winNumWeek, lossNumWeek, lastWeekRank, thisWeekRank;
    public string userID, league = "-";

    public WasRank ()
    {
    }

    public WasRank (JSONNode jnObj)
    {
        Parse (jnObj);
    }
    // "weekScore":1483,"bestScore":0,"contWinNum":0,"winNumWeek":2,"lossNumWeek":0,"winNum":2,"lossNum":0,"lastWeekRank":216,"thisWeekRank":216},"ticketNum":1,"serviceCode":502} 
    public bool Parse (JSONNode jsNd)
    {
        try {
            return (jsNd.ParseTo ("weekScore", out weekScore, "totalScore", out totalScore, "bestScore", out bestScore) &&
            jsNd.ParseTo ("contWinNum", out contWinNum, "winNum", out winNum, "lossNum", out lossNum) &&
            jsNd.ParseTo ("userID", out userID, "league", out league) &&
            jsNd.ParseTo ("winNumWeek", out winNumWeek, "lossNumWeek", out lossNumWeek) &&
            jsNd.ParseTo ("bestScoreRank", out bestScoreRank, "bestScoreUserNum", out bestScoreUserNum) &&
            jsNd.ParseTo ("lastWeekRank", out lastWeekRank, "thisWeekRank", out thisWeekRank));
        } catch {
            return false;
        }
    }

//    private string ToJsonStrCommon ()
//    {
//        string SendStr = "";
//        SendStr = SendStr.AddKV2 ("userID", userID, "league", league);
//        SendStr = SendStr.AddKV3 ("weekScore", weekScore, "totalScore", totalScore, "bestScore", bestScore);
//        SendStr = SendStr.AddKV3 ("contWinNum", contWinNum, "winNum", winNum, "lossNum", lossNum);
//        SendStr = SendStr.AddKV2 ("winNumWeek", winNumWeek, "lossNumWeek", lossNumWeek);
//        SendStr = SendStr.AddKV2 ("lastWeekRank", lastWeekRank, "thisWeekRank", thisWeekRank, false);
//        SendStr = SendStr.AddParen ();
//        return SendStr;
//    }

    public void ShowMyself ()
    {
        string.Format (" WasRank :: ShowMySelf  :::::  weekScore :< {0} >, totalScore :< {1} >, bestScore :< {2} >, contWinNum :< {3} > ", 
            weekScore, totalScore, bestScore, contWinNum).HtLog ();
    }
}

public class Rank
{
    public int mRank, mCountry, mScore;
    public string mNick;

    public int PlayNum { get { return WAS.winNumWeek + WAS.lossNumWeek; } }

    public WasRank WAS = new WasRank ();

    public Rank ()
    {
    }

    public Rank (int r, int c, int s)
    {
        mRank = r;
        mCountry = c;
        mScore = s;
    }

    public Rank (JSONNode jnObj)
    {
        WAS.Parse (jnObj);
    }

    public void SetAsBot (int league, Rank myRank) // league = 1, 2, .. 5
    {
        int initScore = (5 - league) * 20000;
        //전적 : 내 전적 +7  //승수 : 내 승수 +4
        WAS.winNum = myRank.WAS.winNum + 11 * (6 - league);
        //패수 : 내 패수 +3
        WAS.lossNum = myRank.WAS.lossNum + 7 * (6 - league);
        //승점 : 각 리그 기본점수 + (봇 승수 × 157 × 리그)
        WAS.weekScore = WAS.totalScore = initScore + WAS.winNum * 157 * league;
        // 등 수.
        WAS.thisWeekRank = (int)(myRank.WAS.thisWeekRank * 0.9f - AgUtil.RandomInclude (5, 15));

        if (myRank.WAS.thisWeekRank < 100) {
            WAS.winNum = (int)(myRank.WAS.winNum * 0.79 - AgUtil.RandomInclude (2, 12));
            WAS.lossNum = (int)(myRank.WAS.lossNum * 0.81 + AgUtil.RandomInclude (3, 10));
            WAS.weekScore = (int)(myRank.WAS.weekScore * 0.81 - AgUtil.RandomInclude (4000, 12000));
            WAS.thisWeekRank = (int)(myRank.WAS.thisWeekRank * 1.25) + AgUtil.RandomInclude (10, 40);
        }
    }

    public void ShowMySelf ()
    {
        Ag.LogString (">>>>>>>>>>>>>>>>>>>>>>>> Rank : " + mRank + "     Country : " + mCountry + "     Score : " + mScore);
    }
}