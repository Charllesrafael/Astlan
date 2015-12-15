using UnityEngine;
using System.Collections;

public class Olho : MonoBehaviour {
//	private GameObject pai;
//	public float dist;

//	void Start()
//	{
//		pai = GameObject.FindGameObjectWithTag ("Player");
//	}
//
//
//	void Update()
//	{
////		dist = Vector3.Distance (this.transform.position,pai.transform.position);
////		this.transform.position = new Vector3 (this.transform.position.x,-(Mathf.Abs(dist)/2),this.transform.position.z);
//	}


	void OnTriggerEnter(Collider other)
	{

		Destroy(this.gameObject);
	}


}
