using UnityEngine;
using System.Collections;
[RequireComponent(typeof(FisicaInimigos))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]
public class Chefe1 : MonoBehaviour {
	
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
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;


	private int tempoDano = 6;


//	private float contador = 0;
	public float finalAniMorte = 0.22f;
	
	//animacao
	private Animator anim;
	








	private float disty;
	private float distx;




	
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

				Debug.Log("acertou = Chefao");
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
//			contador = Time.deltaTime;
			anim.SetBool ("atacando", atacando);
			anim.SetBool ("morte", morto);
			//anim.SetBool ("perseguindo", perseguindo);
			anim.SetBool ("levadano", levadano);


			switch(Estado)
			{
				case enumEstado.Morto:
					Debug.Log("morreu");
					morto = true;
					currentSpeed = 0;
					this.GetComponent<BoxCollider>().enabled = false;
					this.GetComponent<CharacterController>().enabled = false;



//					if ((Time.deltaTime-contador)>finalAniMorte) {
//						Destroy(this.gameObject);
//					}
				break;
			
				case enumEstado.Atacado:
					
					levadano = true;


					Debug.Log("foi atacadooioooo");
				
					//levadano = false;

					if (tempoDano>5) 
					{
							Debug.Log("acertou = levou dano");

							
							levadano = false;
							atacando = false;
							
							Instantiate(espada.GetComponent<Espada>().ParticulaDano,espada.transform.position,Quaternion.identity);
							
	//						LevaDano(player.GetComponent<JogadorControler>().dano);

							if (vida>0) {
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
//								contador = Time.deltaTime;
								Estado = enumEstado.Morto;
							}

						tempoDano = 0;
				
						if (Estado != enumEstado.Morto)
							Estado = enumEstado.Perseguindo;
					}else{
						tempoDano++;
						if (Estado != enumEstado.Morto)
							Estado = enumEstado.Perseguindo;
					}
					
					
				break;
				
				case enumEstado.Atacando:
					if (!morto) 
					{
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
					}else
						Estado = enumEstado.Morto;
				break;
				
				case enumEstado.Perseguindo:
					//				if (InimigoPhysics.grounded) 
					//				{	
					//levadano = false;
					if (!morto) 
					{
						atacando = false;
						levadano = false;

		//				if (!perseguindo)
		//					perseguindo = true;

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
							
							Estado = enumEstado.Atacando;
							targetSpeed = 0;
							currentSpeed = 0;
						}
						
					


					}else
						Estado = enumEstado.Morto;
				break;
				
				
			}
			
			//		if(InimigoPhysics.movimentoStop)
			//		{
			//			targetSpeed = 0;
			//			currentSpeed = 0;
			//		}

			amountToMove.x = 0;
			//		if (player.transform.position.x < this.transform.position.x) //se player estiver a esquerda
			//		{
			//			amountToMove.x = currentSpeed;
			//		}else if (player.transform.position.x > this.transform.position.x) //se player estiver a direita
			//		{
			//			amountToMove.x = -currentSpeed;
			//			
			//		}
			
			amountToMove.x = currentSpeed;
			
			
			
			//amountToMove.y -= gravity * Time.deltaTime;
			InimigoPhysics.Move(amountToMove * Time.deltaTime);
		}
	}



//	public void LevaDano(int dano)
//	{
//
//
//			if (vida>0) {
//				vida -= dano;
//				Debug.Log("tirou vida - teste");
//				if (vida<=0)
//				{
//					vida = 0;
//					Estado = enumEstado.Morto;
//				}
//			}else
//			{
//				contador = Time.deltaTime;
//				Estado = enumEstado.Morto;
//			}
//
//			
//
//	}
	
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
