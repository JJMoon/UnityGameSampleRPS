// [2013:11:26:MOON<Start>]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Text;

//using LitJson;
using SimpleJSON;

public class WasMail
{
    public int msgID1, msgID2, fromUUID, itemCount;
    // -1 : 운영자.
    public string senderID, content, itemTypeId, date;
    // 98,"fromUUID":-1,"content":
    public bool Parse (JSONNode jsNd)
    {
        try {
            return (jsNd.ParseTo ("msgID1", out msgID1, "msgID2", out msgID2) &&
                    jsNd.ParseTo ("itemCount" , out itemCount, "fromUUID", out fromUUID) &&
                jsNd.ParseTo ("senderID", out senderID, "content", out content) &&
                jsNd.ParseTo ("itemTypeId", out itemTypeId, "date", out date ));
        } catch {
            return false;
        }
    }

    public void ShowMyself ()
    {
        Ag.LogString (string.Format ("   [WasMail] : ID1={0} \t\t ID2={1} \t Sender={2} \t Item={3} \t\t content={4}  fromUUID = {5}", 
            msgID1, msgID2, senderID, itemTypeId, content, fromUUID));
    }
}

public class AmMail
{
    public WasMail WAS = new WasMail ();
    public DateTime DTobj;

    public bool ParseFrom (JSONNode pJson)
    {
        try {
            WAS.Parse (pJson);  // =  JsonMapper.ToObject<WasMail> (pJson.ToJson ());
        } catch {
            " AmMail.. WAS  Parsing Error  .... ".HtLog ();
            pJson ["senderID"] = "Sender";
            pJson ["itemTypeId"] = "itemTypeId";
        }

        DTobj = WAS.date.ToDateTime ();
        DTobj.ToString ().HtLog ();
        WAS.ShowMyself ();
        return true;
    }
}