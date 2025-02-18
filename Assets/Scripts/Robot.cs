using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


 enum RobotStates { Counting , Inspecting};
public class Robot : MonoBehaviour
{
    [SerializeField] private float startInspectionTime = 2f;
    [SerializeField] private AudioSource jinglesource;

    private float currentInspectionTime;
    private RobotStates currentstate = RobotStates.Counting;

    public delegate void OnStartCountingDelegate();
    public OnStartCountingDelegate OnStartCounting;

    public delegate void OnStopCountingDelegate();
    public OnStopCountingDelegate OnStopCounting;

    private Animator animator;
    private List<CharacterMovement> characters = new List<CharacterMovement>();
    


    void Start()
    {
        characters = FindObjectsOfType<CharacterMovement>().ToList(); 
       animator =  GetComponentInChildren<Animator>();
       

        currentInspectionTime = startInspectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (characters == null)
            return;
        if (characters.Count <= 0)
            return;

        StateMachine();
    }
    private void StateMachine()
    {
        switch (currentstate)
        {

            case RobotStates.Counting:
                Count();
                break;
                case RobotStates.Inspecting:
                Inspect();
                break;

            default:
            break;
        }
    }

    private void Count()
    {
        if (!jinglesource.isPlaying)
        {
            animator.SetTrigger("Look");
            currentstate = RobotStates.Inspecting;
            OnStopCounting?.Invoke();
        }
    }
    private void Inspect()
    {
        if (currentInspectionTime > 0)
        {
            currentInspectionTime -= Time.deltaTime;
            var charsToRemove = new List<CharacterMovement>();
            foreach (var character in characters)
            {
                if (character.isMoving() && character.isInvulnerable == false)
                {
                    charsToRemove.Add(character);
                }
            }
                foreach(var character in charsToRemove)
                {
                    characters.Remove(character);
                character.Die();                
                }
            
        }

        else
        {
            currentInspectionTime = startInspectionTime;
            animator.SetTrigger("Look");


            jinglesource.Play();
            currentstate = RobotStates.Counting;
            OnStartCounting?.Invoke();
        }
    }

}
