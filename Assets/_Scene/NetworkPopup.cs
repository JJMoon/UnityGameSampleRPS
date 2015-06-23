using UnityEngine;
using System.Collections;

public class NetworkPopup : AmSceneBase
{
    GameObject mNetworkErrorPopup, mUIpopup;
    // Use this for initialization
    public override void Start ()
    {
        //mRscrcMan = new HtRsrcMan ("");
        mUIpopup = FindGameObject ("Ui_camera/Camera/Ui_popup", false);
        mRscrcMan.AddComponentUISendMessage (FindMyChild (mUIpopup, "alert_networkerror/btn_ok", true), this.gameObject, "NetworkPopupClose");
    }
    // Update is called once per frame
    public override void Update ()
    {

    }
        
    public void NetworkPopupOpen ()
    {
        mUIpopup.SetActive (true);
    }

    public void NetworkPopupClose ()
    {
        //Ag.HttpNetworkFailure = false;
        Ag.NetExcpt.Recover ();
        //AgStt.IsGaming = null;
        Ag.mGameStartAlready = false;
        mUIpopup.SetActive (false);
        Application.LoadLevel ("Title");
    }
//
//    public void NetworkPopupResource ()
//    {
//        if (Application.loadedLevel == "GameScene")
//            mUIpopup = FindGameObject ("Ui_camera/Camera/Ui_popup", false);
//        mRscrcMan.AddComponentUISendMessage (FindMyChild (mUIpopup, "alert_networkerror/btn_ok", true), this.gameObject, "NetworkPopupClose");
//    }
}
