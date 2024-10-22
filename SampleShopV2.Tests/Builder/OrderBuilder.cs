using Domain.Entities;

namespace SampleShopV2.Tests.Builder;

public static class OrderBuilder
{
    public static Order BuildOrder(int? numberItems)
    {
        var order = new Order(Faker.Name.FullName(), Faker.Address.StreetAddress());

        if (numberItems is > 0)
        {
            for (int i = 0; i < numberItems; i++)
            {
                order.AddOrderItem($"Name test {i}", $"Brand test {i}", 20 * i+1, 1);
            }
        }

        return order;
    }

    public static Order BuildOrder(DateTime orderDate, int? numberItems)
    {
        var order = new Order(Faker.Name.FullName(), Faker.Address.StreetAddress(), orderDate);

        if (numberItems is > 0)
        {
            for (int i = 0; i < numberItems; i++)
            {
                order.AddOrderItem($"Name test {i}", $"Brand test {i}", 20 * i + 1, 1);
            }
        }

        return order;
    }
}

