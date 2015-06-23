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
    public  void SetColumnNodeB ()
    {
        muiCol++;
        muiRow = 0;
        int colN, colEA;

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Go to   _____    Was Screen   _____
        Rect rect000 = ndGUI.GetRect (muiCol, muiRow++);
        if (GUI.Button (ndGUI.DivideRect (rect000, 3, 2), "WAS >")) {
            IsNodeScreen = false;
        }

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Connection  _____    Node 2   _____
        if (node2 == null) {
            if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), "ID: " +user2.WAS.KkoID)) {
                node2 = new NodeActions (user2);
                node2.MySocket.MyUser = user2;
                node2.MyUser.ShowMyself ();
            } 
            return;
        } 
        Rect nodeR2 = ndGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Fnd " + node2.MySocket.arrFriends.Count)) {  // Friend List
            node2.FriendsInfo (friendList);
        }
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Join")) {  // Friend List
            node2.MySocket.ActionJoin ();
        }
        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Refuse")) {  // Friend List
            node2.InviteRefuse (); // .InviteAFriend(node1.mName);//node2.RandomMatching (1);
        }


        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "User")) {
            //node2.UserModify ("TestStatus", statusOnly:false);
            node2.MySocket.ActionUser ();
        }

        //            if (GUI.Button (ndGUI.DivideRect (nodeR2, 4, colN++), "Exchange")) {
        //                node2.MySocket.ExchangeInfo ();
        //            }
//        if (GUI.Button (ndGUI.DivideRect (nodeR2, colEA, colN++), "Leave")) {
//            node2.LeaveMyself ();
//        }

        Rect nodeR11 = ndGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        int val = node2.GameStartMsgSent.Mine ? 10 : 0;
        val += node2.GameStartMsgSent.Enem ? 1 : 0;
        if (GUI.Button (ndGUI.DivideRect (nodeR11, 3, colN++), "Prepr")) {
            node2.PrepareGame ();
        }
        if (GUI.Button (ndGUI.DivideRect (nodeR11, 3, colN++), "Start? " + node2.ReceiveGameStartMsgBoth)) {
            node2.StartGameMsg ();
        }
        if (GUI.Button (ndGUI.DivideRect (nodeR11, 3, colN++), "ReMch? " + node2.RematchBoth)) {
            node2.Rematch ();
        }
        string hostStr = "Null ";
        if (node2.AmHost.HasValue)
            hostStr = node2.AmHost.Value ? " Host " : " Vstr ";
        if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), hostStr + node2.MySocket.arrGameSend.Count + " : S < Close > R : " +
                node2.MySocket.arrGameRcvd.Count)) {
            node2.CloseNet ();
            node2 = null;
            return;
        }


        if (GUI.Button (ndGUI.GetRect (muiCol, muiRow++), node2.MySocket.arrGameSend.Count + " : S < Close > R : " +
            node2.MySocket.arrGameRcvd.Count)) {
            node2.CloseNet ();
            node2 = null;
            return;
        }


    }
}