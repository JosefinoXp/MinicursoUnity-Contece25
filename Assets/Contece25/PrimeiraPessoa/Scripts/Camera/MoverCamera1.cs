using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Esse código faz com que a camera do usuario siga a capsula do jogador
*/

public class MoverCamera : MonoBehaviour
{
    public Transform posicaoCamera;

    // Update is called once per frame
    void Update()
    {
        transform.position = posicaoCamera.position;
    }
}
