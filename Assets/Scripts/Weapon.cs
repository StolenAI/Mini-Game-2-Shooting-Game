using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    float BulletVelocity = 30;
    float BulletPrefabLifeTime = 2f;
    bool FireBullet = true;
    bool PlayerDead = false;
    bool PlayerWon = false;

    //reload variables
    bool reload = false;
    int bullets = 10;
    [SerializeField] Text ReloadText;

    private void Start()
    {
        ReloadText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && FireBullet && !PlayerDead &&!PlayerWon &&!reload)
        {
            FireWeapon();
            FireBullet = false;
            StartCoroutine(FireBulletCoroutine());
        }

        if (Input.GetKey(KeyCode.R) && ReloadText.IsActive())
        {
            bullets = 10;
            reload = false;
            ReloadText.enabled = false;
        }
    }

    private void FireWeapon()
    {
        //Instantiate the bullet
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);
        //Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, BulletPrefabLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    IEnumerator FireBulletCoroutine()
    {
        yield return new WaitForSeconds(0.15f);
        bullets--;  
        FireBullet = true;

        if (bullets <= 0)
        {
            reload = true;
            ReloadText.enabled = true;
        }
             
    }

    public void PlayerDied()
    {
        PlayerDead = true; 
    }

    public void AllEnemiesDead()
    {
        PlayerWon = true;
    }
}
