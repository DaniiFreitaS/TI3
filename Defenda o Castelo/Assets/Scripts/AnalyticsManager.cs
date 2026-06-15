using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Mail;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance {get; private set;}
    [Header("Coloque seu email aqui, no inspetor")]
    public string emailRemetente = "seu@gmail.com";
    [Header("Coloque sua senha aqui, NÃO use a senha real, crie com o metódo do Roque")]
    public string senhaApp = "suaSenha";
    [Header("Coloque seu email aqui também, pelo inspetor")]
    public string emailDestinatario = "desenvolvedor@gmail.com";

    private string firstButton = "Null";
    private bool firstButtonClicked = false;
    private int creditsOpen = 0;

    private Dictionary<string, int> restartCount = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene lastScene)
    {
        string assunto = "Última cena" + lastScene.name;
        string corpo   = CreateAnalyticsData(lastScene.name);
        SendEmail(assunto, corpo);
    }

    public void ClickRegisterMenuButton(string nameButton)
    {
        if (!firstButtonClicked)
        {
            firstButton = nameButton;
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

    private string CreateAnalyticsData(string sceneName)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("Última cena:" + sceneName);
        sb.AppendLine("Primeiro botão clicado:" + firstButton);
        sb.AppendLine("Quantos abriram os créditos:" + creditsOpen);
        if (restartCount.Count == 0)
        {
            sb.AppendLine("Não houve reinícios.");
        }
        else
        {
            foreach (var par in restartCount)
                sb.AppendLine("Na cena " + par.Key + " houve " + par.Value + " reinícios");
        }

        return sb.ToString();
    }

    private void SendEmail(string assunto, string corpo)
    {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(emailRemetente, senhaApp),
                    EnableSsl   = true
                };

                MailMessage mensagem = new MailMessage(
                    emailRemetente,
                    emailDestinatario,
                    assunto,
                    corpo
                );

                client.Send(mensagem);
            }
            catch (Exception e)
            {
                Debug.LogError("[Analytics] Erro:" + e.Message);
            }
    }
}
