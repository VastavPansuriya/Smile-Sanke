

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake2D.Player.ScoreItem
{


    public class FoodPower : FoodPlace
    {

        public static Action<FoodPower> OnStartSlider;

        [Header("PowerSettings")]
        [SerializeField] private Vector2 randomizeValueSpawnTime;

        public float coolDownTime;

        [Header("PowerColor")]
        [SerializeField] private List<Color> powerColor = new List<Color>();

        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Player Reference")]
        [SerializeField] private Snake player;
        [SerializeField] private GameObject powerObj;

        [HideInInspector] public Power power;


        [System.Obsolete]
        protected override void Start()
        {
            base.Start();
            StartCoroutine(SpawnPower());
        }

        public IEnumerator SpawnPower()
        {
            SetRandomPower();
            GetColorAccordingEnum();
            powerObj.transform.position = RandomizePosition(powerObj.transform);
            powerObj.SetActive(false);

            float seconds = UnityEngine.Random.Range(randomizeValueSpawnTime.x, randomizeValueSpawnTime.y);
            yield return new WaitForSeconds(seconds);
            powerObj.SetActive(true);

            StartCoroutine(CoolDown());
        }

        private IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(coolDownTime);

            if (player is null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Snake>();
            }
            player.TurnOffPower();

        }

        private void SetRandomPower()
        {
            Power[] allEnumValues = (Power[])System.Enum.GetValues(typeof(Power));

            int randomIndex = UnityEngine.Random.Range(0, allEnumValues.Length);

            power = allEnumValues[randomIndex];
        }

        private void GetColorAccordingEnum()
        {
            spriteRenderer.color = powerColor[(int)power];
        }

        public Color GetColor()
        {
            Color color = powerColor[(int)power];
            return color;
        }
    }
    public enum Power
    {
        Score,
        Speed,
        Shield
    }
}
