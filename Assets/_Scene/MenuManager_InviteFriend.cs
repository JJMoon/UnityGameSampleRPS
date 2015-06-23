using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{

    public void EnemyLeft () //상대방이 퇴장하였을때 발생되는 딜리깃이벤트에 할당된 함수
    {
        //MenuCommonOpen("alert_someoneout","Ui_popup",true);
        Ag.LogIntenseWord ("Menumanager :: EnemyLeft");
        EnemyLeftflag = true;
    }
    
    public void EnemyLeftPopupClose () //상대방이 퇴장하고 나서 나온 팝업을 끄는 함수
    {
        EnemyLeftflag = false;
        MenuCommonOpen ("alert_someoneout", "Ui_popup", false);
        arrState.SetStateWithNameOf ("Init");
        Btn_Fun_MatchCancleAndGoOut ();
        SetNodeDelegate();

        //MatchStart ();
    }
    /// <summary>
    /// 상대방에게 초대 받거나 내가 호스트인경우 초대메세지에 수락했을때
    /// </summary>
    public void MatchStart ()
    {
        arrState.SetStateWithNameOf ("GoMinJung");
    }
    /// <summary>
    /// 상대방에게 초대됬을때 발생되는 딜리깃이벤트에 할당된 함수
    /// </summary>
    public void Invited ()
    {
        InviteStartFlag = true;
        Debug.Log ("Invited OK");
    }
    
    public void InviteRefused () //내가 상대방을 초대하고 거절했을때 발생되는 딜리깃이벤트에 할당된 함수
    {
        Ag.LogDouble ("  InviteFriend.cs >>    InviteRefused ()  >>>>   ");
        InviteRefusedFlag = true;
    }

    void CancelInvitingByMe ()
    {
        //dicMenuList ["Ui_popup"].SetActive (false);
        //dicMenuList ["versus_accept"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = Ag.NodeObj.MySocket.CurEnemy.kkoNick;
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["versus_accept"].SetActive (false);
        InviteStartFlag = false;
    }

    public void versus_inplay_Close () //상대방이 경기중입니다 팝업 끄는 함수
    {
        MenuCommonOpen ("versus_inplay", "Ui_popup", false);
    }
    
    public void SendVersusMessageNoreply_Close () //상대방이 응답이 없습니다 팝업 끄는 함수
    {
        MenuCommonOpen ("rematch_not", "Ui_popup", false);
        Btn_Fun_MatchCancleAndGoOut ();
        Ag.NodeObj.UserModify ("ONLINE", statusOnly:true);
        Ag.NodeObj.LeaveMyself();
        //mInviteRefuse = false;
    }

    public void RetrySendMatchingMessage ()
    { // 재초대 하는 함수
        SendInviteFriendAction ();
        StartCoroutine (WaitNoRePly ());
        MenuCommonOpen ("rematch_not", "Ui_popup", false);
    }

    void Btn_Fun_Invite_Accept () //상대방의 초대에 수락하는 함수
    {
        Ag.NodeObj.MySocket.ActionJoin ();
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["versus_accept"].SetActive (false);
        dicMenuList ["Start_Panel"].SetActive (true);
        dicMenuList ["btn_exit"].SetActive (false);
        Ag.mFriendMode = 1;
    }
    
    void Btn_Fun_Invite_Cancel () //상대방의 초대에 거절하는 함수
    {
        Ag.NodeObj.MySocket.ActionRefuse (Ag.mySelf);
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["versus_accept"].SetActive (false);
    }
    
    public IEnumerator InviteRefuse () 
    //상대방이 초대하고나서 뜨는 팝업에 아무런 액션을 하지 않았을때 자동으로 팝업을 끄는 함수
    {
        yield return new WaitForSeconds (9f);
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["versus_accept"].SetActive (false);
    }
    
    public int FriendNum;
    public bool mSendmessagetoLoginFriend;
    public string mkkoID, mKkoNick;
    
    public void VersusPopup (string pKKOID, string pKkoNick) //로그인된 상대방을 초대할때 띄우는 팝업
    {
        dicMenuList ["KickOffpopup"].SetActive (true);
        dicMenuList ["invite_versus"].SetActive (true);
        dicMenuList ["invite_versus"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = pKkoNick;
        Ag.mFriendMode = 1; // 친구 모드 일때 프렌드 모드 = 1

        mkkoID = pKKOID;
        mKkoNick = pKkoNick;
        mSendmessagetoLoginFriend = true;
    }
    
    void SendInviteFriendAction () //상대방을 초대하는 함수
    {
        Ag.NodeObj.InviteAFriend (mkkoID);
        mInviteRefuse = false;
        dicMenuList ["Panel_matching"].SetActive (true);
    }
    
    public void VersusPopupKakao (string pKKOID, string pKkonick) // 로그인 안된 상대방을 초대할때 띄우는 팝업(카카오)
    {
        dicMenuList ["KickOffpopup"].SetActive (true);
        dicMenuList ["invite_versus"].SetActive (true);
        dicMenuList ["invite_versus"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = pKkonick;
        mkkoID = pKKOID;
        mKkoNick = pKkonick;
        mSendmessagetoLoginFriend = false;
    }
    
    public IEnumerator WaitNoRePly () //내가 상대방을 초대하고나서 무응답일때 띄우는 팝업
    {
        yield return new WaitForSeconds (9f);
        if (Ag.NodeObj.EnemyUser == null && !mInviteRefuse)
            MenuCommonOpen ("rematch_not", "Ui_popup", true); // No Response ... 
    }



}
