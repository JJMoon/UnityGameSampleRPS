using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class HtRsrcMan : MonoBehaviour
{
    string mScnName { get; set; }

    public HtRsrcMan (string pFolder)
    {
        mScnName = pFolder;
        //("HtRscrMan Creation :: of " + mScnName).HtLog();
    }

    public Texture2D xxGetTexture (string pTxtName)
    {
        try {
            return (Texture2D)Resources.Load (mScnName + "/" + pTxtName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("HtBaseObject.cs :: GetTexture Error.LOAD_TEXTURE ");
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            //AgStt.mError = AgStt.Error.LOAD_TEXTURE;
            return null;
        }
    }

    public Texture2D GetTextureIn (string pFolder, string pTxtName)
    {
        try {
            return (Texture2D)Resources.Load (pFolder + "/" + pTxtName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("HtBaseObject.cs :: GetTextureIn Error.LOAD_TEXTURE ");
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            //AgStt.mError = AgStt.Error.LOAD_TEXTURE;
            return null;
        }
    }

    public GameObject GetPrefabAt (string Folder, string pName)
    {
        try {
            return (GameObject)Instantiate (Resources.Load (Folder + "/" + pName));
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.LOAD_TEXTURE");
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }

    public GameObject xxGetPrefab (string pName)
    {
        try {
            Ag.LogString (mScnName + "/Prefab/" + pName);
            return (GameObject)Instantiate (Resources.Load (mScnName + "/Prefab/" + pName));
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.LOAD_TEXTURE >> " + pName);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }

    public GameObject GetPrefabIn (string pFolder, string pName)
    {
        try {
            return (GameObject)(Resources.Load (pFolder + "/" + pName));
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.LOAD_TEXTURE >>  " + pFolder + "/" + pName);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }

    public GameObject FindGameObject (string pName, bool pActive)
    {
        try {
            GameObject obj = GameObject.Find (pName).gameObject; 
            obj.SetActive (pActive);
            return obj;
            //return GameObject.Find(pName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.FindGameObject : " + pName);
            Ag.LogString ("Error.FindGameObject >>  " + pName, pWichtig: true);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }
    //   dicMenuList.Add ("Keeper_popup", mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_playerstatpopup/Panel_keeper/popup", false));
    //    public bool FindAndAddTo(CollectionBase colObj, GameObject pParent, string pName, bool pActive)
    //    {
    //        GameObject childObj = FindChild (pParent, pName, pActive);
    //
    //    }
    public GameObject FindChild (GameObject pParent, string pName, bool pActive)
    {
        try {
            GameObject obj = pParent.transform.FindChild (pName).gameObject; 
            obj.SetActive (pActive);
            return obj;
            //return GameObject.Find(pName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.FindChild" + pName);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }

    public void  DestoryGameObject (GameObject pObj)
    {
        DestroyObject (pObj);
    }

    public GameObject FindInFolder (string pFolder, string pName)
    {
        return GameObject.Find (pFolder + "/" + pName).gameObject;
    }

    public void AddComponentUISendMessage (GameObject pObj, GameObject pTargetObj, string pFolderName)
    {
        pObj.AddComponent<UIButtonMessage> ();
        pObj.GetComponent<UIButtonMessage> ().target = pTargetObj;
        pObj.GetComponent<UIButtonMessage> ().functionName = pFolderName;
    }
}
//  _////////////////////////////////////////////////_    _____   BaseObject   _____   Class   _____
//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Base Game Object  <<<<<
public class BaseObject : MonoBehaviour
{
    public GameObject mObj;
    public string myName, mFolder;
    // mFolder + "/" + theFolder + myName....
    public Dictionary<string, string> dicResourcesFold = new Dictionary<string, string> ();
    // folders of Main, Texture, GameObject, Animation
    public BaseObject ()
    {
    }

    public BaseObject (string pFolder, string pName, string pGameObjFolder)
    {
        mFolder = pFolder;
        myName = pName;
        dicResourcesFold.Add ("GameObj", pGameObjFolder);
		
        SetGameObj ();
    }
    /*
    public BaseObject( string pGameObjFolder, string pMainSourceFolder ) {
        dicResourcesFold.Add("GameObj", pGameObjFolder);
        dicResourcesFold.Add("Main", pMainSourceFolder);
    }
    
    public BaseObject( string pGameObjFolder, string pMainSourceFolder, string pTextureFolder ) {
        dicResourcesFold.Add("GameObj", pGameObjFolder);
        dicResourcesFold.Add("Main", pMainSourceFolder);
        dicResourcesFold.Add("Texture", pTextureFolder);
    } */
    //  ////////////////////////////////////////////////     Set Folders
    public void SetFolder (string pKey, string pTextureName)
    {
        if (dicResourcesFold.ContainsKey (pKey))
            return;
        dicResourcesFold.Add (pKey, pTextureName);
    }

    public void SetGameObj ()
    {
        string fullName;
        try {
            fullName = mFolder + "/" + dicResourcesFold ["GameObj"] + "/" + myName;
            mObj = (GameObject)Resources.Load (fullName);
        } catch {
        }
    }
    //  ////////////////////////////////////////////////     Get Methods
    public string GetFolder (string pKey)
    {
        return mFolder + "/" + dicResourcesFold [pKey];
    }

    public void AssignTexture ()
    { 
        try {
            string fullName = mFolder + "/" + dicResourcesFold ["Texture"] + "/" + myName;
            Debug.Log ("AssignTexture    .....   fullName :: " + fullName);
            mObj.renderer.material.mainTexture = (Texture2D)Resources.Load (fullName);
        } catch {
        }
    }

    public void AssignTexture (string pKey, string pLastName, int pMtrsIdx)
    { 
        try {
            string fullName = mFolder + "/" + dicResourcesFold [pKey] + pLastName;
            Debug.Log ("AssignTexture with Key / LastName / MaterialIndex fullName :: " + fullName);
            mObj.renderer.materials [pMtrsIdx].mainTexture = (Texture2D)Resources.Load (fullName);
        } catch {
        }
    }
}
//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  General Game Object  <<<<<
public class HtGameObj : BaseObject
{
    public List<GameObject> arrGameObj;

    public HtGameObj () : base ()
    {
        arrGameObj = new List<GameObject> ();
    }

    public HtGameObj (string pFolder, string pName, string pGameObjFolder) : base (pFolder, pName, pGameObjFolder)
    {
        arrGameObj = new List<GameObject> ();
    }
    //  ////////////////////////////////////////////////     Instantiate
    public void InstantiateAt (float pX, float pY, float pZ)
    {
        arrGameObj.Add ((GameObject)Instantiate (mObj, new Vector3 (pX, pY, pZ), new Quaternion (0, 0, 0, 0)));
    }
    //  ////////////////////////////////////////////////     Animation Manipulate..
    public void AnimaUpDown ()
    {
		
		
    }

    public void IdelAnima ()
    {
		
        Vector3 posi = arrGameObj [0].transform.position;
        posi.y += 0.1f;
        arrGameObj [0].transform.position = posi;
        //mObj.transform.position = posi;
    }
}



