using UnityEngine;
using System;
using System.Collections;
using System.Timers;
using System.Collections.Generic;


public class BpSolit // Solitaire  Brain Poket
{
    public bool IsVisible;

    public Vector3 Posi;
    public Quaternion Rota;

    // Total : 13 * 4 = 52 .. Jake ..
    // Shape : CDHS

    // Actions ..

    // Explode ..  min distance, position & rotation, select action.

    public BpSolit ()
    {

    }



}

public class MtCompact // Math Compact
{
    UInt64 theVal;
    public UInt64 BaseVal, CurN;
    public List<int> arrValues = new List<int> ();

    public UInt64 MaxNum { get { return (UInt64)(Math.Log ((double)UInt64.MaxValue, (double)BaseVal)); } }

    public MtCompact (int pLimit)
    {
        BaseVal = (UInt64)pLimit;

        Ag.LogString ("  LimitVal : " + BaseVal + "  MaxNum : " + MaxNum + "  UInt64.Max : " + UInt64.MaxValue + "  " + long.MaxValue);
    }

    UInt64 Power (UInt64 nn)
    {
        if (nn == 0)
            return 1;
        UInt64 rVal = BaseVal;
        for (UInt64 k = 1; k < nn; k++) {
            rVal *= BaseVal;
        }
        return rVal;
    }

    public bool AddNum (int pVal)
    {
        UInt64 pV = (UInt64)pVal;
        if (pV >= BaseVal || MaxNum == CurN)
            return false; // error
        theVal += (UInt64)(Power (CurN) * pV);
        CurN++;
        //Ag.LogString (" AddNum ::  theVal >> " + theVal);
        return true;
    }

    public int GetNth (int pN)
    {
        UInt64 pNth = (UInt64)pN;
        UInt64 nn = Power (pNth);
        //Ag.LogString ("  Get Nth >>>    " + pN + "   nn : " + nn);
        UInt64 rVal = theVal - (theVal % nn);
        //Ag.LogString ("     Get Nth >>>   rVal " + rVal + "   (rVal / nn) : " + (rVal / nn) + "   (nn * LimitVal) : " + (nn * LimitVal));
        return (int)((rVal / nn) % BaseVal);
    }

    public void ParseSelf (int pNum = 0)
    {
        arrValues.Clear ();
        if (pNum == 0)
            pNum = GetNth (0);
        for (int k = 1; k < pNum; k++) {
            arrValues.Add (GetNth (k));
            Ag.LogString ("  ParseSelf >>>  " + k + "   value : " + GetNth (k));
        }
    }
}