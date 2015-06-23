using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    void PreGameVoicePlay () {
        int rannum = AgUtil.RandomInclude (1, 100);
        if (Ag.mgIsKick) {
            if (rannum % 6 == 1)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals01");
            if (rannum % 6 == 2)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals03");
            if (rannum % 6 == 3)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals04");
            if (rannum % 6 == 4)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals01");
            if (rannum % 6 == 5)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals03");
            if (rannum % 6 == 0)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KickerGoals04");

        } else {
            if (rannum % 6 == 1)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves01");
            if (rannum % 6 == 2)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves04");
            if (rannum % 6 == 3)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperSaves07");
            if (rannum % 6 == 4)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses01");
            if (rannum % 6 == 5)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses03");
            if (rannum % 6 == 0)
                VoiceSoundManager.Instance.Play_Effect_Sound ("voice/KeeperMisses05");

        }

    }

    void EnemyKeeperInfo ()
    {   float Dirleft, DirRight;
        Dirleft = EnemCard.WAS.direction [0];
        DirRight = EnemCard.WAS.direction [1];

        dicGameSceneMenuList ["direction_left"].transform.localScale = new Vector3 (((Dirleft/100)) * 200, 28, 1);
        dicGameSceneMenuList ["direction_right"].transform.localScale = new Vector3 (((DirRight/100)) * 200, 28, 1);
        dicGameSceneMenuList ["Keeper_Label_backnumber"].GetComponent<UILabel> ().text = EnemCard.WAS.backNum.ToString ();
        dicGameSceneMenuList ["Keeper_Label_playername"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (EnemCard.WAS.playerName);

        KeeperGrade (EnemCard.WAS.grade);

		Cardinfomation("Kicker_Label_enchant1", "Kicker_Label_enchant2");

    }
	/// <summary>
	/// Show Card Level
	/// </summary>
	/// <param name="enchant1">Enchant1.</param>
	/// <param name="enchant2">Enchant2.</param>
	void Cardinfomation (string enchant1, string enchant2) {
		if (EnemCard.WAS.grade == "S" || (EnemCard.WAS.grade == "A" && EnemCard.WAS.level >= 6)) {
			dicGameSceneMenuList [enchant1].SetActive(false);
			dicGameSceneMenuList [enchant2].SetActive(true);
			dicGameSceneMenuList [enchant2].transform.FindChild ("Label_overall").GetComponent<UILabel>().text = "+"+EnemCard.WAS.level.ToString();
		} else {
			dicGameSceneMenuList [enchant1].SetActive(true);
			dicGameSceneMenuList [enchant2].SetActive(false);
			dicGameSceneMenuList [enchant1].transform.FindChild ("Label_overall").GetComponent<UILabel>().text = "+"+EnemCard.WAS.level.ToString();
		}
	}

	IEnumerator CoruEnemyKickerinfo () {

        yield return new WaitForSeconds (5f); 
        dicGameSceneMenuList["Kickerinfo_progress_scouter"].SetActive(false);
        FindMyChild (dicGameSceneMenuList ["Panel_item"], "btn_scouter",false);
		yield return new WaitForSeconds (7f); 
		
		
	}

    void EnemyKickerinfo ()
    {
		StartCoroutine (CoruEnemyKickerinfo());
		Cardinfomation("Kicker_Label_enchant1", "Kicker_Label_enchant2");
		dicGameSceneMenuList ["Kicker_Label_backnumber"].GetComponent<UILabel> ().text = EnemCard.WAS.backNum.ToString ();
        dicGameSceneMenuList ["Kicker_Label_playername"].GetComponent<UILabel> ().text = WWW.UnEscapeURL (EnemCard.WAS.playerName);
        KickerGrade (EnemCard.WAS.grade);
    }

    void KeeperGrade (string pStr)
    {
        dicGameSceneMenuList.SetActiveAll (false, new string [] { "Keeper_Amateur", "Keeper_Legend", "Keeper_professional", "Keeper_Semipro", "Keeper_Student" });

        switch (pStr) {
        case "S":
            dicGameSceneMenuList ["Keeper_Legend"].SetActive (true);
            break;
        case "A":
            dicGameSceneMenuList ["Keeper_professional"].SetActive (true);
            break;
        case "B":
            dicGameSceneMenuList ["Keeper_Semipro"].SetActive (true);
            break;
        case "C":
            dicGameSceneMenuList ["Keeper_Amateur"].SetActive (true);
            break;
        case "D":
            dicGameSceneMenuList ["Keeper_Student"].SetActive (true);

            break;
        }
    }

    void KickerGrade (string pStr)
    {
        dicGameSceneMenuList.SetActiveAll (false, new string [] { "Kicker_Amateur", "Kicker_Legend", "Kicker_professional", "Kicker_Semipro", "Kicker_Student" });

        switch (pStr) {
        case "S":
            dicGameSceneMenuList ["Kicker_Legend"].SetActive (true);
            break;
        case "A":
            dicGameSceneMenuList ["Kicker_professional"].SetActive (true);
            break;
        case "B":
            dicGameSceneMenuList ["Kicker_Semipro"].SetActive (true);
            break;
        case "C":
            dicGameSceneMenuList ["Kicker_Amateur"].SetActive (true);
            break;
        case "D":
            dicGameSceneMenuList ["Kicker_Student"].SetActive (true);
            break;
        }
    }

    Texture2D mTex;

    IEnumerator CaptureImage ()
    {

        Camera owner = mRscrcMan.FindGameObject ("RenderCamera", true).gameObject.GetComponent< Camera > ();
        mRscrcMan.FindGameObject ("RenderCamera/Name", true).gameObject.GetComponent<TextMesh> ().text = WWW.UnEscapeURL (myCard.WAS.playerName);
        //mRscrcMan.FindGameObject ("RenderCamera/Name", true).gameObject.GetComponent<TextMesh> ().color = Tbl.dicDeckBacRgbCode [myCard.WAS.country];
        mRscrcMan.FindGameObject ("RenderCamera/Num", true).gameObject.GetComponent<TextMesh> ().text = myCard.WAS.backNum.ToString ();
        //mRscrcMan.FindGameObject ("RenderCamera/Num", true).gameObject.GetComponent<TextMesh> ().color = Tbl.dicDeckBacRgbCode [myCard.WAS.country];

        RenderTexture target = owner.targetTexture;

        // wait for end of current frame
        yield return new WaitForEndOfFrame ();
        //Debug.Log ("Target.Widht" + target.width + "Target.Height" + target.height);
        // create texture to hold image
        mTex = new Texture2D (target.width, target.height, TextureFormat.ARGB32, false);
        RenderTexture.active = owner.targetTexture;
        owner.Render ();
        // capture the image
        mTex.ReadPixels (new Rect (0, 0, target.width, target.height), 0, 0, false);
        RenderTexture.active = null;
        mTex.Apply ();
        if (Ag.mgIsKick)
        mRscrcMan.FindChild (mPlayerKicker, "Clothes", true).gameObject.transform.renderer.sharedMaterials [0].SetTexture ("_DecalTex", mTex);
        else
            mRscrcMan.FindChild (mPlayerKeeper, "uniform", true).gameObject.transform.renderer.sharedMaterials [0].SetTexture ("_DecalTex", mTex);

    }

}
