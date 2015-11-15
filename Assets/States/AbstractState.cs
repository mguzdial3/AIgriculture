public class AbstractState {
	protected string[] attributesRequired;
	public string[] AttributesRequired{get {return attributesRequired;}}

	//Ensure that this action can be taken on this tile
	public virtual bool Precondition(Agent a){return false;}

	//Take the action associated with this tile
	public virtual void Effect(Agent a){}
}
