using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Button startGameBtn;
    VisualElement r;

    // Start is called before the first frame update
    void Start()    
    {
        r = GetComponent<UIDocument>().rootVisualElement;
        startGameBtn = r.Q<Button>("start-btn");
        startGameBtn.clicked += StartGame;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
