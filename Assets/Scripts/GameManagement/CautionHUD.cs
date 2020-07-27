using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionHUD : MonoBehaviour
{

    [SerializeField]
    GameObject[] images_ = default;
    float[] timeDeactive;

    private const float showSeconds = 2f;

    private enum eImage
    {
        left,
        right,
        above,
        center
    };

    void Start()
    {
        timeDeactive = new float[images_.Length];
        timeDeactive.SetValue(float.MaxValue, 0);
        foreach( var i in images_)
        {
            i.SetActive(false);
        }
    }

    void Update()
    {
        var time = Time.time;

        for (int i = 0; i < images_.Length; i++)
        {
            if (timeDeactive[i] <= time)
            {
                images_[i].SetActive(false);
            }

            var remainTime = timeDeactive[i] - time;
            if(remainTime <= 0.5)
            {
                images_[i].GetComponent<Image>().color = Color.red;
            }
            else if(remainTime <= 1.0f)
            {
                images_[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                images_[i].GetComponent<Image>().color = Color.green;
            }
        }
    }

    public void LeftCaution()
    {
        images_[(int)eImage.left].SetActive(true);
        timeDeactive[(int)eImage.left] = Time.time + showSeconds;
    }


    public void RightCaution()
    {
        images_[(int)eImage.right].SetActive(true);
        timeDeactive[(int)eImage.right] = Time.time + showSeconds;
    }


    public void AboveCaution()
    {
        images_[(int)eImage.above].SetActive(true);
        timeDeactive[(int)eImage.above] = Time.time + showSeconds;
    }


    public void CenterCaution()
    {
        images_[(int)eImage.center].SetActive(true);
        timeDeactive[(int)eImage.center] = Time.time + showSeconds;
    }

}
