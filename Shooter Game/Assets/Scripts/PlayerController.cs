using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary 
{

	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour 
{

	public float speed = 10f;
	public float tilt = 4f;
	public float fireRate = 0.5f;
	public int playerHealth = 100;
	
	public Boundary bondary;

	public GameObject bolt;
	public GameObject playerExplosion;
	public Transform shotSpawn;
	public Slider healthBar;

	private Rigidbody rigBody;
	private AudioSource audioSource;
	private float nextFire = 0.0f;

	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		healthBar.value = playerHealth;
	}

	void Start () {
	
	}

	void Update () {

		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}

	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0f, moveVertical);

		rigBody.velocity = movement * speed;

		rigBody.position = new Vector3 (
			Mathf.Clamp(rigBody.position.x, bondary.xMin, bondary.xMax), 
			0f, 
			Mathf.Clamp(rigBody.position.z, bondary.zMin, bondary.zMax));

		rigBody.rotation = Quaternion.Euler (0f, 0f, rigBody.velocity.x * -tilt);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Boundary") {
			if(playerHealth <= 0) {
				healthBar.value = 0;
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				GameController.instance.GameOver ();
				Destroy(gameObject);

			} else {
				if(other.tag == "Hazard") {
					playerHealth -= 10;
				} else if(other.tag == "EnemyBolt") {
					playerHealth -= 15;
				} else if(other.tag == "Enemy") {
					playerHealth -= 15;
				}

				healthBar.value = playerHealth;
			}
		}
	}
}
