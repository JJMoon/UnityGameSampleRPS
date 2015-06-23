using UnityEngine;
using System.Collections;

public class GameSceneLoad : MonoBehaviour {
    GameObject mGame;
	// Use this for initialization
	void Start () {

        mGame = GameObject.Find("Ui_camera").gameObject;
        mGame.transform.FindChild("Camera/Ui_loadprefeb/panel_tip"+ AgUtil.RandomInclude(10,21)).gameObject.SetActive(true);

        Application.LoadLevel("StartMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
