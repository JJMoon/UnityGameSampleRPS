using UnityEngine;
using System.Collections.Generic;
namespace Common {
	
	public class Util {
		
		
	public delegate void Lambda();
	public delegate void LambdaInt(int i);
	
		public static string AddArgOnURL(string url,string key,string val) {
			if(url == null) return null;
			if(key == null || val == null) return url;
			if(key.Length == 0 || val.Length == 0) return url;
			
			int findQuest = url.IndexOf ("?");
			if(findQuest >= 0) {
				return url+"&"+WWW.EscapeURL(key)+"="+WWW.EscapeURL(val);
			} else {
				return url+"?"+WWW.EscapeURL(key)+"="+WWW.EscapeURL(val);
			}
		}
		public static void GenGarbage() {
			if(Garbages.bin == null) {
				GameObject garbage = new GameObject();
				Garbages garbages = garbage.AddComponent<Garbages>();
				Garbages.bin = garbages;
			}
		}
		public static void Destroy(GameObject target) {
			
			target.SetActive(false);
			GenGarbage();
			target.transform.parent = Garbages.bin.transform;
			Destroy (target);
			
		}
		
		public static Transform Transclear(Transform target) {
			
			target.localScale = Vector3.one;
			target.localPosition = Vector3.zero;
			target.localRotation = Quaternion.Euler(Vector3.zero);
			
			return target;
			
		}
		public static int ParseInteger( string sItem )
	    {
	            int iItem = 0;
	            if (sItem != null) {
			       System.Int32.TryParse( System.Text.RegularExpressions.Regex.Match( sItem, @"\d+" ).Value, out iItem );
	           }
	            return iItem;
	    }
		
		public static List<int> ParseIntergers( string src, string spliter = "," ) {
			
			List<int> ret = new List<int>();
			if(src != null && spliter != null && src.Length > 0 && spliter.Length > 0) {
				string[] _sCT = src.Split(',');
				foreach(string a in _sCT) {
					ret.Add(Common.Util.ParseInteger(a));
				}
			}
			return ret;
			
		}
		public static void ClearChildrens(GameObject target) {
			if(target == null) return;
			Transform t;
			for(int i=0,d=target.transform.GetChildCount();i<d;i++) {
				t = target.transform.GetChild(0);
				if(t != null)
				UnityEngine.Object.DestroyImmediate (t.gameObject);
			}		
		}
		
		public static void ChangeParentKeepTransform(GameObject targetObject,GameObject newParent) {
			
			if(targetObject == null || newParent == null) return;
			
			Transform targetTrans = targetObject.transform;
			
			Vector3 locScl = targetTrans.localScale;
			Vector3 locPos = targetTrans.localPosition;
			Quaternion locRot = targetTrans.localRotation;
			
			targetTrans.parent = newParent.transform;
			
			targetTrans.localScale = locScl;
			targetTrans.localPosition = locPos;
			targetTrans.localRotation = locRot;
			
			
		}
		
		public static void CloneChildrens(GameObject dst, GameObject src) {
			
			if(src == null) return;
			for(int i=0,d=src.transform.GetChildCount();i<d;i++) {
				GameObject srcGO = src.transform.GetChild(i).gameObject;
				GameObject go = Object.Instantiate(srcGO) as GameObject;
				
				if(go != null) {
					
					
					Vector3 befLoc = go.transform.localPosition;
					Vector3 befScl = go.transform.localScale;
					
					go.transform.parent = dst.transform;
					
					go.name = srcGO.name;
					go.transform.localPosition = befLoc;
					go.transform.localScale = befScl;
					
					if(srcGO.transform.GetChildCount() > 0) {
						
					//	CloneChildrens(go,srcGO);
						
					}
					
				}
			}		
		}
	}
}
