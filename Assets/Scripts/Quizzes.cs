using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Skript pre scenu so zoznamom kvizov dostupnych kvizov.
public class Quizzes : MonoBehaviour
{
    // TODO:
    //*     1. Počítanie súborov v Assets/Scripts/Quizzes
    //*     2. Vytvorenie objektov podľa počtu nájdených súborov (kvízov)
    //*     3. Ukladanie zvoleného kvízu do PlayerPrefs, dynamické spúšťatie kvízu v Quiz.cs 

    // Premenne
    List<string> quizNames = new List<string>();                // Ukladanie nazvov kvizov (nazvy suborov)
    public TMP_Text quizzesCount;                               // TextMeshPro objekt pre celkovy pocet najdenych kvizov (suborov)
    int quizzesCountValue;                                      // Celkovy pocet najdenych kvizov (suborov)
    public GameObject objectToSpawn;                            // Aky objekt bude reprezentovat kviz v zozname (v nasom pripade vkladame prefab QuizListItem)
    Vector3 firstPosition = new Vector3(210.0f, 615.0f, 0.0f);  // Pozicia, od ktorej sa budu generovat objekty do zoznamu


    // Start is called before the first frame update
    void Start()
    {
        // Načítanie nájdených kvízov (názov a celkový počet)
        quizzesCountValue = GetQuizzesFromDirectory();
        quizzesCount.text = $"Found quizzes: {quizzesCountValue}";

        // Najdenie GameObject wrappera podla znacky
        GameObject parent = GameObject.FindGameObjectWithTag("quizzes");

        // Vytvorenie nových GameObject objektov podľa prefabu zo vstupu [12]
        Vector3 position = firstPosition;
        if(objectToSpawn != null) {

            for(int i = 0; i < quizzesCountValue; i++) {

                // Vygeneruje sa novy objekt, vlozi sa do neho nazov suboru, a prida sa do zoznamu
                GameObject generatedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
                generatedObject.transform.SetParent(parent.transform);
                print("Setting name: " + quizNames[i]);
                generatedObject.GetComponentInChildren<TextMeshProUGUI>().text = quizNames[i];
                position.x += 250.0f;

            }

        }

    }


    // Vyhladanie suborov v adresari [13-17]
    private int GetQuizzesFromDirectory() {

        // Kazdy subor v default adresari 'Quizzes' sa skontroluje ci ma koncovku .json, a prida sa do listu
        // Aplikacia a spustany nekompilovany projekt nemaju ten isty priecinok pre Assets a nie vzdy obsahuju tie iste subory, preto je potrebne osetrit cestu k priecinku s kvizmi
        int i = 0;
        FileInfo[] fis = new DirectoryInfo(Path.Combine((Application.platform == RuntimePlatform.WindowsPlayer ? Application.streamingAssetsPath : Application.dataPath), "Quizzes")).GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("json")) {
                quizNames.Add(Path.GetFileNameWithoutExtension(fi.FullName));
                i++;
            }
        }

        print("Found quizzes: " + i);
        return i;

    }

}
