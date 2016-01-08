using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	private float speed = 8;
	private Vector3 dir;
	private bool isDead;
	private int score = 0;
	private int multiplyer = 1;

	public GameObject PickupParticles;
	public GameObject ResetButton;
	public Text ScoreText; 
	public Text MultiplyerText;
	public Text FinalScoreText;
	public Text HighscoreText;
	public Text NewHighscoreText;
	public Animator GameOverAnim;

	// Use this for initialization
	void Start () {
		dir = Vector3.forward;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		float amountToMove = speed * Time.deltaTime;

		//On Input... (Left Mouse for Dev work)
		if(Input.GetMouseButtonDown(0) && !isDead){

			score += multiplyer;
			ScoreText.text = score.ToString();

			//If not moving left on click, move left
			//or if not moving forward on click, move forward
			if(dir != Vector3.left){
				transform.GetChild(0).transform.Rotate(Vector3.up, -90);
				dir = Vector3.left;
			} else if(dir != Vector3.forward){
				dir = Vector3.forward;
				transform.GetChild(0).transform.Rotate(Vector3.up, 90);
			}
		//	

		}

		//Move the amount to move, in the chosen direction
		transform.Translate(dir * amountToMove);
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "PickUp"){
			multiplyer++;
			MultiplyerText.text = "x " + multiplyer.ToString();

			if(multiplyer % 3 == 0){
				speed+=2;
			}

			other.gameObject.SetActive(false);
			Instantiate(PickupParticles,transform.position,Quaternion.identity);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Tile"){
			RaycastHit hit;
			Ray downRay = new Ray(transform.position, -Vector3.up);

			if(!Physics.Raycast(downRay, out hit)){
				GameOver();
				transform.DetachChildren();
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Wall"){
			if(dir == Vector3.forward){
				dir = Vector3.left;
			} else {
				dir = Vector3.forward;
			} 
		}
	}

	private void GameOver(){
		isDead = true;
		speed = 0;
		GameOverAnim.SetTrigger("GameOver");
		FinalScoreText.text = score.ToString();
		int Highscore = PlayerPrefs.GetInt("Highscore",0);
		if(score > Highscore){
			NewHighscoreText.gameObject.SetActive(true);
			PlayerPrefs.SetInt("Highscore",score);
		}
		HighscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
	}
}
