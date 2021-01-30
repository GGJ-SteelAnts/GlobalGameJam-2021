using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Scene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
