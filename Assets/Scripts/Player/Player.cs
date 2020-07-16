using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            var toObjectVec = col.gameObject.transform.position - this.transform.position;
            var forwardVec = this.transform.forward;
            var deg = Vector3.Dot(toObjectVec.normalized, forwardVec.normalized);
            if (deg > 0.7f)
            {
                Debug.Log("blocked!!");
            }
            else
            {
                Debug.Log("Dead!!");
            }
        }
    }
}
