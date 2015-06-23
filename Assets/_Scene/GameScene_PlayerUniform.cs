using UnityEngine;
using System.Collections;

public partial class GameScene : AmSceneBase
{
    Object[] subKickerShirts, subKeeperShirts, subPants, subSocks;
    Object[] EnemysubKickerShirts, EnemysubKeeperShirts, EnemysubPants, EnemysubSocks;
    Texture KickermShirts, KickerPants, KickermSocks, KeeperShirts, KeeperPants, KeeperSocks;
    ProceduralMaterial mProcedureMat;

    void ReadmyTexture ()
    {
        subKeeperShirts = Resources.LoadAll ("Textures/Substance/Keepershirts", typeof(ProceduralMaterial));
        subKickerShirts = Resources.LoadAll ("Textures/Substance/Kickershirts", typeof(ProceduralMaterial));
        subPants = Resources.LoadAll ("Textures/Substance/Pants", typeof(ProceduralMaterial));
        subSocks = Resources.LoadAll ("Textures/Substance/Socks", typeof(ProceduralMaterial));

        EnemysubKeeperShirts = Resources.LoadAll ("Textures/EnemySubstance/Keepershirts", typeof(ProceduralMaterial));
        EnemysubKickerShirts = Resources.LoadAll ("Textures/EnemySubstance/Kickershirts", typeof(ProceduralMaterial));
        EnemysubPants = Resources.LoadAll ("Textures/EnemySubstance/Pants", typeof(ProceduralMaterial));
        EnemysubSocks = Resources.LoadAll ("Textures/EnemySubstance/Socks", typeof(ProceduralMaterial));
    }
    void UNiformSetColorColor (string pStr, int pInt) {
        switch (pInt) {
        case 0:
            Uniform_SetColor (pStr, new Color (0.06f, 0.06f, 0.06f));
            break;
        case 1:
            Uniform_SetColor (pStr, new Color (0.18f, 0.18f, 0.18f));
            break;
        case 2:
            Uniform_SetColor (pStr, new Color (1, 1, 1));
            break;
        case 3:
            Uniform_SetColor (pStr, new Color (0.49f, 0, 0));
            break;
        case 4:
            Uniform_SetColor (pStr, new Color (0.81f, 0.14f, 0));
            break;
        case 5:
            Uniform_SetColor (pStr, new Color (0.81f, 0.59f, 0));
            break;
        case 6:
            Uniform_SetColor (pStr, new Color (0.89f, 0.87f, 0));
            break;
        case 7:
            Uniform_SetColor (pStr, new Color (0.25f, 0.47f, 0));
            break;
        case 8:
            Uniform_SetColor (pStr, new Color (0.39f, 0.81f, 0.98f));
            break;
        case 9:
            Uniform_SetColor (pStr, new Color (0.11f, 0.4f, 0.79f));
            break;
        case 10:
            Uniform_SetColor (pStr, new Color (0, 0.26f, 0.79f));
            break;
        case 11:
            Uniform_SetColor (pStr, new Color (0.27f, 0.11f, 0.79f));
            break;
        }
    }



    void Uniform_SetColor (string pLine, Color pColor)
    {

        mProcedureMat.SetProceduralColor (pLine, pColor);
        if ("outputcolor_1" == pLine)
        mProcedureMat.RebuildTextures ();
    }


    void SetTextureCharacter ()
    {

        if (Ag.mgIsKick) {

            mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.NodeObj.MyUser.arrUniform [0].Kick.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Kick.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Kick.Shirt.ColSub);
            KickermShirts = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = KickermShirts;
            mProcedureMat = (ProceduralMaterial)subPants [Ag.NodeObj.MyUser.arrUniform [0].Kick.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Kick.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Kick.Pants.ColSub);
            KickerPants = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = KickerPants;
            mProcedureMat = (ProceduralMaterial)subSocks [Ag.NodeObj.MyUser.arrUniform [0].Kick.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Kick.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Kick.Socks.ColSub);
            KickermSocks = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = KickermSocks;
            mProcedureMat = (ProceduralMaterial)subKeeperShirts [Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Shirt.ColSub);
            KeeperShirts = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [0].mainTexture = KeeperShirts;
            mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Pants.ColSub);
            KeeperPants = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [1].mainTexture = KeeperPants;
            mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Keep.Socks.ColSub);
            KeeperSocks = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [2].mainTexture = KeeperSocks;


        } else {
            mProcedureMat = (ProceduralMaterial)subKeeperShirts [Ag.NodeObj.MyUser.arrUniform [0].Keep.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Keep.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Keep.Shirt.ColSub);
            KeeperShirts = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [0].mainTexture = KeeperShirts;
            mProcedureMat = (ProceduralMaterial)subPants [Ag.NodeObj.MyUser.arrUniform [0].Keep.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Keep.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Keep.Pants.ColSub);
            KeeperPants = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [1].mainTexture = KeeperPants;
            mProcedureMat = (ProceduralMaterial)subSocks [Ag.NodeObj.MyUser.arrUniform [0].Keep.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.MyUser.arrUniform [0].Keep.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.MyUser.arrUniform [0].Keep.Socks.ColSub);
            KeeperSocks = mProcedureMat.mainTexture;
            mPlayerKeeper.transform.FindChild ("uniform").renderer.sharedMaterials [2].mainTexture = KeeperSocks;
            mProcedureMat = (ProceduralMaterial)subKickerShirts [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Shirt.ColSub);
            KickermShirts = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = KickermShirts;
            mProcedureMat = (ProceduralMaterial)EnemysubPants [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Pants.ColSub);
            KickerPants = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = KickerPants;
            mProcedureMat = (ProceduralMaterial)EnemysubSocks [Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.Texture - 1];
            UNiformSetColorColor ("outputcolor", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.ColMain);
            UNiformSetColorColor ("outputcolor_1", Ag.NodeObj.EnemyUser.arrUniform [0].Kick.Socks.ColSub);
            KickermSocks = mProcedureMat.mainTexture;
            mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = KickermSocks;

        }
    }
}


