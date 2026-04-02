using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private float _currenteHealth;
   [SerializeField] private Transform _player;
   [SerializeField] private float _followStartRadius = 3f;
   [SerializeField] private float _followEndRadius = 5f;
   [SerializeField] private float _rotationSpeed = 3f;

   private Vector3 _originalPosition;
   private Vector3 _direction;
   private bool _isFollowing = false;
   private Animator _animator;
   private NavMeshAgent _agent;
   
    private void Start()
    {
        _originalPosition = transform.position;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);
        float z = _agent.velocity.z;
        _animator.SetFloat("Vertical", z);
        
    }
    
   
   public void TakeDamage(int damage) 
   {
       _currenteHealth -= damage;
       if (_currenteHealth <= 0)
       {
           Destroy(gameObject);
       }
   }
}
