using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 5f;
	public float fireRate = 1f;

	public GameObject bolt;
	public GameObject enemyExplosion;
	public Transform shotSpawn;
	public int scoreValue;

	private AudioSource audioSource;
	private Rigidbody rigBody;
	private float nextFire = 0.0f;
	
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		nextFire = Time.time + fireRate/4;
		rigBody = GetComponent<Rigidbody> ();
	}
	
	void Start () {
		rigBody.velocity = transform.forward * speed;
	}

	void Update () {
		
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || other.tag == "Bolt") {

			Instantiate (enemyExplosion, other.transform.position, other.transform.rotation);
			GameController.instance.AddScore(scoreValue);
			Destroy(gameObject);
		}
	}
}
