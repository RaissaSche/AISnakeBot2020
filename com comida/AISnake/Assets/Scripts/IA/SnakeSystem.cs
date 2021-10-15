using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSystem : MonoBehaviour
{
    public State _currentState;

    public void SetState(State state)
    {
        _currentState = state;
        StartCoroutine(_currentState.Start());
    }

    public void ChaseMoney()
    {
        StartCoroutine(_currentState.ChaseMoney());
    }

    public void EvadeEnemies()
    {
        StartCoroutine(_currentState.EvadeEnemies());
    }
}