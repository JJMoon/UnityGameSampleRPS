using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    /*
    dicGameSceneMenuList.Add ("Anim_back_red",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin/back01_red",true));
    dicGameSceneMenuList.Add ("Anim_back_blue",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin/back02_blue",true));
    dicGameSceneMenuList.Add ("Anim_back_green",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin/back03_green",true));
    dicGameSceneMenuList.Add ("Anim_eff01_red",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin2/eff01_red",true));
    dicGameSceneMenuList.Add ("Anim_eff02_blue",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin2/eff02_blue",true));
    dicGameSceneMenuList.Add ("Anim_eff03_green",mRscrcMan.FindChild(mIngameObj,"Panel_item/backgrid_amin2/eff03_green",true));
    /
    */
    void Setitem (string id, bool setactive)
    {
        dicGameSceneMenuList [id].SetActive (setactive);
    }

    string mItemTypeId;

    void ItemUse ()
    {
        WasItemUse aObj = new WasItemUse () { User = Ag.mySelf, itemType = "DRINK", itemTypeId = mItemTypeId };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                ItemInfo ();
                return;
            }
            Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
        };
    }

    /// <summary>
    /// 레드 아이템 로직
    /// </summary>
    void RedItemLogic ()
    {
        if (mSlowEff || Ag.mRedItemFlag) {
            mEventDirspeed = 1f;
            mEventSkillSpeed = 1f;
            StartCoroutine (ItemeffOn ("backeffect_red"));
            if (!Ag.mRedItemFlag) {
                mItemflag1 = mSlowEff = Ag.mBallEventAlready = false;
                Setitem ("Anim_back_red", mSlowEff);
                Setitem ("Anim_eff01_red", mSlowEff);
            }
        }
    }

    public void ItemInfo ()
    {
        WasItemInfo aObj = new WasItemInfo () { User = Ag.mySelf };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                dicGameSceneMenuList ["btn_drink_blue"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("BlueDrink").ToString ();
                dicGameSceneMenuList ["btn_drink_green"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("GreenDrink").ToString ();
                dicGameSceneMenuList ["btn_drink_red"].transform.FindChild ("Label_count").GetComponent<UILabel> ().text = CombiItemListEa ("RedDrink").ToString ();
                return;
            }
        };
    }

    void MessageInfo ()
    {
        
        if (Ag.NodeObj.EnemyUser.GetEndMessage ("EndMessage01") && Ag.mgDidWin == false) {
            dicGameSceneMenuList ["Panel_provokebox"].SetActive (true);
            mRscrcMan.FindChild (dicGameSceneMenuList ["Panel_provokebox"], "provokebox_you/Label_message", true).GetComponent<UILabel> ().text = (MessageItem (Ag.NodeObj.EnemyUser, "EndMessage01") == "null" || MessageItem (Ag.NodeObj.EnemyUser, "EndMessage01").Length < 1) ? WWW.UnEscapeURL ("%EC%88%98%EA%B3%A0%ED%95%98%EC%85%A8%EC%8A%B5%EB%8B%88%EB%8B%A4.") : WWW.UnEscapeURL (MessageItem (Ag.NodeObj.EnemyUser, "EndMessage01").RemoveQuotationMark ());
        } 
        if (Ag.NodeObj.MyUser.GetEndMessage ("EndMessage01") && Ag.mgDidWin == true) {
            dicGameSceneMenuList ["Panel_provokebox"].SetActive (true);
            Debug.Log (MessageItem (Ag.mySelf, "EndMessage01") + "::   MessageInfo ");
            mRscrcMan.FindChild (dicGameSceneMenuList ["Panel_provokebox"], "provokebox_you/Label_message", true).GetComponent<UILabel> ().text = (MessageItem (Ag.mySelf, "EndMessage01") == "null" || MessageItem (Ag.mySelf, "EndMessage01").Length < 1) ? WWW.UnEscapeURL ("%EC%88%98%EA%B3%A0%ED%95%98%EC%85%A8%EC%8A%B5%EB%8B%88%EB%8B%A4.") : WWW.UnEscapeURL (MessageItem (Ag.mySelf, "EndMessage01").RemoveQuotationMark ());
        }
    }

    string MessageItem (AmUser muser, string id)
    {
        string MessageItem = "";
        for (int i = 0; i < muser.arrItem.Count; i++) {
            if (muser.arrItem [i].WAS.itemTypeID == id) {
                MessageItem = muser.arrItem [i].WAS.msg;
            } 
        }
        return MessageItem;
    }

    void ItemEffInit ()
    {
        dicGameSceneMenuList ["backeffect_green"].SetActive (false);
        dicGameSceneMenuList ["backeffect_red"].SetActive (false);
        dicGameSceneMenuList ["backeffect_blue"].SetActive (false);

    }

    IEnumerator ItemeffOn (string pObjName)
    {
        dicGameSceneMenuList [pObjName].SetActive (true);
        yield return new WaitForSeconds (4f);
        dicGameSceneMenuList [pObjName].SetActive (false);
    }
}
