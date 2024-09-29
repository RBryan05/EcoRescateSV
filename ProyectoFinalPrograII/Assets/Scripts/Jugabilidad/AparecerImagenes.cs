using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerImagenes : MonoBehaviour
{
    public GameObject[] goodObjects; // Array de prefabs de imágenes buenas
    public float frecuencia = 1.5f;   // Frecuencia de aparición de las imágenes buenas
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
            SpawnGoodImage();
            nextSpawnTime = Time.time + frecuencia;
        }
    }

    void SpawnGoodImage()
    {
        // Generar una posición aleatoria en el eje X
        float spawnX = Random.Range(minX, maxX);

        // Seleccionar un prefab aleatorio del array goodObjects
        int randomIndex = Random.Range(0, goodObjects.Length);
        GameObject newObject = Instantiate(goodObjects[randomIndex], new Vector3(spawnX, transform.position.y, 0), Quaternion.identity);

        // Asignar una velocidad descendente al objeto
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -3f); // Velocidad de caída de la imagen
        }
    }
}
