// [2012:11:09:MOON] ExpandDirection 40 -> 13 40 13 = 66
// [2012:11:09:LJK] Random Item Acc Good Perfect  modified 
// [2012:11:13:MOON] 
// [2012:11:13:ljk] ExpandDirection 40 -> 13 40 13 = 66
// [2012:11:20:MOON] 
// [2013:7:22:MOON] Enchant ..  mEnchantLevel ..
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;

public partial class AmPlayer : AmObject
{
    //  ////////////////////////////////////////////////     Variables
    public int mSerialNo, mPlayerUNO, mTalent, mPUno, mPrice, mPowerLevel, mEnchantLevel = 0;
    public byte mBackNo, mItemCount, mOrder;
    public bool mIsKicker;
    // Unity Objects
    public Transform mPoligon;
    public ArrayList arrTexture = new ArrayList ();
    // Shirt, Pants, Socks. arrTexture[0] //
    public GameObject mPoly, mRotPoly;
    public Texture2D mPlayerPic, mPlayerDirBar, mBioRythmStateBar, mPlayerTexcoin;

    public enum mTexrSlot
    {
        SHIRT = 1,
        PANT,
        GLOVE,
        SOCK,
        SHOE}
    ;
    // Game Related.
    public ArrayList arrArea;
    public AmDirection mDirectObj;
    public byte mKickNum;
    public float mStatPerfect, mStatGood;
    // Item, Shop
    public int[] arrItem5;
    public string mpName;
    public int mBio, mBioPeriod, mBioPos, mEnchant = 0;
    // -3 ~ +3
    /// /////////////////////////////////////////////////////////////////          Public Functions
    public AmPlayer ()
    {
        //SHIRT, PANT, GLOVE, SOCK, SHOE
        AmTexture newTxr = new AmTexture ();
        mDirectObj = new AmDirection ();
        mpName = "";
        arrTexture.Add (newTxr); // Shirt....0   /// Save 5 texture in arrTexture
    }

    public void ShowAdvantage ()
    {
        Ag.LogNewLine (2);
        Debug.Log ("AmPlayer :: mPosition >> LU : " + mDirectObj.mPosition [0] + " RU : " + mDirectObj.mPosition [1]
        + " LD : " + mDirectObj.mPosition [2] + " RD : " + mDirectObj.mPosition [3]);
        Debug.Log ("mDirectObj :: mWidth " + mDirectObj.mWidth [0] + ",  " + mDirectObj.mWidth [1] + ",  "
        + mDirectObj.mWidth [2] + ",  " + mDirectObj.mWidth [3] + "\n");
    }

    public void xxSetNameAndPolygon (int pPlerNum)
    {
        
        mPlayerUNO = pPlerNum;
        mPrice = 0;
        
        //Ag.LogString("pServernum : " + pPlerNum );
        
        switch (mPlayerUNO) {
        case 1: 
            mpName = "AngriKim";   
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/angri");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/angriRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Angri");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_01");
            mStatGood = 330f;
            mStatPerfect = 35f;
            break;
        case 2: 
            mpName = "BackKwon";    
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/backam");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/BackAmRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Backam");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_02");
            mStatGood = 300f;
            mStatPerfect = 34f;
            break;
        case 3: 
            mpName = "DrockGoo"; 
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/drokba");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/drokBaRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Drokba");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_03");
            mStatGood = 250f;
            mStatPerfect = 33f;
            break;
        case 4: 
            mpName = "JsooPark"; 
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/Jspark");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/JsParkRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Jspark");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_04");
            mStatGood = 330f;
            mStatPerfect = 32f; 
            break;
        case 5: 
            mpName = "KapraRyo";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/KaKa");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/KaKaRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/KaKa");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_05");
            mStatGood = 300f;
            mStatPerfect = 31f;
            break;
        case 6: 
            mpName = "KloseFar";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/klose");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/KloseRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/klose");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_06");
            mPlayerTexcoin = (Texture2D)Resources.Load ("TestItem/8000");
            mPrice = 4000;
            mStatGood = 250f;
            mStatPerfect = 33f;
            break;
        case 7: 
            mpName = "MsTimero";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/Mesi");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Kicker/MesiRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Mesi");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_07");
            mPlayerTexcoin = (Texture2D)Resources.Load ("TestItem/10000");
            mPrice = 6000;
            mStatGood = 330f;
            mStatPerfect = 30f; 
            break;
        case 8: 
            mpName = "Samsonit";
            mPoly = (GameObject)Resources.Load ("prefeb_Polygon/Kicker/Sam");
            mRotPoly = (GameObject)Resources.Load ("prefeb_Polygon/Kicker/SamRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Sam");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Bar_Drection_08");
            mPlayerTexcoin = (Texture2D)Resources.Load ("TestItem/12000");
            mStatGood = 200f;
            mStatPerfect = 36f; 
            mPrice = 8000;
            break;
        case 101:  
            mpName = "Casiyas";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Keeper/Casiyas");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Keeper/CasiyasRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Casiyas");
            mStatGood = 300f;
            mStatPerfect = 30f;
            break;
        case 102:  
            mpName = "Bupyon";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Keeper/bupyon");
            mRotPoly = (GameObject)Resources.Load ("prefab_Polygon/Keeper/buPyonRot");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/bupyon");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Manage_keeperAccurucy01");
            mPlayerTexcoin = (Texture2D)Resources.Load ("TestItem/12000k");
            mStatGood = 250f;
            mStatPerfect = 40f;
            mPrice = 8000;
            break;
        case 103:  
            mpName = "CasCasi";
            mPoly = (GameObject)Resources.Load ("prefab_Polygon/Keeper/Casiyas");
            mPlayerPic = (Texture2D)Resources.Load ("FaceShot/Angri");
            mPlayerDirBar = (Texture2D)Resources.Load ("UIsource/PlayerDir/Manage_keeperAccurucy01");
            mStatGood = 250f;
            mStatPerfect = 40f;
            mPrice = 500;
            break;
        }
        mIsKicker = (mPlayerUNO > 100) ? false : true;        
        
        //SetBioRythmStateBar();
        // SetRandomlyDirectionObj();
        
    }

    public void ShowMyself ()
    {
        ("AmPlayer :: Show Myself ..  >>> Player " + mpName + " ,   UNO : " + mPlayerUNO + " ,   SN : " + mSerialNo).HtLog ();
        ("\t \t Order : " + mOrder + " ,  Back NO : " + mBackNo + " , Power Level : " + mPowerLevel).HtLog ();

        mDirectObj.ShowLevel ();

        ("\t \t Enchant Level : " + mEnchantLevel).HtLog ();
    }
    /*  
    public void SetBioRythmStateBar () {
        if (mBio == -3) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_-3") ;
        if (mBio == -2) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_-2");
        if (mBio == -1) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_-1");
        if (mBio == 0) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_0");
        if (mBio == 1) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_1");
        if (mBio == 2) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_2");
        if (mBio == 3) mBioRythmStateBar = (Texture2D)Resources.Load ("UIsource/LogIn/Manage_Stat_Arrow_3");
    }
    */
    /// /////////////////////////////////////////////////////////////////          ExpandDirection Event Item.....
    /// // [2012:11:09:MOON] ExpandDirection 40 -> 13 40 13 = 66
    public void ExpandDirection ()
    {
        mDirectObj.ExpandSmallDirectionBar ();
        SetDirectionArea ();
    }

    public void ReduceDirection ()
    {
        mDirectObj.ReduceSmallDirectionBar ();
        SetDirectionArea ();
    }

    /// /////////////////////////////////////////////////////////////////          New Game Position Setting....
    public void SetDirectionArea ()
    {
        arrArea = new ArrayList ();
        ArrayList smlArr = new ArrayList ();
        
        int sta = 0, width, sml = 0;
        for (int ij = 0; ij < 4; ij++) {
            width = mDirectObj.mWidth [ij];
            if (width >= 200) {
                arrArea.Add (new int[] { ij + 1, sta, sta + width });   // { 1~4 , 20, 40 }
                sta += width;
            } else {
                smlArr.Add (new int[] { ij + 1, 0, width });   // { 1~4 , 0, 5 }
            }
        }
        
        if (Ag.mIsDebug) {
            Ag.LogString ("  SetDirectionArea :: >>> arrArea ...  ");
            ShowArrArea ();
        }

        // 4 Debugging......
        //for (int ij=0; ij<smlArr.Count; ij++) {
        //int[] curVal = (int[]) smlArr[ij]; 
        //Debug.Log("Dir: <<< " + curVal[0] + " >>> \t Starts: " + curVal[1] + ",   \t Ends: " + curVal[2] + "\n" );        }
        
        // Insert Small Parts...
        int smlNum = 4 - arrArea.Count;
        sml = 0;
        for (int ij = 0; ij < smlNum; ij++) {
            int[] curVal = (int[])smlArr [ij];
            int wid = curVal [2];
            int start = mDirectObj.mPosition [sml], end = start + wid; // 좁은 영역의 위치. 시점 (center 아님)
            //Ag.LogString("sta " + start + " end " + end + " wid " + wid);
            //InsertSmallAreaWithMiss (curVal [0], start, end);
            sml++;
            //int sPo = mDirectObj.mWidth 
        }
        
        if (Ag.mIsDebug)
            ShowArrArea ();
        
    }

    public int GetSkillItem ()
    {
        
        if (arrItem5 == null)
            return 0;  // [2012:11:13:MOON] 
        
        for (int jk = 0; jk < 5; jk++) {
            if (0 < arrItem5 [jk] && arrItem5 [jk] <= 6)
                return arrItem5 [jk];
        }
        return 0;
    }

    public void InsertSmallArea (int pDir, int pSta, int pEnd)
    {
        Ag.LogString (" >>>>>   InsertSmallArea  <<<<<    Value: " + pDir + " Sta: " + pSta + " End: " + pEnd);

        int staIdx = GetDirIndexOfArea (pSta), endIdx = GetDirIndexOfArea (pEnd); // 시점과 종점이 걸친 arrArea 의 Idx
        int[] staObj = (int[])arrArea [staIdx]; // 해당 객체 { D, 시점, 종점 }
        int[] endObj = (int[])arrArea [endIdx]; 

        if (endIdx - staIdx > 1) { // Contain Case.... Will never happen.... 세 영역에 걸쳐 분포.. 절대 없음
            arrArea.RemoveAt (staIdx + 1);
        }

        if (staIdx == endIdx) { // Insert Case...
            arrArea.RemoveAt (staIdx);
            arrArea.Insert (staIdx, new int[] { staObj [0], staObj [1], pSta });
            arrArea.Insert (staIdx + 1, new int[] { pDir, pSta, pEnd }); // Small Area ...
            arrArea.Insert (staIdx + 2, new int[] { staObj [0], pEnd, staObj [2] });
        } else { // Step on 2 areas.. 두 영역에 걸침
            staObj[2] = pSta;
            endObj[1] = pEnd;
            arrArea.Insert (staIdx + 1, new int[] { pDir, pSta, pEnd });
        }
    }

    public void xxInsertSmallAreaWithMiss (int pDir, int pSta, int pEnd)
    {
        // DDong Area Insert ... 
        Ag.LogString (" >>>>>   InsertSmallArea  <<<<<    Value: " + pDir + " Sta: " + pSta + " End: " + pEnd);

        float cen = (pSta + pEnd) * 0.5f;
        int ddWid = 60, ddSta = (int)(cen - ddWid * 0.5), ddEnd = (int)(cen + ddWid * 0.5);

        int staIdx = GetDirIndexOfArea (ddSta), endIdx = GetDirIndexOfArea (ddEnd); // 시점과 종점이 걸친 arrArea 의 Idx
        int[] staObj = (int[])arrArea [staIdx]; // 해당 객체 { D, 시점, 종점 }
        int[] endObj = (int[])arrArea [endIdx]; 
        if (endIdx - staIdx > 1) { // Contain Case.... Will never happen.... 세 영역에 걸쳐 분포.. 절대 없음
            arrArea.RemoveAt (staIdx + 1);
        }

        if (staIdx == endIdx) { // Insert Case...
            arrArea.RemoveAt (staIdx);
            //arrArea.Insert( staIdx,   new int[] {staObj[0], staObj[1], pSta}  );
            arrArea.Insert (staIdx, new int[] { staObj [0], staObj [1], ddSta }); // Wide Area
            arrArea.Insert (staIdx + 1, new int[] { 0, ddSta, pSta }); // DDong
            arrArea.Insert (staIdx + 2, new int[] { pDir, pSta, pEnd }); // Small Area ...
            arrArea.Insert (staIdx + 3, new int[] { 0, pEnd, ddEnd }); // DDong
            arrArea.Insert (staIdx + 4, new int[] { staObj [0], ddEnd, staObj [2] });  // Wide Area
            //arrArea.Insert( staIdx+2, new int[] {staObj[0], pEnd, staObj[2]}  );
        } else { // Step on 2 areas.. 두 영역에 걸침
            staObj [2] = ddSta; // 왼쪽 영역의 종점
            endObj [1] = ddEnd; // 오른쪽 영역의 시점
            //arrArea.Insert (staIdx + 1, new int[] { pDir, pSta, pEnd });
            arrArea.Insert (staIdx + 1, new int[] { 0, ddSta, pSta }); // DDong
            arrArea.Insert (staIdx + 2, new int[] { pDir, pSta, pEnd });
            arrArea.Insert (staIdx + 3, new int[] { 0, pEnd, ddEnd }); // DDong
        }
    }

    public int GetDirIndexOfArea (int pSpot)
    { // returns 0 ~ 3... 4, 5...
        for (int ij = 0; ij < arrArea.Count; ij++) {
            int[] curVal = (int[])arrArea [ij];
            
            if (curVal [1] <= pSpot && pSpot < curVal [2])
                return ij;
        }
        return 99; 
    }

    public void ShowArrArea ()
    {
        Ag.LogIntense (3, true);
//		Debug.Log ("m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>     ShowArrArea     >>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m ");
        for (int ij = 0; ij < arrArea.Count; ij++) {
            int[] curVal = (int[])arrArea [ij];
//            Debug.Log("Dir: <" + curVal[0] + "> Starts: " + curVal[1] + ", Ends: " + curVal[2]  );
        }
        Ag.LogIntense (3, false);
    }

    /// /////////////////////////////////////////////////////////////////          Return Position 0 ~ 3, 5
    public byte GetPosition (float pCoordi)
    { 
        int i, num = arrArea.Count;

        for (i = 0; i < num; i++) {
            int[] curVal = (int[])arrArea [i];
            
            if (curVal [1] <= pCoordi && pCoordi < curVal [2]) {
                return (byte)curVal [0];
            }
            
        }
        return 0;
    }
    //  ////////////////////////////////////////////////     Get Color of Direction..
    public int GetDirectionColor (bool pSkill, int pIdx)
    {
        //Debug.Log("Get Color :: Det Skill  " + mCursorRef.IsDetSkill() + " %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% ");
        //if ( pSkill ) {
        switch (pIdx) {
        case 0:
            return 1;
        case 1:
            return 2;
        case 2:
            return 3;
        case 3: 
            return 4;
            
        }
        //}
        return 0;
    }
    //  ////////////////////////////////////////////////     Return the Nth area Direction..
    private int[] GetMaxValue (ArrayList pArray)
    {
        int[] rVal = { 1, 100 }, curVal;
        int i, num = pArray.Count, maxVal = 0;
        
        for (i = 0; i < num; i++) {
            curVal = (int[])pArray [i];
            if (maxVal < (int)curVal [1]) {
                maxVal = (int)curVal [1];
                rVal = curVal;
            }
        }
        return rVal;
    }

    /// /////////////////////////////////////////////////////////////////          New Game Skill Setting....
    public float mPerfect, mGood;

    public void SetSkillPositions (bool pDidBuyPotion, bool pIsPerfectOn, bool pEventPotion, bool pMinusPotion)
    {//[2012:11:09:LJK] Random Item Acc Good Perfect  modified 
        arrArea = new ArrayList ();
        
        Ag.LogIntense (10, true);
        Debug.Log ("m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>     SetSkillPositions     >>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m ");
        Debug.Log ("SetSkillPositions        CurPlayer UNO : " + mPlayerUNO + "       Potion_ " + pDidBuyPotion + "       Perfect On_ " + pIsPerfectOn);
        
        switch (mPlayerUNO) {
        //case 0:     good = 20f; better = .5f;     break;
        case 1:
            mGood = 330f;
            mPerfect = 35f;
            break;
        case 2:
            mGood = 300f;
            mPerfect = 34f;
            break;
        case 3:
            mGood = 250f;
            mPerfect = 33f;
            break;
        case 4:
            mGood = 330f;
            mPerfect = 32f;
            break;
        case 5:
            mGood = 300f;
            mPerfect = 31f;
            break;
        case 6:
            mGood = 250f;
            mPerfect = 33f;
            break;
        case 7:
            mGood = 330f;
            mPerfect = 30f;
            break;
        case 8:
            mGood = 200f;
            mPerfect = 36f;
            break;
        case 101:
            mGood = 300f;
            mPerfect = 30f;
            break;
        case 102:
            mGood = 250f;
            mPerfect = 35f;
            break;
        default:
            mGood = 300f;
            mPerfect = 30f;
            break;
        }
        
        // Bio Process.....
        mPerfect += mBio * 2;
        mGood *= (1f + (mBio * 0.05f));
        
        // Item..  1, 2, 3, 4, 5, 6...  good 5, 10, 15%, 
        int sklItem = GetSkillItem ();
        
        switch (sklItem) {
        case 1:
            mGood *= 1.05f;
            mPerfect *= 1.3f;
            break; // Keeper  += 5f
        case 2:
            mGood *= 1.10f;
            mPerfect *= 1.42f;
            break;
        case 3:
            mGood *= 1.15f;
            mPerfect *= 1.55f;
            break;
        case 4:
            mGood *= 1.05f;
            mPerfect *= 1.3f;
            break; // Kicker
        case 5:
            mGood *= 1.10f;
            mPerfect *= 1.42f;
            break;
        case 6:
            mGood *= 1.15f;
            mPerfect *= 1.55f;
            break;
        }
        
        // Potion...
        if (pDidBuyPotion) {
            mGood *= 1.15f;
            mPerfect += 30f;
        } //[2012:11:09:LJK] Random Item Acc Good Perfect  modified 
        if (pEventPotion) {
            mPerfect += 20f;
        }//[2012:11:09:LJK] Random Item Acc Good Perfect  modified 
        if (pMinusPotion) {
            mPerfect -= 15f;
        }

        if (!pIsPerfectOn && Ag.mgIsKick)
            mPerfect *= 0.5f; 
        
        float cen = 300f, sta = cen - mGood * 0.5f;
        arrArea.Add (new int[] { 1, (int)sta, (int)(sta + mGood) });  // Save || Kick
        
        Ag.LogString ("mPerfect  " + mPerfect + "   mGood  " + mGood);
        Ag.LogString ("Item ? " + sklItem + "    Potion ? " + pDidBuyPotion);
        
        if (pIsPerfectOn || mPlayerUNO > 100)  // @ DrawGameSkill  ==> Remove perfect bar....  Only Kicker..
            InsertSmallArea (2, (int)(cen - 0.5f * mPerfect), (int)(cen + 0.5f * mPerfect));   // Miracle Save || Super Kick
		
        Debug.Log ("m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>     SetSkillPositions     >>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m ");
        //InsertSmallArea( 2, (int)(cen - 1), (int)(cen + 1) );   // Miracle Save || Super Kick
        if (Ag.mIsDebug)
            ShowArrArea ();
    }
}
