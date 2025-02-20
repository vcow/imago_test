namespace InTheGameScene.Signals
{
	public class UIMessageSignal
	{
		public string Message { get; }

		public UIMessageSignal(string message)
		{
			Message = message;
		}
	}
}