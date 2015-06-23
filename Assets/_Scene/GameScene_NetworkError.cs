using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase {
    void NetworkErrorPopupClose() {
        Ag.mGameStartAlready = false;
        //AgStt.IsGaming = null;
        //Ag.NetExcpt.Recover();
        Application.LoadLevel("Title");

    }


    /// <summary>
    /// 중복로그인해서 에러났을때 뜨는창 
    /// </summary>
    void DoubleLoginError () {
        dicGameSceneMenuList ["popup"].SetActive (true);
        dicGameSceneMenuList["alert_doublelogin"].SetActive(true);
    }
    
    
    /// <summary>
    /// 중복로그인해서 게임끄기
    /// </summary>
    void AlertDoubleLoginRestart () {
        Application.Quit();
    }

}
