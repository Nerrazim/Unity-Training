using UnityEngine;
using System.Collections;

//Used to destory objects when they leave the boundary
public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}
}
