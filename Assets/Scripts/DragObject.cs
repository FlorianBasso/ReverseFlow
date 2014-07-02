using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {
	
	Vector3 screenPoint;
	Vector3 offset;
	Color initialColor;
	public bool locked = false;

    Piece pieceSwap;
    bool canConnected;
	// Use this for initialization
	void Start () {
		initialColor = this.renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown() 
	{
        if (!Camera.main.GetComponent<Manager>().GameEnd)
        {
            if (!locked)
            {

                if (Camera.main.GetComponent<Manager>().pipeSelected == null)
                {
                    //2eme piece 
                    if (Camera.main.GetComponent<Manager>().whoseTurn == Manager.Turn.Player1)
                    {
                        if (this.transform.position.y >= (Camera.main.GetComponent<Manager>().rowsAndColumnsNumber / 2) && this.transform.position.y < (Camera.main.GetComponent<Manager>().rowsAndColumnsNumber))
                        {

                            Camera.main.GetComponent<Manager>().pipeSelected = this.gameObject;
                            Camera.main.GetComponent<Manager>().pipeSelected.renderer.material.color = Color.red;

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
                            Camera.main.GetComponent<Manager>().pipeSelected = this.gameObject;
                            Camera.main.GetComponent<Manager>().pipeSelected.renderer.material.color = Color.red;

                        }
                        else
                        {
                            //Debug.Log("[J2] ERREUR DE SELECTION DE PIECE, RETOUNER DS VOTRE RECTANGLE");
                            Camera.main.GetComponent<Manager>().errorSelectionPiece.GetComponent<GUIText>().text = "Joueur 2, veuillez sélectionner \n une pièce de votre terrain !";
                            StartCoroutine("DisplayError");
                        }
                    }
                }
                else if (Camera.main.GetComponent<Manager>().pipeSelected.transform.position != this.transform.position)
                {
                    SwapPosition();
                }
            }


            Vector3 scanPos = gameObject.transform.position;
            screenPoint = Camera.main.WorldToScreenPoint(scanPos);
            offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        else
        {
            Camera.main.GetComponent<Manager>().CheckPath();
        }
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

        Camera.main.GetComponent<Manager>().GameObjectDico.TryGetValue(this.name, out pieceSwap);

        // CHANGE TURN PLAYER AND SET THE OBJECT COLOR FOR EACH PLAYER
        if (Camera.main.GetComponent<Manager>().whoseTurn == Manager.Turn.Player1)
        {
            canConnected = CheckConnected(pieceSwap, Color.green);
             if (canConnected)
             {
                this.renderer.material.color = Color.green;
                locked = true;
             }
             Camera.main.GetComponent<Manager>().pipeSelected.transform.position = tempPosition;
             Camera.main.GetComponent<Manager>().whoseTurn = Manager.Turn.Player2;
             Camera.main.GetComponent<Manager>().TurnPlayer();

        }
        else
        {
            canConnected = CheckConnected(pieceSwap, Color.blue);
             if (canConnected)
             {
                 this.renderer.material.color = Color.blue;
                 locked = true;
             }
             Camera.main.GetComponent<Manager>().pipeSelected.transform.position = tempPosition;
             Camera.main.GetComponent<Manager>().whoseTurn = Manager.Turn.Player1;
             Camera.main.GetComponent<Manager>().TurnPlayer();
        }

        Camera.main.GetComponent<Manager>().pipeSelected.renderer.material.color = initialColor;
        Camera.main.GetComponent<Manager>().pipeSelected = null;
	}

    bool CheckConnected(Piece pieceActual,Color c)
    {
        Piece piece;
        for (int x = ((int)pieceActual.gameObject.transform.position.x-1); x < (pieceActual.gameObject.transform.position.x + 2); x += 2)
        {
            piece = GetGameObjectByPosition(new Vector3(x, pieceActual.gameObject.transform.position.y, 0));
            if ((piece != null) && (piece.gameObject.renderer.material.color == c || piece.gameObject.name.Contains("Depart") || piece.gameObject.name.Contains("Arrivée")))
            {
                //Horizontal
                if (piece.gameObject.transform.position.y == pieceActual.gameObject.transform.position.y)
                {
                    //pieceActual à gauche par rapport à la piece précedente 
                    if (piece.gameObject.transform.position.x > pieceActual.gameObject.transform.position.x)
                    {
                        if (piece.asLeft && pieceActual.asRight)
                        {
                            //Debug.Log("Connexion Possible entre " + piece.gameObject.name + " et " + pieceActual.gameObject.name);
                            return true;
                        }
                    }
                    //pieceActual à droite par rapport à la piece précedente 
                    else
                    {
                        if (piece.asRight && pieceActual.asLeft)
                        {
                            //Debug.Log("Connexion Possible entre " + piece.gameObject.name + " et " + pieceActual.gameObject.name);
                            return true;
                        }
                    }
                }
            }
        }


        for (int y = ((int)pieceActual.gameObject.transform.position.y-1); y < (pieceActual.gameObject.transform.position.y + 2); y += 2)
        {
            piece = GetGameObjectByPosition(new Vector3(pieceActual.gameObject.transform.position.x, y, 0));
            if ((piece != null) && (piece.gameObject.renderer.material.color == c || piece.gameObject.name.Contains("Depart") || piece.gameObject.name.Contains("Arrivee")))
            {
                //Vertical
                if (piece.gameObject.transform.position.x == pieceActual.gameObject.transform.position.x)
                {
                    //pieceActual en bas par rapport à la piece précedente 
                    if (piece.gameObject.transform.position.y > pieceActual.gameObject.transform.position.y)
                    {
                        if (piece.asBot && pieceActual.asTop)
                        {
                            //Debug.Log("Connexion Possible entre " + piece.gameObject.name + " et " + pieceActual.gameObject.name);
                            return true;
                        }
                    }
                    //pieceActual en haut par rapport à la piece précedente 
                    else
                    {
                        if (piece.asTop && pieceActual.asBot)
                        {
                            //Debug.Log("Connexion Possible entre " + piece.gameObject.name + " et " + pieceActual.gameObject.name);
                            return true;
                        }
                    }

                }
            }
        }

        return false;
    }

    Piece GetGameObjectByPosition(Vector3 v)
    {
        foreach (Piece p in Camera.main.GetComponent<Manager>().GameObjectDico.Values)
        {
            if (p.gameObject.transform.position == v)
                return p;
        }
        return null;
    }
}
