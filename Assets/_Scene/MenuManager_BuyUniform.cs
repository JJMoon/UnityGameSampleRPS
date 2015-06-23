using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase
{
    void BuyUniformImageInit ()
    {
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top00").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top01").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top02").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top03").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top04").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top05").gameObject.SetActive (false);

        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top00").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top01").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top02").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top03").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top04").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top05").gameObject.SetActive (false);

        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants00").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants01").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants02").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants03").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants04").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants05").gameObject.SetActive (false);

        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks00").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks01").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks02").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks03").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks04").gameObject.SetActive (false);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks05").gameObject.SetActive (false);

    }

    void UniformLabelSetting ()
    {
        BuyUniformImageInit ();
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top").gameObject.SetActive (true);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk").gameObject.SetActive (true);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants").gameObject.SetActive (true);
        dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks").gameObject.SetActive (true);

        dicMenuList ["popup_buyuniform"].SetActive (true);


        dicMenuList ["popup_buyuniform"].transform.FindChild ("Price_Label").GetComponent<UILabel> ().text = ItemPrice (uniformTypeid).ToString ();
        BuyUniformInfo (uniformTypeid);
    }

    void UniformLabelinform (string plusnum, string skill1, string skill2)
    {
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_score").GetComponent<UILabel> ().text = plusnum;
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_good").GetComponent<UILabel> ().text = skill1;
        dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_great").GetComponent<UILabel> ().text = skill2;
    }

    void UniformMainPopupSetting (string plusnum, string skill1, string skill2)
    {
        dicMenuList ["LPanel_uniform"].transform.FindChild ("bundle_itemstat/Label_score").GetComponent<UILabel> ().text = plusnum;
        dicMenuList ["LPanel_uniform"].transform.FindChild ("bundle_itemstat/Label_good").GetComponent<UILabel> ().text = skill1;
        dicMenuList ["LPanel_uniform"].transform.FindChild ("bundle_itemstat/Label_great").GetComponent<UILabel> ().text = skill2;
    }

    int UniformPoint, PantPoint, SockPoint;

    void BuyUniformInfo (string UniformTypeId)
    {

        dicMenuList ["popup_buyuniform"].transform.FindChild ("Price_Label").GetComponent<UILabel> ().text = GetRealBuyPrice (UniformTypeId).ToString ();
        switch (UniformTypeId) {
        case "KickerUniformTop1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의1";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 0% FireKick + 0% BlazeKick + 0% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");

            break;
        case "KickerUniformTop2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% FireKick + 10% BlazeKick + 5% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KickerUniformTop3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% FireKick + 10% BlazeKick + 5% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KickerUniformTop4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% FireKick + 10% BlazeKick + 5% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KickerUniformTop5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의5";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% FireKick + 10% BlazeKick + 5% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KickerUniformTop6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/top/top05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 상의6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% FireKick + 10% BlazeKick + 5% ";
            UniformLabelinform ("가산점                                  4%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KickerUniformPants1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의1";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% FireKick + 5% BlazeKick + 3% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformPants2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% FireKick + 5% BlazeKick + 3% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformPants3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% FireKick + 5% BlazeKick + 3% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformPants4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% FireKick + 5% BlazeKick + 3% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformPants5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의5";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% FireKick + 5% BlazeKick + 3% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformPants6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 하의6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 0% FireKick + 0% BlazeKick + 0% ";
            UniformLabelinform ("가산점                                  3%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KickerUniformSocks1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말1";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KickerUniformSocks2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KickerUniformSocks3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KickerUniformSocks4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KickerUniformSocks5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말5";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KickerUniformSocks6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키커 유니폼 양말6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% FireKick + 4% BlazeKick + 2% ";
            UniformLabelinform ("가산점                                  2%", "블레이즈킥                           0%", "파이어킥                              0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformTop1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의1";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformTop2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformTop3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformTop4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformTop5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의5";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformTop6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/topgk/top05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 상의6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 4% Flashjump + 10% Lightningjump + 5% ";
            UniformLabelinform ("가산점                                  4%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("4%","0%","0%");
            break;
        case "KeeperUniformPants1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의1";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% Flashjump + 5% Lightningjump + 3% ";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformPants2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% Flashjump + 5% Lightningjump + 3% ";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformPants3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% Flashjump + 5% Lightningjump + 3% ";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformPants4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% Flashjump + 5% Lightningjump + 3% ";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformPants5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의5";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformPants6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/pants/pants05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 하의6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 3% Flashjump + 5% Lightningjump + 3% ";
            UniformLabelinform ("가산점                                  3%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("3%","0%","0%");
            break;
        case "KeeperUniformSocks1":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks00").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말1";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformSocks2":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks01").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말2";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% Flashjump + 4% Lightningjump + 2% ";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformSocks3":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks02").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말3";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% Flashjump + 4% Lightningjump + 2% ";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformSocks4":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks03").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말4";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% Flashjump + 4% Lightningjump + 2% ";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformSocks5":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks04").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말5";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% Flashjump + 4% Lightningjump + 2% ";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        case "KeeperUniformSocks6":
            dicMenuList ["popup_buyuniform"].transform.FindChild ("bundle_uniform/socks/socks05").gameObject.SetActive (true);
            dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_uniformname").GetComponent<UILabel> ().text = "키퍼 유니폼 양말6";
            //dicMenuList ["popup_buyuniform"].transform.FindChild ("Label_intext").GetComponent<UILabel> ().text = "가산점 + 2% Flashjump + 4% Lightningjump + 2% ";
            UniformLabelinform ("가산점                                  2%", "라이트닝점프                        0%", "플래쉬점프                           0%");
			//UniformMainPopupSetting ("2%","0%","0%");
            break;
        }
    }

    void UniformInitUI ()
    {
        dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType2"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType3"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType4"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType5"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType1"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType2"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType3"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType4"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType5"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType6"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        
        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("buy_yet").gameObject.SetActive (true);
    }

    void UniformPriceInit ()
    {
        dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType2"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType3"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType4"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType5"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        
        dicMenuList ["Btn_Fun_Kp_ShirsType1"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType2"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType3"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType4"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType5"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType6"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        
        
        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("Label_price").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("Label_price").gameObject.SetActive (true);
    }

    void UniformPriceCoinImage ()
    {
        dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType2"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType3"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType4"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType5"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_ShirsType6"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        
        dicMenuList ["Btn_Fun_Kp_ShirsType1"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType2"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType3"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType4"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType5"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_Kp_ShirsType6"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        
        
        dicMenuList ["Btn_Fun_PantsType1"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType3"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType4"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType5"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_PantsType6"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType1"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType2"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType3"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType4"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType5"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
        dicMenuList ["Btn_Fun_SocksType6"].transform.FindChild ("icon_coin").gameObject.SetActive (true);
    }

    void UniformEventTextInit ()
    {
        for (int i = 1; i < 7; i++) {
            dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("txtevent").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("cutline").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("Label_eventprice").gameObject.SetActive (false);

            dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("txtevent").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("cutline").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("Label_eventprice").gameObject.SetActive (false);

            dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("txtevent").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("cutline").gameObject.SetActive (false);
            dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("Label_eventprice").gameObject.SetActive (false);
        }
    }

    void UniformEventCheckKicker ()
    {
        //ItemEventOnCheck("KickerUniformTop1", dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_ShirsType1"].transform.FindChild ("Label_eventprice").gameObject); 
        for (int i = 1; i < 7; i++) {
            ItemEventOnCheck ("KickerUniformTop" + i, dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_ShirsType" + i].transform.FindChild ("Label_eventprice").gameObject); 
            ItemEventOnCheck ("KickerUniformPants" + i, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("Label_eventprice").gameObject);
            ItemEventOnCheck ("KickerUniformSocks" + i, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("Label_eventprice").gameObject);
        }

    }

    void UniformEventCheckKeeper ()
    {
        //ItemEventOnCheck("KeeperUniformPants2", dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_PantsType2"].transform.FindChild ("Label_eventprice").gameObject); 

        for (int i = 1; i < 7; i++) {
            ItemEventOnCheck ("KeeperUniformTop" + i, dicMenuList ["Btn_Fun_Kp_ShirsType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_Kp_ShirsType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_Kp_ShirsType" + i].transform.FindChild ("Label_eventprice").gameObject); 
            ItemEventOnCheck ("KeeperUniformPants" + i, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_PantsType" + i].transform.FindChild ("Label_eventprice").gameObject);
            ItemEventOnCheck ("KeeperUniformSocks" + i, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("txtevent").gameObject, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("cutline").gameObject, dicMenuList ["Btn_Fun_SocksType" + i].transform.FindChild ("Label_eventprice").gameObject);
        }
    }

    void TextureBuyButtonClose (string ItemTypeId)
    { 
        UniformMainPopupSetting ((UniformPoint + PantPoint + SockPoint).ToString () + "%", "0%", "0%");

        for (int i = 0; i < Ag.mySelf.arrUniform.Count; i++) {
            if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == ItemTypeId) {
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/btn_buy", false);
                return;
                Debug.Log ("ItemTypeId" + ItemTypeId + "");
            } else {
                mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/btn_buy", true);
            }
        }
    }

    void UniformLabelSet (string pBtnid)
    {
        dicMenuList [pBtnid].transform.FindChild ("buy_yet").gameObject.SetActive (false);
        dicMenuList [pBtnid].transform.FindChild ("Label_price").gameObject.SetActive (false);
        dicMenuList [pBtnid].transform.FindChild ("icon_coin").gameObject.SetActive (false);
        dicMenuList [pBtnid].transform.FindChild ("txtevent").gameObject.SetActive (false);
        dicMenuList [pBtnid].transform.FindChild ("cutline").gameObject.SetActive (false);
        dicMenuList [pBtnid].transform.FindChild ("Label_eventprice").gameObject.SetActive (false);

    }

    void TextureSet (bool Kicker)
    {
        UniformPriceInit ();
        UniformInitUI ();
        UniformPriceCoinImage ();
        //UniformEventTextInit();

        if (Kicker) {
            //UniformEventCheckKicker ();
            for (int i = 0; i < Ag.mySelf.arrUniform.Count; i++) {
                
                switch (Ag.mySelf.arrUniform [i].WAS.itemTypeId) {
                case "KickerUniformTop1":
                    UniformLabelSet ("Btn_Fun_ShirsType1");
                    break;
                case "KickerUniformTop2":
                    UniformLabelSet ("Btn_Fun_ShirsType2");
                    break;
                case "KickerUniformTop3":
                    UniformLabelSet ("Btn_Fun_ShirsType3");
                    break;
                case "KickerUniformTop4":
                    UniformLabelSet ("Btn_Fun_ShirsType4");
                    break;
                case "KickerUniformTop5":
                    UniformLabelSet ("Btn_Fun_ShirsType5");
                    break;
                case "KickerUniformTop6":
                    UniformLabelSet ("Btn_Fun_ShirsType6");
                    break;
                case "KickerUniformPants1":
                    UniformLabelSet ("Btn_Fun_PantsType1");
                    break;
                case "KickerUniformPants2":
                    UniformLabelSet ("Btn_Fun_PantsType2");
                    break;
                case "KickerUniformPants3":
                    UniformLabelSet ("Btn_Fun_PantsType3");
                    break;
                case "KickerUniformPants4":
                    UniformLabelSet ("Btn_Fun_PantsType4");
                    break;
                case "KickerUniformPants5":
                    UniformLabelSet ("Btn_Fun_PantsType5");
                    break;
                case "KickerUniformPants6":
                    UniformLabelSet ("Btn_Fun_PantsType6");
                    break;
                case "KickerUniformSocks1":
                    UniformLabelSet ("Btn_Fun_SocksType1");
                    break;
                case "KickerUniformSocks2":
                    UniformLabelSet ("Btn_Fun_SocksType2");
                    break;
                case "KickerUniformSocks3":
                    UniformLabelSet ("Btn_Fun_SocksType3");
                    break;
                case "KickerUniformSocks4":
                    UniformLabelSet ("Btn_Fun_SocksType4");
                    break;
                case "KickerUniformSocks5":
                    UniformLabelSet ("Btn_Fun_SocksType5");
                    break;
                case "KickerUniformSocks6":
                    UniformLabelSet ("Btn_Fun_SocksType6");
                    break;
                }
            }
        } else {
            //UniformEventCheckKeeper ();
            for (int i = 0; i < Ag.mySelf.arrUniform.Count; i++) {
                switch (Ag.mySelf.arrUniform [i].WAS.itemTypeId) {
                case "KeeperUniformTop1":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType1");
                    break;
                case "KeeperUniformTop2":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType2");
                    break;
                case "KeeperUniformTop3":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType3");
                    break;
                case "KeeperUniformTop4":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType4");
                    break;
                case "KeeperUniformTop5":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType5");
                    break;
                case "KeeperUniformTop6":
                    UniformLabelSet ("Btn_Fun_Kp_ShirsType6");
                    break;
                case "KeeperUniformPants1":
                    UniformLabelSet ("Btn_Fun_PantsType1");
                    break;
                case "KeeperUniformPants2":
                    UniformLabelSet ("Btn_Fun_PantsType2");
                    break;
                case "KeeperUniformPants3":
                    UniformLabelSet ("Btn_Fun_PantsType3");
                    break;
                case "KeeperUniformPants4":
                    UniformLabelSet ("Btn_Fun_PantsType4");
                    break;
                case "KeeperUniformPants5":
                    UniformLabelSet ("Btn_Fun_PantsType5");
                    break;
                case "KeeperUniformPants6":
                    UniformLabelSet ("Btn_Fun_PantsType6");
                    break;
                case "KeeperUniformSocks1":
                    UniformLabelSet ("Btn_Fun_SocksType1");
                    break;
                case "KeeperUniformSocks2":
                    UniformLabelSet ("Btn_Fun_SocksType2");
                    break;
                case "KeeperUniformSocks3":
                    UniformLabelSet ("Btn_Fun_SocksType3");
                    break;
                case "KeeperUniformSocks4":
                    UniformLabelSet ("Btn_Fun_SocksType4");
                    break;
                case "KeeperUniformSocks5":
                    UniformLabelSet ("Btn_Fun_SocksType5");
                    break;
                case "KeeperUniformSocks6":
                    UniformLabelSet ("Btn_Fun_SocksType6");
                    break;
                }
            }
        }
    }
}
