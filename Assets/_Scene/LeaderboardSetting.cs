using UnityEngine;
using System.Collections;
using System.Text;
using SimpleJSON;

public partial class Login_Register : AmSceneBase
{
    void LoadGameInfo ()
    {
        KakaoNativeExtension.Instance.loadGameInfo (this.onGameInfoComplete, this.onGameInfoError);
    }

    void LoadGameUserInfo ()
    {
        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
    }

    void UpdateUser ()
    {
        KakaoNativeExtension.Instance.updateUser (1, null, null, this.onUpdateUserComplete, this.onUpdateUserError);
    }

    void UseHeart ()
    {
        KakaoNativeExtension.Instance.useHeart (1, this.onUseHeartComplete, this.onUseHeartError);
    }

    void UpdateResult ()
    {
        KakaoNativeExtension.Instance.updateResult ("DEFAULT", 1000, 1000, null, null, this.onUpdateResultComplete, this.onUpdateResultError);
    }

    void LoadLeaderboard ()
    {
        KakaoNativeExtension.Instance.loadLeaderboard ("DEFAULT", this.onLoadLeaderboardComplete, this.onLoadLeaderboardError);
    }

    void LoadGameFriends ()
    {
        KakaoNativeExtension.Instance.loadGameFriends (onLoadGameFriendsComplete, onLoadGameFriendsError);
    }
    /*
    void SendGameMessage ()
    {
        KakaoSample.SampleInstance.moveToView (KakaoViewType.GameFriends);
    }

    void LoadGameMessages ()
    {
        KakaoNativeExtension.Instance.loadGameMessages (this.onLoadGameMessagesComplete, this.onLoadGameMessagesError);
    }
    */

    void AcceptAllGameMessages ()
    {
        KakaoNativeExtension.Instance.acceptAllGameMessages (this.onAcceptAllGameMessagesComplete, this.onAcceptAllGameMessagesError);
    }

    void BlockUnblockMessage ()
    {
        KakaoNativeExtension.Instance.blockMessage (!KakaoGameUserInfo.Instance.message_blocked, this.onBlockMessageComplete, this.onBlockMessageError);
    }

    void DeleteUserInfo ()
    {
        KakaoNativeExtension.Instance.deleteUser (this.onDeleteUserInfoComplete, this.onDeleteUserInfoError);
    }

    private void onGameInfoComplete ()
    {
        Debug.Log ("onGameInfoComplete");

        string maxHeart = KakaoGameInfo.Instance.max_heart.ToString ();
        string rechageableHeart = KakaoGameInfo.Instance.rechargeable_heart.ToString ();
        string heartRegenInterval = KakaoGameInfo.Instance.heart_regen_interval.ToString ();
        string gameMessageInterval = KakaoGameInfo.Instance.game_message_interval.ToString ();
        string invitationInterval = KakaoGameInfo.Instance.invitation_interval.ToString ();
        string nextScoreResetTime = KakaoGameInfo.Instance.next_score_reset_time.ToString ();
        string lastScoreResetTime = KakaoGameInfo.Instance.last_score_reset_time.ToString ();
        string lastScoreResetTimeStamp = KakaoGameInfo.Instance.last_score_reset_timestamp.ToString ();
        string nextScoreResetTimeStamp = KakaoGameInfo.Instance.next_score_reset_timestamp.ToString ();
        string minVersionForiOS = KakaoGameInfo.Instance.min_version_for_ios;
        string currentVersionForiOS = KakaoGameInfo.Instance.current_version_for_ios;
        string minVersionForAndroid = KakaoGameInfo.Instance.min_version_for_android;
        string currentVersionForAndroid = KakaoGameInfo.Instance.current_version_for_android;
        string notice = KakaoGameInfo.Instance.notice;

        string alertMessage = "";

        if (maxHeart != null && maxHeart.Length > 0) {
            alertMessage += "maxHeart : ";
            alertMessage += maxHeart;
            alertMessage += "\n";
        }

        if (heartRegenInterval != null && heartRegenInterval.Length > 0) {
            alertMessage += "heartRegenInterval :";
            alertMessage += heartRegenInterval;
            alertMessage += "\n";
        }

        if (rechageableHeart != null && rechageableHeart.Length > 0) {
            alertMessage += "rechageableHeart :";
            alertMessage += rechageableHeart;
            alertMessage += "\n";
        }

        if (gameMessageInterval != null && gameMessageInterval.Length > 0) {
            alertMessage += "gameMessageInterval :";
            alertMessage += gameMessageInterval;
            alertMessage += "\n";
        }

        if (invitationInterval != null && invitationInterval.Length > 0) {
            alertMessage += "invitationInterval :";
            alertMessage += invitationInterval;
            alertMessage += "\n";
        }

        if (nextScoreResetTime != null && nextScoreResetTime.Length > 0) {
            alertMessage += "nextScoreResetTime :";
            alertMessage += nextScoreResetTime;
            alertMessage += "\n";
        }

        if (lastScoreResetTime != null && lastScoreResetTime.Length > 0) {
            alertMessage += "lastScoreResetTime :";
            alertMessage += lastScoreResetTime;
            alertMessage += "\n";
        }
        if (nextScoreResetTimeStamp != null && nextScoreResetTimeStamp.Length > 0) {
            alertMessage += "nextScoreResetTimeStamp :";
            alertMessage += nextScoreResetTimeStamp;
            alertMessage += "\n";
        }

        if (lastScoreResetTimeStamp != null && lastScoreResetTimeStamp.Length > 0) {
            alertMessage += "lastScoreResetTimeStamp :";
            alertMessage += lastScoreResetTimeStamp;
            alertMessage += "\n";
        }


        if (minVersionForiOS != null && minVersionForiOS.Length > 0) {
            alertMessage += "minVersionForiOS :";
            alertMessage += minVersionForiOS;
            alertMessage += "\n";
        }
        if (currentVersionForiOS != null && currentVersionForiOS.Length > 0) {
            alertMessage += "currentVersionForiOS :";
            alertMessage += currentVersionForiOS;
            alertMessage += "\n";
        }
        if (minVersionForAndroid != null && minVersionForAndroid.Length > 0) {
            alertMessage += "minVersionForAndroid :";
            alertMessage += minVersionForAndroid;
            alertMessage += "\n";
        }
        if (currentVersionForAndroid != null && currentVersionForAndroid.Length > 0) {
            alertMessage += "currentVersionForAndroid :";
            alertMessage += currentVersionForAndroid;
            alertMessage += "\n";
        }
        if (notice != null && notice.Length > 0) {
            alertMessage += "notice :";
            alertMessage += notice;
            alertMessage += "\n";
        }
        mPushLabel.SetActive (true);
        mPushLabelLoadName.SetActive(true);
        mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%84%A0%EC%88%98%20%EC%A0%95%EB%B3%B4%EB%A5%BC%20%EB%B6%88%EB%9F%AC%20%EC%98%A4%EA%B3%A0%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4.");
        LoadGameUserInfo ();

        //KakaoFriends.Instance.AddMyData();
        KakaoNativeExtension.Instance.ShowAlertMessage (alertMessage);


    }

    private void onGameInfoError (string status, string message)
    {
        Debug.Log ("onGameInfoError");
        showAlertErrorMessage (status, message);
    }

    private void onGameUserInfoComplete ()
    {
        Debug.Log ("onGameUserInfoComplete");
        string alertMessage = "";

        string user_id = KakaoGameUserInfo.Instance.user_id;
        string nickname = KakaoGameUserInfo.Instance.nickname;
        string profile_image_url = KakaoGameUserInfo.Instance.profile_image_url;
        string message_blocked = KakaoGameUserInfo.Instance.message_blocked == true ? "true" : "false";
        string exp = KakaoGameUserInfo.Instance.exp.ToString ();
        string heart = KakaoGameUserInfo.Instance.heart.ToString ();
        string heart_regen_starts_at = KakaoGameUserInfo.Instance.heart_regen_starts_at.ToString ();

        string publicData = KakaoGameUserInfo.Instance.publicData == null ? null : Encoding.Unicode.GetString (KakaoGameUserInfo.Instance.publicData);
        string privateData = KakaoGameUserInfo.Instance.privateData == null ? null : Encoding.Unicode.GetString (KakaoGameUserInfo.Instance.privateData);
        string message_count = KakaoGameUserInfo.Instance.message_count.ToString ();

        if (user_id != null && user_id.Length > 0) {
            alertMessage += "user_id : ";
            alertMessage += user_id;
            alertMessage += "\n";
        }
        if (nickname != null && nickname.Length > 0) {
            alertMessage += "nickname : ";
            alertMessage += nickname;
            alertMessage += "\n";
        }
        if (profile_image_url != null && profile_image_url.Length > 0) {
            alertMessage += "profile_image_url : ";
            alertMessage += profile_image_url;
            alertMessage += "\n";
        }
        if (exp != null && exp.Length > 0) {
            alertMessage += "exp : ";
            alertMessage += exp;
            alertMessage += "\n";
        }
        if (heart != null && heart.Length > 0) {
            alertMessage += "heart : ";
            alertMessage += heart;
            alertMessage += "\n";
        }
        if (message_blocked != null && message_blocked.Length > 0) {
            alertMessage += "message_blocked : ";
            alertMessage += message_blocked;
            alertMessage += "\n";
        }
        if (heart_regen_starts_at != null && heart_regen_starts_at.Length > 0) {
            alertMessage += "heart_regen_starts_at : ";
            alertMessage += heart_regen_starts_at;
            alertMessage += "\n";
        }
        if (publicData != null && publicData.Length > 0) {
            alertMessage += "publicData : ";
            alertMessage += publicData;
            alertMessage += "\n";
        }
        if (privateData != null && privateData.Length > 0) {
            alertMessage += "privateData : ";
            alertMessage += privateData;
            alertMessage += "\n";
        }
        if (message_count != null && message_count.Length > 0) {
            alertMessage += "message_count : ";
            alertMessage += message_count;
            alertMessage += "\n";
        }
            
        KakaoNativeExtension.Instance.ShowAlertMessage (alertMessage);
        mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%84%A0%EC%88%98%20%EC%A0%95%EB%B3%B4%EB%A5%BC%20%EB%B6%88%EB%9F%AC%20%EC%98%A4%EA%B3%A0%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4.");
        ;
            
        LoadGameFriends ();

           
    }

    private void onGameUserInfoError (string status, string message)
    {
        Debug.Log ("onGameUserInfoError");
        showAlertErrorMessage (status, message);
    }
    //UpdateUser,
    private void onUpdateUserComplete ()
    {
        Debug.Log ("onUpdateUserComplete");

        // You must call the KakaoNativeExtension::loadGameUserInfo method
        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
    }

    private void onUpdateUserError (string status, string message)
    {
        Debug.Log ("onUpdateUserError");
        showAlertErrorMessage (status, message);
    }
    //UseHeart,
    private void onUseHeartComplete ()
    {
        Debug.Log ("onUseHeartComplete");

        // You must call the KakaoNativeExtension::loadGameUserInfo method
        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
    }

    private void onUseHeartError (string status, string message)
    {
        Debug.Log ("onUseHeartError");
        showAlertErrorMessage (status, message);
    }
    //UpdateResult
    private void onUpdateResultComplete ()
    {
        Debug.Log ("onUpdateResultComplete");

        // You must call the KakaoNativeExtension::loadGameUserInfo method
        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
    }

    private void onUpdateResultError (string status, string message)
    {
        Debug.Log ("onUpdateResultError");
        showAlertErrorMessage (status, message);
    }
    //Update Multiple Result
    private void onUpdateMultipleResultComplete ()
    {
        Debug.Log ("onUpdateMultipleResultComplete");

        // You must call the KakaoNativeExtension::loadGameUserInfo method
        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
    }

    private void onUpdateMultipleResultError (string status, string message)
    {
        Debug.Log ("onUpdateMultipleResultError");
        showAlertErrorMessage (status, message);
    }
    //Load Leaderboard
    private void onLoadLeaderboardComplete ()
    {
        Debug.Log ("onLoadLeaderboardComplete");
        // test
        KakaoLeaderboards.Instance.printToConsole ();
    }

    private void onLoadLeaderboardError (string status, string message)
    {
        Debug.Log ("onLoadLeaderboardError");
        showAlertErrorMessage (status, message);
    }
    //Load Game Friends
    private void onLoadGameFriendsComplete ()
    {
        Debug.Log ("onLoadGameFriendsComplete");
        KakaoGameFriends.Instance.printToConsole ();
        //mPushLabelLoadName.GetComponent<UILabel> ().text = "LOADGAMEFRIEND";
        mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%84%A0%EC%88%98%20%EC%A0%95%EB%B3%B4%EB%A5%BC%20%EB%B6%88%EB%9F%AC%20%EC%98%A4%EA%B3%A0%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4.");
        if (!Ag.mGuest)
            Ag.mySelf.WAS.KkoID = StcPlatform.UserID;
        OnLoginWas ();
    }

    private void onLoadGameFriendsError (string status, string message)
    {
        Debug.Log ("onLoadGameFriendsError");
        showAlertErrorMessage (status, message);
    }
    //Load Game Messages
    /*
    private void onLoadGameMessagesComplete ()
    {
        Debug.Log ("onLoadGameMessagesComplete");
        KakaoSample.SampleInstance.moveToView (KakaoViewType.Messages);
        //this is working
    }
    */

    private void onLoadGameMessagesError (string status, string message)
    {
        Debug.Log ("onLoadGameMessagesError");
        showAlertErrorMessage (status, message);
    }
    //Delete User Info
    private void onDeleteUserInfoComplete ()
    {
        Debug.Log ("onDeleteUserInfoComplete");
        //please call reset method to user game info
    }

    private void onDeleteUserInfoError (string status, string message)
    {
        Debug.Log ("onDeleteUserInfoError");
        showAlertErrorMessage (status, message);
    }

    private void onAcceptAllGameMessagesComplete ()
    {
        Debug.Log ("onAcceptAllGameMessagesComplete");
        //please call reset method to user game info
    }

    private void onAcceptAllGameMessagesError (string status, string message)
    {
        Debug.Log ("onAcceptAllGameMessagesError");
        showAlertErrorMessage (status, message);
    }

    private void onBlockMessageComplete ()
    {
        Debug.Log ("onBlockMessageComplete");
    }

    private void onBlockMessageError (string status, string message)
    {
        Debug.Log ("onBlockMessageError");
        showAlertErrorMessage (status, message);
    }

    private void onAcceptGameMessageComplete ()
    {
        KakaoNativeExtension.Instance.ShowAlertMessage ("Completed accept message!");
    }

    private void onAcceptGameMessageError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    protected void showAlertErrorMessage (string code, string message)
    {		
        /*
		string alertMessage = "";
		if( code!=null ) {
			alertMessage += ("Error Code : "+code);
		}

		if( message!=null ) {
			alertMessage += "\n";
			alertMessage += ("Error Message : "+message);
		}

		KakaoNativeExtension.Instance.ShowAlertMessage(alertMessage);
  */      
    }
}





