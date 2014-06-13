using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {
	
	Vector3 screenPoint;
	Vector3 offset;
	Color initialColor;

	// Use this for initialization
	void Start () {
		initialColor = this.renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown() 
	{
		Vector3 scanPos = gameObject.transform.position;
		screenPoint = Camera.main.WorldToScreenPoint(scanPos);
		offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}	
	
	void OnMouseUp()
	{
		this.renderer.material.color = initialColor;
	}
	void OnMouseDrag() 
	{
		this.renderer.material.color = Color.red;
		
		//Update object position
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}
}
