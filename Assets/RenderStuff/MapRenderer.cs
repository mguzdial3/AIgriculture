using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour {
	public GameObject tilePrefab, agentPrefab;

	public Dictionary<Vector2, TileRenderer> tileObjects;
	public List<AgentRenderer> agentObjects;


	public static Color[] animalColors = new Color[]{new Color32(178, 31, 53, 255), new Color32(216, 39, 53, 255), new Color32(255, 116, 53, 255), 
		new Color32(255, 161, 53, 255), new Color32(255, 203, 53, 255), new Color32(255, 240, 53, 255), new Color32(0, 117, 58, 255), new Color32(0, 158, 71, 255), 
		new Color32(22, 221, 53, 255), new Color32(0, 82, 165, 255), new Color32(0, 121, 231, 255), new Color32(0, 169, 252, 255), new Color32(104, 30, 126, 255), 
		new Color32(125, 60, 181, 255), new Color32(189, 122, 246, 255)};

	public void Init(){
		tileObjects = new Dictionary<Vector2, TileRenderer> ();
		agentObjects = new List<AgentRenderer> ();
	}

	public void SpawnTile(int x, int y, int water, int nutrients){
		GameObject newTileObj = GameObject.Instantiate (tilePrefab);
		TileRenderer tileRender = newTileObj.GetComponent<TileRenderer> ();
		newTileObj.transform.position = new Vector3 (x, y, 0);
		tileRender.InitialValueSet (((float)water) / ((float)Map.STARTING_ATTRIBUTE_VALUE), ((float)nutrients) / ((float)Map.STARTING_ATTRIBUTE_VALUE));
		tileObjects.Add (new Vector2 (x, y), tileRender);
	}

	public void SpawnAgent(int x, int y, Agent a){
		GameObject newAgentObj = GameObject.Instantiate (agentPrefab);
		AgentRenderer agentRender = newAgentObj.GetComponent<AgentRenderer> ();
		agentRender.transform.position = new Vector3 (x, y, -1);
		agentRender.SetMyAgent (a);
		agentObjects.Add(agentRender);
	}

	public void UpdateMapRender(Map m){
		//Update every tile TODO; probably only update changed tiles
		foreach (KeyValuePair<Vector2, TileRenderer> kvp in tileObjects) {
			kvp.Value.UpdateValues((float)m.tiles[kvp.Key].water/((float)Map.STARTING_ATTRIBUTE_VALUE), (float)m.tiles[kvp.Key].nutrients/ ((float)Map.STARTING_ATTRIBUTE_VALUE));

		}
	
		List<AgentRenderer> theDead = new List<AgentRenderer> ();
		foreach (AgentRenderer agentRender in agentObjects) {
			agentRender.UpdateAgent();

			if(agentRender.myAgent==null){
				theDead.Add(agentRender);
			}
		}

		foreach (AgentRenderer dead in theDead) {
			agentObjects.Remove(dead);
			Destroy(dead.gameObject);
		}
	}
}
