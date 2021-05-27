using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager rayCastMgr;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private TrackableType trackableTypeMask = TrackableType.Planes;
    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        rayCastMgr = arOrigin.GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
      if(placementPoseIsValid)
      {
        placementIndicator.SetActive(true);
        placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
      }
      else
      {
        placementIndicator.SetActive(false);
      }
    }

    private void UpdatePlacementPose()
    {
      var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
      var hits = new List<ARRaycastHit>();
      // arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);
      rayCastMgr.Raycast(screenCenter, hits, trackableTypeMask);

      placementPoseIsValid = hits.Count > 0;
      if (placementPoseIsValid)
      {
        placementPose = hits[0].pose;
      }
    }
}
