using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    private Vector3 StartPoint, EndPoint;
    public GameObject[] _Foods;
    public int FoodCount;
    private void Awake()
    {
        StartPoint = new Vector3((float)-2968, (float)-1660, (float)0);
        EndPoint = new Vector3((float)2965, (float)1661, (float)0);
    }
    private void Start()
    {
        CreateFood(FoodCount);
    }
    public void CreateFood(int food)
    {
        for(int i = 0; i < food; ++i)
        {
            GameObject temp = Instantiate(_Foods[Random.Range(0, _Foods.Length)]) as GameObject;
            Vector3 point = new Vector3(
                Random.Range(StartPoint.x, EndPoint.x),
                Random.Range(StartPoint.y, EndPoint.y),
                0);
            temp.transform.parent = transform;
            temp.transform.localPosition = point;
            temp.transform.localScale = new Vector3(1, 1, 0);
        }
    }
    private float _time = 0;
    private void Update()
    {
        //_time++;
        //if(_time > 300)
        //{
        //    CreateFood(1);
        //    _time = 0;
        //}
        if(transform.childCount < 200)
        {
            CreateFood(50);
        }
    }
}
