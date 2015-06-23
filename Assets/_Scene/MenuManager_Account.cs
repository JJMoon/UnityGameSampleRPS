//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PreviewLabs;
using System.Linq;

public static class MailFlag
{
    public static bool OpenMailBoxFlag, mMessageGmFlag;
}

public partial class MenuManager : AmSceneBase
{
    public AmDirection mDirectObj;
    Dictionary<int , Texture2D > dicTex = new Dictionary<int, Texture2D> ();
    Dictionary<string , Texture2D > dicTexAppfriendMydata = new Dictionary<string, Texture2D> ();
    Dictionary<int , Texture2D > dicNoAppfriend = new Dictionary <int , Texture2D > ();
    ArrayList arrTex = new ArrayList ();
    //public Material mMaterial;
    Material mMaterial, mEnemMaterial;
    string LeagueNum;


    //    int MyLeagueGetNum;
    //
    //    int xxGetMyLeagueNum (string Myleague)
    //    {
    //        switch (Myleague) {
    //        case "PRO_5":
    //            MyLeagueGetNum = 5;
    //            break;
    //        case "PRO_4":
    //            MyLeagueGetNum = 4;
    //            break;
    //        case "PRO_3":
    //            MyLeagueGetNum = 3;
    //            break;
    //        case "PRO_2":
    //            MyLeagueGetNum = 2;
    //            break;
    //        case "PRO_1":
    //            MyLeagueGetNum = 1;
    //            break;
    //        }
    //        return MyLeagueGetNum;
    //
    //    }
    //
    void LeagueIcon (string MyLeague, GameObject Gobj)
    {
        for (int i = 1; i < 6; i++) {
            Gobj.transform.FindChild ("div" + i).gameObject.SetActive (false);
        }
        switch (MyLeague) {
        case "PRO_5":
            Gobj.transform.FindChild ("div5").gameObject.SetActive (true);
            break;
        case "PRO_4":
            Gobj.transform.FindChild ("div4").gameObject.SetActive (true);
            break;
        case "PRO_3":
            Gobj.transform.FindChild ("div3").gameObject.SetActive (true);
            break;
        case "PRO_2":
            Gobj.transform.FindChild ("div2").gameObject.SetActive (true);
            break;
        case "PRO_1":
            Gobj.transform.FindChild ("div1").gameObject.SetActive (true);
            break;
        }

    }

    string GetLeague, mLabel;
    string League;

    void kakao_sync_Lobby ()
    {
        //KakaoNativeExtension.Instance.Login (onLoginComplete, onLoginError);
        MenuCommonOpen ("Ui_popup", "notification_kakaologin", true);

    }

    void kakao_sync_Ok ()
    {
        Ag.PlatformLogout = true;
        Application.LoadLevel ("Title");
        //KakaoNativeExtension.Instance.Login (onLoginComplete, onLoginError);
    }

    void kakao_sync_Cancel ()
    {
        MenuCommonOpen ("Ui_popup", "notification_kakaologin", false);
    }

    string LeaguePoint (int WeekScore)
    {

        if (WeekScore < 20000)
            League = "PRO_5";
        if (WeekScore >= 20000 && WeekScore < 40000)
            League = "PRO_4";
        if (WeekScore >= 40000 && WeekScore < 60000)
            League = "PRO_3";
        if (WeekScore >= 60000 && WeekScore < 80000)
            League = "PRO_2";
        if (WeekScore >= 80000)
            League = "PRO_1";
        return League;
    }

    int GetMyPoint (string Myleague, int Point)
    {
        LeagueNum = (Ag.mySelf.LeagueAsInt - 1).ToString ();
        switch (Myleague) {
        case "PRO_5":
            return 20000 - Point;
        case "PRO_4":
            return 40000 - Point;
        case "PRO_3":
            return 60000 - Point;
        case "PRO_2":
            return 80000 - Point;       
        case "PRO_1":
            //LeagueNum = 0;
            return Point;       
        }
        return Point;
    }


    void LegueupLabel (string MyLeague)
    {
        //string mLabel;
        switch (MyLeague) {
        case "PRO_5":
            if (Ag.mySelf.myRank.WAS.weekScore < 20000) {
                GetLeague = "PRO_5";
                mLabel = "디비전4 승격까지 승점 " + GetMyPoint (Ag.mySelf.WAS.League, Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
            }
            if (20000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 40000) {
                GetLeague = "PRO_3";
                mLabel = "디비전3 승격까지 승점 " + GetMyPoint ("PRO_4", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
            }
            if (40000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 60000) {
                GetLeague = "PRO_2";
                mLabel = "디비전2 승격까지 승점 " + GetMyPoint ("PRO_3", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
            }
            if (60000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 80000) {
                GetLeague = "PRO_1";
                mLabel = "디비전1 승격까지 승점 " + GetMyPoint ("PRO_2", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
            }
            if (80000 <= Ag.mySelf.myRank.WAS.weekScore ) {
                GetLeague = "PRO_1";
                mLabel = "디비전1 입니다. 현재 디비전 을 유지하려면 80000점 이상이어야 합니다.";
            }
            //dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel>().text = mLabel;
            break;

        case "PRO_4":
            if (Ag.mySelf.myRank.WAS.weekScore < 20000) {
                mLabel = "디비전5 로 강등대상입니다. 현재 디비전 을 유지하시려면 20000점 이상이어야 합니다.";
                GetLeague = "PRO_5";
            } else {
                if (20000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 40000) {
                    GetLeague = "PRO_3";
                    mLabel = "디비전3 승격까지 승점 " + GetMyPoint ("PRO_4", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (40000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 60000) {
                    GetLeague = "PRO_2";
                    mLabel = "디비전2 승격까지 승점 " + GetMyPoint ("PRO_3", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (60000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 80000) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 승격까지 승점 " + GetMyPoint ("PRO_2", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (80000 <= Ag.mySelf.myRank.WAS.weekScore ) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 입니다. 현재 디비전 을 유지하려면 80000점 이상이어야 합니다.";
                }
            }
            //dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel>().text = mLabel;
            break;
        case "PRO_3":
            if (Ag.mySelf.myRank.WAS.weekScore < 40000) {
                GetLeague = "PRO_4";
                mLabel = "디비전4 로 강등대상입니다. 현재 디비전 을 유지하시려면 40000점 이상이어야 합니다.";
            } else {
                if (40000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 60000) {
                    GetLeague = "PRO_2";
                    mLabel = "디비전2 승격까지 승점 " + GetMyPoint ("PRO_3", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (60000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 80000) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 승격까지 승점 " + GetMyPoint ("PRO_2", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (80000 <= Ag.mySelf.myRank.WAS.weekScore ) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 입니다. 현재 디비전 을 유지하려면 80000점 이상이어야 합니다.";
                }
            }
            //dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel>().text = mLabel;
            break;
        case "PRO_2":
            if (Ag.mySelf.myRank.WAS.weekScore < 60000) {
                GetLeague = "PRO_3";
                mLabel = "디비전3 로 강등대상입니다. 현재 DIVISON 을 유지하시려면 60000점 이상이어야 합니다.";
            } else {
                if (60000 <= Ag.mySelf.myRank.WAS.weekScore && Ag.mySelf.myRank.WAS.weekScore < 80000) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 승격까지 승점 " + GetMyPoint ("PRO_2", Ag.mySelf.myRank.WAS.weekScore) + "점 남았습니다.";
                }
                if (80000 <= Ag.mySelf.myRank.WAS.weekScore ) {
                    GetLeague = "PRO_1";
                    mLabel = "디비전1 입니다. 현재 디비전 을 유지하려면 80000점 이상이어야 합니다.";
                }
            }
            //dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel>().text = mLabel;
            break;
        case "PRO_1":

            //dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel>().text = mLabel;
            if (Ag.mySelf.myRank.WAS.weekScore < 80000) {
                GetLeague = "PRO_2";
                mLabel = "디비전2 로 강등대상입니다. 현재 DIVISON 을 유지하시려면 80000점 이상이어야 합니다.";
            } else {
                GetLeague = "PRO_1";
                mLabel = "디비전1 입니다. 현재 디비전 을 유지하려면 80000점 이상이어야 합니다.";
            }

            break;

        }

    }

    public IEnumerator BlankPack ()
    {
        string Marqueetext, BlankStart = "뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁뷁", BlankEnd = "    ", BlankStart2 = "                                                                    ";

        Marqueetext = BlankStart;
        Marqueetext += mLabel;
        Marqueetext += BlankEnd;
         

        while (true) {
            yield return new WaitForSeconds (0.2f);
            if (Marqueetext.Length > 0) {
                Marqueetext = Marqueetext.Substring (1);
                dicMenuList ["panel_leftgrade"].transform.FindChild ("Label_division").GetComponent<UILabel> ().text = Marqueetext;
                //Debug.Log (Marqueetext+ Marqueetext.Length);
            }
            if (Marqueetext.Length < 1) {
                Marqueetext = BlankStart2;
                Marqueetext += mLabel;
                Marqueetext += BlankEnd;
            }
        }
    }

    void SettingLocal ()
    {
        //------------------------------------------------------------------ 
        //------------------------------------------------------------------ 
        //------------------------------------------------------------------ GUEST MODE
        //------------------------------------------------------------------ 
        //------------------------------------------------------------------ 

        dicMenuList ["AppVersion"].GetComponent<UILabel> ().text = "1.0.4";//UnityEditor.PlayerSettings.bundleVersion;


        if (Ag.mGuest) {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category1_id/Label_idname", true).GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%BC%EB%B0%98%ED%9A%8C%EC%9B%90");
            dicMenuList ["Appid"].GetComponent<UILabel> ().text = SystemInfo.deviceUniqueIdentifier;
            dicMenuList ["Lobby_Coach_Nick"].GetComponent<UILabel> ().text = "No name";
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_account/category1_id/Label_idname", true).GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%B9%B4%EC%B9%B4%EC%98%A4%ED%9A%8C%EC%9B%90%EB%B2%88%ED%98%B8");
            dicMenuList ["Appid"].GetComponent<UILabel> ().text = StcPlatform.UserID;
            StartCoroutine (startPic (StcPlatform.ProfileURL));
            dicMenuList ["Lobby_Coach_Nick"].GetComponent<UILabel> ().text = StcPlatform.PltmNick;
        }


        float WinNumweek = Ag.mySelf.myRank.WAS.winNumWeek, LossNumweek = Ag.mySelf.myRank.WAS.lossNumWeek;
        mMaterial = (Material)Resources.Load ("Materials/KakaoPic");





        dicMenuList ["Lobby_Coach_TeamName"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
        //dicMenuList ["Lobby_Coach_nations"].GetComponent<UILabel> ().text = Ag.mCountryData.SetNationFlag (Ag.mySelf.WAS.Country);
        dicMenuList ["Lobby_Flag"].GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("flag/" + Ag.mCountryData.SetNationSprite (Ag.mySelf.WAS.Country));

        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            BgmSound.Instance.Play ();
        } else {
            BgmSound.Instance.Stop ();
        }

        dicMenuList ["Lobby_gameamount"].GetComponent<UILabel> ().text = (Ag.mySelf.myRank.WAS.winNumWeek + Ag.mySelf.myRank.WAS.lossNumWeek).ToString ();
        dicMenuList ["Lobby_Allrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNum + Ag.mySelf.myRank.WAS.lossNum + " 전 " + Ag.mySelf.myRank.WAS.winNum + " 승 " + Ag.mySelf.myRank.WAS.lossNum + " 패 ";
        dicMenuList ["Lobby_WeekScore"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScore + " 점 ";
        dicMenuList ["Lobby_WeekRank"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.thisWeekRank.ToString () + " 위 ";
        dicMenuList ["Lobby_TopWinNum"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNumWeek + Ag.mySelf.myRank.WAS.lossNumWeek + " 전 " + Ag.mySelf.myRank.WAS.winNumWeek + " 승 " + Ag.mySelf.myRank.WAS.lossNumWeek + " 패 ";


//                dicMenuList ["Lobby_gameamount"].GetComponent<UILabel> ().text = (Ag.mySelf.myRank.winNumWeek + Ag.mySelf.myRank.lossNumWeek).ToString ();
//        dicMenuList ["Lobby_Allrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.winNum + Ag.mySelf.myRank.lossNum + " 전 " + Ag.mySelf.myRank.winNum + " 승 " + Ag.mySelf.myRank.lossNum + " 패 ";
        dicMenuList ["Lobby_rank"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScoreRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.bestScoreRank.ToString () + "위";
//        dicMenuList ["Lobby_TopPoint"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.weekScore.ToString () + " 점 ";
//        dicMenuList ["Lobby_TopWinNum"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.winNumWeek + Ag.mySelf.myRank.lossNumWeek + " 전 " + Ag.mySelf.myRank.winNumWeek + " 승 " + Ag.mySelf.myRank.lossNumWeek + " 패 ";
//
        dicMenuList ["Lobby_Winrate"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.weekScore.ToString () + " 점 "; //WinNumweek == 0 ? 0 + " %".ToString () : (WinNumweek / (WinNumweek + LossNumweek) * 100).ToString () + " %";

        dicMenuList ["Kicker_popup_editplayername"].transform.FindChild ("Input_editnumber").GetComponent<UIInput> ().text = "99";
        GameObject Gobj;
        Gobj = dicMenuList ["panel_leftgrade"].transform.FindChild ("division").gameObject;
        //dicMenuList ["panel_leftgrade"].transform.FindChild ("").GetComponent<UILabel>().text = "1부리그승격까지  승점 100점 남았습니다.";

        LegueupLabel (LeaguePoint (Ag.mySelf.myRank.WAS.weekScore));
        LeagueIcon (GetLeague, Gobj);

        StartCoroutine (BlankPack ());
        //http://cfile23.uf.tistory.com/image/2140663D51AF664A17E4C0
       
        //StartCoroutine (startPic ("http://cfile23.uf.tistory.com/image/2140663D51AF664A17E4C0"));
       
       
        //FriendRank();
        /*
		mDirectObj = new AmDirection ();
		AgStt.MyNodePrepareObj = new NodeGamePrepare ();
		//AgStt.MyNodePrepareObj.arrDirectionInfo = new List<int[]> ();
		AgStt.MyNodePrepareObj.ShowMyself ();
        */      


        if (Ag.mGuest) {
		
        } else {

//            for (int i = 0; i < KakaoFriends.Instance.friends.Count; i++) {
//                StartCoroutine (Noappfriend (KakaoFriends.Instance.friends [i].profileImageUrl, i));
//            }
            StartCoroutine (SetFriendsProfile ());

			
            for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
                StartCoroutine (startPic (KakaoFriends.Instance.appFriends [i].profileImageUrl, i));
            }
			
            for (int i = 0; i < KakaoFriends.Instance.appFriendsplusMyData.Count; i++) {
                StartCoroutine (startPic2 (KakaoFriends.Instance.appFriendsplusMyData [i].profileImageUrl, KakaoFriends.Instance.appFriendsplusMyData [i].userid));
            }
        }
    }

    /// <summary>
    /// Setting Friend
    /// </summary>
    void DeleteFriendList ()
    {
        if (arrFriendList != null) {
            for (int i = 0; i < arrFriendList.Count; i++) {
                DestroyObject (arrFriendList [i]);
            }
        }
    }

    void DeleteMailList ()
    {
        if (arrFriendList != null) {
            for (int i = 0; i < arrGmMailList.Count; i++) {
                DestroyObject (arrGmMailList [i]);
            }
        }
    }

    /// <summary>
    /// Represents the json array.
    /// </summary>

    void Btn_Fun_FriendList ()
    {


        if (Ag.mGuest) {


        } else {

            dicMenuList ["checkbox1_myfriend"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["checkbox1_myfriend"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["scroll_friend"].SetActive (true);
            dicMenuList ["scroll_rank"].SetActive (false);

            dicMenuList ["LPanel_friend"].transform.FindChild ("Scroll Bar2").gameObject.SetActive (true);
            dicMenuList ["LPanel_friend"].transform.FindChild ("Scroll Bar1").gameObject.SetActive (false);

            //DeleteFriendList ();

            UIDraggablePanel2 DragPanel;
            UIGrid Grid;
            //GmMailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_GmMailList");
            //DeleteMailList ();

            InviteMenuOpen ("");
            dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom").gameObject.SetActive (true);


            DragPanel = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_friend", true).GetComponent<UIDraggablePanel2> ();
            Grid = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_friend/grid", true).GetComponent<UIGrid> ();
            DragPanel.Init (KakaoFriends.Instance.friends.Count, delegate(UIListItem item, int index) {

                FriendList scr = item.Target.GetComponent< FriendList > ();

                string userid = KakaoFriends.Instance.friends [index].userid;

                scr.FriendNum = index.ToString ();
                item.Target.transform.FindChild ("Label_name").GetComponent<UILabel> ().text = KakaoFriends.Instance.friends [index].nickname;

                Debug.Log (KakaoGameFriends.Instance.kakaotalkFriends [userid].lastMessageSentAt + " " + KakaoGameFriends.Instance.kakaotalkFriends [userid].nickname);

                if (KakaoGameFriends.Instance.kakaotalkFriends [userid].lastMessageSentAt != 0 || KakaoGameFriends.Instance.kakaotalkFriends [userid].messageBlocked || !KakaoGameFriends.Instance.kakaotalkFriends [userid].supportedDevice ) {
                    item.Target.transform.FindChild ("btn_donsend").gameObject.SetActive (true);
                    item.Target.transform.FindChild ("btn_invite").gameObject.SetActive (false);
                } else {
                    item.Target.transform.FindChild ("btn_donsend").gameObject.SetActive (false);
                    item.Target.transform.FindChild ("btn_invite").gameObject.SetActive (true);
                }

                Material PlayerPic;
                PlayerPic = Instantiate (Resources.Load ("Materials/KakaoPic")) as Material;
                //PlayerPic.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
                item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material = PlayerPic;
                item.Target.name = index.ToString ();


                //LoadUserPic (KakaoFriends.Instance.friends [index].profileImageUrl, item.Target.transform.FindChild ("face").gameObject);
                item.Target.transform.FindChild ("btn_invite").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
                item.Target.transform.FindChild ("btn_invite").gameObject.GetComponent<UIButtonMessage> ().functionName = "sendMessage";

                if (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic")) {
                    for (int i = 0; i < dicNoAppfriend.Count; i++) {
                        if (i == int.Parse (item.Target.name)) {
                            item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = dicNoAppfriend [i];

                        }
                    }
                }
            });
            Grid.Reposition ();
            DragPanel.ResetPosition ();
        }

    }

    WasRank mRankFriend;

    void GetRankList (string id)
    {
        //mRankFriend.league = "PRO_5";
        mRankFriend.weekScore = 0;
        for (int i = 0; i < Ag.mySelf.arrFriendRank.Count; i++) {
            if (Ag.mySelf.arrFriendRank [i].userID == id) {
                mRankFriend = Ag.mySelf.arrFriendRank [i];
            } 
        }
    }

    void InserMyinfoToFriendArray ()
    {
        //Ag.mySelf.GetMyKakaoFriend();

    }

    List<KakaoFriends.Friend> mFriend;

    void Btn_Fun_RankList ()
    {
        if (Ag.mGuest) {
            dicMenuList ["kakao_sync_Lobby"].SetActive (true);
        } else {


            dicMenuList ["LPanel_friend"].transform.FindChild ("Scroll Bar2").gameObject.SetActive (false);
            dicMenuList ["LPanel_friend"].transform.FindChild ("Scroll Bar1").gameObject.SetActive (true);




            mFriend = new List<KakaoFriends.Friend> ();
            //KakaoFriends.Friend score = new KakaoFriends.Friend();

            dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom").gameObject.SetActive (false);

            //DeleteFriendList ();
            dicMenuList ["scroll_rank"].SetActive (true);
            dicMenuList ["checkbox0_rank"].GetComponent<UICheckbox> ().isChecked = true;
            dicMenuList ["checkbox0_rank"].GetComponent<UICheckbox> ().Set (true);
            dicMenuList ["scroll_friend"].SetActive (false);
            //arrPicMat = new List<Material> ();
            UIDraggablePanel2 DragPanel;
            UIGrid Grid;
            //GmMailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_GmMailList");
            //DeleteMailList ();

            for (int i = 0; i < KakaoFriends.Instance.appFriendsplusMyData.Count; i++) {
                GetRankList (KakaoFriends.Instance.appFriendsplusMyData [i].userid);
                KakaoFriends.Instance.appFriendsplusMyData [i].weekscore = mRankFriend.weekScore;

                if (KakaoFriends.Instance.appFriendsplusMyData [i].userid == Ag.mySelf.WAS.KkoID)
                    KakaoFriends.Instance.appFriendsplusMyData [i].weekscore = Ag.mySelf.myRank.WAS.weekScore;
            }

            IEnumerable<KakaoFriends.Friend> scoreQuery =
                from score in KakaoFriends.Instance.appFriendsplusMyData
                orderby score.weekscore descending
                            select score;
            foreach (KakaoFriends.Friend AppFriendList in scoreQuery) {
                //Debug.Log (AppFriendList.userid + " " + AppFriendList.messageBlocked + " " + AppFriendList.nickname);
                mFriend.Add (AppFriendList);
                //i.transform.localPosition = new Vector3 (SortNum++ * 140, i.transform.localPosition.y, i.transform.localPosition.z);
            }

            DragPanel = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_rank", true).GetComponent<UIDraggablePanel2> ();
            Grid = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_friend/scroll_rank/grid", true).GetComponent<UIGrid> ();
            DragPanel.Init (mFriend.Count, delegate(UIListItem item, int index) {


                string Userid = mFriend [index].userid;
                //Debug.Log ("Player Last Sent" + KakaoGameFriends.Instance.leaderboardFriends [Userid].lastMessageSentAt + " PlayerNick " + KakaoGameFriends.Instance.leaderboardFriends [Userid].nickname);

                //Material PlayerPic;
                FriendList scr = item.Target.GetComponent< FriendList > ();
                item.Target.transform.FindChild ("on/Label_name").GetComponent<UILabel> ().text = mFriend [index].nickname;
                scr.FriendNum = index.ToString ();
                scr.KakaoID = mFriend [index].userid;

                item.Target.GetComponent< FriendListMessageBlock > ().userid = mFriend [index].userid;
                Material PlayerPic;
                PlayerPic = Instantiate (Resources.Load ("Materials/KakaoPic")) as Material;
                //PlayerPic.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
                GetRankList (mFriend [index].userid);


                Debug.Log ("Mfriend List" + mRankFriend.league  + "USERID: " + scr.KakaoID);
                item.Target.transform.FindChild ("on/Label_rank").gameObject.GetComponent<UILabel> ().text = (index + 1).ToString ();

                if (mRankFriend.league == "PRO_5" || mRankFriend.league == "PRO_4" || mRankFriend.league == "PRO_3" || mRankFriend.league == "PRO_2" || mRankFriend.league == "PRO_1") {
                    Ag.mViewCard.CardLeagueSpritename (mRankFriend.league);
                    item.Target.transform.FindChild ("on/Sprite (icon_div1)").gameObject.GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteName;
                }
                else
                    item.Target.transform.FindChild ("on/Sprite (icon_div1)").gameObject.GetComponent<UISprite> ().spriteName = "icon_div5";


                item.Target.transform.FindChild ("on/Label_score").gameObject.GetComponent<UILabel> ().text = mFriend [index].weekscore + "점";
                item.Target.transform.FindChild ("on/face").gameObject.GetComponent<UITexture> ().material = PlayerPic;
                //LoadUserPic (KakaoFriends.Instance.appFriends [index].profileImageUrl, item.Target.transform.FindChild ("on/face").gameObject);
                //
                item.Target.transform.FindChild ("on/btn_sendgift").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
                item.Target.transform.FindChild ("on/btn_sendgift").gameObject.GetComponent<UIButtonMessage> ().functionName = "sendGloveMessage";
                item.Target.transform.FindChild ("on/btn_recieveoff").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
                item.Target.transform.FindChild ("on/btn_recieveoff").gameObject.GetComponent<UIButtonMessage> ().functionName = "MessageOn";
                item.Target.transform.FindChild ("on/btn_recieveok").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
                item.Target.transform.FindChild ("on/btn_recieveok").gameObject.GetComponent<UIButtonMessage> ().functionName = "MessageOff";
                item.Target.name = index.ToString ();

                if (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic")) {
                    for (int i = 0; i < mFriend.Count; i++) {
                        if (item.Target.GetComponent<FriendList> ().KakaoID == mFriend [i].userid) {
                            item.Target.transform.FindChild ("on/face").gameObject.GetComponent<UITexture> ().material.mainTexture = dicTexAppfriendMydata [Userid];
                        }
                    }
                }
            
                if (mFriend [index].messageBlocked == true) {
                    item.Target.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (false);
                    item.Target.transform.FindChild ("on/btn_donsend").gameObject.SetActive (true);
                    item.Target.transform.FindChild ("on/btn_recieveok").gameObject.SetActive (false);

                } else {
                    item.Target.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (true);
                    item.Target.transform.FindChild ("on/btn_donsend").gameObject.SetActive (false);

                }
                if (mFriend [index].userid == KakaoGameUserInfo.Instance.user_id) {
                    item.Target.transform.FindChild ("on/Sprite (scroll_on)").GetComponent<UISprite> ().spriteName = "scroll_user";
                    Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);
                    item.Target.transform.FindChild ("on/Sprite (icon_div1)").gameObject.GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteName;

                    if (KakaoGameUserInfo.Instance.message_blocked) {

                        item.Target.transform.FindChild ("on/btn_recieveok").gameObject.SetActive (false);
                        item.Target.transform.FindChild ("on/btn_recieveoff").gameObject.SetActive (true);
                    } else {

                        item.Target.transform.FindChild ("on/btn_recieveok").gameObject.SetActive (true);
                        item.Target.transform.FindChild ("on/btn_recieveoff").gameObject.SetActive (false);
                    }
                    item.Target.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (false);
                    item.Target.transform.FindChild ("on/btn_donsend").gameObject.SetActive (false);
                    item.Target.transform.FindChild ("on/btn_WaitMessageSend").gameObject.SetActive (false);

                } else {
                    if (KakaoGameFriends.Instance.leaderboardFriends [Userid].lastMessageSentAt != 0 && !KakaoGameFriends.Instance.leaderboardFriends [Userid].messageBlocked) {
                        item.Target.transform.FindChild ("on/btn_WaitMessageSend").gameObject.SetActive (true);
                        item.Target.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (false);
                        item.Target.transform.FindChild ("on/btn_donsend").gameObject.SetActive (false);
                    } 
                    if (KakaoGameFriends.Instance.leaderboardFriends [Userid].lastMessageSentAt == 0 && !KakaoGameFriends.Instance.leaderboardFriends [Userid].messageBlocked) {
                        item.Target.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (true);
                        item.Target.transform.FindChild ("on/btn_WaitMessageSend").gameObject.SetActive (false);
                        item.Target.transform.FindChild ("on/btn_donsend").gameObject.SetActive (false);
                    }
                    item.Target.transform.FindChild ("on/Sprite (scroll_on)").GetComponent<UISprite> ().spriteName = "scroll_on";
                }


            
            });
            Grid.Reposition ();
            DragPanel.ResetPosition ();
        }

    }

    void Btn_Fun_AddFriendListBoxOpen ()
    {
        if (Ag.mGuest)
            dicMenuList ["kakao_sync_Lobby"].SetActive (true);

        mBackDepthFlag = true;
        dicMenuList ["LPanel_friend"].SetActive (true);
        //Btn_Fun_RankList ();
        FriendRank ();
    }



    void Btn_Fun_AddFriendListBoxClose ()
    {
        dicMenuList ["LPanel_friend"].SetActive (false);
        DeleteFriendList ();
    }

    /// <summary>
    /// Setting Post
    /// </summary>
    void MailBoxLoad ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);
        WasMailFetch aObj = new WasMailFetch () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) {
            case 0:
                Btn_Fun_PostList ();
                break;
            }
        };
    }

    string mKakaoNickName;

    void KakaoFriend (string id)
    {
        for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
            if (KakaoFriends.Instance.appFriends [i].userid == id) {
                mKakaoNickName = KakaoFriends.Instance.appFriends [i].nickname;
            }
        }

    }

    public void Btn_Fun_PostList ()
    {
        MailFlag.mMessageGmFlag = true;
        //---if have MailBox List
        dicMenuList ["emptymessage"].SetActive (false);
        dicMenuList ["emptynotice"].SetActive (false);
        //---

        if (Ag.mySelf.arrMail.Count == 0)
            dicMenuList ["emptymessage"].SetActive (true);

        dicMenuList ["scroll_notice"].SetActive (false);

        dicMenuList ["Lobbyscroll_message"].SetActive (true);
        DeleteMailList ();

        dicMenuList ["LPanel_post"].transform.FindChild ("bundle_bottombtn").transform.gameObject.SetActive (true);

        GameObject MailList, mailPrefab;
        //mailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_MailList");

        UIDraggablePanel2 DragPanel;
        UIGrid Grid;
        //GmMailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_GmMailList");
        //DeleteMailList ();

        DragPanel = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_message", true).GetComponent<UIDraggablePanel2> ();
        Grid = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_message/grid", true).GetComponent<UIGrid> ();

        dicMenuList ["LPanel_post"].transform.FindChild ("bundle_bottombtn/Label_postamount").transform.gameObject.GetComponent<UILabel> ().text = Ag.mySelf.arrMail.Count + "/50";
        DragPanel.Init (Ag.mySelf.arrMail.Count, delegate(UIListItem item, int index) {

            Material PlayerPic;
            PlayerPic = Instantiate (Resources.Load ("Materials/KakaoPic")) as Material;
            MailList scr = item.Target.GetComponent< MailList > ();
            scr.mMail = Ag.mySelf.arrMail [index];
            KakaoFriend (WWW.UnEscapeURL (Ag.mySelf.arrMail [index].WAS.senderID));

            DTobj = Ag.mySelf.arrMail [index].WAS.date.ToDateTime ();

            //item.Target.transform.FindChild ("Label_date").GetComponent<UILabel> ().text = (Ag.SeoulNow.Day != DTobj.Day) ? (Ag.SeoulNow - DTobj).Days.ToString () + "일전" : "오늘";//DTobj.Day.ToString () + "일";
            if (Ag.SeoulNow.Day == DTobj.Day) {
                item.Target.transform.FindChild ("Label_date").GetComponent<UILabel> ().text = "오늘";//DTobj.Day.ToString () + "일";
            } else {
                if ((Ag.SeoulNow - DTobj).Days < 1)
                    item.Target.transform.FindChild ("Label_date").GetComponent<UILabel> ().text = "1일전";
                else
                    item.Target.transform.FindChild ("Label_date").GetComponent<UILabel> ().text = (Ag.SeoulNow - DTobj).Days.ToString () + "일전" ;
            }

//            Debug.Log (System.DateTime.Today + "    " + DTobj + "days" + (System.DateTime.Today - DTobj).Hours);
//            Debug.Log ("Ag.mySelf.arrMail [index].WAS.itemTypeId :: +" + Ag.mySelf.arrMail [index].WAS.itemTypeId);
            item.Target.transform.FindChild ("contentitem/Label").GetComponent<UILabel> ().text = UseItemCount(Ag.mySelf.arrMail [index].WAS.itemTypeId) ? "x"+ Ag.mySelf.arrMail[index].WAS.itemCount.ToString() : ListItemCode [Ag.mySelf.arrMail [index].WAS.itemTypeId].Value;
            //if ((System.DateTime.Today - DTobj).Days > 1)
                item.Target.transform.FindChild ("icon_new").gameObject.SetActive (false);
            //;

            MailItemSetActive (item.Target.transform.FindChild ("contentitem").gameObject);

            item.Target.transform.FindChild ("contentitem/" + ListItemCode [Ag.mySelf.arrMail [index].WAS.itemTypeId].SpriteName).gameObject.SetActive (true);

            item.Target.GetComponent<MailList> ().mMail = Ag.mySelf.arrMail [index];
            item.Target.transform.FindChild ("btn_riceive").gameObject.GetComponent<UIButtonMessage> ().target = item.Target;
            item.Target.transform.FindChild ("btn_riceive").gameObject.GetComponent<UIButtonMessage> ().functionName = "ReceiveMail";
            item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material = PlayerPic;

           

            if (Ag.mySelf.arrMail [index].WAS.fromUUID == -1) {
                item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = (Texture2D)(Resources.Load ("gmimg"));
                item.Target.transform.FindChild ("Label_content").GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.arrMail [index].WAS.content);
            } else {
                item.Target.transform.FindChild ("Label_content").GetComponent<UILabel> ().text = "알수없는" +  WWW.UnEscapeURL ("%EB%8B%98%EC%9C%BC%EB%A1%9C%EB%B6%80%ED%84%B0%20%EC%84%A0%EB%AC%BC%20%EB%8F%84%EC%B0%A9");
                for (int i = 0; i < KakaoFriends.Instance.appFriends.Count; i++) {
                    if (item.Target.GetComponent<MailList> ().mMail.WAS.senderID == KakaoFriends.Instance.appFriends [i].userid) {
                        item.Target.transform.FindChild ("face").gameObject.GetComponent<UITexture> ().material.mainTexture = dicTex [i];
                        item.Target.transform.FindChild ("Label_content").GetComponent<UILabel> ().text = KakaoFriends.Instance.appFriends [i].nickname + WWW.UnEscapeURL (" %EB%8B%98%EC%9C%BC%EB%A1%9C%EB%B6%80%ED%84%B0%20%EC%84%A0%EB%AC%BC%20%EB%8F%84%EC%B0%A9");
                    }

                }
            }

        });
        Grid.Reposition ();
        DragPanel.ResetPosition ();
    }

    void MailItemSetActive (GameObject Gobj)
    {
        Gobj.transform.FindChild ("icon_bluedrink").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_greendrink").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_reddrink").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_cash").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_coin").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_heart").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_card").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_card2").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_fotune1").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_fotune2").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_fotune3").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_glove1").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_glove2").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_glove3").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_glove4").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_shoes1").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_shoes2").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_shoes3").gameObject.SetActive (false);
        Gobj.transform.FindChild ("icon_shoes4").gameObject.SetActive (false);


    }

    void Btn_Fun_NoticeList ()
    {
        MailFlag.mMessageGmFlag = false;
        //---if have MailBox List
        dicMenuList ["emptymessage"].SetActive (false);
        dicMenuList ["emptynotice"].SetActive (false);

        if (Joycity.arrImageNoti.Count == 0)
            dicMenuList ["emptynotice"].SetActive (true);
        //---
        dicMenuList ["Lobbyscroll_message"].SetActive (false);
        dicMenuList ["scroll_notice"].SetActive (true);
        GameObject GmNoticeMail, GmMailPrefab;
        UIDraggablePanel2 DragPanel;
        UIGrid Grid;
        //GmMailPrefab = (GameObject)Resources.Load ("prefab_General/Pref_GmMailList");
        //DeleteMailList ();

        DragPanel = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_notice", true).GetComponent<UIDraggablePanel2> ();
        Grid = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_notice/grid", true).GetComponent<UIGrid> ();
        dicMenuList ["LPanel_post"].transform.FindChild ("bundle_bottombtn").transform.gameObject.SetActive (false);



        Debug.Log (Joycity.arrImageNoti.Count + "arrImageNotiCount");

        DragPanel.Init (Joycity.arrImageNoti.Count, delegate(UIListItem item, int index) {


            MailList scr = item.Target.GetComponent< MailList > ();
            item.Target.name = index.ToString ();
            scr.mImagePath = Joycity.arrImageNoti [index].image_path;
            scr.mUrl = Joycity.arrImageNoti [index].url;
            Debug.Log ("JoycityUrl    "+ Joycity.arrImageNoti [index].url);
            item.Target.transform.FindChild ("Label_content").GetComponent<UILabel> ().text = Joycity.arrImageNoti [index].title;
            item.Target.transform.FindChild ("btn_ok").GetComponent<UIButtonMessage> ().target = item.Target;
            item.Target.transform.FindChild ("btn_ok").GetComponent<UIButtonMessage> ().functionName = "GotoAppUrl";


            /*
            cCDAppUserData friend = mData[index];

            int r = getRank( index, game );

            scr.ContLabel = index.ToString ();
            //scr.SetData( r, friend.ScoreData.GetScore( game ), friend );
            scr.NoticeMailInit ();
            */

        });
        Grid.Reposition ();
        DragPanel.ResetPosition ();
        /*
        for (int i = 0; i < 100; i++) {

            Material PlayerPic;

            GmNoticeMail = (GameObject)Instantiate (GmMailPrefab, new Vector3 (0, 0, -6.386f), Quaternion.identity);
            //arrFriendList = null;
            arrGmMailList.Add (GmNoticeMail);
            //PlayerPic = Instantiate(FriendList.transform.FindChild("userface").gameObject.GetComponent<UITexture>().material) as Material;
            //arrPicMat.Add(PlayerPic);
            GmNoticeMail.name = "GMNotice" + i;
            GmNoticeMail.transform.parent = mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_post/scroll_notice/grid", true).transform;
            GmNoticeMail.transform.localScale = new Vector3 (1, 1, 1);
            GmNoticeMail.transform.localPosition = new Vector3 (0, 0, -6.386f);
            GmNoticeMail.transform.FindChild ("Label_content").GetComponent<UILabel> ().text = "GMNotice" + i;//KakaoFriends.Instance.appFriends[i].nickname;
            GmNoticeMail.transform.FindChild ("btn_ok").gameObject.AddComponent<UIButtonMessage> ().target = GmNoticeMail;
            GmNoticeMail.transform.FindChild ("btn_ok").gameObject.GetComponent<UIButtonMessage> ().functionName = "ViewNotice";
            GmNoticeMail.transform.FindChild ("btn_del").gameObject.AddComponent<UIButtonMessage> ().target = GmNoticeMail;
            GmNoticeMail.transform.FindChild ("btn_del").gameObject.GetComponent<UIButtonMessage> ().functionName = "DeleteGmMessage";
            mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_lobby/LPanel_post/scroll_notice/grid", true).gameObject.GetComponent<UIGrid> ().Reposition ();

        }
        */


    }

    public void Btn_Fun_PostBoxOpen ()
    {
        mBackDepthFlag = true;
        dicMenuList ["LPanel_post"].SetActive (true);
        MailBoxLoad ();
        //Btn_Fun_PostList ();
        dicMenuList ["CheckBox_Message"].GetComponent<UICheckbox> ().isChecked = true;
        dicMenuList ["CheckBox_Message"].GetComponent<UICheckbox> ().Set (true);


    }

    void Btn_Fun_PostBoxClose ()
    {
        dicMenuList ["LPanel_post"].SetActive (false);
        MailFlag.OpenMailBoxFlag = false;
    }

    void Post_Reposition (string pstr)
    {
        MailBoxLoad ();
        StartCoroutine (MailListUpdate ());
    }

    IEnumerator MailListUpdate ()
    {

        while (MailFlag.OpenMailBoxFlag) {
            //Debug.Log ("OpenMailCorutaine" + "Flag" + MailFlag.OpenMailBoxFlag);
            if (MailFlag.mMessageGmFlag)
                mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_lobby/LPanel_post/scroll_message/grid", true).gameObject.GetComponent<UIGrid> ().Reposition ();
            else
                mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_lobby/LPanel_post/scroll_notice/grid", true).gameObject.GetComponent<UIGrid> ().Reposition ();
           

            yield return new WaitForSeconds (0.2f);

        }
    }

    void EraseMail (int msg1, int msg2)
    {

        dicMenuList ["CenterCircle"].SetActive (true);
        WasMailErase aObj = new WasMailErase () { User = Ag.mySelf, msgID1 = msg1, msgID2 = msg2 };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) {
            case 0:
                messageReceiveCardinfo ();
                Debug.Log ("MailErase");
                MailBoxLoad ();
                break;

            }
        };
        
    }

    void Btn_Fun_All_Receive ()
    {
        EraseMail (-1, -1);
    }

    /// <summary>
    /// Send Presnet to friend 
    /// </summary>

    void SendPresent ()
    {
        dicMenuList ["SendtoPresent"].SetActive (true);
    }

    public string mFriendNum, mUserId;
    public bool InviteFriend;

    void Invitefriend (string pFriendNum)
    {
        InviteFriend = true;
        mFriendNum = pFriendNum;
        //KakaoNativeExtension.Instance.SendMessage("Message from Unity3D Plugin.",KakaoFriends.Instance.friends[pstr].userid,"itemid=01&count=1", onSendMessageComplete, onSendMessageError);
        dicMenuList ["Ui_Popup"].SetActive (true);
        dicMenuList ["invite_friend"].SetActive (true);
        //dicMenuList ["invite_friend"].transform.FindChild ("Label_username").GetComponent<UILabel>().text = mFriendNum;
        dicMenuList ["invite_friend"].transform.FindChild ("Label_username").GetComponent<UILabel> ().text = KakaoFriends.Instance.friends [int.Parse (pFriendNum)].nickname;
        mUserId = KakaoFriends.Instance.friends [int.Parse (mFriendNum)].userid;

    }

    void SendGloveMessage (string pFriendNum)
    {
        InviteFriend = false;
        mFriendNum = pFriendNum;
        //KakaoNativeExtension.Instance.SendMessage("Message from Unity3D Plugin.",KakaoFriends.Instance.friends[pstr].userid,"itemid=01&count=1", onSendMessageComplete, onSendMessageError);
        dicMenuList ["Ui_Popup"].SetActive (true);
        dicMenuList ["send_gift"].SetActive (true);
        //dicMenuList ["invite_friend"].transform.FindChild ("Label_username").GetComponent<UILabel>().text = mFriendNum;
        dicMenuList ["send_gift"].transform.FindChild ("Label_username").GetComponent<UILabel> ().text = mFriend [int.Parse (pFriendNum)].nickname;
        mUserId = mFriend [int.Parse (mFriendNum)].userid;
    }

    void InvitefriendOk ()
    {
        //Ag.LogIntenseWord ("  Invite Friend  >>>  " + InviteFriend + " >> ID >> " + mUserId);
        if (InviteFriend) {
            //KakaoNativeExtension.Instance.SendMessage ("Test01", mUserId, "itemid=01&count=1", onSendMessageComplete, onSendMessageError);

            MenuCommonOpen ("Ui_Popup", "invite_success", false);
            //onSendInviteGameMessageComplete ();
            Invite ("");
 
            dicMenuList ["invite_friend"].SetActive (false);
        } else {
            //KakaoNativeExtension.Instance.SendMessage ("Test01", mUserId, "itemid=01&count=1", onSendMessageComplete, onSendMessageError);
            KakaoNativeExtension.Instance.sendGameMessage (mUserId, KakaoLocalUser.Instance.nickName + "님이 선물을 보냈습니다. 받으러 가볼까요?", "우편함 에서 드링크를 받아가세요", 1, null, null, onSendGameMessageComplete, onSendGameMessageError);
            dicMenuList ["send_gift"].SetActive (false);
        }
        dicMenuList ["Ui_Popup"].SetActive (false);
    }

//    void InviteReward ()
//    {
//        Invite (mUserId);
//    }

    void AlreadySendInviteMessageGetReward () {
        WasInvite aObj = new WasInvite () { User = Ag.mySelf, friendID = mUserId };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:

                Userinfo ();
                Ag.LogString (" result : Success ");
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/Label_man").GetComponent<UILabel> ().text = Ag.mySelf.InviteCount.ToString ();
                InviteRewardItemCheck ();

                return;
            }
        };
        ItemInfo ();
    }



    void Invite (string FriendId)
    {
        WasInvite aObj = new WasInvite () { User = Ag.mySelf, friendID = FriendId };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                if (Ag.mySelf.Isinvite == 0) {
                    //MenuCommonOpen ("Ui_Popup", "invite_success", true);
                    KakaoNativeExtension.Instance.sendInviteGameMessage (mUserId, KakaoLocalUser.Instance.nickName + "님이 긴장감 넘치는 0.1초의 승부! 대전 승부차기에 초대했습니다.", null, onSendInviteGameMessageComplete, onSendInviteGameMessageError);
                }
                if (Ag.mySelf.Isinvite == 1) {
                    MenuCommonOpen("invite_kakaofail","Ui_Popup",true);
                }
                Userinfo ();
                Ag.LogString (" result : Success ");
                dicMenuList ["LPanel_friend"].transform.FindChild ("friendbottom/Label_man").GetComponent<UILabel> ().text = Ag.mySelf.InviteCount.ToString ();
                InviteRewardItemCheck ();

                return;
            }
        };
        ItemInfo ();
    }

    void first_purchase_popupClose () {
        dicMenuList ["Ui_popup"].SetActive (false);
        dicMenuList ["first_purchase"].SetActive (false);
//        if (dicMenuList ["first_purchase"].transform.FindChild ("check_today").GetComponent<UICheckbox> ().isChecked) {
//            PreviewLabs.PlayerPrefs.SetBool ("FirstCharge_check", true);
//            PreviewLabs.PlayerPrefs.Flush ();
//        }
    }

    void first_purchase_popupOpen () {
        dicMenuList ["Ui_popup"].SetActive (true);
        dicMenuList ["first_purchase"].SetActive (true);

    }


    void InvitefriendCancel ()
    {
        dicMenuList ["Ui_Popup"].SetActive (false);
        dicMenuList ["invite_friend"].SetActive (false);
    }

    void SendGiftCancel ()
    {
        dicMenuList ["Ui_Popup"].SetActive (false);
        dicMenuList ["send_gift"].SetActive (false);
    }

    void SendtoPresentClose ()
    {
        dicMenuList ["SendtoPresent"].SetActive (false);
    }

    void InviteBtnClose ()
    {
        dicMenuList ["InviteFriend"].SetActive (false);
    }

    void RanKuser ()
    {
        dicMenuList ["Lobby_Allrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNum + Ag.mySelf.myRank.WAS.lossNum + " 전 " + Ag.mySelf.myRank.WAS.winNum + " 승 " + Ag.mySelf.myRank.WAS.lossNum + " 패 ";
        dicMenuList ["Lobby_WeekScore"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScore + " 점 ";
        dicMenuList ["Lobby_WeekRank"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.thisWeekRank.ToString () + " 위 ";
        dicMenuList ["Lobby_TopWinNum"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNumWeek + Ag.mySelf.myRank.WAS.lossNumWeek + " 전 " + Ag.mySelf.myRank.WAS.winNumWeek + " 승 " + Ag.mySelf.myRank.WAS.lossNumWeek + " 패 ";
        dicMenuList ["Lobby_rank"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScoreRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.bestScoreRank.ToString () + "위";

        if (Ag.mGuest) {
            dicMenuList ["Label_coachname"].GetComponent<UILabel> ().text = "No name";
        } else {
            dicMenuList ["Label_coachname"].GetComponent<UILabel> ().text = StcPlatform.PltmNick;
        }

        dicMenuList ["Label_nations"].GetComponent<UILabel> ().text = Ag.mCountryData.SetNationFlag (Ag.mySelf.WAS.Country);
        dicMenuList ["Label_teamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);
        dicMenuList ["Label1_Allrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNumWeek + Ag.mySelf.myRank.WAS.lossNumWeek + "전" + Ag.mySelf.myRank.WAS.winNumWeek + "승" + Ag.mySelf.myRank.WAS.lossNumWeek + "패";
        dicMenuList ["Label2_WinRate"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.weekScore.ToString () + "점";
        dicMenuList ["Label3_TopPoint"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.thisWeekRank.ToString () + "위";
        dicMenuList ["Label4_TopWinNum"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.lastWeekRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.lastWeekRank.ToString () + "위";
        dicMenuList ["Label1_totalrecord"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.winNum + Ag.mySelf.myRank.WAS.lossNum + "전" + Ag.mySelf.myRank.WAS.winNum + "승" + Ag.mySelf.myRank.WAS.lossNum + "패";
        dicMenuList ["Label3_totalTopPoint"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScoreRank == -1 ? "순위없음" : Ag.mySelf.myRank.WAS.bestScoreRank.ToString () + "위";
        dicMenuList ["Label2_totalWinRate"].GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.bestScore.ToString () + "점";
    }
    //  _////////////////////////////////////////////////_    _____   Daily Check   _____   Methods   _____
    void Btn_Fun_DailyCheckOk ()
    {
        DailyCheckCloseAction ();
    }

    void Btn_Fun_DailyCheckClose ()
    {
        DailyCheckCloseAction ();
    }

    void DailyCheckCloseAction ()
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_check", false);
        //Ag.mySelf.ShowDailyEvent = false;
        Ag.mySelf.CheckFirstDailyEventToday ();
    }
    //  _////////////////////////////////////////////////_    _____   DDD   _____   Methods   _____
    void Btn_Fun_EventOK ()
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_event", false);
    }

    void Btn_Fun_EventClose ()
    {
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_event", false);
    }

    /// <summary>
    /// Setting Gameset
    /// </summary>

    void Btn_Fun_SettingBoxOpen ()
    {
        mBackDepthFlag = true;
        SettingPopupInit ();
        dicMenuList ["LPanel_setting"].SetActive (true);

    }

    void Btn_Fun_SettingBoxClose ()
    {
        dicMenuList ["LPanel_setting"].SetActive (false);
    }

    void SettingPopupInit ()
    {
        Debug.Log ("Getbool" + PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"));
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff");
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"));
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff");
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff"));
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = PreviewLabs.PlayerPrefs.GetBool ("viewUserpic");
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().Set (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic"));
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = PreviewLabs.PlayerPrefs.GetBool ("MessageAlert");
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().Set (PreviewLabs.PlayerPrefs.GetBool ("MessageAlert"));
    }

    public void Btn_Fun_BgmSoundOnoff ()
    {
        Debug.Log ("BgmSound" + PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"));
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = false;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (false);
            PreviewLabs.PlayerPrefs.SetBool ("BgmSoundOff", false);
            BgmSound.Instance.Stop ();
            PreviewLabs.PlayerPrefs.Flush ();
            return;
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = true;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category0_bgsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (true);
            PreviewLabs.PlayerPrefs.SetBool ("BgmSoundOff", true);
            PreviewLabs.PlayerPrefs.Flush ();
            BgmSound.Instance.Play ();
            return;
        }


    }

    public void Btn_Fun_FxSoundOnoff ()
    {
        Debug.Log ("BgmSound" + PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff"));
        if (PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff")) {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = false;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (false);
            PreviewLabs.PlayerPrefs.SetBool ("FXSoundOff", false);
            PreviewLabs.PlayerPrefs.Flush ();
            return;
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = true;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category1_fxsound/btn_onoff", true).GetComponent<UICheckbox> ().Set (true);
            PreviewLabs.PlayerPrefs.SetBool ("FXSoundOff", true);
            PreviewLabs.PlayerPrefs.Flush ();
            return;
        }
    }

    public void Btn_Fun_ViewUserPic ()
    {
        if (PreviewLabs.PlayerPrefs.GetBool ("viewUserpic")) {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = false;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().Set (false);
            PreviewLabs.PlayerPrefs.SetBool ("viewUserpic", false);
            PreviewLabs.PlayerPrefs.Flush ();


            return;
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = true;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category2_userpic/btn_onoff", true).GetComponent<UICheckbox> ().Set (true);
            PreviewLabs.PlayerPrefs.SetBool ("viewUserpic", true);
            PreviewLabs.PlayerPrefs.Flush ();
            return;
        }
    }

    public void Btn_Fun_MessageAlert ()
    {
        if (PreviewLabs.PlayerPrefs.GetBool ("MessageAlert")) {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = false;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().Set (false);
            PreviewLabs.PlayerPrefs.SetBool ("MessageAlert", false);
            //JCE.JceNotiMessage (Ag.mySelf, "alarmOff");

            PreviewLabs.PlayerPrefs.Flush ();
            return;
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().isChecked = true;
            mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_setting/content_gameset/category3_alert/btn_onoff", true).GetComponent<UICheckbox> ().Set (true);
            PreviewLabs.PlayerPrefs.SetBool ("MessageAlert", true);
            //JCE.JceNotiMessage (Ag.mySelf, "alarmOn");

            PreviewLabs.PlayerPrefs.Flush ();
            return;
        }
    }

    /// <summary>
    /// Setting Account
    /// </summary>
    public void EventWebClose ()
    {
        dicMenuList ["LPanel_event"].SetActive (false);
        if (dicMenuList ["LPanel_event"].transform.FindChild ("check_today").GetComponent<UICheckbox> ().isChecked)
            //PreviewLabs.PlayerPrefs.SetString("EventBannerTimestamp",
			;
    }

    public void Btn_Fun_OpenFaceBook ()
    {
        Application.OpenURL ("https://www.facebook.com/pages/Appsgraphy-CoLtd/324419990985687");
    }

    public void Btn_Fun_OpenHomepage ()
    {
        Application.OpenURL ("");
    }

    public void Btn_Fun_OpenCustomerCenter ()
    {
        Application.OpenURL ("http://cafe.naver.com/ArticleList.nhn?search.clubid=27342122&search.menuid=6&search.boardtype=Q");
    }

    public void Btn_Fun_OpenTerm ()
    {
        Application.OpenURL ("http://cafe.naver.com/ArticleRead.nhn?clubid=27342122&page=1&menuid=13&boardtype=L&articleid=4&referrerAllArticles=false");
    }

    public void Btn_Fun_Seccession ()
    {
        MenuCommonOpen ("Ui_Popup", "game_withdraw", true);
    }

    public void Btn_Fun_withDrawCancel ()
    {
        MenuCommonOpen ("Ui_Popup", "game_withdraw", false);
    }

    void GloveSendMessage (string KakaoID, string DrinkId, string pCont)
    {
        Debug.Log (Ag.mySelf.WAS.KkoNick);
        WasMailSend aObj = new WasMailSend () { User = Ag.mySelf, friendID = KakaoID, itemTypeId = GiftRewardCode(), content = pCont
        };
        aObj.messageAction = (int pInt) => {
        };
    }

    public void Btn_Fun_KakaoLogout ()
    {
        KakaoNativeExtension.Instance.Logout (onLogoutComplete, onLogoutError);

        //KakaoNativeExtension.Instance.Unregister (onLogoutComplete, onLogoutError);
        //PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", false); //튜토리얼 모드 ON
        //PreviewLabs.PlayerPrefs.Flush ();
    }

    public void MessageBlock ()
    {
        KakaoNativeExtension.Instance.blockMessage (!KakaoGameUserInfo.Instance.message_blocked, this.onBlockMessageComplete, this.onBlockMessageError);
    }

    private void onLocalUserComplete ()
    {
        /*
         * please below propery after called localUserComplete
         */
        string nickName = StcPlatform.PltmNick;
        string hashedTalkUserId = StcPlatform.HashedUserID;
        string userId = StcPlatform.UserID;
        string profileImageUrl = StcPlatform.ProfileURL;
        string countryIso = StcPlatform.CountryISO;
        bool messageBlocked = StcPlatform.IsMsgBlocked;
        string alertMessage = "";

        if (nickName != null && nickName.Length > 0) {
            alertMessage += "nickName : ";
            alertMessage += nickName;
            alertMessage += "\n";
        }

        if (hashedTalkUserId != null && hashedTalkUserId.Length > 0) {
            alertMessage += "hashedTalkUserId :";
            alertMessage += hashedTalkUserId;
            alertMessage += "\n";
        }

        if (userId != null && userId.Length > 0) {
            alertMessage += "userId :";
            alertMessage += userId;
            alertMessage += "\n";
        }

        if (profileImageUrl != null && profileImageUrl.Length > 0) {
            alertMessage += "profileImageUrl :";
            alertMessage += profileImageUrl;
            alertMessage += "\n";
        }

        if (countryIso != null && countryIso.Length > 0) {
            alertMessage += "countryIso :";
            alertMessage += countryIso;
            alertMessage += "\n";
        }

        alertMessage += (messageBlocked == true ? "true" : "false");

//        KakaoNativeExtension.Instance.ShowAlertMessage (alertMessage);
    }

    private void onLocalUserError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onSendMessageComplete ()
    {
        MenuCommonOpen ("KickOffpopup", "versusinvite_success", true);
       
    }

    void sendgift_successPopupClose ()
    {
        MenuCommonOpen ("Ui_Popup", "sendgift_success", false);
        FriendRank ();
    }

    void inviteSuccess_successPopupClose ()
    {
        MenuCommonOpen ("Ui_Popup", "invite_success", false);
    }


    void invite_kakaofail_successPopupClose () {
        MenuCommonOpen ("Ui_Popup", "invite_kakaofail", false);
    }

    private void onSendMessageError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onSendImageMessageComplete ()
    {
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Succeed SendImageMessage");
    }

    private void onSendImageMessageError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onSendInviteImageMessageComplete ()
    {
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Succeed SendInviteImageMessage");
    }

    private void onSendInviteImageMessageError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onLogoutComplete ()
    {
        Ag.mGameStartAlready = false;
        Ag.PlatformLogout = true;
        AgStt.NodeClose ();
		
        Resources.UnloadUnusedAssets ();
        Application.LoadLevel ("Title");
    }

    private void onLogoutError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onSendGameMessageComplete ()
    {

        MenuCommonOpen ("Ui_Popup", "sendgift_success", true);
        GloveSendMessage (mUserId, "BlueDrink5", "");
        Ag.mGameObj.transform.FindChild ("on/btn_WaitMessageSend").gameObject.SetActive (true);
        Ag.mGameObj.transform.FindChild ("on/btn_sendgift").gameObject.SetActive (false);
        Ag.mGameObj.transform.FindChild ("on/btn_donsend").gameObject.SetActive (false);
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Succeed SendMessage");
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Succeed SendGameMessage");
    }

    private void onSendGameMessageError (string status, string message)
    {

        Debug.Log (status);
        showAlertErrorMessage (status, message);
        if (status == "-31") {
            MenuCommonOpen ("message_cooltimefail", "Ui_popup", true);
            return;
        }
        if (status == "-11") {
            MenuCommonOpen ("message_Deactivateuser", "Ui_popup", true);
            return;
        }
            MenuCommonOpen("message_fail","Ui_popup",true);
    }

    private void onSendInviteGameMessageComplete ()
    {

        //InviteReward ();
        AlreadySendInviteMessageGetReward ();
        Ag.mGameObj.transform.FindChild ("btn_donsend").gameObject.SetActive (true);
        Ag.mGameObj.transform.FindChild ("btn_invite").gameObject.SetActive (false);
        MenuCommonOpen ("Ui_Popup", "invite_success", true);
         
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Succeed SendInviteGameMessage");
    }

    private void onSendInviteGameMessageError (string status, string message)
    {

        MenuCommonOpen("message_fail","Ui_popup",true);
        showAlertErrorMessage (status, message);
    }

    private void onAcceptGameMessageComplete ()
    {
//        KakaoNativeExtension.Instance.ShowAlertMessage ("Completed accept message!");
    }

    private void onAcceptGameMessageError (string status, string message)
    {
        showAlertErrorMessage (status, message);
        MenuCommonOpen("message_fail","Ui_popup",true);

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

    protected void showAlertErrorMessage (string code, string message)
    {     
        /*
        string alertMessage = "";
        if (code != null) {
            alertMessage += ("Error Code : " + code);
        }

        if (message != null) {
            alertMessage += "\n";
            alertMessage += ("Error Message : " + message);
        }

        KakaoNativeExtension.Instance.ShowAlertMessage (alertMessage);
        */

    }
    //Load Game Friends
    private void onLoadGameFriendsComplete ()
    {
        Debug.Log ("onLoadGameFriendsComplete");
        KakaoGameFriends.Instance.printToConsole ();
    }

    private void onLoadGameFriendsError (string status, string message)
    {
        Debug.Log ("onLoadGameFriendsError");
        showAlertErrorMessage (status, message);
    }

    private void onLoginComplete ()
    {

        Application.LoadLevel ("Title");
    }

    private void onLoginError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onUnregisterComplete ()
    {
    }

    private void onUnregisterError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }
}

   
