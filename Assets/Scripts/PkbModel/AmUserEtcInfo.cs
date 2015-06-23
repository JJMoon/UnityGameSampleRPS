// [2013:11:27:MOON<AmUserWAS start>]
using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

public class AmDayMonthEvent
{
    int Day, Mon;
    public string name;

    public string AsString { set { Parse (value); } }

    public AmDayMonthEvent (string pname)
    {
        InitValue ();
    }

    public AmDayMonthEvent (string pS, string pname)
    {
        name = pname;
        Parse (pS);
    }

    public void InitValue ()
    {
        Day = Mon = 0;
    }

    void Parse (string pS)
    {
        try {
            StackOfInt stObj = new StackOfInt (pS);
            Mon = stObj.PopHeadInt ();
            Day = stObj.PopHeadInt ();
            stObj = null;
        } catch {
            Ag.LogIntenseWord (" No Info :::   Set    0 / 0   D/M  ");
        }
    }

    public string ConfirmToday ()
    {
        DateTime dtNow = DateTime.FromFileTimeUtc (Ag.TimeNow); // DateTime.Now;
        Mon = dtNow.Month;
        Day = dtNow.Day;
        return (Mon + "_" + Day);
    }

    public string GetString ()
    {
        return (Mon + "_" + Day);
    }

    public bool ShowDailyEvent {
        get { 
            DateTime dtNow = DateTime.Now;
            return !(Mon == dtNow.Month && Day == dtNow.Day);
        }
    }

    public void ShowMySelf ()
    {
        Ag.LogStartWithStr (1, "  AmDayMonthEvent  :  " + name + "      M / D  ::   " + Mon + "  /  " + Day);
    }
}
//  _////////////////////////////////////////////////_    _____   class   _____   User Etc Info    _____
public class WasUserEtcInfo
{
    // input 은 property 에.. 업데이트 시 dodge .. Parsing 시 property에..
    public int FGame, ContLoseNum, DailyChkMon, DailyChkDay, GameNumToday, HeartPopup, SingleTry, HeartRemainSec;
    //
    public int ExperienceScard;
    // 0아직 안함 1이면 나중에
    public string CTimeHeartSet, CTimeScout, CTimeContWin, CTimePosting;
    // No Parsing Datum.
    public DateTime DtHeart, DtScout, DtContWin, DtPosting;
    public AmDayMonthEvent DmeSingleTryS, DmeSingleTryA;
    // 0 : Not yet, 1 : Done
    public WasUserEtcInfo ()
    {
        SetCTnow ();
        DmeSingleTryA = new AmDayMonthEvent ("SingleTry A");
        DmeSingleTryS = new AmDayMonthEvent ("SingleTry S");
    }

    void SetCTnow ()
    {
        Ag.LogString ("WasUserEtcInfo   Set CTime   to   N O W   .....    ");
        DtHeart = DateTime.Now;
        HeartRemainSec = AgStt.CTHeartMaxSeconds;
        DtScout = DateTime.Now;
        DtContWin = DateTime.Now;
        DtPosting = DateTime.Now;
    }

    public bool Parse (JSONNode jsNd)
    {
        Ag.LogStartWithStr (1, "   WasUserEtcInfo :: Parse  with      " + jsNd.ToString ());
        bool rslt = true;
        try {
            string sTryA, sTryS;    
            rslt = (jsNd.ParseTo ("FGame", out FGame, "ContLoseNum", out ContLoseNum, "DailyChkMon", out DailyChkMon) &&
            jsNd.ParseTo ("DailyChkDay", out DailyChkDay, "GameNumToday", out GameNumToday, "HeartPopup", out HeartPopup) &&
            jsNd.ParseTo ("DmeSingleTryA", out sTryA, "DmeSingleTryS", out sTryS) &&
            jsNd.ParseTo ("SingleTry", out SingleTry, "HeartRemainSec", out HeartRemainSec));
            DmeSingleTryA = new AmDayMonthEvent (sTryA, "SingleTry A");
            DmeSingleTryS = new AmDayMonthEvent (sTryS, "SingleTry S");
        } catch {
            Ag.LogIntenseWord ("WasUserEtcInfo    Error      in  FGame, ContLoseNum, DailyChkMon,   etc    ");
        }
        try {  // Cool Time Info ... 
            rslt = jsNd.ParseTo ("CTimeHeartSet", out CTimeHeartSet, "CTimeScout", out CTimeScout, "CTimeContWin", out CTimeContWin);
            DtHeart = DateTime.FromFileTimeUtc (long.Parse (CTimeHeartSet)); //  UtTimestamp.ToDateTime (int.Parse (CTimeHeartSet));
            DtScout = DateTime.FromFileTimeUtc (long.Parse (CTimeScout)); // UtTimestamp.ToDateTime (int.Parse (CTimeScout));
            DtContWin = DateTime.FromFileTimeUtc (long.Parse (CTimeContWin));// UtTimestamp.ToDateTime (int.Parse (CTimeContWin));
        } catch {
            SetCTnow ();
        }
        try {
            rslt = jsNd.ParseTo ("CTimePosting", out CTimePosting);
            DtPosting = DateTime.FromFileTimeUtc (long.Parse (CTimePosting));
        } catch {
            DtPosting = DateTime.Now - TimeSpan.FromHours(24);
        }
        return rslt;
    }

    public string ToJsonStr ()
    {
        string SendStr = "";
        CTimeHeartSet = DtHeart.ToFileTimeUtc ().ToString (); // UtTimestamp.ToTimestamp (DtHeart).ToString ();
        CTimeScout = DtScout.ToFileTimeUtc ().ToString (); // UtTimestamp.ToTimestamp (DtScout).ToString ();
        CTimeContWin = DtContWin.ToFileTimeUtc ().ToString (); // UtTimestamp.ToTimestamp (DtContWin).ToString ();

        SendStr = SendStr.AddKV3 ("FGame", FGame, "ContLoseNum", ContLoseNum, "DailyChkMon", DailyChkMon);
        SendStr = SendStr.AddKV2 ("SingleTry", SingleTry, "HeartRemainSec", HeartRemainSec);
        SendStr = SendStr.AddKV2 ("DmeSingleTryA", DmeSingleTryA.GetString (), "DmeSingleTryS", DmeSingleTryS.GetString ());
        SendStr = SendStr.AddKV2 ("CTimeHeartSet", CTimeHeartSet, "CTimeScout", CTimeScout);
        SendStr = SendStr.AddKV2 ("CTimeContWin", CTimeContWin, "CTimePosting", CTimePosting);
        SendStr = SendStr.AddKV3 ("DailyChkDay", DailyChkDay, "GameNumToday", GameNumToday, "HeartPopup", HeartPopup, false); //, "winNum", winNum, "lossNum", lossNum);
        SendStr = SendStr.AddParen ();
        return SendStr;
    }

    public void ShowMyself ()
    {
        Ag.LogStartWithStr (2, "    WasUserEtcInfo ::  Show Myself ()   ");
        string.Format (" WasUserEtcInfo ::  First Game : < {0} >, ContLoseNum : < {1} > ,  DailyChk M/D : {2} / {3} , GameNum : {4} ", 
            FGame, ContLoseNum, DailyChkMon, DailyChkDay, GameNumToday).HtLog ();
        string.Format ("       Cool Time :::    Heart : < {0} / {1} >,   Scout : < {2} > ,  Cont Win  : {3} ", 
            DtHeart.ToString (), HeartRemainSec, DtScout.ToString (), DtContWin.ToString ()).HtLog ();
    }
}
//  _////////////////////////////////////////////////_    _____   class   _____   Am User    _____
public partial class AmUser : AmObject
{
    public int MinutesAfterRegist { 
        get {
            string TimeStmp = PreviewLabs.PlayerPrefs.HasKey ("RegistTimeStamp") ? PreviewLabs.PlayerPrefs.GetString ("RegistTimeStamp") : "0";
            DateTime registTime = UtTimestamp.ToDateTime (int.Parse (TimeStmp));
            TimeSpan tSpan = DateTime.Now - registTime;
            return tSpan.Minutes;
        }
    }

    public int GameNumberOfToday { 
        get { 
            string MonDayGNum = PreviewLabs.PlayerPrefs.HasKey ("GameNumInts") ? PreviewLabs.PlayerPrefs.GetString ("GameNumInts") : "0_0_0";
            StackOfInt iObj = new StackOfInt (MonDayGNum);
            int mon = iObj.PopHeadInt (), day = iObj.PopHeadInt (), gNum = iObj.PopHeadInt ();
            if (mon == DateTime.Now.Month && day == DateTime.Now.Day)
                return gNum;
            else
                return 0;
        }
    }
    //  _////////////////////////////////////////////////_    _____   Game Number of Today   _____   Write   _____
    void IncreseGameNumber ()
    {
        string MonDayGNum = PreviewLabs.PlayerPrefs.HasKey ("GameNumInts") ? PreviewLabs.PlayerPrefs.GetString ("GameNumInts") : "0_0_0";

        StackOfInt iObj = new StackOfInt (MonDayGNum);
        int mon = iObj.PopHeadInt (), day = iObj.PopHeadInt (), gNum = iObj.PopHeadInt ();

        if (mon == DateTime.Now.Month && day == DateTime.Now.Day)
            gNum++;
        else {
            mon = DateTime.Now.Month;
            day = DateTime.Now.Day;
            gNum = 1;
        }

        MonDayGNum = mon.ToString () + "_" + day.ToString () + "_" + gNum.ToString ();
        PreviewLabs.PlayerPrefs.SetString ("GameNumInts", MonDayGNum);
    }
    //  _////////////////////////////////////////////////_    _____   Day Month   _____   Single Try   _____
    public bool ShowSingleTry (bool isClassS)
    {
        AmDayMonthEvent curObj = isClassS ? etcInfoObj.DmeSingleTryS : etcInfoObj.DmeSingleTryA;
        curObj.ShowMySelf ();
        return curObj.ShowDailyEvent;
    }

    public void ConfirmSingleTry (bool isClassS)
    {
        AmDayMonthEvent curObj = isClassS ? etcInfoObj.DmeSingleTryS : etcInfoObj.DmeSingleTryA;
        curObj.ConfirmToday ();
        curObj.ShowMySelf ();
        UpdateEtcInfoObj ("ConfirmSingleTry");
    }

    public void InitSingleTry ()
    {
        etcInfoObj.DmeSingleTryA.InitValue ();
        etcInfoObj.DmeSingleTryS.InitValue ();
        UpdateEtcInfoObj ("InitSingleTry");
    }
    //  _////////////////////////////////////////////////_    _____   CoolTime   _____   Cont Win   _____
    public bool ContWinStarted;

    public void ContWinCoolTimeRemain (out int Min, out int Sec)
    {
        Sec = Min = 0;
        int reSec = CurrentContWinRemainSec;
        reSec = (reSec < 0) ? reSec = 0 : reSec;
        reSec = reSec.GetSmaller (AgStt.CTContWin);
        //Ag.LogString ("  AmUserEtcInfo ::  ContWinCoolTimeRemain  >>>   remain Seconds : " + reSec + "  etcInfoObj.DtContWin  " + etcInfoObj.DtContWin + "  Now " + Ag.Now ());
        reSec.DivideMinSec (out Min, out Sec);
    }

    public void CoolTimeChooseOneMoreGameWin () // I choose try one more game ... 
    {
        Ag.LogIntenseWord ("  CoolTimeChooseOneMoreGameWin     Reset ...    ");
        etcInfoObj.DtContWin = Ag.Now ().AddSeconds (AgStt.CTContWin + 1);
        etcInfoObj.ShowMyself ();
        UpdateEtcInfoObj ("CoolTimeChooseOneMoreGameWin");
    }

    public float ContWinCoolTimeRemainPercent ()
    {
        int reSec = CurrentContWinRemainSec;
        return ((float)reSec) / AgStt.CTContWin * 100;
    }

    public bool CanITryIncreseContWin { get { return (CurrentContWinRemainSec > 0); } }

    int CurrentContWinRemainSec { get { return (int)((etcInfoObj.DtContWin - Ag.Now ()).TotalSeconds); } }
    //  _////////////////////////////////////////////////_    _____   CoolTime   _____   Facebook Posting   _____
    public void PostingCoolTimeRemain (out int Min, out int Sec)
    {
        Sec = Min = 0;
        int reSec = PostingRemainSec;
        reSec = (reSec < 0) ? reSec = 0 : reSec;
        reSec = reSec.GetSmaller (AgStt.CTFacebookPostingLimit);
        reSec.DivideMinSec (out Min, out Sec);
    }
    
    public void CoolTimePostingAction ()
    {
        etcInfoObj.DtPosting = Ag.Now ().AddSeconds (AgStt.CTFacebookPostingLimit + 1);
        etcInfoObj.ShowMyself ();
        UpdateEtcInfoObj ("CoolTimePostingAction");
    }
    
//    public float CollTimePercent ()
//    {
//        int reSec = AgStt.CTScout - CurrentScoutRemainSec;
//        return ((float)reSec) / AgStt.CTScout * 100;
//    }
//    
    public bool CanIPostNow { get { return (PostingRemainSec < 0); } }
    
    int PostingRemainSec {
        get { 
            return (int)(etcInfoObj.DtPosting - Ag.Now ()).TotalSeconds;
        }
    }

    //  _////////////////////////////////////////////////_    _____   CoolTime   _____   Scout   _____
    public void ScoutCoolTimeRemain (out int Min, out int Sec)
    {
        Sec = Min = 0;
        int reSec = CurrentScoutRemainSec;
        reSec = (reSec < 0) ? reSec = 0 : reSec;
        reSec = reSec.GetSmaller (AgStt.CTScout);
        reSec.DivideMinSec (out Min, out Sec);
    }

    public void CoolTimeScoutUse ()
    {
        etcInfoObj.DtScout = Ag.Now ().AddSeconds (AgStt.CTScout + 1);
        etcInfoObj.ShowMyself ();
        UpdateEtcInfoObj ("CoolTimeScoutUse");
    }

    public float CollTimeScoutPercent ()
    {
        int reSec = AgStt.CTScout - CurrentScoutRemainSec;
        return ((float)reSec) / AgStt.CTScout * 100;
    }

    public bool CanIScoutNow { get { return (CurrentScoutRemainSec < 0); } }

    int CurrentScoutRemainSec {
        get { 
            if (DeckScouter)
                return (int)((etcInfoObj.DtScout - Ag.Now ()).TotalSeconds * AgStt.CTScouterDeckEffectFactor);
            return (int)(etcInfoObj.DtScout - Ag.Now ()).TotalSeconds;
        }
    }
    //  _////////////////////////////////////////////////_    _____   CoolTime   _____   Heart   _____
    /// <summary>
    /// 하트 남은 시간 표시. + 경기 가능, - 불가
    /// </summary>
    public void HeartCoolTime (out int Min, out int Sec)
    {
        Sec = Min = 0;
        int reSec = CurrentRemainSec ();
        reSec.DivideMinSec (out Min, out Sec);
    }

    public float HeartPercent ()
    {
        int reSec = CurrentRemainSec ();
        return ((float)reSec) / AgStt.CTHeartMaxSeconds * 100;
    }

    public void HeartPercent (out int num, out int frag)
    {
        int reSec = CurrentRemainSec ();
        num = (int)((((float)reSec) / AgStt.CTHeartMaxSeconds) * 100);
        frag = (int)((((float)reSec) / AgStt.CTHeartMaxSeconds) * 1000) % 10;
    }

    public int HeartCoolTime ()
    {
        int reSec = CurrentRemainSec ();
        if (reSec > AgStt.CTHeartMaxSeconds)
            return 1000;
        int percent = (int)((((float)reSec) / AgStt.CTHeartMaxSeconds) * 1000);
        //return percent < 0 ? 0 : percent;
        return percent;
    }

    public int HeartCoolTimeSec ()
    {
        int reSec = CurrentRemainSec ();
        return reSec;
    }

    public void HeartSetMax ()
    {
        etcInfoObj.HeartRemainSec = AgStt.CTHeartMaxSeconds;
        etcInfoObj.DtHeart = Ag.Now ();
        UpdateEtcInfoObj ("HeartSetMax");
    }

    int CurrentRemainSecWithNoLimit ()
    {
        int remainSec = etcInfoObj.HeartRemainSec;
        double healthAdded = (Ag.Now () - etcInfoObj.DtHeart).TotalSeconds;  // +
        healthAdded *= AgStt.CTHeartRecoverFactor;
        remainSec += (int)healthAdded;
        return remainSec;
    }

    public int CurrentRemainSec ()
    {
        int remainSec = CurrentRemainSecWithNoLimit ();
        if (remainSec > AgStt.CTHeartMaxSeconds)
            remainSec = AgStt.CTHeartMaxSeconds;
        return remainSec;
    }

    public void HeartCoolTimeResetWithNow ()
    {
        etcInfoObj.HeartRemainSec = CurrentRemainSecWithNoLimit ();

        Ag.LogIntenseWord ("  etcInfoObj.HeartCoolTimeResetWithNow  >> remainSec ::  " + etcInfoObj.HeartRemainSec + " with Now ");

        etcInfoObj.DtHeart = Ag.Now ();
        etcInfoObj.ShowMyself ();
        UpdateEtcInfoObj ("HeartCoolTimeResetWithNow");
    }

    public void HeartCoolTimeNewGameStarted ()
    {
        etcInfoObj.HeartRemainSec = CurrentRemainSec () - AgStt.CTHeartGameHealth;
        etcInfoObj.DtHeart = Ag.Now ();
        etcInfoObj.ShowMyself ();
        UpdateEtcInfoObj ("HeartCoolTimeNewGameStarted");
    }

    public void FirstGameDoneWithBot () // 봇 기록
    { // 봇하고 첫 게임을 하면 이 함수를 실행할 것.
        etcInfoObj.FGame = 1;
        UpdateEtcInfoObj ("FirstGameDoneWithBot");
    }
}
