using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPCInteractable : MonoBehaviour
{

    public void Interact()
    {
        SceneManager.LoadScene("Quiz Scene");
    }
}
