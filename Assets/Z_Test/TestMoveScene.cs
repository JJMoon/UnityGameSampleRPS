using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class TestMoveScene : MonoBehaviour {
    static IntPtr cls_Activity;
    static IntPtr fid_Activity;
    static IntPtr obj_Activity;
    static IntPtr cls_OurAppNameActivityClass;
    static IntPtr startAdsMethod;
    public String mRootTestStr1, mRootTestStr2;


	// Use this for initialization
	
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI () {
#if UNITY_ANDROID
        /*
        if (GUI.Button (new Rect (100, 100, 200, 50), "RootingTest")) {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            jo.Call ("AppRootingTest","zzzzz");
            jo.Call ("registGCM");
            //jo.Call ("kakao","zzzzz");
        }
        if (GUI.Button (new Rect (100, 350, 200, 50), "Login")) {
            Application.LoadLevel("Login");
        }
        if (GUI.Button (new Rect (100, 450, 200, 50), "Menu")) {
            Application.LoadLevel("StartMenu");
        }
        if (GUI.Button (new Rect (100, 550, 200, 50), "Match")) {
            //Application.LoadLevel("GameScene");
        }


        GUI.Label (new Rect (100, 200, 200, 50), "RootingTest  :" + mRootTestStr1);
        GUI.Label (new Rect (100, 300, 200, 50), "GameAutoPlay  :" + mRootTestStr1);
        */
#endif
    }
}
