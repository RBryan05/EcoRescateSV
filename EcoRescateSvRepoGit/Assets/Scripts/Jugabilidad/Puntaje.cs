using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Puntaje : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;
    private AparecerImagenes imagenesBuenas;
    private AparecerImagenesIncorrectas imagenesIncorrectas;
    private AudioSource sonidoPunto;
    public string TipoNivel;
    private Victoria Victoria;

    bool yaGano = false;

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

    public float GetPuntos()
    {
        return puntos;
    }

    private void AumentarDificultad(float puntaje)
    {       
        if (TipoNivel == "Todos")
        {
            if (puntaje <= 5)
            {
                imagenesBuenas.frecuencia = 1.8f;
            }
            if (puntaje > 5 && puntaje <= 12)
            {
                imagenesBuenas.frecuencia = 1.5f;
            }
            if (puntaje > 12 && puntaje <= 19)
            {
                imagenesBuenas.frecuencia = 1.3f;
            }
            if (puntaje > 19)
            {
                imagenesBuenas.frecuencia = 0.8f;
            }
            if (puntaje == 20)
            {
                Victoria.Win(puntaje);
            }
        }
        else
        {
            if (puntaje <= 5)
            {
                imagenesBuenas.frecuencia = 2.8f;
                imagenesIncorrectas.frecuencia = 3.3f;
            }
            if (puntaje > 5 && puntaje <= 12)
            {
                imagenesBuenas.frecuencia = 2.6f;
                imagenesIncorrectas.frecuencia = 3.1f;
            }
            if (puntaje > 12 && puntaje <= 19)
            {
                imagenesBuenas.frecuencia = 2.4f;
                imagenesIncorrectas.frecuencia = 2.9f;
            }
            if (puntaje > 19)
            {
                imagenesBuenas.frecuencia = 2f;
                imagenesIncorrectas.frecuencia = 2.5f;
            }
            if (puntaje == 1 && !yaGano)
            {
                Victoria.Win(puntaje);
                Debug.Log(puntaje);
                yaGano = true;
            }
            if (puntaje > 1)
            {
                yaGano = false;
            }
        }
    }
}
