using UnityEngine;
using Zenject;

namespace InTheGameScene
{
	[DisallowMultipleComponent]
	public sealed class InTheGameSceneInstaller : MonoInstaller<InTheGameSceneInstaller>
	{
		public override void InstallBindings()
		{
		}
	}
}