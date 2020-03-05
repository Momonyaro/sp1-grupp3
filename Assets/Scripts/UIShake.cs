using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    public float moveStep = .4f;
    
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private GameObject _currentTarget;
    private bool _blinked = false;
    private bool _boomerang = false;
    private bool _returning = false;

    private void Update()
    {
        if (_blinked)
        {
            _currentTarget.transform.position = Vector2.MoveTowards(_currentTarget.transform.position ,_startPos, moveStep);
            
            if (Vector2.Distance(_currentTarget.transform.position, _startPos) < 0.1f)
            {
                _blinked = false;
            }
        }

        if (!_boomerang) return;
        if (_returning)
        {
            _currentTarget.transform.position = Vector2.MoveTowards(_currentTarget.transform.position ,_startPos, moveStep);
                
            if (Vector2.Distance(_currentTarget.transform.position, _startPos) < 0.1f)
            {
                _boomerang = false;
            }
        }
        else
        {
            _currentTarget.transform.position = Vector2.MoveTowards(_currentTarget.transform.position , _targetPos, moveStep);
                
            if (Vector2.Distance(_currentTarget.transform.position, _targetPos) < 0.1f)
            {
                _returning = true;
            }
        }
    }

    public void BlinkAndMove(GameObject g, Vector3 targetPosition)
    {
        AssignTarget(g);
        _currentTarget.transform.position = targetPosition;
        _blinked = true;
    }

    public void Boomerang(GameObject g, Vector3 targetPosition)
    {
        AssignTarget(g);
        _targetPos = targetPosition;
        _boomerang = true;
        _returning = false;
    }
    
    private void AssignTarget(GameObject g)
    {
        if (_currentTarget != null)
        {
            _currentTarget.transform.position = _startPos;
        }

        _currentTarget = g;
        _startPos = _currentTarget.transform.position;
        _blinked = false;
        _boomerang = false;
    }
}
