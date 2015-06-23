// [2012:10:10:MOON] Heart Beat
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AgTime
{
    //: MonoBehaviour {
    DateTime mpMarkTime, TargetDT;
    //float mpTargetTime;
    bool? EventFired;
    public Action dlgtFinished;
    //private Hashtable dicOneLog = new Hashtable();
    //private IDictionary<int, string> dicOneLog =
    //new IDictionary<int, string>();
    public AgTime ()
    {
        //Ag.LogIntenseWord("  AgTime is Generated  ");

        dlgtFinished += delegate {
            Ag.LogIntenseWord (" Log Event ");
        };
    }

    public void ResetLogOnce ()
    {
        //dicOneLog = new Hashtable();
    }

    public void WaitTimeFor (int hor, int min, int sec)
    {
        mpMarkTime = DateTime.Now;
        TargetDT = mpMarkTime.AddSeconds (sec).AddMinutes (min).AddHours (hor);
    }

    public void WaitTimeFor (float pSeconds)
    {
        mpMarkTime = DateTime.Now;
        TargetDT = mpMarkTime.AddSeconds (pSeconds);
        //mpTargetTime = pSeconds;


    }
    // Check if the time is OUT... returns Ture if Finished...
    public bool DidTimerFinished ()
    {
        TimeSpan spanT = TargetDT - DateTime.Now;
        if (spanT.TotalMilliseconds > 0)
            return false;
        else
            return true;

        //float timeDue =  Time.timeSinceLevelLoad - mpMarkTime;
        //TimeSpan spanT = DateTime.Now - mpMarkTime;
//        
//        if (spanT.TotalMilliseconds > mpTargetTime * 1000f) {
//            //mpMarkTime = -1;
//            mpMarkTime = DateTime.MinValue;
//            return true;
//        } else {
//            return false;
//        }
    }

    public void TimeLeft (out int hour, out int min, out int sec)
    {
        TimeSpan spanT = TargetDT - DateTime.Now;

        hour = spanT.Hours;
        min = spanT.Minutes;
        sec = spanT.Seconds;
    }

    public int SecondsLeft ()
    { // Count Down...
        TimeSpan spanT = TargetDT - DateTime.Now;
        return (int)spanT.TotalSeconds;
    }
}
