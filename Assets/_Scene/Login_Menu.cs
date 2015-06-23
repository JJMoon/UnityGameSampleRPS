//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;

public partial class Login_Register : AmSceneBase
{
    public AmUniform Uniform = new AmUniform ();
    //-------------------------------------------------------------------------------BUTTON
    //구단 이름 팝업 닫기
    void GudanNameClose ()
    {

        mRscrcMan.FindChild (mMakeGudan, "nations/btn_left", true).GetComponent<Collider> ().enabled = false;
        mRscrcMan.FindChild (mMakeGudan, "nations/btn_right", true).GetComponent<Collider> ().enabled = false;
        //string pstr = "";
        Ag.LogString ("  test is " + mGudanname.ToString ());
        if (mGudanname.GetComponent<UIInput> ().text.Length > 9) {
            mGudanCheckLabel.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EB%8B%89%EB%84%A4%EC%9E%84%20%EC%9E%85%EB%8B%88%EB%8B%A4.");
            return;
        }
        mLoadingCircle.SetActive (true);

        string inputText = WWW.EscapeURL (mGudanname.GetComponent<UIInput> ().text);
        Ag.LogDouble (" Login_Menu ::  GudanNameClose () ");
        //mRscrcMan.FindGameObject ("JSonObject", true).SendMessage ("InitNode", mGudanname.GetComponent<UIInput> ().text.ToUpper ());

        WasTeamCheck aObj = new WasTeamCheck () { ID = Ag.mySelf.WAS.KkoID, TgtName = inputText };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
            case 1:
                Ag.mySelf.WAS.Country = mXNum;
                Ag.LogIntenseWord (" WasRegist : Success  >> " + pInt);
                Ag.mySelf.WAS.TeamName = inputText; // 팀 이름 설정
                RegistProcess ();
                mMakeGudan.SetActive (false);
                mLoadingCircle.SetActive (false);
                mPushLabel.SetActive (true);
                mPushLabelLoadName.SetActive (true);
                LocalSettingSave ();
                PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", false);
                PreviewLabs.PlayerPrefs.Flush();
                break;
            case -1:
                Ag.LogIntenseWord ("  WasTeamCheck ::  result :: -1 ");
                mMakeGudan.SetActive (true);
                mGudanCheckLabel.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%A4%91%EB%B3%B5%EB%90%9C%20%EC%9D%B4%EB%A6%84%20%EC%9E%85%EB%8B%88%EB%8B%A4.");
                mLoadingCircle.SetActive (false);
                mRscrcMan.FindChild (mMakeGudan, "nations/btn_left", true).GetComponent<Collider> ().enabled = true;
                mRscrcMan.FindChild (mMakeGudan, "nations/btn_right", true).GetComponent<Collider> ().enabled = true;
                break;
            case -2:
                Ag.LogIntenseWord ("  WasTeamCheck ::  result :: -2 ");
                mMakeGudan.SetActive (true);
                mGudanCheckLabel.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%82%AC%EC%9A%A9%ED%95%A0%EC%88%98%20%EC%97%86%EB%8A%94%20%EB%8B%89%EB%84%A4%EC%9E%84%20%EC%9E%85%EB%8B%88%EB%8B%A4.");
                mLoadingCircle.SetActive (false);
                mRscrcMan.FindChild (mMakeGudan, "nations/btn_left", true).GetComponent<Collider> ().enabled = true;
                mRscrcMan.FindChild (mMakeGudan, "nations/btn_right", true).GetComponent<Collider> ().enabled = true;
                break;
            }
        };
    }

    void RegistProcess ()
    {
        if (!Ag.mGuest && (Ag.mySelf.WAS.KkoID == null && Ag.mySelf.WAS.KkoID.Length < 8)) {
            Ag.LogIntenseWord ("  Regist Error ....    >>>>>    if (!Ag.mGuest && (Ag.mySelf.WAS.KkoID == null && Ag.mySelf.WAS.KkoID.Length < 8)) {  ");
            return;
        }

        WasRegist aObj = new WasRegist () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
            case 1:
            case 7:
                Ag.LogIntenseWord (" Regist : Success ");
                RegistLoginWas ();
                //StNet.Login(Ag.mySelf, out goToLogin);
                break;
            case -1:
            case 4:
                break;
            }
        };
    }

    public void FetchEventList ()
    {
        if (Ag.NodeObj == null) 
            AgStt.NodeOpen ();


        if (Ag.rootingFlag) {
            mPopup2.SetActive (true);
            //dicMenuList ["alert_rooting"].SetActive (true);
            return;
        }

        WasEventList aObj = new WasEventList () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0:
                InappPurchaseSotre ();
                break;
            case 1:
                break;
            case -1:
            case 4:
                break;
            }
            aObj = null;
        };
        WasPopupStoreList bObj = new WasPopupStoreList () { User = Ag.mySelf };
        bObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0:
                break;
            }
            bObj = null;
        };
    }

    bool LoadLevelStartMenu =false;

    public void InappPurchaseSotre () {
        WasPopupStoreIAPurchaseList aObj = new WasPopupStoreIAPurchaseList () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) {
            case 0:
                StopTimer ();

                if (!LoadLevelStartMenu) {
                    LoadLevelStartMenu = true;
                    Application.LoadLevel ("StartMenu");
                }

                //Application.LoadLevel ("StartMenu");
                break;
            case 1:
                break;
            case -1:
            case 4:
                break;
            }

            aObj = null;
        };
    }


    public void RegistUniform ()
    {
        WasUniformUpdate aObj = new WasUniformUpdate () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Ag.LogString (" result : Success ");
                break;
            }
        };
    }

    public void RegistUserInfo ()
    {

        Ag.LogIntenseWord (" RegistUserInfo  KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);

        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 1 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                //UniformInit ();
                //Ag.mVirServer.SetUniform ();
                UniformInit ();
                RegistUniform ();
                RegCardUpdate ();
                break;
            case -1:
            case 4:
                return;
            }
            Ag.mySelf.WAS.profileURL = StcPlatform.ProfileURL;
            AgStt.NodeClose ();
            AgStt.NodeOpen ();
        };
    }

    public void RegistLoginWas ()
    {
        Ag.LogIntenseWord ("  RegistLoginWas  KKO  ID ::::::  " + Ag.mySelf.WAS.KkoID);

        WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };

        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                mMainFlag = true;
                mPushLabel.SetActive (true);
                mPushLabelLoadName.SetActive (true);
                mPushLabelLoadName.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EA%B2%BD%EA%B8%B0%EC%9E%A5%EC%97%90%20%EC%9E%85%EC%9E%A5%EC%A4%91%EC%9E%85%EB%8B%88%EB%8B%A4.");
                RegistUserInfo ();
                Ag.mySelf.WAS.KkoNick = StcPlatform.PltmNick;
                Ag.mySelf.KkoNickEncode = WWW.EscapeURL (StcPlatform.PltmNick);
                Ag.mySelf.TeamNameEncoded = WWW.EscapeURL (Ag.mySelf.WAS.TeamName);
                Ag.mySelf.HeartSetMax ();
                break;
            case -1:
            case 4:
                mMakeGudan.SetActive (true);  // 사용자 등록 화면을 띄운다. 
                mPushLabel.SetActive (false);
                mPushLabelLoadName.SetActive (false);
                //mRscrcMan.FindGameObject("Ui_camera/Camera/Ui_title/loding_alert1",false);
                Ag.mySelf.HeartSetMax ();
                return;
            }
        };
    }

    public void SetUniform ()
    {
        int sMai = AgUtil.RandomInclude (5, 12), sSub = AgUtil.RandomInclude (5, 12);
        if (sSub == sMai)
            sSub = (sSub > 8) ? sSub - 1 : sSub + 1;


        Uniform.Kick.SetValue (1, sMai, sSub,
            1, AgUtil.RandomInclude (1, 3), AgUtil.RandomInclude (1, 3),
            1, sMai, sSub);
        sMai = AgUtil.RandomInclude (5, 12);
        sSub = AgUtil.RandomInclude (5, 12);
        if (sSub == sMai)
            sSub = (sSub > 8) ? sSub - 1 : sSub + 1;

        Uniform.Keep.SetValue (1, sMai, sSub,
            1, AgUtil.RandomInclude (1, 3), AgUtil.RandomInclude (1, 3),
            1, sMai, sSub);
    }

    void UniformInit ()
    {
        SetUniform ();
        //Ag.mySelf.arrUniform [0] = Uniform;
        Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = Uniform.Kick.Shirt.Texture;
        Ag.mySelf.arrUniform [0].Kick.Pants.Texture = Uniform.Kick.Pants.Texture;
        Ag.mySelf.arrUniform [0].Kick.Socks.Texture = Uniform.Kick.Socks.Texture;
        Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = Uniform.Keep.Shirt.Texture;
        Ag.mySelf.arrUniform [0].Keep.Pants.Texture = Uniform.Keep.Pants.Texture;
        Ag.mySelf.arrUniform [0].Keep.Socks.Texture = Uniform.Keep.Socks.Texture;

        Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain = Uniform.Keep.Shirt.ColMain;
        Ag.mySelf.arrUniform [0].Kick.Pants.ColMain = Uniform.Keep.Pants.ColMain;
        Ag.mySelf.arrUniform [0].Kick.Socks.ColMain = Uniform.Keep.Socks.ColMain;
        Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub = Uniform.Keep.Shirt.ColSub;
        Ag.mySelf.arrUniform [0].Kick.Pants.ColSub = Uniform.Keep.Pants.ColSub;
        Ag.mySelf.arrUniform [0].Kick.Socks.ColSub = Uniform.Keep.Socks.ColSub;

        Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain = Uniform.Keep.Shirt.ColMain;
        Ag.mySelf.arrUniform [0].Keep.Pants.ColMain = Uniform.Keep.Pants.ColMain;
        Ag.mySelf.arrUniform [0].Keep.Socks.ColMain = Uniform.Keep.Socks.ColMain;
        Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub = Uniform.Keep.Shirt.ColSub;
        Ag.mySelf.arrUniform [0].Keep.Pants.ColSub = Uniform.Keep.Pants.ColSub;
        Ag.mySelf.arrUniform [0].Keep.Socks.ColSub = Uniform.Keep.Socks.ColSub;
    }
    //구단 이름 팝업 확인
    void GudanNameOK ()
    {
        mMakeGudan.SetActive (false);
        //mCountryList.SetActive (false);
    }
    //정보동의 팝업 닫기
    void ProvisiningClose ()
    {
        mProvisioning.SetActive (false);
    }
    //정보동의 팝업 확인
    void ProvisiningOk ()
    {
        mProvisioning.SetActive (false);
    }
    //타이틀 화면 게스트
    void Title_Guest ()
    {
        Ag.LogIntenseWord (" StartCoroutine >>>   Wait and Go to Menu   ...   Load   ...  >>>   MenuPreLoadingScene  ");
        //mProvisioning.SetActive (false);
        StartCoroutine (WaitAndGotoMenu (2f));
    }
    //타이틀 화면 카카오로그인
    void Title_KakaoLogin ()
    {
        StartCoroutine (WaitAndGotoMenu (2f));
        //mProvisioning.SetActive (false);
    }
    // 웹뷰 패널 끄기
    void Webviewpanelclose ()
    {
        dicMenuList ["Webview"].SetActive (false);
    }
    //  점검 패널 끄기
    void RepairPanelClose ()
    {
        dicMenuList ["Repair"].SetActive (false);
    }
    //  Logo Open
    IEnumerator WaitAndGotoMenu (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        StopTimer ();
        Application.LoadLevel ("MenuPreLoadingScene");
    }
    //-----------------------------------------------------------------------------------
}
