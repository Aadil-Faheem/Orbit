﻿using System.Linq;
using Physics;
using UnityEngine;

namespace Celestial
{
    /**
     * Class for a celestial body which has rigidbody, gravitates to other celestial bodies and has an orbit trail
     */
    [RequireComponent(typeof(Rigidbody))]
    public class CelestialBody : AcceleratedMonoBehaviour
    {
        public new Rigidbody rigidbody;

        public new string name;
        public float radius; // Is needed to compute size of a planet. I can somehow get this data from renderers, but for now this will do
        public bool isStationary; // I want the Sun to always be at 0, 0, 0. I can do it with moving sun, but it will ease the numbers
        public float gravityScale;

        private Gravitatable gravitatable;

        // Nested objects
        private Orbit orbit;

        private new void Awake()
        {
            base.Awake();

            rigidbody = GetComponent<Rigidbody>();

            gravitatable = new Gravitatable(rigidbody, FindObjectsOfType<CelestialBody>().Where(body => body != this).ToArray(), true);
            orbit = new Orbit(rigidbody.position, Color.white);
        }

        private void FixedUpdate()
        {
            if (isStationary)
            {
                return;
            }

            gravitatable.ApplyGravity();
            DrawOrbit();
        }

        private void DrawOrbit()
        {
            if (isStationary)
            {
                return;
            }

            orbit.Draw();
            orbit.Update(this);
        }
    }
}
