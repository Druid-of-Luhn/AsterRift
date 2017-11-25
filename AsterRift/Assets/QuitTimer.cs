using UnityEngine;
using System.Collections;

public class QuitTimer : MonoBehaviour {

	public float delay;

	void Start () {
		Invoke ("startGame", delay);
	}

	void startGame () {
		Application.LoadLevel ("MainScence");
	}
}
