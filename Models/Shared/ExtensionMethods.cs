using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Models;

namespace Models.Shared
{
    public static class ExtensionMethods
    {
        public static int AttributeAsInt(this XmlNode node, string attributeName)
        {
            return Convert.ToInt32(AttributeAsString(node, attributeName));
        }
        public static string AttributeAsString(this XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];
            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist");
            }
            return attribute.Value;
        }

        public static string StringValueOf(this JObject jsonObject, string key)
        {
            return jsonObject[key].ToString();
        }
        public static string StringValueOf(this JToken jsonToken, string key)
        {
            return jsonToken[key].ToString();
        }
        public static int IntValueOf(this JToken jsonToken, string key)
        {
            return Convert.ToInt32(jsonToken[key]);
        }
        public static PlayerAttribute GetAttribute(this LivingEntity entity, string attributeKey)
        {
            return entity.Attributes.First(p =>p.Key.Equals(attributeKey, StringComparison.CurrentCultureIgnoreCase));
        }

        public static List<GameItem> ItemsThatAre(this IEnumerable<GameItem> inventory, GameItem.ItemCategory category)
        {
            return inventory.Where(item => item.Category == category).ToList();
        }
    }
}
