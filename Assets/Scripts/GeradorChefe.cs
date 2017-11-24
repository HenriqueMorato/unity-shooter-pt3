using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour 
{
	public GameObject ChefePrefab;
	private float tempoParaProximaGeracao = 0;
    public float TempoEntreGeracoes = 60;
	public Transform[] PosicoesPossiveisDeGeracao;
	private Transform jogador;

	void Start ()
	{
		tempoParaProximaGeracao = TempoEntreGeracoes;
		jogador = GameObject.FindWithTag("Jogador").transform;
	}


	void Update ()
	{
		if(Time.timeSinceLevelLoad > tempoParaProximaGeracao)
		{
			Vector3 posicaoDeCriacao = CalcularPosicaoPossivelMaisDistanteDoJogador();
			Debug.Log(posicaoDeCriacao);
			Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
			tempoParaProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
		}
	}

	Vector3 CalcularPosicaoPossivelMaisDistanteDoJogador ()
	{
		Vector3 posicaoDeMaiorDistancia = Vector3.zero;
		float maiorDistancia = 0;
		foreach(Transform posicao in PosicoesPossiveisDeGeracao)
		{
			float distanciaPosicaoJogador = Vector3.Distance(jogador.position, posicao.position);
			if(distanciaPosicaoJogador > maiorDistancia)
			{
				maiorDistancia = distanciaPosicaoJogador;
				posicaoDeMaiorDistancia = posicao.position;				
			}
		}
		return posicaoDeMaiorDistancia;
	}
}