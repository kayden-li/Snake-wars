using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour {
	public GameObject[] _helpMe;
	public GameObject Back, Skin;
	private int index = 0;
	public void LeftClick(){
		if(index>0)
			index--;
		HidePicture ();
	}
	public void RightClick(){
		if(index<_helpMe.Length)
			index++;
		HidePicture ();
	}
	void HidePicture(){
		switch (index) {
		case 0:
			_helpMe [0].SetActive (true);
			_helpMe [1].SetActive (false);
			_helpMe [2].SetActive (false);
			break;
		case 1:
			_helpMe [0].SetActive (false);
			_helpMe [1].SetActive (true);
			_helpMe [2].SetActive (false);
			break;
		case 2:
			_helpMe [0].SetActive (false);
			_helpMe [1].SetActive (false);
			_helpMe [2].SetActive (true);
			break;
		default:
			break;
		}
	}
	public void ClickRule(){
		//Back.SetActive (false);
		gameObject.SetActive (true);
	}
	public void ClickBack(){
		Back.SetActive (true);
		gameObject.SetActive (false);
	}
	//从皮肤返回主界面
	public void SkinBackMain(){
		Skin.SetActive (false);
		Back.SetActive (true);
	}
	//主界面到皮肤界面
	public void MainToSkin(){
		Skin.SetActive (true);
		Back.SetActive (false);
	}
	//退出游xi
	public void ExitGame(){
		Application.Quit ();
	}
}
