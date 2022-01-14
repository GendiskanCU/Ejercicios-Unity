using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))//Si un proyectil colisiona con el objeto
        {
            //Destruimos ambos
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
