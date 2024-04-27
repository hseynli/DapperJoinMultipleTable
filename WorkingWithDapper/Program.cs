using Dapper;
using Microsoft.Data.SqlClient;

using SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=BikeStores;Integrated Security=True;Trust Server Certificate=True");
var sql = """
    select * from sales.customers c
    join sales.orders o
    on c.customer_id = o.customer_id
    """;

var customer = await connection.QueryAsync<Customer, Order, Customer>(sql, (c, o) =>
{
    c.Customer_Id = o.Customer_Id;
    c.Orders = o;
    return c;
}, splitOn: "customer_id");

customer.ToList().ForEach(customer => Console.WriteLine($"{customer.First_Name} {customer.Orders.Order_Date}"));



Console.WriteLine("\nDone!");

class Customer
{
    public int Customer_Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip_Code { get; set; }
    public Order Orders { get; set; }
}

class Order
{
    public int Order_Id { get; set; }
    public int Customer_Id { get; set; }
    public int Order_Status { get; set; }
    public DateTime Order_Date { get; set; }
    public DateTime Required_Date { get; set; }
    public DateTime Shipped_Date { get; set; }
    public int Store_Id { get; set; }
    public int Staff_Id { get; set; }
    public Customer Customer { get; set; }
}