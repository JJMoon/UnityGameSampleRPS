using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class GitIgnoreThis
{
    public static void SetKKOID4Test ()
    {
        //Ag.mySelf.WAS.KkoID = KakaoLocalUser.Instance.userId;
        //Ag.mySelf.WAS.KkoID = "88454335690450033";
        Ag.mySelf.WAS.KkoID = "88712330645978192";
        //Ag.mySelf.WAS.KkoID = "testID113";
        //Ag.mySelf.WAS.KkoID = "88299368562514961";
        //Ag.mySelf.WAS.KkoID = "88508134386197025";
        //Ag.mySelf.WAS.KkoNick = "TomCurosie";
        // 90060594732486160 <KimDR> 88712330645978192 <Moon>  88214690633939121<Rolco>
        //Ag.mySelf.WAS.KkoID = KakaoLocalUser.Instance.userId;
    }


    public static void GitIgnoreSetup()
    {
        Ag.mDisableLog = false; // 나중에 true 로 할 것.
        AgStt.DebugOnDevice = false; // 로그를 디바이스로 찍을 때.
      
        if (Application.platform == RuntimePlatform.OSXEditor)
            AgStt.DebugOnDevice = false;

        if (!AgStt.DebugOnDevice)
            return;
        AgStt.arrLogOnDevice = new List<string> ();
        AgStt.arrLogWichtig = new List<string> ();
        for (int k = 0; k < AgStt.LinesInDebugButton * 8; k++) {
            AgStt.arrLogOnDevice.Add (" Default");
        }
        for (int k = 0; k < AgStt.LinesInDebugButton * 2; k++) {
            AgStt.arrLogWichtig.Add (" Wichtig \n");
        }
    }

    public static void GitIgnoreTutorial ()
    {
        AgStt.mgGameTutorial = false; // false :: No Tutorial
    }
}