using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolyGraph : MonoBehaviour
{
    
    public List<float> values = new List<float> ();
    public List<Vector3> points = new List<Vector3> ();
    public Vector3 offset = Vector3.zero;
    public float radius = 100f;
    public float mPentagonValue = 0.1f;
    // Use this for initialization
    public string idHeader = "_line_";
    private int indexIncrement = 0;
    public int numPoints = 5;
    private int genPoints = 0;
    public float weight = 0.001f;
    public bool isPolygon = false;
    private bool genPolygon = false;
    public bool linkWithCenter = true;
    public Color startColor = new Color (255f, 0f, 0f);
    public Color endColor = new Color (255f, 0f, 255f);
    public float duration = 0.125f;
    private Mesh bodyMesh;
    private MeshFilter meshFilter;
    private Vector3[] meshVertices;
    private int[] meshTriangles;
    private Vector2[] meshUVs;
    public Material material;
    public Material meshMaterial;


    void Start ()
    {
        //isPolygon = true;
        UpdatePoints ();

        StartCoroutine (Start (10f));
    }

    IEnumerator Start (float Time) {
        while (true) {
            yield return new WaitForSeconds (0.1f);
            for (int i = 0; i < this.values.Count; i++) {
                //this.values [i] = 0.3f;
                this.values [0] = 0.1f;
                this.values [1] = 0.3f;
                this.values [2] = 0.4f;
                this.values [3] = 0.5f;
                this.values [4] = 0.6f;
                Debug.Log (mPentagonValue);
            }

            yield return new WaitForSeconds (3f);
            for (int i = 0; i < this.values.Count; i++) {
                //this.values [i] = 0.5f;
                this.values [0] = 0.4f;
                this.values [1] = 0.2f;
                this.values [2] = 0.1f;
                this.values [3] = 0.5f;
                this.values [4] = 0.9f;
                Debug.Log (mPentagonValue);
            }

            yield return new WaitForSeconds (5f);
            for (int i = 0; i < this.values.Count; i++) {
                this.values [0] = 0.5f;
                this.values [1] = 0.2f;
                this.values [2] = 0.1f;
                this.values [3] = 0.5f;
                this.values [4] = 0.1f;
                Debug.Log (mPentagonValue);
            }

            yield return new WaitForSeconds (7f);
            for (int i = 0; i < this.values.Count; i++) {
                this.values [0] = 0.4f;
                this.values [1] = 0.2f;
                this.values [2] = 0.4f;
                this.values [3] = 0.1f;
                this.values [4] = 0.2f;
                Debug.Log (mPentagonValue);
            }

        }
    }

    void Update () {
        UpdatePoints ();
    }

    void OnGUI () {
        //Debug.Log ("this.values.Count" + this.values.Count);
        /*
        if (GUI.Button (new Rect (110, 10, 50, 50), "Click")) {
            //Debug.Log("Count4"+ this.values.Count);
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.4f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.6f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.2f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;
        }
        
        if (GUI.Button( new Rect ( 10,70,100,30 ),"1")) {
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.3f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.6f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.1f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;
        }

        if (GUI.Button( new Rect ( 10,130,100,30 ),"2")) {
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.2f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.2f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;
        }

        if (GUI.Button( new Rect ( 10,160,100,30 ),"3")) {
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.4f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.6f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.2f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;
        }

        if (GUI.Button( new Rect ( 10,190,100,30),"4")) {
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.3f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.5f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.2f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;

        }

        if (GUI.Button( new Rect ( 10,220,100,30 ),"5")) {
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[0] = 0.4f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[1] = 0.4f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[2] = 0.4f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[3] = 0.3f;
            GameObject.Find("Dynamic graph/_valueGraph").GetComponent<PolyGraph>().values[4] = 0.1f;
        }
        */
    }



    void Clear ()
    {
        
        Common.Util.ClearChildrens (this.gameObject);
        
    }
    
    public void UpdatePoints ()
    {
        int k = 0;
        points.Clear ();
        for (int i=0; i<numPoints; i++) {
            float angleOrign = ((float)1 / (float)5 ) * (Mathf.PI * 2);
            //Debug.Log (angleOrign + "angleOrign");
            float angle = ((float)i / (float)numPoints + 0.25f) * (Mathf.PI * 2);
            //Debug.Log ("Angle" + angle);
            float val = 1f;
            if (this.values.Count > i) {
                val = this.values [i];
            }
            points.Add (offset + new Vector3 (/*x*/(radius * val) * Mathf.Cos (angle),
                                            /*y*/(radius * val) * Mathf.Sin (angle),
                                            /*z*/0f));
            //Debug.Log ("point x" + (radius * val) * Mathf.Cos (angle));
            //Debug.Log ("point Y" + (radius * val) * Mathf.Sin (angle));


        }
        
        UpdatePolygon ();
    }

    void UpdatePolygon ()
    {
        
        if (this.numPoints != this.genPoints || this.isPolygon != this.genPolygon) {
            // generate points
            this.genPoints = this.numPoints;

//          Debug.Log (this.numPoints+","+this.genPoints+","+this.isPolygon+","+this.genPolygon);
            this.Clear ();
            if (this.isPolygon) {
                // generate mesh    
                if (this.meshFilter == null)
                    this.meshFilter = this.AddMesh ();
                this.genPolygon = true;
                
                
            } else {
                // generate line renderer
                if (this.linkWithCenter == true) {
                
                    for (int a=0,d=points.Count; a<d; a++) {
                        AddLine (this.offset, points [a]);
                    }
                    
                } else
                    for (int a=0,b=points.Count-1,d=points.Count; a<d; a++,b++) {
                        if (b >= d)
                            b = 0;
                        AddLine (points [a], points [b]);
                    }
                
                
            }
            
        } else {
            // update it
        
            if (this.isPolygon) {
                
                if (this.meshFilter != null) {
                    
                    List<Vector3> begin = new List<Vector3> (this.meshFilter.mesh.vertices);
                    this.meshVertices [0] = offset;
                    for (int i=0; i<points.Count; i++) {
                        this.meshVertices [i + 1] = points [i];
                    }
                    List<Vector3> end = new List<Vector3> (this.meshVertices);
                    TweenMesh tm = TweenMesh.Begin (this.meshFilter.gameObject, this.duration, begin, end);
                    tm.method = UITweener.Method.EaseOut;
                    
                }
                
            } else {
                
                
            }
            
        }
        
    }
    
    public MeshFilter AddMesh ()
    {
        
        GameObject go = new GameObject ();
        MeshFilter mf = go.AddComponent<MeshFilter> ();
        MeshRenderer mr = go.AddComponent<MeshRenderer> ();
        
        go.transform.parent = this.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.name = this.idHeader + (this.indexIncrement++);
        
        
        mr.material = this.meshMaterial;
        
        this.meshFilter = mf;
        
        this.bodyMesh = new Mesh ();
        this.meshFilter.mesh = this.bodyMesh;
                
        this.meshVertices = new Vector3[points.Count + 1];
        this.meshTriangles = new int[points.Count * 3];
        this.meshVertices [0] = offset;
        for (int a=0,d=points.Count; a<d; a++) {
                    
            this.meshVertices [a + 1] = points [a];
                
            this.meshTriangles [a * 3] = 0;
            this.meshTriangles [a * 3 + 1] = a + 1;
            this.meshTriangles [a * 3 + 2] = ((d - a == 1) ? -1 : a) + 2;
                    
        }
                
        bodyMesh.vertices = this.meshVertices;
        bodyMesh.triangles = this.meshTriangles;
                
        bodyMesh.RecalculateBounds ();
        bodyMesh.RecalculateNormals ();
                
        
        return mf;
        
    }
    
    public LineInstance AddLine (Vector3 posFrom, Vector3 posTo)
    {
            
        GameObject go = new GameObject ();
        go.transform.parent = this.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.name = this.idHeader + (this.indexIncrement++);
        
        LineInstance newCL = go.AddComponent<LineInstance> ();
        newCL.weight = this.weight;
        newCL.posFrom = posFrom;
        newCL.posTo = posTo;
        newCL.offset = this.offset;
        newCL.material = this.material;
        newCL.TweeningDuration = this.duration;
        newCL.colorTo = this.endColor;
        newCL.colorFrom = this.startColor;
        
        return newCL;
        
    }
}
