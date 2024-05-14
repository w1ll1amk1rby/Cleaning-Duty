using UnityEngine;
[RequireComponent(typeof(Item))]
/**
    describes an item as washable
*/
public class Washable : MonoBehaviour
{
    [SerializeField] private Item washedVersion;
    [SerializeField] private GameObject[] dirtParticles;
    [SerializeField] private float currentDirt;
    [SerializeField] private float maxDirt;

    public void Awake()
    {
        this.CapDirt();
        this.UpdateDirtParticles();
    }
    public bool Clean(float amount)
    {
        this.currentDirt -= amount;
        this.CapDirt();
        this.UpdateDirtParticles();
        return this.IsClean();
    }
    public bool IsClean()
    {
        return this.currentDirt <= 0;
    }
    public float GetDirtPercentage()
    {
        return this.currentDirt / this.maxDirt;
    }
    public Item GenerateWashedVersion() {
        return Instantiate(this.washedVersion, new Vector3(0, 0, 0), Quaternion.identity);
    }
    private void CapDirt()
    {
        if (this.currentDirt > this.maxDirt)
        {
            this.currentDirt = this.maxDirt;
        }
        else if (this.currentDirt < 0)
        {
            this.currentDirt = 0;
        }
    }
    private void UpdateDirtParticles()
    {
        int showCount = Mathf.CeilToInt(this.GetDirtPercentage()*this.dirtParticles.Length);
        for (int i = 0; i < this.dirtParticles.Length; i++)
        {
            GameObject particle = this.dirtParticles[i];
            particle.SetActive(i < showCount);
        }
    }
}
