using UnityEngine;
using Unity.Netcode;
namespace Players
{
    public class Player : Actors
    {
        private new void Start()
        {
            base.Start();
        }
        private void Update()
        {
            if (!IsOwner || !IsSpawned || !Application.isFocused) return;
            Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = (vector - (Vector2)transform.position).normalized;
            if (Input.GetKey(KeyCode.A))Move(-1);
			if (Input.GetKey(KeyCode.D))Move(1);
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))NotMoving();
            if (Input.GetKeyDown(KeyCode.Space))Jump();
            if (Input.GetKeyDown(KeyCode.Mouse0))Shoot();
            vector= new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 vector2 = Camera.main.ScreenToWorldPoint(vector);
            vector2 -= base.transform.position;
            float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f - 90f;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, num);
        }
    }
}