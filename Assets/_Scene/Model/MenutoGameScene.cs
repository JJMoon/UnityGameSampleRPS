using UnityEngine;
using System.Collections;

public class MenutoGameScene : MonoBehaviour {
    GameObject mUICamera;
	// Use this for initialization
	void Start () {
        //Debug.Log (AgUtil.RandomInclude(0,9));
        mUICamera = GameObject.Find("Ui_camera");
        mUICamera.transform.FindChild("Camera/Ui_loadprefeb/panel_tip"+ AgUtil.RandomInclude(0,9)).gameObject.SetActive(true);

        Application.LoadLevel("GameScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
