using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IABEventListener : MonoBehaviour
{
    #if UNITY_ANDROID

    private static AndroidJavaObject AndroidPlugin = null;
    // TSTORE
    //private static AndroidJavaObject TStoreBridgeInstance = null;
    // NSTORE
    private static AndroidJavaObject NStoreBridgeInstance = null;

    void Awake ()
    {
        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidPlugin = jc.GetStatic<AndroidJavaObject> ("currentActivity");				
		
    }

    void Start ()
    {	


        string strPublicKey = "CPYR390541402470173413::peMioFqa9l::MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDB8c8uIm96gnasfuFsOSBIDQXuKKbXqQkLsn4cL68SnTBqYSd6er2E03lzT+mseJbWAKHbyCR87wT5dol+UhJsFRuSf/MsgsgKhKrb9d7NKE4Uch6zFmQ62aVVk9dspt28pY5wjiJtScrSvtRb/1iZ6nHQCJlUw0OsTbjOSsricQIDAQAB";		
        switch (3) {
        case 1:
    				// TSTORE
            if (AndroidPlugin != null) {					
                AndroidPlugin.Call ("initializeTStore", strPublicKey);					
            } else {
                Debug.Log ("Error!");
            }
            break;
        case 3:
    				// NSTORE
            if (AndroidPlugin != null) {					
                AndroidPlugin.Call ("initializeNStore", strPublicKey);					
            } else {
                Debug.Log ("Error!");
            }
            break;
        default:
            Debug.Log ("Error!");
            break;
        }

		
    }


    void OnDestroy ()
    {
        if (AndroidPlugin != null)
            AndroidPlugin.Dispose ();
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            AndroidPlugin.Call ("PressBackButton");
        }
    }

    void OnEnable ()
    {
		
    }

    void OnDisable ()
    {
		
    }

    void GetCash (string pBuycode)
    {

        AgStt.mIAB.ProductID = GameObject.Find ("Axis/Camera/Match").GetComponent<MenuManager> ().mProductCode;
        AgStt.mIAB.BuyCode = pBuycode;
        AgStt.mIAB.StoreType = 3;
        WasInAppPrchs aObj = new WasInAppPrchs () {
            User = Ag.mySelf, 
        };

        aObj.messageAction = (int pInt) => {
            switch (pInt) { 
            case 0:
                GameObject.Find ("Axis/Camera/Match").GetComponent<MenuManager> ().MainUserInfo ();
                return;
            }
        };

    }

    void OnGUI ()
    {
        /*
        if (GUI.Button (new Rect (100,100,100,100),"BuyItem")) {
            CallPaymentNRequest ("1000007371",1000,"Extra");
        }
        */
    }

    /// 1. TSTORE 처리함수
    //	// 안드로이드 라이브러리의 결제 요청 메소드 호출
    //	public static void CallPaymentTRequest( string strProductID, string strDeveloperPayload ){
    //		int retCode = getTStoreInstance().Call<int>("PaymentRequest", strProductID, strDeveloperPayload);
    //	}
	
    // TSTORE 인스턴스가 없으면 GET
    //	public static AndroidJavaObject getTStoreInstance(){
    //		if(TStoreBridgeInstance == null){
    //			TStoreBridgeInstance = AndroidPlugin.GetStatic<AndroidJavaObject>("mTStoreBridgeInstance");
    //		}
    //		return TStoreBridgeInstance;
    //	}
    //			
    //	public void onPurchaseTSuccess(string txid){
    //		// 결제 성공시 콜백
    //		string signData = 
    //			TStoreBridgeInstance.Get<String>("strSignData1") + 
    //			TStoreBridgeInstance.Get<String>("strSignData2") +
    //			TStoreBridgeInstance.Get<String>("strSignData3") +
    //			TStoreBridgeInstance.Get<String>("strSignData4") +
    //			TStoreBridgeInstance.Get<String>("strSignData5") +
    //			TStoreBridgeInstance.Get<String>("strSignData6");
    //		// 서버측검증요청 : txid, signData 정보 보냄		
    //	}		
	
    public void onPurchaseTComplete (string message)
    {
        // 결제 완료시 콜백
        Debug.Log ("Callback onPurchaseComplete");
    }

    public void onPurchaseTFail (string message)
    {
        // 결제 실패시 콜백
        Debug.Log ("Callback  onPurchaseFail");
        // 실패처리
    }

    /// 3. NSTORE 처리함수		
    public static void CallPaymentNRequest (string strProductID, int productPrice, string strDeveloperPayload)
    {
        getNStoreInstance ().Call ("PaymentRequest", strProductID, productPrice, strDeveloperPayload);
    }
    // NSTORE 인스턴스가 없으면 GET
    public static AndroidJavaObject getNStoreInstance ()
    {
        if (NStoreBridgeInstance == null) {
            NStoreBridgeInstance = AndroidPlugin.GetStatic<AndroidJavaObject> ("mNStoreBridgeInstance");
        }
        return NStoreBridgeInstance;
    }

    public void onPurchaseNComplete (string paymentSeq)
    {

        Debug.Log ("Nstore Reciet    ::      " + paymentSeq);
        GetCash (paymentSeq);
        // 결제 성공시 콜백
        // 서버측검증요청 : paymentSeq 정보 보냄
    }

    public void onPurchaseNCancel (string message)
    {
        // 결제 취소시 콜백
        // 취소처리
    }

    public void onPurchaseNFail (string message)
    {
        // 결제 실패시 콜백
        // 실패처리
    }
    #endif
}