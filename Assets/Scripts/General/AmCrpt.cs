using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AmCrpt 
{
    UInt32 intVal { get; set; }

    List<int> arrCrpt;
    string strInt;
    int mFactor;

    public AmCrpt(UInt32 pVal)
    {
        mFactor = AgUtil.RandomInclude (3, 7); 
        //Ag.LogDouble ("mFactor is  :: " + mFactor);

        Dlgt_V_Int rNum = () => AgUtil.RandomInclude(3, 500); // Fake List .. Use for Nothing..
        arrCrpt = new List<int>() { rNum(), rNum(), rNum(), rNum(), rNum(), rNum(), rNum(), rNum(), rNum(), rNum() };

        string val = pVal.ToString ();
        int lengVal = val.Length;
        for (int k=0; k< (10 - lengVal); k++) {
            val = "0" + val;
            //Ag.LogDouble(" val :: " + val);
        }
        strInt = "";

        for (int k=0; k<10; k++) { // Random 3Character 
            int rdNum = AgUtil.RandomInclude(3, 50);
            string rndStr = "sdjl23kjas;dlkflasdfsadfsdfewqewroiwequroqweuoiewlkkwlksdflsadlflkasdflkasj,xcm,xzcmv,asd,as,df,slkewlk3io2weiodslfasdl".Substring(rdNum, 3);
            strInt += CriptInt(val.Substring(k, 1));
            strInt += rndStr;
            //Ag.LogDouble( " strInt :: >>" + strInt );
        }
        Ag.LogDouble( " strInt :: >>" + strInt );
    }

    string CriptInt(string pStrInt)
    {
        int val = int.Parse (pStrInt);
        val = (val + 4) * mFactor;
        if (val < 10)
            return "0" + val.ToString ();
        else
            return val.ToString ();
    }

    public UInt32 GetIntValue()
    {
        UInt32 rVal = 0;
        for (int k=0; k<10; k++) {
            string figStr = strInt.Substring(k*5, 2);
            int fig = int.Parse(figStr);
            if (fig == 0)
                return 0;
            rVal += (UInt32)( ((fig / mFactor) - 4) * Math.Pow (10, (9-k)) );
            //Ag.LogDouble( " Cur Val " + rVal + " with k " + k);
        }
        return rVal;
    }


}