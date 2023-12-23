using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Skript pre prefab QuizListItem, ktory je pouzivany v scene Quizzes
public class QuizItem : MonoBehaviour
{
    string text;


    // Start is called before the first frame update
    void Start()
    {

        // Získaj text tlačidla
        text = this.GetComponentInChildren<TMP_Text>().text;
        Debug.Log("The prefab has text: " + text);

    }


    // Nacitavanie kvizu
    public void LoadQuizScene() {

        // Ulozi sa zvoleny kviz do PlayerPrefs, a nacita sa scena pre kviz, ktora podla tejto hodnoty zobrazi zvoleny kviz [11]
        PlayerPrefs.SetString("selected_quiz", text);
        PlayerPrefs.Save();
        Debug.Log("Starting quiz: " + text);

        SceneManager.LoadScene("QuizScene");

    }

}
