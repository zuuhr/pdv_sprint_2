using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    public Text ranking;//Object used for showing what we want in the canvas
    string marcador;//Key used to access to the names saved in playerprefs
    string marcador2;//Key used to access to the scores saved in playerprefs
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            marcador = "namep" + i;//to update the first key for accesing to another element of the playerprefs
            marcador2 = "scoresp" + i;//to update the second key
            ranking.text += "\n" + PlayerPrefs.GetString(marcador, "Desconocido");//we show in the canvas the name associated to the key
            ranking.text += ": " + PlayerPrefs.GetInt(marcador2, 0);//as playerprefs can't work with different types simmultaneously, here we show the score associated to the key
        }
        
    }
}
