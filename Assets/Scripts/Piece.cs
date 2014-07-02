using UnityEngine;
using System.Collections;

public class Piece {

    public string _nom;
    string _TypePiece;
    public GameObject gameObject;
    public bool asLeft;
    public bool asRight;
    public bool asTop;
    public bool asBot;

    public Piece(GameObject go, string nom)
    {

        if (nom.Contains("Depart") || nom.Contains("Arrivee"))
            _TypePiece = "Crossroads";
        else
            _TypePiece = nom;

        _nom = nom;
        gameObject = go;
        asLeft = false;
        asRight = false;
        asBot = false;
        asTop = false;
        CheckType();
        
    }

    void CheckType()
    {
        if (_TypePiece.Contains("Crossroads"))
        {
            asLeft = asBot = asTop = asRight = true;
        }
        else if (_TypePiece.Contains("BotLeft"))
        {
            asLeft = asBot = true;
        }
        else if (_TypePiece.Contains("BotRight"))
        {
            asRight = asBot = true;
        }
        else if (_TypePiece.Contains("TopLeft"))
        {
            asLeft = asTop = true;
        }
        else if (_TypePiece.Contains("TopRight"))
        {
            asRight = asTop = true;
        }
        else if (_TypePiece.Contains("Horizontal"))
        {
            asLeft = asRight = true;
        }
        else if (_TypePiece.Contains("Vertical"))
        {
            asBot = asTop = true;
        }
    }


    public void ToString()
    {
        Debug.Log("Je suis de type : " + _TypePiece + "/nVoici mes connection : /n_asBot : " + asBot + "/n_asTop : " + asTop + "/n_asLeft : " + asLeft + "/n_asRight : " + asRight); 
    }
}
