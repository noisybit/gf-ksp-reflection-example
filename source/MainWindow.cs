using KSPPluginFramework;
using UnityEngine;

namespace GameframerReflection
{
    /* KAMR : Kerbal Automated Mission Recorder */
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    [WindowInitials(Caption = "Gameframer Reflection", Visible = true, DragEnabled = true, ClampToScreen = true, TooltipsEnabled = true)]
    public class MainWindow : MonoBehaviourWindow
    {
        public static int MAIN_WIDTH = 230;
        public static int MAIN_HEIGHT = 100;
        private static string ID = "GF_TEST";
        private static string EVENT_NAME = "onFoobar";
        GFInterface si;

        #region Overrides
        internal override void Awake()
        {
            WindowRect = new Rect(200, 200, MAIN_WIDTH, MAIN_HEIGHT);
            Visible = true;
            UnityEngine.Debug.Log("Tester awake, si = " + si);
            si = gameObject.AddComponent<GFInterface>();
        }

        internal override void OnDestroy()
        {
        }

        internal override void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Register"))
            {
                si.RegisterEvent(ID, EVENT_NAME);
            }
            if (GUILayout.Button("Deregister"))
            {
                si.DeregisterEvent(ID, EVENT_NAME);
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Do it!"))
            {
                si.CaptureEvent(ID, EVENT_NAME, "this is a description");
            }
            /*if (GUILayout.Button("Register Random"))
            {
                si.RegisterEvent(ID, EVENT_NAME + Random.Range(0, 10000));
            }*/
            GUILayout.Label("Last: " + si.GetLastStatus());

            GUILayout.EndVertical();
        }
        #endregion
    }
}
