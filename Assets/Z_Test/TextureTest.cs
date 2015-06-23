using UnityEngine;
using System.Collections;
using SimpleJson;

public class TextureTest : MonoBehaviour
{
    string good;
    GameObject mShadowPlane, mPlayer, mSfield;
    GameObject mCursor1, mCursor2, mCursor3, mCursor4;
    AmCard myCard, EnemCard;



    // Use this for initialization
    void Start ()
    {
        myCard = new AmCard ();
        myCard.mDirectObj.mWidth = new int[4] {250,250,250,250};
        myCard.mDirectObj.mPosition = new int[4] { 0, 250, 500, 750 };
        myCard.SetDirectionArea ();



        Ag.mgIsKick = true;
        mCursor1 = GameObject.Find ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar/nod0").gameObject;
        mCursor2 = GameObject.Find ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar/nod1").gameObject;
        mCursor3 = GameObject.Find ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar/nod2").gameObject;
        mCursor4 = GameObject.Find ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar/nod3").gameObject;
        DrawGuideLine ();

        //GetUniqueKey(20);

        string cocococo = "zozozozoozzoz"+'"'+"ozozo:"+ ":" +"zozozozo" ;

        //'"'+ cocococo.Replace(WWW.UnEscapeURL("%22"),"\"")+'"';

        Debug.Log( cocococo.Replace("\"","\\\"").ToString());


    }


    // Update is called once per frame
    void Update ()
    {
//        mShadowPlane.transform.position = new Vector3(mPlayer.transform.position.x,0,mPlayer.transform.position.z);
    }

    GameObject Guidebar;

    void DrawGuideLine ()
    {
        float staX = (float)(250 / 1000.0f);
        mCursor1.transform.localPosition = new Vector3 ((staX * 580) - 290, -286, -9.27166f);
        staX = (float)(500 / 1000.0f);
        mCursor2.transform.localPosition = new Vector3 ((staX * 580) - 290, -286, -9.27166f);
        staX = (float)(750 / 1000.0f);
        mCursor3.transform.localPosition = new Vector3 ((staX * 580) - 290, -286, -9.27166f);
        staX = (float)(1000 / 1000.0f);
        mCursor4.transform.localPosition = new Vector3 ((staX * 580) - 290, -286, -9.27166f);


        int num = myCard.arrArea.Count;

        for (int i = 0; i < num; i++) {
            int[] curVal = (int[])myCard.arrArea [i];
            int color = curVal [0];
            int sta = curVal [1], end = curVal [2];
            
            //Debug.Log ("Color  " + curVal [0] + "Start  " + curVal [1] + "End  " + curVal [2]);
            staX = (float)(sta / 1000.0f);
            float width = (float)((end - sta) / 1000.0f);
            
            Guidebar = (GameObject)Instantiate (Resources.Load ("prefab_General/IngameKickBar"));
            Guidebar.transform.parent = GameObject.Find ("Ui_camera/Camera/Ui_ingame/Panel_progressbar_kickbar").gameObject.transform;
            Guidebar.transform.localPosition = new Vector3 ((staX * 580) - 290, -260, -0.29f);
            Guidebar.GetComponent<UISprite> ().spriteName = "kickbar" + color;
            if (color == 0 && width * 580 < 0)
                width = 0;
            Guidebar.transform.localScale = new Vector3 (width * 580, 24, 1);
        }
    }


    
    public class SessionIdentifierGenerator : MonoBehaviour {
        


        
    }


}
