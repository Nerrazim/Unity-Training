using UnityEngine;
using System.Collections;

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
	
	public Boundary bondary;

	public GameObject bolt;
	public Transform shotSpawn;

	private Rigidbody rigBody;
	private float nextFire = 0.0f;

	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
	}

	void Start () {
	
	}

	void Update () {

		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			GameObject clone = Instantiate(bolt, shotSpawn.position, shotSpawn.rotation) as GameObject;
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
}
