//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    int mIsKeeperSkl = 0;
    AmPlayer AmP;
    Texture2D mPinTex, mBarTex;
    Texture2D blue, red, yellow, skyblue;
    public Rect mWaguRec, mPicRec, mDirRec, mEnemyBioRec;
    float mDirX, mDirY, mDirWidth, mDirheight;
    float mMousePosX, mMouseAddPos, mMousePosY;
    Vector2 enemCurPosi, enemCurSize, enemPrePosi, enemPreSize;
    Vector2 EnemyBarPosi1, EnemyBarPosi2, EnemyBarPosi3, EnemyBarPosi4;
    Vector2 EnemyBarSize1, EnemyBarSize2, EnemyBarSize3, EnemyBarSize4;
    Vector2 EnemyBarPosiFin1, EnemyBarPosiFin2, EnemyBarPosiFin3, EnemyBarPosiFin4;
    Vector2 EnemyBarSizeFin1, EnemyBarSizeFin2, EnemyBarSizeFin3, EnemyBarSizeFin4;
    GameObject mCursor;
    public float smooth = 5;
    public Vector3 newPosition;

    void DrawGameDirection ()
    {
        //Ag.LogString("mgIsKick "+ Ag.mgIsKick.ToString() );
        if (mStage.mIsTouched && Ag.mgIsKick) {
            mstatusBar = true; 
        }
        if (!Ag.mgIsKick) {   // Defence Case... Draw Buttons..

        } else {
            DrawCursor ();  // Draw Cursor..  Call......
        }
    }

    void DrawGameSkill ()
    {
        if (myCard.arrArea == null || myCard.arrArea.Count == 0)
            return;
        if (mStateArr.GetCurStateName () == "MidPaus")
            sclPotion = 0.4f;
        if (mStage.WillDrawCursor ())
            DrawCursor ();  // Draw Cursor..  Call......
    }
    //-------------------------------------------------------------------------------- PositionChange
    IEnumerator KeeperinfoAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        mIsKeeperSkl = 5;
    }

    IEnumerator SmallinfoFlag (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
    }

    IEnumerator TutorinfoFlag (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        //mSmallEnemyFlag = false;
    }
    //-------------------------------------------------------------------------------- DrawCursorBar
    void CreateCursor ()
    {
        mCursor = dicGameSceneMenuList ["MainBar"].transform.FindChild ("nod").gameObject;
        if (AgStt.mgGameTutorial) {
            mCursor = dicGameSceneMenuList ["Panel_progressbar_kickbar_user"].transform.FindChild ("nod").gameObject;
        }
    }

    void SkillCursor ()
    {
        mCursor = dicGameSceneMenuList ["MainSkillBar"].transform.FindChild ("nod").gameObject;
        if (myCard.WAS.grade == "S")
            mCursor = dicGameSceneMenuList ["SGrade_MainSkillBar"].transform.FindChild ("nod").gameObject;
        if (AgStt.mgGameTutorial) {
            mCursor = dicGameSceneMenuList ["Panel_progressbar_skillbar_user"].transform.FindChild ("nod").gameObject;
        }
    }

    void DrawGuideLineNew ()
    {
        if (Ag.mgIsKick) {
            DrawXStart = 0.2f; 
            DrawXWidth = 0.6f;
        } else {
            DrawXStart = 0.56f; 
            DrawXWidth = 0.425f;
        }

        mGuideBar = new List<GameObject> ();
        dicGuideObjectPos = new Dictionary<int, float> ();
        dicGuideObjectWidth = new Dictionary<int, float> ();
        GameObject Guidebar;
        int i, num;
        if (Ag.mgIsKick) {
            num = myCard.arrArea.Count;
        } else {
            num = EnemCard.arrArea.Count;
        }

        for (int j = 0; j < num; j++) {
            dicGuideObjectPos.Add (j, mPos);
            dicGuideObjectWidth.Add (j, mWidth);
        }
        for (i = 0; i < num; i++) {
            int[] curVal = Ag.mgIsKick ? (int[])myCard.arrArea [i] : (int[])EnemCard.arrArea [i];
            int color = curVal [0];
            int sta = curVal [1], end = curVal [2];

            //Debug.Log ("Color  " + curVal [0] + "Start  " + curVal [1] + "End  " + curVal [2]);
            float staX = (float)(sta / 1000.0f);
            float width = (float)((end - sta) / 1000.0f);

            Guidebar = (GameObject)Instantiate (Resources.Load ("prefab_General/IngameKickBar"));
            Guidebar.transform.parent = dicGameSceneMenuList ["MainBar"].transform;
            Guidebar.transform.localPosition = new Vector3 ((staX * 580) - 290, -260, -0.29f);
            Guidebar.GetComponent<UISprite> ().spriteName = "kickbar" + color;
            if (color == 0 && width * 580 < 0)
                width = 0;
            Guidebar.transform.localScale = new Vector3 (width * 580, 24, 1);
            ListGameObject.Add (Guidebar);

            if (!Ag.mgIsKick) {
                dicGameSceneMenuList ["Kickerinfo"].transform.localPosition = new Vector3 (260, -180, 0);
                for (int j = 0; j < ListGameObject.Count; j++) {

                    ListGameObject [j].transform.parent = dicGameSceneMenuList ["Kickerinfo"].transform.FindChild ("DirectionBar").transform;
                    Guidebar.transform.localPosition = new Vector3 ((staX * 508) - 254, 0, -8);
                    Guidebar.transform.localScale = new Vector3 (width * 508, 24, 1);
                }
                //StartCoroutine (CorutineTest ());
            }
        }
    }

    void DrawCursor ()
    {
        if (AgStt.mgGameTutorial && !mCursorFlag)
            return;
        float curPosi;
        string stt = mStateArr.GetCurStateName ();
        if (stt == "MidPausPotion" || stt == "MidPausBiggerPotion" || stt == "BeforeDirPotion" || stt == "MidPausBiggerGamdDir")
            curPosi = 0;
        else
            curPosi = mStage.GetCursorPosition ();  // 0 ~ 1000

        if (mStage.mIsTouched && !mSkillSound && (/*         ( stt == "GameDir" && Ag.mgDirection == 0 ) || */(stt == "GameSkl" && Ag.mgSkill == 0))) {
            SoundManager.Instance.Play_Effect_Sound ("Bad");
            mSkillSound = true;
        }
        if (mStage.mIsTouched && !mSkillSound && (stt == "GameDir" && Ag.mgDirection > 0 && Ag.mgDirection < 5)) {
            SoundManager.Instance.Play_Effect_Sound ("SelectDirection");
            mSkillSound = true;
        }
        //Ag.LogString (" DrawCursor >>>   " + curPosi);
        mCursor.transform.localPosition = new Vector3 ((((float)curPosi / 1000.0f) * 580) - 290, mCursor.transform.localPosition.y, -10); //imsi
    }
    //    public IEnumerator CorutineTest ()
    //    {
    //        Debug.Log ("KeeperInfoAnimationStart");
    //        yield return new WaitForSeconds (0.1f);
    //        dicGameSceneMenuList ["Kickerinfo"].animation.Play ("versusinfo");
    //
    //    }
    //
    //    void xxDrawOneBar (bool pIsEnemy, int curCol2, int sta, int end)
    //    {
    //        float y1 = mSY * 0.902438f, y2 = mSY * 0.073663f, xScale = 1f;
    //        float staX = xScale * mSX * (DrawXStart + DrawXWidth * (float)sta / 1000.0f);
    //        float width = xScale * mSX * (DrawXWidth * (float)(end - sta) / 1000.0f);
    //        if (curCol2 == 5)
    //            curCol2 = 20;
    //        float MiniStax, MiniStawidth;
    //
    //        if (pIsEnemy) {
    //            staX = mDirRec.x + mDirRec.width * sta / 1000f;
    //            width = mDirRec.width * (end - sta) / 1000f;
    //            MiniStax = mDirRec2.x + mDirRec2.width * sta / 1000f;
    //            MiniStawidth = mDirRec2.width * (end - sta) / 1000f;
    //            y2 = mDirRec.height; // >>>>>> enemCurSize.y
    //            if (staX <= mMousePosX && mMousePosX < staX + width && mMousePosY < mSY * 0.2f && (curCol2 != 0 || curCol2 != 20)) {
    //                mIsKeeperSkl = curCol2;
    //                if (mIsKeeperSkl == 20)
    //                    mIsKeeperSkl = 5;
    //                mBarTex = mKpPinTex [mIsKeeperSkl];
    //            }
    //        } else {
    //            GUI.DrawTexture (new Rect (staX, y1, width, y2), (Texture2D)arrListTxt [curCol2], ScaleMode.StretchToFill, true);
    //        }
    //    }
    //  ////////////////////////////////////////////////     Game :: Direction of Enemy Showing...
    //  ////////////////////////////////////////////////     Draw Moving Cursor
}
