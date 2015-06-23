using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class AmAnima {

    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Matching  <<<<<    
    
    public string mAnimaName, mBallAnima;
    
    byte mKickSkil, mKickDirc, mDefnSkil, mDefnDirc;
    
    // 
    
    public AmAnima( bool pIsKicker, bool pIsSave, byte pKickSkil, byte pKickDirc, byte pDefnSkil, byte pDefnDirc ) {
  
        mKickDirc = pKickDirc; mKickSkil = pKickSkil; mDefnDirc = pDefnDirc; mDefnSkil = pDefnSkil;
        
        // ///// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> _________________     Kicker Case....
        if (pIsKicker) {
            return;
        } 
        
        // ///// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> _________________     Keeper Case....
        
        if (IsDefnNothing() )  {
            mAnimaName = "Noth";
            mAnimaName += GetKickLeftOrRight();  // Noth_R_  Noth_L_
            mAnimaName += GetKickSkillString();  // "Noth_R_Noth" "Noth_L_Gene" "NothSupr" ....
            return;
        }

        if (pIsSave) {      // >>>>>>>>>>>>>>>>>>>>   Save Case....
            mAnimaName = "Save";
            
            switch (pKickSkil) {
            case 0:
                if (mKickDirc == 0) {
                    if (mDefnSkil == 1)         mAnimaName = "GoalBoth";
                    if (mDefnSkil == 2)         mAnimaName = "GoalMira";
                } else { // Kick Direct : 1, 2, 3, 4.
                    if (mDefnSkil == 1) {
                        if ( IsSameDirection() ) {
                            if ( IsExactlySameDirection() ) mAnimaName = "BothGene";
                            else                            mAnimaName = "GoalHand";    // Up/Dn Difference ^ v ^ v
                        } else                  mAnimaName = "GoalBoth";        // Left <=====> Right Difference Case
                    } 
                    if (mDefnSkil == 2) {
                        if ( IsSameDirection() ) {
                            if ( IsExactlySameDirection() ) mAnimaName = "SaveBothMira";
                            else                            mAnimaName = "SaveHandMira";// Up/Dn Difference ^ v ^ v
                        } else                  mAnimaName = "GoalMira";        // Left <=====> Right Difference Case
                    }
                }
                break;
            case 1: // General Kick
                if (mDefnSkil == 1)     {       mAnimaName = "BothGene"; break; } // General Defn ** No Save/Goal
                
                if (mDefnSkil == 2)     { 
                    if ( IsExactlySameDirection() ) mAnimaName += "BothMira";
                    else                            mAnimaName += "HandMira";           // Up/Dn Difference ^ v ^ v
                    break; }  // Miracle Save
                break;
            case 2: // Super Kick
                if (mDefnSkil == 2)             mAnimaName += "SuprMira";
                break;
            case 3: // Ultra Kick :: Goal.. No save case..
                break;
            }
        } else {            // >>>>>>>>>>>>>>>>>>>>  Goal In Case....
            mAnimaName = "Goal";
            switch (pKickSkil) {
            case 0:   break; // Impossible Case...
            case 1: // General Kick
                if (mDefnSkil == 1)     { 
                    if ( IsSameDirection() )    mAnimaName += "Hand";
                    else                        mAnimaName += "Both";           // Left <=====> Right Difference Case
                    break;
                }
                if (mDefnSkil == 2)     {       mAnimaName += "Mira"; break; }
                break;
            case 2: // Super Kick
                if (mDefnSkil == 1)     { 
                    if ( IsSameDirection() )  { // Up Dn Difference..
                        if ( IsExactlySameDirection() ) mAnimaName = "BothGene"; // General Defn ** No Save/Goal  
                        else                            mAnimaName += "Hand";           // Up/Dn Difference ^ v ^ v
                    } else {                    mAnimaName += "Both"; }         // Left <=====> Right Difference Case
                }
                if (mDefnSkil == 2)     {    // Miracle Save
                    if ( IsSameDirection() )    mAnimaName += "SuprMira";
                    else                        mAnimaName += "Mira";           // Left <=====> Right Difference Case
                } 
                break;
            case 3: // Ultra Kick
                break;
            }
        }

        
        mAnimaName += mDefnDirc.ToString();  // Add Direction ... 1, 2, 3, 4 ....
    }
    
    
    bool IsDefnNothing() {
        return mDefnSkil == 0 || mDefnDirc == 0;
    }
    
    bool IsExactlySameDirection() {
        return mKickDirc == mDefnDirc;
    }
    
    bool IsSameDirection() {
        int kickRm = mKickDirc % 2, defnRm = mDefnDirc % 2;
        return kickRm == defnRm;
    }
    
    string GetKickLeftOrRight() {
        switch (mKickDirc) {
        case 0:
        case 2:
        case 4:
            return "_R_";
        case 1:
        case 3:
            return "_L_";
        }
        return "_E_";
    }
    
    string GetKickSkillString() {
        switch (mKickSkil) {
        case 0: return "Noth"; 
        case 1: return "Gene"; 
        case 2: return "Supr"; 
        case 3: return "Ultr"; 
        }
        return "Errr";
    }
    
}
