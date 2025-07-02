using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Esse código faz com que o usuário consiga movimentar a camera.

No Start, aquilo é para só travar o cursor do mouse com a camera.

Os atributos temos a 
    - sensiblidade, que vai multiplicar com o input do usuário em mouseX e mouseY
    - orientacao, para mostrar onde o usuario ta olhando
    - rotacao, onde o usuario ta olhando


*/

public class CameraJogador : MonoBehaviour
{
    public float sensibilidadeX;
    public float sensibilidadeY;

    public Transform orientacao;

    float rotacaoX;
    float rotacaoY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensibilidadeX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensibilidadeY;

        //mover mouse esquerda = camera esquerda
        //mover mouse direita = camera direita
        rotacaoY += mouseX;

        /*
        mover mouse cima = camera cima
        mover mouse baixo = camera baixo

        o calculo eh invertido pq senao

        mover mouse cima = camera baixo
        mover mouse baixo = camera cima
        */
        rotacaoX -= mouseY;

        //aqui limita o usario para nao olhar muito alem da sua angulação
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotacaoX, rotacaoY, 0); //gira camera(cima baixo)
        orientacao.rotation = Quaternion.Euler(0, rotacaoY, 0); //gira lados
    }
}
