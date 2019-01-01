using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour {
	public void LoadScene002(){
		SceneManager.LoadScene ("002");
	}
	public void LoadScene003(){
        Data.isTime = false;
		SceneManager.LoadScene ("003");
	}
    public void LoadScene003_Time()
    {
        Data.isTime = true;
        SceneManager.LoadScene("003");
    }
    public void LoadScene003_Again()
    {
        SceneManager.LoadScene("003");
    }
    public void ExiyGame()
    {
        Application.Quit();
    }
}
