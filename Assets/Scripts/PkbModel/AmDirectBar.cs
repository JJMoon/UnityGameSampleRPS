using System;
using System.Collections;
using System.Collections.Generic;

public class AmDirectBar : AmObject
{
    //  ////////////////////////////////////////////////     Struct || DirectUnit
    class DirectUnit
    {
        string kind; // Fake, Wide, Narr ..
        public int mDirection; // 0 ~ 5 .. -1 is fake..
        AmCrpt startE, widthE;

        public UInt32 startErpt { 
            get { return startE.GetIntValue (); }
            set { startE = new AmCrpt (value); }  
        }

        public UInt32 widthErpt { 
            get { return widthE.GetIntValue (); }
            set { widthE = new AmCrpt (value); }  
        }

        public DirectUnit (UInt32 start, UInt32 width, int direction)
        {
            startErpt = start;
            widthErpt = width;
            mDirection = direction;
        }

        public DirectUnit (bool pIsFake)
        {
            if (pIsFake)
                SetGabage ();
        }

        public void SetGabage ()
        {
            startErpt = (UInt32)AgUtil.RandomInclude (100, 800);
            widthErpt = (UInt32)AgUtil.RandomInclude (100, 500);
            mDirection = -1;
        }
    }

    // Member Variables ...
    List<DirectUnit> arrDirect; //, arrFake1, arrFake2;

    public AmDirectBar ()
    {
        arrDirect = new List<DirectUnit> ();
//        arrFake1 = new List<DirectUnit> ();
//        arrFake2 = new List<DirectUnit> ();
    }

    public void xxAddPositionAndWidth (uint pStart, uint pWidth, int pDirect)
    {
        //AddFake ();
        DirectUnit newObj = new DirectUnit (pStart, pWidth, pDirect);
        if (pWidth > AgStt.NarrowBarLimit)
            arrDirect.Add (newObj);  // Wide Area is put behind.
        else
            arrDirect.Insert (0, newObj); // Narrow Area is put at front 
    }

//    void AddFake ()
//    {
////        arrDirect.Add (new DirectUnit (true));
////        arrFake1.Add (new DirectUnit (true));
////        arrFake2.Add (new DirectUnit (true));
////        arrFake1.Add (new DirectUnit (true));
////        arrFake2.Add (new DirectUnit (true));
//    }

    public int GetDirection(int thePosition)
    {
        if (thePosition < 0 || thePosition > 1000)
            return -1;
        foreach (DirectUnit curObj in arrDirect) {
            if(curObj.mDirection < 0) 
                continue;
            UInt32 sta = curObj.startErpt, end = sta + curObj.widthErpt;
            if (sta <= thePosition && thePosition < end)
                return curObj.mDirection;
        }
        return -1;
    }

    public int[] GetPositionAndWidth (int pNth)
    { // Will be called from DrawOneBar() which is in Update() ...
        int[] rVal = { 1, 3 };

        return rVal;
    }

//    public void xxExpandSmallDirectionBar ()
//    {
//
//    }
//
//    public void xxReduceSmallDirectionBar ()
//    {
//    }
//
//    public int xxGetNumOfSmallArea ()
//    {
//        return 0;
//    }
//
//    public int xxGetNewRandomPosition ()
//    {
//        return 0;
//    }
//
//    private bool xxCheckSmallDist (int pDist)
//    {
//        return true;
//    }
//
//    public byte xxMaxDirect ()
//    {
//        return (byte)0;
//    }
//
//    public byte xxPickRandomKick ()
//    {
//        return (byte)0;
//    }
//
//    public byte xxPickWideAndNarrowRight ()
//    {
//        return (byte)0;
//    }
//
//    public byte xxPickWideRandomDirect (bool pApplyWidth)
//    {
//        return (byte)0;
//    }
}

