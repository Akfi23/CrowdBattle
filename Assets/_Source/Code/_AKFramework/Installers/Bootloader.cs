 using Cysharp.Threading.Tasks;
 using Sirenix.OdinInspector;
 using UnityEngine;
 using UnityEngine.AddressableAssets;
 using UnityEngine.ResourceManagement.AsyncOperations;
 using UnityEngine.ResourceManagement.ResourceProviders;
 using UnityEngine.SceneManagement;

 public class Bootloader : MonoBehaviour
 {
     [Required] [SerializeField]
     private AssetReference contextScene;
     private AsyncOperationHandle<SceneInstance> operation;
     
     protected async void Awake()
     {
         var currentScene = SceneManager.GetActiveScene();
            
         operation = contextScene.LoadSceneAsync(LoadSceneMode.Additive);
         await operation.ToUniTask();
         
         await UniTask.NextFrame();
         await SceneManager.UnloadSceneAsync(currentScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects).ToUniTask();
     }
 }
