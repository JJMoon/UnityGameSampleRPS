// [2013:3:13:MOON] Started ..
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;

public class AmTexture
{
    public string mName;
    public byte mTexture, mR, mG, mB;
    // ID...
    public AmTexture ()
    {
        
    }

    public void CopyFrom (AmTexture pFrom)
    {
        mName = pFrom.mName;
        mR = pFrom.mR;
        mG = pFrom.mG;
        mB = pFrom.mB;
    }

    public string GetRGBString ()
    {
        return mR.ToString () + "_" + mG + "_" + mB;
    }
    //public AmTexture mShirt, mPants, mShoes, mSocks, mGlove; // Apply to All Players... Kickers, Keeper both...
    //public AmTexture mGlShirt, mGlPants, mGlShoes, mGlSocks, mGlGlove;
    public void AiSetRandom (string pCase, byte pGreen)
    {

        switch (pCase) {
        case "mShirt":
            mR = (byte)AgUtil.RandomInclude (1, 4);
            mG = (byte)AgUtil.RandomInclude (1, 8); //rObj.Next(1, 9);
            mB = (byte)AgUtil.RandomInclude (1, 8); //rObj.Next(1, 9);
            break;
        case "mGlShirt":
            mR = (byte)AgUtil.RandomInclude (1, 3); //rObj.Next(1, 4);
            mG = (byte)AgUtil.RandomInclude (1, 8); //rObj.Next(1, 9);
            mB = (byte)AgUtil.RandomInclude (1, 8); //rObj.Next(1, 9);
            break;
        case "mPants":
        case "mGlPants":
            mR = (byte)AgUtil.RandomInclude (1, 4); //rObj.Next(1, 5);
            mG = (byte)AgUtil.RandomInclude (1, 8); //rObj.Next(1, 9);
            if (mR == 1)
                mB = 1;
            else
                mB = (byte)AgUtil.RandomInclude (1, 8);//rObj.Next(1, 9);
            break;
        case "mSocks":
        case "mGlSocks":
            mR = (byte)AgUtil.RandomInclude (1, 2);//rObj.Next(1, 3);
            mG = (byte)AgUtil.RandomInclude (1, 8);//rObj.Next(1, 9);
            if (mR == 1)
                mB = 1;
            else
                mB = (byte)AgUtil.RandomInclude (1, 8);//rObj.Next(1, 9);
            break;
        case "mGlove":
            break;
        case "mShoes":
            break;
        case "mGlShoes":
            break;
        case "mGlGlove":
            break;
        }
        if (pGreen > 0)
            mG = pGreen;

        (" Texture R/G/B " + pCase + "    \t " + mR + " / " + mG + " / " + mB).HtLog ();  
    }
}

