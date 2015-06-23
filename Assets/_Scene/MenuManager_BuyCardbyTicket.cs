using UnityEngine;
using System.Collections;

public partial class MenuManager : AmSceneBase {

    void Btn_Fun_Lobby_FreeHighCard1OK () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl1card", false);
    }

    void Btn_Fun_Lobby_FreeHighCard3OK () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl3card", false);
    }

    void Btn_Fun_Lobby_FreeNorMarCard1OK () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal1card", false);
    }

    void Btn_Fun_Lobby_FreeNorMarCard3OK () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal3card", false);
    }

   


    void Btn_Fun_Lobby_FreeHighCard1cancel () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl1card", false);
    }

    void Btn_Fun_Lobby_FreeHighCard3cancel () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopuphighl3card", false);
    }

    void Btn_Fun_Lobby_FreeNorMarCard1cancel () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal1card", false);
    }

    void Btn_Fun_Lobby_FreeNorMarCard3cancel () {
        MenuCommonOpen ("LobbyBuyFreecardopen_popup", "LobbyBuyFreepopupnormal3card", false);
    }


}
