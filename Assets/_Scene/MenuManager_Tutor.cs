using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase {
    void btn_fun_PlayerinfoTutorial () {
        menututor_cardinfo_Close();
    }


	void InitMenuTutor () {
        dicMenuList["menututor_cardmix"].SetActive(false);
        dicMenuList["menututor_cardmix2"].SetActive(false);
        dicMenuList["menututor_lineup"].SetActive(false);
        dicMenuList["menututor_lineup2"].SetActive(false);
        dicMenuList["menututor_lineup3"].SetActive(false);
        dicMenuList["menututor_lineup4"].SetActive(false);
        dicMenuList["menututor_lobby"].SetActive(false);
    }

    void addsendmessageTutorPanel () {
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_lobby"], mTargetObj, "menututor_lobby_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_cardmix"], mTargetObj, "menututor_cardmix_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_cardmix2"], mTargetObj, "menututor_cardmix2_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_lineup"], mTargetObj, "menututor_lineup_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_lineup2"], mTargetObj, "menututor_lineup2_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_lineup3"], mTargetObj, "menututor_lineup3_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_lineup4"], mTargetObj, "menututor_lineup4_Close");
        mRscrcMan.AddComponentUISendMessage (dicMenuList["menututor_cardinfo"], mTargetObj, "menututor_cardinfo_Close");

        dicMenuList["menututor_lobby"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_lobby"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_cardmix"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_cardmix"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_cardmix2"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_cardmix2"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_lineup"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_lineup"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_lineup2"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_lineup2"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_lineup3"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_lineup3"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_lineup4"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_lineup4"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);
        dicMenuList["menututor_cardinfo"].AddComponent<BoxCollider>().center = new Vector3(0,0,-53f);
        dicMenuList["menututor_cardinfo"].GetComponent<BoxCollider>().size = new Vector3(1400,1000,0);


    }

    void menututor_lobby_Close () {
        dicMenuList["Ui_menututorial"].SetActive(false);
        dicMenuList["menututor_lobby"].SetActive(false);
    }

    void menututor_cardmix_Close () {
        dicMenuList["Ui_menututorial"].SetActive(true);
        dicMenuList["menututor_cardmix"].SetActive(false);
        dicMenuList["menututor_cardmix2"].SetActive(true);
    }

    void menututor_cardmix2_Close () {
        dicMenuList["Ui_menututorial"].SetActive(false);
        dicMenuList["menututor_cardmix2"].SetActive(false);
        PreviewLabs.PlayerPrefs.SetBool("MenuTutorCardMix",true);
        PreviewLabs.PlayerPrefs.Flush();
    }

    void menututor_lineup_Close () {
        dicMenuList["Ui_menututorial"].SetActive(true);
        dicMenuList["menututor_lineup"].SetActive(false);
        dicMenuList["menututor_lineup2"].SetActive(true);

    }
    
    void menututor_lineup2_Close () {
        dicMenuList["Ui_menututorial"].SetActive(true);
        dicMenuList["menututor_lineup2"].SetActive(false);
        dicMenuList["menututor_lineup3"].SetActive(true);
    }

    void menututor_lineup3_Close () {
        dicMenuList["Ui_menututorial"].SetActive(true);
        dicMenuList["menututor_lineup3"].SetActive(false);
        dicMenuList["menututor_lineup4"].SetActive(true);
    }

    void menututor_lineup4_Close () {
        dicMenuList["Ui_menututorial"].SetActive(false);
        dicMenuList["menututor_lineup4"].SetActive(false);
        PreviewLabs.PlayerPrefs.SetBool("MenuTutorLineup",true);
        PreviewLabs.PlayerPrefs.Flush();
    }

    int mCardinfoTutor = 1;
    public void menututor_cardinfo_Close () {
        dicMenuList["Ui_menututorial"].SetActive(true);
        dicMenuList["menututor_cardinfo"].SetActive(true);
        for (int i = 1; i < 6; i++) {
            dicMenuList["menututor_cardinfo"].transform.FindChild("descript"+i).gameObject.SetActive(false);
            dicMenuList["menututor_cardinfo"].transform.FindChild("finger_card"+i).gameObject.SetActive(false);
        }
        if (mCardinfoTutor >= 1 && mCardinfoTutor <= 5){
            dicMenuList["menututor_cardinfo"].transform.FindChild("descript"+mCardinfoTutor).gameObject.SetActive(true);
            dicMenuList["menututor_cardinfo"].transform.FindChild("finger_card"+mCardinfoTutor).gameObject.SetActive(true);
        }
        
        if (mCardinfoTutor == 6) {
            dicMenuList["Ui_menututorial"].SetActive(false);
            dicMenuList["menututor_cardinfo"].SetActive(false);
            PreviewLabs.PlayerPrefs.SetBool("MenuTutorPlayerInfo",true);
            PreviewLabs.PlayerPrefs.Flush();
            mCardinfoTutor = 0;
        }

        mCardinfoTutor++;

    }
}
