using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesRemaining : MonoBehaviour
{
    public int TotalEnemies = 3;
    [SerializeField] Text EnemiesRemainingText;
    [SerializeField] Text YouWonText;
    [SerializeField] GameObject Gun;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesRemainingText.text = "Enemies Remaining: " + TotalEnemies;
        YouWonText.enabled = false;
    }

    void EnemyDead()
    {
        TotalEnemies-=1;
        EnemiesRemainingText.text = "Enemies Remaining: " + TotalEnemies;
        if (TotalEnemies == 0)
        {
            YouWonText.enabled = true;
            Gun.SendMessage("AllEnemiesDead");
        }
    }
}
