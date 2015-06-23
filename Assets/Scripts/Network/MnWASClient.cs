using System;
using UnityEngine;
using System.Collections;

public class MnWASClient : AmSceneBase
{
    // Use this for initialization
    public override void Start ()
    {

        myGUI.SetColumns (4, 13);

        base.Start ();

        //BaseStartSetting ();
        //SocketIOprocess ();

    }

    public override void OnApplicationQuit ()
    {
        Ag.LogIntenseWord ("  OnApplicationQuit()    Mn Node Client    ....   ");
    }

    public override void BaseStartSetting ()
    {

    }
    // Update is called once per frame
    public override void Update ()
    {

    }

    WasTeamCheck teamChk = new WasTeamCheck();

    public override void OnGUI ()
    {
        muiCol = 0;
        muiRow = 1;

        if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), " < TeamCheck > ")) {


              
        } 

        Rect curR = myGUI.GetRect (muiCol, muiRow++);
        //node1.Direction = int.Parse (GUI.TextField (myGUI.DivideRect(curR, 2, 0), node1.Direction.ToString(), 5));
        //node1.Skill     = int.Parse (GUI.TextField (myGUI.DivideRect(curR, 2, 1), node1.Skill.ToString(), 5));



        muiCol = 1;
        muiRow = 1;

        
    }
}
