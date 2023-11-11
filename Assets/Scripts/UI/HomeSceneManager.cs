using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake2D.Enum;
using Snake2D.SceneLoad;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayEffect(SoundType.UI);
            SceneLoadManager.LoadScene(LoadScene.Game);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            infoPanel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            infoPanel.SetActive(false);
        }
    }
}
