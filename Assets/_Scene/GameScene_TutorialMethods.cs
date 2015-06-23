//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    Touch mTouch;
    int mTutorCapN = 0, mKickerCapNum, mKeeperCapNum;
    bool mKpSklFlag = false, mKeeperYellowflag = false, mKeeperRedflag = false, mCerFlag, mCursorFlag = false, mTitleFlag = false, mTouchFlag, mSkilleffflag;
    string msklname, mdirName;
    Action mRestEvent;

    void KickerKeeperSklbarSelect ()
    {

        Vector3 bar2_1, bar3;
        bar2_1 = dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition;
        bar3 = dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition;

        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localPosition = new Vector3 (((0.3f * 580) - 290), bar2_1.y, bar2_1.z);
        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localPosition = new Vector3 (((0.3f * 580) - 290), bar3.y, bar3.z);
        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar2_1").transform.localScale = new Vector3 ((((float)(myCard.mGood) / 1000f) * 580), 24, 0);
        dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("bar3").transform.localScale = new Vector3 (((float)(myCard.mPerfect / 1000f) * 580), 24, 0);
    }

    void KickerDefCloth ()
    {
        mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].shader = Shader.Find ("Bumped Diffuse");
        mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].shader = Shader.Find ("Bumped Diffuse");
        mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].shader = Shader.Find ("Bumped Diffuse");
        mPlayerKicker.transform.FindChild ("face").renderer.material.shader = Shader.Find ("Bumped Diffuse");
    }

    string KickertoString (int pNum)
    {
        GameSklInit ();
        switch (pNum) {
        case 1:
        case 2:
            dicGameSceneMenuList ["eff0_fire"].SetActive (true);
            break;
        case 3:
            dicGameSceneMenuList ["eff1_volcano"].SetActive (true);
            break;
        }
        return msklname;

    }

    string KeeperSkilltoString (int pNum)
    {

        switch (pNum) {

        case 1:
            dicGameSceneMenuList ["eff0_good"].SetActive (true);
            break;
        case 2:
            dicGameSceneMenuList ["eff1_great"].SetActive (false);
            break;
        }

        return msklname;

    }

    void GameSklInit ()
    {
        dicGameSceneMenuList ["eff0_good"].SetActive (false);
        dicGameSceneMenuList ["eff1_great"].SetActive (false);
        //dicGameSceneMenuList ["eff2_volcano"].SetActive (false);
        //dicGameSceneMenuList ["eff3_flash"].SetActive (false);
        //dicGameSceneMenuList ["eff4_lightning"].SetActive (false);
        dicGameSceneMenuList ["eff5_miss"].SetActive (false);
        dicGameSceneMenuList ["eff2_perfect"].SetActive (false);
        dicGameSceneMenuList ["great"].SetActive (false);
        dicGameSceneMenuList ["perfect"].SetActive (false);
    }

    void DirectionMissEff ()
    {
        GameSklInit ();
        mSklName.SetActive (true);
        dicGameSceneMenuList ["eff5_miss"].SetActive (true);
    }

    void GameSklInit2 ()
    {
        dicGameSceneMenuList ["eff0_fire"].SetActive (false);
        dicGameSceneMenuList ["eff1_blaze"].SetActive (false);
        dicGameSceneMenuList ["eff2_volcano"].SetActive (false);
        dicGameSceneMenuList ["eff3_flash"].SetActive (false);
        dicGameSceneMenuList ["eff4_lightning"].SetActive (false);
    }

    void StateAnimaplaySkillAni ()
    {
        GameSklInit2 ();

        if (Ag.mgIsKick) {
            if (Ag.NodeObj.Skill == 1)
                dicGameSceneMenuList ["eff0_fire"].SetActive (true);
            if (Ag.NodeObj.Skill == 2)
                dicGameSceneMenuList ["eff1_blaze"].SetActive (true);
            if (Ag.NodeObj.Skill == 3)
                dicGameSceneMenuList ["eff2_volcano"].SetActive (true);
        } else {
            if (Ag.NodeObj.Skill == 1)
                dicGameSceneMenuList ["eff3_flash"].SetActive (true);
            if (Ag.NodeObj.Skill == 2)
                dicGameSceneMenuList ["eff4_lightning"].SetActive (true);
        }

    }

    void GameSkillstring (int pNum)
    {

        GameSklInit ();

        switch (pNum) {
        case 0:
            dicGameSceneMenuList ["eff5_miss"].SetActive (true);
            break;
        case 1:
            if (Ag.mgIsKick)
                dicGameSceneMenuList ["eff0_good"].SetActive (true);
            else
                dicGameSceneMenuList ["eff0_good"].SetActive (true);
            break;
        case 2:
            if (Ag.mgIsKick)
                dicGameSceneMenuList ["eff1_great"].SetActive (true);
            else
                dicGameSceneMenuList ["eff1_great"].SetActive (true);

            if (myCard.WAS.grade != "S")
                dicGameSceneMenuList ["great"].SetActive (true);

            break;
        case 3:
            if (Ag.mgIsKick)
                dicGameSceneMenuList ["eff2_perfect"].SetActive (true);
            else
                dicGameSceneMenuList ["eff2_perfect"].SetActive (true);

            dicGameSceneMenuList ["perfect"].SetActive (true);


            break;

        }

    }

    string DirtoString (int pNum)
    {

        switch (pNum) {
        case 1:
            mdirName = Ag.mgIsKick ? "arrowc" : "arrow_blue";
            break;
        case 2:
            mdirName = Ag.mgIsKick ? "arrowb" : "arrow_red";
            break;
        case 3:
            mdirName = Ag.mgIsKick ? "arrowd" : "arrow_skyblue";
            break;
        case 4:
            mdirName = Ag.mgIsKick ? "arrowa" : "arrow_yellow";
            break;
        }
        return mdirName;
    }

    void GuidebarAniInit ()
    {
        Clip = (AnimationClip)Resources.Load ("500Game/Animation/bule");
        Clip2 = (AnimationClip)Resources.Load ("500Game/Animation/KickerBarAni");
        Clip3 = (AnimationClip)Resources.Load ("500Game/Animation/BarAni3");
    }

    void CursorPinAni ()
    {
        if ((int)(Time.time * 5) % 2 == 0) {
            arrListTxt [19] = (Texture2D)(Resources.Load ("UIsource/GamePlayUI/Pin"));
        } else {
            arrListTxt [19] = (Texture2D)(Resources.Load ("UIsource/GamePlayUI/Pin2"));

        }
    }

    void DirBarAni ()
    {
        if ((int)(Time.time * 5) % 2 == 0) {
            arrListTxt [1] = (Texture2D)(Resources.Load ("UIsource/DirectBarLU"));
            arrListTxt [2] = (Texture2D)(Resources.Load ("UIsource/DirectBarRU"));
            arrListTxt [3] = (Texture2D)(Resources.Load ("UIsource/DirectBarLD"));
            arrListTxt [4] = (Texture2D)(Resources.Load ("UIsource/DirectBarRD"));

        } else {
            arrListTxt [1] = (Texture2D)(Resources.Load ("UIsource/DirectBarLU_C"));
            arrListTxt [2] = (Texture2D)(Resources.Load ("UIsource/DirectBarRU_C"));
            arrListTxt [3] = (Texture2D)(Resources.Load ("UIsource/DirectBarLD_C"));
            arrListTxt [4] = (Texture2D)(Resources.Load ("UIsource/DirectBarRD_C"));

        }
    }

    void SkillBarAni ()
    {

        if (Ag.mgIsKick) {
            //("SkillBarAni").HtLog();

            if ((int)(Time.time * 5) % 2 == 0) {
                arrListTxt [10] = (Texture2D)Resources.Load ("UIsource/Kicker_GKBAR");
            } else {

                arrListTxt [10] = (Texture2D)Resources.Load ("260_Tutorial/UI/Accurucy_Bar_KICKER_goodbar1");
            }
        } else {
            if ((int)(Time.time * 5) % 2 == 0) {
                arrListTxt [7] = (Texture2D)Resources.Load ("UIsource/Keeper_GDBAR");
            } else {
                arrListTxt [7] = (Texture2D)Resources.Load ("260_Tutorial/UI/Accurucy_Bar_KEEPER_goodbar1");
            }

        }

    }
    /*
    void GuideAni (int pageNum)
    {
        //ArrGameDirbarOff ();
        switch (pageNum) {
        case 2: 
            KickerDirBaroff ();
            StartCoroutine (GuideAni (0.3f, 0));//left up
            StartCoroutine (RoundBarAni (0.5f, 0));
            StartCoroutine (RoundBarAni2 (0.7f, 0));
            break;
        case 3:
            KickerDirBaroff ();
            StartCoroutine (GuideAni (0.3f, 1));//right up
            StartCoroutine (RoundBarAni (0.5f, 1));
            StartCoroutine (RoundBarAni2 (0.7f, 1));
            break;
        case 4:
            KickerDirBaroff ();
            StartCoroutine (GuideAni (0.3f, 2));//left down
            StartCoroutine (RoundBarAni (0.5f, 2));
            StartCoroutine (RoundBarAni2 (0.7f, 2));
            break;
        case 5:
            KickerDirBaroff ();
            StartCoroutine (GuideAni (0.3f, 3));//right down
            StartCoroutine (RoundBarAni (0.5f, 3));
            StartCoroutine (RoundBarAni2 (0.7f, 3));
            break;
        case 6:
            KickerDirBaroff ();
            StartCoroutine (RoundBarAni (0.5f, 0));
            StartCoroutine (RoundBarAni (0.5f, 1));
            StartCoroutine (RoundBarAni (0.5f, 2));
            StartCoroutine (RoundBarAni (0.5f, 3));
            break;
        }
    }
    */
    IEnumerator TouchDelay (float waitTime)
    {   
        yield return new WaitForSeconds (waitTime);
        /*        
        mTouchFlag = true;

        mTouchFlag = false;
        */
    }

    IEnumerator TitleBar (float waitTime, string pObjNum)
    {
        if (Ag.mRound == 1 || Ag.mRound == 3) {
            //dicGameSceneMenuList ["mTitleEff"].SetActive (true);
            //dicGameSceneMenuList["mTitleEff"].GetComponent<UISprite> ().spriteName = pObjNum;
        }
        //TweenAlpha.Begin (mTitleEff, 1f, 0f).Play(true);

        //mTitleEff.GetComponent< UIButtonTween> ().Play (true);
        //mTitleEff.GetComponent<TweenAlpha> ().enabled = true;
        //mTitleEff.GetComponent<TweenAlpha> ().Play (true);
        mTitleFlag = false;
        yield return new WaitForSeconds (waitTime);
        mTitleFlag = true;
        dicGameSceneMenuList ["Panel_explain"].SetActive (true);
//        dicGameSceneMenuList ["mTutoCaption"].SetActive (true);
        //mTitleEff.SetActive (false);
        //mTutoGirl.SetActive (true);
        //dicGameSceneMenuList["dicGameSceneMenuList["dicGameSceneMenuList["mTutoCaption"]"]"].SetActive (true);
        //dicGameSceneMenuList ["Panel_explain"].SetActive (true);
        if (Ag.mRound == 3) {
            dicGameSceneMenuList ["mTutoCaption"].SetActive (false);
            dicGameSceneMenuList ["Panel_explain"].SetActive (false);
            //mTutoGirl.SetActive (true);
        }
    }

    IEnumerator TitleBar2 (float waitTime, GameObject Gobj)
    {
        Gobj.SetActive (true);
        yield return new WaitForSeconds (waitTime);
        Gobj.SetActive (false);
        //mTitleEff.GetComponent<UIFilledSprite> ().spriteName = pObjNum;

    }

    GameObject GmObj;

    IEnumerator DirWinAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        mMyDir.animation.Play ("mallow");
        dicGameSceneMenuList ["mTutoCaption"].animation.Play ("Captionani");
        //    mEnemyDir.animation.Play("eallow");
    }

    IEnumerator DirLoseAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        mMyDir.animation.Play ("eallow");
        //dicGameSceneMenuList["dicGameSceneMenuList["dicGameSceneMenuList["mTutoCaption"]"]"].animation.Play ("Captionani");
        //    mEnemyDir.animation.Play("eallow");
    }

    IEnumerator SkillWinAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        //mMySkl.animation.Play ("SkillwinAni");
        //dicGameSceneMenuList["mTutoCaption"].animation.Play ("Captionani");
        //    mEnemyDir.animation.Play("eallow");
    }

    IEnumerator ResultBarSkl (float waitTime, bool pObflag, int pMYSklNum, int pEnemSklNum)
    {
        yield return new WaitForSeconds (waitTime);
        mMySkl.SetActive (pObflag);
        mEnemySkl.SetActive (pObflag);
        if (Ag.mgIsKick) {
            mMySkl.GetComponent<UIFilledSprite> ().spriteName = KickertoString (pMYSklNum);
            mEnemySkl.GetComponent<UIFilledSprite> ().spriteName = KeeperSkilltoString (pEnemSklNum);
        } else {
            mMySkl.GetComponent<UIFilledSprite> ().spriteName = KeeperSkilltoString (pMYSklNum);
            mEnemySkl.GetComponent<UIFilledSprite> ().spriteName = KickertoString (pEnemSklNum);
        }
        mMySkl.animation.Play ();
        mEnemySkl.animation.Play ();
    }

    IEnumerator TextParticle (float waitTime)
    {
        mTextParticle.SetActive (true);
        mTextParticle.transform.FindChild ("Panel/PanelLabel/GoalLabel").gameObject.GetComponent<UISlicedSprite> ().spriteName = Ag.mgIsKick ? "goalin" : "no goal";
        yield return new WaitForSeconds (waitTime);
        mTextParticle.SetActive (false);

    }

    IEnumerator ResultBarDir (float waitTime, bool pObflag, int pMyDirNum, int pEnemDirNum)
    {
        yield return new WaitForSeconds (waitTime);
        mMyDir.SetActive (pObflag);
        mEnemyDir.SetActive (pObflag);
        mMyDir.GetComponent<UIFilledSprite> ().spriteName = DirtoString (pMyDirNum);
        mEnemyDir.GetComponent<UIFilledSprite> ().spriteName = DirtoString (pEnemDirNum);
        mMyDir.animation.Play ();
        mEnemyDir.animation.Play ();

    }

    IEnumerator ResultBar (float waitTime, bool pObflag, bool pFlag)
    {
        yield return new WaitForSeconds (waitTime);
//        mMyResult.SetActive (pObflag);
//        mMyResult.GetComponent<UIFilledSprite> ().spriteName = pFlag ? "youlwin" : "youlose";
//        mMyResult.animation.Play ();
        //mResultBox.SetActive (pObflag);
    }

    void KpBarChangeAni ()
    {
        if (!Ag.mgIsKick) {
            if (Ag.mRound == 1) {
                if (mTutorCapN == 1)
                    dicGameSceneMenuList ["mTutoCaption"].GetComponent<UILabel> ().text = dicCapt ["Keeper1"];
                if (mTutorCapN == 8) {
                    if ((int)(Time.time * 5) % 2 == 0) {
                        arrListTxt [4] = (Texture2D)(Resources.Load ("UIsource/DirectBarRD"));
                    } else {
                        arrListTxt [4] = (Texture2D)(Resources.Load ("UIsource/DirectBarRD_C"));
                    }

                }
                if (mTutorCapN == 10) {
                    if ((int)(Time.time * 5) % 2 == 0) {
                        arrListTxt [2] = (Texture2D)(Resources.Load ("UIsource/DirectBarRU"));
                    } else {
                        arrListTxt [2] = (Texture2D)(Resources.Load ("UIsource/DirectBarRU_C"));
                    }

                }

            }
        }
    }

    IEnumerator RoundBarAni2 (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        //arrKickerDirBar [pObjNum].renderer.enabled = true;
        //arrKickerDirBar [pObjNum].animation.renderer.enabled = true;
        //arrKickerDirBar [pObjNum].animation.Play ("AlphaAni2");
    }

    IEnumerator TouchBarEff (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        if (mTouchBarEff.activeSelf)
            mTouchBarEff.GetComponent<ParticleSystem> ().Play ();
    }

    int Num;
    bool mTouchBarflag = false;

    IEnumerator TouchBarEffLoop (float waitTime)
    {
        Num = 0;
        while (mTouchBarflag) {
            mTouchBarEff.SetActive (true);
            if (Num > 1 && mTouchBarflag) {
                mTouchBarEff.GetComponent<ParticleSystem> ().Play ();
            }
            yield return new WaitForSeconds (waitTime);

            Debug.Log ("Log" + Num++);
        }
    }

    IEnumerator TouchBarEffectOnOff (float waitTime, bool pActive)
    {
        mTouchBarEff.SetActive (pActive);
        yield return new WaitForSeconds (waitTime);
        mTouchBarEff.SetActive (!pActive);
    }

    IEnumerator MalPungOnoff (float waitTime, bool pActive)
    {
        //dicGameSceneMenuList ["mTutoCaption"].SetActive (pActive);
        dicGameSceneMenuList ["Panel_explain"].SetActive (pActive);
        //mTutoGirl.SetActive (pActive);
        //mTutoGirl.SetActive(pActive);
        yield return new WaitForSeconds (waitTime);
        //dicGameSceneMenuList ["mTutoCaption"].SetActive (!pActive);
        dicGameSceneMenuList ["Panel_explain"].SetActive (!pActive);
        //mTutoGirl.SetActive (!pActive);
        //mTutoGirl.SetActive(pActive);
    }

    void KickerDirBaroffR ()
    {
        for (int i = 0; i < 4; i++) {
            arrKickerDirBar [i].transform.renderer.enabled = false;
        }
    }

    void BarAnimationOff ()
    {
        for (int i = 0; i < 4; i++) {
            arrKickerDirBar [i].SetActive (false);
            arrKickerDirBar2 [i].SetActive (false);
        }
    }

    void attackresultBaroff ()
    {
        dicGameSceneMenuList ["blaze_end"].SetActive (false);
        dicGameSceneMenuList ["keeper_end"].SetActive (false);
        dicGameSceneMenuList ["kick_end"].SetActive (false);
        dicGameSceneMenuList ["lightning_end"].SetActive (false);
    }

    void TutorialBegin ()
    {
        if (Ag.mgIsKick) {
            myCard = Ag.mySelf.GetCardOrderOf (1);
            SetPolyGon (myCard);
            mPlayerKicker = (GameObject)Instantiate (mPoly);
            EnemCard = Ag.mySelf.GetCardOrderOf (0);


            SetPolyGon (EnemCard);
            mPlayerKeeper = (GameObject)Instantiate (mPoly);
            Debug.Log ("mPoly   " + mPoly);
        } 
    }

    void TutorialSetTextureCharacter ()
    {
        if (Ag.mgIsKick) {
            
            mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.mySelf.arrUniform [0].Kick.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub);
            KickermShirts = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = KickermShirts;
            mProcedureMat = (ProceduralMaterial)subPants [Ag.mySelf.arrUniform [0].Kick.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Pants.ColSub);
            KickerPants = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = KickerPants;
            mProcedureMat = (ProceduralMaterial)subSocks [Ag.mySelf.arrUniform [0].Kick.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Socks.ColSub);
            KickermSocks = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = KickermSocks;
            mProcedureMat = (ProceduralMaterial)subKeeperShirts [Ag.mySelf.arrUniform [0].Keep.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub);
            KeeperShirts = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [0].mainTexture = KeeperShirts;
            mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.mySelf.arrUniform [0].Keep.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Pants.ColSub);
            KeeperPants = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [1].mainTexture = KeeperPants;
            mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.mySelf.arrUniform [0].Keep.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Socks.ColSub);
            KeeperSocks = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [2].mainTexture = KeeperSocks;
            
            
        } else {
            mProcedureMat = (ProceduralMaterial)subKeeperShirts [Ag.mySelf.arrUniform [0].Keep.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub);
            KeeperShirts = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [0].mainTexture = KeeperShirts;
            mProcedureMat = (ProceduralMaterial)subPants [Ag.mySelf.arrUniform [0].Keep.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Pants.ColSub);
            KeeperPants = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [1].mainTexture = KeeperPants;
            mProcedureMat = (ProceduralMaterial)subSocks [Ag.mySelf.arrUniform [0].Keep.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Keep.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Keep.Socks.ColSub);
            KeeperSocks = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [2].mainTexture = KeeperSocks;
            mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.mySelf.arrUniform [0].Kick.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub);
            KickermShirts = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = KickermShirts;
            mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.mySelf.arrUniform [0].Kick.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Pants.ColSub);
            KickerPants = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = KickerPants;
            mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.mySelf.arrUniform [0].Kick.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.mySelf.arrUniform [0].Kick.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.mySelf.arrUniform [0].Kick.Socks.ColSub);
            KickermSocks = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = KickermSocks;
            
        }
    }

    IEnumerator KeeperinfoBarFlayAni (float PlayTime)
    {
        yield return new WaitForSeconds (PlayTime);
        //dicGameSceneMenuList ["Keeperinfo"].animation.Stop();
    }

    void TutoriPreGame ()
    {
        if (Ag.mgIsKick) {
            
            Ag.LogString (" myCard count " + myCard.arrArea.Count);
            PlayerOrderNum++;
            
            KickerScenePlay (false);

            if (Ag.mRound == 2) {
                SetKickerDir (true);
                StartCoroutine (ShowKickerFX2 (1.8f));
            }
            DestroyObject (mPlayerKicker);
            DestroyObject (mPlayerKeeper);
            SetPolyGon (myCard);
            mPlayerKicker = (GameObject)Instantiate (mPoly);
            EnemCard = Ag.mySelf.GetCardOrderOf (0);
            SetPolyGon (EnemCard);


            mPlayerKeeper = (GameObject)Instantiate (mPoly);


            Debug.Log ("mPoly" + mPoly);


            PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, "KickerShoes01");
            PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, "KeeperGloves01");
            if (AgStt.mgGameTutorial) {
                dicGameSceneMenuList ["Keeperinfo"].SetActive (true);
                EnemyKeeperInfo ();
                StartCoroutine (KeeperinfoBarFlayAni (5f));
            }
            mBall = mPlayerKicker.transform.FindChild ("deleveryBall").gameObject; //LJk 2013 07 25
            mBall.transform.renderer.enabled = false;
            PreAni ();
            //DrawGuideLineNew ();
            StartCoroutine (CaptureImage ());
        } else {
          
            EnemyOrderNum++;
            dicGuideObjectPos = new Dictionary<int, float> ();
            dicGuideObjectWidth = new Dictionary<int, float> ();
            //DrawGuideLineNew ();
            //mPlayerInfoDirflag = true;
            mIsKeeperSkl = 0;

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
            myCard = Ag.mySelf.GetCardOrderOf (0);
            SetPolyGon (myCard);
            mPlayerKeeper = (GameObject)Instantiate (mPoly);
            
            PlayerCostume.instance.SetCostumeToPlayer (true, mPlayerKicker, "KickerShoes01");
            PlayerCostume.instance.SetCostumeToPlayer (false, mPlayerKeeper, "KeeperGloves01");

            if (!AgStt.mgGameTutorial)
                dicGameSceneMenuList ["Kickerinfo"].SetActive (true);
            //EnemyKickerinfo ();
            mBall = mPlayerKicker.transform.FindChild ("deleveryBall").gameObject.gameObject; //LJK 07:25
            mBall.transform.renderer.enabled = false;
            dicGameSceneMenuList ["Panel_keeperarrow_Main"].SetActive (true);
            DragPositionF (true);
            PreAni ();
            StartCoroutine (CaptureImage ());
        }
    }

    void GotoMainMenu ()
    {
        AgStt.mgGameTutorial = false;
        PreviewLabs.PlayerPrefs.SetBool ("DidTutorial", true);
        PreviewLabs.PlayerPrefs.Flush ();
        Application.LoadLevel ("StartMenu");
    }

    void CaptionNum (string pNum)
    {
        for (int i = 0; i < 31; i++) {
            dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain" + i).gameObject.SetActive (false);
        }
        dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain13_1").gameObject.SetActive (false);
        dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain_re").gameObject.SetActive (false);
        dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain" + pNum).gameObject.SetActive (true);
    }

    IEnumerator CaptionRePlay ()
    {
        for (int i = 0; i < 31; i++) {
            dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain" + i).gameObject.SetActive (false);
        }
        dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain_re").gameObject.SetActive (true);
        yield return new WaitForSeconds (1f);
        dicGameSceneMenuList ["Panel_explain"].transform.FindChild ("explain_re").gameObject.SetActive (false);
    }

    IEnumerator DelayReplay ()
    {
        yield return new WaitForSeconds (1.5f);
        mCursor.SetActive (false);
        mStateArr.SetStateWithNameOf ("Replay");
        Debug.Log ("Replay");

    }

    IEnumerator DelayReplaySkl ()
    {
        yield return new WaitForSeconds (1.5f);
        mStateArr.SetStateWithNameOf ("ReplaySkl");
        
    }

    void KickerDirbarEff (string pNum)
    {
        for (int i = 1; i < 5; i++) {
            dicGameSceneMenuList ["Panel_progressbar_kickbar_user_back_effect_bar" + i].SetActive (false);
        }
        dicGameSceneMenuList ["Panel_progressbar_kickbar_user_back_effect_bar" + pNum].gameObject.SetActive (true);
    }

    void PlayDirect ()
    {
        StartCoroutine (DelayPlay ());
        dicGameSceneMenuList ["Panel_finger"].SetActive (false);
        dicGameSceneMenuList ["Panel_finger1"].SetActive (false);
    }

    IEnumerator DelayPlay ()
    {

        dicGameSceneMenuList ["Panel_explain"].SetActive (false);
        dicGameSceneMenuList ["PlayMode"].SetActive (false);
        dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin2").gameObject.SetActive (false);
        dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("back_effect/fin1").gameObject.SetActive (false);


        mCursor.SetActive (true);
        yield return new WaitForSeconds (1f);
        mTutorCapN++;
        mTitleFlag = true;



        for (int i = 1; i < 5; i++) {
            dicGameSceneMenuList ["Panel_progressbar_kickbar_user_back_effect_bar" + i].gameObject.SetActive (false);
        }


        
    }

    void TutorScore ()
    {

        if (arrEnScore.Count > 5 || arrMyScore.Count > 5) {  // Above 5 case... Remove all...
            for (int jk = 0; jk < 5; jk++) {
                arrMyScore.RemoveAt (0);
                arrEnScore.RemoveAt (0);
                mMyPointBall [jk].SetActive (false);
                mEnemyPointBall [jk].SetActive (false);
            }
        }
        

        for (int i = 0; i < arrAllMyScore.Count; i++) {
            Debug.Log ("arrMyScore.Count" + arrMyScore.Count);

            mMyPointBall [i].SetActive (true);
            mMyPointBall [i].GetComponent<UISprite> ().spriteName = arrAllMyScore [i] ? "img_success" : "img_fail"; 

        }


        
        for (int i = 0; i < arrAllEnScore.Count; i++) {
            Debug.Log ("arrenScore" + arrEnScore.Count);

            mEnemyPointBall [i].SetActive (true);
            mEnemyPointBall [i].GetComponent<UISprite> ().spriteName = arrAllEnScore [i] ? "img_success" : "img_fail"; 
                    

        }



        
        dicGameSceneMenuList ["EnemyPointLabel"].GetComponent<UILabel> ().text = FunResultNum (arrAllEnScore).ToString ();
        dicGameSceneMenuList ["MyPointLabel"].GetComponent<UILabel> ().text = FunResultNum (arrAllMyScore).ToString ();

    }
}