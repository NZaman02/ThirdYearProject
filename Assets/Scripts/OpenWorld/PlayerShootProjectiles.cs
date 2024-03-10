using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform bullet;

    #region BaseSetup
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public GameObject hitObject;
    }

    private void Awake()
    {
        OnShoot += PlayerShootProjectiles_OnShoot();
    }


    private void PlayerShootProjectiles_OnShoot(object sender, OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(bullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = e.shootPosition - e.gunEndPointPosition.normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
    
}
