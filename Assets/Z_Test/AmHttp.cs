using LitJson;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Net;
using System.IO;
using System;
using UnityEngine;

//using System.Security.Cryptography;
//


public class AxmxxHttp
{
    string pURI = "";

    public AxmxxHttp ()
    {
    }



    public void SendTest ()
    {
        // POST, GET 보낼 데이터 입력
        StringBuilder dataParams = new StringBuilder ();
        dataParams.Append ("{serviceCode:'102',director:'aa',kakaoID:'ac',country:1}");
        //dataParams.Append("ser=#############################################");
        //dataParams.Append("&client_secret=####################################");

        // 요청 String -> 요청 Byte 변환
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes (dataParams.ToString ());

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create (pURI + "?" + dataParams);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        // GET 
        // GET 방식은 Uri 뒤에 보낼 데이터를 입력하시면 됩니다.
        /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + dataParams);
            request.Method = "GET";
        */

        // 요청 Byte -> 요청 Stream 변환
        Stream stDataParams = request.GetRequestStream ();
        stDataParams.Write (byteDataParams, 0, byteDataParams.Length);
        stDataParams.Close ();

        // 요청, 응답 받기
        HttpWebResponse response = (HttpWebResponse)request.GetResponse ();

        // 응답 Stream 읽기
        Stream stReadData = response.GetResponseStream ();
        StreamReader srReadData = new StreamReader (stReadData, Encoding.Default);

        // 응답 Stream -> 응답 String 변환
        string strResult = srReadData.ReadToEnd ();

        Ag.LogString (strResult);
        Ag.LogString (" Last ");
    
        Ag.LogIntense (5, false);
    }
}



/*

public class DataReader
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

        string json_bill = JsonMapper.ToJson (bill);

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

*/

