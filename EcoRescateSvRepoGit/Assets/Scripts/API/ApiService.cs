using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ApiService : MonoBehaviour
{
    public static ApiService Instance { get; private set; }

    private string url = "http://127.0.0.1:8000/api/jugadores/"; // Reemplaza con la URL de la API

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

    public void GuardarJugador(string nombre, string puntaje, string modoDeJuego, Action<bool> callback)
    {
        StartCoroutine(EnviarDatos(nombre, puntaje, modoDeJuego, callback));
    }

    private IEnumerator EnviarDatos(string nombre, string puntaje, string modoDeJuego, Action<bool> callback)
    {
        string jsonData = "{\"Nombre\":\"" + nombre + "\", \"Puntaje\":\"" + puntaje + "\", \"ModoDeJuego\":\"" + modoDeJuego + "\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Jugador guardado correctamente: " + request.downloadHandler.text);
            callback(true);
        }
        else
        {
            Debug.LogError("Error al guardar jugador: " + request.error);
            callback(false);
        }
    }
}

// Clases de datos
[System.Serializable]
public class Jugador
{
    public int id;
    public string Nombre;
    public string Puntaje;
    public string ModoDeJuego;
}

[System.Serializable]
public class JugadorContainer
{
    public Jugador[] jugadores;
}
