using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ApiService : MonoBehaviour
{
    public static ApiService Instance { get; private set; }

    private string url = "https://apiavanzo.onrender.com/api/jugadores/"; // Reemplaza con la URL de la API

    public List<Jugador> jugadoresActuales = new List<Jugador>();
    public bool datosCargados = false;

    // Evento que notifica cuando los datos han sido actualizados
    public event Action<List<Jugador>> OnDatosActualizados;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener en todas las escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    void Start()
    {
        StartCoroutine(GetJugadores());
    }

    public IEnumerator GetJugadores()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                jsonResponse = "{\"jugadores\":" + jsonResponse + "}";

                JugadorContainer container = JsonUtility.FromJson<JugadorContainer>(jsonResponse);
                jugadoresActuales = new List<Jugador>(container.jugadores);
                datosCargados = true;

                // Notificar a los suscriptores que los datos se han actualizado
                OnDatosActualizados?.Invoke(jugadoresActuales);

                Debug.Log("Datos actualizados desde la API.");
            }
            else
            {
                // Si no se pudo obtener los datos, se logea un error y se retorna una lista vacía
                Debug.LogError("Error al obtener datos de la API: " + request.error);

                // Retornamos una lista vacía en caso de error
                jugadoresActuales = null;

                // Notificar a los suscriptores que no hay datos disponibles
                OnDatosActualizados?.Invoke(null);

                // Opcional: Mostrar algún mensaje de error o advertencia en la interfaz de usuario.
            }
        }
    }

    public List<Jugador> ObtenerJugadores()
    {
        return jugadoresActuales ?? new List<Jugador>(); // Retorna lista vacía si jugadoresActuales es null
    }

    // Nueva función para forzar una actualización manual
    public void ForzarActualización()
    {
        StartCoroutine(GetJugadores());
    }
}

// Clases de datos
[System.Serializable]
public class Jugador
{
    public int id;
    public string Nombre;
    public string Puntaje;
}

[System.Serializable]
public class JugadorContainer
{
    public Jugador[] jugadores;
}
