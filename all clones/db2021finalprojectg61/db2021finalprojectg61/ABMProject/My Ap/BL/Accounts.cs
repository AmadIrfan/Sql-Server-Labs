namespace My_Ap.BL
{
    public class Accounts
    {
        String id;
        String name;
        String description;
        String type;
        DateTime createDate;
        User user;
        public Accounts(String name,String id,String description, DateTime createDate,String type)
        {
            this.Name = name;
            this.Id = id;
            this.Description = description;
            this.Type = type;
            this.CreateDate = createDate;

        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public User User { get => user; set => user = value; }
    }
}
