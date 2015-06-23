using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtTransVect
{
    //  ////////////////////////////////////////////////     ////////////////////////     this is >>>   GameObject   <<<  related ...
    //  ////////////////////////////////////////////////     this is >>>   Transform   <<<  related ...
    public static void SetAeDora (this Transform pTarget, VecRot pVr)
    {
        pTarget.position = pVr.Ae;
        pTarget.Rotate (pVr.Dora);
    }

    public static void SetJinsimAeDora (this Transform pTarget, Transform pParen, VecRot pVr, bool pKugi = false)
    {
        pTarget.position = pParen.position + pVr.Ae;
        pTarget.Rotate (pVr.Dora);
        if (pKugi)
            pTarget.localScale = pParen.localScale.ApplyKugi (pVr.Kugi);
        else
            pTarget.localScale = pVr.Kugi;
    }

    public static void MoveBack (this Transform pTarget, float pVal)
    {
        pTarget.position = new Vector3 (pTarget.position.x, pTarget.position.y, pTarget.position.z + pVal);
    }

    public static void MoveUp (this Transform pTarget, float pVal)
    {
        pTarget.position = new Vector3 (pTarget.position.x, pTarget.position.y + pVal, pTarget.position.z);
    }

    public static void MoveSide (this Transform pTarget, float pVal)
    {
        pTarget.position = new Vector3 (pTarget.position.x + pVal, pTarget.position.y, pTarget.position.z);
    }

    public static void SetScreenPosition (this GameObject pObj, Vector3 posi, Camera cam, float zPosi)
    {
        Vector3 wrdPo = cam.ScreenToWorldPoint (posi);
        pObj.transform.position = new Vector3 (wrdPo.x, wrdPo.y, zPosi);
    }

    public static Vector3 GetScreenPosition (this Camera cam, Vector3 posi, float zPosi)
    {
        Vector3 wrdPo = cam.ScreenToWorldPoint (posi);
        return new Vector3 (wrdPo.x, wrdPo.y, zPosi);
    }

    public static void SetScaleFactor (this Transform pTarget, Vector3 pVect)
    {
        Vector3 kugi = pTarget.localScale;
        pTarget.localScale = new Vector3 (kugi.x * pVect.x, kugi.y * pVect.y, kugi.z * pVect.z);
    }

    public static void MoveXYZ (this Transform pTarget, float pX, float pY, float pZ = 0f)
    {
        pTarget.position = new Vector3 (pTarget.position.x + pX, pTarget.position.y + pY, pTarget.position.z + pZ);
    }

    public static Vector3 GetApplyVect3 (this Transform pTarget, Vector3 pVect)
    {
        return new Vector3 (pTarget.position.x + pVect.x, pTarget.position.y + pVect.y, pTarget.position.z + pVect.z);
    }

    public static Vector3 Freeze (this Vector3 pTarget)
    {
        return pTarget;
    }

    public static Vector3 AppliedDist (this Vector3 o, bool pVert, float pDist)
    {
        if (pVert)
            return new Vector3 (o.x, o.y + pDist, o.z);
        return new Vector3 (o.x + pDist, o.y, o.z);
    }

    public static void MoveXY (this Transform pTarget, float pDisp, bool pIsVertical)
    {
        Vector3 camCo = pTarget.position;
        if (pIsVertical)
            camCo.y += pDisp;
        else
            camCo.x += pDisp;
        pTarget.position = camCo;
    }

    public static void OffsetFront (this Transform pTrans, float pValue)
    {
        Vector3 cur = pTrans.position;
        ("Current z  " + cur.z).HtLog ();
        pTrans.position = new Vector3 (cur.x, cur.y, cur.z - pValue);
    }
    //  ////////////////////////////////////////////////     ////////////////////////     this is >>>   GameObject   <<<  related ...
    public static Vector3 GetScaledVect (this GameObject pGObj, float pXScale, float pYScale = 0f, float pZScale = 0f)
    {
        Vector3 scl = pGObj.transform.localScale;
        if (pYScale == 0f)
            pYScale = pXScale;
        if (pZScale == 0f)
            pZScale = pXScale;

        scl.Set (scl.x * pXScale, scl.y * pYScale, scl.z * pZScale);
        return scl;
        //return new Vector3 (scl.x * pXScale, scl.y * pYScale, scl.z * pZScale);
    }

    public static float DiffenceXY (this GameObject pFrom, GameObject pTopo, bool pIsVertical)
    {
        return pFrom.transform.position.DiffenceXY (pTopo.transform.position, pIsVertical);
    }

    public static float DistanceXY (this GameObject pFrom, GameObject pTopo, bool pIsVertical)
    {
        return pFrom.transform.position.DistanceXY (pTopo.transform.position, pIsVertical);
    }

    public static float CurrentPosition (this GameObject pObj, bool pIsVertical)
    {
        return pObj.transform.position.CurrentPosition (pIsVertical);
    }

    public static float DistanceIgnoreZ (this Vector3 pVec, Vector3 pTo)
    {
        pVec.z = pTo.z = 0;
        return Vector3.Distance (pVec, pTo);
    }

    public static GameObject GetNearestFrom (this Vector3 pObj, GameObject pExcept, Dictionary<string, GameObject> pDic)
    {
        GameObject rObj = null;
        float leng = float.MaxValue;
        //(" GetNearest   From     " + pObj).HtLog ();
        foreach (KeyValuePair<string, GameObject> kvObj in pDic) {
            if (pExcept != null && kvObj.Value == pExcept)
                continue;
            float curL = kvObj.Value.GetComponent<CuCell> ().Pstn.Targ.DistanceIgnoreZ (pObj);
            //("GetNearestFrom :: " +  kvObj.Value.GetComponent<CuCell>().Pstn.Targ + "   "  + kvObj.Value.name + curL.LogWith ("CurL") + leng.LogWith ("leng")).HtLog ();
            if (curL < leng) {
                leng = curL;
                rObj = kvObj.Value;
            }
        }
        return rObj;
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Vector3  _____  ..  _____
    public static float DiffenceXY (this Vector3 pFrom, Vector3 pTopo, bool pIsVertical)
    {
        if (pIsVertical)
            return pTopo.y - pFrom.y;
        else
            return pTopo.x - pFrom.x;
    }

    public static float DistanceXY (this Vector3 pFrom, Vector3 pTopo, bool pIsVertical)
    {
        if (pIsVertical)
            return Mathf.Abs (pTopo.y - pFrom.y);
        else
            return Mathf.Abs (pTopo.x - pFrom.x);
    }

    public static float xxDistance3D (this Vector3 pFrom, Vector3 pTopo)
    {
        //return Mathf.Sqrt (Mathf.Pow (pTopo.x - pFrom.x, 2) + Mathf.Pow (pTopo.y - pFrom.y, 2) + Mathf.Pow (pTopo.z - pFrom.z, 2));
        return Vector3.Distance (pFrom, pTopo);
    }

    public static float CurrentPosition (this Vector3 pVec, bool pIsVertical)
    {
        if (pIsVertical)
            return pVec.y;
        return pVec.x;
    }

    public static Vector3 ApplyKugi (this Vector3 pVec, Vector3 pApply)
    {
        return new Vector3 (pVec.x * pApply.x, pVec.y * pApply.y, pVec.z * pApply.z);
    }

    public static Vector3 Move (this GameObject pObj, Vector3 pVect)
    {
        pObj.transform.position += pVect;
        return pObj.transform.position;
    }

    public static Vector3 Move (this GameObject pObj, Vector3 pVect, bool? isVert)
    {
        float x, y;
        x = isVert.Value ? 0 : pVect.x;
        y = isVert.Value ? pVect.y : 0;
        //(" Ext ... x, y, z " + x + ", " + y).HtLog ();
        //("    Object :: " + pObj.transform.position).HtLog ();
        Vector3 o = pObj.transform.position;
        pObj.transform.position = new Vector3 (o.x + x, o.y + y, o.z);
        return pObj.transform.position;
    }

    public static Vector3 Move (this Vector3 pVec, bool pIsVertical, float pDist)
    {
        if (pIsVertical)
            return new Vector3 (pVec.x, pVec.y + pDist, pVec.z);
        return new Vector3 (pVec.x + pDist, pVec.y, pVec.z);
    }

    public static float AddXYZ (this Vector3 pObj) // Calculate Scale Size...
    {
        return pObj.x + pObj.y + pObj.z;
    }

    public static Vector2 Vect2 (this Vector3 pObj)
    {
        return new Vector2 (pObj.x, pObj.y);
    }

    public static Vector3 DirectVect (this Vector3 pFrom, Vector3 pTo)
    {
        return pTo - pFrom;
    }

    public static float LengthOfVector (this Vector3 pVect)
    {
        return Mathf.Sqrt (Mathf.Pow (pVect.x, 2) + Mathf.Pow (pVect.y, 2) + Mathf.Pow (pVect.z, 2));
    }

    public static Vector3 UnitVect (this Vector3 pVect)
    {
        float ll = pVect.LengthOfVector ();
        if (ll < 0.0001)            
            return new Vector3 (0, 0, 0);
        return new Vector3 (pVect.x / ll, pVect.y / ll, pVect.z / ll);
    }

    public static Vector3 ApplyLength (this Vector3 pVect, float pDist)
    {
        return new Vector3 (pVect.x * pDist, pVect.y * pDist, pVect.z * pDist);
    }
    //  ////////////////////////////////////////////////     ////////////////////////     this is >>>   Vector2   <<<  related ...
    public static Vector3 Vect3 (this Vector2 pObj, float pZ = 0)
    {
        return new Vector3 (pObj.x, pObj.y, pZ);
    }
    //  ////////////////////////////////////////////////     ////////////////////////     Angle Quartenion Eurer Angles  ...
    public static float GetBotongZ (this Quaternion mySelf)
    {
        float rV = mySelf.eulerAngles.z;
        if (rV > 180) 
            rV -= 360;
        if (rV < -180) 
            rV += 360;
        return rV;
    }
    //  ////////////////////////////////////////////////     ////////////////////////     this is >>>   Go To .. start ani..   <<<  related ...
    public static Vector3 MoveXYZ (this Vector3 pFrom, float pX, float pY, float pZ)
    {
        return new Vector3 (pFrom.x + pX, pFrom.y + pY, pFrom.z + pZ);
    }

    public static Vector3 Go2Target (this Vector3 pCur, Vector3 pTo, float pDist)
    {
        Vector3 directV = pCur.DirectVect (pTo);    // Direction ..
        directV = directV.UnitVect ();              // make it Unit Vector..
        return directV.ApplyLength (pDist);          // Apply Distance ...
    }
    //  ////////////////////////////////////////////////     ////////////////////////     this is >>>   내분점 관련 <좌표>   <<<  related ...
    public static void IntDivideRotZ (this Transform mySelf, Vector3 pToObj, float pFactor)
    {
        float curZ = mySelf.rotation.GetBotongZ ();
        float angZ = pToObj.z - curZ;

        if (Mathf.Abs (angZ) > 0.3f) 
            mySelf.Rotate (new Vector3 (0, 0, angZ * pFactor));
    }

    public static void IntDivideLimit (this Transform pFrTobj, Vector3 pToObj, float pFr, float pTo, float pLimit)
    {
        Vector3 target = pFrTobj.position.IntDivide (pToObj, pFr, pTo);
        float dist = Vector3.Distance (pFrTobj.position, target);
        if (dist < pLimit) {
            pFrTobj.position = target;
            //"dist < pLimit  case ".HtLog();
            return;
        }

        Vector3 direct = pFrTobj.position.DirectVect (pToObj);
        direct.Normalize ();
        direct *= pLimit;

        pFrTobj.position += direct;
    }

    public static void IntDivide (this Transform pFrTobj, Vector3 pToObj, float pFr, float pTo)
    {
        pFrTobj.position = pFrTobj.position.IntDivide (pToObj, pFr, pTo);
    }

    public static Vector2 IntDivide (this Vector2 pFrObj, Vector2 pToObj, float pFr, float pTo)
    {
        //("x " + (pFr * pFrObj.x + pTo * pToObj.x) / (pFr + pTo) + " , Y scale  " + (pFr * pFrObj.y + pTo * pToObj.y) / (pFr + pTo)).HtLog();
        return new Vector2 ((pFr * pFrObj.x + pTo * pToObj.x) / (pFr + pTo), (pFr * pFrObj.y + pTo * pToObj.y) / (pFr + pTo));
    }

    public static float IntDivide (this float pFromVal, float pToVal, float pFr, float pTo)
    {
        if (pFr + pTo == 0)
            return 0;
        return (pFr * pFromVal + pTo * pToVal) / (pFr + pTo);
    }

    public static Vector3 IntDivide (this Vector3 pFrObj, Vector3 pToObj, float pFr, float pTo)
    {
        //("x " + (pFr * pFrObj.x + pTo * pToObj.x) / (pFr + pTo) + " , Y scale  " + (pFr * pFrObj.y + pTo * pToObj.y) / (pFr + pTo)).HtLog();
        return (pFrObj * pFr + pToObj * pTo) / (pFr + pTo);
    }
}