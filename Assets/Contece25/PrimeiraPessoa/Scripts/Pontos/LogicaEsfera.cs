using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEsfera : MonoBehaviour
{
    private LogicaJogador logicaJogador;

    private void Start()
    {
        logicaJogador = GameObject.FindGameObjectWithTag("Logica").GetComponent<LogicaJogador>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            logicaJogador.adicionarPonto(1);

            Destroy(gameObject);
        }
    }
}
