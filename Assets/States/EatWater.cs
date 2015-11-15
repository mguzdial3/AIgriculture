using System.Collections;

public class EatWater : AbstractState {
	private int drinkRate;
	
	public EatWater(int _drinkRate){
		drinkRate = _drinkRate;
		attributesRequired = new string[]{Map.Thirst};
		
	}
	
	public override bool Precondition (Agent a){
		//The tile this agent is on has nutrients and the agent is less than max hunger
		return a.currentTile.water >= drinkRate && a.attributes[Map.Thirst]<Map.STARTING_ATTRIBUTE_VALUE;
	}
	
	public override void Effect (Agent a){
		//Get rid of eatRate nutrients
		a.currentTile.water -= drinkRate;
		//Add eat rate nutrients to agent's hunger
		a.attributes [Map.Thirst] += drinkRate;
	}
}
