using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class AgUIBase : AmObject
{
    public bool isUIActive = true, isDefault = true;
    protected Timer sysTimer;
    protected GameObject DisableGObj, DefaultGObj, AlterGObj;
    // use Default or Alter ...  not both..
    protected GameObject ActiveUIObj { get { return isDefault ? DefaultGObj : AlterGObj; } }

    protected GameObject NonActUIObj { get { return isDefault ? AlterGObj : DefaultGObj; } }

    public bool IsDiabled {
        get {
            //Ag.LogString (" IsDiabled ? " + isUIActive.ShowBool ("  ", "Active", "Diabled"));
            return !isUIActive;
        }
    }

    public void MakeAbility (bool toActive)
    {
        isUIActive = toActive;
    }

    public void SetObjects (GameObject defaultObj, GameObject disableObj)
    {
        DisableGObj = disableObj;
        DefaultGObj = defaultObj;
    }

    public void SetObjectsWithAlt (GameObject defaultObj, GameObject disableObj, GameObject altObj)
    {
        SetObjects (defaultObj, disableObj);
        AlterGObj = altObj;
    }

    public void SetDefaultAsActive (bool nonReverse)
    {
        isDefault = nonReverse; // ActiveUIObj = DefaultGObj  >> no Reverse  ....  false : reverse case ...
    }

    void SetGObject (bool disable)
    {
        if (DisableGObj != null)
            DisableGObj.SetActive (disable);
        if (ActiveUIObj != null)
            ActiveUIObj.SetActive (!disable);
        if (NonActUIObj != null)
            NonActUIObj.SetActive (disable);
    }

    public void StartDisablingFor (float pSeconds)
    {
        isUIActive = false; // Logic ...
        SetGObject (true);
        if (sysTimer != null) {
            sysTimer.Stop ();
            sysTimer = null;
            //"  Timer  Reset !!! ".HtLog ();
        }
        sysTimer = new Timer ();
        sysTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
            Ag.LogString ("   End of Disabled UI object   ... >>>  set Enable ..       >>>>>    ");
            isUIActive = true; // Logic ...

            SetGObject (false);

            sysTimer.Enabled = false;
            sysTimer = null;
        };
        sysTimer.Interval = pSeconds * 1000; // milli second ..
        sysTimer.Enabled = true;

    }
}

public class AgUIButton : AgUIBase
{
    public void SetVisible (bool deflt, bool alt)
    {
        if (AlterGObj != null)
            AlterGObj.SetActive (alt);
        if (DefaultGObj != null)
            DefaultGObj.SetActive (deflt);
    }
}

public class AgGameRelated
{
    bool myslfLeft, enemyLeft, gameMatched, isRandom, exchangeParsed, inGameSession, invitedFromFriend = false, invitingFriend = false;

    public bool NullCheck { get { return enemyLeft; } }

    public bool AmInvitingFriend { get { return invitingFriend; } }

    public bool SomeoneOutPopupEnemyLeft { get { return enemyLeft; } }

    public bool IsGameMatched { get { return gameMatched; } }

    public bool ExchangeParsedForGominjung { get { return exchangeParsed; } }

    public bool WillSendWasGameReport { get { return gameMatched && inGameSession; } }

    public bool WillLoadTitleScene {
        get {
            Ag.LogIntenseWord ("  WillLoadTitleScene  ::  gameMatched : " + gameMatched);
            return gameMatched || invitedFromFriend;
        }
    }

    string mark = "  <<<<< $$$$$$ _____  AgGameRelated  _____  :: _  ";

    public void StartGamePacket (string pComent = ">>>")
    {
        Ag.LogString (mark + "StartGamePacket  >>>    " + pComent);
        inGameSession = true;
    }
    //    public void TitleSceneBegan (string pComent = ">>>")
    //    {
    //        Ag.LogString (mark + "TitleSceneBegan  >>>    " + pComent);
    //    }
    public void GoingOutFromMatching (string pComent = " All set false ")
    {
        Ag.LogString (mark + "GoingOutFromMatching  >>>    " + pComent);
        invitingFriend = invitedFromFriend = inGameSession = exchangeParsed = gameMatched = enemyLeft = false;
    }

    public void NodeInviteOrRandomAction (string pComent = ">> Parameter is not only used for logging <<")
    {
        Ag.LogString (mark + "NodeInviteOrRandomAction  >>>    " + pComent);

        if (pComent == "ActionInvite")
            invitingFriend = true;

        inGameSession = exchangeParsed = gameMatched = enemyLeft = false;
    }

    public void EnemyOrMyselfLeftActionFromNode (bool myself, string pComent = ">>>")
    {
        Ag.LogString (mark + "EnemyOrMyselfLeftActionFromNode  >>>    " + myself.ShowBool (" Case ? ", "Myself", "Enemy") + pComent);
        if (myself)
            myslfLeft = true;
        else
            enemyLeft = true;
        invitingFriend = invitedFromFriend = exchangeParsed = false;
    }

    public void GameRetryOverNetworkFailure (string pComent = ">>>")
    {
        Ag.LogString (mark + "GameRetryOverNetworkFailure  >>>    " + pComent);
        Ag.NetExcpt.ConnectLossAct ();
    }

    public void GameMatchedWithBot (string pComent = ">>>")
    {
        Ag.LogString (mark + "GameMatchedWithBot  >>>    " + pComent);
        Ag.NodeObj.IsRandom = gameMatched = isRandom = true;
        exchangeParsed = false;
    }

    public void GameMatchedAllCase (bool pRandom, string pComent = ">>>")
    {
        //   2> ResRandom   3> Res Join or Matched
        Ag.LogString (mark + "GameMatchedAllCase  >>>    " + pRandom.ShowBool ("It's", "Random", "Friend") + pComent);
        gameMatched = true;
        Ag.NodeObj.IsRandom = isRandom = pRandom;
    }

    public void EnemyInfoExchangeParsed (string pComent = ">>>")
    {
        Ag.LogString (mark + "EnemyInfoParsed  >>>    " + pComent);
        exchangeParsed = true;
    }

    public void FocusBack (string pComent = ">>>")
    {
        Ag.LogString (mark + "FocusBack  >>>    " + pComent);
        invitedFromFriend = gameMatched = exchangeParsed = false;
    }

    public void InvitedFromFriend (string pComent = ">>>")
    {
        Ag.LogString (mark + "InvitedFromFriend  >>>    " + pComent);
        invitingFriend = invitedFromFriend = true;
    }
}