using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{   
    [SerializeField]
    float fillVal = 1.0f;
    [SerializeField]
    GameObject gameManagerObj_ = default;
    GameManager gmg_;
    Image image_;
    void Start()
    {
        gmg_ = gameManagerObj_.GetComponent<GameManager>();
        image_ = this.GetComponent<Image>();
    }

    void Update()
    {
        image_.fillAmount = gmg_.progress_;
    }
}
