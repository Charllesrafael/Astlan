using UnityEngine;
using System.Collections;
[RequireComponent(typeof(FisicaInimigos))]
[RequireComponent(typeof(NoChao))]
public class inimigo1 : MonoBehaviour {
	
	private FisicaInimigos InimigoPhysics;

	public NoChao scrNoChao;

	public bool atacado;
	public bool atacando;
	public float vida = 20;
	public float distAtk;//distancia para atacar
	public float tempAtaque;//contador para comparar tempo de ataque
	public float AtaqueProx;//tempo de um ataque para o outro
	
	public GameObject player;
	
	public int Dano;//dano que ele da ao player
	
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	
	//controle de maquina de estados
	public enum enumEstado{Perseguindo,Atacando,Atacado};
	public enumEstado Estado;
	
	public float currentSpeed;
	public float targetSpeed;
	private Vector2 amountToMove;
	
	
	// Use this for initialization
	void Start () {
		InimigoPhysics = GetComponent<FisicaInimigos>();
		scrNoChao = GetComponent<NoChao>();
		tempAtaque = Time.time;
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		//		transform.Rotate(Time.deltaTime*50, 0, 0);
		//		transform.Rotate(0, Time.deltaTime*50, 0, Space.World);
		if (this.transform.position.z != 0f) 
		{
			this.transform.position =  new Vector3(this.transform.position.x,this.transform.position.y, 0f);
		}
		
		switch(Estado)
		{
		case enumEstado.Atacado:
			if (vida >0)
			{
				vida--;
				Debug.Log("levou dano");
				Estado = enumEstado.Perseguindo;
			}else
			{
				Destroy(this.gameObject);
			}
			
			break;
			
		case enumEstado.Atacando:
			
			if ((Time.time-tempAtaque)> AtaqueProx) 
			{
				tempAtaque = Time.time;
				if (player.GetComponent<JogadorControler>().Vida>0) 
				{
					player.GetComponent<JogadorControler>().Vida -= Dano;
					Debug.Log("atacou");
				}
				
			}
			Estado = enumEstado.Perseguindo;
			break;
			
		case enumEstado.Perseguindo:
			if (scrNoChao.noChao()) 
			{	
				Debug.Log("Perseguindo");
				float dist = Vector3.Distance(this.transform.position,player.transform.position);
				
				if (dist > distAtk) 
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
				
				
			}
			break;
			
			
		}
		
		if(InimigoPhysics.movimentoStop)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}
		
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
		
		
		
		amountToMove.y -= gravity * Time.deltaTime;
		InimigoPhysics.Move(amountToMove * Time.deltaTime);
		
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
