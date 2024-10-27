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
    private AudioSource musicaDeFondo;
    public GameObject mensajeDeAccionBoton;
    private bool Pausado;
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Pausado == false && PuedePausar == true)
            {
                Pausa();
            }
            else if (PuedePausar == false && Pausado == true)
            {
                MostrarCuentaRegresiva();
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
        Pausado = false;
        StartCoroutine(EsperarParaPausar());
        Informacion.SetActive(false);      
        StartCoroutine(cuentaRegresiva.IniciarCuentaRegresiva());
    }
}
