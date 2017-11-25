using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	public Vector2 revolveRange;
	public Vector3 velocity;

	public GameObject explosion;
	
	private Vector3 revolveAxes;
	private float revolveSpeed;

	private GameObject player;
	
	void Start () {
		rigidbody.velocity = velocity;
		setRevolve ();
		player = GameObject.Find ("Player");
	}

	void FixedUpdate () {
		revolve ();
		if (rigidbody.position.z < FindObjectOfType<Camera> ().transform.position.z) {
			reset ();
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.CompareTag ("Bullet")) {
			Destroy(col.gameObject);
			Instantiate (explosion, transform.position, Quaternion.identity);
			reset();
			GameObject.Find ("Player").SendMessage("addScore", 5);
		}
	}

	void reset () {
		player.SendMessage ("spawnAsteroid", this);
		player.SendMessage ("addScore", 1);
	}

	void setRevolve () {
		revolveAxes = new Vector3(Random.Range (0, 2), Random.Range (0, 2), Random.Range (0, 2));
		revolveSpeed = (float) (Random.Range ((int) revolveRange.x, (int) revolveRange.y));
	}

	void revolve () {
		rigidbody.transform.Rotate (revolveAxes, revolveSpeed);
	}
}
