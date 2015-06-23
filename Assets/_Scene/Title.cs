//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class Title : AmSceneBase
{
    public GameObject mLodingBar, mAppLogoPanel, mJoyLogo;

    IEnumerator WaitAndPrint ()
    {
        mJoyLogo.SetActive(true);
        yield return new WaitForSeconds (1f);
        CheckWasServerVision ();
        yield return new WaitForSeconds (1f);

        Application.LoadLevel ("Login");
    }

    void CheckWasServerVision () {
        Debug.Log ("Start CheckWas ServerVision");
        WasServerVersion aObj = new WasServerVersion () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            aObj = null;
        };
    }

//    public IEnumerator Awake ()
//    {
//        return 0;
//        //base.Awake ();
//        //DontDestroyOnLoad (this.gameObject);
//    }
    // Use this for initialization



    public override void Start ()
    {
        //Ag.GameStt.TitleSceneBegan ();

        base.Start ();
        //CheckWasServerVision ();
        //Debug.Log ("Getbool" + PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff"));
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        #if UNITY_IPHONE
        NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound);
        Ag.LogIntenseWord ("NotificationServices.  Register For Remote NotificationTypes  ");
        #endif

        ("  Title :: Start    ").HtLog ();
        //Ag.mFBOrder = "ConNet";
        GameInit ();
        StartCoroutine (WaitAndPrint ());
				JCE.JceUrgentNoticePT(Ag.mySelf);
        JCE.JceTextNoticePT(Ag.mySelf);


        if (AgUtil.IsLGeVuModel()) Screen.SetResolution((Screen.height/2)*3 ,Screen.height,true);

    }
    // Update is called once per frame
    public override void Update ()
    {
        base.Update ();
        if(Input.GetMouseButtonDown(0)) {
            Application.LoadLevel ("Login");
        }
    
    }

    public override void OnGUI ()
    {
        base.OnGUI ();
        
    }

    void GameInit ()
    {
        ("  Title :: GameInit   ").HtLog ();
        try {
//            mLodingBar = mRscrcMan.FindGameObject ("Ui_cameraLogo/Camera/Ui_loading", false);
            //mAppLogoPanel = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_logo/bundle_apps", false);
			mJoyLogo = mRscrcMan.FindGameObject ("Ui_camera/Camera/Ui_logo/bundle_apps", true);
        } catch {
            ("Title  ::  GameInit ()   __________ >>>> Catch <<<< __________  Parse      E R R O R   ").HtLog ();
        }
    }
}
