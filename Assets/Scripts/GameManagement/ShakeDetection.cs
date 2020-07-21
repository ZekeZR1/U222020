using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetection : MonoBehaviour
{
    [SerializeField]
    GameObject accelerationTextObj;

    float shakeDetectionThreshold = 2.0f;
    private Vector3 acceleration;
    StringBuilder stringBuiler;

    void Start()
    {
        stringBuiler = new StringBuilder("Acceleration : ", 100);
    }

    void Update()
    {
        acceleration = Input.acceleration;
        if (acceleration.sqrMagnitude < shakeDetectionThreshold)
        {
            stringBuiler.Clear();
            stringBuiler.Append("Shake State : None");
            accelerationTextObj.GetComponent<Text>().text = stringBuiler.ToString();
            return;
        }

        //var arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //var d = Vector3.Dot(acceleration.normalized, arCamera.transform.forward.normalized);
        stringBuiler.Clear();
        stringBuiler.Append("Shake detected : Accelerate : " + acceleration);
        accelerationTextObj.GetComponent<Text>().text = stringBuiler.ToString();
    }
}
