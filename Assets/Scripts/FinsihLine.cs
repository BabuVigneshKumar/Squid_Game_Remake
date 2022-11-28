using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinsihLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement character = other.GetComponent<CharacterMovement>();
    }
}
