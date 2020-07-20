using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{   
    [SerializeField]
    float fillVal = 1.0f;
    void Start()
    {
    }

    void Update()
    {
        this.GetComponent<Image>().fillAmount = fillVal;
    }
}
