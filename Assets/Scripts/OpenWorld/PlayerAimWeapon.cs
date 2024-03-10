using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs: EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    public Transform aimGunEndPointTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();

    }

    private void HandleAiming()
    {
        Vector3 mousePostion = GetMouseWorldPosition();


        Vector3 aimDirection = (mousePostion - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (aimDirection.x < 0)
        {
            aimTransform.localScale = new Vector3(-1, 1, 1); 
            angle += 180f; 
        }
        else
        {
            aimTransform.localScale = new Vector3(1, 1, 1); 
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }


    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && IsMouseInShootingArea())
        {
            Vector3 mousePosition= GetMouseWorldPosition();
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
            nextFireTime = Time.time + fireRate; 
        }
    }

        public Rect[] nonShootingAreas = new Rect[]
        {

        // Define non-shooting areas for the top-left corner of the screen
         new Rect(0f, 1f - 0.27f, 0.2f, 0.27f),  // Top-left corner

         // Define non-shooting areas for the top-right corner of the screen
         new Rect(1f - 0.2f, 1f - 0.25f, 0.2f, 0.25f) // Top-right corner
        };

    
    private bool IsMouseInShootingArea()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector2 normalizedMousePos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);

        // Check if the mouse position is within any shooting area
        foreach (Rect area in nonShootingAreas)
        {
            if (area.Contains(normalizedMousePos))
            {
                return false;
            }
        }

        return true;
    }


public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }



}
