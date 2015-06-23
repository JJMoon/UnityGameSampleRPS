using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

public partial class AmSceneBase : MonoBehaviour
{
    public GameObject FindGameObject (string pName, bool pActive)
    {
        try {
            GameObject obj = GameObject.Find (pName).gameObject; 
            obj.SetActive (pActive);
            return obj;
            //return GameObject.Find(pName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.FindGameObject");
            Ag.LogString ("Error.FindGameObject >> " + pName, pWichtig: true);
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
            Ag.LogString ("Error.GetPrefabAt >> " + Folder + pName, pWichtig: true);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }

    public GameObject FindMyChild (GameObject pParent, string pName, bool pActive)
    {
        try {
            GameObject obj = pParent.transform.FindChild (pName).gameObject; 
            obj.SetActive (pActive);
            return obj;
            //return GameObject.Find(pName);
        } catch {
            // Exception .... 
            Ag.LogIntenseWord ("Error.FindChild" + pName);
            Ag.LogString ("Error.FindChild >> " + pName, pWichtig: true);
            //AgStt.muiHQ.FatalError(null, Error.LOAD_TEXTURE);
            return null;
        }
    }
}