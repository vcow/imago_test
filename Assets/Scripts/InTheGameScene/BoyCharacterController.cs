using System;
using InTheGameScene.Signals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(PlayerInput), typeof(Collider2D))]
	public class BoyCharacterController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _character;

		[Inject] private readonly SignalBus _signalBus;

		private Vector2 _moveValue;
		private bool _youDead;

		private void Start()
		{
			_signalBus.Subscribe<GameOverSignal>(OnGameOver);
		}

		private void OnGameOver()
		{
			_youDead = true;
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			_moveValue = context.ReadValue<Vector2>();
		}

		private void Update()
		{
			if (_youDead)
			{
				return;
			}

			var position = transform.position;
			_character.destination = new Vector3(position.x + _moveValue.x, position.y + _moveValue.y, 0f);
		}

		private void OnDestroy()
		{
			_signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_character, "_character != null");
		}
	}
}