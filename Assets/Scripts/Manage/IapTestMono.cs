using UnityEngine;
using System.Collections;

public class IapTestMono : MonoBehaviour
{
    // Use this for initialization
    AmUI myGUI;

    void Start ()
    {
        myGUI = new AmUI ();
        myGUI.SetColumns (3, 10);

    }
    // Update is called once per frame
    void Update ()
    {
    
    }

    int muiCol, muiRow;

    void OnGUI ()
    {
        muiCol = 0;
        muiRow = 1;
        int colN = 0, colEA;

        #if UNITY_IPHONE
        string iapMsg = "N : " + AgStt.mIAP.arrProduct.Count + "  Psble : " + AgStt.mIAP.CanMakePayment ();
        GUI.Label (myGUI.GetRect (muiCol, muiRow++), iapMsg);

        //Rect iap = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), "IAP:Init")) {  // 
            AgStt.mIAP.ProductRequest ();
        }
        if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), "cash0030")) {  // 
            AgStt.mIAP.PurchaseProduct ("cash0030");
        }
#endif

    }
}
