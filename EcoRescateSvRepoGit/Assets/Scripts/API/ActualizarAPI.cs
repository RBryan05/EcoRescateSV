using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para trabajar con los botones

public class ApiUpdater : MonoBehaviour
{
    private ApiService apiService;

    void Start()
    {
        // Obtener la instancia de ApiService
        apiService = ApiService.Instance;

        if (apiService == null)
        {
            Debug.LogError("ApiService no encontrado. Asegúrate de que ApiService está en la escena.");
        }
    }

    // Función que actualiza la API cuando se llama
    public void ActualizarApi()
    {
        if (apiService != null)
        {
            Debug.Log("Actualizando la API...");
            StartCoroutine(apiService.GetJugadores()); // Llamar a la API para actualizar los jugadores
        }
        else
        {
            Debug.LogError("ApiService no está disponible.");
        }
    }
}
