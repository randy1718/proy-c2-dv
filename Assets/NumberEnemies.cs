using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class NumberEnemies : MonoBehaviour
{
    VisualElement r;
    GameObject[] StaticEnemies;
    GameObject[] FlyingEnemies;
    Label enemiesCount;
    int numberEnemies;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<UIDocument>().rootVisualElement;
        StaticEnemies = GameObject.FindGameObjectsWithTag("Static enemy");
        FlyingEnemies = GameObject.FindGameObjectsWithTag("Flying enemy");
        enemiesCount = r.Q<Label>("numberEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        StaticEnemies = GameObject.FindGameObjectsWithTag("Static enemy");
        FlyingEnemies = GameObject.FindGameObjectsWithTag("Flying enemy");
        numberEnemies = StaticEnemies.Length + FlyingEnemies.Length;
        enemiesCount.text = numberEnemies.ToString();
        if (numberEnemies == 0)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(3);
        }
        /*if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            Debug.Log("activando numero de enemigos");
            enemies.SetActive(true);

        }
        else
        {
            enemies.SetActive(false);
        }*/
    }
}
