﻿using UnityEngine;

namespace traVRsal.SDK
{
    [RequireComponent(typeof(Renderer))]
    public class ImageAssignment : ExecutorConfig
    {
        public enum ImageSource
        {
            VARIABLE, NAME
        }

        public int materialIndex = 0;
        public ImageSource source = ImageSource.VARIABLE;
        public string variable;
    }
}