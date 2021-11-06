using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class Toucher : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IBeginDragHandler
{
    public event UnityAction TouchStart;
    public event UnityAction TouchEnd;
    [SerializeField] private Ball _ball;
    private Vector2 _oldTouch;
    private Stopwatch _startTouch;
    private Camera _main;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _oldTouch = _main.ScreenToViewportPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newTouch = _main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 ballDirection = newTouch - _oldTouch;
        ballDirection = new Vector3(ballDirection.x, 0, ballDirection.y);
        _ball.Move(ballDirection);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchStart?.Invoke();
        _ball.Stop();
        _startTouch = Stopwatch.StartNew();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchEnd?.Invoke();
        Vector2 newTouch = _main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 ballDirection = newTouch - _oldTouch;
        ballDirection = new Vector3(ballDirection.x, 0.13f, ballDirection.y);
        _ball.Kick(ballDirection);
    }

    private void Awake()
    {
        _main = Camera.main;
    }
}