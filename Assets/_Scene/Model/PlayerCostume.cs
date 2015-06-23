using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCostume : MonoBehaviour
{
    public Dictionary<string, Texture2D> dicCostume;
    public static PlayerCostume _instance;

    public static PlayerCostume instance {
        get {
            if (!_instance) {
                _instance = GameObject.FindObjectOfType (typeof(PlayerCostume)) as PlayerCostume;
                if (!_instance) {
                    GameObject container = new GameObject ();
                    container.name = "PlayerCostume";
                    _instance = container.AddComponent (typeof(PlayerCostume)) as PlayerCostume;
                    DontDestroyOnLoad (_instance);
                }
            }
            
            return _instance;
        }
    }
    // Use this for initialization
    void Awake ()
    {
        Ag.LogString ("   Start    ");
        dicCostume = new Dictionary<string, Texture2D> ();

        ResourceLoad ();
    }

    void ResourceLoad ()
    {
        try {
            dicCostume.Add ("KeeperGloves01", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/glove1"));
            dicCostume.Add ("KeeperGloves02", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/glove2"));
            dicCostume.Add ("KeeperGloves03", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/glove3"));
            dicCostume.Add ("KeeperGloves04", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/glove4"));


            dicCostume.Add ("KickerShoes01", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/Shoes1"));
            dicCostume.Add ("KickerShoes02", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/Shoes2"));
            dicCostume.Add ("KickerShoes03", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/Shoes3"));
            dicCostume.Add ("KickerShoes04", (Texture2D)Resources.Load ("Textures/texture_shoes_glove/Shoes4"));
        } catch {
            Debug.Log ("Error");
        }
    }

    Texture2D GetPlayerCostume (string pCostumeName)
    {
        return dicCostume [pCostumeName];
    }

    public void SetCostumeToPlayer (bool pKickerflag, GameObject GObj, string CostumeName)
    {
        Ag.LogStartWithStr (2, " PlayerCostume :: SetCostumeToPlayer   Kicker ? " + pKickerflag + " Gobj " + GObj.name + "    CostumeName :" + CostumeName);

        if (pKickerflag) {
            GObj.transform.FindChild ("Clothes").renderer.sharedMaterials [3].mainTexture = dicCostume [CostumeName];
        } else
            GObj.transform.FindChild ("keeper_groverpart").renderer.material.mainTexture = dicCostume [CostumeName];

        Ag.LogString (" SetCostume OK ");
    }
    // Update is called once per frame
    void Update ()
    {
	
    }
}
