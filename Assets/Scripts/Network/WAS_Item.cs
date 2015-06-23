using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

//using LitJson;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Was Item Related  _____  Class  _____
public class WasFuncCostUp : WasObject
{
    //    public AmUser usr;
    public int cardID, backNum;
    public string playerName;

    public override void SendAction ()
    {
        Ag.LogString ("WasFuncCostUpdate :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 238);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        int curCostupLevel = User.GetCurCostUpLevel ();
        int buyT = User.GetBuyType ("FuncCostUp" + curCostupLevel);
        (" WasFuncCostUp ::    CurCostupLevel : " + curCostupLevel + "       buyType is   >>>   " + buyT).HtLog ();
        SendStr = SendStr.AddKeyValue ("buyType", buyT, false);
        SendStr = SendStr.AddParen ();
        postAction += () => {
            Ag.LogString ("WasFuncCostUpdate :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasFuncCostUpdate :: postAction " + Result.result);
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
        Ag.LogString ("WasFuncCostUpdate :: CatchAction ...   ");
    }
}

public class WasFuncBackNumEdit : WasObject
{
    //    public AmUser usr;
    public int cardID, backNum;
    public string playerName;

    public override void SendAction ()
    {
        Ag.LogString ("WasFuncBackNumEdit :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 237);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("cardID", cardID);
        SendStr = SendStr.AddKeyValue ("backNum", backNum);
        //SendStr = SendStr.AddKeyValue ("buyType", buyType);
        SendStr = SendStr.AddKeyValue ("playerName", playerName);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncBackNumEdit"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasFuncBackNumEdit :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasFuncBackNumEdit :: postAction " + Result.result);
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
        Ag.LogString ("WasFuncBackNumEdit :: CatchAction ...   ");
    }
}

public class WasFuncTeamEdit : WasObject
{
    //    public AmUser usr;
    public string nuTeamName;
    public int mCountry;

    public override void SendAction ()
    {
        Ag.LogString ("WasFuncTeamEdit :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 236);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("teamName", nuTeamName);
        SendStr = SendStr.AddKeyValue ("country", mCountry);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncTeamNameEdit"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasFuncTeamEdit :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
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
        Ag.LogString ("WasFuncTeamEdit :: CatchAction ...   ");
    }
}

public class WasFuncInitRank : WasObject
{
    public override void SendAction ()
    {
        Ag.LogString ("WasFuncInitRank :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 235);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncResetRank"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasFuncInitRank :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasFuncInitRank :: postAction " + Result.result);
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
        Ag.LogString ("WasFuncInitRank :: CatchAction ...   ");
    }
}

public class WasItemUpdate : WasObject
{
    //    public AmUser usr;
    public AmItem itemObj;

    public override void SendAction ()
    {
        Ag.LogString ("WasItemUpdate :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 232);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddArray ("item", itemObj.WAS.ToJsonStr ()); // JsonMapper.ToJson (itemObj.WAS), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasItemUpdate :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasItemUse :: postAction " + Result.result);
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
        Ag.LogString ("WasItemUpdate :: CatchAction ...   ");
    }
}

public class WasItemUse : WasObject
{
    //    public AmUser usr;
    public string itemType, itemTypeId;

    public override void SendAction ()
    {
        Ag.LogString ("WasItemUse :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 231);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("itemType", itemType);
        SendStr = SendStr.AddKeyValue ("itemTypeId", itemTypeId, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasItemUse :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasItemUse :: postAction " + Result.result);
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
        Ag.LogString ("WasPurchaseItem :: CatchAction ...   ");
    }
}

public class WasPurchaseItem : WasObject
{
    //    public AmUser usr;
    public string itemType, itemTypeId;
    public int ea;

    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseItem :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 230);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("itemType", itemType);
        SendStr = SendStr.AddKeyValue ("itemTypeId", itemTypeId);
        SendStr = SendStr.AddKeyValue ("eaNum", ea);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType (itemTypeId), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseItem :: postAction " + RcvdStr);

            // 0:성공, -1:코인 부족. 1:이미 보유한 아이템, -2:결재 수단 에러. 2:구매 갯수 에러(0보다 커야 함), 3:존재 하지 않는 아이템 타입
            Ag.LogString ("WasPurchaseCostume :: postAction " + Result.result);
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
        Ag.LogString ("WasPurchaseItem :: CatchAction ...   ");
    }
}