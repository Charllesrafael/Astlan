using UnityEngine;
using System.Collections;

public class NoChao : MonoBehaviour {
	//public float delei;
	private Collider colisor = null;
	// Use this for initialization
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.isTrigger == false) 
		{
			colisor = other.collider;
			Debug.Log("aaaaaaaaaff");
		}

	}



	void OnCollisionStay(Collision other)
	{
		if (colisor != other.collider) 
		{
			colisor = other.collider;
			
			Debug.Log("rrrrrrrrrrrrrrrrr");
		}
	}

	// Update is called once per frame
	void OnCollisionExit(Collision other)
	{

		if (colisor == other.collider)
		{
			colisor = null;
			Debug.Log("ooooooooooooooooo");
		}
	}

	public bool noChao()
	{
		return colisor != null;
	}

}
