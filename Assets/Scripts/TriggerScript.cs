using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Enemy.SendMessage("FireAtPlayer");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            Enemy.SendMessage("DoNotFireAtPlayer");
    }
}
