using System;
using System.Collections;
using UnityEngine;

namespace GovorovAleksandr.BridgeBuilding.UnityAdapters
{
	public sealed class PureAnimation
	{
		private readonly CoroutineRunner _coroutineRunner;

		public PureAnimation(CoroutineRunner coroutineRunner)
		{
			_coroutineRunner = coroutineRunner;
		}

		public void Run(float duration, Action<float> handler)
		{
			_coroutineRunner.Run(Coroutine(duration, handler));
		}

		private static IEnumerator Coroutine(float duration, Action<float> handler)
		{
			var progress = 0f;
			var expiredTime = 0f;

			while (progress < 1f)
			{
				expiredTime += Time.deltaTime;
				progress = expiredTime / duration;
				handler(progress);
				yield return null;
			}
		}
	}
}