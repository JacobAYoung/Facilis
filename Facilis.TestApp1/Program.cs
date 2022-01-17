using Facilis.LineItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.LineItems
{
    internal class Program
    {
        private static void Main()
        {
            // Problem: Write a GENERIC EXTENSION METHOD that will Convert a flat list of items into a Hierarchy
            // Populating Parent and Children Properties of Item class
            // The ItemId, ParentId, Parent and Children properties needs to be provided as an input to the Extension Method to make the extension
            // re-usable for different cases.


            // In the following example, the Root Item always have the ParentId set to 0

            var parent1 = new Item(1, "I am first Parent (Root Item)", 0);
            var child1OfParent1 = new Item(2, "I am first Child of Parent1", 1);
            var child2OfParent1 = new Item(3, "I am Second Child of Parent1", 1);
            var child3OfParent1 = new Item(4, "I am Third Child of Parent1", 1);
            var grandChild1OfChild1OfParent1 = new Item(5, "I am the first Child of Child1OfParent1", 2);

            var parent2 = new Item(6, "I am Second Parent (Root Item)", 0);
            var child1OfParent2 = new Item(7, "I am first Child of Parent2", 6);
            var grandChild1OfChild1OfParent2 = new Item(8, "I am the first Child of Child1OfParent2", 7);

            var items = new List<Item>
            {
                parent1,
                child1OfParent1,
                child2OfParent1,
                child3OfParent1,
                grandChild1OfChild1OfParent1,
                parent2,
                child1OfParent2,
                grandChild1OfChild1OfParent2
            };

            // Print all the names of the items
            PrintItems(items);

            Console.Read();
        }

        /// <summary>
        /// Recursively print all the items and their child items. Refer to output.jpg for expected output
        /// </summary>
        private static void PrintItems(IEnumerable<Item> items, string precedence = "-")
        {
            items.CreateParentChildHierarchyList(1, 0, items.First(), null);
            items.CreateParentChildHierarchyList();
        }
    }

    internal static class ExtensionHierarchyList
    {
        public static void CreateParentChildHierarchyList<T>(
            this IEnumerable<T> items, int itemId, int parentId, T parent, List<T> children)
        {
            Type d1 = typeof(T);
            var itemIdProperty = d1.GetProperty("Id").GetValue(parent);
            //var test = itemIdProperty.GetValue(parent);
        }

        #region Item object known
        //Assuming we know we are going to use the Item object.
        public static void CreateParentChildHierarchyList<T>(
            this IEnumerable<T> items)
        {
            if (items is IEnumerable<Item> && items.Any())
            {
                IEnumerable<Item> itemList = items.OfType<Item>();
                //Assumption: In the following example, the Root Item always has the ParentId set to 0.
                itemList.Where(item => item.ParentId == 0)
                    //I added the order by to make sure, atleast in the provided case, that we ordered by the first parent added to the list.
                    .OrderBy(item => item.Id)
                    .ToList()
                    .ForEach(item => SetChildren(item, itemList));
            }
        }

        private static void SetChildren(Item item, IEnumerable<Item> itemList, int precedenceCounter = 1)
        {
            //Get all the children under the same ParentId
            List<Item> children = itemList.Where(x => x.ParentId == item.Id).ToList();

            //Save the list of children to that parent
            item.Children = children;

            //Write the line
            Console.WriteLine($"{string.Join("", Enumerable.Repeat("-", precedenceCounter))} {item.Name}");

            //If any children then recurse through them and find if they have any children. 
            //Also for any child, set their parent
            if (children.Any())
            {
                precedenceCounter += 1;
                children.ForEach(child =>
                {
                    SetChildren(child, itemList, precedenceCounter);
                    SetParent(item, child);
                });
            }
        }

        private static void SetParent(Item parent, Item child)
        {
            child.Parent = parent;
        }
        #endregion
    }
}