using UnityEngine;
using System.Collections;

public class Cameras : MonoBehaviour {

	public GameObject target;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;



	void Start()
	{
		target = Controlador.singleton.Player.GetComponent<JogadorControler>().olho;
		transform.position = target.transform.TransformPoint(new Vector3(0, 0, -10));
	}


	void Update() {
		Vector3 targetPosition = target.transform.TransformPoint(new Vector3(0, 0, -10));
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x,targetPosition.y,-10), ref velocity, smoothTime);
	}
}
