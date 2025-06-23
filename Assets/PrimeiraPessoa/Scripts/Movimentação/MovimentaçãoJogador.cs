using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Esse código faz com que o usuário consiga movimentar o jogador pelo cenário usando física (Rigidbody).

No Start, o Rigidbody é configurado para não girar automaticamente.

Os atributos temos:
    - velocidade: controla a velocidade máxima do jogador
    - atritoChao: define o arrasto quando está no chão
    - AlturaJogador: usado para detectar se o jogador está no chão
    - EhChao: LayerMask (identifica qual Layer está) para identificar o chão
    - orientacao: referência para onde o jogador anda (normalmente igual à orientação da câmera)

No Update:
    - Verifica se o jogador está no chão usando Raycast
    - Lê o input do jogador (WASD/setas)
    - Controla o arrasto do Rigidbody dependendo se está no chão

No FixedUpdate:
    - Aplica a força para mover o jogador na direção desejada

Funções principais:
    - InputJogador(): lê o input do teclado
    - MoverJogador(): aplica movimento na direção baseada na orientação
    - ControleVelocidade(): limita a velocidade máxima do jogador
*/
public class MovimentaçãoJogador : MonoBehaviour
{

    //parte 1
    [Header("Movimento")]

    public float velocidade;

    public float atritoChao;

    //parte 2
    [Header("Chao")]
    public float AlturaJogador;
    public LayerMask EhChao;
    bool chao;

    public Transform orientacao;

    float inputHorizontal;
    float inputVertical;

    Vector3 direcao;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //true se estiver em chao, false caso nao
        chao = Physics.Raycast(transform.position, Vector3.down, AlturaJogador * 0.5f + 0.2f, EhChao);

        //input do usuario para andar
        InputJogador();

        //controlar arrasto
        ControleVelocidade();

        if (chao)
            rb.drag = atritoChao;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MoverJogador();
    }

    private void InputJogador()
    {
        //pega input do usuario
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    private void MoverJogador()
    {
        //faz o jogador andar

        direcao = orientacao.forward * inputVertical + orientacao.right * inputHorizontal;

        rb.AddForce(direcao.normalized * velocidade * 10f, ForceMode.Force);
    }

    private void ControleVelocidade()
    {
        //controla a velocidade do jogador
        Vector3 velocidadeChao = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (velocidadeChao.magnitude > velocidade)
        {
            Vector3 limitado = velocidadeChao.normalized * velocidade;
            rb.velocity = new Vector3(limitado.x, rb.velocity.y, limitado.z);
        }
    }
}
