using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicaJogador : MonoBehaviour
{
    public int pontos;

    public TextMeshProUGUI textoPontos;

    public void adicionarPonto(int pontoParaAdicionar)
    {
        pontos += pontoParaAdicionar;

        textoPontos.text = pontos.ToString();
    }
}
