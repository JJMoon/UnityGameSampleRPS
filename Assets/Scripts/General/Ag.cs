// VER_7_52_MOON  VER_7_61_MOON  VER_7_62_MOON 
// [2012:11:11:MOON] Single Mode...
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
public enum StorePlfm {
    GooglePlay = 1,
    Nstore
};

// 대표계정용 app id: com.joycity.KVSdev
// 엔터계정용 app id: com.joycity.kickvssavedinhouse
// Test  com.appsgraphy.psykickbattlekakao
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  static Ag  _____  Class  _____
public static class Ag
{
    public static StorePlfm CurStorePlfm = StorePlfm.GooglePlay;

    public static ArrayList arrPlayersInMarket;
    public static List<GameObject> arrPlayerPolygon;
    public static AmUser mySelf, myEnem;
    public static AvCard mViewCard = new AvCard ();
    public static NodeActions NodeObj;
    public static AmServer mVirServer;
    public static DataBaseCountry mCountryData;
    public static PlayerCostume mCostume;
    public static Dictionary<string, Dictionary<string, GameObject>> Dic;
    public static Dictionary<string, List<GameObject>> Lst;
    //public static int MixCount;
    //  _////////////////////////////////////////////////_    _____  Guest Mode  _____  ..  _____
    public static string mgLanguage, mKakaoRegenLabel, ScrnLog = "-", ScrnRcv = "-";
    //, DeviceUID;
    public static bool FriendMatchingMode = false, mIsDebug, mBgmSound, mFxSound, mBallEventAlready = false, mKickEffSound,
        mBlueItemFlag = false, mRedItemFlag = false, mGreenItemFlag = false, Uniform = false,
        mgServerLoggedIn, mgIsKick, mgDidGameFinish, mgDidWin, mgEnemGiveup, mGameStartAlready, mgDidGameAway, 
    mgGamePackReceived, mSingleMode, mIsSuspendOnPurpose, mNetPackWaiting, PlatformLogout = false, mGuest, onAuthorizedflag = false, rootingFlag = false;
    //, HttpNetworkFailure;
    public static UInt32 mgSelfWinNo, mgEnemWinNo;
    public static int  mKakaoRegenStart, mgPrevScore, mRound, mFriendNum, SingleTry, ContGameNum, BotTestSetting = -1, mFriendMode = 1;
    public static byte mgDirection, mgSkill, mgEnemDirec, mgEnemSkill, mgStepSend;
    public static float mgScrX, mgScrY;
    public static long TimeNow;
    public static DateTime DTNowTickMark = DateTime.Now;
    public static GameObject mGameObj;
    public static string CurrentScene;
    public static string CoroutineSign = "  ___   ~~~~~  C  O R O U T I N E  ~~~~~  ___";
    // Game, Menu
    #if UNITY_IPHONE
    public static bool mgIsRetina, mgIsIPhone;
    public static int mIapPrice;
    #endif
    #if UNITY_ANDROID
    
    #endif
    public static NetException NetExcpt = new NetException ();
    public static List<AmNotification> arrNoti = new List<AmNotification> ();
    public static AgGameRelated GameStt = new AgGameRelated ();
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Ag  _____  Static Class  _____
    static Ag ()
    {
        LogIntenseWord (" static Ag() ");
            
        if (mVirServer != null) {
            LogIntenseWord (" static Ag() .. Abort ..  ");
            return;
        } else
            mVirServer = new AmServer ();  // [2012:11:11:MOON] Single Mode...
        Ag.mySelf = new AmUser ();
        Ag.myEnem = new AmUser ();        //Ag.mFBobj = new AmFacebook();

        //Ag.DeviceUID = SystemInfo.deviceUniqueIdentifier;

        //SystemInfo.deviceModel          SystemInfo.deviceUniqueIdentifier        SystemInfo.operatingSystem        SystemInfo.deviceName
        
        //  ////////////////////////////////////////////////     Debugging ...
        mIsDebug = true;
        //AgNoShare.GenTempID ();
        mgScrX = Screen.width;
        mgScrY = Screen.height;
        Ag.LogString ("Ag::Init  mgScrX: " + mgScrX + ", mgScrY: " + mgScrY);
        
        //  ////////////////////////////////////////////////     Global Variables : Initial Value Setting

        Ag.mSingleMode = false;
        mgServerLoggedIn = mGameStartAlready = mNetPackWaiting = false;
        //Ag.mLoginPhase = "LP";
        //Ag.mgLanguage = Application.systemLanguage.ToString ();
        //Ag.mgLanguage = "KOR";
        Ag.mgLanguage = "KOR"; //Ag.mgLanguage.Substring (0, 3);
        if (Ag.mgLanguage != "Chi" && Ag.mgLanguage != "Kor" && Ag.mgLanguage != "Jap" &&
            Ag.mgLanguage != "Spa" && Ag.mgLanguage != "Ger")
            Ag.mgLanguage = "Eng";
        mCountryData = new DataBaseCountry ();
        //  ////////////////////////////////////////////////     Deprecate Nominee ss ....
        //  Some variables ...  put here ...
        
        #if UNITY_IPHONE
        Ag.LogString ("IPhone Setting ");



//        if (iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPhone5) {
//            Ag.mgIsRetina = true;
//            Ag.mgIsIPhone = true;
//        }
//        if (iPhone.generation == iPhoneGeneration.iPhone3GS) {
//            Ag.mgIsRetina = false;
//            Ag.mgIsIPhone = true;
//        }
        #endif
        
        #if UNITY_ANDROID
        Ag.LogString ("Android Setting ");

        //AgUtil.mAppleReviewURL = "https://play.google.com/store/apps/details?id=com.appsgraphy.psykickbattle&feature=search_result#?t=W251bGwsMSwyLDEsImNvbS5hcHBzZ3JhcGh5LnBzeWtpY2tiYXR0bGUiXQ";
        //AgUtil.mAppStoreURL = AgUtil.mAppleReviewURL;
        mgScrX = Screen.height / 2 * 3;
        mgScrY = Screen.height;
        #endif
        
        if (mgScrX < mgScrY) {
            mgScrY = mgScrX;
            mgScrX = Screen.height;
        }
        Ag.LogString ("Ag ()   Creation  .....  >>>>>>>>>>>>>>>>>>>>>  >>>>>>>>>>>>  >>>>>>>>   OK   ");

        //GitIgnoreThis.GitIgnoreSetup ();

        Ag.LogNewLine (3);
    }

    public static bool IsSmartDevice ()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
    }

    public static DateTime Now () // Current time by Server ... 
    {
        TimeSpan secon = (DateTime.Now - Ag.DTNowTickMark);
        //Ag.LogString (" DateTime.Now " + DateTime.Now + "   TickMark " + Ag.DTNowTickMark + "  TimeNow : " + TimeNow + "  Sec : " + secon.TotalSeconds);
        return Ag.UnixTimeStampToDateTime (Ag.TimeNow + secon.TotalSeconds);
    }

    public static DateTime SeoulNow { get { return Ag.Now ().AddSeconds (32400); } }

    public static int TotalSecondsHavePassedInSeoulSince (DateTime pWhen)
    {
        return (int)((SeoulNow - pWhen).TotalSeconds);
    }

    public static void SwitchStep ()
    {
        if (mgStepSend == 1)
            mgStepSend = 2;
        else
            mgStepSend = 1;
    }

    public static void Swap<T> (ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static DateTime UnixTimeStampToDateTime (long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime (1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds (unixTimeStamp).ToLocalTime ();
        return dtDateTime;
    }

    public static DateTime UnixTimeStampToDateTime (double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime (1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds (unixTimeStamp).ToLocalTime ();
        return dtDateTime;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Ag  _____  Debug Log  _____
    public static bool mDisableLog = true;
    public static string SIGN_MARK = 
        "UNITY__SIGN_MARK   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
    public static string SIGN_INTENSE = 
        //"UNITY ====================================================================================== UNITY";
        "UNITY ================================================================== UNITY";

    private static void PrintALine (string pStr, bool WichTig = false)
    {
        if (mDisableLog)
            return;
        RuntimePlatform plat = Application.platform;
        if (AgStt.DebugOnDevice) {
            if (WichTig)
                AgStt.arrLogWichtig.Add (pStr);
            else
                AgStt.arrLogOnDevice.Add (pStr);
            AgStt.SetDeviceLog ();
            return;
        }

        string theTime = "< " + DateTime.Now.Minute + " : " + DateTime.Now.Second + " >    ";

        if (plat == RuntimePlatform.OSXEditor || plat == RuntimePlatform.Android)
            Debug.Log (theTime + pStr);
        if (plat == RuntimePlatform.IPhonePlayer)
            GeneralFunction.NativeLog ("U3D_C# : " + theTime + pStr);
    }

    public static void SignIntenseLog ()
    {
        PrintALine (SIGN_INTENSE + "\n");
    }

    public static void LogString (string pStr, bool pWichtig = false)
    {
//        if (Application.platform == RuntimePlatform.OSXEditor)
//            Debug.Log (pStr + " \n");
//        else
        PrintALine (pStr + "\n", pWichtig);
    }

    public static void LogDouble (string pStr)
    {
        LogString ("");
        LogString (pStr);
        LogString ("");
    }

    public static void LogStartWithStr (int pNum, string pMsg)
    {
        LogIntense (pNum, true);
        LogString (pMsg);
    }

    public static void LogIntense (int pNum, bool pIsStart)
    {
        if (pIsStart)
            LogNewLine (pNum);
        SignIntenseLog ();
        if (!pIsStart)
            LogNewLine (pNum);
    }

    public static void LogLineIntense (string pTheLine)
    {
        LogIntense (3, true);
        LogString (pTheLine);
        LogIntense (3, false);
    }

    public static void LogWithBool (string pStr, bool pBool)
    {
        PrintALine ("Ag.LogWithBool  [ " + pStr + " ] :: BOOL:[ " + pBool + "<<<<\n");
    }

    public static void LogNewLine (int pNum)
    {
        if (mDisableLog)
            return;
        for (int i = 0; i < pNum; i++) {
            PrintALine ("\n");
        }
    }

    public static void LogIntenseWord (string pWord)
    {
        LogNewLine (3);
        SignIntenseLog ();
        SignIntenseLog ();
        PrintALine (" >>>>>>>__     " + pWord + "     __<<<<<<<\n", WichTig: true);
        Ag.SignIntenseLog ();
        Ag.SignIntenseLog ();
        LogNewLine (3);
    }

    public static void LogNewScene (string pSceneName, string pFuncName)
    {
        if (pFuncName == "Start") {
            LogNewLine (3);
            LogIntense (5, true);
            PrintALine (" Scene is Loaded >>>>>> " + pSceneName + " <<<  ~ \n");
            LogIntense (3, false);
        }
    }

    public static DateTime UnixTimeStampToDateTimeAddmili (double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime (1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddMilliseconds (unixTimeStamp).ToLocalTime ();
        return dtDateTime;
    }
}
