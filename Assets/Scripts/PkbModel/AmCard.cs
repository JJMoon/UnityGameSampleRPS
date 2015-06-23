using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.Text;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  Class  _____  Am Card  _____
public partial class AmCard : AmObject
{
    public ArrayList arrArea;
    public AmDirection mDirectObj;
    public float mPerfect, mGood;
    public WasCard WAS = new WasCard ();
    public AvCard ViewObj = new AvCard ();
    public int CdType, Grade;
    public List<AmCostume> arrCostumeInCard = new List<AmCostume> ();
    public int[] directionBase, skillBase;

    public bool mustUpdate { get { return WAS.mustUpdate; } set { WAS.mustUpdate = value; } }

    public bool IsSpecialCard { get { return WAS.country == "UNI"; } }

    public bool GreenDrinkOn = false;

    public int KickOrder {
        get { return WAS.KickOrder; }
        set { WAS.KickOrder = value; }
    }
    //public string Weather;
    public AmCard ()
    {
        mDirectObj = new AmDirection ();
    }

    public AmCard (int pType, int pGrade, string pWthr)// pGrade: AgUtil.RandomInclude (0, 20), pType: 5, pWthr: "CLOUD");  // Type, Grade, Weather .. setting ...
    {
        CdType = pType;
        Grade = pGrade;
    }

    public void UpdatedPerformed ()
    {
        mustUpdate = false;
    }

    public void AddScouterValue (int pDir, bool pSuccess)
    {
        ScoutObj.AddValue (pDir, pSuccess);
        WAS.scouter = ScoutObj.GetString ();
        WAS.mustUpdate = true;
    }

    public void ScouterParse ()
    {
        try {
            ScoutObj = new Scouter (WAS.scouter);
        } catch {
            ScoutObj = new Scouter ("0_1_0_0_0_0_0_0_0_0_0_0");
        }
    }

    public int ScouterGameNum (int pDir)
    {
        return ScoutObj.GameNumberOfDirect (pDir);
    }

    public void GetSkillWidth (bool isBlueDrinkOn, out int wid1, out int wid2)
    {
        float fWid1 = WAS.skill [0], fWid2 = WAS.skill [1];
        // condition 적용
        float conditionFactor = (1 + (WAS.condition * 0.1f));
        fWid1 *= conditionFactor;
        fWid2 *= conditionFactor;
        // Blue drink
        if (isBlueDrinkOn)
            fWid2 += 30;
        wid1 = (int)fWid1;
        wid2 = (int)fWid2;
    }
    //add Direction Logic LJK 12 10
    public void SetDirectionArea ()
    {
        arrArea = new ArrayList ();
        ArrayList smlArr = new ArrayList ();

        int sta = 0, width, sml = 0;
        for (int ij = 0; ij < 4; ij++) {
            width = mDirectObj.mWidth [ij];
            if (width >= 130) {
                arrArea.Add (new int[] { ij + 1, sta, sta + width });   // { 1~4 , 20, 40 }
                sta += width;
            } else {

                // Deck Item ... 
                //width = WAS.DirectWidthOfSmallAreaWidhDeck (WAS.level);
                width = WAS.BuffDirectWidthDeck (width);

                Ag.LogString ("   SetDirectionArea   Small Width  " + mDirectObj.mWidth [ij] + "   Deck Applied  >>>     " + width);

                //smlArr.Add (new int[] { ij + 1, 0, width });   // { 1~4 , 0, 5 }
                smlArr.Add (new int[] { ij + 1, mDirectObj.mPosition [ij], width });   // { 1~4 , 0, 5 }
            }
        }
        int smlNum = 4 - arrArea.Count;
        sml = 0;
        for (int ij = 0; ij < smlNum; ij++) {
            int[] curVal = (int[])smlArr [ij];
            int wid = curVal [2];
            //int start = mDirectObj.mPosition [sml], end = start + wid; // 좁은 영역의 위치. 시점 (center 아님)
            int start = curVal [1], end = start + wid; // 좁은 영역의 위치. 시점 (center 아님)
            //Ag.LogString ("sta " + start + " end " + end + " wid " + wid);
            //InsertSmallAreaWithMiss (curVal [0], start, end);
            InsertSmallArea (curVal [0], start, end);

            sml++;
            //int sPo = mDirectObj.mWidth 
        }
        InsertSmallArea (5, 960, 1000);  // Panenka... 
        //ShowArrArea ();
    }

    public void tutorSetdirectionArea ()
    {
        arrArea = new ArrayList ();
        ArrayList smlArr = new ArrayList ();
        
        int sta = 0, width, sml = 0;
        for (int ij = 0; ij < 4; ij++) {
            width = mDirectObj.mWidth [ij];
            if (width >= 130) {
                arrArea.Add (new int[] { ij + 1, sta, sta + width });   // { 1~4 , 20, 40 }
                sta += width;
            } else {
                //smlArr.Add (new int[] { ij + 1, 0, width });   // { 1~4 , 0, 5 }
                smlArr.Add (new int[] { ij + 1, mDirectObj.mPosition [ij], width });   // { 1~4 , 0, 5 }
            }
        }
        int smlNum = 4 - arrArea.Count;
        sml = 0;
        for (int ij = 0; ij < smlNum; ij++) {
            int[] curVal = (int[])smlArr [ij];
            int wid = curVal [2];
            //int start = mDirectObj.mPosition [sml], end = start + wid; // 좁은 영역의 위치. 시점 (center 아님)
            int start = curVal [1], end = start + wid; // 좁은 영역의 위치. 시점 (center 아님)
            Ag.LogString ("sta " + start + " end " + end + " wid " + wid);
            InsertSmallArea (curVal [0], start, end);
            
            sml++;
            //int sPo = mDirectObj.mWidth 
        }
        //InsertSmallArea (5, 960, 1000);
        ShowArrArea ();
        
    }

    public void InsertSmallArea (int pDir, int pSta, int pEnd)
    {
        //Ag.LogString (" >>>>>   InsertSmallArea  <<<<<    Value: " + pDir + " Sta: " + pSta + " End: " + pEnd);
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
            staObj [2] = pSta;
            endObj [1] = pEnd;
            arrArea.Insert (staIdx + 1, new int[] { pDir, pSta, pEnd });
        }
    }

    public void InsertSmallAreaWithMiss (int pDir, int pSta, int pEnd)
    {
        // DDong Area Insert ... 
        //Ag.LogString (" >>>>>   InsertSmallArea  <<<<<    Value: " + pDir + " Sta: " + pSta + " End: " + pEnd);

        float cen = (pSta + pEnd) * 0.5f;
        // 똥볼 영역 .. 시 / 종 위치  Green Drink 적용
        int ddWid = GreenDrinkOn ? WAS.DirectMax () + 20 : WAS.DirectMax (), ddSta = (int)(cen - ddWid * 0.5), ddEnd = (int)(cen + ddWid * 0.5);
        if (GreenDrinkOn) {  // 넓혀 준다. 
            pSta -= 10; 
            pEnd += 10;
        }
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

    public void ShowArrArea ()
    {
        Ag.LogStartWithStr (2, "m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>     ShowArrArea     >>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m m>>>>>>>>>>>m ");
        for (int ij = 0; ij < arrArea.Count; ij++) {
            int[] curVal = (int[])arrArea [ij];
            Ag.LogString ("Dir: <" + curVal [0] + "> Starts: " + curVal [1] + ", Ends: " + curVal [2]);
        }
    }

    public int GetDirIndexOfArea (int pSpot)
    { // returns 0 ~ 3... 4, 5...
        int[] lastVal = (int[])arrArea [0];
        for (int ij = 0; ij < arrArea.Count; ij++) {
            int[] curVal = (int[])arrArea [ij];
            lastVal = curVal;
            if (curVal [1] <= pSpot && pSpot < curVal [2])
                return ij;
        }
        if (pSpot == lastVal [2])
            return arrArea.Count - 1; // 1000 case ...
        return 99; 
    }

    public void SetSkillPositions (AmCard pCard, bool pDidBuyPotion, bool pEventPotion, bool pMinusPotion, AmUser pUser, int costumeNum)
    {//[2012:11:09:LJK] Random Item Acc Good Perfect  modified
        arrArea = new ArrayList ();
            
        //case 0:     good = 20f; better = .5f;     break;
        //Debug.Log ("Card Good :: SetSkillPositions" + pCard.WAS.skill[0] + "Perfect" + pCard.WAS.skill[1] + "      CardGRADE     " + pCard.WAS.grade);

        int good, prft;

        if (AgStt.mgGameTutorial) {
            mGood = pCard.WAS.skill [0];
            mPerfect = pCard.WAS.skill [1];
           
        } else {
            WAS.GetSkillFinalValue (pUser.arrUniform [0].Kick.Shirt.Texture, pUser.arrUniform [0].Kick.Pants.Texture, pUser.arrUniform [0].Kick.Socks.Texture, costumeNum, 
                out good, out prft, Ag.mgDirection);
            
            mGood = good;
            mPerfect = prft;
        }

        //Debug.Log ("mGood :: SetSkillPositions" + mGood + "mPerfect :: SetSkillPositions"+ mPerfect);
           
        // Potion...
        if (pDidBuyPotion) {
            mGood *= 1.2f;
            mPerfect += 30f;
        } //[2012:11:09:LJK] Random Item Acc Good Perfect  modified
        if (pEventPotion) {
            mPerfect += 20f;
        }//[2012:11:09:LJK] Random Item Acc Good Perfect  modified
        float cen, sta;
        if (AgStt.mgGameTutorial) {    
            cen = 241f;
            sta = cen - mGood * 0.5f;
        } else { 
            cen = 300f;
            sta = cen - mGood * 0.5f;
        }

        //Ag.LogIntenseWord ("   SetSkillPositions  >>>>>    mGood :: " + mGood + "     mPerfect :: " + mPerfect + "  sta : " + sta);
        arrArea.Add (new int[] { 1, (int)sta, (int)(sta + mGood) });
        if (mPerfect > 0) // Regend card has no perfect area ....
            InsertSmallArea (2, (int)(cen - 0.5f * mPerfect), (int)(cen + 0.5f * mPerfect));
    }

    public byte GetPosition (float pCoordi)
    { 
        int i, num = arrArea.Count;

        //Ag.LogString ("  AmCard :: GetPosition >>  input : " + pCoordi + "   arrArea : " + arrArea.Count + " ea ");

        for (i = 0; i < num; i++) {
            int[] curVal = (int[])arrArea [i];

            //Ag.LogString ("  AmCard :: GetPosition     Is  Inside of   " + curVal [1] + " ~~~ " + curVal [2]);

            if (curVal [1] <= pCoordi && pCoordi < curVal [2]) {
                Ag.LogString ("  AmCard :: GetPosition >>  returns >>  " + curVal [0]);
                return (byte)curVal [0];
            }
        }
        Ag.LogString ("  AmCard :: GetPosition >>  Error  returns >>  0  _______  ");
        return 0;
    }
    //  ////////////////////////////////////////////////     Get Color of Direction..
    public void SetDirectionPosition ()
    {
        mDirectObj.mWidth = new int[] {
            WAS.direction [0],
            WAS.direction [2],
            WAS.direction [4],
            WAS.direction [6]

        };
        mDirectObj.mPosition = new int[] {
            WAS.direction [1],
            WAS.direction [3],
            WAS.direction [5],
            WAS.direction [7]
        };
    }

    //// [2012:11:09:MOON] ExpandDirection 40 -> 13 40 13 = 66
    public void ExpandDirection ()
    {
        mDirectObj.ExpandSmallDirectionBar ();
        SetDirectionArea ();
    }
}

