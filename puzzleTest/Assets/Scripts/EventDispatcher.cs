using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvtSystem
{
    public class Event
    {

    }

    //event dispatcher is a singleton because we only want one dispatcher
    //seems fairly self explanatory why
    public class EventDispatcher {

        //create some instance of event dispatcher class
        //make it static so that its everywhere (not just in EventDispatcher.cs)
        private static EventDispatcher _instance = null;

        //capital instance is a function (creates an instance of Event dispatcher)
        //programmed so that: if not one, craete one

        public static EventDispatcher Instance {
            get {
                if (_instance == null) {
                    _instance = new EventDispatcher();
                }
                return _instance;
            } 
        }



        //
        #region

        //so far the code is not really well understood to us (or just me)
        //the T is a template? 
        //edit details of T 
        //t can be any class . T is a placeholder
        //however we delegate T to only be an event class or its derivatives
        public delegate void EventDelegate<T>(T e) where T : Event;

        private Dictionary<System.Type, System.Delegate> m_eventDelegates = 
            new Dictionary<System.Type, System.Delegate>();

        private void _AddListener<T>(EventDelegate<T> listener) where T : Event
        {
            System.Delegate del;
            if (m_eventDelegates.TryGetValue(typeof(T), out del)) {
                m_eventDelegates[typeof(T)] = System.Delegate.Combine(del, listener);
            } else {
                m_eventDelegates[typeof(T)] = listener;
            }
        }

        private void _RemoveListener<T>(EventDelegate<T> listener) where T : Event
        {
            System.Delegate del;

            if (m_eventDelegates.TryGetValue(typeof(T), out del)) {

                System.Delegate newDel = System.Delegate.Remove(del, listener);

                if (newDel == null) {
                    m_eventDelegates.Remove(typeof(T));
                } else {
                    m_eventDelegates[typeof(T)] = newDel;
                }
            }
        }

        public void _Raise<T>(T evtData) where T : Event
        {
            System.Delegate del;
            if (m_eventDelegates.TryGetValue(typeof(T), out del))
            {
                EventDelegate<T> callback = del as EventDelegate<T>;
                if (callback != null)
                {
                    callback(evtData);
                }
            }
        }

        public static void Raise<T>(T evtData) where T: Event
        {
            Instance._Raise(evtData);
        }

        public static void AddListener<T>(EventDelegate<T> listener) where T: Event
        {
            Instance._AddListener(listener);
        }

        public static void RemoveListener<T>(EventDelegate<T> listener) where T : Event
        {
            Instance._RemoveListener(listener);
        }

        #endregion
    }
}