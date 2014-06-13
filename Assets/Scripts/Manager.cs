using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameObject[] pipesArray;
	public GameObject tileBackground;
	public int rowsAndColumnsNumber;
	// Use this for initialization
	void Start () 
	{
		GenerateGrid ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateGrid()
	{
		for (int i = 0; i < rowsAndColumnsNumber; i++) 
		{
			for (int j = 0; j < rowsAndColumnsNumber; j++) 
			{
				//Background
				Instantiate(tileBackground, new Vector3(i,j,0), Quaternion.identity);

				//Place pipes
				Instantiate(pipesArray[Random.Range(0, pipesArray.Length)], new Vector3(i,j,0), Quaternion.identity);
				//Camera position and orthographic size
				if(i == rowsAndColumnsNumber/2 && j == rowsAndColumnsNumber/2)
				{
					if(rowsAndColumnsNumber % 2 == 0)
						this.transform.position = new Vector3(i,j-0.5f, -10);
					else
						this.transform.position = new Vector3(i,j, -10);

					this.camera.orthographicSize = rowsAndColumnsNumber/2 + 1; 
				}
			}
		}
	}
}
