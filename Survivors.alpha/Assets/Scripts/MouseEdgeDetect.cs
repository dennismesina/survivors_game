using UnityEngine;
using System.Collections;


public class MouseEdgeDetect : MonoBehaviour {


    double mDelta = 0.0; // Pixels. The width border at the edge in which the movement work
    //var mSpeed = 3.0; // Scale. Speed of the movement

    


    // Use this for initialization
    void Start () {

        //private var mRightDirection = Transform.right; // Direction the camera should move when on the right edge
}
	
	// Update is called once per frame
	void Update () {
        // Check if on the right edge
        int mSpeed = 3;
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            // Move the camera
            transform.position += transform.right * Time.deltaTime * mSpeed;
        }


        if (Input.mousePosition.x <= 0 + mDelta)
        {
            // Move the camera
            transform.position -= transform.right * Time.deltaTime * mSpeed;
        }


        if (Input.mousePosition.y >= Screen.width - mDelta)
        {
            // Move the camera
            transform.position += transform.up * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y <= 0 + mDelta)
        {
            // Move the camera
            transform.position -= transform.up * Time.deltaTime * mSpeed;
        }
    }
}
