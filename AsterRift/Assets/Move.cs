using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector2 range = new Vector2 (-5, 10);
	public Vector3 speed = new Vector3 (0, 0, -10);
	public Vector3 position = new Vector3 (0, 0, 40);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		rigidbody.rotation = Quaternion.identity;
		rigidbody.velocity = speed;
		
		if (rigidbody.position.z < FindObjectOfType<Camera> ().transform.position.z) {
			resetPosition ();
		}
	}
	
	void resetPosition() {
		int rand = Random.Range((int) range.x, (int) range.y);
		rigidbody.position = new Vector3 (position.x, rand, position.z);
	}
}
