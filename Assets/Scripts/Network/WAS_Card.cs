// [2013:11:29:MOON<Start>]
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
public class WasPurchaseCard : WasObject
{
    public string uniformTypeID, leagueType = "N";
    public int option, eaNum, buyType, additionalBuyFlag;
    // additionalBuyFlag : 0 (no), 1 (additional)
    //    public List<string> playerNamze = new List<string> ();
    //    public List<int> backNum = new List<int> ();
    // 0 : cash, 1 : gold
    /*
    public WasPurchaseCard ()
    {
        string hangen = WWW.UnEscapeURL ("%EC%83%9D%EC%84%B1%EC%9E%90");
        Ag.LogString ("WasPurchaseCard ::     Generation   ...  " + hangen);

        dlgt_WillSend = () => {
            return User.arrCard.Count < 30;
        };
    }
    */
    public override void SendAction ()
    {
        Ag.LogString ("WasPurchaseCard :: SendAction ...   Started ...  ");
        User.arrNewCard.Clear ();
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 220);
        SendStr = SendStr.AddKeyValue ("option", option);
        SendStr = SendStr.AddKeyValue ("eaNum", eaNum);
        SendStr = SendStr.AddKeyValue ("leagueType", leagueType);

        SendStr = SendStr.AddKeyValue ("formatVersion", 2); // Deck
        //SendStr = SendStr.AddKeyValue ("kind", kind);
//        SendStr = SendStr.AddArray ("backNum", JsonMapper.ToJson (backNum));
//        SendStr = SendStr.AddArray ("playerName", JsonMapper.ToJson (playerName));

        SendStr = SendStr.AddKeyValue ("additionalBuyFlag", additionalBuyFlag);
        SendStr = SendStr.AddKeyValue ("buyType", buyType, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasPurchaseCard :: postAction " + RcvdStr);
            //JsonData jsData = JsonMapper.ToObject (RcvdStr); 
            JSONNode jsCard = NdObj ["arrCard"];

            if (Result.result == 0)
                for (int k = 0; k < jsCard.Count; k++) {
                    AmCard nuCard = new AmCard ();
                    nuCard.WAS.WasCardParse (jsCard [k]);
                    nuCard.ScouterParse ();
                    if (User.GetCardIdOf (jsCard [k] ["ID"].AsInt) == null)
                        nuCard.KickOrder = -1;
                    User.arrNewCard.Add (nuCard);
                }
            // Card Update   ....
            WasCardUpdate bObj = new WasCardUpdate () { User = User };
            bObj.messageAction = (int pInt) => {
            };

            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasPurchaseCard :: postAction " + Result.result + "   New Card :  " + User.arrNewCard.Count);
            messageAction (Result.result);
            //Ag.LogString (((string)(jsData ["cash1"])).LogWith ("cash1") + ((string)(jsData ["cash2"])).LogWith ("cash2") + ((string)(jsData ["gold"])).LogWith ("gold"));
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
        Ag.LogString ("WasPurchaseCard :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _____   Was   _____   Class   _____
public class WasCardCombi : WasObject
{
    public int cardID1, cardID2, cardID3;
    public List<string> arrItemStr = new List<string> ();
    //public int buyType;
    //  "arrItem":["itemTypeID":"CardCombiAdvt" ,"itemTypeID":"CardCombiAdvtHigh" ,"itemTypeID":"CardCombiGrade"]}
    public override void SendAction ()
    {
        Ag.LogString ("WasCardCombi :: SendAction ...   Started ...  ");
        User.arrNewCard.Clear ();
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 253);

        SendStr = SendStr.AddKeyValue ("formatVersion", 2);

        SendStr = SendStr.AddKeyValue ("cardID1", cardID1);
        SendStr = SendStr.AddKeyValue ("cardID2", cardID2);
        SendStr = SendStr.AddKeyValue ("cardID3", cardID3);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FunCardMix"));


        string strArr = ""; // 0 일 때 에러 안나게
        if (arrItemStr.Count == 0) {
            SendStr = SendStr.AddArray ("arrItem", "[]");
        } else {
            for (int k = 0; k < arrItemStr.Count; k++) {
                string curStr = "";
                curStr = curStr.AddKeyValue ("itemTypeID", arrItemStr [k], false); //, ;
                curStr = curStr.AddParen ();
                strArr += curStr;
                if (k != (arrItemStr.Count - 1))
                    strArr += ",";
            }
            strArr = strArr.AddSqreBrakt ();
            SendStr = SendStr.AddArray ("arrItem", strArr, false);
        }
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCardCombi :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr);
            if (Result.result == 0) {

                JSONNode contJs = NdObj ["cardInfoRslt"];
                contJs.ToString ().HtLog ();
                Ag.LogString ("WasCardCombi :: postAction  .. >>>>  next step is messageAction  ");

                AmCard theCard = new AmCard ();
                theCard.WAS.WasCardParse (contJs);
                theCard.ScouterParse ();
                if (User.GetCardIdOf (contJs ["ID"].AsInt) == null)
                    theCard.KickOrder = -1;
                User.arrNewCard.Add (theCard);
                theCard.mustUpdate = true;
                // Card Update   ....
                WasCardUpdate bObj = new WasCardUpdate () { User = User };
                bObj.messageAction = (int pInt) => {
                };

            }
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
        Ag.LogString ("WasCardCombi :: CatchAction ...   ");
    }
}

public class WasCardLevelup : WasObject
{
    public int cardID;

    public override void SendAction ()
    {
        Ag.LogString ("WasCardLevelup :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 252);
        SendStr = SendStr.AddKeyValue ("cardID", cardID);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1); // Deck

        AmCard curCard = User.GetCardIdOf (cardID);
        int bT = User.GetBuyType ("FuncLevelUp" + (curCard.WAS.level + 1));

        SendStr = SendStr.AddKeyValue ("buyType", bT, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCardLevelup :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr);
            if (Result.result == 0) {
                JSONNode contJs = NdObj ["cardInfoRslt"];
                Ag.LogString ("WasCardLevelup :: postAction  .. >>>>  next step is messageAction  ");
                AmCard theCard = User.arrCard.GetMemberWithCond<AmCard> ((AmCard crd) => {
                    return crd.WAS.ID == contJs ["ID"].AsInt;
                });
                theCard.WAS.WasCardParse (contJs);
                theCard.ScouterParse ();
            }
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
        Ag.LogString ("WasCardLevelup :: CatchAction ...   ");
    }
}

public class WasCardEnchantRecover : WasObject
{
    public int code, cardID;
    // Enchant : 251, Recover : 254
    public override void SendAction ()
    {
        Ag.LogString ("WasCardEnchantRecover :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, code);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        SendStr = SendStr.AddKeyValue ("cardID", cardID);

        int bT = 0;
        if (code == 251) {
            AmCard curCard = User.GetCardIdOf (cardID);
            bT = User.GetBuyType ("FuncEnchant" + (curCard.WAS.enchant + 1));
        }
        if (code == 254)
            bT = User.GetBuyType ("FuncRecover");


        SendStr = SendStr.AddKeyValue ("buyType", bT, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCardEnchantRecover :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr);
            if (Result.result == 0) {
                if (code == 251) {
                    JSONNode contJs = NdObj ["cardInfoRslt"];
                    Ag.LogString ("WasCardEnchantRecover :: postAction  .. >>>>  next step is messageAction  ");
                    AmCard theCard = User.arrCard.GetMemberWithCond<AmCard> ((AmCard crd) => {
                        return crd.WAS.ID == contJs ["ID"].AsInt;
                    });
                    theCard.WAS.WasCardParse (contJs);
                    theCard.ScouterParse ();
                }
            }
            // 0 : 성공
            Ag.LogString ("WasCardEnchantRecover :: postAction  .. >>>>  next step is messageAction  ");
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
        Ag.LogString ("WasCardEnchantRecover :: CatchAction ...   ");
    }
}

public class WasCardExtend : WasObject
{
    public int cardId, count;

    public override void SendAction ()
    {
        Ag.LogString ("WasCardExtend :: SendAction ...   Started ...  ");
        SendStr = "";

        SendStr = SendStr.AddCodeKeyKKOID (User, 315); // AddKeyValue ("serviceCode", 315);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);

        SendStr = SendStr.AddKeyValue ("cardId", cardId);
        SendStr = SendStr.AddKeyValue ("count", count);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncCardExtend" + User.GetCardIdOf (cardId).WAS.grade + "30"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCardExtend :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공
            Ag.LogString ("WasCardExtend :: postAction  .. >>>>  next step is messageAction  ");
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
        Ag.LogString ("WasCardExtend :: CatchAction ...   ");
    }
}

public class WasCardUpdate : WasObject
{
    //    public AmUser usr;
    public List<AmCard> arrSendCard = new List<AmCard> ();
    bool willSend = true;

    public WasCardUpdate ()
    {
        if (Ag.SingleTry > 0) {
            dlgt_WillSend = () => {
                return false;
            };
            Ag.LogIntenseWord ("  WasCardUpdate ()      during   Single  Try  " + Ag.SingleTry);
        }
    }

    public override void SendAction ()
    {
        Ag.LogString ("WasCardUpdate :: SendAction ...   Started ...  " + arrSendCard.Count);

        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 310); 
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        arrSendCard.AddRange (User.arrNewCard);

        arrSendCard.AddRange (User.GetUpdateCards ());

        Ag.LogString (" WasCardUpdate :: Update Cards Number  >>>   " + arrSendCard.Count);

        string strArr = "";

        int count = arrSendCard.Count > 8 ? 8 : arrSendCard.Count;
        //int count = arrSendCard.Count > 5 ? 5 : arrSendCard.Count;

        for (int i = 0; i < count; i++) {
            AmCard uObj = arrSendCard [i];
            ("  i = " + i).HtLog ();
            try {
                //uObj.WAS.SetInfoString();
                uObj.WAS.ShowMySelf ();
            } catch {
                uObj.WAS.skill = new int[] { 0, 0, 0 };
            }
            strArr += uObj.WAS.ToJsonStr (); // JsonMapper.ToJson (uObj.WAS).ToString () + " , ";
            if (i != (arrSendCard.Count - 1))
                strArr += ",";
        }
        strArr = strArr.AddSqreBrakt ();  // [ ]
        SendStr = SendStr.AddArray ("arrCardInfo", strArr, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCardUpdate :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공

            if (Result.result == 0) {
                for (int i = 0; i < count; i++) {
                    arrSendCard [i].UpdatedPerformed ();
                }
            }

            if (User.GetUpdateCards ().Count > 0) {
                WasCardUpdate mObj = new WasCardUpdate () { User = User };
                mObj.messageAction = (int pInt) => {
                    mObj = null;
                };
            }

            Ag.LogString ("WasCardUpdate :: postAction  .. >>>>  next step is messageAction  ");
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
        Ag.LogString ("WasCardUpdate :: CatchAction ...   ");
    }
}
