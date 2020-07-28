using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionHUD : MonoBehaviour
{

    [SerializeField]
    GameObject[] images_ = default;
    float[] timeDeactive;

    public float redCautionTime_ = 0.5f; //`残り何秒の時点で赤い警告にするか
    public float yelloCautionTime_ = 1f;　//残り何秒で赤い警告を出すか
    public const float showSeconds = 2f; //警告は何秒間表示するか

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
            if(remainTime <= redCautionTime_)
            {
                images_[i].GetComponent<Image>().color = Color.red;
            }
            else if(remainTime <= yelloCautionTime_)
            {
                images_[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                images_[i].GetComponent<Image>().color = Color.green;
            }
        }
    }

    //右側の攻撃を通知
    public void NotifyRightShot()
    {
        LeftCaution();
    }


    //左側の攻撃を通知
    public void NotifyLeftShot()
    {
        RightCaution();
    }


    //中心の攻撃を通知
    public void NotifyCenterShot()
    {
        images_[(int)eImage.center].SetActive(true);
        timeDeactive[(int)eImage.center] = Time.time + showSeconds;
    }


    //上側の攻撃を通知
    public void NotifyAboveShot()
    {
        images_[(int)eImage.above].SetActive(true);
        timeDeactive[(int)eImage.above] = Time.time + showSeconds;
    }


    void LeftCaution()
    {
        images_[(int)eImage.left].SetActive(true);
        timeDeactive[(int)eImage.left] = Time.time + showSeconds;
    }


    void RightCaution()
    {
        images_[(int)eImage.right].SetActive(true);
        timeDeactive[(int)eImage.right] = Time.time + showSeconds;
    }
}
