using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	public float time;
	private float remaining_time;
	// Use this for initialization
	void Start () {
		remaining_time = time;
	}
	
	// Update is called once per frame
	void Update () {
		remaining_time -= Time.deltaTime;

		if (remaining_time <= 0) {
			Destroy(this);
		}
	}
}
