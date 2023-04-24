namespace StoreModuleClient.Models;

public partial class Store
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public int EmployeeId { get; set; }

    public int AllCost { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
