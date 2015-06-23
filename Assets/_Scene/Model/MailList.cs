using UnityEngine;
using System.Collections;


public class MailList : AmSceneBase {
    public AmMail mMail;
    public string mUrl, mImagePath;
    public string ContLabel;
    // Use this for initialization
    public override void Start () {
        mRscrcMan = new HtRsrcMan ("");

    }

    // Update is called once per frame
    public override void Update () {


    }

    void ViewNotice () {
        Application.OpenURL ("http://www.joycity.com");

    }

    void GotoAppUrl () {
        //Application.OpenURL (mUrl);
        GameObject Gobj;
        Material ImageBannerPic;
        ImageBannerPic = Instantiate (Resources.Load ("Materials/Imagebanner")) as Material;
        Gobj = (GameObject)Instantiate (Resources.Load ("prefab_General/Lpanel_Event"));
        Gobj.transform.parent = mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Ui_lobby"].gameObject.transform;
        Gobj.transform.localPosition = new Vector3 (0, 0, -295);
        Gobj.transform.localScale = new Vector3 (1, 1, 1);
        Gobj.name = "JoyCityImageBanner";
        //Gobj.GetComponent<CloseThisObject> ().mTimestamp = Joycity.arrImageNoti [i].timestamp;
        Debug.Log (mUrl + "URL");
        Gobj.GetComponent<CloseThisObject> ().mUrl = mUrl;

        Gobj.transform.FindChild ("banner").gameObject.GetComponent<UITexture> ().material = ImageBannerPic;
        StartCoroutine (mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().JoycityImageBannerLoad (mImagePath, Gobj.transform.FindChild ("banner").gameObject));
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_close", true), Gobj, "DestoryObj");
        mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (Gobj, "btn_datail", true), Gobj, "OpenUrl");

    }

    void DeleteGmMessage () {
        DestroyObject (this.gameObject);
        MailFlag.OpenMailBoxFlag = true;

        mRscrcMan.FindGameObject ("Axis/Camera/Match", true).SendMessage ("Post_Reposition",this.gameObject.name);
    }

    void ReceiveMail () {

        mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager>().dicMenuList["CenterCircle"].SetActive(true);

        MailFlag.OpenMailBoxFlag = true;
        WasMailErase aObj = new WasMailErase () { User = Ag.mySelf, msgID1 = mMail.WAS.msgID1, 
            msgID2 = mMail.WAS.msgID2
        };
        aObj.messageAction = (int pInt) => {
            mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager>().dicMenuList["CenterCircle"].SetActive(false);
            switch (pInt) {
            case 0:
                mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager>().messageReceiveCardinfo();
                mRscrcMan.FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager>().Btn_Fun_PostBoxOpen();
                //mRscrcMan.FindGameObject ("Axis/Camera/Match", true).SendMessage ("Post_Reposition",this.gameObject.name);
                break;
            }
        };
        //StartCoroutine (MailListUpdate ());
    }

    public void NoticeMailInit () {

        this.gameObject.transform.FindChild ("Label_content").GetComponent<UILabel>().text = ContLabel;
    }

}
