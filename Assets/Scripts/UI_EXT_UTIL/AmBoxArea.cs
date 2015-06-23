using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class AmBoxArea : AmObject
{
    public AmBoxArea Container;
    public float mContDist = 0;
    // Usually Bigger than myself.. 
    float[] Xrange = new float[2], Yrange = new float[2];
    public bool mMinAlign, mVert;
    // MinAling : Min of container is aligned .. 
    bool? bOutOfMin;
    // HasValue: out of limit.. true : out of min ... 
    protected Vector3 myDirect;
    public AmUiOption myOption;
    // set in UITileManager ``
    public float Xmin { set { Xrange [0] = value; } get { return Xrange [0]; } }

    public float Xmax { set { Xrange [1] = value; } get { return Xrange [1]; } }

    public float Ymin { set { Yrange [0] = value; } get { return Yrange [0]; } }

    public float Ymax { set { Yrange [1] = value; } get { return Yrange [1]; } }

    public Dictionary<string, GameObject> dicCell { set; private get; }
    //  A////////////////////////////////////////////////     A///////////////////////A    Creation    ~~~~~~X
    public AmBoxArea (string name, float[] pRange, bool pVert, AmBoxArea pCont = null)
    {
        mName = name;
        Xrange [0] = pRange [0];
        Xrange [1] = pRange [1];
        Yrange [0] = pRange [2];
        Yrange [1] = pRange [3];
        mVert = pVert;

        myDirect = pVert ? new Vector3 (0, 1, 0) : new Vector3 (1, 0, 0);
        //myOption = pOption;

        Container = pCont;
        if (pCont == null)
            return;

        if (mVert)
            mMinAlign = Yrange [0] == Container.Yrange [0];
        else
            mMinAlign = Xrange [0] == Container.Xrange [0];
    }

    public bool IsInside (Vector3 pV)
    {
        if (pV.x < Xrange [0] || Xrange [1] < pV.x)
            return false;
        if (pV.y < Yrange [0] || Yrange [1] < pV.y)
            return false;
        return true;
    }

    public bool IsContainerInside (Vector3 pV)
    {
        float[] ranX, ranY;
        Container.ShiftedRange (out ranX, out ranY, mContDist);

        if (pV.x < ranX [0] || ranX [1] < pV.x)
            return false;
        if (pV.y < ranY [0] || ranY [1] < pV.y)
            return false;
        return true;
    }

    public void ShiftedRange (out float[] xRange, out float[] yRange, float disp)
    {
        xRange = new float[] { mVert? Xrange[0] : Xrange[0] + disp, mVert? Xrange[1] : Xrange[1] + disp };
        yRange = new float[] { mVert? Yrange[0] + disp : Yrange[0], mVert? Yrange[1] + disp : Yrange[1] };

        Ag.LogDouble ("   Shifted Range " + disp.LogWith ("disp") + xRange [0].LogWith ("X0") + xRange [1].LogWith ("X1") + 
            yRange [0].LogWith ("Y0") + yRange [1].LogWith ("Y1"));
    }

    public void DoScroll (Vector3 pVect)
    {
        pVect = Vector3.Scale (pVect, myDirect);
        float ScrlPosi = mVert ? pVect.y : pVect.x;
        mContDist += ScrlPosi;
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            kv.Value.GetComponent<CuCell> ().DoScroll (pVect);
        }
//        (" AmBoxArea :: DoScroll " + mContDist.LogWith ("mContDist") + ScrlPosi.LogWith("ScrlPosi") ).HtLog ();
    }

    //  _////////////////////////////////////////////////_    _____  Auto Scroll  _____
    public float GetRatioOfPositionInBox (Vector3 pTch)
    { 
        float min, max, hLeng, midCo;
        GetLimitInScrollDirection (out min, out max);
        float codi = mVert ? pTch.y : pTch.x;
        hLeng = (max - min) * 0.5f;
        midCo = (min + max) * 0.5f;
        return (midCo - codi) / hLeng; 

//        return Mathf.Abs (midCo - codi) / hLeng; 
    }

    public void AutoScroll (Vector3 pTch)
    {
        float ratio = GetRatioOfPositionInBox (pTch);

        DoScroll (myDirect * ratio * 0.01f);
    }

    void GetLimitInScrollDirection (out float Min, out float Max)
    {
        Min = mVert ? Ymin : Xmin;
        Max = mVert ? Ymax : Xmax;
    }

    public bool OffLimitOf (out bool? minCase)
    {
        float min, max, cMin, cMax;
        minCase = null;
        GetLimitInScrollDirection (out min, out max);

        if (mVert) {
//            min = Ymin;
//            max = Ymax;
            cMin = Container.Ymin + mContDist;
            cMax = Container.Ymax + mContDist;
        } else {
//            min = Xmin;
//            max = Xmax;
            cMin = Container.Xmin + mContDist;
            cMax = Container.Xmax + mContDist;
        }

        if (cMax < max) {
            bOutOfMin = minCase = false;
            //("   OffLimit :::::: (cMax < max)  case   " + mVert.LogWith("Vert?") ).HtLog ();
            //("      Values ::  " + mContDist.LogWith ("mContDist") + cMax.LogWith ("cMax") + max.LogWith ("max")).HtLog ();
            return true;
        }
        if (min < cMin) {
            bOutOfMin = minCase = true;
            //("   OffLimit :::::: (min < cMin)  case   " + mVert.LogWith("Vert?") ).HtLog ();
            //("      Values ::  " + mContDist.LogWith ("mContDist") + cMin.LogWith ("cMin") + min.LogWith ("min")).HtLog ();
            return true;
        }

        bOutOfMin = null;
        return false;
    }

    public bool Return2Limit_Done ()
    {
        Ag.LogIntenseWord ("  Wagu :: GetReturnVectorAndReturn  ");

        float wagu, cont;

        if (mVert) {
            wagu = bOutOfMin.Value ? Ymin : Ymax;
            cont = bOutOfMin.Value ? Container.Ymin : Container.Ymax;
        } else {
            wagu = bOutOfMin.Value ? Xmin : Xmax;
            cont = bOutOfMin.Value ? Container.Xmin : Container.Xmax;
        }
        cont += mContDist;

        float curDist = (wagu - cont) * (10f / (myOption.vectMoveAni + 10));

        if (Mathf.Abs (curDist) < myOption.minFlybackDistance)
            curDist *= 2;
        //Ag.LogDouble ( myOption.vectMoveAni.LogWith("Opt") +  curDist.LogWith ("curDist"));
        DoScroll (new Vector3 (curDist, curDist, 0));
        if (Mathf.Abs (curDist) < 0.01f) {
            DoScroll (new Vector3 (2 * curDist, 2 * curDist, 0));
            return true;
        } else 
            return false;             
    }

    void InitializeContainerDistance ()
    {

    }

    public void Back2Inside (Vector3 pVect)
    {
        float ScrlPosi = mVert ? pVect.y : pVect.x;
        Ag.LogIntenseWord ("AmBoxArea :: Back2Inside" + ScrlPosi.LogWith ("ScrlPosi"));
        mContDist += ScrlPosi;
        foreach (KeyValuePair<string, GameObject> kv in dicCell) {
            kv.Value.GetComponent<CuCell> ().SetTargetPosition (pVect);
        }
    }

    public void ShowMyself ()
    {
        Ag.LogIntense (3, true);
        (" ======================================================================================================= ").HtLog ();
        ("   Name ::   " + mName).HtLog ();
        ("  OneBox ::   X range >> From " + Xmin + " To " + Xmax + "     Y range >> From " + Ymin + " To " + Ymax).HtLog ();
        (" ======================================================================================================= ").HtLog ();
        Ag.LogIntense (3, false);

    }
}