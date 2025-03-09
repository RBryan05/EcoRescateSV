using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ApiManager : MonoBehaviour
{
    public GameObject ProblemasDeConexion;

    public TextMeshProUGUI Nombre1;
    public TextMeshProUGUI Nombre2;
    public TextMeshProUGUI Nombre3;

    public TextMeshProUGUI Puntaje1;
    public TextMeshProUGUI Puntaje2;
    public TextMeshProUGUI Puntaje3;

    void Start()
    {
        try
        {
            if (ApiService.Instance != null)
            {
                // Suscribirse al evento para actualizar el ranking cuando la API cambie
                ApiService.Instance.OnDatosActualizados += UpdateLeaderboard;

                // Si ya hay datos cargados, actualizar inmediatamente
                if (ApiService.Instance.datosCargados)
                {
                    UpdateLeaderboard(ApiService.Instance.ObtenerJugadores());
                }
            }
            else
            {
                Debug.LogError("ApiService NO encontrado en la escena.");
            }
        }
        catch
        {
            ProblemasDeConexion?.SetActive(true);
        }
    }

    // Función para actualizar el leaderboard
    void UpdateLeaderboard(List<Jugador> jugadores)
    {
        if (jugadores == null || jugadores.Count == 0)
        {
            ProblemasDeConexion?.SetActive(true);
            return;
        }

        var topJugadores = jugadores
            .OrderByDescending(j => int.Parse(j.Puntaje))
            .Take(3)
            .ToList();

        if (topJugadores.Count > 0)
        {
            Nombre1.text = topJugadores[0].Nombre;
            Puntaje1.text = topJugadores[0].Puntaje;
        }
        if (topJugadores.Count > 1)
        {
            Nombre2.text = topJugadores[1].Nombre;
            Puntaje2.text = topJugadores[1].Puntaje;
        }
        if (topJugadores.Count > 2)
        {
            Nombre3.text = topJugadores[2].Nombre;
            Puntaje3.text = topJugadores[2].Puntaje;
        }
    }

    // Función que se llama cuando el usuario quiere intentar reconectar
    public void IntentarReconectar()
    {
        StartCoroutine(AttemptToLoadData());
    }

    // Función que intenta cargar los datos después de un pequeño retraso
    private IEnumerator AttemptToLoadData()
    {
        // Esperar un momento antes de comprobar los datos de la API
        yield return new WaitForSeconds(1f);

        // Comprobar si los datos ya están cargados
        if (ApiService.Instance.datosCargados)
        {
            UpdateLeaderboard(ApiService.Instance.ObtenerJugadores());
        }
        else
        {
            Debug.LogWarning("No se pudo cargar datos desde la API.");
            ProblemasDeConexion?.SetActive(true); // Mostrar mensaje de conexión
        }
    }

    // Nueva función para intentar restablecer la conexión con la API
    public void RestablecerConexion()
    {
        StartCoroutine(RestablecerConexionCoroutine());
    }

    // Corutina que intenta restablecer la conexión y cargar los datos
    private IEnumerator RestablecerConexionCoroutine()
    {
        // Mostrar un mensaje de intento de reconexión
        Debug.Log("Intentando restablecer la conexión...");

        // Esperar un poco antes de intentar de nuevo (puedes ajustar este tiempo)
        yield return new WaitForSeconds(1f);

        // Intentar obtener los datos de la API nuevamente
        if (ApiService.Instance != null)
        {
            // Intentar cargar los datos desde la API
            yield return StartCoroutine(ApiService.Instance.GetJugadores());

            // Verificar si los datos se han cargado correctamente
            if (ApiService.Instance.datosCargados)
            {
                UpdateLeaderboard(ApiService.Instance.ObtenerJugadores());
                ProblemasDeConexion?.SetActive(false); // Ocultar mensaje de error si todo salió bien
            }
            else
            {
                Debug.LogWarning("No se pudieron cargar los datos desde la API.");
                ProblemasDeConexion?.SetActive(true); // Mostrar mensaje de error
            }
        }
        else
        {
            Debug.LogError("ApiService no disponible. No se puede intentar la reconexión.");
            ProblemasDeConexion?.SetActive(true); // Mostrar mensaje de error
        }
    }
}
