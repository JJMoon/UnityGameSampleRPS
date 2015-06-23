using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;


public class VecRot
{
    public Vector3 Ae, Kugi, Dora;
    public string mObjName;
    public int mNum = 0;
    
    public VecRot()
    {
    }

    public VecRot(float x, float y, float z) {
        SetAe (x, y, z);
    }
    public VecRot(float x, float y, float z, float rx, float ry, float rz)
    {
        SetAe (x, y, z);
        SetDora(rx, ry, rz);
    }

    VecRot NewObjectFromMyself(float pXdiff, float pYdiff)
    {
        VecRot rVal = new VecRot ();
        rVal.Ae = Ae.MoveXYZ (pXdiff, pYdiff, 0);
        rVal.Kugi = Kugi;
        rVal.Dora = Dora;

        //(" NewObjectFromMyself  Kugi " + rVal.Kugi.ToString ()).HtLog ();

        return rVal;
    }

    void SetAe(float x, float y, float z) {
        Ae = new Vector3 (x, y, z);
    }
    
    void SetDora(float rx, float ry, float rz){
        Dora = new Vector3 (rx, ry, rz);
    }
    
    public VecRot(float x, float y, float z, float rx, float ry, float rz, float kx, float ky, float kz, string pName = "")
    {
        mObjName = pName;
        SetAe (x, y, z);
        SetDora(rx, ry, rz);
        Kugi = new Vector3 (kz, ky, kz);
    }
    
    public void SetOriginAe(float x, float y, float z)
    {
        Ae = new Vector3 (x, y, z);
    }
    
    public void ApplyVector(float? pX, float? pY, float? pZ)
    {
        Vector3 rVec = Ae;
        if (pX != null)
            rVec.x += pX.Value;
        if (pY != null)
            rVec.y += pY.Value;
        if (pZ != null)
            rVec.z += pZ.Value;
        //(" NewVec : " + rVec.ToString () + "  Ae : " + Ae.ToString ()).HtLog ();
        Ae = rVec;
    }

    // Eye Related ...
    public VecRot GetEyeTargetFromHon()
    {
        //(" VecRot :: GetEyeTargetFromHon   " + mObjName + " ,  mNum : " + mNum ).HtLog(); 
        if (mObjName == "EyeSingle") {
            if (mNum < 1) {
                mNum++;
                return this;
            } else {
                mNum = 0;
                return null;
            }
        }
        if (mObjName == "EyeDouble") {
            if (mNum < 2) {
                VecRot rVal;
                if (mNum == 0)
                    rVal = NewObjectFromMyself( -0.2f, 0);
                else
                    rVal = NewObjectFromMyself(  0.2f, 0);
                mNum++;
                return rVal;
            } else {
                mNum = 0;
                return null;
            }
        }
        if (mObjName == "EyeTriple") {
            if (mNum < 3) {
                VecRot rVal = null;
                if (mNum == 0)
                    rVal = NewObjectFromMyself( -0.2f, 0);
                if (mNum == 1)
                    rVal = NewObjectFromMyself(  0.2f, 0);
                if (mNum == 2)
                    rVal = NewObjectFromMyself(  0, 0.2f);
                mNum++;
                return rVal;
            } else {
                mNum = 0;
                return null;
            }
        }

        return null;
    }
    
    
}

