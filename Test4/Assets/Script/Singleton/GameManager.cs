using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [Header("Player Stats")]
    [SerializeField] private int maxHp = 3;
    [SerializeField] private float staminaMax = 5f;
    [SerializeField] private float staminaRecoverRate = 1f;

    private int playerHp;
    private float playerStamina;
    private float staminaDelta = 1f;

    [Header("Inventory / Status")]
    public int HpPotion = 0;
    public int Gold = 0;
    public float PotionAddiction = 0f;

    public bool IsStory;
    public bool IsPaused;
    public bool IsGroggy;

    [Header("Events")]
    public UnityEvent OnGuardPostureActivated = new();
    public UnityEvent OnGuardPostureDeactivated = new();
    public UnityEvent OnPauseStateChanged = new();

    private MoveCharacter player;

    private void Start()
    {
        playerHp = maxHp;
        playerStamina = staminaMax;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<MoveCharacter>();
    }

    private void Update()
    {
        if (IsStory) return;

        HandlePauseInput();
        HandleGuardInput();
        UpdateStamina();
        CheckPotionAddiction();
    }

    #region 🕹 Input Handling
    private void HandlePauseInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    private void HandleGuardInput()
    {
        if (IsPaused || IsGroggy) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            staminaDelta = -5f;
            Time.timeScale = 0.2f;
            OnGuardPostureActivated.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            staminaDelta = 1f;
            Time.timeScale = 1f;
            OnGuardPostureDeactivated.Invoke();
        }
    }
    #endregion

    #region 💪 Stamina / Groggy

    public float ReturnStamina()
    {
        return playerStamina;
    }

    public float ReturnMaxStamina()
    {
        return staminaMax;
    }

    private void UpdateStamina()
    {
        playerStamina = Mathf.Clamp(playerStamina + staminaDelta * Time.deltaTime, 0f, staminaMax);

        if (playerStamina <= 0f && !IsGroggy)
            StartCoroutine(StartGroggy(3f));
    }

    private IEnumerator StartGroggy(float duration)
    {
        IsGroggy = true;
        staminaDelta = 0f;
        Time.timeScale = 1f;
        OnGuardPostureDeactivated.Invoke();

        if (player != null)
            player.StartCoroutine(player.StartGroggy(duration));

        yield return new WaitForSeconds(duration);

        IsGroggy = false;
        staminaDelta = 1f;
    }
    #endregion

    #region ⚗ Potion / Story
    private void CheckPotionAddiction()
    {
        if (PotionAddiction >= 5f)
        {
            Debug.LogWarning("⚠ 포션 중독 상태입니다!");
        }
    }

    public void ToggleStoryMode()
    {
        IsStory = !IsStory;
    }
    #endregion

    #region ⏸ Pause / Resume
    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;

        UIManager.Instance?.ActivatePauseMenu(true);
        OnPauseStateChanged.Invoke();
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;

        UIManager.Instance?.ActivatePauseMenu(false);
        OnPauseStateChanged.Invoke();
    }
    #endregion

    #region 🔁 Scene / App Control
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}
