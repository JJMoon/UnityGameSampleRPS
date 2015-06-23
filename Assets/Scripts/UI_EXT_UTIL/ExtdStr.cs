// [2013:1:8:MOON] Start
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Linq;

//  ////////////////////////////////////////////////     ////////////////////////     >>>>> String & Debug.... <<<<<
public static class ExtStr
{
    public static void ShowEachChar (this byte[] pByte, string pComment)  // [2013:3:26:MOON] Added..
    {
        Ag.LogIntense (3, true);
        (" >>>>>      HtExtendMethodStr ::  ShowEachChar                 >>>>>>>>>>>>>>>>>>>>>  >>>>>   " + pComment + "   <<<<<").HtLog ();
        Ag.LogString (pComment);
		
        int ii, num = BitConverter.ToUInt16 (pByte, 12) + 14;
		
        for (ii = 0; ii < num; ii++) {
            byte cur;
            cur = pByte [ii];
            string hexOutput = String.Format ("{0:X}", cur);
            //System.Text.Encoding.ASCIIEncoding.GetBytes(x.ToString());
			
            if (ii == 14)
                Ag.LogString ("______________________________ Above are Header ______________________________");
			
            Ag.LogString ("Cur byte is:>> \t\t 0x " + hexOutput + ", \t   at \t ____ " + ii + " \t ____  \t DEC : " + cur + " \t _____      \t" + ((char)cur).ToString ());
        }
        Ag.LogString ("______________________________ Total Length = " + num + "\n");
        (" >>>>>      HtExtendMethodStr ::  ShowEachChar                 >>>>>>>>>>>>>>>>>>>>>  >>>>>   " + pComment + "   <<<<<").HtLog ();
        Ag.LogIntense (3, false);
    }

    public static byte[] StringToByteArray (this String hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes [i / 2] = Convert.ToByte (hex.Substring (i, 2), 16);
        return bytes;
    }

    public static string DecodeFromUtf8 (this string utf8String)
    {
        // read the string as UTF-8 bytes.
        byte[] encodedBytes = Encoding.UTF8.GetBytes (utf8String);

        // convert them into unicode bytes.
        byte[] unicodeBytes = Encoding.Convert (Encoding.UTF8, Encoding.Unicode, encodedBytes);

        // builds the converted string.
        return Encoding.Unicode.GetString (unicodeBytes);
    }

    public static void ShowEachBytes (this byte[] pByte, string pComment)  // [2013:3:26:MOON] Added..
    {
        Ag.LogIntense (3, true);
        (" >>>>>      HtExtendMethodStr ::  ShowEachChar                 >>>>>>>>>>>>>>>>>>>>>  >>>>>   " + pComment + "   <<<<<").HtLog ();
        Ag.LogString ("Length  >>    " + pByte.Length);

        for (int ii = 0; ii < pByte.Length; ii++) {
            byte cur;
            cur = pByte [ii];
            string hexOutput = String.Format ("{0:X}", cur);
            Ag.LogString ("Cur byte is:>> \t\t 0x " + hexOutput + ", \t   at \t ____ " + ii + " \t ____  \t DEC : " + cur + " \t _____      \t" + ((char)cur).ToString ());
        }
    }

    public static bool IsIntegerSingle (this string pStr)
    {
        //(" 1 : to Byte " + Convert.ToByte ("1").ToString () + " -> char -> byte : " + Convert.ToByte (char.Parse ("1"))).HtLog ();
        if (pStr.Length > 1) // "3" ...
            return false;
        byte strInByte = Convert.ToByte (char.Parse (pStr)); // 48 ~ 57.. 0 ~ 9
        return 47 < strInByte && strInByte < 58;
    }

    public static int GetContinuousInteger (this string pStr) // , bool pFirst = true) Later ...  // JKLeeMustSeeThis
    {
        string iStr = "";
        bool contNum = false;
        for (int k = 0; k < pStr.Length; k++) {
            if (IsIntegerSingle (pStr.Substring (k, 1))) {
                iStr += pStr.Substring (k, 1);
                contNum = true;
            } else {
                if (contNum)
                    break;
            }
        }
        if (contNum)
            return int.Parse (iStr);  // cannot be negative ...
        return -1; // No Integer  .. 
    }

    public static string ToFixedWidth (this int pNum, int pWid)
    {
        int max = (int)(Math.Pow (10, pWid)) - 1;
        //Ag.LogString (pNum + " is Targer " + pWid + " is Width,      max is " + max);
        if (pWid < 1)
            return "-"; //"WidIsUnderZero";
        if (pNum > max)
            return "-"; //"biggerThanMaxValue";
        string rVal;
        int jarisu = (pNum == 0) ? 1 : (int)Math.Log10 (pNum) + 1;
        int num0 = pWid - jarisu;
        //Ag.LogString (" jarisu : " + jarisu + "   num 0 : " + num0);
        rVal = num0 > 0 ? new string ('0', num0) : "";
        return rVal + pNum.ToString ();
    }

    public static string RemoveTail (this string pS, int num = 1)
    {
        return pS.Substring (0, pS.Length - num);
    }

    public static DateTime ToDateTime (this string pS)
    {
        if (pS.Length < 14)
            return DateTime.Now;
        string parsed = pS.Substring (4, 2) + "." + pS.Substring (6, 2) + "." + pS.Substring (0, 4) + " "
                        + pS.Substring (8, 2) + ":" + pS.Substring (10, 2) + ":" + pS.Substring (12, 2);
        return DateTime.Parse (parsed);
    }

    public static void HtLog (this string pStr)
    {
        if (AgStt.DebugOnDevice) {
            AgStt.arrLogOnDevice.Add (pStr + "\n");
            AgStt.SetDeviceLog ();
            return;
        }
        string theTime = "< " + DateTime.Now.Minute + " : " + DateTime.Now.Second + " >    ";
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.Android)
            Debug.Log (theTime + " HtLOG >> " + pStr + " \n");
        else
			//GeneralFunction.NativeLog("Ag.LogString  >>>>>>>>>>>>>>>>>>>> [ " + pStr + " ]");
			GeneralFunction.NativeLog ("UNITY C# Log :: " + pStr);
    }

    public static string ToLRUD (this int pDir)
    {
        switch (pDir) {
        case 0:
            return "DD";
        case 1:
            return "RU";
        case 2:
            return "LU";
        case 3:
            return "RD";
        case 4:
            return "LD";
        }
        return "Err@_ToLRUD";
    }
    //  ////////////////////////////////////////////////     4 Debugging ....
    /*public static void Show(this UiState pObj)
	{
		(" State ...... " + pObj.ToString() ).HtLog();
	} */
    //  ////////////////////////////////////////////////     4 Debugging ....
    public static string ShowBool (this bool pBool, string header, string tStr, string fStr)
    {
        return header + (pBool ? tStr : fStr) + "   ";
    }

    public static string ShowBool (this bool? pBool, string header, string tStr, string fStr)
    {
        if (!pBool.HasValue)
            return header + " is Null ";
        return header + (pBool.Value ? tStr : fStr) + "   ";
    }

    public static string Show (this Vector3 pVec)
    {
        return pVec.ToString ();
    }

    public static string ShowPosi (this GameObject pObj)
    {
        return pObj.transform.position.Show ();
    }

    public static string ShowPosi (this Transform pObj)
    {
        return pObj.position.Show ();
    }

    public static string LogWith (this object obj, string msg)
    {
        return " \t__" + msg + " [  " + obj.ToString () + "  ] __ ";
    }
}
