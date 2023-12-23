using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using YG;

public class Fredy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] Animator _animator;
    [SerializeField] private float _lookRadius;
    [SerializeField] private Transform _targetMobile;

    private float _distance;
    private void Start()
    {
        _agent.GetComponent<NavMeshAgent>();
        if (YandexGame.EnvironmentData.isMobile)
        {
            _target = _targetMobile;
        }
    }

    private void Update()
    {
        _distance = Vector3.Distance(_target.position, transform.position);
        if (_distance <= 2)
        {
            SceneManager.LoadScene("Death");
        }
        
        if (_distance <= _lookRadius)
        {
            _animator.SetBool("isRun", true);
            _agent.SetDestination(_target.position);
            LookTarget();
        }
        else
        {
            _animator.SetBool("isRun", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }

    private void LookTarget()
    {
        Vector3 _direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_direction.x,0,_direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime);
    }
}
