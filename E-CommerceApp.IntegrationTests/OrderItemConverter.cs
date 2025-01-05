using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class OrderConverter : JsonConverter<IOrder>
{
    public override IOrder ReadJson(JsonReader reader, Type objectType, IOrder existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        // Deserialize into the concrete Order class
        var order = jsonObject.ToObject<Order>();

        if (order == null)
        {
            return null; // Return null if the order is not deserialized
        }

        return (IOrder)order; // Explicitly cast to IOrder
    }

    public override void WriteJson(JsonWriter writer, IOrder value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}