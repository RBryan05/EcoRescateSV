using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausarYReanudar : MonoBehaviour
{
    public GameObject Informacion;
    public GameObject btnReiniciar;
    public GameObject MenuConfirmar;
    public GameObject MenuConfirmarSeguirJugando;
    public GameObject btnSeguirJugando;
    private AudioSource musicaDeFondo;
    public GameObject mensajeDeAccionBoton;
    private string accionARealizar;
    public Text TextoDeAdvertencia;
    private SeleccionarEscena detenerJuego;
    private CuentaRegresiva cuentaRegresiva;
    public GameObject PantallaFelicidades;
    public GameObject PantallaExitoAlSubirRegistro;

    private Victoria victoria;
    private Puntaje puntaje;
    void Start()
    {
        puntaje = FindObjectOfType<Puntaje>();
        victoria = FindObjectOfType<Victoria>();
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

    private IEnumerator EsperarParaPausar()
    {
        yield return new WaitForSecondsRealtime(3);
    }

    public void Pausa()
    {
        mensajeDeAccionBoton.SetActive(false);
        Time.timeScale = 0f;
        Informacion.SetActive(true);
        btnReiniciar.SetActive(true);
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

    public void cerrarPantallaExito()
    {
        PantallaExitoAlSubirRegistro.SetActive(false);
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
        StartCoroutine(EsperarParaPausar());
        Informacion.SetActive(false);      
        StartCoroutine(cuentaRegresiva.IniciarCuentaRegresiva());
    }

    public void MostrarMenuConfirmarSeguirJugando()
    {
        if (MenuConfirmarSeguirJugando != null)
        {
            MenuConfirmarSeguirJugando.SetActive(true);   
        }
        victoria.OcultarPantallaVictoria();
    }

    public void CerrarMenuConfirmarSeguirJugando()
    {
        MenuConfirmarSeguirJugando.SetActive(false);
        victoria.Win(puntaje.GetPuntos());
    }

    public void SeguirJugando()
    {
        MostrarCuentaRegresiva();
        MenuConfirmarSeguirJugando.SetActive(false);
    }

    public void CerrarPantallaFelicidades()
    {
        PantallaFelicidades.SetActive(false);
    }
}
