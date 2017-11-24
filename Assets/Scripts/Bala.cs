using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 20;
    private Rigidbody rigidbodyBala;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position + 
            transform.forward * Velocidade * Time.deltaTime);
	}

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
			    ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
			    inimigo.TomarDano(1);
                Quaternion inversaoDoSentidoDeRotacao = Quaternion.LookRotation(-transform.forward);
			    inimigo.ParticulaSangue(transform.position, inversaoDoSentidoDeRotacao);
            break;
            case "ChefeDeFase":
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(1);
            break;
        }
        

        Destroy(gameObject);
    }
}
