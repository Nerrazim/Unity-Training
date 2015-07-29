using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : Spaceship 
{
	public float tilt = 4f;
	public int playerHealth = 100;

	public Transform shotSpawn;
	public Slider healthBar;
	public TouchMovement touchMovement;

	void Awake () {
		//Initialize components
		rigBody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		healthBar.value = playerHealth;
	}

	void Start () {
	
	}

	void Update () {
		//Check if IPhone player so the player can fire automatically without tapping on the screen
		if (Application.platform == RuntimePlatform.IPhonePlayer && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		} else {
			if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play ();
			}
		}
	}

	void FixedUpdate() {
		//Move the player with physics
		Vector3 movement;

		//Check if IPhone player so we can use different control system 
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Vector2 direction = touchMovement.GetDirection ();
			
			movement = new Vector3 (direction.x, 0f, direction.y);
		} else {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			movement = new Vector3 (moveHorizontal, 0f, moveVertical);
		}

		//Move the player in the movement vector direction with speed
		rigBody.velocity = movement * speed;

		//Check if the player is about to leave the boundary and restrict his movement so he won't fall from the screen
		rigBody.position = new Vector3 (
			Mathf.Clamp(rigBody.position.x, boundary.xMin, boundary.xMax), 
			0f, 
			Mathf.Clamp(rigBody.position.z, boundary.zMin, boundary.zMax));

		rigBody.rotation = Quaternion.Euler (0f, 0f, rigBody.velocity.x * -tilt);

	}

	void OnTriggerEnter(Collider other)
	{
		//Update player health if collision with object occur
		if (other.tag != "Boundary") {
			if(other.tag == "Hazard") {
				playerHealth -= 10;
			} else if(other.tag == "EnemyBolt") {
				playerHealth -= 15;
			} else if(other.tag == "Enemy") {
				playerHealth -= 15;
			}
			
			healthBar.value = playerHealth;

			if(playerHealth <= 0) {
				healthBar.value = 0;
				Instantiate (explosion, transform.position, transform.rotation);
				GameController.instance.GameOver ();
				Destroy(gameObject);

			} 
		}
	}
}
