using System.Collections;
using System.Threading.Tasks;
using GovorovAleksandr.BridgeBuilding.Contracts;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class UnityFrameAwaiter : IFrameAwaiter, IPrioritizedInitializable
	{
		private readonly IServiceLocator _serviceLocator;
		
		private CoroutineRunner _coroutineRunner;
		
		public UnityFrameAwaiter(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		public int Priority => -150;

		public void Initialize()
		{
			_coroutineRunner = _serviceLocator.Resolve<CoroutineRunner>();
		}

		public Task WaitNextFrame()
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();
			_coroutineRunner.Run(RunNextFrame(taskCompletionSource));
			return taskCompletionSource.Task;
		}

		private static IEnumerator RunNextFrame(TaskCompletionSource<bool> taskCompletionSource)
		{
			yield return null;
			taskCompletionSource.SetResult(true);
		}
	}
}