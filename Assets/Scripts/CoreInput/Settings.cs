using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Text muteToggleText;

    private int Mute
    {
        get {
            return PlayerPrefs.GetInt(nameof(Mute), 1);
        }
        set {
            PlayerPrefs.SetInt(nameof(Mute), value);
        }
    }

    private bool IsMute
    {
        get {
            return Mute == 1;
        }
        set {
            Mute = value ? 1 : 0;
        }
    }

    private void Start()
    {
        ManageSource();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMuteUnmute();
        }
    }

    private void SetMuteUnmute()
    {
        Toggle();
        ManageSource();
    }

    private void ManageSource()
    {
        AudioManager.Instance.MuteToggle(IsMute);
        SetUI();
    }

    private void Toggle()
    {
        IsMute = !IsMute;
    }

    private void SetUI()
    {
        string mute = "Press 'M' to unmute";
        string unmute = "Press 'M' to Mute";

        muteToggleText.text = IsMute ? unmute : mute;
    }
}
