using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(PlayerInput), typeof(Collider2D))]
	public class BoyCharacterController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _character;

		private Vector2 _moveValue;

		public void OnMove(InputAction.CallbackContext context)
		{
			_moveValue = context.ReadValue<Vector2>();
		}

		private void Update()
		{
			var position = transform.position;
			_character.destination = new Vector3(position.x + _moveValue.x, position.y + _moveValue.y, 0f);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_character, "_character != null");
		}
	}
}