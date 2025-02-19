using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PreloaderScene
{
	[DisallowMultipleComponent]
	public class PreloaderSceneController : MonoBehaviour
	{
		[Inject] private readonly ZenjectSceneLoader _sceneLoader;

		private async void Start()
		{
			await InitializeGame();
			_sceneLoader.LoadSceneAsync(Const.RoomSceneName);
		}

		private UniTask InitializeGame()
		{
			// TODO: Insert game initialization actions here.
			return UniTask.CompletedTask;
		}
	}
}