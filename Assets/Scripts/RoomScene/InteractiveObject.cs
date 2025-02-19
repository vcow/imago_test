using System;
using System.Linq;
using UnityEngine;

namespace RoomScene
{
	[DisallowMultipleComponent, RequireComponent(typeof(Collider))]
	public abstract class InteractiveObject : MonoBehaviour
	{
		[SerializeField] private MessageRecord[] _messages;

		public string GetMessage(float distance)
		{
			var result = string.Empty;
			foreach (var record in _messages.OrderBy(record => record._distance))
			{
				result = record._message;

				if (record._distance > distance)
				{
					break;
				}
			}

			return result;
		}

		public abstract void Interact(float distance);

		[Serializable]
		private class MessageRecord
		{
			public float _distance;
			public string _message;
		}
	}
}