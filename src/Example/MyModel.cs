using System.Collections.Generic;

namespace ConsoleApp1
{
    class MyModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; } 
        public MyStates States { get; set; }
        
        public List<MyModel> GetMyModelList() => new List<MyModel>
        {
            new MyModel()
            {
                Id = 1,
                FirstName = "Omid",
                LastName = "Hosseini",
                Age = 31,
                States = MyStates.New
            },
            new MyModel()
            {
                Id = 2,
                FirstName = "Ali",
                LastName = "Hosseini",
                Age = 35,
                States = MyStates.Ordered
            },
            new MyModel()
            {
                Id = 3,
                FirstName = "Roxana",
                LastName = "Khaqani",
                Age = 29,
                States = MyStates.Paid
            }
        };
    }
}