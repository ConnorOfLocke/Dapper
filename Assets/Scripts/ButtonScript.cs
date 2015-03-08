using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
		
	public enum BUTTON_FUNCTION
	{
		BUTTON_START_GAME,
		BUTTON_QUIT,
		BUTTON_CONTINUE,
		BUTTON_MAIN_MENU,
	};
	

	public BUTTON_FUNCTION curFunction = BUTTON_FUNCTION.BUTTON_START_GAME;

	public void PerformFunction()
	{
		switch (curFunction)
		{
			case BUTTON_FUNCTION.BUTTON_START_GAME:
				Application.LoadLevel("GameScene");
			break;
			
			case BUTTON_FUNCTION.BUTTON_QUIT:
				Application.Quit();
				Debug.Log("QUITTN TIME BOYS (ARRRROOOOOOOOOO)");
			break;
			
			case BUTTON_FUNCTION.BUTTON_CONTINUE:
				FindObjectOfType<PauseMenu>().ChangeState();
			break;
			
			case BUTTON_FUNCTION.BUTTON_MAIN_MENU:
				FindObjectOfType<PauseMenu>().ConfirmMainMenu();
			break; 
		};
	}
}
