using UnityEngine;
using System.Collections;

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

	void GenerateGrid()
	{
        //Generate Depart
        int numDepartPlayer1 = Random.Range((rowsAndColumnsNumber/2), rowsAndColumnsNumber);
        int numDepartPlayer2 = Random.Range(0, (rowsAndColumnsNumber / 2));

        Instantiate(depart, new Vector3(0, numDepartPlayer1, 0), Quaternion.identity);
        Instantiate(depart, new Vector3(0, numDepartPlayer2, 0), Quaternion.identity);

        //Generate Arrivée
        int numArrivePlayer1 = Random.Range((rowsAndColumnsNumber / 2), rowsAndColumnsNumber);
        int numArrivePlayer2 = Random.Range(0, (rowsAndColumnsNumber / 2));

        Instantiate(arrive, new Vector3((rowsAndColumnsNumber - 1), numArrivePlayer1, 0), Quaternion.identity);
        Instantiate(arrive, new Vector3((rowsAndColumnsNumber - 1), numArrivePlayer2, 0), Quaternion.identity);

		for (int i = 0; i < rowsAndColumnsNumber; i++) 
		{
			for (int j = 0; j < rowsAndColumnsNumber; j++) 
			{
                if ((i > 0 && i < rowsAndColumnsNumber) || (j != numDepartPlayer1 && j != numDepartPlayer2 && i == 0) || (j != numArrivePlayer1 && j != numArrivePlayer2 && i == (rowsAndColumnsNumber-1)))
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
                    piece.transform.parent = parent.transform;

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
