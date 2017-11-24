using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class ControlaChefe : Inimigo, IMatavel 
{
	private NavMeshAgent agente;
	public GameObject KitMedicoPrefab;

	[Header("Vida Chefe")]
	private Slider sliderVidaChefe;
	public Image SliderImagem;
	public Color CorDaVidaMaxima, CorDaVidaMinima;


	new void Start()
	{
		base.Start();
		agente = GetComponent<NavMeshAgent>();
		agente.speed = statusInimigo.Velocidade;
		sliderVidaChefe = transform.GetComponentInChildren<Slider>();
		sliderVidaChefe.value = sliderVidaChefe.maxValue = statusInimigo.VidaInicial;
		AtualizarInterface();
	}

	void Update()
	{
		agente.SetDestination(jogador.transform.position);
		animacaoInimigo.Movimentar(agente.velocity.magnitude);

		if(agente.pathPending == false)
		{
			bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;
			if(estouPertoDoJogador == true)
			{
				agente.isStopped = true;
				animacaoInimigo.Atacar(true);
				Vector3 direcao = jogador.transform.position - transform.position;
				movimentaInimigo.Rotacionar(direcao);	
			}	
			else
			{
				agente.isStopped = false;				
				animacaoInimigo.Atacar(false);
			}
		}	
	}

	void AtacaJogador ()
    {
		int dano = Random.Range(40,60);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

	public void TomarDano(int dano)
	{
		statusInimigo.Vida -= dano;
		AtualizarInterface();
		if(statusInimigo.Vida <= 0)
		{
			Morrer();
		}
	}

	void AtualizarInterface ()
	{
		sliderVidaChefe.value = statusInimigo.Vida;
		Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, (float)statusInimigo.Vida / statusInimigo.VidaInicial);
		SliderImagem.color = corDaVida;
	}

    public void Morrer()
    {
		agente.enabled = false;
        animacaoInimigo.Morrer();
		movimentaInimigo.Morrer();
        this.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 2);
    }
}
