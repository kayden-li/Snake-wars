using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] snakeHead;
    public GameObject[] snakeBody;
    public GameObject over;
    public GameObject _foods;
    public GameObject snakeEmpty;
    public string[] AISkin = { "小蓝", "中国", "小绿", "大红", "紫红", "小黄" };
    public GameObject[] snakeAI;
    private void Update()
    {
        if (Data.isGameOver)
            over.SetActive(true);
        if (Data.isTime)
            if (Time.timeSinceLevelLoad > 180)
                Data.isGameOver = true;
    }
    /// <summary>
    /// 创建蛇
    /// </summary>
    public void CreateSnake(string str, GameObject _snakeHead, GameObject _body)
    {
        //创建蛇头
        int i = CheckSkin(str);
        GameObject head = Instantiate(snakeHead[i]) as GameObject;
        head.transform.parent = _snakeHead.transform;
        head.transform.localPosition = Vector3.zero;
        head.transform.localScale = Vector3.one;
        head.transform.GetComponent<UISprite>().depth = 5;
        //创建蛇的身体
        GameObject body;
        for (int j = 0; j < Data.snakeLength; ++j)
        {
            if (j % 2 == 0)
                body = Instantiate(snakeEmpty) as GameObject;
            else
                body = Instantiate(snakeBody[i]) as GameObject;
            body.transform.parent = _body.transform;
            body.transform.position = new Vector3(
                head.transform.position.x,
                head.transform.position.y,
                0);
            body.transform.localScale = Vector3.one;
        }
    }
    public int CheckSkin(string str)
    {
        switch (str)
        {
            case "小蓝":
                return 0;
            case "中国":
                return 1;
            case "小绿":
                return 2;
            case "大红":
                return 3;
            case "小黄":
                return 4;
            case "紫红":
                return 5;
            default:
                return 1;
        }
    }
    public void AddSnakeBody(int nPly, GameObject _body)
    {
        GameObject temp;
        if(_body.transform.childCount % 2 == 0)
            temp = Instantiate(snakeEmpty) as GameObject;
        else
            temp = Instantiate(snakeBody[nPly]) as GameObject;
        temp.transform.localScale = Vector3.one;
        temp.transform.parent = _body.transform;
        temp.transform.localScale = new Vector3(1, 1, 0);
        if(_body.name.Substring(0,1) == "A")
        {
            int _temp = int.Parse(_body.name.Substring(_body.name.Length - 1, 1));
            snakeAI[_temp - 1].GetComponent<AISnakeController>().AISnakeLength++;
        }else Data.MySnakeLength++;
    }
    //游戏开始，初始化数据
    public void EmptyData()
    {
        Data.isGameOver = false;
        Data.MySnakeLength = 0;
        Data.isSpeed = false;
    }
    /// <summary>
    /// 蛇死亡之后身体变成食物
    /// </summary>
    public void DeleteSnakeBody(Transform head)
    {
        GameObject body = null;
        if (head.parent.transform.GetComponent<SnakeController>())
        {
            Data.isGameOver = true;
            body = head.parent.transform.GetComponent<SnakeController>().body;
        }
        else if (head.parent.transform.GetComponent<AISnakeController>())
        {
            body = head.parent.transform.GetComponent<AISnakeController>().body;
            head.parent.GetComponent<AISnakeController>().AddSnake();
        }
        //但是使用此方法后会发现蛇的身体出现明显空隙
        while(body.transform.childCount > 0)
        {
            //删除空身体
            //public const string _emptyBody = "SnakeEmpty";
            if (body.transform.GetChild(0).name == Data._emptyBody)
                Destroy(body.transform.GetChild(0).gameObject);
            else
            {
                body.transform.GetChild(0).tag = Data._food;
                body.transform.GetChild(0).parent = _foods.transform;
            }

        }
        Destroy(head.gameObject);
    }
}
