using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour
{
    public Text scoreText;
    public InputField enterName;
    public string marcador2;
   
    void Start()
    {
        int punt = over13_variables.score2;
        scoreText.text = "Score: " + punt;
        int i = 0;//counter variable
        int final = 9;//constant 
        int terminar = 0;//used as boolean (0-1)
        string marcador;//to save the score in the correct key
        string aux;//variables used to keep the correct order in the ranking
        string aux2;
        string aux3;
        string aux4;
        if (punt > PlayerPrefs.GetInt("scoresp9", 0)) {// if the score can be in the ranking, being higher than the lowest saved
            do
            {
                marcador = "scoresp" + i;//to update the keys 
                marcador2 = "namep" + i;
                if (punt < PlayerPrefs.GetInt(marcador, 0)) {
                    i++;//to get the correct position
                }
                else {
                    for(int j = 0; j != (10 - i); j++)
                    {
                        aux = "scoresp" + (final - j);//to reorder the ranking, both name and score
                        aux2= "scoresp" + (final - j - 1);
                        PlayerPrefs.SetInt(aux,(PlayerPrefs.GetInt(aux2, 0)));
                        aux3 = "namep" + (final - j);
                        aux4 = "namep" + (final - j - 1);
                        PlayerPrefs.SetString(aux3, (PlayerPrefs.GetString(aux4, "Desconocido")));
                    }
                    PlayerPrefs.SetInt(marcador, punt);//to save the score in its ranking position
                    terminar = 1;//to get out of the loop
                }
            } while (i<final && terminar==0);//exit loop if the correct position was given or we finished checking the ranking
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey("enter"))//only if we press the correct key
        {
            PlayerPrefs.SetString(marcador2, enterName.text);//we save the name associated to the score obtained
        }
    }
}
