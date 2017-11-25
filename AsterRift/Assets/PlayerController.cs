using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class PlayerController : MonoBehaviour {
	public float max_rotation_degrees;
	public float rotate_speed;
	public float slide_speed;

	public AudioClip shot_sound;

	public int finger_diff;

	public GameObject laser;
	public float gun_cooldown;
	private float cooldown_remaining = 0;

	private Text ap;
	public AudioClip ap_sound;

	private float target_roll_angle;
	private float target_pitch_angle;

	private GameObject left_gun;
	private GameObject right_gun;
	private Controller leap_control;
	
	void Awake () {
		leap_control = new Controller ();
		ap = GameObject.Find ("AutoPilot").GetComponent<Text> ();
		left_gun = GameObject.Find ("GunLeft");
		right_gun = GameObject.Find ("GunRight");
	}

	public void setRollAngle(float angle) {
		if (angle > max_rotation_degrees) {
			angle = max_rotation_degrees;
		} else if (angle < -max_rotation_degrees) {
			angle = -max_rotation_degrees;
		}
		target_roll_angle = angle;
	}

	public void setPitchAngle(float angle) {
		if (angle > max_rotation_degrees) {
			angle = max_rotation_degrees;
		} else if (angle < -max_rotation_degrees) {
			angle = -max_rotation_degrees;
		}
		target_pitch_angle = angle;
	}

	private void shoot () {
		if (cooldown_remaining <= 0) {
			cooldown_remaining = gun_cooldown;
			GameObject leftb = (GameObject)Instantiate (laser, left_gun.transform.position, transform.rotation);
			GameObject rightb = (GameObject)Instantiate (laser, right_gun.transform.position, transform.rotation);
			Physics.IgnoreCollision (leftb.collider, collider);
			Physics.IgnoreCollision (rightb.collider, collider);

			AudioSource.PlayClipAtPoint (shot_sound, transform.position);
		}
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			shoot ();
		}
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel ("MainScence");
		}

		if (cooldown_remaining > 0) {
			cooldown_remaining -= Time.deltaTime;
		}

		Frame frame = leap_control.Frame ();
		
		if (frame.Hands.IsEmpty) { // Autopilot
			setRollAngle (0);
			setPitchAngle (0);

			if (! ap.enabled) {
				ap.enabled = true;
				AudioSource.PlayClipAtPoint (ap_sound, transform.position);
			}
			
		} else {
			ap.enabled = false;
			HandList hands = frame.Hands;
			Hand left, right;
			if (hands.Count == 2) {
				if (hands[0].IsLeft) {
					left = hands[0];
					right = hands[1];
				} else {
					left = hands[1];
					right = hands[0];
				}
			} else {
				left = hands[0];
				right = null;
			}
			setRollAngle (Mathf.Rad2Deg * left.PalmNormal.Roll);
			setPitchAngle (Mathf.Rad2Deg * -left.Direction.Pitch);

			if (right != null) {
				FingerList fingers = right.Fingers;
				float distance_count = 0;
				for (int i = 0; i < fingers.Count; i++) {
					distance_count += fingers[0].TipPosition.y;
				}
				if (distance_count / fingers.Count < right.PalmPosition.y - finger_diff) {
					shoot ();
				}
			}
		}
	}

	void FixedUpdate() {
		Vector3 currentRot = transform.rotation.eulerAngles;
		Vector3 targetRot = new Vector3 (target_pitch_angle, 0, target_roll_angle);

		float rot_x = Mathf.LerpAngle (currentRot.x, targetRot.x, rotate_speed);
		if (rot_x > 180) {
			rot_x -= 360;
		}
		float rot_z = Mathf.LerpAngle (currentRot.z, targetRot.z, rotate_speed);
		if (rot_z > 180) {
			rot_z -= 360;
		}

		transform.rotation = Quaternion.Euler (new Vector3(rot_x, 0, rot_z));

		rigidbody.velocity = new Vector3 ((- rigidbody.rotation.z) * slide_speed,
		                                  (- rigidbody.rotation.x) * slide_speed / 3,
		                                  0);
	}
}
