using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour
{
    public float startingspeed = .1f;
    public float maxspeed = .5f;
    public bool aggro = false;
    public bool avoid = false;
    public bool atcreation = true;
    public float randomX;
    public float randomY;
    public float attackRate;
    public float startingAttackRate = 0.3f;
    private Vector2 destination;
    private float speed;
    private bool arrived = false;

    // Use this for initialization
    void Start()
    {
        attackRate = startingAttackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (atcreation)
        {
            randomX = Random.Range(-30, 30);
            randomY = Random.Range(-23, 23);
            atcreation = false;
            if (transform.position.y < 0)
            {
                destination.y = 30 * 1.28f;
                destination.x = randomX * 1.28f;
            }
            else
            {
                destination.y = -30 * 1.28f;
                destination.x = randomX * 1.28f;
            }
            if (transform.position.x < 0)
            {
                destination.x = 23 * 1.28f;
                destination.y = randomY * 1.28f;
            }
            else
            {
                destination.x = -23 * 1.28f;
                destination.y = randomY * 1.28f;
            }
        }
        if (aggro)
        {
            var name = "Survivor";
            var survivor = FindClosest(name);
            speed = maxspeed;
            walk(survivor.transform.position.x, survivor.transform.position.y);
            if (Vector3.Distance(transform.position, survivor.transform.position) < 1)
            {
                attackRate -= Time.deltaTime;
                if (attackRate < 0)
                {
                    survivor.GetComponent<PlayerHealth>().TakeDamage(5);
                    attackRate = startingAttackRate;
                }
            }
        }
        else if (avoid)
        {
            var name = "Building";
            var building = FindClosest(name);
            speed = startingspeed;
            walkaway(building.transform.position.x, building.transform.position.y);
        }
        else
        {
            speed = startingspeed;
            walk(destination.x, destination.y);
        }
    }

    void walk(float x, float y)
    {
        if (x + 1 <= transform.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else if (x - 1 > transform.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if (y + 1 <= transform.position.y)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        else if (y - 1 > transform.position.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }

    void walkaway(float x, float y)
    {
        if (x - 2 >= transform.position.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else if (x + 2 < transform.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if (y - 2 > transform.position.y)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        else if (y + 2 < transform.position.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }

    // Find the name of the closest enemy
    GameObject FindClosest(string obj)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(obj);
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
