using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int bullets = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            FindObjectOfType<Weapon>().AddAmmo(bullets);
            Destroy(gameObject);
        }
    }
}
