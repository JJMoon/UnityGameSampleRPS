using System;
using System.Collections.Generic;
using System.Text;

public class UtTimestamp
{
    protected static DateTime _basis = new DateTime (1970, 1, 1, 0, 0, 0);
    protected static int _timediff = 0;

    public static int TimeDiff {
        get {
            return _timediff;
        }
        set {
            _timediff = value;
        }
    }

    public static int ToTimestamp (bool timediff = true)
    {
        int timestamp = GetTotalSeconds (DateTime.UtcNow - _basis);
        if (timediff) {
            return timestamp + TimeDiff;
        }
        return timestamp;
    }

    public static Int64 ToMilliTimestamp ()
    {
        return GetTotalMilliseconds (DateTime.UtcNow - _basis);
    }

    public static int ToTimestamp (DateTime datetime)
    {
        return GetTotalSeconds (datetime - _basis);
    }

    public static DateTime ToDateTime (int timestamp)
    {
        DateTime convert_time = new DateTime (1970, 1, 1, 0, 0, 0);
        return convert_time.AddSeconds (timestamp);
        //return convert_time;
    }

    public static int GetWeekTick ()
    {
        return 86400 * 7;
        //return Consts.WeekTick;   by Moon 
    }

    public static int GetWeekCount ()
    {
        // 1970년 1월 1일은 목요일이므로
        // 화요일 기준으로 맞춘후 (-2일) 7일의 시간으로 나눔
        int weekcount = (ToTimestamp () + 86400 * 2) / GetWeekTick ();

        return weekcount;
    }

    public static int GetDayCount ()
    {
        return (int)(ToTimestamp () / 86400);
    }

    public static int GetLeftTournamentTime ()
    {
        int nextCount = GetWeekCount () + 1;
        Int64 nextTime = nextCount * GetWeekTick ();
        nextTime -= 86400 * 2;

        return (Int32)(nextTime - ToTimestamp ());
    }

    protected static int GetTotalSeconds (TimeSpan timespan)
    {
        int totalseconds = timespan.Days * 24 * 60 * 60;
        totalseconds += timespan.Hours * 60 * 60;
        totalseconds += timespan.Minutes * 60;
        totalseconds += timespan.Seconds;

        return totalseconds;
    }

    protected static Int64 GetTotalMilliseconds (TimeSpan timespan)
    {
        Int64 totalmilliseconds = (Int64)timespan.Days * 24 * 60 * 60 * 1000;
        totalmilliseconds += timespan.Hours * 60 * 60 * 1000;
        totalmilliseconds += timespan.Minutes * 60 * 1000;
        totalmilliseconds += timespan.Seconds * 1000;
        totalmilliseconds += timespan.Milliseconds;

        return totalmilliseconds;
    }
}
