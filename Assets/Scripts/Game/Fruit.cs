using System;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float value;

        public float Value => value;

        private Color defaultColor;

        private void Awake()
        {
            defaultColor = spriteRenderer.color;
        }

        public void ChangeColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void RevertColor()
        {
            ChangeColor(defaultColor);
        }
    }
}