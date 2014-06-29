using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {
	
	Vector3 screenPoint;
	Vector3 offset;
	Color initialColor;
	public bool locked = false;

	// Use this for initialization
	void Start () {
		initialColor = this.renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown() 
	{
		if (!locked) 
		{
			if (Camera.main.GetComponent<Manager> ().pipeSelected == null) {
				Camera.main.GetComponent<Manager> ().pipeSelected = this.gameObject;
				Camera.main.GetComponent<Manager> ().pipeSelected.renderer.material.color = Color.red;
			}
			else 
			{
                //2eme piece 
                if (Camera.main.GetComponent<Manager>().whoseTurn == Manager.Turn.Player1)
                {
                    if (this.transform.position.y >= (Camera.main.GetComponent<Manager>().rowsAndColumnsNumber / 2) && this.transform.position.y < (Camera.main.GetComponent<Manager>().rowsAndColumnsNumber - 1))
                    {

                        SwapPosition();
                    }
                    else
                    {
                        Camera.main.GetComponent<Manager>().errorSelectionPiece.GetComponent<GUIText>().text = "Joueur 1, veuillez sélectionner \n une pièce de votre terrain !";
                        StartCoroutine("DisplayError");
                        //Debug.Log("[J1] ERREUR DE SELECTION DE PIECE, RETOUNER DS VOTRE RECTANGLE");
                    }
                }
                else if (Camera.main.GetComponent<Manager>().whoseTurn == Manager.Turn.Player2)
                {
                    if (this.transform.position.y < (Camera.main.GetComponent<Manager>().rowsAndColumnsNumber / 2))
                    {
                        SwapPosition();
                    }
                    else
                    {
                        //Debug.Log("[J2] ERREUR DE SELECTION DE PIECE, RETOUNER DS VOTRE RECTANGLE");
                        Camera.main.GetComponent<Manager>().errorSelectionPiece.GetComponent<GUIText>().text  = "Joueur 2, veuillez sélectionner \n une pièce de votre terrain !";
                        StartCoroutine("DisplayError");
                    }
                }
			}
		}

		Vector3 scanPos = gameObject.transform.position;
		screenPoint = Camera.main.WorldToScreenPoint(scanPos);
		offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


    IEnumerator DisplayError()
    {
        yield return new WaitForSeconds(2);
        Camera.main.GetComponent<Manager>().errorSelectionPiece.GetComponent<GUIText>().text = "";
    }

	void OnMouseUp()
	{
//		this.renderer.material.color = initialColor;
	}

//	void OnMouseDrag() 
//	{
//		this.renderer.material.color = Color.red;
//		
//		//Update object position
//		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
//		transform.position = curPosition;
//	}

	void SwapPosition()
	{
		Vector3 tempPosition = this.transform.position;
		this.transform.position = Camera.main.GetComponent<Manager> ().pipeSelected.transform.position;
		this.renderer.material.color = initialColor;
		Camera.main.GetComponent<Manager> ().pipeSelected.transform.position = tempPosition;
		Camera.main.GetComponent<Manager> ().pipeSelected.GetComponent<DragObject> ().locked = true;

		// CHANGE TURN PLAYER AND SET THE OBJECT COLOR FOR EACH PLAYER
		if (Camera.main.GetComponent<Manager> ().whoseTurn == Manager.Turn.Player1) 
		{
            
			Camera.main.GetComponent<Manager> ().pipeSelected.renderer.material.color = Color.green;
			Camera.main.GetComponent<Manager> ().whoseTurn = Manager.Turn.Player2;
            Camera.main.GetComponent<Manager>().TurnPlayer();
		}
		else if (Camera.main.GetComponent<Manager> ().whoseTurn == Manager.Turn.Player2)
		{
			Camera.main.GetComponent<Manager> ().pipeSelected.renderer.material.color = Color.blue;
			Camera.main.GetComponent<Manager> ().whoseTurn = Manager.Turn.Player1;
            Camera.main.GetComponent<Manager>().TurnPlayer();
		}
		Camera.main.GetComponent<Manager> ().pipeSelected = null;
	}
}
