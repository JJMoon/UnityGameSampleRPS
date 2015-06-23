using UnityEngine;
using System.Collections.Generic;

public class TweenNumber : UITweener
{
	public float from = 0f;
	public float to = 0f;
	
	UILabel label;
	string footer="";
	string header="";
	float ratio=1.0f;
	
	override protected void OnUpdate (float factor, bool isFinished)
	{
		
		if(label != null) {
			float disp = from * (1f - factor) + to * factor;
			label.text = header+(disp*ratio).ToString ("0.0")+footer;
		}
		
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenNumber Begin (GameObject go, float duration, float startValue, float dstValue, string setHeader,string setFooter, float setRatio)
	{
		
		TweenNumber comp = UITweener.Begin<TweenNumber>(go, duration);
		comp.label = go.GetComponent<UILabel>();
		
		if(comp.label == null) return null;
		
		comp.from = startValue;
		comp.to = dstValue;
		comp.footer = setFooter;
		comp.header = setHeader;
		comp.ratio = setRatio;
		
		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}