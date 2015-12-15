using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour {


	//public Light particulateste;

//controles de velocidade
	public float walkSpeed = 0.5f;
	public float runSpeed = 1f;
	public float jumpSpeed = 1.5f;
//controles de estados
	//verifica se esta ou nao no chao
	RaycastHit hit;
	float distancia;
	public bool noChao;


	public float limitadorGrav;//controla a força da gravidade, quanto maior o numero maior a gravidade

	public bool jumping;
	public bool jump = false;
	public bool run = false;
	public bool atacando;
//	public bool pendurado = false;

	public Vector3 escala;
//maquina de estados
//	enum enumEstado{

	//public float vida = 100f;


//	public GameObject Atacante;
//	public BoxCollider colAtaque;



	public Vector3 mover = Vector3.zero;
	public Vector3 gravidade = Vector3.zero;
	
	private CharacterController controler;


	public virtual void Start () {
		escala = transform.localScale;
		controler = GetComponent<CharacterController> ();
		//		colAtaque = Atacante.GetComponent<BoxCollider> ();
	}
	



//	void OnTriggerStay(Collider other) 
//	{
//		//inimigo identificando ataque de player
//		if (other.tag == "quina" && controler.detectCollisions) 
//		{
//			Debug.Log("quina");
//			pendurado = true;
//		}else
//		{
//			pendurado = false;
//		}
//
//		
//	}




	
	public virtual void Uspdate () 
	{

		if(gravidade.y < 0.0f)
		{
		//visualizando "pe" do jogador
			if (Physics.Raycast(transform.position, -Vector3.up, out hit))
			{
				distancia = hit.distance;
				Debug.DrawLine(transform.position, hit.point, Color.cyan);
			}
		}
		//Verificandop se jogador esta ou nao no chao	
			if(distancia < 1.1f )
			{
				//Debug.Log("no chao");
				noChao = true;
//				jumping = false;
				//tempoVelho = Time.time/200;
			}else
			{
				Debug.Log("no ar");
				noChao = false;


				
			}



		//Fazendo ele correr ou andar
			if(!run)
			{
				mover = new Vector3 (Input.GetAxis("Horizontal")*walkSpeed,0, 0.0f);
			}else
			{
				mover = new Vector3 (Input.GetAxis("Horizontal")*runSpeed,0, 0.0f);
			}
		//Identificando direçao de movimento
			if (Input.GetAxis("Horizontal")>0) 
			{
				
				//Debug.Log("direita");
				transform.localScale = escala;//rotacao = 180f;
			}else if (Input.GetAxis("Horizontal")<0) 
			{

				//Debug.Log("esquerda");
				transform.localScale = -escala;//rotacao = 180f;
				//transform.Rotate (0f, 0f, 0f);//rotacao = 0f;
			}


		//Condiçoes para gravidade funcionar   
			if (!noChao)// se nao estiver no chao e nem pendurado
			{
			gravidade += Physics.gravity *limitadorGrav;// aceleradorGrav;
				
				//animation.CrossFade(pulando);
				

//				if(animation.IsPlaying(iniPulo))//se ele nao esta iniciando pulo
//				{
//					
//				}

			}else//no chao
			{

				

//				if(animation.IsPlaying(pulando))
//				{
//					animation.CrossFade(chegandonochao);//aqui e a animacao de quando estava pulando e chegou no chao
//				}

				gravidade = Vector3.zero;

			
				//Fazendo animaçao de andar tocar
//				if (Input.GetAxis("Horizontal")!=0 ) 
//				{	
//					//animation.CrossFade("andando);
//				}

				if (jump)// && jumping == false) //pulo
				{
					
					//animation.CrossFade(iniPulo);
//					if(!animation.IsPlaying(iniPulo))//se ele nao esta pulando
//					{
						gravidade.y += jumpSpeed;
						

						//animation.CrossFade(pulando);//animacao de pulo
						
						//jumping = true;
						jump = false;
//					}

					
				}


		}
		mover += gravidade;
		
		controler.Move(mover);
	}
}
