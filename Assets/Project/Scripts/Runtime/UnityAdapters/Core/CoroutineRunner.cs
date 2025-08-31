using System.Collections;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class CoroutineRunner
	{
		private readonly MonoBehaviour _coroutineContext;
		
		public CoroutineRunner(MonoBehaviour coroutineContext)
		{
			_coroutineContext = coroutineContext;
		}

		public Coroutine Run(IEnumerator runNextFrame)
		{
			return _coroutineContext.StartCoroutine(runNextFrame);
		}

		public void StopCoroutine(Coroutine coroutine)
		{
			_coroutineContext.StopCoroutine(coroutine);
		}
	}
}