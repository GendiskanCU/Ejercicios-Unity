using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 0.1f;

    private PlayerController _playerController;//Para capturar el script que controla al Player

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver)//Mientras no se haya llegado al fin de la partida
            transform.Translate(Vector3.left * speed * Time.deltaTime); //Se desplazará hacia la izquierda
    }
}
