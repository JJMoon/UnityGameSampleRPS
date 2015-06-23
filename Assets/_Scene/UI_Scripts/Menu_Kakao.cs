//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    /// <summary>
    /// WithDrawOK
    /// </summary>
    public void Btn_Fun_withDrawOk ()
    {
        MenuCommonOpen ("Ui_Popup", "game_withdraw", false);
        Ag.mGameStartAlready = false;
        dicMenuList ["CenterCircle"].SetActive (true);
        //AgStt.GoToLoginAfterRegist = false;
        WasUnRegist aObj = new WasUnRegist () { User = Ag.mySelf };
        //AgStt.GoToLoginAfterRegist = false;
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                KakaoNativeExtension.Instance.deleteUser (this.onDeleteUserInfoComplete, this.onDeleteUserInfoError);
                KakaoNativeExtension.Instance.Unregister (onUnregisterComplete, onUnregisterError);
                //KakaoNativeExtension.Instance.Logout (onLogoutComplete, onLogoutError);
                Ag.PlatformLogout = true;
                AgStt.NodeClose ();

                PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", false); //튜토리얼 모드 ON
                PreviewLabs.PlayerPrefs.Flush ();
                Application.LoadLevel ("Title");
                break;
            case 1:
                Ag.LogString (" Regist : Success ");

                //AgStt.GoToLoginAfterRegist = true;
                //StNet.Login(Ag.mySelf, out goToLogin);
                break;
            case -1:
            case 4:
                return;
            }
        };
    }

    public void Btn_Fun_versus_Send () //상대방이 게임중인지  체크해서 게임중이면 게임중이란 팝업을 띄우고 아니면 초대메세지를 보냄
    {
        if (mSendmessagetoLoginFriend) {
            dicMenuList ["KickOffpopup"].SetActive (false);
            dicMenuList ["invite_versus"].SetActive (false);

            if (Ag.NodeObj.MatchOKwith (mkkoID).Value == true) {
                dicMenuList ["invite_versusing"].SetActive (false);
                dicMenuList ["invite_versusing"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = mKkoNick;
                dicMenuList ["Start_Panel"].SetActive (true);
                dicMenuList ["btn_exit"].SetActive (false);
                StartCoroutine (WaitNoRePly ());
                arrState.SetStateWithNameOf ("GoMinJung");
                SendInviteFriendAction ();
                //mKickoff = true;
            } else {
                dicMenuList ["Ui_popup"].SetActive (true);
                dicMenuList ["invite_versus"].SetActive (false);
                dicMenuList ["versus_inplay"].SetActive (true);
                dicMenuList ["versus_inplay"].transform.FindChild ("Label_name").GetComponent<UILabel> ().text = mKkoNick;
            }
        } else {
            dicMenuList ["KickOffpopup"].SetActive (false);
            dicMenuList ["invite_versus"].SetActive (false);
            //KakaoNativeExtension.Instance.SendMessage ("Penaltykick InviteMessage", mkkoID, "itemid=01&count=1", onSendMessageComplete, onSendMessageError);
            KakaoNativeExtension.Instance.sendGameMessage (mkkoID, KakaoLocalUser.Instance.nickName + "님이 승부차기 결투를 신청하셨습니다. 격하게 한 판 붙어볼까요?", "우편함 에서 드링크를 받아가세요", 1, null, null, onSendMessageComplete, onSendGameMessageError);
        }
        //Application.LoadLevel ("PrepareMatch");
    }

    void FriendRank ()
    {
        if (Ag.mGuest) {
            dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom").gameObject.SetActive (false);


        } else {
            dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom").gameObject.SetActive (false);
            if (KakaoFriends.Instance.appFriends.Count > 0) {

                dicMenuList ["CenterCircle"].SetActive (true);
                WasFriendRank aObj = new WasFriendRank () { User = Ag.mySelf };
                for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
                    aObj.arrFriendIDs.Add (KakaoFriends.Instance.appFriends [i].userid);

                }
                aObj.messageAction = (int pInt) => {
                    dicMenuList ["CenterCircle"].SetActive (false);
                    switch (pInt) {
                    case 0: 
                        Btn_Fun_RankList ();
                        break;
                    }
                };
            } else {
                Btn_Fun_RankList ();
            }
        }
    }
}