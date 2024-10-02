using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;
    private AparecerImagenes imagenesBuenas;
    private AparecerImagenesIncorrectas imagenesIncorrectas;
    private AudioSource sonidoPunto;
    public string TipoNivel;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        imagenesBuenas = FindObjectOfType<AparecerImagenes>();
        imagenesIncorrectas = FindObjectOfType<AparecerImagenesIncorrectas>();
        sonidoPunto = GetComponent<AudioSource>();
    }

    private void Update()
    {
        textMesh.text = puntos.ToString("0");
        AumentarDificultad(puntos);
    }

    public void SumarPuntos(float puntosEntrada)
    {
        sonidoPunto.Play();
        puntos += puntosEntrada;
    }

    private void AumentarDificultad(float puntaje)
    {
        if (TipoNivel == null)
        {
            if (puntaje > 10)
            {
                imagenesBuenas.frecuencia = 1.5f;
                imagenesIncorrectas.frecuencia = 2;
            }
            if (puntaje > 25)
            {
                imagenesBuenas.frecuencia = 1;
                imagenesIncorrectas.frecuencia = 1f;
            }
            if (puntaje > 50)
            {
                imagenesBuenas.frecuencia = 0.5f;
                imagenesIncorrectas.frecuencia = 0.5f;
            }
        }
        else if(TipoNivel == "Todos")
        {
            if (puntaje > 10)
            {
                imagenesBuenas.frecuencia = 0.8f;
            }
            if (puntaje > 25)
            {
                imagenesBuenas.frecuencia = 0.5f;
            }
            if (puntaje > 50)
            {
                imagenesBuenas.frecuencia = 0.3f;
            }
        }
    }
}
