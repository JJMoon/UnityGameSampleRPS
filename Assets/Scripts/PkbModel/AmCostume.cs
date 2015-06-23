// [2013:11:26:MOON<Start>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;
using LitJson;
using SimpleJSON;
//  _////////////////////////////////////////////////_    _____   Costume   _____   Class   _____
public class WasCostume
{
    // Client Edit : cardId, etcInfo..
    public int costumeId, cardId;
    public string itemTypeId, etcInfo;

    public WasCostume ()
    {
    }

    public WasCostume (JSONNode pJson)
    {
        ParseFrom (pJson);
    }

    public bool ParseFrom (JSONNode pJson)
    {
        if (pJson.ParseTo ("costumeId", out costumeId, "cardId", out cardId) &&
            pJson.ParseTo ("itemTypeId", out itemTypeId, "etcInfo", out etcInfo))
            return true;
        return false;
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKV2 ("costumeId", costumeId, "cardId", cardId);
        SendStr = SendStr.AddKV2 ("itemTypeId", itemTypeId, "etcInfo", etcInfo, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }
}

public class AmCostume
{
    public WasCostume WAS = new WasCostume ();

    public AmCostume ()
    {
    }

    public AmCostume (JSONNode pJson)
    {
        try {
            ParseFrom (pJson);
        } catch {
            Ag.LogIntenseWord (" AmCostume :: AmCostume ()    ____ E R R O R   in  Parsing   ..  ..   catch   .....   ");
        }
    }

    public bool ParseFrom (JSONNode pJson)
    {
        if (pJson ["etcInfo"].ToString ().IsJsonNull ())
            pJson ["etcInfo"] = " Info not set ";

        return WAS.ParseFrom (pJson);
    }

    public void ShowMySelf (string pMsg = "   ")
    {
        (" WasCostume :: ShowMySelf >>  ID :" + WAS.costumeId + " Card ID : " + WAS.cardId + "  itemTypeId " + WAS.itemTypeId + " , etcInfo : " + WAS.etcInfo).HtLog ();
    }
}