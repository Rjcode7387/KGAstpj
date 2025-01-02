using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    GameObject CannonProjectilePrefab = null;

    [SerializeField]
    Transform CannonStartPoint = null;

    [SerializeField]
    float CannonLaunchSpeed = 50.0f;

    [SerializeField]
    GameObject CannonExplosionEffectPrefab;

    [SerializeField]
    private float rotationSpeed = 100f;

    [SerializeField]
    XRJoystick joystick;

    [SerializeField]
    XRPushButton pushButton;

    void Start()
    {
        if (pushButton != null)
        {
            pushButton.onPress.AddListener(Fire);
        }
        if (joystick != null)
        {
            joystick.onValueChangeX.AddListener(RotateCannon);
        }
    }
    void RotateCannon(float xValue)
    {

        float rotationAmount = xValue * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }

    public void Fire()
    {
        GameObject newObject = Instantiate(CannonProjectilePrefab, CannonStartPoint.position, CannonStartPoint.rotation, null);

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
        {

            rigidBody.AddForce(CannonStartPoint.forward * CannonLaunchSpeed, ForceMode.Impulse);

            var projectile = newObject.AddComponent<CannoBall>();
            projectile.CannonExplosinEffect = CannonExplosionEffectPrefab;
        }

    }
}
