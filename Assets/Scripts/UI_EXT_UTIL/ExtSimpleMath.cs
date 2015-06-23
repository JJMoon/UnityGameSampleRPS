using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtSimpleMath
{
    public static void DivideMinSec (this int pSeconds, out int min, out int sec)
    {
        min = pSeconds / 60;
        sec = pSeconds % 60;
    }

    public static int GetBigger (this int pThis, int pThat)
    {
        if (pThis > pThat)
            return pThis;
        return pThat;
    }
    /// <summary>
    /// 작은 값을 리턴. 같은면 후자.
    /// </summary>
    /// <returns>The smaller.</returns>
    public static int GetSmaller (this int pThis, int pThat)
    {
        if (pThis < pThat)
            return pThis;
        return pThat;
    }

    public static bool IsBiggerThan (this int pFirst, int pSecond)
    {
        return pFirst > pSecond;
    }

    public static bool IsBiggerThan (this float pFirst, float pSecond)
    {
        return pFirst > pSecond;
    }

    public static bool GetRandomTrue (this System.Random rand, int pTruePercent)  // 0 ~ 100
    {
        int numm = AgUtil.RandomInclude (0, 99); //  rand.Next (0, 100);
        //("Get Random True ::  " + numm).HtLog ();
        if (numm < pTruePercent)
            return true;
        return false;
    }

    public static int GetEAofMatch (this int[] pInt, int pVal, int pLimit = -1)
    {
        int rVal = 0;
        pLimit = pLimit == -1 ? pInt.Length : pLimit;

        for (int k = 0; k < pLimit; k++) {
            rVal += pInt [k] == pVal ? 1 : 0;
        }
        return rVal;
    }

    public static int GetSum (this int[] pInt)
    {
        int rVal = 0;
        for (int k = 0; k < pInt.Length; k++) {
            rVal += pInt [k];
        }
        return rVal;
    }
}