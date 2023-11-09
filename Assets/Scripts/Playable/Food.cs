using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Player.ScoreItem
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider2D;

        [SerializeField] private ParticleSystem smile;
        [SerializeField] private ParticleSystem inGameSmile;



        private Bounds bounds;

        [System.Obsolete]
        private void Start()
        {
            InitPaticleSmile();
            bounds = boxCollider2D.bounds;
            RandomizePosition();
        }

        [System.Obsolete]
        private void InitPaticleSmile()
        {
            if (inGameSmile == null)
            {
                smile = Instantiate(smile);

                smile.playOnAwake = false;

                smile.Stop();
            }
        }

        public void RandomizePosition()
        {
            smile.transform.position = transform.position;
            smile.Play();

            bounds = boxCollider2D.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y =  Random.Range(bounds.min.y, bounds.max.y);

            transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y)); 
        }
    }
}
