using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomSpeed = 5f;
    private float zoomInput = 0f;



    // Update is called once per frame
    void Update()
    {
        Vector3 newPos  = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

       
        if (Input.GetKey(KeyCode.O))
        {
            zoomInput = 1f;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            zoomInput = -1f;
        }

        ZoomCamera(zoomInput);
    }

    void ZoomCamera(float zoomInput)
    {
        Camera.main.orthographicSize -= zoomInput * zoomSpeed * Time.deltaTime;

        // Clamp the orthographic size to avoid extreme zoom levels
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

    }
}
