using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private float puntos;
    public GameObject gameOverPrefab;
    public TextMeshProUGUI puntajeFinal;
    private Puntaje puntaje;

    void Start()
    {
        puntaje = FindObjectOfType<Puntaje>();
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
        gameOverPrefab.SetActive(true);
    }

    public void PuntajeFinal(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
