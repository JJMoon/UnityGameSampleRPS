using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
//using LitJson;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Was Item Related  _____  Class  _____
public class WasInvite : WasObject
{

    public string friendID;

    public override void SendAction ()
    {
        Ag.LogString ("WasInvite :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 350); // AddKeyValue ("serviceCode", 350);
        //SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        //SendStr = SendStr.AddKeyValue ("key", User.WAS.WasKey);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("friendID", friendID, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasInvite :: postAction " + RcvdStr);
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
            Ag.LogString ("WasInvite :: postAction " + Result.result);
            if (Result.result == 0) {
                User.InviteCount = NdObj["inviteCount"].AsInt;
                User.Isinvite = NdObj["isInvite"].AsInt;
            }
            messageAction (Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasInvite :: CatchAction ...   ");
    }
}
//
//public class WasFriendRank : WasObject
//{
//    public AmUser usr;
//    //public List<FpUser> arrFriends;
//    public int cardID, backNum, buyType;
//    public string playerName;
//
//    public override void SendAction ()
//    {
//        Ag.LogString ("WasFuncCostUpdate :: SendAction ...   Started ...  ");
//        SendStr = "";
//        SendStr = SendStr.AddKeyValue ("serviceCode", 150);
//        SendStr = SendStr.AddKeyValue ("userID", usr.WAS.KkoID);
//        SendStr = SendStr.AddKeyValue ("key", usr.WAS.WasKey);
//
//        //Ag.LogString ("  Friend Num  " + arrFriends.Count);
//
//        string frdID = "";
////        for (int k = 0; k < arrFriends.Count; k++) {
////            FpUser aFriend = arrFriends [k];
////            frdID = frdID.AddStringWithQuotation (aFriend.mID, k + 1 != arrFriends.Count);
////        }
//        //frdID = frdID.Substring (0, frdID.Length - 2); // 맨 끝 , 제거
//        frdID = frdID.AddSqreBrakt ();
//
//        SendStr = SendStr.AddArray ("arrFriendID", frdID, false);
//        SendStr = SendStr.AddParen ();
//
//        postAction += () => {
//            Ag.LogString ("WasFuncCostUpdate :: postAction " + RcvdStr);
//            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr); 
//            // 0 : 성공, -1 : 코인부족, -2 : 기타 에러
//            Ag.LogString ("WasFuncCostUpdate :: postAction " + Result.result);
//            messageAction (Result.result);
//        };
//
//        SendAndRciv ();
//    }
//
//    public override bool JobCompleted ()
//    {
//        if (!IsOK.HasValue)
//            return false;
//        return IsOK.Value;
//    }
//
//    public override void CatchAction ()
//    {
//        Ag.LogString ("WasFuncCostUpdate :: CatchAction ...   ");
//    }
//}