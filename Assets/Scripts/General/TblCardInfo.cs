using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static partial class Tbl
{
	public static Dictionary<string, string> dicCardName = new Dictionary<string, string> ();
	public static Dictionary<string, int[]> dicDeckItem = new Dictionary<string, int[]> ();
	public static Dictionary<string, string> dicDeckBackNumCol = new Dictionary<string, string> ();
	public static Dictionary<string, Color> dicDeckBacRgbCode = new Dictionary<string, Color> ();
	public static Dictionary<string, string> dicHanLog = new Dictionary<string, string> ();
	public static Dictionary<int, int> dicGamePointOfCardGrade = new Dictionary<int, int> ();
	public static Dictionary<string, List<int>> dicBuffCondition = new Dictionary<string, List<int>> ();
	public static Dictionary<string, string> arrDeckUniformName = new Dictionary<string, string> ();
	public static Dictionary<string, string> arrDeckPlayerinfoUniformName = new Dictionary<string, string> ();
	public static Dictionary<string, string> arrDecknationname = new Dictionary<string, string> ();
	public static Dictionary<string, string> dicDeckNation = new Dictionary<string, string> ();
	public static List<WasCard> arrKickBotCard = new List<WasCard> ();
	public static List<WasCard> arrKeepBotCard = new List<WasCard> ();
	public static List<string> arrDeckBuffName = new List<string> ();

	static Tbl ()
	{
        dicCardName ["W_N_Goalie001"] = "Jepperson";
        dicCardName ["W_N_Goalie002"] = "Cessar";
        dicCardName ["W_N_Goalie003"] = "Pleticosa"; //"애들라";
        dicCardName ["W_N_Goalie004"] = "Ohjoha"; //"부본";
        dicCardName ["W_N_Kicker001"] = "Marquaz"; //"벡곰";
        dicCardName ["W_N_Kicker002"] = "Morenon"; //"매쉬";
        dicCardName ["W_N_Kicker003"] = "Makoon"; //"가카";
        dicCardName ["W_N_Kicker004"] = "Enohc"; //"백지승";
        dicCardName ["W_N_Kicker005"] = "Colluca"; //"앵리";
        dicCardName ["W_N_Kicker006"] = "Modrich"; //"왜인 로니";
        dicCardName ["W_N_Kicker007"] = "Spahiche"; //"비와";
        dicCardName ["W_N_Kicker008"] = "Pjaniche"; //"존 테러";
        dicCardName ["W_N_Kicker009"] = "Dzeka"; //"디비지";
        dicCardName ["W_N_Kicker010"] = "Davori"; //"싸비";
        dicCardName ["W_N_Kicker011"] = "Hosselin"; //"이니애스토";
        dicCardName ["W_N_Kicker012"] = "Dejaker"; // "애블라";
        dicCardName ["W_N_Kicker013"] = "Mooza"; //"델 피엘오";
        dicCardName ["W_N_Kicker014"] = "Ameobi"; //"네수토";
        dicCardName ["W_N_Kicker015"] = "Mikal"; //"에토";
        dicCardName ["W_N_Kicker016"] = "Egwuebe"; //"드로고버";
        dicCardName ["W_N_Kicker017"] = "Ansarifart"; //"클나재";
        dicCardName ["DEFAULT"] = "Beitashoor"; //"표창원";

		int sec = 0;
		dicGamePointOfCardGrade [sec++] = 0;
		dicGamePointOfCardGrade [sec++] = 1;
		dicGamePointOfCardGrade [sec++] = 2;
		dicGamePointOfCardGrade [sec++] = 4;
		dicGamePointOfCardGrade [sec++] = 6;
		dicGamePointOfCardGrade [sec++] = 9;
		dicGamePointOfCardGrade [sec++] = 12;
		dicGamePointOfCardGrade [sec++] = 16;
		dicGamePointOfCardGrade [sec++] = 20;
		dicGamePointOfCardGrade [sec++] = 25;
		dicGamePointOfCardGrade [sec++] = 30;

		dicHanLog ["EveryHourEvent"] = "%EC%A0%95%EA%B0%81%EB%B3%B4%EC%83%81"; // 정각보상.

		dicBuffCondition ["Gold"] = new List<int> () { 1, 2, 3, 4, 5, 6, 7, 8 };
		dicBuffCondition ["WinnerPoint"] = new List<int> () { 9, 10, 11, 12, 13, 14, 15 };
		dicBuffCondition ["Contract"] = new List<int> () { 2, 9, 16, 17, 18, 19, 20, 21 };
		dicBuffCondition ["KickDirect"] = new List<int> () { 3, 10, 16, 22, 23, 24, 25 };
		dicBuffCondition ["KickSkill"] = new List<int> () { 4, 11, 17, 22, 27, 28, 29, 30 };
		dicBuffCondition ["Balance"] = new List<int> () { 5, 12, 18, 23, 27, 31, 32, 33 };
		dicBuffCondition ["Jump"] = new List<int> () { 6, 13, 19, 24, 28, 31, 34, 35 };
		dicBuffCondition ["CoolTime"] = new List<int> () { 7, 14, 20, 25, 29, 32, 34, 36 };
		dicBuffCondition ["LosePoint"] = new List<int> () { 8, 15, 21, 26, 30, 33, 35, 36 };





		dicDeckItem ["BRA"] = new int[] { 2, 3, 1 };
		dicDeckItem ["CRO"] = new int[] { 3, 4, 7 };
		dicDeckItem ["MEX"] = new int[] { 6, 4, 7 };
		dicDeckItem ["CMR"] = new int[] { 6, 4, 6 };
		dicDeckItem ["ESP"] = new int[] { 5, 3, 1 };
		dicDeckItem ["NED"] = new int[] { 5, 1, 7 };
		dicDeckItem ["CHI"] = new int[] { 4, 3, 6 };
		dicDeckItem ["AUS"] = new int[] { 4, 6, 7 };
		dicDeckItem ["JPN"] = new int[] { 6, 3, 7 };
		dicDeckItem ["COL"] = new int[] { 4, 6, 5 };
		dicDeckItem ["CIV"] = new int[] { 7, 3, 4 };
		dicDeckItem ["GRE"] = new int[] { 6, 4, 1 };
		dicDeckItem ["ENG"] = new int[] { 3, 7, 4 };
		dicDeckItem ["ITA"] = new int[] { 5, 4, 5 };
		dicDeckItem ["URU"] = new int[] { 1, 4, 6 };
        dicDeckItem ["CRC"] = new int[] { 6, 3, 4 };
		dicDeckItem ["FRA"] = new int[] { 3, 7, 7 };
		dicDeckItem ["SUI"] = new int[] { 3, 4, 6 };
		dicDeckItem ["ECU"] = new int[] { 7, 4, 3 };
		dicDeckItem ["HON"] = new int[] { 7, 6, 4 };
		dicDeckItem ["ARG"] = new int[] { 1, 2, 1 };
		dicDeckItem ["BIH"] = new int[] { 3, 7, 2 };
		dicDeckItem ["NGA"] = new int[] { 6, 7, 4 };
		dicDeckItem ["IRN"] = new int[] { 7, 6, 4 };
		dicDeckItem ["GER"] = new int[] { 1, 3, 7 };
		dicDeckItem ["POR"] = new int[] { 5, 7, 2 };
		dicDeckItem ["USA"] = new int[] { 3, 4, 5 };
		dicDeckItem ["GHA"] = new int[] { 7, 4, 6 };
		dicDeckItem ["KOR"] = new int[] { 6, 3, 1 };
		dicDeckItem ["BEL"] = new int[] { 6, 3, 7 };
		dicDeckItem ["RUS"] = new int[] { 7, 4, 7 };
		dicDeckItem ["ALG"] = new int[] { 4, 6, 4 };

		dicDeckBackNumCol ["BRA"] = "[41743C]";
		dicDeckBackNumCol ["CRO"] = "[FCFFFF]";
		dicDeckBackNumCol ["MEX"] = "[FCFFFF]";
		dicDeckBackNumCol ["CMR"] = "[EBC221]";
		dicDeckBackNumCol ["ESP"] = "[F1DCA3]";
		dicDeckBackNumCol ["NED"] = "[FCFFFF]";
		dicDeckBackNumCol ["CHI"] = "[111111]";
		dicDeckBackNumCol ["AUS"] = "[E3B967]";
        dicDeckBackNumCol ["JPN"] = "[FCFFFF]";
		dicDeckBackNumCol ["COL"] = "[1E2C51]";
		dicDeckBackNumCol ["CIV"] = "[5FFFAA]";
		dicDeckBackNumCol ["GRE"] = "[132780]";
		dicDeckBackNumCol ["ENG"] = "[2C427C]";
		dicDeckBackNumCol ["ITA"] = "[FCFFFF]";
		dicDeckBackNumCol ["URU"] = "[111111]";
		dicDeckBackNumCol ["CRC"] = "[132780]";
		dicDeckBackNumCol ["FRA"] = "[FCFFFF]";
		dicDeckBackNumCol ["SUI"] = "[FCFFFF]";
		dicDeckBackNumCol ["ECU"] = "[132780]";
		dicDeckBackNumCol ["HON"] = "[2F4B69]";
		dicDeckBackNumCol ["ARG"] = "[27444C]";
		dicDeckBackNumCol ["BIH"] = "[260B58]";
		dicDeckBackNumCol ["NGA"] = "[FCFFFF]";
		dicDeckBackNumCol ["IRN"] = "[41743C]";
		dicDeckBackNumCol ["GER"] = "[333A4D]";
		dicDeckBackNumCol ["POR"] = "[FCFFFF]";
		dicDeckBackNumCol ["USA"] = "[034895]";
		dicDeckBackNumCol ["GHA"] = "[111111]";
		dicDeckBackNumCol ["KOR"] = "[FCFFFF]";
		dicDeckBackNumCol ["BEL"] = "[FCDE00]";
		dicDeckBackNumCol ["RUS"] = "[8C853F]";
		dicDeckBackNumCol ["ALG"] = "[66A938]";
        dicDeckBackNumCol ["UNI"] = "[FCFFFF]";

		dicDeckNation ["ALG"] = "algeria";
		dicDeckNation ["ARG"] = "argentina";
		dicDeckNation ["AUS"] = "australia";
		dicDeckNation ["BEL"] = "belgium";
		dicDeckNation ["BIH"] = "bosnia";
		dicDeckNation ["BRA"] = "brazil";
		dicDeckNation ["CMR"] = "cameroon";
		dicDeckNation ["CRC"] = "costarica";
		dicDeckNation ["CHI"] = "chile";
		dicDeckNation ["CIV"] = "cotedivoire";
		dicDeckNation ["COL"] = "colombia";
		dicDeckNation ["CRO"] = "croatia";
		dicDeckNation ["ECU"] = "ecuador";
		dicDeckNation ["ENG"] = "england";
		dicDeckNation ["FRA"] = "france";
		dicDeckNation ["GER"] = "germany";
		dicDeckNation ["GHA"] = "ghana";
		dicDeckNation ["GRE"] = "greece";
		dicDeckNation ["HON"] = "honduras";
		dicDeckNation ["IRN"] = "iran";
		dicDeckNation ["ITA"] = "italy";
		dicDeckNation ["JPN"] = "japan";
		dicDeckNation ["KOR"] = "korea";
		dicDeckNation ["MEX"] = "mexico";
		dicDeckNation ["NED"] = "netherlands";
		dicDeckNation ["NGA"] = "nigeria";
		dicDeckNation ["POR"] = "portugal";
		dicDeckNation ["RUS"] = "russia";
		dicDeckNation ["ESP"] = "spain";
		dicDeckNation ["SUI"] = "swiss";
		dicDeckNation ["UNI"] = "union";
		dicDeckNation ["URU"] = "uruguay";
		dicDeckNation ["USA"] = "usa";

		dicDeckBacRgbCode ["BRA"] = new Color (65 / 255, 116 / 255, 60 / 255);
		dicDeckBacRgbCode ["CRO"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["MEX"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["CMR"] = new Color (235 / 255, 194 / 255, 33 / 255);
		dicDeckBacRgbCode ["ESP"] = new Color (241 / 255, 220 / 255, 163 / 255);
		dicDeckBacRgbCode ["NED"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["CHI"] = new Color (1 / 255, 0 / 255, 0 / 255);
		dicDeckBacRgbCode ["AUS"] = new Color (227 / 255, 185 / 255, 103 / 255);
		dicDeckBacRgbCode ["JPN"] = new Color (1 / 255, 0 / 255, 0 / 255);
		dicDeckBacRgbCode ["COL"] = new Color (30 / 255, 44 / 255, 81 / 255);
		dicDeckBacRgbCode ["CIV"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["GRE"] = new Color (19 / 255, 39 / 255, 128 / 255);
		dicDeckBacRgbCode ["ENG"] = new Color (44 / 255, 66 / 255, 124 / 255);
		dicDeckBacRgbCode ["ITA"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["URU"] = new Color (1 / 255, 0 / 255, 0 / 255);
		dicDeckBacRgbCode ["CRC"] = new Color (19 / 255, 39 / 255, 128 / 255);
		dicDeckBacRgbCode ["FRA"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["SUI"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["ECU"] = new Color (19 / 255, 39 / 255, 128 / 255);
		dicDeckBacRgbCode ["HON"] = new Color (47 / 255, 75 / 255, 105 / 255);
		dicDeckBacRgbCode ["ARG"] = new Color (39 / 255, 68 / 255, 76 / 255);
		dicDeckBacRgbCode ["BIH"] = new Color (38 / 255, 11 / 255, 88 / 255);
		dicDeckBacRgbCode ["NGA"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["IRN"] = new Color (65 / 255, 116 / 255, 60 / 255);
		dicDeckBacRgbCode ["GER"] = new Color (51 / 255, 58 / 255, 77 / 255);
		dicDeckBacRgbCode ["POR"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["USA"] = new Color (3 / 255, 72 / 255, 149 / 255);
		dicDeckBacRgbCode ["GHA"] = new Color (1 / 255, 0 / 255, 0 / 255);
		dicDeckBacRgbCode ["KOR"] = new Color (252 / 255, 255 / 255, 255 / 255);
		dicDeckBacRgbCode ["BEL"] = new Color (252 / 255, 222 / 255, 0 / 255);
		dicDeckBacRgbCode ["RUS"] = new Color (140 / 255, 133 / 255, 63 / 255);
		dicDeckBacRgbCode ["ALG"] = new Color (102 / 255, 169 / 255, 56 / 255);
        dicDeckBacRgbCode ["UNI"] = new Color (102 / 255, 169 / 255, 56 / 255);



		arrDeckBuffName.Add ("None_0");
		arrDeckBuffName.Add ("승점 +7%"); // 1
		arrDeckBuffName.Add ("슛방향 +5%"); // 1
		arrDeckBuffName.Add ("킥세기 +8%"); // 1
		arrDeckBuffName.Add ("골키퍼발란스 -20%"); // 1
		arrDeckBuffName.Add ("점프력 +8%"); // 1
		arrDeckBuffName.Add ("스카우터쿨타임 -15%"); // 1
		arrDeckBuffName.Add ("패점 10% 방어"); // 1

		//---------------------------------------Keeper
		arrKeepBotCard.Add (new WasCard () { country = "BEL", look = 1, playerID = 1 });
		arrKeepBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 2 });
		arrKeepBotCard.Add (new WasCard () { country = "ALG", look = 1, playerID = 15 });
		arrKeepBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 16 });
		arrKeepBotCard.Add (new WasCard () { country = "RUS", look = 1, playerID = 28 });
		arrKeepBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 29 });
		arrKeepBotCard.Add (new WasCard () { country = "KOR", look = 1, playerID = 42 });
		arrKeepBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 43 });
		arrKeepBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 44 });
		//---------------------------------------KIcker
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 1, playerID = 3 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 4 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 5 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 6 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 7 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 8 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 9 });
		arrKickBotCard.Add (new WasCard () { country = "BEL", look = 2, playerID = 10 });

		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 1, playerID = 17 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 18 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 19 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 20 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 21 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 22 });
		arrKickBotCard.Add (new WasCard () { country = "ALG", look = 2, playerID = 23 });

		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 1, playerID = 30 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 31 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 32 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 33 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 34 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 35 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 36 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 37 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 38 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 39 });
		arrKickBotCard.Add (new WasCard () { country = "RUS", look = 2, playerID = 40 });

		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 1, playerID = 45 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 46 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 47 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 48 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 49 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 50 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 51 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 52 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 53 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 54 });
		arrKickBotCard.Add (new WasCard () { country = "KOR", look = 2, playerID = 55 });


		arrDeckUniformName ["KOR"] = "uniform_korea";
		arrDeckUniformName ["ALG"] = "uniform_algeria";
		arrDeckUniformName ["ARG"] = "uniform_argentina";
        arrDeckUniformName ["AUS"] = "uniform_austraila";
		arrDeckUniformName ["BEL"] = "uniform_belgium";
		arrDeckUniformName ["BIH"] = "uniform_bosnia";
		arrDeckUniformName ["BRA"] = "uniform_brazil";
		arrDeckUniformName ["CMR"] = "uniform_cameroon";
		arrDeckUniformName ["CHI"] = "uniform_chile";
		arrDeckUniformName ["COL"] = "uniform_colombia";
		arrDeckUniformName ["CRC"] = "uniform_costarica";
		arrDeckUniformName ["CIV"] = "uniform_cotedivoire";
        arrDeckUniformName ["CRO"] = "uniform_coatia";
        arrDeckUniformName ["ECU"] = "uniform_equador";
		arrDeckUniformName ["ENG"] = "uniform_england";
		arrDeckUniformName ["FRA"] = "uniform_france";
		arrDeckUniformName ["GER"] = "uniform_germany";
		arrDeckUniformName ["GHA"] = "uniform_ghana";
		arrDeckUniformName ["GRE"] = "uniform_greece";
		arrDeckUniformName ["HON"] = "uniform_honduras";
		arrDeckUniformName ["IRN"] = "uniform_iran";
		arrDeckUniformName ["ITA"] = "uniform_italy";
		arrDeckUniformName ["JPN"] = "uniform_japan";
		arrDeckUniformName ["MEX"] = "uniform_mexico";
		arrDeckUniformName ["NED"] = "uniform_netherlands";
		arrDeckUniformName ["NGA"] = "uniform_nigeria";
		arrDeckUniformName ["POR"] = "uniform_portugal";
		arrDeckUniformName ["RUS"] = "uniform_russia";
		arrDeckUniformName ["ESP"] = "uniform_spain";
		arrDeckUniformName ["SUI"] = "uniform_swiss";
		arrDeckUniformName ["URU"] = "uniform_uruguay";
		arrDeckUniformName ["USA"] = "uniform_usa";
        arrDeckUniformName ["UNI"] = "uniform_legends";


		arrDeckPlayerinfoUniformName ["KOR"] = "";
		arrDeckPlayerinfoUniformName ["ALG"] = "";
		arrDeckPlayerinfoUniformName ["ARG"] = "";
		arrDeckPlayerinfoUniformName ["AUS"] = "";
		arrDeckPlayerinfoUniformName ["BEL"] = "";
		arrDeckPlayerinfoUniformName ["BIH"] = "";
		arrDeckPlayerinfoUniformName ["BRA"] = "";
		arrDeckPlayerinfoUniformName ["CMR"] = "";
		arrDeckPlayerinfoUniformName ["CHI"] = "";
		arrDeckPlayerinfoUniformName ["COL"] = "";
		arrDeckPlayerinfoUniformName ["CRC"] = "";
		arrDeckPlayerinfoUniformName ["CIV"] = "";
		arrDeckPlayerinfoUniformName ["CRO"] = "";
		arrDeckPlayerinfoUniformName ["ECU"] = "";
		arrDeckPlayerinfoUniformName ["ENG"] = "";
		arrDeckPlayerinfoUniformName ["FRA"] = "";
		arrDeckPlayerinfoUniformName ["GER"] = "";
		arrDeckPlayerinfoUniformName ["GHA"] = "";
		arrDeckPlayerinfoUniformName ["GRE"] = "";
		arrDeckPlayerinfoUniformName ["HON"] = "";
		arrDeckPlayerinfoUniformName ["IRN"] = "";
		arrDeckPlayerinfoUniformName ["ITA"] = "";
		arrDeckPlayerinfoUniformName ["JPN"] = "";
		arrDeckPlayerinfoUniformName ["MEX"] = "";
		arrDeckPlayerinfoUniformName ["NED"] = "";
		arrDeckPlayerinfoUniformName ["NGA"] = "";
		arrDeckPlayerinfoUniformName ["POR"] = "";
		arrDeckPlayerinfoUniformName ["RUS"] = "";
		arrDeckPlayerinfoUniformName ["ESP"] = "";
		arrDeckPlayerinfoUniformName ["SUI"] = "";
		arrDeckPlayerinfoUniformName ["URU"] = "";
		arrDeckPlayerinfoUniformName ["USA"] = "";


		arrDecknationname ["KOR"] = "대한민국";
		arrDecknationname ["ALG"] = "알제리";
		arrDecknationname ["ARG"] = "아르헨티나";
		arrDecknationname ["AUS"] = "오스트레일리아";
		arrDecknationname ["BEL"] = "벨기에";
		arrDecknationname ["BIH"] = "보스니아";
		arrDecknationname ["BRA"] = "브라질";
		arrDecknationname ["CMR"] = "카메룬";
		arrDecknationname ["CHI"] = "칠레";
		arrDecknationname ["COL"] = "콜롬비아";
		arrDecknationname ["CRC"] = "코스타리카";
		arrDecknationname ["CIV"] = "코트디브아르";
		arrDecknationname ["CRO"] = "크로아티아";
		arrDecknationname ["ECU"] = "에콰도르";
		arrDecknationname ["ENG"] = "잉글랜드";
		arrDecknationname ["FRA"] = "프랑스";
		arrDecknationname ["GER"] = "독일";
		arrDecknationname ["GHA"] = "가나";
		arrDecknationname ["GRE"] = "그리스";
		arrDecknationname ["HON"] = "혼두라스";
		arrDecknationname ["IRN"] = "이란";
		arrDecknationname ["ITA"] = "이탈리아";
		arrDecknationname ["JPN"] = "일본";
		arrDecknationname ["MEX"] = "멕시코";
		arrDecknationname ["NED"] = "네덜란드";
		arrDecknationname ["NGA"] = "나이지리아";
		arrDecknationname ["POR"] = "포르투칼";
		arrDecknationname ["RUS"] = "러시아";
		arrDecknationname ["ESP"] = "스페인";
		arrDecknationname ["SUI"] = "스위스";
		arrDecknationname ["URU"] = "우루과이";
		arrDecknationname ["USA"] = "미국";


	}

	public static int GetBuffGoldPoint (int pDeckNum)
	{
		if (!dicBuffCondition ["Gold"].HasAny (pDeckNum))
			return -1;
		return 1;
	}
}

