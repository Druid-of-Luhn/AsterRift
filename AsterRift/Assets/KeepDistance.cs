using UnityEngine;
using System.Collections;

public class KeepDistance : MonoBehaviour {

	private GameObject player;
	
	void Start () {
		player = GameObject.Find ("Player");
	}

	void Update () {
	
	}

	void FixedUpdate () {
		transform.position = player.transform.position;
	}
}
