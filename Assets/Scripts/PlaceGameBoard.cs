using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceGameBoard : MonoBehaviour
{
    public GameObject boardPrefab;

    private ARRaycastManager raycastManager;
    private GameObject spawnedBoard;

    static List<ARRaycastHit> hits = new();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (spawnedBoard != null)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(
    touch.position,
    hits,
    TrackableType.PlaneWithinPolygon))
{
    Pose hitPose = hits[0].pose;

    Vector3 forward = Camera.main.transform.forward;
    forward.y = 0f;
    forward.Normalize();

    Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

    Vector3 position = hitPose.position + Vector3.up * 0.05f;

    spawnedBoard = Instantiate(
    boardPrefab,
    position,
    rotation);

spawnedBoard.transform.localScale = Vector3.one * 0.02f;
}
    }
}