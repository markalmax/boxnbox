using UnityEngine;
using Unity.Netcode;

namespace Actors
{
    public class ActorEffects : NetworkBehaviour
    {
        private bool isRight;
        private SpriteRenderer sr;
        private Color color;
        private Vector2 scale;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            color = sr.color;
            scale = base.transform.localScale;
        }

        public void FlipSprite(int dir)
        {
            if ((isRight && dir < 0) || (!isRight && dir > 0))
            {
                isRight = !isRight;
                sr.flipX = !sr.flipX;
            }
        }

        public void DamageFlash()
        {
            if (sr == null) return;
            sr.color = new Color(1f, 1f, 1f, 1f);
            Invoke(nameof(ResetColor), 0.1f);
        }

        private void ResetColor()
        {
            if (sr != null)
            {
                sr.color = color;
            }
        }
    }
}