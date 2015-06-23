/* [2013:11:4:MOOON]
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AgUtilGame
{
    public static bool DidKickerWinThisTurn (int kickDir, int kickSkl, int defnDir, int defnSkl)
    {

        // 5 / 3  1 / 1
        Ag.LogDouble(" AgUtilGame   ::   DidKickerWinThisTurn >>  Kick  :  " + kickDir + " / " + kickSkl + "    Keep  :  " + defnDir + " / " + defnSkl);

        defnSkl = defnSkl > 2 ? 2 : defnSkl;

        if (kickSkl == 3 && kickDir != 5) {  // Kick 만 3이 있다. // 140204
            if (kickDir > 0)
                return true;
            else
                return false;
        }

        //  Kick Result Matrix ... 1: Goul, 2: No goul, 3: Special case..
        byte[,,] resultMat = new byte[2, 3, 3] {
            { { 2, 1, 1 }, { 2, 2, 1 }, { 2, 2, 2 } }, // Same Direction  // { Miss }  { Normal }  { Miracle } 
            { { 2, 1, 1 }, { 2, 1, 1 }, { 2, 3, 1 } }  // Dfrn Direction  // { Miss }  { Normal }  { Miracle } 
        }; // Different Direction
        //{ ddong, normal, super.. kick }

        // Kicker's DDong ball Case ....  No goul...
        if (kickDir == 0 || kickSkl == 0) // 키커의 똥볼 처리.
            return false; // ResultSub (2);

        // Check Panenka Kick
        if (kickDir == 5) {  // skill is 1 ...
            if (defnDir != 0 && defnSkl == 2)
                return true; // ResultSub (1);  // Goul..
            else
                return false; // ResultSub (2);  // No Goul..
        }

        // Kick is Valid // 똥볼 없슴.
        if (defnDir == 0 || defnSkl == 0)
            return true; // 막는게 똥이면 골. !!

        // Use Kick Result Matrix ...
        int nRes = resultMat [kickDir == defnDir ? 0 : 1, defnSkl, kickSkl];

        //Ag.LogString (" nRes in Matrix ::  " + nRes);

        if (nRes == 3) {
            if (kickDir % 2 == defnDir % 2)
                return false; // nRes = 2; // No goul
            else
                return true; // nRes = 1; // Goul in.
        }
        if (nRes == 1)
            return true;
        if (nRes == 2)
            return false;

        if (kickDir == 0)
            return false; // nRes = 2; // kick fail..

        Ag.LogIntenseWord (" !!!!!    THIS MESSAGE SHOULD !! NOT !! BE SHOWN !!!!!!    The Result is Wrong !!!");
        return false;
    }


}