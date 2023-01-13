using UnityEngine;
using MonkeBazooka.Utils;

namespace MonkeBazooka.Core
{
    public class MissileController : MonoBehaviour
    {
        public Rigidbody MissileRigidbody;

        public GameObject GorillaPlayer;
        public Rigidbody PlayerRigidBody;

        public Transform raycastPoint;

        public AudioSource MissileSpeaker;

        internal void Awake()
        {
            if (MissileRigidbody == null)
            {
                MissileRigidbody = GetComponent<Rigidbody>();
            }
            
            MissileSpeaker = GetComponentInChildren<AudioSource>();

            raycastPoint = transform.Find("raycastPoint");

            GorillaPlayer = GorillaLocomotion.Player.Instance.gameObject;
            PlayerRigidBody = GorillaPlayer.GetComponent<Rigidbody>();         
            
            MissileRigidbody.velocity = GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity * 0.9f;
            
            Destroy(gameObject, 5.99f);
            
            MissileSpeaker.PlayOneShot(MissileSpeaker.clip);
        }

        internal void FixedUpdate()
        {
            Collider[] CollidedObjects = Physics.OverlapBox(transform.position, BazookaController.missileSize, transform.rotation, MBUtils.MissileLayerMask);
            if (CollidedObjects.Length > 0) Explode();
            MissileRigidbody.AddForce(-transform.right * BazookaController.MissileSpeed, ForceMode.VelocityChange);
        }

        private void Explode()
        {
            GameObject ClonedExplosion = Instantiate<GameObject>(MBUtils.ExplosionPrefab, transform.position, transform.rotation);
            Knockback();
            Destroy(ClonedExplosion, 5.0f);
            Destroy(gameObject);
        }

        private void Knockback()
        {
            LayerMask KnockbackLayerMask = LayerMask.GetMask("Gorilla Body Collider");
            Collider[] NearbyColliders = Physics.OverlapSphere(transform.position, BazookaController.ExplosionRadius, KnockbackLayerMask);
            
            foreach (Collider Collider in NearbyColliders)
            {
                if (Collider.gameObject.name.Contains("Body") || Collider.gameObject == GorillaLocomotion.Player.Instance.bodyCollider)
                {
                    PlayerRigidBody.AddExplosionForce(MBConfig.ExplosionForce * 10000, transform.position, BazookaController.ExplosionRadius);
                }
            }
        }
    }
}
