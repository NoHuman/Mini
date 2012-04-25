namespace Mini.Controls
{
    using System.Collections.Generic;

    public class Pulse<T>
    {
        private List<T> _list;
        private int _duration;

        public Pulse(int duration)
        {
            _duration = duration;
            _list = new List<T>();
        }

        public void Add(T element)
        {
            _list.Add(element);
        }

        public T Next(int timer)
        {
            return _list[timer/_duration%_list.Count];
        }
    }
}