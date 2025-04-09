using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class StatueFlowerActivation : MonoBehaviour
{

    private Animator animator;
    private bool hasActivated = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    async public void AsyncActivateAfterDelay()
    {
        await Task.Delay(3000);

        animator.SetBool("isActive", true);
    }

}

