using Snake2D.Enum;
using Snake2D.Player.GameOver;
using Snake2D.Player.ScoreItem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake2D.Player
{
    public class Snake : MonoBehaviour
    {
        public static Action<int> OnGrabFood;

        [SerializeField] private Transform bodyPf;

        [SerializeField] private Gradient gradient;

        [SerializeField] private BoxCollider2D bounds;

        private float moveTimer = 0f;
        [SerializeField] private float moveInterval = 0.1f;
        [SerializeField] private float moveIntervalSpeedUp = 0.1f;

        private Vector2 moveDir = Vector2.right;

        private List<Transform> parts = new List<Transform>();

        private List<SpriteRenderer> partsSprites = new List<SpriteRenderer>();

        protected bool isLoss;

        //----------------------------------------------Boost
        [HideInInspector] public bool isScoreBoost;

        [HideInInspector] public bool isSpeedUp;

        [HideInInspector] public bool isShield = false;

        [SerializeField] private FoodPower foodPower;

        private void Start()
        {
            isShield = false;

            parts.Add(transform);
            partsSprites.Add(GetComponent<SpriteRenderer>());
            UpdateSnakeColor();

        }

        public void Update()
        {
            if (isLoss)
            {
                return;
            }
            MovementInput();


            moveTimer += Time.deltaTime;
            float speed = isSpeedUp ? moveIntervalSpeedUp : moveInterval;
            if (moveTimer >= speed)
            {
                PartsMovement();

                HeadMovement();

                IfDead();

                moveTimer = 0f; // Reset the timer after moving
            }
        }

        private void PartsMovement()
        {
            for (int i = parts.Count - 1; i > 0; i--)
            {
                parts[i].position = parts[i - 1].position;
            }
        }

        private void HeadMovement()
        {
            Vector3 movement = new Vector3(Mathf.Round(transform.position.x) + moveDir.x, Mathf.Round(transform.position.y) + moveDir.y);

            transform.position = movement;

            WrapScreen();
        }

        private void UpdateSnakeColor()
        {
            for (int i = 0; i < partsSprites.Count; i++)
            {
                // Calculate the normalized index of this part in the list
                float normalizedIndex = (float)i / (float)partsSprites.Count;
                // Get the color from the gradient using the normalized index
                partsSprites[i].color = gradient.Evaluate(normalizedIndex);
            }
        }

        public void IfDead()
        {
            isLoss = CheckDead();

            if (isLoss)
            {
                GameOverManager.Instace.OnDie();
                return;
            }
        }

        public bool CheckDead()
        {
            if (isShield)
            {
                Debug.Log("EveryTime");
                return false;
            }

            for (int i = 1; i < parts.Count; i++)
            {
                if (transform.position == parts[i].position)
                {
                    return true;
                }
            }
            return false;
        }

        private void MovementInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && moveDir != Vector2.down)
            {
                moveDir = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.A) && moveDir != Vector2.right)
            {
                moveDir = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.S) && moveDir != Vector2.up)
            {
                moveDir = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.D) && moveDir != Vector2.left)
            {
                moveDir = Vector2.right;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out FoodPlace food))
            {
                OnGrabFood?.Invoke(isScoreBoost ? 2 : 1);
                AudioManager.Instance.PlayEffect(SoundType.Food);
                food.RandomizePosition(food.transform);
                AddPart();
            }

            if (other.CompareTag("Power"))
            {
                FoodPower.OnStartSlider?.Invoke(foodPower);
                AudioManager.Instance.PlayEffect(SoundType.Food);
                switch (foodPower.power)
                {
                    case Power.Score:
                        isScoreBoost = true;
                        break;
                    case Power.Speed:
                        isSpeedUp = true;
                        break;
                    case Power.Shield:
                        isShield = true;
                        break;
                    default:
                        break;
                }

                StartCoroutine(foodPower.SpawnPower());
            }
        }

        private void AddPart()
        {
            Transform item = Instantiate(bodyPf, parts[parts.Count - 1].position, Quaternion.identity);

            parts.Add(item);

            if (parts.Count < 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    item.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }

            ManageSprite(item);
        }

        private void ManageSprite(Transform item)
        {
            SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();

            partsSprites.Add(sprite);

            sprite.sortingOrder = -parts.Count;

            UpdateSnakeColor();
        }

        private void WrapScreen()
        {

            if (transform.position.x > bounds.bounds.max.x)
            {
                transform.position = new Vector3(bounds.bounds.min.x, transform.position.y);
            }

            if (transform.position.y > bounds.bounds.max.y)
            {
                transform.position = new Vector3(transform.position.x, bounds.bounds.min.y);

            }

            if (transform.position.x < bounds.bounds.min.x)
            {
                transform.position = new Vector3(bounds.bounds.max.x, transform.position.y);

            }

            if (transform.position.y < bounds.bounds.min.y)
            {
                transform.position = new Vector3(transform.position.x, bounds.bounds.max.y);
            }

        }
        public void TurnOffPower()
        {
            isShield = false;
            isScoreBoost = false;
            isSpeedUp = false;
        }
    }


}
