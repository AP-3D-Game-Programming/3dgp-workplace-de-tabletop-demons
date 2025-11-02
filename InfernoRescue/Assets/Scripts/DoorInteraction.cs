using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;


    public bool isOpen = false;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCourotine;

    public NavMeshObstacle doorObstacle;
    public NavMeshSurface navMeshSurface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    public void ToggleDoor()
    {
        if (_currentCourotine != null)
            return;

        _currentCourotine = StartCoroutine(ToggleDoorCoroutine());
    }

    private IEnumerator ToggleDoorCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        if (isOpen && doorObstacle != null)
        {
            doorObstacle.enabled = false;
        }

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime* openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(time));
            yield return null;
        }

        transform.rotation = targetRotation;
        _currentCourotine = null;

        if (isOpen && navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        if (!isOpen && navMeshSurface != null)
        {
            doorObstacle.enabled = true;
        }
    }
}
