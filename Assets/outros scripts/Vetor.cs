using UnityEngine;
using System.Collections;

public class Vetor : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<LineRenderer> ().material.mainTextureOffset = new Vector2 (Time.time,0);
	}
}
