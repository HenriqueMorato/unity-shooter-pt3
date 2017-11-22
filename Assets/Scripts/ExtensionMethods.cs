using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static Vector3 GerarPosicaoAleatoria(this GameObject go, float distance)
    {
        Vector3 posicao = Random.insideUnitSphere * distance;
        posicao += go.transform.position;
        posicao.y = go.transform.position.y;

        return posicao;
    }
}