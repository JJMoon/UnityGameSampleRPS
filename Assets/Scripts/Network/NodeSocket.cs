using UnityEngine;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using SocketIOClient.Messages;
using SimpleJSON;

public class NodeBooleans
{
    public bool Mine, Enem, UI;

    public bool AllTrue { get { return Mine && Enem; } }

    public void InitSet (bool pV)
    {
        UI = Mine = Enem = pV;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Node Socket  _____  Class  _____
public partial class NodeSocket : JsonParser
{
    public Client MySocket;
    public bool IsRandom;
    public AmUser MyUser, EnemyUser;
    public NodeBooleans GameStartMsg = new NodeBooleans (), SceneLoadComplete = new NodeBooleans (), ReMatchSent = new NodeBooleans (), ReMatchRefuseSend = new NodeBooleans ();
    string userNick, gameMsgCode = "None";
    string channelID;
    //, mCurState = "None";
    public string sessionKey;
    public List<NodeUser> arrFriends;
    public List<NodeGameBase> arrGameSend, arrGameRcvd;
    public bool? IsGameHost;
    //public bool JoinMatched;
    public Action dlgtDisConnected, dlgtConnectFailed;
    public Action dlgtConnect, dlgtInvited, dlgtJoinAsVisitor, dlgtGameStartAsHost, dlgtReverseHostVstr;
    public Action dlgtGameGotResult, dlgtIleft, dlgtEnemyLeft, dlgtIamRefused;
    public Dlgt_String_V InviteErrorOf;
    // Private ...
    public NodeUser CurEnemy;
    public Timer sysTimer;
    AgTime MatchingTimer;
    // Temp...
    //  _////////////////////////////////////////////////_    _____  Generation  _____   ^^  _____
    public NodeSocket (string theName)
    {
        mName = theName;
        //MySocket = new SocketIOClient.Client ("http://221.143.21.32:80/");
        MySocket = new SocketIOClient.Client (AgStt.mNodeURI);

        arrGameSend = new List<NodeGameBase> ();
        arrGameRcvd = new List<NodeGameBase> ();

        arrFriends = new List<NodeUser> ();

        //client.Opened += SocketOpened;
        //MySocket.Message += SocketMessage1;  // 수신 메시지 받는 부분
        //client.SocketConnectionClosed += SocketConnectionClosed; // 에러 나서 disable 시킴.
        //client.Error +=SocketError;  // 에러 나서 disable 시킴.

        MySocket.HeartBeatFailure += () => {
            Ag.LogDouble ("MySocket.HeartBeatFailure     Action ");
            Ag.NetExcpt.NodeConnectionLossAct ();
        };

        MySocket.On ("connect", (fn) => {
            //mCurState = "Connected";
            dlgtConnect ();  //__ Delegate ... Events
            Ag.LogDouble (" NodeSocket  " + mName + "is Connected ....  "); //CurState :: " + mCurState);
        });

        //MySocket.On ("disconnect", ResDisconnected); // connect_failed  이벤트 >> https://github.com/LearnBoost/socket.io/wiki/Exposed-events#client << 참고.
        MySocket.On ("error", EvntError);

        //MySocket.On ("heartbeat_timeout", ResDisconnected);
        //MySocket.On ("connect_failed", ConnectionFailed);  //  "connect_failed" is emitted when socket.io fails to establish a connection to the server and has no more transports to fallback to

        MySocket.On ("RES_USER", ResUser);
        MySocket.On ("RES_USERMOD", ResUserMod);
        MySocket.On ("RES_FRIENDS", ResFriends);
        //MySocket.On ("RES_TMPFRIENDS", ResFriends);
        MySocket.On ("RES_INVITE", ResInviteMirror);
        MySocket.On ("INVITE", ResInviteFromFriend);
        MySocket.On ("RES_JOIN", ResJoinMirror);  // Random Matching, Invite
        MySocket.On ("JOIN", ResJoinOrMatched); 
        MySocket.On ("INVITEREFUSE", ResRefuse); 

        MySocket.On ("GAMEMSG", ResGameMsg);
        //MySocket.On ("GAMEMSG", ParseGameMessage);

        MySocket.On ("RES_GAMEMSG", ResGameMsgMirror);
        MySocket.On ("RES_RANDOM", ResRandom); // Random Matching
        MySocket.On ("RES_LEAVE", ResLeaveMirror);
        MySocket.On ("LEAVE", ResLeave);

        //dlgtInvited += ActionJoin; // 초대 받으면 바로 조인함.
        dlgtDisConnected += EventSign;

        //dlgtDisConnected += Ag.NetExcpt.NodeConnectionLossAct;
        //dlgtConnectFailed += Ag.NetExcpt.NodeConnectionLossAct;

        dlgtConnectFailed += EventSign;
        dlgtIamRefused += EventSign;
        dlgtInvited = null; // += EventSign;
        dlgtGameGotResult += EventSign;
        dlgtEnemyLeft += EventSign;
    }

    public void EvntError (IMessage theData)
    {
        Ag.LogDouble ("     EvntError (IMessage theData)");
        theData.MessageText.HtLog ();
    }

    public void ResDisconnected (IMessage theData)
    {
        Ag.LogDouble ("     ResDisconnected (IMessage theData)");
        Ag.NetExcpt.NodeConnectionLossAct ();
        //dlgtDisConnected ();
    }

    public void ConnectionFailed (IMessage theData)
    {
        Ag.LogDouble ("     ConnectionFailed (IMessage theData)");
        Ag.NetExcpt.NodeConnectionLossAct ();
        //dlgtConnectFailed ();
    }

    public void ConnectSocket ()
    {
        MySocket.Connect ();
    }

    public void EventSign ()
    {
        Ag.LogString (mName + "    NodeSocket :: Event  .... ");
    }

    public void CloseSocket ()
    {
        //mCurState = "DisConnect";
        TimerStop ();
        MySocket.Close ();
        Ag.LogStartWithStr (5, " NodeSocket :: CloseSocket   " + mName + "   is Closing   !!!! End  ....  ");//CurState :: " + mCurState);
    }

    private void SocketMessage1 (object sender, MessageEventArgs e)
    {
        Ag.LogStartWithStr (5, " SocketMessage (object sender, MessageEventArgs e)   called >>>>> " + e.ToString ()); 
        if (e != null && e.Message.Event == "message") {
            string msg = e.Message.MessageText;

            Ag.LogString (" Received ::: >>>" + msg);  // 이게 안 나와요.. 
        }
    }

    public void TimerSet () // private...
    {
        if (sysTimer != null) {
            sysTimer.Stop ();
            sysTimer = null;
            //"  Timer  Reset !!! ".HtLog ();
        }
        sysTimer = new Timer ();
        sysTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
            //Ag.LogString ("   Net Failure       >>>>>    ");
            sysTimer.Enabled = false;
            sysTimer = null;
            "  NodeSocket  >>>   TimerSet ... Elapsed .. ::    sysTimer = null;  ".HtLog ();
            Ag.NetExcpt.NodeConnectionLossAct ();
        };
        sysTimer.Interval = 10000; // milli second ..
        sysTimer.Enabled = true;
    }

    public void TimerStop () // private...
    {
        if (sysTimer != null) {
            sysTimer.Stop ();
            sysTimer = null;
        }
    }

    private void SocketOpened (object sender, MessageEventArgs e)
    {
        //invoke when socket opened
    }
    //  _////////////////////////////////////////////////_    _____   Methods   _____   Prepare Game   _____
    public void PrepareNewGame ()
    {
        Ag.LogDouble ("   NodeSocket :: PrepareNewGame ");
        arrGameRcvd = new List<NodeGameBase> ();
        arrGameSend = new List<NodeGameBase> ();

        ReMatchSent.InitSet (false);
        //xxNoOpponentReceived = null;

        //AgStt.EnemyNodePrepareObj.arrDirectionInfo.Clear ();
        if (IsGameHost.Value) {

            Ag.LogIntenseWord ("  NodeSocket :: PrepareNewGame    I am Host ....   Session Key : " + sessionKey);

            AgStt.MyNodePrepareObj = new NodeGamePrepare ();

            AgStt.MyNodePrepareObj.sessionKey = sessionKey = MyUser.WAS.GameSessionKey;

            ActionGamePrepare (AgStt.MyNodePrepareObj);
        } 
        Ag.GameStt.StartGamePacket ("  I am  Host ?  " + IsGameHost.Value + " ...    Prepare New Game   ");
    }
    //  _////////////////////////////////////////////////_    _____   User   _____   Regist / Modify   _____
    public void ActionUser ()
    {
        Ag.LogIntense (2, true);
        Ag.LogString (mName + "  ActionUser    Emit  ...  \t\t\t\t <<<   Action   >>>");


        MySocket.Emit ("USER", GetCurrentNodeUserDic (statusOnly: false));
        TimerSet ();
        Ag.LogIntense (1, false);
    }

    public Dictionary<string, string> GetCurrentNodeUserDic (bool statusOnly, string statusGame = "ONLINE")
    {
        string kkoNick = MyUser.WAS.KkoNick == null || MyUser.WAS.KkoNick.Length == 0 ? "No name" : MyUser.WAS.KkoNick;
        NodeUser noUser = new NodeUser () {
            id = Ag.mGuest ? MyUser.DeviceID : MyUser.WAS.KkoID,
            country = MyUser.WAS.Country,
            teamName = MyUser.WAS.TeamName,
            league = MyUser.WAS.League,
            kkoNick = Ag.mGuest ? "No name" : MyUser.WAS.KkoNick,
            profileURL = Ag.mGuest ? "NO" : MyUser.WAS.profileURL,
            rankObj = MyUser.myRank.WAS,
            gameStatus = statusGame
        };
        return noUser.GetDicObj (statusOnly);
    }

    public void ActionUserModify (string statusGame, bool statusOnly = true) // MATCHING / ONLINE
    {
        Ag.LogIntense (2, true);
        Ag.LogString (mName + "  ActionUserModify    Emit  ...  \t\t\t\t <<<   Action  statusOnly : " + statusOnly + " >>>");

        MySocket.Emit ("USERMOD", GetCurrentNodeUserDic (statusOnly, statusGame)); // noUser.GetDicObj ());
        TimerSet ();
        Ag.LogIntense (1, false);
    }

    void ResUser (IMessage theData)
    {
        TimerStop ();
        Ag.LogIntense (1, true);

        ActionUserModify ("ONLINE", true);

        Ag.LogString (mName + "  ResUser    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<     " + RcntRcvdName + "  Status " + Status);
        Ag.LogIntense (2, false);
        theData.ToString ().HtLog ();
    }

    void ResUserMod (IMessage theData)
    {
        TimerStop ();
        Ag.LogIntense (1, true);
        Ag.LogString (mName + "  ResUserMod    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        Ag.LogString (" >>>  " + theData.MessageText);
        Ag.LogIntense (2, false);
    }
  
    bool NoOpponentProcess (string pCode)
    {
        if (pCode != "ERR_NOOPPONENT")
            return false; // no problem

        Ag.GameStt.EnemyOrMyselfLeftActionFromNode (myself: false, pComent: "  No Opponent Process ");

        //Ag.NetExcpt.NodeConnectionLossAct ();
        //IsGameHost = null;
        //dlgtIleft ();
        Ag.LogIntenseWord ("   No Enemy ... he's gone ...   NoOpponentProcess   ");
        return true;
    }
    //  _////////////////////////////////////////////////_    _____   Leave   _____   Leave   _____
    public void ActionLeave ()
    {
        Ag.LogIntense (2, true);
        Ag.LogString (mName + "  ActionLeave    Emit  ...  \t\t\t\t <<<   Action :: Leave ....  Game Finish  !!!!!!!!!    >>>");
        //if (JoinMatched)
        MySocket.Emit ("LEAVE", null);
        Ag.LogString (mName + "  ActionLeave    Emit  ...  \t  Done .... ");
        Ag.LogIntense (1, false);
    }

    void ResLeaveMirror (IMessage theData)
    {
        Ag.LogIntense (1, true);
        Ag.LogString (mName + "  ResLeaveMirror    Rcvd  ...  \t\t\t\t >>>   Re Action   I left   <<<");
        Ag.LogString (" >>>  " + theData.MessageText);

        Ag.GameStt.EnemyOrMyselfLeftActionFromNode (myself: true, pComent: "  I Left .. ");

        dlgtIleft ();
        Ag.LogIntense (2, false);
    }

    void ResLeave (IMessage theData)
    {
        Ag.LogIntense (1, true);
        Ag.LogString (mName + "  ResLeave    Rcvd  ...  \t\t\t\t >>>   Re Action   Enemy left   <<<");
        Ag.LogString (" >>>  " + theData.MessageText);

        ActionLeave ();  // I leave too ...!!! 

        if (Ag.mFriendMode != 1)
            Ag.mySelf.CoolTimeChooseOneMoreGameWin ();

        if (Ag.GameStt.WillSendWasGameReport) {
            WasGameReport aObj = new WasGameReport () {
                User = Ag.mySelf, winnerID = Ag.mySelf.WAS.KkoID, loserID = Ag.NodeObj.EnemyUser.WAS.KkoID,
                winPo = (int)Ag.NodeObj.myGameLogic.CurAccumTotal, losPo = 0
            };
            aObj.messageAction = (int pInt) => {
                EnemyUser = null;
                CurEnemy = null;
            };
        } else {
            EnemyUser = null;
            CurEnemy = null;
        }
        
        Ag.GameStt.EnemyOrMyselfLeftActionFromNode (myself: false, pComent: " Enemy Left case   ..."); 

        if (dlgtEnemyLeft != null)
            dlgtEnemyLeft ();
        Ag.LogIntense (2, false);
    }
    //  _////////////////////////////////////////////////_    _____   Friend   _____   Invite   _____
    public void ActionInvite (string toWhom)
    {
        Ag.LogIntense (2, true);
        Ag.LogString (mName + "  ActionInvite    Emit  ...  \t\t\t\t <<<   Action :: Invite   >>>");

        Dictionary<string, string> dInv = new Dictionary<string, string> ();
        dInv ["to_user_id"] = toWhom;
        //NodeInvite noUser = new NodeInvite (toWhom);

        MySocket.Emit ("INVITE", dInv);

        Ag.GameStt.NodeInviteOrRandomAction ("ActionInvite"); // EnemyLeft = false; JoinMatched = false;

        TimerSet ();
        Ag.LogIntense (1, false);
    }

    void ParseCurEnemy (JSONNode pData)
    {
        try {
            CurEnemy = new NodeUser (pData); // JsonMapper.ToObject<NodeUser> (pData);
            CurEnemy.ParseIntArr ();
        } catch {
            Ag.LogIntenseWord (mName + "  NodeSocket :: ParseCurEnemy      >>>>>   E R R O R   >>>>>");
        }
        Ag.LogString (mName + "  NodeSocket :: ParseCurEnemy >>  Enemy Info  :: > " + CurEnemy.ToString ());
    }

    void ResInviteFromFriend (IMessage theData)
    {
        TimerStop ();
        Ag.LogStartWithStr (2, mName + "  __ ResInviteFromFriend    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        JSONNode argsJ = GetArguments (theData, mName); // JsonMapper.ToObject (dataJ ["args"] [0].ToJson ());

        //ParseCurEnemy (argsJ ["channel"] ["user2"].ToJson ());
        ParseCurEnemy (argsJ ["from_user"]);

        if (dlgtInvited != null)
            dlgtInvited ();  //__ Delegate ... Events  .. Action Join..
        IsGameHost = null; // will be set 'false' @ ResJoinMirror
        Ag.GameStt.InvitedFromFriend ("   ResInviteFromFriend   ");
        Ag.LogIntense (2, false);
    }

    void ResInviteMirror (IMessage theData)
    {
        TimerStop ();
        Ag.LogIntense (1, true);
        Ag.LogString (mName + "  ResInvite    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        Ag.LogString (" >>>  " + theData.MessageText);
        JSONNode argsJ = GetArguments (theData, mName); // JsonMapper.ToObject (dataJ ["args"] [0].ToJson ());

        string status = argsJ ["status"];
        if (status == "FAIL") {
            if (argsJ ["code"] == "ERR_NOSUCHUSER") // ERR_NOSUCHUSER  
                InviteErrorOf ("OFFLINE");
            else // ERR_USERONCHANNEL
                InviteErrorOf ("RANDOM");
        }

        IsGameHost = true;
        Ag.LogIntense (2, false);
    }
    //  _////////////////////////////////////////////////_    _____   Random   _____   Matching   _____
    public void RandomMatching (int pFilter1Same2League)
    {
        Ag.LogStartWithStr (2, mName + "  RandomMatching    Emit  ...  \t\t\t\t <<<   Action :: RandomMatching >>>");
        Ag.GameStt.NodeInviteOrRandomAction ("RandomMatching"); //        EnemyLeft = false; JoinMatched = false;
        MatchingTimer = new AgTime ();
        MatchingTimer.WaitTimeFor (0, 0, 5);
        Dictionary<string, int> dRandom = new Dictionary<string, int> ();
        dRandom ["filter"] = pFilter1Same2League;
        //NodeRandom nObj = new NodeRandom () { filter = pFilter1Same2League };
        MySocket.Emit ("RANDOM", dRandom); //nObj);
        // TimerSet ();  상대방이 호스트일 때 엇갈리면 안올 수 있다. 제외.
        Ag.LogIntense (1, false);
    }

    public bool DidMatchingTimerFinish ()
    {
        //Ag.LogString ("  Timer :: " + MatchingTimer.SecondsLeft() + "  Finish : ? " + MatchingTimer.DidTimerFinished ());
        if (MatchingTimer == null)
            return true;
        return MatchingTimer.DidTimerFinished ();
    }

    public void ResRandom (IMessage theData)
    {
        TimerStop ();
        Ag.LogStartWithStr (1, mName + "  __ ResRandom    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        JSONNode argsJ = GetArguments (theData, mName);
        Ag.LogString (mName + "    arg :: >>   " + argsJ.ToString () + "     opp_user :  " + argsJ ["opp_user"].ToString ());
        // if (argsJ ["willThisDie"] == null)  // no die...
        if (argsJ ["opp_user"] == null || argsJ ["opp_user"].ToString ().IsJsonNull ()) { // 상대가 없을 때 : ResJoin 을 기다림.
            Ag.LogString (mName + "   ResRandom  :: > " + " No Enemy Yet  ...  ");
            IsGameHost = true;
        } else {  // 상대가 있을 때 : 게임 시작.
            ParseCurEnemy (argsJ ["opp_user"]);
            IsGameHost = false;

            Ag.GameStt.GameMatchedAllCase (pRandom: true, pComent: "ResRandom");   // JoinMatched = true;
        }
        Ag.LogIntense (1, false);
    }
    //  _////////////////////////////////////////////////_    _____   Friend   _____   Join / Refuse   _____
    public void ActionJoin ()
    {
        Ag.LogStartWithStr (2, mName + "  ActionJoin    Emit  ...  \t\t\t\t <<<   Action :: Join >>>");
        if (CurEnemy == null || CurEnemy.channel_id.Length < 3)
            return;
        NodeJoin nObj = new NodeJoin (CurEnemy.channel_id);

        Ag.GameStt.NodeInviteOrRandomAction ("ActionJoin"); // EnemyLeft = false; JoinMatched = false;

        MySocket.Emit ("JOIN", nObj);
        TimerSet ();
        Ag.LogIntense (1, false);
    }

    void ResJoinOrMatched (IMessage theData)
    {
        TimerStop ();
        Ag.LogStartWithStr (2, mName + "  ResJoinOrMatched    Rcvd  ...  \t\t\t\t >>>   Re Action   Matched    ... !!!! <<<");
        JSONNode argsJ = GetArguments (theData, mName);

        ParseCurEnemy (argsJ ["channel"] ["user2"]);

        Ag.GameStt.GameMatchedAllCase (pRandom: argsJ ["channel"] ["is_random"].AsBool, pComent: "ResJoinOrMatched"); // JoinMatched = true;

        if (IsGameHost.Value) // Random Matching
            ExchangeInfo ();

        if (dlgtGameStartAsHost != null)
            dlgtGameStartAsHost ();

        Ag.LogIntense (5, false);
    }

    void ResJoinMirror (IMessage theData)
    {
        TimerStop ();
        Ag.LogStartWithStr (1, mName + "  ResJoinMirror    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        Ag.LogString (" >>>  " + theData.MessageText);
        IsGameHost = false;

        //JoinMatched = true;

        dlgtJoinAsVisitor ();
        Ag.LogIntense (2, false);
    }

    public void ActionRefuse (AmUser pMe)
    {
        Ag.LogStartWithStr (2, mName + "  ActionRefuse    Emit  ...  \t\t\t\t <<<   Action :: Join >>>");

        Dictionary<string, string> dRef = new Dictionary<string, string> ();
        dRef ["invite_from_user_id"] = CurEnemy.id;
        dRef ["invite_to_user_id"] = pMe.WAS.KkoID;
        dRef ["invite_channel_id"] = CurEnemy.channel_id;
                
        MySocket.Emit ("INVITEREFUSE", dRef);
        Ag.LogIntense (1, false);
    }

    void ResRefuse (IMessage theData)
    {
        TimerStop ();
        Ag.LogIntense (1, true);
        Ag.LogString (mName + "  ResRefuse    Rcvd  ...  \t\t\t\t >>>   My invitation is  .....  Refused  !!!!!... !!!! <<<");
        Ag.LogString (" >>>  " + theData.MessageText);
        dlgtIamRefused ();
        Ag.LogIntense (2, false);
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Send in Dic  _____  GameMsg  _____
    public void ActionGameStartMsg ()
    {
        Ag.LogStartWithStr (2, mName + "  ActionGameStartMsg    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");
        SendGameMsg (new NodeGameStartMsg ());
        SceneLoadComplete.InitSet (false);
        Ag.LogString (mName + "  ActionGameStartMsg    Emit  ...  \t\t\t\t <<<  Done >>>");
    }

    public void ActionSceneLoadComplete ()
    {
        Ag.LogStartWithStr (2, mName + "  ActionSceneLoadComplete    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");
        SendGameMsg (new NodeGameStartMsg ("SceneLoadComplete"));
        SceneLoadComplete.Mine = true;
        Ag.LogString (mName + "  ActionSceneLoadComplete    Emit  ...  \t\t\t\t <<<  Done >>>");
    }

    public void GoldenBallEvent ()
    {
        // BallKind = "NONE"; // GOLD SILVER BRONZE
        SendGameMsg (new NodeGameGoldenBall ("GOLD"));
    }
    //  _////////////////////////////////////////////////_    _____   GameMsg   _____   Re Match   _____
    public void RematchApply ()
    {
        Ag.LogStartWithStr (2, mName + "  RematchApply    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");

        SendGameMsg (new NodeRematchApply ());

        ReMatchSent.Mine = true;
        Ag.LogString (mName + "  RematchApply    Emit  ...  \t\t\t\t <<<  Done >>>");
        Ag.LogIntense (1, false);
    }

    public void RematchRefuse ()
    {
        Ag.LogStartWithStr (2, mName + "  RematchRefuse    Emit  ...  \t\t\t\t <<<   Action :: GameMsg >>>");

        SendGameMsg (new NodeRematchRefuse ());

        ReMatchRefuseSend.Mine = true;
        Ag.LogString (mName + "  RematchRefuse    Emit  ...  \t\t\t\t <<<  Done >>>");
        Ag.LogIntense (1, false);
    }
    //  _////////////////////////////////////////////////_    _____   Game   _____   Prepare   _____
    public void ActionGamePrepare (NodeGamePrepare gameObj)
    {
        Ag.LogStartWithStr (2, mName + "  ActionGamePrepare    Emit  ...  \t\t\t\t <<<   Action :: ActionGamePrepare   >>>");

        SendGameMsg (gameObj);
    }

    public void SendMyUserInfo (NodeAmUser amUser)
    {
        Ag.LogStartWithStr (2, mName + "  SendMyUserInfo    Emit  ...  \t\t\t\t <<<   Action :: SendMyUserInfo   >>>");
        SendGameMsg (amUser);
    }
    //  _////////////////////////////////////////////////_    _____   Emit   _____   Action   _____
    public void ActionGameObj (NodeGameBase gameObj)
    {
        Ag.LogIntense (2, true);
        Ag.LogString (mName + "  ActionGameObj    Emit  ...  \t\t\t\t <<<   Action :: Game   >>>");

        if (gameObj.msgType != "REVERSE")
            arrGameSend.Add (gameObj);
        SendGameMsg (gameObj);
        Ag.LogIntense (1, false);
    }
    //  _////////////////////////////////////////////////_    _____   Friends   _____   List   _____
    public void xxActionTempFriends ()
    {
        Ag.LogStartWithStr (2, mName + "  ActionTempFriends    Emit  ...  \t\t\t\t <<<   Action   >>>");
        Dictionary<string, string> aDic = new Dictionary<string, string> ();
        //aDic ["Key"] = "None";

        MySocket.Emit ("TMPFRIENDS", aDic);
        Ag.LogString (mName + "  ActionTempFriends    Emit  ...  \t\t\t\t <<<   Action    Done   >>>");
        Ag.LogIntense (1, false);
    }

    public void ActionFriends (string friendsIDs)
    {
        Ag.LogStartWithStr (2, mName + "  ActionFriends    Emit  ...  \t\t\t\t <<<   Action   >>>");
        NodeFriends frds = new NodeFriends ();
        frds.AddFriend (friendsIDs);

        Dictionary<string, string> aDic = new Dictionary<string, string> ();
        aDic ["friend_user_id"] = frds.friend_user_id;
        //Ag.LogString ("Friends >> " + frds.friend_user_id);

        MySocket.Emit ("FRIENDS", aDic);

        Ag.LogIntense (1, false);
    }

    public void ResFriends (IMessage theData)
    {
        TimerStop ();
        Ag.LogStartWithStr (1, mName + "  ResFriends    Rcvd  ...  \t\t\t\t >>>   Re Action   <<<");
        JSONNode argsJ = GetArguments (theData, mName); // JsonMapper.ToObject (dataJ ["args"] [0].ToJson ());
        string status = argsJ ["status"];
        int frdNum = argsJ ["friends"].Count;
        JSONNode friends = argsJ ["friends"];

        Ag.LogString ("status :: >>   " + status + " ,         My Friend Num is :::   " + frdNum);

        arrFriends = new List<NodeUser> ();
        for (int k = 0; k < frdNum; k++) {
            Ag.LogString ("       ________  . . . . . . . . .    A Friend :: " + friends [k]);
            NodeUser aFrnd = new NodeUser (friends [k]); // = JsonMapper.ToObject<NodeUser> (friends [k]);
            if (aFrnd.ParseIntArr ())
                arrFriends.Add (aFrnd);
            Ag.LogString ("       ________  . . . . . . . . .     >>>>    Parsed  :: " + aFrnd.ToString ());
            //Ag.LogString ("       ________  " + k + "    A Friend :: " + aFrnd.ToString ());
        }
        Ag.LogIntense (2, false);
    }
}
