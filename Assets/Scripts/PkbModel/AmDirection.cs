using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class AmDirection
{
    public int[] mLevel;
    public int[] mUpgrade;
    // 4ea..
    public int[] mPosition, mWidth;

    public void AiSetUpgrade ()
    {
        // Random Setting
    }

    public void ShowLevel ()
    {
        try {
            (" AmDirection ~~~ mLevel  :  " + mLevel [0] + " ,  " + mLevel [1] + " ,  " + mLevel [2] + " ,  " + mLevel [3]).HtLog ();
        } catch {
            Ag.LogIntenseWord (" AmDirection :: ShowLevel  ...   has No Level  ...   ERROR ... ");
        }
    }

    public void ShowMyself ()
    {
        Ag.LogIntense (4, true);
        "_____ Am Direction :: Show Myself _____     _____ Am Direction :: Show Myself _____".HtLog ();
        ("      mWidth         " + mWidth [0] + " , \t " + mWidth [1] + " , \t " + mWidth [2] + " , \t " + mWidth [3] + "  >>>>> ").HtLog ();
        ("      mPosition     " + mPosition [0] + " , \t " + mPosition [1] + " , \t " + mPosition [2] + " , \t " + mPosition [3] + "  >>>>> ").HtLog ();
        Ag.LogIntense (4, false);
    }
    // [2012:11:09:MOON] ExpandDirection 40 -> 13 40 13 = 66
    public void ExpandSmallDirectionBar ()
    {
        for (int i = 0; i < 4; i++) {
            if (mWidth [i] < 100) {
                mPosition [i] -= 10;
                mWidth [i] += 20;
            }
        }
    }
    // [2012:11:13:ljk] ExpandDirection 40 -> 13 40 13 = 66
    public void ReduceSmallDirectionBar ()
    {
        for (int i = 0; i < 4; i++) {
            if (mWidth [i] < 100) {
                mPosition [i] += 6;
                mWidth [i] -= 12;
            }
        }
    }

    public int GetNumOfSmallArea ()
    {
        int rVal = 0;
        for (int i = 0; i < 4; i++) {
            if (mWidth [i] < 180)
                rVal++;
        }
        return rVal;
    }

    public int GetNewRandomPosition ()
    {
        System.Random rObj = new System.Random ();
        bool checkOK = false;
        int newPosi;

        do {
            newPosi = rObj.Next (50, 900);
            checkOK = CheckSmallDist (newPosi);
        } while (!checkOK);

        return newPosi;
    }

    private bool CheckSmallDist (int pDist)
    {
        for (int i = 0; i < 4; i++) {
            int dist = Math.Abs (pDist - mPosition [i]);
            if (dist < 60)
                return false;
        }
        return true;
    }

    public byte MaxDirect (bool is2ndCase = false ) // 넓이가 최대인 방향의 정보 (1/2/3/4)를 넘김.
    {
        int maxWid = 0, mas2nd = 0;
        byte maxDir = 0;
        for (int ii = 0; ii < 4; ii++) {
            if (mWidth [ii] > maxWid) {
                maxWid = mWidth [ii];
                maxDir = (byte)(ii + 1);
            }
        }
        if (!is2ndCase)
            return maxDir;

        maxWid = 0;
        for (int ii = 0; ii < 4; ii++) {
            if (maxDir == (byte)(ii + 1))
                continue;
            if (mWidth [ii] > maxWid) {
                maxWid = mWidth [ii];
                maxDir = (byte)(ii + 1);
            }
        }

        Ag.LogString (" AmDirection :: MaxDirect >>  returns    " + maxDir);

        return maxDir; // this is 2nd Widest Area.. Index..
    }

    public byte PickRandomKick ()
    {
        //System.Random rObj = new System.Random ();
        return (byte)(AgUtil.RandomInclude(1, 4));  //rObj.Next (1, 5);
    }

    public byte PickWideAndNarrowRight ()
    {
        List<int> wideArea = new List<int> ();
        int smallIdx = 0;
        //ShowMyself ();

        for (int ii = 0; ii < 4; ii++) {
            if (mWidth [ii] >= 180) {
                wideArea.Add (ii + 1);  // add 1 || 2 || 3 || 4 || 5   ... 
            } else {
                if (mPosition [smallIdx++] > 600) {
                    wideArea.Add (ii + 1);
                    //("PickWideAndNarrowRight >> Right Small Area is  Added  " + wideArea.Last() ).HtLog();
                }
            }
        }
        //("PickWideAndNarrowRight >> Area Number considering   " + wideArea.Count ).HtLog();
        System.Random rObj = new System.Random ();
        int idx = rObj.Next (0, wideArea.Count - 1);
        return (byte)wideArea [idx];
    }

    public byte PickWideRandomDirect (bool pApplyWidth)
    {
        List<int> wideArea = new List<int> ();
        List<int> accumPosi = new List<int> ();
        int accum = 0;
        for (int ii = 0; ii < 4; ii++) {
            if (mWidth [ii] >= 180) {
                wideArea.Add (ii + 1);  // add 1 || 2 || 3 || 4 ... 

                if (pApplyWidth)
                    accum += mWidth [ii];
                else
                    accum += 200;
            } else {
                accum += mWidth [ii];
            }
            accumPosi.Add (accum);
        }

        // wideArea = [ 1, 3 ] .. 
        // mWidth = [45, 300, 45, 600] ... 
        // accumPosi = [45, 345, 390, 990 ] ...

        System.Random rObj = new System.Random ();
        int prevV = 0, ranVal = rObj.Next (0, accum); // 1000); // % wideArea.Count;

        for (int j = 0; j < 4; j++) {
            if (prevV <= ranVal && ranVal < accumPosi [j])
                return (byte)(j + 1);
            prevV = accumPosi [j];
        }
        return (byte)2;
    }
}
