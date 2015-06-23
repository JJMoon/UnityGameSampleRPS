
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


public class AmStage : object {
    public Action mAnimaPlay;
    public StateArray arrTheState;
 
    //  ////////////////////////////////////////////////     Public
    public bool mAmIGoing = true, mIsTouched;   // Cursor Direction
    public bool mIsTouchEnable = true;

    public byte mRsltDirec, mRsltSkill;
    public float mCursorPosition, mPrevPosition, mPrevTime;
    
    //  ////////////////////////////////////////////////     Private
    float mtInit;  // Time
    
    //  ////////////////////////////////////////////////     Creating...
    public AmStage () {
        
    }
    
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  Touch Process  <<<<<
    public bool TouchProcess() {
        string curStage = CurStt();

        if (!mIsTouchEnable)
            return false;

        //Ag.LogStartWithStr (2, "  Just Touched :: Stage  >>>  " + curStage + "  Am I Goint " + mAmIGoing + "   Cursor Position  " + mCursorPosition + "\n");
        switch (curStage) {
        /* case "MidPaus":
            if (Ag.mgIsKick) return false;
            mIsTouched = true;
            Ag.LogString("TouchProcess.. Defence Direction " );
            break;
        case "PreGame":  // */
        case "GameDir":
        case "GameSkl":
            if ( mAmIGoing ) return false;
            //Ag.LogIntenseWord(" Effect Touched Here !! ");
            //mCursorPosition = GetCursorPosition();
            Ag.LogDouble ("  Just Touched :: Stage  >>>  " + curStage + "  Am I Goint " + mAmIGoing + "   Cursor Position  " + mCursorPosition + "\n");
            mIsTouched = true;
            Ag.LogDouble("TouchProcess.. mCurPosition is " + mCursorPosition.ToString());
            return true;    // Effective Touch
        } 
        return false;       // Useless Touch
    }    
    
        
    string CurStt() {
        if ( arrTheState != null )  return arrTheState.GetCurStateName();
        else                        return "";
    }
    
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  State Query   <<<<<
    public bool IsCountDown() {
        if ( CurStt() == "CountDn" ) return true;
        else return false;
    }
    
    public bool WillDrawText() {
        string stt = CurStt();
        //Begin, CountDn, PreGame, GameDir, MidPaus, GameSkl,  AftPaus, NetWait, AnimaPlay, Ceremony, GameFinish 
        if ( stt == "Begin" ||  stt == "CountDn" ||  stt == "MidPaus" ||  stt == "AftPaus" ||  stt == "Ceremony" )
            return true;
        return false;
    }
    
    //  ////////////////////////////////////////////////
    public bool WillDrawDirection() {
        //Ag.LogString("Cur State  >>>" + CurStt() );
        
        if (Ag.mgIsKick) {
            if ( CurStt() == "GameDir" || CurStt() == "MidPaus" || CurStt() == "PreGame" || CurStt() == "BeforeDirPotion" || CurStt() == "MidPausBiggerGamdDir" || 
                CurStt() == "GameDirStopped" || CurStt() == "Replay" )// ljk 11/14
                return true;
        } else {
            if ( CurStt() == "GameDir" || CurStt() == "MidPaus" || CurStt() == "PreGame" || CurStt() == "BeforeDirPotion" || CurStt() == "MidPausBiggerGamdDir" || CurStt() == "Replay")// ljk 11/14
                return true;
        }
        return false;
    }

    //  ////////////////////////////////////////////////
    public bool WillInputDrag() {
		string stt = CurStt();
        if ( !Ag.mgIsKick && (stt == "GameDir" || stt == "PreGame" || CurStt() == "BeforeDirPotion" || CurStt() == "MidPausBiggerGamdDir") || CurStt() == "Replay" ) //ljk 11/26 
            return true;
        return false;
    }

    //  ////////////////////////////////////////////////
    public bool WillDrawSkill() {
		string stt = CurStt();
        if ( stt == "GameSkl" || stt == "AftPaus" || stt == "MidPausPotion" || stt == "MidPausBiggerPotion"|| stt =="NetWait" || stt == "KpDirBar" || stt == "ReplaySkl") //LJk 0831
            return true;
        return false;
    }

    //  ////////////////////////////////////////////////
    public bool WillDrawCursor() {
        return ( WillDrawDirection() || WillDrawSkill() );
        
        //if ( CurStt() == "GameDir" || CurStt() == "MidPaus" || CurStt() == "GameSkl" || CurStt() == "AftPaus" )
    }
    
    //  ////////////////////////////////////////////////     
    public bool IsCursorMoving() {
        if ( CurStt() == "GameSkl" || CurStt() == "GameDir"  )
            return true;
        return false;
    }
    
    float mT, mLinePosi, mA, mLinTime, mLinVel;
    //  ////////////////////////////////////////////////     Cursor Moving  ... Initialize.. 
    public void InitCursorMove( float pTime, float pLinearPosi ) {  //  related to Cursor Moving... Called by Exit Action..
        //Ag.LogIntense(3, true);
        Ag.LogStartWithStr (5, "m>>>>>>>>>>>m m>>>>>>>>>>>     InitCursorMove     >>>>>>>>>>>m m>>>>>>>>>>>m  ");
        Ag.LogString("Init Cursor Move   Swipe Time:  " +  pTime + "    LinearPosi:  " + pLinearPosi );
        mAmIGoing = true; mIsTouched = false; mCursorPosition = 1.0f;
        mtInit  = -1f;
        
        mT = pTime; mLinePosi = pLinearPosi;
        mA = 1000f / (mT * mT);
        mLinTime = mT - Mathf.Sqrt( (1000 - mLinePosi) / mA ) ;
        mLinVel = 2 * mA * ( mT - mLinTime );
    }
    
    float TouchedPos;
    
    //  ////////////////////////////////////////////////     ////////////////////////     >>>>>  GetCursorPosition  <<<<<    
    public float GetCursorPosition() {
        if ( CurStt() == "AftPaus" || CurStt() == "MidPaus" )  { return mCursorPosition; }
        if ( mIsTouched ) { 
            if (mCursorPosition > 998)
                mCursorPosition = 998;
            return mCursorPosition; 
        }
        
        //float t = 0.8f, linPos = 70f;

        if (mtInit < 0) { // Started...
            mtInit = Time.realtimeSinceStartup;
            return 0f;
        }
        
        float delTime ;
        
        delTime = ( Time.realtimeSinceStartup - mtInit );
        
        //linTime = t - Mathf.Sqrt( (100 - linPos) / a ) ;
        //mLinVel = 2 * a * ( t - linTime );
        
        float linDist = mLinVel * delTime; 
        
		mPrevPosition4Test = mPrevPosition;
		
        if ( mAmIGoing &&  linDist < mLinePosi ) mCursorPosition = linDist;
        else {
            float linPastTime = mLinePosi / mLinVel;
            mCursorPosition = - mA * Mathf.Pow( delTime - (mT + linPastTime - mLinTime), 2) + 1000f;
        }
        
        //float vel = (mCursorPosition - mPrevPosition) / ( delTime - mPrevTime );
        //Debug.Log( "linTime " + linTime + " ,  linVel " + linVel + "  delTime : " + delTime );
        //Debug.Log("Time " + delTime + ",  Goint? > " + mAmIGoing + " , \t  Vel : > " + vel + " ,  \t CursorPosition :: >> " + mCursorPosition + "\n" );
        
        if ( mAmIGoing && 100 < mCursorPosition ) {
            if ( mCursorPosition < mPrevPosition ) mAmIGoing = false;
        }
        
        mPrevPosition = mCursorPosition;    mPrevTime = delTime;
        
        if ( mCursorPosition < -1f )  {
            mIsTouched = true;
            mCursorPosition = -1f;
        }
        
        return mCursorPosition;
    }
    
	public float mPrevPosition4Test;
    
}
