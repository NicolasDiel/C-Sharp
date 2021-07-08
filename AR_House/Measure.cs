using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Measure : MonoBehaviour
{
    [Header("Arrows")]
    public GameObject arrowL;
    public GameObject arrowR;

    [Range(-0.15f, 0.15f)]
    public float arrowScale = 0.15f;

    [Range(0, 90)]
    public float arrowAngle = 0f;
    public Color arrowColor;

    [Header("Text")]
    public Text textField;
    [Range(0, 0.05f)]
    public float textScale = 0.02f;
    public Color textColor;

    [Header("Canvas")]
    public GameObject canvas;

    [Header("Line")]
    public GameObject line;

    private float distance;


    void OnDrawGizmos() //Execute when the game is modified in the editor
    {
        MeasureStuff();
    }
        
    void OnValidate() //Execute when the game is compiled
    {
        MeasureStuff();     
    }

    public void MeasureStuff()
    {
        distance = Vector3.Distance(arrowL.transform.position, arrowR.transform.position)/2;
        textField.text = distance.ToString("N2") + "m";

        canvas.transform.position = LerpByDistance(arrowL.transform.position, arrowR.transform.position, 0.5f);

        if (arrowL != null)
        {
            arrowL.transform.localScale = new Vector3(arrowScale, arrowScale, arrowScale);
            arrowL.transform.localRotation = Quaternion.Euler(arrowAngle, 0, 0);
        }

        if (arrowR != null)
        {
            arrowR.transform.localScale = new Vector3(arrowScale, arrowScale, arrowScale);
            arrowR.transform.localRotation = Quaternion.Euler(arrowAngle, 0, 0);
        }

        if (textField != null)
        {
            textField.transform.localScale = new Vector3(textScale, textScale, 0);
        }

        if (line != null)
        {
            line.transform.localScale = new Vector3(distance/9.63f, line.transform.localScale.y, 0.05f);
            line.transform.position = canvas.transform.position;
        }
    }   

    Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = A + x * (B - A);
        return P;
    }
}
