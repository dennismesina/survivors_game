using UnityEngine;
using System.Collections;

public class SurvivorController : MonoBehaviour {

    public float startingspeed = 2.0f;
    public float maxspeed = 4.0f;
    public bool idle = false;
    public bool scavenge = false;
    public bool secure = false;
    public bool attack = false;
    public bool dead = false;
    public float attackRate;
    public float startingAttackRate = 0.5f;
    private bool wasClicked = false;
    private Vector2 destination;
    private float speed;

    // Use this for initialization
    void Start () {
        speed = startingspeed;
        attackRate = startingAttackRate;
	}
	
	// Update is called once per frame
	void Update () {
        OnGUI();
        if(attack)
        {
            var name = "Zombie";
            var zombie = FindClosest(name);
            speed = maxspeed;
            walk(zombie.transform.position.x, zombie.transform.position.y);
            if (Vector3.Distance(transform.position, zombie.transform.position) < 2)
            {
                attackRate -= Time.deltaTime;
                if (attackRate < 0)
                {
                    zombie.GetComponent<PlayerHealth>().TakeDamage(15);
                    attackRate = startingAttackRate;
                }
            }
        }
        else if(scavenge)
        {
            secure = false;
            var building = FindClosest("Building");
            float x = transform.position.x;
            float y = transform.position.y;
            var mouse = Camera.main.GetComponent<checkMouseClicks>();
            if(Input.GetMouseButtonUp(0))
            {
                building = mouse.ClickSelect();
            }
            if(building.tag == "Building")
            {
                x = building.transform.position.x;
                y = building.transform.position.y;
            }
            walk(x, y);
        }
        else if (idle)
        {
            //float randomX = Random.Range(transform.position.x - 2, transform.position.x + 2);
            //float randomY = Random.Range(transform.position.y - 1, transform.position.y + 1);
            //for (int i = 0; i < 20; i++)
            //{
            //    walk(randomX, randomY);
            //    if (!idle)
            //        return;
            //}
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
        //if ((x + 1 == transform.position.x || x - 1 == transform.position.x) && (y + 1 == transform.position.y || y - 1 > transform.position.y))
        //{
        //    idle = true;
        //}
    }

    void setDestination(GameObject obj)
    {
        var x = obj.transform.position.x;
        var y = obj.transform.position.y;
        destination.x = x;
        destination.y = y;
    }

    private void OnMouseUp()
    {
        //Debug.Log("Click!");
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

    GameObject FindNextClosest(string obj)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(obj);
        GameObject closest = null;
        GameObject nextclosest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nextclosest = closest;
                closest = go;
                distance = curDistance;
            }
        }
        return nextclosest;
    }
}