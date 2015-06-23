using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

public class IosCallBack  : MonoBehaviour
{
    public static event Dlgt_String_V mEventSend;
    //public static event Dlgt_Float_V EventTime;
    //public static event Dlgt_Bool_V mEventGuestLogin;
    //public static event Dlgt_BoolString_V mEventLoginToken;
    void Awake ()
    {
        GeneralFunction.NativeLog ("");
        GeneralFunction.NativeLog ("DontDestroyOnLoad  GameObject :: PluginIOS ");
        GeneralFunction.NativeLog ("");
        if (Fb.NotDestroyIosCallBack)
            Destroy (this.gameObject);
        else {
            DontDestroyOnLoad (this);
            Fb.NotDestroyIosCallBack = true;
        }
    }

    public void callbackSimple (string pThePara)
    {
        Debug.Log (" call back Simple >>  " + pThePara);
        mEventSend (pThePara);
    }

    public void callbackTime (string pStr)
    {
        // Xcode :: UnityUI 에 Touch 함수에서 SendMessage 하는 부분 추가. 
        try {
            GeneralFunction.NativeLog (" IosCallBack :: callbackTime >>> " + pStr);
            //EventTime( float.Parse(pStr));
        } catch (Exception ex) {
            GeneralFunction.NativeLog (" IosCallBack :: callbackTime >>>>>  ERROR ::  " + ex.ToString ());
        }
        //GeneralFunction.NativeLog (" Time from Xcode ::  " + pStr);
    }

    public void callJailBreak (string pStr)
    {
        string header = Parsing.parseThis (pStr, 0);
        GeneralFunction.LogIntense (3, true, "IosCallBack.cs :: >>  callJailBreak :: " + header);
        string jailBrk = Parsing.parseThis (pStr, 1);
        if (jailBrk == "Broken")
            Fb.JailBreakIOS = true;
        else
            Fb.JailBreakIOS = false;
        GeneralFunction.LogIntense (3, false, "IosCallBack.cs :: >>  callJailBreak :: " + jailBrk);
    }

    // UnitySendMessage("FpLogMan", "callbackLogin", "Yes" );
    //    public void callbackLogin ( string pStr ) {
    //
    //        string header = Parsing.parseThis(pStr, 0);
    //        string isYess = Parsing.parseThis(pStr, 1);
    //
    //        GeneralFunction.NativeLog( "CallBack :: callbackLogin" + header +  " , " + isYess);
    //
    //        if (header == "Guest") {
    //            if (isYess == "Yes" ) mEventGuestLogin(true);
    //            else mEventGuestLogin(false);
    //        } else {
    //            string token = Parsing.parseThis(pStr, 2);
    //            if (isYess == "Yes" ) mEventLoginToken(true, token);
    //            else mEventLoginToken(false, "NoToken");
    //        }
    //    }
    //
    //    public void callUserInfo ( string pString ) {
    //        Debug.Log("callUserInfo:: >>  " + pString);
    //
    //        FpUser aUser = new FpMyself();
    //        aUser.mID = Parsing.parseThis(pString, 0);
    //        aUser.mNick = Parsing.parseThis(pString, 1);
    //        aUser.mProfUrl = Parsing.parseThis(pString, 2);
    //
    //        mEventFpUser(aUser);
    //    }
    //
    //    public void callSingleFriend( string pStr) {
    //        Debug.Log("callFriends:: >>  " + pStr);
    //
    //        FpUser aUser = new FpUser();
    //        string header = Parsing.parseThis(pStr, 0);
    //
    //        if (header == "Init") {
    //            Fp.InitFpMyself();
    //            return;
    //        }
    //
    //        aUser.mID = Parsing.parseThis(pStr, 1);
    //        aUser.mNick = Parsing.parseThis(pStr, 2);
    //        aUser.mProfUrl = Parsing.parseThis(pStr, 3);
    //
    //        if (header == "AppFriend")
    //            Fp.mySelf.arrFriends.Add(aUser);
    //        else
    //            Fp.mySelf.arrFriendsNoGame.Add(aUser);
    //    }
    //
    //    public void callEventFriends( string pStr ) {
    //        GeneralFunction.LogIntense(3, true, "callEventFriends");
    //        mEventFriends(Fp.mySelf);
    //        GeneralFunction.LogIntense(3, false, "callEventFriends");
    //    }
    //
}

public class Parsing : MonoBehaviour
{
    // Target should not contain _*00*_    Just start with _*01*_  #### IMPORTANT
    public static string parseThis (string pTarget, int pIdx)
    {
        //Debug.Log("Parsing :: parseThis  >>  \n\n");
        // "abcdefg_*01*_ekekgefe_*02*_ekflekfjel"
        string seprtr, sepEnd;
        if (pIdx < 10)
            seprtr = "_*0" + pIdx + "*_";
        else
            seprtr = "_*" + pIdx + "*_";
        if (++pIdx < 10)
            sepEnd = "_*0" + pIdx + "*_";
        else
            sepEnd = "_*" + pIdx + "*_";     //Debug.Log("Separators >>" + seprtr +"_,   _" + sepEnd);

        int idx = pTarget.IndexOf (seprtr);
        int lst = pTarget.IndexOf (sepEnd);    //Debug.Log("Two Index .. " + idx + ",   " + lst);

        if (idx < 0 && lst < 0)
            return pTarget; // "Init" like case ...

        if (idx < 0 && lst > 1) // First member.. index Not found
            return pTarget.Substring (0, lst);
        if (lst < 0) // Last member
            return pTarget.Substring (idx + 6);
        return pTarget.Substring (idx + 6, lst - idx - 6); // Normal case..
    }
}