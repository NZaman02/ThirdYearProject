using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<CharacterAim_Base>().OnShoot += PlayerShootProjectiles_OnShoot();
    }

}
