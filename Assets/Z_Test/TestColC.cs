using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;
using LitJson;
using SimpleJSON;

// http://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa  dictionary <-> object   mapping example ..
public partial class Test : AmSceneBase
{
    int packetNum = 610;

    public  void SetColumnC ()
    {
        int colN = 0, colEA;
        muiCol++;
        muiRow = 0;

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  ***  _____  column 3  _____
        Rect rect004 = myGUI.GetRect (muiCol, muiRow++);
        
        int h, m, s;  // 타이머 테스트 
        timerObj.TimeLeft (out h, out m, out s);
        GUI.Label (myGUI.DivideRect (rect004, 3, 0), h + ":" + m + ":" + s + "  " + timerObj.DidTimerFinished () + " , " + timerObj.SecondsLeft ());

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Go to   _____    Node Screen   _____
        if (GUI.Button (myGUI.DivideRect (rect004, 3, 2), "Node >")) {
            IsNodeScreen = true;
        }

        colN = 0;
        Rect rectPlugin = myGUI.GetRect (muiCol, muiRow++);
//        if (GUI.Button (myGUI.DivideRect (rectPlugin, 3, colN++), "JailBrk")) {
//            pluginObj.CheckRootingJailbreak ();
//        }
//        if (GUI.Button (myGUI.DivideRect (rectPlugin, 3, 1), "Percent")) {
//            AgUtil.LinearPercentVari (100, 10000, 3800);
//
//        }

        string msggg = "";
        #if UNITY_IPHONE
        if (NotificationServices.deviceToken == null)
            msggg = "null";
        else
            msggg = " Token ";
        #endif
        if (GUI.Button (myGUI.DivideRect (rectPlugin, 3, colN++), msggg)) {
            JCE.JceNotiTokenSetting (myUser);
        }
        if (GUI.Button (myGUI.DivideRect (rectPlugin, 3, colN++), "Noti " + Ag.arrNoti.Count)) {
//            arrNoti = NotificationServices.remoteNotifications;
//            for (int k = 0; k < arrNoti.Length; k++) {
//                RemoteNotification curNoti = arrNoti [k];
//                Ag.LogString ("  Notification ::  alertBody > " + curNoti.alertBody + "     userInfo.Count > " + curNoti.userInfo.Count +
//                "   badgeNum > " + curNoti.applicationIconBadgeNumber);
//
//                Ag.arrNoti.Add (new AmNotification () { msg = curNoti.alertBody });
//            }
        }
        if (GUI.Button (myGUI.DivideRect (rectPlugin, 3, colN++), "SendNoti")) {
            JCE.JceNotiSendMessage (myUser, " 안드로이드 iOS .. .. ");
        }


       

        //  _////////////////////////////////////////////////_    _///////////////////////_    _____  SingleTry  _____  Test  _____
        Rect rect005 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 5;
        if (GUI.Button (myGUI.DivideRect (rect005, colEA, colN++), "Try A")) {
            myUser.ShowSingleTry (false).ToString ().HtLog ();
        }
        if (GUI.Button (myGUI.DivideRect (rect005, colEA, colN++), "Confirm")) {
            myUser.ConfirmSingleTry (false);
        }
        if (GUI.Button (myGUI.DivideRect (rect005, colEA, colN++), "Try S")) {
            myUser.ShowSingleTry (true).ToString ().HtLog ();
        }
        if (GUI.Button (myGUI.DivideRect (rect005, colEA, colN++), "Confirm")) {
            myUser.ConfirmSingleTry (true);
        }
        if (GUI.Button (myGUI.DivideRect (rect005, colEA, colN++), "Init")) {
            myUser.InitSingleTry ();
        }

        Rect rect006 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 5;
        int mm, ss;
        myUser.HeartCoolTime (out mm, out ss);
        mm = myUser.HeartCoolTime (); // % 
        if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), "H  " + mm + " %, S " + ss)) {
            myUser.HeartCoolTimeNewGameStarted ();
            //myUser.HeartSetMax (); // Set Maximum
        }
        if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), " SetMax")) {
            myUser.HeartSetMax ();
        }
        if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), " CurTime")) {
            ("   Remain Sec ::   " + myUser.CurrentRemainSec ()).HtLog ();
        }
        // Scout
//        myUser.ScoutCoolTimeRemain (out mm, out ss);
//        if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), "S  " + mm + ":" + ss)) {
//            myUser.CoolTimeScoutUse ();
//        }

        myUser.ContWinCoolTimeRemain (out mm, out ss);
        if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), "CW  " + mm + ":" + ss)) {
            myUser.CoolTimeChooseOneMoreGameWin ();
            myUser.ContWinCoolTimeRemainPercent ().ToString ().HtLog ();  // + or - ... 
        }


        Rect rect007 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (rect007, colEA, colN++), "PopupIAPL")) {
            WasPopupStoreIAPurchaseList aObj = new WasPopupStoreIAPurchaseList () { User = myUser };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (rect007, colEA, colN++), "PopupL")) {
            WasPopupStoreList aObj = new WasPopupStoreList () { User = myUser };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (rect007, colEA, colN++), "Popup")) {
            WasPopupPurchase aObj = new WasPopupPurchase () { User = myUser, PopupCode = "DiscHeartDay" };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
            aObj = new WasPopupPurchase () { User = myUser, PopupCode = "DiscHeartWeek" };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
            aObj = new WasPopupPurchase () { User = myUser, PopupCode = "DiscHeartMonth" };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (rect007, colEA, colN++), "PopPurchase")) {
            WasPopupPurchase aObj = new WasPopupPurchase () { User = myUser, PopupCode = "DiscGlove" };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }

        Rect rctNoti = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        if (GUI.Button (myGUI.DivideRect (rctNoti, 3, colN++), "Logout")) {
            JCE.JceNotiMessage (myUser, "logout"); // logout, alarmOn, alarmOff
        }
        if (GUI.Button (myGUI.DivideRect (rctNoti, 3, colN++), "Alm : On")) {
            JCE.JceNotiMessage (myUser, "alarmOn"); // logout, alarmOn, alarmOff
        }
        if (GUI.Button (myGUI.DivideRect (rctNoti, 3, colN++), "Alm : Off")) {
            JCE.JceNotiMessage (myUser, "alarmOff"); // logout, alarmOn, alarmOff
        }

       


        Rect joy1 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (joy1, colEA, colN++), "JCE")) {  // 
            //JoyLogin jObj = new JoyLogin ();

            JCE.JceUrgentNoticePT (myUser);

        }
        if (GUI.Button (myGUI.DivideRect (joy1, colEA, colN++), "Img")) {  // 
            JCE.JceImageNotice (myUser);
        }
        if (GUI.Button (myGUI.DivideRect (joy1, colEA, colN++), "Text")) {  // 
            //JCE.JceEventBanner (myUser);
            JCE.JceTextNoticePT (myUser);
        }
        if (GUI.Button (myGUI.DivideRect (joy1, colEA, colN++), "Event")) {  // 
            //JCE.JceEventBanner (myUser);
            JCE.JceEventBanner (myUser);
        }
//        if (GUI.Button (myGUI.DivideRect (joy1, colEA, colN++), "Test")) {  // 
//            int wid1, wid2;
//            WasCard was = myUser.arrCard [2].WAS;
//            myUser.arrCard [2].GetSkillWidth (false, out wid1, out wid2);
//            ("   Wid1, wid2  ::   " + was.condition + "  " + was.skill [0] + "  / " + was.skill [1] + "  >>> " + wid1 + " / " + wid2).HtLog ();
//            myUser.arrCard [2].GetSkillWidth (true, out wid1, out wid2);
//            ("  drink on   Wid1, wid2  ::   " + wid1 + "   " + wid2).HtLog ();
//
//        }


        //  _////////////////////////////////////////////////_    _____  Code Only  _____  Packet Send  _____
        Rect joy2 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        packetNum = int.Parse (GUI.TextField (myGUI.DivideRect (joy2, colEA, colN++), packetNum.ToString (), 5));
        if (GUI.Button (myGUI.DivideRect (joy2, colEA, colN++), "Send")) {  // 
            WasCodeOnlyProtocol aObj = new WasCodeOnlyProtocol () { User = Ag.mySelf, protoCode = packetNum };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (joy2, colEA, colN++), "TEST")) {  // 
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser,
                itemType = "HEARTUPGRADE",
                itemTypeId = "HeartSpeedUp", //"HeartLimitUp",
                ea = 1,
            };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
//            WasHeartFillMax aObj = new WasHeartFillMax () { User = myUser };
//            aObj.messageAction = (int pInt) => {
//                aObj = null;
//            };
        }

        Rect corot = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (corot, colEA, colN++), "Corout")) {  // 
            StartCoroutine ("SomeCrtine");
        }
        if (GUI.Button (myGUI.DivideRect (corot, colEA, colN++), "End")) {  // 
            StopCoroutine ("SomeCrtine");
        }

        #if UNITY_IPHONE
        //  _////////////////////////////////////////////////_    _____  Purchase  _____    IAP   _____
        string iapMsg = "N : " + AgStt.mIAP.arrProduct.Count + "  Psble : " + AgStt.mIAP.CanMakePayment ();
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), iapMsg);

        Rect iap1 = myGUI.GetRect (muiCol, muiRow++);
        int colNum = 3;
        int col = 0;

        if (GUI.Button (myGUI.DivideRect (iap1, colNum, col++), "IAP:Init")) {  // 
            AgStt.mIAP.ProductRequest ();
        }

        if (GUI.Button (myGUI.DivideRect (iap1, colNum, col++), "cash0030")) {  // 
            AgStt.mIAP.PurchaseProduct ("com.appsgraphy.psykickbattlekakao.cash0030");
        }
        #endif
        //  _////////////////////////////////////////////////_    _____  Code  _____  Etc Test  _____
        Rect joy9 = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;

        if (GUI.Button (myGUI.DivideRect (joy9, colEA, colN++), " SetNow ")) {
            testDT = DateTime.Now;
            Ag.LogIntenseWord ("    Now is " + testDT + "   Ag. Now () :: " + Ag.Now());
            Ag.LogIntenseWord ("    Diff is " + (testDT - Ag.Now ()).TotalSeconds);
        }

        if (GUI.Button (myGUI.DivideRect (joy9, colEA, colN++), " Time "))
            Ag.LogIntenseWord ("  Total Seconds  ... " + Ag.TotalSecondsHavePassedInSeoulSince (testDT));
        

    }

    DateTime testDT;

    IEnumerator SomeCrtine ()
    {
        //while (true) {
        yield return new WaitForSeconds (1);
        Ag.LogIntenseWord ("   Elapsed ....    1");
        yield return new WaitForSeconds (3);
        Ag.LogIntenseWord ("   Elapsed ....    3");
        yield return new WaitForSeconds (2);
        Ag.LogIntenseWord ("   Elapsed ....    2");
        //}
    }
}