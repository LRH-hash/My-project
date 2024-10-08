using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState { get; private set; }

    // Start is called before the first frame update
   public void Init(PlayerState playerState)
    {
        currentState = playerState;
        currentState.Enter();
    }

    // Update is called once per frame
    public void ChangeState(PlayerState playerState)
    {
        currentState.Exit();
        currentState = playerState;
        currentState.Enter();
    }
}
