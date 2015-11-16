using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	//CHEAP AND EASY CONSTANTS
	public static int STARTING_ATTRIBUTE_VALUE = 100;


	//ATTRIBUTE NAMES
	public static readonly string Lifespan = "Lifespan";
	public static readonly string Hunger = "Hunger";
	public static readonly string Thirst = "Thirst";
	public static readonly string Mobile = "Mobile";
	public static string[] allAttributes = new string[]{Lifespan, Hunger, Thirst, Mobile};

	//SINGLETON
	public static Map Instance;

	//Internal Map Representation
	public Dictionary<Vector2, Tile> tiles;
	public List<Agent> agents;

	public int mapSize = 100;

	//PLAYER ACTIONS
	private  bool addNutrients = true;//If not add nutrients, add water
	private float coolDown = 0;
	private float COOL_DOWN_MAX = 0.2f;

	public MapRenderer mapRenderer;

	void Awake(){
		Instance = this;
		mapRenderer.Init ();
		tiles = new Dictionary<Vector2, Tile> ();
		for(int i = 0; i<mapSize; i++){
			for(int j= 0; j<mapSize; j++){
				tiles.Add(new Vector2(i,j), new Tile(i, j, Random.Range(0, STARTING_ATTRIBUTE_VALUE), Random.Range(0, STARTING_ATTRIBUTE_VALUE)));

				mapRenderer.SpawnTile(i,j,tiles[new Vector2(i,j)].water, tiles[new Vector2(i,j)].nutrients);
			}
		}
		//TEST
		agents = new List<Agent> ();
		Agent testAgent = new Agent (0, 100, new AbstractState[]{new EatNutrients(5),new MoveRandom()}, tiles [new Vector2 (3, 3)]);
		Agent hunterAgent = new Agent (4, 100, new AbstractState[]{new EatAgent(10),new MovePrey()}, tiles [new Vector2 (8, 3)]);
		Agent hunterAgent2 = new Agent (4, 100, new AbstractState[]{new EatAgentNoncannibal(10),new MovePreyNoncannibal()}, tiles [new Vector2 (3, 8)]);
		mapRenderer.SpawnAgent (3, 3, testAgent);
		mapRenderer.SpawnAgent (9, 0, hunterAgent);
		mapRenderer.SpawnAgent (0, 9, hunterAgent2);
		agents.Add (testAgent);
		agents.Add (hunterAgent2);
		agents.Add (hunterAgent);

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

	//ALTER THIS TO ALTER 'TIME'
	float currTime = 0;
	const float MAX_TIME = 0.1f;
	void Update(){
		if (currTime < MAX_TIME) {
			currTime += Time.deltaTime;
		} else {
			currTime = 0;

			//Update each agent
			List<Agent> toRemove = new List<Agent> ();
			foreach (Agent a in agents) {
				//Debug.Log ("Agent: "+a.id+" has life "+a.attributes[Lifespan]+" has hunger "+a.attributes[Hunger]+" pos "+a.currentTile.pos);
				float prob = ((float)a.attributes[Lifespan])/((float)a.MAX_LIFESPAN);
				if (prob>Random.value){
					a.Update ();
				}

				if (a.CheckForDeath ()) {
					toRemove.Add (a);
				}
			}

			foreach (Agent dead in toRemove) {
				agents.Remove (dead);
			}

			//Update the renderer
			mapRenderer.UpdateMapRender (this);
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			addNutrients = !addNutrients;
			Debug.Log ("Add nuritents: " + addNutrients);
		}

		if (coolDown <= 0) {
			if (Input.GetMouseButton (0)) {


				Vector3 worldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
				Vector2 mousePos = new Vector2 ((int)worldPos.x, (int)worldPos.y);
				Debug.Log ("Mouse Pos: " + mousePos);
				if (tiles.ContainsKey (mousePos)) {
					if (addNutrients) {
						tiles [mousePos].nutrients += 10;
					} else {
						tiles [mousePos].water += 10;
					}
					coolDown = COOL_DOWN_MAX;
				}
			}
		} else {
			coolDown -= Time.deltaTime;
		}
	}
		
	public Tile[] GetAdjacentTilesOpen(Tile currentTile) {
		Tile[] adjacentTiles = this.GetAdjacentTiles (currentTile);
		List<Tile> openTiles = new List<Tile> ();

		foreach (Tile t in adjacentTiles) {
			if (t.Unoccupied) {
				openTiles.Add(t);
			}
		}
		return openTiles.ToArray();
	}

//	public List<Agent> GetMapAgents() {
//		List<Agent> agents = new List<Agent> ();
//		for (int x = 0; x < this.mapSize; x++) {
//			for (int y = 0; y < this.mapSize; y++) {
//				Vector2 xy = new Vector2(x,y);
//				if (!tiles[xy].Unoccupied) {
//					agents.Add(tiles[xy].occupiedAgent);
//				}
//			}
//		}
//
//		return agents;
//	}


}
