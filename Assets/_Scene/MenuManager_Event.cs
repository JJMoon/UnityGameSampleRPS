using UnityEngine;
using System.Collections;
using System;

public partial class MenuManager : AmSceneBase {
    /// <summary>
    /// 리뷰 팝업 끄기
    /// </summary>

    void ReviewPopupClose () {
        MenuCommonOpen ("popup_review", "Ui_popup", false);
        PreviewLabs.PlayerPrefs.SetString ("ReviewStampTime" , UtTimestamp.ToMilliTimestamp().ToString());
        PreviewLabs.PlayerPrefs.Flush();
    }
    /// <summary>
    /// 리뷰페이지로 이동
    /// </summary>
    void ReviewPopupOK() {

        Application.OpenURL ("https://play.google.com/store/apps/details?id=com.appsgraphy.KVSdev");
        MenuCommonOpen ("popup_review", "Ui_popup", false);
        WasReview aObj = new WasReview () { User = Ag.mySelf };
            aObj.messageAction = (int pInt) => {
        };
    }
	/*
	dicMenuList.Add ("LPanel_olclock",  FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock", false));
	dicMenuList.Add ("popup_oclock", mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock", false));
	mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/btn_oclock", true), mTargetObj, "Oclock_event_Open");
	mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock/btn_close", true), mTargetObj, "Oclock_event_Close");
	mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_olclock/popup_oclock/btngrid/btn_ok", true), mTargetObj, "Oclock_event_Close");
	*/

    public DateTime mDtobj;
	void Oclock_event_Open () {
		dicMenuList["popup_oclock"].SetActive(true);

        mDtobj = System.DateTime.Now ; //.AddHours(1);
	}

	void Oclock_event_Close () {
		dicMenuList["LPanel_olclock"].SetActive(false);
		dicMenuList["popup_oclock"].SetActive(false);

        AgStt.NetManager.HaveSeenHourlyEvent = false;
	}
}
