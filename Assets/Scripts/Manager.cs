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
    //public GameObject parent;
    public GameObject errorSelectionPiece;
    public GameObject endGame;

	//ARRAY TRAIL RENDERER FOR EACH PLAYER
	public ArrayList trailRendererPlayer1Array = new ArrayList();
	public ArrayList trailRendererPlayer2Array = new ArrayList();
	public GameObject trailRendererObject;
    public bool GameEnd = false;

    GameObject DepartJ1;
    GameObject DepartJ2;
    GameObject ArriveeJ1;
    GameObject ArriveeJ2;

    GameObject pieces;
    GameObject trails;
    public Dictionary<string, Piece> GameObjectDico = new Dictionary<string, Piece>();

    public bool moving = false;

    int nbTrail = 0;

    //public bool validate = false;
    //public bool cancel = false;

    int nbPiece = 0;
	// Use this for initialization
	void Start () 
	{
		//CLear trail renderer for each player
		trailRendererPlayer1Array.Clear();
		trailRendererPlayer2Array.Clear();
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
        GameObjectDico.Clear();
        trailRendererPlayer2Array.Clear();
        trailRendererPlayer1Array.Clear();
        pieces = new GameObject("Pieces");
        trails = new GameObject("Trails");

        //Generate Depart
        int numDepartPlayer1 = Random.Range((rowsAndColumnsNumber/2), rowsAndColumnsNumber);
        int numDepartPlayer2 = Random.Range(0, (rowsAndColumnsNumber / 2));

        DepartJ1 = (GameObject) Instantiate(depart, new Vector3(0, numDepartPlayer1, 0), Quaternion.identity);
        DepartJ2 = (GameObject) Instantiate(depart, new Vector3(0, numDepartPlayer2, 0), Quaternion.identity);
        DepartJ1.name = "DepartJ1";
        DepartJ2.name = "DepartJ2";
        DepartJ1.transform.parent = pieces.transform;
        DepartJ2.transform.parent = pieces.transform;

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
        ArriveeJ1.transform.parent = pieces.transform;
        ArriveeJ2.transform.parent = pieces.transform;


        p = new Piece(ArriveeJ1, ArriveeJ1.name);
        GameObjectDico.Add(ArriveeJ1.name, p);
        p = new Piece(ArriveeJ2, ArriveeJ2.name);
        GameObjectDico.Add(ArriveeJ2.name, p);

        
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
                        background.transform.parent = pieces.transform;
                    }
                    else
                    {
                        GameObject background = (GameObject)Instantiate(tileBackgroundJ2, new Vector3(i, j, 0), Quaternion.identity);
                        background.transform.parent = pieces.transform;
                    }


                    //Place pipes
                    GameObject piece = (GameObject)Instantiate(pipesArray[Random.Range(0, pipesArray.Length)], new Vector3(i, j, 0), Quaternion.identity);
                    piece.name += (nbPiece++);
                    piece.transform.parent = pieces.transform;

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

	// TRAIL RENDERER MANAGEMENT
	public void addTrailRenderer()
	{
		if (whoseTurn == Turn.Player1) 
		{
			GameObject tempTrailRenderer1 = (GameObject)Instantiate(trailRendererObject, new Vector3(DepartJ1.transform.position.x, DepartJ1.transform.position.y, 0), Quaternion.identity);
            tempTrailRenderer1.name = "Trail" + nbTrail++;
			trailRendererPlayer1Array.Add(tempTrailRenderer1);
            tempTrailRenderer1.transform.parent = trails.transform;
		}
		else
		{
			GameObject tempTrailRenderer2 = (GameObject)Instantiate(trailRendererObject, new Vector3(DepartJ2.transform.position.x, DepartJ2.transform.position.y, 0), Quaternion.identity);
            tempTrailRenderer2.name = "Trail" + nbTrail++;
			trailRendererPlayer2Array.Add(tempTrailRenderer2);
            tempTrailRenderer2.transform.parent = trails.transform;
		}
	}

	public IEnumerator moveTrailRenderer(Vector3 newPosition, int pathIndex)
	{
        //if (moving) yield return new WaitForSeconds(0);
        float duration = 0.9f; // duration of movement in seconds
		if (whoseTurn == Turn.Player1) 
		{
            //Debug.Log("MOVE TURN 1");
            moving = true; 
			GameObject aTrailRenderer = trailRendererPlayer1Array[pathIndex] as GameObject;
            Vector3 initPos = aTrailRenderer.transform.position;
            for (float t = 0f; t < 1; )
            {
                t += Time.deltaTime / duration;
                aTrailRenderer.transform.position = Vector3.Lerp(initPos, newPosition, t);
                yield return new WaitForSeconds(0);
            }
            moving = false;

		}
		else
		{
            //Debug.Log("MOVE TURN 2");
			GameObject aTrailRenderer = trailRendererPlayer2Array[pathIndex] as GameObject;
            Vector3 initPos = aTrailRenderer.transform.position;
            moving = true; 
            for (float t = 0f; t < 1; )
            {

                t += Time.deltaTime / duration;
                aTrailRenderer.transform.position = Vector3.Lerp(initPos, newPosition, t);
                yield return new WaitForSeconds(0);
            }
            moving = false;
		}
	}

    public int GetTrailRenderer(Vector3 vec, Piece piece)
    {
        ArrayList listTrail = new ArrayList();
        listTrail.Clear();
        if (whoseTurn == Turn.Player1) 
		{
            //Debug.Log("TURN 1");
            for(int i = 0 ; i < trailRendererPlayer1Array.Count ; i++)
            {
                GameObject renderer = (GameObject)trailRendererPlayer1Array[i];
                if (piece.gameObject.transform.position == renderer.transform.position)
                {
                    //StartCoroutine(Camera.main.GetComponent<Manager>().moveTrailRenderer(vec, i));

                    //Debug.Log("Trail Name : " + renderer.name);
                    return i;
                }
            }
		}
        else if (whoseTurn == Turn.Player2) 
		{
            //Debug.Log("TURN 2");
            for (int i = 0; i < trailRendererPlayer2Array.Count; i++)
            {
                GameObject renderer = (GameObject)trailRendererPlayer2Array[i];
                if (piece.gameObject.transform.position == renderer.transform.position)
                {
                    //StartCoroutine(Camera.main.GetComponent<Manager>().moveTrailRenderer(vec, i));
                    //listTrail.Add(i);
                    //Debug.Log("Trail Name : " + renderer.name);
                    return i;
                }
            }
		}
        return -1;
    }

    public void EndGame()
    {
        endGame.GetComponent<GUIText>().text = "FIN DE LA PARTIE - " + whoseTurn.ToString() + " A GAGNE ";
        Destroy(pieces);
        Destroy(trails);
    }

    public void Retry()
    {
        nbPiece = 0;
        GameEnd = false;
        whoseTurn = Turn.Player1;
        text.GetComponent<TextMesh>().text = whoseTurn.ToString();
        endGame.GetComponent<GUIText>().text = "";
        if(pieces)
            Destroy(pieces);
        if(trails)
            Destroy(trails);
        GenerateGrid();
    }
}
