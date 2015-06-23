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

public class JsonParser : AmObject
{
    public string Status;
    public string RcntRcvdName;

    public JsonParser ()
    {
    }

    public JSONNode GetArguments (IMessage pMsg, string owner = " ^^ ")
    {
        //Ag.LogString (owner + "   _____   JsonParser :: GetArguments  >>  " + pMsg.MessageText);
        JSONNode dataJ = JSON.Parse (pMsg.MessageText);
        RcntRcvdName = dataJ ["name"];
        //Ag.LogString ("_____   JsonParser :: name  >>  " + RcntRcvdName);
        try {
            Status = (string)(dataJ ["args"] [0] ["status"]);
        } catch {
            //Ag.LogString (owner + "   _____   JsonParser  ::  No Status   __________ >>>> Catch <<<< __________       ");
            Status = "Null";
        }
        Ag.LogString (owner + "   _____   JsonParser :: GetArguments  >>  Name : " + RcntRcvdName + " ,   Status : " + Status + "   **** End ***");
        return dataJ ["args"] [0];
    }
}

public class NodeGamePrepare : NodeGameBase
{
    #region NodeGameBase  영역

    //public List<int[]> arrDirectionInfo = new List<int[]> ();
    // Direction, Width ... X 5 .. 10 ea..
    public string sessionKey;

    public NodeGamePrepare ()
    {
        msgType = "PREPARE";
    }

    public bool Parse (JSONNode jsNd)
    {
        sessionKey = jsNd ["sessionKey"];
        return true;
    }

    #endregion

    public void ShowMyself ()
    {
        Ag.LogString ("NodeGamePrepare   .   arrDirectionInfo  >>>");
//        foreach (int[] mem in arrDirectionInfo) {
//            Ag.LogString (">>>   " + mem [0] + "  " + mem [1] + "   <<<"); 
//        }
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Simple Message  _____  Class  _____
public class NodeRematchApply : NodeGameBase
{
    public NodeRematchApply ()
    {
        msgType = "RematchApply";
    }
}

public class NodeRematchRefuse : NodeGameBase
{
    public NodeRematchRefuse ()
    {
        msgType = "RematchRefuse";
    }
}

public class NodeGameStartMsg : NodeGameBase
{
    public NodeGameStartMsg (string msg = "GameStartMsg")
    {
        msgType = msg;
    }
}

public class NodeGamePreReverseHostVstr : NodeGameBase
{
    public NodeGamePreReverseHostVstr ()
    {
        msgType = "REVERSE";
    }
}

public class NodeGameGoldenBall : NodeGameBase
{
    public string BallKind;

    public NodeGameGoldenBall (string kindOfBall = "NONE")
    {
        BallKind = kindOfBall; // GOLD SILVER BRONZE
        msgType = "GOLDENBALL";
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Node Base  _____  Class  _____
public class NodeJoin
{
    public string channel_id;

    public NodeJoin (string chID)
    {
        channel_id = chID;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  NodeFriends  _____  Class  _____
public class NodeFriends
{
    public string friend_user_id = "";

    public void AddFriend (string fID)
    {
        if (friend_user_id.Length > 1)
            friend_user_id += ",";
        friend_user_id += fID;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Node Am User  _____  Class  _____
public class NodeAmUser : NodeGameBase
{
    //public AmUserWAS WAS;
    public string KkoID, KkoNick, KNickEncode, WasKey, TeamName, TeamNameEncoded, KkoPW, League, etcInfo, GameSessionKey, profileURL, contWin;
    public List<WasUniform> arrWasUniform = new List<WasUniform> ();
    public List<WasCard> arrCard = new List<WasCard> ();
    public List<WasItem> arrItemWas = new List<WasItem> ();
    public List<AmCostume> arrCostume = new List<AmCostume> ();
    public List<WasCostume> arrCostumeWas = new List<WasCostume> ();

    public NodeAmUser ()
    {
        msgType = "AmUser";
    }

    public AmUser ParseFrom (JSONNode pJData)
    {
        AmUser rUsr = new AmUser (true, "NodeEnemyAmUserParse");
        "NodeRltd :: NodeAmUser :: ParseFrom  ".HtLog ();
        try {
            rUsr.WAS.KkoID = pJData ["KkoID"]; //.ToString ().RemoveQuotationMark ();
            rUsr.WAS.KkoNick = pJData ["KkoNick"]; //.ToString ().RemoveQuotationMark ();
            rUsr.KkoNickEncode = pJData ["KNickEncode"];
            rUsr.TeamNameEncoded = pJData ["TeamNameEncoded"];
            rUsr.WAS.League = pJData ["League"];

            //Ag.NetExcpt.GamingEnemyID = rUsr.WAS.KkoID;
        } catch {
            Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    KkoID   KkoNick  !!!!!     E R R O R    !!!!! ", pWichtig: true);
        }
        if (rUsr.KkoNickEncode == "%E0%B8%B8") {
            Ag.LogIntenseWord ("   >>>  KkoNick   <<<   " + rUsr.KkoNickEncode);
            rUsr.KkoNickEncode = "..";
        }

        try {
            string contWinTryNot = pJData ["contWin"];
            rUsr.ContWinStarted = contWinTryNot == "TRY";
            Ag.LogString (" NodeRltd ::  " + rUsr.ContWinStarted.ShowBool (" ContWin ", " Try", " Not"));

        } catch {
            Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    ContWin TRY  NOT");
        }
        Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    KkoID : " + rUsr.WAS.KkoID + "   KkoNick : " + rUsr.WAS.KkoNick + "  !!!!! ");

        JSONNode itemArr = pJData ["arrItemWas"];
        ("    NodeRltd :: NodeAmUser :: ParseFrom >>          Item Array Parsing ____________" + itemArr.Count + "_____________ ").HtLog ();
        for (int k = 0; k < itemArr.Count; k++) {
            try {
                AmItem itm = new AmItem ();
                itm.ParseEnemyFrom (itemArr [k]);
                //itm.WAS = JsonMapper.ToObject<WasItem> (itemArr [k].ToJson ());
                itm.ShowMyself ();
                rUsr.arrItem.Add (itm);
            } catch {
                Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    Item Parse   !!!!!     E R R O R    !!!!! ", pWichtig: true);
            }
        }

        JSONNode uniformArr = pJData ["arrWasUniform"];
        Ag.LogStartWithStr (1, "    NodeRltd :: NodeAmUser :: ParseFrom >>          Uniform Array Parsing ____________" + uniformArr.Count + "_____________ ");
        for (int k = 0; k < uniformArr.Count; k++) {
            AmUniform nUni = new AmUniform ();
            rUsr.arrUniform.Add (nUni);
            try {
                nUni.Parse (uniformArr [k]);
                nUni.WasParseColorStringToKickKeepObj ();
            } catch {
                Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    ParseColorInfo   !!!!!     E R R O R    !!!!! ", pWichtig: true);
            }
        }

        JSONNode cardArr = pJData ["arrCard"];
        Ag.LogStartWithStr (1, "    NodeRltd :: NodeAmUser :: ParseFrom >>          Card Array Parsing ____________" + cardArr.Count + "_____________ ");
        //cardArr.ToJson ().ToString ().HtLog ();
        for (int k = 0; k < cardArr.Count; k++) {
            try {
                AmCard nCard = new AmCard ();
                nCard.WAS.WasCardParse (cardArr [k]);
                nCard.ScouterParse ();
                rUsr.arrCard.Add (nCard);
            } catch {
                Ag.LogString (" NodeRltd :: ParseFrom ::  >>>    Card Parsing Failure    !!!!!     E R R O R    !!!!! ", pWichtig: true);
            }
        }
//        JSONNode cstmdArr = pJData ["arrCostume"];  // Should be deprecate ....
//        ("    NodeRltd :: NodeAmUser :: ParseFrom >>        Costume Array Parsing ____________" + cstmdArr.Count + "_____________ ").HtLog ();
//        for (int k = 0; k < cstmdArr.Count; k++) {
//            arrCostume.Add (new AmCostume (cstmdArr [k]));
//        }

        JSONNode cstmWasArr = pJData ["arrCostumeWas"];
        ("    NodeRltd :: NodeAmUser :: ParseFrom >>        Costume Was Array Parsing ____________" + cstmWasArr.Count + "_____________ ").HtLog ();
        for (int k = 0; k < cstmWasArr.Count; k++) {
            AmCostume nObj = new AmCostume ();
            nObj.WAS.ParseFrom (cstmWasArr [k]);
            rUsr.arrCostume.Add (nObj);
        }

        rUsr.ApplyCurrentDeck ();
        //rUsr.ApplyDeckItemBeforeGame ();

        return rUsr;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  NodeGameTurnRslt  _____  Class  _____
public class NodeGameTurnRslt : NodeGameBase
{
    // GameHost, Visitore  // 게임 메시지 전달시 쓰임.. Host : 선공.
    public int direction, skill, scoreVisi, level, enchant, turnNum;
    public string roll, grade = "X", sessionKey = "No";
    // KICK / KEEP
    public NodeGameTurnRslt () // Need for .. ToObject<>
    {
        msgType = "TURN_RSLT";
    }

    public bool Parse (JSONNode jsNd)
    {
        try {
            return (jsNd.ParseTo ("direction", out direction, "skill", out skill, "scoreVisi", out scoreVisi) &&
            jsNd.ParseTo ("level", out level, "enchant", out enchant, "turnNum", out turnNum) &&
            jsNd.ParseTo ("roll", out roll, "grade", out grade, "sessionKey", out sessionKey));
        } catch {
            Ag.LogIntenseWord (" E R R O R   in  Parsing  NodeGameTurnRslt ..  ..   catch   .....   ");
            return false;
        }
    }

    public void SetValueWith (AmCard pCard)
    {
        grade = pCard.WAS.grade;
        level = pCard.WAS.level;
    }

    public override string ToJsonStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKV3 ("direction", direction, "skill", skill, "scoreVisi", scoreVisi);
        SendStr = SendStr.AddKV3 ("level", level, "enchant", enchant, "turnNum", turnNum);
        SendStr = SendStr.AddKV3 ("roll", roll, "grade", grade, "sessionKey", sessionKey, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }

    public override string ToString ()
    {
        string rVal = "  Roll: " + roll;
        return rVal + string.Format (" ||| dir={0}|skill={1}|grd={2}|lvl={2}|ench={2}  ", direction, skill, grade, level, enchant);
    }

    public void ShowMySelf ()
    {
        //Ag.LogString (this.ToString ());
        Ag.LogString (string.Format ("[NodeGameTurnRslt: roll={0}, \t grade={1}, level={2}, enchant={3} ]", 
            roll, grade, level, enchant));
    }
}
//  _////////////////////////////////////////////////_    _____  User Rltd  _____    Class   _____
public class NodeUser
{
    public int winNum = 5, losNum = 5, country = 82, contWinTime;
    public string id = "id", teamName = "teamName", kkoNick = "KNick", gameStatus = "ONLINE", league = "LEAGUE_1", strIntArr, channel_id = "-",
        profileURL, rankIntStr;
    public WasRank rankObj;

    public NodeUser ()
    {
        rankObj = new WasRank ();
        gameStatus = "ONLINE"; // "MATCHING"
    }

    public NodeUser (JSONNode jsNd)
    {
        Ag.LogString (" NodeUser :: NodeUser() ::  Parse Started  >>>     ");
        //if (Parse (jsNd))
        Parse (jsNd);
        ParseIntArr ();
    }

    public bool Parse (JSONNode jsNd)
    {
        ("  NodeUser :: Parse   >>>>     " + jsNd.ToString ()).HtLog ();
        return (
            //jsNd.ParseTo ("winNum", out winNum, "losNum", out losNum, "country", out country) &&
            jsNd.ParseTo ("id", out id, "teamName", out teamName, "kkoNick", out kkoNick) &&
            jsNd.ParseTo ("contWinTime", out contWinTime) &&
            jsNd.ParseTo ("gameStatus", out gameStatus, "league", out league) &&
            jsNd.ParseTo ("strIntArr", out strIntArr, "channel_id", out channel_id) &&
            jsNd.ParseTo ("profileURL", out profileURL, "rankIntStr", out rankIntStr)
        );
    }

    public Dictionary<string, string> GetDicObj (bool statusOnly)
    {
        Dictionary<string, string> rDic = new Dictionary<string, string> ();
        StackOfInt rInt;
        if (statusOnly) {
            rDic ["gameStatus"] = gameStatus;
            rInt = new StackOfInt (rankObj);
            rDic ["rankIntStr"] = rInt.str;

            Ag.LogDouble (" GetDicObj  statusOnly ? ::  " + gameStatus + "    rank :: " + rInt.str);

            return rDic;
        }
        rDic ["gameStatus"] = gameStatus;
        rDic ["id"] = id;
        rDic ["teamName"] = teamName;

        if (kkoNick == null || kkoNick.Length == 0)
            rDic ["kkoNick"] = "....";
        else
            rDic ["kkoNick"] = kkoNick;
        rDic ["league"] = league;
        rDic ["contWinTime"] = contWinTime.ToString ();
        rDic ["strIntArr"] = strIntArr;
        //rDic ["channel_id"] = channel_id;
        rDic ["profileURL"] = profileURL;
        StackOfInt sInt = new StackOfInt (winNum, losNum, country);
        rDic ["strIntArr"] = sInt.str;
        rInt = new StackOfInt (rankObj);
        rDic ["rankIntStr"] = rInt.str;

        Ag.LogDouble (" GetDicObj  statusOnly ? ::  " + gameStatus + " rank " + rInt.str + " sInt " + sInt.str);

        return rDic;
    }
    //    "id":"90973535271650928","teamName":"batgi","kkoNick":"김정수","gameStatus":"ONLINE","league":"PRO_5","strIntArr":"5_5_0_","channel_id":"",
    //    "profileURL":"http://th-p.talk.kakao.co.kr/th/talkp/wkeHiXE0v9/zeO2E90FY2qYqgH159OWdk/cv6w1h_110x110_c.jpg",
    //    "rankIntStr":"0_0_0_0_0_0_0_0_0_0_",
    //    "server_id":"1","socket_id":"5V8oimk5rTGcDscV1pvk","channel_server_id":"","channel_user_no":""
    public bool ParseIntArr ()
    {
        try {
            Ag.LogString (" NodeUser :: ParseIntArr ::  >>>     " + strIntArr);
            StackOfInt sInt = new StackOfInt (strIntArr);
            sInt.GetValue (out winNum, out losNum, out country);
            StackOfInt rInt = new StackOfInt (rankIntStr);
            rankObj = rInt.ParseRank ();
        } catch {
            Ag.LogIntenseWord (" E R R O R   in  Parsing  ParseIntArr .. Country, Rank .. ");
            return false;
        }
        return true;
    }

    public override string ToString ()
    {
        return string.Format ("[NodeUser: id={0}, \t teamName={1}, kkoNick={2},  \t gameStatus={3} \t league={4}, \twin/los={5}/{6} \t country={7} ]", 
            id, teamName, kkoNick, gameStatus, league, winNum, losNum, country);
    }
}
