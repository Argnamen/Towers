using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text movesLeftText; // ����� ��� ����������� ���������� �����
    [SerializeField] private TMP_Text messageText;   // ����� ��� ����������� ���������
    [SerializeField] private Button restartButton; // ������ ��������
    [SerializeField] private Button winButton; // ������ ������

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
        winButton.gameObject.SetActive(false);

        // ��������� ���������� ��� ������ ��������
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        winButton.onClick.AddListener(OnNextLevelButtonClicked);
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
        winButton.gameObject.SetActive(true); // ���������� ������ ��������
    }

    /// <summary>
    /// ���������� ��������� � ���������� ������.
    /// </summary>
    public void ShowLevelFail()
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
    /// ���������� ������� �� ������ ���������� ������.
    /// </summary>
    private void OnNextLevelButtonClicked()
    {
        // �������� ��������� � ������ ��������
        messageText.gameObject.SetActive(false);
        winButton.gameObject.SetActive(false);

        // �������� ����� �������� ������ ����� GameController
        _gameController.NextLevel();
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