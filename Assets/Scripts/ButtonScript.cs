using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public string NextScene = "";

    void OnMouseEnter()
    {
        
        renderer.material.color = Color.gray;
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    void OnMouseDown()
    {
        if(!NextScene.Equals(""))
            Application.LoadLevel(NextScene);
        if (this.name.Equals("Quit"))
        {
            Application.Quit();
        }
    }
}
