using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;

public class UICam : BaseObject
{
    public bool muiIsVertical { set; get; }

    public UIManagerBase muiManager { set; get; } 

    public float muiFlyingSpeed, muiAutoScrollSpeed;
    public float muiSplitOffset;
    public bool mIsTouchOnly;
    public GameObject muiTargetObj;
    
    // Delegate
    public delegate void delSendSimpleMsg ();

    public delSendSimpleMsg delFlyingIsOver, delFlyStart;
    float[] muiScreenLimit = null;
    float muiCurPo, muiSclVsDragging;
    UiState muiState;
    Vector3 muiTouchCo, muiPrvTouchCo;
    List<Vector3> arrTouch = new List<Vector3> ();
    GameObject muiSwitchingObj;
    bool muiIsOffLimit, muiOffLimitSmallerCase;
    Vector3 muiTargetPosi, muiDefaultPosi;
    
    //  ////////////////////////////////////////////////     Starting Init Job
    public  void Start ()
    {
    }

    public void GetWorldPointsOfCurScreen(out Vector3 pOri, out Vector3 pMax)
    {
        //Ag.LogDouble (" UICam :: GetWorldPointsOfCurScreen  X : " + Ag.mgScrX + ",    Y : " + Ag.mgScrY);
        pOri = this.camera.ScreenToWorldPoint (new Vector3 (0, 0));
        pMax = this.camera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height));
    }
    
    public float GetScreenRatio ()
    {
        Vector3 po1 = new Vector3 (50, 50, 0);
        Vector3 po2 = new Vector3 (110, 110, 0);
        Vector3 prvPo = this.camera.ScreenToWorldPoint (po1);
        Vector3 curPo = this.camera.ScreenToWorldPoint (po2);
        muiSclVsDragging = (prvPo.x - curPo.x) / (po1.x - po2.x);
        return muiSclVsDragging;
    }
    
    public void SetEvents ()
    {
        muiManager.delSetState += pStt => {
            //SceneProto.delSetState += pStt => {
            muiState = pStt;
            (" ========================================================" + " Set State To ==>  " + pStt).HtLog ();
        };
        

    }

    public void SetLimit (float pValue)
    {
        if (muiScreenLimit == null) {
            muiScreenLimit = new float[2];
            muiScreenLimit [0] = pValue;
        } else {
            if (muiScreenLimit [0] < pValue)
                muiScreenLimit [1] = pValue;
            else {
                float val = muiScreenLimit [0];
                muiScreenLimit [0] = pValue;
                muiScreenLimit [1] = val;
            }
        }
        
        Ag.LogString (" Limit Set Between  " + muiScreenLimit [0] + " ,  " + muiScreenLimit [1]);
    }
    
    public void SetSplitTargetPosition (Vector3 pObject)
    {
        muiDefaultPosi = transform.position;
        muiTargetPosi = pObject;
        muiTargetPosi = muiTargetPosi.Move (muiIsVertical, muiSplitOffset);
    }
    
    public void GoToDefaultPosition ()
    {
        muiTargetPosi = muiDefaultPosi;
    }
    
    public int GetTouchNum ()
    {
        return arrTouch.Count;
    }
    
    //  ////////////////////////////////////////////////     Update related
    public  void Update ()
    {
//        if (muiState == UiState.NEW_ELEMENT) {
//            MoveToTargetObj ();
//            return;
//        }
//            
//        
//        if (muiScreenLimit == null)
//            return;
//        
//        if (Input.GetMouseButtonDown (0)) {
//            muiPrvTouchCo = Input.mousePosition;
//            arrTouch.Clear ();
//        }
//  
//        // Set Variables..
//        muiCurPo = this.gameObject.CurrentPosition (muiIsVertical);
//                    
//        muiIsOffLimit = IsOffLimit ();
//        
//        if (muiState == UiState.NONE && muiIsOffLimit)
//            muiState = UiState.FlyBackToLimit;
//
//        if (muiState == UiState.FLY)
//            Fly ();
//        if (muiState == UiState.FlyBackToLimit) {
//            muiFlyingSpeed = 0;
//            FlyBack ();
//        }
//        
//        //  ////////////////////////////////////////////////     ////////////////////////     >>>>> Holding.... <<<<<
//        if (Input.GetMouseButton (0)) {
//            //"UICamera ::    Mouse Button >>>>>   ".HtLog();
//            
//            muiTouchCo = Input.mousePosition;
//            muiCurPo = this.gameObject.CurrentPosition (muiIsVertical);
//            
//            if (mIsTouchOnly)
//                return;
//            
//            DraggingCamProcess ();
//            //("UICamera ::    Mouse Button >>>>>   " + muiTouchCo).HtLog();
//            if (muiState == UiState.SWITCH)
//                AutoScroll ();
//        }
//        
//        if (Input.GetMouseButtonUp (0) && (muiState != UiState.SPLIT && muiState != UiState.SPL_CLOSE)) {
//            if (arrTouch.Count < 3)
//                return;
//            //float del = arrTouch.Last().CurrentPosition(muiIsVertical) - arrTouch[0].CurrentPosition(muiIsVertical);
//            float del = arrTouch [arrTouch.Count - 1].CurrentPosition (muiIsVertical) - arrTouch [0].CurrentPosition (muiIsVertical);
//            
//            muiFlyingSpeed = del / arrTouch.Count;
//            
//            // Flying or Not to Fly
//            if (Math.Abs (muiFlyingSpeed) > 2f)
//                delFlyStart ();
//            else
//                delFlyingIsOver ();
//            
//            //("UICamera :: Update  >>>   FlyingSpeed   :  " + muiFlyingSpeed).HtLog();
//        }
//        
//        if (muiState == UiState.SPLIT || muiState == UiState.SPL_CLOSE) {
//            MoveToSplitPosition ();
//            
//            float dist = muiDefaultPosi.DistanceXY (transform.position, muiIsVertical);  // Split Closing case... 
//            if (dist < 0.05f && muiState == UiState.SPL_CLOSE) {
//                delFlyingIsOver ();
//            } else {
//                //
//            }
//        }
      
        
    }
    
    void MoveToTargetObj ()
    {
        transform.position = transform.position.IntDivide (muiTargetObj.transform.position, 10, 1);
    }
    
    void MoveToSplitPosition ()
    {
        float newCo = 0; //, dist = 1 ;
        
        Vector3 curCo = transform.position;
        
        if (muiIsVertical) {
            newCo = (15f * curCo.y + muiTargetPosi.y) / 16f;  // New Coordinate..
            //dist = Mathf.Abs(curCo.y - newCo);
        } else {
            newCo = (15f * curCo.x + muiTargetPosi.x) / 16f;
            //dist = Mathf.Abs(curCo.x - newCo);
        }
        
        if (muiIsVertical)
            transform.position = new Vector3 (curCo.x, newCo, curCo.z);
        else
            transform.position = new Vector3 (newCo, curCo.y, curCo.z);
    }
    
    bool IsOffLimit ()
    {
        if (muiCurPo < muiScreenLimit [0] - 0.01f) { // 100  ~ 126.7 ... world coordinate..
            //Ag.LogString ("Off Limit during Flying " + muiCurPo + " , " + muiScreenLimit [0] + "  Smaller Case " );
            muiOffLimitSmallerCase = true;
            return true;
        }
        if (muiScreenLimit [1] + 0.01f < muiCurPo) { // 100  ~ 126.7 ... world coordinate..
            //Ag.LogString ("Off Limit during Flying " + muiCurPo + " , " + muiScreenLimit [0] + "  Bigger Case " );
            muiOffLimitSmallerCase = false;
            return true;
        }
        return false;
    }
   
    void FlyBack ()
    {
        float dist;
        //Ag.LogString ("Fly Back " + muiCurPo + " , " + muiScreenLimit [0] + ", " + muiScreenLimit [1] );
        if (Mathf.Abs (muiCurPo - muiScreenLimit [0]) > Mathf.Abs (muiCurPo - muiScreenLimit [1]))
            dist = muiCurPo - muiScreenLimit [1];
        else 
            dist = muiCurPo - muiScreenLimit [0];
        
        if (Mathf.Abs (dist) < 0.3)
            dist *= 3;
        
        this.gameObject.transform.MoveXY (-dist / 10f, muiIsVertical);        
        if (Mathf.Abs (dist) < 0.01f)
            delFlyingIsOver ();
    }
    
    void Fly ()
    {
        if (muiIsOffLimit) 
            muiFlyingSpeed *= 0.5f; //0.8f;
        
        this.gameObject.transform.MoveXY (-muiFlyingSpeed * muiSclVsDragging, muiIsVertical);
        
        muiFlyingSpeed *= 0.95f;    
        if (Mathf.Abs (muiFlyingSpeed) < 0.5f) {
            
            "  Fly  ".HtLog ();
            /*
            float dist = muiDefaultPosi.DistanceXY(transform.position, muiIsVertical);  // Split Closing case... 
            if (dist > 0.1f) {
                MoveToSplitPosition();
                return;
            } */
            
            delFlyingIsOver ();
        }
            
    }
    
    void DraggingCamProcess ()
    {
        if (muiState != UiState.SCROLL) // && muiState != UiState.SPLIT)
            return;
        
        Vector3 prvPo = this.camera.ScreenToWorldPoint (muiPrvTouchCo);
        Vector3 curPo = this.camera.ScreenToWorldPoint (muiTouchCo);
        
        float diff = prvPo.DiffenceXY (curPo, muiIsVertical);
        
        // Change Camera Position
        float limitScrollDownRatio = 1.0f;
        if (IsOffLimit ())
            limitScrollDownRatio = 0.5f;
        //Ag.LogString("DraggingCamProcess  " + -diff + ", Off Limit Ratio : " + limitScrollDownRatio);
        this.gameObject.transform.MoveXY (-diff * limitScrollDownRatio, muiIsVertical);
        
        if (Mathf.Abs (muiPrvTouchCo.x - muiTouchCo.x) > 0)
            muiSclVsDragging = (prvPo.x - curPo.x) / (muiPrvTouchCo.x - muiTouchCo.x);
        //(  (float)(muiPrvTouchCo.x - muiTouchCo.x) / (float) (prvPo.x - curPo.x) ).ToString().HtLog();
        
        muiPrvTouchCo = muiTouchCo;
        
        arrTouch.Add (muiTouchCo);
        while (arrTouch.Count > 6)
            arrTouch.RemoveAt (0);
    }
    
    public bool IsScrolling ()
    {
        float sum = 0, x, y;
        if (arrTouch.Count == 0)
            return false;
        x = arrTouch [0].x;
        y = arrTouch [0].y;
        foreach (Vector3 vec in arrTouch) {
            sum += Mathf.Abs (vec.x - x);
            sum += Mathf.Abs (vec.y - y);
            x = vec.x;
            y = vec.y;
        }
        
        if (sum < 1)
            return false;
        else
            return true;
    }
    
    void AutoScroll ()
    {
        // Camera Scroll
        float tchPo, scrSize;
        
        if (muiIsVertical) {
            tchPo = muiTouchCo.y;
            scrSize = Ag.mgScrY;
        } else {
            tchPo = muiTouchCo.x;
            scrSize = Ag.mgScrX;
        }
        
        //Ag.LogString ("TouchCo ::  " + tchPo + ", Screen :: " + scrSize*0.8f);
        if (tchPo > scrSize * 0.8f) {
            if (muiIsOffLimit && !muiOffLimitSmallerCase)
                return;
            this.gameObject.transform.MoveXY (muiAutoScrollSpeed, muiIsVertical);
            muiSwitchingObj.transform.MoveXY (muiAutoScrollSpeed, muiIsVertical);
        }
        
        if (tchPo < scrSize * 0.2f) {
            if (muiIsOffLimit && muiOffLimitSmallerCase)
                return;
            this.gameObject.transform.MoveXY (-muiAutoScrollSpeed, muiIsVertical);
            muiSwitchingObj.transform.MoveXY (-muiAutoScrollSpeed, muiIsVertical);
        }    
    }
}

