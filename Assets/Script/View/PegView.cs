using UnityEngine;

public class PegView : MonoBehaviour
{
    private Peg _peg; // ������ �� ������ ������
    private Renderer _renderer; // ��������� ��� ����������� �������������

    private void Awake()
    {
        _peg = GetComponent<Peg>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        // ��������� ������ ��� ���������
        _renderer.material.color = Color.yellow;
    }

    private void OnMouseDown()
    {
        _peg.Select();
    }

    private void OnMouseExit()
    {
        // ���������� ������� ����
        _renderer.material.color = Color.white;
    }
}