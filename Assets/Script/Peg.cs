using System.Collections.Generic;
using UnityEngine;
using Zenject.Asteroids;
using Zenject;

public class Peg : MonoBehaviour
{
    public List<Ring> Rings { get; private set; } = new List<Ring>();

    private GameController _gameController;

    [Inject]
    public void Construct(GameController gameController)
    {
        _gameController = gameController;
    }


    public void Select()
    {
        // Обработка выбора кольца (передаем управление GameController)
        _gameController.HandlePegSelection(this);
    }
    public void AddRing(Ring ring)
    {
        Rings.Add(ring);
        ring.CurrentPeg = this;
        ring.transform.SetParent(this.transform); // Привязываем кольцо к основе
        ring.MoveToPosition(CalculateRingPosition(ring));
    }

    public void RemoveRing(Ring ring)
    {
        Rings.Remove(ring);
    }

    private Vector3 CalculateRingPosition(Ring ring)
    {
        float ringHeight = ring.GetComponent<Renderer>().bounds.size.y;
        int ringIndex = Rings.IndexOf(ring);
        return transform.position + new Vector3(0, ringHeight * ringIndex, 0);
    }
}
