using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class AgTile
{
    int eaX, eaY;
    Vector3 llVec, urVec, cenVec;
    // lower left / upper right. 
    AgTile mFrame;

    public Vector3 Center { get { return cenVec; } }

    float[] arrX, arrY;
    bool mVert, mReverse = false;
    float mSetBack;

    public float SetBack { get { return mSetBack; } }

    //bool? bOutOfMin;

    public float Xmin { get { return arrX [0]; } }

    public float Xmax { get { return arrX [eaX - 1]; } }

    public float Ymin { get { return arrY [0]; } }

    public float Ymax { get { return arrY [eaY - 1]; } }

    public float MinVal { get { return mVert ? Ymin : Xmin; } }

    public float MaxVal { get { return mVert ? Ymax : Xmax; } }

    public float FrameMin { get { return mFrame.mSetBack + mFrame.MinVal; } }

    public float FrameMax { get { return mFrame.mSetBack + mFrame.MaxVal; } }

    public AgTile (AgTile pContain, int eaX, int eaY, Vector3 llVec, Vector3 urVec, bool vert, bool reverse)
    {
        mFrame = pContain;
        this.eaX = eaX;
        this.eaY = eaY;
        this.llVec = llVec;
        this.urVec = urVec;
        cenVec = (llVec + urVec) * 0.5f;
        //(llVec.LogWith ("LL") + urVec.LogWith ("UR") + cenVec.LogWith ("CEN")).HtLog ();
        arrX = new float[eaX + 1];
        arrY = new float[eaY + 1];
        mVert = vert;
        mReverse = reverse;

        Ag.LogIntenseWord (" AgTile::AgTile >>  Ea ( " + eaX + " / " + eaY + " ) >>  LLvect : " + llVec + ", URvect : " + urVec);

        Divide ();

    }

    public AgTile (Vector3 llV, Vector3 urV)
    {
        arrX = new float[] { llV.x, urV.x };
        arrY = new float[] { llV.y, urV.y };
    }

    public float GetCodiFromOrigin (bool IsX, float Ratio)
    {
        float min, max, dist;
        min = IsX ? arrX [0] : arrY [0];
        max = IsX ? arrX [1] : arrY [1];
        dist = max - min;
        //(min + Ratio * dist).LogWith ("dist").HtLog();
        return min + Ratio * dist;
    }

    public void ApplySetBack (Vector3 pV)
    {
        mSetBack += pV.x;
        mSetBack += pV.y;
    }

    public float GetCodiFromOriginWithDist (bool IsX, float Dist)
    {
        float min;
        min = IsX ? arrX [0] : arrY [0];

        (min + Dist).LogWith ("Codi").HtLog ();

        return min + Dist;
    }

    void Divide ()
    { // 
        float disX = (urVec.x - llVec.x) / eaX, disY = (urVec.y - llVec.y) / eaY; // distance ..

        for (int k = 0; k <= eaX; k++) {
            arrX [k] = llVec.x + disX * k;
        }

        for (int k = 0; k <= eaY; k++) {
            arrY [k] = llVec.y + disY * k;
        }

        Ag.LogDouble ("  AgTile :: Divide >>>   X : " + arrX [0] + "/" + arrX [1] + " ,   Y : " + arrY [0] + "/" + arrY [1]);
       // Ag.LogDouble ("  AgTile :: Divide >>>   X : " + arrX[0] + "/"+ arrX[1] + " ,   Y : " + arrY[0] + "/"+ arrY[1]);
    }

    public void GetUnitSize (out float pX, out float pY)
    {
        pX = (urVec.x - llVec.x) / eaX;
        pY = (urVec.y - llVec.y) / eaY;
    }

    public void GetSequenceNumOf (int seq, out int idX, out int idY)
    {
        int row = mVert ? seq / eaX : seq / eaY, col = mVert ? seq % eaX : seq % eaY;

        if (mVert) {
            idY = row;
            idX = !mReverse ? col : eaX - col - 1;
        } else {
            idX = row;
            idY = !mReverse ? col : eaY - col - 1;
        }  
        //Ag.LogDouble (seq.LogWith ("seq") + idX.LogWith ("idX") + idY.LogWith ("idY") + mReverse.LogWith ("Reverse"));
    }
    //  ////////////////////////////////////////////////     >>>>>   Reverse Apply Methods   <<<<<
    public Vector3 GetCenter (int pX, int pY, bool pRevApplied = true)
    {
        //Ag.LogDouble ("  AgTile :: GetCenter  >>>  " + pX.LogWith ("pX") + pY.LogWith ("pY"));
        //Ag.LogDouble ("  AgTile :: GetCenter  >>>  " + ((arrX [pX] + arrX [pX + 1]) * 0.5f) + " ,  " + ((arrY [pY] + arrY [pY + 1]) * 0.5f));
        if (pRevApplied && mReverse) {
            if (mVert)
                pX = eaX - pX - 1;
            else
                pY = eaY - pY - 1;
        }
        return new Vector3 ((arrX [pX] + arrX [pX + 1]) * 0.5f, (arrY [pY] + arrY [pY + 1]) * 0.5f, 0);
    }

    public Vector3 GetCenterWithOffset (int pX, int pY, float pDist, bool pVert)
    {
        //Ag.LogDouble ("  AgTile :: GetCenter  >>>  " + ((arrX [pX] + arrX [pX + 1]) * 0.5f) + " ,  " + ((arrY [pY] + arrY [pY + 1]) * 0.5f));
        Vector3 rV = GetCenter (pX, pY);
        return rV.Move (pVert, pDist);
    }
    //  ////////////////////////////////////////////////     >>>>>   NO !! Reverse Apply Methods   <<<<<
    public bool IsInside (Vector3 pVec)
    {
        return arrX [0] <= pVec.x && pVec.x <= arrX [arrX.Length - 1] &&
            arrY [0] <= pVec.y && pVec.y <= arrY [arrY.Length - 1];
    }

    public bool IsInside (Vector3 pTar, out int idxX, out int idxY) // Just Position .. X, Y index .
    {
        idxX = idxY = -1;
        for (int k=0; k < eaX; k++) {
            if (arrX [k] <= pTar.x && pTar.x <= arrX [k + 1])  // <=  <= .. pick the last case ...
                idxX = k;
        }
        for (int k=0; k < eaY; k++) {
            if (arrY [k] <= pTar.y && pTar.y <= arrY [k + 1])  // <=  <= .. pick the last case ...
                idxY = k;
        }
        if (idxX >= 0 && idxY >= 0)
            return true;
        return false;
    }

    void GetLimitInScrollDirection (out float Min, out float Max)
    {
        Min = mVert ? Ymin : Xmin;
        Max = mVert ? Ymax : Xmax;
    }

    public bool? IsFrameOutsideMinCase()
    {
        if (MinVal < FrameMin) 
            return true; // Min Case ..
        if (FrameMax < MaxVal)
            return false;
        return null;
    }

    public float GetLength2Inside()
    {
        if (MinVal < FrameMin) 
            return MinVal - FrameMin;
        if (FrameMax < MaxVal)
            return MaxVal - FrameMax;
        return 0;
    }

//
//    public bool xxReturn2Limit_Done ()
//    {
//        Ag.LogString ("  AgTile :: GetReturnVectorAndReturn  ");
//
//        float wagu, cont;
//
////        if (mVert) {
////            wagu = bOutOfMin.Value ? Ymin : Ymax;
////            cont = bOutOfMin.Value ? mFrame.Ymin : mFrame.Ymax;
////        } else {
////            wagu = bOutOfMin.Value ? Xmin : Xmax;
////            cont = bOutOfMin.Value ? mFrame.Xmin : mFrame.Xmax;
////        }
//        cont += mSetBack;
//
//        float curDist = (wagu - cont) * (10f / 200);
//
//        //if (Mathf.Abs (curDist) < myOption.minFlybackDistance)
//        //  curDist *= 2;
//
//        //DoScroll (new Vector3 (curDist, curDist, 0));
//        if (Mathf.Abs (curDist) < 0.01f) {
//            //DoScroll (new Vector3 (2 * curDist, 2 * curDist, 0));
//            return true;
//        } else 
//            return false;             
//    }

    public int GetIndex (int pX, int pY)
    {
        return pY * eaX + pX;
    }
}
