using UnityEngine;
using System.Collections;

public class SpawnAsteroids : MonoBehaviour {
	public Asteroid[] models;

	public Vector2 rand_range;
	public float spawn_distance;
	public int asteroid_count;
	
	void Start () {
		for(int i = 0; i < asteroid_count; i++){
			spawnAsteroid((Asteroid) Instantiate (models[i % models.Length]));
		}
	}

	void Update () {
	
	}

	void spawnAsteroid (Asteroid asteroid) {
		float rand_x = Random.Range(rand_range.x, rand_range.y);
		float rand_y = Random.Range(rand_range.x, rand_range.y);
		
		asteroid.transform.position = new Vector3 (transform.position.x + rand_x,
		                                           transform.position.y + rand_y,
		                                           spawn_distance + rand_x + rand_y);
	}
}
