using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    GameObject[] gos;
    
	// Use this for initialization
	void Start () {
        gos = GameObject.FindGameObjectsWithTag("Survivors");
        foreach (GameObject go in gos)
        {
            go.GetComponent<SurvivorController>().scavenge = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        gos = GameObject.FindGameObjectsWithTag("Survivors");
        foreach (GameObject go in gos)
        {
            if(go.GetComponent<SurvivorController>().attack == false)
                go.GetComponent<SurvivorController>().scavenge = true;
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
