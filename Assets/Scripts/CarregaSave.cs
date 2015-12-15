using UnityEngine;
using System.Collections;

public class CarregaSave : MonoBehaviour {

	public GameObject[] opcoes;
	public int selecao;

	
	private bool cima;
	private bool baixo;
	private bool apertado;
	public int afff;
	public int afffasds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (Input.GetAxisRaw("Vertical")>0 && !apertado) 
		{
			cima = true;
			baixo = false;
			apertado = true;
		}
		
		
		
		if (Input.GetAxisRaw("Vertical")<0  && !apertado) 
		{
			cima = false;
			baixo = true;
			apertado = true;
			
//			afff++;
		}
		
		if (Input.GetAxisRaw("Vertical")==0 && apertado) 
		{
			cima = false;
			baixo = false;
			apertado = false;
//			afffasds++;
		}
		
		
		
		
		if (baixo && selecao < 1) 
		{
			selecao++;//seleciona o de baixo
		}
		
		if (cima && selecao > 0) 
		{
			selecao--;//seleciona o de cima
		}
		
		
		
		



	}
}
