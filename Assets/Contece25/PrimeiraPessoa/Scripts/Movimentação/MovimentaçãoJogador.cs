using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Esse código faz com que o usuário consiga movimentar o jogador pelo cenário usando física (Rigidbody).

No Start, o Rigidbody é configurado para não girar automaticamente.

Os atributos temos:
    - velocidade: controla a velocidade máxima do jogador
    - atritoChao: define o arrasto quando está no chão
    - forcaPulo: força aplicada ao pular
    - cooldownPulo: tempo de recarga entre pulos
    - MultiplicadorPulo: multiplica a força de movimento no ar
    - prontoPulo: indica se o jogador pode pular novamente
    - AlturaJogador: usado para detectar se o jogador está no chão
    - EhChao: LayerMask (identifica qual Layer está) para identificar o chão
    - orientacao: referência para onde o jogador anda (normalmente igual à orientação da câmera)
    - PularKey: tecla usada para pular

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
    - Pular(): executa o pulo
    - ResetarPulo(): reseta o cooldown do pulo
*/
public class MovimentaçãoJogador : MonoBehaviour
{

    //parte 1
    [Header("Movimento")]

    public float velocidade;

    public float atritoChao;

    public float forcaPulo;

    public float cooldownPulo;

    public float MultiplicadorPulo;

    bool prontoPulo;

    [Header("Keybinds")]
    public KeyCode PularKey = KeyCode.Space;

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

        prontoPulo = true;
    }

    private void Update()
    {
        //true se estiver em chao, false caso nao
        chao = Physics.Raycast(transform.position, Vector3.down, AlturaJogador * 0.5f + 0.2f, EhChao);

        Debug.DrawRay(transform.position, Vector3.down * (AlturaJogador * 0.5f + 0.3f), chao ? Color.green : Color.red);

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

        //quando pular
        if (Input.GetKey(PularKey) && prontoPulo && chao)
        {
            Debug.Log("Pular triggered!");
            prontoPulo = false;

            Pular();

            Invoke(nameof(ResetarPulo), cooldownPulo);
        }
    }

    private void MoverJogador()
    {
        //faz o jogador andar

        direcao = orientacao.forward * inputVertical + orientacao.right * inputHorizontal;

        //no chao
        if (chao)
            rb.AddForce(direcao.normalized * velocidade * 10f, ForceMode.Force);

        else if (!chao)
            rb.AddForce(direcao.normalized * velocidade * 10f * MultiplicadorPulo, ForceMode.Force);
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

    private void Pular()
    {
        //reseta velocidade Y
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
    }

    private void ResetarPulo()
    {
        prontoPulo = true;
    }
}
