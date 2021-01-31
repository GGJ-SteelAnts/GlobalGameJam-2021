using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI ;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ScoreText;
    public InputField name;
    public InputField score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + DataManager.Score();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        StartCoroutine(GetText("dev.steelants.cz/GGJ2021/GeorgeJones/Server/api.php"));
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ScoreSubmit()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name.text);
        form.AddField("score", DataManager.Score());

        StartCoroutine(PostText("dev.steelants.cz/vasek/GGJ2021/GeorgeJones/Server/api.php", form));
        SceneManager.LoadScene(0);
    }

    public void RestartLevel(){
        SceneManager.LoadScene(DataManager.Level());
    }

    IEnumerator GetText(string uri) {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            ScoreText.text = www.downloadHandler.text;
        }
    }

    IEnumerator PostText(string uri, WWWForm data) {
        UnityWebRequest www = UnityWebRequest.Post(uri, data);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
    }
}
