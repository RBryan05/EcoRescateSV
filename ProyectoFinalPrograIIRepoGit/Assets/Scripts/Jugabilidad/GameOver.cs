using System.Collections;
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

    void Start()
    {
        musicaDeFondo = Camera.main.GetComponent<AudioSource>();
        puntaje = FindObjectOfType<Puntaje>();
    }

    void Update()
    {
        // Actualiza el texto del puntaje final constantemente
        puntajeFinal.text = puntos.ToString("0");
    }

    // M�todo para finalizar el juego y mostrar el men� de Game Over
    public void JuegoTerminado()
    {
        Time.timeScale = 0;
        musicaDeFondo.Pause();
        gameOverPrefab.SetActive(true);
    }

    public void PuntajeFinal(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
