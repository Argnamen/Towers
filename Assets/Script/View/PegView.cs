using UnityEngine;

public class PegView : MonoBehaviour
{
    private Peg _peg; // Ссылка на логику основы
    private Renderer _renderer; // Компонент для визуального представления

    private void Awake()
    {
        _peg = GetComponent<Peg>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        // Подсветка основы при наведении
        _renderer.material.color = Color.yellow;
    }

    private void OnMouseDown()
    {
        _peg.Select();
    }

    private void OnMouseExit()
    {
        // Возвращаем обычный цвет
        _renderer.material.color = Color.white;
    }
}