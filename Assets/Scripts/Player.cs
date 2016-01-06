using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;

	private Vector3 dir;

	// Use this for initialization
	void Start () {
		dir = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
		float amountToMove = speed * Time.deltaTime;

		//On Input... (Left Mouse for Dev work)
		if(Input.GetMouseButtonDown(0)){

			//If not moving left on click, move left
			//or if not moving forward on click, move forward
			if(dir != Vector3.left){
				dir = Vector3.left;
			} else if(dir != Vector3.forward){
				dir = Vector3.forward;
			}

		}

		//Move the amount to move, in the chosen direction
		transform.Translate(dir * amountToMove);
	
	}
}
