#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;

public abstract class DarkcupTab
{
	public DarkcupWindow window;
	public GUILayoutOption buttonWidth = GUILayout.Width(DarkcupWindow.BUTTON_WIDTH);
	public DarkcupTab(DarkcupWindow window)
    {
		this.window = window;
    }
	public abstract void ShowTabMain(int width);
	public abstract void ShowTabLeft(int width);
}

public class PopupTab : DarkcupTab
{
	private string inputText = "";
	private string pathText = "Scripts/Popup/";

	private Vector2 scrollPosition;
	private int currentSelect;
	List<Type> popupTypes = new List<Type>();

	Array ids;
	public PopupTab(DarkcupWindow wd) : base(wd) { }
		
	public override void ShowTabLeft(int width)
	{
		popupTypes.Clear();
		List<Type> allPopup = GetDerivedClasses<Popup>();
		List<string> names = new List<string>();
		for (int i = 0; i < allPopup.Count; i++)
		{
			popupTypes.Add(allPopup[i]);
			names.Add(allPopup[i].Name);
			Debug.Log(allPopup[i].Name);
        }
		currentSelect = GUILayout.SelectionGrid(currentSelect, names.ToArray(), 1);
	}

	public override void ShowTabMain(int width)
	{
		EditorGUILayout.Separator();

		inputText = EditorGUILayout.TextField("Enter Popup Name:", inputText);
		pathText = EditorGUILayout.TextField("Create at directory", pathText);

		string popupName = "Popup" + inputText;
		string textContent = Resources.Load<TextAsset>("ScriptTemplate/PopupTemplate").text;

		textContent = textContent.Replace("{0}", inputText);
		GUI.enabled = false;
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));
		EditorGUILayout.TextArea(textContent);
		EditorGUILayout.EndScrollView();
		GUI.enabled = true;

		if (GUILayout.Button($"Create Script \"{popupName}\""))
		{
			if (inputText == "") return;
			CheckEventSystem();
            SaveScriptData(textContent, "Popup" + inputText, pathText);
		}

		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();

		Type selectedPopup = popupTypes[currentSelect];

		if (GUILayout.Button($"Create {selectedPopup} in scene"))
		{
			CheckEventSystem();
            List<string> canvasPopupNamaes = new List<string>()
				{
					"Canvas Popup", "CanvasPopup","canvas popup","canvas Popup","canvaspopup","Popup","popup"
				};
			Canvas canvasPopup = null;
			GameObject tmp;
			int index = 0;
			while (canvasPopup == null)
			{
				tmp = GameObject.Find(canvasPopupNamaes[index]);
				if (tmp != null)
				{
					canvasPopup = tmp.GetComponent<Canvas>();
					if (canvasPopup != null) break;
				}
				index++;
				if (index >= canvasPopupNamaes.Count) break;
			}
			var prefab = Resources.Load<GameObject>("PrefabTemplate/PopupPrefabTemplate");
			if (prefab == null)
			{
				Debug.LogError("could not found prefab at: " + "PrefabTemplate/PopupPrefabTemplate");
			}
			prefab.name = "Popup" + inputText;
			if (canvasPopup == null)
            {
				canvasPopup = GameObject.Instantiate(Resources.Load<GameObject>("PrefabTemplate/CanvasPopup")).GetComponent<Canvas>();
				canvasPopup.name = "Canvas Popup";
            }
			var spawned = GameObject.Instantiate(prefab, canvasPopup.transform);
			spawned.name = selectedPopup.Name;
			spawned.AddComponent(selectedPopup);
		}
	}
	
	void CheckEventSystem()
	{
		var eventSystem = GameObject.FindObjectOfType<EventSystem>();
		if (eventSystem == null)
		{
			var spawned = GameObject.Instantiate(Resources.Load<GameObject>("PrefabTemplate/EventSystem"));
			spawned.name = "EventSystem";
        }
	}

	public static List<Type> GetDerivedClasses<TBase>()
	{
		var assembly = System.Reflection.Assembly.GetAssembly(typeof(Popup));  // Assembly.GetExecutingAssembly();

        var derivedClasses = assembly.GetTypes()
										.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TBase)))
										.ToList();

		return derivedClasses;
	}

	public string GetInputText()
	{
		return inputText;
	}

	public static void SaveScriptData(string content, string fileName, string pathFromAsset)
	{
		string path = Path.Combine(Application.dataPath, pathFromAsset, fileName + ".cs");
		string directory = Path.Combine(Application.dataPath, pathFromAsset);
		try
		{
			if (Directory.Exists(directory) == false)
            {
				Directory.CreateDirectory(directory);
			}
			File.WriteAllText(path, content);
			Debug.Log("File saved successfully at: " + path);
			string assetPath = $"Assets/{pathFromAsset}/{fileName}.cs";
			AssetDatabase.ImportAsset(assetPath);
		}
		catch (IOException e)
		{
			Debug.LogError("Failed to save file: " + e.Message);
		}
	}
}

public class PackageTab : DarkcupTab
{
	public PackageTab(DarkcupWindow pw) : base(pw) { }

    public override void ShowTabLeft(int width)
    {
            
    }

    public override void ShowTabMain(int width)
	{
		EditorGUILayout.Separator();

		GUILayout.Label("Particle UI", EditorStyles.boldLabel);
		EditorGUILayout.Separator();

		GUILayout.Label("\"com.coffee.ui - effect\": \"https://github.com/mob-sakai/UIEffect.git\",", EditorStyles.label);

		if (GUILayout.Button("Add Particle UGUI", buttonWidth))
		{
			AddGitPackage("", "");
			//UnityEngine.Debug.LogError("this is refresh!!");
		}

		if (GUILayout.Button("PIKACHU!!!", buttonWidth))
		{
			UnityEngine.Debug.LogError("this is refresh!!");
		}

		EditorGUILayout.BeginVertical();

		// buttons
		EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();

		if (GUILayout.Button("PIKACHU!!!", buttonWidth))
		{
			UnityEngine.Debug.LogError("this is refresh!!");
		}

		EditorGUI.EndChangeCheck();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
	}

	public static void AddGitPackage(string packageName, string githubPath)
	{
		packageName = "com.coffee.ui-effect";
		githubPath = "https://github.com/mob-sakai/UIEffect.git";

		string manifestPath = "Packages/manifest.json";
		string jsonContent = File.ReadAllText(manifestPath);

		if (!jsonContent.Contains("\"dependencies\""))
		{
			Debug.LogError("No dependencies section found in manifest.json.");
			return;
		}
		if (!jsonContent.Contains(packageName))
		{
			
			int insertIndex = jsonContent.IndexOf("\"dependencies\"") + "\"dependencies\"".Length;
			string newDependency = $",\n    \"{packageName}\": \"{githubPath}\"";
			jsonContent = jsonContent.Insert(insertIndex, newDependency);
			//File.WriteAllText(manifestPath, jsonContent);
			//AssetDatabase.Refresh();
			Debug.Log(jsonContent);

			Debug.Log($"Git package {packageName} added successfully!");
		}
		else
		{
			Debug.LogWarning($"Package {packageName} already exists in dependencies.");
		}
	}
}

public class ThuMoVitTab : DarkcupTab
{
	public bool toggleState = false;
	public ThuMoVitTab(DarkcupWindow pw) : base(pw) { }
	public override void ShowTabMain(int i)
	{
			
	}

    public override void ShowTabLeft(int width)
    {
            
    }
}

public class MeoDiHiaTab : DarkcupTab
{
	public MeoDiHiaTab(DarkcupWindow pw) : base(pw) { }

    public override void ShowTabLeft(int width)
    {
		int selection = 0;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical(GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));
		EditorGUILayout.BeginVertical("box");
		var SP1 = EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));

		string[] texts = new string[]
		{
			"item1", "item2", "item3"
		};
		var prev = selection;
		selection = GUILayout.SelectionGrid(selection, texts, 1, GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));
		if (prev != selection)
		{
			GUI.FocusControl("ID");
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
	}

    public override void ShowTabMain(int i)
	{
		EditorGUILayout.Separator();

		GUILayout.Label("Welcome to the MeoDiHiaTab!", EditorStyles.boldLabel);
		EditorGUILayout.Separator();

		GUILayout.Label("Select one of the Pikachu options below:", EditorStyles.label);
			
		EditorGUILayout.Separator();
		GUILayout.Label("End of options.", EditorStyles.helpBox);
	}
}

public class DarkcupWindow : EditorWindow
{
	public const int LEFT_PANEL_WIDTH = 200;
	public const int BUTTON_WIDTH = 75;

	public bool isLoading = false;

	private Type[] types = new Type[] {
		typeof(PopupTab),
		typeof(PackageTab),
		typeof(ThuMoVitTab),
		typeof(MeoDiHiaTab)
	};

	private Dictionary<int, DarkcupTab> dicTab = new Dictionary<int, DarkcupTab>();
	private string[] tabnames;
	private int currentTab;

	[MenuItem("Darkcup/Darkcup Window")]
	public static void ShowMainWindow()
	{
		DarkcupWindow window = GetWindow<DarkcupWindow>();
		window.titleContent = new GUIContent("Darkcup Window");
		window.Show();
	}

	public void OnGUI()
	{
		GUI.SetNextControlName("Toolbar");

		if (tabnames == null || tabnames.Length == 0)
        {
			tabnames = new string[types.Length];
            for (int i = 0; i < tabnames.Length; i++)
            {
				tabnames[i] = types[i].Name.Replace("Tab","");
			}
		}
		currentTab = GUILayout.SelectionGrid(currentTab, tabnames, 7);
		GUILayout.Box(" ", GUILayout.ExpandWidth(true));
		EditorGUILayout.Separator();

		if (dicTab.ContainsKey(currentTab) == false)
		{
			var newTab = (DarkcupTab)Activator.CreateInstance(types[currentTab], new object[] { this });
			dicTab.Add(currentTab, newTab);
		}

		EditorGUI.BeginDisabledGroup(isLoading);

		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();

		GUILayout.BeginVertical(); // Left Column
		dicTab[currentTab].ShowTabLeft(300);
		GUILayout.EndVertical();

		GUILayout.BeginVertical(); // Right Column
		dicTab[currentTab].ShowTabMain(300);
		GUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();

		EditorGUI.EndDisabledGroup();
	}
}
#endif