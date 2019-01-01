using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {
    public GameManager instance;
    public EasyJoystick joyStick;
    public GameObject body;
    public float speed; 

    private Vector3 MovePoint;
    private void Awake()
    {
        MovePoint = new Vector3(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            0);
        instance = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        instance.EmptyData();
        //蛇的击杀个数
        Data.MySnakeKill = Data.snakeKill;
        //蛇的长度
        Data.MySnakeLength = Data.snakeLength;
        //创建蛇
        instance.CreateSnake(Data._Skin, gameObject, body);
    }
    private void Update()
    {
        if (Data.isGameOver)
            return;
        if (Data.isSpeed)
        {
            for (int i = 0; i < 2; ++i)
            {
                Vector3 temp = joyStick.JoystickAxis;    
                if (temp != Vector3.zero)
                    MovePoint = temp;
                BodyFlowHead(transform.position);
                transform.position += MovePoint.normalized * speed * Time.deltaTime;
                Vector3 v = GetRotation(MovePoint);
                if (v != Vector3.zero)
                    transform.localEulerAngles = new Vector3(0, 0, v.z);
                else transform.localEulerAngles = MovePoint;
            }
        }
        else
        {
            Vector3 temp = joyStick.JoystickAxis;
            if (temp != Vector3.zero)
                MovePoint = temp;
            BodyFlowHead(transform.position);
            transform.position += MovePoint.normalized * speed  * Time.deltaTime;
            Vector3 v = GetRotation(MovePoint);
            if (v != Vector3.zero)
                transform.localEulerAngles = new Vector3(0, 0, v.z);
            else transform.localEulerAngles = MovePoint;
        }
    }
    private Vector3 GetRotation(Vector3 temp)
    {
        float angle;
        Vector3 v = Vector3.zero;
        angle = 180 * Mathf.Acos(temp.x / Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y))/Mathf.PI;
        if(Mathf.Acos(temp.x / Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y)) != 0)
        {
            if (temp.y > 0)
                v = new Vector3(0, 0, angle + 90);
            else v = new Vector3(0, 0, -angle + 90);
        }
        return v;
    }
    void BodyFlowHead(Vector3 point)
    {
        int bodyCount = body.transform.childCount;
        Vector3 bodyPrePoint = Vector3.zero,
                bodyNowPoint = Vector3.zero;
        for(int i = 0; i < bodyCount; ++i)
        {
            GameObject temp = body.transform.GetChild(i).gameObject;
                
            if (i == 0)
            {
                bodyPrePoint = temp.transform.position;
                temp.transform.position = point;
            }
            else
            {
                bodyNowPoint = temp.transform.position;
                temp.transform.position = bodyPrePoint;
                bodyPrePoint = bodyNowPoint;
            }
            
        }
    }
}
