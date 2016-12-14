using UnityEngine;
using System.Collections;

public class AggroRadius : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<CircleCollider2D>();
        var aggroRadius = gameObject.GetComponent<CircleCollider2D>();
        aggroRadius.isTrigger = true;
        aggroRadius.radius = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        var zombie = other.gameObject.GetComponent<ZombieMove>();
        zombie.aggro = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var zombie = other.gameObject.GetComponent<ZombieMove>();
        zombie.aggro = false;
    }
}
