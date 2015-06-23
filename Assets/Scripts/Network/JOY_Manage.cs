using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WAS_GM related  _____  Class  _____
public static class JCE
{
    public static string ComUrl = "https://dev-kataccts.m.joycity.com/";
    //public static string NoticeUrl = "http://dev-sys.m.joycity.com/";
    public static string NoticeUrl = "http://sys.m.joycity.com/";
    //
    //    public string Decrypt (byte[] cipher)
    //    {
    //        ICryptoTransform transform = rijndael.CreateDecryptor ();
    //        byte[] decryptedValue = transform.TransformFinalBlock (cipher, 0, cipher.Length);
    //        return unicodeEncoding.GetString (decryptedValue);
    //    }
    //
    //    public string DecryptFromBase64String (string base64cipher)
    //    {
    //        return Decrypt (Convert.FromBase64String (base64cipher));
    //    }
    //  http://www.codeproject.com/Articles/6465/Using-CryptoStream-in-C
    // http://msdn.microsoft.com/ko-kr/library/bb348142(v=vs.110).aspx
    // http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa
    public static bool WebDownloadString (WebClient pClnt, string pURL, out string oResult)
    {
        oResult = "";

        Ag.LogString ("Joy WebDownloading >> " + pURL);

        try {
            pClnt.Encoding = System.Text.Encoding.UTF8;
            oResult = pClnt.DownloadString (pURL);
        } catch (Exception ex) {
            //Ag.NetExcpt.DisconnectedWith (was:true);  // Error  ... later ..
            Ag.LogDouble ("  WebClient Exception     in   " + pURL);
            //Ag.LogDouble ("    ____  Exception  ::   " + ex.ToString ());
            oResult = "";

            return false;
        }
        return true;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Notification  _____  Setting  _____
    // >> Start 에서 실행될 부분. 앱 시작 시 노티 등록.
    // NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound);
    // >> 노티를 모두 지울 때.
    // NotificationServices.ClearRemoteNotifications ();
    public static string GetToken ()
    {
        #if UNITY_IPHONE
        if (NotificationServices.deviceToken == null || NotificationServices.deviceToken.Length < 5)
            return "ERROR";
        string token = "%" + System.BitConverter.ToString (NotificationServices.deviceToken).Replace ('-', '%');
        Ag.LogDouble (" Jce.GetToken >>   Device Token :: " + token);
        Ag.LogDouble (" Jce.GetToken >>   Device Token :: " + token.Replace ("%", "").ToLower ());
        return token.Replace ("%", "").ToLower ();
        #endif
        #if UNITY_ANDROID
        return Fb.AndroidRegistrationID;
        #endif
    }

    public static void JceNotiTokenSetting (AmUser user)
    {
        Ag.LogStartWithStr (2, "   JceNotiTokenSetting  >>      Started   .........  . . . . . . ");
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("uid", Ag.mySelf.WAS.KkoID); //"88865833564111728");
        webClient.QueryString.Add ("gcode", "113");
        #if UNITY_IPHONE
        RemoteNotificationType arrNotiType = NotificationServices.enabledRemoteNotificationTypes;
        Ag.LogString (" Device Model ::  " + SystemInfo.deviceModel);
//        if (SystemInfo.deviceModel.Substring (0, 4) == "iPad")
//            webClient.QueryString.Add ("device", "3");
//        else
        webClient.QueryString.Add ("device", "2");
        #endif
        #if UNITY_ANDROID
        webClient.QueryString.Add ("device", "1");
        #endif

        string tken = JCE.GetToken ();

        AgStt.PushURI.HtLog ();

        if (tken == "ERROR") {
            Ag.LogIntenseWord ("   Notification Token  Error    ");
            return;
        }

        webClient.QueryString.Add ("token", JCE.GetToken ());
        string result;
        string pushUrl = AgStt.PushURI + "/token/setting.json";
        if (!JCE.WebDownloadString (webClient, pushUrl, out result))
            return;
        ("   JceNotiTokenSetting  >>  Received :::   " + result).HtLog ();
        //JSONNode jnObj = JSON.Parse (result);
        return;
    }

    public static void JceNotiMessage (AmUser user, string pUrl)
    { // logout, alarmOn, alarmOff
        Ag.LogStartWithStr (2, "   JceNotiLogMessage  >>      Started   .........  . . . . . . " + pUrl);
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("uid", Ag.mySelf.WAS.KkoID);
        webClient.QueryString.Add ("gcode", "113");
        webClient.QueryString.Add ("token", JCE.GetToken ());
        string result;
        string pushUrl = AgStt.PushURI + "/token/" + pUrl + ".json";
        if (!JCE.WebDownloadString (webClient, pushUrl, out result))
            return;

        ("   JceNotiLogMessage  >>  Received :::   " + result).HtLog ();
        //JSONNode jnObj = JSON.Parse (result);
        return;
    }

    public static void JceNotiSendMessage (AmUser user, string pMsg, string title = "This is Title", string image = "http://blog.ccbcmd.edu/connection/files/2012/09/No-Smoking-Sign-K-2685.gif")
    { 
        Ag.LogStartWithStr (2, "   JceNotiSendMessage  >>      Started   .........  . . . . . . " + pMsg);
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("uid", Ag.mySelf.WAS.KkoID);
        webClient.QueryString.Add ("gcode", "113");
        webClient.QueryString.Add ("msg", pMsg);
        //#if UNITY_ANDROID
        webClient.QueryString.Add ("img", image);
        webClient.QueryString.Add ("title", title);
        //#endif
        string result;
        string pushUrl = AgStt.PushURI + "/msg/insert.json";
        if (!JCE.WebDownloadString (webClient, pushUrl, out result))
            return;

        ("   JceNotiSendMessage  >>  Received :::   " + result).HtLog ();
        //JSONNode jnObj = JSON.Parse (result);
        return;
    }
    //  _////////////////////////////////////////////////_    _____  Joyple  _____    JCE   _____
    public static void JceUrgentNoticePT (AmUser User)
    {
        Ag.LogStartWithStr (2, "   JceUrgentNoticePT  >>      Started   .........  . . . . . . ");
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("game_code", "113");
        webClient.QueryString.Add ("client_secret", "5423b620768434b2a928e2cc62af2785");

        string result;
        if (!JCE.WebDownloadString (webClient, JCE.NoticeUrl + "urgent-notice/info", out result))
            return;
        JSONNode jnObj = JSON.Parse (result);
        ("   JceUrgentNoticePT  >>  Received :::   " + result).HtLog ();

        if (AgUtil.IsNullJson (jnObj ["result"] ["info"]))
            return;

        JSONNode imgNtc = jnObj ["result"] ["info"];
        Ag.LogString (" JceUrgentNoticePT ::  Count  " + imgNtc.Count);
        JceTextNotice img = new JceTextNotice (jnObj ["timestamp"]);
        img.ParseFrom (imgNtc);
        if (img.platform == 0 || img.platform == AgStt.JoyplePlatformID)
            Joycity.UrgentNotice = img;
    }

    public static void JceTextNoticePT (AmUser User)
    {
        Ag.LogStartWithStr (2, "   JceTextNotice  >>      Started   .........  . . . . . . ");
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("game_code", "113");
        webClient.QueryString.Add ("client_secret", "5423b620768434b2a928e2cc62af2785");

        string result;
        if (!JCE.WebDownloadString (webClient, JCE.NoticeUrl + "notice/list", out result))
            return;

        Joycity.arrTextNotice.Clear ();

        JSONNode jnObj = JSON.Parse (result);
        ("   JceTextNotice  >>  Received :::   " + result).HtLog ();
        if (jnObj ["status"].AsInt == 1) {
            JSONNode imgNtc = jnObj ["result"] ["notice"];
            Ag.LogString (" JceTextNotice ::  Count  " + imgNtc.Count);
            for (int k = 0; k < imgNtc.Count; k++) {
                JceTextNotice img = new JceTextNotice (jnObj ["timestamp"]);
                img.ParseFrom (imgNtc [k]);

                if (img.platform == 0 || img.platform == AgStt.JoyplePlatformID)
                    Joycity.arrTextNotice.Add (img);
            }
        } else
            Ag.LogIntenseWord ("Joyple    JceTextNotice    Failed ....      Error      ....    ");
        return;
    }

    public static void JceImageNotice (AmUser User)
    {
        Ag.LogStartWithStr (2, "   JceImageNotice  >>      Started   .........  . . . . . . ");
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("game_code", "113");
        webClient.QueryString.Add ("client_secret", "5423b620768434b2a928e2cc62af2785");

        //NoticeUrl = "http://dev-sys.m.joycity.com/";
        //string result = webClient.DownloadString (JCE.NoticeUrl + "image-notice/list");

        Joycity.arrImageNoti.Clear ();

        string result;
        if (!JCE.WebDownloadString (webClient, JCE.NoticeUrl + "image-notice/list", out result))
            return;

        ("   JceImageNotice  >>  Received :::   " + result).HtLog ();
        JSONNode jnObj = JSON.Parse (result);
        if (jnObj ["status"].AsInt == 1) {
            JSONNode imgNtc = jnObj ["result"] ["imageNotice"];
            Ag.LogString (" JceImageNotice ::  Count  " + imgNtc.Count);
            for (int k = 0; k < imgNtc.Count; k++) {
                JceImgNotice img = new JceImgNotice (jnObj ["timestamp"]);
                img.ParseFrom (imgNtc [k]);
                if (img.platform == 0 || img.platform == AgStt.JoyplePlatformID)
                    Joycity.arrImageNoti.Add (img);
            }
        } else
            Ag.LogIntenseWord ("Joyple Login Failed ....      Error      ....    ");
        return;
    }

    public static void JceEventBanner (AmUser User)
    {
        Ag.LogStartWithStr (2, "   JceEventBanner  >>      Started   .........  . . . . . . ");
        WebClient webClient = new WebClient ();
        webClient.QueryString.Add ("game_code", "113");
        webClient.QueryString.Add ("client_secret", "5423b620768434b2a928e2cc62af2785");

        //string result = webClient.DownloadString (JCE.NoticeUrl + "banner/list");
        string result;
        if (!JCE.WebDownloadString (webClient, JCE.NoticeUrl + "banner/list", out result))
            return;

        Joycity.arrEvtBanner.Clear ();

        ("   JceEventBanner  >>  Received :::   " + result).HtLog ();
        JSONNode jnObj = JSON.Parse (result);
        if (jnObj ["status"].AsInt == 1) {
            JSONNode imgNtc = jnObj ["result"] ["banner"];
            Ag.LogString (" JceEventBanner ::  Count  " + imgNtc.Count);
            for (int k = 0; k < imgNtc.Count; k++) {
                JceEvtBanner img = new JceEvtBanner (jnObj ["timestamp"]);
                img.ParseFrom (imgNtc [k]);
                if (img.platform == 0 || img.platform == AgStt.JoyplePlatformID)
                    Joycity.arrEvtBanner.Add (img);
            }
        } else
            Ag.LogIntenseWord ("Joyple JceEventBanner Failed ....      Error      ....    ");
        return;
    }

//    public static void JceLogin (AmUser User) // Server   ..
//    {
//        WebClient webClient = new WebClient ();
//        webClient.QueryString.Add ("game_code", "113");
//        webClient.QueryString.Add ("client_secret", "5423b620768434b2a928e2cc62af2785");
//        webClient.QueryString.Add ("user_id", UTAES.AESEncrypt128 (User.WAS.KkoID));
//
//        int device = 1;
//        #if UNITY_IPHONE
//        device = 2;
//        #endif
//
//        webClient.QueryString.Add ("device_type", device.ToString ());
//
//        string result = "";
//        try {
//            if (!JCE.WebDownloadString (webClient, JCE.ComUrl + "session/login", out result))
//                return;
//        } catch {
//            Ag.LogIntenseWord ("Joyple Login Failed ....      Error      ....    ");
//        }
//        //string result = webClient.DownloadString (JCE.ComUrl + "session/login/");
//
//        ("   JceLogin  >>  Received :::   " + result).HtLog ();
//        JSONNode jnObj = JSON.Parse (result);
//
//        if (jnObj ["status"].AsInt == 1)
//            Ag.LogString ("  OK  ");
//        else
//            Ag.LogIntenseWord ("Joyple Login Failed ....      Error      ....    ");
//        return;
//    }
}



