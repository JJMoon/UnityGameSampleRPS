using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// 
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  CuTouch  _____  Class  _____
//
//
public class CuTouch : AmObject
{
    // Events
    public DlgtVect3 evntTouchDown, evntTouchOnce, evntHold, evntHoldMove, evntDoubleTouch, evntTouchUp, evntTooFast, evntInitFly;
    public DlgtVec3Vec3 evntDrag;
    public DlgtVectRvect evntFlying;
    // Private Members
    CuUiOption mOption;
    List<Vector3> arrTouch = new List<Vector3> ();
    Vector3 mCurPo, mPrvPo, mFlyingVect;
    float mAccumDist;
    bool IamHolding;

    public bool IsTouchDown { get; private set; }

    public CuTouch (CuUiOption pOpt)
    {
        mOption = pOpt;

        evntTouchOnce += (pV) => {
            Ag.LogDouble ("Event Touch Once " + pV.LogWith ("Vect")); };

        evntHold += (pV) => {
            Ag.LogIntenseWord ("  33 : [ CuTouch :: { _____     evntHold     _____ } ]"); };
        evntHoldMove += (pV) => { }; // "HoldMove".HtLog(); };
        //  ("Event evntHoldMove " + mAccumDist.LogWith ("Accum")).HtLog (); };
        //evntDrag += (pV, pCo) => { ("Event Drag " + pV.LogWith ("Vect")).HtLog (); };
        evntTouchUp += (pV) => { }; //("Event U P " + pV.LogWith ("Vect")).HtLog (); };
        evntTooFast += (pV) => {
            ("Event Too Fast " + mOption.optSpdLimit.LogWith ("SpdOption")).HtLog (); };
        evntFlying += pS => {
            return mFlyingVect;
        };
    }

    public void Empty (Vector3 pVect)
    {
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  CuTouch  _____  Call  _____
    public void TouchDown (Vector3 pCo)
    {
        IsTouchDown = true;
        IamHolding = false;
        mPrvPo = mCurPo = pCo;
        mCnt = 0;
        mAccumDist = 0f;
        arrTouch.Clear ();
        arrTouch.Add (pCo);
        evntTouchDown (pCo);
    }

    public void TouchHold (Vector3 pCo)
    {
        arrTouch.Add (pCo);
        mCurPo = pCo;

        Vector3 dir = (mCurPo - mPrvPo);

        if (IamHolding) {
            evntHoldMove (pCo);
            mPrvPo = mCurPo;
            return;
        }

        if (Vector3.zero != dir) {
            mAccumDist += Vector3.Magnitude (dir);
            evntDrag (dir, pCo);
            if (SpeedLimit ())
                evntTooFast (pCo);
        }
        //Ag.LogString (mCnt.LogWith ("Cnt") + mOption.optHoldLimit.LogWith ("Limit") + mAccumDist.LogWith ("Dist") + mOption.optSelectionDist.LogWith ("DistLimit"));
        if (mCnt++ > mOption.optHoldLimit && mAccumDist < mOption.optSelectionDist) { // Selection ..)
            evntHold (pCo); // Send Once ..
            IamHolding = true;
        }
        mPrvPo = mCurPo;
    }

    public void TouchUp ()
    {
        IsTouchDown = false;
        if (mAccumDist < mOption.optSelectionDist && mCnt < mOption.optHoldLimit)
            evntTouchOnce (mCurPo);
        else 
            evntTouchUp (mCurPo);

        if (arrTouch.Count < 1) 
            mFlyingVect = Vector3.zero;
        else
            mFlyingVect = arrTouch.Count > 5 ? 
                (arrTouch [arrTouch.Count - 1] - arrTouch [arrTouch.Count - 6]) / 5 : 
                (arrTouch [arrTouch.Count - 1] - arrTouch [0]) / arrTouch.Count;

        evntInitFly (mFlyingVect);
    }

    public void UpdateAction ()
    {
        //        if (mTchState)
        //            return;
        //        if (mOption.optFlyingSpdMin < Vector3.Magnitude (mFlyingVect)) {
        //            (" CuTouch :: UpdateAction  " + Vector3.Magnitude (mFlyingVect).LogWith ("Magnitude")).HtLog ();
        //            mFlyingVect = evntFlying (mFlyingVect);
        //        }
    }

    public void AddTouchOnceAction (DlgtVect3 pMethod)
    {
        evntTouchOnce += pMethod;
    }

    bool SpeedLimit ()
    {
        //float delta = mPrvPo.Distance3D (mCurPo);
        float delta = Vector3.Distance (mPrvPo, mCurPo);
        if (Mathf.Abs (delta) > mOption.optSpdLimit) {
            Ag.LogString (" CuTouch ::  __________  Too Fast  !!!!   _____ ");
            return true;
        }
        return false;
    }
}