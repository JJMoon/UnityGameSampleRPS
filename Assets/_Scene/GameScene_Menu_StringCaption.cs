//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameScene   : AmSceneBase
{
    //string[] mCaptionStr = new string[50]; 

    Dictionary<string, string> dicCapt = new Dictionary<string, string>();
    List<string> arrCapt = new List<string>();


    void SetTutorialDictionary () {
        dicCapt.Add ("Kicker0", "킥커는 킥 방향과 킥세기를 결정하여 '\n'한번의 킥을 합니다.");
        dicCapt.Add ("Kicker1", "방향바의 네 가지 색깔은 '\n'각각 고유의 방향을 나타냅니다.");
        dicCapt.Add ("Kicker2", "파란색은 왼쪽위");
        dicCapt.Add ("Kicker3", "빨간색은 오른쪽위");
        dicCapt.Add ("Kicker4", "하늘색은 왼쪽아래");
        dicCapt.Add ("Kicker5", "노란색은 오른쪽아래");
        dicCapt.Add ("Kicker6", "방향선택핀이 '\n'[ff0000]오른쪽에서 왼쪽으로 이동할 때 '\n'[ffffff]핀을 멈춰 킥방향(색깔)을 결정합니다.");
        dicCapt.Add ("Kicker7", "왼쪽아래(하늘색)를 선택해 보세요.'\n''\n'[ff0000]화면을 터치하면 핀이 이동을 시작합니다.");
        dicCapt.Add ("Kicker8", "");
        dicCapt.Add ("Kicker9", "");
        dicCapt.Add ("Kicker10", "");
        dicCapt.Add ("Kicker11", "");
        dicCapt.Add ("Kicker12", "왼쪽 아래 선택 성공");
        dicCapt.Add ("Kicker13", "");
        dicCapt.Add ("Kicker14", "킥바의 깜빡이는 영역에 핀을 멈춰 파이어킥을 차보세요.” '\n''\n'[ff0000] “화면을 터치하면 핀이 이동을 시작합니다.");
        dicCapt.Add ("Kicker15", "");
        dicCapt.Add ("Kicker16", "파이어킥 성공" );

        dicCapt.Add ("Keeper1", "골키퍼는 킥방향을 예측하고 점프속도를 결정하여 한번의 방어를 합니다.");
        dicCapt.Add ("Keeper2", "[ff0000]상대 킥커의 방향바를 보고 '\n'킥 방향을 예측합니다.");
        dicCapt.Add ("Keeper3", " '예측한 방향(색깔)로 드래그하세요.” '\n''\n'[ff0000] “파란색(오른쪽위) 확률 99% 이상입니다.");
        dicCapt.Add ("Keeper4", "");
        dicCapt.Add ("Keeper5", "");
        dicCapt.Add ("Keeper6", "");
        dicCapt.Add ("Keeper7", "오른쪽 위 선택 성공");
        dicCapt.Add ("Keeper8", "");
        dicCapt.Add ("Keeper9", "");
        dicCapt.Add ("Keeper10", "");
        dicCapt.Add ("Keeper11", "이번엔 점프 속도를 결정할 차례입니다.");
        dicCapt.Add ("Keeper12", "");
        dicCapt.Add ("Keeper13", "");
        dicCapt.Add ("Keeper14", "“점프바의 깜빡이는 영역에 핀을 멈춰 플래쉬점프를 해보세요.” '\n''\n'[ff0000] “화면을 터치하면 핀이 이동을 시작합니다.");
        dicCapt.Add ("Keeper15", "“플래쉬점프 성공“");

        dicCapt.Add ("Kicker2_1", " “킥바의 깜빡이는 영역에 핀을 멈춰 블레이즈킥을 차보세요.”  '\n''\n'[ff0000] “화면을 터치하면 핀이 이동을 시작합니다.” ");
        dicCapt.Add ("Kicker2_2", " “블레이즈킥 성공” ");

        dicCapt.Add ("Keeper2_1", "(시간 안에 선택 시 그냥 진행, 선택 안했을 시 캡션 노출)'\n' 점프 방향을 선택하세요. 실전에선 2번 기회를 드리지 않아요. (버튼에 이펙트)");
        dicCapt.Add ("Keeper2_2", "“점프바의 깜빡이는 영역에 핀을 멈춰 라이트닝점프를 해보세요.” '\n''\n'[ff0000] “화면을 터치하면 핀이 이동을 시작합니다.”");
        dicCapt.Add ("Keeper2_3", "“라이트닝점프 성공”");


        dicCapt.Add ("Kicker3_2", "이번엔 드링크 덕 좀 봤네요. '\n'내친 김에 다른 드링크도 한 번 먹어 보죠");
        dicCapt.Add ("Kicker3_3", "아.. '\n'드링크 덕은 못 봤지만 다행히 골인이네요.'\n' 상대가 너무 머리를 쓴 것 같네요. '\n'내친 김에 다른 드링크도 한 번 먹어 보죠.");
        dicCapt.Add ("Kicker3_4", "와!! 3:0 완벽한 승리입니다. '\n'가장 높은 보너스 승점을 얻으셨어요. '\n'(누적 승점에 이펙트)");
        dicCapt.Add ("Kicker3_5", "각 에너지드링크의 효과는'\n' 스토어에서 확인하시고 미리 준비해두세요.'\n' 팀 전체에 적용되는 팀아이템도 준비되어 있습니다.");
        dicCapt.Add ("RePlay", "다시 선택하세요.");

        dicCapt.Add ("KickerCerCap1", "킥 방향을 멋지게 속여 득점에 성공하였습니다.");
        dicCapt.Add ("KeeperCerCap2", "킥 방향을 정확히 예측하여 방어에 성공하였습니다.");
        dicCapt.Add ("KickerCerCap2_1", "골키퍼에게 킥 방향을 읽혔습니다.");
        dicCapt.Add ("KickerCerCap2_2", "그러나 강한 킥(블레이즈킥)으로 득점에 성공하였습니다.");
        dicCapt.Add ("KeeperCerCap2_1", "킥 방향을 정확히 예측하지 못했습니다.");
        dicCapt.Add ("KeeperCerCap2_2", "그러나 빠른 점프(라이트닝점프)로 방어에 성공하였습니다.");



    }
}
