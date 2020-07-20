using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject gameManagerObj = default;
    GameManager gameManager;

    private void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag( "Bullet")) { 
            var toObjectVec = col.gameObject.transform.position - this.transform.position;
            var forwardVec = this.transform.forward;
            var deg = Vector3.Dot(toObjectVec.normalized, forwardVec.normalized);
            gameManager.CalcCurrentAccuracy(deg);
        }
    }
}
