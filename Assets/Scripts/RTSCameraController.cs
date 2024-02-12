using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [Header("Settings")]
    [SerializeField] float edgeScrollSpeed = 1.0f;
    [SerializeField] float dragScrollSpeed = 1.0f;
    [SerializeField] float zoomSpeed = 1.0f;

    Vector2? prevMousePos = null;

    private void OnValidate()
    {
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        // MouseWheel
        if (Input.GetMouseButtonUp(2))
        {
            prevMousePos = null;
        }
        if (Input.GetMouseButton(2))
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            if (prevMousePos != null)
            {
                Vector2 mouseScreenDelta = mouseScreenPos - prevMousePos.Value;
                Vector2 mouseWorldDelta = mouseScreenDelta;
                MoveCam(-mouseWorldDelta * Time.deltaTime);
            }
            prevMousePos = mouseScreenPos;
        }

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            ZoomCam(Input.mouseScrollDelta.x * zoomSpeed * Time.deltaTime);
        }
    }

    void MoveCam(Vector2 moveDelta)
    {
        transform.Translate(new Vector3(moveDelta.x, 0, moveDelta.y), Space.World);
    }

    void ZoomCam(float zoomDelta)
    {
        transform.Translate(cam.transform.forward * zoomDelta, Space.World);
    }
}
