using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Transform cameraPosition;

    GameObject player;

    private void Start()
    {
        player = PlayerMovement.player.gameObject;
        cameraPosition = player.GetComponent<PlayerMovement>().cameraPos.transform;
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
