using UnityEngine;
using System.Collections;




public class MouseEdgeDetect : MonoBehaviour
{


    double mDelta = 0.0; // Pixels. The width border at the edge in which the movement work
    public Vector3 frameSize1;
    public Vector3 frameSize2;



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Check if on the right edge
        int mSpeed = 3;

        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            if (transform.position.x <= frameSize1.x)
            {
                // Move the camera right side
                transform.position += transform.right * Time.deltaTime * mSpeed;
            }

        }


        if (Input.mousePosition.x <= 0 + mDelta)
        {
            if (transform.position.x >= frameSize2.x)
            {
                // Move the camera left side
                transform.position -= transform.right * Time.deltaTime * mSpeed;
            }

        }


        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            if (transform.position.y <= frameSize1.y)
            {
                // Move the camera up
                transform.position += transform.up * Time.deltaTime * mSpeed;
            }
        }

        if (Input.mousePosition.y <= 0 + mDelta)
        {
            if (transform.position.y >= frameSize2.y)
            {
                // Move the camera down
                transform.position -= transform.up * Time.deltaTime * mSpeed;
            }
        }




    }
}