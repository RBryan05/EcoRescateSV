using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausarYReanudar : MonoBehaviour
{
    public GameObject vidaYPuntaje;
    public GameObject Informacion;
    public GameObject btnReiniciar;
    public GameObject MenuConfirmar;
    private AudioSource musicaDeFondo;
    public GameObject mensajeDeAccionBoton;
    private bool Pausado = false;
    private string accionARealizar;
    public Text TextoDeAdvertencia;
    private SeleccionarEscena detenerJuego;
    private CuentaRegresiva cuentaRegresiva;
    private bool PuedePausar;
    void Start()
    {
        cuentaRegresiva = FindAnyObjectByType<CuentaRegresiva>();
        detenerJuego = FindAnyObjectByType<SeleccionarEscena>();
        Pausa();
        btnReiniciar.SetActive(false);
        musicaDeFondo = Camera.main.GetComponent<AudioSource>();
        if (musicaDeFondo != null)
        {
            musicaDeFondo.Pause(); // Pausa la música
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Pausado)
            {
                Pausado = false;
                MostrarCuentaRegresiva();
            }
            else if(!Pausado && PuedePausar)
            {
                Pausado = true;
                Pausa();
            }
        }
    }

    private IEnumerator EsperarParaPausar()
    {
        yield return new WaitForSecondsRealtime(3);
        PuedePausar = true;
    }

    public void Pausa()
    {
        PuedePausar = false;
        Pausado = true;
        mensajeDeAccionBoton.SetActive(false);
        Time.timeScale = 0f;
        vidaYPuntaje.SetActive(false);
        Informacion.SetActive(true);
        btnReiniciar.SetActive(true);
        Pausado = true;
        if (musicaDeFondo != null)
        {
            musicaDeFondo.Pause(); // Pausa la música
        }
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        if (musicaDeFondo != null)
        {
            musicaDeFondo.Play();
        }
        Pausado = false;
    }

    public void PrecionarReiniciar()
    {
        accionARealizar = "Reiniciar";
        AbrirMenuConfirmar();
    }

    public void PrecionarSalir()
    {
        accionARealizar = "Salir";
        AbrirMenuConfirmar();
    }

    public void Confirmar()
    {
        if (accionARealizar == "Reiniciar")
        {
            Reiniciar();
        }
        else if(accionARealizar == "Salir")
        {
            detenerJuego.ElegirEscena(0);
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AbrirMenuConfirmar()
    {
        MenuConfirmar.SetActive(true);
        if (accionARealizar == "Reiniciar")
        {
            TextoDeAdvertencia.text = "¿Deseas reiniciar el nivel?";
        }
        else if (accionARealizar == "Salir")
        {
            TextoDeAdvertencia.text = "¿Deseas salir del nivel?";
        }    
    }
    public void CerrarMenuConfirmar()
    {
        MenuConfirmar.SetActive(false);
    }

    public void MostrarCuentaRegresiva()
    {
        Informacion.SetActive(false);
        vidaYPuntaje.SetActive(true);
        StartCoroutine(EsperarParaPausar());
        StartCoroutine(cuentaRegresiva.IniciarCuentaRegresiva());
    }
}
