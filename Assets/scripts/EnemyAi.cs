using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 2f;
    

    NavMeshAgent navMeshAgent;
    float distanceTarget = Mathf.Infinity;
    bool isProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    // Update is called once per frame
    void Update()
    {
        distanceTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
            EngageTarget();
        else if (distanceTarget <= chaseRange) {
            isProvoked = true;
            navMeshAgent.SetDestination(target.position);
        }

    }

    void EngageTarget() {
        FaceTarget();
        if(distanceTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }

    }

    private void ChaseTarget() {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Debug.Log(name + "has seeked and is destroying " + target.name);
    }

    private void FaceTarget() {
        // rotate to the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
