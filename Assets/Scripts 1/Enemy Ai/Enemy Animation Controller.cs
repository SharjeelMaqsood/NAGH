using UnityEngine;
using UnityEngine.AI;
public class EnemyAnimationController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
