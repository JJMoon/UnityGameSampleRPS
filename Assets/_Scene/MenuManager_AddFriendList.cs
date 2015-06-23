using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    // 친구상태 업데이트
    bool FriendListUpdate = false;

    void SetDivisionSpriteName ()
    {

    }

    void FriendInit ()
    {
        //mRankFriend = new WasRank ();
        UIDraggablePanel2 DragPanel;
        UIGrid Grid;
        DragPanel = mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_friend/scroll_friend", true).GetComponent<UIDraggablePanel2> ();
        Grid = mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_friend/scroll_friend/grid", true).GetComponent<UIGrid> ();
        SetFriendsListObject ();

        DragPanel.Init (arrFriendObj.Count, delegate(UIListItem item, int index) {
            FriendList scr = item.Target.GetComponent< FriendList > ();
            AmFriend curObj = arrFriendObj [index];
            item.Target.transform.FindChild ("Label_name").GetComponent<UILabel> ().text = curObj.Nick;
            Material PlayerPic;
            PlayerPic = Instantiate (Resources.Load ("Materials/KakaoPic")) as Material;
            GetRankList (curObj.UserID);
            scr.KakaoID = curObj.UserID;

//            for (int i = 0 ; i < Ag.mySelf.arrFriendRank.Count; i ++) {
//                Debug.Log (curObj.UserID + "FriendUSerID" + mRankFriend.league + "MySelf " + Ag.mySelf.arrFriendRank.Count + "arrCount " + "  friendUserid  " + Ag.mySelf.arrFriendRank[i].userID + "friendLeague" + Ag.mySelf.arrFriendRank[i].league);
//            }

            scr.FriendNick = curObj.Nick;
            if (mRankFriend.league.Substring (0, 3) == "PRO") { //_5" || mRankFriend.league == "PRO_4" || mRankFriend.league == "PRO_3" || mRankFriend.league == "PRO_2" || mRankFriend.league == "PRO_1") {
                Ag.mViewCard.CardLeagueSpritename (mRankFriend.league);
                int lgNum = mRankFriend.league.GetContinuousInteger ();
                item.Target.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = "icon_div" + lgNum + "s";
                //child.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                Ag.LogIntenseWord ("mRankFriend.league.GetContinuousInteger :: " + mRankFriend.league + " RankUserid " + mRankFriend.userID);

            } else
                item.Target.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = "icon_div5s";


            Debug.Log ("Ag.mViewCard.LeagueSpriteNameS" + Ag.mViewCard.LeagueSpriteNameS);
            item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material = PlayerPic;
            item.Target.transform.FindChild ("btn_versuscall").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
            item.Target.transform.FindChild ("btn_versuscall").gameObject.GetComponent<UIButtonMessage> ().functionName = "versuscall";
            item.Target.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendoff";
            scr.PlayerNum = index;
            item.Target.name = index.ToString ();
            //item.Target.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (false);
            item.Target.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);

            if (curObj.IsOnNode) {
                Debug.Log (Ag.NodeObj.MatchOKwith (curObj.UserID).HasValue + "KKONICKNAME      " + curObj.Nick);
                if (Ag.NodeObj.MatchOKwith (curObj.UserID).Value == true) {
                    item.Target.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendon";
                    item.Target.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (true);
                    item.Target.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);
                } else { 
                    item.Target.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendplay";
                    item.Target.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (false);
                    item.Target.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (true);
                }
                scr.LogOn = true;
                
            } else {
                item.Target.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (true);
                item.Target.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);
                scr.LogOn = false;
            }
            if (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic")) {
                for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
                    if (item.Target.GetComponent<FriendList> ().KakaoID == KakaoFriends.Instance.appFriends [i].userid) {
                        if (dicTex [i] != null) {
                            item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = dicTex [i];
                        } else {
                            item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
                        }
                    }
                }
            }
        });
        Grid.Reposition ();
        DragPanel.ResetPosition ();
    }

    List<AmFriend> arrFriendObj;

    void SetFriendsListObject ()
    {
        arrFriendObj = StcPlatform.SortArrFriendsOnlineFirst ();

        //int arrCnt = arrFriendObj.Count;
        for (int j = 0; j < arrFriendObj.Count; j++) {
            AmFriend aFrnd = arrFriendObj [j];
            Debug.Log ("SetFriendsListObject ::  J : " + j + "      " + arrFriendObj [j].UserID + "     IsInTheWasFriends ::" + IsInTheWasFriends (aFrnd));
            if (!IsInTheWasFriends (aFrnd)) {
                arrFriendObj.Remove (aFrnd);
                j--;

                Ag.LogString ("  j " + j + "  Cnt " + arrFriendObj.Count);
            }
            Ag.LogString ("  j " + j + "  Cnt " + arrFriendObj.Count);
            //Debug.Log ("SetFriendsListObject After Remove:: " + arrFriendObj [j].UserID + "     IsInTheWasFriends ::" + IsInTheWasFriends (aFrnd));
        }
    }

    bool IsInTheWasFriends (AmFriend pFrnd)
    {
        for (int k = 0; k < Ag.mySelf.arrFriendRank.Count; k++) {
             
            WasRank rObj = Ag.mySelf.arrFriendRank [k];
            if (pFrnd.UserID == rObj.userID)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 현재 게임 플레이중 표기
    /// </summary>
    IEnumerator  FriendUpdate ()
    {
        FriendListUpdate = true;
        mRankFriend = new WasRank ();
        //Ag.LogString ("FriendUpdate");

        while (FriendListUpdate) {
            //while (!Ag.GameStt.IsGameMatched) {
            Ag.NodeObj.FriendsInfo (mFriendId);  // "90060594732486160,90060594732486160,90060594732486160"
            yield return new WaitForSeconds (3);

            UIDraggablePanel2 DragPanel;
            UIGrid Grid;
            //GmMailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_GmMailList");
            //DeleteMailList ();
            yield return new WaitForSeconds (3);
            SetFriendsListObject ();

            foreach (Transform child in mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "LPanel_friend/scroll_friend/grid", true).transform) {
                child.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendoff";
                child.GetComponent<FriendList> ().LogOn = false;
                for (int j = 0; j < arrFriendObj.Count; j++) {
                    if (child.name == j.ToString ()) {
                        Material PlayerPic;
                        PlayerPic = Instantiate (Resources.Load ("Materials/KakaoPic")) as Material;
                        FriendList scr = child.GetComponent< FriendList > ();
                        AmFriend curObj = arrFriendObj [j];
                        child.transform.FindChild ("Label_name").GetComponent<UILabel> ().text = curObj.Nick;
                        GetRankList (curObj.UserID);
                        scr.KakaoID = curObj.UserID;
                        scr.FriendNick = curObj.Nick;
                        if (mRankFriend.league.Substring (0, 3) == "PRO") { //_5" || mRankFriend.league == "PRO_4" || mRankFriend.league == "PRO_3" || mRankFriend.league == "PRO_2" || mRankFriend.league == "PRO_1") {
                            Ag.mViewCard.CardLeagueSpritename (mRankFriend.league);
                            int lgNum = mRankFriend.league.GetContinuousInteger ();
                            child.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = "icon_div" + lgNum + "s";
                            //child.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
                        } else
                            child.transform.FindChild ("division/div1").gameObject.GetComponent<UISprite> ().spriteName = "icon_div5s";
                        child.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material = PlayerPic;
                        child.transform.FindChild ("btn_versuscall").gameObject.GetComponent<UIButtonMessage> ().target = child.gameObject;
                        child.transform.FindChild ("btn_versuscall").gameObject.GetComponent<UIButtonMessage> ().functionName = "versuscall";
                        child.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendoff";
                        child.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);
                        //child.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (false);
                        scr.PlayerNum = j;
                        scr.LogOn = false;
                        child.name = j.ToString ();
                        if (curObj.IsOnNode) {
                            Debug.Log (Ag.NodeObj.MatchOKwith (curObj.UserID).HasValue + "KKONICKNAME      " + curObj.Nick);
                            if (Ag.NodeObj.MatchOKwith (curObj.UserID).Value == true) {
                                child.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendon";
                                child.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (true);
                                child.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);
                            } else { 
                                child.transform.FindChild ("Checkbox_onoff/Background").GetComponent<UISprite> ().spriteName = "img_friendplay";
                                child.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (false);
                                child.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (true);
                            }
                            scr.LogOn = true;
                        } else {
                            scr.LogOn = false;
                            child.transform.FindChild ("btn_versuscall").transform.gameObject.SetActive (true);
                            child.transform.FindChild ("btn_nowplay").transform.gameObject.SetActive (false);
                        }

                        if (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic")) {
                            for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
                                if (child.GetComponent<FriendList> ().KakaoID == KakaoFriends.Instance.appFriends [i].userid) {
                                    child.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = dicTex [i];
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
