using System.Collections;
using UnityEngine;

namespace Com.WIAP.Checkers
{
    /// taken from https://stackoverflow.com/questions/67704820/how-do-i-print-unitys-debug-log-to-the-screen-gui
   
    public class DebugLog : MonoBehaviour
    {
        uint qsize = 15;  // number of messages to keep
        Queue myLogQueue = new Queue();

        void Start()
        {
            Debug.Log("Started up logging.");
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            string formattedLog = "[" + type + "] : " + logString;

            if (!myLogQueue.Contains(formattedLog))
            {
                myLogQueue.Enqueue(formattedLog);

                if (type == LogType.Exception)
                    myLogQueue.Enqueue(stackTrace);

                while (myLogQueue.Count > qsize)
                    myLogQueue.Dequeue();
            }
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()));
            GUILayout.EndArea();
        }
    }
}