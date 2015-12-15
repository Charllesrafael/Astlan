using UnityEngine;
using System.Collections;

public class Flecha : MonoBehaviour {
	public GameObject pai;
	public int dano = 1;
	public GameObject olho;
	public float velocidade = 0.3f;
	private bool colidiu;
	// Use this for initialization
	void Start () {
		pai = GameObject.FindGameObjectWithTag ("Player");
		olho = (GameObject) Instantiate (olho,new Vector3(pai.transform.position.x,pai.transform.position.y+20.194f,pai.transform.position.z),Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (olho != null) 
		{
			
			this.transform.LookAt (olho.transform);
		}
//		else
//			this.transform.LookAt (Vector3.forward);
		if (!colidiu) 
		{
			this.transform.Translate (Vector3.forward*velocidade);
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag=="Player") 
		{
			collision.gameObject.GetComponent<JogadorControler>().Vida -= dano;

		}
		Destroy(this.gameObject);
		colidiu = true;
		Debug.Log ("TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
	}
}
