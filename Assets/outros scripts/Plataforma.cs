using UnityEngine;
using System.Collections;

public class Plataforma : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (-Vector3.left*0.01f);
	}


	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Player") 
		{
			other.gameObject.transform.parent = this.transform;
		}

	}



	void OnCollisionExit(Collision other)
	{

		if (other.collider.tag == "Player") 
		{
			other.gameObject.transform.parent = null;
		}
	}


}
