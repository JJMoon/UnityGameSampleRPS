//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;

public class GameScene_TrigerAction : MonoBehaviour
{
    GameObject mKeeperEffect, mTutoScript, mMainConView, mKeeperEffectGen;
    GameScene mMain;
    GameObject mBall, ShootBall;
    public GameObject goalfence;
    GameObject mGamematch320, Explode_02, Explode03;

    void Start ()
    {
        if (Application.loadedLevelName == "GameScene")
            mGamematch320 = GameObject.Find ("MainControllView").gameObject;
        Explode_02 = (GameObject)Resources.Load ("Effect/Explode_02");
        Explode03 = (GameObject)Resources.Load ("Effect/Exp02");
        mKeeperEffect = (GameObject)Resources.Load ("Effect/Spell_01");
        mKeeperEffectGen = (GameObject)Resources.Load ("Effect/Spell_02");

        //Ag.LogIntenseWord ("  GameScene   Triger   ");

        mMainConView = GameObject.Find ("MainControllView").gameObject;

    }
    // Update is called once per frame
    void KickerPhysicsAni (Vector3 pVec, bool pgravity, bool pTrigger)
    {
        mMainConView.GetComponent<GameScene> ().mKickBall.animation.Stop ();
        mMainConView.GetComponent<GameScene> ().mKickBall.rigidbody.AddForce (pVec);
        mMainConView.GetComponent<GameScene> ().mKickBall.rigidbody.rigidbody.useGravity = pgravity;
        mMainConView.GetComponent<GameScene> ().mKickBall.rigidbody.collider.isTrigger = pTrigger;
    }
    // Update is called once per frame
    void Update ()
    {
    }

    void OnTriggerEnter ()
    {
        //-------------------------------------------------------------------------------------------------- 키퍼 막는효과
        if (this.gameObject.name == "Bip001 L Hand" || this.gameObject.name == "Bip001 R Hand001" || this.gameObject.name == "Bip001 R Hand") {
            if (Ag.mgIsKick) {
                if (Ag.mgEnemSkill == 1) {
                    SoundManager.Instance.Play_Effect_Sound ("punching_GoodPerfect");
                    Instantiate (mKeeperEffectGen, this.gameObject.transform.position, Quaternion.identity);
                } 
                if (Ag.mgEnemSkill == 2) {
                    if ((Ag.mgDirection == 4 && Ag.mgSkill == 2) && (Ag.mgEnemDirec == 2 && Ag.mgEnemSkill == 2))
                        return;
                    SoundManager.Instance.Play_Effect_Sound ("punching_GoodPerfect");
                    Instantiate (mKeeperEffect, this.gameObject.transform.position, Quaternion.identity);
                }

                if (Ag.mgDirection == 5 && (Ag.mgEnemDirec == 0)) {
                    KickerPhysicsAni (new Vector3 (0, 20, 200), true, false);
                    return;
                }
                if (Ag.mgDirection == 5 && (Ag.mgEnemDirec == 1 || Ag.mgEnemDirec == 3)) {
                    KickerPhysicsAni (new Vector3 (400, 20, -200), true, false);
                    return;
                }
                if (Ag.mgDirection == 5 && (Ag.mgEnemDirec == 2 || Ag.mgEnemDirec == 4)) {
                    KickerPhysicsAni (new Vector3 (-400, 20, -200), true, false);
                    return;
                }

                if (Ag.mgSkill == 3) return;
                if ((Ag.mgDirection == 4 && Ag.mgSkill == 2) && (Ag.mgEnemDirec == 2 && Ag.mgEnemSkill == 2))
                    return;

                if (Ag.mgDirection == 2 || Ag.mgDirection == 4) {
                    KickerPhysicsAni (new Vector3 (-400, 20, -200), true, false);
                    return;
                }
                if (Ag.mgDirection == 1 || Ag.mgDirection == 3) {
                    KickerPhysicsAni (new Vector3 (400, 20, -200), true, false);
                    return;
                }



            } else {

                // when Goal is Success Show Eff Camera
                if (Ag.mgDidWin) {
                    ;//mMainConView.GetComponent<GameScene> ().mCameraDefn.animation.Play ("camera2");
                }

                if (Ag.mgSkill == 1) {
                    SoundManager.Instance.Play_Effect_Sound ("punching_GoodPerfect");
                    Instantiate (mKeeperEffectGen, this.gameObject.transform.position, Quaternion.identity);
                }
                if (Ag.mgSkill == 2) {
                    if ((Ag.mgDirection == 2 && Ag.mgSkill == 2) && (Ag.mgEnemDirec == 4 && Ag.mgEnemSkill == 2))
                        return;
                    SoundManager.Instance.Play_Effect_Sound ("punching_GoodPerfect");
                    Instantiate (mKeeperEffect, this.gameObject.transform.position, Quaternion.identity);
                }

                if (Ag.mgEnemDirec == 5 && (Ag.mgDirection == 0)) {
                    KickerPhysicsAni (new Vector3 (0, 20, 200), true, false);
                    return;
                }
                if (Ag.mgEnemDirec == 5 && (Ag.mgDirection == 1 || Ag.mgDirection == 3)) {
                    KickerPhysicsAni (new Vector3 (400, 20, -200), true, false);
                    return;
                }
                if (Ag.mgEnemDirec == 5 && (Ag.mgDirection == 2 || Ag.mgDirection == 4)) {
                    KickerPhysicsAni (new Vector3 (-400, 20, -200), true, false);
                    return;
                }

                if (Ag.mgEnemSkill == 3) return;
                if ((Ag.mgDirection == 2 && Ag.mgSkill == 2) && (Ag.mgEnemDirec == 4 && Ag.mgEnemSkill == 2))
                    return;
            
                if (Ag.mgDirection == 2 || Ag.mgDirection == 4) {
                    KickerPhysicsAni (new Vector3 (-400, 20, -200), true, false);
                    return;
                }
                if (Ag.mgDirection == 1 || Ag.mgDirection == 3) {
                    KickerPhysicsAni (new Vector3 (400, 20, -200), true, false);
                    return;
                }

            }
        }

        //-------------------------------------------------------------------------------------------------- 골대 그물망 애니메이션
        if (this.gameObject.name == "fencetriggerL" || this.gameObject.name == "fencetriggerR") { 
			if (Ag.mgIsKick) {
				if (Ag.mgDidWin) {
                    ;//mMainConView.GetComponent<GameScene> ().mCameraKick.animation.Play ("camera1");
				}
				if (Ag.mgDirection == 1) 
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_LT");
				if (Ag.mgDirection == 2)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_RT");
				if (Ag.mgDirection == 3)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_LB");
				if (Ag.mgDirection == 4)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_RB");
				if (Ag.mgDirection == 5)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_C");
			} else {
				if (Ag.mgEnemDirec == 1) 
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_LT");
				if (Ag.mgEnemDirec == 2)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_RT");
				if (Ag.mgEnemDirec == 3)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_LB");
				if (Ag.mgEnemDirec == 4)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_RB");
				if (Ag.mgEnemDirec == 5)
                    GameObject.Find ("MainControllView").GetComponent<GameScene> ().dicGameSceneMenuList ["GoalNet"].animation.Play ("goalnetAni_C");
			}


            GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall.animation.Stop ();
            GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall.rigidbody.AddForce (0, 20, 20);
            GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall.rigidbody.useGravity = true;
            GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall.collider.isTrigger = false;
            //goalfence.animation.Play (this.gameObject.name == "fencetriggerL" ? "goalleftdown" : "goal_Rightdown");
            SoundManager.Instance.Play_Effect_Sound ("Goal in_1");
        }
        //-------------------------------------------------------------------------------------------------- 슛할공 보이게하기

        if (this.gameObject.name == "deleveryBall") { 
            mBall = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mBall;// Ball
            ShootBall = GameObject.Find ("MainControllView").GetComponent<GameScene> ().mKickBall;
            if (!AgStt.mgGameTutorial && mBall != null && ShootBall != null) {
                mBall.transform.renderer.enabled = false;
                ShootBall.transform.renderer.enabled = true;
            }
        }
        //-------------------------------------------------------------------------------------------------- 슛할때 효과
        if (Application.loadedLevelName == "GameScene" && mGamematch320.GetComponent<GameScene> ().mStateArr.GetCurStateName () == "AnimaPlay" && this.gameObject.name == "KickTrigger") {
            if (Ag.mKickEffSound) {
                if (Ag.mgIsKick) {
                    if (Ag.mgSkill == 1 || Ag.mgSkill == 0) {
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Good");
                        Instantiate (Explode03, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                    } 
                    if (Ag.mgSkill == 2) {
                        Instantiate (Explode03, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Good");

                    }
                    if (Ag.mgSkill == 3) {
                        Instantiate (Explode_02, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Perfect");
                    }

                } else {
                    if (Ag.mgEnemSkill == 1 || Ag.mgEnemSkill == 0) {
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Good");
                        Instantiate (Explode03, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                    } 
                    if (Ag.mgEnemSkill == 2) {
                        Instantiate (Explode03, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Good");

                    }
                    if (Ag.mgEnemSkill == 3) {
                        Instantiate (Explode_02, new Vector3 (0.2397667f, 0.1346343f, -34.64585f), Quaternion.identity);
                        SoundManager.Instance.Play_Effect_Sound ("Shoot_Perfect");
                    }
                }
            }
        }
    }
}
