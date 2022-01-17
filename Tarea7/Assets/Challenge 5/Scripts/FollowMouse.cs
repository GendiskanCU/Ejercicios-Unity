using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mousePosition;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -0.5f);

        //TODO: Hay que ajustar la animación del martillazo
        if (Input.GetMouseButton(0))
        {
            animator.speed = 1;
            Invoke("StopAnimation", 0.2f);
        }


    }

    private void StopAnimation()
    {
        animator.speed = 0;
    }
}
