using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Skript pre hlavne metody aplikacie (zmena scien, ukoncenie aplikacie) a metod ktore sa opakuju v inych suboroch 
public class ApplicationController : MonoBehaviour
{

    // Premenna pre text verzie aplikacie
    public string version;


    // Start metodu pouzivame len pre nastavenie textu verzie aplikacie
    void Start() {

        this.SetVersionText(version);

    }


    // Update is called once per frame
    void Update()
    {

        // Ak pouzivatel stlaci klavesu Esc, tak sa aplikacia ukonci
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

    }


    // Metoda pre zmenu sceny podla nazvu
    public void ChangeScene(string sceneName) {

        // Ak nie je sceneName prazdna, tak sa nevykona
        if(sceneName == null || sceneName == "") {

            return;

        }

        // Ak scena neexistuje, tak sa nevykona nic [1]
        if(Application.CanStreamedLevelBeLoaded(sceneName) == false) {

            return;

        }

        SceneManager.LoadScene(sceneName);

    }


    // Metoda pre ukoncenie aplikacie
    public void ExitApplication() {

        Application.Quit();

    }


    // Metoda pre nastavenie textu pre verziu
    public void SetVersionText(string version) {


        // Ak nie je zadana verzia, tak sa nastavi verzia na "unresolved-version"
        if(version == null || version == "") {

            version = "0.0.0_unresolved";

        }

        // Vyhladanie TMP_Text objektu pre verziu -- ak neexistuje, vypise sa hlasenie a ukonci metoda
        TMP_Text versionObject = GameObject.Find("VersionText").GetComponent<TextMeshProUGUI>();
        if(versionObject == null) {

            Debug.Log("Cannot find 'VersionText' object, double check the objects.");
            return;

        }

        versionObject.text = $"Version: v{version}";

    }

}
