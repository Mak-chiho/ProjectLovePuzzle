﻿using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Giro
{
    public class LoadLevelFromDef : AbstractState
    {
        public readonly LevelDefinition m_LevelDefinition;
        readonly SceneController m_SceneController;
        readonly GameObject[] m_ManagerPrefabs;

        public LoadLevelFromDef(SceneController sceneController, AbstractLevelData levelData, GameObject[] managerPrefabs)
        {
            if (levelData is LevelDefinition levelDefinition)
                m_LevelDefinition = levelDefinition;

            m_ManagerPrefabs = managerPrefabs;
            m_SceneController = sceneController;
        }

        public override IEnumerator Execute()
        {
            if (m_LevelDefinition == null)
                throw new Exception($"{nameof(m_LevelDefinition)} is null!");

            yield return m_SceneController.LoadNewScene(nameof(m_LevelDefinition));
            // Load managers specific to the level
            foreach (var prefab in m_ManagerPrefabs)
            {
                //GameObject.Instantiate(prefab);
                GameObject go = (GameObject)Resources.Load("LoadLevelFromDef/" + prefab.name);
                GameObject.Instantiate(Resources.Load("LoadLevelFromDef/" + prefab.name));
            }

            GameManager.Instance.LoadLevel(m_LevelDefinition);
        }
    }
}