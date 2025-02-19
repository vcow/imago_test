using UnityEngine;
using Zenject;

namespace RoomScene
{
	[DisallowMultipleComponent]
	public sealed class RoomSceneInstaller : MonoInstaller<RoomSceneInstaller>
	{
		public override void InstallBindings()
		{
		}
	}
}