using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeName : MonoBehaviour {
    public UILabel name;
	// Use this for initialization
	void Start () {
        name.text = Data.userName;
	}
}
