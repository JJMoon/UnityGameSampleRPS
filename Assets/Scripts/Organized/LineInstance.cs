using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineInstance : MonoBehaviour {
	
	public Material material;
	public Vector3 posFrom = Vector3.zero;
	public Vector3 posTo = Vector3.zero;
	public Vector3 offset = Vector3.zero;
	
	public float weight = 0.05f;
	public Color colorFrom = new Color(1f,1f,0f);
	public Color colorTo = new Color(0f,1f,1f);
	
	private LineRenderer m_renderer;
	
	public float TweeningDuration = 0.5f;
	public GameObject lineInstance;
	
	// Use this for initialization
	void Start () {
		
		this.lineInstance = new GameObject();
		
		Vector3 oldScale = this.lineInstance.transform.localScale = Vector3.one;
		Vector3 oldPosition = this.lineInstance.transform.localPosition = Vector2.zero;
		this.lineInstance.transform.parent = this.transform;
		this.lineInstance.transform.localScale = oldScale;
		this.lineInstance.transform.localPosition = oldPosition;
		this.lineInstance.name = "_lineInstance";
		
		m_renderer = this.lineInstance.AddComponent<LineRenderer>();
		m_renderer.material = this.material;
		
		this.UpdateLinks();
		
	}
	
	public void SetColor(Color fromColor, Color toColor) {
		
		this.colorFrom = fromColor;
		this.colorTo = toColor;
		UpdateLinks();
	}
	
	public void UpdateLinks() {
		if(m_renderer != null) {
			
			m_renderer.material = this.material;
			m_renderer.useWorldSpace = false;
			m_renderer.SetVertexCount(2);
			m_renderer.SetWidth(this.weight,this.weight);
			m_renderer.SetColors(this.colorFrom,this.colorTo);
			
			
			List<Vector3> iPosFrom = new List<Vector3>();
			List<Vector3> iPosTo = new List<Vector3>();
			
			iPosFrom.Add (this.posFrom);
			iPosFrom.Add (this.posFrom);
			
			iPosTo.Add(this.posFrom);
			iPosTo.Add(this.posTo);
			
			TweenLineRenderer.Begin(this.lineInstance.gameObject,this.TweeningDuration,iPosFrom,iPosTo);
			
			
			
		}
	}
}
