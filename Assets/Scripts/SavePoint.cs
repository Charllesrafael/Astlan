using UnityEngine;
using System.Collections;

public class SavePoint : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter()
	{
		if(!Controlador.singleton.SavePoint)
		{
			Controlador.singleton.SavePoint = true;
			Debug.Log("Save Point ativo !!");
		}
	}
}
