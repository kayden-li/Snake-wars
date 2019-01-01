using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSkin : MonoBehaviour {
	void OnClick(){
		string str = GetComponent<UISprite> ().spriteName;
		GameObject.Find ("Skin/Label/Label").GetComponent<UILabel> ().text = str;
        Data._Skin = str;
	}
}
