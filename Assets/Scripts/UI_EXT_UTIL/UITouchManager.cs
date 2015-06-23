using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UITouchManager
{
    Camera muiCam;
    List<UIManagerBase> arrMan = new List<UIManagerBase>();

    GameObject muiHoveringObj = null;

    float muiDiagonalDist; // Option Obj is up to UIManager ..

    public UITouchManager (Camera pCam, float pDiagDist) //, float [] pRange)
    {
        muiCam = pCam;
        muiDiagonalDist = pDiagDist;
    }

    public void LogSortNum()
    {
        foreach (UIManagerBase man in arrMan) {
            man.LogSortNum ();
            Ag.LogNewLine (3);
        }
    }

    public void AddUiManager(AmBoxArea pWagu, int xN, int yN, bool? scrVert, bool reverse, string pName, 
                             bool pSwitchLock = false, bool pLimit2Inside = true, bool pEnableScrl = true)
    {
        UITileManager tileMan = new UITileManager (xN, yN, pWagu, muiDiagonalDist, scrVert,  pName, this, 
                                                   reverse:reverse, pSwitchLock:pSwitchLock, pLimit2Inside:pLimit2Inside, pEnableScrl:pEnableScrl);
        arrMan.Add (tileMan);

        tileMan.EventSetAlienToTouchManager += gObj => {
            string theName = (gObj == null)? " Null " : gObj.name;
            Ag.LogIntenseWord (" >>>>>    UI_Touch_Manager ::  Got the Alien " + theName );
            muiHoveringObj = gObj;
        };
    }

    public void AddACell(GameObject pObj, int x, int y, string kind, bool? pSortOrStock = null)
    {
        UIManagerBase curMan = arrMan [arrMan.Count - 1];
        Vector3 pstn;
        curMan.GetPosition (x, y, out pstn);
        pObj.transform.position = pstn;
        pObj.GetComponent<CuCell> ().muiSortOrStuck = pSortOrStock;
        pObj.CellCs ().myKind = kind;
        curMan.AddAMember (pObj);
    }

    //  ////////////////////////////////////////////////     ////////////////////////     >>>>> Mouse Do2wn Hold Up.... <<<<<
    public void MouseDown (Vector3 pPosition)
    {
        Vector3 tWorldPo = muiCam.GetScreenPosition (pPosition, 0);
        foreach (UIManagerBase man in arrMan) {
            man.MouseDown(tWorldPo, pPosition);
        }
    }

    public void MouseHold (Vector3 pPosition)
    {
        Vector3 tWorldPo = muiCam.GetScreenPosition (pPosition, 0);
        //("  UITouchManager :: MouseHold  >>>>>       " + tWorldPo.LogWith ("world Po") + pPosition.LogWith("UI Po")).HtLog ();
        foreach (UIManagerBase man in arrMan) {
            man.MouseHold(tWorldPo, pPosition, muiHoveringObj);
        }
    }

    public void MouseUp ()
    {
        foreach (UIManagerBase man in arrMan) {
            man.MouseUp();
        }
    }

    public void UpdateAction()
    {
        foreach (UIManagerBase man in arrMan) {
            man.UpdateAction ();
        }
    }


}
