using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using Random = UnityEngine.Random;

namespace InTheGameScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
	public class MonstroSpawnController : MonoBehaviour
	{
		[SerializeField] private MonstroCharacterController _monstroPrefab;
		[SerializeField] private BoyCharacterController _victim;
		[SerializeField] private float _maxDelayTimeToSpawn;

		[Inject] private readonly DiContainer _container;

		private bool _monstroIsSpawned;
		private IDisposable _spawnTimerHandler;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_monstroIsSpawned)
			{
				return;
			}

			var characterCollider = _victim.GetComponent<Collider2D>();
			Assert.IsNotNull(characterCollider, "Victim must have collider.");
			if (other == characterCollider)
			{
				Assert.IsNull(_spawnTimerHandler);
				if (_maxDelayTimeToSpawn <= 0)
				{
					Spawn();
				}
				else
				{
					var delayTime = _maxDelayTimeToSpawn * Random.value;
					_spawnTimerHandler = Observable.Timer(TimeSpan.FromSeconds(delayTime), Scheduler.MainThread)
						.Subscribe(_ => Spawn());
				}
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (_monstroIsSpawned)
			{
				return;
			}

			var characterCollider = _victim.GetComponent<Collider2D>();
			Assert.IsNotNull(characterCollider, "Victim must have collider.");
			if (other == characterCollider)
			{
				Assert.IsNotNull(_spawnTimerHandler);
				_spawnTimerHandler.Dispose();
				_spawnTimerHandler = null;
			}
		}

		private void Spawn()
		{
			var monstro = _container.InstantiatePrefabForComponent<MonstroCharacterController>(_monstroPrefab, new object[] {_victim});
			monstro.transform.position = transform.position;
			_monstroIsSpawned = true;
		}

		private void OnDestroy()
		{
			_spawnTimerHandler?.Dispose();
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_monstroPrefab, "_monstroPrefab != null");
			Assert.IsNotNull(_victim, "_victim != null");
		}
	}
}