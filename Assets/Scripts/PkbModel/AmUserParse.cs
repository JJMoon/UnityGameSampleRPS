// [2013:11:27:MOON<AmUserWAS start>]
using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

//  _////////////////////////////////////////////////_    _____   class   _____   Am User    _____
public partial class AmUser : AmObject
{
    public bool ParseUserInfoOK (JSONNode pData)
    {
        if (!ParseUserProp (pData))
            return false;

        JSONNode uniformArr, cardArr, itemArr, costumeArr;
        try {
            cardArr = pData ["arrCard"];
            if (cardArr.Count == 0)
                return true;
            if (!ParseCards (cardArr))
                return false;
            uniformArr = pData ["arrUniform"];
            if (!ParseUniform (uniformArr))
                return false;
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse User Info  [ arrCard  arrUniform ] >>>>>  E R R O R  :: ");
        }
        try {
            itemArr = pData ["arrItem"];
            if (!ParseItem (itemArr))
                return false;
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse User Info  [ arrItem ] >>>>>  E R R O R  :: ");
        }
        try {
            costumeArr = pData ["arrCostume"];
            if (costumeArr.Count == 0)
                Ag.LogString (" Costume ::  0   .... Skip   ");
            else if (!ParseCostume (costumeArr))
                return false;
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse User Info  [ Array ] >>>>>  E R R O R  :: ");
        }

        CheckKickOrder ();

        // Ag.mySelf.arrCard [0].mustUpdate = true;  for test ...
        if (Ag.mySelf.ShouldUpdateCard ()) {
            WasCardUpdate aObj = new WasCardUpdate () { User = Ag.mySelf };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" AmUserParse . cs  >>>    Card Update OK..  end of parsing .... ");
                    break;
                }
            };
        }

        Ag.LogString (" Parse ....  SetCostumeToCard   >>>>>     ");
        SetCostumeToCard ();

        return true;
    }

    long FreeCouponTime;
    public DateTime FreeCouponDT;

    public TimeSpan FreeCouponTS { get { return FreeCouponDT - Ag.Now (); } }

    public bool IsFreeCouponRemain { get { return FreeCouponDT > DateTime.Now; } }

    bool ParseUserProp (JSONNode pData)
    {
        string header = "  ParseUserProp ::>>   ";
        try {
            Ag.LogStartWithStr (1, header + " Country , League, etcInfo, freeCoupon ");
            WAS.Country = pData ["country"].AsInt;
            try {
                WAS.TeamName = pData ["teamName"];
            } catch {
                " Team Name Error.  ".HtLog ();
            }
            pData.ParseTo ("league", out WAS.League, "etcInfo", out WAS.etcInfo);
            try {
                FreeCouponTime = long.Parse (pData ["freeCouponDate"]) / 1000; // formatVersion 1 : yyyyMMddHHmmss .. parse ..
                FreeCouponDT = Ag.UnixTimeStampToDateTime (FreeCouponTime);

                Ag.LogString ("  freeCouponDate : " + pData ["freeCouponDate"]); // Ag.UnixTimeStampToDateTime (
                Ag.LogString ("  FreeCouponDT : " + FreeCouponDT + "   Remain Sec   " + FreeCouponTS.TotalSeconds + "  IsFreeCouponRemain ? " + IsFreeCouponRemain);



            } catch {
                FreeCouponTime = Ag.TimeNow;
                " Free Coupon Date Not Set  >>  set to now  ".HtLog ();
            }
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse User Info  [ Country, league, freeCoupon  ] >>>>>  E R R O R  :: ");
        }

        if (WAS.League == null || WAS.League.Length < 3)
            WAS.League = "PRO_5";

        try {
            pData.ParseTo ("cost", out WAS.Cost, "reviewEvent", out WAS.reviewEvent); // 리뷰 확인 0: 아직 안함, 1: 함..
            pData.ParseTo ("gold", out mGold, "cash1", out mCash1, "cash2", out mCash2);
            (header + " gold / cash1 / cash2  " + mGold + "  " + mCash1 + "  " + mCash2).HtLog ();
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse User Info  [ cost, review, gold/cash  ] >>>>>  E R R O R  :: ");
            return false;
        }

        if (pData ["etcInfo"].ToString ().IsJsonNull ()) {
            "   etcInfo is NULL  Reset ... and Update   ".HtLog ();
            etcInfoObj = new WasUserEtcInfo ();
            DateTime dtNow = DateTime.Now;
            etcInfoObj.DailyChkMon = dtNow.Month;
            etcInfoObj.DailyChkDay = dtNow.Day;

            etcInfoObj.HeartRemainSec = AgStt.CTHeartMaxSeconds;
            etcInfoObj.DtHeart = Ag.Now ();

            UpdateEtcInfoObj ("Parsing ..  JsonNull");
        } else {
            try {
                (header + "   etcInfo  :  " + pData ["etcInfo"]).HtLog ();
                //etcInfoObj = JsonMapper.ToObject<WasUserEtcInfo> (pData ["etcInfo"].ToJson ().RecoverFromDodge ().RemoveHeadFootOneChar ());
                etcInfoObj = new WasUserEtcInfo ();
                etcInfoObj.Parse (JSON.Parse (pData ["etcInfo"].ToString ().RecoverFromDodge ().RemoveHeadFootOneChar ()));
                etcInfoObj.ShowMyself ();
            } catch {
                Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse etc Info >>>>>  E R R O R  ::  Read etcInfo  ");
            }
        }
        try {
            ("  My Rank : " + pData ["myRank"].ToString ()).HtLog ();
            myRank = new Rank (pData ["myRank"]); // JsonMapper.ToObject<WasRank> (pData ["myRank"].ToJson ()); //.HtLog ();
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse Rank   >>>>>  E R R O R  :: ");
        }
        //Ag.LogString (" FreeCoupon ::   " + WAS.freeCouponDate + "   Length : " + WAS.freeCouponDate.Length);

        //FreeCouponLimitDT = WAS.freeCouponDate.ToDateTime ();
//        if (WAS.freeCouponDate.Length > 5) { // < 10) {
//            try {
//                FreeCouponLimitDT = WAS.freeCouponDate.ToDateTime ();
//            } catch {
//                FreeCouponLimitDT = DateTime.Now;
//            }
//        } else
//            FreeCouponLimitDT = DateTime.Now;

        Ag.LogString (header + WAS.TeamName + "     has      Cash : " + mCash1 + " / " + mCash2 + "         Gold : " + mGold +
        "     Review : " + WAS.reviewEvent + "     Cost : " + WAS.Cost + "  in league : " + WAS.League);
        Ag.LogString (header + WAS.TeamName + "     etcInfo :: " + WAS.etcInfo);
        myRank.WAS.ShowMyself ();
        Ag.LogIntense (2, false);
        return true;
    }
    //  _////////////////////////////////////////////////_    _____   Array   _____   Parse Objects   _____
    public bool ParseUniform (JSONNode pData)
    {
        Ag.LogStartWithStr (1, " [< AmUserNet.cs >]  <<<<<   ParseUniform   >>>>>   >>  " + pData.Count);
        pData.ToString ().HtLog ();
        try {
            arrUniform.Clear ();
            for (int k = 0; k < pData.Count; k++) {
                AmUniform aObj = new AmUniform ();
                //if (aObj.ParseFrom (pData [k]))
                aObj.Parse (pData [k]);
                arrUniform.Add (aObj);
                //("  arrUniform Count :: " + arrUniform.Count).HtLog ();
            }
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse <<<<<   ParseUniform   >>>>>  E R R O R  :: ");
            return false;
        }

        ("  Final ...   arrUniform Count :: " + arrUniform.Count).HtLog ();
        return true;
    }

    public bool ParseCards (JSONNode pData)
    {
        Ag.LogStartWithStr (1, " [< AmUserNet.cs >]  <<<<<   ParseCards   >>>>>   >>  " + pData.Count);
        pData.ToString ().HtLog ();
        try {
            arrCard.Clear ();
            for (int k = 0; k < pData.Count; k++) {
                Ag.LogIntense (1, true);
                Ag.LogString (pData [k].ToString () + "   k  :  " + k);
                AmCard aObj = new AmCard ();

//                if (k == 0) { // 임 시 로 키 퍼 세 팅. 
//                    aObj.WAS.isKicker = false;
//                    //polyNum = 101;
//                } else
//                    aObj.WAS.isKicker = true;

                //if (aObj.WAS.ParseFrom (pData [k])) 
                aObj.WAS.WasCardParse (pData [k]);
                aObj.ScouterParse ();
                arrCard.Add (aObj);
            }
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse <<<<<   ParseCards   >>>>>  E R R O R  :: ");
            return false;
        }
        return true;
    }

    public bool ParseItem (JSONNode pData)
    {
        Ag.LogStartWithStr (1, " [< AmUserNet.cs >]  <<<<<   ParseItem   >>>>>   >>  " + pData.Count);
        pData.ToString ().HtLog ();
        arrItem.Clear ();
        try {
            for (int k = 0; k < pData.Count; k++) {
                AmItem aObj = new AmItem ();
                aObj.ParseFrom (pData [k]);
                //aObj.WAS = JsonMapper.ToObject<WasItem> (pData [k].ToJson ());
                aObj.WAS.ShowMyself ();
                arrItem.Add (aObj);
            }
            (" [< AmUserNet.cs >]  <<<<<   ParseItem   >>>>>   >>  arr.Count  " + arrItem.Count).HtLog ();

        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse <<<<<   ParseItem   >>>>>  E R R O R  :: ");
            return false;
        }
        return true;
    }

    public bool ParseCostume (JSONNode pData)
    {
        Ag.LogStartWithStr (1, " [< AmUserNet.cs >]  <<<<<   ParseCostume   >>>>>   >>  " + pData.Count);
        pData.ToString ().HtLog ();
        try {
            arrCostume.Clear ();
            for (int k = 0; k < pData.Count; k++) {
                AmCostume aObj = new AmCostume ();
                aObj.ParseFrom (pData [k]);
                aObj.ShowMySelf ();
                arrCostume.Add (aObj);
            }
        } catch {
            Ag.LogIntenseWord (" [< AmUserNet.cs >]       Parse <<<<<   ParseCostume   >>>>>  E R R O R  :: ");
            //Ag.LogString (pData.ToJson ());
            return false;
        }
        return true;
    }

    public void ShowCurrentCash ()
    {
        Ag.LogString (" Total Case : " + (mCash1 + mCash2).ToString () + "  Cash 1/2  :  " + mCash1 + "  /  " + mCash2 + "    Gold  :  " + mGold);
    }
}