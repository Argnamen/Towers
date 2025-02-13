using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text movesLeftText; // Текст для отображения оставшихся ходов
    [SerializeField] private TMP_Text messageText;   // Текст для отображения сообщений
    [SerializeField] private Button restartButton; // Кнопка рестарта
    [SerializeField] private Button winButton; // Кнопка победы

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
        winButton.gameObject.SetActive(false);

        // Назначаем обработчик для кнопки рестарта
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        winButton.onClick.AddListener(OnNextLevelButtonClicked);
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
        winButton.gameObject.SetActive(true); // Показываем кнопку рестарта
    }

    /// <summary>
    /// Показывает сообщение о завершении уровня.
    /// </summary>
    public void ShowLevelFail()
    {
        messageText.text = "Уровень проигран!";
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
    /// Обработчик нажатия на кнопку следующего уровня.
    /// </summary>
    private void OnNextLevelButtonClicked()
    {
        // Скрываем сообщение и кнопку рестарта
        messageText.gameObject.SetActive(false);
        winButton.gameObject.SetActive(false);

        // Вызываем метод рестарта уровня через GameController
        _gameController.NextLevel();
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