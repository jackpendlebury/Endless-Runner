using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	//Script Attched to each tile.

	//If the Player is the Obvject Colliding with the Box Trigger...
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			TileManager.Instance.spawnTile();
			StartCoroutine(FallDown());
		}
	}

	//..Cause the block to fall, eventually despawn and re-add itself to the stack.
	IEnumerator FallDown(){
		yield return new WaitForSeconds(1.2f);
		GetComponent<Rigidbody>().isKinematic = false;

		yield return new WaitForSeconds(2);

		switch(gameObject.name){
		case "LeftTile":
			TileManager.Instance.LeftTiles.Push(gameObject);
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.SetActive(false);
			break;

		case "TopTile":
			TileManager.Instance.TopTiles.Push(gameObject);
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.SetActive(false);
			break;
		}
	}
}
