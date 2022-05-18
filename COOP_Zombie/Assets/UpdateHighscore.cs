using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using UnityEngine;

public class UpdateHighscore : MonoBehaviour
{
    //[SerializeField] private TMP_InputField playerName, playerScore;

    //public void SubmitButton()
    //{
    //    StartCoroutine(SubmitUser());
    //}

    //private IEnumerator SubmitUser()
    //{
    //    string url = "http://localhost/updateScore.php";
    //    wwwForm form = new wwwForm();

    //    form.AddField("name", playerName.text);
    //    form.AddField("score", playerScore.text);

    //    UnityWebRequest www = UnityWebRequest.Post(url, form);
    //    yield return www.SendWebRequest();

    //    Debug.Log("DB Updated");
    //}


    private string playerName;
    private int score = 50;

    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        gameObject.GetComponent<TMP_InputField>().onEndEdit.AddListener(NewScore);
    }

    public void NewScore(string text)
    {
        playerName = text;
        StartCoroutine(ConnectToPHP());
    }

    private IEnumerator ConnectToPHP()
    {
        string url = "http://localhost/updateScore.php";
        url += "?name=" + playerName + "&score" + score;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log("DB Updated");
    }
}
