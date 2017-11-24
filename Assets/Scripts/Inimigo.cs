using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    protected GameObject jogador;
    protected MovimentoPersonagem movimentaInimigo;
    protected AnimacaoPersonagem animacaoInimigo;
    protected Status statusInimigo;
    protected Vector3 direcao;
    protected ControlaInterface scriptControlaInterface;

	// Use this for initialization
	protected void Start () {
        jogador = GameObject.FindWithTag("Jogador");
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
}
