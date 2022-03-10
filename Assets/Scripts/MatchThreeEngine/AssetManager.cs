//������ѡ�ļ��У����Ҹ��ļ����Լ����ļ����� ��׺Ϊ .prefab���ļ�·��

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
        //����1 ΪҪ���ҵ���·���� ����2 ����·��
        private void GetDirs(string dirPath, ref List<string> dirs)
        {
               
            foreach (string path in Directory.GetFiles(dirPath+"/TileAsset"))
            {
                //��ȡ�����ļ����а�����׺Ϊ .asset ��·��
                if (Path.GetExtension(path) == ".asset")
                {
                    dirs.Add(path.Substring(path.IndexOf("Assets")));
                    tileTypeContainer.Add(AssetDatabase.LoadAssetAtPath<TileTypeAsset>(path.Substring(path.IndexOf("Assets"))));
                }
            }
            foreach (string path in Directory.GetFiles(dirPath + "/TextAsset"))
            {
                //��ȡ�����ļ����а�����׺Ϊ .asset ��·��
                if (Path.GetExtension(path) == ".asset")
                {
                    dirs.Add(path.Substring(path.IndexOf("Assets")));
                    textTypeContainer.Add(AssetDatabase.LoadAssetAtPath<TextTypeAsset>(path.Substring(path.IndexOf("Assets"))));
                }
            }

            /*if (Directory.GetDirectories(dirPath).Length > 0)  //�����������ļ���
            {
                foreach (string path in Directory.GetDirectories(dirPath))
                {
                    GetDirs(path, ref dirs);
                }
            }*/
        }
    }
}