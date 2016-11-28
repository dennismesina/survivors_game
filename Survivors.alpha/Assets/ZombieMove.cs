using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour
{
    public float hspeed = 1;

    public int ranNum;
    public GameObject blinky;
    public float speed;

    // Use this for initialization
    void Start()
    {
        ranNum = 1;
        speed = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        ranNum = Random.Range(1, 5);
        switch (ranNum)
        {
            case 1:
                blinky.transform.Translate(Vector2.up * speed);
                break;
            case 2:
                blinky.transform.Translate(-Vector2.up * speed);
                break;
            case 3:
                blinky.transform.Translate(Vector2.right * speed);
                break;
            case 4:
                blinky.transform.Translate(Vector2.left * speed);
                break;

        }

    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
    }
}
