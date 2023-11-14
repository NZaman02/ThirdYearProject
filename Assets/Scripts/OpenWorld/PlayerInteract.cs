using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public string animalInteractedWith;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 3f;
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);

            foreach (Collider2D collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }
    }
}
