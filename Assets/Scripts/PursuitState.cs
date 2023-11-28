using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : IBaseGuardState
{
    float distanceToStopFollowThePlayer = 5;
    GuardStateMachine stateMachine;
    public void OnEnter(GuardStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public void OnExit()
    {

    }

    //Poursuit le joueur, si il est trop loin il va repartir patrouiller
    public void Update()
    {
        if (Vector3.Distance(stateMachine.player.position, stateMachine.transform.position) >= distanceToStopFollowThePlayer)
        {
            stateMachine.Transition(stateMachine.patrolState);
        }
        else
        {
            stateMachine.agent.destination = stateMachine.player.position;
        }
    }
}
