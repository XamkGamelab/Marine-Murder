/* Original code by Emma Ewert
 * https://gist.github.com/EmmaEwert/fad38a248f62e1c7267daa4e778468b1
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonInputModule : StandaloneInputModule
{
	protected override MouseState GetMousePointerEventData(int id)
	{
		var lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
		var mouseState = base.GetMousePointerEventData(id);
		Cursor.lockState = lockState;
		return mouseState;
	}

	protected override void ProcessMove(PointerEventData pointerEvent)
	{
		var lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		base.ProcessMove(pointerEvent);
		Cursor.lockState = lockState;
	}

	protected override void ProcessDrag(PointerEventData pointerEvent)
	{
		var lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		base.ProcessDrag(pointerEvent);
		Cursor.lockState = lockState;
	}
}