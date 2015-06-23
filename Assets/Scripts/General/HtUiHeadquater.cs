// [2013:1:10:MOON] Started
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Error { LOAD_TEXTURE, NET_FAIL, ETC, NORMAL };

//  ////////////////////////////////////////////////     ////////////////////////     >>>>>  UI Headquarter  <<<<<
public class HtUiHeadquater
{
    
    
    public Error mError { get; set; }
    
    public enum HierArchy { A, B, C, D};
    
    //List<string> arrActiveScene = new List<string>();
    Dictionary<string, AmSceneBase> dicScene = new Dictionary<string, AmSceneBase>();
    Dictionary<string, AmSceneBase> dicDeactivated = new Dictionary<string, AmSceneBase>();
    //AmSceneBase mCurActiveScene;
    
    //  ////////////////////////////////////////////////     Creation ..
    public HtUiHeadquater ()
    {
        
    }
    
    
    //  ////////////////////////////////////////////////     Add Scene ..
    public void AddScene(AmSceneBase pSceneObj)
    {
        string scnName = pSceneObj.GetType().ToString();

        Ag.LogIntense (3, true);
        Ag.LogString("________________________________________________________________________________ UI HQ _____");
        Ag.LogString("/HQ//HQ//HQ//HQ/  HtUiHeadquater  :: ]]] Add Scene [[[  >>>>> " + scnName + " <<<<<");
        Ag.LogString("________________________________________________________________________________ UI HQ _____");
        Ag.LogIntense (3, false);
        if (!dicScene.ContainsKey(scnName))
            dicScene.Add(scnName, pSceneObj);
        pSceneObj.muiActive = true;
        //DeActivateOther(pSceneObj);  # Active Scene can be MULTILPLE !!! ... 
    }
    
    public void DetachScene(AmSceneBase pSceneObj) // Called from AmSceneBase.OnDisable ..
    {
        dicScene.Remove(pSceneObj.GetType().ToString());
    }
    
    public void DeActivateOther(AmSceneBase pNewObj) 
    {
        foreach(KeyValuePair<string,  AmSceneBase> curObj in dicScene) {
            if (curObj.Value != pNewObj)
                curObj.Value.muiActive = false;
        }
    }
    
    //  ////////////////////////////////////////////////     Error Happens ..
//    public void FatalError(AmNetBase pNetObj, Error pError)
//    {
//        mError = pError;
//        var keyValueArr = from scnObj in dicScene where scnObj.Value.muiActive == true select scnObj;
//        foreach( KeyValuePair<string, AmSceneBase> obj in keyValueArr ) {
//            dicDeactivated.Add(obj.Key, obj.Value);
//            obj.Value.muiActive = false;
//        }
//    }
//    
    public void RestoreDeactivatedScenes()
    {
        foreach( KeyValuePair<string, AmSceneBase> obj in dicDeactivated ) {
            obj.Value.muiActive = true;
        }
        dicDeactivated.Clear();
        AgStt.muiHQ.mError = Error.NORMAL;
    }
    

}

/*
struct UImsg
    {
        public HierArchy mHier;
        public int mHierarchy;
        public string mMsg;
    };
    
    Dictionary<string, UImsg> dicUImsg = new Dictionary<string, UImsg>();
 
 
    public void AddUiMsg( string pStr) {
        UImsg msgObj = new UImsg();
        msgObj.mHierarchy = 35;
        msgObj.mMsg = pStr;
        
        HierArchy a = HierArchy.A;
        HierArchy b = HierArchy.B;
        
        if (a < b) 
            Ag.LogString("a < b" + a + ", " + b);
        else
            Ag.LogString("No a < b");
        
        dicUImsg.Add("key", msgObj);
    }
    
    public bool ThisIsTrue() {
        return true;
    }
 //*/