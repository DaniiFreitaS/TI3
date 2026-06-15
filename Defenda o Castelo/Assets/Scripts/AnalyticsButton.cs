using UnityEngine;

public class AnalyticsButton : MonoBehaviour
{
    public enum ButtonType
    {
        ButtonCredits,
        ButtonRestart,
        ButtonNeutral
    }

    public ButtonType buttonType = ButtonType.ButtonRestart;
    public string buttonName = "Botão";

    public void Register()
    {
        if (AnalyticsManager.Instance == null)
        {
            Debug.Log("Não existe AnalyticsManager");
            return;
        }

        switch (buttonType)
        {
            case ButtonType.ButtonNeutral:
                AnalyticsManager.Instance.ClickRegisterMenuButton(buttonName);
                break;
            case ButtonType.ButtonCredits:
                AnalyticsManager.Instance.ClickRegisterMenuButton(buttonName);
                AnalyticsManager.Instance.ResgisterCredits();
                break;

            case ButtonType.ButtonRestart:
                AnalyticsManager.Instance.RegisterClickRestart();
                break;
        }
    }
}
