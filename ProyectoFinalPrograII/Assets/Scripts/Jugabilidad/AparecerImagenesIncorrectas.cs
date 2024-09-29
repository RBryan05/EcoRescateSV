using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerImagenesIncorrectas : MonoBehaviour
{
    public GameObject[] BasuraMala; // Array de prefabs de imágenes incorrectas
    public float frecuencia = 2.0f;   // Frecuencia de aparición de las imágenes
    public float minX, maxX;         // Límites en el eje X para la aparición

    private float nextSpawnTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Generar imágenes a intervalos de tiempo
        if (Time.time > nextSpawnTime)
        {
            AparecerImagenesMalas();
            nextSpawnTime = Time.time + frecuencia;
        }
    }

    void AparecerImagenesMalas()
    {
        // Generar una posición aleatoria en el eje X
        float spawnX = Random.Range(minX, maxX);

        // Seleccionar un prefab aleatorio del array goodObjects
        int randomIndex = Random.Range(0, BasuraMala.Length);
        GameObject newObject = Instantiate(BasuraMala[randomIndex], new Vector3(spawnX, transform.position.y, 0), Quaternion.identity);

        // Asignar una velocidad descendente al objeto
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -3f); // Velocidad de caída de la imagen
        }
    }
}
