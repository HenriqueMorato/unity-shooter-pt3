using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantideDeZumbisMortos;
    public Text TextoQuantideDeZumbisMortos;
    public Text TextoChefeAparece;

	// Use this for initialization
	void Start () {
        scriptControlaJogador = GameObject.FindWithTag("Jogador")
                                .GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeZumbisMortos ()
    {
        quantideDeZumbisMortos++;
        TextoQuantideDeZumbisMortos.text = string.Format("X {0}", quantideDeZumbisMortos);
    }

    public void GameOver ()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = 
            "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima (int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = 
                string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            TextoPontuacaoMaxima.text =
                string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar ()
    {
		StartCoroutine(MudarCena("game"));
    }

	public void JogarJogo ()
	{
		StartCoroutine(MudarCena("game"));
	}

    IEnumerator MudarCena (string name)
    {
        //TODO: discutir porque o uso do unscaled time
        yield return new WaitForSecondsRealtime(0.3f);       
        SceneManager.LoadScene(name);
    }

    public void SairDoJogo ()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void AparecerTextoDoChefeCriado ()
	{
		StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
	}

	IEnumerator DesaparecerTexto (float tempoDeSumico, Text texto)
	{
		texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);
		texto.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);	
		while(texto.color.a > 0)
		{
			texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a - (Time.deltaTime / tempoDeSumico));
			if(texto.color.a <= 0)
			{
				texto.gameObject.SetActive(false);
			}
			yield return null;
		}
	}
}
