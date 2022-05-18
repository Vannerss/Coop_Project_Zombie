using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class AccessDatabase : MonoBehaviour
{
    private string url = "http://localhost/updateScore.php";

    [SerializeField] private TMP_Text highScore;

    private void Start()
    {
        StartCoroutine(GetRequest());
    }

    private IEnumerator GetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        highScore.text = www.downloadHandler.text;
    }
}
