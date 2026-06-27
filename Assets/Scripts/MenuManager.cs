using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public const string ControlModeKey = "ControlMode";
    public const string ControlModeWasd = "WASD";
    public const string ControlModeArrows = "Arrows";

    public GameObject instructionPanel;
    public GameObject settingsPanel;
    public TMP_Text instructionText;
    public TMP_Text currentControlText;

    void Start()
    {
        EnsureDefaultControlMode();
        UpdateInstructionText();
        UpdateSettingsText();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowInstructions()
    {
        UpdateInstructionText();
        instructionPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        UpdateSettingsText();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetWasdControls()
    {
        SetControlMode(ControlModeWasd);
    }

    public void SetArrowControls()
    {
        SetControlMode(ControlModeArrows);
    }

    void SetControlMode(string mode)
    {
        PlayerPrefs.SetString(ControlModeKey, mode);
        PlayerPrefs.Save();

        UpdateInstructionText();
        UpdateSettingsText();
    }

    void EnsureDefaultControlMode()
    {
        if(!PlayerPrefs.HasKey(ControlModeKey))
        {
            PlayerPrefs.SetString(
                ControlModeKey,
                ControlModeArrows
            );
        }
    }

    void UpdateInstructionText()
    {
        if(instructionText==null)
            return;

        instructionText.text =
            "HƯỚNG DẪN CHƠI\n\n" +
            "Chế độ điều khiển: Cài đặt trong SETTING\n" +
            "Bắn laser: Space hoặc Chuột trái\n" +
            "Nhặt star: +100 điểm\n" +
            "Phá hủy asteroid: +10 điểm\n" +
            "Va chạm asteroid: -500 điểm và mất 1 mạng\n" +
            "Shield chắn 1 lần va chạm\n" +
            "Hết 3 mạng thì Game Over";
    }

    void UpdateSettingsText()
    {
        if(currentControlText==null)
            return;

        currentControlText.text =
            "Current control: " +
            (
                GetControlMode()==ControlModeWasd
                ? "WASD"
                : "ARROW KEYS"
            );
    }

    string GetControlMode()
    {
        return PlayerPrefs.GetString(
            ControlModeKey,
            ControlModeArrows
        );
    }
}
