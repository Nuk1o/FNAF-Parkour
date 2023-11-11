using UnityEngine;
using UnityEngine.AI;

public class Fredy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] Animator _animator;

    private float _distance;
    private void Start()
    {
        _agent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _distance = Vector3.Distance(_target.position, transform.position);
        if (_distance <= _agent.stoppingDistance)
        {
            Debug.Log("GameOver");
        }
        _animator.SetBool("isRun", true);
        _agent.SetDestination(_target.position);
        LookTarget();
    }

    private void LookTarget()
    {
        Vector3 _direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_direction.x,0,_direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime);
    }
}
