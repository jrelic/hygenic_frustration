using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _MainMenu;

    [SerializeField]
    private GameObject _LevelSelect;

    private void Awake()
    {
        _LevelSelect.SetActive(LevelDataHolder.Inst.GameLoaded);
        _MainMenu.SetActive(!LevelDataHolder.Inst.GameLoaded);
    }

    public void OnStartGameSclicked()
    {
        _LevelSelect.SetActive(true);
        _MainMenu.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnClickGoBack()
    {
        _LevelSelect.SetActive(false);
        _MainMenu.SetActive(true);
    }
}
