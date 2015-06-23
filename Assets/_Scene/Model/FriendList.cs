using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendList : AmSceneBase {
    public string FriendNum;
    public string FriendNick;
    public int PlayerNum;
    public bool LogOn;
    public string KakaoID;
    public double LastMessageAtsent;

    GameObject mGameObj;
	// Use this for initialization
	public override void Start () {
        mRscrcMan = new HtRsrcMan ("");
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

    void Init () {

    }

    void versuscall () {
        int SumPlayerCost = 0, PlayerExtendEa = 0;

        for (int i = 0; i < 6; i++) {
            SumPlayerCost += Ag.mySelf.GetCardOrderOf (i).WAS.cost;
            if (Ag.mySelf.GetCardOrderOf (i).WAS.limitGameEA < 1)
                PlayerExtendEa ++;
        }
        if (SumPlayerCost > Ag.mySelf.WAS.Cost) {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().MenuCommonOpen ("popup_levelpointalert", "KickOffpopup", true);
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().dicMenuList ["popup_levelpointalert"].transform.FindChild ("Label_maxlevelpoint").GetComponent<UILabel> ().text = Ag.mySelf.WAS.Cost.ToString ();
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().dicMenuList ["popup_levelpointalert"].transform.FindChild ("Label_mylevelpoint").GetComponent<UILabel> ().text = SumPlayerCost.ToString ();
            return;
        }
        if (PlayerExtendEa > 0) {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().MenuCommonOpen ("popup_playeralert", "KickOffpopup", true);
            return;
        }

        if (mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().mSumGold < 0) {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).gameObject.GetComponent<MenuManager> ().MenuCommonOpen ("Ui_popup", "havenotpoint", true);
            return;
        }

        if (LogOn) {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().VersusPopup (KakaoID, FriendNick);
            Debug.Log (KakaoID+ "KKoID");
        } else {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().VersusPopupKakao (KakaoID, FriendNick);
            Debug.Log (KakaoID+ "KKoID");
        }
    }

    void sendMessage () {
        mRscrcMan.FindGameObject ("Axis/Camera/Match",true).gameObject.SendMessage ("Invitefriend",FriendNum);
        Ag.mGameObj = this.gameObject;
    }
	void sendGloveMessage () {
		mRscrcMan.FindGameObject ("Axis/Camera/Match",true).gameObject.SendMessage ("SendGloveMessage",FriendNum);
        Ag.mGameObj = this.gameObject;
	}



    void MessageOn () {

        this.gameObject.transform.FindChild("on/btn_recieveok").gameObject.SetActive(true);
        this.gameObject.transform.FindChild("on/btn_recieveoff").gameObject.SetActive(false);
        mRscrcMan.FindGameObject ("Axis/Camera/Match",true).gameObject.GetComponent<MenuManager>().MessageBlock();
        Debug.Log ("thisObjectname" + this.gameObject.name);
    }

    void MessageOff () {

        this.gameObject.transform.FindChild("on/btn_recieveok").gameObject.SetActive(false);
        this.gameObject.transform.FindChild("on/btn_recieveoff").gameObject.SetActive(true);
        mRscrcMan.FindGameObject ("Axis/Camera/Match",true).gameObject.GetComponent<MenuManager>().MessageBlock();
        Debug.Log ("thisObjectname" + this.gameObject.name);
    }
	/*
    void ImgeSendMessage () {
        int FriendNum = int.Parse (this.gameObject.name.ToString().Substring(10));
        string fileName = "capture_for_image_message.png";
        string imagePath = Application.persistentDataPath+"/"+fileName;
        Application.CaptureScreenshot(fileName);

        Dictionary<string,string> metaInfo = new Dictionary<string, string>();
        metaInfo.Add("nickname","A Good Friend");
        //KakaoNativeExtension.Instance.SendImageMessage("196",KakaoFriends.Instance.friends[FriendNum].userid,imagePath,"itemid=01&count=1",metaInfo, onSendImageMessageComplete, onSendImageMessageError);

    }
    */










}
