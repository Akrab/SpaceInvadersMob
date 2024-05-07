
using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;

namespace SpaceInvadersMob
{
    public static class ExtensionsGame
    {

        public static Bounds OrthographicBounds(this Camera camera)
        {
            var screenAspect = Screen.width / (float)Screen.height;
            var cameraHeight = camera.orthographicSize * 2.0f;
            Bounds bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }

        public static IObservable<float> ToObservable(this UnityEngine.AsyncOperation asyncOperation)
        {

            if (asyncOperation == null) throw new ArgumentNullException("asyncOperation");

            return Observable.FromCoroutine<float>((observer, cancellationToken) =>
                RunAsyncOperation(asyncOperation, observer, cancellationToken));
        }

        static IEnumerator RunAsyncOperation(UnityEngine.AsyncOperation asyncOperation, IObserver<float> observer,
            CancellationToken cancellationToken)
        {
            while (!asyncOperation.isDone && !cancellationToken.IsCancellationRequested)
            {
                observer.OnNext(asyncOperation.progress);
                yield return null;
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                observer.OnNext(asyncOperation.progress); // push 100%
                observer.OnCompleted();
            }
        }
    }
}