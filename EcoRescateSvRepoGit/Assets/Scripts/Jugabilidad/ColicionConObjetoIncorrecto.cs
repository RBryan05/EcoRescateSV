using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColicionCobObjetoIncorrecto : MonoBehaviour
{
    public GameObject basureroPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D choque)
    {
        if (choque.CompareTag("Player"))
        {
            if (basureroPrefab != null)
            {
                if (basureroPrefab.TryGetComponent<MoverBasurero>(out var basurero))
                {
                    basurero.PerderVida();
                }
            }
            Destroy(gameObject);
        }
        if (choque.CompareTag("BasuraCorrecta"))
        {
            Destroy(gameObject);
        }

        if (choque.CompareTag("Tilemap"))
        {
            Destroy(gameObject);
        }
    }
}
