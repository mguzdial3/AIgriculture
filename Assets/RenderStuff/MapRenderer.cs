using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : MonoBehaviour {
	public GameObject tilePrefab;

	public Dictionary<Vector2, TileRenderer> tileObjects;


	public void Init(){
		tileObjects = new Dictionary<Vector2, TileRenderer> ();
	}

	public void SpawnTile(int x, int y, int water, int nutrients){
		GameObject newTileObj = GameObject.Instantiate (tilePrefab);
		TileRenderer tileRender = newTileObj.GetComponent<TileRenderer> ();
		newTileObj.transform.position = new Vector3 (x, y, 0);
		tileRender.UpdateValues (((float)water) / ((float)Map.STARTING_ATTRIBUTE_VALUE), ((float)nutrients) / ((float)Map.STARTING_ATTRIBUTE_VALUE));
		tileObjects.Add (new Vector2 (x, y), tileRender);
	}
}
