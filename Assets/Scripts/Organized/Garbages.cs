using UnityEngine;
using System.Collections;

public class Garbages : MonoBehaviour {
	
	public static Garbages bin;
	
	// Use this for initialization
	void Start () {
	
		if(bin == null) bin = this;
		
	}
	
	void OnDestroy() {
		
		if(bin == this) bin = null;
		
	}
	
	IEnumerator tryKill() {
		
		Common.Util.ClearChildrens(this.gameObject);
		yield return new WaitForSeconds(5);
		
	}
	// Update is called once per frame
	void Update () {
	
		tryKill();
		
	}
}
