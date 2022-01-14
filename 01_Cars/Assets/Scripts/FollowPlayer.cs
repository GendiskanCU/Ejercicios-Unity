using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;//El objeto al que debe seguir

    public Vector3 offset = new Vector3(0f, 4f, -7f);

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
