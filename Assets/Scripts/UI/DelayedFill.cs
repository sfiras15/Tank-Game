using UnityEngine;
using UnityEngine.UI;


public class DelayedFill : MonoBehaviour
{


    [SerializeField] private Health_SO healthSO;
    private Image fillBar;

    private void OnEnable()
    {
        if (healthSO != null) healthSO.onHealthChanged += UpdateBar;
       
    }
    private void OnDisable()
    {
        if (healthSO != null) healthSO.onHealthChanged -= UpdateBar;
    }
    private void Awake()
    {
        fillBar = GetComponent<Image>();


    }
    public void UpdateBar(int maxValue, int currentValue)
    {
        fillBar.fillAmount = (float)currentValue / maxValue;
    }

}
