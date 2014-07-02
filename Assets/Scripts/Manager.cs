using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	public GameObject[] pipesArray;
	public GameObject pipeSelected;
	public GameObject tileBackgroundJ1;
    public GameObject tileBackgroundJ2;
    public GameObject depart;
    public GameObject arrive;
	public int rowsAndColumnsNumber;
	public Turn whoseTurn = Turn.Player1;
	public enum Turn{
		Player1,
		Player2
	};
    public GameObject text;
    public GameObject parent;
    public GameObject errorSelectionPiece;

    public bool GameEnd = false;

    GameObject DepartJ1;
    GameObject DepartJ2;
    GameObject ArriveeJ1;
    GameObject ArriveeJ2;

    public Dictionary<string, Piece> GameObjectDico = new Dictionary<string, Piece>();


	// Use this for initialization
	void Start () 
	{
		GenerateGrid ();
        text.GetComponent<TextMesh>().text = whoseTurn.ToString();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void TurnPlayer()
    {
        text.GetComponent<TextMesh>().text = whoseTurn.ToString();
    }

    public void CheckPath()
    {
        CheckNeghboor(DepartJ1);
    }

    void CheckNeghboor(GameObject Parent)
    {
        for (int i = ((int)Parent.transform.position.x - 1); i < ((int)Parent.transform.position.x + 1); i++)
        {
            for (int j = ((int)Parent.transform.position.y - 1); j < ((int)Parent.transform.position.y + 1); j++)
            {

            }

        }
    }

    /*BinaryTree<int> GetBinaryTree(Vector3 vector)
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if()
        }
    }*/
	void GenerateGrid()
	{
        //Generate Depart
        int numDepartPlayer1 = Random.Range((rowsAndColumnsNumber/2), rowsAndColumnsNumber);
        int numDepartPlayer2 = Random.Range(0, (rowsAndColumnsNumber / 2));

        DepartJ1 = (GameObject) Instantiate(depart, new Vector3(0, numDepartPlayer1, 0), Quaternion.identity);
        DepartJ2 = (GameObject) Instantiate(depart, new Vector3(0, numDepartPlayer2, 0), Quaternion.identity);
        DepartJ1.name = "DepartJ1";
        DepartJ2.name = "DepartJ2";

        Piece p = new Piece(DepartJ1, DepartJ1.name);
        GameObjectDico.Add(DepartJ1.name, p);
        p = new Piece(DepartJ2, DepartJ2.name);
        GameObjectDico.Add(DepartJ2.name, p);

        //Generate Arrivée
        int numArrivePlayer1 = Random.Range((rowsAndColumnsNumber / 2), rowsAndColumnsNumber);
        int numArrivePlayer2 = Random.Range(0, (rowsAndColumnsNumber / 2));

        ArriveeJ1 = (GameObject) Instantiate(arrive, new Vector3((rowsAndColumnsNumber - 1), numArrivePlayer1, 0), Quaternion.identity);
        ArriveeJ2 = (GameObject) Instantiate(arrive, new Vector3((rowsAndColumnsNumber - 1), numArrivePlayer2, 0), Quaternion.identity);
        ArriveeJ1.name = "ArriveeJ1";
        ArriveeJ2.name = "ArriveeJ2";


        p = new Piece(ArriveeJ1, ArriveeJ1.name);
        GameObjectDico.Add(ArriveeJ1.name, p);
        p = new Piece(ArriveeJ2, ArriveeJ2.name);
        GameObjectDico.Add(ArriveeJ2.name, p);

        int nbPiece = 0;
		for (int i = 0; i < rowsAndColumnsNumber; i++) 
		{
			for (int j = 0; j < rowsAndColumnsNumber; j++) 
			{
                if ((i > 0 && i < (rowsAndColumnsNumber-1)) || (j != numDepartPlayer1 && j != numDepartPlayer2 && i == 0) || (j != numArrivePlayer1 && j != numArrivePlayer2 && i == (rowsAndColumnsNumber-1)))
                {
                    //Background
                    if (j >= (rowsAndColumnsNumber / 2))
                    {
                        GameObject background = (GameObject)Instantiate(tileBackgroundJ1, new Vector3(i, j, 0), Quaternion.identity);
                        background.transform.parent = parent.transform;
                    }
                    else
                    {
                        GameObject background = (GameObject)Instantiate(tileBackgroundJ2, new Vector3(i, j, 0), Quaternion.identity);
                        background.transform.parent = parent.transform;
                    }


                    //Place pipes
                    GameObject piece = (GameObject)Instantiate(pipesArray[Random.Range(0, pipesArray.Length)], new Vector3(i, j, 0), Quaternion.identity);
                    piece.name += (nbPiece++);
                    piece.transform.parent = parent.transform;

                    GameObjectDico.Add(piece.name, new Piece(piece, piece.name));

                    //Camera position and orthographic size
                    if (i == rowsAndColumnsNumber / 2 && j == rowsAndColumnsNumber / 2)
                    {
                        if (rowsAndColumnsNumber % 2 == 0)
                            this.transform.position = new Vector3(i, j - 0.5f, -10);
                        else
                            this.transform.position = new Vector3(i, j, -10);

                        this.camera.orthographicSize = rowsAndColumnsNumber / 2 + 1;
                    }
                }
			}
		}
	}
}
