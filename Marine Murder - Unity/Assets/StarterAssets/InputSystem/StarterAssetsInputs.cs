using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;


		[SerializeField] private Camera mainCamera;
		[SerializeField] private GameManagerScript gameManager;
		[SerializeField] private FirstPersonController firstPersonController;
		[SerializeField] private DialogueScript dialogueScript;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
        {
			if (firstPersonController.playerState != PlayerState.dialogue)
			{
				// Bit shift the index of the layer (6) to get a bit mask, the "Interactables" layer
				// This will cast rays only against colliders in layer 6.
				int layerMask = 1 << 6;

				RaycastHit hit;
				// Does the ray intersect any objects in the "Interactables" layer
				if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
				{
					hit.transform.gameObject.GetComponent<IInteract>().Interact();
				}
			}
			else if(firstPersonController.playerState == PlayerState.dialogue)
            {
				dialogueScript.NextDialogue();
            }
		}

		public void OnExitFocus(InputValue value)
        {
			gameManager.ExitFocus();
        }
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}