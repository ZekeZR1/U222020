using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject objectToPlace;

    public GameObject placementIndicator;
    private ARSessionOrigin arOrigin;
    private bool placementPoseIsValid = false;
    private Pose PlacementPose;
    public GameObject text;
    private ARRaycastManager raycasttManager;

    [SerializeField]
    GameObject arCamera = default;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycasttManager = arOrigin.GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        //calc dot
        var arcrot = arCamera.transform.rotation;

        text.GetComponent<Text>().text = "";


        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
            //text.GetComponent<Text>().text = PlacementPose.position.ToString();
        }
        else
        {
            placementIndicator.SetActive(false);
            //text.GetComponent<Text>().text = "the ray doesn't hit anything lol";
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycasttManager.Raycast(screenCenter, hits, TrackableType.All);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
            var cF = Camera.current.transform.forward;
            var cB = new Vector3(cF.x, 0, cF.z).normalized;
            PlacementPose.rotation = Quaternion.LookRotation(cB);
        }
    }
}

