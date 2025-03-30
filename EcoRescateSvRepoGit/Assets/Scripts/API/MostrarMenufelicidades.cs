using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMenufelicidades : MonoBehaviour
{
    private float puntos;
    public GameObject btnConfirmarEnviarPuntaje;
    public TMP_InputField NombreJugador;

    private ApiService apiService;
    public GameObject Felicitaciones;

    public Text PuntajeAGuardar;
    public Text GameMode;
    public string ModoDeJuego;

    public GameObject Exito;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la instancia de ApiService
        apiService = ApiService.Instance;
        if (apiService == null)
        {
            Debug.LogError("ApiService no encontrado. Asegúrate de que ApiService está en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el campo de texto no está vacío
        if (!string.IsNullOrEmpty(NombreJugador.text))
        {
            if (!btnConfirmarEnviarPuntaje.activeSelf)
            {
                btnConfirmarEnviarPuntaje.SetActive(true);
            }
        }
        else
        {
            if (btnConfirmarEnviarPuntaje.activeSelf)
            {
                btnConfirmarEnviarPuntaje.SetActive(false);
            }
        }

        PuntajeAGuardar.text = puntos.ToString("0");
        GameMode.text = ModoDeJuego;
    }

    public void MostrarMenuSubirPuntaje()
    {
        List<Jugador> jugadores = apiService.ObtenerJugadores();

        var topJugadores = jugadores
            .OrderByDescending(j => int.Parse(j.Puntaje))
            .Take(3)
            .ToList();

        int puntajeFinalInt = int.Parse(PuntajeAGuardar.text);
        int puntajeTop1 = int.Parse(topJugadores[0].Puntaje);
        int puntajeTop2 = int.Parse(topJugadores[1].Puntaje);
        int puntajeTop3 = int.Parse(topJugadores[2].Puntaje);

        Debug.Log(topJugadores);

        // Comparar si el puntaje final es mayor que alguno de los 3 primeros
        if (puntajeFinalInt > puntajeTop1 || puntajeFinalInt > puntajeTop2 || puntajeFinalInt > puntajeTop3)
        {
            Felicitaciones.SetActive(true);
        }
    }

    public void OcultarMenusubirPuntaje()
    {
        Felicitaciones.SetActive(false);
    }

    public void GuardarPuntaje()
    {
        Debug.Log("¡Guardando puntaje!");
        string nombre = NombreJugador.text;
        string puntaje = PuntajeAGuardar.text;
        string modo = GameMode.text;

        Debug.Log("Guardando jugador: " + nombre + " - " + puntaje + " - " + modo);

        apiService.GuardarJugador(nombre, puntaje, modo, (exito) =>
        {
            if (exito)
            {
                Debug.Log("Jugador guardado exitosamente.");
                OcultarMenusubirPuntaje();
                Exito.SetActive(true);
            }
            else
                Debug.LogError("Error al guardar el jugador.");
        });
    }

    public void PuntajeFinalAGuardar(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }

    // Llamar a esta función cuando termine el juego
    public void JuegoTerminado()
    {
        MostrarMenuSubirPuntaje();
    }
}
