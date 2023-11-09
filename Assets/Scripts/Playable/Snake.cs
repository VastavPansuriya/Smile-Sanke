using Snake.Player.ScoreItem;
using System.Collections.Generic;
using UnityEngine;
using Snake.Enum;
using Snake.Player.GameOver;
using System;

namespace Snake.Player
{
    public class Snake : MonoBehaviour
    {
        public static Action OnGrabFood;

        [SerializeField] private Transform bodyPf;

        [SerializeField] private Gradient gradient;

        [SerializeField] private BoxCollider2D bounds;

        private Vector2 moveDir = Vector2.right;

        private List<Transform> parts = new List<Transform>();

        private List<SpriteRenderer> partsSprites = new List<SpriteRenderer>();

        protected bool isLoss;

        private void Start()
        {
            parts.Add(transform);
            partsSprites.Add(GetComponent<SpriteRenderer>());
            UpdateColor();
        }

        public void Update()
        {
            if (isLoss)
            {
                return;
            }
            MovementInput();
        }

        private void FixedUpdate()
        {
            if (isLoss)
            {
                return;
            }
            PartsMovement();

            Movement();

            IfDead();
        }

        private void PartsMovement()
        {
            for (int i = parts.Count - 1; i > 0; i--)
            {
                parts[i].position = parts[i - 1].position;
            }
        }

        private void Movement()
        {
            Vector3 movement = new Vector3(Mathf.Round(transform.position.x) + moveDir.x, Mathf.Round(transform.position.y) + moveDir.y);

            transform.position = movement;

            WrapScreen();
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
            if (other.TryGetComponent(out Food food))
            {
                OnGrabFood?.Invoke();
                AudioManager.Instance.PlayEffect(SoundType.Food);
                food.RandomizePosition();
                AddPart();
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

            SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();

            partsSprites.Add(sprite);

            sprite.sortingOrder = -parts.Count;

            UpdateColor();
        }

        private void UpdateColor()
        {
            for (int i = 0; i < partsSprites.Count; i++)
            {
                // Calculate the normalized index of this part in the list
                float normalizedIndex = (float)i / (float)partsSprites.Count;

                // Get the color from the gradient using the normalized index
                partsSprites[i].color = gradient.Evaluate(normalizedIndex);
            }
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

    }
}
