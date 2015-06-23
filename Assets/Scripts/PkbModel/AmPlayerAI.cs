// [2013:3:13:MOON] AI related...
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

using System.Linq;
using System.Text;

public partial class AmPlayer : AmObject
{


    public int[] arr4Grade = new int[] { 0, 0, 0, 0 };

    public void AiSetEnchantGrade(int pGrade)  // Grade : 1 ~ 10
    {
        // Enchant Setting..
        //System.Random rObj = new System.Random();

        int min = pGrade - 2, max = pGrade + 2;
        if (min < 0)
            min = 0;
        mEnchant = AgUtil.RandomInclude (min, max);

        // Grade..
        int sum = 0;
        for (int k=0; k<4; k++) {
            //arr4Grade [k] = rObj.GetRandomChant(pGrade, pMax: 3);
            arr4Grade [k] = AgUtil.RandomInclude(0, mEnchant+2);
            sum += arr4Grade[k];
        }
        ("Amplayer :: Ai Set Enchant Grade  >>>   mEnchant : " + mEnchant + " ,  arr4Grade :  " + 
         arr4Grade[0] + ", " + arr4Grade[1] + ", " + arr4Grade[2] + ", " + arr4Grade[3] +
         "  Total :: " + (mEnchant + sum ) ).HtLog();
    }

    public byte SetKeeperDirect ( byte pBotGrade)
    {
        if (!Ag.mgIsKick) 
            return 0;
        System.Random ranObj = new System.Random();
        byte rslt = mDirectObj.PickWideAndNarrowRight();
        int dongPercent = 5;
        switch (pBotGrade) {
        case 1:
            rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // Just pick from wide area..'
            dongPercent = 30;
            break;
        case 2:
            dongPercent = 15;
            break;
        case 3:
            if (ranObj.GetRandomTrue(pTruePercent: 50))
                rslt = Ag.mgDirection;
            dongPercent = 2;
            break;
        case 4: // more than 70 % defence success...  know the enemy... 
            if (ranObj.GetRandomTrue(pTruePercent: 70))
                rslt = Ag.mgDirection;
            dongPercent = 1;
            break;
        }

        if (ranObj.GetRandomTrue(pTruePercent: dongPercent))
            rslt = 0;

        if (rslt == 5)
            rslt = 0;

        return rslt;
    }

    public byte SetKickerDirect ( byte pBotGrade)
    {
        if (Ag.mgIsKick)
            return 0;
        System.Random ranObj = new System.Random();
        byte rslt = 0;
        int dongPercent = 5;
        switch (pBotGrade) {
        case 1:
            rslt = mDirectObj.PickWideRandomDirect (pApplyWidth: false); // Just pick from wide area..'
            dongPercent = 30;
            break;
        case 2:
            rslt = mDirectObj.PickWideAndNarrowRight();
            dongPercent = 15;
            break;
        case 3:
            rslt = mDirectObj.PickRandomKick();
            dongPercent = 5;
            break;
        case 4:
            rslt = mDirectObj.PickRandomKick();
            dongPercent = 1;
            break;
        }
        
        if (ranObj.GetRandomTrue(pTruePercent: dongPercent))
            rslt = 0;
        
        return rslt;
    }

    public void SetRandomlyDirectionObj ()
    {
        ("PlayerUNo " + mPlayerUNO).HtLog ();
        if (mPlayerUNO > 100)
            return;
        
        switch (mPlayerUNO) {
        case 1:
            mDirectObj.mWidth = new int[] { 45, 45, 1000, 45 };
            break;
        case 2:
            mDirectObj.mWidth = new int[] { 700, 45, 300, 45 };
            break;
        case 3:
            mDirectObj.mWidth = new int[] { 45, 300, 45, 700 };
            break;
        case 4:
            mDirectObj.mWidth = new int[] { 200, 800, 45, 45 };
            break;
        case 5:
            mDirectObj.mWidth = new int[] { 500, 45, 45, 500 };
            break;
        case 6:
            mDirectObj.mWidth = new int[] { 200, 300, 45, 500 };
            break;  // [2012:11:20:MOON] 
        case 7:
            mDirectObj.mWidth = new int[] { 300, 45, 350, 350 };
            break;  // [2012:11:20:MOON] 
        case 8:
            mDirectObj.mWidth = new int[] { 45, 500, 300, 200 };
            break;  // [2012:11:20:MOON] 
        }

        SetRandomPosition ();
        SetDirectionArea ();

        ApplyDirectUpgrade ();

        SetDirectScaleMark ();

        // Debugging ..
        ShowArrArea ();
    }

    void ApplyDirectUpgrade()
    {
        // Set Direction Width ... 45 + alpha...
        for (int k=0; k<4; k++) {
            if (mDirectObj.mWidth[k] < 100)
                mDirectObj.mWidth[k] = 45 + arr4Grade[0] * 1;
        }
        ("AmplayerAI :: ApplyDirectUpgrade  >>>    .............................................").HtLog ();
        mDirectObj.ShowMyself ();
        ("AmplayerAI :: ApplyDirectUpgrade  >>>   mEnchant : " + mEnchant + " ,  arr4Grade :  " + 
         arr4Grade[0] + ", " + arr4Grade[1] + ", " + arr4Grade[2] + ", " + arr4Grade[3] ).HtLog();

    }
    
    private void SetRandomPosition ()
    {
        
        //int smlNum = mDirectObj.GetNumOfSmallArea();
        mDirectObj.mPosition = new int[] {0, 0, 0, 0};
        int smN = 0;
        
        for (int i=0; i<4; i++) {
            int curA = mDirectObj.mWidth [i];
            int sta = 0;
            if (curA > 180) {
                mDirectObj.mPosition [i] = sta;
                sta += curA;
            } else
                mDirectObj.mPosition [smN++] = mDirectObj.GetNewRandomPosition ();
        }
    }

    List<int> arrDirectMark = new List<int>();

    void SetDirectScaleMark()
    {
        ("SetDirectScaleMark :: "  ).HtLog();
        if (arrArea.Count == 0)  // Error
            //" if (arrArea.Count == 0)  // Error  ".HtLog ();
            return;

        arrDirectMark.Add (0);
        ("SetDirectScaleMark :: Add   >> " +  0 ).HtLog();
        for (int k=0; k < arrArea.Count; k++) {
            int[] curArea = (int[]) arrArea[k];  // 3, 15, 50 < posi, sta, end >
            int preVal = curArea[1], curVal = curArea[2];
            int width = curVal - preVal;

            (" Current Area ....  " ).HtLog();
            (" >>> " + preVal + "  ,   " + curVal + "   Width :: " + width).HtLog();

            if (width < 20)  // Too small value :: Skip
                continue;
            if (width < 100) {  // Small Area Case ...  add Center Position..
                arrDirectMark.Add( (int)((preVal + curVal)*0.5) );
                ("SetDirectScaleMark :: Add   width < 80  >> " +  preVal + " ,   " + curVal + " ,   " + (int)((preVal + curVal)*0.5) ).HtLog();
                continue;
            }
            /*
            int num = (int)( (curVal - preVal) / 50f);  // Wide Area... Add Multiple Integer.
            for(int j=1; j<num; j++) {
                arrDirectMark.Add( (int)(preVal + j * 50f));
                ("SetDirectScaleMark :: Add   >> " +  ((int)(preVal + j * 50f)).ToString() ).HtLog();
            } */

        }
        arrDirectMark.Add(1000);
        ("SetDirectScaleMark :: Add   >> " +  1000 ).HtLog();
    }

    int prev = 0;

    int GetDirectNumInArrArea(int pPosition)
    {
        foreach(int[] obj in arrArea)
        {
            if (obj[1] <= pPosition && pPosition < obj[2])
                return obj[0];
        }
        return 0;
    }

    public int GetCenterXcordInSmallArea(int pPosition)
    {
        foreach(int[] obj in arrArea)
        {
            if (obj[1] <= pPosition && pPosition < obj[2]) {
                int width = obj[2] - obj[1];
                if (width < 80)
                    return (int)((obj[2] + obj[1]) * 0.5f);
            }
        }
        return pPosition;
    }


    public int Deprecated_GetNearestScaleMark( int pVal) 
    {
        foreach (int val in arrDirectMark) {
            if (val == pVal)
                return val;
            if (prev < pVal && pVal < val) {
                if ((pVal - prev) > (val - pVal) )
                    return val;
                else 
                    return prev;
            }
            prev = val;
        }
        return -1;
    }

}
