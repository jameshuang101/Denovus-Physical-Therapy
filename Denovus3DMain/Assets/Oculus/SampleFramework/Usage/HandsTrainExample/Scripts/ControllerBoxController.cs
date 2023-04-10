using UnityEngine;
using UnityEngine.SceneManagement;

namespace OculusSampleFramework
{
	public class ControllerBoxController : MonoBehaviour
	{
		private bool isPaused;

		public void PauseGame(InteractableStateArgs obj)
		{
			if (obj.NewInteractableState == InteractableState.ActionState)
			{
				if (isPaused)
                {
					Time.timeScale = 1;
					isPaused = false;
				}
				else
                {
					Time.timeScale = 0;
					isPaused = true;
				}
			}
		}

		public void MainMenu(InteractableStateArgs obj)
		{
			if (obj.NewInteractableState == InteractableState.ActionState)
			{
				SceneManager.LoadScene("DenovusTurretShooterIntroduction");
			}
		}
	}
}
