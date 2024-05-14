using UnityEngine;
using UnityEngine.UI;
/**
    a basic progress bar UI element that can be used to show progress.
*/
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private GameObject progressBar;
    public float progress = 0;
    // Update is called once per frame
    public void Update()
    {
        this.mask.fillAmount = progress;
    }
    public void SetProgressPercentage(float progress)
    {
        this.progress = progress;
    }
    public void SetShowBar(bool shouldShow)
    {
        this.progressBar.SetActive(shouldShow);
    }
}
