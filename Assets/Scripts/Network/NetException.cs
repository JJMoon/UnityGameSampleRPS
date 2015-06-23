using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  NetException  _____  Class  _____
public class NetException
{
    //public string GamingEnemyID;
    //public int GamingEnemyPoint;

    public Action ConnectLossAct, NodeConnectionLossAct;
    public bool ConnectLossSignalGone = false, WasLoginDuplicate = false;

    public DateTime xxWASActionTime = DateTime.Now;

    bool? WAS, Platform;
    Timer NodeTimer;
    //public bool IsOK { get { return WAS && Node; } }
    public NetException ()
    {
        ConnectLossAct += () => { // WAS Error  or    Time out ...  Lte <--> Wifi 
            Ag.LogIntenseWord (" Connection Loss Event ");
            // Gaming ...  
            if (Ag.CurrentScene == "GAME")  // ignore ? send leave..  timeout...   //return;
                Ag.NodeObj.LeaveMyself ();
            ConnectLossSignalGone = true;  // popup open..
        }; // mUIpopup.SetActive (true);  // NetworkPopup.cs 에서 처리함.

        NodeConnectionLossAct = () => {
            Ag.LogIntenseWord (" Connection Loss Event   @  Node ");

            AgStt.NodeClose ();

            // Gaming ...  
            if (Ag.CurrentScene == "GAME") {
                Ag.LogDouble (" Node Connection Loss Evnt  >>> In GAME <<     .....  _____ ");
                ConnectLossSignalGone = true;
                return;
            }
            // Auto Restart Node ....
            AgStt.NodeOpen ();
            Ag.LogDouble (" NetException :: Node   ....   End    .....  _____ ");
        };
    }

    public void Recover ()
    {
        ConnectLossSignalGone = false;
        if (WAS.HasValue && WAS.Value)
            WAS = null;
        if (Platform.HasValue && Platform.Value)
            Platform = null;
        //if (Node.HasValue && Node.Value) {
        AgStt.NodeClose ();
        AgStt.NodeOpen ();

        WasLogin aObj = new WasLogin () { User = Ag.mySelf, osVer = "1.1" };
        aObj.messageAction = (int pInt) => {
            aObj = null;
        };
    }
}
