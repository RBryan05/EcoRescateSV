using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private float puntos;
    public GameObject gameOverPrefab;
    private AudioSource musicaDeFondo;
    public TextMeshProUGUI puntajeFinal;
    private Puntaje puntaje;

    private MostrarMenufelicidades mostrarMenuFelicidades;

    void Start()
    {
        musicaDeFondo = Camera.main.GetComponent<AudioSource>();
        puntaje = FindObjectOfType<Puntaje>();
        mostrarMenuFelicidades = FindObjectOfType<MostrarMenufelicidades>();
    }

    void Update()
    {
        // Actualiza el texto del puntaje final constantemente
        puntajeFinal.text = puntos.ToString("0");
    }

    // Método para finalizar el juego y mostrar el menú de Game Over
    public void JuegoTerminado()
    {
        Time.timeScale = 0;
        musicaDeFondo.Pause();
        gameOverPrefab.SetActive(true);

        mostrarMenuFelicidades.JuegoTerminado();

    }

    public void PuntajeFinal(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
