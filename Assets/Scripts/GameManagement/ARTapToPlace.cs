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
    private bool isObjectPlaced_;

    [SerializeField]
    GameObject arCamera = default;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycasttManager = arOrigin.GetComponent<ARRaycastManager>();
        isObjectPlaced_ = false;
    }

    void Update()
    {
        if (isObjectPlaced_)
        {
            return;
        }
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();


        if (placementPoseIsValid)
        {
            objectToPlace.transform.position = PlacementPose.position;
            objectToPlace.transform.rotation = PlacementPose.rotation;
        }

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // && isObjectPlaced_ == false)
        {
            text.GetComponent<Text>().text = "Touched ";// + PlacementPose.position;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                text.GetComponent<Text>().text = "Placed at " + PlacementPose.position;
                objectToPlace.transform.position = PlacementPose.position;
                objectToPlace.transform.rotation = PlacementPose.rotation;
                isObjectPlaced_ = true;
                //Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            text.GetComponent<Text>().text = "the ray doesn't hit anything lol";
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
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

