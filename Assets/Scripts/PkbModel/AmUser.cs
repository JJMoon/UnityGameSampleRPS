// [2012:11:13:MOON] 
// [2013:11:25:MOON<AmUserWAS start>]
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  _////////////////////////////////////////////////_    _____   class   _____   AmUser    _____
public partial class AmUser : AmObject
{
    #region Member Variable Define..

    public UInt64 mFBookID;
    // Shop Related ..  My Info ..
    public int mGold, mCash1, mCash2, mRewardGold;
    // User Info
    public int mServerNum, mGoalUNO;
    //public byte mAgex, mStep, mBackNo, mItemCount, mEventItem, mEventGold, mEventTutorial, mLevel;
    //public bool mIsMale, mIsMyAdvantage;
    public AmTexture mShirt, mPants, mShoes, mSocks, mGlove, mGlShirt, mGlPants, mGlShoes, mGlSocks, mGlGlove;
    // Apply to All Players... Kickers, Keeper both...
    // Game Related Temp. Object
    public AmPlayer mCurPlayer;
    //  _////////////////////////////////////////////////_    _____   Deck related   ________
    public int DeckItemEA = 0;
    public bool DeckScouter = false;
    // Deprecate Target ..
    //public int mTempScr;    // during matching...
    //public int mModelUNsO;
    //  ////////////////////////////////////////////////     Lists
    // Array of players that I can scout..
    public List<int> arrTexture;
    //  ////////////////////////////////////////////////     C# Test ...   >>>> }}}}}
    public string DeviceID;

    #endregion

    public AmUser (bool NodeEnemy, string EDID)
    {
        DeviceID = EDID;
    }
    //  ////////////////////////////////////////////////     C# Test ...   >>>> }}}}}
    //  ////////////////////////////////////////////////     ////////////////////////     Public Functions...
    public AmUser (bool pVoid)
    {
        DeviceID = SystemInfo.deviceUniqueIdentifier;

        mCurPlayer = new AmPlayer ();

        arrTexture = new List<int> ();
    }

    public AmUser ()
    {
        DeviceID = SystemInfo.deviceUniqueIdentifier;
        Ag.LogString (" AmUser :: AmUser   ... Creation ");
        WAS.etcInfo = "";

        mCurPlayer = new AmPlayer ();
        arrTexture = new List<int> ();
    }

    #region Deck related

//    public void ApplyDeckItemBeforeGame ()
//    {
//        if (DeckItemEA < 1)
//            return;
//
//        List<AmCard> main = GetMainCards ();
//        for (int j = 0; j < main.Count; j++) {
//            main [j].WAS ApplyDeckItem (1, 1, 1, 1);
//        }
//    }

    public string mMaxCountry;

    public bool CanThisJoin2MainCards (AmCard pTarget, AmCard pAlter)
    {
        if (pTarget.WAS.playerID == pAlter.WAS.playerID)
            return true;

        Ag.LogString ("if (pTarget.WAS.playerID == pAlter.WAS.playerID)   pass  ....  " + pTarget.WAS.playerID +  "     "  + pAlter.WAS.playerID);

        for (int k = 0; k < 6; k++) {
            AmCard cur = GetCardOrderOf (k);
            Ag.LogString ("if (cur.WAS.playerID == pAlter.WAS.playerID)   Notpass  ....  " + cur.WAS.playerID +  "     "  + pAlter.WAS.playerID);
            if (cur.WAS.playerID == pAlter.WAS.playerID)
                return false;



        }
        Ag.LogString ("    Can this join 2 main cards ....     end ......   return true");
        return true;
    }
    public int SpecialCardNum = 0;
    public void ApplyCurrentDeck ()
    {
        Ag.LogDouble (" AmUser :: ApplyCurrentDeck   ...  ");

        // Get Current Deck Item Combi.
        List<AmCard> main = GetMainCards ();

        Ag.LogString (" AmUser :: ApplyCurrentDeck   ...    Main EA  ::  " + main.Count);

        // Set Dictionary
        Dictionary<string, int> cntryDic = new Dictionary<string, int> ();

        SpecialCardNum = 0;

        for (int j = 0; j < main.Count; j++) {
            //Ag.LogString ("         " + main [j].WAS.country);
            string country = main [j].WAS.country;
            // Special card  ...  not included in contryDic.. key..  limited to 2 cards in main ...
            if (main [j].IsSpecialCard) {
                SpecialCardNum++;
                continue;
            }
            if (cntryDic.ContainsKey (country))
                cntryDic [country]++;
            else
                cntryDic [country] = 1;
        }
        // Select Max Country
        string maxCntry = "";
        int maxNum = 0;
        foreach (KeyValuePair<string, int> cnry in cntryDic) {
            //Ag.LogString ("         " + cnry.Key + " : " + cnry.Value);
            if (maxCntry.Length == 0) {
                maxCntry = cnry.Key;
                maxNum = cnry.Value;
                continue;
            }
            if (maxNum < cnry.Value) {
                maxCntry = cnry.Key;
                maxNum = cnry.Value;
            }
        }
        //mMaxCountry = ""; // maxCntry;
        WAS.DeckItem = Tbl.dicDeckItem [maxCntry];

        //Ag.LogString (" AmUser :: ApplyCurrentDeck   ...    Max Country  " + maxCntry + "  .. " + maxNum);
        DeckItemEA = 0;
        for (int l = 0; l < arrCard.Count; l++) {
            arrCard [l].WAS.SetDeckItemValue (0, 0, 0, 0);
        }
        if (maxNum + SpecialCardNum == 6)
            maxNum = 6;
        // maxNum is 4, 5, 6  ==> 1, 2, 3
        if (maxNum < 4)
            return;
        mMaxCountry = maxCntry;
        DeckItemEA = maxNum - 3;
        Ag.LogString (" AmUser :: ApplyCurrentDeck   ...   Country :: " + mMaxCountry + "    apply Item EA ::  " + DeckItemEA);
        // Deck Item Percentage Calculation...
        int kickDir = 0, kickSkl = 0, keepBal = 0, keepSkl = 0;
        for (int k = 0; k < DeckItemEA; k++) {
            Ag.LogString ("  Appling Deck Item ::  >>>    " + WAS.DeckItem [k] + "  <<<<<  ");
            switch (WAS.DeckItem [k]) {
            case 2:
                kickDir += 5;
                break;
            case 3:
                kickSkl += 8;
                break;
            case 4:
                keepBal += 20;
                break;
            case 5:
                keepSkl += 8;
                break;
            case 6:
                DeckScouter = true;
                break;
            }
        }
        Ag.LogString (" AmUser :: ApplyCurrentDeck   ...    Deck Item Percentage ::  " + kickDir + " / " + kickSkl + " / " + keepBal + " / " + keepSkl);
        // Set Card ...
        for (int l = 0; l < arrCard.Count; l++) {
            arrCard [l].WAS.SetDeckItemValue (kickDir, kickSkl, keepBal, keepSkl);
        }
    }

    #endregion

    #region Condition Check Logic ... return bool  >>>>

    public bool CardContractLimitCheck ()
    {
        for (int i = 0; i < 6; i++) {
            //SumPlayerCost += Ag.mySelf.GetCardOrderOf (i).WAS.cost;
            try {
                if (Ag.mySelf.GetCardOrderOf (i).WAS.limitGameEA < 1)
                    return false;
            } catch {
                return false;
            }
        }
        return true;
    }

    public bool CardTotalCostCheck (out int totalCost)
    {
        totalCost = 0;
        for (int i = 0; i < 6; i++) {
            try {
                totalCost += Ag.mySelf.GetCardOrderOf (i).WAS.cost;
            } catch {
                Ag.LogIntenseWord ("  AmUser ::  CardTotalCostCheck   >>>    Card not found ...  kick order of  " + i);
            }
        }
        return totalCost <= Ag.mySelf.WAS.Cost;  // it's OK to play game..
    }

    #endregion

    public void SetKickOrder (int cardID, int kickOrder)
    {
        for (int j = 0; j < arrCard.Count; j++) {
            if (arrCard [j].WAS.ID == cardID)
                arrCard [j].KickOrder = kickOrder;
        }
    }
    //  ////////////////////////////////////////////////     Game Support Function
    public void CopyTextureFrom (AmUser pFrom)
    {
        mShirt.CopyFrom (pFrom.mShirt);
        mPants.CopyFrom (pFrom.mPants);
        mShoes.CopyFrom (pFrom.mShoes);
        mSocks.CopyFrom (pFrom.mSocks);
        mGlove.CopyFrom (pFrom.mGlove);
        mGlShirt.CopyFrom (pFrom.mGlShirt);
        mGlPants.CopyFrom (pFrom.mGlPants);
        mGlShoes.CopyFrom (pFrom.mGlShoes);
        mGlSocks.CopyFrom (pFrom.mGlSocks);
        mGlGlove.CopyFrom (pFrom.mGlGlove);
    }

    public void ShowMyself ()
    {
        if (!Ag.mIsDebug)
            return;
        Ag.LogStartWithStr (3, "AmUser :: ShowMyself >> ");
        Ag.LogString ("     Was Info  ::  ID " + WAS.KkoID + "   Nick : " + WAS.KkoNick + "  League : " + WAS.League);
        Ag.LogNewLine (3);
    }
    //  ////////////////////////////////////////////////     Set arrAvailable... from arrAllPlayer  ...PacketShop ...
    //  ////////////////////////////////////////////////     Texture Item Related...
    bool DoIhaveTextureUNO (int pUNO)
    {
        int num = arrTexture.Count;
        if (num == 0) {
            SetTextureArray ();
            num = arrTexture.Count;
        }
        for (int ij = 0; ij < num; ij++) {
            if (arrTexture [ij] == pUNO)
                return true;
        }
        return false;
    }

    void SetTextureArray ()
    {
        int num = arrItem.Count;
        for (int ij = 0; ij < num; ij++) {
            AmItem curObj = arrItem [ij];
            //if (20 <= curObj.mItemUNO && curObj.mItemUNO <= 40)
            //      arrTexture.Add (curObj.mItemUNO);
        }
    }
}
