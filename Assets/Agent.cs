using System.Collections.Generic;

public class Agent {
	public Dictionary<string, int> attributes;
	public AbstractState[] states;
	public int id;

	public int MAX_LIFESPAN; 
	public Tile currentTile;

	public Agent(int _id, int lifespan, AbstractState[] _states, Tile _currentTile){
		id = _id;
		states = _states;


		//Attributes Update
		attributes = new Dictionary<string, int> ();
		attributes.Add (Map.Lifespan, lifespan);

		foreach (AbstractState state in states) {
			foreach(string requirement in state.AttributesRequired){
				attributes[requirement] = Map.STARTING_ATTRIBUTE_VALUE;
			}
		}

		MAX_LIFESPAN = lifespan;
		currentTile = _currentTile;
	}

	public bool Update(){
		for (int i = 0; i<states.Length; i++) {
			if(states[i].Precondition(this)){
				states[i].Effect(this);
				return true;
			}
		}
		return false;
	}


	public bool CheckForDeath(){
		attributes ["Lifespan"] -= 1;

		foreach (KeyValuePair<string, int> kvp in attributes) {
			if (kvp.Value<=0){
				return true;
			}
		}
		return false;
	}

}
