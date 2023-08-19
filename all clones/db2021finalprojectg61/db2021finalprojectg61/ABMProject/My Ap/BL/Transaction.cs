namespace My_Ap.BL

{
    public class Transaction
    {
        String id;
        String userId;
        String bn;
        int payment;
        String? title;
        String? description;
        DateTime dateTime;
        public Transaction(String id, String userId, String bn, int payment, String title, String description,DateTime dateTime)
        {
            this.dateTime = dateTime;
            this.Id = id;
            this.UserId = userId;
            this.Bn = bn;
            this.Payment = payment;
            this.Title = title;
            this.Description = description;
            this.Id = id;
            this.UserId = userId;
        }

        public String Id { get => id; set => id=value; }
        public string Bn { get => bn; set => bn = value; }
        public int Payment { get => payment; set => payment = value; }
        public string? Description { get => description; set => description = value; }
        public string? Title { get => title; set => title = value; }
        public string UserId { get => userId; set => userId = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
