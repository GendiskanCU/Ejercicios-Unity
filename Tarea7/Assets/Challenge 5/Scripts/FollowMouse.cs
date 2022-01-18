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
        animator.SetBool("hit", false);
    }

    // Update is called once per frame
    void Update()
    {
        placeObject();
        AnimateObject();
    }

    /// <summary> Coloca el objeto en la posición del cursor con un ajuste para mejorar la visualización </summary>
    private void placeObject()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x + 0.42f, mousePosition.y - 0.68f, -2f);
    }
    
    /// <summary> Inicia la animación al pulsar el botón principal del ratón y la detiene al soltarlo </summary>
    private void AnimateObject()
    {
        bool hit;
        
        if (Input.GetMouseButtonDown(0))
        {
            hit = true;
        }
        else
        {
            hit = false;
        }

        if (hit)
        {
            animator.SetBool("hit", true);
            
        }
        else
        {
            animator.SetBool("hit", false);
        }
    }
}
