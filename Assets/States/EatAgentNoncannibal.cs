using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAgentNoncannibal : AbstractState {
	
	private int eatRate;
	
	public EatAgentNoncannibal(int _eatRate){
		eatRate = _eatRate;
		attributesRequired = new string[]{Map.Hunger};		
	}
	
	public override bool Precondition (Agent a){
		if (a.attributes [Map.Hunger] > Map.STARTING_ATTRIBUTE_VALUE) {
			return false;
		}
		
		foreach(Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
			if (!t.Unoccupied && t.occupiedAgent.id != a.id) { 
				return true;
			}
		}
		return false;
	}
	
	public override void Effect (Agent a){
		List<Agent> prey = new List<Agent> ();
		
		foreach (Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
			if (!t.Unoccupied && t.occupiedAgent.id != a.id) {
				prey.Add(t.occupiedAgent);
			}
		}
		int idx = Random.Range(0, prey.Count); // pick random prey to attack
		a.attributes [Map.Hunger] += eatRate;
		prey [idx].attributes [Map.Lifespan] -= eatRate;
	}
}
