//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    GameObject m_LodingCircle;
    //--------------------------------------------------------------------Kicker LodingBar
    void mLodingInerOuterAni (bool pbool)
    {
        if (pbool) {
            //m_LodingCircle.transform.FindChild ("InGround").animation.Play ();
            //m_LodingCircle.transform.FindChild ("OutGround").animation.Play ();
        } else {
            //m_LodingCircle.transform.FindChild ("InGround").animation.Stop ();
            //m_LodingCircle.transform.FindChild ("OutGround").animation.Stop ();
        }
    }

    IEnumerator PlayerinfobarFlag (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        //mKeeperLabel.transform.renderer.material.mainTexture = (Texture2D)Resources.Load ("200_Start/UI/font");
        //mKeeperLabel.transform.renderer.enabled = true;
        //mKeeperLabel.animation.Play ("KeeperFonttex");
        //mPlayerinfoflag = true;
    }

    IEnumerator LodingBarAni (float waitTime, Texture2D pObjString, string paniname, bool pbool)
    {
        yield return new WaitForSeconds (waitTime);
        //LodingBarOff (true)
        ;
        //mLodingInerOuterAni (pbool);
    }

    IEnumerator KeeperLodingBarAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        //mKeeperLabel.GetComponent<UILabel>().text = "SWIPE";
        //LodingBarOff (true)
        ;
        //mKeeperLabel.transform.renderer.material.mainTexture = (Texture2D)Resources.Load ("200_Start/UI/fonttex");
        //mPlayerinfoflag = false;
        //mKeeperLabel.animation.Play ("KeeperFonttex");
        //Defaultcloth ();

        
    }
    /*
    void LodingBarOff (bool pbool)
    {
        m_LodingCircle.SetActive (pbool);
    }
    */

    void KpLodingBarAni (float waitTime, Texture2D pObjString, string pAniName, bool pbool)
    {
        StartCoroutine (LodingBarAni (waitTime, pObjString, pAniName, pbool));
        
    }
   
    //----------------------------------------------------------------------------Kicker view event;
    IEnumerator GuideAni (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        arrListTxt [pObjNum + 1] = arrTexBar [pObjNum];
        ListGameObject [pObjNum].animation.AddClip (Clip, "BarAni");
        ListGameObject [pObjNum].renderer.enabled = true;
        ListGameObject [pObjNum].animation.Play ("BarAni");
        SoundManager.Instance.Play_Effect_Sound ("Swoosh");
    }

    IEnumerator SelectAni (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        ListGameObject [pObjNum].animation.AddClip (Clip, "BarAni");
        ListGameObject [pObjNum].renderer.enabled = true;
        ListGameObject [pObjNum].animation.Play ("BarAni");
        SoundManager.Instance.Play_Effect_Sound ("Swoosh");
    }

    IEnumerator RoundBarAni (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        arrListTxt [pObjNum + 1] = arrTexBar [pObjNum + 4];
        //arrKickerDirBar [pObjNum].animation.renderer.enabled = true;
        //arrKickerDirBar [pObjNum].animation.AddClip (Clip2, "KickerBarRoundAni");
        //arrKickerDirBar [pObjNum].animation.Play ("KickerBarRoundAni");
        
    }

    IEnumerator RoundAllBarAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        for (int i = 0; i < 4; i++) {
            ;//arrKickerDirBar [i].animation.Play ("UiANI_DirectionBar");
        }
    }

    void GuidebarAniPlay ()
    {
        Clip = (AnimationClip)Resources.Load ("Animation/KeeperViewAni/bule");
        Clip2 = (AnimationClip)Resources.Load ("Animation/KickerBarAni");
        Clip3 = (AnimationClip)Resources.Load ("Animation/BarAni2");
        StartCoroutine (GuideAni (0.4f, 0));//left up
        StartCoroutine (RoundBarAni (0.41f, 0));
        StartCoroutine (GuideAni (0.5f, 1));//right up
        StartCoroutine (RoundBarAni (0.51f, 1));
        StartCoroutine (GuideAni (0.6f, 2));//left down
        StartCoroutine (RoundBarAni (0.61f, 2));
        StartCoroutine (GuideAni (0.7f, 3));//right down
        StartCoroutine (RoundBarAni (0.71f, 3));
        StartCoroutine (RoundAllBarAni (0.9f));
    }

    Material GameTexture (int pcolnum)
    {
        //Material mat = 
//        Debug.Log ("PColNUM" + pcolnum);
        return (Material)Resources.Load ("Textures/GameUI/Material/" + pcolnum);

    }

    Material GameTexture2 (int pcolnum, string pCap)
    {
        return (Material)Resources.Load ("Textures/GameUI/Material/" + pcolnum.ToString () + pCap);
    }

    void DestoryGuideBar ()
    {

        for (int i = 0; i < ListGameObject.Count; i++) {
            DestroyObject (ListGameObject [i]);
        }
        ListGameObject.Clear ();
        dicGuideObjectPos.Clear ();
        dicGuideObjectWidth.Clear ();
    }
    //----------------------------------------------------------------------------KeeperView Evnet;
    IEnumerator KeeperViewPreAni (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        arrListTxt [pObjNum + 1] = arrTexBar [pObjNum];
    }

    IEnumerator KeeperAllAni (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        for (int i = 0; i < 4; i++) {
            arrKeeperBarB [i].renderer.enabled = true;
            arrKeeperBarB [i].animation.Play ("KeeperDirBar");
            
        }
    }

    IEnumerator KeeperinfoAni (float waitTime, int pObjNum)
    {
        yield return new WaitForSeconds (waitTime);
        arrListTxt [pObjNum + 1] = arrTexBar [pObjNum + 4];
    }

    void GuideKeeperViewAni ()
    {
        Clip = (AnimationClip)Resources.Load ("500Game/Animation/KeeperPreBar");
    }
    //------------------------------------------------------------------------------
    void KickerDirbaroff ()
    {
        for (int i = 0; i < 4; i++) {
            arrKickerDirBar [i].SetActive (false);
            arrKickerDirBar2 [i].SetActive (false);
        }
    }

    IEnumerator ShowZoomCameraoff (float time)
    {
        yield return new WaitForSeconds (time);
        //mSmallEnemyFlag = false;
    }

    IEnumerator ShowKickerFX2 (float time)
    {
        yield return new WaitForSeconds (time);
        for (int i = 0; i < 4; i++) {
            arrKickerDirBar2 [i].SetActive (true);
        }
    }


    IEnumerator ShowZoomCameraoff (float time, int Pnum)
    {
        //Ag.LogDouble (" ShowZoomCameraoff ");
        yield return new WaitForSeconds (time);
        //Ag.LogDouble (" ShowZoomCameraoff      yield   >>>   ShowZoomCameraoff ");
        arrKickerDirBar2 [Pnum].SetActive (true);
    }

    IEnumerator WaittimeItemShow (float time)
    {
        yield return new WaitForSeconds (time);
        if (!Ag.mBallEventAlready) {

            mEventItemShowTime = false;
        } else {

            mEventItemShowTime = false;
        }
            
    }

    IEnumerator CResultShow (float time)
    {
        yield return new WaitForSeconds (time);
        mResultShow = true;    
    }

    IEnumerator mRandomItemoff (float time)
    {
        yield return new WaitForSeconds (time);

        mMiniItem.SetActiveRecursively (false);
    }

    void SetKickerDir (bool dir)
    {
        dicGameSceneMenuList ["Fx_kick"].SetActive (dir);
        for (int i = 0; i< 4; i++) {
            arrKickerDirBar [i].active = dir;


        }
    }
    //  ////////////////////////////////////////////////     Game :: Intro Ani.....
    void SetPlayerDir2 ()
    {
        //Debug.Log ("Incoming");
        //Ag.LogIntenseWord ("Set Player Dir 2");

        mstatusBar = false;
     
        if (Ag.mgIsKick) {
            //Debug.Log ("SetPlayer2 :: DirectionNum " + Ag.mgDirection );
            KickerDirbaroff ();
            if (Ag.mgDirection == 0) DirectionMissEff();

            if (Ag.mgDirection == 1) {
                {
                    StartCoroutine (ShowZoomCameraoff (0.3f, 0));
                }
            }
            if (Ag.mgDirection == 2) {
                {
                    StartCoroutine (ShowZoomCameraoff (0.3f, 1));
                }
            }
            if (Ag.mgDirection == 3) {
                {
                    StartCoroutine (ShowZoomCameraoff (0.3f, 2));
                }
            }
            if (Ag.mgDirection == 4) {
                {
                    StartCoroutine (ShowZoomCameraoff (0.3f, 3));
                }
            }
        } else {

            switch (Ag.mgDirection) {

            case 1:
                KeeperUISwipeAlpha (1);
                break;
            case 2:
                KeeperUISwipeAlpha (2);
                break;
            case 3:
                KeeperUISwipeAlpha (3);
                break;
            case 4:
                KeeperUISwipeAlpha (4);
                break;
            case 5:
                KeeperUISwipeAlpha (5);
                break;

            }
        }
    }

    public IEnumerator KeeperUIBarTransformAni ()
    {
        arrKeeperBarB [0].animation.Play ("KeeperDirBar");
        yield return new WaitForSeconds (0.2f);
        arrKeeperBarB [1].animation.Play ("KeeperDirBar");
        yield return new WaitForSeconds (0.2f);
        arrKeeperBarB [2].animation.Play ("KeeperDirBar");
        yield return new WaitForSeconds (0.2f);
        arrKeeperBarB [3].animation.Play ("KeeperDirBar");

    }
    /// <summary>
    /// 키퍼 드래그 이펙트
    /// </summary>
    /// 
    int mChoiceNum;
    void KeeperUISwipeAlpha (int pNum)
    {

        //Debug.Log ("KeeperUISwipe " + pNum);
        dicGameSceneMenuList ["Panel_keeperarrow_Main"].SetActive (true);
        dicGameSceneMenuList ["Panel_keeperarrow_Main2"].SetActive (true);
        for (int i=0; i < 4; i++) {
            arrKeeperBarF [i].SetActive (true);
            arrKeeperBarB [i].SetActive (true);
            arrKeeperBarS [i].SetActive (false);
            arrKeeperBarD [i].SetActive (false);
        }
        arrKeeperBarF [pNum-1].SetActive (false);
        arrKeeperBarB [pNum-1].SetActive (false);
        arrKeeperBarS [pNum-1].SetActive (true);

        //StopAllCoroutines ();
        StopCoroutine ("arrTutorKeeperBarArrowSet");
        StopCoroutine ("arrKeeperBarArrowSet");
        mChoiceNum = pNum;
        if (AgStt.mgGameTutorial) {
            StartCoroutine(arrTutorKeeperBarArrowSet());
        } else  {
            StartCoroutine(arrKeeperBarArrowSet());
        }
    }



    IEnumerator arrKeeperBarArrowSet () {
        //arrKeeperBarS [pNum - 1].SetActive (false);
        yield return new WaitForSeconds (0.4f);
        arrKeeperBarS [mChoiceNum-1].SetActive (false);
        arrKeeperBarD [mChoiceNum-1].SetActive (true);
    }

    IEnumerator arrTutorKeeperBarArrowSet () {

        //arrKeeperBarS [pNum - 1].SetActive (false);
        yield return new WaitForSeconds (0.4f);
        if (Ag.mRound == 1 && !Ag.mgIsKick && Ag.mgDirection == 2) {
            arrKeeperBarS [mChoiceNum-1].SetActive (false);
            arrKeeperBarD [mChoiceNum-1].SetActive (true);
        }
        if (Ag.mRound == 2 && !Ag.mgIsKick && Ag.mgDirection == 4) {
            arrKeeperBarS [mChoiceNum-1].SetActive (false);
            arrKeeperBarD [mChoiceNum-1].SetActive (true);
        }
    }


    void KeeperUISwipeSetLastDir (int pNum)
    {
        Debug.Log ("KeeperUISwipe " + pNum);
        dicGameSceneMenuList ["Panel_keeperarrow_set"].SetActive (true);

        dicGameSceneMenuList ["Panel_keeperarrow_Main2"].SetActive (false);
        for (int i=0; i < 4; i++) {
            arrKeeperBarS [i].SetActive (false);
        }
        if (pNum < 1)
            return;
        else arrKeeperBarS [pNum - 1].SetActive (true);
        dicGameSceneMenuList ["Panel_keeperarrow_set"].animation.Play();
    }



    void SetPlayerDir ()
    {
        if (Ag.mgIsKick) {
            Debug.Log ("mGDir>>>>>>>>>>>>>>>>>>>>>>>>>" + Ag.mgDirection);
            if (Ag.mgDirection == 0) {
                for (int i = 0; i < 4; i++) {
                    arrKickerDirBar [i].SetActive (false);
                } 
            } else {
                for (int i = 1; i < 5; i++) {
                    if (Ag.mgEnemDirec == i) {
                        arrKickerDirBar [i - 1].SetActive (true);
                        continue;
                    }
                    arrKickerDirBar [i - 1].SetActive (false);
                } 
            }
        } else {
        }
        
    }

    void EnemyCharacterEffect ()
    {



        int myDir, enDir, mySkl, enSkl;
        Ag.NodeObj.GetEnemyDirectSkill (out enDir, out enSkl);
        myDir = Ag.mgDirection;
        mySkl = Ag.mgSkill;
        if (AgStt.mgGameTutorial) {
            enDir = Ag.mgEnemDirec;
            enSkl = Ag.mgEnemSkill;
        }

        //Ag.LogString ("endir"  +  enDir + "enskl"  + enSkl); 
        
        if (Ag.mgIsKick) {
            if (enSkl == 1) {
                //mDirUpclone4 = (GameObject)Instantiate (mDirUpeff2, mKpTrailL.transform.position, Quaternion.identity);
                //mDirUpclone4.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameL); //mDirUpclone4.animation.Play();
                //mDirUpclone5 = (GameObject)Instantiate (mDirUpeff2, mKpTrailR.transform.position, Quaternion.identity);
                //mDirUpclone5.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameR); //mDirUpclone5.animation.Play();
            }
            if (enSkl == 2) {
                mKpTrailL.GetComponent<TrailRenderer> ().enabled = true;
                mKpTrailR.GetComponent<TrailRenderer> ().enabled = true;
                mDirUpclone4 = (GameObject)Instantiate (mDirUpeff, mKpTrailL.transform.position, Quaternion.identity);
                mDirUpclone4.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameL);
                mDirUpclone5 = (GameObject)Instantiate (mDirUpeff, mKpTrailR.transform.position, Quaternion.identity);
                mDirUpclone5.transform.parent = mPlayerKeeper.transform.FindChild (mFoldNameR);
                mDirUpclone4.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
                mDirUpclone5.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
            }
        } else {
            if (enSkl == 1) {

            }
            if (enSkl == 2) {
                mKickBall.GetComponent<TrailRenderer> ().enabled = true;
                mKickBall.GetComponent<TrailRenderer> ().time = 0.2f;
                mDirUpclone6 = (GameObject)Instantiate (mSkillUpeff2, mPlayerKicker.transform.FindChild (mkickerRfoot).position, Quaternion.identity);
                mDirUpclone6.transform.parent = mPlayerKicker.transform.FindChild (mkickerRfoot); //mDirUpclone6.animation.Play();
            }
            if (enSkl == 3) {
                mKickBall.GetComponent<TrailRenderer> ().enabled = true;
                mKickBall.GetComponent<TrailRenderer> ().time = 0.49f;


                mDirUpclone6 = (GameObject)Instantiate (mSkillUpeff, mPlayerKicker.transform.FindChild (mkickerRfoot).position, Quaternion.identity);
                mDirUpclone6.transform.parent = mPlayerKicker.transform.FindChild (mkickerRfoot);
                mDirUpclone6.transform.localScale = new Vector3 (1.01f, 1.01f, 1.01f);
            }
        }
    }
}
//
//public class GameScene_MakePlane
//{
//    AnimationClip mClip;
//    //[MenuItem("GameObject/Create Other/plane5")]                     //메뉴에 아이템 추가
//    public static GameObject CreatePlane5 (Vector3 pvector1, Vector3 pvector2, Vector3 pvector3, Vector3 pvector4)
//    {
//        GameObject gameObject = new GameObject ("plane5");    //하이라키에 나오는 이름
//        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();   //MeshFilter 컴포넌트 추가
//        Mesh mesh = new Mesh ();
//        MeshCollider meshcollider = gameObject.AddComponent<MeshCollider> ();  //MeshCollider 컴포넌트 추가
//        gameObject.AddComponent<MeshRenderer> ();  //MeshRenderer컴포넌트 추가
//        gameObject.AddComponent<Animation> ();
//        gameObject.animation.playAutomatically = false;
//        mesh.vertices = new Vector3[] {                   //정점 4개만들어줌
//            //new Vector3(-0.5f, 0.5f, 0.0f), 
//            pvector1, pvector2, 
//            pvector3, pvector4
//        };
//        mesh.uv = new Vector2[] {                        //정점 별로 UV좌표 찍어줌
//            new Vector2 (0.0f, 1.0f), new Vector2 (1.0f, 1.0f),
//            new Vector2 (0.0f, 0.0f), new Vector2 (1.0f, 0.0f)
//        };
//        
//        mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };        //삼각형그릴때 순서
//        mesh.RecalculateNormals ();              //일반좌표계로 다시 계산해주는 함수
//        meshFilter.mesh = mesh;                 //메쉬 필터에 Mesh를 넣어줌
//        meshcollider.sharedMesh = mesh;       //collider에도 mesh를 넣어줌
//        
//        return gameObject;
//    }
//}



