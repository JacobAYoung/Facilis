using System.Collections.Generic;

namespace Facilis.LineItems.Models
{
    public class Item
    {
        public Item(int id, string name, int parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Children = new List<Item>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int ParentId { get; private set; }
        public Item Parent { get; set; }
        public List<Item> Children { get; set; }
    }
}