//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    int CostumeNum, CostumeNumEnem;
    bool mTempUseInStates;
    //int mTurnNum, mRetryCount;
    int mRetryCount;
    List<bool> arrAllMyScore, arrAllEnScore;
    public float mMycurScore, mEnemyCurScore;
    int mSkill0, mSkill1;

    void DrinkSkill ()
    {
        if (Ag.mBlueItemFlag) {
            Setitem ("Anim_back_blue", true);
            Setitem ("Anim_eff02_blue", true);
            dicGameSceneMenuList ["btn_drink_blue"].GetComponent<BoxCollider> ().enabled = false;
        }
        if (Ag.mRedItemFlag) {
            Setitem ("Anim_back_red", true);
            Setitem ("Anim_eff01_red", true);
            dicGameSceneMenuList ["btn_drink_red"].GetComponent<BoxCollider> ().enabled = false;
        }
        if (Ag.mGreenItemFlag) {
            //Debug.Log ("backGreenItemPosition    " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
            Setitem ("Anim_back_green", true);
            Setitem ("Anim_eff03_green", true);
            //Debug.Log ("backGreenItemPosition    " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
            //dicGameSceneMenuList["Anim_eff03_green"].gameObject.transform.localPosition = new Vector3(0,-180,0);
            //dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition = new Vector3(0,-180,0);
            dicGameSceneMenuList ["btn_drink_green"].GetComponent<BoxCollider> ().enabled = false;
            //Debug.Log ("backGreenItemPosition    " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
        }
    }

    void ItemPowerUpImagechange ()
    {
        dicGameSceneMenuList ["Panel_item"].transform.FindChild ("btn_drink_blue/Sprite (icon_drink00)").gameObject.SetActive (false);
        dicGameSceneMenuList ["Panel_item"].transform.FindChild ("btn_drink_blue/Background").gameObject.SetActive (false);

        if (Ag.mgIsKick) {
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].SetActive (false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
            dicGameSceneMenuList ["Panel_item"].transform.FindChild ("btn_drink_blue/Sprite (icon_drink00)").gameObject.SetActive (true);
        } else {
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].SetActive (false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
            dicGameSceneMenuList ["Panel_item"].transform.FindChild ("btn_drink_blue/Background").gameObject.SetActive (true);
        }
    }

    void SetLabelTextAtBegin ()
    {
        dicGameSceneMenuList ["EnemyPointLabel"].GetComponent<UILabel> ().text = "0";
        dicGameSceneMenuList ["MyPointLabel"].GetComponent<UILabel> ().text = "0";
        dicGameSceneMenuList ["Mynick"].GetComponent<UILabel> ().text = Ag.mGuest ? "No name" : WWW.UnEscapeURL (Ag.NodeObj.MyUser.WAS.TeamName);  // KKO / Guest ..
        dicGameSceneMenuList ["Enemnick"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (Ag.NodeObj.MySocket.CurEnemy.teamName);
        dicGameSceneMenuList ["MyScore"].GetComponent<UILabel> ().text = "0";
        dicGameSceneMenuList ["EnemScore"].GetComponent<UILabel> ().text = "0";
        Ag.mViewCard.CardLeagueSpritename (Ag.mySelf.WAS.League);
        dicGameSceneMenuList ["IngameUserDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        if (Ag.mSingleMode) {
            dicGameSceneMenuList ["IngameEnemDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        } else {
            Ag.mViewCard.CardLeagueSpritename (Ag.NodeObj.MySocket.CurEnemy.league);
            dicGameSceneMenuList ["IngameEnemDiv"].GetComponent<UISprite> ().spriteName = Ag.mViewCard.LeagueSpriteNameS;
        }
    }

    void SetStateArray ()
    {
        //  _////////////////////////////////////////////////_    _____  State  _____   Begin   _____
        mStateArr.AddAMember ("Begin", 4f); //ljk 10.31
        mStateArr.AddEntryAction (() => {
            if (NullCheck ()) {
                Ag.LogIntenseWord ("  GameScene :: mStateArr.AddAMember (''Begin'', 4f)  ....    NullCheck  True   ");
                dicGameSceneMenuList ["popup"].SetActive (true);
                dicGameSceneMenuList ["alert_someoneout"].SetActive (true);
                return;
            }
            if (!Ag.NodeObj.AmHost.HasValue) {  //  Pause ...    I went out ...
                Ag.LogDouble ("     Begin   AmHost ...  null Value .....   return   !!!!!    ");
                mStateArr.SetStateWithNameOf ("PreGame");
                return;
            }

            SetLabelTextAtBegin ();



            Ag.ContGameNum++;
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            mGameOver = false;
            mNetworkError = false;
            EnemUserCheck = false;
            if (!Ag.mSingleMode)
                Ag.mgIsKick = Ag.NodeObj.MySocket.IsGameHost.Value;
            arrMyScore = new List<bool> ();
            arrEnScore = new List<bool> ();
            arrAllMyScore = new List<bool> ();
            arrAllEnScore = new List<bool> ();


            // Network related ...
            Ag.NodeObj.MySocket.dlgtIleft = IleftGame; // leave;
            Ag.NodeObj.MySocket.dlgtEnemyLeft = EnemyLeftGame; // enemy Leave;
            if (Ag.NodeObj.AmHost.HasValue)
                Ag.mgIsKick = Ag.NodeObj.AmHost.Value;


            Ag.mySelf.SetCostumeToCard ();
            Ag.NodeObj.EnemyUser.SetCostumeToCard ();


            
            DrinkSkill ();


        });
        //  _////////////////////////////////////////////////_    _____  State  _____   CountDn   _____
        mStateArr.AddAMember ("CountDn", 0.5f);
        mStateArr.AddEntryAction (() => { 
            Ag.LogString ("Game :: CountDn ");
            //Debug.Log ("GamePack" + Ag.mgGamePackReceived);
        });
        mStateArr.AddExitCondition (() => {
            if (Ag.mSingleMode)
                return Ag.mgGamePackReceived;  //ljk 11 11
            else
                return true;  //ljk 11 11
        }); 
        mStateArr.AddTimeOutProcess (20.0f, () => {  
            Ag.LogNewLine (20);
            Ag.LogString ("Application.LoadLevel");
            mStateArr.SetStateWithNameOf ("HeartBeat"); // [2012:11:12:MOON] Heart Beat       //mAwayMyself = true;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   PreGame   _____
        mStateArr.AddAMember ("PreGame", 3f);
        mStateArr.AddEntryAction (() => {
            dicGameSceneMenuList ["Panel_item"].SetActive (true);
            ItemPowerUpImagechange ();
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

            StartCoroutine (CoruKickerDirBarOff ());
        });
        mStateArr.AddExitAction (() => { 
            RedbullNum ();
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   BeforeDirPotion   _____
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
            else { 
                //mDirBareff.GetComponent<ParticleSystem> ().Play ();
            }
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   MidPausBiggerGamdDir   _____
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
        //  _////////////////////////////////////////////////_    _____  State  _____   GameDir   _____
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
        //  _////////////////////////////////////////////////_    _____  State  _____   MidPaus   _____
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
        //  _////////////////////////////////////////////////_    _____  State  _____   MidPausPotion   _____
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

            SetCostumeNum ();

            if (Ag.mgIsKick) {
                //Debug.Log ("GoodBar :: stateArray "+ myCard.mGood);
                //myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Kick.Shirt.Texture, Ag.mySelf.arrUniform [0].Kick.Pants.Texture, Ag.mySelf.arrUniform [0].Kick.Socks.Texture, CostumeNum, out myCard.WAS.skill [0], out myCard.WAS.skill [1]);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar0";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar1_1";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);
            } else {
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar1").GetComponent<UISprite> ().spriteName = "skillbar_keeper0";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").GetComponent<UISprite> ().spriteName = "skillbar_keeper1_1";
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);
            }
            LegendSkillbar ();

            myCard.SetSkillPositions (myCard, false, false, false, Ag.mySelf, CostumeNum);

            SetSkillBarTextureSize ();
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   MidPausBiggerPotion   _____
        mStateArr.AddAMember ("MidPausBiggerPotion", 1f);
        mStateArr.AddEntryAction (() => {

            //mCharacterinfor.SetActive (false);
            mStartTime = Time.timeSinceLevelLoad;
            // Potion apply...
            GameObject mDirUPclone;
            if (mDidBuyPotion || (Ag.mgIsKick && (mEventPotion || mEventminusPotion)) || Ag.mBlueItemFlag) {

                StartCoroutine (ItemeffOn ("backeffect_blue"));
                if (mEventPotion || mEventminusPotion)
                    SoundManager.Instance.Play_Effect_Sound ("ApplyRedBull");

                DestoryGuideBar ();
                //DrawCreateSklLine (4, 125, 475);

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
                //Debug.Log ("GoodBar :: stateArray "+ myCard.mGood);
                dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);

                SetSkillBarTextureSize ();
                LegendSkillbar ();

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
        //  _////////////////////////////////////////////////_    _____  State  _____   GameSkl   _____
        mStateArr.AddAMember ("GameSkl", 2f);
        mStateArr.AddEntryAction (() => {

            //Debug.Log ("Good" + myCard.mGood + "Perfect" + myCard.mPerfect);
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
        //  _////////////////////////////////////////////////_    _____  State  _____   AftPaus   _____
        mStateArr.AddAMember ("AftPaus", 0.7f);
        mStateArr.AddEntryAction (() => {
            mRetryCount = 0;
            mskillflag = mStatusSillBar = true;
            //Panelka Exception.....
            if (Ag.mgIsKick && Ag.mgDirection == 5 && Ag.mgSkill > 1)
                Ag.mgSkill = 1;
            Ag.mgGamePackReceived = true;  //[2013:07-23:LJK]   
            mNetworkWaitAni ();
        });
        mStateArr.AddExitCondition (() => {
            return Ag.mgGamePackReceived;
        });
        mStateArr.AddTimeOutProcess (25.0f, () => {  
            Ag.LogNewLine (20);
            Ag.LogString ("Application.LoadLevel");
            mStateArr.SetStateWithNameOf ("HeartBeat"); // [2012:11:12:MOON] Heart Beat   //  mAwayMyself = true;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   NetWait   _____
        mStateArr.AddAMember ("NetWait", 1.5f);
        mStateArr.AddEntryAction (() => {
            Ag.LogString ("  NetWait (2 sec) :: Entry >>>    mRetryCount : " + mRetryCount);

            if (Ag.mSingleMode)
                Ag.mVirServer.Result ();
            else {
                Ag.NodeObj.HostGameTurn (TurnNum);
                //Debug.Log (Ag.NodeObj.EnemyUser.WAS.GameSessionKey + "SssessionKey" + Ag.NodeObj.MyUser.WAS.GameSessionKey + "MY SsessionKey");

                mRetryCount++;
            }
            if (mRetryCount > 12)
                Ag.NetExcpt.ConnectLossAct ();
            //SoundManager.Instance.audio.volume = 1f;
            SoundManager.Instance.Play_Effect_Sound ("whistle_1");
            //SoundManager.Instance.audio.volume = 1f;

        });
        //  _////////////////////////////////////////////////_    _____  State  _____   PackWait   _____
        mStateArr.AddAMember ("PackWait", 1f);
        mStateArr.AddEntryAction (() => {
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   AnimaPlay   _____
        mStateArr.AddAMember ("AnimaPlay", 0f);
        mStateArr.AddEntryAction (() => {
            EnemyCharacterEffect ();
            StateAnimaplaySkillAni ();

            dicGameSceneMenuList ["GoalNet_2"].SetActive (true);
            dicGameSceneMenuList ["Panel_item"].SetActive (false);
            //KickerDirbaroff();
            //mPanelItem.SetActive (false);
            CheckTurnNumber ();

            AmUniform.PlyerKind MyUnfi, EnUnif;

            if (Ag.mgIsKick) {
                MyUnfi = Ag.NodeObj.MyUser.arrUniform [0].Kick;
                EnUnif = Ag.NodeObj.EnemyUser.arrUniform [0].Keep;
            } else {
                MyUnfi = Ag.NodeObj.MyUser.arrUniform [0].Keep;
                EnUnif = Ag.NodeObj.EnemyUser.arrUniform [0].Kick;
            }

            Ag.NodeObj.GameScoreAddNewTurn (new int[] { 
                MyUnfi.Shirt.Texture, MyUnfi.Pants.Texture, MyUnfi.Socks.Texture, CostumeNum,
                EnUnif.Shirt.Texture, EnUnif.Pants.Texture, EnUnif.Socks.Texture, CostumeNumEnem
            });
//                Ag.NodeObj.MyUser.arrUniform[0].Kick.Shirt.Texture, Ag.NodeObj.MyUser.arrUniform [0].Kick.Pants.Texture, Ag.NodeObj.MyUser.arrUniform [0].Kick.Socks.Texture, CostumeNum,
//                Ag.NodeObj.EnemyUser.arrUniform[0].Kick.Shirt.Texture, Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.Texture , Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.Texture , CostumeNumEnem }); // Mine : Unif/Cstm, Enemy : Unif/Cstm

            Ag.LogNewLine (2);
            Ag.LogString ("  My Info :: Shirt / Pants / Socks ::  " + MyUnfi.Shirt.Texture + " / " + MyUnfi.Pants.Texture + " / " + MyUnfi.Socks.Texture + " / " + CostumeNum);
            Ag.LogString ("  En Info :: Shirt / Pants / Socks ::  " + EnUnif.Shirt.Texture + " / " + EnUnif.Pants.Texture + " / " + EnUnif.Socks.Texture + " / " + CostumeNumEnem);
            Ag.LogNewLine (2);

            dicGameSceneMenuList.SetActiveAll (false, new string [] { "MainBar", "MainSkillBar", "SGrade_MainSkillBar", 
                "Panel_keeperarrow_Main2", "Panel_keeperarrow_set", "Keeperinfo", "Kickerinfo"
            });

            KickerDirbaroff ();

            if (!Ag.mSingleMode) {
                //dicGameSceneMenuList["MyScore"].GetComponent<UILabel>().text = Ag.NodeObj.myGameLogic.CurScore.ToString();
                //dicGameSceneMenuList["EnemScore"].GetComponent<UILabel>().text = Ag.NodeObj.enGameLogic.CurScore.ToString();
                int enemD, enemS;
                Ag.NodeObj.GetEnemyDirectSkill (out enemD, out enemS);
                Ag.mgEnemDirec = (byte)enemD;
                Ag.mgEnemSkill = (byte)enemS;
                //Debug.Log ("arrIskick" + Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().isKick + "arrDidwin" + Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().didWin + "Iskick" + Ag.mgIsKick);

                Ag.LogString ("  >> Ag.NodeObj.myGameLogic.arrScore  ::  " + Ag.NodeObj.myGameLogic.arrScore.Count + " ea ");
                Ag.mgDidWin = Ag.NodeObj.myGameLogic.arrScore.GetLastMember ().didWin;
            }

            //mKeeperDirBar.SetActive (false);
            DestoryGuideBar ();
            DragPositionF (false);
            DragPositionLastSetDir (false);
            SetKickerDir (false);
            KickerScenePlay (true);
            SoundManager.Instance.Play_Effect_Sound ("01_Crowd_ready_loop");

            AnimaPlay ();
            Add_ScoutValue ();
            dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo_scouter_discript"].SetActive (false);
            dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_cash").gameObject.SetActive (true);
            StartCoroutine (AnimaStopCoru ());
            Ag.LogStartWithStr (10, " Ag.NodeObj.GameScoreAddNewTurn (new int[] {   ");

            //Ag.LogStartWithStr (10, " Ag.NodeObj.GameScoreAddNewTurn (new int[] {   ");
        
        });
        mStateArr.AddDuringAction (() => { 
            mKeeperPosi.x = mBippos.transform.position.x;
            mKeeperPosi.z = mBippos2.transform.position.z;
        });
        mStateArr.AddExitCondition (() => { 
            return mAnimationStopflag;
        });

        //  _////////////////////////////////////////////////_    _____  State  _____   Ceremony   _____
        mStateArr.AddAMember ("Ceremony", 2.4f, "Packet");
        mStateArr.AddEntryAction (() => {

            Ag.NodeObj.myGameLogic.GetTotalScore (Ag.mySelf.GetApplyIDofItem ("CeremonyDefault")); // 내 점수
            Ag.NodeObj.enGameLogic.GetTotalScore (Ag.NodeObj.EnemyUser.GetApplyIDofItem ("CeremonyDefault")); // 상대 점수

            dicGameSceneMenuList ["MyScore"].GetComponent<UILabel> ().text = ((int)Ag.NodeObj.myGameLogic.CurAccumTotal).ToString ();//((int)mMycurScore).ToString ();
            dicGameSceneMenuList ["EnemScore"].GetComponent<UILabel> ().text = ((int)Ag.NodeObj.enGameLogic.CurAccumTotal).ToString ();//((int)mEnemyCurScore).ToString ();

            //Ag.NetExcpt.GamingEnemyPoint = (int)Ag.NodeObj.enGameLogic.CurAccumTotal;

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
                //arrKickerDirBar[i].active = false;
                arrKickerDirBar [i].active = false;
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
            if (!Ag.mSingleMode) {
                if (Ag.NodeObj.GameFinish.HasValue) {

                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                }
            } else {
                if (Ag.mgDidGameFinish) {
                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                    Ag.mgDidGameFinish = false;
                    return;
                }
            }
            SoundManager.Instance.Play_Effect_Sound ("03_Crowd_goal");
            mPlayerKeeper.transform.position = new Vector3 (mKeeperPosi.x, 0, mKeeperPosi.z);
            CerAni ();   
        });
        mStateArr.AddExitAction (() => { 
            if (Ag.mSingleMode)
                Ag.SwitchStep ();
            else
                Ag.mgIsKick = !Ag.mgIsKick; //ljk 11 11;
            mStateArr.SetStateWithNameOf ("CountDn");
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   EndingCeremony   _____
        mStateArr.AddAMember ("EndingCeremony", 7f);
        mStateArr.AddEntryAction (() => { 

            if (Ag.mFriendMode != 1)
                Ag.mySelf.CoolTimeChooseOneMoreGameWin ();


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
        //  _////////////////////////////////////////////////_    _____  State  _____   ShowEndingResult   _____
        mStateArr.AddAMember ("ShowEndingResult", 0);
        mStateArr.AddEntryAction (() => {

            GameTotalScore ();

            if (Ag.ContGameNum >= 4) {
                FindMyChild (mResultPanel, "Panel_btn/btn_rematch", false);
                dicGameSceneMenuList ["btn_Label"].SetActive (true);
                mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9E%AC%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%ED%95%98%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
            }

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
                Ag.LogString ("GameScene :: StateArr :: myEnemyScore" + mEnemyCurScore);
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Win");
            } else {
                if (Ag.NodeObj.MyUser.WAS.League == "PRO_5")
                    finalEarnScore = 0;
                Ag.LogString ("GameScene :: StateArr :: myCurScore" + mMycurScore);
                GameReport (Ag.NodeObj.EnemyUser, Ag.NodeObj.MyUser, (int)mEnemyCurScore, (int)finalEarnScore);
                SoundManager.Instance.Play_Effect_Sound ("Short metal clip - Lose");
            }


            Ag.mySelf.DidWinOrLoseGame (Ag.mgDidWin);

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
            

        });
        mStateArr.AddExitCondition (() => {
            return false;
        });
        //  _////////////////////////////////////////////////_    _____  State  _____   GameFinish   _____
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
        if (!Ag.mSingleMode)
            AddAdditionalActions ();
        //  ////////////////////////////////////////////////     //[2012:11:12:MOON] Heart Beat
    }

    public void GameSceneCardUpdate ()
    {
        if (Ag.mySelf.ShouldUpdateCard ()) {
            WasCardUpdate aObj = new WasCardUpdate () { User = Ag.mySelf, arrSendCard = null
            };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    break;
                case -1:
                case 4:
                    return;
                }
            };
        }
    }

    void RematchWasGameStart ()
    {
        WasGameStart aObj = new WasGameStart () { User = Ag.mySelf, enemyID = Ag.NodeObj.EnemyUser.WAS.KkoID, friendGame = Ag.mFriendMode,
            contWinMyFlag = Ag.mgDidWin ? 1 : 2, //Ag.mySelf.ContWinCoolTimeRemainPercent () > 0) ? 1 : 2,
            contWinEnemFlag = Ag.mgDidWin ? 2 : 1, //contwinEnFlag,
            arrCardId = Ag.NodeObj.MyUser.GetMainCardIDs (), arrayEnemyId = Ag.NodeObj.EnemyUser.GetMainCardIDs ()
        };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                Ag.LogString (" result : Success ");
                break;
            }
        };
    }

    public void GameReport (AmUser WinnerUser, AmUser LoserUser, int WinPoint, int LoserPoint)
    {
        string winID, losID, myID = Ag.mGuest ? Ag.mySelf.DeviceID : Ag.mySelf.WAS.KkoID;
        myWeekScr = Ag.mySelf.myRank.WAS.weekScore;
        enWeekScr = Ag.NodeObj.MySocket.CurEnemy.rankObj.weekScore;

        if (Ag.SingleTry > 0) {
            Ag.NodeObj.MyUser.myRank.WAS.contWinNum = 0;
            Wincheck ();
            WasCardUniformCostume aaObj = new WasCardUniformCostume () { User = Ag.mySelf, code = 240 };
            aaObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Wincheck ();
                    Ag.SingleTry = 0;
                    return;
                }
            };
            return;
        }

        if (Ag.mgDidWin) {
            if (Ag.mFriendMode != 1)
                Ag.mySelf.CoolTimeChooseOneMoreGameWin ();
            winID = myID;
            losID = LoserUser.WAS.KkoID;
        } else {
            winID = WinnerUser.WAS.KkoID;
            losID = myID;
        }

        WasGameReport aObj = new WasGameReport () {
            User = Ag.mySelf, winnerID = winID, loserID = losID,
            winPo = (int)TotalWinerPoint, losPo = (int)TotalLoserPoint
        };


        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
            case 0:
                if (mNetworkError) {
                    WincheckNetworkError ();
                    mRscrcMan.FindChild (dicGameSceneMenuList ["popup"], "rematch_accept/btngrid/btn_rematch", false);
                } else {
                    Wincheck ();
                }
                myCard.WAS.ResetWidthAndSkill ();
                GameSceneCardUpdate ();
                aObj = null;
                Ag.LogString (" result : Success ");
                return;
            }
        };
    }

    void RemovePrevTurnObjects () // 현재 턴 앞에 까지 남기고 삭제
    {
        Ag.NodeObj.MySocket.arrGameSend.RemoveRange (TurnNum - 1, Ag.NodeObj.MySocket.arrGameSend.Count - TurnNum + 1);
        Ag.NodeObj.MySocket.arrGameRcvd.RemoveRange (TurnNum - 1, Ag.NodeObj.MySocket.arrGameRcvd.Count - TurnNum + 1);
    }

    void RemoveCurrentObject () // 현재 턴 객체 하나만 삭제
    {
        Ag.NodeObj.MySocket.arrGameSend.RemoveAt (TurnNum - 1);
        Ag.NodeObj.MySocket.arrGameRcvd.RemoveAt (TurnNum - 1);
    }

    void CheckTurnNumber ()
    {
        if (Ag.NodeObj.MySocket.arrGameSend.Count > TurnNum)
            Ag.NodeObj.MySocket.arrGameSend.RemoveRange (TurnNum, Ag.NodeObj.MySocket.arrGameSend.Count - TurnNum);
        if (Ag.NodeObj.MySocket.arrGameRcvd.Count > TurnNum)
            Ag.NodeObj.MySocket.arrGameRcvd.RemoveRange (TurnNum, Ag.NodeObj.MySocket.arrGameRcvd.Count - TurnNum);
    }

    int FunResultNum (List<bool> pbool)
    {
        int resultNum = 0;
        for (int i = 0; i < pbool.Count; i++) {
            if (pbool [i] == true)
                resultNum++;
        }
        return resultNum;
    }
}
