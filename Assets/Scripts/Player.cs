using UnityEngine;
using System.Collections;


[RequireComponent (typeof(CharacterController))]//vearifica se existe CharacterController

public class Player : BasePlayer {




	// Use this for initialization
	public override void   Start () {

		base.Start();
	}
	
	// Update is called once per frame
//	public override  void Update () {
//
////		if(!animation.IsPlaying("atacando"))
////		{
//			if(noChao && Input.GetKeyDown(KeyCode.X))//verifica se esta pulando else esta noChao noChao else apertou X
//			{
//				//animation.CrossFade("jump");
//				jump = true;//pulando
//			}
//
//
//			run = Input.GetKey (KeyCode.Z);//correndo
//
//			if( Input.GetKeyDown (KeyCode.F))
//			{
//				atacando = true;
//			}else
//			{
//				atacando = false;
//			}
////		}
//
//
//		base.Update ();
//	}
}
