using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class JogadorControler : MonoBehaviour {
	

	public float tempo = 0;

	//Dafuq??
	public float Vida = 2f;

	public GameObject particula;
	public int dano = 1;

	//Player component variables
	Animator anim_playerAnimator ;
	public GameObject go_playerObj;
	public GameObject olho;
	public GameObject espada;


	public float f_speedMultiplier;

	bool b_IsDead = false;
	bool b_canMove = true;
	
	
	//Movement
	CapsuleCollider col_playerCollider;
	
	bool b_goingRight = true;
	public float f_pathPercentage = 0f;
	
	public float f_playerSpeed = 5f;
	public float f_playerSpeedAdjustment = 1000;
	
	public float f_jumpSpeed = 5f;
	float f_distanceToGround;
	
	public float f_dashSpeed = 1f;
	public float f_dashDelay = 1.5f;
	public float f_dashDuration = 0.3f;
	bool b_canDash = true;
	
	//Trail Control
	//regula os pontos da trilha
	//public Transform[] TrailNodes; //lista de pontos
	//public int currentNode = 1; //ponto inicial. deixei public so pra olhar de fora. tem que comecar o no 1 que eh o prox ponto na frente quando da spawn
	//public Vector3 v3_trailSectionFoward;
	
	//public float turnSpeed = 5f; //velocidade que o player vira
	
	//Attacking
	public float f_attackSpeed = 2f;
	bool b_attackLocked = false;

	void Awake() {
		
		Controlador.singleton.Player = this.gameObject;
		}
	void Start () {
		iTween.MoveUpdate(gameObject, iTween.Hash("position", iTween.PointOnPath(iTweenPath.GetPath("PlayerPath"), f_pathPercentage)));
		
		b_goingRight = true;
		//v3_trailSectionFoward = GetTrailSection();
		
		anim_playerAnimator = go_playerObj.GetComponent<Animator>();
		col_playerCollider = go_playerObj.GetComponent<CapsuleCollider>();
		
		//StartCoroutine("LookAtPoint");
		f_distanceToGround = col_playerCollider.bounds.extents.y;


	}
	
	void Update () {
		
		if(!b_IsDead){			
			
			
			//Foward movement
			f_speedMultiplier = Input.GetAxis("Horizontal");

			if (f_speedMultiplier != 0 && IsGrounded()) {
				particula.GetComponent<ParticleEmitter>().emit = true;
			}else
				particula.GetComponent<ParticleEmitter>().emit = false;
			
			if(f_speedMultiplier != 0){
				//transform.Translate(f_playerSpeed*v3_trailSectionFoward*Time.deltaTime, Space.World);
				f_pathPercentage += f_playerSpeed*(1/f_playerSpeedAdjustment)*f_speedMultiplier;
				f_pathPercentage = Mathf.Clamp01(f_pathPercentage);
				//iTween.Stop();
				iTween.MoveUpdate(gameObject, iTween.Hash("position", iTween.PointOnPath(iTweenPath.GetPath("PlayerPath"), f_pathPercentage), "orienttopath", true));
				//iTween.PutOnPath(gameObject, iTweenPath.GetPath("PlayerPath"), f_pathPercentage);
			}
			LockRotation();
			
			if(f_speedMultiplier > 0 && !b_goingRight){
				b_goingRight = true;
				//ChangeNode();
				//go_playerObj.transform.Rotate(transform.up, 180f);
			}
			else if(f_speedMultiplier < 0 && b_goingRight){
				b_goingRight = false;
				//go_playerObj.transform.Rotate(transform.up, 180f);
				//ChangeNode();
			}
			
			anim_playerAnimator.SetFloat("Speed", Mathf.Abs(f_playerSpeed*f_speedMultiplier));
			
			//Jump
			if(Input.GetButtonDown("Jump") && IsGrounded()){
				Vector3 aux = go_playerObj.rigidbody.velocity;
				aux.y += f_jumpSpeed*1;//Vector3.up;
				go_playerObj.rigidbody.velocity = aux;

				anim_playerAnimator.SetTrigger("Jump");
				
			}

			if (anim_playerAnimator.GetBool("Jump") == true) {
				if (tempo < 1) {
					tempo++;
				}else
				{
					anim_playerAnimator.SetBool("Jump",false);
					tempo = 0;
				}
			}
			
			//Attack
			if(Input.GetButton("Fire1"))
				Attack();
			
			//Dash
			if(Input.GetButtonDown("Dash") && b_canDash)
				StartCoroutine("Dash");
			
			//Die
			if(Vida <= 0){
				anim_playerAnimator.SetBool("Death",true);
				b_IsDead = true;
			}
			
		}
		
		//CorrectVelocity();
		
	}
	
	public void ataca()
	{
		this.GetComponentInChildren<Espada> ().acertou = true;
		Debug.Log ("comocou ataque");
	}
	
	public void Fimataca()
	{
		this.GetComponentInChildren<Espada> ().acertou = false;
		Debug.Log ("Fim Ataque");
	}
	
	void Attack(){
		if(!b_attackLocked){
			b_attackLocked = true;
			particula.GetComponent<ParticleEmitter>().emit = false;
			rigidbody.velocity = Vector3.zero;
			//anim_playerAnimator.SetTrigger("Attack");
			anim_playerAnimator.Play("Attack", 0);
			Invoke("ResetAttack", 1/f_attackSpeed);
		}
	}
	
	void ResetAttack(){
		b_attackLocked = false;
	}
	
	
	//troca o 'proximo ponto'. se o player esta olhando pra direita, vai pro prox na lista. se ele esta voltando, vai pro anterior.
	/*void ChangeNode(){
		if(b_goingRight)
			currentNode++;
		else
			currentNode--;

		currentNode = Mathf.Clamp(currentNode, 0, TrailNodes.Length-1); //n deixa o ponto sair dos limites do array.

		v3_trailSectionFoward = GetTrailSection();
		StopCoroutine("LookAtPoint");
		StartCoroutine("LookAtPoint");

	}*/
	
	/*Vector3 GetTrailSection(){

		Vector3 v3_section;

		if(b_goingRight){
			v3_section = TrailNodes[currentNode].position - TrailNodes[currentNode-1].position;
			v3_section = new Vector3(v3_section.x, 0, v3_section.z);
		}
		else{
			v3_section = TrailNodes[currentNode].position - TrailNodes[currentNode+1].position;
			v3_section = new Vector3(v3_section.x, 0, v3_section.z);
		}

		return v3_section.normalized;
	}*/
	
	
	bool IsGrounded(){
		RaycastHit hit;
		if(Physics.Raycast(go_playerObj.transform.position, -Vector3.up, out hit, f_distanceToGround/3))
			return (hit.collider.tag == "Terrain");
		
		return false;
	}
	
	/*IEnumerator LookAtPoint(){
		//DIOGO: Isso aqui d'a um smooth lookat pro proximo ponto da trilha. Sempre. eh.
		//rotation =D
		Vector3 v3_targetPoint = new Vector3(TrailNodes[currentNode].position.x, transform.position.y, TrailNodes[currentNode].position.z);
		Quaternion quat_targetRotation = Quaternion.LookRotation(v3_targetPoint - (b_goingRight ? TrailNodes[currentNode - 1].position : TrailNodes[currentNode + 1].position));
		
		// Smoothly rotate towards the target point.
		while(transform.rotation != quat_targetRotation){
			transform.rotation = Quaternion.Slerp(transform.rotation, quat_targetRotation, turnSpeed * Time.deltaTime);
			yield return null;
		}
	}*/
	
	IEnumerator Dash(){
		//rigidbody.velocity += f_dashForce*transform.forward;
		f_playerSpeed += f_dashSpeed;
		b_canDash = false;
		yield return new WaitForSeconds(f_dashDuration);
		f_playerSpeed -= f_dashSpeed;
		yield return new WaitForSeconds(f_dashDelay - f_dashDuration);
		b_canDash = true;
		Debug.Log ("Dash");
	}
	
	void LockRotation(){
		transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
	}
	
	void CorrectVelocity(){
		Vector3 temp = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
		temp = temp.magnitude*transform.forward;
		rigidbody.velocity = new Vector3(temp.x, rigidbody.velocity.y, temp.z);
	}
	
	
	
}
