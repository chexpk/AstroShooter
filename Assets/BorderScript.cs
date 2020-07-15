using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour {
    // срабатывает при выходе объекта за границы
    private void OnTriggerExit (Collider other) {
        Destroy (other.gameObject); //уничтожаем объект
    }
}