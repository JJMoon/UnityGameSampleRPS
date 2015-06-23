using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtUtils
{
    //public static object GetCs(this GameObject pObj, Types tpe )
    //{        return pObj.GetComponent<tpe> ();    }
    public static T GetLastMember<T> (this List<T> arrObj)
    {
        if (arrObj.Count == 0)
            return default(T);
        return arrObj [arrObj.Count - 1];
    }

    public static T GetLastMemberWithCond<T> (this List<T> arrObj, Dlgt_Gen_Obj_Bool<T> dlgtCond)
    {
        T rObj = default(T);
        foreach (T obj in arrObj) {
            if (dlgtCond (obj))
                rObj = obj;
        }
        return rObj;
    }

    public static T GetMemberWithCond<T> (this List<T> arrObj, Dlgt_Gen_Obj_Bool<T> dlgtCond)
    {
        foreach (T obj in arrObj) {
            if (dlgtCond (obj))
                return obj;
        }
        return default(T);
    }

    public static void SetActiveAll<T> (this Dictionary<T, GameObject> pDic, bool pIsActive, T[] arrT)
    {
        for (int k = 0; k < arrT.Length; k++) {
            if (pDic.ContainsKey (arrT [k]))
                pDic [arrT [k]].SetActive (pIsActive);
            else
                Ag.LogString (" Key Not Found >>>>>  ERROR   >>>>>  " + arrT [k], pWichtig: true);
        }
    }

    public static bool HasAny (this List<int> pList, int val)
    {
        for (int k = 0; k < pList.Count; k++) {
            if (pList [k] == val)
                return true;
        }
        return false;
    }

    public static CuCell CellCs (this GameObject pObj)
    {
        return pObj.GetComponent<CuCell> ();
    }

    public static UILabel GetLabel (this GameObject pObj)
    {
        return pObj.GetComponent<UILabel> ();
    }

    public static void SetLabelText (this GameObject pObj, string pTxt)
    {
        pObj.GetComponent<UILabel> ().text = pTxt;
    }

    static public T GetCom<T> (this GameObject gObj) where T : Component
    {
        return gObj.GetComponent<T> ();
    }
}
