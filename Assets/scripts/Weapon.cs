using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] int ammo = 10;

    [Header("Weapon Attr")]
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject bulletImpact;

    private float bulletFade = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        } 
    }

    private void Shoot() {
        ProcessRaycast();
    }

    private void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range) && ammo > 0)
        {
            ammo --;
            PlayMuzzleFlash();
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash() {
       flash.Play();
    }

    private void CreateHitImpact(RaycastHit hit) {
        GameObject impact = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, bulletFade);
    }

    public void AddAmmo(int bullet) {
        ammo += bullet;
    }
}
