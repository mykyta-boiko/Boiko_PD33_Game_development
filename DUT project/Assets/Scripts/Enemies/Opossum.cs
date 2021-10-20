using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opossum : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _enemySystem;
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewingDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _pushPower;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private Slider _hitPointsBar;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _chasingAnimatorKey;

    private int _currentHitPoints;

    void Start()
    {
        _hitPointsBar.maxValue = _maxHitPoints;
        ChangeHitPoints(_maxHitPoints);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer < _viewingDistance)
        {
            _animator.SetBool(_chasingAnimatorKey, true);
            StartChasing();
        }
        else
        {
            _animator.SetBool(_chasingAnimatorKey, false);
            StopChasing();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMover player = other.collider.GetComponent<PlayerMover>();
        if (player != null)
        {
                player.TakeDamage(_damage, _pushPower, transform.position.x);
        }
    }

    private void ChangeHitPoints(int hitPoints)
    {
        _currentHitPoints = hitPoints;
        if (_currentHitPoints <= 0)
        {
            Destroy(_enemySystem);
        }
        _hitPointsBar.value = hitPoints;
    }

    public void StartChasing()
    {
        if (_player.position.x < transform.position.x)
        {
            _rigidbody.velocity = new Vector2(-_speed, 0);
            _spriteRenderer.flipX = false;
        }

        else if (_player.position.x > transform.position.x)
        {
            _rigidbody.velocity = new Vector2(_speed, 0);
            _spriteRenderer.flipX = true;
        }

    }

    public void StopChasing()
    {
        _rigidbody.velocity = new Vector2(0, 0);
    }

    public void TakeDamage(int damage)
    {
        ChangeHitPoints(_currentHitPoints - damage);
    }
}
