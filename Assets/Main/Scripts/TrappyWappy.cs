using System;
using Bubble.Enemies;
using UnityEngine;

namespace Bubble
{
    public class TrappyWappy : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            try
            {
                other.gameObject.GetComponent<GenericAhEnemy>().Die();
                print("DIE DIE DIE");
            }
            catch (UnityException e)
            { }
        }
    }
}
