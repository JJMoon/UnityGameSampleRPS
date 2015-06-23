using UnityEngine;
using System.Collections.Generic;

public class TweenLineRenderer : UITweener
{
	public List<Vector3> from = new List<Vector3>();
	public List<Vector3> to = new List<Vector3>();
	//public bool updateTable = false;
	
	//Transform mTrans;
	//UITable mTable;
	LineRenderer lRenderer;
	
	//public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
	//public Vector3 scale { get { return cachedTransform.localScale; } set { cachedTransform.localScale = value; } }

	override protected void OnUpdate (float factor, bool isFinished)
	{
		//cachedTransform.localScale = from * (1f - factor) + to * factor;
		if(lRenderer != null)
		for(int i=0,d=this.from.Count;i<d;i++) {
			lRenderer.SetPosition(i,from[i] * (1f - factor) + to[i] * factor);
		}
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenLineRenderer Begin (GameObject go, float duration, List<Vector3> posFrom, List<Vector3> posTo)
	{
		
		TweenLineRenderer comp = UITweener.Begin<TweenLineRenderer>(go, duration);
		comp.lRenderer = go.GetComponent<LineRenderer>();
		
		if(comp.lRenderer == null) return null;
		if(posFrom.Count != posTo.Count) return null;
		
		comp.lRenderer.SetVertexCount(posFrom.Count);
		
		comp.from = posFrom;
		comp.to = posTo;
		
		for(int i=0,d=posFrom.Count;i<d;i++) {
			comp.lRenderer.SetPosition(i,posFrom[i]);
		}

		if (duration <= 0f || comp.from.Count == 0)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}