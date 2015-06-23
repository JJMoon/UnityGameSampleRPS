//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
// [2013:12:3:MOON<Start>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene : AmSceneBase
{
    bool mRematchRefuse;

    int TurnNum { get { return Ag.NodeObj.TurnNum; } set { Ag.NodeObj.TurnNum = value; } }

    void AddAdditionalActions ()
    {
        mStateArr.AddAdditionalActionOf ("Begin", "Entry", () => { // "During":  "Exit":
            TurnNum = 0;  //Ag.NodeObj.TurnNum = mTurnNum;
            Ag.LogIntenseWord ("  Begin :: Entry   >>>   TurnNumber " + TurnNum);
        });

        mStateArr.AddAdditionalActionOf ("AftPaus", "Entry", () => { // "During":  "Exit":
            Ag.LogString ("  AftPaus :: Entry   >>>  TurnNum++ ::   " + TurnNum);
            TurnNum++;
            Ag.LogString ("  AftPaus :: Entry   >>>  TurnNum ::   " + TurnNum);
        });

        mStateArr.AddAdditionalExitConditionTo ("PackWait", () => { // "During":  "Exit":
            if (Ag.mSingleMode)
                return true;
            else {
                if (Ag.NodeObj.MySocket.arrGameRcvd.Count < TurnNum || Ag.NodeObj.MySocket.arrGameSend.Count < TurnNum) { // 갯수가 모자람
                    Ag.LogIntenseWord ("  Retry : Turn >  " + TurnNum + " < :: Rcvd.Cnt : " + Ag.NodeObj.MySocket.arrGameRcvd.Count + "  Send.Cnt : " + Ag.NodeObj.MySocket.arrGameSend.Count);
                    Ag.LogString (" Retry >>>>>>>>> :: " + mRetryCount);
                    RemovePrevTurnObjects (); // 기존에 받은 객체를 지우고 새출발
                    mStateArr.SetStateWithNameOf ("NetWait");
                    return false;
                }
                if (!(((NodeGameTurnRslt)Ag.NodeObj.MySocket.arrGameSend [TurnNum - 1]).turnNum == TurnNum && //
                    ((NodeGameTurnRslt)Ag.NodeObj.MySocket.arrGameRcvd [TurnNum - 1]).turnNum == TurnNum)) {
                    Ag.LogIntenseWord ("  Retry : Turn >  " + TurnNum + " < :: Send Turn : " + ((NodeGameTurnRslt)Ag.NodeObj.MySocket.arrGameSend [TurnNum - 1]).turnNum +
                    "  Rcvd Turn : " + ((NodeGameTurnRslt)Ag.NodeObj.MySocket.arrGameRcvd [TurnNum - 1]).turnNum);
                    Ag.LogString (" Retry >>>>>>>>> :: " + mRetryCount);
                    //RemovePrevTurnObjects(); // 기존에 받은 객체를 지우고 새출발
                    RemoveCurrentObject ();
                    //mStateArr.SetStateWithNameOf ("NetWait");
                    return false;
                }

                Ag.LogString ("  OK... Next Turn     Condition :::   " + Ag.NodeObj.MySocket.arrGameSend.Count + " ,   " + Ag.NodeObj.MySocket.arrGameRcvd.Count + " , TurnNum : " + TurnNum);
                return (Ag.NodeObj.MySocket.arrGameSend.Count >= TurnNum && TurnNum <= Ag.NodeObj.MySocket.arrGameRcvd.Count); // OK..
            }
        });
    }

    void Rematch () // I send Rematch
    {
        mGameScoreeff = false;
        ISentRematchAndWait ();
//        dicGameSceneMenuList ["popup"].SetActive (true);
//        dicGameSceneMenuList ["rematch"].SetActive (true);
        StartCoroutine (RematchWait (9));
        mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
        Ag.NodeObj.Rematch ();
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  UI  _____  Method  _____
    void CloseRematchPopups ()
    {
        dicGameSceneMenuList.SetActiveAll (false, new string[] {
            "popup",
            "rematch",
            "rematch_accept",
            "rematch_refuse"
        });
    }

    void ISentRematchAndWait ()
    {
        dicGameSceneMenuList ["popup"].SetActive (true);
        dicGameSceneMenuList ["rematch"].SetActive (true);
    }

    void EnemySentRematchAndWait ()
    {
        dicGameSceneMenuList ["popup"].SetActive (true);
        dicGameSceneMenuList ["rematch_accept"].SetActive (true);
    }

    void EnemSentRematchAndINoReplyTimeOut ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["rematch_accept"].SetActive (false);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);

        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9E%AC%EA%B2%BD%EA%B8%B0%EC%88%98%EB%9D%BD%20%EC%8B%9C%EA%B0%84%EC%B4%88%EA%B3%BC");
        // 재경기수락 시간초과
    }

    void ISentRematchAndTimeOutNoReply ()
    {
        dicGameSceneMenuList ["popup"].SetActive (true);
        dicGameSceneMenuList ["rematch"].SetActive (false);
        dicGameSceneMenuList ["rematch_not"].SetActive (true);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);
        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EC%9D%91%EB%8B%B5%ED%95%98%EC%A7%80%20%EC%95%8A%EC%9D%8C");
        // 상대가 응답하지 않음
    }

    void EnemyRefusedRematching ()
    {
        dicGameSceneMenuList ["popup"].SetActive (true);
        dicGameSceneMenuList ["rematch_refuse"].SetActive (true);
        dicGameSceneMenuList ["rematch"].SetActive (false);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);
        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EA%B1%B0%EC%A0%88%ED%95%98%EC%98%80%EC%9D%8C");
        // ㅅㅏㅇㄷㅐㄱㅏ ㄱㅓㅈㅓㄹㅎㅏㅁ
    }

    void ISentRefuse ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["rematch_accept"].SetActive (false);
        mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);
        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%9D%B4%EB%AF%B8%20%EA%B1%B0%EC%A0%88%ED%95%98%EC%98%80%EC%9D%8C");

    }

    void IAcceptTheRematchFromEnemy ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["rematch_accept"].SetActive (false);
    }

    void EnemRefusedAndGotoResultView ()
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["rematch_refuse"].SetActive (false);
        dicGameSceneMenuList ["btn_Label"].SetActive (true);
        mRscrcMan.FindChild (dicGameSceneMenuList ["btn_Label"], "Label", true).gameObject.GetComponent<UILabel> ().text = WWW.UnEscapeURL ("%EC%83%81%EB%8C%80%EA%B0%80%20%EA%B1%B0%EC%A0%88%ED%95%98%EC%98%80%EC%9D%8C");

    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  UI  _____  Method  _____
    void GotoHome ()
    {
        mGameScoreeff = false;
        Ag.mSingleMode = false;
        Ag.NodeObj.LeaveMyself ();
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            BgmSound.Instance.Play ();
        }
        
        //Ag.NodeObj.UserModify ("ONLINE", statusOnly:false);
        //Ag.NodeObj.MySocket.ActionUser ();

        Ag.mGameStartAlready = true;
        dicGameSceneMenuList ["loading"].SetActive (true);

        WasUserInfo aObj = new WasUserInfo () { User = Ag.mySelf, flag = 1 };
        aObj.messageAction = (int pInt) => {
            switch (pInt) { // 0:성공
            case 0:
                Application.LoadLevel ("GameScenetoMenuscene");    
                break;
            case -1:
            case 4:
                return;
            }
        };


    }

    IEnumerator RematchNoAccept (float pTime) // 8
    {
        yield return new WaitForSeconds (pTime);
        if (!Ag.NodeObj.ReMatchRefuseSent.Mine) {
            EnemSentRematchAndINoReplyTimeOut ();
        }
        mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
    }

    IEnumerator RematchWait (float pTime) // 9
    { // I sent Rematch .. 
        yield return new WaitForSeconds (pTime);

        if (!Ag.NodeObj.ReMatchRefuseSent.Enem) {
            ISentRematchAndTimeOutNoReply ();
        }

//        if (Ag.mSingleMode) {
//            dicGameSceneMenuList ["popup"].SetActive (true);
//            dicGameSceneMenuList ["rematch_not"].SetActive (true);
//        }
    }
    //    void RematchCancel ()
    //    {
    //        dicGameSceneMenuList ["popup"].SetActive (false);
    //        dicGameSceneMenuList ["rematch"].SetActive (false);
    //        mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
    //    }
    void Rematch_NotResponse () // touch "" X ""
    {
        dicGameSceneMenuList ["popup"].SetActive (false);
        dicGameSceneMenuList ["rematch_not"].SetActive (false);
    }

    void RematchAceept ()
    {
        EnemUserCheck = false;
        IAcceptTheRematchFromEnemy ();
        Ag.NodeObj.Rematch ();
    }

    void rematch_refuse ()
    {
        EnemUserCheck = false;
        ISentRefuse ();
        Ag.NodeObj.SendRematchRefuse ();
    }

    void rematch_refuse_Ok ()
    {
        EnemRefusedAndGotoResultView ();
        mRscrcMan.FindChild (mResultPanel, "Panel_btn/btn_rematch", false);
        Ag.mGameStartAlready = true;
        dicGameSceneMenuList ["loading"].SetActive (false);


        Ag.NodeObj.UserModify ("ONLINE", statusOnly:true);

        //Ag.mSingleMode = false;
        //Ag.NodeObj.LeaveMyself ();
        //Ag.NodeObj.EnemyUser = null;
        /*
        if (PreviewLabs.PlayerPrefs.GetBool ("BgmSoundOff")) {
            BgmSound.Instance.Play ();
        }

        Application.LoadLevel("GameScenetoMenuscene");
        */
    }
    /*
    mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicGameSceneMenuList["popup"],"rematch/btn_cancle",true), dicGameSceneMenuList ["TargetObj"], "RematchCancel");
    mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicGameSceneMenuList["popup"],"rematch_accept/btngrid/btn_refuse",true), dicGameSceneMenuList ["TargetObj"], "RematchAceept");
    mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicGameSceneMenuList["popup"],"rematch_accept/btngrid/btn_rematch",true), dicGameSceneMenuList ["TargetObj"], "rematch_refuse");
    mRscrcMan.AddComponentUISendMessage (mRscrcMan.FindChild(dicGameSceneMenuList["popup"],"rematch_refuse/btn_ok",true), dicGameSceneMenuList ["TargetObj"], "rematch_refuse_Ok");
    */
}