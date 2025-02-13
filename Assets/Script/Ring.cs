using UnityEngine;
using Zenject;

public class Ring : MonoBehaviour
{
    public int Size { get; private set; }
    public Peg CurrentPeg { get; set; }

    private GameController _gameController;

    [Inject]
    public void Construct(GameController gameController)
    {
        _gameController = gameController;
    }

    public void Initialize(int size)
    {
        Size = size;

        this.transform.localScale = new Vector3(size + 0.2f, 0.2f, size + 0.2f);
    }

    public void Select()
    {
        // Обработка выбора кольца (передаем управление GameController)
        _gameController.HandleRingSelection(this);
    }

    public void MoveToPosition(Vector3 position)
    {
        // Передаем управление RingView для анимации
        GetComponent<RingView>().MoveToPosition(position);
    }
}
