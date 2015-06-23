// [2013:11:27:MOON<AmUserWAS start>]
using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

public class AmUserWAS
{
    public string KkoID, KkoNick, WasKey, TeamName, KkoPW, League, etcInfo, GameSessionKey, profileURL, freeCouponDate;
    public int Country, expireRemainDate, Cost, rank1, rank2, rank3;
    public int[] DeckItem = new int[] { 0, 0, 0 };
    /// <summary>
    /// 리뷰 확인 0: 아직 안함, 1: 함..
    /// </summary>
    public int reviewEvent;
    //public bool pushAllow;
    public AmUserWAS ()
    {
        KkoID = KkoNick = WasKey = TeamName = KkoPW = League = etcInfo = GameSessionKey = profileURL = freeCouponDate = "-";
        KkoID = "";
        TeamName = KkoNick = "--";
        League = "PRO_5";
    }
}
//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  AmUser  <<<<<
public partial class AmUser : AmObject
{
    public AmUserWAS WAS = new AmUserWAS ();
    private WasUserEtcInfo etcInfoObj = new WasUserEtcInfo ();
    public List<AmUniform> arrUniform = new List<AmUniform> ();
    public List<AmCostume> arrCostume = new List<AmCostume> ();
    public List<AmCard> arrCard = new List<AmCard> ();
    public List<AmItem> arrItem = new List<AmItem> ();
    public List<AmMail> arrMail = new List<AmMail> ();
    public List<AmCard> arrNewCard = new List<AmCard> ();
    public List<WasRank> arrFriendRank = new List<WasRank> ();
    public List<WasItemPriceObj> arrItemPrice = new List<WasItemPriceObj> ();
    public List<WasEvent> arrEvent = new List<WasEvent> ();
    public List<AmPopupProd> arrPopupItem = new List<AmPopupProd> ();
    public List<AmPopupProdIAP> arrPopupIAPItem = new List<AmPopupProdIAP> ();
    public Rank myRank = new Rank ();
    //public List<AmMail> arrMail = new List<AmMail> ();
    public bool DidProcessFirstGame { get { return etcInfoObj.FGame == 1; } }

    public bool DidNotReviewYet { get { return WAS.reviewEvent == 0; } }

    public int ContLoseNum { get { return etcInfoObj.ContLoseNum; } set { etcInfoObj.ContLoseNum = value; } }

    public int ExperieceSCard { get { return etcInfoObj.ExperienceScard; } }
    // ClientVersion = 1
    public int ServerVer, loginCount, InviteCount, Isinvite;
    public DateTime  TimeEventEnd = new DateTime ();
    public string noticeImageUrl, KkoNickEncode, TeamNameEncoded;

    public List<AmCard> GetUpdateCards ()
    {
        List<AmCard> rCrd = new List<AmCard> ();
        foreach (AmCard tCd in arrCard) {
            if (tCd.mustUpdate)
                rCrd.Add (tCd);
        }
        //Ag.LogString (" AmUserNet :: GetUpdateCards   >>>    " + rCrd.Count);
        return rCrd;
    }

    bool IsKickOrderOK ()
    {
        int mainPlayerNum = 0;
        AgBoolList kickOrder = new AgBoolList (6, false);
        bool keeperSet = false;
        foreach (AmCard aCard in arrCard) {
            if (aCard.KickOrder >= 0 && aCard.KickOrder <= 5) {
                kickOrder.SetValueAt (aCard.KickOrder, true);
                mainPlayerNum++;
            }
            if (aCard.KickOrder > 5)
                return false; // Error .. 
            if (aCard.WAS.kickOrder == 0) {
                if (keeperSet)
                    aCard.WAS.KickOrder = -1;  // Duplicate Main Keeper ....  Just Set  ...  
                keeperSet = true;
            }
        }

        Ag.LogString (" IsKickOrderOK :: mainPlayerNum :: " + mainPlayerNum, pWichtig: mainPlayerNum != 6);

        //if (kickNum != 5 || !keeperSet)
        if (kickOrder.AllValuesAre (true) && mainPlayerNum == 6)
            return true;
        return false;
    }
    //  _////////////////////////////////////////////////_    _____   EtcInfoObj   _____   출첵 보일것인가   _____
    public int TodaysGameEA { get { return etcInfoObj.GameNumToday; } }

    public bool ShowDailyEvent {
        get { 
            DateTime dtNow = DateTime.Now;
            return !(etcInfoObj.DailyChkMon == dtNow.Month && etcInfoObj.DailyChkDay == dtNow.Day);
        }
    }

    public bool ShowHeartPopup {
        get { 
            return  etcInfoObj.GameNumToday > 14 && etcInfoObj.HeartPopup == 0;
        }
    }

    public bool Show3ContLosePopup {
        get { return etcInfoObj.ContLoseNum == 3; }
    }

    public void CheckFirstDailyEventToday ()  // 출 첵 // Game Number of Today ... Initialize ...
    {
        Ag.LogIntenseWord ("  FirstDailyEventToday  " + etcInfoObj.DailyChkMon + " / " + etcInfoObj.DailyChkDay);
        //etcInfoObj.ShowMyself ();
        DateTime dtNow = DateTime.Now;
        etcInfoObj.DailyChkMon = dtNow.Month;
        etcInfoObj.DailyChkDay = dtNow.Day;
        etcInfoObj.GameNumToday = 0;
        etcInfoObj.ExperienceScard = 0;
        UpdateEtcInfoObj ("CheckFirstDailyEventToday");
    }

    /// <summary>
    /// I already saw the Popup.
    /// [ Heart discount event ]
    /// </summary>
    public void CheckPopupHeartToday ()
    {
        etcInfoObj.HeartPopup = 1;
        UpdateEtcInfoObj ("CheckPopupHeartToday");
    }

    public void PopupHeartInit ()
    {
        etcInfoObj.HeartPopup = 0;
        UpdateEtcInfoObj ("PopupHeartInit");
    }

    public int GetApplyIDofItem (string id)
    {
        int ApplyID = 0;

        for (int i = 0; i < arrItem.Count; i++) {
            if (arrItem [i].WAS.itemTypeID == id) {
                ApplyID = arrItem [i].WAS.applyID;
            }
        }
        return ApplyID;
    }

    bool Endmessage = false;

    public bool GetEndMessage (string id)
    {
        Endmessage = false;
        for (int i = 0; i < arrItem.Count; i++) {
            if (arrItem [i].WAS.itemTypeID == id) {
                Endmessage = true;
            }
        }
        return Endmessage;
    }
    /*
    public KakaoFriends.Friend GetMyKakaoFriend()
    {
        KakaoFriends.Friend myObj = new KakaoFriends.Friend();
        myObj.profileImageUrl = WAS.profileURL;
        myObj.nickname = WAS.KkoNick;
        myObj.messageBlocked = true;
    }
    */
    public void DidWinOrLoseGame (bool didWin)  // 연 패
    { // 연패 기록 위한 작업.
        etcInfoObj.ShowMyself ();

		IncreseGameNumber ();

        etcInfoObj.GameNumToday++;
        if (didWin) {
            if (etcInfoObj.ContLoseNum > 0) { // 연패 저지
                etcInfoObj.ContLoseNum = 0;
                UpdateEtcInfoObj ("DidWinOrLoseGame if");
            } 
        } else {
            etcInfoObj.ContLoseNum++;
            UpdateEtcInfoObj ("DidWinOrLoseGame else");
        }
        Ag.LogString ("  AmUser :: DidWinOrLoseGame  >>> Win ? " + didWin);
        etcInfoObj.ShowMyself ();
    }

    private void UpdateEtcInfoObj (string caller)  // User Update Call ....  <<<  private  >>>
    {
        WAS.etcInfo = etcInfoObj.ToJsonStr (); //JsonMapper.ToJson (etcInfoObj);

        Ag.LogStartWithStr (3, " UpdateEtcInfoObj  From >>>>   " + caller);
        Ag.LogString ( WAS.etcInfo);

        WasUserUpdate aObj = new WasUserUpdate () { User = this, etcInfo = WAS.etcInfo.DodgeJson () };
        aObj.messageAction = (int pInt) => {
            aObj = null;
        };
    }

    public void TEST_ResetDailyEvent ()  // Debug ...
    {
        etcInfoObj.DailyChkMon = etcInfoObj.DailyChkDay = 0;
        UpdateEtcInfoObj ("TEST_ResetDailyEvent");
    }
    //  _////////////////////////////////////////////////_    _____   Node   _____   노드 전달 위한 변환   _____
    public string ToNodeAmUserStr ()
    {
        string SendStr = "";
        SendStr = SendStr.AddKV3 ("KkoID", WAS.KkoID, "KkoNick", WAS.KkoNick, "KNickEncode", KkoNickEncode, false);
        SendStr = SendStr.AddParen ();
        return SendStr;
        //string strArr = ""; // 0 일 때 에러 안나게
//        if (arrUniform.Count == 0) {
//            SendStr = SendStr.AddArray ("arrWasUniform", "[]");
//        } else {
//            for (int k = 0; k < arrUniform.Count; k++) {
//                string curStr = "";
//                curStr = arrUniform [k].WAS.ToJsonStr ();
//                strArr += curStr;
//                if (k != (arrUniform.Count - 1))
//                    strArr += ",";
//            }
//            strArr = strArr.AddSqreBrakt ();
//            SendStr = SendStr.AddArray ("arrWasUniform", strArr, false);
//        }

//       
//
//        strArr = ""; // 0 일 때 에러 안나게
//        if (arrCard.Count == 0) {
//            SendStr = SendStr.AddArray ("arrCard", "[]");
//        } else {
//            for (int k = 0; k < arrCard.Count; k++) {
//                string curStr = "";
//                curStr = arrCard [k].WAS.ToJsonStr ();
//                strArr += curStr;
//                if (k != (arrCard.Count - 1))
//                    strArr += ",";
//            }
//            strArr = strArr.AddSqreBrakt ();
//            SendStr = SendStr.AddArray ("arrCard", strArr);
//        }
//
//        strArr = ""; // 0 일 때 에러 안나게
//        if (arrCostume.Count == 0) {
//            SendStr = SendStr.AddArray ("arrCostumeWas", "[]", false);
//        } else {
//            for (int k = 0; k < arrCostume.Count; k++) {
//                string curStr = "";
//                curStr = arrCostume [k].WAS.ToJsonStr ();
//                strArr += curStr;
//                if (k != (arrCostume.Count - 1))
//                    strArr += ",";
//            }
//            strArr = strArr.AddSqreBrakt ();
//            SendStr = SendStr.AddArray ("arrCostumeWas", strArr, false);
//        }


    }

    public NodeAmUser ToNodeAmUser ()
    {
        string kkoNick = WAS.KkoNick == null || WAS.KkoNick.Length == 0 ? "No name" : WAS.KkoNick;
        KkoNickEncode = KkoNickEncode == null || KkoNickEncode.Length == 0 ? "No name" : KkoNickEncode;

        NodeAmUser nUsr = new NodeAmUser () {
            KkoID = WAS.KkoID, KkoNick = kkoNick, KNickEncode = KkoNickEncode, TeamNameEncoded = TeamNameEncoded, contWin = (ContWinCoolTimeRemainPercent() > 0)? "TRY": "NOT"
        };

//        if (Ag.NodeObj.IsRandom) 
//            nUsr.TeamName = nUsr.TeamNameEncoded = nUsr.KNickEncode = nUsr.KkoNick = "-";

        foreach (AmUniform uO in arrUniform) {
            nUsr.arrWasUniform.Add (uO.WAS);
        }
        foreach (AmItem itm in arrItem) {
            nUsr.arrItemWas.Add (itm.WAS);
        }
        foreach (AmCard aCard in arrCard) {
            if (aCard.WAS.kickOrder >= 0) {
                if (aCard.WAS.direction == null)
                    aCard.WAS.SetZeroToDirectSkill ();
                nUsr.arrCard.Add (aCard.WAS);
            }
        }
        foreach (AmCostume aObj in arrCostume) {
            nUsr.arrCostumeWas.Add (aObj.WAS);
        }
        // nUsr.arrCostume.AddRange (arrCostume);
        ("AmUserNet :: ToNodeAmUser ::      arrCard : " + nUsr.arrCard.Count + " ,   arrWasUniform : " + nUsr.arrWasUniform.Count).HtLog ();
        return nUsr;
    }
    //  _////////////////////////////////////////////////_    _____   Get   _____   GetCards   _____
    public int GetBuyType (string pTypeID)
    {
        for (int k = 0; k < arrItemPrice.Count; k++) {
            if (arrItemPrice [k].itemTypeID == pTypeID)
                return arrItemPrice [k].BuyType01 ();
        }
        Ag.LogIntenseWord ("  AmUserNet :: GetBuyType  >>>   No Item Price .....       Error     ....." + pTypeID);
        return 2; // Return Gold Case ....  this is Error ..
    }

    public int GetCurCostUpLevel ()
    {
        int baseCost = 0;
        switch (WAS.League) {
        case "PRO_5":
            baseCost = 19;
            break;
        case "PRO_4":
            baseCost = 20;
            break;
        case "PRO_3":
            baseCost = 21;
            break;
        case "PRO_2":
            baseCost = 22;
            break;
        case "PRO_1":
            baseCost = 23;
            break;
        }
        ("AmUserNet :: GetCurCostUpLevel ::      league : " + WAS.League + " ,   baseCost : " + baseCost + WAS.Cost.LogWith ("Cur Cost")).HtLog ();
        return WAS.Cost - baseCost + 1;
    }

    public AmCard GetCardOrderOf (int pOdr)
    { // 킥 오더로 가져옴.
        return arrCard.GetMemberWithCond ((AmCard crd) => {
            return crd.WAS.kickOrder == pOdr;
        });
    }

    public AmCard GetCardIdOf (int pID)
    { // 카드를 아이디로 가져옴.
        return arrCard.GetMemberWithCond ((AmCard crd) => {
            return crd.WAS.ID == pID;
        });
    }

    public List<AmCard> GetMainCards ()
    { // 주전 카드를 리스트로 리턴함. 
        List<AmCard> rCard = new List<AmCard> ();
        for (int k = 0; k < arrCard.Count; k++) {
            if (arrCard [k].WAS.kickOrder >= 0)
                rCard.Add (arrCard [k]);
        }
        return rCard;
    }

    public int[] GetMainCardIDs ()
    { // 주전 카드의 아이디리스트를 리턴함. 
        int n = GetMainCards ().Count, idx = 0;
        int[] rCard = new int[n];
        for (int k = 0; k < arrCard.Count; k++) {
            if (arrCard [k].WAS.kickOrder >= 0)
                rCard [idx++] = arrCard [k].WAS.ID; 
        }
        return rCard;
    }
    //    bool CheckKeeper()
    //    {
    //        int cnt = 0;
    //        for (int k = 0; k < arrCard.Count; k++) {
    //            if (!arrCard [k].WAS.isKicker && arrCard [k].WAS.kickOrder == 0) {
    //                cnt++;
    //            }
    //        }
    //        return cnt == 1;
    //    }
    public void CheckKickOrder ()
    { // kick Order 가 틀리면 전체 다시 세팅. UI를 통해 수정 가능 하므로 OK. 
        Ag.LogStartWithStr (2, "   AmUserNet ::  CheckKickOrder   Is All New Card ??    _ _ _ _ _ _ _ _ _ _ _ _ ");
        if (IsKickOrderOK ())
            return;
        Ag.LogString ("   AmUserNet ::  Kick Order is N O T   Valid ___   Setting Start    _ _ _ _ _ _ _ _ _ _ _ _ ");
        int kickOrder = 1;
        bool isKeeperSet = false;
        foreach (AmCard aCard in arrCard) {
            if (aCard.WAS.isKicker) { // Kicker Setting 
                if (kickOrder <= 5) // Limit to 5 .. no 6, 7 ...
                    aCard.WAS.KickOrder = kickOrder++;
                else
                    aCard.WAS.KickOrder = -1;
            } else { // Keeper Setting
                if (isKeeperSet)
                    aCard.WAS.KickOrder = -1;
                else {
                    aCard.WAS.KickOrder = 0;
                    isKeeperSet = true;
                }
            }
        }
        Ag.LogString ("  AmUserNet ::  CheckKickOrder    _ _ _ _ _ _ _ _ _ _ _ _ Proceded  _ _ _ _ _ _ _ _ _ _ _ _ _ _ ");
    }
    //  _////////////////////////////////////////////////_    _____   Costume   _____   Util   _____
    /// <summary>
    /// 카드가 갖고 있는 코스츔을 카드 내의 어레이에 저장.
    /// </summary>
    public void SetCostumeToCard ()
    {
        //Ag.LogString ("  SetCostumeToCard ()  카드가 갖고 있는 코스츔을 카드 내의 어레이에 저장. ");
        ClearAllarrCostumeInCard (); // 일단 어레이 초기화.
        for (int k = 0; k < arrCostume.Count; k++) {
            if (arrCostume [k].WAS.cardId > 0) {
                AmCard theCard = GetCardIdOf (arrCostume [k].WAS.cardId);
                if (theCard != null)
                    theCard.arrCostumeInCard.Add (arrCostume [k]); // 채우기
            }
        }
        Ag.LogString ("  SetCostumeToCard ()  End ..... >>>>    ");
    }

    void ClearAllarrCostumeInCard ()
    { // 모든 카드 안의 arrCostumeInCard 어레이를 클리어 함.
        for (int k = 0; k < arrCard.Count; k++) {
            arrCard [k].arrCostumeInCard.Clear ();
        }
    }

    public bool ShouldUpdateCard ()
    {
        foreach (AmCard aCard in arrCard) {
            if (aCard.mustUpdate)
                return true;
        }
        return false;
    }
}
 