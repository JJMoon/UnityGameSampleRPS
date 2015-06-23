//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    bool mStartDrag = false;
    bool mKeeperDragFlag = false, mKickerTutorStartFlag = false;

    void WasCarduniform ()
    {
        WasCardUniformCostume aObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                return;
            }
        };
        Application.LoadLevel ("StartMenu");
    }

    void SetStateArrayTutorial ()
    {
        mStateArr.AddAMember ("Begin", 7f);
        mStateArr.AddEntryAction (() => { 
            Ag.mKickEffSound = true;

            //Debug.Log ("TutorialStart");
            dicGameSceneMenuList ["Panel_greetings"].SetActive (true);
            dicGameSceneMenuList ["Panel_greetings_username"].GetComponent<UILabel> ().text = Ag.mGuest ? WWW.UnEscapeURL ("%EC%9D%BC%EB%B0%98%ED%9A%8C%EC%9B%90") : 
                StcPlatform.PltmNick;
            dicGameSceneMenuList ["Panel_greetings_teamname"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.mySelf.WAS.TeamName);

            BgmSound.Instance.Pause ();
            // Tutorial .....
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            Ag.mgIsKick = true;
            SetTutorialDictionary ();
            arrMyScore = new List<bool> ();
            arrEnScore = new List<bool> ();
            arrAllMyScore = new List<bool> ();
            arrAllEnScore = new List<bool> ();

            dicGameSceneMenuList ["Ui_tutorial"].SetActive (true);
            dicGameSceneMenuList ["EnemyPointLabel"].GetComponent<UILabel> ().text = "0";
            dicGameSceneMenuList ["MyPointLabel"].GetComponent<UILabel> ().text = "0";

            if (Ag.mGuest) {
                dicGameSceneMenuList ["Mynick"].GetComponent<UILabel> ().text = "No name"; // "NONAME";
            } else {
                dicGameSceneMenuList ["Mynick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.MyUser.WAS.TeamName);
            }
            dicGameSceneMenuList ["Enemnick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%8F%84%EC%9A%B0%EB%AF%B8");
            dicGameSceneMenuList ["MyScore"].GetComponent<UILabel> ().text = "0";
            dicGameSceneMenuList ["EnemScore"].GetComponent<UILabel> ().text = "0";

            arrMyScore.Add (true);
            arrAllMyScore.Add (true);
            arrMyScore.Add (true);
            arrAllMyScore.Add (true);
            arrMyScore.Add (false);
            arrAllMyScore.Add (false);
            arrEnScore.Add (true);
            arrAllEnScore.Add (true);
            arrEnScore.Add (true);
            arrAllEnScore.Add (true);
            arrEnScore.Add (true);
            arrAllEnScore.Add (true);
            TutorScore ();

            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TutorialBegin1");
        });
        
        mStateArr.AddAMember ("Caption", 0);
        mStateArr.AddEntryAction (() => {            
        });
        
        mStateArr.AddDuringAction (() => {
            if (Input.GetMouseButtonDown (0)) {
                dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = arrCapt [mTutorCapN++];
            }
        });
        mStateArr.AddExitCondition (() => {
            return true;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("CountDn", 1f);
        mStateArr.AddEntryAction (() => { 
            Ag.mgSkill = Ag.mgDirection = 0;
            dicGameSceneMenuList ["Panel_explain_bottom"].SetActive (false);
            if (Ag.mgIsKick) { 
                Ag.mRound++;
                EnemCard = Ag.myEnem.GetCardOrderOf (0);
                myCard = Ag.mySelf.GetCardOrderOf (Ag.mRound);
            } else {
                myCard = Ag.mySelf.GetCardOrderOf (0);
                EnemCard = Ag.mySelf.GetCardOrderOf (Ag.mRound);
            }
        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("PreGame", 3f);
        mStateArr.AddEntryAction (() => {
            mKickerTutorStartFlag = true;
            mKeeperDragFlag = false;
            mStartDrag = false;
            dicGameSceneMenuList ["Panel_greetings"].SetActive (false);
            dicGameSceneMenuList ["Ui_cont"].SetActive (true);
            mRscrcMan.FindGameObject ("Stadium2/crowdf03", true).GetComponent<Crowd> ().enabled = false;
            if (Ag.mgIsKick) {
                BarAnimationOff ();
                KickerDirbaroff ();
                dicGameSceneMenuList ["Fx_kick_left_top"].SetActive (false);
                dicGameSceneMenuList ["Fx_kick_right_top"].SetActive (false);
                dicGameSceneMenuList ["Fx_kick_left_bottom"].SetActive (false);
                dicGameSceneMenuList ["Fx_kick_right_bottom"].SetActive (false);
            } else {
                dicGameSceneMenuList ["GoalNet_2"].SetActive (false);
                dicGameSceneMenuList ["Ui_cont"].SetActive (false);
            }

            dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].SetActive (false);
            if (Ag.mgIsKick)
                dicGameSceneMenuList ["Panel_progressbar_kickbar_user"].SetActive (true);
            CreateCursor ();
            mCursor.transform.localPosition = new Vector3 (-290, -260, 0);
            mPlayerInfowid = 0.552083334f;
            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
            mTitleFlag = false;
            mCursorFlag = false;
            mIsKeeperSkl = 0;
            dicGameSceneMenuList ["Panel_txt"].SetActive (true);

            if (Ag.mRound == 1) {
                CaptionNum ("0");
                mKickerCapNum = 9;
                mKeeperCapNum = 16;
                if (Ag.mgIsKick) {
                    mTutorCapN = 1;

                    myCard.mDirectObj.mWidth = new int[4] { 100, 100, 1000, 100 };
                    myCard.mDirectObj.mPosition = new int[4] { 100, 900, 0, 0 };
                    myCard.tutorSetdirectionArea ();

                } else {
                    dicGameSceneMenuList ["tutor_Kickerinfo"].SetActive (true);
                    mTutorCapN = 13; 
                    //StartCoroutine (TitleBar2 (2f, dicGameSceneMenuList ["Panel_txt_Label_jump_course"]));
                    EnemCard.mDirectObj.mWidth = new int[4] { 100, 100, 1000, 100 };
                    EnemCard.mDirectObj.mPosition = new int[4] { 100, 900, 0, 0 };
                    EnemCard.tutorSetdirectionArea ();
                    CaptionNum ("12");
                }
            }
            if (Ag.mRound == 2) {
                mKickerCapNum = 19;
                mKeeperCapNum = 26;
                dicGameSceneMenuList ["Panel_explain"].SetActive (true);

                if (Ag.mgIsKick) {
                    mTutorCapN = 19;
                    //StartCoroutine (TitleBar2 (2f, dicGameSceneMenuList ["Panel_txt_Label_kick_power"]));
                    myCard.mDirectObj.mWidth = new int[4] { 100, 100, 1000, 100 };
                    myCard.mDirectObj.mPosition = new int[4] { 100, 900, 0, 0 };
                    myCard.tutorSetdirectionArea ();
                    //KickerDirBaroff ();
                    CaptionNum ("17");

                } else {
                    dicGameSceneMenuList ["tutor_Kickerinfo"].SetActive (false);
                    dicGameSceneMenuList ["tutor_Kickerinfo2"].SetActive (true);
                    mTutorCapN = 23;
                    //StartCoroutine (TitleBar2 (2f, dicGameSceneMenuList ["Panel_txt_Label_jump_speed"]));
                    //EnemCard.mDirectObj.mWidth = new int[4] {34,34,1000,34};
                    //EnemCard.mDirectObj.mPosition = new int[4] { 354, 900, 0, 82 };
                    EnemCard.mDirectObj.mWidth = new int[4] { 100, 100, 1000, 100 };
                    EnemCard.mDirectObj.mPosition = new int[4] { 100, 900, 0, 0 };
                    EnemCard.tutorSetdirectionArea ();
                    CaptionNum ("22");
                }
            }

            if (Ag.mRound >= 2)
                mStage.mIsTouchEnable = true;

            StartCoroutine (CoruKickerDirBarOff ());
        });
        mStateArr.AddDuringAction (() => { 
            if (Ag.mRound == 1) {
                if (Ag.mgIsKick) {
                    if (mTutorCapN == 8) {
                        Ag.LogString (mTutorCapN + "   TutorCapn  ");
                    }
                    if (mTutorCapN == 2) {
                        DirBarAni ();  //mTutorFadeBox.SetActive(true); //mObjPopupLabel.SetActive(true); 
                    }
                    if (mTutorCapN == 3) {
                    }
                    if (mTutorCapN == 7) {
                        mCursorFlag = true;
                        DrawCursor ();
                        CursorPinAni ();
                    }
                    if (mTutorCapN == 8) {
                        mCursorFlag = false;
                    }
                    if (mTutorCapN == 9) {
                        mTouchBarflag = false;
                        //dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                    }
                } else { // Keeper ..
                    if (mStartDrag) {
                        if (Ag.mgDirection == 2) {
                            mStateArr.SetStateWithNameOf ("BeforeDirPotion");
                            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");

                        }
                        if (Ag.mgDirection != 2 && Ag.mgDirection != 0) {
                            Ag.LogString ("  mTutorCapN   " + mTutorCapN);
                            StartCoroutine (CaptionRePlay ());
                            Ag.mgDirection = 0;
                            DragPosition (false);
                            DragPositionF (false);
                            DragPosition (true);
                            DragPositionF (true);
                            DragPositionLastSetDir (false);
                            mStartDrag = false;
                        }
                    }
                }
            }
            if (Ag.mRound == 2) {
                if (!Ag.mgIsKick) {
                    if (mStartDrag) {
                        if (Ag.mgDirection == 4) {
                            mStateArr.SetStateWithNameOf ("BeforeDirPotion");
                            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                        }

                        if (Ag.mgDirection != 4 && Ag.mgDirection != 0) {
                            StartCoroutine (CaptionRePlay ());
                            DragPosition (false);
                            DragPositionF (false);
                            DragPosition (true);
                            DragPositionF (true);
                            DragPositionLastSetDir (false);
                            Ag.mgDirection = 0;
                            Ag.LogString ("mTutorCapN " + mTutorCapN);
                            mStartDrag = false;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown (0)) {
                if (Ag.mgIsKick) {
                    if (Ag.mRound == 1) {

                        if (mTutorCapN < 9)
                            CaptionNum ((mTutorCapN++).ToString ());
                        if (mTutorCapN == 12)
                            StartCoroutine (TouchBarEff (1f));

                        if (mTutorCapN == 1) {
                            //dicGameSceneMenuList ["Keeperinfo"].animation.Play();
                        }
                        if (mTutorCapN == 3) {
                            dicGameSceneMenuList ["Panel_progressbar_kickbar_user"].SetActive (true);
                            for (int i = 1; i < 5; i++) {
                                dicGameSceneMenuList ["Panel_progressbar_kickbar_user_back_effect_bar" + i].gameObject.SetActive (true);
                            }
                        }
                        if (mTutorCapN == 4) {
                            BarAnimationOff ();
                            //arrKickerDirBar [0].SetActive (true);
                            KickerDirbarEff ("1");
                            arrKickerDirBar2 [0].SetActive (true);
                            //dicGameSceneMenuList ["Fx_kick_left_top"].animation.Play ();
                        }
                        if (mTutorCapN == 5) {
                            BarAnimationOff ();
                            //arrKickerDirBar [1].SetActive (true);
                            KickerDirbarEff ("2");
                            arrKickerDirBar2 [1].SetActive (true);
                            //dicGameSceneMenuList ["Fx_kick_right_top"].animation.Play ()
                        }
                        if (mTutorCapN == 6) {
                            BarAnimationOff ();
                            arrKickerDirBar2 [2].SetActive (true);
                            KickerDirbarEff ("3");
                            //dicGameSceneMenuList ["Fx_kick_left_bottom"].animation.Play ();
                        }
                        if (mTutorCapN == 7) {
                            //KickerDirBaroff();
                            BarAnimationOff ();
                            arrKickerDirBar2 [3].SetActive (true);
                            KickerDirbarEff ("4");
                            //dicGameSceneMenuList ["Fx_kick_right_bottom"].animation.Play ();
                        }
                        if (mTutorCapN == 8) {
                            for (int i = 1; i < 5; i++) {
                                dicGameSceneMenuList ["Panel_progressbar_kickbar_user_back_effect_bar" + i].gameObject.SetActive (false);
                            }
                            BarAnimationOff ();
                            arrKickerDirBar2 [0].SetActive (true);
                            arrKickerDirBar2 [1].SetActive (true);
                            arrKickerDirBar2 [2].SetActive (true);
                            arrKickerDirBar2 [3].SetActive (true);
                        }
                        if (mTutorCapN == 9) {
                            mTouchBarflag = false; 
                            Ag.LogString (mTutorCapN + "MTutorCapN");
                            //mTutorCapN++;
                            if (mKickerTutorStartFlag) {
                                dicGameSceneMenuList ["PlayMode"].SetActive (true);
                                dicGameSceneMenuList ["Panel_finger"].SetActive (true);
                                mKickerTutorStartFlag = false;
                                KickerDirbarEff ("3");
                            }
                            mCursor.SetActive (false);
                        }
                    }
                    if (Ag.mRound == 2) {
                        if (mTutorCapN == 19) {
                            if (mKickerTutorStartFlag) {
                                dicGameSceneMenuList ["PlayMode"].SetActive (true);
                                dicGameSceneMenuList ["Panel_finger1"].SetActive (true);
                                mKickerTutorStartFlag = false;
                                KickerDirbarEff ("3");
                            }
                            mCursor.SetActive (false);

                            CaptionNum ((mTutorCapN).ToString ());
                            return;
                        }
                        (mTutorCapN + "CaptionNum").HtLog ();
                        CaptionNum ((mTutorCapN++).ToString ());
                    }

                } else {

                    if (Ag.mRound == 1) {
                        if (mTutorCapN == 16)
                            mKeeperDragFlag = true;
                        if (mTutorCapN == 13) { //mTutorFadeBox.SetActive(true); 
                            //DrawGuideLineNew ();
                            //mPlayerinfoflag = true;
                            dicGameSceneMenuList ["tutor_infoeffect"].SetActive (true);
                            CaptionNum ("13");
                        }
                        if (mTutorCapN == 14) { //mTutorFadeBox.SetActive(true); 
                            CaptionNum ("13_1");
                            dicGameSceneMenuList ["Drag_finger1"].SetActive (true);
                            dicGameSceneMenuList ["shadow_bundle1"].SetActive (true);
                            dicGameSceneMenuList ["tutor_infoeffect"].SetActive (false);
                            dicGameSceneMenuList ["Ui_cont"].SetActive (true);
                            DragPosition (true);                                                 
                        }
                        if (mTutorCapN == 15) { //mTutorFadeBox.SetActive(true); 
                            CaptionNum ("14");
                            dicGameSceneMenuList ["Ui_cont"].SetActive (true);
                            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (true);
                            dicGameSceneMenuList ["tutor_Kickerinfo_eff"].SetActive (true);
                            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (true);
                            dicGameSceneMenuList ["Drag_finger1"].SetActive (false);
                            dicGameSceneMenuList ["shadow_bundle1"].SetActive (false);
                            mStartDrag = true;
                        }

                        if (mTutorCapN > 15) {
                            CaptionNum ("15");
                            dicGameSceneMenuList ["Panel_finger2"].SetActive (true);
                            dicGameSceneMenuList ["Drag_finger1"].SetActive (true);
                            dicGameSceneMenuList ["shadow_bundle1"].SetActive (true);
                            dicGameSceneMenuList ["tutor_Kickerinfo_eff"].SetActive (false);
                            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (false);
                        }
                        if (mTutorCapN <= 15) {
                            mTutorCapN++;
                        }
                    }
                    if (Ag.mRound == 2) {
                        if (mTutorCapN == 19) {
                            dicGameSceneMenuList ["Panel_finger1"].SetActive (true);
                            return;
                        }
                        if (mTutorCapN == 23) {
                            dicGameSceneMenuList ["tutor_infoeffect"].SetActive (true);
                        }
                        if (mTutorCapN == 24) {
                            dicGameSceneMenuList ["Ui_cont"].SetActive (true);
                            dicGameSceneMenuList ["tutor_infoeffect"].SetActive (false);
                            DragPosition (true);
                            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (true);
                            dicGameSceneMenuList ["Panel_keeperarrow_yellow_eff"].SetActive (true);
                            dicGameSceneMenuList ["tutor_Kickerinfo2_eff"].SetActive (false);

                        }
                        if (mTutorCapN == 25) {
                            //dicGameSceneMenuList["Panel_keepereff"].SetActive(false);
                            dicGameSceneMenuList ["Panel_finger2"].SetActive (true);
                            dicGameSceneMenuList ["Drag_finger1"].SetActive (false);
                            dicGameSceneMenuList ["Drag_finger2"].SetActive (true);
                            dicGameSceneMenuList ["shadow_bundle1"].SetActive (false);
                            dicGameSceneMenuList ["shadow_bundle2"].SetActive (true);
                            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (false);
                            dicGameSceneMenuList ["Panel_keeperarrow_yellow_eff"].SetActive (false);
                            dicGameSceneMenuList ["tutor_Kickerinfo2_eff"].SetActive (false);
                            mKeeperDragFlag = true;
                            mStartDrag = true;
                        }
                        if (mTutorCapN <= 25) {
                            CaptionNum ((mTutorCapN++).ToString ());
                        }
                    }
                }
            }
        });
        mStateArr.AddExitCondition (() => {
            return Ag.mgIsKick ? mTutorCapN > mKickerCapNum : mTutorCapN > mKeeperCapNum;
        });
        mStateArr.AddExitAction (() => { 
            RedbullNum ();
            mCursor.SetActive (true);
            dicGameSceneMenuList ["Drag_finger1"].SetActive (false);
            dicGameSceneMenuList ["Drag_finger2"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger1"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger2"].SetActive (false);
            dicGameSceneMenuList ["shadow_bundle1"].SetActive (false);
            dicGameSceneMenuList ["shadow_bundle2"].SetActive (false);
            dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (false);
            dicGameSceneMenuList ["Panel_keeperarrow_yellow_eff"].SetActive (false);
            dicGameSceneMenuList ["PlayMode"].SetActive (false);
        });
        //  ________________________________________________ Add A Member.. Ljk Mid Direction potion..
        mStateArr.AddAMember ("BeforeDirPotion", 1f);
        mStateArr.AddEntryAction (() => {

            dicGameSceneMenuList ["tutor_infoeffect"].SetActive (false);
            dicGameSceneMenuList ["tutor_Kickerinfo"].SetActive (false);
            dicGameSceneMenuList ["tutor_Kickerinfo2"].SetActive (false);
            mKeeperDragFlag = false;
            if (Ag.mgIsKick) {
                mCursorFlag = true;
            } else {
                dicGameSceneMenuList ["Drag_finger1"].SetActive (false);
                dicGameSceneMenuList ["Drag_finger2"].SetActive (false);
                dicGameSceneMenuList ["Panel_finger"].SetActive (false);
                dicGameSceneMenuList ["Panel_finger1"].SetActive (false);
                dicGameSceneMenuList ["Panel_finger2"].SetActive (false);
                dicGameSceneMenuList ["shadow_bundle1"].SetActive (false);
                dicGameSceneMenuList ["shadow_bundle2"].SetActive (false);
                dicGameSceneMenuList ["Panel_keeperarrow_red_eff"].SetActive (false);
                dicGameSceneMenuList ["Panel_keeperarrow_yellow_eff"].SetActive (false);
                mStateArr.SetStateWithNameOf ("MidPaus");
            }
            mStage.mIsTouched = mStatusSillBar = false;
            if (mEventItemShowTime)
                StartCoroutine (WaittimeItemShow (2f));
            else
                mEventItemShowTime = false;
            if (!mDidEventPotion && !mDirMinuspotion)
                mStateArr.SetStateWithNameOf ("MidPausBiggerGamdDir");
        });
        //  ________________________________________________ Add A Member.. Ljk Mid Direction potion..
        mStateArr.AddAMember ("MidPausBiggerGamdDir", 1f);
        mStateArr.AddEntryAction (() => {
            DragPosition (false);
            DragPositionF (false);

            mTitleFlag = true;
            if (Ag.mgIsKick) {
                if (mDidEventPotion) {
                    myCard.ExpandDirection ();
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                }
                if (mDirMinuspotion) {
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                }
                if (mDidEventPotion) {
                    if (Ag.mgIsKick) {
                        Ag.mBallEventAlready = mItemflag1 = mDidEventPotion = mDirMinuspotion = false;
                    }
                }
                mStartTime = Time.timeSinceLevelLoad;
            }
        });
        mStateArr.AddAMember ("Replay", 1f);
        mStateArr.AddEntryAction (() => {
            if (Ag.mgIsKick & !mTitleFlag) {
                if (Ag.mRound == 1)
                    CaptionNum ("8");
                if (Ag.mRound == 2)
                    CaptionNum ("19");
                dicGameSceneMenuList ["PlayMode"].SetActive (true);
                dicGameSceneMenuList ["Panel_finger"].SetActive (true);
                dicGameSceneMenuList ["Panel_explain"].SetActive (true);
            } else
                mTitleFlag = true;
            mCursorFlag = true;
            mStage.mIsTouched = true;
            mStage.mCursorPosition = 0;

        }); 
        mStateArr.AddExitCondition (() => {
            return mTitleFlag;
        });

        mStateArr.AddExitAction (() => {
            mCursor.SetActive (true);
            dicGameSceneMenuList ["PlayMode"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger"].SetActive (false);
            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
        });

        //  ________________________________________________ Add A Member.. Ljk Mid Direction potion..
        mStateArr.AddAMember ("GameDir", 2f);
        mStateArr.AddEntryAction (() => { 

            mStage.InitCursorMove (mEventDirspeed, 300f);
            mSkillSound = false;
            mDidEventPotion = false;
            mTempUseInStates = false;
            
            if (Ag.mgIsKick) {
                SoundManager.Instance.Play_Effect_Sound ("BarMoving_01");
            }
        });
        mStateArr.AddDuringAction (() => {
            if (!mTitleFlag)
                return;
            if (Ag.mRound == 1) {
                if (Ag.mgIsKick && !mStage.mAmIGoing) {

                    if (mTitleFlag && mStage.GetCursorPosition () < 1) {
                        dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                        StartCoroutine (CaptionRePlay ());
                        mTitleFlag = false;
                        StartCoroutine (DelayReplay ());
                        mTouchBarflag = false;
                    }
                    if (mStage.GetCursorPosition () < 1000) {
                        if (Input.GetMouseButtonDown (0)) {
                            if (mStage.GetCursorPosition () < 10 || mStage.GetCursorPosition () > 910)
                                Ag.mgDirection = 1;
                            else
                                Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);

                            if (Ag.mgDirection != 3) {
                                myCard.ShowArrArea ();
                                dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                                StartCoroutine (CaptionRePlay ());
                                mTitleFlag = mTouchBarflag = false;
                                StartCoroutine (DelayReplay ());
                            }
                        }
                    }
                }
            }
            if (Ag.mRound == 2) {
                if (Ag.mgIsKick) {
                    if (mTitleFlag && !mStage.mAmIGoing && mStage.GetCursorPosition () < 1) {
                        Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);
                        StartCoroutine (MalPungOnoff (0.1f, false));
                        dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                        StartCoroutine (CaptionRePlay ());
                        mTitleFlag = false;
                        StartCoroutine (DelayReplay ());

                        mTouchBarflag = false;
                    } 
                    if (!mStage.mAmIGoing && mStage.GetCursorPosition () < 1000) {
                        if (Input.GetMouseButtonDown (0)) {

                            if (mStage.GetCursorPosition () < 10 || mStage.GetCursorPosition () > 910)
                                Ag.mgDirection = 1;
                            else
                                Ag.mgDirection = myCard.GetPosition (mStage.mCursorPosition);

                            if (Ag.mgDirection != 3) {
                                dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                                StartCoroutine (CaptionRePlay ());
                                mTitleFlag = false;
                                StartCoroutine (DelayReplay ());
                                mTouchBarflag = false;
                            }
                        }
                    }
                }
            }
            if (mStage.mIsTouched && !mTempUseInStates) {
                mTempUseInStates = true;
            }
        });
        mStateArr.AddExitCondition (() => {
            if (Ag.mRound == 1) {
                return mTitleFlag;
            } 
            return true;
        });
        mStateArr.AddExitAction (() => {
            
        });
        mStateArr.AddAMember ("GameDirStopped", 1);
        mStateArr.AddEntryAction (() => {
            mTimer.SetActive (false);
            DragPosition (false);
            mKpSklFlag = false;
            if (Ag.mgIsKick) {
                BarAnimationOff ();
                arrKickerDirBar2 [2].SetActive (true);
                if (Ag.mRound == 1) {
                    dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                    CaptionNum ("9");
                    Ag.mgDirection = 3;
                    Ag.mgEnemDirec = 1;

                }
                if (Ag.mRound == 2) {
                    Ag.mgEnemDirec = Ag.mgDirection;
                }
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                mTutorCapN = 14;
            } else {
                if (!Ag.mgIsKick) {
                    if (Ag.mRound == 1) {
                        Ag.mgDirection = Ag.mgEnemDirec = 2;
                    }
                    if (Ag.mRound == 2) {
                        Ag.mgEnemDirec = Ag.mgDirection > 2 ? (byte)(Ag.mgDirection - 2) : (byte)(Ag.mgDirection + 2);
                    }
                }
                mStateArr.SetStateWithNameOf ("MidPaus");
                mTutorCapN = 11;
            }
            switch (Ag.mRound) {
            case 1:
                mKickerCapNum = 13;
                mKeeperCapNum = 10;
                break;
            case 2:
                mKickerCapNum = 13;
                mKeeperCapNum = 10;
                break;
                
            case 3:
                mKickerCapNum = 12;
                mKeeperCapNum = 1;
                break;
            }
        });
        
        mStateArr.AddDuringAction (() => {
            if (Input.GetMouseButtonUp (0)) {
                if (Ag.mRound == 1) {
                    Ag.LogString ("CaptionN" + mTutorCapN);
                    if (Ag.mgIsKick) {
                    } else {
                    }
                }
                if (Ag.mRound == 2) {
                    if (Ag.mgIsKick) {
                        if (Ag.mgDirection > 0) {
                        } else {
                        }
                    } else { 
                        if (Ag.mgDirection > 0) {
                        } else {
                        }
                    }
                }
            }
        });
        mStateArr.AddExitCondition (() => {
            return Ag.mgIsKick ? mTutorCapN > mKickerCapNum : mTutorCapN > mKeeperCapNum;
            // touch... exit...
        });

        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("MidPaus", 0.3f);
        mStateArr.AddEntryAction (() => {

            SetSkillValues ();
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false);

            //----------------------------------------------------------------------
            if (Ag.mRound == 1) {
                if (Ag.mgIsKick) {

                } else {
                    DragPosition (false);
                    DragPositionF (false);
                    Ag.LogString ("Enter the MidPaus");
                    mTutorCapN = 11;
                    //mTutoGirl.SetActive (false);
                    dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                    //dicGameSceneMenuList ["mTutoCaption"].SetActive (false);
                }
                mKickerCapNum = 10;
                mKeeperCapNum = 10;
            }
            if (Ag.mRound == 2) {
                if (Ag.mgIsKick) { 
                    mTutorCapN = 5;
                    mKickerCapNum = 4;
                } else {
                    DragPosition (false);
                    DragPositionF (false);
                    mTutorCapN = 5;
                    mKeeperCapNum = 4;
                }
            }
            if (Ag.mgIsKick) {
                if (Ag.mgDirection == 0)
                    SetKickerDir (false);
            } else {
                if (0 < Ag.mgDirection)
                    SoundManager.Instance.Play_Effect_Sound ("SelectDirection");
            }
        });
        mStateArr.AddExitAction (() => {
            mStage.InitCursorMove (5f, 300f); 
        });
        
        mStateArr.AddDuringAction (() => {
            if (Input.GetMouseButtonDown (0)) {
                if (Ag.mRound == 1) {
                    if (Ag.mgIsKick)
                        ;
                    else
                        ;
                }
                if (Ag.mRound == 2) {
                    if (Ag.mgIsKick)
                        ;//                        dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker2_" + (mTutorCapN++).ToString ()];;
                    else
                        mTutorCapN++;
                }
            }            
        });
        
        mStateArr.AddExitCondition (() => {
            return Ag.mgIsKick ? mTutorCapN > mKickerCapNum : mTutorCapN > mKeeperCapNum;
        });

        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("KpBeforeBar", 0f);
        mStateArr.AddEntryAction (() => {
            SkillCursor ();
            mTitleFlag = false;

            mCursor.transform.localPosition = new Vector3 (-290, mCursor.transform.localPosition.y, mCursor.transform.localPosition.z);
           
            if (!Ag.mgIsKick) {
                dicGameSceneMenuList ["Panel_keeperarrow_Main"].SetActive (false);
            }
            if (Ag.mRound == 1) {

                if (Ag.mgIsKick) {
                    myCard.WAS.skill [0] = 344;
                    myCard.WAS.skill [1] = 0;
                    myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                    StartCoroutine (TitleBar (2f, "tutotitel3"));
                } else {
                    myCard.WAS.skill [0] = 344;
                    myCard.WAS.skill [1] = 0;
                    myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                    //mKeeperSelectBar.SetActive (false);
                    //mEnemyPlayerInforLabel.SetActive (false);
                    Ag.LogString ("TitleFlag" + mTitleFlag);
                    StartCoroutine (TitleBar (2f, "ASD"));
                }
            } 
            if (Ag.mRound == 2) {
                if (Ag.mgIsKick) {
                    myCard.WAS.skill [0] = 344;
                    myCard.WAS.skill [1] = 50;
                    myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
                    KickerKeeperSklbarSelect ();
                    StartCoroutine (TitleBar (2f, "tutotitel8"));
                } else {
                    myCard.WAS.skill [0] = 344;
                    myCard.WAS.skill [1] = 50;
                    StartCoroutine (TitleBar (2f, "tutotitel9"));
                    myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
                    KickerKeeperSklbarSelect ();
                    Ag.LogString ("TitleFlag" + mTitleFlag);
                    //StartCoroutine (TitleBar (2f, "tutotite4"));
                }
            }
        });
        mStateArr.AddExitCondition (() => {
            return mTitleFlag;
        });

        mStateArr.AddExitAction (() => {
        });
        
        mStateArr.AddAMember ("KpDirBar", 0f);
        mStateArr.AddEntryAction (() => {
            mCursorFlag = false;
            mStage.mIsTouched = true;
            mKpSklFlag = false;
            //mKeeperSelectBar.SetActive (false);
            myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
            //DestoryGuideBar ();
            Ag.LogString (mTutorCapN + "TutorCap");
           
            if (Ag.mRound == 1) {
                if (Ag.mgIsKick) { 
                    dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                    CaptionNum ("10");
                    mTutorCapN = 15; 
                    mKpSklFlag = true;
                    Ag.LogString ("TUtorCap");
                    mTouchBarflag = true; //StartCoroutine(TouchBarEffLoop(1.166f) ); StartCoroutine(TouchBarEff(1.166f));
                } else {
                    CaptionNum ("16");
                    mKpSklFlag = true;
                    mTouchBarflag = true; //StartCoroutine(TouchBarEffLoop(1.166f)); 
                }
            }
            if (Ag.mRound == 2) { 
                if (Ag.mgIsKick) {
                    mKpSklFlag = true;
                    CaptionNum ("20");
                } else {
                    mTouchBarflag = true;
                    mTutorCapN = 2;
                    CaptionNum ("26");
                    mKpSklFlag = true;
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Keeper2_" + (mTutorCapN).ToString ()];
                }//}
                myCard.mPerfect = 50;

            }
            if (Ag.mRound == 3) { 
                myCard.mPerfect = 40;
                myCard.mGood = 330;
                mKpSklFlag = true;
            }
        });
        mStateArr.AddDuringAction (() => { 
            if (Input.GetMouseButtonUp (0)) {
                if (mTouchFlag)
                    return; 
                StartCoroutine (TouchDelay (0.2f));
                if (Ag.mRound == 1) {
                    if (Ag.mgIsKick) {
                        //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker" + (mTutorCapN++).ToString ()];
                        if (mTutorCapN == 15) {
                        }
                        if (mTutorCapN > 15) {
                            mKpSklFlag = true;
                            mTouchBarflag = false;
                            mTouchBarflag = false;
                            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                        }
                    }
                    if (!Ag.mgIsKick) {
                        mTutorCapN++;
                        if (mTutorCapN < 15)
                            ;
                        if (mTutorCapN > 14) {
                            mTouchBarflag = false;
                            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = "";
                            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                        }  
                    }
                }
                if (Ag.mRound == 2) {
                    if (Ag.mgIsKick) {
                        if (mTutorCapN == 2) {
                        }
                        if (mTutorCapN > 2) {
                            mKpSklFlag = true;
                            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                        }
                    }
                    if (!Ag.mgIsKick) {
                        if (mTutorCapN == 2) {
                            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Keeper2_" + (mTutorCapN++).ToString ()];
                        }
                        if (mTutorCapN > 2) {
                            dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = "";
                            mKpSklFlag = true;
                            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                        }
                    }
                    Ag.LogString (mTutorCapN.ToString ());
                }
            }
        });
        
        mStateArr.AddExitCondition (() => {
            return mKpSklFlag;
        });
        mStateArr.AddExitAction (() => {
        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("MidPausPotion", 1f);
        mStateArr.AddEntryAction (() => {
            dicGameSceneMenuList ["Panel_progressbar_kickbar_user"].SetActive (false);
            dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].SetActive (true);
            dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin2").gameObject.SetActive (false);
            dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin1").gameObject.SetActive (false);

            if (Ag.mRound == 1)
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin2").gameObject.SetActive (true);
            if (Ag.mRound == 2)
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin1").gameObject.SetActive (true);

            SkillCursor ();

            mCursorFlag = true;
            mStage.mIsTouched = mStatusSillBar = false;
            
            if (!mDidBuyPotion && !mEventPotion && !mEventminusPotion)
                mStateArr.SetStateWithNameOf ("MidPausBiggerPotion");
            else {
                if (mDidBuyPotion)
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
            }
            myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
            Ag.LogString (myCard.arrArea.Count + "ArrCount!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("MidPausBiggerPotion", 1f);
        mStateArr.AddEntryAction (() => {
            if (Ag.mgIsKick) {
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
            } else {
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
            }

            mStartTime = Time.timeSinceLevelLoad;
            if (mDidBuyPotion || (Ag.mgIsKick && (mEventPotion || mEventminusPotion))) {
                if (mEventPotion || mEventminusPotion) {
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");
                } //else if (mEventminusPotion) {                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");  
                myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                if (Ag.mgIsKick) {
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
                    KickerKeeperSklbarSelect ();
                } else {
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                    dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
                    KickerKeeperSklbarSelect ();
                }
                if (Ag.mgIsKick && (mEventPotion || mEventminusPotion)) {
                    myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);
                }
            }
            if (Ag.mgIsKick && mItemflag1 && Ag.mBallEventAlready && (mEventPotion || mEventminusPotion)) {
                if (Ag.mgIsKick) {
                    StartCoroutine (mRandomItemoff (0.8f));
                    mEventPotion = mEventminusPotion = mItemflag1 = Ag.mBallEventAlready = false;
                }
            }
        });
        mStateArr.AddAMember ("ReplaySkl", 1f);
        mStateArr.AddEntryAction (() => {
            mCursorFlag = true;
            mStage.InitCursorMove (5f, 300f);
            mStage.mIsTouched = true;
            mTitleFlag = false;
            if (!mTitleFlag) {
                if (Ag.mRound == 1) {
                    if (Ag.mgIsKick)
                        CaptionNum ("10");
                    else
                        CaptionNum ("16");
                    dicGameSceneMenuList ["PlayMode"].SetActive (true);
                    dicGameSceneMenuList ["Panel_finger"].SetActive (true);

                } else {
                    if (Ag.mgIsKick)
                        CaptionNum ("20");
                    else
                        CaptionNum ("26");
                    dicGameSceneMenuList ["PlayMode"].SetActive (true);
                    dicGameSceneMenuList ["Panel_finger1"].SetActive (true);
                }
                mCursor.SetActive (false);
            }
        }); 
        
        mStateArr.AddExitCondition (() => {
            return mTitleFlag;
        });

        mStateArr.AddExitAction (() => {
            dicGameSceneMenuList ["PlayMode"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger"].SetActive (false);
            dicGameSceneMenuList ["Panel_finger1"].SetActive (false);
            mCursor.SetActive (true);
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("GameSkl", 3f);
        mStateArr.AddEntryAction (() => {

            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
            mCursorFlag = true;
            mSkilleffflag = false;
            mSkillSound = false;
            if (Ag.mgIsKick) {
                KickerScenePlay (false);
                mstatusBar = false;
            }
            SoundManager.Instance.Play_Effect_Sound ("BarMoving_01");
            mStage.InitCursorMove (mEventSkillSpeed, 300f);
            
            mDidBuyPotion = false;
            if (Ag.mRound == 1) {
                myCard.mGood = 400;
            }
            if (Ag.mRound == 2) {
                myCard.mPerfect = 50;
            } 
        }); 
        
        mStateArr.AddDuringAction (() => {
            if (Ag.mRound == 1) {
                if (mTitleFlag && !mStage.mAmIGoing && mStage.GetCursorPosition () < 1) {
                    dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                    Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
                    Ag.mgSkill = 0;
                    mSkilleffflag = true;
                    StartCoroutine (CaptionRePlay ());
                    StartCoroutine (Skleff (2f, Ag.mgSkill));
                    StartCoroutine (DelayReplaySkl ());
                    mTitleFlag = false;
                } 
                if (!mStage.mAmIGoing && mStage.GetCursorPosition () < 999) {
                    if (Input.GetMouseButtonDown (0)) {
                        if (mSkilleffflag)
                            return;
                        Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
                        mSkilleffflag = true;
                        
                        if (Ag.mgSkill == 0) {
                            mSkilleffflag = true;
                            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            StartCoroutine (CaptionRePlay ());
                            StartCoroutine (Skleff (2f, Ag.mgSkill));
                            StartCoroutine (DelayReplaySkl ());
                            mTitleFlag = false;
                        }
                        if (Ag.mgSkill == 1) {
                            mSkilleffflag = true;
                            StartCoroutine (Skleff (2f, Ag.mgSkill));
                            mTitleFlag = true;
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                        }

                        if (Ag.mgSkill == 2) {
                            Ag.mgSkill = 1;
                            mSkilleffflag = true;
                            StartCoroutine (Skleff (2f, Ag.mgSkill));
                            mTitleFlag = true;
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                        }
                    }
                } 
            }

            if (Ag.mRound == 2) {
                if (mTitleFlag && !mStage.mAmIGoing && mStage.GetCursorPosition () < 1) {
                    dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                    Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
                    StartCoroutine (Skleff (2f, Ag.mgSkill));
                    StartCoroutine (CaptionRePlay ());
                    //mStateArr.SetStateWithNameOf ("ReplaySkl");
                    StartCoroutine (DelayReplaySkl ());
                    mTitleFlag = false;
                    mSkilleffflag = true;
                }
                if (!mStage.mAmIGoing && mStage.GetCursorPosition () < 999) {
                    if (Input.GetMouseButtonDown (0)) {
                        if (mSkilleffflag)
                            return;
                        Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
                        if (Ag.mgSkill == 0) {
                            mSkilleffflag = true;
                            //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
                            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["RePlay"];
                            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            //mStateArr.SetStateWithNameOf ("ReplaySkl");
                            StartCoroutine (CaptionRePlay ());
                            StartCoroutine (DelayReplaySkl ());
                            mTitleFlag = false;
                        }

                        if (Ag.mgSkill == 2) {
                            mSkilleffflag = true;
                            //dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            StartCoroutine (Skleff (2f, Ag.mgSkill));
                            Ag.LogString ("SklEff Activated" + mskillflag);
                            mTitleFlag = true;
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                        }

                        if (Ag.mgSkill == 1) {
                            Ag.mgSkill = 2;
                            mSkilleffflag = true;
                            //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
                            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = Ag.mgIsKick ? dicCapt ["Kicker2_2"] : dicCapt ["Keeper2_3"];
                            //dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                            StartCoroutine (Skleff (2f, Ag.mgSkill));
                            Ag.LogString ("SklEff Activated" + mskillflag);
                            mTitleFlag = true;
                            VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialProceed1");
                        }

                    }
                }
                if (mStage.mIsTouched && Ag.mgSkill == 1 || Ag.mgSkill == 2) {
                    mStage.mCursorPosition = 241;
                    DrawCursor ();
                }
                
            }
            Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
            if (mStage.mIsTouched && !mTempUseInStates) {
                mTempUseInStates = true;
            }
        });
        
        mStateArr.AddExitCondition (() => {
            return mTitleFlag;
        });
        mStateArr.AddExitAction (() => {
            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
            if (Ag.mgIsKick) {
                if (Ag.mRound == 1)
                    CaptionNum ("11");
                if (Ag.mRound == 2)
                    CaptionNum ("21");
            } else {
                if (Ag.mRound == 1)
                    CaptionNum ("11");
                if (Ag.mRound == 2)
                    CaptionNum ("27");
            }

        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("AftPaus", 2f);
        mStateArr.AddEntryAction (() => {
            Ag.LogString ("myDir    :" + Ag.mgDirection + "     mySkl     :" + Ag.mgSkill + "       enDir         :" + Ag.mgEnemDirec + "      enskl      " + Ag.mgEnemSkill);
            if (Ag.mgIsKick) {
                if (Ag.mRound == 1) {
                    Ag.mgDirection = 3;
                    Ag.mgEnemDirec = 1;
                }
                if (Ag.mRound == 2)
                    Ag.mgEnemDirec = Ag.mgDirection;
                if (Ag.mRound == 3)
                    Ag.mgEnemDirec = (Ag.mgDirection == 4) ? (byte)1 : (byte)(Ag.mgDirection + 1);
            } else {
                if (Ag.mRound == 1) {
                    Ag.mgDirection = 2;
                    Ag.mgEnemDirec = 2;
                }
                if (Ag.mRound == 2) {
                    if (Ag.mgDirection < 3)
                        Ag.mgEnemDirec = (byte)(Ag.mgDirection + 2);
                    else
                        Ag.mgEnemDirec = (byte)(Ag.mgDirection - 2);
                }
                if (Ag.mRound == 3)
                    Ag.mgEnemDirec = Ag.mgDirection;
            }
            
            Ag.mgDidWin = true;
            //            mTutorFadeBox.SetActive(false);
            if (Ag.mRound == 1) {
                dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                //mTutoGirl.SetActive (true);
                if (Ag.mgIsKick) {
                    //mSkilEffFlag = true;
                    mskillflag = mStatusSillBar = true;
//                    mTouchBarEff.SetActive (false);
                    //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker16"];
                    mTutorCapN = 14;
                    Ag.mgSkill = 1;
                    Ag.mgEnemSkill = 1;
                    //Ag.mgDidWin = true;
                } else {
                    Ag.mgSkill = 1;
                    Ag.mgEnemSkill = 1;
                    //mSkilEffFlag = true;
                    mskillflag = mStatusSillBar = true;
//                    mTouchBarEff.SetActive (false);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Keeper15"];
                    //CaptionNum("14");
                    mTutorCapN = 1;
                    mKickerCapNum = 13;
                    mKeeperCapNum = 14;
                }
            }
            if (Ag.mRound == 2) {
                dicGameSceneMenuList ["Panel_explain"].SetActive (true);
                //mTutoGirl.SetActive (true);
                mTutorCapN = 4;
                mKickerCapNum = 5;
                if (Ag.mgIsKick) {
                    Ag.mgSkill = 2;
                    Ag.mgEnemSkill = 1;
                    //mSkilEffFlag = true;
                    mskillflag = mStatusSillBar = true;
                    //mTouchBarEff.SetActive (false);
                    //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker2_2"];
                } else {
                    Ag.mgSkill = 2;
                    Ag.mgEnemSkill = 1;
                    //mSkilEffFlag = true;
                    mskillflag = mStatusSillBar = true;
                    //mTouchBarEff.SetActive (false);
                    //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Keeper2_3"];
                }
                //mKeeperCapNum = 10;
            }
            if (Ag.mRound == 2 || Ag.mRound == 3)
                mskillflag = mStatusSillBar = true;
            //Panelka Exception.....
            if (Ag.mgIsKick && Ag.mgDirection == 5 && Ag.mgSkill > 1)
                Ag.mgSkill = 1;
            //mCrowd.GetComponent<Crowd> ().mFlag = true;
            //           mParTicle.GetComponent<ParticleSystem> ().Play ();
            Ag.mgGamePackReceived = false;
            mNetworkWaitAni ();
            //mStage.mIsTouched = false;
        });
        
        mStateArr.AddDuringAction (() => {
            if (!mStage.mAmIGoing && mStage.GetCursorPosition () < 300) {
                mStage.mIsTouched = true;
                Ag.mgSkill = myCard.GetPosition (mStage.mCursorPosition);
            }
            
            if (Input.GetMouseButtonUp (0)) {
                if (mTouchFlag)
                    return; 
                StartCoroutine (TouchDelay (0.2f));
                
                if (Ag.mRound == 1) {
                    if (Ag.mgIsKick) {
                        if (mTutorCapN == 14) {
                            //mSkilEffFlag = true;
                            mskillflag = mStatusSillBar = true;
                            //mTouchBarEff.SetActive (false);
                        }
                    } else {
                        //dicGameSceneMenuList["mTutoCaption"].GetComponent<UILabel>().text = dicCapt ["Keeper"+(mTutorCapN++).ToString()];
                    }
                } 
                if (Ag.mRound == 2) {
                    if (Ag.mgIsKick)
                        ;//dicGameSceneMenuList["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker2_" + (mTutorCapN++).ToString ()];
                    else
                        mTutorCapN++;
                    //dicGameSceneMenuList["mTutoCaption"].GetComponent<UILabel>().text = dicCapt ["Keeper2_"+(mTutorCapN++).ToString()];
                }
                if (Ag.mRound == 3) {
                    //if (Ag.mgIsKick) 
                    //    ;//dicGameSceneMenuList["mTutoCaption"].GetComponent<UILabel>().text = dicCapt ["Kicker3_2"+(mTutorCapN++).ToString()];
                    //else 
                    //  ;//dicGameSceneMenuList["mTutoCaption"].GetComponent<UILabel>().text = dicCapt ["Kicker3_3"+(mTutorCapN++).ToString()];
                }
            }
            
            if (mStage.mIsTouched && !mTempUseInStates) {
                mTempUseInStates = true;
            }
        });
        mStateArr.AddExitAction (() => { 
            //mTouchBarEff.SetActive (false);
        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("NetWait", 0.1f);
        mStateArr.AddEntryAction (() => { 
            mCursorFlag = false;
            SoundManager.Instance.Play_Effect_Sound ("whistle_1");
            //SoundManager.Instance.audio.volume = 1f;
            EnemyCharacterEffect ();
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("AnimaPlay", 0f);
        mStateArr.AddEntryAction (() => {

            dicGameSceneMenuList ["GoalNet_2"].SetActive (true);
            DragPosition (false);
            KickerDirbaroff ();
            dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo"].SetActive (false);
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
            //mKeeperDirBar.SetActive (false);
            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
            //dicGameSceneMenuList ["mTutoCaption"].SetActive (false);
            //mTutoGirl.SetActive (false);
            //---------------------------------------------------------
            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = "";
            if (Ag.mRound == 1) {
                mTutorCapN = 17;
                mKickerCapNum = 16;
                mKeeperCapNum = 15;
            }
            if (Ag.mRound == 2) {
                mTutorCapN = 5;
                mKickerCapNum = 4;
                mKeeperCapNum = 4;
            }
            if (Ag.mRound == 3) {
                mTutorCapN = 4;
                mKickerCapNum = 3;
                mKeeperCapNum = 3;
            }
            //---------------------------------------------------------
            mPlayerInfoX = 0.3f;
            mPlayerInfoY = 0.2f;
            mPlayerInfowid = 0f;
            MplayerInfoHeight = 0.23f;
            DragPositionF (false);
            DragPositionLastSetDir (false);
            SetKickerDir (false);
            KickerScenePlay (true);
            SoundManager.Instance.Play_Effect_Sound ("01_Crowd_ready_loop");
            
           
            SkillSoundAfter ();
            AnimaPlay ();

            StartCoroutine (AnimaStopCoru ());



        });
        mStateArr.AddDuringAction (() => { 
            if (Ag.mgIsKick) {
                if (Ag.mRound == 3) {
                    if (Ag.mgDirection == 1 || Ag.mgDirection == 3)
                        dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker3_2"];
                    else
                        dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker3_3"];
                }
            }
            if (Input.GetMouseButtonUp (0)) {
                if (mTouchFlag)
                    return; 
                StartCoroutine (TouchDelay (0.2f));
                if (Ag.mRound == 1) {
                    if (Ag.mgIsKick) {
                        mTutorCapN++;
                    } else {
                        ;
                    }
                }
                if (Ag.mRound == 2) {
                }

                Ag.LogString ("mTutorCapn" + mTutorCapN);
            }
            mKeeperPosi.x = mBippos.transform.position.x;
            mKeeperPosi.z = mBippos2.transform.position.z;
        });
        mStateArr.AddExitCondition (() => { 
            return Ag.mgIsKick ? (mTutorCapN > mKickerCapNum) && mAnimationStopflag : (mTutorCapN > mKeeperCapNum) && mAnimationStopflag;
        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("Ceremony", 2.4f, "Packet");
        mStateArr.AddEntryAction (() => {

            //dicGameSceneMenuList ["Panel_attackresult"].SetActive (true);
            attackresultBaroff ();
            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
            //dicGameSceneMenuList ["Panel_explain"].SetActive (true);
            //dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
            dicGameSceneMenuList ["Panel_explain_bottom"].SetActive (true);

            for (int i = 1; i < 5; i++) {
                dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain" + i).gameObject.SetActive (false);
            }

            if (Ag.mRound == 1) {
                //StartCoroutine (DirWinAni (2f));
                if (Ag.mgIsKick) {
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["KickerCerCap1"];
                    dicGameSceneMenuList ["kick_end"].SetActive (true);
                    dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain1").gameObject.SetActive (true);
                } else {
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["KeeperCerCap2"];
                    dicGameSceneMenuList ["keeper_end"].SetActive (true);
                    dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain2").gameObject.SetActive (true);
                } 
            }
            if (Ag.mRound == 2) {
                if (Ag.mgIsKick) {
                    mTutorCapN = 1;
                    dicGameSceneMenuList ["blaze_end"].SetActive (true);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["KickerCerCap2_1"];
                    dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain3").gameObject.SetActive (true);
                } else {
                    mTutorCapN = 1;
                    dicGameSceneMenuList ["lightning_end"].SetActive (true);
                    //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["KeeperCerCap2_1"];
                    dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain4").gameObject.SetActive (true);
                }
            }
            mCerFlag = false;
            //addKickSpotLight ("Bumped Diffuse");
            
            UICamEff (false);
            mStatusSillBar = mskillflag = false;
            mEffballflag = false;
            mgoldenBalleff ();
            
            if (Ag.mgIsKick && mGoldenBallEff) { // &&  (mGoldenBall || mBronzeBall || mSilverBall) 
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
            //Ac.ReadUserInfo ();
            mKpTrailL.GetComponent<TrailRenderer> ().enabled = false;    
            mKpTrailR.GetComponent<TrailRenderer> ().enabled = false;    
            mKickBall.GetComponent<TrailRenderer> ().enabled = false;    
            
            // Red & Blue  Signal Display... Setting...
            mPreMyWin = Ag.mRound;
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
            DestroyObject (mBall);
            //DestroyObject (mKickBall);
            TutorScore ();
            SoundManager.Instance.Play_Effect_Sound ("03_Crowd_goal");
            mPlayerKeeper.transform.position = new Vector3 (mKeeperPosi.x, 0, mKeeperPosi.z);
            CerAni ();   
        });
        mStateArr.AddDuringAction (() => { 
            if (Ag.mRound == 3)
                mCerFlag = true;
            if (Input.GetMouseButtonDown (0)) {
                if (Ag.mgIsKick) {
                    if (Ag.mRound == 1)
                        mCerFlag = true;
                    if (Ag.mRound == 2) {

                        mCerFlag = true;
                    }
                    if (Ag.mRound == 3)
                        mCerFlag = true;
                } else {
                    if (Ag.mRound == 1)
                        mCerFlag = true;
                    if (Ag.mRound == 2) {
                        mCerFlag = true;
                    }
                    if (Ag.mRound == 3)
                        mCerFlag = true;
                }
            }

        });
        
        mStateArr.AddExitCondition (() => { 
            return mCerFlag;
        });
        mStateArr.AddExitAction (() => { 
            if (Ag.mRound == 2) {
                if (!Ag.mgIsKick) {
                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                    return;
                }
            }

            dicGameSceneMenuList ["Panel_attackresult"].SetActive (false);
            Ag.mgIsKick = !Ag.mgIsKick;
            mStateArr.SetStateWithNameOf ("CountDn");
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("EndingCeremony", 7f);
        mStateArr.AddEntryAction (() => { 
            Ag.mgIsKick = false;
            //dicGameSceneMenuList ["mTutoCaption"].SetActive (false);
            mTutorCapN = 28;
            dicGameSceneMenuList ["Panel_explain"].SetActive (true);
            CaptionNum ("28");
            //dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Kicker3_5"];
            //dicGameSceneMenuList ["Panel_explain_bottom"].transform.FindChild ("bottom_explain1/Label_explain").GetComponent<UILabel> ().text = dicCapt ["Kicker3_5"];
            dicGameSceneMenuList ["Panel_explain_bottom"].SetActive (false);
            //mPlayerKeeper.transform.position = new Vector3 (mKeeperPosi.x, 0, mKeeperPosi.z);
            //mCerCamAxis.SetActive (true);
            DefnCam.gameObject.camera.enabled = true;
            //EndingCer ();
            CerCam.enabled = false;
            mCameraDefn.camera.enabled = true;
            mCameraDefn.transform.localPosition = new Vector3 (0.6244949f, 37.91407f, -28.70337f);
            mCameraDefn.transform.localEulerAngles = new Vector3 (21.27243f, 359.0441f, 359.5659f);
            mCameraDefn.fieldOfView = 50;
            mCameraDefn.nearClipPlane = 9;
            RotStadium mStadium = new RotStadium ();
            mStadium = mRscrcMan.FindGameObject ("Stadium2", true).GetComponent<RotStadium> ();
            mStadium.mStadiumRotflag = true;



            dicGameSceneMenuList ["Panel_top"].SetActive (false);
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            dicGameSceneMenuList ["MainBar"].SetActive (false);
            dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].SetActive (false);
            dicGameSceneMenuList ["Keeperinfo"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo"].SetActive (false);
            dicGameSceneMenuList ["Ui_cont"].SetActive (false);

            SoundManager.Instance.Play_Effect_Sound ("04_Crowd_Game finish");
        });
        mStateArr.AddDuringAction (() => { 
            if (Input.GetMouseButtonDown (0)) {
                if (Ag.mRound == 2) {
                    if (mTutorCapN <= 29) {
                        if (mTutorCapN == 28)
                            CaptionNum ("30");
                    }

                    if (mTutorCapN == 29) {
                        /*
                        dicGameSceneMenuList ["Panel_explain"].SetActive (false);
                        //dicGameSceneMenuList ["mTutoCaption"].SetActive (false);
                        dicGameSceneMenuList ["Panel_reward"].SetActive (true);
                        dicGameSceneMenuList ["Panel_explain_bottom"].SetActive (false);
                        */
                        PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", true);
                        PreviewLabs.PlayerPrefs.Flush ();
                        WasCarduniform ();
                        VoiceSoundManager.Instance.Play_Effect_Sound ("voice/TurotialEnd1");

                    }
                    mTutorCapN++;
                }
            }
        });


        mStateArr.AddExitCondition (() => { 
            return false;
        });
        
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("ShowEndingResult", 0);
        mStateArr.AddEntryAction (() => {
            mMiniItem.SetActive (false);
            
            if (Ag.mgDidWin) {
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Win");
            } else {
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Lose");
            }
            StartCoroutine (CResultShow (1f));
            mCameraDefn.enabled = false;
            mCameraKick.enabled = false;
            mCerCamAxis.SetActive (false);
            CerCam.enabled = false;
            LastResult ();
            DestroyObject (mPlayerKicker);
            DestroyObject (mPlayerKeeper);
            DragPositionF (false);
            DragPosition (false);
            
            GameObject.Find ("UI Root/Camera/Anchor").SetActive (false);
        });
        mStateArr.AddExitCondition (() => {
            return false;
        });
        //  ________________________________________________ Add A Member.. Add A Member..
        mStateArr.AddAMember ("GameFinish", 0);
        mStateArr.AddEntryAction (() => { 
            //Ac.ReadUserInfo ();
            if (!Ag.mSingleMode)
                Application.LoadLevel ("300PrepareGame"); 
        });
        //  ________________________________________________ SetSerialExitMember
        mStateArr.SetSerialExitMember ();
        mStateArr.SetStateWithNameOf ("Begin");
        mStateArr.SetDebug (true, false);
    }
}
