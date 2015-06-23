using UnityEngine;
using System.Collections;

public class PushNoti : MonoBehaviour {
    // Android call back Methods ...

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad (this.gameObject);
        #if UNITY_ANDROID

        try {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            //jo.Call ("PushRegid");
            jo.Call ("rootingCheck");
        } catch {
            " OSX Editor    ....    OK  ".HtLog ();
        }
        //Debug.Log("");
        #endif
	}

    public void RegisterPushId (string id) {
        Ag.LogDouble (id + "Register ID");

        Fb.AndroidRegistrationID = id;
        //JCE.JceNotiTokenSetting(Ag.mySelf);
    }

    public void CheckRooting (string id) {
        Ag.rootingFlag = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
