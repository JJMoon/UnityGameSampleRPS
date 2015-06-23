using UnityEngine;
using System.Collections;



public class AmIABdroid : MonoBehaviour {
     #if UNITY_ANDROID
    public bool mCheckedBillingService = false;
    public string mProductId ="ProductID" ;
    
    public string mDebugStr1, mDebugStr2, mDebugStr3;
    GameObject mCoin ,mExit;
    bool mBtnFlag = true;
    
    AmUI myGUI;
    
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
       
        int col = 0, row = 1;
        
        GUI.Label(  myGUI.GetRect(col, row++), mCheckedBillingService.ToString() );
        
        
        // GUI.Label(new Rect( xPos, yPos, width, height ), mCheckedBillingService.ToString() );
        GUI.Label(myGUI.GetRect(col, row++),"billingService"+mCheckedBillingService.ToString() );
        GUI.Label(myGUI.GetRect(col, row++),"mBtnFlag"+mBtnFlag );
        //GUI.Label(myGUI.GetRect(col, row++),"mFbstate"+Ag.mFBState );
        GUI.Label(myGUI.GetRect(col, row++),"ServerLogin"+Ag.mgServerLoggedIn );
        GUI.Label(myGUI.GetRect(col, row++),Ag.mySelf.mGold.ToString() );
        //GUI.Label(myGUI.GetRect(col, row++),Ag.mDebugStr );
        if (GUI.Button (myGUI.GetRect(col, row++), "don"  )){
            //Ag.mIapPrice = 1000;
            //Ag.mIAP.mFileIO.mReceipt = " This is Receipt ... Fuck you...   "; //Ag.mReceiptSample;

        }
        
        
        
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

        }
        */
        
    }


    
        
    void ExitMenu() {
        mBtnFlag = false;
        Resources.UnloadUnusedAssets();
            
        //Ag.mIAP.mSendPackBool.FinishAction();
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
        //PurchaseServerSend(Ag.mIapPrice , developerPayload);
    }
    
    //  ////////////////////////////////////////////////     Packet Work  ......
    

    void purchaseCancelledEvent( string productId, string developerPayload ) {
        /*
        Debug.Log( "purchaseCancelledEvent: " + productId + ", payload: " + developerPayload );
        mDebugStr2 = "purchaseCancelledEvent";
        mProductId = productId;
        PurchaseServerSend(1000);
        */
        //PurchaseServerSend(Ag.mIapPrice);
        
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
        AgStt.mIapPrice = pPrice;
        mDebugStr1 = "Purchase Before ";

        mDebugStr1 = "Purchase Done  ";
    }
    

    string[] lines = {"First line", "Second line", "Third line"};

#endif

}
