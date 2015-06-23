using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase {
    WasEvent mPriceSaleList, mItemSaleList, mWasevent;
    void SaleItemSetting () {

        mPriceSaleList = new WasEvent();
        mItemSaleList = new WasEvent();
        for (int i = 0; i < Ag.mySelf.arrEvent.Count; i++) {
            if (Ag.mySelf.arrEvent[i].eventType == "salePriceItem") {
                mPriceSaleList = Ag.mySelf.arrEvent[i];
            }
            if (Ag.mySelf.arrEvent[i].eventType == "saleItem") {
                mItemSaleList = Ag.mySelf.arrEvent[i];
            }
        }
    }

    /*
    bool ItemEventOnCheck (string EventTypeid) {
        bool Check = false;
        for (int i = 0; i < mItemSaleList.arrReward.Count; i++) {
            if(EventTypeid == mItemSaleList.arrReward[i].code) 
                Check = true;
        }
        return Check;
    }
    */

    string GiftRewardCode () {
        string CheckTypeid = "BlueDrink5";
        for (int i = 0; i < Ag.mySelf.arrEvent.Count; i++) {
            if("gift" == Ag.mySelf.arrEvent[i].eventType) 
                CheckTypeid = Ag.mySelf.arrEvent[i].arrReward[0].value;
        }
        return CheckTypeid;
    }


    bool PriceEventOnCheck (string EventTypeid) {

        bool Check = false;
        for (int i = 0; i < mPriceSaleList.arrReward.Count; i++) {
            if(EventTypeid == mItemSaleList.arrReward[i].code) 
                Check = true;
        }
        return Check;
    }




}
