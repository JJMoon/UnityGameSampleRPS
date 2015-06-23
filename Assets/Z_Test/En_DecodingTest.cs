using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Text;
using System.IO;
using System;

public static class ObjectExtensions
{
    public static T ToObject<T> (this IDictionary<string, object> source)
        where T : class, new()
    {
        Debug.Log ("  ToObject :: " + typeof(T));

        T someObject = new T ();
        Type someObjectType = someObject.GetType ();

        foreach (KeyValuePair<string, object> item in source) {
            someObjectType.GetProperty (item.Key).SetValue (someObject, item.Value, null);
        }

        return someObject;
    }

    public static IDictionary<string, object> AsDictionary (this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
    {
        return source.GetType ().GetProperties (bindingAttr).ToDictionary (
            propInfo => propInfo.Name,
            propInfo => propInfo.GetValue (source, null)
        );

    }
}

public class A
{
    public string Prop1 {
        get;
        set;
    }

    public int Prop2 {
        get;
        set;
    }
}

public class EnDecodingProgram
{
    public static void MainProcess (string[] args)
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object> ();
        dictionary.Add ("Prop1", "hello world!");
        dictionary.Add ("Prop2", 3893);
        A someObject = dictionary.ToObject<A> ();

        ("  Dic ==> A  :  prop1 > " + someObject.Prop1 + "      prop2 >  " + someObject.Prop2).HtLog ();

        IDictionary<string, object> objectBackToDictionary = someObject.AsDictionary ();


    }
}