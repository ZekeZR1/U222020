using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Enemy Ref
    [SerializeField]
    GameObject enemyObj_ = default;
    private Enemy enemy_;

    //HUD Ref
    [SerializeField]
    GameObject accTextObj = default;
    [SerializeField]
    GameObject resultObj = default;

    //Game Progress
    [SerializeField]
    float gameTimeSec_;
    public float progress_;
    private float startTime_;
    private float elpasedSec_ = 0f;

    //Game Score
    private StringBuilder stringBuilder_;
    private int score;
    private int totalNotes = 0;
    private float hittenNotes = 0f;
    public float totalAccuracy = 0f;
    public int combo_;

    void Start()
    {
        enemy_ = enemyObj_.GetComponent<Enemy>();
        stringBuilder_ = new StringBuilder("0.00%", 10);
        startTime_ = Time.time;
        combo_ = 0;
        score = 0;
    }

    void Update()
    {
        var currentTime = Time.time;
        elpasedSec_ = currentTime - startTime_;
        progress_ = (elpasedSec_) / gameTimeSec_;

        Debug.Log(progress_);
        if (elpasedSec_ >= 1f)
        {
            enemy_.StartShooting();
        }
        if (progress_ >= 1f)
        {
            enemy_.StopShooting();
            resultObj.SetActive(true);
        }

    }

    public void CalcCurrentAccuracy(float acc)
    {
        totalNotes++;
        const float perfectAcc = 0.95f;
        const float greatAcc = 0.9f;
        const float goodAcc = 0.85f;

        if(acc >= goodAcc)
        {
            combo_++;
        }
        else
        {
            combo_ = 0;
        }

        if (acc >= perfectAcc)
        {
            //Perfect
            hittenNotes += 1f;
        }
        else if (acc >= greatAcc)
        {
            //Great
            hittenNotes += 0.8f;
        }
        else if (acc >= goodAcc)
        {
            //Good
            hittenNotes += 0.5f;
        }
        else
        {
            //Miss
            hittenNotes += 0f;
        }

        totalAccuracy = (hittenNotes / totalNotes) * 100f;
        stringBuilder_.Clear();
        stringBuilder_.Append(totalAccuracy.ToString("F2"));
        stringBuilder_.Append("%");
        accTextObj.GetComponent<Text>().text = stringBuilder_.ToString() ;
    }

}
