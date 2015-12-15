using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(CharacterController))]
public class FisicaInimigos : MonoBehaviour {


	public CharacterController controle;



	public LayerMask collisionMask;
	private BoxCollider colisor;
	private Vector3 s;
	private Vector3 c;
//	float dir = 1;
	
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool movimentoStop;

	Ray ray;
	RaycastHit hit;


	// Use this for initialization
	void Start () {
		colisor = GetComponent<BoxCollider>();
		s = this.transform.localScale;
		c = colisor.center;


		controle = this.GetComponent<CharacterController> ();
	}
	

	public void Move(Vector2 moveAmount){
		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector2 p = this.transform.position;


	



//		grounded = false;
//		
//		for (int i = 0; i < 3; i++) 
//		{
//			float dir = Mathf.Sign(deltaY);
//			float x = (p.x +c.x -s.x/2) +s.x/2 * i;
//			float y = p.y + c.y +s.y/2 *dir;
//			
//			
//			ray = new Ray(new Vector2(x,y),new Vector2(0,dir));
//			
//			Debug.DrawRay(ray.origin,ray.direction);
//			
//			
//			if(Physics.Raycast(ray,out hit,Mathf.Abs(deltaY) +skin,collisionMask))
//			{
//				float Dist = Vector3.Distance(ray.origin, hit.point);
//
//				if(Dist > skin)
//				{
//					deltaY = Dist* dir -skin *dir;
//				}else
//				{
//					deltaY = 0;
//				}
//				grounded = true;
//				break;
//			}
//
//
//		}

//		movimentoStop = false;
//		
//		for (int i = 0; i < 3; i++) 
//		{
//
//		
//
//			if (this.GetComponent<inimigo>().targetSpeed >0 && this.gameObject.tag == "Inimigo") 
//			{
//				dir = 1;
//			}else if (this.GetComponent<inimigo>().targetSpeed <0 && this.gameObject.tag == "Inimigo") {
//				dir = -1;
//			}
//
//
//			float x = p.x + c.x +s.x/2 *dir;
//			float y = p.y + c.y -s.y/2  +s.y/2 * i ;
//			
//			
//			ray = new Ray(new Vector2(x,y),new Vector2(dir,0));
//			
//			Debug.DrawRay(ray.origin,ray.direction);
//			
//			if(Physics.Raycast(ray,out hit,Mathf.Abs(deltaX) +skin,collisionMask))
//			{
////				float Dist = Vector3.Distance(ray.origin, hit.point);
////				
////				
////				if(Dist > skin)
////				{
////					deltaX = Dist* dir -skin *dir;
////				}else
////				{
//					deltaX = 0;
////				}
//				movimentoStop = true;
//				break;
//			}
//
//		}


		Vector3 finalTransform = new Vector3(deltaX, deltaY,0);
		//transform.Translate (finalTransform);

		//this.transform.Translate (finalTransform);
		controle.Move(finalTransform);
	}






}
