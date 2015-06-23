using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public delegate bool Dlgt_Gen_Obj_Bool<T> (T obj);
public delegate bool Dlgt_V_Bool ();
public delegate bool Dlgt_Vec_Bool (Vector3 pV);
public delegate int Dlgt_V_Int ();
public delegate void Dlgt_Int_V (int pInt);
public delegate void Dlgt_Bool_V (bool pLogged);
public delegate void Dlgt_GObj_Void (GameObject gObj);
public delegate void Dlgt_Float_V (float floatVal);
public delegate void Dlgt_String_V (string pStr);
public delegate void Dlgt_BoolString_V (bool pIsTrue,string pMsg);
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  static Fb  _____  Class  _____
public static class Fb
{
    // Don't Destroy
    public static bool NotDestroyIosCallBack = false, NotDestroyIosStoreKit = false;

    public static bool? JailBreakIOS;
    public static string AndroidRegistrationID;
}

public class GeneralFunction
{

    [DllImport ("__Internal")]
    private static extern void _NativeLog (string pString);

    public static string SIGN_INTENSE = 
        "SIGN_INTENSE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> \n";

    public static void SignIntenseLog (int pNum)
    {
        for (int i = 0; i < pNum; i++)
            GeneralFunction.NativeLog (SIGN_INTENSE);
    }

    public static void LogIntense (int pNum, bool pIsStart, string pName)
    {
        if (Application.platform == RuntimePlatform.Android)
            return;
        if (pIsStart)
            SignIntenseLog (pNum);
        GeneralFunction.NativeLog (">>>>>>>>>>>>>>>>>>>>>>>>>>>  >>>>>>>>>>>>>>>>>>>>>>>>>>>   " + pName);
        if (!pIsStart)
            SignIntenseLog (pNum);
    }

    public static void NativeLog (string pString)
    {
        if (Application.platform == RuntimePlatform.Android)
            return;
        // Call plugin only when running on real device
        if (Application.platform != RuntimePlatform.OSXEditor)
            _NativeLog (pString);
    }
}