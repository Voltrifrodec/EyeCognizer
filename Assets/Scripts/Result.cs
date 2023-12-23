using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Skript sceny pre zobrazenie vysledkov kvizu 
public class Result : MonoBehaviour
{

    /// Premenne
    public TMP_Text resultText;         // TextMeshPro objekt pre zobrazenie vysledku
    int score = 0;                      // Skore, ktore pouzivatel ziskal z kvizu
    int max_score = 0;                  // Maximalne skore kvizu
    double percentual_score = 0.0;      // Percentualna uspesnost pouzivatela


    // Start is called before the first frame update
    void Start()
    {

        // Ziskanie nahraneho skore a maximalneho skore z PlayerPrefs. V aplikacii mame zaistene ze pri nacitani tejto sceny tieto premenne existuju, takze nie je potrebne kontrolovat existenciu
        score = PlayerPrefs.GetInt("score");
        max_score = PlayerPrefs.GetInt("max_score");

        // Vypocitanie percentualnej uspesnosti -- v pripade nuloveho skore alebo ziadnych hodnot sa nastavi 0
        percentual_score = ((double)score / max_score * 100);
        if(Double.IsNaN(percentual_score)) {
            percentual_score = 0;
        }

        // Nastavenie vysledneho textu do TextMeshPro
        resultText.text = $"You have scored {score}/{max_score} ({(percentual_score == 0 ? "0.00" : percentual_score.ToString("F2"))}%).";
    
    }

}
