//遍历所选文件夹，查找该文件夹以及子文件夹中 后缀为 .prefab的文件路径

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace MatchEngine
{
    public class AssetManager : MonoBehaviour
    {

        [SerializeField] public List<TileTypeAsset> tileTypeContainer;
        [SerializeField] public List<TextTypeAsset> textTypeContainer;
        [SerializeField] public List<TextTypeAsset> objTextContainer;
        [SerializeField] public List<TextTypeAsset> opTextContainer;
        [SerializeField] public List<TextTypeAsset> propTextContainer;

        public void CheckSceneSetting()
        {
            List<string> dirs = new List<string>();
            tileTypeContainer = new List<TileTypeAsset>();
            textTypeContainer = new List<TextTypeAsset>();
            objTextContainer = new List<TextTypeAsset>();
            opTextContainer = new List<TextTypeAsset>();
            propTextContainer = new List<TextTypeAsset>();
            GetDirs(Application.dataPath + "/Tile_Types/NotRealElimination", ref dirs);
        }
        //参数1 为要查找的总路径， 参数2 保存路径
        private void GetDirs(string dirPath, ref List<string> dirs)
        {
               
            foreach (string path in Directory.GetFiles(dirPath+"/TileAsset"))
            {
                //获取所有文件夹中包含后缀为 .asset 的路径
                if (Path.GetExtension(path) == ".asset")
                {
                    dirs.Add(path.Substring(path.IndexOf("Assets")));
                    tileTypeContainer.Add(AssetDatabase.LoadAssetAtPath<TileTypeAsset>(path.Substring(path.IndexOf("Assets"))));
                }
            }
            foreach (string path in Directory.GetFiles(dirPath + "/TextAsset"))
            {
                //获取所有文件夹中包含后缀为 .asset 的路径
                if (Path.GetExtension(path) == ".asset")
                {
                    dirs.Add(path.Substring(path.IndexOf("Assets")));
                    textTypeContainer.Add(AssetDatabase.LoadAssetAtPath<TextTypeAsset>(path.Substring(path.IndexOf("Assets"))));
                }
            }

            /*if (Directory.GetDirectories(dirPath).Length > 0)  //遍历所有子文件夹
            {
                foreach (string path in Directory.GetDirectories(dirPath))
                {
                    GetDirs(path, ref dirs);
                }
            }*/
        }
    }
}