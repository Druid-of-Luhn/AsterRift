using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	private Text score_text;
	public int score_val;
	
	void Start () {
		score_text = GameObject.Find ("Score").GetComponent<Text> ();
		score_text.text = score_val.ToString ();
	}

	void Update () {
	
	}

	void addScore (int amount) {
		score_val += amount;
		score_text.text = score_val.ToString ();
	}
}
