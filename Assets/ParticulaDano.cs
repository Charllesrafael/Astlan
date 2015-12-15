using UnityEngine;
using System.Collections;

public class ParticulaDano : MonoBehaviour {
	public float tempovida;
	private float tempo;
	public GameObject  pai;
	// Use this for initialization
	void Start () {
		tempo = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time- tempo >tempovida) 
		{
			Destroy(this.gameObject);
		}
	}
}
