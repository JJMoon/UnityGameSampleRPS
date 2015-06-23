using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class PlayerCardInfo : AmSceneBase
{
    public override void Start ()
    {
        base.Start ();
        //mwas = new WasCard ();
    }

    public WasCard mwas;
    public AmCard mCard;
    public bool mIsKicker, MixCardChoice;
    public int MixOrderNum;
    public int[] arrDirection;
    public int[] arrskl;
    public int mLimitGameEa, cost, level, enchant, condition, backnum, mgrade, mSklPoint, mPolyGonNum;
    public GameObject mPoly;
    public GameObject PlayerPolygon;
    public string mplayernum, mPolygonName;
    public int mPlayerPosition;
    public uint DirNarrow, DirMiss, FireKick, Blaze, Overall;
    public uint FlashJump, LightningJump, balance;
    string mGradeName, mGradeSprite, mEalimitSprite, mCondition;

    void SendCardInfo ()
    {
        GameObject.Find ("Axis/Camera/Match").SendMessage ("CardPosChange", this.gameObject);
    }

    void SendMixCardInfo ()
    {
        GameObject.Find ("Axis/Camera/Match").SendMessage ("CardMixSelect", this.gameObject);
        //Debug.Log ("Clicked");
    }

    public void PlayerDetail ()
    {
        Ag.mySelf.SetCostumeToCard ();
        //Debug.Log (arrPlayerPolygon.Count);
        FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["LPanel_playerstatpopup"].SetActive (true);
        FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_kicker"].SetActive (false);
        FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_keeper"].SetActive (false);

        if (!PreviewLabs.PlayerPrefs.GetBool ("MenuTutorPlayerInfo")) {
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().menututor_cardinfo_Close ();
        }
        Ag.LogString ("mwas.isKicker   ::   " + mwas.isKicker   + "mCardWas  " + mCard.WAS.isKicker); 

        if (mwas.isKicker) {
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_kicker"].SetActive (true);

            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().mwas = mCard.WAS;
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().mCard = mCard;
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().KickerInfo ();
           

            if (mwas.limitGameEA < 5)
                FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_kicker"].transform.FindChild ("profile_detail/grid_btn/btn_recharter/alert").gameObject.SetActive(true);
            if (mwas.limitGameEA > 5)
                FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_kicker"].transform.FindChild ("profile_detail/grid_btn/btn_recharter/alert").gameObject.SetActive(false);


            //FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_kicker"].transform.FindChild ("KickerCard").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();
            //KickerInfo ();
        } else {
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_keeper"].SetActive (true);

            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().mwas = mCard.WAS;
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().mCard = mCard;
            FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().KeeperInfo ();

            if (mwas.limitGameEA < 5)
                FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_keeper"].transform.FindChild ("profile_detail/grid_btn/btn_recharter/alert").gameObject.SetActive(true);
            if (mwas.limitGameEA > 5)
                FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_keeper"].transform.FindChild ("profile_detail/grid_btn/btn_recharter/alert").gameObject.SetActive(false);
            //KeeperInfo ();
            //FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().dicMenuList ["Panel_keeper"].transform.FindChild ("Gk_Card").gameObject.GetComponent<PlayerCardInfo> ().CardInit ();

        }

        FindGameObject ("Axis/Camera/Match", true).GetComponent<MenuManager> ().SendWasCardupdate ();
    }

    public void OpenCardInfo ()
    {


    }

    string PPlayerName;

    public void CardInit ()
    {
        GradeName (mwas.grade);

        //SetPolyGon (mwas.info);

        mPlayerPosition = mwas.kickOrder;
        //PPlayerName = "W_N_Kicker005";
        //Debug.Log (this.gameObject);
        this.gameObject.transform.FindChild ("Sprite (card_legends)").GetComponent<UISprite> ().spriteName = mGradeSprite;
        this.gameObject.transform.FindChild ("Sprite (uniform_korea)").GetComponent<UISprite> ().spriteName = Tbl.arrDeckUniformName[mwas.country]; //  .dicDeckUniformName[mwas.country];  //dicDeckUniformName ["KOR"] = "uniform_korea";
        if (mwas.grade == "S" || (mwas.grade == "A" && mwas.level >= 6)) {
            this.gameObject.transform.FindChild ("enchant1").gameObject.SetActive (false);
            this.gameObject.transform.FindChild ("enchant2").gameObject.SetActive (true);
            this.gameObject.transform.FindChild ("enchant2/Label_overall").GetComponent<UILabel> ().text = "+" + mwas.level;
        } else {
            this.gameObject.transform.FindChild ("enchant1").gameObject.SetActive (true);
            this.gameObject.transform.FindChild ("enchant2").gameObject.SetActive (false);
            this.gameObject.transform.FindChild ("enchant1/Label_overall").GetComponent<UILabel> ().text = "+" + mwas.level;
        }

        Overall = (uint)mwas.GetAbilityDisplay (); //(uint)(mwas.skill[0]/2)-6;
        this.gameObject.transform.FindChild ("Label_playcount").GetComponent<UILabel> ().text = mwas.limitGameEA.ToString ();
        //this.gameObject.transform.FindChild ("Sprite (Kicker01)").GetComponent<UISprite> ().spriteName = PPlayerName;
        //this.gameObject.transform.FindChild ("Sprite (card_legendstime)").GetComponent<UISprite> ().spriteName = mEalimitSprite;

        this.gameObject.transform.FindChild ("Label_playernumber").gameObject.GetComponent<UILabel> ().text = Tbl.dicDeckBackNumCol[mwas.country] + mwas.backNum.ToString ();



        //Ag.LogString ("Country " + mCard.WAS.country.ToString () + "Look     " + mCard.WAS.look.ToString ()); 
        this.gameObject.transform.FindChild ("nations").gameObject.GetComponent<UISprite> ().spriteName = "flag_" + mwas.country.ToString ();


        //this.gameObject.transform.FindChild ("face").GetComponent<UISprite> ().spriteName = mwas.info;
        foreach (Transform child in this.gameObject.transform.FindChild("condition").transform) {
            if (child.name == ConditionName (mwas.condition)) {
                child.gameObject.SetActive (true);
            } else
                child.gameObject.SetActive (false);
        }
        mPlayerPosition = mwas.kickOrder;
        try {
            if (mwas.leagueType == "K") {
                this.gameObject.transform.FindChild ("kleague").gameObject.SetActive (true);
            }
            if (mwas.leagueType == "N") {
                this.gameObject.transform.FindChild ("kleague").gameObject.SetActive (false);
            }
        } catch {
        }

        this.gameObject.transform.FindChild ("Label_playername").GetComponent<UILabel> ().text = Tbl.dicDeckBackNumCol[mwas.country] + UnityEngine.WWW.UnEscapeURL (mwas.playerName);
        try {
            if (mwas.isKicker)
                this.gameObject.transform.FindChild ("Sprite (img_cardgk)").gameObject.SetActive (false);
            else
                this.gameObject.transform.FindChild ("Sprite (img_cardgk)").gameObject.SetActive (true);
        } catch {
        }

        try {
            if (mwas.limitGameEA < 5)
                this.gameObject.transform.FindChild ("cardalert").gameObject.SetActive (true);
            if (mwas.limitGameEA > 5)
                this.gameObject.transform.FindChild ("cardalert").gameObject.SetActive (false);



        } catch {
            Debug.Log ("Flag_Error"  +  mwas.country);
        }

    }

    void DestoryObject ()
    {
        for (int i = 0; i < Ag.arrPlayerPolygon.Count; i++) {
            DestroyObject (Ag.arrPlayerPolygon [i]);
        }
    }
    /*
    void SetStringToLabel (string pLabel, string pTxt)
    {
        gameObject gObj = mRscrcMan.FindGameObject ("Axis/Camera/Match", true);
        if (gObj == null)
            Ag.LogIntenseWord (" ERROR ");
        else
            gObj.GetComponent<MenuManager> ().dicMenuList [pLabel].GetComponent<UILabel> ().text = pTxt;
    }
    */
    void SetGradeWith (string name, string sprite, string eaLimit, int gradeNum)
    {
        mGradeName = name;
        mGradeSprite = sprite;
        mEalimitSprite = eaLimit;
        mgrade = gradeNum;
    }

    void GradeName (string pgrade)
    {
        switch (pgrade) {
        case "S":
            SetGradeWith ("1txt_legend", "card_legends", "card_legendstime", 1);
            break;
        case "A":
            SetGradeWith ("2txt_pro", "card_professional", "card_professionaltime", 2);

            break;
        case "B":
            SetGradeWith ("3txt_semipro", "card_semipro", "card_semiprotime", 3);
            break;
        case "C":
            SetGradeWith ("4txt_amatuer", "card_amateur", "card_amateurtime", 4);

            break;
        case "D":
            SetGradeWith ("5txt_student", "card_student", "card_studenttime", 5);

            break;
        }

    }

    string ConditionName (int pCondition)
    {

        switch (pCondition) {
        case 2:
            mCondition = "0_up";
            break;
        case 1:
            mCondition = "1_littleup";
            break;
        case 0:
            mCondition = "2_normal";
            break;
        case -1:
            mCondition = "3_littledown";
            break;
        case -2:
            mCondition = "4_edown";
            break;
        }
        return mCondition;

    }
}
