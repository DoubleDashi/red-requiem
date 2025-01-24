using System;
using Configs;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Morph
    {
        public MorphConfig config;
        public Transform pivotPoint;
        public Transform collisionPoint;
        public LineRenderer lineRenderer;
    }
}