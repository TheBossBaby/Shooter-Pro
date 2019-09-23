using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Serialized Private Attribute
        [SerializeField]
        Text _coinText;
        [SerializeField]
        Sprite _emptyHeart;
        [SerializeField]
        GameObject[] _liveImage;
    #endregion

    #region Public Method
        public void UpdateCoinText(int coinNumber) => _coinText.text = coinNumber.ToString();
        public void UpdateLifeImage(int leftLife) 
        {
            if (leftLife < 0)
                return;
            else{
                // _liveImage[leftLife].SetActive(false);
                _liveImage[leftLife].GetComponent<Image> ().sprite = _emptyHeart;
            }
        }
    #endregion
}
