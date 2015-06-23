using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;

// Not detached code ....    PLATFORM_DEPENDENT_SOURCE   Search it !!!!  They make ERRORs ...
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  Static Platform  _____
public static class StcPlatformFB
{
    public static string UserID { get { return KakaoLocalUser.Instance.userId; } }

    public static string HashedUserID { get { return KakaoLocalUser.Instance.hashedTalkUserId; } }

    public static string PltmNick { get { return KakaoLocalUser.Instance.nickName; } }

    public static string ProfileURL { get { return KakaoLocalUser.Instance.profileImageUrl; } }

    public static string CountryISO { get { return KakaoLocalUser.Instance.countryIso; } }

    public static bool IsMsgBlocked { get { return KakaoLocalUser.Instance.messageBlocked; } }

    public static Dictionary<string, AmFriend> dicFriends = new  Dictionary<string, AmFriend> ();

    public static List<AmFriend> SortArrFriendsOnlineFirst ()
    {
        List<AmFriend> arrFriends = new List<AmFriend> ();
        List<AmFriend> tempArr = new List<AmFriend> ();
        for (int k = 0; k < KakaoFriends.Instance.appFriends.Count; k++) {
            AmFriend aFriend = new AmFriend (KakaoFriends.Instance.appFriends [k]);
            //aFriend.KKO = KakaoFriends.Instance.appFriends [k];
            aFriend.NodeObj = Ag.NodeObj.GetAFriend (aFriend.UserID);
            aFriend.IsOnNode = aFriend.NodeObj != null;
            if (aFriend.IsOnNode)
                arrFriends.Add (aFriend);
            else
                tempArr.Add (aFriend);
        }
        arrFriends.AddRange (tempArr);
        Ag.LogString (arrFriends.Count + " FriendObjCount");
        return arrFriends;
    }

    public static void Authorized (KakaoResponseHandler.delegateAuthorized autho)
    {
        KakaoNativeExtension.Instance.Authorized (autho);
    }
    //  me/friends?fields=installed,name,picture
    public static void ParseFriends (string pJsn)
    {
//        "data": [
//            {
//                "name": "Moon James", 
//                "id": "100001887642045", 
//                "picture": {
//                    "data": {
//                        "url": "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-ash2/t1.0-1/c8.8.95.95/s50x50/560682_400528403353411_856898041_s.jpg", 
//                        "is_silhouette": false
//                    }
//                }
//            }, 
//            {
        //                "name": "조영준",   // url encoded ....

        JSONNode NdObj = JSON.Parse (pJsn);
        JSONNode dataNd = NdObj ["data"];

        for (int k = 0; k < dataNd.Count; k++) {
            AmFriend aFrd = new AmFriend ();
            JSONNode frndNd = dataNd [k];
            aFrd.PlfmID = frndNd ["id"];
            aFrd.ProfileUrl = frndNd ["picture"] ["data"] ["url"];
            try {
                aFrd.IsAppUser = frndNd["installed"].AsBool;
            } catch {
                aFrd.IsAppUser = false;
            }
        }
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  Am Friend  _____
//public class AmFriend : AmObject
//{
//    //public KakaoFriends.Friend KKO;
//    KakaoFriends.Friend plfmObj = null;
//    KakaoGameFriends.LeaderboardFriend LBFriend = null;
//
//    public KakaoFriends.Friend PlfmObj { set { plfmObj = value; } }
//
//    public bool IsBlocked { get { return LBFriend.messageBlocked; } }
//
//    public bool CanSendMsg { get { return LBFriend.lastMessageSentAt != 0 && !IsBlocked; } }
//
//    public bool IsOnNode;
//
//    public string UserID { get { return plfmObj.userid; } }
//
//    public string Nick { get { return plfmObj.nickname; } }
//
//    public NodeUser NodeObj;
//
//    public AmFriend (KakaoFriends.Friend pObj)
//    {
//        plfmObj = pObj;
//    }
//}
// */