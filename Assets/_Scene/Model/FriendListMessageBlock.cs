using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;


public class FriendListMessageBlock : MonoBehaviour {
    public string userid = "";
    double IntervalTime, LastSentMesage;
    double Hour, Minitues, Second;
	public string muserid;

    GameObject btn_WaitMessageSend, btn_recieveoff, btn_recieveok, btn_sendgift, btn_donsend, btn_WaitMessageSendLabel;
	void Start () {
        StcPlatform.SetLBFriendDict ();
        InitGameObj ();
    }

    void InitGameObj () {
        btn_WaitMessageSend = this.gameObject.transform.FindChild ("on/btn_WaitMessageSend").gameObject;
        btn_recieveoff = this.gameObject.transform.FindChild ("on/btn_recieveoff").gameObject;
        btn_recieveok = this.gameObject.transform.FindChild ("on/btn_recieveok").gameObject;
        btn_sendgift = this.gameObject.transform.FindChild ("on/btn_sendgift").gameObject;
        btn_donsend = this.gameObject.transform.FindChild ("on/btn_donsend").gameObject;
        btn_WaitMessageSendLabel = this.gameObject.transform.FindChild ("on/btn_WaitMessageSend/Label_count").gameObject;
    }

    bool Myuserid () {
        if (userid == "" || userid == StcPlatform.UserID)
            return false;
        else
            return true;
    }
	// Update is called once per frame
	void Update () {
        //Debug.Log ("Userid   Checked" + userid);
        //if (KakaoGameFriends.Instance.leaderboardFriends[userid].messageBlocked && Myuserid ()) {
        if (StcPlatform.dicFriends[userid].IsBlocked && Myuserid ()) {
            btn_WaitMessageSend.gameObject.SetActive (false);
            btn_recieveoff.gameObject.SetActive (false);
            btn_recieveok.gameObject.SetActive (false);
            btn_sendgift.gameObject.SetActive (false);
            btn_donsend.gameObject.SetActive (true);
        }

        //if (KakaoGameFriends.Instance.leaderboardFriends[userid].lastMessageSentAt != 0 && !KakaoGameFriends.Instance.leaderboardFriends[userid].messageBlocked && Myuserid()) {
        if (StcPlatform.dicFriends[userid].CanSendMsg && Myuserid()) {
            btn_recieveoff.SetActive (false);
            btn_recieveok.SetActive (false);
            Hour = (Ag.UnixTimeStampToDateTime (KakaoGameFriends.Instance.leaderboardFriends[userid].lastMessageSentAt).AddSeconds (KakaoGameInfo.Instance.game_message_interval) - DateTime.Now).Hours;
            Minitues = (Ag.UnixTimeStampToDateTime (KakaoGameFriends.Instance.leaderboardFriends[userid].lastMessageSentAt).AddSeconds (KakaoGameInfo.Instance.game_message_interval) - DateTime.Now).Minutes;
            Second = (Ag.UnixTimeStampToDateTime (KakaoGameFriends.Instance.leaderboardFriends[userid].lastMessageSentAt).AddSeconds (KakaoGameInfo.Instance.game_message_interval) - DateTime.Now).Seconds;
            if (Ag.UnixTimeStampToDateTime (KakaoGameFriends.Instance.leaderboardFriends [userid].lastMessageSentAt).AddSeconds (KakaoGameInfo.Instance.game_message_interval) > DateTime.Now) {
                Debug.Log (Minitues + " :: " + Second + " :: Time");
                btn_WaitMessageSend.SetActive (true);
                btn_sendgift.SetActive (false);
                btn_WaitMessageSendLabel.GetComponent<UILabel>().text = Hour > 0 ? ((int)Hour).ToFixedWidth(2) + ":" + ((int)Minitues).ToFixedWidth(2) : ((int)Minitues).ToFixedWidth(2) + ":" + ((int)Second).ToFixedWidth(2);
                btn_WaitMessageSendLabel.SetActive (true);
            } else {
                btn_sendgift.gameObject.SetActive(true);
                btn_WaitMessageSend.gameObject.SetActive(false);
            }
        } 
        if (KakaoGameFriends.Instance.leaderboardFriends[userid].lastMessageSentAt == 0 && !KakaoGameFriends.Instance.leaderboardFriends[userid].messageBlocked && Myuserid()) {
            btn_recieveoff.gameObject.SetActive (false);
            btn_recieveok.gameObject.SetActive (false);
            btn_sendgift.gameObject.SetActive(true);
            btn_WaitMessageSend.gameObject.SetActive(false);
        }
        
	}

}
