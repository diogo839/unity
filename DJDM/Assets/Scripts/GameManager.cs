using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; } = null;
    public bool IsPaused { get; private set; } = false;

    [Header("Abilities")]
    public const string JUMP_UPGRADE = "Double Jump";
    public const string SHOOT_UPGRADE = "Shoot";
    public const string DAMAGE_BOOST_UPGRADE = "Damage";
    public const string SPEED_BOOST_UPGRADE = "Speed";
    public const string JUMP_BOOST_UPGRADE = "Jump";
    private Dictionary<string, float> upgrades = new Dictionary<string, float>();

    public float baseDamage = 10f;

    //[SerializeField]
    //public TMP_Text gameScoreText = null;

    //private int score = 0;

    private int level = 0;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        upgrades.Add(JUMP_UPGRADE, 0f); //Starts false, no double jump
        upgrades.Add(SHOOT_UPGRADE, 0f); //Starts false, no shoot
        upgrades.Add(DAMAGE_BOOST_UPGRADE, 1f);
        upgrades.Add(SPEED_BOOST_UPGRADE, 1f);
        upgrades.Add(JUMP_BOOST_UPGRADE, 1f);
    }

    public bool CanDoubleJump() {
        return Convert.ToBoolean(upgrades[JUMP_UPGRADE]);
    }

    public void UnlockDoubleJump() {
        print("xisd?");
        upgrades[JUMP_UPGRADE] = 1f;
    }

    public bool CanShoot() {
        return Convert.ToBoolean(upgrades[SHOOT_UPGRADE]);
    }

    public void UnlockShoot() {
        print("xisd? xute");
        upgrades[SHOOT_UPGRADE] = 1f;
    }

    public float DamageMultiplier() {
        return upgrades[DAMAGE_BOOST_UPGRADE];
    }

    public float SpeedMultiplier() {
        return upgrades[SPEED_BOOST_UPGRADE];
    }

    public float JumpMultiplier() {
        return upgrades[JUMP_BOOST_UPGRADE];
    }

    public void Upgrade(string upgrade) {
        switch (upgrade) {
            case DAMAGE_BOOST_UPGRADE:
                upgrades[DAMAGE_BOOST_UPGRADE] += 0.1f;
                break;
            case SPEED_BOOST_UPGRADE:
                upgrades[SPEED_BOOST_UPGRADE] += 0.1f;
                break;
            case JUMP_BOOST_UPGRADE:
                upgrades[JUMP_BOOST_UPGRADE] += 0.1f;
                break;
            default:
                Console.WriteLine("Invalid Upgrade");
                break;
        }
    }

    public void LoadNextLevel() {
        if (level + 1 < SceneManager.sceneCountInBuildSettings) {
            StartCoroutine(LoadNextLevelAsync(++level));
            switch (level) {
                case 2:
                    UnlockDoubleJump();
                    break;
                case 3:
                    UnlockShoot();
                    UnlockDoubleJump();
                    break;
                default:
                    break;
            }
            UIManager.Instance.ShowHUD(true);
        } else {
            print("End Game!");
        }
        if (level == SceneManager.sceneCountInBuildSettings - 1) {
            UIManager.Instance.ShowHUD(false);
        }
    }

    public void LoadNextLevel(string sceneName) {
        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            StartCoroutine(LoadNextLevelAsync(sceneName));
        } else {
            Debug.LogWarning("Scene: " + sceneName + "does not exist!");
        }
    }

    private IEnumerator LoadNextLevelAsync(int sceneIndex) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone) {
            print(asyncLoad.progress);
            yield return null;
        }

        print(asyncLoad.progress);
        print("The Scene was loaded!");
    }

    private IEnumerator LoadNextLevelAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            print(asyncLoad.progress);
            yield return null;
        }

        print(asyncLoad.progress);
        print("The Scene was loaded!");
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadNextLevelAsync(0));
        level = 0;
        UIManager.Instance.ShowHUD(false);
    }

    public void PauseGame(bool pause) {
        IsPaused = pause;
        if (pause) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
        UIManager.Instance.ShowPanelPause(pause);
    }
}


