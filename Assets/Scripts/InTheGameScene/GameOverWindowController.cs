using InTheGameScene.Signals;
using UnityEngine;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent]
	public class GameOverWindowController : MonoBehaviour
	{
		[Inject] private readonly SignalBus _signalBus;

		private void Start()
		{
			gameObject.SetActive(false);
			_signalBus.Subscribe<GameOverSignal>(OnGameOver);
		}

		private void OnGameOver()
		{
			gameObject.SetActive(true);
		}

		private void OnDestroy()
		{
			_signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
		}
	}
}