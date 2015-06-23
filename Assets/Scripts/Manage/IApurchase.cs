// [2012:10:29:MOON] IAP .. Reject ..
// [2012:11:13:MOON] Network Debugging

using UnityEngine;
using System.Collections;

public class IApurchase : MonoBehaviour
{
    GameObject mCoin, mExit, mLodingbar, mLodingR, mLodingbar2;
    bool mBtnFlag = true;
    AmUI myGUI;
    #if UNITY_IPHONE
    StateArray mSttArr;
    int mCount;
    // Use this for initialization
    void Start ()
    {
        AgStt.mIAP.ProductRequest ();
        mCoin = GameObject.Find ("Coin/Text_VALUE_COIN").gameObject.gameObject;
        mExit = GameObject.Find ("UI Root/Button").gameObject.gameObject;
        mLodingbar = GameObject.Find ("LodingBar2").gameObject;
        mLodingbar.active = false;
        mLodingbar2 = GameObject.Find ("LodingBar").gameObject;
        mLodingbar2.active = false;
        myGUI = new AmUI (2, 16);
        mCount = 1;
        
        SetStateArray ();  // [2012:10:29:MOON] IAP .. Reject ..
    }
    // Update is called once per frame
    void Update ()
    {
        
        mSttArr.DoAction ();  // [2012:10:29:MOON] IAP .. Reject ..
        
        mCount++;
        
        mCoin.GetComponent<TextMesh> ().text = Ag.mySelf.mGold.ToString ();
        
//        if (mCount > 900) { // Once in 30 sec...
//            if ( mSttArr.GetCurStateName() == "Rest")  {  AgStt.mIAP.CheckUnsentTransaction();  }
//            mCount = 1;
//        }
        
        if (mSttArr.GetCurStateName () == "InitPurchase") {
            mLodingbar2.active = true;
        } else {
            mLodingbar2.active = false;
        }
        
        //Ag.LogString("Is Started .. " + Ag.mIAP.mSendPackBool.mIsStarted );
    }

    void Purchase1000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("001000", 1000);
        }
    }

    void Purchase2200 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("002200", 2200);
        }
    }

    void Purchase3600 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("003600", 3600);
        }
    }

    void Purchase6500 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("06500", 6500);
        }
    }

    void Purchase9800 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("009800", 9800);
        }
    }

    void Purchase15000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("015000", 15000);
        }
    }

    void Purchase32000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("032000", 32000);
        }
    }

    void Purchase51000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("051000", 51000);
        }
    }

    void Purchase90000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("090000", 90000);
        }
    }

    void Purchase190000 ()
    {
        if (IsPurchasePossible ()) {
            Purchase ("190000", 190000);
        }
    }

    void OnGUI ()
    {
        /*int col = 0, row = 13;
        
        //Ag.mIAP.mDidProductReceive && mBtnFlag && !Ag.mIAP.mIsUiLocked;
        
        GUI.Label(  myGUI.GetRect(col, row++), Ag.mIAP.mDidProductReceive + ", " + Ag.mIAP.mIsUiLocked + ", " + mBtnFlag);
        
        */
    }
    //  ////////////////////////////////////////////////     Purchase
    bool IsPurchasePossible ()
    {
        return AgStt.mIAP.arrProduct.Count > 0 && mBtnFlag && Ag.mgServerLoggedIn;  // [2012:11:13:MOON] Network Debugging
    }
    //  ////////////////////////////////////////////////     Purchase
    void Purchase (string pStr, int pPrice)
    {
        
        if (mSttArr.GetCurStateName () != "Rest")
            return;
        
        mSttArr.SetStateWithNameOf ("InitPurchase");
        AgStt.mIAP.PurchaseProduct (pStr);
    }
    //  ////////////////////////////////////////////////     State Array Setting...
    void SetStateArray ()
    {
        mSttArr = new StateArray ();
        
        mSttArr.AddAMember ("InitPurchase", 5f);
        mSttArr.AddEntryAction (() => {
            mCount = 1;
        });
        mSttArr.AddExitCondition (() => {
            return true;
        }); // !AgStt.mIAP.mIsUiLocked; });
        mSttArr.AddTimeOutProcess (100f, () => {
            Application.LoadLevel ("menu");
        });
        
        mSttArr.AddAMember ("Purchasing", 3f);
//        mSttArr.AddExitCondition (() => {
//            //return !AgStt.mIAP.mSendPackBool.mIsStarted;
//        });
        mSttArr.AddExitAction (() => {
            mCount = 1;
        });
        mSttArr.AddTimeOutProcess (50f, () => {
            Application.LoadLevel ("menu");
        });
        
        //mSttArr.AddAMember ("CheckFile", 2f);
                
        mSttArr.AddAMember ("Rest", 0f);
        mSttArr.AddExitCondition (() => {
            return false;
        });
        
        mSttArr.SetSerialExitMember (pClose: false);
        mSttArr.SetStateWithNameOf ("Rest");
    }

    void ExitMenu ()
    {
        if (!mBtnFlag)
            return;
        
        mBtnFlag = false;
        Resources.UnloadUnusedAssets ();
            
        //AgStt.mIAP.mSendPackBool.FinishAction ();
        //Application.LoadLevel("StartMenu");
        Ag.mIsSuspendOnPurpose = false; // [2012:10:29:MOON] IAP .. Reject ..
        
        StartCoroutine (Start1 ());
    }

    IEnumerator Start1 ()
    {
        AsyncOperation async;
        async = Application.LoadLevelAsync ("StartMenu");
        //GameObject.Find("LodingBar2").GetComponent<LodingBar2>().mLodingActive = true;
        yield return async;
    }
    
    
    #endif
    #if UNITY_ANDROID
    public bool mCheckedBillingService = false;
    public string mProductId ="ProductID" ;
    
    public string mDebugStr1, mDebugStr2, mDebugStr3;
    //GameObject mCoin ,mExit;
    //bool mBtnFlag = true;
    
    //AmUI myGUI;
    
 // Use this for initialization
 void Start () {
        var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvPX/r/0ovyIfx5cPa45s+rJEJkEWAmna+zvkRpp+YU/mcXhUiXNBUDkivtQC6hwW6r9T4huggEulBIyH6CU9NjkxQX34MUEuyEHSAUjTByo56HmKbDyg6qcUqNzP20naYiWtYup12QOc5XmI+knTY9dviJAwDlAvQRbJHKYDWIZ5Wz0sFURnkcDMg3AFZN0DXgrUUYrvpJLeO3HnvunOEqi22t86oBmUyhF2jU+vi7p4mKcXaGhNmrFxreJmBfiFilrVdQclSXuuGSLEutv4QI+LTtutMC/Grp7zORiqonbFpjZh34vDl7jsqMWkguE3eH3mdT8DK5hzUJA3K/4tQIDAQAB";
        //IABAndroid.init( key );
        myGUI = new AmUI(2, 10);
        mDebugStr1 = "Present Situation";
        mDebugStr2 = "EventName";
        mDebugStr3 = Ag.mIsSuspendOnPurpose.ToString();
        mCoin = GameObject.Find("Coin/Text_VALUE_COIN").gameObject.gameObject;
        mExit = GameObject.Find("UI Root/Button").gameObject.gameObject;
        mLodingR = GameObject.Find("LodingBar").gameObject;
        mLodingR.active = false;
        mLodingbar = GameObject.Find("LodingBar2").gameObject;
        mLodingbar.active = false;
 }
 
 // Update is called once per frame
 void Update () {
       mCoin.GetComponent<TextMesh>().text = Ag.mySelf.mGold.ToString();
     
 }
    void OnGUI () {
     /*
        float yPos = 5.0f;
        float xPos = 5.0f;
        float width = ( Screen.width >= 800 || Screen.height >= 800 ) ? 320 : 160;
        float height = ( Screen.width >= 800 || Screen.height >= 800 ) ? 80 : 40;
        float heightPlus = height + 10.0f;
      */ 
       //----------test
       /*
        int col = 0, row = 1;
        
        GUI.Label(  myGUI.GetRect(col, row++), mCheckedBillingService.ToString() );
        
        
        // GUI.Label(new Rect( xPos, yPos, width, height ), mCheckedBillingService.ToString() );
        GUI.Label(myGUI.GetRect(col, row++),"billingService"+mCheckedBillingService.ToString() );
        GUI.Label(myGUI.GetRect(col, row++),"mBtnFlag"+mBtnFlag );
        GUI.Label(myGUI.GetRect(col, row++),"mFbstate"+Ag.mFBState );
        GUI.Label(myGUI.GetRect(col, row++),"ServerLogin"+Ag.mgServerLoggedIn );
        GUI.Label(myGUI.GetRect(col, row++),Ag.mySelf.mGold.ToString() );
        GUI.Label(myGUI.GetRect(col, row++),Ag.mDebugStr );
        if (GUI.Button (myGUI.GetRect(col, row++), "don"  )){
            AgStt.mIAPPrice = 1000;
            AgStt.mIAP.mFileIO.mReceipt = " This is Receipt ... Fuck you...   "; //Ag.mReceiptSample;
            Ac.IAPinit();
        }
        
        */
        
        /*
        if( GUI.Button( myGUI.GetRect(col, row++), "Initialize IAB" ) ){
             var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvPX/r/0ovyIfx5cPa45s+rJEJkEWAmna+zvkRpp+YU/mcXhUiXNBUDkivtQC6hwW6r9T4huggEulBIyH6CU9NjkxQX34MUEuyEHSAUjTByo56HmKbDyg6qcUqNzP20naYiWtYup12QOc5XmI+knTY9dviJAwDlAvQRbJHKYDWIZ5Wz0sFURnkcDMg3AFZN0DXgrUUYrvpJLeO3HnvunOEqi22t86oBmUyhF2jU+vi7p4mKcXaGhNmrFxreJmBfiFilrVdQclSXuuGSLEutv4QI+LTtutMC/Grp7zORiqonbFpjZh34vDl7jsqMWkguE3eH3mdT8DK5hzUJA3K/4tQIDAQAB";
             IABAndroid.init( key );
        }
        col = row = 1;
    
        if( GUI.Button( myGUI.GetRect(col, row++), "Test Purchase" ) ){
             IABAndroid.purchaseProduct( "android.test.purchased", "my magical payload of extra data" );
        }
        
     
        if( GUI.Button(myGUI.GetRect(col, row++), "Test Refund" ) ){
             IABAndroid.purchaseProduct( "android.test.refunded" );
        }
         
        
         
        if( GUI.Button( myGUI.GetRect(col, row++), "Test Unavailable Item" ) ){
             IABAndroid.purchaseProduct( "android.test.item_unavailable" );
        }
        
     
        if( GUI.Button( myGUI.GetRect(col, row++), "Restore Transactions" ) ){
             IABAndroid.restoreTransactions();
        }
        
        if( GUI.Button( myGUI.GetRect(col, row++), "Purchase Real Product" ) ){
            Ag.mIsSuspendOnPurpose = true;
            IABAndroid.purchaseProduct( "com.appsgraphy.psykickbattle.coin001000d" );
        }
        
        if( GUI.Button( myGUI.GetRect(col, row++), "Enable High Details Logs" ) ){
             IABAndroid.enableLogging( true );
             Debug.LogWarning( "YOU HAVE ENABLED HIGH DETAIL LOGS. DO NOT DISTRIBUTE THE GENERATED APK PUBLICLY. IT WILL DUMP SENSITIVE INFORMATION TO THE CONSOLE!" ); 
        }
        
        if( GUI.Button( myGUI.GetRect(col, row++), "Quit and Crash App (for testing)" ) ){
            IABAndroid.stopBillingService();
            Application.Quit();
        }
        */
        
    }

   
    void ExitMenu() {
        mBtnFlag = false;
        Resources.UnloadUnusedAssets();
            
        //AgStt.mIAP.mSendPackBool.FinishAction();
        //Application.LoadLevel("StartMenu");
        StartCoroutine(Start1 ());
    }
    
    IEnumerator Start1() {
        //IABAndroid.stopBillingService();
        AsyncOperation async;
        async = Application.LoadLevelAsync("StartMenu");
        //GameObject.Find("LodingBar2").GetComponent<LodingBar2>().mLodingActive = true;
        yield return async;
   }
    
    
    
    
   
    /*
   
     void OnEnable()
     {
         // Listen to all events for illustration purposes
         IABAndroidManager.billingSupportedEvent += billingSupportedEvent;
         IABAndroidManager.purchaseSignatureVerifiedEvent += purchaseSignatureVerifiedEvent;
         IABAndroidManager.purchaseSucceededEvent += purchaseSucceededEvent;
         IABAndroidManager.purchaseCancelledEvent += purchaseCancelledEvent;
         IABAndroidManager.confirmationFailedEvent += confirmationFailedEvent;
         IABAndroidManager.purchaseRefundedEvent += purchaseRefundedEvent;
         IABAndroidManager.purchaseFailedEvent += purchaseFailedEvent;
         IABAndroidManager.transactionsRestoredEvent += transactionsRestoredEvent;
         IABAndroidManager.transactionRestoreFailedEvent += transactionRestoreFailedEvent;
     }
    
    
     void OnDisable()
     {
         // Remove all event handlers
         IABAndroidManager.billingSupportedEvent -= billingSupportedEvent;
         IABAndroidManager.purchaseSignatureVerifiedEvent -= purchaseSignatureVerifiedEvent;
         IABAndroidManager.purchaseSucceededEvent -= purchaseSucceededEvent;
         IABAndroidManager.purchaseCancelledEvent -= purchaseCancelledEvent;
         IABAndroidManager.confirmationFailedEvent -= confirmationFailedEvent;
         IABAndroidManager.purchaseRefundedEvent -= purchaseRefundedEvent;
         IABAndroidManager.purchaseFailedEvent -= purchaseFailedEvent;
         IABAndroidManager.transactionsRestoredEvent -= transactionsRestoredEvent;
         IABAndroidManager.transactionRestoreFailedEvent -= transactionRestoreFailedEvent;
     }
    
    */
    
     void billingSupportedEvent( bool isSupported )
     {  
         mCheckedBillingService = isSupported;
         Debug.Log( "billingSupportedEvent: " + isSupported );
     }
     
     
     void purchaseSignatureVerifiedEvent( Hashtable payload )
     {
         var signedData = payload["signedData"].ToString();
         var signature = payload["signature"].ToString();
         
         Debug.Log( "purchaseSignatureVerifiedEvent. signedData: " + signedData + ", signature: " + signature );
     }
    
    
     
    void purchaseSucceededEvent( string productId, string developerPayload ) { 
        /*
        Debug.Log( "purchaseSucceededEvent: " + productId + ", payload: " + developerPayload );
        mDebugStr2 = "purchaseSucceededEvent";
        mProductId = productId;
        PurchaseServerSend(1000);
        */
        //PurchaseServerSend(AgStt.mIAPPrice , developerPayload);
    }
    
    //  ////////////////////////////////////////////////     Packet Work  ......
    

    void purchaseCancelledEvent( string productId, string developerPayload ) {
        /*
        Debug.Log( "purchaseCancelledEvent: " + productId + ", payload: " + developerPayload );
        mDebugStr2 = "purchaseCancelledEvent";
        mProductId = productId;
        PurchaseServerSend(1000);
        */
        //PurchaseServerSend(AgStt.mIAPPrice);
        
    }
     
    void confirmationFailedEvent( string productId, string developerPayload ) {
        Debug.Log( "confirmationFailedEvent: " + productId + ", payload: " + developerPayload );
    }
    
    
    void purchaseRefundedEvent( string productId, string developerPayload ) {
        Debug.Log( "purchaseRefundedEvent: " + productId + ", payload: " + developerPayload );
    }
     
    void purchaseFailedEvent( string productId, string developerPayload ) {
        Debug.Log( "purchaseFailedEvent: " + productId + ", payload: " + developerPayload );
    }
     
    void transactionsRestoredEvent() {
        Debug.Log( "transactionsRestoredEvent" );
    }
    
    
    void transactionRestoreFailedEvent( string error ) {
        Debug.Log( "transactionRestoreFailedEvent: " + error );
    }
    
    void PurchaseServerSend( int pPrice ,string payload) {
        mDebugStr1 = "Purchase Started ";
        //AgStt.mIAP.mFileIO.mReceipt = payload; //Ag.mReceiptSample;
        mDebugStr1 = "Purchase int ";
        //AgStt.mIAPPrice = pPrice;
        mDebugStr1 = "Purchase Before ";

        mDebugStr1 = "Purchase Done  ";
    }
    

    string[] lines = {"First line", "Second line", "Third line"};

#endif
}
