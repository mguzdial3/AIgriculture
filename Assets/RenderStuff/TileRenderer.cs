using UnityEngine;
using System.Collections;

public class TileRenderer : MonoBehaviour {
	float waterVal = 1.0f;
	float nutrientsVal = 1.0f;
	Renderer mine;


	public void InitialValueSet(float _waterVal, float _nutrientsVal){
		mine = gameObject.GetComponent<Renderer>();
		UpdateValues( _waterVal,  _nutrientsVal);
	}

	public void UpdateValues(float _waterVal, float _nutrientsVal){
		waterVal = _waterVal;
		nutrientsVal = _nutrientsVal;
		RecolorTile ();
	}

	public void RecolorTile(){
		mine.material.color = Color.Lerp (Color.Lerp (new Color32 (133, 87, 35, 255), new Color32 (78, 97, 114, 255), waterVal), new Color32 (102, 141, 60, 255), nutrientsVal);
	}
}
