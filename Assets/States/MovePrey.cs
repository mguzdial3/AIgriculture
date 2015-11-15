using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePrey : AbstractState {
	
	public MovePrey(){
	}
	
	public override bool Precondition (Agent a){
		
		foreach(Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
			if (!t.Unoccupied) { 
				return true;
			}
		}
		return false;
	}
	
	public override void Effect (Agent a){
		// move toward target

//		Agent prey = nearestTarget(a, Map.Instance.agents);
//		Vector2 dist = prey.currentTile.pos - a.currentTile.pos;
//		if (dist.x < dist.y) {
//			if (dist.x < 0) {
//
//			}
//		} else {
//
//		}
//
		// TODO: update me! NOT FINISHED
//		int idx = Random.Range(0, prey.Count); // pick random prey to attack
//		a.currentTile.occupiedAgent = null;
//		a.currentTile = prey [idx];
//		slots [idx].occupiedAgent = a;
	}

	public Agent nearestTarget(Agent a, List<Agent> targets) {
		Vector2 xy = new Vector2 (a.currentTile.x, a.currentTile.y);
		float minDist = Mathf.Infinity;

		Agent minAgent = null;
		foreach (Agent t in targets) {
			float dist = (xy - new Vector2(t.currentTile.x, t.currentTile.y)).sqrMagnitude;
			if (dist < minDist) {
				minDist = dist;
				minAgent = t;
			}
		}

		return minAgent;
	}
}
