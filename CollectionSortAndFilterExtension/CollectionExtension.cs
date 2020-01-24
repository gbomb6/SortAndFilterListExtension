using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CollectionFilterAndSortApplication
{
    public static class CollectionSortAndFilterExtension
    {
        // Example of use case for extension.

        //public class ExampleObject
        //{
        //    public int ExampleInt { get; set; }
        //    public string ExampleString { get; set; }
        //    public decimal ExampleDecimal { get; set; }

        //    public void Test()
        //    {
        //        var exampleObject1 = new ExampleObject
        //        {
        //            ExampleInt = 3,
        //            ExampleString = "testString1",
        //            ExampleDecimal = 4.23m
        //        };

        //        var exampleObject2 = new ExampleObject
        //        {
        //            ExampleInt = 71,
        //            ExampleString = "testString2",
        //            ExampleDecimal = 9.99m
        //        };

        //        var exampleObject3 = new ExampleObject
        //        {
        //            ExampleInt = 1,
        //            ExampleString = "testString3",
        //            ExampleDecimal = 2.56m
        //        };

        //        var exampleObject4 = new ExampleObject
        //        {
        //            ExampleInt = 6,
        //            ExampleString = "testString4",
        //            ExampleDecimal = 6.00m
        //        };

        //        var list = new List<ExampleObject> { exampleObject1, exampleObject2, exampleObject3, exampleObject4 };

        //        var sortedAndFilteredList = CollectionSortAndFilterExtension.FilterAndOrderListByParameters(list, "ExampleInt", "1", "ExampleDecimal", true);

        //        This returns a list with only exampleObject2 and exampleObject3 since they are the only two containing a 1 in their ExampleInt property.It will be sorted exampleObject3 then exampleObject2 because I included the argument to order by "ExampleDecimal" by Ascending.
        //    }
        //}


        /// <summary>
        /// Search and/or sort collection by named property in the collection object. 
        ///
        /// Passing empty string as search parameters returns unfiltered list. Passing empty string as orderBy parameter doesn't do any sorting and returns original list.
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="entries">collection object</param>
        /// <param name="searchBy">object property name to search by</param>
        /// <param name="searchString">value to search for on object property</param>
        /// <param name="orderBy">object property to sort collection by</param>
        /// <param name="orderAscending">sort collection by ascending, set to true, otherwise will sort by descending</param>
        /// <returns>True if value is not null and elements exist in collection</returns>
        public static IList<T> FilterAndOrderListByParameters<T>(this ICollection<T> entries, string searchBy = null, string searchString = null, string orderBy = null, bool orderAscending = false)
        {
            if (entries == null || entries.Count <= 0)
            {
                return new List<T>();
            }

            if (searchBy != null && searchBy.Length > 0 && searchString != null && searchString.Length > 0)
            {
                var searchMatches = new List<T>();
                var propertyPath = searchBy.Split('.');

                if (entries.FirstOrDefault()?.GetType().GetProperty(propertyPath[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static) == null)
                {
                    return new List<T>();
                }

                foreach (var entry in entries)
                {
                    if (propertyPath.Length > 1)
                    {
                        var partialPath = entry.GetType().GetProperty(propertyPath[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(entry);

                        if (partialPath.GetType().GetProperty(propertyPath[1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static) != null)
                        {
                            var newEntry = partialPath.GetType().GetProperty(propertyPath[1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(partialPath);
                            if (newEntry != null && newEntry.ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                searchMatches.Add(entry);
                            }
                        }
                    }

                    else
                    {
                        var newValue = entry.GetType().GetProperty(searchBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(entry);
                        if (newValue != null && newValue.ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            searchMatches.Add(entry);
                        }
                    }
                }
                entries = searchMatches;
            }

            if (orderBy != null && orderBy.Length > 0)
            {
                var sortColumn = entries.FirstOrDefault()?.GetType().GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (sortColumn != null)
                {
                    entries = orderAscending ? entries.OrderBy(e => sortColumn.GetValue(e, null)).ToList() : entries.OrderByDescending(e => sortColumn.GetValue(e, null)).ToList();
                }
            }

            return entries.ToList();
        }
    }
}