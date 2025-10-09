using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBola : MonoBehaviour
{
    public float fuerzaDeLanzamiento = 1000f;
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;

    private Rigidbody rb;
    private bool haSidoLanzada = false;

    public CameraFollow cameraFollow;
    public ScoreManager scoreManager;

    void Start()
    {
        // Obtenemos el Rigidbody para aplicar fuerza
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!haSidoLanzada)
        {
            Apuntar();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LanzarBola();
            }
        }
    }

    void Apuntar()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);

        Vector3 posicionActual = transform.position;
        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);
        transform.position = posicionActual;
    }

    void LanzarBola()
    {
        haSidoLanzada = true;

        if (rb != null)
        {
            rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
        }
        else
        {
            Debug.LogWarning("¡El Rigidbody no está asignado en " + gameObject.name);
        }

        if (cameraFollow != null)
        {
            cameraFollow.IniciarSeguimiento();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            if (cameraFollow != null)
            {
                cameraFollow.DetenerSeguimiento();
            }

            if (scoreManager != null)
            {
                Invoke("CalcularPuntaje", 2f);
            }
        }
    }

    void CalcularPuntaje()
    {
        if (scoreManager != null)
        {
            scoreManager.CalcularPuntaje();
        }
    }
}