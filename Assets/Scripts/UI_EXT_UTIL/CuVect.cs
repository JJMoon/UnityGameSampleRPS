using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

//  _////////////////////////////////////////////////_    _///////////////////////_    _____  CuVect  _____  Class  _____
public class CuVect
{
    AmUiOption option;
    CuFrameOption mOptObj;

    public enum TempName
    {
        SCATTER,
        COMPRESS,
    }

    Vector3 initV, targV;
    public Vector3 SaveV;
    // initV : 생성 시 위치 기억용.
    // targV : 애니메이션의 타겟.
    // SaveV : 현재 위치. 
    public Vector3 mTempV { private get; set; }

    GameObject parent;
    bool IsPosition;
    Dictionary<TempName, Vector3> dicTempVect;

    public Vector3 MyVector {
        // 현재 위치, SaveV 강제 세팅. targV를 바꾸지 않으면 targV로 애니메이션 이동함.
        set { 
            if (IsPosition)
                parent.transform.position = value;
            else
                parent.transform.localScale = value;
        }
        get { 
            if (IsPosition)
                return parent.transform.position;
            else
                return parent.transform.localScale;
        }
    }

    public Vector3 Targ {
        set { targV = value; }
        get { return targV;}
    }
    //  _////////////////////////////////////////////////_    _///////////////////////_    _____  Creation  _____  Init  _____
    public CuVect (GameObject par, CuFrameOption opt, bool IsPosi = true)
    {
        parent = par;
        if (IsPosi)
            SaveV = targV = initV = par.transform.position;
        else 
            SaveV = targV = initV = par.transform.localScale;
        IsPosition = IsPosi;
        mOptObj = opt;



        //(" CuVect :: " + parent.name.LogWith("name") + mOptObj.optMoveAniSpd.LogWith ("OptObj")).HtLog ();
    }




    public bool AmIAnimating = true;

    public void AniToTarget (CuFrameOption optO) // Animation               _< Update >_ 
    { // Position Only
        mOptObj = optO;
        //if (parent.name == "LW_BOX_1")

        if (AmIAnimating) {
            MyVector = MyVector.IntDivide (targV, mOptObj.optMoveAniSpd, 10);
            float dist = Vector3.Distance (MyVector, targV);
            if (dist < mOptObj.LimitOfPstnAnimation) {  // Finishing Animation
                SaveV = MyVector = targV;
                AmIAnimating = false;
                //ShowMyself (" Animation Finished !!!  ");
            }
        } 
        //Ag.LogString (" CuVect :: AniToTarget  >>   [ " + parent.name.LogWith("name") + MyVector.LogWith ("MyVect") + SaveV.LogWith("SaveV") );
    }

    //  _////////////////////////////////////////////////_    _____  Scroll  _____  Init  _____
    public void Scroll (Vector3 pVec)
    {
        parent.transform.position += pVec;
        SetCurPosition ();
        targV += pVec; // Separate ..
    }

    public void SetCurPosition () // called from Cell                       _< Scroll >_
    {
        //SetTargetPosition (gameObject.Move (pVect));  ....  CuCell Source ...
            //  -->>  Pstn.Targ = pNewCo;   // Move Target
        //Pstn.SetCurPosition ();           // Move MyVect  &   SaveV ...

        //(" CuVect :: SetCurPosition      AmIAnimating   " + AmIAnimating).HtLog ();

        if (!AmIAnimating)
            SaveV = parent.transform.position;
    }










    public void SetTargetZcodi (float pVal)
    {
        Ag.LogString ("  SetTarget Z codi " + pVal);
        targV.z = pVal;
        parent.transform.position.Set (MyVector.x, MyVector.y, pVal);
    }

    public void SetZeroTargZ ()
    {
        //Ag.LogString ("  SetZeroTargZ ________    0     _____" );
        targV.z = 0;
    }


    public void AniSize (CuFrameOption optO)
    {
        //Ag.LogString (" CuVect :: AniSize  >>   [ " + optObj.optMoveAniSpd.LogWith ("Opt"));
        MyVector = MyVector.IntDivide (targV, optO.optSizeAniSpd, 10);
    }

    public CuVect (GameObject par, AmUiOption opt, bool IsPosi = true)  // deprecate 
    {
        parent = par;
        if (IsPosi)
            SaveV = targV = initV = par.transform.position;
        else 
            SaveV = targV = initV = par.transform.localScale;
        option = opt;
        this.IsPosition = IsPosi;
        //(" CuVect :: CuVect  >>   [ " + parent.name + " ]   curV : " + SaveV + " ,  tarV : " + targV + "  {{{   Creation   }}}" ).HtLog();
    }

    public void Go2Init ()
    {
        targV = initV;
    }

    public void Move2TargetNow ()
    {
        parent.transform.position = targV;
    }
    //  _////////////////////////////////////////////////_    _____  Swap  _____  SaveV  _____
    public void SwapCurposiAndSetTargetWith (CuVect pT)
    {
        //Ag.LogIntenseWord (" SwapCurposiAndSetTargetWith " + SaveV + "  /  " + pT.SaveV);
        //Ag.LogString ("  This Vector : " + MyVector + " / " + pT.MyVector);
        ShowMyself (" SwapCurposiAndSetTargetWith  ");

        Ag.Swap<Vector3> (ref SaveV, ref pT.SaveV);
        Return2SavedVect ();
        pT.Return2SavedVect ();
    }

    public void Return2SavedVect (bool ForceImmediate = false) // called from  >>  Cell :: Back2InitSize  *** Size 에서만 이용 OK..
    {
        ShowMyself (" Return2SavedVect ");
        targV = SaveV;
        if (ForceImmediate)
            MyVector = targV;
    }

    public void ShowMyself (string pCmt = "CuVect")
    {
        string kind = IsPosition ? "Position" : "__Size__";

        Ag.LogString ("  CuVect :: ShowMyself >>> " + kind + " <<<   " + pCmt);
        Ag.LogString (initV.LogWith ("InitV") + SaveV.LogWith ("\tSaveV") + Targ.LogWith ("\tTarg") + 
            MyVector.LogWith ("\tMyVector") + "   " + pCmt);

    }
    //  _////////////////////////////////////////////////_    _____  ..  _____  Temp  _____
    //    void xxSetTempVect (TempName pName, Vector3 pV)
    //    {
    //        if (dicTempVect == null)
    //            dicTempVect = new Dictionary<TempName, Vector3> ();
    //
    //        if (dicTempVect.ContainsKey (pName))
    //            dicTempVect [pName] = pV;
    //        else
    //            dicTempVect.Add (pName, pV);
    //    }
    //
    //    public void xxSet_Go2Temp (TempName pCase, Vector3 pV)
    //    {
    //        SaveV = parent.transform.position; // 현재의 위치는 나중에 돌아올 걸 대비해서 저장.
    //        Targ = pV;  // Set 하고,,
    //        xxSetTempVect (pCase, pV);  // Temp 에 저장하고. 
    //    }
    //
    //    public void xxGo2Temp (TempName pName, bool save2curV = false)
    //    {
    //        if (save2curV)
    //            SaveV = MyVector;
    //        if (dicTempVect.ContainsKey (pName))
    //            Targ = dicTempVect [pName];
    //    }
}