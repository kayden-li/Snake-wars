using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnakeController : MonoBehaviour {
    public float speed = 0.5f;
    public Vector3 direction;
    public GameObject body;
    private GameManager instance;
    public int AISnakeLength;
    private string AISkin;
    public bool isDie = false;//是否死亡
    private void Awake()
    {
        AISnakeLength = Data.snakeLength;
        direction = new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            0);
        instance = GameObject.Find("GameManager").GetComponent<GameManager>();
        AISkin = instance.AISkin[UnityEngine.Random.Range(0, instance.AISkin.Length)];
    }
    private void Start()
    {
        instance.CreateSnake(AISkin, gameObject, body);
    }
    private void Update()
    {
        if (isDie)
            return;
        //Vector3 temp = direction;
        Vector3 v = GetRotation(direction);
        //if (temp != Vector3.zero)
        //    MovePoint = temp;
        BodyFlowHead(transform.position);
        transform.position += direction.normalized * speed * Time.deltaTime * 1.9f;
        if (v != Vector3.zero)
            transform.localEulerAngles = v;
    }
    /// <summary>
    /// 得到一个旋转角度
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    private Vector3 GetRotation(Vector3 temp)
    {
        float angle;
        Vector3 v = Vector3.zero;
        angle = 180 * Mathf.Acos(temp.x / Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y)) / Mathf.PI;
        if (Mathf.Acos(temp.x / Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y)) != 0)
        {
            if (temp.y > 0)
                v = new Vector3(0, 0, angle + 90);
            else v = new Vector3(0, 0, -angle + 90);
        }
        return v;
    }
    /// <summary>
    /// 坐标跟随
    /// </summary>
    /// <param name="point"></param>
    void BodyFlowHead(Vector3 point)
    {
        int bodyCount = body.transform.childCount;
        Vector3 bodyStartPoint = Vector3.zero,
            bodyEndPoint = Vector3.zero;
        for (int i = 0; i < bodyCount; ++i)
        {
            GameObject temp = body.transform.GetChild(i).gameObject;
            if (i % 2 == 0)
            {
                bodyStartPoint = temp.transform.position;
                if (i == 0)
                    temp.transform.position = point;
                else
                    temp.transform.position = bodyEndPoint;
            }
            else
            {
                bodyEndPoint = temp.transform.position;
                temp.transform.position = bodyStartPoint;
            }
        }
    }
    /// <summary>
    /// 当蛇前方有障碍物时，改变方向
    /// </summary>
    public void ChangeDirection()
    {
        //得到新的方向向量
        GetNewPositon();

    }
    void GetNewPositon()
    {
        direction = new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            0);
    }
    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
/// <summary>
/// AI蛇死亡后，添加新蛇
/// </summary>
public void AddSnake()
    {
        transform.localPosition = new Vector3(
            UnityEngine.Random.Range(-2900, 2900),
            UnityEngine.Random.Range(-1600, 1600),
            0);
        AISnakeLength = Data.snakeLength;
        direction = new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            0);
        AISkin = instance.AISkin[UnityEngine.Random.Range(0, instance.AISkin.Length)];
        instance.CreateSnake(AISkin, gameObject, body);
        isDie = false;
    }
}
