using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using SocketIOClient;
using SocketIOClient.Eventing;
using SocketIOClient.Messages;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  NodeGameBase  _____  Class  _____
public class NodeGameBase
{
    public string msgType;

    public virtual string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKeyValue ("msgType", msgType, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }
}

public class NodeGameObject : NodeGameBase
{
    public AmUser MyAmUser, EnAmUser;

    public Dictionary<string, string> GetJsonExchangeInfo ()
    {
        Dictionary<string, string> rDic = new Dictionary<string, string> ();

        rDic ["msgType"] = "Exchange";

        rDic ["User"] = MyAmUser.ToNodeAmUserStr ().DodgeJsonWithStr (" ` ");  //DodgeBackSlashQuoMark ();
        Ag.LogStartWithStr (3, "  NodeGameObject :: GetJsonExchangeInfo        User :   " + rDic ["User"]);

        string sArr = "";
        if (MyAmUser.arrItem.Count == 0) {  // Item
            sArr = "[]";
        } else {
            for (int k = 0; k < 20; k++) {
                string curStr = "";
                curStr = MyAmUser.arrItem [k].WAS.ToJsonStr ();
                sArr += curStr;
                if (k != 19)   //(MyAmUser.arrItem.Count - 1))
                  sArr += ",";
            }
            sArr = sArr.AddSqreBrakt ();
        }
        rDic ["arrItemWas"] = sArr.DodgeJsonWithStr (" ` ");
        Ag.LogString ("  NodeGameObject :: GetJsonExchangeInfo        arrItemWas :   " + rDic ["arrItemWas"]);

        return rDic;


        sArr = "";
        if (MyAmUser.arrUniform.Count == 0) { // Uniform
            sArr = "[]";
        } else {
            for (int k = 0; k < MyAmUser.arrUniform.Count; k++) {
                string curStr = "";
                curStr = MyAmUser.arrUniform [k].WAS.ToJsonStr ();
                sArr += curStr;
                if (k != (MyAmUser.arrUniform.Count - 1))
                    sArr += ",";
            }
            sArr = sArr.AddSqreBrakt ();
        }
        rDic ["arrUniform"] = sArr.DodgeJsonWithStr (" ` ");
        Ag.LogString ("  NodeGameObject :: GetJsonExchangeInfo        arrUniform :   " + rDic ["arrUniform"]);


        Ag.LogIntense (2, false);

        return rDic;
    }

    public bool ParseExchange (JSONNode pData, AmUser pEnemy)
    {
        Ag.LogStartWithStr (3, "NodeGameObject  ::  Parse Exchange   ");
        pEnemy = EnAmUser = new AmUser ();

        Ag.LogString (" >>>   User   <<<     " + pData ["User"].ToString ());

        JSONNode user = JSON.Parse (pData ["User"].ToString ().RecoverFromDodgeStr (" ` ", true));
        EnAmUser.WAS.KkoID = user ["KkoID"];
        EnAmUser.WAS.KkoNick = user ["KkoNick"];
        EnAmUser.KkoNickEncode = user ["KNickEncode"];
        Ag.LogString ("   >>>   " + EnAmUser.WAS.KkoID + EnAmUser.WAS.KkoNick);

        Ag.LogString (" >>>   Item   <<<     " + pData ["arrItemWas"]);
        JSONNode arrItm = JSON.Parse (pData ["arrItemWas"].ToString ().RecoverFromDodgeStr (" ` ", true));
        for (int k = 0; k < arrItm.Count; k++) {
            Ag.LogString (arrItm [k].ToString ());
            try {
                AmItem itm = new AmItem ();
                itm.ParseFrom (arrItm [k]);
                itm.ShowMyself ();
                EnAmUser.arrItem.Add (itm);
            } catch {
                Ag.LogString ("                 ::  >>>    Item Parse   !!!!!     E R R O R    !!!!! ", pWichtig: true);
            }
        }
        Ag.LogNewLine (2);

        Ag.LogString (" >>>   Uniform   <<<     " + pData ["arrUniform"]);
        arrItm = JSON.Parse (pData ["arrUniform"].ToString ().RecoverFromDodgeStr (" ` ", true));
        for (int k = 0; k < arrItm.Count; k++) {
            Ag.LogString (arrItm [k].ToString ());
            AmUniform nUni = new AmUniform ();
            EnAmUser.arrUniform.Add (nUni);
            try {
                nUni.Parse (arrItm [k]);
                nUni.WasParseColorStringToKickKeepObj ();
            } catch {
                Ag.LogString ("                 ::  >>>    ParseColorInfo   !!!!!     E R R O R    !!!!! ", pWichtig: true);
            }
        }
        Ag.LogString ("NodeGameObject  ::  Parse Exchange           >>>>>      >>>>>    ");
        Ag.LogIntense (5, false);
        return true;
    }

    public string GetJsonUser ()
    {
        return "  ";
    }

    public bool ParseUser ()
    {
        return true;
    }
}