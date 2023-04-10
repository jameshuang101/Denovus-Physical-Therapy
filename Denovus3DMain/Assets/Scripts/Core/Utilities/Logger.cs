using DilmerGames.Core.Singletons;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace DilmerGames.Core.Utilities
{
    public class Logger : Singleton<Logger>
    {
        public Transform screen;

        private bool feedbackShown = true;

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void GloveFeedback()
        {
            if (feedbackShown)
            {
                screen.transform.Translate(0f, -5, 0f);
                feedbackShown = false;
            }
            else
            {
                screen.transform.Translate(0f, 5f, 0f);
                feedbackShown = true;
            }
        }
    }
}