using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    public GameObject orderPrefab;
    public Transform ordersContainer;

    public void CreateOrderUI(CreateOrderForTests.Order order)
    {
        // Создаем новый экземпляр префаба заказа

        GameObject orderObject = Instantiate(orderPrefab, ordersContainer);

        // Получаем ссылки на объекты Image и Text в префабе

        //Image orderImage = orderObject.GetComponentInChildren<Image>();
        Text nameText = orderObject.transform.Find("NameText").GetComponent<Text>();
        Text priceText = orderObject.transform.Find("PriceText").GetComponent<Text>();
        Text timerText = orderObject.transform.Find("TimerText").GetComponent<Text>();

        // Устанавливаем значения для каждого объекта

        //orderImage.image = order.sprite;
        nameText.text = order.name;
        priceText.text = order.price;
        timerText.text = order.timer;
    }
}
