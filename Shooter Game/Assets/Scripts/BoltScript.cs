using UnityEngine;
using System.Collections;

public class BoltScript : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Boundary" && other.tag != "EnemyBolt") {
			Destroy (gameObject);
		}
	}
}
