using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class CreateObject : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;
    public GameObject text;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // 初期化時に呼ばれる
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // フレーム毎に呼ばれる
    void Update()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        if (raycastManager.Raycast(screenCenter, hitResults, TrackableType.All))
        {
            text.GetComponent<Text>().text = "Raycast working";
        }
        else
        {
            text.GetComponent<Text>().text = "Raycast not working";
        }
        if(hitResults.Count > 0)
        {
            text.GetComponent<Text>().text = hitResults[0].pose.position.ToString();
        }
    }
}