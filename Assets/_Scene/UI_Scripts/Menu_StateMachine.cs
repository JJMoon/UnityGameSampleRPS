//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


// [2012:11:14:MOON] Menu Fast Loading
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System;

public partial class MenuManager : AmSceneBase
{
    bool SomebodyOutException ()
    {
        if (Ag.GameStt.SomeoneOutPopupEnemyLeft) {
            Ag.LogDouble ("   PopUp  ::  alert_someoneout  >> @   Advant  >> AmHost has NOT value  >>>>>   ");
            MenuCommonOpen ("alert_someoneout", "Ui_popup", true);
            arrState.SetStateWithNameOf ("Init");
            return true;
        }
        return false;
    }

    void StateMachineSetup ()
    {
        //  _////////////////////////////////////////////////_    _____  State  _____   Init   _____
        arrState.AddAMember ("Init", 0);
        arrState.AddEntryAction (() => {
            FriendListUpdate = false;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   MatchApply   _____
//        arrState.AddAMember ("MatchApply", 0); // [2012:10:10:MOON] Heart Beat
//        arrState.AddEntryAction (() => {
//            arrState.SetStateWithNameOf ("GoMinJung");
//        });
//        arrState.AddTimeOutProcess (5f, () => { 
//            arrState.SetStateWithNameOf ("NetOffInMatch"); 
//            //Ag.mFBOrder = "ThreadReboot";  // [2012:10:29:MOON] Tuning...
//        });
//        //  _////////////////////////////////////////////////_    _____  State  _____   MatchApplyPacket   _____
//        arrState.AddAMember ("MatchApplyPacket", 0);  // [2012:10:10:MOON] Heart Beat
//        arrState.AddTimeOutProcess (10f, () => {
//        });
        //  _////////////////////////////////////////////////_    _____  State  _____   GoMinJung   _____
        arrState.AddAMember ("GoMinJung", 0);
        arrState.AddEntryAction (() => {
            Ag.LogDouble ("  Entry of GoMinJung :::  " + Ag.mSingleMode.ShowBool (" Single ", "Yes", "No"));
            StateGominjungUI ();
            FriendListUpdate = false; 
            ExeBl_DidNotMatchStarted = true;
            SoundManager.Instance.GameSoundPlayer ();
        });
        arrState.AddDuringAction (() => {
            try {
                if (Ag.NodeObj.GameStartMsgSent.Enem)
                    dicMenuList ["ready_someone"].SetActive (true);
            } catch {
                Ag.LogDouble ("   PopUp  ::  alert_someoneout  >> @   GoMinJung  >>>>>   ");
                MenuCommonOpen ("alert_someoneout", "Ui_popup", true);
            }

            if (Ag.NodeObj == null) // Pause Out case ...  go out ....
                return;

            if (SomebodyOutException ())
                return;

            //(Ag.NodeObj.EnemyUser != null && Ag.NodeObj.EnemyUser.Parsed)) {
            if (Ag.mSingleMode ? (Ag.NodeObj.EnemyUser != null) : (Ag.NodeObj.EnemyUser != null && Ag.GameStt.ExchangeParsedForGominjung)) {

                if (ExeBl_DidNotMatchStarted) {
                    dicMenuList ["btn_exit"].SetActive (false);
                    int Rannum = AgUtil.RandomInclude (0, 1);
                    if (Rannum == 0)
                        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/MatchWaiting01");
                    else
                        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/MatchWaiting02");

                    Ag.LogDouble ("   GoMinJung >>>>>    call EnemyJoin    " + Ag.mSingleMode.ShowBool ("Single :", "Yes", "No"));

                    EnemyOrBotUniformLabelSetting ();
                    //AgStt.IsGaming = mGameMatchOk = true;
                    //AgStt.IsGaming  = true;
                    //MatchingFlag = false;
                    dicMenuList ["Panel_matching"].SetActive (false);

                    dicMenuList ["btn_start"].SetActive (true);
                }
            }
        });

        arrState.AddExitCondition (() => {
            if (Ag.NodeObj == null)  // Pause Out case ...  go out ....  do not exit .. hold ....
                return false;
            return Ag.NodeObj.ReceiveGameStartMsgBoth;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   Coin   _____
        arrState.AddAMember ("Coin", 0);  // Matching Finished... Animation Play and Go to Game Scene.
        arrState.AddEntryAction (() => {
            Ag.NodeObj.UserModify ("MATCHING", statusOnly: true);
            MessageInfo ();
            MenuCommonOpen ("rematch_not", "Ui_popup", false);
            dicMenuList ["ready_me"].SetActive (true);
            dicMenuList ["ready_someone"].SetActive (true);
            dicMenuList ["Panel_count"].SetActive (false);
            //if (Ag.mySelf.FreeCouponLimitDT < System.DateTime.Now) {
            if (!Ag.mySelf.IsFreeCouponRemain) {
                Ag.LogIntenseWord ("    Use Heart   ....   Set Cool time  ");
                UseHeart ();
            }
            dicPlayerOrObj ["Refree"].animation.Play ("ThrowCoin");
            dicPlayerOrObj ["Coin"].animation.Play ("CoinAni3");
            dicMenuList ["btn_start"].SetActive (false);
            //dicMenuList ["loading"].SetActive (false);
            dicMenuList ["btn_exit"].SetActive (false);
            dicMenuList ["Panel_provokebox"].SetActive (true);
            dicMenuList ["Ball"].SetActive (false);
            mKicker.SetActive (false);
            GetTeamItemTypeName ();
            if (Ag.mBlueItemFlag || Ag.mRedItemFlag || Ag.mGreenItemFlag)
                BuyItem ();

        }); 
        arrState.AddExitCondition (() => {
            return !dicPlayerOrObj ["Refree"].animation.isPlaying;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   SSKey   _____
        arrState.AddAMember ("SSKey", 1f);
        arrState.AddEntryAction (() => {
            if (SomebodyOutException ())
                return;
            //if (!Ag.NodeObj.AmHost.HasValue) {  // Pause  ...   I went out  ....  
//            if (Ag.GameStt.SomeoneOutPopupEnemyLeft) {
//                Ag.LogDouble ("   PopUp  ::  alert_someoneout  >> @   SSKey  >>>>>   ");
//                MenuCommonOpen ("alert_someoneout", "Ui_popup", true);
//                return;
//            }
            if (Ag.mSingleMode) {
                Ag.LogDouble ("  Menu  KickOff    Single Mode  ...   ::  >>>>     Bot  ");
                Ag.NodeObj.EnemyUser.WAS.KkoID = "BOT";
                Ag.mgIsKick = true;
            } else {
                Ag.LogDouble ("  Menu  KickOff     Not Single Mode  ...   ::  >>>>  Host ? " + Ag.NodeObj.AmHost.Value);
                Ag.mgIsKick = Ag.NodeObj.AmHost.Value;
            }
            if (Ag.mgIsKick && !AgStt.mgGameTutorial)
                SendWasGamestart (Ag.mySelf, Ag.NodeObj.EnemyUser);
        });
        arrState.AddDuringAction (() => {
            if (SomebodyOutException ())
                return;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   Advant   _____
        arrState.AddAMember ("Advant", 3f);
        arrState.AddEntryAction (() => {
            EnemyLeftflag = false;
            int Rannum = AgUtil.RandomInclude (0, 1);
            if (Rannum == 0)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/CoinToss01");
            else
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/CoinToss02");

            //if (!Ag.NodeObj.AmHost.HasValue) {  // Pause  ...   I went out  ....  
//            if (Ag.GameStt.SomeoneOutPopupEnemyLeft) {
//                Ag.LogDouble ("   PopUp  ::  alert_someoneout  >> @   Advant  >> AmHost has NOT value  >>>>>   ");
//                MenuCommonOpen ("alert_someoneout", "Ui_popup", true);
//                return;
//            }
            if (SomebodyOutException ())
                return;

            Ag.LogString (" Menu_KickOff  >>  SingleMode " + Ag.mSingleMode + "   Node.AmHost " + Ag.NodeObj.AmHost.Value);

            if (Ag.mSingleMode) {
                Ag.LogDouble ("  Menu  KickOff    Single Mode  ...   ::  >>>>     Bot  ");
                Ag.NodeObj.EnemyUser.WAS.KkoID = "BOT";
                Ag.mgIsKick = true;
                Ag.NodeObj.PrepareGameBot ();
            } else {
                Ag.LogDouble ("  Menu  KickOff     Not Single Mode  ...   ::  >>>>  Host ? " + Ag.NodeObj.AmHost.Value);
                Ag.mgIsKick = Ag.NodeObj.AmHost.Value;
                Ag.NodeObj.PrepareGame ();
            }

            Ag.LogString (" Menu_KickOff  >>  2  SingleMode " + Ag.mSingleMode + "   Node.AmHost " + Ag.NodeObj.AmHost.Value);

            dicMenuList ["Panel_firstaction"].SetActive (true);
            dicMenuList ["Panel_firstaction1"].SetActive (true);
            dicMenuList ["Panel_provokebox"].SetActive (false);
            dicMenuList ["ready_me"].SetActive (false);
            dicMenuList ["ready_someone"].SetActive (false);
            dicMenuList ["data_someone"].SetActive (false);
            dicMenuList ["data_user"].SetActive (false);

            dicMenuList ["img_attack"].SetActive (Ag.mgIsKick);
            dicMenuList ["img_attack1"].SetActive (!Ag.mgIsKick);
            dicMenuList ["img_defense"].SetActive (!Ag.mgIsKick);
            dicMenuList ["img_defense1"].SetActive (Ag.mgIsKick);
            if (Ag.mgIsKick) {
                dicPlayerOrObj ["Refree"].animation.Play ("PlayerWin");
            } else {
                dicPlayerOrObj ["Refree"].animation.Play ("EnemyWin");
            }
        });
        arrState.AddDuringAction (() => {
            if (SomebodyOutException ()) {
                Ag.LogDouble (" arrState.SetStateWithNameOf   Init ....   SomebodyOutException ... ");
                //arrState.SetStateWithNameOf ("Init");
                //Btn_Fun_MatchCancleAndGoOut ();
                return;
            }
        });
        arrState.AddExitCondition (() => {
            return !dicPlayerOrObj ["Refree"].animation.isPlaying && !EnemyLeftflag;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   SceneLoad   _____
        arrState.AddAMember ("SceneLoad", 1);
        arrState.AddEntryAction (() => { 
            //Ag.mySelf.ContWinStarted = Ag.mySelf.ContWinCoolTimeRemainPercent () > 0;
            dicMenuList ["Panel_count"].SetActive (true);
            dicMenuList ["Panel_countKickoff"].SetActive (true);
            MyDataLoad = false;
            SoundManager.Instance.Play_Effect_SoundStop ();
            dicMenuList ["CenterCircle"].SetActive (true);
            Ag.NodeObj.GameStartMsgSent.Mine = Ag.NodeObj.GameStartMsgSent.Enem = false;
        });
        arrState.AddExitAction (() => {
            if (!Ag.NetExcpt.ConnectLossSignalGone) {
                Application.LoadLevel ("GameScenePreload");
            }
        });

        arrState.SetSerialExitMember ();
        arrState.SetStateWithNameOf ("Init");  // Set default ..
        //arrState.AddAMember ("NetOffInMatch", 0);
        //arrState.AddAMember ("ToTheMatch", 0);
//        arrState.AddExitCondition (() => {
//            return true; // Ag.mFBState == "Ready";
//        });
        //arrState.SetNextStateOf ("NetOffInMatch", "ToTheMatch");
        //arrState.SetNextStateOf ("ToTheMatch", "Init");
    }
}