using UnityEngine;
using System.Collections;

public class Crowd : AmSceneBase {
    public Texture2D[] textrue;
    public int Num ;
    public float NumInterVal = 0.1f;
    public bool mFlag = true;
    //public var Texturename : String[];

	// Use this for initialization
	void Start () {
        mRscrcMan = new HtRsrcMan ("");
	}
	
	// Update is called once per frame
	void Update () {
        if(mFlag){
            Num=  (int)(Time.time / NumInterVal);
            //Num++;
            var Num1=Num % 21 ;
            transform.renderer.material.mainTexture= (Texture2D)Resources.Load("Textures/FanAni/Fans_Anima_"+Num1+" copy");
            //Debug.Log (Num1+"Num");
        }
	
	}
    
    IEnumerator WaitAndPrint(float waitTime) {
       
            yield return new WaitForSeconds(waitTime);
            
        
    }
}
