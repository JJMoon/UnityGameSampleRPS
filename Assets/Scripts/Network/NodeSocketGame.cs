using UnityEngine;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient.Messages;
using LitJson;
using SimpleJSON;

public partial class NodeSocket : JsonParser
{
    void ResGameMsg (IMessage theData)
    {
        //   {"name":"RES_GAMEMSG","args":[{"status":"FAIL","code":"ERR_NOOPPONENT"}]
        TimerStop ();
        Ag.LogStartWithStr (2, mName + "  ResGameMsg    Rcvd  ...  " + IsGameHost.Value.ShowBool ("I am ", "Host", "Vstr") + "\t\t\t\t >>>   Re Action   <<<");
        JSONNode argsJ = GetArguments (theData, mName);
        try {
            gameMsgCode = argsJ ["code"];
        } catch {
            Ag.LogString (mName + "  ResGameMsg ::  No code ");
        }

        if (NoOpponentProcess (gameMsgCode))
            return;

        JSONNode gData = argsJ ["data"];// JsonMapper.ToObject (argsJ ["data"]);
        string msgType = gData ["msgType"];
        gData = gData ["content"];

        string dodgeStr = gData.ToString ().DodgeBackSlashQuoMark ();
        (" ResGameMsg ::   dodgeStr  ::: " + dodgeStr).HtLog ();
        dodgeStr = dodgeStr.Substring (1, dodgeStr.Length - 2);
        gData = JSON.Parse (dodgeStr);
        (" ResGameMsg ::   gData ::: " + gData.ToString ()).HtLog ();

        Ag.LogString (mName + "  msgType    >>>>>  " + msgType + "  <<<<<   ");
        NodeGameBase theObj = null;
        switch (msgType) {
        case "GameStartMsg":
            GameStartMsg.Enem = true;
            return;
        case "SceneLoadComplete":
            SceneLoadComplete.Enem = true;
            return;
        case "RematchApply":
            ReMatchSent.Enem = true;
            return;
        case "RematchRefuse":
            ReMatchRefuseSend.Enem = true;
            return;
        case "AmUser":
            gData = JSON.Parse (dodgeStr.RecoverFromDodgeStr ("@#@"));
            Ag.LogString (" ResGameMsg :: " + gData.ToString ());

            EnemyUser = new AmUser (true, "DodgeDeviceID");
            EnemyUser = (new NodeAmUser ()).ParseFrom (gData);
            "  NodeSocket :: ResGameMsg ::     case  ''AmUser''    >>>>>     Parsing Ended .....  ".HtLog ();
            //AgStt.IsGaming = EnemyUser.Parsed = true;
            //AgStt.IsGaming = true;


            Ag.GameStt.EnemyInfoExchangeParsed (" AmUser ");


            if (!IsGameHost.Value)
                ExchangeInfo ();
            //EnemyUser.ShowMyself ();
            return;
        case "PREPARE":
            AgStt.EnemyNodePrepareObj = new NodeGamePrepare ();
            AgStt.MyNodePrepareObj = new NodeGamePrepare ();
            AgStt.EnemyNodePrepareObj.Parse (gData);
            AgStt.EnemyNodePrepareObj.ShowMyself ();
            if (!IsGameHost.Value) {
                if (AgStt.EnemyNodePrepareObj.sessionKey != null && AgStt.EnemyNodePrepareObj.sessionKey.Length > 5)
                    sessionKey = MyUser.WAS.GameSessionKey = AgStt.EnemyNodePrepareObj.sessionKey;
                ActionGamePrepare (AgStt.MyNodePrepareObj);
            }
            return;
        case "TURN_RSLT":
            Ag.LogString (mName + "  Parsing   NodeGameTurnRslt   Object   ");
            theObj = new NodeGameTurnRslt ();
            ((NodeGameTurnRslt)theObj).Parse (gData);
            //theObj = JsonMapper.ToObject<NodeGameTurnRslt> (gData.ToJson ());

            if (!IsGameHost.Value) // 받는 부분
                sessionKey = ((NodeGameTurnRslt)theObj).sessionKey;

            Ag.LogString (mName + "  Parsing Data" + theObj.ToString ());
            Ag.LogString (mName + "  Parsing   NodeGameTurnRslt   Object   ... End   ");
            break;
        case "HOST_SEND":
            Ag.LogString (mName + "  Parsing   Game Void   Received    ... End   ");
            Ag.LogIntense (2, false);
            //theObj = JsonMapper.ToObject<NodeGameHostSend> (gData.ToJson ());
            return;//break;
        case "REVERSE":
            Ag.LogString (mName + "  ResGameMsg         REVERSE ... End ");
            dlgtReverseHostVstr ();
            return;
        case "GOLDENBALL":
            Ag.LogString (mName + "  ResGameMsg         GOLDENBALL   " + gData ["BallKind"].ToString ());
            Ag.LogString (mName + "  ResGameMsg         GOLDENBALL ... End ");
            return;
        }

        Ag.LogString (mName + "  ResGameMsg         Rcvd ... End ");
        arrGameRcvd.Add (theObj);

        dlgtGameGotResult ();

        Ag.LogIntense (2, false);
    }
    //  _////////////////////////////////////////////////_    _____   Receive   _____   GameMessage   _____
    void ResGameMsgMirror (IMessage theData)
    { // Just Mirror 무시해도 됨.
        TimerStop ();
        Ag.LogStartWithStr (1, mName + "  ResGameMsgMirror    Rcvd  ...  \t\t\t\t >>>   Re Action  ___ Mirror ___   <<<");
        JSONNode argsJ = GetArguments (theData, mName);

        Ag.LogString ("argsJ   ::     " + argsJ.ToString ());

        NoOpponentProcess (argsJ ["code"]);

        if (Status != "OK")
            return;
        Ag.LogString (mName + "  ResGameMsgMirror   < Mirror >       Rcvd ... End ");
        Ag.LogIntense (1, false);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Send in Dic  _____  GameMsg  _____
    public void SendGameMsg (NodeGameBase pObj)
    {
        string content = JsonMapper.ToJson (pObj);

        Ag.LogString ("   SendGameMsg ::  content  >>  " + content);

        content = content.DodgeBackSlashQuoMark ();

        Ag.LogStartWithStr (2, mName + "  SendGameMsg    >>> " + content);
        Ag.LogString ("         Message Length >>>   content.Length   :   " + content.Length);

        Dictionary<string, string> aDic = new Dictionary<string, string> ();
        aDic ["msgType"] = pObj.msgType;
        aDic ["content"] = content;
        MySocket.Emit ("GAMEMSG", aDic);
        TimerSet ();
    }

    public void ExchangeInfo ()
    { // called from Host ...
        Ag.LogStartWithStr (2, mName + "  ExchangeInfo    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");

        SendGameMsg (MyUser.ToNodeAmUser ());

        Ag.LogString (mName + "  ExchangeInfo    Emit  ...  \t\t\t\t <<<  Done >>>");
    }

    void xxxTempxxxxxxParseGameMessage (IMessage theData)
    {
        Ag.LogStartWithStr (5, mName + "  ParseGameMessage    Rcvd  ...  " + IsGameHost.Value.ShowBool ("I am ", "Host", "Vstr") + "\t\t\t\t >>>   Re Action   <<<");
        JSONNode argsJ = GetArguments (theData, mName);
        Ag.LogString ("  argsJ  :::    " + argsJ.ToString ());
        try {
            gameMsgCode = argsJ ["code"];
        } catch {
            Ag.LogString (mName + "  ParseGameMessage ::  No code ");
        }
        switch (gameMsgCode) {
        case "ERR_NOOPPONENT":
                //xxNoOpponentReceived = true;
            dlgtIleft ();
            Ag.LogIntenseWord ("   No Enemy ... he's gone ...  ");
            return;
        }

        JSONNode gData = argsJ ["data"];// JsonMapper.ToObject (argsJ ["data"]);
        string msgType = gData ["msgType"];
        //(" ResGameMsg ::   dodgeStr  ::: " + gData.ToString ()).HtLog ();

        Ag.LogString ("     Message Type  msgType ::  " + msgType);

        GameObj.ParseExchange (gData, EnemyUser);


        //gData = gData ["content"];
//
//        string dodgeStr = gData.ToString ().DodgeBackSlashQuoMark ();
//        (" ParseGameMessage ::   dodgeStr  ::: " + dodgeStr).HtLog ();
//        dodgeStr = dodgeStr.Substring (1, dodgeStr.Length - 2);
//        gData = JSON.Parse (dodgeStr);
//        (" ParseGameMessage ::   gData ::: " + gData.ToString ()).HtLog ();
//
//        Ag.LogString (mName + "  msgType    >>>>>  " + msgType + "  <<<<<   ");
//        NodeGameBase theObj = null;
//        switch (msgType) {
//        case "GameStartMsg":
//            GameStartMsg.Enem = true;
//            return;
//           
//        }
    }

    NodeGameObject GameObj = new NodeGameObject ();

    public void ExchangeBeforeGame ()
    {
        GameObj.MyAmUser = MyUser;

        MySocket.Emit ("GAMEMSG", GameObj.GetJsonExchangeInfo ());

    }

    string aa = "1234566789_1234566789_1234566789_1234566789_1234566789_1234566789_1234566789_1234566789_1234566789_1234566789_";

    public void Ex1 (int pNum)
    {
        Ag.LogStartWithStr (2, mName + "  Ex1    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");
        Dictionary<string,string> dict = new Dictionary<string, string> ();
        string aX = "";
        for (int k = 0; k < pNum; k++) {
            aX += aa;
        }
        dict ["a"] = aX;
        MySocket.Emit ("GAMEMSG", dict);
    }

    public void Ex2 (int pNum)
    {
        Ag.LogStartWithStr (2, mName + "  Ex2    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");
        Dictionary<string,string> dict = new Dictionary<string, string> ();

        string aX = "";
        for (int k = 0; k < pNum; k++) {
            aX += aa;
        }
        dict ["ar"] = aX;
        dict ["ab"] = aX;
        dict ["aa"] = aX;
        dict ["as"] = aX;
        dict ["ae"] = aX;

        MySocket.Emit ("GAMEMSG", dict);
    }

    public void Ex3 (int pNum)
    {
        Ag.LogStartWithStr (2, mName + "  Ex3    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");
        List <string> aList = new List<string> ();
        for (int k = 0; k < pNum; k++) {
            aList.Add (aa);
        }
        MySocket.Emit ("GAMEMSG", aList);
    }

    public void Ex33 ()
    {
        Dictionary<string,string> dict = new Dictionary<string, string> ();
        string aa = "{\"someKey\":\"someValue\", \"another\":\"value alasdlkfjasdf \"}";
        dict ["a"] = "[" + aa + "," + aa + "," + aa + "]";
        dict ["b"] = "[" + aa + "," + aa + "," + aa + "]";
        MySocket.Emit ("GAMEMSG", dict);
    }
}
