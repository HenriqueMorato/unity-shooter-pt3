﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
	private float quantidadeMaximaZumbis = 2;
	private float quantidadeZumbis;
	private float tempoAumentarDificuldade = 30;
	public float contadorDificuldade = 0;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
		contadorDificuldade = tempoAumentarDificuldade;
		for (int i = 0; i < quantidadeMaximaZumbis; i++) {
			StartCoroutine(GerarNovoZumbi());
		}
    }

    // Update is called once per frame
    void Update () {

        if(Vector3.Distance(transform.position, 
            jogador.transform.position) > 
			DistanciaDoJogadorParaGeracao && quantidadeZumbis < quantidadeMaximaZumbis)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }  

		if(Time.timeSinceLevelLoad > contadorDificuldade)
		{

			contadorDificuldade = Time.timeSinceLevelLoad + tempoAumentarDificuldade;
			quantidadeMaximaZumbis++;
		}
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi ()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }

		ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
		zumbi.meuGerador = this;
		quantidadeZumbis++;
    }

	public void DiminuirQuantidadeZumbis ()
	{
		quantidadeZumbis --;
	}

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
