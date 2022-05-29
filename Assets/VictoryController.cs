using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    // Start is called before the first frame update
    VisualElement r;
    Button mainMenu;
    void Start()
    {
        r = GetComponent<UIDocument>().rootVisualElement;
        mainMenu = r.Q<Button>("backButton");
        mainMenu.clicked += BackToMainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
