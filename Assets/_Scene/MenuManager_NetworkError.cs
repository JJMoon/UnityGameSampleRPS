using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase {

    void NetworkReboot () {
        //Debug.Log ("AgStt.IsGaming.HasValue  : " + AgStt.IsGaming.HasValue + "AgStt.IsGaming.Value  : ");
        //if (AgStt.IsGaming.HasValue && AgStt.IsGaming.Value == true) {
        if (Ag.CurrentScene == "GAME") {
            MenuCommonOpen("alert_restart","Ui_popup",false);
            Ag.mGameStartAlready = false;
            //AgStt.IsGaming = null;
            Ag.NetExcpt.Recover();
            Ag.PlatformLogout = true;
            Application.LoadLevel("Title");
        } else {
            //WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };
            MenuCommonOpen("alert_networkerror","Ui_popup",false);
            dicMenuList["CenterCircle"].SetActive(false);
            Ag.NetExcpt.Recover();
        }
    }

    /// <summary>
    /// 중복로그인해서 에러났을때 뜨는창 
    /// </summary>
    void DoubleLoginError () {
        MenuCommonOpen("alert_doublelogin","Ui_popup",true);
     
    }


    /// <summary>
    /// 중복로그인해서 게임끄기
    /// </summary>
    void AlertDoubleLoginRestart () {
        Application.Quit();
    }


    void message_fail_close () {
        MenuCommonOpen("message_fail","Ui_popup",false);
    }

    void message_cooltimefail_close () {
        MenuCommonOpen("message_cooltimefail","Ui_popup",false);
    }

    void message_Deactivateuser_close () {
        MenuCommonOpen("message_Deactivateuser","Ui_popup",false);
    }


}
