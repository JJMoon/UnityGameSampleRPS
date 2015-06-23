using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.Text;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  AvCard  _____  Class  _____
public class AvCard
{
    public string LeagueSpriteName, LeagueSpriteNameS;


    public void CardLeagueSpritename (string CardLeage)
    {

        switch (CardLeage) {
        case "PRO_5":
            LeagueSpriteName = "icon_div5";
            LeagueSpriteNameS = "icon_div5s";
//            Debug.Log (LeagueSpriteName+"LeagueName");
            break;
        case "PRO_4":
            LeagueSpriteName = "icon_div4";
            LeagueSpriteNameS = "icon_div4s";
            break;
        case "PRO_3":
            LeagueSpriteName = "icon_div3";
            LeagueSpriteNameS = "icon_div3s";
            break;
        case "PRO_2":
            LeagueSpriteName = "icon_div2";
            LeagueSpriteNameS = "icon_div2s";
            break;
        case "PRO_1":
            LeagueSpriteName = "icon_div1";
            LeagueSpriteNameS = "icon_div1s";
            break;


        }
    }
}