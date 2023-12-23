using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Skript pre scenu kvizu. Obsahuje hlavne nastavenie kvizu
public class Quiz : MonoBehaviour
{

    // Premenne
    public TMP_Text titleText;              // Objekt, do ktoreho sa uklada nazov kvizu
    public TMP_Text questionText;           // Objekt pre text otazky
    public TMP_Text progressText;           // Objekt pre zobrazenie stavu kvizu (na ktorej otazke sme z kolkych)
    public TMP_Text scoreText;              // Objekt pre text skore

    private int currentIndex = 0;           // Index aktualnej otazky
    List<GameObject> optionsList;           // List, ktory obsahuje objekty moznosti
    public QuestionList content;            // Premenna pre ulozenie a pracu s najdenymi otazkami
    public int playerScore = 0;             // Skore pouzivatela


    // Start is called before the first frame update
    void Start()
    {

        // Inicializacia listu moznosti
        optionsList = new List<GameObject>();

        // Nastavenie / Reset aktualneho a maximalneho skore v PlayerPrefs
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("max_score", 0);
        PlayerPrefs.Save();

        // Načítanie default kvízu z JSON súboru
        string quizName = PlayerPrefs.GetString("selected_quiz");
        
        // Ak nastane chyba alebo sa nikde nenachadza dany subor, otvori sa ukazkovy test
        if(quizName == null) {

            quizName = "example-test.json";

        }

        // Nastavenie nazvu sceny
        titleText.text = $"QUIZ: {quizName}";

        // Najdenie kvizov [6]
        // Aplikacia a spustany nekompilovany projekt nemaju ten isty priecinok pre Assets a nie vzdy obsahuju tie iste subory, preto je potrebne osetrit cestu k priecinku s kvizmi
        string quizPath = Path.Combine((Application.platform == RuntimePlatform.WindowsPlayer ? Application.streamingAssetsPath : Application.dataPath), $"Quizzes/{quizName}.json");
        string json = System.IO.File.ReadAllText(quizPath); // Precitanie / "Otvorenie" suboru
        content = JsonUtility.FromJson<QuestionList>(json);

        // Nastavenie textu pre skore a text aktualnej otazky
        progressText.text = $"1/{content.questions.Count}";
        scoreText.text = "0";

        // Ak existuju otazky v kvize, tak sa zobrazi prva otazka a jej moznosti, inak sa automaticky presunie aplikacia na ResultScene
        if(content.questions != null) {

            Debug.Log("Found questions. Amount: " + content.questions.Count);

            //? Workflow:
            // 1. Pri spustení scény sa načíta prvá otázka (index = 0)
            // 2. Akonáhle sa otázka zodpovie, tak sa index zvýši o 1. Kontroluje sa, či sa index rovná celkovému počtu otázok.
            // 3. Ak sa index == questions.Length, tak vymaž objekt s otázkou a objekt s možnosťami, zmeň text na 'Result' a zobraz skóre spolu s tlačidlom do hlavného menu (neskôr tlačidlo na scénu s levelami).
            // 4. Ak sa index != questions.Length, tak zmeň text otázky a zmeň možnosti.
 
            Debug.Log(content.questions[0]);
            DisplayQuestion(content.questions[0]);

        }
        else {

            Debug.Log("The quiz does not contain any questions! Skipping to result scene...");
            PlayerPrefs.SetInt("score", playerScore);
            PlayerPrefs.SetInt("max_score", content.questions.Count);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ResultScene");

        }

    }


    // Update is called once per frame [7-8]
    void Update()
    {

        // https://stackoverflow.com/a/44542726/11557114
        // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
        if(currentIndex == content.questions.Count) {

            PlayerPrefs.SetInt("score", playerScore);
            PlayerPrefs.SetInt("max_score", content.questions.Count);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ResultScene");

        }

    }


    // Metoda pre zobrazenie otazky v scene
    void DisplayQuestion(Question question) {

        // Najde sa objekt, do ktoreho sa generuju objekty pre moznosti (kontajner)
        GameObject optionsSection = GameObject.Find("QuizOptionsSection");

        // Resetovanie pozicie v pripade ze pocet moznosti vacsi ako 2
        GameObject.Find("QuizOptionSection3").GetComponent<RectTransform>().transform.localPosition = new Vector3(500.0f, 0.0f, 0.0f);

        // Ak je otazka z nejakeho dovodu null, tak sa metoda ukonci
        if(question == null) {

            Debug.Log("Question was not found.");
            return;

        }


        // Nastavenie TextMeshPro objektov (progress text, text pre skore, text otazky), a resetovanie listu moznosti
        progressText.text = $"{currentIndex + 1}/{content.questions.Count}";
        scoreText.text = $"Score: {playerScore}";
        questionText.text = question.getQuestion();
        optionsList.Clear(); // Vymaže predošlé možnosti

        // Vytvorenie premenej do ktorej si ulozim poziciu hlavneho kontajnera s moznostami
        Vector3 vector = optionsSection.transform.localPosition;
        // Ak je pocet otazok mensi ako 3, tak sa presunie tretia moznost mimo obrazovku (z nejakeho dovodu SetActive(bool) nechcel fungovat, preto je to riesene takymto sposobom) [9]
        // Inak sa nastavi klasicka velkost pre tri moznosti
        if(question.getOptions().Length < 3) {

            GameObject.Find("QuizOptionSection3").GetComponent<RectTransform>().transform.localPosition = new Vector3(1000.0f, 0.0f, 0.0f);
            optionsSection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1000);
            vector.x = 250.0f;
            optionsSection.transform.localPosition = vector;

        } else {

            optionsSection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1500);
            vector.x = 0.0f;
            optionsSection.transform.localPosition = vector;

        }

        // Nastavenie hodnot pomocou for loop [10]
        for(int i = 0; i < question.getOptions().Length; i++) {

            GameObject quizOptionSection = GameObject.Find("QuizOptionSection" + (i + 1));

            Transform textTransform = quizOptionSection.transform.Find("QuizOption" + (i + 1) + "Text");
            TextMeshProUGUI optionText = textTransform.GetComponent<TextMeshProUGUI>();
            optionText.text = question.getOptions()[i];

        }


    }


    // Metoda pre kontrolu odpovede
    public void ValidateOption(int selfIndex) {

        // Debug.Log($"{selfIndex}, {content.questions[currentIndex].getCorrectOption()}, {currentIndex}");

        // Ak je odpoved spravna, pripocita sa skore
        if(content.questions[currentIndex].getCorrectOption() == selfIndex) {

            playerScore++;

        }

        // Ak este ostavaju otazky (index nie je rovny celkovemu poctu otazok), tak sa zobrazi dalsie otazka v poradi
        currentIndex++;
        if(currentIndex != content.questions.Count) {

            DisplayQuestion(content.questions[currentIndex]);

        }

    }

}
