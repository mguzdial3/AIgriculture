using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePrey : AbstractState {
	
	public MovePrey(){
	}
	
	public override bool Precondition (Agent a){
		Agent prey = nearestTarget(a, Map.Instance.agents);
		if (prey != null) {
			foreach (Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
				if (t.Unoccupied) { 
					return true;
				}
			}
		}
		return false;
	}
	
	public override void Effect (Agent a){
		// move toward target

		Agent prey = nearestTarget(a, Map.Instance.agents);
		Vector2 ppos = prey.currentTile.pos;
		Tile[] openTiles = Map.Instance.GetAdjacentTilesOpen (a.currentTile);

		float minDist = Mathf.Infinity;
		Tile minTile = null;
		foreach (Tile t in openTiles) {
			float dist = (ppos - t.pos).sqrMagnitude;
			if (dist < minDist) {
				minDist = dist;
				minTile = t;
			}
		}

		a.currentTile.occupiedAgent = null;
		a.currentTile = minTile;
		minTile.occupiedAgent = a;
	}

	public Agent nearestTarget(Agent a, List<Agent> targets) {
		Vector2 xy = new Vector2 (a.currentTile.x, a.currentTile.y);
		float minDist = Mathf.Infinity;

		Agent minAgent = null;
		foreach (Agent t in targets) {
			if (t!=a){
				float dist = (xy - t.currentTile.pos).sqrMagnitude;
				if (dist < minDist) {
					minDist = dist;
					minAgent = t;
				}
			}
		}

		return minAgent;
	}
}
