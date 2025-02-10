using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text movesLeftText; // ����� ��� ����������� ���������� �����
    [SerializeField] private Text messageText;   // ����� ��� ����������� ���������
    [SerializeField] private Button restartButton; // ������ ��������

    private GameController _gameController;

    // ��������� ����������� ����� �����������
    [Inject]
    public void Construct(GameController gameController)
    {
        _gameController = gameController;
    }

    private void Start()
    {
        // �������� ��������� � ������ �������� ��� ������
        messageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // ��������� ���������� ��� ������ ��������
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    /// <summary>
    /// ��������� ����� � ����������� ���������� �����.
    /// </summary>
    /// <param name="movesLeft">���������� ���������� �����.</param>
    public void UpdateMovesLeft(int movesLeft)
    {
        movesLeftText.text = $"�������� �����: {movesLeft}";
    }

    /// <summary>
    /// ���������� ��������� � ���������� ������.
    /// </summary>
    public void ShowLevelComplete()
    {
        messageText.text = "������� ��������!";
        messageText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true); // ���������� ������ ��������
    }

    /// <summary>
    /// ���������� ��������� � ������������ ����.
    /// </summary>
    public void ShowInvalidMoveMessage()
    {
        messageText.text = "������������ ���!";
        messageText.gameObject.SetActive(true);

        // �������� ��������� ����� 2 �������
        Invoke(nameof(HideMessage), 2f);
    }

    /// <summary>
    /// �������� ���������.
    /// </summary>
    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���������� ������� �� ������ ��������.
    /// </summary>
    private void OnRestartButtonClicked()
    {
        // �������� ��������� � ������ ��������
        messageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // �������� ����� �������� ������ ����� GameController
        _gameController.RestartLevel();
    }
}