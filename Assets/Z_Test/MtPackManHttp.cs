// [2014:5:20:MOON<Start>]
using System;
using UnityEngine;
using System.Net;
using System.Text;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.IO;
//  _////////////////////////////////////////////////_    _///////////////////////_    _____  MtPackManager  _____  Class  _____
public partial class MtPackManager : AmSceneBase
{
    string  SendWASvrMessage (string pURI, string pPostMsg, bool pEncript)
    {
        // Receive Encript
        bool ReceiveEncripted = false;
        // POST, GET 보낼 데이터 입력
        StringBuilder dataParams = new StringBuilder ();
        Ag.LogString ("MtPackManager :: SendMessage  ::>>  URI :: >> " + pURI + pEncript.ShowBool ("   Encript:", "Yes", "No"));
        
        if (pEncript) {
            string encrptStr = "_encoded=" + (ReceiveEncripted ? 1 : 0) + "&packet=" + UTAES.AESEncrypt128 (pPostMsg);
            Ag.LogString ("WAS_Static :: Packet  ::>>  " + encrptStr);
            dataParams.Append (encrptStr);
        } else
            dataParams.Append (pPostMsg);
        
        // 요청 String -> 요청 Byte 변환
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes (dataParams.ToString ());
        
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create (pURI + "?" + dataParams);
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