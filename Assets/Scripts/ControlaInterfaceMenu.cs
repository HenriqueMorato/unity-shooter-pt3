using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterfaceMenu : MonoBehaviour {

    public GameObject botaoSair;
    private int quantideZumbisMortos;

	// Use this for initialization
	void Start () {
        #if UNITY_STANDALONE || UNITY_EDITOR
            botaoSair.SetActive(true);
        #endif
    }

	public void JogarJogo ()
	{
		SceneManager.LoadScene("game");
	}

    public void SairDoJogo ()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
