using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private AISnakeController instance;
    private void Awake()
    {
        instance = transform.parent.GetComponent<AISnakeController>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (gameObject.tag != other.transform.parent.tag && other.tag != Data._food)
            instance.ChangeDirection();
    }
}
