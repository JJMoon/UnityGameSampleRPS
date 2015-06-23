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

public class ATestClass
{
    public int A = 55, B = 24234, C = 222;
    public string sA, sB = "\" some String \"", sCC = " Test String";
}
// http://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa  dictionary <-> object   mapping example ..
public partial class Test : AmSceneBase
{
    public GameObject mTestObj, Angri, mCamera;
    AmUser myUser, user2;
    AgTime timerObj = new AgTime ();
    FpLoginObj pluginObj = new FpLoginObj ();
    string chkTeamName = "12345678910";
    NodeActions node1, node2, node3;
    bool DailyChkReturn = false;
    // For Scout Test ..
    Scouter aScout = new Scouter ("1_2_3_4_1_2_3_4_1_2_3_4");
    int Dir, Success;
    // Node Test
    bool IsNodeScreen = false;



    public override void Start ()
    {
        Ag.LogIntenseWord (" Test.cs :: Start ");
        myGUI = new AmUI ();
        myGUI.SetColumns (3, 18);
        ndGUI.SetColumns (2, 18);

        mTimeLooseAtStartPoint = 0.5f;
        base.Start ();
        // Test ID Setting ....    WAS, Node     related ......    etc...
        Ag.mySelf = myUser = new AmUser ();

        // Kakao Login case
        myUser.WAS.KkoID = "88214690633939999"; //"00000690633939993";//(CAMERA)  // "88214690633939999";  // <FakeKKO>  88299368562514961 <Legend Card>

//        Ag.mGuest = true;
//        myUser.DeviceID = "MOONTEST0000";


        myUser.WAS.TeamName = "TeamName22"; // 01604eb9f3657ae65bb9d8382b36d4c7 <Rolco DID>  //  "90973535271650928";//(CAMERA)
        myUser.WAS.KkoNick = "Nick2424";
        myUser.WAS.Country = 11;

        user2 = new AmUser ();

        user2.DeviceID = user2.DeviceID + "Alt";

        user2.WAS.KkoID = "91278098233517152";//"90060594732486160"; //"88894476708738001";//"APPS_TEST_ID_0002";  90060594732486160  88306087115705857  90060594732486160 <KimDR> 
        user2.WAS.TeamName = "Teamamama";  // 88214690633939121<Rolco>  91278098233517152 <Moon iPAD>  88712330645978192 <Moon> 88159078716546208 <Cho>
        user2.WAS.KkoNick = "Nickkkkk";
        user2.WAS.Country = 22;

        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____   Test Started   _____");




        Ag.LogString ("   abc_34 >>> " + "abc_34".GetContinuousInteger ());
        Ag.LogString ("   Pro_5 >>> " + "Pro_5".GetContinuousInteger ());
        Ag.LogString ("   empty >>> " + "".GetContinuousInteger ());

        MtCompact aCom = new MtCompact (60);

        aCom.AddNum (10);
        aCom.AddNum (5);
        aCom.AddNum (8);
        aCom.AddNum (9);
        aCom.AddNum (31);

        aCom.AddNum (29);
        aCom.AddNum (48);
        aCom.AddNum (59);
        aCom.AddNum (9);
        aCom.AddNum (59);

        for (int k = 0; k < 10; k++) {
            Ag.LogString ("  nth >> " + k + "    val >> " + aCom.GetNth (k));
        }

        aCom.ParseSelf ();


        Ag.LogString (" time span check " + DateTime.Now + " is now " + (DateTime.Now - TimeSpan.FromHours (24)));


        #if UNITY_IPHONE


        AgStt.mIAP.TheUser = myUser;

        NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound);
//        Ag.LogIntenseWord ("NotificationServices.RegisterForRemoteNotificationTypes  ");


        //        Ag.LogString ("  string Null Check " + string.IsNullOrEmpty (null));   // IsNullOrEmpty  string ...
        //        Ag.LogString ("  string Null Check " + string.IsNullOrEmpty (""));  true
        //        Ag.LogString ("  string Null Check " + string.IsNullOrEmpty (" ")); false

        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____   Time & Date   _____");
        DateTime dtNow = DateTime.Now;
        Ag.LogString (" theNow.ToLongTimeString () " + dtNow.ToLongTimeString ());
        Ag.LogString (" 1398369347000 theNow.ToFileTime () " + dtNow.ToFileTime ());
        Ag.LogString (" theNow.ToFileTimeUtc () " + dtNow.ToFileTimeUtc ());

        long lgNow = dtNow.ToFileTimeUtc ();
        Ag.LogString (" Recover >>> " + DateTime.FromFileTimeUtc (lgNow).ToString ());


        (" dtNow   " + dtNow.ToString ()).HtLog ();
        dtNow = dtNow.AddSeconds (1234567);
        (" dtNow   " + dtNow.ToString ()).HtLog ();
        (" 10,000 sec :: " + UtTimestamp.ToDateTime (100000).ToString ()).HtLog ();
        int iNow = UtTimestamp.ToTimestamp (DateTime.Now);
        (" UtTimestamp.ToTimestamp  : Now  ==> " + iNow).HtLog ();
        (" Back to Not :: " + UtTimestamp.ToDateTime (iNow).ToString ()).HtLog ();

        DateTime after10min = UtTimestamp.ToDateTime (iNow + 600);

        Ag.LogString (" After 10 Min ::  " + after10min.Minute.ToString () + " : " + after10min.Second.ToString ());


        //  _////////////////////////////////////////////////_    _____  Test  _____    Score   _____
                GameLogic (); 

        //  _////////////////////////////////////////////////_    _____  Test  _____    Encript   _____
        // TestEncript();

        //  _////////////////////////////////////////////////_    _____  Test  _____    Deck System   _____
        Ag.LogString ("//  _////////////////////////////////////////////////_    _____  Test  _____    Deck System   Case 1 >> _____");

        AmGameLogic aGLogic = new AmGameLogic ("", "");
        Ag.LogString ("  Score " + 5000 + "  1 * 7% " + aGLogic.ApplyDeckIncrease (5000f, new int[] { 0, 1, 0 }, 1));
        Ag.LogString ("  Score " + 5000 + "  2 * 7% " + aGLogic.ApplyDeckIncrease (5000f, new int[] { 1, 1, 0 }, 2));
        Ag.LogString ("  Score " + 5000 + "  3 * 7% " + aGLogic.ApplyDeckIncrease (5000f, new int[] { 1, 1, 1 }, 3));

        Ag.LogString ("  Score " + 5000 + "  1 * 10% " + aGLogic.ApplyDeckLosingScore (5000f, new int[] { 2, 7, 3 }, 2));
        Ag.LogString ("  Score " + 5000 + "  2 * 10% " + aGLogic.ApplyDeckLosingScore (5000f, new int[] { 7, 7, 3 }, 2));
        Ag.LogString ("  Score " + 5000 + "  3 * 10% " + aGLogic.ApplyDeckLosingScore (5000f, new int[] { 7, 7, 7 }, 2));



        //  _////////////////////////////////////////////////_    _____  Test  _____    Meta Programming   _____
        Ag.LogDouble ("  Meta Programming >>>  " + char.Parse ("3") + " " + char.Parse ("4") + char.Parse ("K"));
//        char ch3 = char.Parse ("3"), ch4 = char.Parse ("4");
//        (ch3 > ch4).ToString ().HtLog ();
//        (ch3 < ch4).ToString ().HtLog ();
//        (" 0 : to Byte " + Convert.ToByte ("0").ToString () + " -> char -> byte : " + Convert.ToByte (char.Parse ("0"))).HtLog ();
//        (" 1 : to Byte " + Convert.ToByte ("1").ToString () + " -> char -> byte : " + Convert.ToByte (char.Parse ("1"))).HtLog ();
//        (" 9 : to Byte " + Convert.ToByte ("9").ToString () + " -> char -> byte : " + Convert.ToByte (char.Parse ("9"))).HtLog ();
//        ("00345 parse :: " + int.Parse ("000345")).HtLog ();
//
//        ("GetContinuousInteger  Test >>   abcde345dkdk  ::  " + "abcde345dkdk".GetContinuousInteger ()).HtLog ();
//        ("GetContinuousInteger  Test >>   abcde00345dkdk34399  ::  " + "abcde00345dkdk34399".GetContinuousInteger ()).HtLog ();
//        ("GetContinuousInteger  Test >>   abcde002345dkdk34399  ::  " + "abcde002345dkdk34399".GetContinuousInteger ()).HtLog ();
//        ("GetContinuousInteger  Test >>   abcdewlwlwl  ::  " + "abcdewlwlwl".GetContinuousInteger ()).HtLog ();

        //  _////////////////////////////////////////////////_    _____  Test  _____    Game Win / Lose   _____
//        bool goal = AgUtilGame.DidKickerWinThisTurn(5, 3, 0, 0);
//        Ag.LogString ("   goal  >>>   " + goal);
//        goal = AgUtilGame.DidKickerWinThisTurn(5, 3, 1, 2);
//        Ag.LogString ("   goal  >>>   " + goal);

        //  _////////////////////////////////////////////////_    _____  Test  _____    ??   _____
//
//
//        string str = "1~`aA+_';\"지금";
//
//        Encoding unicode = Encoding.Unicode;
//        Encoding utf8 = Encoding.UTF8;
//
//        byte[] unicodeBytes = unicode.GetBytes(str);
//
//        byte[] utf8Bytes = Encoding.Convert( unicode,
//            utf8,
//            unicodeBytes );
//
//        //Console.WriteLine( "UTF Bytes:" );
//        StringBuilder sb = new StringBuilder();
//        foreach( byte b in utf8Bytes ) {
//            sb.Append( b ).Append(" : ");
//        }
//
//
//        Ag.LogIntenseWord ("   UTF 16 :: " + sb.ToString());
//
        Ag.LogString ("  Jail Break Result >>>>>  Fb.JailBreakIOS : " + Fb.JailBreakIOS);
        #endif
        timerObj.WaitTimeFor (0, 0, 8);


        #if UNITY_EDITOR
        " #if UNITY_EDITOR ".HtLog ();
        #endif

        //  _////////////////////////////////////////////////_    _____  Test  _____    T e s t   _____
        // # 참고... String 이 너무 짧아 crash 되는 소스. iOS try 문에서 잡아줌. ...
        //NotificationServices.enabledRemoteNotificationTypes.ToString ().HtLog ();



//        Ag.LogStartWithStr (3, "  Card  WAS   SetDirection ()  ");
//        AmCard tCrd = new AmCard ();
//        tCrd.WAS.grade = "S";
//        tCrd.WAS.isKicker = true;
//        tCrd.WAS.SetDirection ();
//        tCrd.WAS.ShowMySelf ();

        //Ag.SingleTry = 1;

        string hanTest = WWW.UnEscapeURL ("%EC%9E%AC%EA%B2%BD%EA%B8%B0%EC%88%98%EB%9D%BD%20%EC%8B%9C%EA%B0%84%EC%B4%88%EA%B3%BC");

        hanTest = "%E0%B8%B8%32";
        if (hanTest == "%E0%B8%B8")
            Ag.LogIntenseWord ("  if (hanTest  ");

        WWW.EscapeURL ("ุ").HtLog ();
        hanTest = WWW.UnEscapeURL (null);

        string errhanTest = WWW.UnEscapeURL (WWW.EscapeURL ("종국이 총각 김치")); // Error

        Ag.LogStartWithStr (3, "  0.ToFixedWidth (jarisu++).HtLog();" + hanTest + errhanTest);

        int jarisu = 0;
        0.ToFixedWidth (jarisu++).HtLog ();

        //  AgUtil.ToJson  int 어레이를 Json 으로. 
        ("  AgUtil.ToJson  Test ::   " + AgUtil.IntArrToJson ("AgUtil.ToJson", new int[] {
            3,            4,            5
        })).HtLog ();
        ("  AgUtil.ToJson  Empty ::   " + AgUtil.IntArrToJson ("AgUtil.ToJson  >> ", new int[] { })).HtLog ();
        Ag.LogNewLine (1);

        // Null Parsing
        string strNullInclude = " {\"WasKey\":null}";
        JSONNode aNde = JSON.Parse (strNullInclude);
        string strNullParsed = aNde ["WasKey"];
        Ag.LogString ("  if (aNde['WasKey'] == null)   is true   " + aNde ["WasKey"] + aNde ["WasKey"].ToString ());
        if (AgUtil.IsNullJson (aNde ["WasKey"]))
            Ag.LogString ("  OK   print this line ......     AgUtil.IsNullJson    Success  ...... ");
        ("  this is null parse to Int test >>>  " + aNde ["Nooo"].AsInt).HtLog ();
        ("  this is null parsing  >>>>  " + aNde ["WasKey"]).HtLog (); // null
        ("  this is null parsed String  >>>>  " + strNullParsed + "  Length : " + strNullParsed.Length).HtLog (); // null4 .. ??


        // Simple JSON Test
        ATestClass aObb = new ATestClass ();
        string jsStr = JsonMapper.ToJson (aObb); // LitJson 으로 인코딩.. int 355 
        ("  object ==> JsonMapper.ToJson  ==>   jsStr :: >>   " + jsStr).HtLog ();
        JSONNode jsLitSim = JSON.Parse (jsStr);
        ("       Parsed  as  >>>>      " + jsLitSim.ToString ()).HtLog (); 
        ("  If no info JSON Parsing ::  This will Crash ???   " + jsLitSim ["Empty"].AsBool.ToString () + " <<  AsBool == false ...   ").HtLog ();
        ("  If no info JSON Parsing ::  This will Crash ???   " + jsLitSim ["Empty"].AsInt.ToString () + " <<  AsInt == false ...   ").HtLog ();
        ("        Just[\"sB\"] :  " + jsLitSim ["sB"] + "        Add  ToString() : " + jsLitSim ["sB"].ToString ()).HtLog ();
        //public string nameOfYourVariable = MemberInfoGetting.GetMemberName(() => MyVariable);
        Ag.LogNewLine (1);

        // Print Name of variables ....   Reflection  ???
        int nameOfInt = 35;
        string nameOfYourVariable = AgUtil.GetN (() => nameOfInt);
        ("   nameOfYourVariable  >>  Test ::  Key : " + nameOfYourVariable + "  Value : " + nameOfInt).HtLog ();
        Ag.LogNewLine (1);

        // Null test ...
        ("  {  }   Test IsJsonNull  >> All True ?? >>  " + " { } ".IsJsonNull () + " {} ".IsJsonNull () + "null".IsJsonNull () + "{ }".IsJsonNull ()).HtLog ();
        ("  {  }   Test IsJsonNull  >> All True ?? >>  " + " { _ } ".IsJsonNull () + "  }{  ".IsJsonNull ()).HtLog ();

     

        Ag.LogIntense (5, false);
    }

    public override void BaseStartSetting ()
    {
        base.BaseStartSetting ();
    }

    public override void OnApplicationQuit ()
    {
        if (node1 != null)
            node1.CloseNet ();
        if (node2 != null)
            node2.CloseNet ();
    }
    // Update is called once per frame
    int result;

    public override void Update ()
    {
//        if (!tokenSent) {
//            byte[] token = NotificationServices.deviceToken;
//            if (token != null) {
//                // send token to a provider
//                string hexToken = "%" + System.BitConverter.ToString (token).Replace ('-', '%');
//
//                Ag.LogIntenseWord (" Token :: " + hexToken);
//
////                new WWW("http://"+address+"/?token="+hexToken);
//                tokenSent = true;
//            } 
//        }
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  ###  _____  OnGUI  _____
    AmUI BigUI = new AmUI (3, 5);
    AmUI ndGUI = new AmUI ();

    public override void OnGUI ()
    {
       
        if (IsNodeScreen) {

            SetColumnNodeA ();
            SetColumnNodeB ();
        } else {
            SetColumnA ();
            SetColumnB ();
            SetColumnC ();
        }
        GUI.Label (BigUI.GetRect (0, 3), Ag.ScrnLog);
        GUI.Label (BigUI.GetRect (0, 4), Ag.ScrnRcv);
    }
}
