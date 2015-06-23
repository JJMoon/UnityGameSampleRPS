//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    void GoldenBallEvent ()
    {

        if (!Ag.mSingleMode) {
            if (mGoldenAfter && Ag.mgDidWin) {
                mItemBonus += 100;
            } else if (mSilverAfter && Ag.mgDidWin) {
                mItemBonus += 30;
            } else if (mBronzeAfter && Ag.mgDidWin) {
                mItemBonus += 10;
            }
        } else {
            if (mGoldenAfter && Ag.mgDidWin) {
                mItemBonus += 50;
            } else if (mSilverAfter && Ag.mgDidWin) {
                mItemBonus += 15;
            } else if (mBronzeAfter && Ag.mgDidWin) {
                mItemBonus += 5;
            }
        }
        //mGodenBallCoin.GetComponent<ParticleSystem> ().Play ();
        mGoldenAfter = mSilverAfter = mBronzeAfter = mGoldenBallEff = false;
    }

    void GoldenBallEffect ()
    {
        if (mGoldenBall) {
            BallChangeTexture ("golden copy");
        } else if (mSilverBall) {
            BallChangeTexture ("silver");
        } else if (mBronzeBall) {
            BallChangeTexture ("bronze");
        }
        mGoldenBall = mSilverBall = mBronzeBall = false;
    }

    void MiniEffectPlay (int pint)
    {
        if (pint == 1) {
            mMiniItem.SetActiveRecursively (true);
            mMiniItem.animation.Play ("EventItemScale");
            mMiniItem.transform.renderer.material.mainTexture = mRndBoxpng;

        } else if (pint == 2) {
        }
    }

    void UICamEff (bool pFlag)
    {
        mDirLightFlag = true;
        RenderSettings.ambientLight = new Color32 (139, 146, 196, 255);
    }

    void statusSkill ()
    {
       
        if (mskillflag) {


            if (Ag.mgIsKick) {
                if (Ag.mgDirection == 5) {
                    mKickBall.GetComponent<TrailRenderer> ().enabled = true;
                    mKickBall.GetComponent<TrailRenderer> ().time = 0.2f;
                    mDirUpclone1 = (GameObject)Instantiate (mSkillUpeff2, mPlayerKicker.transform.FindChild (mkickerRfoot).position, Quaternion.identity);
                    mDirUpclone1.transform.parent = mPlayerKicker.transform.FindChild (mkickerRfoot); //mDirUpclone1.animation.Play();
                    SoundManager.Instance.Play_Effect_Sound ("Sound effect Kickerfoot_good");
                }
                if (Ag.mgSkill == 0 || Ag.mgSkill == 1) {
                    //SoundManager.Instance.Play_Effect_Sound ("Sound effect Kickerfoot_good");
                }
                if (Ag.mgSkill == 2) {
                    mKickBall.GetComponent<TrailRenderer> ().enabled = true;
                    mKickBall.GetComponent<TrailRenderer> ().time = 0.2f;
                    mDirUpclone1 = (GameObject)Instantiate (mSkillUpeff2, mPlayerKicker.transform.FindChild (mkickerRfoot).position, Quaternion.identity);
                    mDirUpclone1.transform.parent = mPlayerKicker.transform.FindChild (mkickerRfoot); //mDirUpclone1.animation.Play();
                    SoundManager.Instance.Play_Effect_Sound ("Sound effect Kickerfoot_good");
 
                }
                if (Ag.mgSkill == 3) {
                    mKickBall.GetComponent<TrailRenderer> ().enabled = true;
                    mKickBall.GetComponent<TrailRenderer> ().time = 0.49f;
                    mDirUpclone1 = (GameObject)Instantiate (mSkillUpeff, mPlayerKicker.transform.FindChild (mkickerRfoot).position, Quaternion.identity);
                    mDirUpclone1.transform.parent = mPlayerKicker.transform.FindChild (mkickerRfoot);
                    SoundManager.Instance.Play_Effect_Sound ("Sound effect Kickerfoot");
                    mDirUpclone1.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
                    mDirLight.GetComponent<Light> ().intensity = 0.3f;
                    RenderSettings.ambientLight = new Color32 (80, 84, 113, 255);
                    //addKickSpotLight ("Self-Illumin/Bumped Specular");
                }
            } else {
                if (Ag.mgSkill == 0) {
                }
                if (Ag.mgSkill == 1) {/*
                    mDirUpclone2 = (GameObject)Instantiate (mDirUpeff2, mKpTrailL.transform.position, Quaternion.identity);
                    mDirUpclone2.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameL); //mDirUpclone2.animation.Play();
                    mDirUpclone3 = (GameObject)Instantiate (mDirUpeff2, mKpTrailR.transform.position, Quaternion.identity);
                    mDirUpclone3.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameR); //mDirUpclone3.animation.Play();
                    SoundManager.Instance.Play_Effect_Sound ("Sound effect GoalkeeperH_good");
                    */
                    //SoundManager.Instance.Play_Effect_Sound ("Sound effect GoalkeeperH");
                }
                if (Ag.mgSkill == 2) {
                    SoundManager.Instance.Play_Effect_Sound ("Sound effect GoalkeeperH");
                    mKpTrailL.GetComponent<TrailRenderer> ().enabled = true;
                    mKpTrailR.GetComponent<TrailRenderer> ().enabled = true;
                    mDirUpclone2 = (GameObject)Instantiate (mDirUpeff, mKpTrailL.transform.position, Quaternion.identity);
                    mDirUpclone2.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameL);
                    mDirUpclone3 = (GameObject)Instantiate (mDirUpeff, mKpTrailR.transform.position, Quaternion.identity);
                    mDirUpclone3.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameR);
                    mDirUpclone2.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
                    mDirUpclone3.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
                    mDirLight.GetComponent<Light> ().intensity = 0.3f;
                    //RenderSettings.ambientLight = new Color32 (80, 84, 113, 255);
                    //addKickSpotLight ("Self-Illumin/Bumped Specular");
                }
            }
        }
        mskillflag = false;
    }
    //-------------------------------------------------------------------------------
    void mgoldenBalleff ()
    {
        DestroyObject (mDirUpclone1);
        DestroyObject (mDirUpclone2);
        DestroyObject (mDirUpclone3);
        DestroyObject (mDirUpclone4);
        DestroyObject (mDirUpclone5);
        DestroyObject (mDirUpclone6);

        /*
        if (Ag.mgIsKick) {
            if (Ag.mgSkill == 1) {
                //DestroyObject (mDirUpclone1, 4f);
                //StartCoroutine (GameSkillEff (mDirUpclone1));
            }
            if (Ag.mgSkill == 2) {
                DestroyObject (mDirUpclone1, 0f);
                StartCoroutine (GameSkillEff (mDirUpclone1));
            }
            if (Ag.mgEnemSkill == 1) {


            }
            if (Ag.mgEnemSkill == 2) {
                DestroyObject (mDirUpclone4, 0f);
                DestroyObject (mDirUpclone5, 0f);
                //StartCoroutine (GameSkillEff (mDirUpclone4));
                //StartCoroutine (GameSkillEff (mDirUpclone5));


                //DestroyObject (mDirUpclone4, 4f);
                //DestroyObject (mDirUpclone5, 4f);
                //StartCoroutine (GameSkillEff (mDirUpclone4));
                //StartCoroutine (GameSkillEff (mDirUpclone5));
            }
        } else {
            
            if (Ag.mgSkill == 1) {

            }
            if (Ag.mgSkill == 2) {
                DestroyObject (mDirUpclone2, 4f);
                DestroyObject (mDirUpclone3, 4f);
                //StartCoroutine (GameSkillEff (mDirUpclone2));
                //StartCoroutine (GameSkillEff (mDirUpclone3));


                //DestroyObject (mDirUpclone2, 4f);
                //DestroyObject (mDirUpclone3, 4f);
                //StartCoroutine (GameSkillEff (mDirUpclone2));
                //StartCoroutine (GameSkillEff (mDirUpclone3));
            }
            if (Ag.mgEnemSkill == 1) {
                //DestroyObject (mDirUpclone6, 4f);
                //StartCoroutine (GameSkillEff (mDirUpclone6));
            }
            if (Ag.mgEnemSkill == 2) {
                DestroyObject (mDirUpclone6, 4f);
                StartCoroutine (GameSkillEff (mDirUpclone6));
            }
        }
        */
    }

    public IEnumerator GameSkillEff (GameObject pObj)
    {
        yield return new WaitForSeconds (0.5f);
        //pObj.animation.Play ();

    }
    //-------------------------------------------------------------------------------
    void ResultShow ()
    {
            
        if (ItemBonus) {
            if (mItemBonus <= 0) {
                ItemBonus = false;
                WinBonus = true;
            } else {
                if (mAllUsePoint != mItemBonus) {
                    mAllUsePoint++;
                }
                if (mAllUsePoint == mItemBonus) {
                    mAllUsePoint = 0;
                    ItemBonus = false;
                    WinBonus = true;
                }
            }
        } else if (WinBonus) {
            if (mWinBonus <= 0) {
                WinBonus = false;
                BonusCoin = true;
            } else {
                if (mAllUsePoint != mWinBonus) {
                    mAllUsePoint++;
                }
                if (mAllUsePoint == mWinBonus) {
                    mAllUsePoint = 0;
                    WinBonus = false;
                    BonusCoin = true;
                }
            }
        } else if (BonusCoin) {
            if (mBonusCoin <= 0) {
                BonusCoin = false;
                mMissPenalty = true;
            } else {
                if (mAllUsePoint != mBonusCoin) {
                    mAllUsePoint++;
                }
                if (mAllUsePoint == mBonusCoin) {
                    mAllUsePoint = 0;
                    BonusCoin = false;
                    mMissPenalty = true;
                }
            }
        } else if (mMissPenalty) {
            if (mMissNum >= 0) {
                if (mAllUsePoint != mMissNum) {

                }
                if (mAllUsePoint == mMissNum) {
                    mAllUsePoint = 0;
                    mMissPenalty = false;
                    WinPoint = true;
                }
            } else {
                if (mAllUsePoint != mMissNum) {
                    mAllUsePoint--;
                }
                if (mAllUsePoint == mMissNum) {
                    mAllUsePoint = 0;
                    mMissPenalty = false;
                    WinPoint = true;
                }
            }
        } else if (WinPoint) {
            if (mWinpoint <= 0) {
                if (mAllUsePoint != mWinpoint) {
                    mAllUsePoint--;
                }
                if (mAllUsePoint == mWinpoint) {
                    mAllUsePoint = 0;
                    WinPoint = false;
                    AllPoint1 = true;
                }
            } else {
                if (mAllUsePoint != mWinpoint) {
                    mAllUsePoint++;
                }
                if (mAllUsePoint == mWinpoint) {
                    mAllUsePoint = 0;
                    WinPoint = false;
                    AllPoint1 = true;
                }
            }
                
        } else if (AllPoint1) {
            if (mWinpoint <= 0) {
                if (mAllPoint != Ag.mgPrevScore) {
                    Ag.mgPrevScore--;
                }
                if (mAllPoint == Ag.mgPrevScore) {
                    AllPoint1 = false;
                }
            } else {
                if (mAllPoint != Ag.mgPrevScore) {
                    Ag.mgPrevScore++;
                }
                if (mAllPoint == Ag.mgPrevScore) {
                    AllPoint1 = false;
                }
            }
        } 
    }
    //----------------------------------------------------------------------------------------------
    void LastResult ()
    {
        if (Ag.mgDidWin) {
            GameObject.Find ("ResultCamera/ResultWin/Background").GetComponent<UISprite> ().spriteName = "YOUWIN";
        } else {
            GameObject.Find ("ResultCamera/ResultWin/Background").GetComponent<UISprite> ().spriteName = "YOULOSE";
        }
    }

    IEnumerator GetEventBall (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        //Debug.Log ("GetEventBall Start>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //AppointItemBall ();
        
    }

    IEnumerator KeeperTimer ()
    {   
        mTimer.SetActive (true);
        yield return new WaitForSeconds (8f);
        mTimer.SetActive (false);
        FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
        FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false);
        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].SetActive (false);
    }

	void BlueDrink ()
	{
		if (mDidBuyPotion)
			return;
		if (CombiItemListEa ("BlueDrink") < 1)
			return;
		if (AgStt.mgGameTutorial && Ag.mRound < 3)
			return;


		mItemTypeId = "BlueDrink";
		
		mDidBuyPotion = true;
        Setitem ("Anim_back_blue", true);
        Setitem ("Anim_eff02_blue", true);
        if (Ag.SingleTry > 0)
            return;
        ItemUse ();
		

	}

	void GreenDrink ()
	{
        if (!Ag.mgIsKick)
            return;
		if (mDidEventPotion)
			return;
		if (CombiItemListEa ("GreenDrink") < 1)
			return;
		if (AgStt.mgGameTutorial && Ag.mRound < 3)
			return;



		mItemTypeId = "GreenDrink";
        mDidEventPotion = true;
        Setitem ("Anim_back_green", true);
        Setitem ("Anim_eff03_green", true);
        if (Ag.SingleTry > 0)
            return;
		ItemUse ();


		
	}

	void RedDrink ()
	{
		if (mSlowEff)
			return;
		if (CombiItemListEa ("RedDrink") < 1)
			return;
		if (AgStt.mgGameTutorial && Ag.mRound < 3)
			return;


		mItemTypeId = "RedDrink";
        mSlowEff = true;
        Setitem ("Anim_back_red", true);
        Setitem ("Anim_eff01_red", true);
        if (Ag.SingleTry > 0)
            return;
		ItemUse ();
		
		
	}

    

    void DirUPEvent ()
    {
        if (AgStt.mgGameTutorial) {
            if (Ag.mRound == 3 && Ag.mgIsKick) {
                //mTeamDirUP.SetActive (true);
                //mTutoGirl.SetActive (false);
                //mTouchEff.SetActive (false);
                //mTouchItem.SetActive (false);
                mTutorCapN = 3;
            }
        }
        if (!AgStt.mgGameTutorial) {
            if (Ag.mgIsKick) {
                //mTeamDirUP.SetActive (true);
                mDidEventPotion = true;
            }
        }
    }

    void SlowPinEvent ()
    {
        if (!AgStt.mgGameTutorial) {
            //mTeamSlowPin.SetActive (true);
            mSlowEff = true;
        }
    }

    bool mEnergyUseFlag;

    void EnergyDrink ()
    {
        if (AgStt.mgGameTutorial) {
            if (Ag.mRound == 3 && !Ag.mgIsKick) {
                //mTeamSklUP.SetActive (true);
                //mTutoGirl.SetActive (false);
                mTouchEff.SetActive (false);
                mTouchItem.SetActive (false);
                mEnergyUseFlag = true;
                mPoint.SetActive (true);
                //mKpDragPositionFlag = true;
            }
        } else {
            mDidBuyPotion = true;
            mEventPotion = true;
        }
    }

    void MiniItemRndBox ()
    {
        
        mMiniItem.SetActive (true);
        mMiniItem.transform.renderer.material.mainTexture = mRndBoxpng;
        
    }
}
