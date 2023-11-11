using UnityEngine;

#if  UNITY_EDITOR
using UnityEditor;
#endif

namespace Snake2D.Player.ScoreItem
{
    public class FoodPlace : MonoBehaviour
    {
        public BoxCollider2D boxCollider2D;

        public ParticleSystem smile;

        [HideInInspector] public Vector2 randomPos;

        private ParticleSystem inGameSmile;

        private Bounds bounds;

        protected virtual void Awake()
        {
            
        }

        [System.Obsolete]
        protected virtual void Start()
        {
            InitPaticleSmile();

            bounds = boxCollider2D.bounds;

            RandomizePosition(transform);
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

        public Vector3 RandomizePosition(Transform obj)
        {
            smile.transform.position = obj.position;
            smile.Play();

            bounds = boxCollider2D.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            obj.position = new Vector3(Mathf.Round(x), Mathf.Round(y));
            return obj.position;
        }
    }

/*    #region Editor

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FoodPlace))]
    public class FoodPlaceEditor : Editor
    {
        private SerializedProperty m_BoxCollider;
        private SerializedProperty m_smile;
        private SerializedProperty m_randomPos;

        private FoodPlace food;

        private void OnEnable()
        {
            food = target as FoodPlace;

            m_BoxCollider = serializedObject.FindProperty(nameof(food.boxCollider2D));
            m_smile = serializedObject.FindProperty(nameof(food.smile));
            m_randomPos = serializedObject.FindProperty(nameof(food.randomPos));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_BoxCollider);
            EditorGUILayout.PropertyField(m_smile);



            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
;
        }
    }
#endif
    #endregion*/
}
