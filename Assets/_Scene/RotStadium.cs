using UnityEngine;
using System.Collections;

public class RotStadium : MonoBehaviour {
    public bool mStadiumRotflag;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        if (mStadiumRotflag) {
            this.gameObject.transform.Rotate (0, 0.1f, 0);
        }
	}
}
