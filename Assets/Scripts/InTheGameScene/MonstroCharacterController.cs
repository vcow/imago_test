using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
	public class MonstroCharacterController : MonoBehaviour
	{
		private const float ValidateTargetDelayTime = 1f;

		[SerializeField] private NavMeshAgent _character;

		[Inject] private readonly BoyCharacterController _victim;

		private IDisposable _timerHandler;

		private void Start()
		{
			_character.SetDestination(_victim.transform.position);
			_timerHandler = Observable.Timer(TimeSpan.FromSeconds(ValidateTargetDelayTime), Scheduler.MainThread)
				.Repeat().Subscribe(_ => _character.SetDestination(_victim.transform.position));
		}

		private void OnDestroy()
		{
			_timerHandler?.Dispose();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var characterCollider = _victim.GetComponent<Collider2D>();
			Assert.IsNotNull(characterCollider, "Victim must have collider.");
			if (other == characterCollider)
			{
				_timerHandler?.Dispose();
				_timerHandler = null;
				_character.isStopped = true;

				// TODO: Game over
			}
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_character, "_character != null");
		}
	}
}