using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveRandom : AbstractState {
	
	public MoveRandom(){
	}
	
	public override bool Precondition (Agent a){
		foreach(Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
			if (t.Unoccupied) { 
				return true;
			}
		}
		return false;
	}
	
	public override void Effect (Agent a){
		List<Tile> slots = new List<Tile> ();
		
		foreach (Tile t in Map.Instance.GetAdjacentTiles(a.currentTile)) {
			if (t.Unoccupied) {
				slots.Add(t);
			}
		}
		int idx = Random.Range(0, slots.Count); // pick random prey to attack
		a.currentTile.occupiedAgent = null;
		a.currentTile = slots [idx];
		slots [idx].occupiedAgent = a;
	}
}
