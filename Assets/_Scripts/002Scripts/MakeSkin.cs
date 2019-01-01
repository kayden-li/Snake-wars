using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSkin : MonoBehaviour {
	public Sprite[] _Skin;
	public GameObject _Skin1;
	void Awake(){
		int CountSprite = _Skin.Length;
		for (int i = 0; i < CountSprite; ++i) {
			GameObject temp = Instantiate (_Skin1) as GameObject;
			temp.transform.parent = transform;
			temp.transform.localScale = Vector3.one;
			temp.transform.GetComponent<UISprite> ().spriteName = _Skin [i].name;
		}
	}
}
