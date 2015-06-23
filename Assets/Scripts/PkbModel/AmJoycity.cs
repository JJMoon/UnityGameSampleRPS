using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;
using LitJson;
using SimpleJSON;

public class AmNotification
{
    public int idx, platform, AlreadySeenNum;
    public string title, msg, img, frequency, frequency_time;
    public bool IsFreqency;

    public virtual void ShowMySelf ()
    {
        Ag.LogString (" AmNotification : ShowMySelf >> " + idx + "  Platform : " + platform +
        " Title : " + title + "  freq : " + frequency + " , " + IsFreqency + "   freq_time : " + frequency_time);
    }
}

public class JceTextNotice : AmNotification
{
    public string  url, content, timestamp;

    public JceTextNotice (string timeStamp = "")
    {
        timestamp = timeStamp;
    }

    public bool ParseFrom (JSONNode pJson)
    {
        bool rV;
        idx = pJson ["idx"].AsInt;
        platform = pJson ["platform"].AsInt;
        rV = pJson.ParseTo ("title", out title, "url", out url) &&
        pJson.ParseTo ("frequency", out frequency, "frequency_time", out frequency_time, "content", out content);
        IsFreqency = frequency == "true";
        ShowMySelf ();

//        #if UNITY_IPHONE
//        WWW.EscapeURL(pJson["title"]).HtLog();
//        WWW.UnEscapeURL(WWW.EscapeURL(pJson["title"])).HtLog();
//
//
//        title = title.DecodeFromUtf8 ();
//        content = content.DecodeFromUtf8 ();
//        #endif

        //Ag.LogString (" JceTextNotice :: Title / content " + title + "  /  " + content);

        return rV;
    }
}

public class JceEvtBanner : AmNotification
{
    public string  url, banner_path, timestamp;

    public JceEvtBanner (string timeStamp)
    {
        timestamp = timeStamp;
    }

    public bool ParseFrom (JSONNode pJson)
    {
        idx = pJson ["idx"].AsInt;
        platform = pJson ["platform"].AsInt;
        if (pJson.ParseTo ("title", out title, "url", out url, "banner_path", out banner_path))
            return true;
        return false;
    }
}

public class JceImgNotice : AmNotification
{
    public string url, image_path, image_path2, timestamp;

    public JceImgNotice (string timeStamp)
    {
        timestamp = timeStamp;
    }

    public bool ParseFrom (JSONNode pJson)
    {
        idx = pJson ["idx"].AsInt;
        platform = pJson ["platform"].AsInt;
        pJson.ParseTo ("title", out title, "url", out url, "image_path", out image_path);
        pJson.ParseTo ("image_path2", out image_path2, "frequency", out frequency, "frequency_time", out frequency_time);
        IsFreqency = frequency == "true";
        ShowMySelf ();
        return true;
    }



}

public static class Joycity
{
    public static List<JceImgNotice> arrImageNoti = new List<JceImgNotice> ();
    public static List<JceEvtBanner> arrEvtBanner = new List<JceEvtBanner> ();
    public static List<JceTextNotice> arrTextNotice = new List<JceTextNotice> ();
    public static JceTextNotice UrgentNotice;
    // List<JceTextNotice> arrUrgentNotice = new List<JceTextNotice> ();
}