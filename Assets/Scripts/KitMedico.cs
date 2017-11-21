using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour 
{
	private float tempoDestruir = 5;
	private int quantidadeCura = 15;
	void Start()
	{
		Destroy(gameObject, tempoDestruir);
	}
	void OnTriggerEnter(Collider objetoDeColisao)
	{
		if(objetoDeColisao.tag == "Jogador")
		{
			objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeCura);
			Destroy(gameObject);
		}
	}
}
