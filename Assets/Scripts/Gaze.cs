using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;


// Skript na ovladanie obrazovky pomocou Eye Tracking [2]
public class Gaze : MonoBehaviour
{

    /// Premenne
    private float timer = 0;                    // Ukladanie casu, ktory ubehol sledovanim na tlacidlo
    private bool isGazeOnButton;                // Detekovanie ci sa pozerame na tlacidlo

    private float delay = 1.0f;                 // Oneskorenie - ako dlho trva dokym sa vykona akcia pre kontroler
    private float cooldown = 0.0f;              // Cooldown nam zaistuje aby medzi jednotlivymi 
    private bool isActiveCooldown = false;      // Premenna pre ukladanie stavu cooldownu (je/nie je aktivny)


    // Update is called once per frame
    void Update()
    {

        // Ziskanie dat zo zariadenia (v nasom pripade Tobii Eye Tracker)
        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        // Ak su data validne a sme ocami nad niektorym z danych tlacidiel
        if(gazePoint.IsValid && IsWithinButtonBounds(gazePoint.Screen)) {

            // Pripocitanie casu -- ak ubehla viac ako sekunda a nie je aktivny cooldown, tak vykonaj kliknutie [3]
            timer += Time.deltaTime;
            if(timer >= delay && !isActiveCooldown) {
                GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                isActiveCooldown = true;
            }

            // Pozeram sa na tlacidlo
            isGazeOnButton = true;

        }
        else {
            // Resetovanie casovacu a premennej pre sledovanie
            {
                timer = 0f;
                isGazeOnButton = false;
            }

            // Je mozne stlacit spustit tlacidlo aj ked sa na neho pozeram kratsie nez je delay pomocou medzernika
            if (isGazeOnButton && Input.GetKeyDown(KeyCode.Space) && !isActiveCooldown) {

                print("Executing click on the button...");
                GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                isActiveCooldown = true; // Možno zbytočné?

            }
        }

        // Priebeh cooldownu
        if(isActiveCooldown) {
            
            cooldown += Time.deltaTime;
            if(cooldown >= delay) {  // 1.0f je delay
                cooldown = 0.0f;
                isActiveCooldown = false;
            }

        }

    }


    // Kontrola ci sa pozerame na tlacidlo. Na vstupe ziadame suradnice kde sa pozerame
    private bool IsWithinButtonBounds(Vector2 coor) {

        if(coor == null) {

            return false;

        }

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), coor, null, out localPoint);

        return GetComponent<RectTransform>().rect.Contains(localPoint);

    }

}
