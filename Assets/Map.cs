using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	//CHEAP AND EASY CONSTANTS
	public static int STARTING_ATTRIBUTE_VALUE = 100;


	//ATTRIBUTE NAMES
	public static readonly string Hunger = "Hunger";


	//SINGLETON
	public static Map Instance;

	//Internal Map Representation
	public Dictionary<Vector2, Tile> tiles;

	public int mapSize = 100;

	void Awake(){
		Instance = this;

		for(int i = 0; i<mapSize; i++){
			for(int j= 0; j<mapSize; j++){
				tiles.Add(new Vector2(i,j), new Tile(i, j, Random.Range(0, 100), Random.Range(0, 100)));
			}
		}
	}

	public Tile[] GetAdjacentTiles(Tile currentTile){
		List<Vector2> possibleAdjacenetLocations = new List<Vector2> ();

		for (int xDiff = -1; xDiff<2; xDiff++) {
			for(int yDiff = -1; yDiff<2; yDiff++){
				if(xDiff!=0 || yDiff!=0){
					possibleAdjacenetLocations.Add(new Vector2(xDiff+currentTile.x, yDiff+currentTile.y));
				}
			}
		}

		List<Tile> possibleAdjacentTiles = new List<Tile> ();
		foreach (Vector2 pos in possibleAdjacenetLocations) {
			if (tiles.ContainsKey(pos)){
				possibleAdjacentTiles.Add(tiles[pos]);
			}
		}
		return possibleAdjacentTiles.ToArray ();
	}




}
