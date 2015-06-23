//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Text;

public partial class MenuManager : AmSceneBase
{
    public int BuyGold, BuyCash;
    public string Productid;

    /// <summary>
    /// Point
    /// </summary>
    /// 
    void InitInAppPurchaseIOSnADR ()
    {
#if UNITY_ANDROID
        var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzTm3Kt267irxsdP52FN/drPBS/CD8+RGyH7DRQhE6d3UZKrm1FObbzSEfS6tm//h2oeLbb5iyBv8sVWd3E9sR6vhPYLkUPbTPlYR1OvuipHOqvN+J7YYQVdUFiqMkST+t3DuXt58Zu+vmmYfgwwooXtoh1yrTzCJAGCT93J9oX1zJcMpzcLQPstJ8WwO8hRDicTnhXzYNznXvbVsy7FtgWJcSqRhp+FJ37+4e0TcpDxyDc6T95Tk6BPBl05Z/jVp8HNODTDNw8MJCgp6qsGhF6/yQ9GYj7qUDElpyYO/BiQ/XViodAFNBk2/qc10LPkrkxCj+d8el5IBXihC+Vp4HQIDAQAB";
        GoogleIAB.init (key);
#endif
#if UNITY_IPHONE
        AgStt.mIAP.ProductRequest ();
#endif
    }

    void Btn_LPanel_Shop_CoseBtn_OK ()
    {
        dicMenuList ["LPanel_shop"].SetActive (false);
        MenuCommonOpen ("Ui_popup", "havenotpoint", false);
        MenuCommonOpen ("Ui_popup", "havenotcash", false);
    }

    public void Btn_Fun_Point3000 ()
    {
        BuyCash = 10;
        BuyGold = 1000;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "1,000";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold1000").ToString ();
    }

    public void Btn_Fun_Point16500 ()
    {
        BuyCash = 50;
        BuyGold = 5500;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "5,500";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold5500").ToString ();
    }

    public void Btn_Fun_Point36000 ()
    {
        BuyCash = 100;
        BuyGold = 12000;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "12,000";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold12000").ToString ();
    }

    public void Btn_Fun_Point117000 ()
    {
        BuyCash = 300;
        BuyGold = 39000;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "39,000";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold39000").ToString ();
    }

    public void Btn_Fun_Point210000 ()
    {
        BuyCash = 500;
        BuyGold = 70000;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "70,000";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold70000").ToString ();
    }

    public void Btn_Fun_Point450000 ()
    {
        BuyCash = 1000;
        BuyGold = 150000;
        dicMenuList ["popup_buypoint"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_num", true).GetComponent<UILabel> ().text = "150,000";//BuyGold.ToString ();
        mRscrcMan.FindChild (dicMenuList ["popup_buypoint"], "Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice ("Gold150000").ToString ();
    }

    /// <summary>
    /// Cash
    /// </summary>

    void SetPopupPriceLabel (string numStr, string numberPrice, string labelText)
    {
        mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_num", true).GetComponent<UILabel> ().text = numStr;
#if UNITY_IPHONE
        mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_", true).GetComponent<UILabel> ().text = "$";
        mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_price", true).GetComponent<UILabel> ().text = numberPrice + "$";
#endif
#if UNITY_ANDROID
        mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_", true).GetComponent<UILabel> ().text = WWW.UnEscapeURL("%5C");
        mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_price", true).GetComponent<UILabel> ().text = labelText;    

        GoogleIABManager.purchaseSucceededEvent += PurchaseSuccessed;
#endif
    }

    public void Btn_Fun_Cash10 ()
    {
        Productid = "com.appsgraphy.kvsskakao.cash0030";
        mProductCode = "1000007357";
        mPaymentPrice = 3000;

        dicMenuList ["popup_buycash"].SetActive (true);

        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        GetTranjectionkey ();
        SetPopupPriceLabel ("30", "2.99", "3,000");
        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash10   >>>  ProductID " + Productid);
    }

    public void Btn_Fun_Cash55 ()
    {

        mProductCode = "1000007358";
        mPaymentPrice = 5000;

        Productid = "com.appsgraphy.kvsskakao.cash0050";
        dicMenuList ["popup_buycash"].SetActive (true);

        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        GetTranjectionkey ();
        SetPopupPriceLabel ("50", "4.99", "5,000");
        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash55   >>>  ProductID " + Productid);
    }

    public void Btn_Fun_Cash120 ()
    {
        Productid = "com.appsgraphy.kvsskakao.cash0100";
        dicMenuList ["popup_buycash"].SetActive (true);
        GetTranjectionkey ();

        mProductCode = "1000007359";
        mPaymentPrice = 10000;

        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        SetPopupPriceLabel ("100", "9.99", "10,000");
        //mRscrcMan.FindChild (dicMenuList ["popup_buycash"], "Label_num", true).GetComponent<UILabel> ().text = "100";


        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash120   >>>  ProductID " + Productid);
    }

    public void Btn_Fun_Cash390 ()
    {
        Productid = "com.appsgraphy.kvsskakao.cash0330";
        dicMenuList ["popup_buycash"].SetActive (true);

        GetTranjectionkey ();
        mProductCode = "1000007360";
        mPaymentPrice = 30000;


        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        SetPopupPriceLabel ("330", "29.99", "30,000");
        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash390   >>>  ProductID " + Productid);
        
    }

    public void Btn_Fun_Cash700 ()
    {
        Productid = "com.appsgraphy.kvsskakao.cash0600";
        dicMenuList ["popup_buycash"].SetActive (true);
        GetTranjectionkey ();
        mProductCode = "1000007362";
        mPaymentPrice = 50000;


        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        SetPopupPriceLabel ("600", "49.99", "50,000");
        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash700   >>>  ProductID " + Productid);
        
    }

    public void Btn_Fun_Cash1500 ()
    {
        Productid = "com.appsgraphy.kvsskakao.cash1300";
        dicMenuList ["popup_buycash"].SetActive (true);
        GetTranjectionkey ();
        mProductCode = "1000007363";
        mPaymentPrice = 100000;


        #if UNITY_ANDROID
        GoogleIAB.consumeProduct (Productid);
        #endif

        SetPopupPriceLabel ("1300", "99.99", "100,000");
        Ag.LogString (" MenuManager_Shop ::   Btn_Fun_Cash1500   >>>  ProductID " + Productid);
    }

    /// <summary>
    /// PopUpClose BuyOK
    /// </summary>
    int James;

    void Btn_Fun_CashBuyOk ()
    {
        dicMenuList ["popup_buycash"].SetActive (false);
        
        #if UNITY_IPHONE
        AgStt.mIAP.TheUser = Ag.mySelf;
        AgStt.mIAP.PurchaseProduct (Productid);
        #endif

        #if UNITY_ANDROID
        AgStt.IntendedPause = true;

        if (Ag.CurStorePlfm == StorePlfm.GooglePlay) {
            GoogleIAB.purchaseProduct (Productid,GetUniqueKey(20));
        }
        if (Ag.CurStorePlfm == StorePlfm.Nstore) {
            IABEventListener.CallPaymentNRequest(mProductCode, mPaymentPrice, GetUniqueKey(20));
        }
        #endif



    }

    public static string GetUniqueKey (int length)
    {
        string guidResult = string.Empty;
        
        while (guidResult.Length < length) {
            // Get the GUID.
            guidResult += System.Guid.NewGuid ().ToString ().GetHashCode ().ToString ("x");
        }
        
        // Make sure length is valid.
        if (length <= 0 || length > guidResult.Length)
            Debug.Log ("GetLenth");
        //throw new ArgumentException("Length must be between 1 and " + guidResult.Length);
        
        // Return the first length bytes.
        
        Debug.Log (guidResult.Substring (0, length) + "GetUnique key");
        return guidResult.Substring (0, length);
    }
    #if UNITY_ANDROID
    
    public void PurchaseSuccessed (GooglePurchase purchase)
    {
        GoogleIAB.consumeProduct (Productid);
        AgStt.mIAB.ProductID = Productid;
        AgStt.mIAB.Signature = purchase.signature;
        AgStt.mIAB.BuyCode = purchase.orderId;
        //AgStt.mIAB.Receipt = '"'+purchase.originalJson.Replace("\"","\\\"")+'"';
        AgStt.mIAB.Receipt = purchase.originalJson.Replace("\"","\\\"");
        AgStt.mIAB.StoreType = 2;

        Debug.Log (AgStt.mIAB.ProductID);
        Debug.Log (AgStt.mIAB.Signature);
        Debug.Log (AgStt.mIAB.BuyCode + "BuyCode");
        Debug.Log (AgStt.mIAB.TransactionKey);
        Debug.Log (AgStt.mIAB.Receipt);
        GetCash();


    }
    #endif
    void Btn_Fun_CashBuyCancel ()
    {
        dicMenuList ["popup_buycash"].SetActive (false);
        #if UNITY_ANDROID
        //GoogleIAB.consumeProduct (Productid);
        #endif
    }

    void GetPurchaseData (string pData)
    {
#if UNITY_ANDROID
        //AgStt.mIAB.Receipt = pData;
        //pData = pData.DodgeBackSlashQuoMark();
        //JSONNode nodeObj = JSON.Parse (pData);
        //AgStt.mIAB.Receipt = nodeObj["originalJson"];
        Debug.Log (" MenuManager_Shop ::  Receipt >>  " + AgStt.mIAB.Receipt);
#endif
    }

    void GetPurchaseBuyCode (string pData)
    {
        #if UNITY_ANDROID
        AgStt.mIAB.BuyCode = pData;
        Debug.Log (" MenuManager_Shop ::  Receipt >>  " + AgStt.mIAB.BuyCode);
        #endif
    }

    void GetTranjectionkey ()
    {
        Ag.LogString (" MenuManager_Shop ::   GetTranjectionkey   >>>  ProductID " + Productid);
#if UNITY_ANDROID    
        WasGoogleIABillingKey aObj = new WasGoogleIABillingKey ()  {
            User = Ag.mySelf, cashTypeID = Productid
        };

        aObj.messageAction = (int pInt) => {
            switch (pInt) { 
            case 0:
                Ag.LogString (" result : Success ");
                return;
            }
        };
#endif
    }

    void GetCash ()
    {
        WasInAppPrchs aObj = new WasInAppPrchs () {
            User = Ag.mySelf, 
        };

        aObj.messageAction = (int pInt) => {
            switch (pInt) { 
            case 0:
                MainUserInfo ();

                Ag.LogString (" result : Success ");

                #if UNITY_ANDROID
                GoogleIAB.consumeProduct (Productid);
                #endif

                return;
            }
        };
    }
    /*
    void PurchaseGlove (int EaNum)
    {
        WasPurchaseHeart aObj = new WasPurchaseHeart () { User = Ag.mySelf, eaN = EaNum };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공
            case 0:
                Ag.mySelf.HeartSetMax ();
                //if (EaNum == 5) 

                break;
            case -1:
                Debug.Log ("NO MONEY");
                MenuCommonOpen ("Ui_popup", "havenotcash", true);
                break;

            }
        };
    }
    */
//    private void xxonUpdateUserComplete ()
//    {
//        Debug.Log ("onUpdateUserComplete");
//
//        // You must call the KakaoNativeExtension::loadGameUserInfo method
//        KakaoNativeExtension.Instance.loadGameUserInfo (this.onGameUserInfoComplete, this.onGameUserInfoError);
//    }

    private void onUpdateUserError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    private void onGameUserInfoComplete ()
    {
        string alertMessage = "";
        string user_id = StcPlatform.MyUserID; // KakaoGameUserInfo.Instance.user_id;
        string nickname = StcPlatform.NickName; // KakaoGameUserInfo.Instance.nickname;
        string profile_image_url = StcPlatform.ProfileImageUrl; // = KakaoGameUserInfo.Instance.profile_image_url;
        string message_blocked = StcPlatform.IsMessageBloked ? "true" : "false";
        string exp = StcPlatform.Exp;
        string publicData = StcPlatform.PublicData;
        string privateData = StcPlatform.PrivateData;
        string message_count = StcPlatform.MessageCount;

        if (user_id != null && user_id.Length > 0) {
            alertMessage += "user_id : ";
            alertMessage += user_id;
            alertMessage += "\n";
        }
        if (nickname != null && nickname.Length > 0) {
            alertMessage += "nickname : ";
            alertMessage += nickname;
            alertMessage += "\n";
        }
        if (profile_image_url != null && profile_image_url.Length > 0) {
            alertMessage += "profile_image_url : ";
            alertMessage += profile_image_url;
            alertMessage += "\n";
        }
        if (exp != null && exp.Length > 0) {
            alertMessage += "exp : ";
            alertMessage += exp;
            alertMessage += "\n";
        }

        if (message_blocked != null && message_blocked.Length > 0) {
            alertMessage += "message_blocked : ";
            alertMessage += message_blocked;
            alertMessage += "\n";
        }

        if (publicData != null && publicData.Length > 0) {
            alertMessage += "publicData : ";
            alertMessage += publicData;
            alertMessage += "\n";
        }
        if (privateData != null && privateData.Length > 0) {
            alertMessage += "privateData : ";
            alertMessage += privateData;
            alertMessage += "\n";
        }
        if (message_count != null && message_count.Length > 0) {
            alertMessage += "message_count : ";
            alertMessage += message_count;
            alertMessage += "\n";
        }
    }

    private void onGameUserInfoError (string status, string message)
    {
        showAlertErrorMessage (status, message);
    }

    void Btn_Fun_GloveBuyCancel ()
    {
        dicMenuList ["popup_buyglove"].SetActive (false);
    }

    void Btn_Fun_PointBuyOk ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);
        dicMenuList ["popup_buypoint"].SetActive (false);
        WasPurchaseGold aObj = new WasPurchaseGold () { User = Ag.mySelf, Gold = BuyGold };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                //PopupAfterUserCash();
                Ag.LogString (" result : Success ");
                return;
            case -1:
                //MenuCommonOpen ("Ui_Popup","havenotplayball",false);
                MenuCommonOpen ("Ui_popup", "havenotcash", true);
                Ag.LogIntenseWord (" result : Cash Not enough ");

                return;
            case 1:
                Ag.LogIntenseWord (" result : Wrong Unit ");
                return;
            }
        };

    }

    void Btn_Fun_PointBuyCancel ()
    {
        dicMenuList ["popup_buypoint"].SetActive (false);
    }

    /// <summary>
    /// Glove
    /// </summary>
    /// 
    /// 
    void BuyHeart ()
    {

    }

    void HeartBuy ()
    {
    }

    string GloveTypeId;
    bool GloveFree;
    int GloveNum;

    void SetGloveLabelNumberAndPrice (int gloveNum, string labelNum, string labelPrice)
    {
        GloveNum = gloveNum;
        dicMenuList ["popup_buyglove"].SetActive (true);
        mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1", true);
        mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/Label_num", true).GetComponent<UILabel> ().text = labelNum;
        mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/Label_price", true).GetComponent<UILabel> ().text = GetRealBuyPrice (labelPrice).ToString ();
    }

    void SetGloveLabel1andLabel2 (string name, string name2)
    {
        mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/Label_content", true).GetComponent<UILabel> ().text = name;
        mRscrcMan.FindChild (dicMenuList ["popup_buyglove"], "popup_buyglove1/Label_content2", true).GetComponent<UILabel> ().text = name2;

    }

    public void Btn_Fun_Glove5 ()
    {
        GloveFree = false;
        GloveTypeId = "FuncHeartMax";
        SetGloveLabelNumberAndPrice (5, WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%EC%A6%89%EC%8B%9C%ED%9A%8C%EB%B3%B5"), "FuncHeartMax");
        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%EC%9D%84%20%EC%A6%89%EC%8B%9C%20%ED%9A%8C%EB%B3%B5%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EA%B9%8C%3F"), WWW.UnEscapeURL (""));
    }

    public void Btn_Fun_Glove11 ()
    {
        GloveFree = false;
        GloveTypeId = "HeartSpeedUp";
        SetGloveLabelNumberAndPrice (11, WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%ED%9A%8C%EB%B3%B5%EC%86%8D%EB%8F%84%EC%A6%9D%EA%B0%80"), "HeartSpeedUp");

        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%ED%9A%8C%EB%B3%B5%EC%86%8D%EB%8F%84%20%EC%A6%9D%EA%B0%80%20%EA%B8%B0%EB%8A%A5%EC%9D%84%20%EA%B5%AC%EB%A7%A4%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EB%8B%88%EA%B9%8C%3F"), WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%ED%9A%8C%EB%B3%B5%EC%86%8D%EB%8F%84%EA%B0%80%202%EB%B0%B0%20%EC%A6%9D%EA%B0%80%ED%95%A9%EB%8B%88%EB%8B%A4.%201%ED%9A%8C%20%EA%B5%AC%EB%A7%A4%EB%A1%9C%20%EC%98%81%EA%B5%AC%20%EC%A0%81%EC%9A%A9%EB%90%A9%EB%8B%88%EB%8B%A4."));
    }

    public void Btn_Fun_Glove24 ()
    {
        GloveFree = false;
        GloveTypeId = "HeartLimitUp";
        SetGloveLabelNumberAndPrice (24, WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%EB%9F%89%EC%A6%9D%EA%B0%80"), "HeartLimitUp");
        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%EB%9F%89%EC%A6%9D%EA%B0%80%20%EA%B8%B0%EB%8A%A5%EC%9D%84%20%EA%B5%AC%EB%A7%A4%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EB%8B%88%EA%B9%8C%3F"), WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%EB%9F%89%EC%9D%B4%202%EB%B0%B0%20%EC%A6%9D%EA%B0%80%ED%95%A9%EB%8B%88%EB%8B%A4.%201%ED%9A%8C%20%EA%B5%AC%EB%A7%A4%EB%A1%9C%20%EC%98%81%EA%B5%AC%20%EC%A0%81%EC%9A%A9%EB%90%A9%EB%8B%88%EB%8B%A4."));
    }

    public void Btn_Fun_OneDayFree ()
    {
        GloveFree = true;
        GloveTypeId = "GloveFreeDay";
        SetGloveLabelNumberAndPrice (0, WWW.UnEscapeURL ("24%EC%8B%9C%EA%B0%84"), "GloveFreeDay");
        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("24%EC%8B%9C%EA%B0%84%20%EC%B2%B4%EB%A0%A5%EC%9C%A0%EC%A7%80%20%EA%B8%B0%EB%8A%A5%EC%9D%84%20%EA%B5%AC%EB%A7%A4%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EB%8B%88%EA%B9%8C%3F"), WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%20%EC%86%8C%EB%AA%A8%20%EC%97%86%EC%9D%B4%2024%EC%8B%9C%EA%B0%84%EB%8F%99%EC%95%88%20%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%EC%A6%90%EA%B8%B8%20%EC%88%98%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4."));
    }

    public void Btn_Fun_OneWeekFree ()
    {
        GloveFree = true;
        GloveTypeId = "GloveFreeWeek";
        SetGloveLabelNumberAndPrice (0, WWW.UnEscapeURL ("7%EC%9D%BC"), "GloveFreeWeek");
        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("7%EC%9D%BC%20%EC%B2%B4%EB%A0%A5%EC%9C%A0%EC%A7%80%20%EA%B8%B0%EB%8A%A5%EC%9D%84%20%EA%B5%AC%EB%A7%A4%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EB%8B%88%EA%B9%8C%3F"), WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%20%EC%86%8C%EB%AA%A8%20%EC%97%86%EC%9D%B4%207%EC%9D%BC%EB%8F%99%EC%95%88%20%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%EC%A6%90%EA%B8%B8%20%EC%88%98%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4."));
    }

    public void Btn_Fun_OneMonthFree ()
    {
        GloveFree = true;
        GloveTypeId = "GloveFreeMonth";
        SetGloveLabelNumberAndPrice (0, WWW.UnEscapeURL ("30%EC%9D%BC"), "GloveFreeMonth");
        SetGloveLabel1andLabel2 (WWW.UnEscapeURL ("30%EC%9D%BC%20%EC%B2%B4%EB%A0%A5%EC%9C%A0%EC%A7%80%20%EA%B8%B0%EB%8A%A5%EC%9D%84%20%EA%B5%AC%EB%A7%A4%ED%95%98%EC%8B%9C%EA%B2%A0%EC%8A%B5%EB%8B%88%EA%B9%8C%3F"), WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5%20%EC%86%8C%EB%AA%A8%20%EC%97%86%EC%9D%B4%2030%EC%9D%BC%EB%8F%99%EC%95%88%20%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%EC%A6%90%EA%B8%B8%20%EC%88%98%20%EC%9E%88%EC%8A%B5%EB%8B%88%EB%8B%A4."));
    }

    /// <summary>
    /// CardMixItem
    /// </summary>

    string CombiItemName;
    string CombiItemNameUrl, CombiItemPrice;

    public void Btn_Fun_MixItem1 ()
    {
        BuyType = 0;
        CombiItemName = "CardCombiGrade";
        CombiItemNameUrl = "%EC%B5%9C%EA%B3%A0%20%EB%93%B1%EA%B8%89%20%EB%B3%B4%EC%A1%B4%EA%B6%8C";
        CombiItemPrice = GetRealBuyPrice ("CardCombiGrade").ToString ();
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/mixitem/itemlist/item01", true).GetComponent<UICheckbox> ().Set (true);
    }

    public void Btn_Fun_MixItem2 ()
    {
        BuyType = 0;
        CombiItemName = "CardCombiAdvtHigh";
        CombiItemNameUrl = "%EA%B3%A0%EA%B8%89%20%ED%96%89%EC%9A%B4%EA%B6%8C";
        CombiItemPrice = GetRealBuyPrice ("CardCombiAdvtHigh").ToString ();
    }

    public void Btn_Fun_MixItem3 ()
    {
        BuyType = 1;
        CombiItemName = "CardCombiAdvt";
        CombiItemNameUrl = "%EC%98%81%EC%9E%85%20%ED%96%89%EC%9A%B4%EA%B6%8C";
        CombiItemPrice = GetRealBuyPrice ("CardCombiAdvt").ToString ();
    }

    public void Btn_Fun_MixItem_Buy ()
    {

        MenuCommonOpen ("Ui_Popup", "buy_item2");
        dicMenuList ["buy_item2"].transform.FindChild ("Label_item").GetComponent<UILabel> ().text = WWW.UnEscapeURL (CombiItemNameUrl);
        dicMenuList ["buy_item2"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = CombiItemPrice;
        if (CombiItemName == "CardCombiAdvt") {
            dicMenuList ["buy_item2"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
            dicMenuList ["buy_item2"].transform.FindChild ("icon_cash").gameObject.SetActive (false);
        } else {
            dicMenuList ["buy_item2"].transform.FindChild ("icon_coin").gameObject.SetActive (false);
            dicMenuList ["buy_item2"].transform.FindChild ("icon_cash").gameObject.SetActive (true);
        }

    }

    public void Btn_Fun_MixItem_BuyCancel ()
    {
        MenuCommonOpen ("Ui_Popup", "buy_item2", false);
    }

    public void Btn_Fun_MixItem_BuyOK ()
    {

        dicMenuList ["CenterCircle"].SetActive (true);
        WasPurchaseItem aObj = new WasPurchaseItem () {

            User = Ag.mySelf,
            itemType = "CombiAdvt",
            itemTypeId = CombiItemName, //"FuncBackNumEdit : ",
            ea = 1,
            //buyType = BuyType
        };
        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);

            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                ItemInfo ();
                //dicMenuList ["alert"].SetActive (true);
                MenuCommonOpen ("Ui_Popup", "buy_item", false);
                Ag.LogString (" result : Success ");
                break;
            case -1:
                Debug.Log ("MixItem");
                if (CombiItemName == "CardCombiGrade")
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                if (CombiItemName == "CardCombiAdvtHigh")
                    MenuCommonOpen ("Ui_popup", "havenotcash", true);
                if (CombiItemName == "CardCombiAdvt")
                    MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
            // Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
        };
        if (CombiItemName == "CardCombiGrade" || CombiItemName == "CardCombiAdvtHigh") 
            //PopupAfterUserCash();
        MenuCommonOpen ("Ui_Popup", "buy_item2", false);
    }

    /// <summary>
    /// SoccerShoose
    /// </summary>


     

    /// <summary>
    /// ItemShop Buy Card
    /// </summary>
    /// 
    /// 
    /// 
    /// 
   
    public void MenuCommonOpen (string pName, string pName2)
    {
        dicMenuList [pName].SetActive (true);
        dicMenuList [pName2].SetActive (true);
    }

    public void MenuCommonOpen (string pName, string pName2, bool pActive)
    {
        dicMenuList [pName].SetActive (pActive);
        dicMenuList [pName2].SetActive (pActive);
    }

    /// <summary>
    /// SoccerShoose
    /// </summary>
    /// 
    string CostumeName;
    /*
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes01").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes02").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes03").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/shoes/shoeslist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KickerShoes04").gold.ToString ();
    
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves01").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves02").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves03").gold.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/glove/glovelist/grid/item04/Label_price").GetComponent<UILabel> ().text = ItemPrice ("KeeperGloves04").gold.ToString ();
    
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item01/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiGrade").cash.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item02/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvtHigh").cash.ToString ();
    dicMenuList ["Ui_lobby"].transform.FindChild ("LPanel_itemshop/mixitem/itemlist/item03/Label_price").GetComponent<UILabel> ().text = ItemPrice ("CardCombiAdvt").gold.ToString ();
    */
    public void Btn_Fun_SoccerShose1 ()
    {
        CostumeName = "%ED%8F%AC%EC%84%B8%EC%9D%B4%EB%8F%88";
        Costume = "KickerShoes01";
        CostumePrice = GetRealBuyPrice ("KickerShoes01").ToString ();
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/shoes/shoeslist/grid/item01", true).GetComponent<UICheckbox> ().Set (true);
    }

    string CostumePrice;

    public void Btn_Fun_SoccerShose2 ()
    {
        CostumeName = "%ED%95%98%EB%8D%B0%EC%8A%A4";
        CostumePrice = GetRealBuyPrice ("KickerShoes02").ToString ();
        Costume = "KickerShoes02";
    }

    public void Btn_Fun_SoccerShose3 ()
    {
        CostumeName = "%EC%95%84%EB%A0%88%EC%8A%A4";
        CostumePrice = GetRealBuyPrice ("KickerShoes03").ToString ();
        Costume = "KickerShoes03";
    }

    public void Btn_Fun_SoccerShose4 ()
    {
        CostumeName = "%EC%95%84%ED%85%8C%EB%82%98";
        CostumePrice = GetRealBuyPrice ("KickerShoes04").ToString ();
        Costume = "KickerShoes04";
    }

    public void Btn_Fun_BuySoccerShoes_Buy ()
    {
        MenuCommonOpen ("Ui_Popup", "buy_item");
        dicMenuList ["buy_item"].transform.FindChild ("Label_item").GetComponent<UILabel> ().text = WWW.UnEscapeURL (CostumeName);
        dicMenuList ["buy_item"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = CostumePrice;

        for (int i = 1; i < 5; i++) {
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item0" + i).gameObject.SetActive (false);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item0" + i).gameObject.SetActive (false);
        }
        //Debug.Log ("CostumeNAME" + Costume);
        ShoesGlove (Costume);
    }

    /// <summary>
    /// SoccerShoose
    /// </summary>

    string Costume, costumeName;

    public void Btn_Fun_SoccerGlove1 ()
    {
        CostumeName = "%EB%84%B5%ED%88%AC%EB%88%84%EC%8A%A4";
        Costume = "KeeperGloves01";
        CostumePrice = GetRealBuyPrice ("KeeperGloves01").ToString ();
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01", true).GetComponent<UICheckbox> ().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_lobby"], "LPanel_itemshop/glove/glovelist/grid/item01", true).GetComponent<UICheckbox> ().Set (true);
    }

    public void Btn_Fun_SoccerGlove2 ()
    {
        CostumeName = "%ED%94%8C%EB%A3%A8%ED%86%A4";
        CostumePrice = GetRealBuyPrice ("KeeperGloves02").ToString ();
        Costume = "KeeperGloves02";
    }

    public void Btn_Fun_SoccerGlove3 ()
    {
        CostumeName = "%EB%A7%88%EB%A5%B4%EC%8A%A4";
        CostumePrice = GetRealBuyPrice ("KeeperGloves03").ToString ();
        Costume = "KeeperGloves03";
    }

    public void Btn_Fun_SoccerGlove4 ()
    {
        CostumeName = "%EB%AF%B8%EB%84%A4%EB%A5%B4%EB%B0%94";
        CostumePrice = GetRealBuyPrice ("KeeperGloves04").ToString ();
        Costume = "KeeperGloves04";
    }

    public void Btn_Fun_SoccerGlove_Buy ()
    {

        MenuCommonOpen ("Ui_Popup", "buy_item");
        dicMenuList ["buy_item"].transform.FindChild ("Label_item").GetComponent<UILabel> ().text = WWW.UnEscapeURL (CostumeName);
        dicMenuList ["buy_item"].transform.FindChild ("Label_price").GetComponent<UILabel> ().text = CostumePrice;

        for (int i = 1; i < 5; i++) {
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item0" + i).gameObject.SetActive (false);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item0" + i).gameObject.SetActive (false);
        }
        //Debug.Log ("CostumeNAME" + Costume);
        ShoesGlove (Costume);
    }

    void ShoesGlove (string costumename)
    {
        switch (costumename) {
        case "KickerShoes01":
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item01").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item01/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 2";
            break;
        case "KickerShoes02":
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item02").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item02/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 4";
            break;
        case "KickerShoes03":
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item03").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item03/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 6";
            break;
        case "KickerShoes04":
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item04").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_shoes/item04/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 8";
            break;
        case "KeeperGloves01":
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item01").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item01/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 2";
            break;
        case "KeeperGloves02":
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item02").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item02/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 4";
            break;
        case "KeeperGloves03":
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item03").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item03/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 6";
            break;
        case "KeeperGloves04":
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item04").gameObject.SetActive (true);
            dicMenuList ["buy_item"].transform.FindChild ("item_glove/item04/Label_discript").gameObject.GetComponent<UILabel> ().text = "가산점 + 8";
            break;
        }
    }

    public void Btn_Fun_BuySoccerGloveShose_BuyOk ()
    {
        dicMenuList ["CenterCircle"].SetActive (true);

        MenuCommonOpen ("Ui_Popup", "buy_item", false);
        WasPurchaseCostume aObj = new WasPurchaseCostume () { User = Ag.mySelf, costumeName = Costume }; // ,buyType = 1 };


        aObj.messageAction = (int pInt) => {
            dicMenuList ["CenterCircle"].SetActive (false);
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                CostumeInfo ();
                //dicMenuList ["alert"].SetActive (true);
                MenuCommonOpen ("Ui_Popup", "buy_item", false);
                Ag.LogString (" result : Success ");
                break;
            case -1:
                MenuCommonOpen ("Ui_popup", "havenotpoint", true);
                break;
            }
        };
    }

    public void CostumeInfo ()
    {
        WasCardUniformCostume aObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 242 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Ag.mySelf.SetCostumeToCard ();
                CostumeSetting ();
                Ag.LogString (" result : Success ");
                return;
            }
        };

    }

    public void TicketNumSetting ()
    {
        dicMenuList ["NormalCardTicket1_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
        dicMenuList ["NormalCardTicket3_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal3").ToString ();
        dicMenuList ["HighCardTicket1_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
        dicMenuList ["HighCardTicket3_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium3").ToString ();
        dicMenuList ["Team_NormalCardTicket1_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal").ToString ();
        dicMenuList ["Team_NormalCardTicket3_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketNormal3").ToString ();
        dicMenuList ["Team_HighCardTicket1_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium").ToString ();
        dicMenuList ["Team_HighCardTicket3_NumLabel"].GetComponent<UILabel> ().text = CombiItemListEa ("TicketPremium3").ToString ();
    }

    public void Btn_Fun_BuySoccerGloveShose_BuyCancel ()
    {
        MenuCommonOpen ("Ui_Popup", "buy_item", false);
    }

    /// <summary>
    /// Coupon
    /// </summary>

    public void Btn_Fun_CouponNumInputOk ()
    {
    }

    /// <summary>
    /// BuyUniform
    /// </summary>


    public void Btn_Fun_UniformBuy ()
    {
        /*
        dicMenuList ["popup_buyuniform"].SetActive (true);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = uniformTypeid;
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%AA%A8%EB%93%A0%EB%8A%A5%EB%A0%A5%EC%B9%98%20%2B5");
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Price_Label").GetComponent<UILabel> ().text = ItemPrice (uniformTypeid).gold.ToString ();
        */
        UniformLabelSetting ();
    }

    public void Btn_Fun_UniformBuyCancel ()
    {

        dicMenuList ["popup_buyuniform"].SetActive (false);
    }
}
