using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject accTextObj;

    private int totalNotes = 0;
    private float hittenNotes = 0f;
    private float totalAccuracy = 0f;

    void Start()
    {
            
    }

    void Update()
    {
        
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
        accTextObj.GetComponent<Text>().text = totalAccuracy.ToString();
    }

}
