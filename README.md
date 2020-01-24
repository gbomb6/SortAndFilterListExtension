Extension used to pick any property of an object and pass in the object name to filter by a value on that object or sort by that value.

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