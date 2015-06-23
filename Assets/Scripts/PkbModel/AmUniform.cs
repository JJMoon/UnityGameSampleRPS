// [2013:11:25:MOON<Start>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;
using SimpleJSON;

public class WasUniform
{
    public int uniformId, applyFlag;
    public string itemTypeId = "UniformID", colorInfo = " color ", textureInfo = "   Reserved   ";

    public void Parse (JSONNode jsNd)
    {
        uniformId = jsNd ["uniformId"].AsInt;
        applyFlag = jsNd ["applyFlag"].AsInt;
        itemTypeId = jsNd ["itemTypeId"];
        colorInfo = jsNd ["colorInfo"];
        textureInfo = jsNd ["textureInfo"];
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKV2 ("uniformId", uniformId, "applyFlag", applyFlag);
        SendStr = SendStr.AddKV3 ("colorInfo", colorInfo, "textureInfo", textureInfo, "itemTypeId", itemTypeId, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }

    public void ShowMyself ()
    {
        //Ag.LogString (string.Format ("[WasUniform] : uniformId={0} \t typeId={1} \t  Flay={2} ",
        //  uniformId, itemTypeId, applyFlag));
        Ag.LogString (string.Format ("[WasUniform] : text={0} \t\t color={1}", textureInfo, colorInfo));
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  AmUniform  _____  Class  _____
public class AmUniform : AmObject
{
    public string uniformName;
    public bool applying;
    public PlyerKind Kick = new PlyerKind (), Keep = new PlyerKind ();
    public WasUniform WAS = new WasUniform ();
    public bool mustUpdate = false;
    //  _////////////////////////////////////////////////_    _____   PlyerKind   _____   Class   _____
    public class PlyerKind : JSONNode
    {
        //  _////////////////////////////////////////////////_    _____   UnifProp   _____   Class   _____
        public class UnifProp
        {
            public int Texture, ColMain, ColSub;

            public string ToJsString ()
            {
                string SendStr = "";
                SendStr = SendStr.AddKV3 ("Texture", Texture, "ColMain", ColMain, "ColSub", ColSub, false);
                SendStr = SendStr.AddParen ();
                return SendStr;
            }

            public void SetVal (int T, int Cm, int Cs)
            {
                Texture = T;
                ColMain = Cm;
                ColSub = Cs;
            }
        }

        public UnifProp Shirt = new UnifProp (), Pants = new UnifProp (), Socks = new UnifProp ();

        public string ToJsString ()
        {
            string SendStr = "";
            SendStr = SendStr.AddArrayKV3 ("Shirt", Shirt.ToJsString (), "Pants", Pants.ToJsString (), "Socks", Socks.ToJsString (), false);
            SendStr = SendStr.AddParen ();
            return SendStr;
        }

        public void SetValue (int ST, int SCm, int SCs, int PT, int PCm, int PCs, int KT, int KCm, int KCs)
        {
            Shirt.SetVal (ST, SCm, SCs);
            Pants.SetVal (PT, PCm, PCs);
            Socks.SetVal (KT, KCm, KCs);
        }

        public string MyString ()
        {
            return string.Format ("[Tex / Col] : Shirt= {0} / {1} / {2} , \t Pants= {3} / {4} / {5} , \t Socks= {6} / {7} / {8} ", 
                Shirt.Texture, Shirt.ColMain, Shirt.ColSub, Pants.Texture, Pants.ColMain, Pants.ColSub, Socks.Texture, Socks.ColMain, Socks.ColSub);
        }
    }

    public bool Parse (JSONNode pJson)
    {
        try {
            Ag.LogStartWithStr (1, pJson.ToString () + "    _  _ _  _ _  _ _  _ ");
            try {
                WAS.Parse (pJson);
                //WAS = JsonMapper.ToObject<WasUniform> (pJson.ToJson ());
            } catch {
                pJson ["colorInfo"] = " color ";
                pJson ["textureInfo"] = " Reserved ";
                //WAS = JsonMapper.ToObject<WasUniform> (pJson.ToJson ());
                SetColorInfoString ();
            }
            WasParseColorStringToKickKeepObj ();
        } catch {
            return false;
        }
        WAS.ShowMyself ();
        ShowMySelf ();
        return true;
    }

    public void WasParseColorStringToKickKeepObj ()
    {
        try {
            JSONClass js = (JSONClass)JSON.Parse (WAS.colorInfo.RecoverFromDodge ());
            JSONClass jsK = (JSONClass)js ["Kick"];
            JSONClass jsP = (JSONClass)js ["Keep"];

            //("  WasParseColorStringToKickKeepObj ::  dodged Json >>>  " + jsK.ToString ()).HtLog ();
            //("  WasParseColorStringToKickKeepObj ::  dodged Json >>>  " + jsP.ToString ()).HtLog ();

            if (jsK.ToString ().Length < 10) {
                Ag.mVirServer.SetUniform (1, this);
                SetColorInfoString ();
                return;
            }

            Kick.SetValue (jsK ["Shirt"] ["Texture"].AsInt, jsK ["Shirt"] ["ColMain"].AsInt, jsK ["Shirt"] ["ColSub"].AsInt, 
                jsK ["Pants"] ["Texture"].AsInt, jsK ["Pants"] ["ColMain"].AsInt, jsK ["Pants"] ["ColSub"].AsInt, 
                jsK ["Socks"] ["Texture"].AsInt, jsK ["Socks"] ["ColMain"].AsInt, jsK ["Socks"] ["ColSub"].AsInt);
            Keep.SetValue (jsP ["Shirt"] ["Texture"].AsInt, jsP ["Shirt"] ["ColMain"].AsInt, jsP ["Shirt"] ["ColSub"].AsInt, 
                jsP ["Pants"] ["Texture"].AsInt, jsP ["Pants"] ["ColMain"].AsInt, jsP ["Pants"] ["ColSub"].AsInt, 
                jsP ["Socks"] ["Texture"].AsInt, jsP ["Socks"] ["ColMain"].AsInt, jsP ["Socks"] ["ColSub"].AsInt);
            //ShowMySelf ();
        } catch {
            Ag.LogString (" Uniform  ::   colorInfo is not set !!!!!   catch ....   Ag.mVirServer.SetUniform   ");
            Ag.mVirServer.SetUniform (1, this);
            SetColorInfoString ();
        }
    }

    public void SetColorInfoString ()
    {
        string playerKindStr = ""; // Kic, Keep 객체를 스트링으로 
        playerKindStr = playerKindStr.AddArray ("Kick", Kick.ToJsString ());
        //playerKindStr.HtLog ();

        playerKindStr = playerKindStr.AddArray ("Keep", Keep.ToJsString ()); // JsonMapper.ToJson (Keep), false);
        //playerKindStr.HtLog ();
        playerKindStr = playerKindStr.AddParen ();
        //playerKindStr.HtLog ();
        WAS.colorInfo = playerKindStr.DodgeJson ();
        //playerKindStr.HtLog ();

        mustUpdate = true;
        ("  SetColorInfoString :: colorInfo >>  " + WAS.colorInfo).HtLog ();
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKeyValue ("uniformId", WAS.uniformId);
        SendStr = SendStr.AddKeyValue ("itemTypeId", WAS.itemTypeId);
        SendStr = SendStr.AddKeyValue ("applyFlag", WAS.applyFlag);
        SetColorInfoString ();
        SendStr = SendStr.AddKeyValue ("colorInfo", WAS.colorInfo);
        SendStr = SendStr.AddKeyValue ("textureInfo", WAS.textureInfo, false);
        SendStr = SendStr.AddParen ();
        Ag.LogString ("  a WasUniform ::: " + SendStr);
        return SendStr;
    }

    public void ShowMySelf ()
    {
        //(WAS.colorInfo).HtLog ();
        Ag.LogString (" Kicker : " + Kick.MyString ());
        Ag.LogString (" Keeper : " + Keep.MyString ());
    }
}