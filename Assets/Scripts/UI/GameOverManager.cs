using UnityEngine;
using Snake2D.Enum;
using Snake2D.SceneLoad;

namespace Snake2D.Player.GameOver
{

    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instace { get; private set; }

        [SerializeField] private GameObject lossPanel;

        private void Awake()
        {
            if (Instace == null)
            {
                Instace = this;
            }
            else
            {
                Destroy(gameObject);
            }

            enabled = false;
        }

        public void OnDie()
        {
            AudioManager.Instance.PlayEffect(SoundType.Loss);

            lossPanel.SetActive(true);

            enabled = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneLoadManager.LoadScene(LoadScene.Game);
                enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                SceneLoadManager.LoadScene(LoadScene.Home);
                enabled = false;
            }
        }
    }

}