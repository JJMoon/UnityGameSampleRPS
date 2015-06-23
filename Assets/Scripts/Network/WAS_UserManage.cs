// [2013:11:25:MOON<Start>]
using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

//using LitJson;
using SimpleJSON;

public class WasUserInfo : WasObject
{
    public int flag;
    public string ID;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Ag.LogString ("WasUserInfo :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 101);
        SendStr = SendStr.AddKeyValue ("formatVersion", 2); // Free coupon time. ..
        SendStr = SendStr.AddKeyValue ("flag", flag, false);
        SendStr = SendStr.AddParen ();

        postAction = () => {
            User.ParseUserInfoOK (NdObj);
            messageAction (Result.result);
        };
        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }
    //    public override void CatchAction ()
    //    {
    //        Ag.LogString ("WasUserInfo :: CatchAction ...   ");
    //    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Regist  _____  Class  _____
public class WasServerVersion : WasObject
{
    //public bool IsAvailable;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Encript = false;
        //AgStt.mURI = "http://221.143.21.33/api.psy.versionCheck.trd";
        //AgStt.mURI = "http://psy.kakao.joycity.com/api.psy.versionCheck.do";  // dev
        AgStt.mURI = " http://psy-kakao.joycity.com/api.psy.versionCheck.do";
        Ag.LogString ("WasServerVersion :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddKeyValue ("serviceCode", 99);
        SendStr = SendStr.AddKeyValue ("formatVersion", 0);
        SendStr = SendStr.AddKeyValue ("buildVersion", AgStt.CliVersion);
        SendStr = SendStr.AddKeyValue ("osVersion", "1.0");
        SendStr = SendStr.AddKeyValue ("deviceUUID", User.DeviceID);
        SendStr = SendStr.AddKeyValue ("osType", 2);
        SendStr = SendStr.AddKeyValue ("deviceModel", SystemInfo.deviceModel, false);

        SendStr = SendStr.AddParen ();

        postAction = () => {
            if (Result.result == 0 || Result.result == 1 || Result.result == 2)
                AgStt.SvrVersion = NdObj ["svrVersion"].AsInt;
            else {
                messageAction (Result.result);
                Ag.LogString (Result.ToString () + "      ,   E r r o r   ");
            }

            if (AgStt.SvrVersion == AgStt.CliVersion) { // Service
                //AgStt.mURI = "http://221.143.21.33/api.psy.do"; // mServiceURI;
                //AgStt.mNodeURI = "http://221.143.21.32";
                AgStt.mURI = AgStt.mServiceURI;//"http://221.143.21.33/api.psy.do"; // mServiceURI;
                AgStt.mNodeURI = AgStt.mServiceNodeURI;//"http://221.143.21.32"; // mServiceNodeURI;
            }
            if (AgStt.SvrVersion > AgStt.CliVersion) { // Update case ... Server is faster 
                AgStt.mURI = AgStt.mServiceURI; // mServiceURI;
                AgStt.mNodeURI = AgStt.mServiceNodeURI; // mServiceNodeURI;
            }
            if (AgStt.SvrVersion < AgStt.CliVersion) { // Review    ..... Client is faster
                AgStt.mURI = AgStt.mServiceURI; // mServiceURI;
                AgStt.mNodeURI = AgStt.mServiceNodeURI; // mServiceNodeURI;
            }

            messageAction (Result.result);
            Ag.LogString (Result.ToString () + "      ,   Server Version : " + AgStt.SvrVersion + "    Client Version : " + AgStt.CliVersion);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasServerVersion :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Regist  _____  Class  _____
public class WasUnRegist : WasObject
{
    public bool OKtoGO;
    //public bool IsAvailable;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Ag.LogString ("WasUnRegist :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 115);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);
        SendStr = SendStr.AddParen ();


        //SendStr = SendStr.AddKeyValue ("encodedPassword", StNet.encodedPassword, false);

        postAction = () => {
            switch (Result.result) {  //0 : 성공, 1 : 탈퇴 회원 재가입, -1 : 카카오 ID 중복 2: 블럭 사용자
            case 0:
                OKtoGO = true;
                break;
            case -1:
                OKtoGO = false;
                break;
            }
            messageAction (Result.result);
            Ag.LogString (Result.ToString () + "      ,   OK to GO : " + OKtoGO);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value && OKtoGO;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasRegist :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Login  _____  Class  _____
public class WasLogin : WasObject
{
    public string osVer = "1.0";
    public int osType = 1;
    // 1 : windows, 2 : iOS, 3 : android, 0 : unknown
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        AgStt.IntendedPause = false;

        AgStt.ReLoginAction ();

        osType = 0;
        #if UNITY_IPHONE
        osType = 2;
        #endif
        #if UNITY_ANDROID
        osType = 3;
        #endif

        Ag.LogString ("WasLogin :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddKeyValue ("serviceCode", 100);

        SendStr = SendStr.AddKeyValue ("userID", Ag.mGuest ? "" : User.WAS.KkoID);

        string str4key = Ag.mGuest ? User.DeviceID : User.WAS.KkoID;


//        int formatVer = 3;
//
//        string theKey = StcPlatform.TheToken;  // Kakao token ..
//        int formatVer = 2;
//        string theKey = StNet.GetEncodedPassword (str4key);

//        Ag.LogIntenseWord ("  The Token ::  " + theKey);

        /*

        string theKey = StcPlatform.TheToken;  // Kakao token ..
        */

        //        string theKey = StNet.GetEncodedPassword (str4key);
//
//        Ag.LogIntenseWord ("  The Token ::  " + theKey);

        int formatVer = 3;
        #if UNITY_EDITOR
        formatVer = 2;
        string theKey = StNet.GetEncodedPassword (str4key);
        #else
        formatVer = 3;
        SendStr = SendStr.AddKeyValue ("accessToken", StcPlatform.TheToken);
        Ag.LogIntenseWord ("  The Token ::  " + StcPlatform.TheToken);
        #endif

        SendStr = SendStr.AddKeyValue ("encodedPassword", StNet.GetEncodedPassword (str4key));
        SendStr = SendStr.AddKeyValue ("formatVersion", formatVer);
        int userType = Ag.mGuest ? 0 : 1;
        //userType = Ag.mGuest && StNet.GuestUserType0Tried ? 1 : userType;

        SendStr = SendStr.AddKeyValue ("userType", userType);

        SendStr = SendStr.AddKeyValue ("osType", osType);
        SendStr = SendStr.AddKeyValue ("osVersion", osVer);
        SendStr = SendStr.AddKeyValue ("deviceModel", SystemInfo.deviceModel);
        SendStr = SendStr.AddKeyValue ("deviceUUID", User.DeviceID, false);
        SendStr = SendStr.AddParen ();

        postAction = () => {
            " post Action ".HtLog ();
            User.WAS.WasKey = NdObj ["key"];
            User.mServerNum = NdObj ["svrVersion"].AsInt;
            User.noticeImageUrl = NdObj ["noticeImageUrl"];

            // 
            if (NdObj ["result"].AsInt == -1)  // go to Regist ..
                ;

            // Server Version Check..
            int svrVersion = NdObj ["svrVersion"].AsInt;
//            if (AgStt.ClientVersion < svrVersion) // New client has come.. Go to App Store ..
//                AgStt.mURI = "http://221.143.21.33/api.psy.trd";
//            else if (svrVersion < AgStt.ClientVersion) // Review
//                AgStt.mURI = "http://221.143.21.33/api.psy.trd";
//            else // same version  Service ..
//                AgStt.mURI = "http://221.143.21.33/api.psy.trd";

            try {
                User.TimeEventEnd = NdObj ["timeEventEndDate"].ToString ().ToDateTime ();
            } catch {
                " No timeEventEndDate ....  ".HtLog ();
            }
            User.loginCount = NdObj ["loginCount"].AsInt;
            messageAction (Result.result);// 0 : 성공, -1 : 중복, -2 : 허용 불가 이름, 1: 존재하지 않는 사용자
            (User.mServerNum.LogWith ("svrVersion") + User.WAS.WasKey.LogWith ("key") + User.noticeImageUrl.LogWith ("notiURL")).HtLog ();
            (User.TimeEventEnd.ToString ().LogWith ("TimeEvent") + User.ServerVer.LogWith ("Svr Ver")).HtLog ();
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }
    //    public override void CatchAction ()
    //    {
    //        Ag.LogString ("WasRegist :: CatchAction ...   ");
    //    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Regist  _____  Class  _____
public class WasRegist : WasObject
{
    //public bool IsAvailable;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Ag.LogString (" Regist " + User.WAS.KkoID + "   " + User.WAS.TeamName);
        Ag.LogString ("WasRegist :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddKeyValue ("serviceCode", 110);
        SendStr = SendStr.AddKeyValue ("formatVersion", 3); // Deck  2);
        SendStr = SendStr.AddKeyValue ("teamName", User.WAS.TeamName);

        if (AgStt.FromGuest2Kakao)
            SendStr = SendStr.AddKeyValue ("userType", 2);
        else 
            SendStr = SendStr.AddKeyValue ("userType", Ag.mGuest ? 0 : 1);

        SendStr = SendStr.AddKeyValue ("userID", User.WAS.KkoID);
        SendStr = SendStr.AddKeyValue ("deviceUUID", User.DeviceID);
        SendStr = SendStr.AddKeyValue ("country", User.WAS.Country, false);
        SendStr = SendStr.AddParen ();

        postAction = () => {

            if (Result.result == 7) {
                AgStt.FromGuest2Kakao = false;
                Ag.mGuest = false;
            }

            messageAction (Result.result);  //0 : 성공, 1 : 탈퇴 회원 재가입, -1 : 카카오 ID 중복 2: 블럭 사용자


            PreviewLabs.PlayerPrefs.SetString ("RegistTimeStamp", UtTimestamp.ToTimestamp (DateTime.Now).ToString ());

            Ag.LogString (Result.ToString () + "      ,   Msg Regist : " + StNet.MsgRegist);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasRegist :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Team Check  _____  Class  _____
public class WasTeamCheck : WasObject
{
    public string ID, TgtName;
    //public bool IsAvailable;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        Ag.LogString ("WasTeamCheck :: SendAction ...   Started ...  ");
        //StNet.TeamNameAvailable = null;
        SendStr = "";
        SendStr = SendStr.AddKeyValue ("serviceCode", 111);
        SendStr = SendStr.AddKeyValue ("userID", ID);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("teamName", TgtName, false);
        SendStr = SendStr.AddParen ();

        postAction = () => {
            messageAction (Result.result);// 0 : 성공, -1 : 중복, -2 : 허용 불가 이름, 1: 존재하지 않는 사용자

            Ag.LogString (Result.ToString () + "      ,  TeamNameChk : " + Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasTeamCheck :: CatchAction ...   ");
        IsOK = false;
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  WasPurchaseGold  _____  Class  _____
public class WasFriendRank : WasObject
{
    public List<string> arrFriendIDs = new List<string> ();

    public override void SendAction ()
    {
        Ag.LogString ("WasFriendRank :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 150);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        string frdArrStr = "";
        for (int j = 0; j < arrFriendIDs.Count; j++) {
            string frdStr = "";
            frdStr = frdStr.AddKeyValue ("userID", arrFriendIDs [j], false);
            frdStr = frdStr.AddParen ();
            frdArrStr += frdStr;
            if (j != arrFriendIDs.Count - 1)
                frdArrStr += ",";
        }
        frdArrStr = frdArrStr.AddSqreBrakt ();

        SendStr = SendStr.AddArray ("arrFriendID", frdArrStr, false);

        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasFriendRank :: postAction " + Result.result);
            // :[{"userID":"AppsTest0101","weekScore":0,"bestScore":0,"contWinNum":0,"winNumWeek":0,"lossNumWeek":0,"winNum":0,"lossNum":0},

            //JsonData arrJsO = JData ["arrFriendRank"];
            JSONNode arrJsO = NdObj ["arrFriendRank"];

            Ag.LogString ("   Friend Count    " + arrJsO.Count);
            for (int j = 0; j < arrJsO.Count; j++) {
                WasRank anObj = new WasRank (); // JsonMapper.ToObject<WasRank> (arrJsO [j].ToJson ());
                anObj.Parse (arrJsO [j]);
                User.arrFriendRank.Add (anObj);
            }
            messageAction (Result.result);
            Ag.LogString ("WasFriendRank :: postAction >>>  ");
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasFriendRank :: CatchAction ...   ");
    }
}

public class WasItemInfo : WasObject
{
    public int Gold;

    public override void SendAction ()
    {
        Ag.LogString ("WasItemInfo :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 233);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1, false);

        SendStr = SendStr.AddParen ();

        postAction += () => {
            JSONNode itemArr = NdObj ["arrItem"];

            if (User.ParseItem (itemArr))
                Ag.LogDouble ("     WasItemInfo    OK    ");

            messageAction (Result.result);
            Ag.LogString ("WasItemInfo :: postAction >>>  ");
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasItemInfo :: CatchAction ...   ");
    }
}

public class WasCardUniformCostume : WasObject // uniform 241, costume : 242  // 가져오기. 
{
    public int code;
    //card 240, unif 241, cost 242
    public override void SendAction ()
    {
        Ag.LogString ("WasCardUniformCostume :: SendAction ...   Started ...  " + code);
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, code);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddParen ();
        postAction += () => {
            Ag.LogString ("WasCardUniformCostume :: postAction " + Result.result + "   " + code);
    
            //JsonData jsUInfo = JsonMapper.ToObject (RcvdStr);
            JSONNode jsCnt;
            switch (code) {
            case 240:
                jsCnt = NdObj ["arrCard"];
                if (User.ParseCards (jsCnt))
                    Ag.LogIntenseWord ("     Fetch Card    OK    ");
                break;
            case 241:
                jsCnt = NdObj ["arrUniform"];
                if (User.ParseUniform (jsCnt))
                    Ag.LogIntenseWord ("     Fetch Uniform    OK    ");
                break;
            case 242:
                jsCnt = NdObj ["arrCostume"];
                if (User.ParseCostume (jsCnt))
                    Ag.LogIntenseWord ("     Fetch Costume    OK    ");
                break;
            }
    
            messageAction (Result.result);
            Ag.LogString ("WasCardUniformCostume :: postAction >>>  ");
        };
    
        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasCardArray :: CatchAction ...   ");
    }
}

public class WasCostumeUpdate : WasObject
{
    // 325
    public int Gold;

    public override void SendAction ()
    {
        Ag.LogString ("WasUniformUpdate :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 325);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);

        string unifArr = "   ";
        for (int i = 0; i < User.arrCostume.Count; i++) {
            AmCostume uObj = User.arrCostume [i];
            unifArr += uObj.WAS.ToJsonStr (); // JsonMapper.ToJson (uObj.WAS);
            if (i != User.arrCostume.Count - 1)
                unifArr += ",";
        }
        unifArr = unifArr.AddSqreBrakt ();
        SendStr = SendStr.AddArray ("arrCostume", unifArr, false);
        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasCostumeUpdate :: postAction " + Result.result);
            messageAction (Result.result);
            Ag.LogString ("WasCostumeUpdate :: postAction >>>  ");
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasCostumeUpdate :: CatchAction ...   ");
    }
}

public class WasUniformUpdate : WasObject
{
    // 320
    public int Gold;

    List<AmUniform> partOfAll = null;

    public override void SendAction ()
    {
        Ag.LogString ("WasUniformUpdate :: SendAction ...   Started ...  ");

        if (partOfAll == null) {
            partOfAll = new List<AmUniform> ();

            for (int k = 0; k < User.arrUniform.Count; k++) {
                if (k == 0 || User.arrUniform [k].mustUpdate)
                    partOfAll.Add (User.arrUniform [k]);
            }
        }

        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 320);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        string unifArr = "";

        int sendNum = partOfAll.Count > 8 ? 8 : partOfAll.Count;

        Ag.LogDouble ("  Update  >>> ....      Remain >>> " + sendNum);

        for (int i = 0; i < sendNum; i++) {
            AmUniform uObj = partOfAll [i];
            //Ag.LogString (" Must Update ?? " + uObj.mustUpdate);
            //if (uObj.mustUpdate || i == 0)
                unifArr += uObj.ToJsonStr () + " , ";
        }

        partOfAll.RemoveRange (0, sendNum);

        unifArr = unifArr.Substring (0, unifArr.Length - 2);
        unifArr = unifArr.AddSqreBrakt ();
        SendStr = SendStr.AddArray ("arrUniform", unifArr, false);

        SendStr = SendStr.AddParen ();

        postAction += () => {
            Ag.LogString ("WasUniformUpdate :: postAction " + Result.result);

            if (partOfAll.Count > 0) {
                Ag.LogIntenseWord ("   Update Again ....      Remain >>> " + partOfAll.Count);
                SendAction ();
            }

            messageAction (Result.result);
            Ag.LogString (" postAction ");
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasUniformUpdate :: CatchAction ...   ");
    }
}
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Login  _____  Class  _____
public class WasUserUpdate : WasObject
{
    public string etcInfo;
    //  _////////////////////////////////////////////////_    _____   Main   _____   Methods   _____
    public override void SendAction ()
    {
        WasUserInfo addObj;
        Ag.LogString ("WasUserUpdate :: SendAction ...   Started ...  ");
        SendStr = "";
        SendStr = SendStr.AddCodeKeyKKOID (User, 102);
        SendStr = SendStr.AddKeyValue ("formatVersion", 1);
        SendStr = SendStr.AddKeyValue ("etcInfo", etcInfo, false);
        SendStr = SendStr.AddParen ();
        postAction = () => {
            if (Result.result == 0)
                addObj = new WasUserInfo () { User = User, flag = 0 };
            messageAction (Result.result);
        };

        SendAndRciv ();
    }

    public override bool JobCompleted ()
    {
        if (!IsOK.HasValue)
            return false;
        return IsOK.Value;
    }

    public override void CatchAction ()
    {
        Ag.LogString ("WasUserUpdate :: CatchAction ...   ");
    }
}