using UnityEngine;

public static class WaitExtension
{
    public static void Wait(this MonoBehaviour mono, float delay, UnityEngine.Events.UnityAction action)
    {
        mono.StartCoroutine(ExecuteAction(delay, action));
    }

    private static System.Collections.IEnumerator ExecuteAction(float delay, UnityEngine.Events.UnityAction action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action.Invoke();
        yield break;
    }
}