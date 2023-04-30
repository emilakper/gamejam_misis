using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrderForTests : MonoBehaviour
{
    // Start is called before the first frame update
    public OrderUI orderUI;

    public struct Order
    {
        public string name;
        public string price;
        public string timer;
        public string sprite;
    }
    public void GenerateOrder()
    {
        // Создаем экземпляр класса Order
        Order order;
        order.name = "Coffe";
        order.price = "10";
        order.timer = "60";
        order.sprite = "CoffeeSprite";

        // Создаем экземпляр для заказа в UI
        orderUI.CreateOrderUI(order);
    }

}
