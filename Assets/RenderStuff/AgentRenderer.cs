using UnityEngine;
using System.Collections;

public class AgentRenderer : MonoBehaviour {
	Renderer mine;
	public Agent myAgent;


	public void SetMyAgent(Agent _myAgent){
		myAgent = _myAgent;
		mine = gameObject.GetComponent<Renderer> ();


	}


	//Update color and position
	public void UpdateAgent(){
		if (myAgent.IsDead ()) {
			myAgent = null;
		} else {
			transform.position = new Vector3 (myAgent.currentTile.x, myAgent.currentTile.y, -1);
			//Set the current color
			mine.material.color = Color.Lerp (MapRenderer.animalColors [myAgent.id], Color.black, 1.0f - (((float)myAgent.attributes [Map.Lifespan]) / ((float)myAgent.MAX_LIFESPAN)));
		}
	}
}
