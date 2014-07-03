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
        if (this.name.Equals("Retry"))
            Camera.main.GetComponent<Manager>().Retry();
        /*if (Camera.main.GetComponent<Manager>().pipeSelected != null && this.name.Equals("Validate"))
        {
            Camera.main.GetComponent<Manager>().validate = true;
            Camera.main.GetComponent<Manager>()
        }
        if (Camera.main.GetComponent<Manager>().pipeSelected != null && this.name.Equals("Cancel"))
            Camera.main.GetComponent<Manager>().cancel = true;*/
        if (this.name.Equals("Quit"))
        {
            Application.Quit();
        }
    }
}
