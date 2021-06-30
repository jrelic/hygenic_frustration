using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int Id;

    [SerializeField]
    private Sprite ZeroStars;

    [SerializeField]
    private Sprite OneStar;

    [SerializeField]
    private Sprite TwoStars;

    [SerializeField]
    private Sprite ThreeStars;

    [SerializeField]
    private Image StarImage;

    [SerializeField]
    private Image Locked;

    [SerializeField]
    private Button button;

    private LevelData data;

    private void Awake()
    {
        data = LevelDataHolder.Inst.GetLevelDataForId(Id);

        if(data != null)
        {
            if(data.Locked)
            {
                StarImage.gameObject.SetActive(false);
                Locked.gameObject.SetActive(true);
                button.interactable = false;
            }
            else
            {
                StarImage.gameObject.SetActive(true);
                Locked.gameObject.SetActive(false);
                button.interactable = true;

                switch(data.StarCount)
                {
                    case 0:
                        StarImage.sprite = ZeroStars;
                        break;
                    case 1:
                        StarImage.sprite = OneStar;
                        break;
                    case 2:
                        StarImage.sprite = TwoStars;
                        break;
                    case 3:
                        StarImage.sprite = ThreeStars;
                        break;
                }

            }

        }
    }

    public void OnClick()
    {
        LevelDataHolder.Inst.GameLoaded = true;
        LevelDataHolder.Inst.SetCurrentLevelData(data);
        SceneManager.LoadScene(data.LevelName);
    }
}
