using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

// Not detached code ....    PLATFORM_DEPENDENT_SOURCE   Search it !!!!  They make ERRORs ...
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  Static Platform  _____
public static class StcPlatform
{
    public static bool InitCompleted = false;

    public static string TheToken = "";

    public static string UserID { get { return KakaoLocalUser.Instance.userId; } }

    public static string HashedUserID { get { return KakaoLocalUser.Instance.hashedTalkUserId; } }

    public static string PltmNick { get { return KakaoLocalUser.Instance.nickName; } }

    public static string ProfileURL { get { return KakaoLocalUser.Instance.profileImageUrl; } }

    public static string CountryISO { get { return KakaoLocalUser.Instance.countryIso; } }

    public static bool IsMsgBlocked { get { return KakaoLocalUser.Instance.messageBlocked; } }

    public static string MyUserID { get { return KakaoGameUserInfo.Instance.user_id; } }

    public static string NickName { get { return KakaoGameUserInfo.Instance.nickname; } }

    public static string ProfileImageUrl { get { return KakaoGameUserInfo.Instance.profile_image_url; } }

    public static bool IsMessageBloked { get { return KakaoGameUserInfo.Instance.message_blocked; } }

    public static string Exp { get { return KakaoGameUserInfo.Instance.exp.ToString (); } }

    public static string Heart { get { return KakaoGameUserInfo.Instance.heart.ToString (); } }

    public static string PublicData { get { return KakaoGameUserInfo.Instance.publicData == null ? null : Encoding.Unicode.GetString (KakaoGameUserInfo.Instance.publicData); } }

    public static string PrivateData { get { return KakaoGameUserInfo.Instance.privateData == null ? null : Encoding.Unicode.GetString (KakaoGameUserInfo.Instance.privateData); } }

    public static string MessageCount { get { return KakaoGameUserInfo.Instance.message_count.ToString (); } }
    //public static bool IsOsAllowed { get { return KakaoGameFriends.Instance.kakaotalkFriends [0].supportedDevice; } }
    //public static bool Authorized { get { return ; } }
    public static Dictionary<string, AmFriend> dicFriends = new  Dictionary<string, AmFriend> ();
    public static List<AmFriend> arrAppFriend = new List<AmFriend> ();

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

    public static void SetLBFriendDict ()
    {
        dicFriends.Clear ();
        foreach (KeyValuePair<string,  KakaoGameFriends.LeaderboardFriend> aFrdKV in KakaoGameFriends.Instance.leaderboardFriends) {
            AmFriend curFrd = new AmFriend (aFrdKV.Value);
            dicFriends.Add (aFrdKV.Key, curFrd);
        }
    }

    public static void SetAppFriends ()
    {
        arrAppFriend.Clear ();
        for (int k = 0; k < KakaoFriends.Instance.appFriends.Count; k++) {
            AmFriend nFrd = new AmFriend (KakaoFriends.Instance.appFriends [k]);
            arrAppFriend.Add (nFrd);
        }
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  Am Friend  _____
public class AmFriend : AmObject
{
    //public KakaoFriends.Friend KKO;
    KakaoFriends.Friend plfmObj = null;
    KakaoGameFriends.LeaderboardFriend LBFriend = null;

    public KakaoFriends.Friend PlfmObj { set { plfmObj = value; } }

    public bool IsBlocked { get { return LBFriend.messageBlocked; } }

    public bool CanSendMsg { get { return LBFriend.lastMessageSentAt != 0 && !IsBlocked; } }

    public bool IsOnNode;

    public string UserID { get { return plfmObj.userid; } }

    public string Nick { get { return plfmObj.nickname; } }

    // facebook
    public string ProfileUrl, PlfmID;
    public bool IsAppUser;

    public NodeUser NodeObj;

    public AmFriend ()
    {
    }

    public AmFriend (KakaoFriends.Friend pObj)
    {
        plfmObj = pObj;
    }

    public AmFriend (KakaoGameFriends.LeaderboardFriend pObj)
    {
        LBFriend = pObj;
    }
}
// */