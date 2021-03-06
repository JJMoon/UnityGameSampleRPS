using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public partial class AmServer
{
    private void SetNames (string kkoNck, string tmName)
    {
        kkoNick = kkoNck;
        teamName = tmName;
    }

    public void SetNickTeamName ()
    {
        switch (mBotID) {
        case 1:
            SetNames ("강국창", "내일은태양");
            break;
        case 2:
            SetNames ("강석태", "마담뚜");
            break;
        case 3:
            SetNames ("고태호", "메까자와");
            break;
        case 4:
            SetNames ("구학서", "나랑");
            break;
        case 5:
            SetNames ("권영진", "사신토스");
            break;
        case 6:
            SetNames ("권정택", "하츠네미쿠");
            break;
        case 7:
            SetNames ("권창수", "기차여행");
            break;
        case 8:
            SetNames ("김광림", "구리철사");
            break;
        case 9:
            SetNames ("김남진", "캥모");
            break;
        case 10:
            SetNames ("김대식", "남행열차");
            break;
        case 11:
            SetNames ("김덕환", "Madmax");
            break;
        case 12:
            SetNames ("김동욱", "굴평");
            break;
        case 13:
            SetNames ("김병록", "에이펑크");
            break;
        case 14:
            SetNames ("김봉찬", "오이김치");
            break;
        case 15:
            SetNames ("김상훈", "자유인");
            break;
        case 16:
            SetNames ("김선철", "샘물");
            break;
        case 17:
            SetNames ("김성태", "소론의적통");
            break;
        case 18:
            SetNames ("김세현", "조용한남자");
            break;
        case 19:
            SetNames ("김영래", "기적");
            break;
        case 20:
            SetNames ("김영로", "대머리지존");
            break;
        case 21:
            SetNames ("김영범", "너혼자집에");
            break;
        case 22:
            SetNames ("김영진", "화투");
            break;
        case 23:
            SetNames ("김용한", "여백");
            break;
        case 24:
            SetNames ("김의환", "피아골");
            break;
        case 25:
            SetNames ("김정현", "토토미어캣");
            break;
        case 26:
            SetNames ("김종우", "유동골뱅이");
            break;
        case 27:
            SetNames ("김주성", "유랑민");
            break;
        case 28:
            SetNames ("김주일", "황해");
            break;
        case 29:
            SetNames ("김진", "왜그래자꾸");
            break;
        case 30:
            SetNames ("김태환", "킬빌");
            break;
        case 31:
            SetNames ("김형근", "비아토맨");
            break;
        case 32:
            SetNames ("김호식", "수지만세");
            break;
        case 33:
            SetNames ("김호용", "난자완스");
            break;
        case 34:
            SetNames ("민경묵", "갈매나무");
            break;
        case 35:
            SetNames ("민경찬", "발터폰쉔코프");
            break;
        case 36:
            SetNames ("박경한", "모노폴리");
            break;
        case 37:
            SetNames ("박동원", "퓨어클린");
            break;
        case 38:
            SetNames ("박보명", "흐린날");
            break;
        case 39:
            SetNames ("박성규", "만나세크스");
            break;
        case 40:
            SetNames ("박종주", "돌고래");
            break;
        case 41:
            SetNames ("박진배", "팡야");
            break;
        case 42:
            SetNames ("석태민", "백두산백곰");
            break;
        case 43:
            SetNames ("설동윤", "꽃보단할매");
            break;
        case 44:
            SetNames ("손범규", "외인구단");
            break;
        case 45:
            SetNames ("송익선", "부산갈매기");
            break;
        case 46:
            SetNames ("송준호", "대전하늘소");
            break;
        case 47:
            SetNames ("신동혁", "존슨");
            break;
        case 48:
            SetNames ("신춘규", "셜록홈");
            break;
        case 49:
            SetNames ("신홍순", "데쓰노트");
            break;
        case 50:
            SetNames ("양성국", "죽어봐라");
            break;
        case 51:
            SetNames ("양승함", "녹차조아");
            break;
        case 52:
            SetNames ("엄경식", "팬숀남");
            break;
        case 53:
            SetNames ("오삼환", "담배먹는양");
            break;
        case 54:
            SetNames ("오풍영", "육백불의사나이");
            break;
        case 55:
            SetNames ("유희만", "매직오브오페라");
            break;
        case 56:
            SetNames ("유희진", "마법의가을");
            break;
        case 57:
            SetNames ("윤진동", "자다봉창");
            break;
        case 58:
            SetNames ("윤치환", "배아포");
            break;
        case 59:
            SetNames ("이결", "그게뭔데");
            break;
        case 60:
            SetNames ("이근", "멀더요원");
            break;
        case 61:
            SetNames ("이기봉", "다알아");
            break;
        case 62:
            SetNames ("이대실", "마그마");
            break;
        case 63:
            SetNames ("이상봉", "산해");
            break;
        case 64:
            SetNames ("이상훈", "쏠로모여");
            break;
        case 65:
            SetNames ("이석구", "아그리젠또");
            break;
        case 66:
            SetNames ("이순동", "리키티스");
            break;
        case 67:
            SetNames ("이승복", "헤르메스");
            break;
        case 68:
            SetNames ("이원승", "섹시가이");
            break;
        case 69:
            SetNames ("이재근", "꿈깨라");
            break;
        case 70:
            SetNames ("이종수", "개풀뜯는소리");
            break;
        case 71:
            SetNames ("이중명", "풀씹는강아지");
            break;
        case 72:
            SetNames ("이진혁", "애벌래");
            break;
        case 73:
            SetNames ("이한영", "거위의꿈");
            break;
        case 74:
            SetNames ("이홍수", "봉천동부랑아");
            break;
        case 75:
            SetNames ("장근한", "소라와오이");
            break;
        case 76:
            SetNames ("장세강", "잉여루저");
            break;
        case 77:
            SetNames ("장세주", "자나자나");
            break;
        case 78:
            SetNames ("장철호", "귀뚜라미보일러");
            break;
        case 79:
            SetNames ("장충근", "미라클포스");
            break;
        case 80:
            SetNames ("전용준", "프랜드");
            break;
        case 81:
            SetNames ("정병규", "히로");
            break;
        case 82:
            SetNames ("정성건", "알콩달콩");
            break;
        case 83:
            SetNames ("정영진", "피곤하다");
            break;
        case 84:
            SetNames ("정용석", "에드지젝");
            break;
        case 85:
            SetNames ("정종길", "웃으면복이와요");
            break;
        case 86:
            SetNames ("정태년", "가스마리");
            break;
        case 87:
            SetNames ("조창현", "음냐리");
            break;
        case 88:
            SetNames ("TomyPage", "TomyPageJK");
            break;
        case 89:
            SetNames ("AlanSomm", "ALKorlm");
            break;
        case 90:
            SetNames ("Bivky", "RusBik");
            break;
        case 91:
            SetNames ("Japzap", "Japzap");
            break;
        case 92:
            SetNames ("Sommer", "Summer50");
            break;
        case 93:
            SetNames ("BranRol", "BranRollK");
            break;
        case 94:
            SetNames ("Atomeo", "Atomeo33");
            break;
        case 95:
            SetNames ("MereCat2", "MereCatz2");
            break;
        case 96:
            SetNames ("Moria", "MoriaSaga");
            break;
        case 97:
            SetNames ("KorKor", "Korgottt");
            break;
        case 98:
            SetNames ("Byun", "ByunShinDl");
            break;
        case 99:
            SetNames ("QqqRrr", "JustQRQR");
            break;
        case 100:
            SetNames ("Midddr", "MidMidrr");
            break;
        case 101:
            SetNames ("Wiriwiri", "Wiri22q");
            break;
        case 102:
            SetNames ("JongwooM", "JongwooM");
            break;
        case 103:
            SetNames ("JKLee", "JkLeeJK");
            break;
        case 104:
            SetNames ("JSJustic", "JustinJS");
            break;
        case 105:
            SetNames ("Mandorini", "JamMando");
            break;
        case 106:
            SetNames ("Kogottel", "Kogottel");
            break;
        case 107:
            SetNames ("ZZZooo", "ZZZZooo");
            break;
        case 108:
            SetNames ("Posrel", "Postrel");
            break;
        case 109:
            SetNames ("MllPoo", "MllPoo0");
            break;
        case 110:
            SetNames ("Drakoma", "Dra99koma");
            break;
        case 111:
            SetNames ("Mekidoco", "Mekidoco");
            break;
        case 112:
            SetNames ("Lrewrkl", "Lrewrkl");
            break;
        case 113:
            SetNames ("Roowll", "Roowll00");
            break;
        case 114:
            SetNames ("Qwerty", "QWERTY");
            break;
        case 115:
            SetNames ("Meielk", "Meieksl");
            break;
        case 116:
            SetNames ("TomCrise", "TomeCrui");
            break;
        case 117:
            SetNames ("Vettnam", "Vettnam");
            break;
        case 118:
            SetNames ("Rexzsw", "Rexzsw");
            break;
        case 119:
            SetNames ("Oieloo", "Oieloo3");
            break;
        case 120:
            SetNames ("Majeon", "Majeon");
            break;
        case 121:
            SetNames ("LkjLkj", "LkjLkj");
            break;
        case 122:
            SetNames ("YHNHKL", "YHNHKL");
            break;
        case 123:
            SetNames ("xCdxlo", "xCdxlo");
            break;
        case 124:
            SetNames ("Oqlwwww", "Oqlwww8");
            break;
        case 125:
            SetNames ("VineTree", "VineTree");
            break;
        case 126:
            SetNames ("Floww", "Floww99");
            break;
        case 127:
            SetNames ("Uttiko", "Uttiko22");
            break;
        case 128:
            SetNames ("Xxx333", "Xxx333");
            break;
        case 129:
            SetNames ("Mistique", "Mistique");
            break;
        case 130:
            SetNames ("Rachael", "Rachael8");
            break;
        case 131:
            SetNames ("Sweork", "Sweork2");
            break;
        case 132:
            SetNames ("RorinWelk", "RorinWelk");
            break;
        case 133:
            SetNames ("AmusePk", "AmusePk");
            break;
        case 134:
            SetNames ("MoonTT2", "MoonTT2");
            break;
        case 135:
            SetNames ("Danguen", "Dangkue");
            break;
        case 136:
            SetNames ("Zeto", "Zeto99");
            break;
        case 137:
            SetNames ("BlissLog", "Blissooo");
            break;
        case 138:
            SetNames ("Minss", "MinsMax");
            break;
        case 139:
            SetNames ("LittleLo", "LittleLo");
            break;
        case 140:
            SetNames ("ArtLeern", "ArtLeer");
            break;
        case 141:
            SetNames ("Delegator", "Delegator");
            break;
        case 142:
            SetNames ("GameShumin", "GameShu");
            break;
        case 143:
            SetNames ("Pender222", "PenderGi");
            break;
        case 144:
            SetNames ("User3327946", "KILRK332");
            break;
        case 145:
            SetNames ("BikerJohn", "BikerJoh");
            break;
        case 146:
            SetNames ("ActivePolka", "ActvePlk");
            break;
        case 147:
            SetNames ("sivanesan", "ivanesan");
            break;
        case 148:
            SetNames ("fashuser", "fashuser");
            break;
        case 149:
            SetNames ("llyadyosi", "llyadyos");
            break;
        case 150:
            SetNames ("Kush", "Kuthbach");
            break;
        case 151:
            SetNames ("Okrel", "Okreloll");
            break;
        case 152:
            SetNames ("Modlekl", "Modlekl");
            break;
        case 153:
            SetNames ("Mawri", "NoakNuk");
            break;
        case 154:
            SetNames ("weasd", "MaxKing");
            break;
        case 155:
            SetNames ("King", "Kingwang");
            break;
        case 156:
            SetNames ("Aaaw", "ZZangZZ");
            break;
        case 157:
            SetNames ("Korell", "KromaT");
            break;
        case 158:
            SetNames ("Gow", "Gowris");
            break;
        case 159:
            SetNames ("Jeses", "JesusXmas");
            break;
        case 160:
            SetNames ("Mian", "MianSorry");
            break;
        case 161:
            SetNames ("Gettoo", "GentooMan");
            break;
        case 162:
            SetNames ("Korkel", "CloeMoriz");
            break;
        case 163:
            SetNames ("Mibian", "irootoo");
            break;
        case 164:
            SetNames ("Lweoo", "Lessoio");
            break;
        case 165:
            SetNames ("Drewol", "Drewolak");
            break;
        case 166:
            SetNames ("Socce", "SocKing2");
            break;
        case 167:
            SetNames ("wrork", "Fooalll");
            break;
        case 168:
            SetNames ("Footlbl", "Mreritkl");
            break;
        case 169:
            SetNames ("Fxlckck", "Fxlckck");
            break;
        case 170:
            SetNames ("qlw", "Juanyy");
            break;
        case 171:
            SetNames ("wlwlw", "RahlMate");
            break;
        case 172:
            SetNames ("Wekek", "MyNamell");
            break;
        case 173:
            SetNames ("Wllcock", "Matte112");
            break;
        case 174:
            SetNames ("Weofmfl", "Willyn89");
            break;
        case 175:
            SetNames ("w", "JohnBakl");
            break;
        case 176:
            SetNames ("wlwlw", "Jolraka");
            break;
        case 177:
            SetNames ("saechi", "Saechik");
            break;
        case 178:
            SetNames ("Arula", "Arula123");
            break;
        case 179:
            SetNames ("Meoo", "Meouran");
            break;
        case 180:
            SetNames ("For", "KickKin2");
            break;
        case 181:
            SetNames ("red", "RedMan9");
            break;
        case 182:
            SetNames ("ww", "Whitor2");
            break;
        case 183:
            SetNames ("www", "wwwPaGe");
            break;
        case 184:
            SetNames ("wvmmw", "Momboo88");
            break;
        case 185:
            SetNames ("234sdf", "WooZoozo");
            break;
        case 186:
            SetNames ("Reck", "Recktloo");
            break;
        case 187:
            SetNames ("vivivv", "Rxxxoww");
            break;
        case 188:
            SetNames ("qxsswx", "XXXmoMM");
            break;
        case 189:
            SetNames ("wwwww", "ForgetMe");
            break;
        case 190:
            SetNames ("Veow", "Bjbjk33");
            break;
        case 191:
            SetNames ("What", "WHFvck2");
            break;
        case 192:
            SetNames ("Mantyy", "PantsWhtl");
            break;
        case 193:
            SetNames ("XCVA", "WetPants9");
            break;
        case 194:
            SetNames ("Del", "DeleteKe");
            break;
        case 195:
            SetNames ("Kov", "IlClojure");
            break;
        case 196:
            SetNames ("CppHe", "CppPynh");
            break;
        case 197:
            SetNames ("zxxcdw", "PythonPy");
            break;
        case 198:
            SetNames ("JackJo", "JackJo99");
            break;
        case 199:
            SetNames ("erewax", "RealMakiy");
            break;
        case 200:
            SetNames ("End", "EndOfGame");
            break;
        


        }
    }
}