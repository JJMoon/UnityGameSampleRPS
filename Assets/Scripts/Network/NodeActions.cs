using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Messages;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Node Socket  _____  Class  _____
public class NodeActions : AmObject
{
    public NodeSocket MySocket;
    public int Direction, Skill;
    public AmCard CurMyCard;

    public AmUser EnemyUser { get { return MySocket.EnemyUser; } set { MySocket.EnemyUser = value; } }

    public bool IsRandom { get { return MySocket.IsRandom; } set { MySocket.IsRandom = value; } }

    public AmUser MyUser { get { return MySocket.MyUser; } set { MySocket.MyUser = value; } }
    // 게임 시작 버튼을 눌렀는 지 확인 Ag.NodeObj.GameStartMsgSent.Mine 또는 Enem
    public NodeBooleans GameStartMsgSent  { get { return MySocket.GameStartMsg; } set { MySocket.GameStartMsg = value; } }
    // 리매치를 눌렀는 지 확인 Ag.NodeObj.ReMatchSent.Mine 또는 Enem
    public NodeBooleans ReMatchSent  { get { return MySocket.ReMatchSent; } set { MySocket.ReMatchSent = value; } }

    public NodeBooleans ReMatchRefuseSent  { get { return MySocket.ReMatchRefuseSend; } set { MySocket.ReMatchRefuseSend = value; } }
    // 양측이 모두 게임 시작 버튼을 눌렀음.
    public bool ReceiveGameStartMsgBoth { get { return GameStartMsgSent.AllTrue; } }
    // 양측이 모두 <리매치> 신청을 함.
    public bool RematchBoth { get { return ReMatchSent.AllTrue; } }

    public bool ReMatchRefusedBoth { get { return ReMatchRefuseSent.AllTrue; } }

    public string SessionKey { get { return MySocket.sessionKey; } set { MySocket.sessionKey = value; } }

    public string EnemyLeague { get { return MySocket.CurEnemy.league; } }

    public bool? AmHost {
        get { return MySocket.IsGameHost; }
        set { MySocket.IsGameHost = value; }
    }

    public bool? GameFinish;
    public List<NodeGameBase> arrGamePack = new List<NodeGameBase> ();
    //public int TurnNum { get { return MySocket.arrGameSend.Count; } }
    public int TurnNum;
    public AmGameLogic myGameLogic, enGameLogic;

    public List<NodeUser> arrOnlineFriends { 
        get {
            if (MySocket.arrFriends == null)
                return null;
            return MySocket.arrFriends;
        } 
    }

    public NodeActions (AmUser myAmUser)
    {
        MySocket = new NodeSocket (myAmUser.WAS.KkoID);

        MyUser = myAmUser;

        Ag.LogIntenseWord ("NodeActions ::  Creating   >>>>>>>     " + MySocket.mName + "      <<<<<<<   ");

        MySocket.dlgtConnect += () => {
            MySocket.ActionUser ();
        };

        MySocket.dlgtJoinAsVisitor += () => {
            if (AmHost.HasValue)
                Ag.LogString (MySocket.mName + "_____  AmHost.HasValue    _____   " + AmHost.Value + " will be set as                 false   ");
            AmHost = false;
        };

        MySocket.dlgtReverseHostVstr += () => {
            Ag.LogIntenseWord (MySocket.mName + "   MySocket.dlgtReverseHostVstr += () =>  From " + AmHost);
            AmHost = !AmHost;
        };

        MakeConnect ();
    }

    public bool AnyProblemInGame ()
    {
        if (enGameLogic == null || myGameLogic == null)
            return true;
        return false;
    }

    public void GetEnemyDirectSkill (out int direct, out int skill)
    {
        if (MySocket.arrGameRcvd.Count == 0) {
            direct = skill = 0;
            return;
        }
        NodeGameTurnRslt enemRslt = (NodeGameTurnRslt)MySocket.arrGameRcvd.GetLastMember ();
        direct = enemRslt.direction;
        skill = enemRslt.skill;
    }
    //  _////////////////////////////////////////////////_    _____  Game  _____    Prepare Games    _____
    public void PrepareGame ()
    {
        Ag.LogStartWithStr (3, "  PrepareGame ::  EnemyUser  " + Ag.NodeObj.MySocket.CurEnemy.id + "   MyUser    " + MyUser.WAS.League + "    NodeLeague    " + Ag.NodeObj.MySocket.CurEnemy.league);
        myGameLogic = new AmGameLogic (MyUser.WAS.League, Ag.NodeObj.MySocket.CurEnemy.league);  // Array 객체 초기화
        enGameLogic = new AmGameLogic (Ag.NodeObj.MySocket.CurEnemy.league, MyUser.WAS.League);
        MySocket.PrepareNewGame ();
        GameFinish = null;

        Ag.LogDouble ("  PrepareGame ::  >>>>  ");

        GameStartMsgSent.InitSet (false); // 게임 시작 메시지 초기화.
        ReMatchRefuseSent.InitSet (false);
        ReMatchSent.InitSet (false);

        Ag.LogDouble ("  PrepareGame ::  InitSet  >>>>  ");

        // Set Delegates
        if (AmHost.Value)
            MySocket.dlgtGameGotResult = () => { // 결과가 갔다 왔슴.
            };
        else
            MySocket.dlgtGameGotResult = () => {
                SendGameTurnResult (TurnNum);
            };

        Ag.LogDouble ("  PrepareGame ::  >>>>  dlgt ");

        MySocket.dlgtEnemyLeft = MySocket.ActionLeave;
        MySocket.dlgtIleft = MySocket.EventSign;

        Ag.LogDouble ("  PrepareGame ::  >>>>  end ");
    }

    public bool CancelRandomStartGameWithBot ()
    {
        //if (Ag.GameStt.ExchangeParsedForGominjung)
        //  return false;
        MySocket.ActionLeave ();
        StartGameWithBotProcess ();
        return true;
    }

    public void StartGameWithBotProcess ()
    {
        PrepareGameBot ();
        MySocket.CurEnemy = new NodeUser ();
        MySocket.CurEnemy.rankObj = Ag.myEnem.myRank.WAS;
        EnemyUser = Ag.myEnem;
        AmHost = true;
        //GameStartMsgSent.Enem = true;
    }

    public void PrepareGameBot ()
    {
        Ag.mSingleMode = true;
        //MySocket.IsGameHost = true;  // bot
        GameStartMsgSent.InitSet (false);
        ReMatchRefuseSent.InitSet (false);
        ReMatchSent.InitSet (false);
        UserModify ("MATCHING", statusOnly: true);
        Ag.GameStt.GameMatchedWithBot (pComent: " Bot Game ");
        myGameLogic = new AmGameLogic (MyUser.WAS.League, "PRO_5");  // Array 객체 초기화
        enGameLogic = new AmGameLogic ("PRO_5", MyUser.WAS.League);
        MySocket.arrGameRcvd = new List<NodeGameBase> ();
        MySocket.arrGameSend = new List<NodeGameBase> ();

        Ag.myEnem.BotUniformCardRankItemSetting ();
    }

    public NodeUser GetAFriend (string kkoID)
    {
        for (int k = 0; k < MySocket.arrFriends.Count; k++) {

            if (kkoID == MySocket.arrFriends [k].id)
                return MySocket.arrFriends [k];
        }
        return null;
    }

    public bool? MatchOKwith (string friendID)
    {
        for (int k = 0; k < MySocket.arrFriends.Count; k++) {
            NodeUser curFrnd = MySocket.arrFriends [k];
            if (curFrnd.id == friendID) {
                if (curFrnd.gameStatus == "ONLINE")
                    return true;
                else
                    return false;
            }
        }
        return null;
    }
    //  _////////////////////////////////////////////////_    _____  Actions  _____    Random Matching   _____
    public void RandomMatching (int pFilter1Same2League)
    {
        MySocket.RandomMatching (pFilter1Same2League);
    }
    //  _////////////////////////////////////////////////_    _____  Actions  _____    Invite Friend   _____
    public void UserModify (string pStatus, bool statusOnly)
    {
        MySocket.ActionUserModify (pStatus, statusOnly);
    }

    public void InviteAFriend (string nodeFriend)
    {
        NodeUser usr = GetFriend (nodeFriend);
        if (usr == null)
            return;
        MySocket.ActionInvite (usr.id);
        AmHost = true;
    }

    public void InviteRefuse ()
    {
        MySocket.ActionRefuse (Ag.mySelf);
    }
    //  _////////////////////////////////////////////////_    _____  Actions  _____    Game   _____
    public void Rematch ()
    {
        MySocket.RematchApply ();
    }

    public void SendRematchRefuse ()
    {
        MySocket.RematchRefuse ();
    }

    public void GoldenBallEvent ()
    {
        if (AmHost.Value)
            MySocket.GoldenBallEvent ();
    }

    public void LeaveMyself ()
    {
        EnemyUser = null;
        MySocket.CurEnemy = null;

        MySocket.ActionLeave ();
        MySocket.dlgtEnemyLeft = MySocket.EventSign;
    }

    public void HostGameTurn (int turnN)
    {
        Ag.LogIntenseWord (" NodeAction :: HostGameTurn (int turnN)  " + turnN + "     Host ? " + MySocket.IsGameHost.Value);
        TurnNum = turnN;
        if (MySocket.IsGameHost.Value) {
            MySocket.sessionKey = MyUser.WAS.GameSessionKey;
            SendGameTurnResult (turnN);
        }
    }

    void GetTotalScore (out float myScore, out float enScore)
    {
        myScore = myGameLogic.GetTotalScore ();
        enScore = enGameLogic.GetTotalScore ();
    }

    public void StartGameMsg ()
    {
        GameStartMsgSent.Mine = true;
        if (Ag.mSingleMode)
            return;
        MySocket.ActionGameStartMsg ();
    }

    public void GameTurnBot (int turnN, AmCard EnemCard)
    {
        //Ag.LogIntenseWord ("  Value at Bot   " + " " + Direction + " " + " " + Skill + " " + Ag.mgEnemDirec + " " + Ag.mgEnemSkill);
        //Ag.LogDouble (" NodeActions :: GameTurnBot   ...  turn : " + turnN + "  EnemCard : " + EnemCard.WAS.grade + "  /  " + EnemCard.WAS.level);
        NodeGameTurnRslt gameTurn = new NodeGameTurnRslt () {
            roll = Ag.mgIsKick ? "KICK" : "KEEP",
            direction = Direction,
            skill = Skill,
            turnNum = turnN,
            sessionKey = MySocket.sessionKey,
            grade = CurMyCard.WAS.grade,
            level = CurMyCard.WAS.level
        };
        MySocket.arrGameSend.Add (gameTurn);
        NodeGameTurnRslt enemTurn = new NodeGameTurnRslt () {
            roll = !Ag.mgIsKick ? "KICK" : "KEEP",
            direction = Ag.mgEnemDirec,
            skill = Ag.mgEnemSkill,
            turnNum = turnN,
            sessionKey = MySocket.sessionKey,
            grade = EnemCard.WAS.grade,
            level = EnemCard.WAS.level
        };
        MySocket.arrGameRcvd.Add (enemTurn);

        Ag.LogIntenseWord (" NodeActions :: GameTurnBot   arrGameSend.count : " + MySocket.arrGameSend.Count + "  Rcvd : " + MySocket.arrGameRcvd.Count);
    }

    void SendGameTurnResult (int turnN)
    {
        Ag.LogIntense (2, true);
        //string myRoll = ((TurnNum % 2 == 1) ^ (MySocket.IsGameHost.Value)) ? "KICK" : "KEEP";
        string myRoll; // = ((TurnNum % 2 == 1) ^ (!MySocket.IsGameHost.Value)) ? "KICK" : "KEEP";
        if (TurnNum % 2 == 1)
            myRoll = MySocket.IsGameHost.Value ? "KICK" : "KEEP";
        else
            myRoll = MySocket.IsGameHost.Value ? "KEEP" : "KICK";

        MyUser.WAS.GameSessionKey = MySocket.sessionKey;

        NodeGameTurnRslt gameTurn = new NodeGameTurnRslt () {
            roll = myRoll,
            direction = Direction,
            skill = Skill,
            turnNum = turnN,
            sessionKey = MySocket.sessionKey,
            grade = CurMyCard.WAS.grade,
            level = CurMyCard.WAS.level
        };
        Ag.LogString (MySocket.mName + " NodeAction :: GameTurn  " + TurnNum + "        My Roll is    " + myRoll);
        Ag.LogString (MySocket.mName + " NodeAction :: GameTurn  Send   >>>  my <Dir/Skl>  :  <<< " + Direction + " / " + Skill + " >>>");
        MySocket.ActionGameObj (gameTurn);
        Ag.LogString (MySocket.mName + " NodeAction :: GameTurn   _____ Sent   !!! ");
        Ag.LogIntense (2, false);
    }

    public void GameScoreAddNewTurn (int[] pUnifCstmInfo = null)
    {  // 1 at MySocket.arrGameSend, MySocket.arrGameRcvd   each ...
        Ag.LogStartWithStr (7, "  NodeAction  ::  GameScoreAddNewTurn    ");
        bool isMyKick;
        Ag.LogString ("         IsGameHost ...  " + MySocket.IsGameHost);

        isMyKick = ((TurnNum % 2 == 0) ^ (MySocket.IsGameHost.Value));
        Ag.LogString (MySocket.mName + " NodeActions :: GameScore . . . . . . . .  ________________________________ ");
        Ag.LogString (MySocket.mName + " NodeActions :: GameScore . . . . . . . .  " + TurnNum.LogWith ("Turn") + MySocket.IsGameHost.LogWith ("Host? ") + isMyKick.LogWith (" MyKick? "));

        Dlgt_Gen_Obj_Bool<NodeGameBase> dlgtTurnRslt = ( NodeGameBase pObj) => { 
            return pObj.msgType == "TURN_RSLT";
        };

        NodeGameTurnRslt kickNodeRslt, keepNodeRslt;
        NodeGameTurnRslt myRslt = (NodeGameTurnRslt)MySocket.arrGameSend.GetLastMemberWithCond (dlgtTurnRslt);
        NodeGameTurnRslt enRslt = (NodeGameTurnRslt)MySocket.arrGameRcvd.GetLastMemberWithCond (dlgtTurnRslt);

        Ag.LogString ("         arrGameSend.count : " + MySocket.arrGameSend.Count + "      Rcvd : " + MySocket.arrGameRcvd.Count);

        bool kickerWin;
        if (isMyKick) {
            Ag.LogString ("   >>>   I am Kicker   ");
            kickNodeRslt = myRslt;
            keepNodeRslt = enRslt;
        } else {
            Ag.LogString ("   >>>   I am Keeper   ");
            kickNodeRslt = enRslt;
            keepNodeRslt = myRslt;
        }

        kickerWin = AgUtilGame.DidKickerWinThisTurn (kickNodeRslt.direction, kickNodeRslt.skill, keepNodeRslt.direction, keepNodeRslt.skill);  // 골/노골 결정

        Ag.LogString (isMyKick.ShowBool (" I am ", "Kicker", "Keeper") + kickerWin.ShowBool (" Turn : ", "GoalIn", "NoGoal") + "       My Dir/Skl " + myRslt.direction + " / " + myRslt.skill +
        "       Enem  Dir/Skl " + enRslt.direction + " / " + enRslt.skill);
        Ag.LogString ("  Direction in Ag..  Dir / Skl :  " + Ag.mgDirection + "  /  " + Ag.mgSkill);

        int[] myUnCm, enUnCm;

        if (pUnifCstmInfo == null) {
            myUnCm = new int[] { 1, 1, 1, 0 };
            enUnCm = new int[] { 1, 1, 1, 0 };
        } else {
            myUnCm = new int[] { pUnifCstmInfo [0], pUnifCstmInfo [1], pUnifCstmInfo [2], pUnifCstmInfo [3] };
            enUnCm = new int[] { pUnifCstmInfo [4], pUnifCstmInfo [5], pUnifCstmInfo [6], pUnifCstmInfo [7] };
        }

        myGameLogic.AddNewTurn (myRslt, enRslt, 0, myUnCm);
        enGameLogic.AddNewTurn (enRslt, myRslt, 0, enUnCm);

        GameFinish = myGameLogic.DidIFinalWin (enGameLogic);

        if (GameFinish.HasValue) {
            Ag.LogIntenseWord (" Game is Finished !!!!!   ");
            float mS, mE;
            GetTotalScore (out mS, out mE);
            Ag.LogString (MySocket.mName + " Total Score :: " + mS + "  /  " + mE);
        }

        int enemD, enemS;
        GetEnemyDirectSkill (out enemD, out enemS);

        Ag.LogString (MySocket.mName + "   Enemy Direct : " + enemD + "   Skill : " + enemS);
        Ag.LogString (MySocket.mName + "   " + MySocket.IsGameHost.ShowBool (" I am ", "Host", "Vstr") + "   Kicker Win ?  " + kickerWin);
        Ag.LogString (MySocket.mName + "   My Score : " + myGameLogic.CurScore + " Enemy Score : " + enGameLogic.CurScore);
        Ag.LogString (MySocket.mName + "   Me : Enemy Goals   << " + myGameLogic.GetGoalNumber () + "  :  " + enGameLogic.GetGoalNumber () + GameFinish.ShowBool (" GameFinish :", "Win", "Lose"));
        Ag.LogString (MySocket.mName + "   NodeActions :: GameScore . . . . . . . .  ________________________________    End   ________");
        Ag.LogIntense (7, false);
    }
    //  _////////////////////////////////////////////////_    _____  Actions  _____    Host Decision   _____
    public void FlipCoin () // 선공 정하기
    {
        int rand = AgUtil.RandomInclude (11, 112);
        if (rand % 2 == 0)
            return;
        AmHost = !AmHost;
        NodeGamePreReverseHostVstr gamePre = new NodeGamePreReverseHostVstr ();
        MySocket.ActionGameObj (gamePre);
    }

    public void FriendsInfo (string pFriends)
    {
        MySocket.ActionFriends (pFriends);
//        MySocket.ActionTempFriends ();
    }
    //  _////////////////////////////////////////////////_    _____  Connection  _____    Socket   _____
    private void MakeConnect ()
    {
        MySocket.ConnectSocket ();
    }

    public void CloseNet ()
    {
        MySocket.CloseSocket ();
    }

    NodeUser GetFriend (string userID)
    {
        foreach (NodeUser frnd in arrOnlineFriends) {
            if (frnd.id == userID)
                return frnd;
        }
        return null;
    }
}