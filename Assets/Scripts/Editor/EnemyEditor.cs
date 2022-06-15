using UnityEngine;
using UnityEditor;
 
[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    //Everytime the inspector is rendered
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Enemy enemy = (Enemy) target;
 
        if(GUILayout.Button("Hit Enemy"))
        {
            enemy.LoseHealth(1);
        }

        if(GUILayout.Button("Attack"))
        {
            enemy.Attack();
        }
    }
}
