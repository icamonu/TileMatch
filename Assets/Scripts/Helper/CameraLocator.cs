using ScriptableObjects;
using UnityEngine;

namespace Helper
{
    public class CameraLocator: MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameSettings gameSettings;

        private void OnEnable()
        {
            mainCamera.orthographicSize = gameSettings.columns + 2;
            Vector3 cameraPosition = new Vector3((gameSettings.columns-1) / 2f, gameSettings.rows / 2f, -10f);
            mainCamera.transform.position = cameraPosition;
        }
    }
}