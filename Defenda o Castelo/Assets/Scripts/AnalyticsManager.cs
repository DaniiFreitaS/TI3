using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    [Header("Coloque seu e-mail no inspetor")]
    [SerializeField] private string emailRemetente;
    [Header("Coloque sua senha aqui, NÃO use a senha real, crie com o metódo do Roque")]
    [SerializeField] private string senhaApp;
    [Header("Coloque seu email aqui também, pelo inspetor")]
    [SerializeField] private string emailDestinatario;

    private string firstButton = "Null";
    private bool firstButtonClicked = false;
    private int creditsOpen = 0;

    private Dictionary<string, int> restartCount = new();

    [System.Serializable]
    public class RestartData
    {
        public string sceneName;
        public int restartAmount;
    }

    [System.Serializable]
    public class AnalyticsData
    {
        public string lastScene;
        public string firstButton;
        public int creditsOpen;
        public List<RestartData> restartData;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string assunto = "Analytics " + sceneName;
        AnalyticsData data = CreateAnalyticsData(sceneName);
        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/analytics.json";
        System.IO.File.WriteAllText(path, json);
        SendEmail(assunto, json, path);
        Debug.Log("O jogo saiu");
    }

    public void ClickRegisterMenuButton(string buttonName)
    {
        if (!firstButtonClicked)
        {
            firstButton = buttonName;
            firstButtonClicked = true;
        }
    }

    public void ResgisterCredits()
    {
        creditsOpen++;
    }

    public void RegisterClickRestart()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (!restartCount.ContainsKey(currentScene))
            restartCount[currentScene] = 0;

        restartCount[currentScene]++;
    }

    private AnalyticsData CreateAnalyticsData(string sceneName)
    {
        AnalyticsData data = new AnalyticsData();

        data.lastScene = sceneName;
        data.firstButton = firstButton;
        data.creditsOpen = creditsOpen;
        data.restartData = new List<RestartData>();

        foreach (var restart in restartCount)
        {
            data.restartData.Add(new RestartData
            {
                sceneName = restart.Key,
                restartAmount = restart.Value
            });
        }

        return data;
    }
    private void SendEmail(string assunto, string corpo, string path)
    {
        try
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(emailRemetente, senhaApp),
                EnableSsl = true
            };

            MailMessage mensagem = new MailMessage(
                emailRemetente,
                emailDestinatario,
                assunto,
                corpo
            );

            Attachment attachment = new Attachment(path);
            mensagem.Attachments.Add(attachment);

            client.Send(mensagem);
        }
        catch (Exception e)
        {
            Debug.LogError("Erro:" + e.Message);
        }
    }
}