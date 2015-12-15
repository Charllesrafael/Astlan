using UnityEngine;
using System.Collections;

public class CompBatalha : MonoBehaviour {

	public GameObject[] bloqueios;
	public bool ativado;
	public bool fim = false;
	public GameObject pontoCriacao;

	public GameObject[] inimigos;//tipos de inimigos criados
	public float tempoCriaInimigo;
	public float tempoRespaw;
	public float dist;
	public float asddas;
	public GameObject player;


	public int NRound;//numero de round atual
	public int NMRound;//numero maxim de rounds


	int contador = 0;
	// Use this for initialization
	void Start () {
		player = Controlador.singleton.Player;;
	}
	
	// Update is called once per frame
	void Update () 
	{
		asddas = Time.time - tempoCriaInimigo;

		if (!ativado ) 
		{
			dist = Vector3.Distance (this.transform.position,player.transform.position);
		}

		if (!ativado && dist<2f) 
		{
			ativado = true;
			Debug.Log ("Começo de batalha!");
//			tempoCriaInimigo = Time.time ;
			contador = inimigos.Length;

			foreach (var item in inimigos) 
			{
				if (item.GetComponent<inimigo>())
				{
					item.GetComponent<inimigo>().Ativado = true;
				}else if (item.GetComponent<espadachim>())
				{
					item.GetComponent<espadachim>().Ativado = true;
				}
			} 
		}

		if (ativado) 
		{
			for (int i = 0; i < bloqueios.Length; i++) 
			{
				bloqueios[i].SetActive(true);
			}


//			if ((Time.time -tempoCriaInimigo)>tempoRespaw) 
//			{
//				if(NRound < NMRound) 
//				{
//
//					Debug.Log ("asdasdasd");
//					if (inimigos.Length != contador)
//					{
//						Instantiate(inimigos[contador],pontoCriacao.transform.position,Quaternion.identity); 				tempoCriaInimigo = Time.time;
//						contador++;
//					}else
//					{
//						GameObject[] aux = GameObject.FindGameObjectsWithTag ("Inimigo");
//						if (aux.Length == 0)
//						{
//							contador = 0;
//							NRound++;
//						}
//					}
//				}
//				else
//				{
//					fim = true;
//				}
//			}



			if (fim)
			{
				Debug.Log("GANHOU A BATALHA");
				Destroy(this.gameObject);
			}

		}
	}



}
