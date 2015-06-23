// [2013:11:18:MOON<Start>]
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using LitJson;
using SimpleJSON;

//using System.Threading;
//using System.Net;
//using System.Text;
//using System.IO;
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WAS Object  _____  Class  _____
public class WasObject
{
    protected bool? IsOK;
    public bool Encript = true;
    public string SendStr, RcvdStr, RqstURI, TpStr;
    public AmUser User;
    public WasResult Result;
    public int RetryN;
    public Action postAction;
    // ~= Receiving
    public  Dlgt_Int_V messageAction;
    public Dlgt_V_Bool dlgt_WillSend;
    public JsonData JData;
    public JSONNode NdObj;

    public WasObject ()
    {
        messageAction = (int pInt) => {
            Ag.LogString (" WasObject :: Default messageAction ..  !!!  " + pInt.LogWith ("Result"));
        };
        TpStr = this.GetType ().ToString ();
        StNet.AddWASJob2Thread (this);
    }
    //    public void AddParameter (string pKey, string pVal, bool isEncript, bool addEmp) // Joyple
    //    {
    //        SendStr += (pKey + "=");
    //        if (isEncript) {
    //            byte[] encryptedBlock = AgStt.AesJoyple.EncryptToByte (pVal);
    //            foreach (byte aByte in encryptedBlock) {
    //                SendStr += aByte.ToString ();
    //            }
    //        } else
    //            SendStr += pVal;
    //        if (addEmp)
    //            SendStr += "&";
    //        ("Was_Object ::  SendStr ::  >>  " + SendStr).HtLog ();
    //    }
    public virtual bool JobCompleted ()
    {
        return false;
    }

    public virtual void CatchAction ()
    {
        Ag.LogDouble ("   ERROR    " + TpStr + "    :: CatchAction ...   ");
    }

    public virtual void SendAction ()
    {
    }
    //  _////////////////////////////////////////////////_    _____   Send   _____   Receive   _____
    public void SendAndRciv ()
    {
        //Ag.NetExcpt.WASActionTime = DateTime.Now;

        IsOK = null;
        Ag.LogString (" {{ " + TpStr + " }} :  SendAndRciv () >>> SEND ing >>    " + SendStr);
        try {
            RetryN++;

            RcvdStr = WAS.SendWASvrMessage (SendStr, Encript);
            Result = new WasResult ();
            Ag.LogString (" {{ " + TpStr + " }}    Received  () :: " + RcvdStr);
            if (RcvdStr == "CATCH") {
                IsOK = false;
                Result.result = -99;
                return;
            }

            IsOK = true;
            NdObj = JSON.Parse (RcvdStr);

            Result.result = -1; // Default
            try {
                Result.serviceCode = NdObj ["serviceCode"].AsInt; // JData ["serviceCode"];
                Result.result = NdObj ["result"].AsInt;// (int)JData ["result"];  //  OK => 0 ...

                if (Result.result == 501)
                    Ag.NetExcpt.WasLoginDuplicate = true;
                else
                    Ag.NetExcpt.WasLoginDuplicate = false;

                Ag.LogString (" {{ " + TpStr + " }}          Result : " + Result.result + "        serviceCode : " + Result.serviceCode);
            } catch {
                Ag.LogIntenseWord (" {{ " + TpStr + " }} SendAndRciv () >>>  No result, serviceCode  !!   ");
            }
            try {
                if ((Result.result == 0 || Result.result == 4) && NdObj ["cash1"].AsBool) {
                    User.mCash1 = Ag.mySelf.mCash1 = NdObj ["cash1"].AsInt;
                    User.mCash2 = Ag.mySelf.mCash2 = NdObj ["cash2"].AsInt;
                    User.mGold = Ag.mySelf.mGold = NdObj ["gold"].AsInt;
                    User.ShowCurrentCash ();  // Log ..
                }
            } catch {
                Ag.LogString (" {{ " + TpStr + " }}  Result : " + Result.result + " serviceCode : " + Result.serviceCode + "   has  No Cash Info ...  OK ");
                //" No Cash Info ...  OK  ".HtLog ();
            }

            try {
                Ag.TimeNow = long.Parse (NdObj ["serverTimeStampToUTC"]) / 1000;
                Ag.DTNowTickMark = DateTime.Now;
                Ag.LogString ("   ServerTime :: " + Ag.UnixTimeStampToDateTime (Ag.TimeNow));
                //Ag.LogString ("   Parsing Time :: >>>    " + Ag.TimeNow);
            } catch {
                Ag.LogDouble (" {{ " + TpStr + " }}   ::   has  No Time Info ...  OK ");
            }

            if (postAction != null)
                postAction ();
            //Ag.LogString (" {{ " + TpStr + " }} SendAndRciv () :: " + RcvdStr);
            Ag.LogNewLine (3);
        } catch {
            Ag.LogNewLine (5);
            Ag.LogString (" {{ " + TpStr + " }} SendAndRciv () >>>   Catch  !!!!!!!!   ");
            Ag.LogNewLine (5);
            CatchAction ();
        }
    }

    public string GetIntArray (int[] arrInt)
    {
        string strArr = ""; // 0 일 때 에러 안나게
        if (arrInt.Length == 0) {
            return "[]";
        } else {
            for (int k = 0; k < arrInt.Length; k++) {
                strArr += arrInt [k].ToString ();
                if (k != (arrInt.Length - 1))
                    strArr += ",";
            }
            strArr = strArr.AddSqreBrakt ();
        }
        return strArr;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Received Result  _____  4 Parsing Base Class  _____
public class WasResult
{
    public int result, serviceCode;

    public override string ToString ()
    {
        return string.Format ("[WasTeamCheck] ::  result [ {0} ] ,     serviceCode [ {1} ] ", result, serviceCode);
    }
}

public static class ExtJson
{
    public static bool ParseTo (this JSONNode nd, string k1, out int oInt1)
    {
        oInt1 = 0;
        try {
            oInt1 = nd [k1].AsInt;
        } catch {
            Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   int __ " + k1 + " __   >>>   Catch  !!!!!!!!   ");
            return false;
        }
        return true;
    }

    public static bool ParseTo (this JSONNode nd, string k1, out int oInt1, string k2, out int oInt2)
    {
        if (nd.ParseTo (k1, out oInt1) && nd.ParseTo (k2, out oInt2))
            return true;
        Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   int __ 2 __   >>>   Fail  !!!!!!!!   ");
        return false;
    }

    public static bool ParseTo (this JSONNode nd, string k1, out int oInt1, string k2, out int oInt2, string k3, out int oInt3)
    {
        if (nd.ParseTo (k1, out oInt1) && nd.ParseTo (k2, out oInt2) && nd.ParseTo (k3, out oInt3))
            return true;
        Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   int __ 3 __   >>>   Fail  !!!!!!!!   ");
        return false;
    }

    public static bool ParseTo (this JSONNode nd, string k1, out string oStr)
    {
        oStr = "";
        try {
            oStr = nd [k1];
        } catch {
            Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   static __ " + k1 + " __   >>>   Catch  !!!!!!!!   ");
            return false;
        }
        return true;
    }

    public static bool ParseTo (this JSONNode nd, string k1, out string oStr1, string k2, out string oStr2)
    {
        if (nd.ParseTo (k1, out oStr1) && nd.ParseTo (k2, out oStr2))
            return true;
        Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   string __ 2 __   >>>   Fail  !!!!!!!!   ");
        return false;
    }

    public static bool ParseTo (this JSONNode nd, string k1, out string oStr1, string k2, out string oStr2, string k3, out string oStr3)
    {
        if (nd.ParseTo (k1, out oStr1) && nd.ParseTo (k2, out oStr2) && nd.ParseTo (k3, out oStr3))
            return true;
        Ag.LogString (" {{ " + "ParseTo" + " }}   <<<   string __ 3 __   >>>   Fail  !!!!!!!!   ");
        return false;
    }

    public static bool IsJsonNull (this string pS)
    {
        string noSpace = pS.Replace (" ", "");
        noSpace = noSpace.Replace ("\"", "");
        if (noSpace == "{}" || noSpace == "" || noSpace == "\"\"" || noSpace == "null" || noSpace == "Null" || noSpace == "NULL")
            return true;
        else
            return false;
    }

    public static string DodgeBackSlashQuoMark (this string pS)
    {
        return pS.Replace ("\\\"", "\"");
    }

    public static string RemoveQuotationMark (this string pS)
    {
        return pS.Replace ("\"", "");
    }

    /// <summary>
    /// Dodges / Recover the json.  Replace ("\"", "''");
    /// </summary>

    public static string DodgeJsonWithStr (this string pS, string pD)
    {
        return pS.Replace ("\"", pD);
    }

    public static string RecoverFromDodgeStr (this string pS, string pD, bool CutTail = false)
    {
        string rVal = pS.Replace (pD, "\"");

        if (CutTail)
            return rVal.Substring (1, rVal.Length - 2);
        return rVal;
    }

    /// <summary>
    /// Dodges / Recover the json.  Replace ("\"", "''");
    /// </summary>

    public static string DodgeJson (this string pS)
    {
        return pS.Replace ("\"", "''");
    }

    public static string RecoverFromDodge (this string pS)
    {
        return pS.Replace ("''", "\"");
    }

    public static string RemoveHeadFootOneChar (this string pS)
    {
        return pS.Substring (1, pS.Length - 2);
    }

    public static string AddCodeKeyKKOID (this string pS, AmUser usr, int code, bool withComma = true)
    {
        if (usr == null)
            Ag.LogString (" AddCodeKeyKKOID ::  User is Null   ! ! ! ! ! ! ! ! ! ! !  ", pWichtig: true);
        pS = pS.AddKeyValue ("serviceCode", code); 
        if (Ag.mGuest) {
            pS = pS.AddKeyValue ("userType", 0);
        } else {
            pS = pS.AddKeyValue ("userType", 1);
        }
        pS = pS.AddKeyValue ("userID", usr.WAS.KkoID);
        pS = pS.AddKeyValue ("deviceUUID", usr.DeviceID);
        //pS = pS.AddKeyValue ("formatVersion", 1);
        return pS.AddKeyValue ("key", usr.WAS.WasKey, withComma);
    }

    public static string AddCodeKey (this string pS, AmUser usr, int code, bool withComma = true)
    {
        pS = pS.AddKeyValue ("serviceCode", code);
        return pS.AddKeyValue ("key", usr.WAS.WasKey, withComma);
    }

    public static string AddKV2<T> (this string pS, string pKey, T pVal, string pKey2, T pVal2, bool withComma = true)
    {
        pS = pS.AddKeyValue (pKey, pVal);
        return pS.AddKeyValue (pKey2, pVal2, withComma);
    }

    public static string AddArrayKV3 (this string pS, string pKey, string pVal, string pKey2, string pVal2, string pKey3, string pVal3, bool withComma = true)
    {
        pS = pS.AddArray (pKey, pVal);
        pS = pS.AddArray (pKey2, pVal2);
        return pS.AddArray (pKey3, pVal3, withComma);
    }

    public static string AddKV3<T> (this string pS, string pKey, T pVal, string pKey2, T pVal2, string pKey3, T pVal3, bool withComma = true)
    {
//        if (pVal == null)
//            Ag.LogIntenseWord ("   null  ");
        pS = pS.AddKeyValue (pKey, pVal);
        pS = pS.AddKeyValue (pKey2, pVal2);
        return pS.AddKeyValue (pKey3, pVal3, withComma);
    }

    public static string AddKeyValue<T> (this string pS, string pKey, T pVal, bool withComma = true)
    {
        //if (typeof(T) == typeof(string))
        if (pVal is string)
            return pS.AddStrValue (pKey, pVal.ToString (), withComma);

        if (pVal == null) {
            if (withComma)
                return pS + "\"" + pKey + "\":\"\",";
            else
                return pS + "\"" + pKey + "\":\"\" ";
        }

        if (withComma)
            return pS + "\"" + pKey + "\":" + pVal.ToString () + ",";

        return pS + "\"" + pKey + "\":" + pVal.ToString ();
    }

    public static string AddStrValue (this string pS, string pKey, string pVal, bool withComma = true)
    {
        if (withComma)
            return pS + "\"" + pKey + "\":\"" + pVal + "\",";
        return pS + "\"" + pKey + "\":\"" + pVal + "\"";
    }

    public static string AddStringWithQuotation (this string pS, string pVal, bool withComma = true)
    {
        if (withComma)
            return pS + "\"" + pVal + "\",";
        return pS + "\"" + pVal + "\"";
    }

    public static string AddArray (this string pS, string pKey, string pVal, bool withComma = true)
    {
        if (withComma)
            return pS + "\"" + pKey + "\":" + pVal.ToString () + ",";
        return pS + " \"" + pKey + "\":" + pVal.ToString ();
    }

    public static string AddParen (this string pS)  // [2013:11:18:MOON]
    {
        return "{" + pS + "}";
    }

    public static string AddSqreBrakt (this string pS)  // [2013:11:18:MOON]
    {
        return "[" + pS + "]";
    }
}
//public delegate void Action<T> ();
public class WasNoReturn : WasObject
{
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Ag.LogString ("WasNoReturn :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddKeyValue ("serviceCode", 101333);
        SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey, false);
        SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID, false);
        SendStr = SendStr.AddParen ();
        postAction = () => {
            "   >>>>>   ".HtLog ();
            messageAction (Result.result);
        };

        SendAndRciv ();
    }
}


