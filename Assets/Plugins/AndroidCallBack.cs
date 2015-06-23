using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

public class AndroidCallBack  : MonoBehaviour {


    #if UNITY_ANDROID

    public static event Dlgt_String_V mEventSend;
    public static event Dlgt_Bool_V mEventGuestLogin, EvntJailBreak;
    public static event Dlgt_BoolString_V mEventLoginToken;
   // public static event DlgtFpUser mEventFpUser;
   // public static event DlgtMyself  mEventFriends;
    


    void Awake()
    {
        GeneralFunction.NativeLog ("");
        GeneralFunction.NativeLog ("DontDestroyOnLoad FpLogMan ");
        GeneralFunction.NativeLog ("");
        DontDestroyOnLoad( this );
    }
    
    public void callbackSimple ( string pThePara ) {
        Debug.Log(" call back Simple >>  " + pThePara);
        mEventSend(pThePara);
    }
    
    // UnitySendMessage("FpLogMan", "callbackLogin", "Yes" );
    public void callbackLogin ( string pStr ) {
        
        string header = Parsing.parseThis(pStr, 0);
        string isYess = Parsing.parseThis(pStr, 1);

        if (header == "Guest") {
            if (isYess == "Yes" ) mEventGuestLogin(true);
            else mEventGuestLogin(false);
        } else {
            string token = Parsing.parseThis(pStr, 2);
            if (isYess == "Yes" ) mEventLoginToken(true, token);
            else mEventLoginToken(false, "NoToken");
        }
    }

    /*
    public void callUserInfo ( string pString ) {
        Debug.Log("callUserInfo:: >>  " + pString);
        
        FpUser aUser = new FpMyself();
        aUser.mID = Parsing.parseThis(pString, 0);
        aUser.mNick = Parsing.parseThis(pString, 1);
        aUser.mProfUrl = Parsing.parseThis(pString, 2);
        
        mEventFpUser(aUser);
    } 
    */
    #endif
}
