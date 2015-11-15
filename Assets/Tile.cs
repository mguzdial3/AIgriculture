using UnityEngine;
using System.Collections;

public class Tile {
	public int x, y;
	public Agent occupiedAgent = null;
	public bool Unoccupied{get{return occupiedAgent==null;}}

	public int water, nutrients;

	public Tile(int _x, int _y, int _water, int _nutrients){
		this.x = _x;
		this.y = _y;
		this.water = _water;
		this.nutrients = _nutrients;
	}
	
}
