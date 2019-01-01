using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class LR : MonoBehaviour {

    public GameObject _start;
    public GameObject _lr;
    public GameObject button_L;
    public GameObject button_R;

    public UIInput _id;
    public UIInput _password;

    private DbAccess db;
	// Use this for initialization
	void Start () {
        //如果运行在编辑器中
#if UNITY_EDITOR
        //通过路径找到第三方数据库
        string appDBPath = Application.dataPath + "/Plugins/Android/assets/" + "snake.db";

        db = new DbAccess("URI=file:" + appDBPath);

        //如果运行在Android设备中
#elif UNITY_ANDROID
        //将第三方数据库拷贝至Android可找到的地方
        string appDBPath = Application.persistentDataPath + "/" + "snake.db";

        //如果已知路径没有地方放数据库，那么我们从Unity中拷贝
        if (!File.Exists(appDBPath))
        {
            //用www先从Unity中下载到数据库
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "snake.db");

            while (!loadDB.isDone) { }

            //拷贝至规定的地方
            File.WriteAllBytes(appDBPath, loadDB.bytes);
        }

        //在这里重新得到db对象。
        db = new DbAccess("URI=file:" + appDBPath);

#endif
    }

    private void DisplayStart()
    {
        _start.SetActive(true);
        _lr.SetActive(false);
    }
    public void r_Back()
    {
        button_L.SetActive(true);
        button_R.SetActive(false);
    }
    public void r_Go()
    {
        button_L.SetActive(false);
        button_R.SetActive(true);
    }

    public void Login()
    {
        string id = _id.value;
        string password = _password.value;

        SqliteDataReader reader = db.SelectWhere(
            "User",
            new string[] { "password" },
            new string[] { "id" },
            new string[] { "=" },
            new string[] { id }
        );
        while (reader.Read())
        {
            if(password.Equals(
                reader.GetString(reader.GetOrdinal("password"))))
            {
                Data.userName = id;
                DisplayStart();
            }
        }
    }
    public void Regiest()
    {
        string id = _id.value;
        string password = _password.value;
        try
        {
            SqliteDataReader reader = db.InsertIntoSpecific(
                "User",
                new string[] { "id", "password" },
                new string[] { id, password });
        }
        catch
        {
            Debug.Log("注册失败");
            return;
        }
        DisplayStart();
    }
    public void Tour()
    {
        Data.userName = GetRandomString(10, true, true, true, false, "");
        DisplayStart();
    }

    ///<summary>
    ///生成随机字符串 
    ///</summary>
    ///<param name="length">目标字符串的长度</param>
    ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
    ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
    ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
    ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
    ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
    ///<returns>指定长度的随机字符串</returns>
    private static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
    {
        byte[] b = new byte[4];
        new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
        System.Random r = new System.Random(BitConverter.ToInt32(b, 0));
        string s = null, str = custom;
        if (useNum == true) { str += "0123456789"; }
        if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
        if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
        for (int i = 0; i < length; i++)
        {
            s += str.Substring(r.Next(0, str.Length - 1), 1);
        }
        return s;
    }
}
