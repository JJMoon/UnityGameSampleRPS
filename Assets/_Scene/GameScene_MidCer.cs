//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GameScene_MidCer : AmSceneBase
{
    public GameObject mBall, mKeeper, mPlayerKicker;
    bool mKpRight, mIsGoalIn;
    public string mKickerAni, mKeeperAni, mBallAni;

    string GetDirectionChar (int pDirection)
    {
        if (pDirection == 1 || pDirection == 3)
            return "R";
        if (pDirection == 2 || pDirection == 4)
            return "L";
        if (pDirection == 0)
            return "";
        return "P"; // Panenka ..
    }

    void AddKeeperVolcanoKick (int pKeeperDir, int pKeeperSkl, int pKickerDir)
    {
        if (pKeeperDir == 0 || pKeeperSkl == 0) {
            mKeeperAni = "GK" + GetDirectionChar (pKickerDir) + "_noActCer_K1";
            if (pKickerDir == 0)
                mKeeperAni = "GKL_noActCer_K1";
        }

        if (pKeeperDir != 0 && pKeeperSkl == 1) {

            if (pKeeperDir == 1)
                mKeeperAni = "BUTHR";
            if (pKeeperDir == 2)
                mKeeperAni = "BUTHL";
            if (pKeeperDir == 3)
                mKeeperAni = "BDTHR";
            if (pKeeperDir == 4)
                mKeeperAni = "BDTHL";

            if (pKeeperDir % 2 == 0) {
                if (pKickerDir % 2 == 1)
                    mKeeperAni = "GKLM01";
            } else {
                if (pKickerDir % 2 == 0)
                    mKeeperAni = "GKRM01";
            }
        }
        if (pKeeperDir != 0 && pKeeperSkl == 2) {
            if (pKeeperDir == 1)
                mKeeperAni = "BUTHRM_K2";
            if (pKeeperDir == 2)
                mKeeperAni = "BUTHLM_K2";
            if (pKeeperDir == 3)
                mKeeperAni = "BDTHRM_K2";
            if (pKeeperDir == 4)
                mKeeperAni = "BDTHLM_K2";

            if (pKeeperDir % 2 == 0) {
                if (pKickerDir % 2 == 1)
                    mKeeperAni = "GKLM02";
            } else {
                if (pKickerDir % 2 == 0)
                    mKeeperAni = "GKRM02";
            }
        }
    }

    void AddBallName (int KickerDir, int Kickerskl)
    {
        if (KickerDir < 1)
            mBallAni = "B_CENM_DD_K2";
        if (KickerDir == 1)
            mBallAni = "B_Volcano_LU_G_K3";
        if (KickerDir == 2)
            mBallAni = "B_Volcano_RU_G_K3";
        if (KickerDir == 3)
            mBallAni = "B_Volcano_LD_G_K3";
        if (KickerDir == 4)
            mBallAni = "B_Volcano_RD_G_K3";

    }

    void AddKeeperRightPanenka (int pKeeperDir )  // Goal In..
    {   
        if (pKeeperDir == 0 ) {
            mKeeperAni = "GKR_noActCer_K1";
        }

        if (pKeeperDir == 1 || pKeeperDir == 3) {
            mKeeperAni = "G_PANENKA_RIGHT_G(440F)";
        }
        if (pKeeperDir == 2 || pKeeperDir == 4) {
            mKeeperAni = "G_PANENKA_LEFT_G(440F)";
        } 
    }

	void AddKeeperSkillAniNamePanenka (int pKeeperDirNum, int pKeeperSklNum ,int pKickerskl) // No Goal..
    {   
		if (pKeeperDirNum == 0 ) {
            mKeeperAni = "G_PANENKA_CENTER_S(365F)";
        }
        if (pKeeperDirNum == 1 || pKeeperDirNum == 3) {
            mKeeperAni = "G_PANENKA_RIGHT_S(400F)";
        }
        if (pKeeperDirNum == 2 || pKeeperDirNum == 4) {
            mKeeperAni = "G_PANENKA_LEFT_S(400F)";
        } 
		if (pKickerskl < 1) {
			if (pKeeperDirNum == 1 || pKeeperDirNum == 3) {
				mKeeperAni = "G_PANENKA_RIGHT_G(440F)";
			}
			if (pKeeperDirNum == 2 || pKeeperDirNum == 4) {
				mKeeperAni = "G_PANENKA_LEFT_G(440F)";
			} 
		}
    }

    void AddBallDirAniNamePanenka (int pKeeperDirNum )
    {
        if (pKeeperDirNum < 1 )
            mBallAni = "B_CEN_DD";
        if (pKeeperDirNum == 1 || pKeeperDirNum == 3)
            mBallAni = "B_PANENKA_RIGHT_G";
        if (pKeeperDirNum == 2 || pKeeperDirNum == 4)
            mBallAni = "B_PANENKA_LEFT_G";
    }

	void AddBallSklAniNamePanenka (int pKeeperDirNum, int pKeeperSklNum, int pKickerSkl)
    {
		if (pKeeperDirNum < 1 ) {
            mBallAni = "B_PANENKA_CENTER_S";
		}
        if (pKeeperDirNum == 1 || pKeeperDirNum == 3) {
            mBallAni = "B_PANENKA_RIGHT_S";
        }
        if (pKeeperDirNum == 2 || pKeeperDirNum == 4) {
            mBallAni = "B_PANENKA_LEFT_S";
        } 
		if (pKickerSkl < 1) {
			mBallAni = "B_CEN_DD";
		}

      
    }

    public void PanenkaAni (int KickerSkl, int KickerDir, int KeeperSkl, int KeeperDir, bool pIsGoalIn)
    {   
		bool mKeeperSave;
        if (!Ag.mgIsKick) {
            if (KeeperDir == 5)
                KeeperDir = 0;
        }
		mKeeperSave = pIsGoalIn;

        mPlayerKicker = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKicker;
        mKeeper = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKeeper;
        mBall = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall;

        // Keeper Animation Name Setting ...
		if (!mKeeperSave)
			AddKeeperRightPanenka (KeeperDir);
        else
            AddKeeperSkillAniNamePanenka (KeeperDir, KeeperSkl, KickerSkl);

        // Ball Animation ...
		if (!mKeeperSave)
            AddBallDirAniNamePanenka (KeeperDir);
        else
            AddBallSklAniNamePanenka (KeeperDir, KeeperSkl, KickerSkl);

        mKickerAni = "Kick_panenka_(190F)";

        /*
		Debug.Log ("KickerSkl_KickerDir_KeeperSkl_KeeperDir_pIsGoalIn" + KickerSkl + "_" + KickerDir + "_" + KeeperSkl+ "_" + KeeperDir + "_" + pIsGoalIn);
		Debug.Log ("KickerSkl_KickerDir_KeeperSkl_KeeperDir_pIsGoalIn" + KickerSkl + "_" + KickerDir + "_" + KeeperSkl+ "_" + KeeperDir + "_" + pIsGoalIn);
		Debug.Log ("KickerSkl_KickerDir_KeeperSkl_KeeperDir_pIsGoalIn" + KickerSkl + "_" + KickerDir + "_" + KeeperSkl+ "_" + KeeperDir + "_" + pIsGoalIn);
		Debug.Log ("KickerSkl_KickerDir_KeeperSkl_KeeperDir_pIsGoalIn" + KickerSkl + "_" + KickerDir + "_" + KeeperSkl+ "_" + KeeperDir + "_" + pIsGoalIn);
		Debug.Log ("KickerSkl_KickerDir_KeeperSkl_KeeperDir_pIsGoalIn" + KickerSkl + "_" + KickerDir + "_" + KeeperSkl+ "_" + KeeperDir + "_" + pIsGoalIn);
  */      

        mBall.animation.Play (mBallAni);
        mKeeper.animation.Play (mKeeperAni);
        mPlayerKicker.animation.Play (mKickerAni);

    }

    public void VolcanoAni (int KickerSkl, int KickerDir, int KeeperSkl, int KeeperDir, bool pIsGoalIn)
    {   
        mIsGoalIn = pIsGoalIn;
        
        mPlayerKicker = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKicker;
        mKeeper = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKeeper;
        mBall = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall;

        AddKeeperVolcanoKick (KeeperDir, KeeperSkl, KickerDir);
        AddBallName (KickerDir, KickerSkl);
        mKickerAni = "Kick_volcano_(250F)";



        mBall.animation.Play (mBallAni);
        mKeeper.animation.Play (mKeeperAni);
        mPlayerKicker.animation.Play (mKickerAni);
        
    }

    bool mKeeperSave = false;

    public void StartAnimation (int KickerSkl, int KickerDir, int KeeperSkl, int KeeperDir)
    {
        //Debug.Log ("StartAnimation");
        
        string Aninum = KickerSkl.ToString () + KickerDir.ToString () + KeeperSkl.ToString () + KeeperDir.ToString ();
           
        mPlayerKicker = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKicker;
        mKeeper = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mPlayerKeeper;
        mBall = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall;

        // Panenka Case...
        if (KickerDir == 5) {
            
			if (KeeperDir > 0 && KeeperSkl <= 1 || KeeperDir < 1 && KeeperSkl < 1 ) {
                mKeeperSave = true;
            }
            if (KeeperDir < 1 && KeeperSkl == 2 || KeeperDir < 1 && KeeperSkl == 1) {
                mKeeperSave = true;
            }
            if (KeeperDir > 0 && KeeperSkl == 2) {
                mKeeperSave = false;
            }
			if (KickerSkl == 0) {
				mKeeperSave = true; 
			}
            PanenkaAni (KickerSkl, KickerDir, KeeperSkl, KeeperDir, mKeeperSave);
            return;
        }

        if (KickerSkl == 3 ) {
            VolcanoAni (KickerSkl, KickerDir, KeeperSkl, KeeperDir, false);
        }

        /*
        if (KeeperDir == 5 || KeeperSkl == 0) {
            if (KickerDir == 5)
                mKeeperSave = true;
            else
                mKeeperSave = false;
            PanenkaAni (KickerSkl, KickerDir, KeeperSkl, KeeperDir, mKeeperSave);
        }
        */

        switch (Aninum) {
        case "0000":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0001":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        
        case "0003":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "0002":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
            
        case "0004":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
            
        case "0010":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
            
        case "0011":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0013":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0012":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break; 
        case "0014":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0020":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break; 
        case "0021":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break; 
        case "0023":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break; 
        case "0022":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0024":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0100":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0101":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0103":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0102":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0104":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0110":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0111":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "BUTHR";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("BUTHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0113":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "BUOHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("BUOHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0112":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0114":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0120":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0121":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "BUTHRM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("BUTHRM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0123":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "BUOHRM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("BUOHRM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0122":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0124":
            mBallAni = "B_BUR_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUR_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0200":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0201":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0203":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0202":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0204":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0210":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKL_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKL_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0211":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0213":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0212":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "BUTHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("BUTHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0214":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "BUOHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("BUOHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0220":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKL_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKL_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0221":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0223":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0222":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "BUTHLM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("BUTHLM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0224":
            mBallAni = "B_BUL_DD";
            mKeeperAni = "BUOHLM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUL_DD");
            mKeeper.animation.Play ("BUOHLM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            
            break;
        case "0300":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0301":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0303":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0302":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0304":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "0310":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "0311":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "BDOHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("BDOHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0313":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "BDTHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("BDTHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0312":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0314":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0320":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0321":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "BDOHRM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("BDOHRM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0323":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "BDTHRM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("BDTHRM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0322":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0324":
            mBallAni = "B_BDR_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDR_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0400":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0401":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0403":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0402":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";


            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0404":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";



            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "0410":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKL_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKL_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "0411":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0413":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0412":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "BDOHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("BDOHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0414":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "BDTHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("BDTHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0420":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKL_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKL_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0421":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0423":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0422":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "BDOHLM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("BDOHLM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "0424":
            mBallAni = "B_BDL_DD";
            mKeeperAni = "BDTHLM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDL_DD");
            mKeeper.animation.Play ("BDTHLM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1000":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1001":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1003":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1002":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1004":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1010":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1011":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1013":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1012":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1014":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1020":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKR_noActCer_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKR_noActCer_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1021":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1023":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "1022":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1024":
            mBallAni = "B_CEN_DD";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_CEN_DD");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1100":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        case "1101":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1103":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1102":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1104":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1110":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1111":
            mBallAni = "B_BUTHR_S";
            mKeeperAni = "BUTHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUTHR_S");
            mKeeper.animation.Play ("BUTHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1113":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "BUOHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("BUOHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1112":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1114":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1120":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1121":
            mBallAni = "B_BUTHRM_S_K1";
            mKeeperAni = "BUTHRM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUTHRM_S_K1");
            mKeeper.animation.Play ("BUTHRM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1123":
            mBallAni = "B_BUOHRM_S";
            mKeeperAni = "BUOHRM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHRM_S");
            mKeeper.animation.Play ("BUOHRM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1122":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1124":
            mBallAni = "B_BUOHR_G";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHR_G");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1200":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1201":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1203":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1202":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1204":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1210":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1211":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1213":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1212":
            mBallAni = "B_BUTHL_S";
            mKeeperAni = "BUTHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUTHL_S");
            mKeeper.animation.Play ("BUTHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1214":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "BUOHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("BUOHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1220":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1221":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
           
        case "1223":
            mBallAni = "B_BUOHL_G";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHL_G");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1222":
            mBallAni = "B_BUTHLM_S_K1";
            mKeeperAni = "BUTHLM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUTHLM_S_K1");
            mKeeper.animation.Play ("BUTHLM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1224":
            mBallAni = "B_BUOHLM_S";
            mKeeperAni = "BUOHLM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BUOHLM_S");
            mKeeper.animation.Play ("BUOHLM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
        
            
        case "1300":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1301":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1303":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1302":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1304":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1310":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1311":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "BDOHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("BDOHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1313":
            mBallAni = "B_BDTHR_S";
            mKeeperAni = "BDTHR";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDTHR_S");
            mKeeper.animation.Play ("BDTHR");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1312":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1314":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1320":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKR_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKR_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1321":
            mBallAni = "B_BDOHRM_S";
            mKeeperAni = "BDOHRM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHRM_S");
            mKeeper.animation.Play ("BDOHRM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1323":
            mBallAni = "B_BDTHRM_S_K1";
            mKeeperAni = "BDTHRM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDTHRM_S_K1");
            mKeeper.animation.Play ("BDTHRM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1322":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1324":
            mBallAni = "B_BDOHR_G";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHR_G");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1400":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1401":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
            
            
        case "1403":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1402":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1404":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
            
        case "1410":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1411":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
            
        case "1413":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1412":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "BDOHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("BDOHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1414":
            mBallAni = "B_BDTHL_S";
            mKeeperAni = "BDTHL";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDTHL_S");
            mKeeper.animation.Play ("BDTHL");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1420":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKL_noActDis_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKL_noActDis_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1421":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1423":
            mBallAni = "B_BDOHL_G";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHL_G");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1422":
            mBallAni = "B_BDOHLM_S";
            mKeeperAni = "BDOHLM";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDOHLM_S");
            mKeeper.animation.Play ("BDOHLM");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "1424":
            mBallAni = "B_BDTHLM_S_K1";
            mKeeperAni = "BDTHLM_S_K1";
            mKickerAni = "Kick_fire_(200F)";

            mBall.animation.Play ("B_BDTHLM_S_K1");
            mKeeper.animation.Play ("BDTHLM_S_K1");
            mPlayerKicker.animation.Play ("Kick_fire_(200F)");
            break;
            
        case "2000":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2001":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2003":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2002":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2004":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2010":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2011":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2013":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2012":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2014":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2020":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKR_noActCer_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKR_noActCer_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2021":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2023":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2024":
            mBallAni = "B_CENM_DD_K2";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_CENM_DD_K2");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2100":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2101":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2103":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2102":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2104":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2110":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2111":
            mBallAni = "B_BUTHR_G_K2";
            mKeeperAni = "BUTHR";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHR_G_K2");
            mKeeper.animation.Play ("BUTHR");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2113":
            mBallAni = "B_BUOHR_G_K2";
            mKeeperAni = "BUOHR";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHR_G_K2");
            mKeeper.animation.Play ("BUOHR");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2112":
            mBallAni = "B_BUOHR_G_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHR_G_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2114":
            mBallAni = "B_BUOHR_G_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHR_G_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2120":
            mBallAni = "B_BUOHRM_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHRM_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2121":
            mBallAni = "B_BUTHRM_S_K2";
            mKeeperAni = "BUTHRM_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHRM_S_K2");
            mKeeper.animation.Play ("BUTHRM_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
        
        case "2123":
            mBallAni = "B_BUOHRM_G_K2";
            mKeeperAni = "BUOHRM";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHRM_G_K2");
            mKeeper.animation.Play ("BUOHRM");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2122":
            mBallAni = "B_BUOHRM_G_K2";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHRM_G_K2");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2124":
            mBallAni = "B_BUOHRM_G_K2";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHRM_G_K2");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2200":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2201":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2203":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2202":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2204":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2210":
            mBallAni = "B_BUOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2211":
            mBallAni = "B_BUOHL_G_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHL_G_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2213":
            mBallAni = "B_BUOHL_G_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHL_G_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2212":
            mBallAni = "B_BUTHL_G_K2";
            mKeeperAni = "BUTHL";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHL_G_K2");
            mKeeper.animation.Play ("BUTHL");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2214":
            mBallAni = "B_BUOHL_G_K2";
            mKeeperAni = "BUOHL";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHL_G_K2");
            mKeeper.animation.Play ("BUOHL");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2220":
            mBallAni = "B_BUOHLM_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHLM_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2221":
            mBallAni = "B_BUOHLM_G_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHLM_G_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2223":
            mBallAni = "B_BUOHLM_G_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHLM_G_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2222":
            mBallAni = "B_BUTHLM_S_K2";
            mKeeperAni = "BUTHLM_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUTHLM_S_K2");
            mKeeper.animation.Play ("BUTHLM_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2224":
            mBallAni = "B_BUOHLM_G_K2";
            mKeeperAni = "BUOHLM";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BUOHLM_G_K2");
            mKeeper.animation.Play ("BUOHLM");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2300":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2301":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2303":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2302":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2304":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2310":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2311":
            mBallAni = "B_BDOHR_G_K2";
            mKeeperAni = "BDOHR";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHR_G_K2");
            mKeeper.animation.Play ("BDOHR");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2313":
            mBallAni = "B_BDTHR_G_K2";
            mKeeperAni = "BDTHR";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHR_G_K2");
            mKeeper.animation.Play ("BDTHR");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2312":
            mBallAni = "B_BDTHR_G_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHR_G_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2314":
            mBallAni = "B_BDTHR_G_K2";
            mKeeperAni = "GKLM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHR_G_K2");
            mKeeper.animation.Play ("GKLM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2320":
            mBallAni = "B_BDOHRM_G_K2";
            mKeeperAni = "GKR_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHRM_G_K2");
            mKeeper.animation.Play ("GKR_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2321":
            mBallAni = "B_BDOHRM_G_K2";
            mKeeperAni = "BDOHRM";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHRM_G_K2");
            mKeeper.animation.Play ("BDOHRM");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2323":
            mBallAni = "B_BDTHRM_S_K2";
            mKeeperAni = "BDTHRM_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHRM_S_K2");
            mKeeper.animation.Play ("BDTHRM_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2322":
            mBallAni = "B_BDOHRM_G_K2";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHRM_G_K2");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2324":
            mBallAni = "B_BDOHRM_G_K2";
            mKeeperAni = "GKLM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHRM_G_K2");
            mKeeper.animation.Play ("GKLM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;    
        case "2400":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2401":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2403":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2402":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2404":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2410":
            mBallAni = "B_BDTHL_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHL_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2411":
            mBallAni = "B_BDTHL_G_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHL_G_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2413":
            mBallAni = "B_BDTHL_G_K2";
            mKeeperAni = "GKRM01";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHL_G_K2");
            mKeeper.animation.Play ("GKRM01");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2412":
            mBallAni = "B_BDOHL_G_K2";
            mKeeperAni = "BDOHL";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHL_G_K2");
            mKeeper.animation.Play ("BDOHL");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2414":
            mBallAni = "B_BDTHL_G_K2";
            mKeeperAni = "BDTHL";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHL_G_K2");
            mKeeper.animation.Play ("BDTHL");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2420":
            mBallAni = "B_BDOHLM_G_K2";
            mKeeperAni = "GKL_noActDis_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHLM_G_K2");
            mKeeper.animation.Play ("GKL_noActDis_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2421":
            mBallAni = "B_BDOHLM_G_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHLM_G_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2423":
            mBallAni = "B_BDOHLM_G_K2";
            mKeeperAni = "GKRM02";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHLM_G_K2");
            mKeeper.animation.Play ("GKRM02");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2422":
            mBallAni = "B_BDOHLM_G_K2";
            mKeeperAni = "BDOHLM";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDOHLM_G_K2");
            mKeeper.animation.Play ("BDOHLM");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;
            
        case "2424":
            mBallAni = "B_BDTHLM_S_K2";
            mKeeperAni = "BDTHLM_K2";
            mKickerAni = "Kick_blaze_(290F)";

            mBall.animation.Play ("B_BDTHLM_S_K2");
            mKeeper.animation.Play ("BDTHLM_K2");
            mPlayerKicker.animation.Play ("Kick_blaze_(290F)");
            break;

        }
        
    }
}
