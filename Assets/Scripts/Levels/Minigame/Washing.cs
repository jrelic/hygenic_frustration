using UnityEngine;
using UnityEngine.UI;

public class Washing : MonoBehaviour
{
    public Image SpongeImage;

    public Sprite[] SpongeSprites;

    public Image PatientImage;

    public Sprite[] PatientSprites;

    public int NumberOfUsesLeft;

    public int MaxNumberOfUses;

    public int CurrentWashesPerformed;

    public int WashesNeeded;

    private void OnEnable()
    {
        CurrentWashesPerformed = 0;
    }

    public void Refill()
    {
        NumberOfUsesLeft = MaxNumberOfUses;
        SpongeImage.sprite = SpongeSprites[NumberOfUsesLeft];
    }

    public void PerformWash()
    {
        if(NumberOfUsesLeft > 0)
        {
            NumberOfUsesLeft--;
            CurrentWashesPerformed++;
            SpongeImage.sprite = SpongeSprites[NumberOfUsesLeft % SpongeSprites.Length];
            PatientImage.sprite = PatientSprites[CurrentWashesPerformed % PatientSprites.Length];

            if(CurrentWashesPerformed >= WashesNeeded)
            {
                TaskManager.Inst.CompleteCurrentTask();
                SpongeImage.sprite = SpongeSprites[0];
                PatientImage.sprite = PatientSprites[0];
            }
        }
    }
}
