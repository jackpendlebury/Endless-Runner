using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
	
	public GameObject[] tilePrefabs;
	public GameObject currentTile;

	private Stack<GameObject> leftTiles =  new Stack<GameObject>();
	public Stack<GameObject> LeftTiles {
		get {
			return leftTiles;
		}
		set {
			leftTiles = value;
		}
	}

	private Stack<GameObject> topTiles =  new Stack<GameObject>();
	public Stack<GameObject> TopTiles {
		get {
			return topTiles;
		}
		set {
			topTiles = value;
		}
	}
	
	private static TileManager instance;
	public static TileManager Instance {
		get {
			if(instance == null){
				instance = GameObject.FindObjectOfType<TileManager>();
			}
			return TileManager.instance;
		}
	}

	void Start () {
		createTiles(30);
		for(int i = 0; i < 20; i++){
			spawnTile();
		}
	}
	
	void Update () {
		
	}

	//Method for instantiating potential tiles with the Stacks
	public void createTiles(int amount){
		for(int i = 0; i <= amount; i++){
			leftTiles.Push(Instantiate(tilePrefabs[0]));
			topTiles.Push(Instantiate(tilePrefabs[1]));

			topTiles.Peek().name = "TopTile";
			leftTiles.Peek().SetActive(false);

			leftTiles.Peek().name = "LeftTile";
			topTiles.Peek().SetActive(false);
		}
	}

	public void spawnTile(){

		//When the Stack's are out of tiles, create 10 more.
		if(leftTiles.Count == 0 || topTiles.Count == 0){
			createTiles(10);
		}

		//Grab a Random Tile (Either Left or Top)
		int randomTile= Random.Range(0,2);

		//Take the next inactive tile from the respective Stack, and place it in the correct position.
		if(randomTile == 0){
			GameObject tmp = leftTiles.Pop();
			tmp.SetActive(true);
			//Set Position to CurrentTile's attach point
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomTile).position;
			currentTile = tmp;
		} else if(randomTile == 1){
			GameObject tmp = topTiles.Pop();
			tmp.SetActive(true);
			//Set Position to CurrentTile's attach point
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomTile).position;
			currentTile = tmp;
		} 
	}
}
