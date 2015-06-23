using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    void SetCostumeNum ()
    {



        Ag.LogIntenseWord ("ENEMCARD costume Count" + EnemCard.arrCostumeInCard.Count  + "ENEMYUSER Costume Count" + Ag.NodeObj.EnemyUser.arrCostume.Count);
        Ag.LogIntenseWord ("ENEMCARD costume Count" + myCard.arrCostumeInCard.Count  + "ENEMYUSER Costume Count" + Ag.NodeObj.MyUser.arrCostume.Count);

        if (EnemCard.arrCostumeInCard.Count > 0) {
            Ag.LogIntenseWord ("ENEMCARD itemTypeid" + EnemCard.arrCostumeInCard [0].WAS.itemTypeId);

            Ag.LogIntenseWord ("ENEMCARD itemCardID" + EnemCard.arrCostumeInCard [0].WAS.cardId);
        }

        if (Ag.mgIsKick) {
            if (myCard.arrCostumeInCard.Count > 0) {
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes01") {
                    CostumeNum = 1;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes02") {
                    CostumeNum = 2;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes03") {
                    CostumeNum = 3;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes04") {
                    CostumeNum = 4;
                }
            }
            if (EnemCard.arrCostumeInCard.Count > 0) {
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves01") {
                    CostumeNumEnem = 1;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves02") {
                    CostumeNumEnem = 2;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves03") {
                    CostumeNumEnem = 3;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves04") {
                    CostumeNumEnem = 4;
                }
            }

        } else {
            if (myCard.arrCostumeInCard.Count > 0) {
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves01") {
                    CostumeNum = 1;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves02") {
                    CostumeNum = 2;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves03") {
                    CostumeNum = 3;
                }
                if (myCard.arrCostumeInCard [0].WAS.itemTypeId == "KeeperGloves04") {
                    CostumeNum = 4;
                }
            }
            if (EnemCard.arrCostumeInCard.Count > 0) {
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes01") {
                    CostumeNumEnem = 1;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes02") {
                    CostumeNumEnem = 2;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes03") {
                    CostumeNumEnem = 3;
                }
                if (EnemCard.arrCostumeInCard [0].WAS.itemTypeId == "KickerShoes04") {
                    CostumeNumEnem = 4;
                }
            }
        }
	
    }
}
