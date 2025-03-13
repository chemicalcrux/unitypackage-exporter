using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ChemicalCrux.UnityPackageExporter.Editor
{
    [CreateAssetMenu(menuName = "chemicalcrux/UnityPackage Exporter/Package Definition")]
    public class PackageDefinition : ScriptableObject
    {
        public string fileName;
        public string version;
        public List<DefaultAsset> roots;

        [ContextMenu("Export")]
        public void Export()
        {
            IEnumerable<string> guids = Enumerable.Empty<string>();

            foreach (var root in roots)
            {
                var path = AssetDatabase.GetAssetPath(root);
                path += "/**";
                
                guids = guids.Concat(AssetDatabase.FindAssets($"glob:\"{path}\""));
            }
            
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();

            string result = $"{fileName} v{version}.unitypackage";
            AssetDatabase.ExportPackage(paths, result, ExportPackageOptions.Recurse);
        }
    }
}
