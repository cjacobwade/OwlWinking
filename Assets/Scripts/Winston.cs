using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Winston : MonoBehaviour 
{
    private Animator animator;
    private SphereCollider sphereCollider;
    void Awake()
    {
        animator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void PlayAction()
    {
        animator.SetTrigger("Bounce");
        sphereCollider.enabled = false;
    }
}
