using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour {
    public int index;
    public GameManager instance;
    private float _time = 2f;
    private void Awake()
    {
        instance = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        _time -= 0.1f;
    }
    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Data._obs:
                if (transform.GetComponent<SnakeController>())
                    Data.isGameOver = true;
                else if (transform.parent.GetComponent<AISnakeController>())
                    transform.parent.GetComponent<AISnakeController>().isDie = true;
                instance.DeleteSnakeBody(transform);
                break;
            case Data._food:
                GameObject temp = null;
                if (transform.parent.gameObject.GetComponent<SnakeController>())
                    temp = transform.parent.GetComponent<SnakeController>().body;
                else if(transform.parent.GetComponent<AISnakeController>())
                    temp = transform.parent.GetComponent<AISnakeController>().body;
                Destroy(other.gameObject);
                instance.AddSnakeBody(index, temp);
                break;
            default:
                if (other.transform.GetComponent<AI>() ||
                    other.transform.parent.GetComponent<UIController>() ||
                    other.name == "Over"||
                    other.transform.parent.name == "Over" )
                    break;
                if(transform.parent.tag != other.transform.parent.tag && _time < 0)
                {
                    if (transform.GetComponent<SnakeController>())
                        Data.isGameOver = true;
                    else if (transform.parent.GetComponent<AISnakeController>())
                    {
                        transform.parent.GetComponent<AISnakeController>().isDie = true;
                        if (other.GetComponent<SnakeController>() ||
                            other.transform.parent.name == Data._body)
                            Data.MySnakeKill++;
                    }
                    instance.DeleteSnakeBody(transform);
                }
                break;
        }
    }
}
