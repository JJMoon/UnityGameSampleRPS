//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System;

public partial class MenuManager : AmSceneBase
{
    //public int mGloveNum;
    float Percent;
    int Contmm, Contss;
    bool VictoryTimeFlag;

    bool PhysicalBarCheck (string id)
    {
        bool PhysicalItem = false;
        for (int i = 0; i < Ag.mySelf.arrItem.Count; i++) {
            if (Ag.mySelf.arrItem [i].WAS.itemTypeID == id) {
                PhysicalItem = true;
            }
        }
        return PhysicalItem;
    }

    public void KakaoPlayCoolTime ()
    {
        //Debug.Log ("Ag.mySelf.myRank.WAS.contWinNum   " +  Ag.mySelf.myRank.WAS.contWinNum);
        Ag.mySelf.ContWinCoolTimeRemain (out Contmm, out Contss);


        if (Ag.mySelf.myRank.WAS.contWinNum > 0 && Ag.mySelf.ContWinCoolTimeRemainPercent () > 0) {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready", true); // alter
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready2", false); // default
            //BtnKickOff.SetVisible (deflt: false, alt: true);
            dicMenuList ["Label_victorytime"].SetActive (true);
            dicMenuList ["Label_victorynum"].SetActive (true);
            dicMenuList ["Progress_victory"].SetActive (true);
            dicMenuList ["Progress_victory"].transform.FindChild ("Foreground").GetComponent<UISprite> ().fillAmount = Ag.mySelf.ContWinCoolTimeRemainPercent () / 100;
            dicMenuList ["Label_victorytime"].GetComponent<UILabel> ().text = Contmm.ToFixedWidth (2) + ":" + Contss.ToFixedWidth (2);
            dicMenuList ["Label_victorynum"].GetComponent<UILabel> ().text = (Ag.mySelf.myRank.WAS.contWinNum + 1).ToString () + WWW.UnEscapeURL ("%EC%97%B0%EC%8A%B9%20%EB%8F%84%EC%A0%84");
        } else {
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready", false); // alter
            mRscrcMan.FindChild (dicMenuList ["Ui_kickoff"], "Panel_bottom/bundle_rightbtn/btn1_ready2", true); // default
            //BtnKickOff.SetVisible (deflt: true, alt: false);
            dicMenuList ["Label_victorytime"].SetActive (false);
            dicMenuList ["Label_victorynum"].SetActive (false);
            dicMenuList ["Progress_victory"].SetActive (false);
        }
		


//        if (Ag.mySelf.FreeCouponLimitDT > System.DateTime.Now) {
//            //dicMenuList ["heart_freebundle_eaHeart"].GetComponent<UILabel> ().text = "x" + KakaoGameUserInfo.Instance.heart.ToString ();
//            Hours = (int)(Ag.mySelf.FreeCouponLimitDT - DateTime.Now).TotalHours;
//            Minitues = (Ag.mySelf.FreeCouponLimitDT - DateTime.Now).Minutes;
//            Second = (Ag.mySelf.FreeCouponLimitDT - DateTime.Now).Seconds;
            
        if (Ag.mySelf.IsFreeCouponRemain) {
            //dicMenuList ["heart_freebundle_eaHeart"].GetComponent<UILabel> ().text = "x" + KakaoGameUserInfo.Instance.heart.ToString ();
            TimeSpan remainTime = Ag.mySelf.FreeCouponTS;
            Hours = (int)remainTime.TotalHours;
            Minitues = remainTime.Minutes;
            Second = remainTime.Seconds;

            mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().text = Hours.ToString () + ":" + Minitues.ToFixedWidth (2) + ":" + Second.ToFixedWidth (2);
            mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action", false);
            mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action2", false);

            //  Debug.Log (Ag.mySelf.HeartPercent () + "Percent :: " + Percent + "Hours " + Hours + "Minitues " + Minitues   + "Second " + Second);
            //  Debug.Log (Ag.mySelf.HeartPercent () + "Percent :: " + Percent + "Hours " + Hours.ToFixedWidth(2) + "Minitues " + Minitues.ToFixedWidth(2)  + "Second " + Second.ToFixedWidth(2));
			
        } else {
			
            int mm, ss, mm2, ss2;
            Ag.mySelf.HeartPercent (out mm, out ss);
            float percentHrt = Ag.mySelf.HeartPercent ();
            string pct = " %", divider = ".", FontColor = "";
            if (percentHrt < 0) {
                Ag.mySelf.HeartCoolTime (out mm, out ss);
                //FontColor = "[#FF0000]";
                pct = " ";
                divider = " : ";
            }
            mm = Math.Abs (mm);
            ss = Math.Abs (ss);
			
            Percent = Ag.mySelf.HeartCoolTime (); // % 
            //            Debug.Log (ss + "HeartPercent");
			



			
            dicMenuList ["heart_freebundle_Label_refreshtime"].GetComponent<UILabel> ().text = "H  " + Percent + " %, S " + ss;
            mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Foreground", true).GetComponent<UISprite> ().fillAmount = (1000 - ((1000 - Percent) * 0.8f)) / 1000;

            if (PhysicalBarCheck ("HeartSpeedUp")) {
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action2", mm < 100 ? true : false);
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action", false);
            } else {
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action", mm < 100 ? true : false);
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/heart_action2", false);
            }

			
            if (percentHrt < 0) {
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().color = Color.red;
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().text = mm.ToFixedWidth (2) + divider + ss.ToFixedWidth (2) + pct;
                dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_1").gameObject.SetActive (false);
                /*
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().text = 

                    WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5") + "[FF0000]" + Ag.mySelf.HeartCoolTimeSec ().ToString () + "[FFFFFF]" + "/" +
                (PhysicalBarCheck ("HeartLimitUp") ? 
                    AgStt.CTHeartMaxDoubled.ToString () : 
                        AgStt.CTHeartMaxSeconds.ToString ());//mm.ToFixedWidth (2) + divider + ss.ToFixedWidth (2) + pct;
                        */
            } else {
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().color = Color.white;
                mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().text = 
                    WWW.UnEscapeURL ("%EC%B2%B4%EB%A0%A5") + Ag.mySelf.HeartCoolTimeSec ().ToString () + "/" +
                (PhysicalBarCheck ("HeartLimitUp") ? AgStt.CTHeartMaxDoubled.ToString () : AgStt.CTHeartMaxSeconds.ToString ());//mm.ToFixedWidth (2) + divider + ss.ToFixedWidth (2) + pct;
                if (percentHrt > 99.9) {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_1").gameObject.SetActive (true);
                } else {
                    dicMenuList ["LPanel_shop_table_glove"].transform.FindChild ("table_disable/btn_1").gameObject.SetActive (false);
                }
                //mRscrcMan.FindChild (dicMenuList ["Panel_top"], "heart_freebundle/heart_progress/Progress Bar/Label_refreshtime", true).GetComponent<UILabel> ().text = mm + divider + ss + pct;
            }
            //            Debug.Log (Percent + "Percent");
            //if (GUI.Button (myGUI.DivideRect (rect006, colEA, colN++), "H  " + mm + ":" + ss)) {

            //Debug.Log ("Percent :: " + Percent + "HeartCoolTime     " + mm +"    " + ss);
        }
		
    }
    //Kakao Pic
    IEnumerator startPic (string url)
    {
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;
			
            //mMaterial.mainTexture = www.texture;
            if (url == "")
                ;
            else {
                dicMenuList ["Lobby_KakaoFace"].GetComponent<UITexture> ().material.mainTexture = www.texture;
            }
        } else {
            ;
        }
    }

    IEnumerator startPic (string url, int Index)
    {
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;
            Texture2D mtex;
            //mMaterial.mainTexture = www.texture;
            if (url == "") {
                //Debug.Log ("ProfileUrl");
                mtex = (Texture2D)Resources.Load ("userface_bundle");
                dicTex.Add (Index, mtex);
            } else {
                if (www.error != null) {
                    //Debug.Log ("WWWError");
                    //Gobj.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
					
                }
                try {
                    //Debug.Log ("Success");
                    mtex = www.texture;
                    dicTex.Add (Index, mtex);
                } catch {
                    //Debug.Log ("Error");
                }
            }
        } else {
            //Debug.Log ("null");
        }
    }

    IEnumerator SetFriendsProfile ()
    {
        for (int i = 0; i < KakaoFriends.Instance.friends.Count; i++) {
            StartCoroutine (Noappfriend (KakaoFriends.Instance.friends [i].profileImageUrl, i));
            yield return new WaitForSeconds (0.1f);
        }
    }

    IEnumerator startPic2 (string url, string Index)
    {
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;
            Texture2D mtex;
            //mMaterial.mainTexture = www.texture;
            if (url == "") {
                //Debug.Log ("ProfileUrl");
                mtex = (Texture2D)Resources.Load ("userface_bundle");
                dicTexAppfriendMydata [Index] = mtex;
                //dicTexAppfriendMydata.Add (Index, mtex);
            } else {
                if (www.error != null) {
                    //Debug.Log ("WWWError");
                    //Gobj.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
					
                }
                try {
                    //Debug.Log ("Success");
                    mtex = www.texture;
                    dicTexAppfriendMydata [Index] = mtex;
                    //dicTexAppfriendMydata.Add (Index, mtex);
                } catch {
                    //Debug.Log ("Error");
                }
            }
        } else {
            //Debug.Log ("null");
        }
    }

    IEnumerator Noappfriend (string url, int Index)
    {
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;
            Texture2D mtex;
            //mMaterial.mainTexture = www.texture;
            if (url == "") {
                //Debug.Log ("ProfileUrl");
                mtex = (Texture2D)Resources.Load ("userface_bundle");
                dicNoAppfriend.Add (Index, mtex);
            } else {
                if (www.error != null) {
                    //Debug.Log ("WWWError");
                    //Gobj.GetComponent<UITexture> ().material.mainTexture = (Texture2D)Resources.Load ("userface_bundle");
					
                }
                try {
                    //Debug.Log ("Success");
                    mtex = www.texture;
                    dicNoAppfriend.Add (Index, mtex);
                } catch {
                    //Debug.Log ("Error");
                }
            }
        } else {
            //Debug.Log ("null");
        }
    }
    //List<new Dictionary(int Num, Texture2D Tex)> arrTex = new List<new Dictionary(int Num, Texture2D Tex)>();
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

    /// <summary>
    /// ImageBanner Load
    /// </summary>
    /// <returns>The image banner load.</returns>
	
    public IEnumerator JoycityImageBannerLoad (string url, GameObject Gobj)
    {
		
        dicMenuList ["CenterCircle"].SetActive (true);
        if (url != null) {
            WWW www = new WWW (url);
            yield return www;
            if (url == "") {
                //Debug.Log ("");
            } else
                dicMenuList ["CenterCircle"].SetActive (false);
            Gobj.GetComponent<UITexture> ().material.mainTexture = www.texture;
        } else {
            //Debug.Log ("null");
        }
    }
}
