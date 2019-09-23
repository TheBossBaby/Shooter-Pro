using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform _respawnPoint;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            CharacterController _characterController = p.GetComponent<CharacterController>();
            p.PlayerDamage();
            _characterController.enabled = false;
            p.transform.position = _respawnPoint.position;
            _characterController.enabled = true;
        }
    }
}
