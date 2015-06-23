// [2013:11:26:MOON<Start>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using SimpleJSON;

public class WasEvent
{
    public string eventType, useFlag;
    public DateTime StartDT, EndDT;
    public List<Reward> arrReward = new List<Reward> ();

    public struct Reward
    {
        public string code, value;
    }

    public bool ParseFrom (JSONNode pJson)
    {
        string startDateTime, endDateTime;
        bool rVal = false;
        if (pJson.ParseTo ("eventType", out eventType, "startDateTime", out startDateTime) &&
            pJson.ParseTo ("useFlag", out useFlag, "endDateTime", out endDateTime))
            rVal = true;

        StartDT = startDateTime.ToDateTime ();
        EndDT = endDateTime.ToDateTime ();

        Ag.LogStartWithStr (1, " Event  >>>  " + eventType + "      useFlag : " + useFlag + "     is Effective    ::   From  " + StartDT.ToString () + "  ~  " + EndDT.ToString ());

        if (eventType == "saleItem" || eventType == "invite" || eventType == "quest" || eventType == "attendance" || eventType == "gift") {
            JSONNode rew = pJson ["reward"];
            for (int k = 0; k < rew.Count; k++) {
                JSONNode nObj = rew [k];
                Reward nRew;
                nRew.code = nObj ["code"];
                nRew.value = nObj ["value"];
                arrReward.Add (nRew);

                ("         reward    parsing    >>  code : " + nRew.code + "      value :  " + nRew.value).HtLog ();
            }
        }
        return rVal;
    }
}

public class WasItem
{
    public int ea, applyID;
    public string itemType, itemTypeID, msg, info, limitTime;

    public void ShowMyself ()
    {
        Ag.LogString (string.Format ("   **   [WasItem] : Type={0} \t\t TypeID={1} \t EA={2}", itemType, itemTypeID, ea));
        Ag.LogString (string.Format ("   **   [WasItem] : Msg={0}  applyID={1} info = {2} ", msg, applyID, info));
    }

    public bool ParseEnemyFrom (JSONNode pJson)
    {
        return Parse (pJson);
    }

    bool Parse (JSONNode pJson)
    {
        bool rVal = pJson.ParseTo ("ea", out ea, "applyID", out applyID) &&
                    pJson.ParseTo ("itemType", out itemType, "itemTypeID", out itemTypeID);
        try {
            rVal = (pJson.ParseTo ("msg", out msg) && pJson.ParseTo ("info", out info, "limitTime", out limitTime));
        } catch {
            rVal = false;
        }
        return rVal;
        //Ag.LogString (" Item Parse  ::   " + rVal + "   itemTypeID " + itemTypeID);
    }

    public bool ParseFrom (JSONNode pJson)
    {
        bool rval = Parse (pJson);
        Ag.LogStartWithStr (1, " WasItem : ParseFrom >> " + pJson.ToString ());
        if (itemTypeID == "HeartSpeedUp") {
            Ag.mySelf.HeartCoolTimeResetWithNow ();
            AgStt.CTHeartRecoverFactor = 2f;
        }
        if (itemTypeID == "HeartLimitUp")
            AgStt.CTHeartMaxSeconds = AgStt.CTHeartMaxDoubled;

        return rval;
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKV2 ("ea", ea, "applyID", applyID);
        SendStr = SendStr.AddKV3 ("itemType", itemType, "itemTypeID", itemTypeID, "msg", msg);
        SendStr = SendStr.AddKV2 ("info", info, "limitTime", limitTime, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }
}

public class WasItemPriceObj
{
    public string itemType, itemTypeID, useFlag, itemName, description;
    public int cash, gold, originalCash, originalGold;

    public bool ParseFrom (JSONNode pJson)
    {
        if (pJson.ParseTo ("itemType", out itemType, "itemTypeID", out itemTypeID, "useFlag", out useFlag) &&
            pJson.ParseTo ("itemName", out itemName, "description", out description) &&
            pJson.ParseTo ("cash", out cash, "gold", out gold) &&
            pJson.ParseTo ("originalCash", out originalCash, "originalGold", out originalGold))
            return true;
        return false;
    }

    public void ShowMyself ()
    {
        Ag.LogString (string.Format ("[WasItemPrice] : Type={0} \t\t TypeID={1} \t\t Cash={2}/{3} \t\t Gold={4}/{5} ", 
            itemType, itemTypeID, cash, originalCash, gold, originalGold));
    }

    public int BuyType01 ()
    {
        if (cash == 0 && gold > 0)
            return 1; // gold case ..
        else
            return 0;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  AmItem  _____  Class  _____
public class AmItem
{
    public WasItem WAS = new WasItem ();

    public void ShowMyself ()
    {
        WAS.ShowMyself ();
    }

    public bool ParseFrom (JSONNode pJson)
    {
        return WAS.ParseFrom (pJson);
    }

    public bool ParseEnemyFrom (JSONNode pJson)
    {
        return WAS.ParseEnemyFrom (pJson);
    }

    public void SetVarInBot ()
    {
        switch (WAS.itemTypeID) {
        case "StartMessage":
            WAS.msg = "";
            break;
        case "CeremonyDefault":
            WAS.applyID = 0;
            break;
        }
    }
}

