using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;

public class MoonUnitTest : AmSceneBase
{
    GameObject Stadium;

    public void Awake ()
    {

    }

    public override void Start ()
    {
        Ag.LogIntenseWord (" UnitTest.cs :: Start ");
        myGUI = new AmUI ();
        myGUI.SetColumns (2, 20);
        mTimeLooseAtStartPoint = 0.5f;
        base.Start ();

        //GameObject gObj = AgStt.Dic ["some"] ["anoth"];

        // Test ID Setting ....    WAS, Node     related ......    etc...

//        Stadium = FindGameObject ("prefab_Polygon/Stadium/Stadium2", true);
//        Stadium = FindGameObject ("Stadium2", true);
//        GetPrefabAt ("prefab_Polygon/Stadium", "Stadium2");// Resources.Load<GameObject> ( "prefab_Polygon/Stadium/Stadium2");
//        StartCoroutine (CrtnMethod ());


        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____   Test Started   _____");

        MtCompact compObj = new MtCompact (24);

        compObj.AddNum (6); // total num ... including this ..
        compObj.AddNum (12);
        compObj.AddNum (9);
        compObj.AddNum (23);
        compObj.AddNum (0);
        compObj.AddNum (12);

        compObj.ParseSelf ();

        Ag.LogIntense (5, false);
    }

    IEnumerator CrtnMethod ()
    {
        Ag.LogString ("  IEnumerator CrtnMethod()  ::  Before return  ");
        Stadium.SetActive (false);
        yield return new WaitForSeconds (3);
        Stadium.SetActive (true);
        Ag.LogString ("  IEnumerator CrtnMethod()  ::  1  ");
        yield return new WaitForSeconds (3);
        Stadium.SetActive (false);
        Ag.LogString ("  IEnumerator CrtnMethod()  ::  2  ");
        yield return new WaitForSeconds (3);
        Ag.LogString ("  IEnumerator CrtnMethod()  ::  3  ");
    }

    void StopTheCoroutine ()
    {
        StopCoroutine ("CrtnMethod");
    }

    public override void BaseStartSetting ()
    {
        base.BaseStartSetting ();
    }

    public override void OnApplicationQuit ()
    {
    }

    public override void Update ()
    {

    }

    string strT = "Text";
    int colN = 0, colEA;
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  ###  _____  OnGUI  _____
    public override void OnGUI ()
    {
        muiCol = 0;
        muiRow = 0;

        //GUI.Label (myGUI.GetRect (muiCol, muiRow++), myUser.WAS.KkoID + " C/C/G : " + myUser.mCash1 + " _ " + myUser.mCash2 + " _ " + myUser.mGold);
        colEA = 3;
        colN = 0;
        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Regist   _____
        Rect curRegis = myGUI.GetRect (muiCol, muiRow++);
        strT = GUI.TextField (myGUI.DivideRect (curRegis, 2, 0), strT);

        if (GUI.Button (myGUI.DivideRect (curRegis, colN, colEA), "Test")) {


        }

//        if (GUI.Button (myGUI.DivideRect (curRegis, 6, 4), "Reg")) {
//            WasRegist aObj = new WasRegist () { User = myUser };
//            //AgStt.GoToLoginAfterRegist = false;
//            aObj.messageAction = (int pInt) => {
//            };
//        } 
//
//        if (GUI.Button (myGUI.DivideRect (curRegis, 6, 5), "UnRgst")) {
//            WasUnRegist aObj = new WasUnRegist () { User = myUser };
//            //AgStt.GoToLoginAfterRegist = false;
//            aObj.messageAction = (int pInt) => {
//            };
//        } 

    }
}
