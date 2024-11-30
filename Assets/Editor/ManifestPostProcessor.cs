using System.Xml;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ManifestPostProcessor : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        // Verifica que estamos construyendo para Android
        if (report.summary.platform != BuildTarget.Android)
            return;

        // Ruta al AndroidManifest generado por Unity
        string manifestPath = report.summary.outputPath + "/src/main/AndroidManifest.xml";

        // Carga y modifica el manifiesto
        UpdateManifest(manifestPath);
    }

    private void UpdateManifest(string manifestPath)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(manifestPath);

        // Encuentra el nodo <manifest>
        XmlNode manifestNode = xmlDocument.SelectSingleNode("/manifest");

        // Encuentra todos los nodos <uses-permission>
        XmlNodeList permissionNodes = manifestNode.SelectNodes("uses-permission");

        // Itera y elimina permisos específicos
        foreach (XmlNode node in permissionNodes)
        {
            XmlAttribute nameAttribute = node.Attributes["android:name"];
            if (nameAttribute != null)
            {
                // Permisos a eliminar, puedes agregar más si es necesario
                if (nameAttribute.Value == "android.permission.READ_PHONE_STATE" ||
                    nameAttribute.Value == "android.permission.WRITE_EXTERNAL_STORAGE" ||
                    nameAttribute.Value == "android.permission.READ_EXTERNAL_STORAGE")
                {
                    manifestNode.RemoveChild(node);
                    Debug.Log($"Permiso {nameAttribute.Value} eliminado del manifiesto.");
                }
            }
        }

        // Guarda los cambios
        xmlDocument.Save(manifestPath);
        Debug.Log("Modificaciones al AndroidManifest guardadas.");
    }
}

