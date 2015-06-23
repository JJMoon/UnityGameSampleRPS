using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    int tempDir = 0;
    bool mAnimationStopflag = false;
    AnimationClip mKickerAniclip, mKeeperAniclip, mKickBallAniclip;

    /// <summary>
    /// 화면을 잠깐 멈췄다가 플레이
    /// </summary>
    /// <param name="pNum">P number.</param>
    void AnimaSpeedSetting (float pNum, bool pBallAni)
    {
        //Ag.LogString ("AmAni.mKickerAni :: " + AmAni.mKickerAni + "AmAni.mKeeperAni :: " + AmAni.mKeeperAni + " :AmAni.mBallAni: " + AmAni.mBallAni);
        mPlayerKicker.animation [AmAni.mKickerAni].speed = pNum;
        mPlayerKicker.animation.Play (AmAni.mKickerAni);
        mPlayerKeeper.animation [AmAni.mKeeperAni].speed = pNum;
        mPlayerKeeper.animation.Play (AmAni.mKeeperAni);

        mKickBall.animation [AmAni.mBallAni].speed = pNum;
        if (pBallAni) {
            mKickBall.animation.Play (AmAni.mBallAni);
        }
    }

    /// <summary>
    /// 리플레이 애니메이션 플레이
    /// </summary>
    void AnimaMakeRewindAni (int KickerFirstFrame, int KickerLastFrame, int KeeperFirstFrame, int KeeperLastFrame, int BallFirstFrame, int BallLastFrame)
    {
        mKickerAniclip = mPlayerKicker.animation.GetClip (AmAni.mKickerAni);
        mKeeperAniclip = mPlayerKeeper.animation.GetClip (AmAni.mKeeperAni);
        mKickBallAniclip = mKickBall.animation.GetClip (AmAni.mBallAni);
        mPlayerKicker.animation.AddClip (mKickerAniclip, "KickerAni", KickerFirstFrame, KickerLastFrame, false);
        mPlayerKeeper.animation.AddClip (mKeeperAniclip, "KeeperrAni", KeeperFirstFrame, KeeperLastFrame, false);
        mKickBall.animation.AddClip (mKickBallAniclip, "KickBallAni", BallFirstFrame, BallLastFrame, false);
     
    }

    void AnimaRewind ()
    {
        mPlayerKicker.animation.Play ("KickerAni");
        mPlayerKeeper.animation.Play ("KeeperrAni");
        mKickBall.animation.Play ("KickBallAni");
    }

    IEnumerator AnimaSound ()
    {
        yield return new WaitForSeconds (0.3f);
            SoundManager.Instance.Play_Effect_Sound ("camera_shutter");
        yield return new WaitForSeconds (0.3f);
            SoundManager.Instance.Play_Effect_Sound ("camera_shutter");

    }


    IEnumerator CoruKickerDirBarOff ()
    {
        yield return new WaitForSeconds (1f);
        SetKickerDir (false);
    }


    IEnumerator AnimaStopCoru ()
    {
        float AnimaTime = 1;
        Ag.mKickEffSound = false;
        mAnimationStopflag = false;
        //AnimaMakeRewindAni (20,40,20,40,20,40);
        mCameraDefn.animation.Play ("zoom_save_start");
        mCameraKick.animation.Play ("zoom_kicks_start");
        if (Ag.mgDirection == 5 || Ag.mgSkill < 2) {
            statusSkill ();
            SkillSoundAfter ();
            AnimaTime = 0.2f;
        }
      
        yield return new WaitForSeconds (1.92f);

        Ag.mKickEffSound = true;
         //Animation Stop and PLAY


        if (Ag.mgSkill > 1 && Ag.mgDirection != 5) {
            statusSkill ();
            SkillSoundAfter ();
            AnimaSpeedSetting (0,true); //Animation Stop and PLAY
            int rannum = AgUtil.RandomInclude (1, 10);
            if (rannum % 2 == 1) {
                mCameraDefn.animation.Play ("special_save2");
                mCameraKick.animation.Play ("special_kicks2");
                StartCoroutine (AnimaSound ());
                dicGameSceneMenuList ["Ui_focus"].SetActive (true);
            } else {
                mCameraDefn.animation.Play ("special_save");
                mCameraKick.animation.Play ("special_kicks");

            }
            AnimaTime = 1f;
        } 

        if (Ag.mgSkill < 2 && IsSameDirection () && (Ag.mgDirection != 5 || Ag.mgEnemDirec != 5) && !IsGoulerDdong() && !IsKickerDdong ()) {
            yield return new WaitForSeconds (0.33f);
            AnimaSpeedSetting (0.2f,true);
            if (Ag.mgDirection == 1 || Ag.mgDirection == 3) {
                if (Ag.mgIsKick) 
                    mCameraKick.animation.Play ("ball_zoom1_kicker");
                else 
                    mCameraDefn.animation.Play ("ball_zoom1_keeper");

            }
            if (Ag.mgDirection == 2 || Ag.mgDirection == 4) {
                if (Ag.mgIsKick) 
                    mCameraKick.animation.Play ("ball_zoom2_kicker");
                else 
                    mCameraDefn.animation.Play ("ball_zoom2_keeper");
            }
            yield return new WaitForSeconds (0.73f);
            AnimaSpeedSetting (1f,false);
            dicGameSceneMenuList ["Ui_focus"].SetActive (false);

        }


        //AnimaRewind (); //Animation Rewind 3times
        yield return new WaitForSeconds (1.1f);
        Ag.mKickEffSound = true;
        if (Ag.mgSkill > 1 && Ag.mgDirection != 5) {
            AnimaSpeedSetting (1,true);
        }
        if (Ag.mgSkill > 1 && IsSameDirection () && (Ag.mgDirection != 5 || Ag.mgEnemDirec != 5) && !IsGoulerDdong() && !IsKickerDdong ()) {
            yield return new WaitForSeconds (0.23f);
            AnimaSpeedSetting (0.2f,true);
            if (Ag.mgDirection == 1 || Ag.mgDirection == 3) {
                if (Ag.mgIsKick) 
                    mCameraKick.animation.Play ("ball_zoom1_kicker");
                else 
                    mCameraDefn.animation.Play ("ball_zoom1_keeper");

            }
            if (Ag.mgDirection == 2 || Ag.mgDirection == 4) {
                if (Ag.mgIsKick) 
                    mCameraKick.animation.Play ("ball_zoom2_kicker");
                else 
                    mCameraDefn.animation.Play ("ball_zoom2_keeper");
            }
            yield return new WaitForSeconds (0.63f);
            AnimaSpeedSetting (1f,false);
        }
        yield return new WaitForSeconds (AnimaTime);
        mAnimationStopflag = true;
    }

    void SetStateArraySingleMode ()
    {
        mStateArr.AddAMember ("Begin", 4f); //ljk 10.31
        mStateArr.AddEntryAction (() => {
           
            Ag.ContGameNum++;
            mGameOver = false;
            mNetworkError = false;
            EnemUserCheck = false;
            //TurnNum = 0;  //Ag.NodeObj.TurnNum = mTurnNum;
            Ag.NodeObj.EnemyUser = Ag.myEnem; 
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            arrMyScore = new List<bool> ();
            arrEnScore = new List<bool> ();
            arrAllMyScore = new List<bool> ();
            arrAllEnScore = new List<bool> ();
            dicGameSceneMenuList ["EnemyPointLabel"].GetComponent<UILabel> ().text = "0";
            dicGameSceneMenuList ["MyPointLabel"].GetComponent<UILabel> ().text = "0";
            if (Ag.mGuest) {
                dicGameSceneMenuList ["Mynick"].GetComponent<UILabel> ().text = "No name"; // "NONAME";
            } else {
                dicGameSceneMenuList ["Mynick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL(Ag.mySelf.WAS.TeamName);
            }
            dicGameSceneMenuList ["Enemnick"].GetComponent<UILabel> ().text = Ag.mVirServer.teamName;
            dicGameSceneMenuList ["MyScore"].GetComponent<UILabel> ().text = "0";
            dicGameSceneMenuList ["EnemScore"].GetComponent<UILabel> ().text = "0";
            
            Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);
            dicGameSceneMenuList ["IngameUserDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;


            if (Ag.mSingleMode) {
                dicGameSceneMenuList ["IngameEnemDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
            } else {
                Ag.mViewCard.CardLeagueSpritename (Ag.NodeObj.EnemyUser.WAS.League);
                dicGameSceneMenuList ["IngameEnemDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
            }

//            Ag.NodeObj.MySocket.dlgtIleft = IleftGame; // leave;
//            Ag.NodeObj.MySocket.dlgtEnemyLeft = EnemyLeftGame; // enemy Leave;
            
            if (Ag.NodeObj.AmHost.HasValue)
                Ag.mgIsKick = Ag.NodeObj.AmHost.Value;
            
            DrinkSkill ();
            Ag.mySelf.SetCostumeToCard ();
            Ag.NodeObj.EnemyUser.SetCostumeToCard ();

        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("CountDn", 0.5f);
        mStateArr.AddEntryAction (() => { 
            //Ag.LogString ("Game :: CountDn ");
            //Debug.Log ("GamePack" + Ag.mgGamePackReceived);
        });
        mStateArr.AddExitCondition (() => {
            return true;  //ljk 11 11
        }); 
        mStateArr.AddExitAction (() => {
            //Debug.Log ("GotoPreGame");
        });
        mStateArr.AddTimeOutProcess (20.0f, () => {  
            Ag.LogNewLine (20);
            Ag.LogString ("Application.LoadLevel");
            mStateArr.SetStateWithNameOf ("HeartBeat"); // [2012:11:12:MOON] Heart Beat       //mAwayMyself = true;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("PreGame", 3f);
        mStateArr.AddEntryAction (() => {
            Ag.ContGameNum += 1;
            //Debug.Log ("backGreenItemPosition    " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
            dicGameSceneMenuList ["Panel_item"].SetActive (true);
            ItemPowerUpImagechange ();
            //Debug.Log ("backGreenItemPosition    " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
            GUI.color = new Color (1f, 1f, 1f, 1f);
            if (Ag.mgIsKick)
                CreateCursor ();
            
            if (Ag.mgIsKick && !AgStt.mgGameTutorial) {   //...  Set Player Object
                dicGameSceneMenuList ["MainBar"].SetActive (true);
            } else {
                DragPosition (true);
                DragPositionF (true);
                dicGameSceneMenuList ["Panel_keeperarrow_Main2"].SetActive (true);
                dicGameSceneMenuList ["Panel_keeperarrow_set"].SetActive (true);
                StartCoroutine (KeeperTimer ());
            }
            StartCoroutine(CoruKickerDirBarOff());
            //Debug.Log (mSinglePlayerNum + "------------------------------------------------------------------mSinglePlayer");
        });
        mStateArr.AddExitAction (() => { 
            RedbullNum ();
        });
        mStateArr.AddAMember ("BeforeDirPotion", 1f);
        mStateArr.AddEntryAction (() => {
            SetKickerDir (false);
            if (Ag.mgIsKick)
                RedItemLogic ();
            
            mStage.mIsTouched = mStatusSillBar = false;
            if (mEventItemShowTime)
                StartCoroutine (WaittimeItemShow (2f));
            else {
                mEventItemShowTime = false;
            }
            if (!mDidEventPotion && !mDirMinuspotion)
                mStateArr.SetStateWithNameOf ("MidPausBiggerGamdDir");

        });
        //  ________________________________________________ Add A Member.. Ljk Mid Direction potion..
        mStateArr.AddAMember ("MidPausBiggerGamdDir", 1f);
        mStateArr.AddEntryAction (() => {
            GameObject mDirUPclone;
            if (Ag.mgIsKick) {
                if (mDidEventPotion || Ag.mGreenItemFlag) {
                    StartCoroutine (ItemeffOn ("backeffect_green"));
                    myCard.ExpandDirection ();
                    DestoryGuideBar ();
                    //DrawGuideLine2 ();
                    DrawGuideLineNew ();
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                    if (!Ag.mGreenItemFlag) {
                        Ag.mBallEventAlready = mItemflag1 = mDidEventPotion = mDirMinuspotion = false;
                        Setitem ("Anim_back_green", false);
                        Setitem ("Anim_eff03_green", false);
                    }
                }
                mStartTime = Time.timeSinceLevelLoad;
            }
        });
        
        mStateArr.AddAMember ("GameDir", 2f);
        mStateArr.AddEntryAction (() => { 

            mStage.InitCursorMove (mEventDirspeed, 300f);
            mSkillSound = mTempUseInStates = false;
            //Ag.NodeObj.GameVoid ();
            if (Ag.mgIsKick) {
                SoundManager.Instance.Play_Effect_Sound ("BarMoving_01");
            }
        });
        mStateArr.AddDuringAction (() => {
            if (mStage.mIsTouched && !mTempUseInStates) {
                SetPlayerDir2 ();   //SetStatusBar();    
                mTempUseInStates = true;
            }
        });
        mStateArr.AddAMember ("MidPaus", 0.3f);
        mStateArr.AddEntryAction (() => {
            mTimer.SetActive (false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false);
            SetSkillValues ();

            if (Ag.mgIsKick) {
                Ag.mRound++;
                if (Ag.mgDirection == 0) {
                    SetKickerDir (false);
                }
            } else {
                if (0 < Ag.mgDirection)
                    SoundManager.Instance.Play_Effect_Sound ("SelectDirection");
            }
            DragPosition (false);
            DragPositionF (false);
            dicGameSceneMenuList ["Panel_keeperarrow_Main2"].SetActive (false);
            //DragPositionLastSetDir (true);
        });
        mStateArr.AddExitAction (() => {
            mStage.InitCursorMove (0.8f, 300f); 
        });  // Save Touch Points [GAM_RLT]
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("MidPausPotion", 1f);
        mStateArr.AddEntryAction (() => {
            if (!Ag.mgIsKick)
                RedItemLogic ();
            SkillCursor ();
            if (!Ag.mgIsKick) {
                //CreateCursor ();
                //SkillCursor ();
                //KeeperUISwipeSetLastDir (Ag.mgDirection);
            }
            if (!Ag.mgIsKick && mIsKeeperSkl != 5) {
                //Debug.Log (mIsKeeperSkl + "KeeperDirNum");
                //KeeperSelectedBar (mIsKeeperSkl);
                //mKeeperUIBar [mIsKeeperSkl - 1].animation.Play ("AlphaAni2");
            }
            mStage.mIsTouched = mStatusSillBar = false;
            //mKeeperSelectBar.SetActive (false);
            if (!mDidBuyPotion && !mEventPotion && !mEventminusPotion && !Ag.mBlueItemFlag)
                mStateArr.SetStateWithNameOf ("MidPausBiggerPotion");
            else {
                if (mDidBuyPotion || Ag.mBlueItemFlag) {
                    //Debug.Log ("Drink!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                    //arrStatusBar[0].animation.Play("");
                    //GameObject.Find ("UI Root/Camera/Anchor/StatusBar/Redbull/Background").gameObject.animation.Play ("pocari");
                } 
            }
            //            Debug.Log ("Create Skl BAr");
            //DestoryGuideBar();
            if (!AgStt.mgGameTutorial) {
                dicGameSceneMenuList ["MainBar"].SetActive (false);
                dicGameSceneMenuList ["MainSkillBar"].SetActive (true);
                if (myCard.WAS.grade == "S") {
                    dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
                    dicGameSceneMenuList ["SGrade_MainSkillBar"].SetActive (true);
                }
            }
            DestoryGuideBar ();
            //DrawCreateSklLine (4, 125, 475);
            
            if (Ag.mgIsKick) {
                if (myCard.arrCostumeInCard.Count > 0) {
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes01") {
                        CostumeNum = 1;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes02") {
                        CostumeNum = 2;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes03") {
                        CostumeNum = 3;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes04") {
                        CostumeNum = 4;
                    }
                }
                //Debug.Log ("GoodBar :: GetSkillFinalValue "+ myCard.mGood);
                //myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Kick.Shirt.Texture, Ag.mySelf.arrUniform [0].Kick.Pants.Texture, Ag.mySelf.arrUniform [0].Kick.Socks.Texture, CostumeNum, out myCard.WAS.skill [0], out myCard.WAS.skill [1]);

                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);

                LegendSkillbar ();


                //Debug.Log ("GoodBar :: stateArray "+ myCard.mGood);
            } else {
                if (myCard.arrCostumeInCard.Count > 0) {
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves01") {
                        CostumeNum = 1;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves02") {
                        CostumeNum = 2;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves03") {
                        CostumeNum = 3;
                    }
                    if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves04") {
                        CostumeNum = 4;
                    }
                }
                //Debug.Log ("GoodBar :: stateArray " + myCard.mGood);
                //myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Keep.Shirt.Texture, Ag.mySelf.arrUniform [0].Keep.Pants.Texture, Ag.mySelf.arrUniform [0].Keep.Socks.Texture, CostumeNum, out myCard.WAS.skill [0], out myCard.WAS.skill [1]);


                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);
                //Debug.Log ("GoodBar :: stateArray " + myCard.mGood);
                LegendSkillbar ();
            }
            myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
            SetSkillBarTextureSize ();

        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("MidPausBiggerPotion", 1f);
        mStateArr.AddEntryAction (() => {
            mStartTime = Time.timeSinceLevelLoad;
            // Potion apply...
            //GameObject mDirUPclone;
            if (mDidBuyPotion || (Ag.mgIsKick && (mEventPotion || mEventminusPotion)) || Ag.mBlueItemFlag) {
                StartCoroutine (ItemeffOn ("backeffect_blue"));
                if (mEventPotion || mEventminusPotion)
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                
                DestoryGuideBar ();

                myCard.SetSkillPositions (myCard, true, false, false, Ag.mySelf, CostumeNum);
                if (Ag.mgIsKick) {
                    //myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Keep.Shirt.Texture, Ag.mySelf.arrUniform [0].Keep.Pants.Texture, Ag.mySelf.arrUniform [0].Keep.Socks.Texture, CostumeNum, out myCard.WAS.skill [0], out myCard.WAS.skill [1]);
                    dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                    dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
                } else {
                    //myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Keep.Shirt.Texture, Ag.mySelf.arrUniform [0].Keep.Pants.Texture, Ag.mySelf.arrUniform [0].Keep.Socks.Texture, CostumeNum, out myCard.WAS.skill [0], out myCard.WAS.skill [1]);

                    dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                    dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
                }
                
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);

                LegendSkillbar ();
                SetSkillBarTextureSize ();

                //Debug.Log ("GoodBar :: stateArray " + myCard.mGood);
                
                if (Ag.mgIsKick && (mEventPotion || mEventminusPotion)) {
                    myCard.SetSkillPositions (myCard, mDidBuyPotion, mEventPotion, mEventminusPotion, Ag.mySelf, CostumeNum);
                }
                if (!Ag.mBlueItemFlag) {
                    mDidBuyPotion = false;
                    Setitem ("Anim_back_blue", mDidBuyPotion);
                    Setitem ("Anim_eff02_blue", mDidBuyPotion);
                }
                //Debug.Log ("Good" + myCard.mGood + "Perfect" + myCard.mPerfect);
                
            }
            if (Ag.mgIsKick && mItemflag1 && Ag.mBallEventAlready && (mEventPotion || mEventminusPotion)) {
                if (Ag.mgIsKick) {
                    StartCoroutine (mRandomItemoff (0.8f));
                    mEventPotion = mEventminusPotion = mItemflag1 = Ag.mBallEventAlready = false;
                }
            }
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("GameSkl", 2f);
        mStateArr.AddEntryAction (() => {

            Ag.NodeObj.CurMyCard = myCard;
            //Ag.NodeObj.GameVoid ();
            mSkillSound = false;
            if (Ag.mgIsKick) {
                KickerScenePlay (false);
                mstatusBar = false;
            } 
            SoundManager.Instance.Play_Effect_Sound ("BarMoving_01");
            mStage.InitCursorMove (mEventSkillSpeed, 300f);
            mTempUseInStates = false;
        }); 
        mStateArr.AddDuringAction (() => {
            if (mStage.mIsTouched && !mTempUseInStates) {
                StartCoroutine (Skleff (2f, Ag.mgSkill)); //SetStatusBar();    
                mTempUseInStates = true;
            }
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("AftPaus", 0.7f);
        mStateArr.AddEntryAction (() => {




//
//            tempDir++;
//            if (tempDir == 6)
//                tempDir = Ag.NodeObj.Direction = Ag.mgDirection = 1;
//            else
//                Ag.NodeObj.Direction = Ag.mgDirection = (byte)tempDir;
////
//            if (Ag.mgIsKick) {
//                Ag.NodeObj.Skill = Ag.mgSkill = 3;
//                Ag.NodeObj.Direction = Ag.mgDirection = 5;
//            } else {
//                Ag.mgEnemDirec = 5;
//                Ag.mgEnemSkill = 2;
//                Ag.NodeObj.Direction = Ag.mgDirection = 3;
//                Ag.NodeObj.Skill = Ag.mgSkill = 2;
//            }


//
//            Ag.LogString ("  My  Send / Rcvd " + Ag.NodeObj.MySocket.arrGameSend.Count + " / " + Ag.NodeObj.MySocket.arrGameRcvd.Count );
//


            mRetryCount = 0;
            mskillflag = mStatusSillBar = true;
            //Panelka Exception.....
            if (Ag.mgIsKick && Ag.mgDirection == 5 && Ag.mgSkill > 1)
                Ag.mgSkill = 1;
            Ag.mgGamePackReceived = true;  //[2013:07-23:LJK]   
            mNetworkWaitAni ();

            TurnNum++;
        });
        mStateArr.AddExitCondition (() => {
            return Ag.mgGamePackReceived;
        });
        mStateArr.AddTimeOutProcess (25.0f, () => {  
            Ag.LogNewLine (20);
            Ag.LogString ("Application.LoadLevel");
            mStateArr.SetStateWithNameOf ("HeartBeat"); // [2012:11:12:MOON] Heart Beat   //  mAwayMyself = true;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        
        mStateArr.AddAMember ("NetWait", 1f);
        mStateArr.AddEntryAction (() => {
            Ag.LogString ("  mRetryCount : " + mRetryCount);
            if (Ag.mgIsKick) {
                Ag.mVirServer.Result (myCard);
            } else {
                Ag.mVirServer.Result (EnemCard);
            }


//            Ag.LogString ("  My  Dir / Skl " + Ag.mgDirection + " / " + Ag.mgSkill + "    Enemy D / S " + Ag.mgEnemDirec + " / " + Ag.mgEnemSkill);

            WasUserInfo uObj = new WasUserInfo () { User = Ag.mySelf, flag = 0 };

            Ag.NodeObj.GameTurnBot (TurnNum, EnemCard);


            //SoundManager.Instance.audio.volume = 1f;
            SoundManager.Instance.Play_Effect_Sound ("whistle_1");
            //SoundManager.Instance.audio.volume = 1f;

        });
        // Ljk Statemachine Add
        mStateArr.AddAMember ("PackWait", 1f);
        mStateArr.AddEntryAction (() => {

            EnemyCharacterEffect ();


        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("AnimaPlay", 0f);
        mStateArr.AddEntryAction (() => {
            StateAnimaplaySkillAni ();
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            dicGameSceneMenuList ["GoalNet_2"].SetActive (true);
            //KickerDirbaroff();
            //mPanelItem.SetActive (false);
            //CheckTurnNumber ();

            Ag.NodeObj.GameScoreAddNewTurn (new int[] {Ag.NodeObj.MyUser.arrUniform [0].Kick.Shirt.Texture, Ag.NodeObj.MyUser.arrUniform [0].Kick.Pants.Texture, 
                Ag.NodeObj.MyUser.arrUniform [0].Kick.Socks.Texture, CostumeNum,
                Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.Texture, Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.Texture, 
                Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.Texture, 1
            }); // Mine : Unif/Cstm, Enemy : Unif/Cstm

            //Ag.NodeObj.GameScoreAddNewTurn ();

            int enemD, enemS;
            Ag.NodeObj.GetEnemyDirectSkill (out enemD, out enemS);

            Ag.mgEnemDirec = (byte)enemD;
            Ag.mgEnemSkill = (byte)enemS;



            dicGameSceneMenuList ["MainBar"].SetActive (false);
            dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].SetActive (false);
            dicGameSceneMenuList ["Panel_keeperarrow_Main2"].SetActive (false);
            dicGameSceneMenuList ["Panel_keeperarrow_set"].SetActive (false);
            dicGameSceneMenuList ["Keeperinfo"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo"].SetActive (false);


              
            //Debug.Log ("arrIskick" + Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().isKick + "arrDidwin" + Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().didWin + "Iskick" + Ag.mgIsKick);
            if (Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().didWin)
                Ag.mgDidWin = true;
            else
                Ag.mgDidWin = false;
            DestoryGuideBar ();
            DragPositionF (false);
            SetKickerDir (false);
            KickerScenePlay (true);
            SoundManager.Instance.Play_Effect_Sound ("01_Crowd_ready_loop");

            //SkillSoundAfter ();
            AnimaPlay ();
            Add_ScoutValue ();
            dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo_scouter_discript"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_cash").gameObject.SetActive (true);
            StartCoroutine (AnimaStopCoru ());

        });
        mStateArr.AddDuringAction (() => { 
            mKeeperPosi.x = mBippos.transform.position.x;
            mKeeperPosi.z = mBippos2.transform.position.z;
        });
        mStateArr.AddExitCondition (() => { 
            return mAnimationStopflag;
        });

        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("Ceremony", 2.4f, "Packet");
        mStateArr.AddEntryAction (() => {
            KickerDirbaroff ();
            DragPositionLastSetDir (false);
            //--------- Kicker Keepr Selected Ani
            Ag.NodeObj.myGameLogic.GetTotalScore (Ag.mySelf.GetApplyIDofItem ("CeremonyDefault")); // 내 점수
            Ag.NodeObj.enGameLogic.GetTotalScore (Ag.myEnem.GetApplyIDofItem ("CeremonyDefault")); // 상대 점수

            //Ag.NodeObj.GetTotalScore (out mMycurScore, out mEnemyCurScore);
            dicGameSceneMenuList ["MyScore"].GetComponent<UILabel> ().text = ((int)Ag.NodeObj.myGameLogic.CurAccumTotal).ToString ();//((int)mMycurScore).ToString ();
            dicGameSceneMenuList ["EnemScore"].GetComponent<UILabel> ().text = ((int)Ag.NodeObj.enGameLogic.CurAccumTotal).ToString ();//((int)mEnemyCurScore).ToString ();
            //  ________________________________________________ LJK 2013 08 20 Delete Soon;
            //addKickSpotLight ("Bumped Diffuse");
            
            UICamEff (false);
            mStatusSillBar = mskillflag = false;
            mEffballflag = false;
            mgoldenBalleff ();
            
            if (Ag.mgIsKick && mGoldenBallEff /* &&  (mGoldenBall || mBronzeBall || mSilverBall) */) {
                if (Ag.mgDidWin) {
                    GoldenBallEvent ();
                    SoundManager.Instance.Play_Effect_Sound ("fixgoldenball");
                } else {
                    mGoldenAfter = mSilverAfter = mBronzeAfter = mGoldenBallEff = false;
                }
            }
            
            for (int i = 0; i < 4; i++) {
                arrKickerDirBar [i].SetActive (false); // active = false;
            }
            //moon Ac.ReadUserInfo ();
            mKpTrailL.GetComponent<TrailRenderer> ().enabled = false;    
            mKpTrailR.GetComponent<TrailRenderer> ().enabled = false;    
            mKickBall.GetComponent<TrailRenderer> ().enabled = false;
            
            
            mPreMyWin = (int)Ag.mgSelfWinNo;
            mPreEnWin = (int)Ag.mgEnemWinNo;
            if (Ag.mgDirection == 0)
                mMissNum--;
            if (Ag.mgSkill == 2)
                mPerfectNum++;
            if (Ag.mgSkill == 0)
                mMissNum--;
            if (Ag.mgIsKick) {
                if (Ag.mgDidWin) {
                    arrMyScore.Add (true);
                    arrAllMyScore.Add (true);
                } else {
                    arrMyScore.Add (false);
                    arrAllMyScore.Add (false);
                }
            } else {
                if (Ag.mgDidWin) {
                    arrEnScore.Add (false);
                    arrAllEnScore.Add (false);
                } else {
                    arrAllEnScore.Add (true);
                    arrEnScore.Add (true);
                }
            }
            if (arrEnScore.Count > 5 || arrMyScore.Count > 5) {  // Above 5 case... Remove all...
                for (int jk = 0; jk < 5; jk++) {
                    arrMyScore.RemoveAt (0);
                    arrEnScore.RemoveAt (0);
                    mMyPointBall [jk].SetActive (false);
                    mEnemyPointBall [jk].SetActive (false);
                }
            }
            dicGameSceneMenuList ["EnemyPointLabel"].GetComponent<UILabel> ().text = FunResultNum (arrAllEnScore).ToString ();
            dicGameSceneMenuList ["MyPointLabel"].GetComponent<UILabel> ().text = FunResultNum (arrAllMyScore).ToString ();
            if (Ag.mgIsKick) {
                mMyPointBall [arrMyScore.Count - 1].SetActive (true);
                if (Ag.mgDidWin) {
                    mMyPointBall [arrMyScore.Count - 1].GetComponent<UISprite> ().spriteName = arrMyScore [arrMyScore.Count - 1] ? "img_success" : "img_fail"; 
                } else {
                    mMyPointBall [arrMyScore.Count - 1].GetComponent<UISprite> ().spriteName = arrMyScore [arrMyScore.Count - 1] ? "img_success" : "img_fail"; 
                }
            } else {
                mEnemyPointBall [arrEnScore.Count - 1].SetActive (true);
                if (Ag.mgDidWin) {
                    mEnemyPointBall [arrEnScore.Count - 1].GetComponent<UISprite> ().spriteName = arrEnScore [arrEnScore.Count - 1] ? "img_success" : "img_fail"; 
                } else {
                    mEnemyPointBall [arrEnScore.Count - 1].GetComponent<UISprite> ().spriteName = arrEnScore [arrEnScore.Count - 1] ? "img_success" : "img_fail"; 
                    
                }
            }
            if (Ag.NodeObj.GameFinish.HasValue) {
                if (Ag.NodeObj.GameFinish.Value)
                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                else
                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                //Ag.mgDidGameFinish = false;
            }
            SoundManager.Instance.Play_Effect_Sound ("03_Crowd_goal");
            mPlayerKeeper.transform.position = new Vector3 (mKeeperPosi.x, 0, mKeeperPosi.z);
            CerAni ();   
        });
        mStateArr.AddExitAction (() => { 
//            if (Ag.mSingleMode)
//                Ag.SwitchStep ();
//            else
            Ag.mgIsKick = !Ag.mgIsKick; //ljk 11 11;
            mStateArr.SetStateWithNameOf ("CountDn");
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("EndingCeremony", 7f);
        mStateArr.AddEntryAction (() => { 
            GameResultLabelEff ();
            GameFinish ();
            MessageInfo ();
            DefnCam.GetComponent<Camera> ().enabled = true;
            mPlayerKeeper.transform.position = new Vector3 (mKeeperPosi.x, 0, mKeeperPosi.z);
            EndingCer ();
            SoundManager.Instance.Play_Effect_Sound ("04_Crowd_Game finish");
        });
        mStateArr.AddExitCondition (() => {
            return mSkillCeremony;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("ShowEndingResult", 0);
        mStateArr.AddEntryAction (() => {
            GameTotalScore ();
            if (Ag.ContGameNum >= 4) {
                FindMyChild (mResultPanel, "Panel_btn/btn_rematch", false);
                dicGameSceneMenuList ["btn_Label"].SetActive (true);
                mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9E%AC%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%ED%95%98%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
            }
            if (Ag.mySelf.myRank.WAS.winNum == 0 && Ag.mgDidWin) {
                Ag.mySelf.FirstGameDoneWithBot ();
            }

            Ag.mySelf.DidWinOrLoseGame (Ag.mgDidWin);

            mGameOver = true;
            dicGameSceneMenuList ["Panel_top"].SetActive (false);
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            dicGameSceneMenuList ["MainBar"].SetActive (false);
            dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].SetActive (false);
            EnemUserCheck = true;
            //           mMiniItem.SetActiveRecursively (false);
            mIngameObj.transform.position = new Vector3 (0, 3, 0);
            mResultPanel.SetActive (true);
            //mNoticePop.SetActive (true);
            for (int i = 0; i < ListGameObject.Count; i++) {
                DestroyObject (ListGameObject [i]);
            }
            dicGameSceneMenuList ["Keeperinfo"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo"].SetActive (false);
            dicGameSceneMenuList ["Ui_cont"].SetActive (false);
            
            mCameraDefn.camera.enabled = true;
            mCameraDefn.transform.localPosition = new Vector3 (0.6244949f, 37.91407f, -28.70337f);
            mCameraDefn.transform.localEulerAngles = new Vector3 (21.27243f, 359.0441f, 359.5659f);
            mCameraDefn.fieldOfView = 50;
            mCameraDefn.nearClipPlane = 9;
            RotStadium mStadium = new RotStadium ();
            mStadium = mRscrcMan.FindGameObject ("Stadium2", true).GetComponent<RotStadium> ();
            mStadium.mStadiumRotflag = true;
            mCameraDefn.clearFlags = CameraClearFlags.Nothing;

            int finalEarnScore;

            if (Ag.mgDidWin)
                finalEarnScore = (int)FinalWinPoDeck;
            else
                finalEarnScore = (int)FinalLosPoDeck;


            if (Ag.mgDidWin) {
                if (Ag.NodeObj.EnemyUser.WAS.League == "PRO_5")
                    mEnemyCurScore = 0;
                GameReport (Ag.NodeObj.MyUser, Ag.NodeObj.EnemyUser, (int)finalEarnScore, (int)mEnemyCurScore);
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Win");
                mWinBonus = 7;
                if (Ag.mSingleMode) {
                    mWinpoint = 0;
                } else {
                    mWinpoint = 10;
                    mAllPoint += 10;
                }
            } else {
                if (Ag.NodeObj.MyUser.WAS.League == "PRO_5")
                    finalEarnScore = 0;
                GameReport (Ag.NodeObj.EnemyUser, Ag.NodeObj.MyUser, (int)mEnemyCurScore, (int)finalEarnScore);
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Lose");
                if (Ag.mSingleMode) {
                    mWinpoint = 0;
                } else {
                    mWinpoint = -10;
                    mAllPoint -= 10;
                }
                mWinBonus = 2;
            }
            
            if (!Ag.mSingleMode)
                mAllPoint += mMissNum;
            else
                mMissNum = 0;
            mBonusCoin += mWinBonus;
            mBonusCoin += mItemBonus;
            
            StartCoroutine (CResultShow (1f));
            //mKResult.transform.FindChild ("MYnick").GetComponent<TextMesh> ().text = Ag.mySelf.mNick.ToString ().ToUpper ();
            //mCameraDefn.enabled = false;
            mCameraKick.enabled = false;
            mCerCamAxis.SetActiveRecursively (false);
            CerCam.enabled = false;
            
            //LastResult ();
            DestroyObject (mPlayerKicker);
            DestroyObject (mPlayerKeeper);
            
            DragPositionF (false);
            DragPosition (false);
            if (Ag.mSingleMode) {
            } else {
            }
            FirstGameWin ();
            if (Ag.NodeObj.EnemyUser.WAS.KkoID == "BOT") {
                mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
                dicGameSceneMenuList ["btn_Label"].SetActive (true);
                mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EC%9D%B4%EB%AF%B8%20%ED%87%B4%EC%9E%A5%ED%95%A8");
            }

        });
        mStateArr.AddExitCondition (() => {
            return false;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("GameFinish", 0);
        mStateArr.AddEntryAction (() => { 
            if (!Ag.mSingleMode)
                Application.LoadLevel ("300PrepareGame"); 
            
        });
        //  ________________________________________________ SetSerialExitMember
        mStateArr.SetSerialExitMember ();
        mStateArr.SetStateWithNameOf ("Begin");
        mStateArr.SetDebug (true, false);
        mStateArr.AddAMember ("ReadUserInfo", 0f);
        //  ////////////////////////////////////////////////     //[2012:11:12:MOON] Heart Beat
//        if (!Ag.mSingleMode)
//            AddAdditionalActions ();
        //  ////////////////////////////////////////////////     //[2012:11:12:MOON] Heart Beat
        
    }
}
