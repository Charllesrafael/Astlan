using UnityEngine;
using System.Collections;

public class Espada : MonoBehaviour {
	public float tempovida;
	private float tempo;

	public bool acertou = false;
	public GameObject  ParticulaDano;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Time.time- tempo >tempovida) 
//		{
//			acertou = false;
//		}
	}

//	public void DaDano()
//	{
//		acertou = true;
////		Instantiate(ParticulaDano,this.transform.position,Quaternion.identity);
////		tempo = Time.time;
//	}


	void OnTriggerStay(Collider other) {
		if (acertou)
		{
			Debug.Log("atacooooou-espadaa");
//			if (other.tag == "Inimigo" && other.gameObject.GetComponent<inimigo>().Estado != inimigo.enumEstado.Morto)
//			{
//				Debug.Log("acertou");
//				other.GetComponent<inimigo>().LevaDano(dano);;
//			}
//			acertou = false;
//			if (other.tag == "Inimigo") {
//				Instantiate(ParticulaDano,this.transform.position,Quaternion.identity);
//			}

		}


	}


}
