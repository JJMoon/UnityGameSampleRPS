// [2013:11:19:MOON<Start>]
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  AmNetUnitJob  _____  Class  _____
public class AmNetUnitJob
{
    public AmNetUnitJob NextJob;
    public object Content;
    public Action SendJob, CtchJob, AfterJob;
    public Dlgt_V_Bool dlgtJobCompleted, dlgt_WillSend;
    // how I know it is completed
    public bool sendOnly = false;
    public string jobName;
    public int RetryCnt;
    public bool? wasSent;

    public bool IsReady {
        get {
            if (!wasSent.HasValue)
                return true;
            return false;
        }
    }

    public AmNetUnitJob ()
    {
        SendJob += () => {
            Ag.LogIntense (2, true);
            Ag.LogString (" < " + jobName + " > AmNetUnitJob process is started _____  SendJob   ! ! !   _____ ");
            wasSent = true;
            RetryCnt++;
        };
        CtchJob += () => {
            wasSent = false;
        };

        AfterJob += () => {
            //Ag.LogString (" < " + jobName + " > AmNetUnitJob process is completely _____  F I N I S H E D ! ! !   _____ ");
            //Ag.LogIntense (2, false);
        };
    }

    public void BeforeRetryAction ()
    {
        wasSent = null;
    }
}

public enum WasMsg
{
    UserID_Invalid,
    Success,
    Duplicate,
    NotAllowableName,
    BlockUser,
    Re_Regist,
    KKO_ID_Duplicate,
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Network  _____  Static Class  _____
public class StNet
{
    public static string encodedPassword, key;
    public static WasMsg MsgTeamNameChk, MsgRegist;

    public static bool GuestUserType0Tried = false;

    // 모델 데이터 외의 단순한 결과는 여기 변수로 저장
    //  _////////////////////////////////////////////////_    _____   Login   _____   Static   _____
    //  _////////////////////////////////////////////////_    _____   Util Method   _____   Static   _____
    public static void AddWASJob2Thread (WasObject wasObj)
    {
        AmNetUnitJob uObj = new AmNetUnitJob () { Content = wasObj, jobName = wasObj.GetType ().ToString () };
        uObj.SendJob += wasObj.SendAction;
        uObj.dlgtJobCompleted += wasObj.JobCompleted;
        uObj.CtchJob += wasObj.CatchAction;

        AgStt.NetManager.AddAUnitJob (uObj);
    }

    public static string GetEncodedPassword (string input)
    {
        byte[] input_byte = Encoding.UTF8.GetBytes (input);

        SHA1 sha = new SHA1CryptoServiceProvider ();
        byte[] sha1_result = sha.ComputeHash (input_byte);

        return BitConverter.ToString (sha1_result).Replace ("-", "").ToLower ();
    }

    public static void GuestLogin (AmUser user)
    {
        StNet.GuestUserType0Tried = false;
        WasLogin aObj = new WasLogin () { User = user, osVer = "1.1" };
        aObj.messageAction = (int pInt) => {
            Ag.LogIntenseWord (" Result :: " + pInt);
            if (pInt == 0) { // OK..
                InitialWASJobs (user);
            }
            if (pInt == -1) { 
                GuestUserType1Login (user);
            }
        };
    }

    public static void GuestUserType1Login (AmUser user)
    {
        StNet.GuestUserType0Tried = true;
        WasLogin aObj = new WasLogin () { User = user, osVer = "1.1" };
        aObj.messageAction = (int bInt) => {
            Ag.LogIntenseWord (" Result :: " + bInt);
            if (bInt == 0) {
                InitialWASJobs (user);
            } else 
                Ag.LogIntenseWord (" Login Error   Guest Case    .... Critical  ");
        };
    }

    public static void InitialWASJobs (AmUser user)
    {
        WasUserInfo bObj = new WasUserInfo () { User = user, flag = 1 };
        bObj.messageAction = (int pInt2) => {
            WasItemPrice cObj = new WasItemPrice () { User = user, DiscountOnly = false };
            cObj.messageAction = (int pInt3) => {
            };
        };
    }


    //  _////////////////////////////////////////////////_    _____   카드 전체를 업데이트 할 때    _____   Update   _____
    public static void UpdateAllCards (AmUser usr)
    {
        int tNum = usr.arrCard.Count;
        List<AmCard> upCards = usr.GetUpdateCards ();
        int cardCounts = upCards.Count;
        //for (int kk = 0; kk < tNum; kk++) {
//        while (cardCounts > 0) {
//
//                WasCardUpdate aObj = new WasCardUpdate () { User = usr, arrSendCard = cardArr };
//                aObj.messageAction = (int pInt) => {
//                    switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
//                    case 0:
//                        Ag.LogString (" result : Success ");
//                        return;
//                    }
//                };
//                cardArr = new List<AmCard> ();
//            }
//        }
    }
}