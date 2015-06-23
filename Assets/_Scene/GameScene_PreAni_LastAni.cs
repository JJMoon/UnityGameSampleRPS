//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    AmAnima MidCer;

    void PreAni ()
    {
        CerCam.enabled = false;
        if (Ag.mgIsKick) {
            mKickBall.collider.isTrigger = true;
            mKickBall.rigidbody.useGravity = false;
            
            mKickBall.transform.position = new Vector3 (0.4018516f, 0.1229186f, -34.6767f);
            /*
            mCameraKick.transform.localPosition = new Vector3 (1.231774f, -1.820157f, -29.05658f);
            mCameraKick.transform.eulerAngles = new Vector3 (354.8362f, 178.2565f, 359.9102f);
            mCameraKick.fieldOfView = 40;
            */
            mCameraKick.transform.localPosition = new Vector3 (9.405171f, -0.9257562f, -18.21554f);
            mCameraKick.transform.eulerAngles = new Vector3 (1.112548f, 199.045f, 0.3840297f);
            mCameraKick.fieldOfView = 13;

            
        } else {
            mKickBall.rigidbody.useGravity = false;
            mKickBall.collider.isTrigger = true;
            mKickBall.transform.position = new Vector3 (0.4018516f, 0.1229186f, -34.6767f);
            /*
            mCameraDefn.transform.localPosition = new Vector3 (0.6681237f, -1.834172f, -50.99377f);
            mCameraDefn.GetComponent<DragPos>().mnewPosition = new Vector3 (0.6681237f, -1.834172f, -50.99377f);
            mCameraDefn.transform.eulerAngles = new Vector3 (357.2246f, 356.7078f, 0.5659171f);
            mCameraDefn.fieldOfView = 28f;
            */
            mCameraDefn.transform.localPosition = new Vector3 (1.337146f, -0.8998313f, -57.61932f);
            //mCameraDefn.GetComponent<DragPos>().mnewPosition = new Vector3 (0.6681237f, -1.834172f, -50.99377f);
            mCameraDefn.transform.eulerAngles = new Vector3 (2.961914f, 356.0515f, 6.68E-09f);
            mCameraDefn.fieldOfView = 13f;
        }
        mPlayerKicker.transform.eulerAngles = new Vector3 (0f, 180f, 0f);
        
        mPlayerKeeper.transform.position = new Vector3 (0.06f, 0f, -45.67266f); //keeper setting
        mPlayerKeeper.transform.eulerAngles = new Vector3 (0, 0, 0);
        mAnimaRand = Random.Range (0, 5);
        
        switch (mAnimaRand) {
        case 0:
            mPlayerKicker.transform.position = new Vector3 (0.5916449f, 0.04181996f, -34.25025f);
            mBall.transform.position = new Vector3 (1.350924f, 1.283536f, -34.30703f);
            break;
        case 1:
            mPlayerKicker.transform.position = new Vector3 (0.782496f, 0.04181996f, -33.99356f);
            mBall.transform.position = new Vector3 (1.541775f, 1.283536f, -34.05035f);
            break;
        case 2:
            mPlayerKicker.transform.position = new Vector3 (0.6011075f, 0.04181996f, -34.11449f);
            mBall.transform.position = new Vector3 (1.360387f, 1.283536f, -34.17127f);
            break;
        case 3:
            mPlayerKicker.transform.position = new Vector3 (0.5774047f, 0.04181996f, -34.21926f);
            mBall.transform.position = new Vector3 (1.336684f, 1.283536f, -34.27605f);
            break;
        case 4:
            mPlayerKicker.transform.position = new Vector3 (0.6541872f, 0.04181996f, -34.13402f);
            mBall.transform.position = new Vector3 (1.413466f, 1.283536f, -34.1908f);
            break;
        case 5:
            mPlayerKicker.transform.position = new Vector3 (0.5704616f, 0.04181996f, -34.34101f);
            mBall.transform.position = new Vector3 (1.329741f, 1.283536f, -34.39779f);
            break;
        }
        mPlayerKicker.animation.Play ("ballset0" + (mAnimaRand + 1).ToString ());
        if (Ag.mgIsKick) 
            mPlayerKeeper.animation.Play ("EnemyGoalReady");
        else
            mPlayerKeeper.animation.Play ("goalready");
        
        //Debug.Log ("Player Animation is playing ?? Keeper  >> " + mPlayerKeeper.animation.isPlaying + " and   Kicker  >>  " + mPlayerKicker.animation.isPlaying);
    }
    //----------------------------------------------------------------------------Network Ani
    void mNetworkWaitAni ()
    {
        //Ag.LogString ("NetworkAnimationPlay :: " + mAnimaRand.ToString ());

        if (Ag.mgIsKick) 
            mPlayerKeeper.animation.Play ("EnemyGoalReady_after");
        else
            mPlayerKeeper.animation.Play ("goalready_after");

        switch (mAnimaRand) {
        case 0:
            mPlayerKicker.animation.Play ("ballset01_after");
            break;
        case 1:
            mPlayerKicker.animation.Play ("ballset02_after");
            break;
        case 2:
            mPlayerKicker.animation.Play ("ballset03_after");
            break;
        case 3:
            mPlayerKicker.animation.Play ("ballset04_after");
            break;
        case 4:
            mPlayerKicker.animation.Play ("ballset05_after");
            break;
        case 5:
            mPlayerKicker.animation.Play ("ballset06_after");
            break;
        }
    }
    //-----------------------------------------------------------------------------mid Kick Ani
    void AnimaPlay ()
    {
        int mark = 0;
        //Ag.LogString ("animationplay");
        
        byte myDir, enDir, mySkl, enSkl;
        /*
        myDir = Ag.mgDirection;
        mySkl = Ag.mgSkill;
        enDir = Ag.mgEnemDirec;
        enSkl = Ag.mgEnemSkill;
        */

        myDir = Ag.mgDirection;
        mySkl = Ag.mgSkill;
        enDir = Ag.mgEnemDirec;
        enSkl = Ag.mgEnemSkill;
        Ag.LogString ("AnimaPlay >>   myDir    :" + myDir + "     mySkl     :" + mySkl + "       enDir         :" + enDir + "      enskl      " + enSkl);

        AmAni = new GameScene_MidCer ();




        //-----------------------------------------------------------

        ////////////////////////////////////////////////////////////
        if (Ag.mgIsKick) {

            //mCameraKick.animation.Play ("KickAni");
            mCameraKick.transform.localPosition = new Vector3 (1.887791f, 0.3084412f, -12.32919f);
            mCameraKick.transform.eulerAngles = new Vector3 (3.722382f, 181.77f, 0.04598999f);
            mCameraKick.fieldOfView = 10;


            mPlayerKicker.transform.position = new Vector3 (1.78f, 0.04181999f, -31.6f);
            mPlayerKicker.transform.eulerAngles = new Vector3 (0f, 180f, 0f);
            AmAni.StartAnimation (mySkl, myDir, enSkl, enDir);
            MidCer = new AmAnima (true, Ag.mgDidWin, mySkl, myDir, enSkl, enDir);
            //Debug.Log (MidCer + "MidCeremony Name");
        } else {
            //mKickBall.animation.Play ("B_BLUOH_S");
            //mCameraDefn.animation.Play ("KeeperAni");
            mCameraDefn.transform.localPosition = new Vector3 (0.8f, 0.9032068f, -75.5436f);
            mCameraDefn.transform.eulerAngles = new Vector3 (4.23877f, 359.1068f, 359.8467f);
            mCameraDefn.fieldOfView = 10;

            mPlayerKicker.transform.position = new Vector3 (1.78f, 0.04181999f, -31.6f);
            mPlayerKicker.transform.eulerAngles = new Vector3 (0f, 180f, 0f);
            AmAni.StartAnimation (enSkl, enDir, mySkl, myDir);
            MidCer = new AmAnima (false, Ag.mgDidWin, enSkl, enDir, mySkl, myDir);
            //Debug.Log (MidCer.mAnimaName + "MidCeremony Name");
        }
        
        Ag.LogString ("AnimaPlay >> Delegate_GameAnimationPlay >>>>>>  End... >>>> " + mark++);

    }
    //----------------------------------------------------------------------------- Short Ani
    void IntroAni ()
    {
        if (Ag.mgIsKick) {
            
            mIntroCam.animation.Play ("KickIntro2");
            //Debug.Log ("KickerAnimation");
            mPlayerKicker.animation.Play ("ballmove");
            mPlayerKeeper.animation.Play ("goalmove");
        } else {
            mIntroCam.animation.Play ("keeperIntro");
            mPlayerKicker.animation.Play ("ballmove");
            mPlayerKeeper.animation.Play ("goalmove");
        }
    }

    bool AmIRight ()
    {
        return (Ag.mgDirection % 2) == 0;
    }

    bool IsSameDirection ()
    {
        int kickRm = Ag.mgDirection % 2, defnRm = Ag.mgEnemDirec % 2;
        return kickRm == defnRm;
    }

    bool IsKickerWin ()
    {
        if (Ag.mgIsKick && Ag.mgDidWin)
            return true;
        if (!Ag.mgIsKick && !Ag.mgDidWin)
            return true;
        return false;
    }

    bool IsKeeperRight ()
    {
        if (!Ag.mgIsKick && (Ag.mgDirection % 2 == 0))
            return true;
        if (Ag.mgIsKick && (Ag.mgEnemDirec % 2 == 0))
            return true;
        return false;
    }

    bool IsKickerRight ()
    {
        if (Ag.mgIsKick && (Ag.mgDirection % 2 == 0))
            return true;
        if (!Ag.mgIsKick && (Ag.mgEnemDirec % 2 == 0))
            return true;
        return false;
    }

    bool IsGoulerDdong ()
    {
        if (!Ag.mgIsKick && (Ag.mgDirection == 0 || Ag.mgSkill == 0))
            return true;
        if (Ag.mgIsKick && (Ag.mgEnemDirec == 0 || Ag.mgEnemSkill == 0))
            return true;
        return false;
    }

    bool IsKickerDdong ()
    {
        if (Ag.mgIsKick && (Ag.mgDirection == 0 || Ag.mgSkill == 0))
            return true;
        if (!Ag.mgIsKick && (Ag.mgEnemDirec == 0 || Ag.mgEnemSkill == 0))
            return true;
        return false;
    }

    void KickerVoice () {
        int rannum = AgUtil.RandomInclude (1, 10);


        if (Ag.mgIsKick) {
            if (Ag.mgDidWin) {
                if (Ag.mgSkill == 1) {
                    if (rannum % 2 == 1)
                        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals05");
                    else
                        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals09");
                }
                if (Ag.mgSkill == 2)
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals07");
                if (Ag.mgSkill == 3)
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals07");
            } else {
                if (IsKickerDdong ()) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerNoGoals04");
                    return;
                }
                if (IsSameDirection ()) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerNoGoals07");
                    return;
                }
                if (IsSameDirection () && Ag.mgEnemSkill == 2) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerNoGoals09");
                    return;
                }
            }
        } else {
            if (Ag.mgDidWin) {
                if (IsSameDirection () && Ag.mgEnemSkill == 2) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves03");
                    return;
                }
                if (IsSameDirection ()) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves06");
                    return;
                }

                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves08");

            } else {
                if (!IsSameDirection ()) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses04");
                    return;
                }
                if (IsSameDirection ()) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses05");
                    return;
                }
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses02");
            }

        }
    }


    void CerAni ()
    {

        //byte myDir, enDir, mySkl, enSkl;
        //myDir = Ag.mgDirection; mySkl = Ag.mgSkill; enDir = Ag.mgEnemDirec; enSkl = Ag.mgEnemSkill;
        bool myWin;
        myWin = Ag.mgDidWin;
        /*
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("is Win                            " + Ag.mgDidWin);
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        Debug.Log ("---------------------------------------------------------------------------------------");
        */

        
        mPlayerKicker.transform.position = new Vector3 (2.59593f, 0.04181996f, -36.41669f);
        mCerKickAni = Random.Range (0, 5);
        mDisKickAni = Random.Range (0, 4);
        mDisKeepAni = Random.Range (0, 4);
        mCerKeepAni = Random.Range (0, 3);
        
        
        
        //kicker
        string[] mCerKickStr = {
            "cere01",
            "cere02",
            "cere03",
            "cere04",
            "cere05"
        };   // 5 ea
        string[] mDisKickStr = {
            "cerlose01",
            "cerlose02",
            "cerlose03",
            "cerlose04"
        };  // 4 ea
        
        // Kicker  ... Cam
        string[] mCerCamera = {
            "C_KickCer1",
            "C_KickCer2",
            "C_KickCer3",
            "C_KickCer4",
            "C_KickCer5"
        }; // 5 ea
        string[] mDisCamera = {
            "C_Kick_Dis01",
            "C_Kick_Dis02",
            "C_Kick_Dis03",
            "C_Kick_Dis04"
        };  // 4£ ea
        // Keeper  ... Cam
        string[] mCerKPCamL = {
            "C_Keeper_Cer1_L",
            "C_Keeper_Cer2_L",
            "C_Keeper_Cer3_L"
        };  // 3 ea
        string[] mCerKPCamR = {
            "C_Keeper_Cer1_R",
            "C_Keeper_Cer2_R",
            "C_Keeper_Cer3_R"
        };  // 3 ea
        string[] mDisKPCamL = {
            "C_Keeper_DisA_L",
            "C_Keeper_DisB_L",
            "C_Keeper_DisC_L",
            "C_Keeper_DisD_L"
        };  // 4 ea
        string[] mDisKPCamR = {
            "C_Keeper_DisA_R",
            "C_Keeper_DisB_R",
            "C_Keeper_DisC_R",
            "C_Keeper_DisD_R"
        };  // 4 ea
        string[] mKpMissDissCamL = {
            "C_Keeper_L_Miss_01",
            "C_Keeper_L_Miss_02"
        };
        string[] mKpMissDissCamR = {
            "C_Keeper_R_Miss_01",
            "C_Keeper_R_Miss_02"
        };
        
  
        // Keeper... 
        string[] mCerKeepStrL = {
            "CerKeeperA_L",
            "CerKeeperB_L",
            "CerKeeperC_L"
        };  // 3 ea
        string[] mCerKeepStrR = {
            "CerKeeperA_R",
            "CerKeeperB_R",
            "CerKeeperC_R"
        };  // 3 ea
        string[] mDisKeePStrL = {
            "DisKeeperA_L",
            "DisKeeperB_L",
            "DisKeeperC_L",
            "DisKeeperD_L"
        };  // 4 ea
        string[] mDisKeePStrR = {
            "DisKeeperA_R",
            "DisKeeperB_R",
            "DisKeeperC_R",
            "DisKeeperD_R"
        };  // 4 ea
        string[] mDisGKLM = { "DisGKLM01_m", "keeper_left02(300)sujung" };  // Sitting..
        string[] mDisGKRM = { "DisGKRM01_m", "keeper_right02(300)sujung" };
        
        string kickAni, keepAni, cameraAni;
        //int kickSkill = Ag.mgIsKick ? Ag.mgSkill: Ag.mgEnemSkill; 
        int kickSkill = Ag.mgSkill;
        CerCam.enabled = true;
        
        // //////////////////////////////////////////////////   Kicker Animation Setting...
        if (IsKickerWin ())
            kickAni = (mCerKickStr [mCerKickAni]);
        else
            kickAni = (mDisKickStr [mDisKickAni]);
        
        // //////////////////////////////////////////////////   Camera... Animation Setting...  
        if (Ag.mgIsKick) {
            if (myWin)
                cameraAni = (mCerCamera [mCerKickAni]);
            else
                cameraAni = (mDisCamera [mDisKickAni]);
        } else { // Keeper...
            if (myWin)
                cameraAni = AmIRight () ? mCerKPCamL [mCerKeepAni] : mCerKPCamR [mCerKeepAni];
            else {
                cameraAni = AmIRight () ? mDisKPCamL [mDisKeepAni] : mDisKPCamR [mDisKeepAni];
                if (Ag.mgSkill > 0 && !IsSameDirection ())
                    cameraAni = AmIRight () ? mKpMissDissCamL [kickSkill - 1] : mKpMissDissCamR [kickSkill - 1];
            } //ljk 11/27
        }
        
        // //////////////////////////////////////////////////   Keeper .....
		if (IsKickerWin ()) {    // Goul In  .......>>>>>>>>>
			//-2014_04_24
			if (IsSameDirection ()) {
				keepAni = IsKeeperRight () ? mDisKeePStrL [mDisKeepAni] : mDisKeePStrR [mDisKeepAni];
				if (Ag.mgEnemDirec == 5 && !Ag.mgIsKick)
					mPlayerKeeper.transform.position = IsKeeperRight () ?  new Vector3 (-2.384015f,0f,-44.09128f) : new Vector3  (2.988599f,0f,-44.09128f);
			} else {
				//keepAni = IsKeeperRight()?  mDisGKRM[kickSkill-1]: mDisGKLM[kickSkill-1];  // 0 or 1 ...
				if (Ag.mgSkill > 0) {
					if (Ag.mgSkill == 3) {
						keepAni = IsKeeperRight () ? mDisGKLM [1] : mDisGKRM [1];
					} else 
						keepAni = IsKeeperRight () ? mDisGKLM [kickSkill - 1] : mDisGKRM [kickSkill - 1];  // 0 or 1 ...
				} else
					keepAni = IsKeeperRight () ? "Dis_noActR" : "Dis_noActL"; //ljk 11/27
				if (Ag.mgEnemDirec == 5 && !Ag.mgIsKick)
					mPlayerKeeper.transform.position = IsKeeperRight () ?  new Vector3 (-2.384015f,0f,-44.09128f) : new Vector3  (2.988599f,0f,-44.09128f);

			}

			if (IsGoulerDdong ()) {
				keepAni = IsKeeperRight () ? "Dis_noActR" : "Dis_noActL";

				if (!Ag.mgIsKick && IsSameDirection ()) {  // >>>>>  Camera  <<<<< //
					cameraAni = IsKeeperRight () ? "C_Keeper_Dis_NoAct_R" : "C_Keeper_Dis_NoAct_L";
				} else if (!Ag.mgIsKick && !IsSameDirection ()) {
					//cameraAni = IsKeeperRight()? "C_Keeper_R_Miss_0" + kickSkill.ToString(): "C_Keeper_L_Miss_0" + kickSkill.ToString();
					cameraAni = IsKeeperRight () ? "C_Keeper_Dis_NoAct_R" : "C_Keeper_Dis_NoAct_L";
				} else {
					cameraAni = mCerCamera [mCerKickAni];
				}
			}
        } else {                // No Goul Case....  >>>>>>>>>>>
            //-2012_08_20
            if (IsGoulerDdong ()) {
                keepAni = IsKeeperRight () ? "Cer_noActL" : "Cer_noActR";
                if (!Ag.mgIsKick) {
                    cameraAni = IsKeeperRight () ? "C_Keeper_Dis_NoAct_R" : "C_Keeper_Dis_NoAct_L";


                }

            } else {
                //keepAni = IsKeeperRight () ? mCerKeepStrL [mCerKeepAni] : mCerKeepStrR [mCerKeepAni];
                mPlayerKeeper.transform.position = IsKeeperRight () ?  new Vector3 (-1.9f,0.04699671f,-45.3f) : new Vector3 (1.9f,0.04699671f,-45.3f);
                keepAni = IsKeeperRight () ? "CerKeeperC_L" : "CerKeeperC_R";
                if (!Ag.mgIsKick) 
                    cameraAni = IsKeeperRight () ? "C_Keeper_Cer3_L_Misskick" : "C_Keeper_Cer3_R_Misskick";
            }

            
            //if (IsKickerDdong())    keepAni = IsKickerRight()? mCerKeepStrL [mCerKeepAni]: mCerKeepStrR [mCerKeepAni];
        }
        

        // //////////////////////////////////////////////////  Play Animations....
        mPlayerKicker.animation.Play (kickAni);
        mPlayerKeeper.animation.Play (keepAni);
        if (!Ag.mgIsKick)
            CerCam.fieldOfView = 51;


        CerCam.animation.Play (cameraAni);
        
        
        Ag.LogIntense (10, true);
        Ag.LogString ("  Kick? " + Ag.mgIsKick + "   Win? " + Ag.mgDidWin + "    MyDirSkl " + Ag.mgDirection + " / " + Ag.mgSkill + "   Enem_ " +
            Ag.mgEnemDirec + " / " + Ag.mgEnemSkill + "       Ani : Kick " + kickAni + "  Goul " + keepAni +
            "  cam " + cameraAni + "\n");
        Ag.LogIntense (5, false);
        KickerVoice ();

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------


    void CereMonySet (Vector3 WinCamPos, Vector3 WinCamRot, Vector3 LoserCamPos, Vector3 LoserCamRot, Vector3 Winpos, Vector3 WinRot, Vector3 LoserPos, Vector3 LoserRot, string WinAniname, string LoseAniname)
    {

        if (Ag.mgDidWin) {
            CerCam.transform.position = WinCamPos;
            CerCam.transform.eulerAngles = WinCamRot;
            if (Ag.mgIsKick) {
                mPlayerKicker.transform.position = Winpos;
                mPlayerKicker.transform.eulerAngles = WinRot;
                mPlayerKicker.animation.Play (WinAniname);
                mPlayerKeeper.transform.position = LoserPos;
                mPlayerKeeper.transform.eulerAngles = LoserRot;
                mPlayerKeeper.animation.Play (LoseAniname);
            } else {
                mPlayerKeeper.transform.position = Winpos;
                mPlayerKeeper.transform.eulerAngles = WinRot;
                mPlayerKeeper.animation.Play (WinAniname);
                mPlayerKicker.transform.position = LoserPos;
                mPlayerKicker.transform.eulerAngles = LoserRot;
                mPlayerKicker.animation.Play (LoseAniname);
            }
        } else {
            CerCam.transform.position = LoserCamPos;
            CerCam.transform.eulerAngles = LoserCamRot;
            if (Ag.mgIsKick) {
                mPlayerKicker.transform.position = LoserPos;
                mPlayerKicker.transform.eulerAngles = LoserRot;
                mPlayerKicker.animation.Play (LoseAniname);

                mPlayerKeeper.transform.position = Winpos;
                mPlayerKeeper.transform.eulerAngles = WinRot;
                mPlayerKeeper.animation.Play (WinAniname);
            } else {
                mPlayerKeeper.transform.position = LoserPos;
                mPlayerKeeper.transform.eulerAngles = LoserRot;
                mPlayerKeeper.animation.Play (LoseAniname);
                mPlayerKicker.transform.position = Winpos;
                mPlayerKicker.transform.eulerAngles = WinRot;
                mPlayerKicker.animation.Play (WinAniname);
            }
        }
    }

    void SkillCeremony ()
    {
        CerCam.enabled = true;
        CerCam.animation.Stop ();
        CerCam.fieldOfView = 20;
        DefnCam.GetComponent<Camera>().enabled = false;

        mCameraDefn.enabled = false;
        mCameraKick.enabled = false;
        mPlayerKicker.animation.Stop ();
        mPlayerKeeper.animation.Stop ();

        dicGameSceneMenuList ["Ui_wineff"].SetActive (true);


        Debug.Log ("MyCeremony      :: " + MyCeremony  + "EnemyCeremony       ::  " + EnemyCeremony);


        if (MyCeremony == 1 && Ag.mgDidWin|| EnemyCeremony == 1 && !Ag.mgDidWin ) {
            ranNumStr = "2";
            ranKeeperStr = "2";
            LastCerLogic ();

        }

        if (MyCeremony == 2 && Ag.mgDidWin|| EnemyCeremony == 2 && !Ag.mgDidWin) {
            ranNumStr = "3";
            ranKeeperStr = "3";
            LastCerLogic ();

        }
        if (MyCeremony == 3 && Ag.mgDidWin|| EnemyCeremony == 3 && !Ag.mgDidWin) {
            CereMonySet (new Vector3 (5.2f, 2.6f, -35.5f), //campos
                new Vector3 (13f, 318f, 0),  //camRot
                new Vector3 (5.2f, 2.6f, -35.5f), //LoserCampos
                new Vector3 (13f, 318f, 0), //LoserCamRot
                new Vector3 (-0.1385008f, 0.04181996f, -29.03078f), //WinPos
                new Vector3 (0, 90f, 0), //WinRot
                new Vector3 (0.4513091f, 0.04181996f, -28.99653f), //LoserPos
                new Vector3 (0, 180f, 0), //LoserRot
                "Cere_Skill_Winner_01_(500F)", //WinAniname
                "Cere_Skill_Loser_03_(540F)"); //LoserAniname

            /*
            CereMonySet (new Vector3 (5.3f, 2.6f, -34f),
                             new Vector3 (15.5f, 310f, 0), 
                             new Vector3 (5.3f, 2.6f, -34f), 
                             new Vector3 (15.5f, 310f, 0), 
                             new Vector3 (0.49f, 0.04181996f, -30.55f), 
                             new Vector3 (0, 0, 0), 
                             new Vector3 (0.4513091f, 0.04181996f, -28.99653f), 
                             new Vector3 (0, 180f, 0), 
                             "Cere_Skill_Winner_03_(430F)", 
                             "Cere_Skill_Loser_02_(400F)"); 
                             */

        }
        if (MyCeremony == 4 && Ag.mgDidWin|| EnemyCeremony == 4 && !Ag.mgDidWin) {
            CereMonySet (new Vector3 (7.3f, 2.6f, -28f),
                new Vector3 (14f, 254f, 0), 
                new Vector3 (3.3f, 1.85f, -35f), 
                new Vector3 (11f, 330f, 0), 
                new Vector3 (-0.4f, 0.04181996f, -30.3f), 
                new Vector3 (0, 90, 0), 
                new Vector3 (0.4513091f, 0.04181996f, -28.99653f), 
                new Vector3 (0, 180f, 0), 
                "Cere_Skill_Winner_05_(485F)", 
                "Cere_Skill_Loser_03_(540F)"); 

        }
        if (MyCeremony == 5 && Ag.mgDidWin || EnemyCeremony == 5 && !Ag.mgDidWin) {
            CereMonySet (new Vector3 (6f, 3.18f, -28f),
                new Vector3 (23f, 253f, 360), 
                new Vector3 (5.6f, 3f, -32.4f), 
                new Vector3 (21f, 297f, 0), 
                new Vector3 (0.35f, 0.04181996f, -30.3f), 
                new Vector3 (0, 0, 0), 
                new Vector3 (0.4513091f, 0.04181996f, -28.99653f), 
                new Vector3 (0, 180f, 0), 
                "Cere_Skill_Winner_02_(430F)", 
                "Cere_Skill_Loser_02_(400F)"); 
            /*
            CereMonySet (new Vector3 (3.7f, 1.8f, -25.7f),
                             new Vector3 (12f, 212f, 0), 
                             new Vector3 (5.8f, 2.5f, -35.8f), 
                             new Vector3 (15f, 311f, 0), 
                             new Vector3 (0.2f, 0.04181996f, -32f), 
                             new Vector3 (0, 0, 0), 
                             new Vector3 (0.4513091f, 0.04181996f, -28.99653f), 
                             new Vector3 (0, 180f, 0), 
                             "Cere_Skill_Winner_04-1_(530F)", 
                             "Cere_Skill_Loser_02_(400F)"); 
                             */

        }
        if (Ag.mySelf.GetApplyIDofItem ("CeremonyDefault") == 6 || Ag.NodeObj.EnemyUser.GetApplyIDofItem ("CeremonyDefault") == 6) {
            /*
            CereMonySet (new Vector3 (-3.5f, 2.2f, -36.5f),
                             new Vector3 (13f, 37f, 0), 
                             new Vector3 (-2.1f, 2.7f, -24f), 
                             new Vector3 (18f, 157f, 0), 
                             new Vector3 (0.2f, 0.04181996f, -32f), 
                             new Vector3 (0, 0, 0), 
                             new Vector3 (0.4513091f, 0.04181996f, -28.99653f), 
                             new Vector3 (0, 180f, 0), 
                             "Cere_Skill_Winner_04-2_(530F)", 
                             "Cere_Skill_Loser_01_(560F)"); 
                             */

        }


       


    }

    IEnumerator WaitSkillAction ()
    {
        yield return new WaitForSeconds (2f);
        SkillCeremony ();
        yield return new WaitForSeconds (8f);
        mSkillCeremony = true;

    }

    bool mSkillCeremony = false;
    string ranNumStr, ranKeeperStr;
    int EnemyCeremony, MyCeremony;


    void EndingCer ()
    {
        EnemyCeremony = Ag.NodeObj.EnemyUser.GetApplyIDofItem ("CeremonyDefault");
        MyCeremony = Ag.mySelf.GetApplyIDofItem ("CeremonyDefault");


        if (Ag.mSingleMode) {
            EnemyCeremony = 0;
            switch (Ag.mVirServer.maiGradeOfBot) {
            case 2:
                if (AgUtil.RandomInclude (1, 50) % 5 == 2)
                    EnemyCeremony = AgUtil.RandomInclude (3, 5);
                break;
            case 3:
            case 4:
                EnemyCeremony = AgUtil.RandomInclude (3, 5);
                break;
            case 0:
                if (AgUtil.RandomInclude (1, 50) % 3 == 1)
                    EnemyCeremony = AgUtil.RandomInclude (3, 5);
                break;
            }
        }

        ranNumStr = "1";
        ranKeeperStr = "1";
        mSkillCeremony = true;
        if (!AgStt.mgGameTutorial) {
            if (MyCeremony > 0 && Ag.mgDidWin == true || EnemyCeremony > 0 && Ag.mgDidWin == false ) {
                StartCoroutine (WaitSkillAction ());
                mSkillCeremony = false;
            }
        }
        LastCerLogic ();

        int rannum = AgUtil.RandomInclude (1, 100);

        if (Ag.mgDidWin) {
            if (rannum % 3 == 0) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Winthegame1");
            if (rannum % 3 == 1) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Winthegame2");
            if (rannum % 3 == 2) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Winthegame3-2");
        } else {
            if (rannum % 3 == 0) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Losethegame02");
            if (rannum % 3 == 1) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Losethegame04");
            if (rannum % 3 == 2) VoiceSoundManager.Instance.Play_Effect_Sound("voice/Losethegame07");
        }

    }



    void LastCerLogic ()
    {
        //ranNumStr = "1";
        //ranKeeperStr = "1";
        CerCam.enabled = true;
        bool myWin;
        myWin = Ag.mgDidWin;





        
        Debug.Log ("ranNumStr" + ranNumStr +  "ranKeeperStr"+ ranKeeperStr  + "agmiskick :: " + Ag.mgIsKick  + " ::  " + myWin);
        
        mkeeperPos = mPlayerKeeper.transform.FindChild ("Bip001").gameObject.gameObject;
        mKickerPos = mPlayerKicker.transform.FindChild ("Bip001").gameObject.gameObject;
        
        if (Ag.mgIsKick) { //MyTurn is KickerMode
            if (myWin) {
                switch (ranNumStr) {
                case "1":
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, -30, 0);
                    break;
                case "2":
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, 165, 0);
                    break;
                case "3":
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, 165, 0);
                    break;
                }
                mPlayerKicker.transform.position = new Vector3 (2.59593f, 0.04181996f, -36.41669f);
                CerCam.animation.Play ("C_Kicker_LastCer" + ranNumStr);
                mPlayerKicker.animation.Play ("CereLastWin0" + ranNumStr);
                
                
                //keeperRight Lose
                if (IsKeeperRight ()) {
                    if (ranKeeperStr == "1")
                        mPlayerKeeper.animation.Play ("CerLastLoseA_R");
                    else
                        mPlayerKeeper.animation.Play ("Cer_Lose_Hard_B_R");
                } else {
                    //KeeperLeft Lose    
                    if (ranKeeperStr == "1")
                        mPlayerKeeper.animation.Play ("CerLastLoseA_L");
                    else
                        mPlayerKeeper.animation.Play ("Cer_Lose_Hard_B_L");
                }
            } else {
                if (ranNumStr == "1") {
                    mPlayerKicker.transform.position = new Vector3 (2.59593f, 0.04181996f, -36.41669f);
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, 180, 0);
                    mPlayerKicker.animation.Play ("DisLastLose");
                    CerCam.animation.Play ("C_Kicker_LastLose");
                    mPlayerKeeper.animation.Play ("Cere_Skill_Winner_06_(440F)");
                    mPlayerKeeper.transform.position = new Vector3 (0, 0, -44.2f);
                    mPlayerKeeper.transform.eulerAngles = new Vector3 (0, 0, 0);
                }
                if (ranNumStr == "2") {
                    mPlayerKeeper.animation.Play ("CereLastWin02");
                    CerCam.animation.Play ("C_Keeper_LastCer2");
                }
                if (ranNumStr == "3") {
                    mPlayerKeeper.animation.Play ("CereLastWin03");
                    CerCam.animation.Play ("C_Keeper_LastCer3");
                }
                mPlayerKeeper.transform.position = new Vector3 (0, 0, -44.2f);
                mPlayerKeeper.transform.eulerAngles = new Vector3 (0, 0, 0);
                //CerCam.animation.Play("C_Cere_Skill_Winner_06_(440F)");

                
            }
        } else { //MyTurn is KeeperMode

            if (myWin) { 
                if (ranKeeperStr == "1") {
                    mPlayerKeeper.animation.Play ("Cere_Skill_Winner_06_(440F)");
                    CerCam.animation.Play ("C_Cere_Skill_Winner_06_(440F)");
                }
                if (ranKeeperStr == "2") {
                    mPlayerKeeper.animation.Play ("CereLastWin02");
                    CerCam.animation.Play ("C_Keeper_LastCer2");
                }
                if (ranKeeperStr == "3") {
                    mPlayerKeeper.animation.Play ("CereLastWin03");
                    CerCam.animation.Play ("C_Keeper_LastCer3");
                }
                mPlayerKeeper.transform.position = new Vector3 (0, 0, -44.2f);
                mPlayerKeeper.transform.eulerAngles = new Vector3 (0, 0, 0);

                //Kicker Lose
                mPlayerKicker.transform.position = new Vector3 (2.59593f, 0.04181996f, -36.41669f);
                mPlayerKicker.transform.eulerAngles = new Vector3 (0, 180, 0);
                mPlayerKicker.animation.Play ("DisLastLose");
                //keeper
                
                
            } else { 
                CerCam.enabled = false;
                mCerCamAxis.SetActive (true);
                //DefnCam = GameObject.Find ("CerAxis/DefCamera").gameObject.gameObject;
                //KeeperLose
                if (IsKeeperRight ()) {
                    
                    if (ranKeeperStr == "1") { 
                        mKPlastCer = 5;
                        mPlayerKeeper.animation.Play ("CerLastLoseA_L");
                        
                    } else {
                        mKPlastCer = 6;
                        mPlayerKeeper.animation.Play ("Cer_Lose_Hard_B_L");
                    }
                } else {
                    
                    if (ranKeeperStr == "1") {
                        mKPlastCer = 7;
                        mPlayerKeeper.animation.Play ("CerLastLoseA_R");
                    } else {
                        mKPlastCer = 8;
                        mPlayerKeeper.animation.Play ("Cer_Lose_Hard_B_R");
                    }
                }
                
                //KickerWin
                switch (ranNumStr) {
                case "1":

                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, -30, 0);
                    break;
                case "2":
                    DefnCam.camera.enabled = false;
                    CerCam.enabled = true;
                    CerCam.animation.Play ("C_Kicker_LastCer2");
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, 165, 0);
                    break;
                case "3":
                    DefnCam.camera.enabled = false;
                    CerCam.enabled = true;
                    CerCam.animation.Play ("C_Kicker_LastCer3");
                    mPlayerKicker.transform.eulerAngles = new Vector3 (0, 165, 0);
                    break;
                }
                mPlayerKicker.transform.position = new Vector3 (2.59593f, 0.04181996f, -36.41669f);
                mPlayerKicker.animation.Play ("CereLastWin0" + ranNumStr);
                
            }
        }

    }

    void RedbullAni ()
    {
        //arrStatusBar[0].transform.localScale = (new Vector3(704.9203f, 0.002108231f, 0.002492417f));
        //arrStatusBar[0].transform.localPosition = (new Vector3(-0.2096453f, -1.015724f, 0.002227783f));
        //arrStatusBar[0].animation.Play("animation");
    }

    void CeremonyAnimation (bool isKicker, bool didWin)
    {
        int randNum = AgUtil.RandomInclude (1, 8);


    }

    void KeeperWinAni1 ()
    {
        mCerCamAxis.transform.Rotate (new Vector3 (0, 1.3f, 0));
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.50f, mkeeperPos.transform.position.z);
        //DefnCam.transform.LookAt (new Vector3(mKickerPos.transform.position.x,1.508962f -3f ,mKickerPos.transform.position.z + 4f));
        //mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, 180f, 0);
    }

    void KeeperWinAni2 ()
    {
        mCerCamAxis.transform.Rotate (new Vector3 (0, mRotSpeed, 0));
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z);
        
    }

    void KeeperWinAni3 ()
    {
        mCerCamAxis.transform.Rotate (new Vector3 (0, 1.3f, 0));
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z);
        //DefnCam.transform.LookAt (new Vector3(mKickerPos.transform.position.x,1.508962f -3f ,mKickerPos.transform.position.z + 4f));
        //mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, 180f, 0);
    }

    void KeeperWinAni4 ()
    {
        mCerCamAxis.transform.Rotate (new Vector3 (0, mRotSpeed, 0));
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z);
    }

    void KeeperLoseAni1 ()
    {
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z - 0.5f);
        DefnCam.transform.LookAt (new Vector3 (mKickerPos.transform.position.x, 1.508962f - 3f, mKickerPos.transform.position.z + 4f));
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
    }

    void KeeperLoseAni2 ()
    {
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z + 0.5f);
        DefnCam.transform.LookAt (new Vector3 (mKickerPos.transform.position.x, 1.508962f - 3f, mKickerPos.transform.position.z + 4f));
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
    }

    void KeeperLoseAni3 ()
    {
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z - 0.5f);
        DefnCam.transform.LookAt (new Vector3 (mKickerPos.transform.position.x, 1.508962f - 3f, mKickerPos.transform.position.z + 4f));
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
    }

    void KeeperLoseAni4 ()
    {
        mCerCamAxis.transform.position = new Vector3 (mkeeperPos.transform.position.x, 1.508962f, mkeeperPos.transform.position.z + 0.5f);
        DefnCam.transform.LookAt (new Vector3 (mKickerPos.transform.position.x, 1.508962f - 3f, mKickerPos.transform.position.z + 4f));
        mCerCamAxis.transform.eulerAngles = new Vector3 (0, DefnCam.transform.rotation.y * 100f, 0);
    }
}
