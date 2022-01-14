using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectsX : MonoBehaviour
{
    public float spinSpeed;

    private PlayerControllerX _playerControllerX;

    private void Start()
    {
        _playerControllerX = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_playerControllerX.gameOver)
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
