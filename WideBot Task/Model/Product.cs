namespace WideBot_Task.Model
{
	public class Product
	{
        public int Id { get; set; }
        public string  Title { get; set; }
        public float price { get; set; }
        public string  Image { get; set; }

		public string Category { get; set; }
       // public int IdCategory { get; set; }
					
		public Rating rating { get; set; }
		public string description { get; set; }

	}
}
