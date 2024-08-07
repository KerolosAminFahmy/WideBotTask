using System.ComponentModel.DataAnnotations;

namespace WideBot_Task.Model
{
	public class ShoppingCart
	{
		[Key]
        public int Id { get; set; }
        public string UserId { get; set; } 

		public ApplicationUser User { get; set; }

		public int ProductId { get; set; }
        public int Quantity { get; set; }
		public float price {  get; set; }
    }
}
