using System;
using UnityEngine;

/// <summary>
/// Ecoute les entrées utilisateurs et déplace le personnage du joueur
/// en fonction.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Composant Unity permettant de déplacer facilement un objet dans
    /// un niveau.
    /// </summary>
    /// Un event qui permet de lancer une fonction dans la machine à état de n'importe quel ennemie
    CharacterController _characterController;
    public GuardStateMachine _guardStateMachine;
    public event Action _noiseAction;
    /// <summary>
    /// Vitesse de déplacement (en m/s) du personnage.
    /// </summary>
    [SerializeField]
    float _moveSpeed;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 inputDirection = new Vector3()
        {
            x = Input.GetAxis("Horizontal"),
            y = 0,
            z = Input.GetAxis("Vertical")
        };

        Vector3 velocity = inputDirection * _moveSpeed * Time.deltaTime;

        _characterController.Move(velocity);
    }

    //Va lancer l'event lorsque le joueur fait un clique droit
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("fireeeee");
            _noiseAction();
        }
    }

    
}
