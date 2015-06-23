using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;

public class SceneProto : AmSceneBase
{
}

/*
    //UIManager muiMan;
    UIScrolManager muiMan;

    bool muiIsVertical = false; // default is Horizontal..
    AmPlayer mCurPlayer;
    float muiObjWidth = 5f, muiCamBothOffset = 100;
    
    //  ////////////////////////////////////////////////     Starting Init Job
    public override void Start ()
    {
        //Ag.Init ();
                
        mTimeLooseAtStartPoint = 0.1f; // Second
        mSeldomActionNum = 500;
        base.Start ();
        myGUI.SetColumns (3, 12);
        muiMan = new UIScrolManager (GameObject.Find ("CameraHorizontal").camera, muiIsVertical, GetType().ToString (), 12, 14, 25);
        // float pSplitOffset, float pSplitPanelOffset, float pSplitOthers)
        muiMan.SetSplitAnimation(true, new Vector2(0, 10), new Vector2(22, 15));
        muiMan.SetSpeed(pSplitSpeed: 15, pCellSpeed: 7, pAutoScroll: 0.2f, pSpeedLimit: 300);

        //muiMan.TouchOnlyEnable();
        
        // float pSplitOffset, float pSplitPanelOffset, float pSplitOthers)
    }
    
    public override void BaseStartSetting ()
    {
        base.BaseStartSetting ();
  
        //Vector3 rotation = new Vector3 (0f, 0f, 0f);
        Quaternion rotQ = new Quaternion (-90f, -90f, -90f, 0f);
        
        float limit = 0;

        for (int ii=0; ii<20; ii++) {
            GameObject curObj = mRscrcMan.GetPrefab ("JustPlane"); // HtBaseObject.cs 
            curObj.name = "Obj" + (ii+1);
            
            //if (ii > 6)                curObj.name  = "Pur" + (ii+1);
            if (ii > 6)
                curObj.GetComponent<Cell>().muiSwitchEnable = false;
            
            float curPosition = muiCamBothOffset + muiObjWidth * ii;
            curObj.transform.position = new Vector3 (curPosition, -1f, 10f);
            curObj.transform.localScale = new Vector3 (2.3f, (float)(5 + ii * 0.1), 1);
            curObj.transform.rotation = new Quaternion (0, 180f, 0, 0);
            
            GameObject btn = mRscrcMan.GetPrefab ("CubeBttn");
            btn.transform.position = new Vector3 (curPosition, -1f, 8f);
            //btn.transform.localRotation = rotQ;
            btn.transform.parent = curObj.transform;  // parent / child  setting....  ******
            //btn.transform.localScale = new Vector3 (0.3f, 0.03f, 0.2f);
            btn.name = "Btn" + (ii+1);
            
            AmPlayer aPlay = new AmPlayer();  // no need...  assign existing player...
            
            aPlay.mpName = btn.name;   // no need....
            
            // Add GameObject, AmPlayer  Object....  ###  M U S T  ###
            muiMan.AddPlayer(curObj.name, aPlay);  // Key, Value...
            muiMan.AddAMember (curObj);
            
            limit = muiCamBothOffset + muiObjWidth * ii;
        }

        float margin = 11;
        muiMan.SetLimit (muiCamBothOffset + margin , limit - margin);
    }
 
    //  ////////////////////////////////////////////////     Update related
    public override void Update ()
    {
        base.Update ();
        
        if (muiMan == null) {
            Ag.LogIntenseWord ("SceneProto :: Update >>>>>  muiMan is NULL   ");
            return;
        }
            
        // Split.. Closing..
        if (Input.GetMouseButtonDown (0)) {
            muiMan.MouseDown (Input.mousePosition, "");
            string hitName = RayHit();
            
            if (hitName == "")
                return;
            
            if (hitName.Substring(0, 3) == "Btn") {
                muiMan.HitSplitButton (hitName);
                
                mCurPlayer = muiMan.GetPlayer( "Obj" +  hitName.Substring(3));  //  Get the Current Showing AmPlayer   ###  M U S T  ### 
                
                Ag.LogNewLine(5);
                (" Split ::  Object is   >> " + hitName + " <<       mCurPlayer.mpName  >>>> " + mCurPlayer.mpName  + "  <<<< " ).HtLog();
                Ag.LogNewLine(5);
            }
            
            if (hitName == "SplitPanel") {
                muiMan.HitClosePanel();
            }
        }
        
        if (Input.GetMouseButtonUp (0)) {
            muiMan.MouseUp ();
        }
        
        // Scroll & Switch...
        if (Input.GetMouseButton (0)) {
            muiMan.MouseHold (Input.mousePosition, RayHit());
        }
        
        //foreach (Touch touch in Input.touches) {        }
    }
    
    string RayHit ()
    {
        Ray ray = (muiMan.muiCam.ScreenPointToRay (Input.mousePosition)); //create the ray
        RaycastHit hit; //create the var that will hold the information of where the ray hit
        
        if (Physics.Raycast (ray, out hit)) {
            if (hit.transform == null) {
                return "";
            }
            else
                return hit.transform.name;
        }
        return "";
    }
    
    //  ////////////////////////////////////////////////     OnGUI related
    public override void OnGUI ()
    {
        base.OnGUI ();
        
        muiCol = 0;
        muiRow = 10;
        
        if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), " < Set Order > ")) {
            
            muiMan.SetOrder();  // ###  M U S T  ###
            
        }
    }
}

/*
 
        // Create Mesh with Scripts.
        MeshFilter meshFlt = GetComponent<MeshFilter>();
        
        if (meshFlt == null) {
            Ag.LogIntenseWord(" Mesh Filter is NULL !!! ");
            return;
        }
        
        Vector3 pt0 = new Vector3(0, 0, 0);
        Vector3 pt1 = new Vector3(10, 0, 0);
        Vector3 pt2 = new Vector3(5, 0, 3);
        Vector3 pt3 = new Vector3(5, 3, 5);
        
        Vector2 uv1 = new Vector2(0, 0);
        Vector2 uv2 = new Vector2(0.3f, 0.5f);
        Vector2[] uv = new Vector2[] { uv1, uv2, uv1, uv2, uv1, uv2, uv1, uv2, uv1, uv2, uv1, uv2 };
        
        Mesh theMesh = meshFlt.sharedMesh;
        if (theMesh == null) {
            Ag.LogString(" >>>>> the Mesh is null ");
            meshFlt.mesh = new Mesh();
            theMesh = meshFlt.sharedMesh;
        }
        theMesh.Clear();
        
        theMesh.name = "newMesh";
        theMesh.vertices = new Vector3[] { pt0, pt1, pt2, pt0, pt2, pt3, pt2, pt1, pt3, pt0, pt3, pt1};
        //theMesh.triangles = new int[] {0, 1, 2, 0, 2, 3, 2, 1, 3, 0, 3, 1};
        theMesh.uv = uv;
        theMesh.triangles = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        
        theMesh.RecalculateNormals();
        theMesh.RecalculateBounds();
        theMesh.Optimize();
       // */ 