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
    private Victoria Victoria;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        imagenesBuenas = FindObjectOfType<AparecerImagenes>();
        imagenesIncorrectas = FindObjectOfType<AparecerImagenesIncorrectas>();
        sonidoPunto = GetComponent<AudioSource>();
        Victoria = FindObjectOfType<Victoria>();
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
        if(TipoNivel == "Todos")
        {
            if (puntaje > 9)
            {
                imagenesBuenas.frecuencia = 1.8f;
            }
            if (puntaje > 24)
            {
                imagenesBuenas.frecuencia = 1.5f;
            }
            if (puntaje > 39)
            {
                imagenesBuenas.frecuencia = 1.3f;
            }
            if (puntaje > 49)
            {
                imagenesBuenas.frecuencia = 0.8f;
            }
            if (puntaje == 60)
            {
                Victoria.Win(puntaje);
            }
        }
        else
        {
            if (puntaje > 5)
            {
                imagenesBuenas.frecuencia = 2.8f;
                imagenesIncorrectas.frecuencia = 3.3f;
            }
            if (puntaje > 9)
            {
                imagenesBuenas.frecuencia = 2.6f;
                imagenesIncorrectas.frecuencia = 3.1f;
            }
            if (puntaje > 19)
            {
                imagenesBuenas.frecuencia = 2.4f;
                imagenesIncorrectas.frecuencia = 2.9f;
            }
            if (puntaje > 29)
            {
                imagenesBuenas.frecuencia = 2f;
                imagenesIncorrectas.frecuencia = 2.5f;
            }
            if (puntaje > 49)
            {
                imagenesBuenas.frecuencia = 1.3f;
                imagenesIncorrectas.frecuencia = 1.8f;
            }
            if (puntaje == 60)
            {
                Victoria.Win(puntaje);
            }
        }
    }
}
