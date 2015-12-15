using UnityEngine;
using System.Collections;

public class Personagem : MonoBehaviour {
	private NoChao testChao;
	public float velocidade;
	// Use this for initialization
	void Start () {
		testChao = GetComponentInChildren<NoChao> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown("Jump") && testChao.noChao())
		{
			rigidbody.AddForce(new Vector3(0f,velocidade,0f));
		}

	}
}
