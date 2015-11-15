using UnityEngine;
using System.Collections;

public class EatNutrients : AbstractState {
	private int eatRate;

	public EatNutrients(int _eatRate){
		eatRate = _eatRate;
		attributesRequired = new string[]{Map.Hunger};

	}

	public override bool Precondition (Agent a){
		//The tile this agent is on has nutrients and the agent is less than max hunger
		return a.currentTile.nutrients >= eatRate && a.attributes[Map.Hunger]<Map.STARTING_ATTRIBUTE_VALUE;
	}

	public override void Effect (Agent a){
		//Get rid of eatRate nutrients
		a.currentTile.nutrients -= eatRate;
		//Add eat rate nutrients to agent's hunger
		a.attributes [Map.Hunger] += eatRate;
	}
}
