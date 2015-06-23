using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;

//using LitJson;
using SimpleJSON;

// http://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa  dictionary <-> object   mapping example ..
public partial class Test : AmSceneBase
{
    string itemType = "type", itemTypeID = "itemID";

    public  void SetColumnA ()
    {
        muiCol = 0;
        muiRow = 0;
        int colN = 0, colEA;


        GUI.Label (myGUI.GetRect (muiCol, muiRow++), myUser.WAS.KkoID + "   " + myUser.WAS.League);

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Regist   _____
        Rect curRegis = myGUI.GetRect (muiCol, muiRow++);
        chkTeamName = GUI.TextField (myGUI.DivideRect (curRegis, 3, 0), chkTeamName, 15);
        colEA = 5;
        colN = 2;
        if (GUI.Button (myGUI.DivideRect (curRegis, colEA, colN++), "Chk Team")) {
            WasTeamCheck aObj = new WasTeamCheck () { ID = myUser.WAS.KkoID, TgtName = chkTeamName };
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curRegis, colEA, colN++), "Reg")) {

            myUser.WAS.TeamName = chkTeamName;

            WasRegist aObj = new WasRegist () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curRegis, colEA, colN++), "S. Ver")) {
            WasServerVersion aObj = new WasServerVersion () { User = myUser };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        } 
//        if (GUI.Button (myGUI.DivideRect (curRegis, colEA, colN++), "SvrVer")) {
//            WasServerVersion aObj = new WasServerVersion () { User = myUser };
//            //AgStt.GoToLoginAfterRegist = false;
//            aObj.messageAction = (int pInt) => {
//            };
//        }
//        if (GUI.Button (myGUI.DivideRect (curRegis, colEA, colN++), "UnRgst")) {
//            WasUnRegist aObj = new WasUnRegist () { User = myUser };
//            //AgStt.GoToLoginAfterRegist = false;
//            aObj.messageAction = (int pInt) => {
//            };
//        } 
        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Login 2   _____
        Rect curLogin = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;

        if (GUI.Button (myGUI.DivideRect (curLogin, colEA, colN++), "Login")) {
            WasLogin aObj = new WasLogin () { User = myUser, osVer = "1.1" };
            aObj.messageAction = (int pInt) => {
                Ag.LogIntenseWord (" Result :: " + pInt);
                if (pInt == 0) {
                    WasUserInfo bObj = new WasUserInfo () { User = myUser, flag = 1 };
                    bObj.messageAction = (int pInt2) => {
                        WasItemPrice cObj = new WasItemPrice () { User = myUser, DiscountOnly = false };
                        cObj.messageAction = (int pInt3) => {
                        };
                    };
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curLogin, colEA, colN++), "U:Info:f0")) {
            WasUserInfo aObj = new WasUserInfo () { User = myUser, flag = 0 };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (curLogin, colEA, colN++), "Review")) {
            WasReview aObj = new WasReview () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Login 2   _____

        Rect curRank = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (curRank, colEA, colN++), "Friend Rank")) {
            WasFriendRank aObj = new WasFriendRank () { User = myUser };
            aObj.arrFriendIDs.Add ("91278098233517152");
            aObj.arrFriendIDs.Add ("88214690633939121");
            aObj.arrFriendIDs.Add ("88712330645978192");
            aObj.arrFriendIDs.Add ("88159078716546208");
            aObj.arrFriendIDs.Add ("88159078716500000");
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curRank, colEA, colN++), "Itm:Pr")) {
            WasItemPrice aObj = new WasItemPrice () { User = myUser, DiscountOnly = false };
            aObj.messageAction = (int pInt) => {
            };
        } 

        if (GUI.Button (myGUI.DivideRect (curRank, colEA, colN++), "EvntList")) {
            WasEventList aObj = new WasEventList () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        } 



        GUI.Label (myGUI.GetRect (muiCol, muiRow++), " C/C/G : " + myUser.mCash1 + " _ " + myUser.mCash2 + " _ " + myUser.mGold);

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Purchase 5   _____
        Rect curR = myGUI.GetRect (muiCol, muiRow++);
        colEA = 4;
        colN = 0;
        if (GUI.Button (myGUI.DivideRect (curR, colEA, colN++), "Free")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser,
                itemType = "GloveFreeTime",
                itemTypeId = "GloveFreeDay",// "GloveFreeMonth",
                ea = 1,
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        if (GUI.Button (myGUI.DivideRect (curR, colEA, colN++), "Ticket")) {
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser, itemType = "TICKET", itemTypeId = "TicketNormal", ea = 1
            };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
        }
        if (GUI.Button (myGUI.DivideRect (curR, colEA, colN++), "Drink")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser, itemType = "DRINK", itemTypeId = "TeamBlueDrink", // "BlueDrink", // "GreenDrink",
                ea = 1
            };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
        }
        if (GUI.Button (myGUI.DivideRect (curR, colEA, colN++), "Ceremony")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser,
                itemType = "CEREMONY",
                itemTypeId = "CeremonySkill01",
                ea = 1
            };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
        }
        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Purchase   _____
        Rect rctPur = myGUI.GetRect (muiCol, muiRow++);
        colEA = 4;
        colN = 0;
        if (GUI.Button (myGUI.DivideRect (rctPur, colEA, colN++), "Msg")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser, itemType = "MESSAGE", itemTypeId = "EndMessage", ea = 1
            };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
        }
        if (GUI.Button (myGUI.DivideRect (rctPur, colEA, colN++), "Func")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser, itemType = "Func", ea = 1,
                itemTypeId = "FuncCardExtendD" //"FuncBackNumEdit : ",
            };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Purchase Item with TextField   _____
        Rect curRpur = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        itemType = GUI.TextField (myGUI.DivideRect (curRpur, colEA, colN++), itemType);
        itemTypeID = GUI.TextField (myGUI.DivideRect (curRpur, colEA, colN++), itemTypeID);

        if (GUI.Button (myGUI.DivideRect (curRpur, colEA, colN++), "Purchase")) {  // Purchase
            WasPurchaseItem aObj = new WasPurchaseItem () {
                User = myUser, itemType = itemType, itemTypeId = itemTypeID, ea = 1
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Purchase    _____
        Rect rctPrc = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (rctPrc, colEA, colN++), "Uniform")) {  // Purchase
            WasPurchaseUniform aObj = new WasPurchaseUniform () {
                User = myUser,
                uniformTypeID = "KickerUniformTop4", // "DefaultUniform",
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        if (GUI.Button (myGUI.DivideRect (rctPrc, colEA, colN++), "Costume")) {  // Purchase
            WasPurchaseCostume aObj = new WasPurchaseCostume () { User = myUser, costumeName = "RoseCostume"  };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        }
        if (GUI.Button (myGUI.DivideRect (rctPrc, colEA, colN++), "BuyGold")) {  // Purchase
            WasPurchaseGold aObj = new WasPurchaseGold () { User = myUser, Gold = 100 };
            aObj.messageAction = (int pInt) => {
            };
        } 


        if (GUI.Button (myGUI.DivideRect (rctPrc, colEA, colN++), "Card")) {  // Purchase
            WasPurchaseCard aObj = new WasPurchaseCard () { User = myUser, option = 1, eaNum = 1, buyType = 0,
                leagueType = "K", additionalBuyFlag = 1
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    ItemUse Drink / Scouter   _____
        Rect curRUse = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 3;
        if (GUI.Button (myGUI.DivideRect (curRUse, colEA, colN++), "UseDrink")) {  // Item Use
            WasItemUse aObj = new WasItemUse () { User = myUser, itemType = "DRINK", itemTypeId = "GreenDrink" };
            aObj.messageAction = (int pInt) => {
                Ag.LogString (" result :    >>> " + pInt.LogWith ("result is"));
            };
        }
        if (GUI.Button (myGUI.DivideRect (curRUse, colEA, colN++), "Scout")) {  // Scouter
            WasScouter aObj = new WasScouter () { User = myUser };
            aObj.messageAction = (int pInt) => {
                aObj = null;
            };
        }
        if (GUI.Button (myGUI.DivideRect (curRUse, colEA, colN++), "Add +")) {  // Scouter
            myUser.GetCardOrderOf (1).AddScouterValue (3, false);
        }

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Mail   _____
        Rect curB = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        if (GUI.Button (myGUI.DivideRect (curB, 3, colN++), "Mail")) {
            WasMailFetch aObj = new WasMailFetch () { User = myUser };
            aObj.messageAction = (int pInt) => {
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curB, 3, colN++), "M:Erase")) {
            WasMailErase aObj = new WasMailErase () { User = myUser, msgID1 = myUser.arrMail [0].WAS.msgID1, 
                msgID2 = myUser.arrMail [0].WAS.msgID2
            };
            aObj.messageAction = (int pInt) => {
            };
        }
        if (GUI.Button (myGUI.DivideRect (curB, 3, colN++), "M:Send")) {
            WasMailSend aObj = new WasMailSend () { User = myUser, friendID = "88214690633939121", itemTypeId = "BlueDrink", content = " JJJ !!! "
            };
            aObj.messageAction = (int pInt) => {
            };
        }

        muiRow++;

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Card 3   _____
        Rect curFetch = myGUI.GetRect (muiCol, muiRow++);
        if (GUI.Button (myGUI.DivideRect (curFetch, 4, 0), "F:Item : " + myUser.arrItem.Count)) { // Item
            WasItemInfo aObj = new WasItemInfo () { User = myUser };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curFetch, 4, 1), "F:Card")) { // Card
            WasCardUniformCostume aObj = new WasCardUniformCostume () { User = myUser, code = 240 };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
            //            myUser.CheckKickOrder ();
        } 
        if (GUI.Button (myGUI.DivideRect (curFetch, 4, 2), "F:Unif")) { // Uniform
            WasCardUniformCostume aObj = new WasCardUniformCostume () { User = myUser, code = 241 };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 
        if (GUI.Button (myGUI.DivideRect (curFetch, 4, 3), "F:Cstm")) { // Costume
            WasCardUniformCostume aObj = new WasCardUniformCostume () { User = myUser, code = 242 };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    return;
                }
            };
        } 

        //  _////////////////////////////////////////////////_    _____  Item  _____    Update   _____
        if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), "Msg Update :  " + myUser.arrItem.Count)) {
            // 아이템은 메시지만 업데이트 대상임.
            AmItem startMsg = myUser.arrItem.GetMemberWithCond ((AmItem iObj) => {
                return iObj.WAS.itemTypeID == "CeremonySkill01";  //"EndMessage"; //"StartMessage";  // 이렇게 조건을 지정하여 해당 아이템을 가져온다.
            });

            startMsg.WAS.applyID = -1;

            startMsg.WAS.msg = " again DoitAgain''' ";
            WasItemUpdate aObj = new WasItemUpdate () { User = myUser,
                itemObj = startMsg // 이렇게 업데이트 대상 아이템 <하나> 만 넣어준다.
            };
            aObj.messageAction = (int pInt) => {
            };
        } 

        //  _////////////////////////////////////////////////_    _____  DivideRect  _____    Update   _____
        Rect curRUp = myGUI.GetRect (muiCol, muiRow++);
        colN = 0;
        colEA = 4;
        if (GUI.Button (myGUI.DivideRect (curRUp, colEA, colN++), "Uniform " + myUser.arrUniform.Count)) {  // Update
            WasUniformUpdate aObj = new WasUniformUpdate () { User = myUser };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    break;
                }
            };
        }

        if (GUI.Button (myGUI.DivideRect (curRUp, colEA, colN++), "Cstm " + myUser.arrCostume.Count)) {  // Update

            myUser.arrCostume [0].WAS.cardId = 11;
            myUser.arrCostume [0].WAS.etcInfo = " Changed Info";

            WasCostumeUpdate aObj = new WasCostumeUpdate () { User = myUser };
            aObj.messageAction = (int pInt) => {
                switch (pInt) { // 0:성공
                case 0:
                    Ag.LogString (" result : Success ");
                    break;
                }
            };
        }
        if (GUI.Button (myGUI.DivideRect (curRUp, colEA, colN++), "Card " + myUser.arrCard.Count)) {  // Update
            //if (GUI.Button (myGUI.GetRect (muiCol, muiRow++), " < Card Update :: " + myUser.arrCard.Count + " >")) { // Update

            for (int kk = 0; kk < myUser.arrCard.Count; kk++) {
                myUser.arrCard [kk].mustUpdate = true;
            }

            List<AmCard> cardArr = myUser.GetUpdateCards ();
            if (cardArr.Count != 0) {
                WasCardUpdate aObj = new WasCardUpdate () { User = myUser, arrSendCard = null };
                aObj.messageAction = (int pInt) => {
                    switch (pInt) { // 0:성공, -1:캐쉬 부족, 1:잘못된 단위
                    case 0:
                        Ag.LogString (" result : Success ");
                        return;
                    }
                };
            }
        }

    
       

        #if UNITY_EDITOR
        #endif
    }
}