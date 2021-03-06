//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartMenu_ItemScript  : MonoBehaviour {
    public Dictionary<string, string> dicCapt = new Dictionary<string, string>();
    List<string> arrCapt = new List<string>();
    
    void Start () {
        ShopItemListCaption ();
    }

    void ShopItemListCaption () {
        dicCapt.Add ("Drink1", "에너지드링크(블루)");
        dicCapt.Add ("Drink1_1", "다리힘 증가");
        dicCapt.Add ("Drink1_2", "기능: '\n'파이어킥(플래쉬점프)과 블레이즈킥(라이트닝점프)의 선택 영역을 넓혀줍니다.");
        dicCapt.Add ("Drink1_3", "적용대상: 킥커, 골키퍼");
        dicCapt.Add ("Drink2", "에너지드링크(그린)");
        dicCapt.Add ("Drink2_1", "기술 향상");
        dicCapt.Add ("Drink2_2", "기능: '\n'킥커가 잘 못 차는 방향들의 선택 영역을 넓혀줍니다.");
        dicCapt.Add ("Drink2_3", "적용대상: 킥커");
        dicCapt.Add ("Drink3", "에너지드링크(레드)");
        dicCapt.Add ("Drink3_1", "집중력 향상");
        dicCapt.Add ("Drink3_2", "기능: '\n'방향선택과 킥세기(점프속도) 선택 시 선택핀의 이동 속도를 감소시켜줍니다.");
        dicCapt.Add ("Drink3_3", "적용대상: 킥커, 골키퍼");
        dicCapt.Add ("Kicker6", "킥커6");
        dicCapt.Add ("Kicker6_1", "우측 상하와 좌측 하단 3방향을 자유자재로 찰 수 있음. ");
        dicCapt.Add ("Kicker6_2", "업그레이드 레벨당 획득 점수의 1.5% 가산됨.");
        dicCapt.Add ("Kicker7", "킥커7");
        dicCapt.Add ("Kicker7_1", "우측 상하와 좌측 상단 3방향을 자유자재로 찰 수 있음.");
        dicCapt.Add ("Kicker7_2", "업그레이드 레벨당 획득 점수의 1.5% 가산됨.");
        dicCapt.Add ("Kicker8", "킥커8");
        dicCapt.Add ("Kicker8_1", "좌측 상하와 우측 하단 3방향을 자유자재로 찰 수 있음.");
        dicCapt.Add ("Kicker8_2", "업그레이드 레벨당 획득 점수의 2% 가산됨.");
        dicCapt.Add ("Keeper2", "골키퍼2");
        dicCapt.Add ("Keeper2_1", "우측(파,하) 도약력이 좌측(빨,노) 도약력 보다 좋음.");
        dicCapt.Add ("Keeper2_2", "기본 킥커 5명중 2명에 대한 대응력이 좋음.");
    }
}
