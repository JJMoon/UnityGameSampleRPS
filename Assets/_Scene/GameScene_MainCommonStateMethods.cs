//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    int PlayerNum, PlayerOrderNum, EnemyOrderNum, mFirstRewardGold;
    GameObject mPoly;
    string mPolygonName;
    AmCard myCard, EnemCard;
    bool mGameScoreeff;

    /// <summary>
    /// 연승 보상 아이템 수령하기
    /// </summary>
    void WasContWinRewardItemReceive ()
    {
        WasReceiveContWinItems aObj = new WasReceiveContWinItems () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            aObj = null;
        };
    }

    void GameFinish ()
    {
        Ag.mgDidWin = Ag.NodeObj.GameFinish.Value;
        dicGameSceneMenuList ["Panel_top"].SetActive (false);
        dicGameSceneMenuList ["Panel_item"].SetActive (false);
        dicGameSceneMenuList ["MainBar"].SetActive (false);
        dicGameSceneMenuList ["MainSkillBar"].SetActive (false);
        dicGameSceneMenuList ["SGrade_MainSkillBar"].SetActive (false);
        dicGameSceneMenuList ["Keeperinfo"].SetActive (false);
        dicGameSceneMenuList ["Kickerinfo"].SetActive (false);
        dicGameSceneMenuList ["Ui_cont"].SetActive (false);
    }

    void FirstGameWin ()
    {
        if (PreviewLabs.PlayerPrefs.GetBool ("FirstGameWin") == false && Ag.NodeObj.GameFinish.Value && Ag.mySelf.myRank.WAS.winNum == 0) {
            dicGameSceneMenuList ["popup"].SetActive (true);
            dicGameSceneMenuList ["division_notice1"].SetActive (true);
        }
    }

    void Notice1PopupOk ()
    {
        dicGameSceneMenuList ["division_notice1"].SetActive (false);
        dicGameSceneMenuList ["division_notice2"].SetActive (true);
    }

    void Notice1PopupClose ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["division_notice1"].SetActive (false);

    }

    void Notice2PopupOk ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["division_notice2"].SetActive (false);
        PreviewLabs.PlayerPrefs.SetBool ("FirstGameWin", true);
    }

    void Notice2PopupClose ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["division_notice2"].SetActive (false);
        PreviewLabs.PlayerPrefs.SetBool ("FirstGameWin", true);
    }

    void LegendSkillbar ()
    {
        if (myCard.WAS.grade == "S") {
            dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, 0);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), -260, -0.1f);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar2_1").transform.localScale = new Vector3 ((((float)(mSkill0) / 1000f) * 580), 24, 0);
            dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar3").transform.localScale = new Vector3 (((float)(mSkill1 / 1000f) * 580), 24, 0);
        }
    }

    void SetSkillValues ()
    {
        myCard.WAS.GetSkillFinalValue (Ag.mySelf.arrUniform [0].Kick.Shirt.Texture, Ag.mySelf.arrUniform [0].Kick.Pants.Texture, Ag.mySelf.arrUniform [0].Kick.Socks.Texture, CostumeNum, 
            out mSkill0, out mSkill1, Ag.mgDirection);
    }

    void SetSkillBarTextureSize ()
    {
        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localScale = new Vector3 (myCard.mGood / 1000f * 580, 24, 0);
        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localScale = new Vector3 (myCard.mPerfect / 1000f * 580, 24, 0);


        dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar2_1").transform.localScale = new Vector3 ((((float)(myCard.mGood) / 1000f) * 580), 24, 0);
        if (Ag.mgIsKick) dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("bar3").transform.localScale = new Vector3 (((float)(myCard.mPerfect / 1000f) * 580), 24, 0);
    }

    /// <summary>
    /// 연승보상 출력
    /// </summary>
    string Reward1, Reward2;

    void CloseContwinNumReward ()
    {
        //dicGameSceneMenuList ["victory_gift"].SetActive(false);
        ContiWinRewardReceivePack ();
        dicGameSceneMenuList ["Main_victory_gift"].SetActive (false);
    }

    void ContWinNumReward ()
    {
        RewardItemInit ();
        //Debug.Log ("Ag.NodeObj.MyUser.myRank.WAS.contWinNum" + Ag.mySelf.myRank.WAS.contWinNum);

        dicGameSceneMenuList ["victory_info"].transform.FindChild ("Label_victorynum").gameObject.GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.contWinNum + WWW.UnEscapeURL ("%EC%97%B0%EC%8A%B9%EC%A4%91");
        //dicGameSceneMenuList ["victory_gift"].transform.FindChild ("Label_victorynum").gameObject.GetComponent<UILabel> ();


        if (Ag.mySelf.myRank.WAS.contWinNum > 0 && Ag.mySelf.myRank.WAS.contWinNum < 9) {
            dicGameSceneMenuList ["Main_victory_gift"].SetActive (true);
            //dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check1/check" + Ag.mySelf.myRank.WAS.contWinNum).gameObject.SetActive (true);
            for (int i = 1; i <= Ag.mySelf.myRank.WAS.contWinNum; i++ ) {
                dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check1/check" + i).gameObject.SetActive (true);
            }

            //Debug.Log ("Ag.NodeObj.MyUser.myRank.WAS.contWinNum" + Ag.mySelf.myRank.WAS.contWinNum);
        }
        if (Ag.mySelf.myRank.WAS.contWinNum > 8 && Ag.mySelf.myRank.WAS.contWinNum < 17) {
            dicGameSceneMenuList ["Main_victory_gift"].SetActive (true);

            for (int i = 1; i < 9; i++ ) {
                dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check1/check" + i).gameObject.SetActive (true);
            }
            for (int j = 1; j < Ag.mySelf.myRank.WAS.contWinNum - 7; j++ ) {
                dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check2/check" + j).gameObject.SetActive (true);
            }


            //Debug.Log ("Ag.NodeObj.MyUser.myRank.WAS.contWinNum" + Ag.mySelf.myRank.WAS.contWinNum);
        } 
        /*
        if (Ag.NodeObj.MyUser.myRank.WAS.contWinNum < 2) {
            dicGameSceneMenuList ["ContwinNumReward_btn_receive"].gameObject.SetActive (false);
            dicGameSceneMenuList ["ContwinNumReward_Label_donotreceive"].gameObject.SetActive (true);

            if (Ag.NodeObj.MyUser.myRank.WAS.contWinNum < 1) {
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.contWinNum.ToString () + WWW.UnEscapeURL ("%EC%8A%B9%EB%B3%B4%EC%83%81");
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = (Ag.mySelf.myRank.WAS.contWinNum + 1).ToString () + WWW.UnEscapeURL ("%EC%8A%B9%EB%B3%B4%EC%83%81");
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%B3%B4%EC%83%81%EC%97%86%EC%9D%8C");
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%B3%B4%EC%83%81%EC%97%86%EC%9D%8C");

            } 
            if (Ag.NodeObj.MyUser.myRank.WAS.contWinNum == 1) {
                ShowRewardItem (QuestEvent ("quest").arrReward [Ag.mySelf.myRank.WAS.contWinNum + 1].value, false, out Reward1, out Reward2);
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.contWinNum.ToString () + WWW.UnEscapeURL ("%EC%8A%B9%EB%B3%B4%EC%83%81");
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = (Ag.mySelf.myRank.WAS.contWinNum + 1).ToString () + WWW.UnEscapeURL ("%EC%97%B0%EC%8A%B9%EB%B3%B4%EC%83%81");
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EB%B3%B4%EC%83%81%EC%97%86%EC%9D%8C");//QuestEvent("quest").arrReward[Ag.mySelf.myRank.WAS.contWinNum].value;
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = Reward2;
                
            }
        } else {
            if (Ag.NodeObj.MyUser.myRank.WAS.contWinNum == 10) {
                Btn_Fun_ContWinRewardReceive ();
                ShowRewardItem (QuestEvent ("quest").arrReward [Ag.mySelf.myRank.WAS.contWinNum].value, true, out Reward1, out Reward2);
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.contWinNum.ToString () + WWW.UnEscapeURL ("%EC%8A%B9%EB%B3%B4%EC%83%81");
                dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label2").gameObject.SetActive (false);
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Reward1;
                dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label2").gameObject.SetActive (false);
                
                //when ContiWinNum is reach 10 wins to Make ContWinMenu
                return;
            }
            ShowRewardItem (QuestEvent ("quest").arrReward [Ag.mySelf.myRank.WAS.contWinNum].value, true, out Reward1, out Reward2);
            ShowRewardItem (QuestEvent ("quest").arrReward [Ag.mySelf.myRank.WAS.contWinNum+1].value, false, out Reward1, out Reward2);
            dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Ag.mySelf.myRank.WAS.contWinNum.ToString () + WWW.UnEscapeURL ("%EC%97%B0%EC%8A%B9");
            dicGameSceneMenuList ["ContwinNumReward_grid_vic"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = (Ag.mySelf.myRank.WAS.contWinNum + 1).ToString () + WWW.UnEscapeURL ("%EC%97%B0%EC%8A%B9");
            dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label1").GetComponent<UILabel> ().text = Reward1;
            dicGameSceneMenuList ["ContwinNumReward_grid_victory"].gameObject.transform.FindChild ("Label2").GetComponent<UILabel> ().text = Reward2;
            
        }
        */
    }

    /// <summary>
    /// Show RewardItemInit 
    /// </summary>

    void RewardItemInit ()
    {
        for (int i = 1; i < 9; i++) {
            dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check1/check" + i).gameObject.SetActive (false);
            dicGameSceneMenuList ["victory_gift"].transform.FindChild ("grid_check2/check" + i).gameObject.SetActive (false);
        }


        /*
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_red"].transform.FindChild ("red1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_red"].transform.FindChild ("red2").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_green"].transform.FindChild ("green1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_green"].transform.FindChild ("green2").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_blue"].transform.FindChild ("blue1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_blue"].transform.FindChild ("blue2").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold2").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_card"].transform.FindChild ("card1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_card"].transform.FindChild ("card2").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_cash"].transform.FindChild ("cash1").gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_cash"].transform.FindChild ("cash2").gameObject.SetActive (false);
        */
    }

    /// <summary>
    /// Shows the reward item.
    /// </summary>

    void ShowRewardItem (string Code, bool PosLeft, out string Reward1, out string Reward2)
    {
        Reward1 = Reward2 = "";
        Debug.Log ("Code :: " + Code + " PosLeft ::" + PosLeft);
        //RewardItemInit ();
        switch (Code) {
        case "RedDrink1":
            if (PosLeft) {
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_red"].transform.FindChild ("red1").gameObject.SetActive (true);
                Reward1 = "X1";

            } else {
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_red"].transform.FindChild ("red2").gameObject.SetActive (true);
                Reward2 = "X1";
            }
            break;
        case "GreenDrink3":
            if (PosLeft) {
                Reward1 = "X3";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_green"].transform.FindChild ("green1").gameObject.SetActive (true);
            } else {
                Reward2 = "X3";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_green"].transform.FindChild ("green2").gameObject.SetActive (true);
            }
            break;
        case "BlueDrink5":
            if (PosLeft) {
                Reward1 = "X5";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_blue"].transform.FindChild ("blue1").gameObject.SetActive (true);
            } else {
                Reward2 = "X5";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_blue"].transform.FindChild ("blue2").gameObject.SetActive (true);
            }
            break;
        case "Gold300":
            if (PosLeft) {
                Reward1 = "X300";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold1").gameObject.SetActive (true);
            } else {
                Reward2 = "X300";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold2").gameObject.SetActive (true);
            }
            break;
        case "Gold500":
            if (PosLeft) {
                Reward1 = "X500";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold1").gameObject.SetActive (true);
            } else {
                Reward2 = "X500";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold2").gameObject.SetActive (true);
            }
            break;
        case "TicketNormal":
            if (PosLeft) {
                Reward1 = "X1";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_card"].transform.FindChild ("card1").gameObject.SetActive (true);
            } else {
                Reward2 = "X1";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_card"].transform.FindChild ("card2").gameObject.SetActive (true);
            }
            break;
        case "Gold1000":
            if (PosLeft) {
                Reward1 = "X1000";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold1").gameObject.SetActive (true);
            } else {
                Reward2 = "X1000";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold2").gameObject.SetActive (true);
            }
            break;
        case "Gold2000":
            if (PosLeft) {
                Reward1 = "X2000";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold1").gameObject.SetActive (true);
            } else {
                Reward2 = "X2000";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_gold"].transform.FindChild ("gold2").gameObject.SetActive (true);
            }
            break;
        case "EventCash2":
            if (PosLeft) {
                Reward1 = "X 2cash";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_cash"].transform.FindChild ("cash1").gameObject.SetActive (true);
            } else {
                Reward2 = "X 2cash";
                dicGameSceneMenuList ["ContwinNumReward_grid_victoryicon_cash"].transform.FindChild ("cash2").gameObject.SetActive (true);
            }
            break;
        }
        
    }

    void Add_ScoutValue ()
    {
        if (Ag.mgDidWin)
            myCard.AddScouterValue (Ag.mgDirection, Ag.mgDidWin);
        else
            myCard.AddScouterValue (Ag.mgDirection, Ag.mgDidWin); 
    }

    /// <summary>
    /// 스카우터 정보창 보여주기
    /// </summary>
    void ScouterUI ()
    {
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].SetActive (true);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Label_blue").gameObject.GetComponent<UILabel> ().text = EnemCard.ScouterGameNum (1).ToString ();
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Label_red").gameObject.GetComponent<UILabel> ().text = EnemCard.ScouterGameNum (2).ToString ();
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Label_marine").gameObject.GetComponent<UILabel> ().text = EnemCard.ScouterGameNum (3).ToString ();
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Label_yellow").gameObject.GetComponent<UILabel> ().text = EnemCard.ScouterGameNum (4).ToString ();
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Label_center").gameObject.GetComponent<UILabel> ().text = EnemCard.ScouterGameNum (5).ToString ();
        dicGameSceneMenuList ["Kickerinfo_progress_scouter"].transform.FindChild ("Label_cash").gameObject.SetActive (false);
        Scouter_FxInit ();
    }

    void Scouter_FxInit ()
    {
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx/Fx_Sprite_blue").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx/Fx_Sprite_center").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx/Fx_Sprite_marine").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx/Fx_Sprite_red").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_bundle"].transform.FindChild ("Scout_Fx/Fx_Sprite_yellow").gameObject.SetActive (false);
    }

    /// <summary>
    /// 스카우터 작동
    /// </summary>
    void Btn_ScouterOn ()
    {
        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].SetActive (true);
        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].transform.FindChild ("Label_nocash").gameObject.SetActive (false);
        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].transform.FindChild ("Label_cash").gameObject.SetActive (false);
        if (Ag.mySelf.CanIScoutNow) {
            Ag.mySelf.CoolTimeScoutUse ();
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
            FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false);
            ScouterUI ();

        } else {
            if ((Ag.mySelf.mCash1 + Ag.mySelf.mCash2) > 1) {
                Ag.mySelf.CoolTimeScoutUse ();
                WasScouter aObj = new WasScouter () { User = Ag.mySelf };
                aObj.messageAction = (int pInt) => {
                    switch (pInt) {
                    case 0:
                        ScouterUI ();
                        FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter", false);
                        FindMyChild (dicGameSceneMenuList ["Panel_item"], "progress_scouter", false);
                        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].transform.FindChild ("Label_cash").gameObject.SetActive (true);
                        break;
                    case -1:
                        dicGameSceneMenuList ["Kickerinfo_scouter_discript"].transform.FindChild ("Label_nocash").gameObject.SetActive (true);

                        break;

                    }

                    aObj = null;
                };
                return;
            } else {

            }
        }
    }

    /// <summary>
    /// 보상 수령하기
    /// </summary>
    void Btn_Fun_ContWinRewardReceive ()
    {
        dicGameSceneMenuList ["ContwinNumReward_btn_receive"].gameObject.SetActive (false);
        dicGameSceneMenuList ["ContwinNumReward_Label_donotreceive"].gameObject.SetActive (true);
        ContiWinRewardReceivePack ();
    }

    /// <summary>
    /// 보상 수령하기 팩
    /// </summary>
    void ContiWinRewardReceivePack ()
    {
        WasReceiveContWinItems aObj = new WasReceiveContWinItems () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            aObj = null;
        };
    }

    WasEvent mWasevent;

    WasEvent QuestEvent (string EventName)
    {
        for (int i = 0; i < Ag.mySelf.arrEvent.Count; i++) {
            if (Ag.mySelf.arrEvent [i].eventType == EventName) {
                mWasevent = Ag.mySelf.arrEvent [i];
            }
        }
        return mWasevent;
    }

    void WincheckNetworkError ()
    {
        FindMyChild (mResultPanel, "Panel_btn/btn_rematch", false);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);
        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9E%AC%EA%B2%BD%EA%B8%B0%EB%A5%BC%20%ED%95%98%EC%8B%A4%EC%88%98%20%EC%97%86%EC%8A%B5%EB%8B%88%EB%8B%A4.");
        //재경기를 하실수 없습니다
        Wincheck ();
        // Debug.Log ("mDidwin" + Ag.mgDidWin);
    }

    public void SetPolyGon (AmCard pPolyGonNum)
    {
        //// Debug.Log (" info " + pPolyGonNum.WAS.info + " iskicker " + pPolyGonNum.WAS.isKicker);
        if (pPolyGonNum.WAS.isKicker) {
            mPoly = (GameObject)Resources.Load ("Kicker/Prefab/" + pPolyGonNum.WAS.look);
        } else {
            mPoly = (GameObject)Resources.Load ("Keeper/Prefab/" + pPolyGonNum.WAS.look);
        }
    }

    void KeeperSelectBar (int Pnum)
    {
        for (int i = 0; i < 4; i++) {
            mKeeperUIBar [i].SetActive (true);
            if (Pnum - 1 == i) {
                // Debug.Log ("KeeperSelectBar ChoiceNum");
                if ((int)(Time.time * 5) % 2 == 0) {
                    mKeeperUIBar [i].transform.renderer.material.SetColor ("_TintColor", new Color (0.5f, 0.5f, 0.5f, 0.2f));
                } else {
                    mKeeperUIBar [i].transform.renderer.material.SetColor ("_TintColor", new Color (0.5f, 0.5f, 0.5f, 0.5f));
                }
                // continue;
            } else {
                mKeeperUIBar [i].transform.renderer.material.SetColor ("_TintColor", new Color (0.5f, 0.5f, 0.5f, 0.2f));
            }
        }
    }

    void KeeperSelectedBar (int Pnum)
    {
        for (int i = 0; i < 4; i++) {
            
            if (Pnum - 1 == i) {
                mKeeperUIBar [i].SetActive (true);
                continue;
            }
            mKeeperUIBar [i].SetActive (false);
        }
    }

    //Load Game Friends
    private void onLoadGameFriendsComplete() {
        Debug.Log("onLoadGameFriendsComplete");
     
    }
    private void onLoadGameFriendsError(string status, string message) {
        Debug.Log("onLoadGameFriendsError");
     
    }

    void CommonStateMethods ()
    {
        mStateArr.GetState ("Begin").mEntryAction += () => {

            KakaoNativeExtension.Instance.loadGameFriends (onLoadGameFriendsComplete, onLoadGameFriendsError);
            arrMyScore = new List<bool> ();
            arrEnScore = new List<bool> ();
            mAllPoint = Ag.mgPrevScore = Ag.mySelf.myRank.mScore;
            SoundManager.Instance.Play_Effect_Sound ("05_Crowd_etc");
            CerCam.enabled = false;
            mIntroCam.enabled = true;
            Ag.NodeObj.ReMatchSent.InitSet (false);
            Ag.mRound = 0;

            if (AgStt.mgGameTutorial) {
                //ljk 11 11
                TutorialBegin ();
                TutorialSetTextureCharacter ();
            } else {
                if (Ag.mgIsKick) {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/Pre-matchIntro(PlayerFirst)01");
                    myCard = Ag.NodeObj.MyUser.GetCardOrderOf (1);
                    SetPolyGon (myCard);
                    mPlayerKicker = (GameObject)Instantiate (mPoly);
                    EnemCard = Ag.NodeObj.EnemyUser.GetCardOrderOf (0);
                    //                Debug.Log(" EnemCard.WAS.ID" +EnemCard.WAS.ID);
                    SetPolyGon (EnemCard);
                    mPlayerKeeper = (GameObject)Instantiate (mPoly);
                } else {
                    VoiceSoundManager.Instance.Play_Effect_Sound ("voice/Pre-matchIntro(OpponentFirst)1");
                    myCard = Ag.NodeObj.MyUser.GetCardOrderOf (0);
                    SetPolyGon (myCard);
                    mPlayerKeeper = (GameObject)Instantiate (mPoly);
                    EnemCard = Ag.NodeObj.EnemyUser.GetCardOrderOf (1);
                    SetPolyGon (EnemCard);
                    mPlayerKicker = (GameObject)Instantiate (mPoly);
                    KeeperRemoveTrailer ();
                }
                SetTextureCharacter ();
            }
            IntroAni ();

        };
        mStateArr.GetState ("CountDn").mEntryAction += () => {
            Ag.mgDirection = Ag.mgSkill = 0;
            Ag.NodeObj.Direction = Ag.NodeObj.Skill = 0;
        };

        mStateArr.GetState ("PreGame").mEntryAction += () => {
            //Ag.mySelf.ApplyDeckItemBeforeGame ();
            PreGameVoicePlay ();//voice Play

            CeremonyResultLabelEffInit ();
            if (Ag.GameStt.SomeoneOutPopupEnemyLeft) {
                dicGameSceneMenuList ["popup"].SetActive (true);
                dicGameSceneMenuList ["alert_someoneout"].SetActive (true);
                Ag.mgDidWin = true;
                Ag.NodeObj.GameFinish = true; // I won .. 
                if (mStateArr.GetCurStateName () != "ShowEndingResult") {
                    mStateArr.SetStateWithNameOf ("EndingCeremony");
                }
            }

            mFirstRewardGold = Ag.mySelf.mGold;
            Ag.mySelf.mRewardGold = 0;
            ItemEffInit (); //ItemEffInit
            dicGameSceneMenuList ["Panel_top"].SetActive (true);
            mIsKeeperSkl = 5;
            mDirLightFlag = false;
            mskillflag = true;
            mEffballflag = true;
            //mItemSlot.SetActive (true);

            mEventDirspeed = 0.8f;
            mEventSkillSpeed = 0.8f;

            SoundManager.Instance.Play_Effect_Sound ("02_Crowd_Set");

            mStage.InitCursorMove (0.8f, 800f);

            mCerCamAxis.SetActive (false);
            mDidDragStarted = false;
            mKeeperSetDir = false;
            mIntroCam.enabled = false;
            mStage.mIsTouched = true;
            CameraSet ();
            DestroyObject (mKickBall);
            mKickBall = (GameObject)Instantiate (Resources.Load ("prefab_Polygon/Ball/Ball"));

            if (AgStt.mgGameTutorial) {
                TutoriPreGame ();
                // 여 기 에  myCard = 이 들 어 가 야 함... 
                //mCurPlayer = Ag.mySelf.mCurPlayer; //GetKicker( 1 );
                //mCurEnem = Ag.myEnem.mCurPlayer; //.GetGoulKeeper();
            } else {
                if (Ag.mgIsKick) { // ljk 11 11
                    myCard = Ag.NodeObj.MyUser.GetCardOrderOf ((PlayerOrderNum % 5) + 1);

                    myCard.SetDirectionPosition ();
                    myCard.SetDirectionArea ();
                    //Ag.LogString (" myCard count " + myCard.arrArea.Count);
                    PlayerOrderNum++;
                } else {
                    EnemCard = Ag.NodeObj.EnemyUser.GetCardOrderOf ((EnemyOrderNum % 5) + 1);
                    for (int i = 0; i < EnemCard.WAS.direction.Length; i++) {
                        //Ag.LogString (" EnemCard.WAS.direction ::     i " + i + "      >>>>  " + EnemCard.WAS.direction [i].ToString ());
                    }
                    EnemCard.SetDirectionPosition ();
                    EnemCard.SetDirectionArea ();
                    EnemyOrderNum++;
                }
            }
            
            if (AgStt.mgGameTutorial) {
                TutorialSetTextureCharacter ();
            } else {
                if (Ag.mgIsKick) {

                    KickerScenePlay (false);
                    SetKickerDir (true);
                    StartCoroutine (ShowKickerFX2 (1.8f));
                    DestroyObject (mPlayerKicker);
                    DestroyObject (mPlayerKeeper);
                    SetPolyGon (myCard);
                    mPlayerKicker = (GameObject)Instantiate (mPoly);
                    EnemCard = Ag.NodeObj.EnemyUser.GetCardOrderOf (0);
                    SetPolyGon (EnemCard);
                    //Debug.Log ("EnemCard :: "+ EnemCard.WAS.look);
                    mPlayerKeeper = (GameObject)Instantiate (mPoly);
                    if (myCard.arrCostumeInCard.Count > 0) {
                        for (int i = 0; i < myCard.arrCostumeInCard.Count; i++) {
                            //// Debug.Log ("CostumeNAme" + myCard.arrCostumeInCard [i].WAS.itemTypeId);
                            PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, (myCard.arrCostumeInCard [i].WAS.itemTypeId));
                        }
                    } else {
                        PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, "KickerShoes01");
                    }

                    if (EnemCard.arrCostumeInCard.Count > 0) {
                        for (int i = 0; i < EnemCard.arrCostumeInCard.Count; i++) {
                            // Debug.Log ("CostumeNAme" + EnemCard.arrCostumeInCard [i].WAS.itemTypeId);
                            PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, (EnemCard.arrCostumeInCard [i].WAS.itemTypeId));
                        }
                    } else {
                        PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, "KeeperGloves01");
                    }
                    if (!AgStt.mgGameTutorial) {
                        dicGameSceneMenuList ["Keeperinfo"].SetActive (true);

                        EnemyKeeperInfo ();
                    }
                    mBall = mPlayerKicker.transform.FindChild ("deleveryBall").gameObject; //LJk 2013 07 25
                    mBall.transform.renderer.enabled = false;
                    PreAni ();
                    DrawGuideLineNew ();
                    StartCoroutine (CaptureImage ());
                } else {

                    dicGuideObjectPos = new Dictionary<int, float> ();
                    dicGuideObjectWidth = new Dictionary<int, float> ();
                    DrawGuideLineNew ();

                    if (!AgStt.mgGameTutorial) { 
                        StartCoroutine (SmallinfoFlag (1f));
                    } 
                    if (AgStt.mgGameTutorial)
                        StartCoroutine (TutorinfoFlag (1f));
                    //Debug.Log ("backGreenItemPosition Pregame   " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
                    //mPlayerInfoDirflag = true;
                    mIsKeeperSkl = 0;
                    mPlayerInfoX = 0.224f;
                    mPlayerInfoY = 0.7f;
                    mPlayerInfowid = 0f;
                    MplayerInfoHeight = 0.2078125f;
                    //KickinfoPlag = false;
                    StartCoroutine (KeeperinfoAni (2.5f));
                    if (!AgStt.mgGameTutorial)
                        StartCoroutine (PlayerinfobarFlag (0.5f));
                    DragNum = 0;
                    KickerScenePlay (false);
                    //Ag.LogString ("Number    >>> " + Ag.mySelf.arrAllPlayer.Count);
                    //GuideKeeperViewAni ();
                    DestroyObject (mPlayerKicker);
                    DestroyObject (mPlayerKeeper);
                    SetPolyGon (EnemCard);
                    mPlayerKicker = (GameObject)Instantiate (mPoly);
                    myCard = Ag.NodeObj.MyUser.GetCardOrderOf (0);
                    SetPolyGon (myCard);
                    mPlayerKeeper = (GameObject)Instantiate (mPoly);
                    //Debug.Log ("backGreenItemPosition Pregame   " + dicGameSceneMenuList["Anim_back_green"].gameObject.transform.localPosition.y);
                    if (EnemCard.arrCostumeInCard.Count > 0) {
                        for (int i = 0; i < EnemCard.arrCostumeInCard.Count; i++) {
                            // Debug.Log ("CostumeNAme" + EnemCard.arrCostumeInCard [i].WAS.itemTypeId);
                            PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, (EnemCard.arrCostumeInCard [i].WAS.itemTypeId));
                        }
                    } else {
                        PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, "KickerShoes01");
                    }

                    if (myCard.arrCostumeInCard.Count > 0) {
                        for (int i = 0; i < myCard.arrCostumeInCard.Count; i++) {
                            // Debug.Log ("CostumeNAme" + myCard.arrCostumeInCard [i].WAS.itemTypeId);
                            PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, (myCard.arrCostumeInCard [i].WAS.itemTypeId));
                        }
                    } else {
                        PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, "KeeperGloves01");
                    }

                    if (!AgStt.mgGameTutorial) {
                        dicGameSceneMenuList ["Kickerinfo"].SetActive (true);
                    }
                    EnemyKickerinfo ();
                    mBall = mPlayerKicker.transform.FindChild ("deleveryBall").gameObject.gameObject; //LJK 07:25
                    mBall.transform.renderer.enabled = false;
                    dicGameSceneMenuList ["Panel_keeperarrow_Main"].SetActive (true);
                    DragPositionF (true);
                    PreAni ();
                    StartCoroutine (CaptureImage ());
                }
                SetTextureCharacter ();
            }
            KeeperRemoveTrailer ();
        };
        mStateArr.GetState ("Ceremony").mEntryAction += () => {
            ShowWinORLose_Label_Eff ();

        };
    }

    public void CeremonyResultLabelEffInit ()
    {
        dicGameSceneMenuList ["Eff_Fail_Save"].SetActive (false);
        dicGameSceneMenuList ["Eff_Fail_Attack"].SetActive (false);
        dicGameSceneMenuList ["Eff_LoseEff"].SetActive (false);
        dicGameSceneMenuList ["Eff_WinEff"].SetActive (false);
        dicGameSceneMenuList ["Eff_Success_Attack"].SetActive (false);
        dicGameSceneMenuList ["Eff_Success_Save"].SetActive (false);

        dicGameSceneMenuList ["Eff_Fail"].SetActive (false);
        dicGameSceneMenuList ["Eff_Gameresult"].SetActive (false);
        dicGameSceneMenuList ["Eff_Success"].SetActive (false);
    }

    void GameResultLabelEff ()
    {
        CeremonyResultLabelEffInit ();
        dicGameSceneMenuList ["Eff_Gameresult"].SetActive (true);
        if (Ag.NodeObj.GameFinish.HasValue) {
            if (Ag.NodeObj.GameFinish.Value)
                dicGameSceneMenuList ["Eff_WinEff"].SetActive (true);
            else
                dicGameSceneMenuList ["Eff_LoseEff"].SetActive (true);
        }
    }

    void ShowWinORLose_Label_Eff ()
    {

        if (Ag.mgIsKick) {
            if (Ag.mgDidWin) {
                dicGameSceneMenuList ["Eff_Success"].SetActive (true);
                dicGameSceneMenuList ["Eff_Success_Attack"].SetActive (true);
            } else {
                dicGameSceneMenuList ["Eff_Fail_Attack"].SetActive (true);
                dicGameSceneMenuList ["Eff_Fail"].SetActive (true);
            }
        } else {
            if (Ag.mgDidWin) {
                dicGameSceneMenuList ["Eff_Success"].SetActive (true);
                dicGameSceneMenuList ["Eff_Success_Save"].SetActive (true);
            } else {
                dicGameSceneMenuList ["Eff_Fail_Save"].SetActive (true);
                dicGameSceneMenuList ["Eff_Fail"].SetActive (true);
            }
        }
    }

    void KeeperRemoveTrailer ()
    {
        mBippos = mPlayerKeeper.transform.FindChild ("Bip001").gameObject.gameObject;
        mBippos2 = mPlayerKeeper.transform.FindChild ("Bip001").gameObject.gameObject;
        mKpTrailL = mPlayerKeeper.transform.FindChild (mFoldNameL).gameObject.gameObject;
        mKpTrailL.GetComponent<TrailRenderer> ().time = 0.68f;
        mKpTrailL.GetComponent<TrailRenderer> ().startWidth = 0.58f;
        mKpTrailL.GetComponent<TrailRenderer> ().endWidth = 0.03f;
        mKpTrailL.AddComponent<GameScene_TrigerAction> ();
        mKpTrailL.GetComponent<Collider> ().isTrigger = true;
        mKpTrailR = mPlayerKeeper.transform.FindChild (mFoldNameR).gameObject.gameObject;
        mKpTrailR.GetComponent<TrailRenderer> ().time = 0.68f;
        mKpTrailR.GetComponent<TrailRenderer> ().startWidth = 0.58f;
        mKpTrailR.GetComponent<TrailRenderer> ().endWidth = 0.03f;

        mKpTrailR.AddComponent<GameScene_TrigerAction> ();
        mKpTrailR.GetComponent<Collider> ().isTrigger = true;
        mKpTrailR.GetComponent<TrailRenderer> ().enabled = false;
        mKpTrailL.GetComponent<TrailRenderer> ().enabled = false;

    }
}