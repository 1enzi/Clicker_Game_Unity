using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    public Animator DogAnimator;

    void Start()
    {
        DogAnimator = GetComponent<Animator>();
    }

    private void OnMouseUp()
    {
        DogAnimator.Play("DogDrag");
        DogAnimator.Play("DogIdle");
    }
}
