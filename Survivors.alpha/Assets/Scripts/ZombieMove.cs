using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour
{
    public float hspeed = 1;
    public float speed = .3f;
    public bool aggro = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            Aggro();
        }
        else
        {
            walk();
        }
    }

    void walk()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }

    void face(GameObject trigger)
    {
        Quaternion newRotation = Quaternion.LookRotation(trigger.transform.position - gameObject.transform.position);
    }

    void Aggro()
    {
        var survivor = FindClosestEnemy();
        speed = 1.5f;
        if (survivor.transform.position.x < transform.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else if (survivor.transform.position.x > transform.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if(survivor.transform.position.y - 1 < transform.position.y)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        else if (survivor.transform.position.y + 1 > transform.position.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }

    // Find the name of the closest enemy
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Survivor");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
