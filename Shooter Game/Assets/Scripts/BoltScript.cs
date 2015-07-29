using UnityEngine;
using System.Collections;

public class BoltScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (this.tag == "EnemyBolt" && other.tag == "Enemy" ||
		    this.tag == "EnemyBolt" && other.tag == "Bolt") {
			return;
		}

		if (other.tag != "Boundary" && other.tag != "EnemyBolt") {
			Destroy (gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (this.tag == "EnemyBolt" && other.tag == "Enemy" ||
		    this.tag == "EnemyBolt" && other.tag == "Bolt") {
			return;
		}

		if (other.tag == "Boundary") 
		{
			Destroy (gameObject);
		}
	}
}
