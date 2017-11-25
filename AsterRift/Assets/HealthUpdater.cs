using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour {
	
	public GameObject explosion;
	public float invuln_time;
	public int health_val;

	private Text health_text;

	private float remaining_invuln = 0;

	void Start () {
		health_text = GameObject.Find ("Health").GetComponent<Text> ();
		displayHealth ();
	}

	void Update () {
		if (remaining_invuln > 0) {
			remaining_invuln -= Time.deltaTime;
		}
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.collider.gameObject.CompareTag ("Asteroid")) {
			Instantiate (explosion, coll.collider.transform.position, Quaternion.identity);
			coll.collider.gameObject.SendMessage ("reset");
			damage (1);
		}
	}

	void damage(int amount) {
		Debug.Log ("Damaged.");
		if (remaining_invuln <= 0) {
			remaining_invuln = invuln_time;

			health_val -= amount;
			displayHealth ();

			if (health_val <= 0) {
				Application.LoadLevel ("MainScence");
				return;
			}
		}
	}

	void displayHealth () {
		health_text.text = "";
		for (int i = 0; i < health_val; i++) {
			health_text.text += "|";
		}
	}
}
