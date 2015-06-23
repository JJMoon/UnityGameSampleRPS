// [2012:10:29:MOON] IAP .. Reject ..
// [2012:11:25:MOON] Coin Double Action
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAP  _____  iOS  _____
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAP  _____  iOS  _____
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  IAP  _____  iOS  _____
#if UNITY_IPHONE
public class AmIAP
{
    public List<StoreKitProduct> arrProduct;
    public int mPackNum, ProdNumFromAppl;
    //public bool InTheProcess = false;
    // OnApplicationPause @ NetworkManagerMono.cs
    public string ProductID, TransactionID, ReceiptOfIAP = "";
    public AmUser TheUser;
    //public Action ActPurchaseSuccess, ActPurchaseCancelled;
    public AmIAP ()
    {
        arrProduct = new List<StoreKitProduct> ();

        StoreKitManager.productListReceivedEvent += allProducts => { // 상품 목록 받는 부분.
            Ag.LogString ("received total products: " + allProducts.Count);
            ProdNumFromAppl = allProducts.Count;
            arrProduct = allProducts;
            for (int k = 0; k < arrProduct.Count; k++) {
                (" Product : " + arrProduct [k].productIdentifier + "      Prc : " + arrProduct [k].price).HtLog ();
            }
            Ag.LogDouble (" StoreKitManager.productListReceivedEvent ::   Done !!! ");
        };

        StoreKitManager.purchaseSuccessfulEvent += tObject => { // StoreKitTransaction Object..
            Ag.LogString ("Receipt >>> " + tObject.base64EncodedTransactionReceipt.Substring (0, 10), pWichtig: true);

            AgStt.IntendedPause = false; // 초 기 화  
            ReceiptOfIAP = tObject.base64EncodedTransactionReceipt;
            TransactionID = tObject.transactionIdentifier;
            if (ReceiptOfIAP.Length > 100) { // 구매 성공.
                WasInAppPrchs aObj = new WasInAppPrchs () {
                    User = TheUser //, ActPurchaseSuccess = this.ActPurchaseSuccess, ActPurchaseCancelled = this.ActPurchaseCancelled
                };
                aObj.messageAction = (int pInt) => {
                    switch (pInt) { // 
                    case 0:
                        WasUserInfo uObj = new WasUserInfo () { User = TheUser, flag = 0 };
                        uObj.messageAction = (int uInt) => {
                            Ag.LogString (" OK ");
                        };
                        break;
                    }
                };
                Ag.LogString (" purchaseSuccessfulEvent  WasInAppPrcs call.. ", pWichtig: true);
            }
        };

        // public static event Action<string> purchaseCancelledEvent;
        StoreKitManager.purchaseCancelledEvent += tStr => {
            AgStt.IntendedPause = false; // 초 기 화  

            EventPurchaseCancelled ();
            Debug.Log ("purchase Canceled >>_ " + tStr);
        };
    }
    //  _////////////////////////////////////////////////_    _____   Action   _____    상품 리스트 가져오기    _____
    public void ProductRequest ()
    {
        Ag.LogIntenseWord ("Product Request ");
        var productIdentifiers = new string[] {
            "com.appsgraphy.kvsskakao.cash0030_test",
            "com.appsgraphy.kvsskakao.cash0050_test",
            "com.appsgraphy.kvsskakao.cash0100_test",
            "com.appsgraphy.kvsskakao.cash0330_test",
            "com.appsgraphy.kvsskakao.cash0600_test",
            "com.appsgraphy.kvsskakao.cash1300_test",
            "com.appsgraphy.kvsskakao.popupstore01_test",
            "com.appsgraphy.kvsskakao.popupstore02_test",
            "com.appsgraphy.kvsskakao.popupstore03_test",
            "com.appsgraphy.kvsskakao.popupstore04_test",
            "com.appsgraphy.kvsskakao.popupstore05_test",
            "com.appsgraphy.kvsskakao.popupstore06_test",
            "com.appsgraphy.kvsskakao.popupstore07_test",
            "com.appsgraphy.kvsskakao.popupstore08_test"

//            "com.appsgraphy.psykickbattlekakao.cash0500",
//            "com.appsgraphy.psykickbattlekakao.cash1000",
//            "com.appsgraphy.psykickbattlekakao.cash1000",
//            "com.appsgraphy.psykickbattlekakao.cash1000"
        };
        StoreKitBinding.requestProductData (productIdentifiers); 
    }
    //  _////////////////////////////////////////////////_    _____  Action  _____    구 매   _____
    public void PurchaseProduct (string pProductID)
    {
        Ag.LogStartWithStr (3, "Purchase Product >>>    " + pProductID + "   From Apple Count : " + ProdNumFromAppl);

        AgStt.IntendedPause = true; // 구매 진행 시작. Node close 방지. 
        ReceiptOfIAP = "";
        //ProductID = "com.appsgraphy.psykickbattlekakao." + pProductID; // cash0500 .. cash0030..
        ProductID = pProductID;
        StoreKitBinding.purchaseProduct (productIdentifier: ProductID, quantity: 1);
        Ag.LogString ("      Sent  ...    " + ProductID);
    }
    //  _////////////////////////////////////////////////_    _____  Get  _____    StoreKit Product   _____
    public StoreKitProduct GetProduct (int pPrice)
    {
        int k, num = arrProduct.Count;
        for (k = 0; k < num; k++) {
            char[] tChar = pPrice.ToString ().ToCharArray ();
            int idx = arrProduct [k].productIdentifier.IndexOfAny (tChar);
            if (idx > 0)
                return arrProduct [k];
        }
        return null;
    }
    //  _////////////////////////////////////////////////_    _____  Get  _____    Possible   _____
    public bool CanMakePayment ()
    {
        return StoreKitBinding.canMakePayments ();
    }

    public void EventPurchaseCancelled ()
    {
        Ag.LogIntenseWord ("EventPurchaseCancelled ");

    }
}

#endif