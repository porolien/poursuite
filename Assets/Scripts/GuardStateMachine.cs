using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateMachine : MonoBehaviour
{
    public IBaseGuardState currentState;
    public PatrolState patrolState;
    public AlertState alertState;
    public PursuitState pursuitState;
    public DiscussState discussState;
    public Transform player;
    public Transform otherGuard;
    public GameObject[] balises = new GameObject[0];
    public GameObject baliseToPatrol;
    public NavMeshAgent agent;
    float distanceToHeardNoice = 12;

    // L'ennemie va tout initier ici
    void Start()
    {
        discussState = new DiscussState();
        patrolState = new PatrolState();
        alertState = new AlertState();
        pursuitState = new PursuitState();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.GetComponent<PlayerController>()._guardStateMachine = this;
        player.GetComponent<PlayerController>()._noiseAction += OnNoiceMade;
        balises = GameObject.FindGameObjectsWithTag("Balise");
        Transition(patrolState);
    }

    //On va appeler la fonction update de l'état de notre ennemie ici
    void Update()
    {
        currentState.Update();
    }

    //On va faire une transition pour changer l'état de notre ennemie ici,
    //de plus si l'état doit faire quelque chose quand l'ennemie rentre dans cet état ou en sort il le fera ici
    public void Transition(IBaseGuardState _theNewState)
    {
        if (currentState != null) 
        {
            currentState.OnExit();
        }
        currentState = _theNewState;
        currentState.OnEnter(this);
    }

    //Permet de détruire le joueur lorsque l'ennemie le touche
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
            Transition(patrolState);
        }
    }

    //Si jamais le joueur fait du bruit et que l'ennemie est assez proche il va l'entendre et il va changer d'état
    public void OnNoiceMade()
    {
        
        if (currentState != pursuitState && Vector3.Distance(player.position, transform.position) <= distanceToHeardNoice) 
        {
            alertState.noiceHeard = player.position;
            Transition(alertState);
        }
    }
}
