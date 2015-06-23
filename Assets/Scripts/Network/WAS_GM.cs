using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using SimpleJSON;


public class WasScouter : WasObject
{
    public override void SendAction ()
    {
        Ag.LogString ("WasScouter :: SendAction ...   Started ...  "  + "   ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 505);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("buyType", User.GetBuyType ("FuncScouter"), false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogStartWithStr (1, "WasScouter :: postAction " + RcvdStr);
            Ag.LogString (RcvdStr.Substring (0, 10), pWichtig: true);
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
        Ag.LogString ("WasScouter :: CatchAction ...   ");
    }
}

public class WasCodeOnlyProtocol : WasObject // 코드만 있는 프로토콜. 
{
    public int protoCode;
    // 610 : GIVE_REWARD_EVERY_HOUR_EVENT  
    public override void SendAction ()
    {
        Ag.LogString ("WasCodeOnlyProtocol :: SendAction ...   Started ...  "  + "   Code : _____  " + protoCode + "  _____");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, protoCode);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogStartWithStr (1, "WasCodeOnlyProtocol :: postAction " + RcvdStr);
            Ag.LogString (RcvdStr.Substring (0, 10), pWichtig: true);
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
        Ag.LogString ("WasCodeOnlyProtocol :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WAS_GM related  _____  Class  _____
#if UNITY_ANDROID
public class WasGoogleIABillingKey : WasObject
{
    public string cashTypeID;

    public override void SendAction ()
    {
        Ag.LogString ("WasGoogleIABillingKey :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 201);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1); // Added by Moon
        SendStr = SendStr.AddKeyValue ("cashTypeID", cashTypeID, false);
        SendStr = SendStr.AddParen ();

        postAction = () => {
            Ag.LogStartWithStr (1, "WasGoogleIABillingKey :: postAction " + RcvdStr);

            if (Result.result == 0)
                AgStt.mIAB.TransactionKey = NdObj["buyTransactionKey"];

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
}
#endif
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAP  _____  Class  _____
public class WasInAppPrchs : WasObject
{
    public string Signature;

    public override void SendAction ()
    {
        Ag.LogString ("WasInAppPrchs :: SendAction ...   Started ...  ", pWichtig: true);
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 202);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        #if UNITY_IPHONE
        SendStr = SendStr.AddKeyValue ("productID", AgStt.mIAP.ProductID);
        SendStr = SendStr.AddKeyValue ("receipt", AgStt.mIAP.ReceiptOfIAP);
        SendStr = SendStr.AddKeyValue ("buyCode", AgStt.mIAP.TransactionID);
        Ag.LogString ("ID : " + AgStt.mIAP.ProductID + " Receit : " + AgStt.mIAP.ReceiptOfIAP.Substring (0, 5) + AgStt.mIAP.TransactionID, pWichtig: true);
        SendStr = SendStr.AddKeyValue ("androidSignature", "");
        SendStr = SendStr.AddKeyValue ("buyTransactionKey", "");
        SendStr = SendStr.AddKeyValue ("appStoreType", 1, false); // iOS 
        #endif
        #if UNITY_ANDROID

        if (Ag.CurStorePlfm == StorePlfm.Nstore) {
            AgStt.mIAB.Receipt = AgStt.mIAB.BuyCode;
            AgStt.mIAB.Signature = "";
            AgStt.mIAB.TransactionKey = "";
        }

        SendStr = SendStr.AddKeyValue ("productID", AgStt.mIAB.ProductID);
        SendStr = SendStr.AddKeyValue ("receipt", AgStt.mIAB.Receipt);
        SendStr = SendStr.AddKeyValue ("buyCode", AgStt.mIAB.BuyCode);
        SendStr = SendStr.AddKeyValue ("androidSignature", AgStt.mIAB.Signature);
        SendStr = SendStr.AddKeyValue ("buyTransactionKey", AgStt.mIAB.TransactionKey);
        SendStr = SendStr.AddKeyValue ("appStoreType", 2 , false); // google 2,  nStore 3
        ("WasInAppPrchs ::  buyCode : " + AgStt.mIAB.BuyCode + "   productID : " + AgStt.mIAB.ProductID).HtLog ();
        #endif

        SendStr = SendStr.AddParen ();

        Ag.LogString (" WasInAppPrchs " + SendStr, pWichtig: true);

        postAction += () => {
            Ag.LogStartWithStr (1, "WasInAppPrchs :: postAction " + RcvdStr);
            Ag.LogString (RcvdStr.Substring (0, 10), pWichtig: true);

            if (Result.result == 0) {
                AgStt.InAppPurchaseSuccess = true;
                Ag.LogIntenseWord ("  Was In App Purchase >>>>    AgStt.InAppPurchaseSuccess " + AgStt.InAppPurchaseSuccess);
            }

//            if (Result.result == 0)
//                ActPurchaseSuccess ();
//            else
//                ActPurchaseCancelled ();
            //Ag.LogString ("WasReview :: postAction " + Result.result);
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
}

public class WasReview : WasObject
{
    public override void SendAction ()
    {
        Ag.LogString ("WasReview :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 374); // AddKeyValue ("serviceCode", 374);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogStartWithStr (1, "WasReview :: postAction " + RcvdStr);

            //Ag.LogString ("WasReview :: postAction " + Result.result);
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
}

public class WasMailSend : WasObject
{
    //    public AmUser usr;
    public string friendID, content, itemTypeId;

    public override void SendAction ()
    {
        Ag.LogString ("WasMailSend :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 362); // .AddKeyValue ("serviceCode", 362);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);

        SendStr = SendStr.AddKeyValue ("friendID", friendID);
        SendStr = SendStr.AddKeyValue ("content", content);
        SendStr = SendStr.AddKeyValue ("itemTypeId", itemTypeId, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogStartWithStr (1, "WasMailSend :: postAction " + RcvdStr);

            //if (usr.ParseMail (arrObj))
            //Ag.LogIntenseWord ("     WasMail    OK    ");

            Ag.LogString ("WasMailSend :: postAction " + Result.result);
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
}

public class WasMailFetch : WasObject
{
    //    public AmUser usr;
    public override void SendAction ()
    {
        User.arrMail.Clear ();
        Ag.LogString ("WasMailFetch :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 360); // AddKeyValue ("serviceCode", 360);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogStartWithStr (1, "WasMailFetch :: postAction " + RcvdStr);

            JSONNode arrObj = NdObj ["arrMails"];

            for (int k = 0; k < arrObj.Count; k++) {
                AmMail nObj = new AmMail ();
                if (nObj.ParseFrom (arrObj [k]))
                    User.arrMail.Add (nObj);
            }

            //if (usr.ParseMail (arrObj))
            //Ag.LogIntenseWord ("     WasMail    OK    ");

            Ag.LogString ("WasMailFetch :: postAction " + Result.result);
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
}

public class WasMailErase : WasObject
{
    //    public AmUser usr;
    public int msgID1, msgID2;
    public bool eraseAll = false;
    // mailbox(360) 에서 받은 msgID2. 모두 받기는 msgID1, msgID2 모두 -1
    public override void SendAction ()
    {
        if (eraseAll)
            msgID1 = msgID2 = -1; // 모두 받기는 msgID1, msgID2 모두 -1
        Ag.LogString ("WasMailErase :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 361); // AddKeyValue ("serviceCode", 361);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);

        SendStr = SendStr.AddKeyValue ("msgID1", msgID1);
        SendStr = SendStr.AddKeyValue ("msgID2", msgID2, false);

        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasMailErase :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 

            WasUserInfo aObj = new WasUserInfo () { User = User, flag = 0 };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" User Info ::  Result   " + Result.result);
            };

            Ag.LogString ("WasMailErase :: postAction " + Result.result);
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
}


