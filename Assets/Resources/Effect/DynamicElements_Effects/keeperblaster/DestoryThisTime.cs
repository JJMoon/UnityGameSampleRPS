using UnityEngine;
using System.Collections;

public class DestoryThisTime : MonoBehaviour {
    public float destroyTime = 5;
	// Use this for initialization
	void Start () {
        Destroy (gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
