using UnityEngine;
using System.Collections;

public class CFase : MonoBehaviour {
	public GameObject jogador;
	public GameObject SPoint;//save point

	// Use this for initialization
	void Awake() {
		if (Controlador.singleton.SavePoint) 
		{
			jogador.transform.position = SPoint.transform.position;
//			Controlador.singleton.Player = jogador;
		}
	}
	

}
