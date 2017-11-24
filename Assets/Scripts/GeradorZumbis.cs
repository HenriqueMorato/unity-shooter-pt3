using System.Collections;
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
	private float quantidadeMaximaDeZumbisVivos = 2;
	private float quantidadeDeZumbisVivos;
	private float tempoProximoAumentoDificuldade = 30;
	private float tempoAumentarDificuldade = 0;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
		tempoAumentarDificuldade = tempoProximoAumentoDificuldade;
		for (int i = 0; i < quantidadeMaximaDeZumbisVivos; i++) {
			StartCoroutine(GerarNovoZumbi());
		}
    }

    // Update is called once per frame
    void Update () {

        bool possoGerarZumbis = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao;
        if(possoGerarZumbis && quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }  

		if(Time.timeSinceLevelLoad > tempoAumentarDificuldade)
		{

			tempoAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDificuldade;
			quantidadeMaximaDeZumbisVivos++;
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
		quantidadeDeZumbisVivos++;
    }

	public void DiminuirQuantidadeZumbisVivos ()
	{
		quantidadeDeZumbisVivos --;
	}

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
