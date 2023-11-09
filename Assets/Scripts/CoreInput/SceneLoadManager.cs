using UnityEngine.SceneManagement;
using Snake.Enum;

namespace Snake.SceneLoad
{
    public static class SceneLoadManager
    {
        public static void LoadScene(LoadScene scene)
        {
            SceneManager.LoadScene((int)scene);
        }

    }
}
