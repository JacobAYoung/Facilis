﻿using Facilis.LineItems.Models;
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
            items.CreateParentChildHierarchyList();
            //items.CreateParentChildHierarchyList(0);
        }
    }

    internal static class ExtensionHierarchyList
    {
        public static void CreateParentChildHierarchyList<T>(
            this IEnumerable<T> items) where T : Item
        {
            //Assumption: In the following example, the Root Item always has the ParentId set to 0.
            items.Where(item => item.ParentId == 0)
                    //I added the order by to make sure, atleast in the provided case, that we ordered by the first parent added to the list.
                    .OrderBy(item => item.Id)
                    .ToList()
                    .ForEach(item => SetChildren(item.Id, item.ParentId, item, items.ToList()));
        }

        private static void SetChildren<T>(int itemId, int parentId, T parent, List<T> itemList, int precedenceCounter = 1) where T : Item
        {
            //Get all the children under the same ParentId
            List<Item> children = itemList.Where(x => x.ParentId == itemId).ToList<Item>();

            //Save the list of children to that parent
            parent.Children = children;

            //Write the line
            Console.WriteLine($"{string.Join("", Enumerable.Repeat("-", precedenceCounter))} {parent.Name}");

            //If any children then recurse through them and find if they have any children. 
            //Also for any child, set their parent
            if (children.Any())
            {
                precedenceCounter += 1;
                children.ForEach(child =>
                {
                    SetChildren(child.Id, parentId, child, itemList.ToList<Item>(), precedenceCounter);
                    SetParent(parent, child);
                });
            }
        }

        private static void SetParent(Item parent, Item child)
        {
            child.Parent = parent;
        }

        #region Other way of doing it
        //Assuming we know we are going to use the Item object.
        public static void CreateParentChildHierarchyList<T>(
            this IEnumerable<T> items, int otherExample)
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

        private static void SetChildren(Item parent, IEnumerable<Item> itemList, int precedenceCounter = 1)
        {
            //Get all the children under the same ParentId
            List<Item> children = itemList.Where(x => x.ParentId == parent.Id).ToList();

            //Save the list of children to that parent
            parent.Children = children;

            //Write the line
            Console.WriteLine($"{string.Join("", Enumerable.Repeat("-", precedenceCounter))} {parent.Name}");

            //If any children then recurse through them and find if they have any children. 
            //Also for any child, set their parent
            if (children.Any())
            {
                precedenceCounter += 1;
                children.ForEach(child =>
                {
                    SetChildren(child, itemList, precedenceCounter);
                    SetParent(parent, child);
                });
            }
        }
        #endregion
    }
}