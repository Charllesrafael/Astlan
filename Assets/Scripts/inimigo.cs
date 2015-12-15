using UnityEngine;
using System.Collections;
[RequireComponent(typeof(FisicaInimigos))]
public class inimigo : MonoBehaviour {
	
	private FisicaInimigos InimigoPhysics;

	public bool Ativado;

	public bool atacando;
	public float vida = 20;
	public float distAtk;//distancia para atacar
	public float tempAtaque;//contador para comparar tempo de ataque
	public float AtaqueProx;//tempo de um ataque para o outro
	
	private GameObject player;
	private GameObject espada;
	
	
	private bool direita = true;
	private bool esquerda = false;
	
	public int Dano;//dano que ele da ao player
	public bool morto = false;
	public bool perseguindo;
	public bool levadano;

	public float speed = 8;
	public float acceleration = 30;
	
	//controle de maquina de estados
	public enum enumEstado
	{
		Morto,
		Perseguindo,
		Atacando,
		Atacado
	}
	public enumEstado Estado;
	
	public float currentSpeed;
	public float targetSpeed;
	private Vector2 amountToMove;






	public int tempoDano = 6;


	public float contador = 0;
	public float finalAniMorte = 0.22f;
	
	//animacao
	private Animator anim;
	








	public float disty;
	public float distx;




	
	// Use this for initialization
	void Start () {
		InimigoPhysics = GetComponent<FisicaInimigos>();
		tempAtaque = Time.time;
		player = Controlador.singleton.Player;
		espada = Controlador.singleton.Player.GetComponent<JogadorControler>().espada;
		anim = GetComponent<Animator>();
	}



	public void CriaPart()
	{
		Instantiate(espada.GetComponent<Espada>().ParticulaDano,new Vector3(player.transform.position.x,player.transform.position.y+1.5f,player.transform.position.z),Quaternion.identity);
	}
	
	void OnTriggerStay(Collider other)
	{
		
		if (other.tag == "espada" && Ativado)
		{	Debug.Log("nao mudou de estado");
			if (espada.GetComponent<Espada>().acertou && Estado != enumEstado.Morto)
			{
				Debug.Log("trocou de estado");
				
				Debug.Log("acertou = piranha");
				Estado = enumEstado.Atacado;
				
				
			}
		}
		
	}

	// Update is called once per frame
	void Update () {
		//		transform.Rotate(Time.deltaTime*50, 0, 0);
		//		transform.Rotate(0, Time.deltaTime*50, 0, Space.World);
		if (Ativado) 
		{

			if (this.transform.position.z != 0f) 
			{
				this.transform.position =  new Vector3(this.transform.position.x,this.transform.position.y, 0f);
			}
			contador = Time.time;
			anim.SetBool ("atacando", atacando);
			anim.SetBool ("morte", morto);
			//anim.SetBool ("perseguindo", perseguindo);
			anim.SetBool ("levadano", levadano);


			switch(Estado)
			{
				case enumEstado.Morto:
					levadano = false;
					morto = true;
					currentSpeed = 0;
					this.GetComponent<BoxCollider>().enabled = false;
					this.GetComponent<CharacterController>().enabled = false;

//
//					if ((Time.deltaTime-contador)>finalAniMorte) {
//						Destroy(this.gameObject);
//					}
				break;
			
				case enumEstado.Atacado:
					

					Debug.Log("atacadooo");
				
					//levadano = false;

					if (tempoDano>5) 
					{
						Debug.Log("acertou = aaaaaaaaaa");
						
					
						levadano = true;
						atacando = false;
						
						Instantiate(espada.GetComponent<Espada>().ParticulaDano,espada.transform.position,Quaternion.identity);
						
						//						LevaDano(player.GetComponent<JogadorControler>().dano);
						
						if (vida>0) 
						{
							vida -= player.GetComponent<JogadorControler>().dano;
							Debug.Log("tirou vida - teste");
							if (vida<=0)
							{
								vida = 0;
								Debug.Log("vida = 0");
								//								Destroy(this.gameObject);
								Estado = enumEstado.Morto;
							}
						}else
						{
							contador = Time.deltaTime;
							Estado = enumEstado.Morto;
						}
						
						
					}else{
						tempoDano++;
						if (Estado != enumEstado.Morto)
							Estado = enumEstado.Perseguindo;
					}
				break;
				
				case enumEstado.Atacando:
					levadano = false;
					if ((Time.time-tempAtaque)> AtaqueProx) 
					{
						
						if (player.GetComponent<JogadorControler>().Vida > 0) 
						{
							player.GetComponent<JogadorControler>().Vida -= Dano;
							Debug.Log("atacou player");
							
						}else
							Estado = enumEstado.Morto;
						
						tempAtaque = Time.time;
						if (!atacando)
							atacando = true;
						
						Estado = enumEstado.Perseguindo;
						
					}else
					{
						atacando = false;
						
						Estado = enumEstado.Perseguindo;
					}
				break;
				
				case enumEstado.Perseguindo:
					levadano = false;
					atacando = false;



					if (!perseguindo)
						perseguindo = true;

					if (player.transform.position.x > this.transform.position.x && !direita && esquerda) 
					{
						direita = true;
						esquerda = false;
						transform.Rotate (new Vector3 (0,180,0), Space.Self);
						currentSpeed = 0;
					}
					
					if (player.transform.position.x < this.transform.position.x && !esquerda & direita) 
					{
						direita = false;
						esquerda = true;
						transform.Rotate (new Vector3 (0,180,0), Space.Self);
						currentSpeed = 0;
					}
					
					
					Debug.Log("Perseguindo");

					distx = Vector2.Distance(new Vector2(this.transform.position.x,0f),new Vector2(player.transform.position.x,0f));
					disty = Vector2.Distance(new Vector2(this.transform.position.y,0f),new Vector2(player.transform.position.y,0f));


					

					if (disty>2)
					{
						amountToMove.y = -1;
					}else if (disty<-2)
					{
						amountToMove.y = 1;
					}else
					{
						amountToMove.y = 0;
					}

				
				if (distx > distAtk) 
					{
						if (player.transform.position.x < this.transform.position.x) //se player estiver a esquerda
						{
							targetSpeed = -1f * speed;
						}else if (player.transform.position.x > this.transform.position.x) //se player estiver a direita
						{
							targetSpeed = 1f * speed;
							
						}
						
						currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
					}else
					{
						if ((Time.time-tempAtaque)> AtaqueProx) 
						{
							

							Estado = enumEstado.Atacando;
							targetSpeed = 0;
							currentSpeed = 0;
						}else
						{
							atacando = false;
						}
					


					}
					
				

					
				//				}
				break;
				
				
			}
			

			amountToMove.x = 0;
			
			amountToMove.x = currentSpeed;
			
			
			
			
			InimigoPhysics.Move(amountToMove * Time.deltaTime);
		}
	}



	public void LevaDano(int dano)
	{
		if (Estado != enumEstado.Morto) 
		{
			if (vida>0) {
				vida -= dano;
			}else
			{
				contador = Time.deltaTime;
				Estado = enumEstado.Morto;
			}

			//Estado = enumEstado.Atacado;
		}
	}
	
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}
