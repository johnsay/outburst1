
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public class DragDummy : MonoBehaviour
    {
        [SerializeField] private Image _icon;


        public void Initialize(Sprite icon)
        {
            
            _icon.sprite = icon;
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
        }

        public void Reset()
        {
            //hide
            gameObject.SetActive(false);
        }


    }
}


