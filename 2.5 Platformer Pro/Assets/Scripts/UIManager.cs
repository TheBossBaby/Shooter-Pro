using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Serialized Private Attribute
        [SerializeField]
        Text _coinText;
    #endregion

    #region Public Method
        public void UpdateCoinText(int coinNumber) => _coinText.text = coinNumber.ToString();
    #endregion
}
