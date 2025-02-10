using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text movesLeftText; // Текст для отображения оставшихся ходов
    [SerializeField] private Text messageText;   // Текст для отображения сообщений
    [SerializeField] private Button restartButton; // Кнопка рестарта

    private GameController _gameController;

    // Внедрение зависимости через конструктор
    [Inject]
    public void Construct(GameController gameController)
    {
        _gameController = gameController;
    }

    private void Start()
    {
        // Скрываем сообщение и кнопку рестарта при старте
        messageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Назначаем обработчик для кнопки рестарта
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    /// <summary>
    /// Обновляет текст с количеством оставшихся ходов.
    /// </summary>
    /// <param name="movesLeft">Количество оставшихся ходов.</param>
    public void UpdateMovesLeft(int movesLeft)
    {
        movesLeftText.text = $"Осталось ходов: {movesLeft}";
    }

    /// <summary>
    /// Показывает сообщение о завершении уровня.
    /// </summary>
    public void ShowLevelComplete()
    {
        messageText.text = "Уровень завершен!";
        messageText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true); // Показываем кнопку рестарта
    }

    /// <summary>
    /// Показывает сообщение о недопустимом ходе.
    /// </summary>
    public void ShowInvalidMoveMessage()
    {
        messageText.text = "Недопустимый ход!";
        messageText.gameObject.SetActive(true);

        // Скрываем сообщение через 2 секунды
        Invoke(nameof(HideMessage), 2f);
    }

    /// <summary>
    /// Скрывает сообщение.
    /// </summary>
    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Обработчик нажатия на кнопку рестарта.
    /// </summary>
    private void OnRestartButtonClicked()
    {
        // Скрываем сообщение и кнопку рестарта
        messageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Вызываем метод рестарта уровня через GameController
        _gameController.RestartLevel();
    }
}