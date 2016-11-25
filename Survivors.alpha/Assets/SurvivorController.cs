using UnityEngine;
using System.Collections;

public class SurvivorController : MonoBehaviour {

    private bool wasClicked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        OnGUI();
	}

    private void OnMouseUp()
    {
        Debug.Log("Click!");
        wasClicked = true;
    }

    private void OnGUI()
    {
        if (wasClicked)
        {
            var someText = "Survivor was selected";
            if(GUI.Button(new Rect(20,50,100,20), someText))
            {
                wasClicked = false;
            }
        }
    }
}
