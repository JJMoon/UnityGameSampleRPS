using System.Text;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Net;
using System.IO;
using System;
using SimpleJSON;
using UnityEngine;

public static class WAS
//public class AmHttp
{
    // = "http://221.143.21.33/api.psy.trd";
    public static string  SendWASvrMessage (string pPostMsg, bool pEncript)
    {
        // Receive Encript
        bool ReceiveEncripted = false;
        // POST, GET 보낼 데이터 입력
        StringBuilder dataParams = new StringBuilder ();
        Ag.LogString ("WAS_Static :: SendMessage  ::>>  mURI :: >> " + AgStt.mURI + pEncript.ShowBool ("   Encript:", "Yes", "No"));

        if (pEncript) {
            string encrptStr = "_encoded=" + (ReceiveEncripted ? 1 : 0) + "&packet=" + UTAES.AESEncrypt128 (pPostMsg);
            Ag.LogString ("WAS_Static :: Packet  ::>>  " + encrptStr);
            dataParams.Append (encrptStr);
        } else
            dataParams.Append (pPostMsg);

        // 요청 String -> 요청 Byte 변환
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes (dataParams.ToString ());

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create (AgStt.mURI + "?" + dataParams);
        request.Timeout = 20000; // 5000;  iap took 13 seconds.
        request.Method = "POST";
        request.ContentLength = byteDataParams.Length;
        request.ContentType = "application/x-www-form-urlencoded";
        // 요청 Byte -> 요청 Stream 변환
        Stream stDataParams = request.GetRequestStream ();
        stDataParams.Write (byteDataParams, 0, byteDataParams.Length);
        stDataParams.Close ();
        string strResult = null;
        try {
            // 요청, 응답 받기
            HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
            // 응답 Stream 읽기
            Stream stReadData = response.GetResponseStream ();
            StreamReader srReadData = new StreamReader (stReadData, Encoding.Default);
            // 응답 Stream -> 응답 String 변환
            strResult = srReadData.ReadToEnd ();
            //Ag.LogIntenseWord ("WAS_Static :: SendMessage  ::>>    try   Received " + " Encript ? " + pEncript + "  >> " + strResult);
            if (ReceiveEncripted) {
                JSONNode jN = JSON.Parse (strResult);
                string pckt = jN ["packet"].ToString ();
                pckt = pckt.RemoveHeadFootOneChar ();
                strResult = UTAES.AESDecrypt128 (pckt);
            }
        } catch {
            Ag.NetExcpt.ConnectLossAct (); // DisconnectedWith (was: true);
            Ag.LogIntenseWord ("WAS_Static :: SendMessage  ::>>    RESP >>   C A T C H   ! ! ! ! !  ");
            return "CATCH";
        }
        string spltr = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
        Ag.LogString ("WAS_Static :: SendMessage  ::>>  Responded   Ends   ....    " + spltr);
        //Ag.LogString ("WAS_Static :: SendMessage  ::>>  RESP >>    " + strResult);
        //Ag.LogNewLine (2);
        return strResult;
    }
}
/*
public class DRDataReader
{
    public static void Main ()
    {
        string sample = @"{
            ""name""  : ""Bill"",
            ""age""   : 32,
            ""awake"" : true,
            ""n""     : 1994.0226,
            ""note""  : [ ""life"", ""is"", ""but"", ""a"", ""dream"" ]
          }";

        PrintJson (sample);
    }

    public void PersonToJson ()
    {
        Person bill = new Person ("this is private");

        bill.Name = "William Shakespeare";
        bill.Age = 51;
        bill.Birthday = new DateTime (1564, 4, 26);

        //string json_bill = JsonMapper.ToJson (bill);

        Ag.LogString (json_bill);
    }

    public void JsonToPerson ()
    {
        string json = @"
            {
                ""Name""     : ""Thomas More"",
                ""Age""      : 57,
                ""Birthday"" : ""02/07/1478 00:00:00""
            }";

        Person thomas = JsonMapper.ToObject<Person> (json);

        Ag.LogString (thomas.ToString ());
        //Console.WriteLine("Thomas' age: {0}", thomas.Age);
    }

    public static void DataWrite ()
    {
        StringBuilder sb = new StringBuilder ();
        JsonWriter writer = new JsonWriter (sb);

        writer.WriteObjectStart ();  // Use Only Once ...

        writer.WritePropertyName ("color");
        writer.Write ("blue");

        writer.WritePropertyName ("SomeArray");

        writer.WriteArrayStart ();
        writer.Write (1);
        writer.Write (2);
        writer.Write (3);
        writer.WriteArrayEnd ();

        writer.WriteObjectEnd ();  // Use Only Once ...

        Ag.LogString (sb.ToString ());
    }

    public static void PrintJson (string json)
    {
        JsonReader reader = new JsonReader (json);

        Ag.LogString (string.Format ("{0,14} {1,10} {2,16}", "Token", "Value", "Type"));
        Ag.LogString (new String ('-', 42));

        // The Read() method returns false when there's nothing else to read
        while (reader.Read ()) {
            string type = reader.Value != null ?
                reader.Value.GetType ().ToString () : "";

            Ag.LogString (string.Format ("___    Token: {0,14} \t\t Value: {1,10} \t\t type: {2,16}",
                reader.Token, reader.Value, type));
        }
    }
}

public class JsonSample
{
    public static void Main ()
    {
        string json = @"
          {
            ""album"" : {
              ""name""   : ""The Dark Side of the Moon"",
              ""artist"" : ""Pink Floyd"",
              ""year""   : 1973,
              ""tracks"" : [
                ""Speak To Me"",
                ""Breathe"",
                ""On The Run""
              ]
            }
          }
        ";

        LoadAlbumData (json);
    }

    public static void LoadAlbumData (string json_text)
    {
        Ag.LogString ("Reading data from the following JSON string: " + json_text);

        JsonData data = JsonMapper.ToObject (json_text);

        // Dictionaries are accessed like a hash-table
        Ag.LogString (string.Format ("Album's name: {0}", data ["album"] ["name"]));

        // Scalar elements stored in a JsonData instance can be cast to
        // their natural types
        string artist = (string)data ["album"] ["artist"];
        int year = (int)data ["album"] ["year"];

        Ag.LogString (string.Format ("Recorded by {0} in {1}", artist, year));

        // Arrays are accessed like regular lists as well
        Ag.LogString (string.Format ("First track: {0}", data ["album"] ["tracks"] [0]));

    }
}

public class Person
{
    // C# 3.0 auto-implemented properties
    string thePrivate;

    public Person ()
    {
    }

    public Person (string prvt)
    {
        thePrivate = prvt;
    }

    public string   Name     { get; set; }

    public int      Age      { get; set; }

    public DateTime Birthday { get; set; }

    public string justPublicMember  { get; set; }

    public override string ToString ()
    {
        return string.Format ("[Person: \tName={0}, \tAge={1}, \tBirthday={2}, \tjustPublicMember={3}]", Name, Age, Birthday, justPublicMember);
    }
}

*/