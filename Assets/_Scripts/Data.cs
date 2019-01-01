using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static string _Skin = "中国";  //玩家选择的皮肤
    public const int snakeLength = 5;  //蛇的默认长度
    public const int snakeKill = 0; //蛇的击杀个数
    public static bool isGameOver = false;  //游戏是否结束
    //tags
    public const string _head = "SnakeHead";
    public const string _obs = "Obs";
    public const string _food = "Food";
    public const string _emptyBody = "SnakeEmpty";
    public const string _body = "Body";

    public static int MySnakeLength;//玩家的长度
    public static int MySnakeKill;//玩家击杀个数
    public static bool isSpeed;
    public static bool isTime = false;

    public static string userName = "";
}

