using UnityEngine;

public class RingView : MonoBehaviour
{
    private Ring _ring; // ������ �� ������ ������
    private bool _isMoving = false;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _ring = GetComponent<Ring>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            // �������� �����������
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _isMoving = false;
            }
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        _targetPosition = position;
        _isMoving = true;
    }

    private void OnMouseDown()
    {
        // ��������� ����� �� ������
        _ring.Select();
    }
}
