using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    private float puntos;
    public GameObject victoriaPrefab;
    private AudioSource musicaDeFondo;
    public TextMeshProUGUI puntajeFinal;
    private Puntaje puntaje;
    // Start is called before the first frame update
    void Start()
    {
        musicaDeFondo = Camera.main.GetComponent<AudioSource>();
        puntaje = FindObjectOfType<Puntaje>();
    }

    // Update is called once per frame
    void Update()
    {
        // Actualiza el texto del puntaje final constantemente
        puntajeFinal.text = puntos.ToString("0");
    }

    public void Win(float puntajeFinal)
    {
        Time.timeScale = 0;
        musicaDeFondo.Pause();
        victoriaPrefab.SetActive(true);
        puntos = puntajeFinal;
    }
    public void PuntajeFinal(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
