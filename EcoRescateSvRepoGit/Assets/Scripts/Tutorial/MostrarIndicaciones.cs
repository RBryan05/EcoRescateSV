using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarIndicaciones : MonoBehaviour
{
    public GameObject InformacionNivel;
    private SeleccionarEscena seleccionarEscena;
    public GameObject mensajeDeAccionBoton;
    public GameObject btnPausa;

    void Start()
    {
        PonerIndicaciones();
        seleccionarEscena = FindAnyObjectByType<SeleccionarEscena>();
    }

    public void PonerIndicaciones()
    {
        mensajeDeAccionBoton.SetActive(false);
        btnPausa.SetActive(false);
        Time.timeScale = 0;
        InformacionNivel.SetActive(true);
    }

    public void Reanudar()
    {
        btnPausa.SetActive(true);
        InformacionNivel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Salir()
    {
        seleccionarEscena.ElegirEscena(0);
    }
}
