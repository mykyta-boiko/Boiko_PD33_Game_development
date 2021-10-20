using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMan : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _enemySystem;
    [SerializeField] private float _walkRange;
    [SerializeField] private float _speed;
    [SerializeField] private float _pushPower;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private bool _faceRight;

    private Vector2 _startPostion;
    private int _currentHitPoints;

    private void Start()
    {
        ChangeHitPoints(_maxHitPoints);
        _startPostion = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_drawPostion, new Vector3(_walkRange * 2, 1, 0));
    }

    private void Update()
    {
        float xPos = transform.position.x;
        if (xPos > _startPostion.x + _walkRange && _faceRight)
        {
            Flip();
        }
        else if (xPos < _startPostion.x - _walkRange && !_faceRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    private void ChangeHitPoints(int hitPoints)
    {
        _currentHitPoints = hitPoints;
        if (_currentHitPoints <= 0)
        {
            Destroy(_enemySystem);
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMover player = other.collider.GetComponent<PlayerMover>();
        if (player != null)
        {
                player.TakeDamage(_damage, _pushPower, transform.position.x);
        }
    }

    private Vector2 _drawPostion
    {
        get
        {
            if (_startPostion == Vector2.zero)
                return transform.position;
            else
                return _startPostion;
        }
    }

    public void TakeDamage(int damage)
    {
        ChangeHitPoints(_currentHitPoints - damage);
    }
}