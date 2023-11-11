using UnityEngine.SceneManagement;
using Snake2D.Enum;

namespace Snake2D.SceneLoad
{
    public static class SceneLoadManager
    {
        public static void LoadScene(LoadScene scene)
        {
            SceneManager.LoadScene((int)scene);
        }

    }
}
