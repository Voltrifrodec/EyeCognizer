# EyeCognizer

Unity 2D aplikácia pre riešenie jednoduchých kvízov pomocou zraku s Tobii Eye Tracker.


## Vytváranie nových kvízov

Pre pridanie vlastného súboru je nutné vytvoriť ten istý súbor na dvoch miestach: jeden v `/Assets/Quizzes`, a druhý v `/Assets/StreamingAssets/Quizzes`. Taktiež je nutné spomenúť niekoľko vecí:
- Vo finálnej verzii projektu nie je podpora pre viacej ako **päť kvízov naraz** (inak sa kvízy nezobrazania na obrazovke).
- Každá otázka musí obsahovať **minimálne dve, a maximálne tri možnosti**.

V tomto projekte sa nachádzajú jeden hlavný a dva ukážkové kvízy:
1. *Example Quiz*: Tento kvíz sa nesmie odstrániť, inak nebude zabezpečená správnosť aplikácie. Kvíz obsahuje tri orázky a slúži hlavne ako ukážka testov.
2. *Matematika*: Jednoduchý kvíz s matematickými otázkami.
3. *Vybrané slová po B*: Kvíz s niekoľkými otázkami zamerané na slovenské vybrané slová po b.


### Postup pridávania nových kvízov
1. Vytvorte súbor typu JSON, napríklad `my-quiz.json`.
2. Skopírujte si šablónu pre kvíz a upravte ju podľa potrieb:

    ```json
    {        
        "questions": [
            {
                "id": 0,
                "question": "quiz_question",
                "options": [
                    "Option_1",
                    "Option_2",
                    "Option_3"
                ],
                "correctOption": 0
            },
        ]
    }
    ```
    - Vysvetlivky:
      - `"questions": []` obsahuje pole otázok.
      - `"id": 0`: ID otázky. Začína sa na 0, a postupne sa inkrementuje o 1.
      - `"question": "string"` text otázky.
      - `"options": []`: pole reťazcov, pričom jeden reťazec reprezentuje jednu možnosť pre otázku. Počet možností musí byť v rozsahu 2-3.
      - `"correctOption": 0`: číslo správnej možnosti, v rozsahu 0-2 (záleží na počte možností).

3. Nakopírujte vytvorený do priečinkov `/Assets/Quizzes` a `Assets/StreamingAssets/Quizzes` a otestuje správnosť aplikácie (či vie aplikácia daný kvíz rozpoznať).


## Referencie

Použité materiály, články a dokumentácia pri vývoji aplikácie.

#### ApplicationController.cs

1. *How to determine whether a Scene with ‘name’ exists (with the new SceneManager)?*. Unity Discussions. Online: https://discussions.unity.com/t/how-to-determine-whether-a-scene-with-name-exists-with-the-new-scenemanager/159799.

#### Gaze.cs

2. *Getting Started*. Tobii Developer Zone. Online: https://developer.tobii.com/pc-gaming/unity-sdk/getting-started/.
3. *How to trigger a button click from script*. Unity Discussions. Online: https://discussions.unity.com/t/how-to-trigger-a-button-click-from-script/135868/2.

#### Question.cs

4. *Unity - Scripting API: SerializeField*. Unity Documentation. Online:  https://docs.unity3d.com/ScriptReference/SerializeField.html.

#### QuestionList.cs

5. *JSON .Net Serialization not working correctly*. Stack Overflow. Online: https://stackoverflow.com/questions/31028816/json-net-serialization-not-working-correctly.

#### Quiz.cs

6. *Getting values from JSON*. Unity Forum. ****Online: https://forum.unity.com/threads/getting-values-from-json.1224765/.
7. (Answer) *How to pass data (and references) between scenes in Unity***.** Stack Overflow. Online: https://stackoverflow.com/a/44542726/11557114.
8. ********************Unity - Scripting API: PlayerPrefs.******************** Unity Documentation. Online: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html.
9. *How to set a value at transform.localPosition.y*. Unity Forum. Online: https://forum.unity.com/threads/how-to-set-a-value-at-transform-localposition-y.116926/.
10. (Answer) *How to find a Child Gameobject by name?*. Unity Discussions. Online: https://discussions.unity.com/t/how-to-find-a-child-gameobject-by-name/31255/2.

#### QuizItem.cs

11.  *Unity - Scripting API: PlayerPrefs.SetString*. Unity Documentation. Online: https://docs.unity3d.com/ScriptReference/PlayerPrefs.SetString.html.

#### Quizzes.cs

12. *How to spawn an object in Unity (using Instantiate).* gamedevbeginner*.* Online: https://gamedevbeginner.com/how-to-spawn-an-object-in-unity-using-instantiate/#instantiate_multiple.
13. *Count the number of files in one directory*. Unity Discussions. Online: https://discussions.unity.com/t/count-the-number-of-files-in-one-directory/46434.
14. *How to get project folder path?in editor Mode*. Unity Forum. Online: https://forum.unity.com/threads/how-to-get-project-folder-path-in-editor-mode.311469/.
15. *FileInfo Class (System.IO)*. Microsoft Learn. Online****:**** https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-8.0.
16. *Unity - Scripting API: RuntimePlatform*. Unity Documentation. Online: https://docs.unity3d.com/ScriptReference/RuntimePlatform.html.
17. *Path.GetFileNameWithoutExtension Method (System.IO)*. Microsoft Learn. Online: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-8.0.

#### Ostatné zdroje

[https://docs.unity3d.com](https://docs.unity3d.com/ScriptReference/RuntimePlatform.html), [https://discussions.unity.com](https://discussions.unity.com/t/count-the-number-of-files-in-one-directory/46434), Internet