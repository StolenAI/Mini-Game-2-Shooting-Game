using System.Collections;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public float health = 100;
    bool dead = false;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject TriggerPoint;
    [SerializeField] GameObject EnemiesRemainingText;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public bool Firing = false;
    public bool PlayerNear = false;
    public bool BulletCooldown = true;
    private float BulletPrefabLifeTime = 1f;

    private float BulletVelocity = 30;

    public bool reload = false;
    int bullets = 10;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            if (health <= 0)
            {
                dead = true;
                StopFiring();
                animator.SetBool("Dead", true);
                EnemiesRemainingText.SendMessage("EnemyDead");
                StartCoroutine(DeadAndDestroy());
            }

            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            PlayerNear = distanceToPlayer < 20;

            if ((Firing || PlayerNear) && BulletCooldown && !reload)
            {
                StartFiring();
                Vector3 direction = Player.transform.position - transform.position;
                direction.y = 0; // Keep the y component unchanged
                transform.rotation = Quaternion.LookRotation(direction);

                //Shoot 
                GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);
                StartCoroutine(DestroyBulletAfterTime(bullet, BulletPrefabLifeTime));

                BulletCooldown = false;
                StartCoroutine(FireBulletCoroutine());
            }
        }
    }

    void StartFiring()
    {
        if (!reload)
        {
            Firing = true;
            animator.SetBool("Firing", true);
        }
    }

    void StopFiring()
    {
        Firing = false;
        animator.SetBool("Firing", false);
    }

    IEnumerator FireBulletCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        bullets--;
        BulletCooldown = true;

        if (bullets <= 0)
        {
            reload = true;
            StopFiring();
            animator.SetTrigger("Reload");
            yield return new WaitForSeconds(3f);
            reload = false;
            Firing = true;
            bullets = 10;
        }
    }
    void FireAtPlayer()
    {
        Firing = true;
    }

    void DoNotFireAtPlayer()
    {
        Firing = false;
        animator.SetBool("Firing", false);
    }
    public void GotHit()
    {
        if (health > 0)
            health -= 10;
    }

    IEnumerator DeadAndDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(TriggerPoint);
        Destroy(gameObject);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
