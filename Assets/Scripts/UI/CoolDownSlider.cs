using Snake2D.Player.ScoreItem;
using Snake2D.Player;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownSlider : MonoBehaviour
{

    private bool isSliderStart;
    [SerializeField] private Image sliderImage;
    private FoodPower curruntFood;

    public float minValue = 1f;
    public float maxValue = 50f;
    public float fillTimeInSeconds = 5f;

    private float coolDownTime;

    private void Awake()
    {
        isSliderStart = false;
    }

    private void OnEnable() => FoodPower.OnStartSlider += StartSlider;
    private void Start()
    {
        sliderImage.fillAmount = 0;
    }
    private void Update()
    {
        if (isSliderStart == true)
        {
            FillSlider();
        }

    }

    private void FillSlider()
    {
        float fillAmount = (coolDownTime / curruntFood.coolDownTime);

        coolDownTime += Time.deltaTime;

        sliderImage.fillAmount = fillAmount;


        if (sliderImage.fillAmount >= 1)
        {
            isSliderStart = false;
            sliderImage.fillAmount = 0;
        }
    }

    private void StartSlider(FoodPower curruntFood)
    {
        sliderImage.color = curruntFood.GetColor();

        this.curruntFood = curruntFood;

        coolDownTime = 0;

        isSliderStart = true;
    }

    private void OnDisable()
    {
        FoodPower.OnStartSlider -= StartSlider;
    }

}
