/* [2013:7:8:MOOON] Generic Added
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq.Expressions;
using SimpleJSON;

public class AgUtil
{
    public static List<Texture2D> arrSomeTexture = new List<Texture2D> ();
    // Event Related Variables...
    public static int mRandomCounter = 81000;

    public static string IntArrToJson (string key, int[] arrInt, bool withComma = true)
    {
        string curStr = "";
        for (int k = 0; k < arrInt.Length; k++) {
            curStr += arrInt [k].ToString ();
            if (k != (arrInt.Length - 1))
                curStr += ",";
        }
        curStr = curStr.AddSqreBrakt ();
        if (withComma)
            return  "\"" + key + "\":" + curStr + ",";
        return  "\"" + key + "\":" + curStr;
    }

    public static bool IsNullJson (JSONNode pJson)
    {
        if (pJson == null)
            return true;
        string jStr = pJson.ToString ();
        jStr = jStr.Replace (" ", "");
        jStr = jStr.Replace ("\"", "");
        if (jStr == "{}" || jStr == "" || jStr == "\"\"" || jStr == "null" || jStr == "Null" || jStr == "NULL")
            return true;
        else
            return false;
    }

    public static bool? LinearPercentVari (int spot0, int spot1, int pSpot)
    {
        if ((spot0 < pSpot && spot1 < pSpot) || (spot0 > pSpot && spot1 > pSpot))
            return null;

        if (pSpot == spot1)
            return true;
        if (pSpot == spot0)
            return false;

        int ratio = Math.Abs ((int)(Math.Abs (pSpot - spot0) * 1000 / Math.Abs (
                        (double)(spot1 - spot0)))); // in %% 1000 

        int rand = AgUtil.RandomInclude (0, 1000);
        Ag.LogString ("   Is it ?  ;: " + rand + "  <  " + ratio + "  => " + (rand < ratio));
        return rand < ratio;
    }

    public static bool IsLGeVuModel ()
    {
        string dvice = SystemInfo.deviceModel.Replace (" ", "").ToUpper ().Substring (0, 10);
        return dvice == "LGELG-F100" || dvice == "LGELG-F200" || dvice == "LGELG-F300";
    }

    public static string GetN<T> (Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }

    public static List<int> CombiSelect (int pAmong, int pSel)  // 4 C 2  // include 0, 1, 2 ...
    {
        Ag.LogString ("  AgUtil :: CombiSelect " + pAmong + "   select " + pSel);
        List<int> among = new List<int> ();
        List<int> rVal = new List<int> ();

        for (int k = 0; k < pAmong; k++) {
            among.Add (k);
        }

        for (int k = 0; k < pSel; k++) {
            int val = among [AgUtil.RandomInclude (0, among.Count - 1)];
            among.Remove (val);
            rVal.Add (val);
        }
        return rVal;
    }

    public static int RandomInclude (int pMin, int pMax)
    {
        int wid = pMax - pMin + 1;
        AgUtil.mRandomCounter += 5;
        double ran = (DateTime.Now.Millisecond + 1) * 1.2443423342 * pMax * AgUtil.mRandomCounter / (AgUtil.mRandomCounter + 7.33459f);
        ran = pMax * (ran - (int)ran) * 10;
        
        //DateTime.Now.Millisecond.ToString ().HtLog ();
        //ran.ToString ().HtLog (); 
        
        AgUtil.mRandomCounter += ((int)ran) % 4;
        
        switch (((int)ran) % 4) {
        case 0:
            ran += (DateTime.Now.Second);
            break;
        case 1:
            ran += (DateTime.Now.Hour);
            break;
        case 2:
            ran += (DateTime.Now.Minute);
            break;
        case 3:
            ran += (DateTime.Now.Day);
            break;
        }
        
        ran = pMax * (ran - (int)ran) * 100;
        
        if (AgUtil.mRandomCounter > 99999)
            AgUtil.mRandomCounter -= 12345;
        
        //(" Mili " + DateTime.Now.Millisecond + " ,  " + AgUtil.mRandomCounter + " ,   " + ran).HtLog ();
        return (((int)ran) % wid) + pMin;
    }

    public static bool xxCheckNickValid (string pNick)
    {
        //string sPattern = "^[a-zA-Z]{3}[a-zA-Z0-9.]{3,10}$";
        if (pNick.Length < 6)
            return false;
        if ("GUEST" == pNick.Substring (0, 5).ToUpper ())
            return false;
        //string sPattern = "^[a-zA-Z0-9]{3}[a-zA-Z0-9]{3,9}$";
        string sPattern = "^[a-zA-Z0-9]{2}[a-zA-Z0-9]{2,16}$";
        return System.Text.RegularExpressions.Regex.IsMatch (pNick, sPattern);
    }

    public static bool CheckNickValidWithPattern (string pNick, string pPatern)
    {
        return System.Text.RegularExpressions.Regex.IsMatch (pNick, pPatern);
    }

    public static string xxRemoveDot (string pTarget)
    {
        string rStr = "";
        string pattern = "^[a-zA-Z0-9]{1}$";
        
        int j, num = pTarget.Length;
        for (j = 0; j < num; j++) {
            string cur = pTarget.Substring (j, 1);
            if (AgUtil.CheckNickValidWithPattern (cur, pattern))
                rStr += cur;
        }
        return rStr;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  StackOfInt  _____  Class  _____
public class StackOfInt
{
    public string str = "";

    public StackOfInt (string pStr)
    {
        str = pStr;
    }

    public StackOfInt (int a, int b = 0, int c = 0)
    {
        PushAnInt (a);
        PushAnInt (b);
        PushAnInt (c);
    }

    public StackOfInt (WasRank rank)
    { // weekScore, totalScore, bestScore, contWinNum, winNum, lossNum, winNumWeek, lossNumWeek, lastWeekRank, thisWeekRank;
        Push3Int (rank.weekScore, rank.totalScore, rank.bestScore);
        Push3Int (rank.contWinNum, rank.winNum, rank.lossNum);
        Push3Int (rank.winNumWeek, rank.lossNumWeek, rank.lastWeekRank);
        PushAnInt (rank.thisWeekRank);
    }

    public WasRank ParseRank ()
    {
        WasRank rO = new WasRank () { weekScore = PopHeadInt (), totalScore = PopHeadInt (), bestScore = PopHeadInt (), 
            contWinNum = PopHeadInt (), winNum = PopHeadInt (), lossNum = PopHeadInt (), winNumWeek = PopHeadInt (), 
            lossNumWeek = PopHeadInt (), lastWeekRank = PopHeadInt (), thisWeekRank = PopHeadInt ()
        };
        return rO;
    }

    public void GetValue (out int v1, out int v2, out int v3)
    {
        v1 = PopHeadInt ();
        v2 = PopHeadInt ();
        v3 = PopHeadInt ();
    }

    public void Push3Int (int p1, int p2, int p3)
    {
        PushAnInt (p1);
        PushAnInt (p2);
        PushAnInt (p3);
    }

    public string PushAnInt (int pI)
    {
        str += pI.ToString () + "_";
        return str;
    }

    public int PopHeadInt ()
    {
        int idx = str.IndexOf ('_');
        //(str + "  " + idx).HtLog();
        if (idx < 0 && str.Length > 0) {
            int last = int.Parse (str);
            str = "";
            return last;
        }
        int rV = int.Parse (str.Substring (0, idx));
        str = str.Substring (idx + 1, str.Length - idx - 1);
        return rV; 
    }
}
//  _////////////////////////////////////////////////_    _____   class   _____   Bool List    _____
public class AgBoolList
{
    private List<bool> blList = new List<bool> ();

    public AgBoolList (int Length, bool defltV)
    {
        for (int k = 0; k < Length; k++) {
            blList.Add (defltV);
        }
    }

    public void SetValueAt (int Idx, bool Val)
    {
        blList [Idx] = Val;
    }

    public bool AllValuesAre (bool Val)
    {
        for (int k = 0; k < blList.Count; k++) {
            if (blList [k] != Val)
                return false;
        }
        return true;
    }
}
//  _////////////////////////////////////////////////_    _____   class   _____   Variable    _____
public class AgVariable
{
    public AmObject mObject;
    // AmUser, AmPlayer 등의 객체가 할당 받을 수 있음.
    public byte mByte;
    public bool? mBool;
    public int? mIntOut, mIntIn;
    public string mStrVar, mTrueStateName, mFalseStateName, mClearStateName, mVarName;

    public AgVariable ()
    {
    }

    public AgVariable (int pInt, string pStr = "")
    {
        mIntIn = pInt;
        mStrVar = pStr;
    }

    public AgVariable (int pInt, int pOut)
    {
        mIntIn = pInt;
        mIntOut = pOut;
    }

    public AgVariable (int pPlayer, int pOkItem, int pNoItem)
    {
        mIntIn = pPlayer;
        mIntOut = pOkItem;
        mStrVar = pNoItem.ToString ();
    }

    public virtual void SetValueBy (string pState)
    {
    }

    public virtual bool IsItClearStage (string pState)
    { 
        return true; 
    }
}

