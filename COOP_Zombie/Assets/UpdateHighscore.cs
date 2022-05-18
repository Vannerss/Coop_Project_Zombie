using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using UnityEngine;

public class UpdateHighscore : MonoBehaviour
{
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
