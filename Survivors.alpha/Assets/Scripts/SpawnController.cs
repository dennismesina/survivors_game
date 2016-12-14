using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {




	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 5f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public float speed = 2f;

	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void Update() {
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		//enemy.transform.position = new Vector3(Random.Range(-30,30), transform.position.y, transform.position.z);

	}

}
