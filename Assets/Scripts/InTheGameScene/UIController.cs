using InTheGameScene.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent]
	public class UIController : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _message;

		[Inject] private readonly SignalBus _signalBus;

		private void Start()
		{
			_signalBus.Subscribe<UIMessageSignal>(OnUiMessage);
			_message.text = "Find a terminal through which you can exit the game.";
		}

		private void OnUiMessage(UIMessageSignal signal)
		{
			_message.text = signal.Message;
		}

		private void OnDestroy()
		{
			_signalBus.Unsubscribe<UIMessageSignal>(OnUiMessage);
		}

		private void OnValidate()
		{
			Assert.IsNotNull(_message, "_message != null");
		}
	}
}