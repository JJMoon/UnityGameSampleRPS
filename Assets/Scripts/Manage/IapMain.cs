using UnityEngine;
using System.Collections;
using System;
/*using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
 */
public class IapMain : MonoBehaviour {

    GameObject mLoadingBar;
    AmUI myGUI;
#if UNITY_IPHONE
	// Use this for initialization
	void Start () {
	
        myGUI = new AmUI(2, 16);
        
        mProductId = 0;
        
        //Ag.mIAP.ProductRequest();
        //Ag.mIAP.mFileIO.mDebugStr2 = "OK";
    
        
        //mLoadingBar = GameObject.Find("LodingBar").gameObject;
        //mLoadingBar.active = false;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if ( Ag.mIAP.mSendPackBool.mIsStarted ) {
            
            Ag.mIAP.mFileIO.mDebugStr1 = "Started ..";
            mLoadingBar.active = true;
            //mExit.active = false;
        } else {
            //Ag.LogIntenseWord("Not Started >>>>  ");
            Ag.mIAP.mFileIO.mDebugStr1 = "Not Started ..";
            mLoadingBar.active = false;
            //mExit.active = true;
        }
        */
         
	
	}
	
#endif
  
    
    
    int mRow, mCol, mProductId;
	void OnGUI()
    {
        int col = 0, row = 0;
        
        /*
        
         //Ag.mIAP.mDidProductReceive && mBtnFlag && !Ag.mIAP.mIsUiLocked;
       
        GUI.Label(  myGUI.GetRect(col, row++), Ag.mIAP.mDidProductReceive + ", " + Ag.mIAP.mIsUiLocked );
        
        GUI.Label(  myGUI.GetRect(col, row++), Ag.mIAP.mFileIO.mDebugStr1 );
            
        GUI.Label(  myGUI.GetRect(col, row++), Ag.mIAP.mFileIO.mDebugStr2 );
            //GUI.Label(  myGUI.GetRect(col, row++), Ag.net.mCounter + " <<  >> " + Ag.mIAP.mFileIO.mDebugStr3 );
        if( GUI.Button( myGUI.GetRect(col, row++), "Read User Info " ) ) {
            Ac.ReadUserInfo();
        }
        
        
        //if( GUI.Button( myGUI.GetRect(col, row++), "Purchase 1000" ) && Ag.mIAP.mDidProductReceive ) {         Ag.mIAP.PurchaseProduct("001000"); Ag.mIapPrice = 1000;          }
        
        col=1; row=0;
        
        if( GUI.Button( myGUI.GetRect(col, row++), "WriteBeforeSvrSendFile" ) ) { 
            Ag.mIapPrice = 12345;
            Ag.mIAP.mFileIO.WriteBeforeSvrSendFile(null); 
        }
        
        if( GUI.Button( myGUI.GetRect(col, row++), "CheckUnsentTransaction" ) ) { 
            Ag.mIAP.CheckUnsentTransaction();
        }
        
        
        
        //if( GUI.Button( myGUI.GetRect(col, row++), "Purchase 15000" ) ) {     Ag.mIAP.PurchaseProduct("015000");  Ag.mIapPrice = 15000;  }
        
        if( GUI.Button( myGUI.GetRect(col, row++), "Sample Packet Test" ) ) { 
            Ag.mIapPrice = 10000;
            Ag.mIAP.mFileIO.mReceipt = Ag.mReceiptSample;
            Ac.IAPinit();
        } 
        
        
        
        
        
        if( GUI.Button( myGUI.GetRect(col, row++), "WriteBeforeSvrSendFile 90000" ) ) {
            StoreKitTransaction tObj = new StoreKitTransaction();
            Ag.mIapPrice = 90000;
            tObj.productIdentifier = "com.appsgraphy.rpsonline.coin090000";
            tObj.base64EncodedTransactionReceipt = Ag.mReceiptSample;
            Ag.mIAP.mFileIO.WriteBeforeSvrSendFile(tObj );
        }
        
        
        if( GUI.Button( myGUI.GetRect(col, row++), "Read and Check Before~~.iap" ) ) {
            bool isUnsent = Ag.mIAP.mFileIO.CheckUnsentReceipt();
            Debug.Log(" Is There Unsent IAP ??  " + isUnsent );
        } //  
        
        
        
         if( GUI.Button( myGUI.GetRect(col, row++), " Set Product ID " ) ) {
            mProductId = (mProductId > 9)? 0: mProductId;
            Ag.mIAP.mFileIO.mDebugStr2 = Ag.mIAP.arrProduct[ mProductId++ ].productIdentifier;
            Ag.mIAP.mFileIO.mDebugStr2 += "  num: " + Ag.mIAP.arrProduct.Count;
        } // 
        
        
        //row++;
        
        if( GUI.Button( myGUI.GetRect(col, row++), "<<<  Return   >>" ) ) {
            
            Ag.mIAP.mSendPackBool.FinishAction();
            
            Application.LoadLevel("menu");
        }
        
        */
        
        
        //Convert.ToInt16("234");
        //Int32.Parse("123");

        
        
    }
	
}
