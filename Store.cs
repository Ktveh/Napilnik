using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		Store store = new Store();
		store.Work();
	}
}

public class Store
{
	public void Work()
	{
		Good iPhone12 = new Good("IPhone 12");
		Good iPhone11 = new Good("IPhone 11");

		Warehouse warehouse = new Warehouse();

		Shop shop = new Shop(warehouse);

		warehouse.Delive(iPhone12, 10);
		warehouse.Delive(iPhone11, 1);

		//Вывод всех товаров на складе с их остатком
		warehouse.ShowGoods();

		Cart cart = shop.Cart();
		cart.Add(iPhone12, 4);
		cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

		//Вывод всех товаров в корзине
		cart.ShowGoods();

		Console.WriteLine(cart.Order().Paylink);

		cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
	}
}

public class Good
{
	private string _name;

	public string Name => _name;

	public Good(string name)
	{
		_name = name;
	}
}

public class Container
{
	protected Dictionary<Good, int> Goods;

	public Container()
	{
		Goods = new Dictionary<Good, int>();
	}

	public void ShowGoods()
	{
		foreach (KeyValuePair<Good, int> element in Goods)
		{
			Console.WriteLine($"{element.Key.Name}, количество: {element.Value}");
		}
	}
}

public class Cart : Container
{
	private Warehouse _warehouse;

	public string Paylink;

	public Cart(Warehouse warehouse)
	{
		Paylink = "";
		_warehouse = warehouse;
	}

	public void Add(Good good, int count)
	{
		if (_warehouse.CheckAvailability(good, count))
			Goods[good] = count;
		else
			throw new InvalidOperationException();
	}

	public Cart Order()
	{
		foreach (KeyValuePair<Good, int> element in Goods)
		{
			_warehouse.Ship(element.Key, element.Value);
		}
		Goods.Clear();
		Paylink = $"Order:{DateTime.Now}";
		return this;
	}
}

public class Warehouse : Container
{
	public void Delive(Good good, int count)
	{
		Goods[good] = count;
	}

	public bool CheckAvailability(Good good, int count)
	{
		if (Goods.ContainsKey(good) && Goods[good] >= count)
			return true;
		return false;
	}

	public void Ship(Good good, int count)
	{
		if (CheckAvailability(good, count))
			Goods[good] -= count;
		else
			throw new InvalidOperationException();
	}
}

public class Shop
{
	private Warehouse _warehouse;

	public Shop(Warehouse warehouse)
	{
		_warehouse = warehouse;
	}

	public Cart Cart()
	{
		return new Cart(_warehouse);
	}
}