using UnityEngine;
using System.Collections;

public class ShadowPosition : MonoBehaviour {
    GameObject mPlayerBip, mShadowObj;
	// Use this for initialization
	void Start () {
        mPlayerBip =  this.gameObject.transform.FindChild("Bip001").gameObject;
        mShadowObj = this.gameObject.transform.FindChild("ShadowPlane").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        mShadowObj.transform.localPosition = new Vector3(mPlayerBip.transform.localPosition.x,0.01f,mPlayerBip.transform.localPosition.z);
	}
}
