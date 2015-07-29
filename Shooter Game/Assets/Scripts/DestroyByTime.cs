using UnityEngine;
using System.Collections;


//Used to destroy objects when their life time expires
public class DestroyByTime : MonoBehaviour {

	public float lifetime;
	
	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}
