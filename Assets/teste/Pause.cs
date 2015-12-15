	using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	public float tempo;

	public Camera cPause;//camera de pause
	public bool pause;
	public int selecao = 0;
	public bool sair = false;

	public GameObject menuSimNao;
	public GameObject menupause;
	public bool Mpause;
	public bool MSimNao;
	public TextMesh[] opcoesmenupause;
	public TextMesh bsim;
	public TextMesh bnao;
	
	
	private bool cima;
	private bool baixo;
	private bool direita;
	private bool esquerda;
	private bool apertadoh;
	private bool apertadoV;
	// Use this for initialization
	void Start () {
		pause = false;

		Controlador.singleton.saveAtual = Application.loadedLevel;
		Mpause = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		tempo = Time.timeScale;




		if (Input.GetButtonDown("Start") && menuSimNao.activeSelf == false)// && !pause)//Input.GetKeyDown (KeyCode.P)) 
			pause = !pause;
//		
		if(pause)
		{
			Time.timeScale = 0f;
			cPause.enabled = true;
			Controlador.singleton.Estado = Controlador.enumEstado.Pausado;
			if (Mpause) 
			{
				
				if (Input.GetAxisRaw("Vertical")>0 && !apertadoV) 
				{
					cima = true;
					baixo = false;
					apertadoV = true;
				}
				
				
				
				if (Input.GetAxisRaw("Vertical")<0  && !apertadoV) 
				{
					cima = false;
					baixo = true;
					apertadoV = true;
				}
				
				if (Input.GetAxisRaw("Vertical")==0 && apertadoV) 
				{
					cima = false;
					baixo = false;
					apertadoV = false;
				}



				
				if (baixo && selecao < 1) 
				{
					selecao++;//seleciona o de baixo
				}
				
				if (cima && selecao > 0) 
				{
					selecao--;//seleciona o de cima
				}
				



				switch (selecao) {
					case 0:
						opcoesmenupause[0].fontSize = 35;
//						opcoesmenupause[0].color = ativo;
						opcoesmenupause[1].fontSize = 30;
//						opcoesmenupause[1].color = inativo;
					
					break;
					
					case 1:
						opcoesmenupause[0].fontSize = 30;
//						opcoesmenupause[0].color = inativo;
						opcoesmenupause[1].fontSize = 35;
//						opcoesmenupause[1].color = ativo;
					break;
					
					
				}

				if (Input.GetButtonDown("BotaoA")) 
				{
					switch (selecao) {
						case 0:
							pause = false;

						break;

						case 1:
							MSimNao = true;
							Mpause = false;
							menupause.SetActive(false);
							menuSimNao.SetActive(true);
						break;


					}


				}


				
			}else
			{

				
				if (Input.GetAxisRaw("Horizontal")>0 && !apertadoh) 
				{
					direita = true;
					esquerda = false;
					apertadoh = true;
				}
				
				
				
				if (Input.GetAxisRaw("Horizontal")<0  && !apertadoh) 
				{
					direita = false;
					esquerda = true;
					apertadoh = true;
				}
				
				if (Input.GetAxisRaw("Horizontal")==0 && apertadoh) 
				{
					direita = false;
					esquerda = false;
					apertadoh = false;
				}



				if(esquerda)
				{
					sair = true;
					bsim.fontSize = 35;
					bnao.fontSize = 30;
					//						bsim.color = ativo;
					//						bnao.color = inativo;
				}else if(direita)
				{
					sair = false;
					bsim.fontSize = 30;
					bnao.fontSize = 35;
					
					//						bsim.color = inativo;
					//						bnao.color = ativo;
				}
				
				
				if(Input.GetButtonDown("BotaoA"))
				{
					if (sair)
					{
						Controlador.singleton.SalvarJogo();
						Application.LoadLevel("menu");
					}else
					{
						Mpause = true;
						MSimNao = false;
						menupause.SetActive(true);
						menuSimNao.SetActive(false);
					}
				}
			}

		}else
		{
			Time.timeScale = 1f;
			cPause.enabled = false;
			if (Controlador.singleton.Estado != Controlador.enumEstado.GameOver) 
			Controlador.singleton.Estado = Controlador.enumEstado.Jogando;
		}



	}
}
