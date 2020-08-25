using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject accTextObj;
    [SerializeField]
    float gameTimeSec_;
    StringBuilder stringBuilder_;

    private int totalNotes = 0;
    private float hittenNotes = 0f;
    private float totalAccuracy = 0f;

    private float startTime_;
    public float progress_;

    void Start()
    {
        stringBuilder_ = new StringBuilder("0.00%", 10);
        startTime_ = Time.time;
    }

    void Update()
    {
        var currentTime = Time.time;
        progress_ = (currentTime - startTime_) / gameTimeSec_;
    }

    public void CalcCurrentAccuracy(float acc)
    {
        totalNotes++;

        if (acc >= 0.95f)
        {
            //Perfect
            hittenNotes += 1f;
        }
        else if (acc >= 0.9f)
        {
            //Great
            hittenNotes += 0.8f;
        }
        else if (acc >= 0.85f)
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
