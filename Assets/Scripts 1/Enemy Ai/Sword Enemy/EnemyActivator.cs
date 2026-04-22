using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [Header("Detection")]
    public Transform player;
    public float activationDistance = 15f;

    [Header("Enemy")]
    public GameObject enemyRoot; 
    private bool isActive;

    void Start()
    {
        if (enemyRoot == null)
            enemyRoot = gameObject;

        enemyRoot.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isActive && distance <= activationDistance)
        {
            ActivateEnemy();
        }
    }

    void ActivateEnemy()
    {
        isActive = true;
        enemyRoot.SetActive(true);
    }
}