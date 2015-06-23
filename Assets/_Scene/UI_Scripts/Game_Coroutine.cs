//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    IEnumerator StartdrgQuitBtn (float time)
    {
        yield return new WaitForSeconds (time);
        mDragExitBtn.animation ["DragBtn"].speed = -1;
        mDragExitBtn.animation ["DragBtn"].time = mDragExitBtn.animation ["DragBtn"].length;
        mDragExitBtn.animation.Play ("DragBtn");
    }

    IEnumerator GameScoreEffFlag ()
    {
        yield return new WaitForSeconds (2f);
        mGameScoreeff = true;
    }

    IEnumerator startPic (string url, GameObject Gobj)
    {
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;

            //mMaterial.mainTexture = www.texture;
            if (url == "")
                Gobj.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
            else {
                Gobj.GetComponent<UITexture> ().material.mainTexture = www.texture;
            }
        } else {
            //Debug.Log ("");
            Gobj.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
        }
    }

    IEnumerator Skleff (float waitTime, int pnum)
    {
        //Debug.Log ("SkillActive" + pnum);
        mSklName.SetActive (true);
        GameSkillstring (pnum);
        yield return new WaitForSeconds (waitTime);
        GameSklInit ();
        mSklName.SetActive (false);
    }
}