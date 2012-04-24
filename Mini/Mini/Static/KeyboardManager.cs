namespace Mini.Static
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal static class KeyboardManager
    {
        private static TimeSpan _duration = new TimeSpan(0, 0, 0, 0, 350);
        private static Dictionary<Keys, TimeSpan> _prevKeys = new Dictionary<Keys, TimeSpan>();
        private static List<Keys> _keys = new List<Keys>();

        public static Keys[] PressedKeys()
        {
            return _keys.ToArray();
        }

        public static void Update(GameTime time)
        {
            _keys = new List<Keys>();
            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (!_prevKeys.ContainsKey(key))
                {
                    _keys.Add(key);
                    _prevKeys[key] = time.TotalGameTime;
                }
                else
                {
                    if (time.TotalGameTime - _prevKeys[key] > _duration)
                    {
                        _prevKeys[key] = time.TotalGameTime;
                        _keys.Add(key);
                    }
                }
            }
        }
    }
}