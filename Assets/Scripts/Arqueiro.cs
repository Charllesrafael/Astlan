using UnityEngine;
using System.Collections;

public class Arqueiro : MonoBehaviour {
	public GameObject pontoFlecha;
	public GameObject flecha;
	// Use this for initialization
	void Start () {
		InvokeRepeating("LancaFlecha", 7, 1f);
	}
	
	// Update is called once per frame
	void Update () {
//		Invoke("LancaFlecha", 5);


	}


	void LancaFlecha()
	{
		Instantiate(flecha, pontoFlecha.transform.position, Quaternion.identity);
	}
}
