using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private UILabel snakeLength;
    private UILabel snakeKill;
    public GameObject overSnakeLength;
    public GameObject killNum;
    public GameObject time;
    private void Awake()
    {
        snakeLength = transform.Find("Length").GetComponent<UILabel>();
        snakeKill = transform.Find("Kill").GetComponent<UILabel>();
    }
    private void LateUpdate()
    {
        snakeLength.text = "长度:" + Data.MySnakeLength.ToString();
        overSnakeLength.GetComponent<UILabel>().text = "您的最大长度:" + Data.MySnakeLength.ToString();
        snakeKill.text = "击杀个数:" + Data.MySnakeKill;
        killNum.GetComponent<UILabel>().text = "您的击杀个数:" + Data.MySnakeKill.ToString();
        if (Data.isTime)
        {
            if (Time.timeSinceLevelLoad > 150)
                time.GetComponent<UILabel>().color = Color.red;
            time.SetActive(true);
            time.GetComponent<UILabel>().text = "剩余时间:" + (180 - (int)Time.timeSinceLevelLoad) + "S";
        }
        else time.SetActive(false);
    }
}
