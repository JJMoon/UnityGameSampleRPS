// [2013:1:3:MOOON] Started
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

//public delegate AmPack FunctionPointerAmPack ();
public enum BenderE
{
    NONE,
    FACEBOOK,
    KAKAO,
    LINE,
    QQ
}

public enum Url
{
    JOYPLE_LOGIN,
    JOYPLE,
    WAS
}

/// <summary>
/// Global Singleton Object AgStt
/// _ Enum / Objects 
/// _ Has a [AmNetBase Object] [AmNetGen Object]
/// </summary>

//  ////////////////////////////////////////////////     ////////////////////////     >>>>> Ag Static <<<<<
public class AgStt
{
    // General Variables
    public static BenderE CurBender = BenderE.KAKAO;
    public static HtUiHeadquater muiHQ;
    public static AmInAppBill mIAB;
    public static int JoyplePlatformID = 1; // 1: G Play, 2: iOS, 4: N, 5: T, 6: LGU+

    public static int SvrVersion, CliVersion = 11;

    //  _////////////////////////////////////////////////_    _____   Debug Log On   _____   Device   _____
    public static bool DebugOnDevice;
    //public static bool? IsGaming;
    // null, gaming = true, pauseDuringGame = false
    public static List<string> arrLogOnDevice, arrLogWichtig;
    public static int LinesInDebugButton = 15;
    public static float FramePS, FpsLast;
    public static bool IntendedPause;
    //  _////////////////////////////////////////////////_    _____   Event & Popup related   _____   bool   _____
    public static bool InAppPurchaseSuccess = false;
    //  _////////////////////////////////////////////////_    _____   IPHONE   _____   Setting   _____
    #if UNITY_IPHONE
    /// <summary>
    /// IOS 전용 객체. Android 에서는 안보임. 
    /// </summary>
    public static AmIAP mIAP = new AmIAP ();
    public static FpLoginObj FpLoginIOS = new FpLoginObj ();
    #endif
    //  _////////////////////////////////////////////////_    _____   ANDROID   _____   Setting   _____
    #if UNITY_ANDROID
    //public static AmIABdroid AmIAB = new AmIABdroid();
    public static int mIapPrice;
    #endif
    //  _////////////////////////////////////////////////_    _____   etc   _____   ..   _____
    public static bool mgGameTutorial, FromGuest2Kakao = false;
    //public static StarJune.Cryptography.JuneAES JCEcript = new StarJune.Cryptography.JuneAES ("47dad345a6475f378071ae136347027d", "ab39f2dafa2efe25734b2955d4dc6556");
    //public static UtAES AesJoyple, AesWas;
    // Constants ...
    public const int NarrowBarLimit = 195, ClientVersion = 1;
    //public static string mURI = "http://221.143.21.33/api.psy.trd";
    public static string mURI, mNodeURI, PushURI;
    public static string mReviewURI = "http://psy-review-kakao.joycity.com/api.psy.do";
    public static string mReviewNodeURI = "http://psy-review-node-kakao.joycity.com";
    public static string mServiceURI = "http://psy-kakao.joycity.com/api.psy.do";
    public static string mServiceNodeURI = "http://psy-node-kakao.joycity.com";
    public static string US_ServiceURI = "http://psy-fb.joycity.com/api.psy.do";

    public static string JceURL4Push_Service = "http://push.joyple.joycity.com";
    public static string JceURL4Push_Dev = "http://dev.push.joyple.joycity.com"; 

    // psy-review-kakao.joycity.com / psy-review-node-kakao.joycity.com

    //public static string mURI = "http://192.168.0.31/api.psy.trd"; // appsgraphy.iptime.org";
    //public static string mURI = "http://appsgraphy.iptime.org/api.psy.trd"; // appsgraphy.iptime.org";
    //  ////////////////////////////////////////////////     FB / Kakao Login Related...
    public static string mgFBUserName, mgFBid;
    public static  NodeGamePrepare MyNodePrepareObj, EnemyNodePrepareObj;
    // = new NodeGamePrepare ();
    public static NetworkManagerMono NetManager;
    // Cool Time Setting..
    public static int CTHeartGameHealth = 600, CTHeartMaxSeconds = 6000, CTHeartMaxDoubled = 12000, CTScout = 300, CTContWin = 1800; // 1800
    public static int CTFacebookPostingLimit = 24 * 3600; 
    public static float CTHeartRecoverFactor = 1f, CTScouterDeckEffectFactor = 1.15F;
    //  ////////////////////////////////////////////////     static Init ..
    static AgStt ()
    {
        Ag.LogStartWithStr (3, "Unity :: <<< AgStt.cs >>>>>   ~~~~~~~~~~~~~~~~~  Static AgStt  ");

        #if UNITY_IPHONE
        Ag.LogString ("IPhone Setting ");
        mIAP.TheUser = Ag.mySelf;
        #endif

        mURI = "http://221.143.21.33/api.psy.trd";  // Develope Server 
        mURI = "psy-dev-kakao.joycity.com/api.psy.do";  // Develope Server 

        mURI = "http://221.143.21.33/api.psy.do"; // mServiceURI;
        mNodeURI = "http://221.143.21.32"; // mServiceNodeURI;

        mURI = mServiceURI; // mServiceURI;
        mNodeURI = mServiceNodeURI; // mServiceNodeURI;
        PushURI = JceURL4Push_Dev;

        AgStt.mIAB = new AmInAppBill ();
        Ag.LogString ("   <<< AgStt.cs >>>>>   ~~~~~~~~~~~~~~~~~  Static AgStt  ");
        Ag.LogString ("  End of Ag Stt . Init ~~~ ~~~ ~~~ ~~~ ~~~ ~~~ ~~~ ");
    }

    public static void ReLoginAction ()
    {
        CTHeartMaxSeconds = 6000;
    }

    public static void SetDeviceLog ()
    {
        if (AgStt.arrLogOnDevice.Count > LinesInDebugButton * 8)
            AgStt.arrLogOnDevice.RemoveAt (0);
        if (AgStt.arrLogWichtig.Count > LinesInDebugButton * 2)
            AgStt.arrLogWichtig.RemoveAt (0);
    }

    public static void NodeOpen ()
    {
        if (Ag.NodeObj == null) {
            Ag.LogIntenseWord (" AgStt :: Node is null  ....   Open   !!!!!   ");
            Ag.NodeObj = new NodeActions (Ag.mySelf);
        } else
            Ag.LogIntenseWord (" AgStt :: Node   Open    ....  not Null     SKIP  ....     !!!!!   ");
        //AgStt.IsGaming = null;
    }

    public static void NodeClose ()
    {
        if (Ag.NodeObj == null) {
            Ag.LogIntenseWord (" AgStt :: Node Close   !!!!!     Null case ");
            return;
        }
        Ag.LogIntenseWord (" AgStt :: Node Close   !!!!!     Closing  ... ");
        Ag.NodeObj.CloseNet ();
        Ag.NodeObj = null;
    }

    public static string GetLogString (int pCol, bool Wichtig = false)
    {
        string rVal = "";

        if (pCol == 0 && Wichtig)
            rVal += "FPS : " + string.Format ("{0:F1}", FpsLast) + "    FPS (Mean) : " + string.Format ("{0:F1}", FramePS) + "\n";

        //Debug.Log (pCol + "  " + arrLogWichtig.Count + "  " +arrLogOnDevice.Count);
        int staId = pCol * AgStt.LinesInDebugButton;

        List<string> rList;
        if (Wichtig)
            rList = arrLogWichtig;
        else
            rList = arrLogOnDevice;

        for (int k = 0; k < AgStt.LinesInDebugButton; k++) {  // 10 ea ... 
            rVal += rList [staId++];
        }
        return rVal;
    }
}

