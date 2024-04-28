using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(WaypointManager))]
public class WaypointManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        WaypointManager manager = (WaypointManager)target;
        Handles.color = Color.blue;
        foreach (Waypoint way in manager.Waypoints)
        {
            Vector3 newpos = Handles.PositionHandle(way.Position, Quaternion.identity);
            if (way.Position != newpos)
            {
                way.Position = newpos;
            }



        }
    }
    public override void OnInspectorGUI()
    {
        WaypointManager manager = (WaypointManager)target;

        manager.debugType = (DebugType)EditorGUILayout.EnumPopup("Debug Type", manager.debugType);
        if (manager.debugType == DebugType.General || manager.debugType == DebugType.Mixed)
        {

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("GeneralConfig"), true);
            serializedObject.ApplyModifiedProperties();

        }
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Waypoints"), true);
        serializedObject.ApplyModifiedProperties();



    }


}
// modifico como se dibuja la clase waypoint lo que modifica como se ve en una lista
[CustomPropertyDrawer(typeof(Waypoint))]
public class WaypointDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), //permito que se pueda plegar cada elemento de la lista
            property.isExpanded, GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x, position.y - 2f, 300, EditorGUIUtility.singleLineHeight), "Waypoint " + GetIndexNum(property.displayName)); // dibujo el titulo
        EditorGUI.indentLevel++;

        // Dibuja solo la etiqueta del waypoint sin el campo Position si el elemento está plegado

        if (property.isExpanded)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight,
                position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("Position"), GUIContent.none); // dibujo la variable posicion

            WaypointManager manager = (WaypointManager)property.serializedObject.targetObject; // consigo referencia al manager

            // Mostrar la variable SingularConfig solo si debugType es DebugType.Singular
            if (manager.debugType == DebugType.Singular)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, // dubujo la clase serrialisada 
                    position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("SingularConfig"), true);
            }
            if (manager.debugType == DebugType.Mixed)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, // dubujo use singular
                    position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("UseSingular"));
                if (property.FindPropertyRelative("UseSingular").boolValue)
                {
                    EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing, // dubujo la clase serrialisada 
                        position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("SingularConfig"), true);
                }
            }
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            WaypointManager manager = (WaypointManager)property.serializedObject.targetObject; //obtengo referencia al manager
            float totalHeight = EditorGUIUtility.singleLineHeight * 2;  // calculo la altura si el elemento de la lista esta desplegado

            if (manager.debugType == DebugType.Singular)
            {
                totalHeight += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("SingularConfig")) + EditorGUIUtility.standardVerticalSpacing; // calculo la altura total si la clase serialisada esta dibujada
            }
            else if (manager.debugType == DebugType.Mixed)
            {
                totalHeight += EditorGUIUtility.singleLineHeight;
                if (property.FindPropertyRelative("UseSingular").boolValue)
                {

                    totalHeight += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("SingularConfig")) + EditorGUIUtility.standardVerticalSpacing;
                }
                totalHeight += 5;
            }

            return totalHeight;
        }

        return EditorGUIUtility.singleLineHeight; // devuelvo 1 sola linea de altura si el elemento no esta desplegado
    }
    private string GetIndexNum(string displayname)
    {
        return displayname[displayname.Length - 1].ToString(); // esto es pa sacar el numero perque el displey name te da element (num) y con esta linea saco solo el numero
    }
}





