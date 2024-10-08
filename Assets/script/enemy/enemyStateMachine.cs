using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateMachine 
{
    public enemyState currentState { get; private set; }
    // Start is called before the first frame update
   public void Init(enemyState _enemyState)
    {
        currentState = _enemyState;
        currentState.Enter();
    }
   public void ChangeState(enemyState _enemyState)
    {
        currentState.Exit();
        currentState = _enemyState;
        currentState.Enter();
    }
}
