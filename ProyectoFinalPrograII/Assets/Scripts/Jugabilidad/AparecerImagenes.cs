using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerImagenes : MonoBehaviour
{
    public GameObject[] imagenesCorrectas; // Array de prefabs de im�genes buenas
    public float frecuencia = 1.5f;   // Frecuencia de aparici�n de las im�genes buenas
    public float minX, maxX;         // L�mites en el eje X para la aparici�n

    private float siguienteAparicion;

    // Update is called once per frame
    void Update()
    {
        // Generar im�genes a intervalos de tiempo
        if (Time.time > siguienteAparicion)
        {
            SpawnGoodImage();
            siguienteAparicion = Time.time + frecuencia;
        }
    }

    void SpawnGoodImage()
    {
        // Generar una posici�n aleatoria en el eje X
        float spawnX = Random.Range(minX, maxX);

        // Seleccionar un prefab aleatorio del array goodObjects
        int randomIndex = Random.Range(0, imagenesCorrectas.Length);
        GameObject newObject = Instantiate(imagenesCorrectas[randomIndex], new Vector3(spawnX, transform.position.y, 0), Quaternion.identity);
    }
}
