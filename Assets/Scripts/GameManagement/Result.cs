using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    [SerializeField]
    GameObject gameManager = default;
    [SerializeField]
    GameObject resultTextDataObj = default;

    private StringBuilder score_stb;

    void Start()
    {
        var gm = gameManager.GetComponent<GameManager>();
        score_stb = new StringBuilder("精度 : ",30);
        score_stb.Append(gm.totalAccuracy.ToString("F2"));
        score_stb.Append("%\nScore : ");
        score_stb.Append(gm.score.ToString());
        resultTextDataObj.GetComponent<Text>().text = score_stb.ToString();
    }

    public void moveToTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }
}
