using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using SimpleJSON;

public partial class Test : AmSceneBase
{
    string friendList = "88712330645978192,91278098233517152,88894476708738001,90060594732486160,88159078716546208,00000690633939999";
    int chNum = 10;

    public  void SetColumnNodeA ()
    {
        muiCol = 0;
        muiRow = 1;

        int colN, colEA;
       

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Connection  _____    Ag.NodeObj   _____

        if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), " Ag.NodeObj ")) {
            AgStt.NodeOpen ();
        }

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Connection  _____    Node 1   _____
        if (node1 == null) {
            if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), "ID: " + myUser.WAS.KkoID)) {
                node1 = new NodeActions (myUser);

                node1.MySocket.MyUser = myUser;
                myUser.ShowMyself ();
                //AgStt.NodeOpen ();
                //Ag.NodeObj.MySocket.MyUser = myUser;
            } 
            return;
        } 
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), " Item " + node1.MyUser.arrItem.Count);
        // //"88894476708738001"; //"88712330645978192"; "APPS_MOOON_0001"; // "AppsTest088";//"APPS_TEST_ID_0003"; // "AppsTest043"; //  "APPS_TEST_ID_0002";
        // 88214690633939121<Rolco>  91278098233517152 <Moon iPAD>  88712330645978192 <Moon> 88159078716546208 <Cho>
        //"90060594732486160"; //"88894476708738001";//"APPS_TEST_ID_0002";  90060594732486160  88306087115705857  90060594732486160 <KimDR> 
        Rect nodeR1 = ndGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (ndGUI.DivideRect (nodeR1, colEA, colN++), "Fnd " + node1.MySocket.arrFriends.Count)) {  // Friend List
            node1.FriendsInfo (friendList);
        }

        if (GUI.Button (ndGUI.DivideRect (nodeR1, colEA, colN++), "Invite")) {
            node1.InviteAFriend (user2.WAS.KkoID);  // node2.MyUser.WAS.KkoID);
        }

//        if (GUI.Button (ndGUI.DivideRect (nodeR1, colEA, colN++), "UserMod")) {
//            node1.UserModify ("ONLINE", statusOnly: true);
//        }

//        if (GUI.Button (ndGUI.DivideRect (nodeR1, colEA, colN++), "Leave")) {
//            node1.LeaveMyself ();
//        }

        //  _////////////////////////////////////////////////_    _____  Login  _____    Net Failure    _____
        Rect rctTmr = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;


        if (GUI.Button (myGUI.DivideRect (rctTmr, colEA, colN++), "Leave")) {
            node1.LeaveMyself ();
        }

        if (GUI.Button (myGUI.DivideRect (rctTmr, colEA, colN++), "Close")) {
            AgStt.NodeClose ();
        }

//        if (GUI.Button (myGUI.DivideRect (rctTmr, colEA, colN++), "Timer")) {
//            node1.MySocket.TimerSet ();
//        }
//
//        if (GUI.Button (myGUI.DivideRect (rctTmr, colEA, colN++), "Cancel")) {
//            node1.MySocket.TimerStop ();
//        }

        //  _////////////////////////////////////////////////_    _____  Node  _____    Exchange    _____
        Rect nodeR2 = ndGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Exchange ")) {
            node1.MySocket.ExchangeInfo ();
        }
        chNum = int.Parse (GUI.TextField (ndGUI.DivideRect (nodeR2, colEA, colN++), chNum.ToString (), 5)); // GUI.TextField (myGUI.DivideRect (curRegis, 2, 0), chkTeamName, 15);
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Ex 1 ")) {
            node1.MySocket.Ex1 (chNum);
        }
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Ex 3 ")) {
            node1.MySocket.Ex3 (chNum);
        }

        if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), myUser.WAS.KkoID + "  " + node1.MySocket.arrGameSend.Count + " : S < Close > R : " +
            node1.MySocket.arrGameRcvd.Count)) {
            node1.CloseNet ();
            node1 = null;
            return;
        }
        if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), "All Friends")) {
            node1.MySocket.xxActionTempFriends ();
        }


        //
        //            Rect nodeR1a = ndGUI.GetRect (muiCol, muiRow++);
        //            colN = 0;
        //            colEA = 4;
        //            if (GUI.Button (ndGUI.DivideRect (nodeR1a, colEA, colN++), "Golden ")) {  // Friend List
        //                node1.GoldenBallEvent ();
        //            }
        //
        //            Rect nodeR2 = ndGUI.GetRect (muiCol, muiRow++);
        //            colN = 0;
        //            int val = node1.GameStartMsgSent.Mine ? 10 : 0;
        //            val += node1.GameStartMsgSent.Enem ? 1 : 0;
        //            if (GUI.Button (ndGUI.DivideRect (nodeR2, 3, colN++), "Prepr")) {
        //                node1.PrepareGame ();
        //            }
        //            if (GUI.Button (ndGUI.DivideRect (nodeR2, 3, colN++), "Start? " + node1.ReceiveGameStartMsgBoth)) {
        //                node1.StartGameMsg ();
        //            }
        //            if (GUI.Button (ndGUI.DivideRect (nodeR2, 3, colN++), "ReMch? " + node1.RematchBoth)) {
        //                node1.Rematch ();
        //            }
        //
        //            Rect nodeR11 = ndGUI.GetRect (muiCol, muiRow++);
        //            colN = 0;
        //            if (GUI.Button (ndGUI.DivideRect (nodeR11, 2, colN++), " < Set Delegate > ")) {
        //                node1.PrepareGame ();
        //            }
        //            if (GUI.Button (ndGUI.DivideRect (nodeR11, 2, colN++), " < Game Message > ")) {
        //                node1.HostGameTurn (1);
        //            }
        //            string hostStr = "Null ";
        //            if (node1.AmHost.HasValue)
        //                hostStr = node1.AmHost.Value ? " Host " : " Vstr ";
        //            if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), hostStr + node1.MySocket.arrGameSend.Count + " : S < Close > R : " +
        //                node1.MySocket.arrGameRcvd.Count)) {
        //                node1.CloseNet ();
        //                node1 = null;
        //                return;
        //            }
    
        /*
{"name":"JOIN",

"args":
[  

{"channel":

{"server_id":"1","id":"1_2014050118154511",

"user1":{"gameStatus":"ONLINE","id":"90060594732486160","teamName":"sefefesfs",
"kkoNick":"Nick2424","league":"PRO_2","contWinTime":"0","strIntArr":"5_5_0_","profileURL":"-","rankIntStr":"3000_0_4000_2_25_9_0_0_5_3_","server_id":"1",
"socket_id":"WJFXHJCXZRo0OGw7iPAK","channel_server_id":"1","channel_id":"1_2014050118154511","channel_user_no":"1","client_ip":"183.101.188.21",
"created_at":"2014-05-01T09:15:40.645Z"},

"user2":{"gameStatus":"ONLINE","id":"91278098233517152","teamName":"MMM","kkoNick":"Nickkkkk","league":"PRO_5","contWinTime":"0","strIntArr":"5_5_5_",
"profileURL":"-","rankIntStr":"0_0_3000_0_24_28_0_0_91_101_","server_id":"1","socket_id":"XwIGNsn5hlVftjW_iPAL","channel_server_id":"1","channel_id":"1_2014050118154511",
"channel_user_no":"2","client_ip":"183.101.188.21","created_at":"2014-05-01T09:15:41.070Z"},
"is_random":false,"is_waiting":false,"to_user_id":"91278098233517152","created_at":"2014-05-01T09:15:45.419Z"},

"user":{"gameStatus":"ONLINE","id":"91278098233517152","teamName":"MMM","kkoNick":"Nickkkkk","league":"PRO_5","contWinTime":"0","strIntArr":"5_5_5_","profileURL":"-",
"rankIntStr":"0_0_3000_0_24_28_0_0_91_101_","server_id":"1","socket_id":"XwIGNsn5hlVftjW_iPAL","channel_server_id":"1","channel_id":"1_2014050118154511",
"channel_user_no":"2","client_ip":"183.101.188.21","created_at":"2014-05-01T09:15:41.070Z"}

} // channel 

] // args

}

//*/


    }
}