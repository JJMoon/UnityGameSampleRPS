// [2013:11:25:MOON<Start>]
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
//using LitJson;
using SimpleJSON;


//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasPurchaseGold  _____  Class  _____
public class WasReceiveContWinItems : WasObject
{
    public override void SendAction ()
    {
        Ag.LogString ("WasReceiveContWinItems :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 605); // AddKeyValue ("serviceCode", 605);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasReceiveContWinItems :: postAction " + RcvdStr);
           
            messageAction (Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasReceiveContWinItems :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasPurchaseGold  _____  Class  _____
public class WasGameStart : WasObject
{
    public string enemyID;
    public int friendGame, contWinMyFlag = 1, contWinEnemFlag = 1;
    public int[] arrCardId, arrayEnemyId;
    // 0 - 성공. -1: 게임 세션키 생성 실패. 1: 글러브 부족, 2: 카톡 갱신 실패, 3: 글러브 갱신 실패, 4: 카드중에 LimitGameEA 부족한 카드가 있음
    public override void SendAction ()
    {
        Ag.LogString ("WasGameStart :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 501); // AddKeyValue ("serviceCode", 501);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);

        SendStr = SendStr.AddKeyValue ("enemyID", enemyID);
        SendStr = SendStr.AddKeyValue ("contWinMyFlag", contWinMyFlag);
        SendStr = SendStr.AddKeyValue ("contWinEnemyFlag", contWinEnemFlag);

        SendStr = SendStr.AddArray ("arrayCardId", GetIntArray (arrCardId));
        SendStr = SendStr.AddArray ("arrayEnemyCardId", GetIntArray (arrayEnemyId));

        SendStr = SendStr.AddKeyValue ("friendGame", friendGame, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasGameStart :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            //if (Result.result == 0 || )
            try {
                User.WAS.GameSessionKey = NdObj ["gameSessionKey"];
            } catch {
                " gameSessionKey   parsing    failure   ".HtLog ();
            }
            // 0 - 성공. -1:게임 세션키 생성 실패. 1: 글러브 부족, 2:카톡 갱신 실패, 3:글러브 갱신 실패, 4: 카드중에 LimitGameEA 부족한 카드가 있음
            Ag.LogString ("WasGameStart :: postAction   result : " + Result.result + "     Session Key : " + User.WAS.GameSessionKey);
            messageAction (Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasGameStart :: CatchAction ...   ");
    }
}

public class WasGameReport : WasObject
{
    public string winnerID, loserID;
    public int winPo, losPo;
    public int rTicketNum;


    public override void SendAction ()
    {
        //AgStt.IsGaming = null;

        Ag.LogString ("WasGameReport :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 502); // AddKeyValue ("serviceCode", 502);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("gameSessionKey", User.WAS.GameSessionKey);
        SendStr = SendStr.AddKeyValue ("winner", winnerID);
        SendStr = SendStr.AddKeyValue ("loser", loserID);
        SendStr = SendStr.AddKeyValue ("winPoint", winPo);
        SendStr = SendStr.AddKeyValue ("losePoint", losPo, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasGameReport :: postAction " + RcvdStr);

            try {
                rTicketNum = NdObj ["ticketNum"].AsInt;
                Ag.mySelf.mRewardGold = NdObj ["gold"].AsInt;
            } catch {
                " TicketNum   Num    0   ".HtLog ();
            }
            if (Result.result == 0) {
                if (!User.myRank.WAS.Parse(NdObj["myRank"]))
                    Ag.LogIntenseWord ("GameReport..   Parse myRank  ERROR   ");
                //usr.mRankObj.WAS = JsonMapper.ToObject<WasRank>(jsUInfo["myRank"].ToJson());
            }

            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasGameReport :: postAction   result : " + Result.result + "     ticket : " + rTicketNum  + "   RewardGold  " + Ag.mySelf.mRewardGold );
            messageAction (Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasGameReport :: CatchAction ...   ");
    }
}
