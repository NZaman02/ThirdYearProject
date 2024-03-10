using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 shootDir;
    private float moveSpeed = 10f;


    private float shrinkSpeed = 0.7f;
    private Vector3 startingScale;
    public float minScale = 0.1f;
    private Collider2D bulletCollider;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0,0,GetAngleFromVectorFloat(shootDir));
        startingScale = transform.localScale;
        bulletCollider = GetComponent<Collider2D>();

    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        ShrinkBullet();
        if (transform.localScale.magnitude <= minScale)
        {
            Destroy(gameObject);
        }

    }


    private void ShrinkBullet()
    {
        // Decrease the scale gradually
        transform.localScale -= startingScale * shrinkSpeed * Time.deltaTime;
        if (bulletCollider != null)
        {
            bulletCollider.transform.localScale = transform.localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NPCInteractable npc = other.GetComponent<NPCInteractable>();
        if (npc != null)
        {
            npc.Interact();
            Destroy(gameObject); 
        }
    }

}


