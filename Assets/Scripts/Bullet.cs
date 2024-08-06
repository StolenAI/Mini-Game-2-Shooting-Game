using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.tag == "Enemy" || hitObject.tag == "Player")
            hitObject.SendMessage("GotHit");

        Destroy(gameObject);
    }
}
