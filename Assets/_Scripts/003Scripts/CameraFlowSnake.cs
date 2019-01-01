using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlowSnake : MonoBehaviour {
    public Transform snakeHead;
    private void LateUpdate()
    {
        if (!Data.isGameOver)
        {
            Vector3 temp = snakeHead.position;
            transform.position = Vector3.Lerp(transform.position, temp, 1f);
        }
    }
}
