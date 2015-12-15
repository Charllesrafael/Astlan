using UnityEngine;
using System.Collections;
using System.IO;

public class Menu : MonoBehaviour {

	public GameObject[] menu;
	public int selecao;
	public int MinSelecao;
	public int MaxSelecao;
	public bool selecionado = false;


	public int selecaoSalves = 0;
	public GameObject[] btSalves;
	public Material[] materiais;


	public Camera principal;
	public Camera Save;
	public Camera instrucao;
	
	private bool cima;
	private bool baixo;
	private bool apertado;

	//continue top
//	public bool Continue;//se existe continue





	//texturas
//	public Texture apertado;
//	public Texture naoApertado;



	// Use this for initialization
	void Start () {
		selecao = 0;
		MaxSelecao = menu.Length-1;

	}











	void Update()
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
		}

		if (Input.GetAxisRaw("Vertical")==0 && apertado) 
		{
			cima = false;
			baixo = false;
			apertado = false;
		}




		if (principal.enabled) 
		{
			
				

			if (baixo && selecao < MaxSelecao) //Input.GetKeyDown("s") && 
			{
				selecao++;//seleciona o de baixo
			}

			if (cima && selecao > MinSelecao) //Input.GetKeyDown("w")
			{
				selecao--;//seleciona o de cima
			}


			if (Input.GetButtonDown("Start") || Input.GetButtonDown("BotaoA") ) 
			{
				selecionado = true;
			}

		

			//altera visualmente o menu principal
			switch (selecao)
			{
				
				case 0://jogar
					menu[0].GetComponent<TextMesh>().fontSize = 35;
					menu[1].GetComponent<TextMesh>().fontSize = 30;
					menu[2].GetComponent<TextMesh>().fontSize = 30;
				break;
				
				case 1://instrucoes
					menu[0].GetComponent<TextMesh>().fontSize = 30;
					menu[1].GetComponent<TextMesh>().fontSize = 35;
					menu[2].GetComponent<TextMesh>().fontSize = 30;
				break;
				
				case 2://sair
					menu[0].GetComponent<TextMesh>().fontSize = 30;
					menu[1].GetComponent<TextMesh>().fontSize = 30;
					menu[2].GetComponent<TextMesh>().fontSize = 35;
				break;


			}

			if (instrucao.enabled == true && Input.GetButtonDown("Start") || Input.GetButtonDown("BotaoA")) 
			{
				Debug.Log("Saiir instrucoes");
				instrucao.enabled = false;
				principal.enabled = true;
			}
			//acoes referente a cada selecao no menu principal
			if (selecionado)
			{
				switch (selecao)
				{
					
					case 0://jogar
						
						Save.enabled = true;	
						principal.enabled = false;
						Debug.Log("jogar");
						selecionado = false;
						selecaoSalves = 0;
					break;

					case 1://instrucoes
						Debug.Log("instrucoes");
						selecionado = false;
						instrucao.enabled = true;
						principal.enabled = false;
						
					break;

					case 2://sair
						Debug.Log("sair");
						selecionado = false;
					break;


				}
			}
		}else//fim camera principal
		{
			if (instrucao.enabled && Input.GetButtonDown("Start") || instrucao.enabled && Input.GetButtonDown("BotaoA")) 
			{
				instrucao.enabled = false;
				principal.enabled = true;
			}else
			{



				if (baixo && selecaoSalves < 2) 
				{
					selecaoSalves++;//seleciona o de baixo
				}
				
				if (cima && selecaoSalves > 0) 
				{
					selecaoSalves--;//seleciona o de cima
				}
				switch (selecaoSalves) 
				{
					case 0:
						btSalves[0].GetComponent<MeshRenderer>().material = materiais[0];
						btSalves[1].GetComponent<MeshRenderer>().material = materiais[1];
						btSalves[2].GetComponent<MeshRenderer>().material = materiais[1];

					break;
					case 1:
						btSalves[0].GetComponent<MeshRenderer>().material = materiais[1];
						btSalves[1].GetComponent<MeshRenderer>().material = materiais[0];
						btSalves[2].GetComponent<MeshRenderer>().material = materiais[1];

					break;
					case 2:
						btSalves[0].GetComponent<MeshRenderer>().material = materiais[1];
						btSalves[1].GetComponent<MeshRenderer>().material = materiais[1];
						btSalves[2].GetComponent<MeshRenderer>().material = materiais[0];

					break;
					
				}
				
				if (Input.GetButtonDown("Start") && instrucao.enabled == false || Input.GetButtonDown("BotaoA") && instrucao.enabled == false) 
				{
					Debug.Log(selecaoSalves);
					switch (selecaoSalves) 
					{
						case 0:
							Controlador.singleton.saveOrdem = "A";
							if (btSalves[0].GetComponent<Salve>().fa != -1) 
							{
								Application.LoadLevel(btSalves[0].GetComponent<Salve>().fa);
							}else
							{
								Application.LoadLevel(1);
							}
						break;
						case 1:
							Controlador.singleton.saveOrdem = "B";
							if (btSalves[1].GetComponent<Salve>().fa != -1) 
							{
								Application.LoadLevel(btSalves[1].GetComponent<Salve>().fa);
							}else
							{
								Application.LoadLevel(1);
							}
							break;
						case 2:
							Controlador.singleton.saveOrdem = "C";
							if (btSalves[2].GetComponent<Salve>().fa != -1) 
							{
								Application.LoadLevel(btSalves[2].GetComponent<Salve>().fa);
							}else
							{
								Application.LoadLevel(1);
							}
						break;

					}
				}
			}	
		}

		if (apertado) 
		{
			cima = false;
			baixo = false;
		}

		if(Input.GetAxisRaw("Vertical")==0)
			apertado = false;

	}
}
