using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

    public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    private Status statusJogador;

	void Start () 
    {
		statusJogador = GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && statusJogador.Vida > 0)
        {
            Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
	}
}
