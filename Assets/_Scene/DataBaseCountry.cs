//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------


using UnityEngine;
using System.Collections;

public class DataBaseCountry {
    public string[] mNation, mNationSprite; 

    public string SetNationFlag (int pNationCode ) {
        mNation = new string[40];

        mNation [0] = "대한민국";
        mNation [1] = "알제리";
        mNation [2] = "아르헨티나";
        mNation [3] = "호주";
        mNation [4] = "벨기에";
        mNation [5] = "보스니아헤르체고비나";
        mNation [6] = "브라질";
        mNation [7] = "카메룬";
        mNation [8] = "칠레";
        mNation [9] = "콜롬비아";
        mNation [10] = "코스타리카";
        mNation [11] = "코트디브아르";
        mNation [12] = "크로아티아";
        mNation [13] = "에콰도르";
        mNation [14] = "잉글랜드";
        mNation [15] = "프랑스";
        mNation [16] = "독일";
        mNation [17] = "가나";
        mNation [18] = "그리스";
        mNation [19] = "온두라스";
        mNation [20] = "이란";
        mNation [21] = "이탈리아";
        mNation [22] = "일본";
        mNation [23] = "멕시코";
        mNation [24] = "네덜란드";
        mNation [25] = "나이지리아";
        mNation [26] = "포르투갈";
        mNation [27] = "러시아";
        mNation [28] = "스페인";
        mNation [29] = "스위스";
        mNation [30] = "우루과이";
        mNation [31] = "미국";
        return mNation [pNationCode];
    }

    public string SetNationSprite (int pNationCode ) {
        mNationSprite = new string[40];

        mNationSprite [0] = "flag_KOR";
        mNationSprite [1] = "flag_ALG";
        mNationSprite [2] = "flag_ARG";
        mNationSprite [3] = "flag_AUS";
        mNationSprite [4] = "flag_BEL";
        mNationSprite [5] = "flag_BIH";
        mNationSprite [6] = "flag_BRA";
        mNationSprite [7] = "flag_CMR";
        mNationSprite [8] = "flag_CHI";
        mNationSprite [9] = "flag_COL";
        mNationSprite [10] = "flag_CRC";
        mNationSprite [11] = "flag_CIV";
        mNationSprite [12] = "flag_CRO";
        mNationSprite [13] = "flag_ECU";
        mNationSprite [14] = "flag_ENG";
        mNationSprite [15] = "flag_FRA";
        mNationSprite [16] = "flag_GER";
        mNationSprite [17] = "flag_GHA";
        mNationSprite [18] = "flag_GRE";
        mNationSprite [19] = "flag_HON";
        mNationSprite [20] = "flag_IRN";
        mNationSprite [21] = "flag_ITA";
        mNationSprite [22] = "flag_JPN";
        mNationSprite [23] = "flag_MEX";
        mNationSprite [24] = "flag_NED";
        mNationSprite [25] = "flag_NGA";
        mNationSprite [26] = "flag_POR";
        mNationSprite [27] = "flag_RUS";
        mNationSprite [28] = "flag_ESP";
        mNationSprite [29] = "flag_SUI";
        mNationSprite [30] = "flag_URU";
        mNationSprite [31] = "flag_USA";
        return mNationSprite [pNationCode];
    }

}