using System;
using System.Collections.Generic;

// Абстрактный класс для адреса
public abstract class Address    //использование абстрактных классов
{
    public string Street { get; set; }  //использование принципов инкапсуляции
    public string City { get; set; }    //использование свойств
    public string PostalCode { get; set; }

    public override string ToString()    
    {
        return $"{Street}, {City}, {PostalCode}";
    }
}

// Класс для домашнего адреса
public class HomeAddress : Address  //использование наследования
{
    public string ApartmentNumber { get; set; }
}

// Класс для адреса пункта выдачи
public class PickPointAddress : Address
{
    public string PickPointName { get; set; }
}

// Класс для адреса магазина
public class ShopAddress : Address
{
    public string ShopName { get; set; }
}

//использование минимум 4 собственных классов

// Абстрактный класс доставки
public abstract class Delivery
{
    public Address Address { get; set; }
    public abstract void Deliver();
}

// Доставка на дом
public class HomeDelivery : Delivery
{
    public string CourierName { get; set; }

    public override void Deliver() //использование переопределений методов/свойств
    {
        Console.WriteLine($"Доставка на дом курьером {CourierName} по адресу: {Address}");
    }
}

// Доставка в пункт выдачи
public class PickPointDelivery : Delivery
{
    public string CompanyName { get; set; }
    public string PickUpCode { get; set; }

    public override void Deliver()
    {
        Console.WriteLine($"Доставка в пункт выдачи {CompanyName}, код: {PickUpCode}, адрес: {Address}");
    }
}

// Доставка в магазин
public class ShopDelivery : Delivery
{
    public string ShopId { get; set; }

    public override void Deliver()
    {
        Console.WriteLine($"Доставка в магазин {ShopId}, адрес: {Address}");
    }
}

// Класс продукта
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Класс заказа
public class Order<TDelivery, TStruct>
    where TDelivery : Delivery, new()
{
    public TDelivery Delivery { get; set; }
    public int Number { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; private set; } = new List<Product>(); // Список продуктов

    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.Address.ToString());
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void ProcessOrder()
    {
        Console.WriteLine($"Обработка заказа #{Number} - {Description}");
        Delivery.Deliver();
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        var order = new Order<HomeDelivery, object>
        {
            Number = 1,
            Description = "Заказ еды",
            Delivery = new HomeDelivery
            {
                Address = new HomeAddress
                {
                    Street = "Ленина",
                    City = "Москва",
                    PostalCode = "123456",
                    ApartmentNumber = "5"
                },
                CourierName = "Иван"
            }
        };

        order.AddProduct(new Product { Name = "Пицца", Price = 500 });
        order.AddProduct(new Product { Name = "Кола", Price = 150 });

        order.ProcessOrder();
        order.DisplayAddress();
    }
}