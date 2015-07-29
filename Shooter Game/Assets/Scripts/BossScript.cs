using UnityEngine;
using System.Collections;

public class BossScript : Spaceship {

	public Transform shotSpawn;
	public int scoreValue;

	public int health = 100;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
		nextFire = Time.time + fireRate;
		rigBody = GetComponent<Rigidbody> ();
	}
	
	void Start () {
		rigBody.velocity = transform.forward * speed;
	}

	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Vector3 shotSpawnPosition = shotSpawn.position;
			Instantiate(bolt, new Vector3(shotSpawnPosition.x - 2f, 0f, shotSpawnPosition.z + 1f), shotSpawn.rotation);
			Instantiate(bolt, new Vector3(shotSpawnPosition.x - 1f, 0f, shotSpawnPosition.z + 0.5f), shotSpawn.rotation);
			Instantiate(bolt, shotSpawnPosition, shotSpawn.rotation);
			Instantiate(bolt, new Vector3(shotSpawnPosition.x + 1f, 0f, shotSpawnPosition.z + 0.5f), shotSpawn.rotation);
			Instantiate(bolt, new Vector3(shotSpawnPosition.x + 2f, 0f, shotSpawnPosition.z + 1f), shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	void FixedUpdate ()
	{
		rigBody.position = new Vector3
			(
				Mathf.Clamp(rigBody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp(rigBody.position.z, boundary.zMin, boundary.zMax)
				);
		rigBody.rotation = Quaternion.identity;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Boundary") {
			health -= 5;

			if (other.tag == "Bolt" && health <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);
				GameController.instance.AddScore (scoreValue);
				Destroy (gameObject);
			}
		}
	}

}
