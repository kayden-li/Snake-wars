using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour {

    public void Walk()
    {
        Data.isSpeed = false;
    }
    public void Run()
    {
        Data.isSpeed = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Run();
        if (Input.GetMouseButtonUp(1))
            Walk();
    }
}
