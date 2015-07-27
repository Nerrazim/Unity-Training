using UnityEngine;
using System.Collections;

public class HazardScript : MonoBehaviour {

	public GameObject hazardExplosion;
	public int scoreValue;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Boundary") {
			
			if (other.tag != "Enemy") {
				Instantiate (hazardExplosion, transform.position, transform.rotation);
				GameController.instance.AddScore (scoreValue);
				
				Destroy (gameObject);
			}
		}
	}
}
