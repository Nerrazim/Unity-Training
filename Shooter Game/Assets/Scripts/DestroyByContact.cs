using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public int scoreValue;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Boundary") {

			if(this.tag == "Hazard" && other.tag != "Enemy") {
				Instantiate(explosion, transform.position, transform.rotation);
				GameController.instance.AddScore(scoreValue);

				Destroy (gameObject);
			}

			if(this.tag == "Bolt" || (this.tag == "EnemyBolt" && other.tag != "Enemy")) {
				Destroy (gameObject);
			}
		}
	}
}
