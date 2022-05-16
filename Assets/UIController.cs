using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Button startGameBtn;
    Button msgBtn;
    bool started = false;
    Label msgText;
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
        /*if (started == false)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }*/
    }

    void StartGame()
    {
        //r.SetEnabled(false);
        //r.style.display = DisplayStyle.None;
        //SceneManager.LoadScene(SceneManager.GetSceneByName("GameOver").buildIndex);
        SceneManager.LoadScene(1);
    }

    void ShowMessage(){
    	//msgText.text = "Hello Player!! :)";
	    //msgText.style.display = DisplayStyle.Flex;
    }
}
