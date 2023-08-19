namespace My_Ap.BL
{
    public class User : Authentication
    {
       private String? id;
       private String? name;
       private String? userId;
       private String? address;
       private DateTime? createDate;
       private String? ibn;
       private DateTime? dob;
      public  User(String id,String name,
         String userId,
         String address,
         DateTime createDate,
         String ibn,
         DateTime dob, String email,String password ) : base(email:email, password:password)
        {
            this.id = id;
            this.Name = name;
            this.UserId = userId;
            this.Address = address;
            this.CreateDate = createDate;
            this.Ibn = ibn;
            this.Dob = dob;
            this.email = email;
          }
        public User():base()
        {

        }
     public  String? Id{
            get => id; set => id = value;}
        public string? Address { get => address; set => address = value; }
        public string? UserId { get => userId; set => userId = value; }
        public string? Name { get => name; set => name = value; }
        public DateTime? CreateDate { get => createDate; set => createDate = value; }
        public string? Ibn { get => ibn; set => ibn = value; }
        public DateTime? Dob { get => dob; set => dob = value; }
    }
}
