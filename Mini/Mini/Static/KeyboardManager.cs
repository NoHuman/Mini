using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mini.Static
{
    internal static class KeyboardManager
    {
        private static TimeSpan _duration = new TimeSpan(0, 0, 0, 0, 50);
        private static Dictionary<Keys, DateTime> _keys = new Dictionary<Keys, DateTime>();

        public static Keys[] PressedKeys()
        {
            var pressedKeys = new List<Keys>();
            foreach (var key in _keys)
            {
                if (IsKeyDown(key.Key) && (DateTime.Now - key.Value) > _duration)
                {
                    pressedKeys.Add(key.Key);
                }
            }
            return pressedKeys.ToArray();
        }

        public static bool IsKeyDown(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key))
            {
                var pressed = _keys[key];
                return (DateTime.Now - pressed) > _duration;
            }
            return false;
        }

        public static void Update()
        {
            foreach (var key in _keys)
            {
                if (!Keyboard.GetState().IsKeyDown(key.Key) || (DateTime.Now - key.Value) > _duration)
                {
                    _keys.Remove(key.Key);
                }
            }
            foreach (var pressedKey in Keyboard.GetState().GetPressedKeys())
            {
                if (!_keys.ContainsKey(pressedKey))
                {
                    _keys[pressedKey] = DateTime.Now;
                }
            }
        }
    }
}