// [2013:11:25:MOON<Start>]
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using SimpleJSON;

public class WasPopupPurchase : WasObject
{
    public string PopupCode;

    public override void SendAction ()
    {
        Ag.LogString ("WasPopupPurchase :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 602);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("popupCode", PopupCode, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPopupPurchase :: postAction " + RcvdStr);

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
        Ag.LogString ("WasPopupPurchase :: CatchAction ...   ");

    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasPopupStoreList  _____  Class  _____
public class WasPopupStoreIAPurchaseList : WasObject
{
    public override void SendAction ()
    {
        // popupList":[{"popupCode":"test.code","rewardCash":230,"arrItem":[{"itemTypeId":"CardCombiAdvt","count":3},
        // {"itemTypeId":"CardCombiAdvtHigh","count":2}]},
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 611);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        SendStr = SendStr.AddParen ();
        postAction += () => {
            //Ag.LogStartWithStr (3, "WasPopupStoreList :: postAction " + RcvdStr);
            JSONNode jdArr = NdObj ["popupList"];
            Ag.LogString ("Parsing Started          >>>>>     WasEventList ::   Number >>  " + jdArr.Count + "  EA  ");
            for (int j = 0; j < jdArr.Count; j++) {
                AmPopupProdIAP aObj = new AmPopupProdIAP ();
                aObj.Parse (jdArr [j]);
                User.arrPopupIAPItem.Add (aObj);
            }
            Ag.LogString ("WasPopupStoreIAPurchaseList :: postAction " + Result.result + "   arrEvent : " + User.arrEvent.Count);
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
        Ag.LogString ("WasPopupStoreIAPurchaseList :: CatchAction ...   ");

    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasPopupStoreList  _____  Class  _____
public class WasPopupStoreList : WasObject
{
    public bool DiscountOnly;

    public override void SendAction ()
    {
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 601);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddParen ();
        postAction += () => {
            //Ag.LogStartWithStr (3, "WasPopupStoreList :: postAction " + RcvdStr);
            JSONNode jdArr = NdObj ["popupList"];
            Ag.LogString ("Parsing Started          >>>>>     WasEventList ::   Number >>  " + jdArr.Count + "  EA  ");
            for (int j = 0; j < jdArr.Count; j++) {
                AmPopupProd aObj = new AmPopupProd ();
                aObj.ParseFrom (jdArr [j]);
                aObj.WAS.ShowMyself ();
                User.arrPopupItem.Add (aObj);
            }
            Ag.LogString ("WasPopupStoreList :: postAction " + Result.result + "   arrEvent : " + User.arrEvent.Count);
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
        Ag.LogString ("WasEventList :: CatchAction ...   ");

    }
}

public class WasEventList : WasObject
{
    public bool DiscountOnly;

    public override void SendAction ()
    {
        //User.arrItemPrice.Clear ();
        Ag.LogString ("WasEventList :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 243);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        SendStr = SendStr.AddParen ();
        postAction += () => {
            Ag.LogString ("WasEventList :: postAction " + RcvdStr);
            JSONNode jdArr = NdObj ["arrEventInfo"];
            Ag.LogString ("WasEventList ::   Number >>  " + jdArr.Count);
            for (int j = 0; j < jdArr.Count; j++) {
                WasEvent aObj = new WasEvent ();
                aObj.ParseFrom (jdArr [j]);
                //aObj.ShowMyself ();
                User.arrEvent.Add (aObj);
            }
            Ag.LogString ("WasEventList :: postAction " + Result.result + "   arrEvent : " + User.arrEvent.Count);
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
        Ag.LogString ("WasEventList :: CatchAction ...   ");

    }
}

public class WasItemPrice : WasObject
{
    //public AmUser usr;
    public bool DiscountOnly;
    // 0 : cash, 1 : gold
    public override void SendAction ()
    {
        User.arrItemPrice.Clear ();
        Ag.LogString ("WasItemPrice :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 239);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        if (DiscountOnly)
            SendStr = SendStr.AddKeyValue ("discountFlag", 1, false);
        else
            SendStr = SendStr.AddKeyValue ("discountFlag", 0, false);

        SendStr = SendStr.AddParen ();
        postAction += () => {
            Ag.LogString ("WasItemPrice :: postAction " + RcvdStr);
            //JsonData jsItems = JData ["arrItem"];
            JSONNode jsItems = NdObj ["arrItem"];
            Ag.LogString ("WasItemPrice :: " + jsItems.Count);
            for (int j = 0; j < jsItems.Count; j++) {
                WasItemPriceObj aObj = new WasItemPriceObj (); // JsonMapper.ToObject<WasItemPriceObj> (jsItems [j].ToJson ()); 
                aObj.ParseFrom (jsItems [j]);
                aObj.ShowMyself ();
                User.arrItemPrice.Add (aObj);
            }
            Ag.LogString ("WasItemPrice :: postAction " + Result.result + "   arrItemPrice : " + User.arrItemPrice.Count);
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
        Ag.LogString ("WasItemPrice :: CatchAction ...   ");

    }
}

public class WasPurchaseHeart : WasObject
{
    public int eaN;
    // 0 : cash, 1 : gold
    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseHeart :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 234);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("eaNum", eaN);

        int bT = User.GetBuyType ("Heart" + eaN);

        SendStr = SendStr.AddKeyValue ("buyType", bT, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseHeart :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            //Ag.LogString ("WasPurchaseHeart :: postAction " + Result.result);
            //Ag.LogString (((string)(jsUInfo ["cash1"])).LogWith ("cash1") + ((string)(jsUInfo ["cash2"])).LogWith ("cash2") + ((string)(jsUInfo ["gold"])).LogWith ("gold"));
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
        Ag.LogString ("WasPurchaseHeart :: CatchAction ...   ");

    }
}

public class WasPurchaseCostume : WasObject
{
    public string costumeName;
    //public int buyType;
    // 0 : cash, 1 : gold
    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseCostume :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 222);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("itemTypeID", costumeName);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType (costumeName), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseCostume :: postAction " + RcvdStr);
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
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
        Ag.LogString ("WasPurchaseCostume :: CatchAction ...   ");
    }
}

public class WasHeartFillMax : WasObject
{
    public string TypeID;
    //public int buyType;    // 0 : cash, 1 : gold
    public override void SendAction ()
    {
        Ag.LogString ("WasHeartFillMax :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 260);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncHeartMax"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasHeartFillMax :: postAction " + RcvdStr);

            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasHeartFillMax :: postAction " + Result.result);
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
        Ag.LogString ("WasHeartFillMax :: CatchAction ...   ");
    }
}

public class WasPurchaseUniform : WasObject
{
    // 221
    public string uniformTypeID;
    //public int buyType;    // 0 : cash, 1 : gold
    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseUniform :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 221);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("itemTypeID", uniformTypeID);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType (uniformTypeID), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseUniform :: postAction " + RcvdStr);

            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasPurchaseGold :: postAction " + Result.result);
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
        Ag.LogString ("WasPurchaseUniform :: CatchAction ...   ");
    }
}

public class WasPurchaseGold : WasObject
{
    // 210
    public int Gold;

    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseGold :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 210);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("gold", Gold, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseGold :: postAction " + RcvdStr);
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
        Ag.LogString ("WasPurchaseGold :: CatchAction ...   ");
    }
}