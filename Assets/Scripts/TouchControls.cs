
using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {


    private Tile tileScript;

	// Use this for initialization
	void Start ()
    {
        tileScript = FindObjectOfType<Tile>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {

	    
	}

    public void Click()
    {
        tileScript.TileClicked();
    }
}
