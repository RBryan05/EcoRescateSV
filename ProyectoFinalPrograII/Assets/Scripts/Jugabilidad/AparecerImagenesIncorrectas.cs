using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerImagenesIncorrectas : MonoBehaviour
{
    public GameObject[] BasuraMala; // Array de prefabs de im�genes incorrectas
    public float frecuencia = 2.0f;   // Frecuencia de aparici�n de las im�genes
    public float minX, maxX;         // L�mites en el eje X para la aparici�n

    private float siguienteAparicion;

    // Update is called once per frame
    void Update()
    {
        // Generar im�genes a intervalos de tiempo
        if (Time.time > siguienteAparicion)
        {
            AparecerImagenesMalas();
            siguienteAparicion = Time.time + frecuencia;
        }
    }

    void AparecerImagenesMalas()
    {
        // Generar una posici�n aleatoria en el eje X
        float spawnX = Random.Range(minX, maxX);

        // Seleccionar un prefab aleatorio del array goodObjects
        int randomIndex = Random.Range(0, BasuraMala.Length);
        GameObject newObject = Instantiate(BasuraMala[randomIndex], new Vector3(spawnX, transform.position.y, 0), Quaternion.identity);
    }
}
