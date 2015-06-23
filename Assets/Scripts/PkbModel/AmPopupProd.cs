using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

public class PopupWas
{
    public string popupCode, imgURL, itemTypeID;
    public int EA, buyType, price;

    public bool ParseIapFrom (JSONNode pJson)
    {
        //  {"itemTypeId":"CardCombiAdvt","count":3}
        if (pJson.ParseTo ("itemTypeID", out itemTypeID) &&
            pJson.ParseTo ("count", out EA))
            return true;
        return false;
    }

    public bool ParseFrom (JSONNode pJson)
    {
        if (pJson.ParseTo ("popupCode", out popupCode, "imgURL", out imgURL, "itemTypeID", out itemTypeID) &&
            pJson.ParseTo ("EA", out EA, "buyType", out buyType, "price", out price))
            return true;
        return false;
    }

    public void ShowMyself ()
    {
        Ag.LogString (string.Format ("[PopupWas] : Code={0} \t URL={1} \t item={2}  \t EA={3} \t buyType={4}  \t  price ={5}  typeID ={6}", 
            popupCode, imgURL, itemTypeID, EA, buyType, price, itemTypeID));
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  AmPopupProd  _____


public class AmPopupProdIAP : AmObject
{
    // "popupList":[{"popupCode":"test.code","rewardCash":230,"arrItem":[{"itemTypeId":"CardCombiAdvt","count":3},
    // {"itemTypeId":"CardCombiAdvtHigh","count":2}]},

    public List<PopupWas> arrIapProd = new List<PopupWas> ();
    public string PopupCode;
    public int RewardCash;

    public void Parse (JSONNode pJson)
    {
        PopupCode = pJson ["popupCode"];
        RewardCash = pJson ["rewardCash"].AsInt;
        for (int k = 0; k < pJson["arrItem"].Count; k++) {
            PopupWas curObj = new PopupWas ();
            curObj.ParseIapFrom (pJson ["arrItem"] [k]);
            //curObj.ShowMyself ();
            arrIapProd.Add (curObj);
        }
        Ag.LogString (" AmPopupProdIAP   ::    Parsed  ...   arrIapProd : " + arrIapProd.Count);
    }
}

public  class AmPopupProd : AmObject
{
    public PopupWas WAS = new PopupWas ();

    public AmPopupProd ()
    {
    }

    public bool ParseFrom (JSONNode pJson)
    {
        return WAS.ParseFrom (pJson);
    }
}