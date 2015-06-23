using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  FpLogin  _____  Class  _____
public class FpLoginObj
{
    /* Interface to native implementation */
    [DllImport ("__Internal")]
    private static extern void _TimeCheck ();

    [DllImport ("__Internal")]
    private static extern void _CheckJailBreak ();
    /* Public interface for use inside C# / JS code */
    // Starts lookup for some bonjour registered service inside specified domain
    public virtual void Login ()
    {
    }

    public void TimeCheck ()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
            _TimeCheck ();
    }
    // Hacking Related
    public void CheckRootingJailbreak ()
    {
        GeneralFunction.LogIntense (3, false, "Fp KakaoIos :: Check JailBreak Start _");
        _CheckJailBreak ();
    }
}

