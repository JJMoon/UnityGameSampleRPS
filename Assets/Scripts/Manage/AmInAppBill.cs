// [2012:10:29:MOON] IAP .. Reject ..
// [2012:11:25:MOON] Coin Double Action
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAB  _____  Android  _____
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAB  _____  Android  _____
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAB  _____  Android  _____
#if UNITY_ANDROID
public class AmFileIO  {
    
    public string mDocuPath, mDebugStr1, mDebugStr2, mDebugStr3;
    
}

public class AmIAPfileIO : AmFileIO {
    
    public string mReceipt, mProductID;
    public bool mIsSent2Svr, mIsApply2Svr;
    public int mKeyValue4Pack;
    public List<string> arrReceipt;
    
    public AmIAPfileIO() : base () {
        
    }
    
    public void DivideReceipt() {
        int num = mReceipt.Length / 800 + 1, strLen = 800, ttlLen = mReceipt.Length; // 4.. 3060 ..
        arrReceipt = new List<string>();
        
        //Debug.Log("ttlLen  " + ttlLen + ",  num  " + num );
        for (int k=0; k<num; k++) {
            int staIdx = k * 800;
            strLen = (ttlLen > 800)? 800: ttlLen;
            arrReceipt.Add(mReceipt.Substring( staIdx, strLen ));
            //Debug.Log("Receipt >> " + staIdx + ", Len " + strLen + ", >>> " + mReceipt.Substring( staIdx, strLen ) );
            ttlLen -= strLen;
        }
    }
    
}

#endif


public class AmInAppBill  {
    #if UNITY_ANDROID
    //public AmIAPfileIO mFileIO;
    public int Amount, StoreType;

    public string TransactionKey, Receipt, BuyCode, ProductID, Signature ;

    public bool mIsBeforeUndoneIAP, mDidProductReceive;
    
    //public AgBoolObj mSendPackBool;
    
    public AmInAppBill() {
        //mIsBeforeUndoneIAP = false;
        //mFileIO = new AmIAPfileIO();
        //mSendPackBool = new AgBoolObj();
    }
    #endif
}
