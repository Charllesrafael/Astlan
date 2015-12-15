using UnityEngine;
using System.Collections;

public class NovoJogo : MonoBehaviour {
	public GameObject pai;

	public void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{

			pai.GetComponent<Salve>().NovoJogo();
//			pai.GetComponentInChildren<Salve>().XJ.SetActive(false);

			Debug.Log("Novo Jogo");
		}
	}
}
