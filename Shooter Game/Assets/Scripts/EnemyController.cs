using UnityEngine;
using System.Collections;

public class EnemyController : Spaceship {

	public Transform shotSpawn;
	public int scoreValue;
	
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	
	private float currentSpeed;
	private float targetManeuver;
	
	void Awake () {
		//Initialize components
		audioSource = GetComponent<AudioSource> ();
		nextFire = Time.time + fireRate/4;
		rigBody = GetComponent<Rigidbody> ();
	}
	
	void Start () {

		rigBody.velocity = transform.forward * speed;
		currentSpeed = rigBody.velocity.z;
		StartCoroutine(Evade());
	}

	void Update () {

		//Enemy fire
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
		
	}

	//Used for the enemies to perform evasive maneuvers
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate ()
	{
		//Moves the enemy around
		float newManeuver = Mathf.MoveTowards (rigBody.velocity.x, targetManeuver, smoothing * Time.deltaTime);
		rigBody.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rigBody.position = new Vector3
			(
				Mathf.Clamp(rigBody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp(rigBody.position.z, boundary.zMin, boundary.zMax)
				);
		rigBody.rotation = Quaternion.Euler (0, 0, rigBody.velocity.x * -tilt);
	}

	//Handle Collisions on Enter
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || other.tag == "Bolt") {
			Instantiate (explosion, transform.position, transform.rotation);
			GameController.instance.AddScore(scoreValue);
			Destroy(gameObject);
		}
	}
	//Handle Collisions on Exit
	void OnTriggerExit(Collider other)
	{

	}
}
