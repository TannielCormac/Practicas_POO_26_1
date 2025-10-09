using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textoPuntaje;
    private int puntajeActual = 0;
    private Pin[] pinos;

    void Start()
    {
        pinos = FindObjectsOfType<Pin>();
    }

    public void CalcularPuntaje()
    {
        int puntaje = 0;

        foreach (Pin pin in pinos)
        {
            if (pin.EstaCaido())
            {
                puntaje++;
            }
        }

        puntajeActual = puntaje;

        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeActual;
        }
    }
}
