using UnityEngine;
using System.Collections.Generic;

public class TweenMesh : UITweener
{
	public List<Vector3> from = new List<Vector3>();
	public List<Vector3> to = new List<Vector3>();
	private Vector3[] buffer = null;
	public Color fromColor = new Color(255f,255f,255f);
	public Color toColor = new Color(255f,255f,255f);
	
	public MeshFilter meshFilter;
	
	//public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
	//public Vector3 scale { get { return cachedTransform.localScale; } set { cachedTransform.localScale = value; } }

	override protected void OnUpdate (float factor, bool isFinished)
	{
		if(meshFilter!= null) {
			for(int i=0,d=this.from.Count;i<d;i++) {
				this.buffer[i] = from[i] * (1f-factor) + to[i]*factor;
			}
			meshFilter.mesh.vertices = this.buffer;
		}
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenMesh Begin (GameObject go, float duration, List<Vector3> vertexFrom, List<Vector3> vertexTo)
	{
		
		TweenMesh comp = UITweener.Begin<TweenMesh>(go, duration);
		comp.meshFilter = go.GetComponent<MeshFilter>();
		
		if(comp.meshFilter == null) return null;
		if(vertexFrom.Count != vertexTo.Count) return null;
		if(vertexFrom.Count != comp.meshFilter.mesh.vertexCount) return null;
		
		comp.from = vertexFrom;
		comp.to = vertexTo;
		comp.buffer = new Vector3[vertexFrom.Count];
		
		if (duration <= 0f || comp.from.Count == 0)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}