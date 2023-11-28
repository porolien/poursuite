using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IBaseGuardState
{
    float distanceToFindPlayer = 6;
    float distanceToChangeBalise = 1;
    float distanceToFindOtherGuard = 3;
    float discussDelay = 3;
    bool canDiscuss;
    GuardStateMachine stateMachine;
    public void OnEnter(GuardStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        ChooseABalise();
        canDiscuss = false;
        _stateMachine.StartCoroutine(WaitToDiscuss());
    }

    public void OnExit()
    {

    }

    //Si il trouve le joueur il va le poursuivre
    public void Update()
    {
        if(stateMachine.player != null)
        {
            if (Vector3.Distance(stateMachine.player.position, stateMachine.transform.position) <= distanceToFindPlayer)
            {
                stateMachine.Transition(stateMachine.pursuitState);
            }
            else
            {
                MoveToABalise();
            }
        }
        else
        {
            MoveToABalise();
        }
        
    }

    //Si il trouve un garde il va aller lui parler
    void MoveToABalise()
    {
        stateMachine.agent.destination = stateMachine.baliseToPatrol.transform.position;

        if(Vector3.Distance(stateMachine.otherGuard.position, stateMachine.transform.position) <= distanceToFindOtherGuard && canDiscuss)
        {
            stateMachine.Transition(stateMachine.discussState);
        }

        else if (Vector3.Distance(stateMachine.baliseToPatrol.transform.position, stateMachine.transform.position) <= distanceToChangeBalise)
        {
            ChooseABalise();
        }
    }

    //Il va choisir une nouvelle balise si il est arrivé à la balise
    void ChooseABalise()
    {
        stateMachine.baliseToPatrol = stateMachine.balises[Random.Range(0, stateMachine.balises.Length)];
    }

    //On ajoute un délai avant que l'ennemie ne puisse discuter avec un autre garde
    public IEnumerator WaitToDiscuss()
    {
        yield return new WaitForSeconds(discussDelay);
        if (stateMachine.currentState == this)
        {
            canDiscuss = true;
        }
    }
}
